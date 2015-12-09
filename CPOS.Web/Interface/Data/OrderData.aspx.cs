using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Data;
using JIT.CPOS.Web.OnlineShopping.data;
using System.Web;
using System.Net;
using System.IO;

namespace JIT.CPOS.Web.Interface.Data
{
    public partial class OrderData : System.Web.UI.Page
    {
        string customerId = "7ba0d0bc2c13403892deb6499d2c7266";
        string channelId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["action"].ToString().Trim();
                string ip = this.Context.GetClientIP();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);

                switch (dataType)
                {
                    case "setVirtualOrderInfo":     //提交虚拟订单 Jermyn20140307 
                        content = setVirtualOrderInfo();
                        break;
                    case "setUpdateOrderPrice":     //订单修改实付金额 Jermyn20140312
                        content = setUpdateOrderPrice();
                        break;
                    case "setUpdateOrderInfo":      //订单信息修改 Jermyn20140312
                        content = setUpdateOrderInfo();
                        break;
                    case "GetPaymentListBycId":     //根据客户获取对应的支付方式集合 qianzhi20140312
                        content = GetPaymentListBycId();
                        break;
                    case "setPayOrder":             //订单支付 qianzhi20140312
                        content = SetPayOrder(ip);
                        break;
                    case "GetCollectionRecord":     //根据客户获取对应的收款记录 qianzhi20140317
                        content = GetCollectionRecord();
                        break;
                    case "GetOrderStatusList":      //获取订单的下一个状态集合Jermyn20140313
                        content = GetOrderStatusList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region setVirtualOrderInfo 提交虚拟订单 Jermyn20140307
        /// <summary>
        /// 提交虚拟订单
        /// </summary>
        /// <returns></returns>
        public string setVirtualOrderInfo()
        {
            string content = string.Empty;
            var respData = new setVirtualOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setVirtualOrderInfo: {0}", reqContent)
                });

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setVirtualOrderInfoReqData>();
                reqObj = reqObj == null ? new setVirtualOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setVirtualOrderInfoReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }

                if (reqObj.common.customerId == null || reqObj.common.customerId.Equals(""))
                {
                    respData.code = "2211";
                    respData.description = "客户标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2211";
                    respData.description = "用户标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "订单标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.unitId == null || reqObj.special.unitId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "门店标识不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.amount == null || reqObj.special.amount.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "金额不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.dataFromId == null || reqObj.special.dataFromId.Equals(""))
                {
                    respData.code = "2204";
                    respData.description = "来源不能为空 2=Pad，3=微信";
                    return respData.ToJSON().ToString();
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion


                #region
                string strError = string.Empty;

                InoutService server = new InoutService(loggingSessionInfo);
                var result = server.SetVirtualOrderInfo(ToStr(reqObj.special.orderId)
                                                    , ToStr(reqObj.common.customerId)
                                                    , ToStr(reqObj.special.unitId)
                                                    , ToStr(reqObj.common.userId)
                                                    , ToStr(reqObj.special.dataFromId)
                                                    , ToStr(reqObj.special.amount), "", "", ToStr(reqObj.common.userId));
                if (result.Equals("1"))
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                    respData.exception = strError;
                }
                else
                {
                    respData.code = "111";
                    respData.description = "后台业务错误，请联系管理员.";
                    respData.exception = strError;
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setVirtualOrderInfoRespData : Default.LowerRespData
        {

        }


        public class setVirtualOrderInfoReqData : Default.ReqData
        {
            public setVirtualOrderInfoReqSpecialData special;
        }
        public class setVirtualOrderInfoReqSpecialData
        {
            public string orderId { get; set; }
            public string amount { get; set; }
            public string dataFromId { get; set; }
            public string unitId { get; set; }
        }

        #endregion

        #region 2.6.10 订单修改实付金额 Jermyn20140312
        public string setUpdateOrderPrice()
        {
            string content = string.Empty;
            var respData = new setUpdateOrderPriceRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setUpdateOrderPrice: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setUpdateOrderPriceReqData>();
                reqObj = reqObj == null ? new setUpdateOrderPriceReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setUpdateOrderPriceReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "订单标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.price == null || reqObj.special.price.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "实付价格不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new setUpdateOrderPriceRespContentData();

                InoutService inoutService = new InoutService(loggingSessionInfo);

                var flag = inoutService.UpdateOrderPrice(ToStr(reqObj.special.orderId)
                                                    , Convert.ToDecimal(ToDouble(reqObj.special.price))
                                                    , ToStr(reqObj.common.userId));

                if (!flag)
                {
                    respData.code = "103";
                    respData.description = "更新订单价格失败";
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setUpdateOrderPrice content: {0}", content)
            });
            return content;
        }


        public class setUpdateOrderPriceRespData : Default.LowerRespData
        {
            public setUpdateOrderPriceRespContentData content { get; set; }
        }
        public class setUpdateOrderPriceRespContentData
        {

        }
        public class setUpdateOrderPriceReqData : Default.ReqData
        {
            public setUpdateOrderPriceReqSpecialData special;
        }
        public class setUpdateOrderPriceReqSpecialData
        {
            public string orderId { get; set; }
            public string price { get; set; }
        }
        #endregion

        #region 2.6.8 修改订单信息 Jermyn20140312
        /// <summary>
        /// 2.6.8 订单修改
        /// </summary>
        /// <returns></returns>
        public string setUpdateOrderInfo()
        {
            string content = string.Empty;
            var respData = new setUpdateOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setUpdateOrderInfo: {0}", reqContent)
                });

                //reqContent = "{"common":{"locale":"zh","userId":"c45f87741005ab3a4d9a6b6da21e9162","openId":"c45f87741005ab3a4d9a6b6da21e9162","customerId":"f6a7da3d28f74f2abedfc3ea0cf65c01"},"special":{"skuId":"","qty":"","storeId":"","salesPrice":"","stdPrice":"","totalAmount":"640","tableNumber":"","username":"","mobile":"","email":"","remark":"1","deliveryId":"1","deliveryAddress":"","deliveryTime":"","orderDetailList":[{"skuId":"648245A5233C48D9817B561139CE9548","salesPrice":"640","qty":"1"}]}}";
                #region //解析请求字符串 check
                var reqObj = reqContent.DeserializeJSONTo<setUpdateOrderInfoReqData>();
                reqObj = reqObj == null ? new setUpdateOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setUpdateOrderInfoReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }



                if (reqObj.special.orderDetailList == null || reqObj.special.orderDetailList.Count == 0)
                {
                    respData.code = "2201";
                    respData.description = "必须选择商品";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.orderId))
                {
                    respData.code = "2207";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                CPOS.BS.Entity.Interface.SetOrderEntity orderInfo = new CPOS.BS.Entity.Interface.SetOrderEntity();

                int itemTotalQty = 0;
                foreach (var detail in reqObj.special.orderDetailList)
                {
                    itemTotalQty += ToInt(detail.qty);
                }
                reqObj.special.qty = itemTotalQty.ToString();

                string purchaseUnitId = string.Empty;
                orderInfo.TotalQty = ToInt(reqObj.special.qty);
                UnitService unitServer = new UnitService(loggingSessionInfo);
                orderInfo.StoreId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //获取在线商城的门店标识

                if (reqObj.special.storeId == null || reqObj.special.storeId.Trim().Equals(""))
                {

                }
                else
                {
                    //orderInfo.StoreId = ToStr(reqObj.special.storeId);
                    orderInfo.PurchaseUnitId = ToStr(reqObj.special.storeId);
                }

                if (orderInfo.StoreId == null || orderInfo.StoreId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "该客户未配置在线商城.";
                    return respData.ToJSON().ToString();
                }

                //orderInfo.SalesPrice = Convert.ToDecimal(reqObj.special.salesPrice);
                //orderInfo.StdPrice = Convert.ToDecimal(reqObj.special.stdPrice);
                orderInfo.OrderId = ToStr(reqObj.special.orderId);
                orderInfo.TotalAmount = Convert.ToDecimal(reqObj.special.totalAmount);
                orderInfo.Mobile = ToStr(reqObj.special.mobile);
                orderInfo.Email = ToStr(reqObj.special.email);
                orderInfo.Remark = ToStr(reqObj.special.remark);
                orderInfo.CreateBy = ToStr(reqObj.common.userId);
                orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.DeliveryId = ToStr(reqObj.special.deliveryId);
                orderInfo.DeliveryTime = ToStr(reqObj.special.deliveryTime);
                orderInfo.DeliveryAddress = ToStr(reqObj.special.deliveryAddress);
                orderInfo.CustomerId = customerId;
                orderInfo.OpenId = ToStr(reqObj.common.openId);
                orderInfo.username = ToStr(reqObj.special.username);
                orderInfo.tableNumber = ToStr(reqObj.special.tableNumber);
                orderInfo.CouponsPrompt = ToStr(reqObj.special.couponsPrompt);
                orderInfo.Remark = ToStr(reqObj.special.remark);
                orderInfo.JoinNo = ToInt(reqObj.special.joinNo);

                if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals("") && !ToStr(reqObj.special.actualAmount).Equals(""))
                {
                    orderInfo.ActualAmount = Convert.ToDecimal(ToStr(reqObj.special.actualAmount));
                }
                if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
                {
                    orderInfo.OrderId = BaseService.NewGuidPub();
                    orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    orderInfo.Status = (string.IsNullOrEmpty(reqObj.special.reqBy) || reqObj.special.reqBy == "0") ? "-99" : "1";   //Jermyn20140219 //haibo.zhou20140224
                    orderInfo.StatusDesc = "未支付";
                    //orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo();
                }
                int i = 1;
                orderInfo.OrderDetailInfoList = new List<InoutDetailInfo>();
                foreach (var detail in reqObj.special.orderDetailList)
                {
                    InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
                    orderDetailInfo.order_id = orderInfo.OrderId;
                    orderDetailInfo.order_detail_id = BaseService.NewGuidPub();
                    orderDetailInfo.sku_id = ToStr(detail.skuId);
                    orderDetailInfo.enter_qty = ToInt(detail.qty);
                    orderDetailInfo.order_qty = ToInt(detail.qty);
                    orderDetailInfo.std_price = Convert.ToDecimal(detail.salesPrice);
                    orderDetailInfo.unit_id = orderInfo.StoreId;
                    orderDetailInfo.display_index = i;
                    orderDetailInfo.enter_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.enter_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.retail_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.retail_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.Field1 = ToStr(detail.beginDate);
                    orderDetailInfo.Field2 = ToStr(detail.endDate);
                    orderDetailInfo.Field3 = ToStr(detail.appointmentTime);
                    i++;
                    orderInfo.OrderDetailInfoList.Add(orderDetailInfo);
                }
                #endregion

                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderUpdate(orderInfo, out strError, out strMsg);

                #region 返回信息设置

                respData.content = new setUpdateOrderInfoNewRespContentData();
                respData.content.orderId = orderInfo.OrderId;
                respData.exception = strMsg;
                respData.description = strError;
                if (!bReturn)
                {
                    respData.code = "111";
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setUpdateOrderInfoRespData : Default.LowerRespData
        {
            public setUpdateOrderInfoNewRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setUpdateOrderInfoNewRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setUpdateOrderInfoReqData : Default.ReqData
        {
            public setUpdateOrderInfoReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setUpdateOrderInfoReqSpecialData
        {
            public string qty { get; set; }		    //商品数量
            public string storeId { get; set; }		//门店标识
            public string totalAmount { get; set; }		//订单总价
            public string mobile { get; set; }		//手机号码
            public string deliveryId { get; set; }//		配送方式标识
            public string deliveryAddress { get; set; }//	配送地址
            public string deliveryTime { get; set; }//		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
            public string tableNumber { get; set; }
            public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213--Field16）
            public string actualAmount { get; set; }    //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
            public string reqBy { get; set; }   //请求0-wap,1-手机.
            public string joinNo { get; set; }  //餐饮--人数
            public string orderId { get; set; } //订单号

            public IList<setOrderDetailClass> orderDetailList { get; set; }
            //public IList<setOrderCouponClass> couponList { get; set; }  //优惠券集合 （Jermyn20131213--tordercouponmapping
        }



        public class setOrderDetailClass
        {
            public string skuId { get; set; }       //商品SKU标识
            public string salesPrice { get; set; }  //商品销售单价
            public string qty { get; set; }         //商品数量
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public string appointmentTime { get; set; }//added by zhangwei 2014-2-26预约时间
        }

        //public class setOrderCouponClass
        //{
        //    public string couponId { get; set; }       //优惠券标识
        //}
        #endregion
        #endregion

        #region GetPaymentListBycId 根据客户获取对应的支付方式集合  qianzhi20140312

        /// <summary>
        /// 根据客户获取对应的支付方式集合
        /// </summary>
        public string GetPaymentListBycId()
        {
            string content = string.Empty;

            var respData = new GetPaymentListRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetPaymentListReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetPaymentList: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                else
                {
                    respData.code = "103";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }


                //判断渠道ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.channelId))
                {
                    channelId = reqObj.common.channelId;
                }
                //else
                //{
                //    respData.code = "103";
                //    respData.description = "channelId不能为空";
                //    return respData.ToJSON();
                //}
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new GetPaymentListRespContentData();
                respData.content.paymentList = new List<PaymentEntity>();

                DataSet ds = new TPaymentTypeBLL(loggingSessionInfo).GetPaymentListByCustomerId(customerId, channelId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(reqObj.common.plat))
                    {
                        respData.content.paymentList = DataTableToObject.ConvertToList<PaymentEntity>(ds.Tables[0]);//H5
                    }
                    else
                    {
                        var temp = ds.Tables[0].AsEnumerable().Select(t => new PaymentEntity
                        {
                            paymentTypeId = t["paymentTypeId"].ToString(),
                            paymentTypeName = t["paymentTypeName"].ToString(),
                            displayIndex = Convert.ToInt64(t["displayIndex"].ToString()),
                            logoUrl = t["logoUrl"].ToString(),
                            paymentTypeCode = t["paymentTypeCode"].ToString()
                        }).Where(t1 => t1.paymentTypeId != "DFD3E26D5C784BBC86B075090617F44B");
                        respData.content.paymentList = temp.ToArray();
                    }

                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        public class GetPaymentListRespData : Default.LowerRespData
        {
            public GetPaymentListRespContentData content;
        }
        public class GetPaymentListRespContentData
        {
            public IList<PaymentEntity> paymentList { get; set; }     //支付方式集合
        }
        public class PaymentEntity
        {
            public string paymentTypeId { get; set; }   //标识
            public string paymentTypeName { get; set; } //支付姓名
            public Int64 displayIndex { get; set; }     //排序
            public string logoUrl { get; set; }
            public string paymentTypeCode { get; set; }

        }
        public class GetPaymentListReqData : Default.ReqData
        {
            public GetPaymentListReqSpecialData special;
        }
        public class GetPaymentListReqSpecialData
        {

        }

        #endregion

        #region SetPayOrder 订单支付  qianzhi20140312

        /// <summary>
        /// 订单支付
        /// 第一种：没有订单，提交金额，生成虚拟订单，同时调用交易中心完成支付，交易中心处理完成之后，回调修改订单状态。
        /// 第二种：有订单，不需要生成虚拟订单，直接提交交易中心，完成支付，交易中心处理完成之后，回调修改订单状态。
        /// </summary>
        public string SetPayOrder(string pBillCreateIP)
        {
            string content = string.Empty;

            var respData = new SetPayOrderRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<SetPayOrderReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPayOrder: {0}", reqContent)
                });

                #region 判断参数是否传递

                if (string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    respData.code = "103";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }
                if (string.IsNullOrEmpty(reqObj.common.userId))
                {
                    respData.code = "103";
                    respData.description = "userId不能为空";
                    return respData.ToJSON();
                }
                if (string.IsNullOrEmpty(reqObj.special.amount))
                {
                    respData.code = "103";
                    respData.description = "amount不能为空";
                    return respData.ToJSON();
                }
                if (string.IsNullOrEmpty(reqObj.special.paymentId))
                {
                    respData.code = "103";
                    respData.description = "paymentId不能为空";
                    return respData.ToJSON();
                }
                if (string.IsNullOrEmpty(reqObj.special.dataFromId))
                {
                    respData.code = "103";
                    respData.description = "dataFromId不能为空";
                    return respData.ToJSON();
                }

                #endregion

                customerId = reqObj.common.customerId;
                LoggingSessionInfo loggingSessionInfo=null;
                if (!string.IsNullOrEmpty(reqObj.common.userId))
                    loggingSessionInfo = Default.GetBSLoggingSession(customerId, reqObj.common.userId, reqObj.common.IsAtoC);
                else
                    loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //门店标识，当前用户所在的门店【没传后台获取在线商城】
                if (string.IsNullOrEmpty(reqObj.special.unitId))
                {
                    UnitService unitServer = new UnitService(loggingSessionInfo);
                    reqObj.special.unitId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //获取在线商城的门店标识
                }

                InoutService server = new InoutService(loggingSessionInfo);
                //第一种：没有订单，提交金额，生成虚拟订单，同时调用交易中心完成支付，交易中心处理完成之后，回调修改订单状态。
                //第二种：有订单，不需要生成虚拟订单，直接提交交易中心，完成支付，交易中心处理完成之后，回调修改订单状态。
                if (string.IsNullOrEmpty(reqObj.special.orderId))
                {
                    //生成虚拟订单
                    reqObj.special.orderId = Common.Utils.NewGuid();


                    var amount = ToFloat(reqObj.special.amount) / 100;

                    var result = server.SetVirtualOrderInfo(ToStr(reqObj.special.orderId)
                                                        , ToStr(reqObj.common.customerId)
                                                        , ToStr(reqObj.special.unitId)
                                                        //, ToStr(reqObj.common.userId)  //Henry 
                                                        , ToStr(loggingSessionInfo.UserID)
                                                        , ToStr(reqObj.special.dataFromId)
                                                        , amount.ToString(), "", "", ToStr(loggingSessionInfo.UserID));
                    if (!result.Equals("1"))
                    {
                        respData.code = "103";
                        respData.description = "生成虚拟订单错误";
                        return respData.ToJSON();
                    }
                }

                var ds = new TPaymentTypeBLL(loggingSessionInfo).
                     GetPaymentByCustomerIdAndPaymentID(reqObj.common.customerId, reqObj.special.paymentId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    #region 货到付款的支付方式

                    if (dr["Payment_Type_Code"].ToString() == "GetToPay")
                    {
                        server.SetGetToPay(reqObj.special.orderId);
                        respData.code = "200";
                        respData.description = "操作成功";
                        respData.content = new SetPayOrderRespContentData();
                        respData.content.OrderID = reqObj.special.orderId;

                        return respData.ToJSON();
                    }
                    #endregion
                }

                //调用交易中心方法
                decimal appOrderAmount = new OnlineShoppingItemBLL(loggingSessionInfo).GetOrderAmmount(reqObj.special.orderId);
                if (reqObj.special.dataFromId.Equals("3"))
                {
                    #region 微信支付

                    OrderPackage op = new OrderPackage();
                    op.OrderNO = reqObj.special.orderId;
                    op.TotalAmount = (appOrderAmount * 100).ToString("F0");
                    op.ClientIP = HttpContext.Current.GetClientIP();
                    var json = WeiXinPayGateway.GeneratePreOrderRequest(op);
                    //
                    respData.code = "200";
                    respData.description = "操作成功";
                    respData.content = new SetPayOrderRespContentData();
                    respData.content.OrderID = reqObj.special.orderId;
                    respData.content.WeiXinPreOrderContent = json;

                    #endregion
                }
                else
                {
                    #region 非微信支付

                    string pUrlPath = ConfigurationManager.AppSettings["paymentcenterUrl"];
                    string pReturnUrl = reqObj.special.returnUrl;

                    //var ds = new TPaymentTypeBLL(loggingSessionInfo).
                    //    GetPaymentByCustomerIdAndPaymentID(reqObj.common.customerId, reqObj.special.paymentId);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        #region 货到付款的支付方式

                        //if (dr["Payment_Type_Code"].ToString()=="GetToPay")
                        //{
                        //    server.SetGetToPay(reqObj.special.orderId);
                        //    respData.code = "200";
                        //    respData.description = "操作成功";
                        //    respData.content = new SetPayOrderRespContentData();
                        //    respData.content.OrderID = reqObj.special.orderId;

                        //    return respData.ToJSON();
                        //}

                        #endregion

                        var dic = new Dictionary<string, string>();
                        dic["SpbillCreateIp"] = pBillCreateIP;

                        var itemNameDs = server.GetItemNameByOrderId(reqObj.special.orderId);

                        var itemNameList = itemNameDs.Tables[0].AsEnumerable().Aggregate("", (x, j) =>
                        {
                            x += string.Format("{0}|", j["item_name"].ToString());
                            return x;
                        }).Trim('|');

                        if (Encoding.Default.GetBytes(itemNameList).Length > 128)
                        {
                            itemNameList = itemNameList.Substring(0, 60) + "......";
                        }
                        itemNameList = itemNameList.Replace("+", "");
                        itemNameList = itemNameList.Replace(" ", "-");





                        var para = new
                        {
                            PayChannelID = dr["ChannelId"].ToString(),
                            AppOrderID = reqObj.special.orderId,
                            AppOrderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                            AppOrderAmount = ToInt(appOrderAmount * 100),
                            AppOrderDesc = string.IsNullOrEmpty(itemNameList) ? "jitmarketing" : itemNameList,
                            Currency = 1,
                            MobileNO = reqObj.special.mobileNo,
                            ReturnUrl = pReturnUrl,
                            DynamicID = reqObj.special.dynamicId,
                            DynamicIDType = reqObj.special.dynamicIdType,
                            Paras = dic,
                            OpenId=reqObj.common.openId,
                            ClientIP = pBillCreateIP
                        };

                        var request = new
                        {
                            AppID = 1,
                            ClientID = reqObj.common.customerId,
                            UserID = loggingSessionInfo.UserID,
                            Parameters = para
                        };
                        //Json参数准备
                        string jsonString = string.Format("action=CreateOrder&request={0}", request.ToJSON());

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("jsonString: {0}", jsonString)
                        });

                        string httpResponse = SendHttpRequest(pUrlPath, jsonString);

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("httpResponse: {0}", httpResponse)
                        });

                        //反序列化
                        var payres = httpResponse.DeserializeJSONTo<orderPayCenterContentData>();

                        if (payres.ResultCode == 0)
                        {
                            respData.code = "200";
                            respData.description = "操作成功";
                            respData.content = new SetPayOrderRespContentData();
                            respData.content.OrderID = reqObj.special.orderId;
                            respData.content.PayUrl = payres.Datas.PayUrl;
                            respData.content.QrCodeUrl = payres.Datas.QrCodeUrl;
                            respData.content.ResultCode = payres.ResultCode;
                            respData.content.Message = payres.Message;
                            respData.content.WXPackage = payres.Datas.WXPackage;
                            new OnlineShoppingItemBLL(loggingSessionInfo).SetOrderPayCenterCode(reqObj.special.orderId, payres.Datas.OrderID);
                        }
                        else
                        {
                            respData.code = payres.ResultCode.ToString();
                            respData.description = payres.Message;
                            return respData.ToJSON();
                        }
                    }
                    else
                    {
                        respData.code = "103";
                        respData.description = "支付方式不正确";
                        return respData.ToJSON();
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class SetPayOrderRespData : Default.LowerRespData
        {
            public SetPayOrderRespContentData content;
        }
        public class SetPayOrderRespContentData
        {
            public int ResultCode { get; set; }         //支付平台(银联,AliPay)返回的结果码
            public string OrderID { get; set; }         //订单ID
            public string PayUrl { get; set; }          //预订单成功生成跳转的支付URL(只有wap支付才会返回)
            public string QrCodeUrl { get; set; }       //线下支付预支付订单生成的二维码URL(AliPayOffline支付)
            public string Message { get; set; }         //返回的信息,如发生错误时返回错误信息
            public string WXPackage { get; set; }       //微信返回包信息

            public string WeiXinPreOrderContent { get; set; }
        }
        public class SetPayOrderReqData : Default.ReqData
        {
            public SetPayOrderReqSpecialData special;
        }
        public class SetPayOrderReqSpecialData
        {
            public string amount { get; set; }          //订单总价【必须】,Int类型，价格单位是分
            public string orderId { get; set; }         //订单标识 字符窜guid
            public string paymentId { get; set; }       //支付方式【必须】
            public string dataFromId { get; set; }      //来源：2=Pad，3=微信【必须】
            public string unitId { get; set; }          //门店标识，当前用户所在的门店【没传后台获取在线商城】
            public string mobileNo { get; set; }        //手机号
            public string returnUrl { get; set; }       //回调界面
            public string dynamicId { get; set; }       //动态码,用于AliPayOffline面支付,其它支付方式可不填
            public string dynamicIdType { get; set; }   //动态ID类型(用于AliPayOffline面支付,其它支付方式可不填)  声波:soundwave、二维码:qrcode、条码:barcode
            public string orderDesc { get; set; }       //订单描述
            public string spbillCreateIp { get; set; }  //生成订单的主机IP地址
        }
        public class orderPayCenterContentData
        {
            public int ResultCode { get; set; }
            public string Message { get; set; }
            public orderPayCenterContentDetailData Datas { get; set; }
        }
        public class orderPayCenterContentDetailData
        {
            public string OrderID { get; set; }
            public string PayUrl { get; set; }
            public string QrCodeUrl { get; set; }
            public string Message { get; set; }
            public string WXPackage { get; set; }//微信支付包信息
        }

        #endregion

        #region GetOrderStatusList 获取订单的下一个状态集合Jermyn20140313

        public string GetOrderStatusList()
        {
            string content = string.Empty;
            var respData = new GetOrderStatusListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetOrderStatusList: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<GetOrderStatusListReqData>();
                reqObj = reqObj == null ? new GetOrderStatusListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new GetOrderStatusListReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 业务处理
                respData.content = new GetOrderStatusListRespContentData();
                respData.content.orderStatusList = new List<OrderStatusInfo>();
                IList<OrderStatusInfo> OrderStatusList = new List<OrderStatusInfo>();
                TInOutStatusNodeBLL serer = new TInOutStatusNodeBLL(loggingSessionInfo);
                IList<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
                list = serer.GetOrderStatusList(ToStr(reqObj.special.orderId), ToStr(reqObj.special.paymentTypeId));
                if (list != null && list.Count > 0)
                {
                    foreach (var info in list)
                    {
                        OrderStatusInfo orderInfo = new OrderStatusInfo();
                        orderInfo.nodeId = ToStr(info.NodeID.ToString());
                        orderInfo.nodeValue = ToStr(info.NodeValue);
                        orderInfo.previousValue = ToStr(info.PreviousValue);
                        orderInfo.payMethod = ToStr(info.PayMethod);
                        orderInfo.nextValue = ToStr(info.NextValue);
                        respData.content.orderStatusList.Add(orderInfo);
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class GetOrderStatusListRespData : Default.LowerRespData
        {
            public GetOrderStatusListRespContentData content { get; set; }
        }
        public class GetOrderStatusListRespContentData
        {
            public IList<OrderStatusInfo> orderStatusList { get; set; }
        }

        public class OrderStatusInfo
        {
            public string nodeId { get; set; }              //主标识
            public string nodeValue { get; set; }           //当前状态
            public string previousValue { get; set; }       //前一状态
            public string nextValue { get; set; }           //后一状态
            public string payMethod { get; set; }           //支付方式
            public string sequence { get; set; }            //排序
        }

        public class GetOrderStatusListReqData : ReqData
        {
            public GetOrderStatusListReqSpecialData special;
        }
        public class GetOrderStatusListReqSpecialData
        {
            public string orderId { get; set; }     //订单标识
            public string paymentTypeId { get; set; }   //支付方式
        }


        #endregion
        #endregion

        #region GetCollectionRecord 根据客户获取对应的收款记录 qianzhi20140317

        /// <summary>
        /// 根据客户获取对应的支付方式集合
        /// </summary>
        public string GetCollectionRecord()
        {
            string content = string.Empty;

            var respData = new GetCollectionRecordRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<GetCollectionRecordReqData>();
                reqObj = reqObj == null ? new GetCollectionRecordReqData() : reqObj;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetCollectionRecord: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    respData.code = "103";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON();
                }
                if (reqObj.special == null)
                {
                    reqObj.special = new GetCollectionRecordReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 20;
                }

                customerId = reqObj.common.customerId;
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new GetCollectionRecordRespContentData();
                respData.content.recordList = new List<CollectionEntity>();

                DataSet ds = new TPaymentTypeBLL(loggingSessionInfo).GetCollectionRecord(customerId, reqObj.special.page, reqObj.special.pageSize);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.recordList = DataTableToObject.ConvertToList<CollectionEntity>(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class GetCollectionRecordRespData : Default.LowerRespData
        {
            public GetCollectionRecordRespContentData content;
        }
        public class GetCollectionRecordRespContentData
        {
            public IList<CollectionEntity> recordList { get; set; }     //收款记录集合
        }
        public class CollectionEntity
        {
            public string amount { get; set; }          //金额
            public string ordeId { get; set; }          //订单ID
            public string paymentDate { get; set; }     //支付日期
            public string paymentTypeName { get; set; } //支付类型
            public string vipId { get; set; }           //会员ID
            public string vipName { get; set; }         //会员名称
            public Int64 displayIndex { get; set; }     //序号
        }
        public class GetCollectionRecordReqData : Default.ReqData
        {
            public GetCollectionRecordReqSpecialData special;
        }
        public class GetCollectionRecordReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }

        #endregion

        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        public static string SendHttpRequest(string requestURI, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = requestURI;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            //myRequest.Accept = "application/plain";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }
        #endregion
    }
}