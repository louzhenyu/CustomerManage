using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPOS.BS.BLL;
using CPOS.BS.Entity;
using CPOS.Common;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.DTO.Module.Order.Delivery.Response;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.DTO.Module.PA.Request;
using JIT.CPOS.DTO.Module.PA.Response;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using log4net.Repository.Hierarchy;
using JIT.CPOS.DTO.Module.SapMessageApi.Response;
using System.Threading;
using System.Data;

namespace JIT.CPOS.BS.BLL.PA
{
    public class PAAppApiBLL
    {
        // 请求超时时间  秒
        private readonly static int _timeout = 10;
        // 令牌
        private static string Acc_Token
        {
            get
            {
                string _acc_Token = string.Empty;
                // 从缓存读取
                string result = HttpHelper.GetData(string.Empty, string.Format("{1}/keyvalue/get/{0}", "PAToken", ConfigHelper.GetAppSetting("QueueAddr", "http://182.254.242.12:8011")));

                // 序列化
                RedisOpenApiRD<object> obj = result.DeserializeJSONTo<RedisOpenApiRD<object>>();
                if (obj.Code == (int)ResponseCode.Success)
                {
                    _acc_Token = obj.Result.ToString();
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "从缓存读取PA的acc_token失败,返回结果:" + result
                    });
                }

                // 先从缓存拿token，拿不到再获取
                if (string.IsNullOrEmpty(_acc_Token))
                {
                    _acc_Token = ReLoadToken();
                }
                return _acc_Token;
            }
        }

        /// <summary>
        /// 重新加载令牌
        /// </summary>
        private static string ReLoadToken()
        {
            string acc_Token = string.Empty;
            // 拼接参数
            string url = string.Format("{0}/oauth/oauth2/access_token", ConfigHelper.GetAppSetting("PAApiDomain", "https://test-api.pingan.com.cn:20443"));
            string data = string.Format("client_id={0}&grant_type=client_credentials&client_secret={1}"
                , ConfigHelper.GetAppSetting("PAAppID", "P_CAICAI"), ConfigHelper.GetAppSetting("PAAppPwd", "751jctrP"));
            try
            {
                string res = HttpHelper.GetData(data, url);

                // 记录日志
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("调用PA获取token接口：{0},请求参数：{1}，返回数据：{2}", url, data, res)
                });
                GetAccessTokenRD acctokenrd = res.DeserializeJSONTo<GetAccessTokenRD>();
                if (acctokenrd.ret.Equals("0"))
                {
                    acc_Token = acctokenrd.data.access_token;
                    string redisUrl = string.Format("{2}/keyvalue/set/{0}/{1}", "PAToken", acctokenrd.data.access_token, ConfigHelper.GetAppSetting("QueueAddr", "http://182.254.242.12:8011"));
                    string result = HttpHelper.GetData(string.Empty, redisUrl);
                    // 记录日志
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("调用redis写缓存接口：{0}", redisUrl)
                    });
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = "刷新PA的acc_token失败,返回结果:" + res
                    });
                }
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("调用PA接口异常：{0},请求参数：{1}，异常信息：{2}", url, data, ex)
                });
            }
            return acc_Token;
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="pOrderId">订单ID</param>
        /// <param name="pOpenId">用户ID</param>
        /// <returns></returns>
        public static string GetOrderDetail(string pOrderId, string pOpenId)
        {
            GetOrderInfoRP req = new GetOrderInfoRP()
            {
                merchantCode = ConfigHelper.GetAppSetting("MerchantCode"),
                merchantId = ConfigHelper.GetAppSetting("MerchantId"),
                openId = pOpenId,
                orderNo = pOrderId,
                orderStatus = "null"
            };
            req.GetSecuritySign();

            string reqJson = req.ToJSON();
            PAResponseDetailRD res = GetResponse(reqJson, "/open/appsvr/openapi/app/order/getOrderInfo");
            if (res.CODE.Equals("00"))
            {
                //SaveOrderRD orderRd = res.DATA as SaveOrderRD;

                //// 验证签名防串改
                //if (orderRd.CheckSecuritySign())
                //{
                //}
            }
            return "";
        }

        /// <summary>
        /// 提交订单到平安并拿到返回的预付单号
        /// </summary>
        /// <param name="pOrderInfo"></param>
        /// <returns></returns>
        public static bool SubmitOrderToPA(GetOrderDetailRD pOrderInfo, LoggingSessionInfo pUserInfo)
        {
            bool flag = false;
            try
            {
                // 组装请求数据
                var paUserBll = new PA_UserInfoBLL(pUserInfo);
                PA_UserInfoEntity userInfo = paUserBll.GetByID(pOrderInfo.OrderListInfo.VipID);
                SaveOrderInfoRP orderReq = ConvertToRPOrder(pOrderInfo, userInfo);
                string reqJson = orderReq.ToJSON();
                PAResponseDetailRD res = GetResponse(reqJson, "/open/appsvr/openapi/app/order/savePrepayOrder");
                if (res.CODE.Equals("00"))
                {
                    SaveOrderRD orderRd = res.DATA.ToJSON().DeserializeJSONTo<SaveOrderRD>();

                    // 验证签名防串改
                    //if (orderRd.CheckSecuritySign())
                    //{
                    // 保存预付单号
                    var prepayBll = new PA_PrepayNoBLL(pUserInfo);
                    LogConsole.PrintLog("用户信息:" + userInfo.ToJSON());
                    prepayBll.Create(new PA_PrepayNoEntity()
                    {
                        OrderNo = pOrderInfo.OrderListInfo.OrderCode,
                        CustomerId = ConfigHelper.GetAppSetting("customer_id"),
                        PrepayNo = orderRd.prepayId,
                        TradeType = orderRd.tradeType,
                        OrderId = pOrderInfo.OrderListInfo.OrderID,
                        Field1 = userInfo.OpenId
                    });
                    flag = true;
                    //}
                }
                else
                {
                    // 记录日志
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("调用PA保存预付订单接口失败({1}),返回数据：{0}", res.ToJSON(), Thread.CurrentThread.ManagedThreadId)
                    });
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
            return flag;
        }

        /// <summary>
        /// 通知平安最新的订单信息
        /// </summary>
        /// <param name="pOrderInfo"></param>
        /// <returns></returns>
        public static bool UpdateOrderToPA(GetOrderDetailRD pOrderInfo, LoggingSessionInfo pUserInfo)
        {
            bool flag = false;
            try
            {
                // 组装请求数据
                UpdateOrderInfoRP orderReq = ConvertToUpdateOrderRP(pOrderInfo);
                string reqJson = orderReq.ToJSON();
                PAResponseDetailRD res = GetResponse(reqJson, "/open/appsvr/openapi/app/order/savePrepayOrder");
                if (res.CODE.Equals("00"))
                {
                    // 成功
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
            return flag;
        }

        /// <summary>
        /// 获取指定用户的地址
        /// </summary>
        /// <param name="pOpenID"></param>
        /// <returns></returns>
        public static List<AddressRD> GetAddress(string pOpenID)
        {
            GetAddressRP reqRp = new GetAddressRP()
            {
                openId = pOpenID,
                merchantCode = ConfigHelper.GetAppSetting("MerchantCode", "2016051000000041")
            };
            reqRp.GetSecuritySign();
            List<AddressRD> listaAddress = new List<AddressRD>();
            string reqJson = reqRp.ToJSON();
            //LogConsole.PrintLog("请求数据：" + reqJson);
            PAResponseDetailRD res = GetResponse(reqJson, "/open/appsvr/openapi/app/address/query");
            if (res.CODE.Equals("00"))
            {
                listaAddress = res.DATA.ToJSON().DeserializeJSONTo<List<AddressRD>>();
            }
            return listaAddress ?? new List<AddressRD>();
        }

        /// <summary>
        /// 发起退款请求
        /// </summary>
        /// <param name="pPrepayId"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public static bool RefundAmount(string pPrepayId, string pAmount)
        {
            bool flag = false;
            RefuntAmountRP pRefuntAmountRp = new RefuntAmountRP()
            {
                prepayId = pPrepayId,
                amount = pAmount,
                appId = ConfigHelper.GetAppSetting("MerchantCode"),
                merchantId = ConfigHelper.GetAppSetting("MerchantId")
            };
            pRefuntAmountRp.GetSecuritySign();
            string reqJson = pRefuntAmountRp.ToJSON();
            PAResponseDetailRD res = GetResponse(reqJson, "/");
            //if (res.CODE.Equals("00"))
            //{
            //    listaAddress = res.DATA as List<AddressRD>;
            //}
            //return listaAddress ?? new List<AddressRD>();
            return flag;
        }

        /// <summary>
        /// 佣金分润
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static bool Commission(string pOrderId, LoggingSessionInfo pUserInfo)
        {
            var prepaybll = new PA_PrepayNoBLL(pUserInfo);
            var inoutbll = new T_InoutBLL(pUserInfo);
            var paUserInfobll = new PA_UserInfoBLL(pUserInfo);
            var rateBll = new T_SuperRetailTraderProfitDetailBLL(pUserInfo);
            var agentBll = new T_SuperRetailTraderBLL(pUserInfo);
            var deliveryBll = new TOrderCustomerDeliveryStrategyMappingBLL(pUserInfo);
            try
            {
                PA_PrepayNoEntity paInfo = prepaybll.GetByID(pOrderId);
                DataTable dt = inoutbll.GetCommissionList(pOrderId);
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("开始计算佣金，订单ID:{0}\r\n", pOrderId));

                // 判断paInfo不等于Null可以解决非平安订单进入分佣金流程问题
                if (paInfo != null && dt.Rows.Count > 0)
                {
                    decimal DeliverAmount = deliveryBll.GetDeliverAmount(pOrderId);
                    decimal orderActAmount = (decimal.Parse(dt.Rows[0]["actual_amount"].ToString()));
                    decimal orderAmount = (decimal.Parse(dt.Rows[0]["total_amount"].ToString()));
                    // 计算商品折扣
                    decimal dis = (orderActAmount - DeliverAmount) / (orderAmount - DeliverAmount);
                    //decimal dis = 0.98m;
                    sb.Append(string.Format("折扣计算：({0}(订单实付金额) - {2}(运费))/({1}(订单总金额) - {2}(运费))={3}(折扣)\r\n", orderActAmount, orderAmount, DeliverAmount, dis));
                    decimal commissionAmount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append(string.Format("品类ID：{0},佣金比例：{1}\r\n", dr["item_category_id"], dr["CommissionRate"]));
                        decimal itemAmount = decimal.Parse(dr["enter_price"].ToString());
                        // 根据商品折扣计算商品的实付价格
                        decimal actualAmount = itemAmount * dis;
                        decimal rate = decimal.Parse(dr["CommissionRate"].ToString()) / 100.0m;
                        decimal tmpcommissionAmount = actualAmount * rate;
                        // 用商品实付的价格计算佣金
                        sb.Append(string.Format("商品佣金计算：{0}(商品金额)*{1}(折扣)*{2}(佣金比例)={3}(佣金)\r\n", itemAmount, dis, rate, tmpcommissionAmount));
                        commissionAmount += tmpcommissionAmount;
                    }
                    sb.Append(string.Format("总佣金：{0}", commissionAmount));
                    // 记录日志
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = sb.ToString()
                    });

                    T_SuperRetailTraderEntity[] agent = agentBll.QueryByEntity(new T_SuperRetailTraderEntity()
                    {
                        SuperRetailTraderCode = paUserInfobll.GetByID(dt.Rows[0]["vip_no"].ToString()).AgentNo
                    }, null);

                    // 插入佣金记录
                    rateBll.Create(new T_SuperRetailTraderProfitDetailEntity()
                    {
                        Id = Guid.NewGuid(),
                        SuperRetailTraderID = agent[0].SuperRetailTraderID,
                        SuperRetailTraderProfitConfigId = Guid.Parse("224AA2F0-299F-41E1-B0E3-059191D4FF16"),
                        Level = 1,
                        ProfitType = "Cash",
                        OrderType = "Order",
                        OrderId = pOrderId,
                        OrderNo = dt.Rows[0]["order_no"].ToString(),
                        OrderDate = dt.Rows.Count > 0 ? DateTime.Parse(dt.Rows[0]["create_time"].ToString()) : DateTime.Now,
                        OrderActualAmount = orderActAmount,
                        SalesId = null,
                        VipId = dt.Rows[0]["vip_no"].ToString(),
                        CreateTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now,
                        CreateBy = "System",
                        LastUpdateBy = "System",
                        IsDelete = 0,
                        CustomerId = pUserInfo.CurrentUser.customer_id,
                        Profit = commissionAmount
                    });
                    return true;
                }
                if (paInfo != null)
                {
                    // 记录日志
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("计算旺财佣金失败,未查询到数据库的数据，订单ID：{1}", pOrderId)
                    });
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("旺财分佣金出现异常订单ID：{0},异常信息：{1}", pOrderId, ex)
                });
                return false;
            }

            #region 通知平安
            // 组装佣金数据   默认只分一单
            //List<CommissionList> com = new List<CommissionList>() {
            //    new CommissionList()
            //    {
            //        orderNo=paInfo.PrepayNo,
            //        agentCommission=(commissionAmount * 100).ToString("f0")
            //    }
            //};
            // 组装平安请求参数
            //CommissionRP req = new CommissionRP()
            //{
            //    merchantCode = ConfigHelper.GetAppSetting("MerchantCode", "2016051000000041"),
            //    openId = paInfo.Field1,
            //    orderCommissionList = com.ToJSON()
            //};
            //req.GetSecuritySign();// 签名

            //PAResponseDetailRD res = GetResponse(req.ToJSON(), "/open/appsvr/openapi/app/order/updateOrderCommission");

            //if (res.CODE.Equals("00"))
            //{
            //    // 记录日志
            //    Loggers.Debug(new DebugLogInfo()
            //    {
            //        Message = string.Format("旺财分佣金成功订单ID：{0},佣金：{1}", pOrderId, commissionAmount)
            //    });
            //    return true;
            //}
            //else
            //{
            //    // 记录日志
            //    Loggers.Debug(new DebugLogInfo()
            //    {
            //        Message = string.Format("旺财分佣金失败订单ID：{0},佣金：{1}", pOrderId, commissionAmount)
            //    });
            //    return false;
            //}
            #endregion

            return false;
        }

        /// <summary>
        /// 佣金分润
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static bool DisableCommission(string pOrderId, LoggingSessionInfo pUserInfo)
        {
            var prepaybll = new PA_PrepayNoBLL(pUserInfo);
            var rateBll = new T_SuperRetailTraderProfitDetailBLL(pUserInfo);
            var agentBll = new T_SuperRetailTraderBLL(pUserInfo);
            try
            {
                PA_PrepayNoEntity paInfo = prepaybll.GetByID(pOrderId);
                // 判断paInfo不等于Null可以解决非平安订单进入分佣金流程问题
                if (paInfo != null)
                {
                    // 插入佣金记录
                    T_SuperRetailTraderProfitDetailEntity[] rates = rateBll.QueryByEntity(new T_SuperRetailTraderProfitDetailEntity()
                    {
                        OrderId = pOrderId
                    }, null);

                    if (rates != null && rates.Length > 0)
                    {
                        // 记录日志
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("开始删除订单ID：{0}分佣数据", pOrderId)
                        });
                        rateBll.Delete(rates[0]);
                        // 记录日志
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("删除订单ID：{0}分佣数据成功", pOrderId)
                        });
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("旺财分佣金出现异常订单ID：{0},异常信息：{1}", pOrderId, ex)
                });
                return false;
            }

            return false;
        }

        /// <summary>
        /// 获取API响应信息
        /// </summary>
        /// <param name="pReqPar">请求参数</param>
        /// <param name="pUrl">请求action</param>
        /// <param name="pReqType">请求头</param>
        /// <returns></returns>
        private static PAResponseDetailRD GetResponse(string pReqPar, string pUrl, string pReqType = "application/json")
        {
            string url = string.Format("{0}{1}?access_token={2}", ConfigHelper.GetAppSetting("PAApiDomain", "https://test-api.pingan.com.cn:20443"), pUrl, Acc_Token);
            int reloadCount = 1;// 当前是第几次重试
            int reloadTotalCount = 2;// token过期重试次数
            try
            {
                Loop:
                Hashtable ht = new Hashtable();
                //ht.Add("Accept", "application/json;charset=UTF-8");
                LogConsole.PrintLog("开始请求平安：" + pUrl + ",参数：" + pReqPar);
                // 记录日志
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("调用PA接口({2})：{0},请求参数：{1}", pUrl, pReqPar, Thread.CurrentThread.ManagedThreadId)
                });
                string result = HttpHelper.PostSoapRequest(pReqPar, url, _timeout, ht, pReqType);

                LogConsole.PrintLog("平安返回结果：" + result);
                // 记录日志
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("调用PA接口({2})：{0},返回数据：{1}", pUrl, result, Thread.CurrentThread.ManagedThreadId)
                });
                PAResponseRD res = result.DeserializeJSONTo<PAResponseRD>();
                if (res.ret.Equals("13012"))// 判断是否是token过期问题
                {
                    if (reloadCount < reloadTotalCount)
                    {
                        // 重新加载acctoken
                        ReLoadToken();
                        reloadCount++;
                        goto Loop;
                    }
                    else
                    {
                        // todo 重试失败记录日志
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("调用PA接口失败后重试次数达到上限，接口：{0},请求参数：{1}，返回数据：{2}", pUrl, pReqPar, result)
                        });
                    }
                }
                return res.data;
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("调用PA接口异常：{0},请求参数：{1}，异常信息：{2}", pUrl, pReqPar, ex)
                });
                return new PAResponseDetailRD() { CODE = "500", MSG = "调用PA接口异常,异常信息:" + ex };
            }
        }

        /// <summary>
        /// 组装同步订单到PA的请求实体
        /// </summary>
        /// <param name="pOrderInfo">订单详细信息</param>
        /// <returns></returns>
        private static SaveOrderInfoRP ConvertToRPOrder(GetOrderDetailRD pOrderInfo, PA_UserInfoEntity pPaUser)
        {
            SaveOrderInfoRP orderReq = new SaveOrderInfoRP();
            orderReq.agentNo = pPaUser.AgentNo;// 
            orderReq.creditFlag = "Y";
            orderReq.currency = "CNY";
            string host = ConfigHelper.GetAppSetting("interfacehost").EndsWith("/")
                ? ConfigHelper.GetAppSetting("interfacehost")
                : ConfigHelper.GetAppSetting("interfacehost") + "/";
            orderReq.detailUrl = ConfigHelper.GetAppSetting("OrderDetailAddr", "http://auth.dev.tf.chainclouds.cn/Order/PAGetOrderDetail");
            orderReq.merOrderNo = pOrderInfo.OrderListInfo.OrderID;
            orderReq.merchantCode = ConfigHelper.GetAppSetting("MerchantCode", "2016051000000041");
            orderReq.merchantId = ConfigHelper.GetAppSetting("MerchantId", "900000161760");
            //orderReq.notityUrl = ConfigHelper.GetAppSetting("PALifePayAddr");
            orderReq.openId = pOrderInfo.OrderListInfo.VipID;
            orderReq.couponAmount = "null";
            orderReq.couponCount = "null";
            orderReq.couponId = "null";
            LogConsole.PrintLog("订单金额：" + pOrderInfo.OrderListInfo.ActualDecimal);
            orderReq.orderAmount = (pOrderInfo.OrderListInfo.ActualDecimal * 100).ToString("f0");// 平安是以分为单位
            orderReq.orderCategory = "01";
            orderReq.orderPrepayExpireTime = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd hh:mm:ss");
            orderReq.orderStatus = "01";
            orderReq.orderPrepayTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            orderReq.orderSubject = string.Join(",", pOrderInfo.OrderListInfo.OrderDetailInfo.Select(o => o.ItemName));
            orderReq.tradeType = "JSAPI";//

            // 组装订单详情
            List<PAOrderDetailRP> paOrderDetails = new List<PAOrderDetailRP>();
            foreach (var od in pOrderInfo.OrderListInfo.OrderDetailInfo)
            {
                string imageUrl = string.Empty;
                if (od.ImageInfo.Any())
                {
                    imageUrl = od.ImageInfo[0].ImageUrl;
                }
                paOrderDetails.Add(new PAOrderDetailRP()
                {
                    commodity = od.ItemName,
                    commodityCount = od.Qty.ToString("f0"),
                    commodityID = od.SkuID,
                    commodityImageUrl = imageUrl,
                    commodityPrice = (od.SalesPrice * 100).ToString("f0"),
                    commoditySubject = od.ItemName,
                    commodityUrl = imageUrl
                });
            }

            orderReq.orderDetail = paOrderDetails.ToJSON();
            orderReq.GetSecuritySign();
            return orderReq;
        }

        /// <summary>
        /// 组装更新订单到PA的请求实体
        /// </summary>
        /// <param name="pOrderInfo">订单详细信息</param>
        /// <returns></returns>
        private static UpdateOrderInfoRP ConvertToUpdateOrderRP(GetOrderDetailRD pOrderInfo)
        {
            UpdateOrderInfoRP orderReq = new UpdateOrderInfoRP();
            orderReq.currency = "CNY";
            string host = ConfigHelper.GetAppSetting("interfacehost").EndsWith("/")
                ? ConfigHelper.GetAppSetting("interfacehost")
                : ConfigHelper.GetAppSetting("interfacehost") + "/";
            orderReq.detailUrl = string.Format("{3}HtmlApps/html/public/shop/order_detail.html?customerId={0}&orderId={1}&version={2}"
                , ConfigHelper.GetAppSetting("customer_id"), pOrderInfo.OrderListInfo.OrderID
                , DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), host);
            orderReq.merOrderNo = pOrderInfo.OrderListInfo.OrderID;
            orderReq.merchantCode = ConfigHelper.GetAppSetting("MerchantCode");
            orderReq.merchantId = ConfigHelper.GetAppSetting("MerchantId");
            orderReq.notityUrl = "";//todo
            orderReq.openId = pOrderInfo.OrderListInfo.VipID;
            orderReq.orderAmount = pOrderInfo.OrderListInfo.ActualDecimal.ToString();
            orderReq.orderCategory = "01";
            orderReq.orderPrepayExpireTime = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd hh:mm:ss");
            orderReq.orderStatus = "01";
            orderReq.orderPrepayTime = pOrderInfo.OrderListInfo.OrderDate;
            orderReq.orderSubject = string.Join(",", pOrderInfo.OrderListInfo.OrderDetailInfo.Select(o => o.ItemName));
            orderReq.tradeType = "JSAPI";//todo
            orderReq.payType = "01";
            orderReq.couponAmount = "0";
            orderReq.realAmount = pOrderInfo.OrderListInfo.ActualDecimal.ToString();
            orderReq.counponIds = string.Empty;
            orderReq.waybillId = pOrderInfo.OrderListInfo.CourierNumber;//
            orderReq.orderPayTime = pOrderInfo.OrderListInfo.PaymentTime;//pOrderInfo.OrderListInfo.;

            // 组装订单详情
            List<UpdateOrderDetailRP> paOrderDetails = new List<UpdateOrderDetailRP>();
            foreach (var od in pOrderInfo.OrderListInfo.OrderDetailInfo)
            {
                string imageUrl = string.Empty;
                if (od.ImageInfo.Any())
                {
                    imageUrl = od.ImageInfo[0].ImageUrl;
                }
                paOrderDetails.Add(new UpdateOrderDetailRP()
                {
                    commodity = od.ItemName,
                    commodityCount = od.Qty.ToString(),
                    commodityID = od.SkuID,
                    commodityPrice = od.SalesPrice.ToString(),
                    commodityUrl = imageUrl,
                    orderNo = pOrderInfo.OrderListInfo.OrderID
                });
            }

            orderReq.orderDetail = paOrderDetails.ToJSON();
            orderReq.GetSecuritySign();

            return orderReq;
        }
    }
}
