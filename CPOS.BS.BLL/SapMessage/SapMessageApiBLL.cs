using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CPOS.BS.BLL;
using CPOS.BS.Entity;
using CPOS.Common;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.SAP;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.DTO.Module.SapMessageApi.Request;
using JIT.CPOS.DTO.Module.SapMessageApi.Response;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using System.Net;
using System.IO;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.Log;
using System.Configuration;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    public class SapMessageApiBLL
    {

        // 请求超时时间  秒
        private readonly static int _timeout = 10;
        // 请求接口每次必须带的令牌,过期自动获取
        private static string _accToken = string.Empty;

        private static string AccToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accToken))
                {
                    string postData = string.Format("grant_type=password&username={0}&password={1}&ran={2}", "zmind@tonysFarm.com", "Zmind2016!#", Guid.NewGuid().ToString("N"));
                    string token = HttpHelper.SendSoapRequest(postData, SapApiUrl + _tokenUrl);
                    var res = token.DeserializeJSONTo<SapTokenRD>();
                    _accToken = res.access_token;
                }
                return _accToken;
            }
        }

        #region 静态字段->sap接口域名及地址
        private static string _sapApiUrl = ConfigHelper.GetAppSetting("SapApiUrl", "http://211.152.46.133:1133/");
        private static string SapApiUrl
        {
            get
            {
                if (!_sapApiUrl.EndsWith("/"))
                {
                    return _sapApiUrl + "/";
                }

                return _sapApiUrl;
            }
        }

        // 获取token地址
        private readonly static string _tokenUrl = "token";
        // 获取前N条消息
        public readonly static string _getMessagesByTOPNumberUrl = "api/Interface/GetMessagesByTOPNumber";
        // 获取队列id列表SequenceIDList
        public readonly static string _getMessageSequenceIDListUrl = "api/Interface/GetMessageSequenceIDList";
        // 根据SequenceID获取单条消息
        public readonly static string _getMessageBySequenceIDUrl = "api/Interface/GetMessageBySequenceID";
        // 初始化管理员用户的用户名和密码地址
        private readonly static string _addAdminUserUrl = "Interface/AddAdminUser";
        // 注册新用户
        private readonly static string _registerUrl = "Api/Register";
        // 添加消息
        private readonly static string _addMsgObjUrl = "api/Interface/AddMessage";
        // 重置消息的实体（系统自动更新消息队列的状态）
        private readonly static string _resetMsgContentUrl = "Interface/ ResetMsgContent";
        // 删除消息表(主表Status 置为-1)
        private readonly static string _deleteMsgObjUrl = "Interface/DeleteMsgObj";
        // 获取权限内的最早一条符合条件的消息对象
        private readonly static string _getMsgObjUrl = "Interface/GetMsgObj";
        // 根据 SequencdID 获取消息实体
        private readonly static string _getMsgObjByIdUrl = "Interface/GetMsgObjById";
        // 消费方处理消息后调用此 API, 返回消息处理成功还是失败（如果消息处理成功，则 API 直接返回消费方下一条待处理的消息 id）
        private readonly static string _setMsgHandleResultUrl = "api/Interface/SetMessageOperationResult";
        // 调用其他系统的接口实现消息的推送， 推送消息时，直接推送第一条消息的完全内容，以及下一条消息的 id（以便于系统知道是否要等待下一个轮询周期）
        private readonly static string _postNewMessageUrl = "Interface/PostNewMessage";
        #endregion

        #region 拼接XML数据->GetContentXml
        /// <summary>
        /// 拼接XML数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pObj"></param>
        /// <param name="pAdmInfo"></param>
        /// <returns></returns>
        public static string GetContentXml(List<object> pObj, AdmInfoEntity pAdmInfo)
        {
            string xmlObj = string.Empty;
            string xmlAdmInfo = XmlHelper.SerializeToXmlStr<AdmInfoEntity>(pAdmInfo, true);

            foreach (var obj in pObj)
            {
                Type tp = obj.GetType();
                xmlObj += XmlHelper.SerializeToXmlStr<object>(obj, true);
            }
            return string.Format("<BOM><BO>{0}{1}</BO></BOM>", xmlAdmInfo, xmlObj);
        }
        #endregion

        #region 入队列操作->PushQueue<T>
        /// <summary>
        /// 入队列操作--PushQueue<T>
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public static bool PushQueue<T>(DataQueueEntity pEntity)
        {
            string reqJson = pEntity.ToJSON();
            Loggers.Debug(new DebugLogInfo()
            {
                Message = "开始请求队列插入待推送到SAP的数据:" + reqJson
            });
            string result = HttpHelper.PostSoapRequest(reqJson, ConfigHelper.GetAppSetting("QueueAddr", "http://182.254.242.12:8011") + "/SapMessage/setSapMessage", 5, new Hashtable());

            Loggers.Debug(new DebugLogInfo()
            {
                Message = "插入待推送到SAP的数据返回结果:" + result
            });
            RedisOpenApiRD<T> obj = result.DeserializeJSONTo<RedisOpenApiRD<T>>();

            // 记录消息
            return obj.Code == (int)ResponseCode.Success;
        }
        #endregion

        #region 取消订单同步到SAP
        /// <summary>
        /// 取消订单同步到SAP
        /// </summary>
        /// <param name="pLoggingSessionInfo"></param>
        /// <param name="pOrderID"></param>
        /// <returns></returns>
        public static string CanelOrder(LoggingSessionInfo pLoggingSessionInfo, string pOrderID)
        {
            try
            {
                var inoutBll = new T_InoutBLL(pLoggingSessionInfo);//订单业务对象实例化
                T_InoutEntity order = inoutBll.GetByID(pOrderID);

                AddMsgObjRP reqObj = new AddMsgObjRP()
                {
                    Msg1 = new Msg1()
                    {
                        Content = string.Empty,
                        iLength = 0
                    },
                    Omsg = new Omsg()
                    {
                        Comments = string.Empty,
                        FieldNames = "order_no",
                        FieldValues = order.order_no,
                        Status = 0,
                        UpDateTime = DateTime.Now,
                        TransType = "C",
                        FieldsInKey = 1,
                        ObjectType = "UDSORDR",
                        FromCompany = "Zmind",
                        Timestamp = DateTime.Now
                    }
                };

                return AddMsgObj(reqObj).ErrorMsg;
            }
            catch (Exception ex)
            {
                return "出现异常：" + ex;
            }
        }
        #endregion


        #region 向SAP发送订单->AddOrderMsg
        /// <summary>
        /// 向SAP发送订单
        /// </summary>
        /// <param name="pLoggingSessionInfo">会话信息</param>
        /// <param name="pOrderID">订单ID</param>
        /// <param name="pTransType">事务类型   新增(A)，修改(U)，删除(D)，取消(C)，关闭(L)</param>
        /// <param name="pState">订单状态   A 已创建 P 分拣中 D 配送中 C 已完成 L 已取消</param>
        public static string AddOrderMsg(LoggingSessionInfo pLoggingSessionInfo
            , string pOrderID, string pTransType = "A", string pState = "A")
        {
            StringBuilder logSb = new StringBuilder();
            try
            {
                string orderNo = string.Empty;
                logSb.Append("开始组装xml数据\r\n");
                string xmlData = GetOrderXML(pLoggingSessionInfo, pOrderID, out orderNo);
                logSb.AppendFormat("组装xml数据完成{0}\r\n", xmlData);

                AddMsgObjRP reqObj = new AddMsgObjRP()
                {
                    Msg1 = new Msg1()
                    {
                        Content = xmlData,
                        iLength = xmlData.Length
                    },
                    Omsg = new Omsg()
                    {
                        Comments = string.Empty,
                        FieldNames = "order_no",
                        FieldValues = orderNo,
                        Status = 0,
                        UpDateTime = DateTime.Now,
                        TransType = pTransType,
                        FieldsInKey = 1,
                        ObjectType = "UDSORDR",
                        FromCompany = "Zmind",
                        Timestamp = DateTime.Now
                    }
                };

                return AddMsgObj(reqObj).ErrorMsg;
            }
            catch (Exception ex)
            {
                Loggers.Exception(pLoggingSessionInfo, ex);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("订单插入到消息队列失败,订单id:{0},事务类型:{1},订单状态:{2},异常信息：{3}", pOrderID, pTransType, pState, ex)
                });

                throw ex;
            }

        }
        #endregion

        #region 获取订单XML数据

        /// <summary>
        /// 向SAP发送订单
        /// </summary>
        /// <param name="pLoggingSessionInfo">会话信息</param>
        /// <param name="pOrderID">订单ID</param>
        /// <param name="pTransType">事务类型   新增(A)，修改(U)，删除(D)，取消(C)，关闭(L)</param>
        /// <param name="pState">订单状态   A 已创建 P 分拣中 D 配送中 C 已完成 L 已取消</param>
        public static string GetOrderXML(LoggingSessionInfo pLoggingSessionInfo
            , string pOrderID, out string pOrderNo, string pTransType = "A", string pState = "A")
        {
            pOrderNo = string.Empty;
            try
            {
                var inoutBll = new T_InoutBLL(pLoggingSessionInfo);//订单业务对象实例化
                var vipBll = new VipBLL(pLoggingSessionInfo);//
                GetOrderDetailRD pOrderInfo = inoutBll.GetInoutDetail(pOrderID, pLoggingSessionInfo);
                VipEntity vipItem = vipBll.GetByID(pOrderInfo.OrderListInfo.VipID);
                SAPOrder sapOrder = new SAPOrder();
                pOrderNo = pOrderInfo.OrderListInfo.OrderCode;
                #region 订单主表信息
                var orderExpandEntityBll = new T_Inout_ExpandBLL(pLoggingSessionInfo);
                T_Inout_ExpandEntity[] orderExpandEntity = orderExpandEntityBll.QueryByEntity(new T_Inout_ExpandEntity()
                {
                    OrderId = pOrderInfo.OrderListInfo.OrderID
                }, new OrderBy[] { });
                sapOrder.UDSORDR.row.AmountPay = "0";
                sapOrder.UDSORDR.row.BalanceMethod = string.Empty;
                sapOrder.UDSORDR.row.BalanceMethodID = string.Empty;
                sapOrder.UDSORDR.row.BoxNum = "0";
                sapOrder.UDSORDR.row.BuyType = "普通";
                sapOrder.UDSORDR.row.CardNo = string.Empty;
                sapOrder.UDSORDR.row.CardType = "个人客户";
                sapOrder.UDSORDR.row.sCardWthFD = "N";
                sapOrder.UDSORDR.row.Wave = "A";
                sapOrder.UDSORDR.row.TranType = "1";
                sapOrder.UDSORDR.row.TranMod = string.Empty;
                sapOrder.UDSORDR.row.TranModNo = string.Empty;
                sapOrder.UDSORDR.row.DiscRemarks = string.Empty;
                sapOrder.UDSORDR.row.DiscType = string.Empty;
                sapOrder.UDSORDR.row.DistStatus = string.Empty;
                sapOrder.UDSORDR.row.LogiLine = string.Empty;
                sapOrder.UDSORDR.row.LogiLineNo = string.Empty;
                sapOrder.UDSORDR.row.LogiRemarks = string.Empty;
                sapOrder.UDSORDR.row.RUN_END_TIME = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                sapOrder.UDSORDR.row.PackRemarks = string.Empty;
                sapOrder.UDSORDR.row.OrderRunID = string.Empty;
                sapOrder.UDSORDR.row.OrderCodeOrigial = string.Empty;
                sapOrder.UDSORDR.row.OrderStatus = pState;
                sapOrder.UDSORDR.row.SourceDesc = "SCRM";
                sapOrder.UDSORDR.row.SignWay = string.Empty;
                sapOrder.UDSORDR.row.SignWayNo = string.Empty;
                sapOrder.UDSORDR.row.ShipAddrCntct2 = string.Empty;
                sapOrder.UDSORDR.row.ShipAddrPhone2 = string.Empty;
                sapOrder.UDSORDR.row.ShipAddrTel2 = string.Empty;
                sapOrder.UDSORDR.row.PrcCode = string.Empty;
                sapOrder.UDSORDR.row.GoodsAndInvoice = "Y";
                sapOrder.UDSORDR.row.IsSendCard = "N";
                sapOrder.UDSORDR.row.ShipAddrType = "0";
                sapOrder.UDSORDR.row.CardCntctPrsn = pOrderInfo.OrderListInfo.ReceiverName;
                sapOrder.UDSORDR.row.BillDate = pOrderInfo.OrderListInfo.OrderDate;
                sapOrder.UDSORDR.row.CardCode = vipItem.VipCode;
                sapOrder.UDSORDR.row.CardName = pOrderInfo.OrderListInfo.UserName;
                sapOrder.UDSORDR.row.ChannlBP = pOrderInfo.OrderListInfo.SysVipSource.VipSourceName;
                sapOrder.UDSORDR.row.ChannlBPNo = pOrderInfo.OrderListInfo.SysVipSource.VipSourceID;
                sapOrder.UDSORDR.row.ProvinceCode = orderExpandEntity[0].ProvinceCode;
                sapOrder.UDSORDR.row.ProvinceName = orderExpandEntity[0].Province;
                sapOrder.UDSORDR.row.CityCode = orderExpandEntity[0].CityCode;
                sapOrder.UDSORDR.row.CityName = orderExpandEntity[0].City;
                sapOrder.UDSORDR.row.StreetCode = orderExpandEntity[0].AreaCode;
                sapOrder.UDSORDR.row.StreetName = orderExpandEntity[0].Area;
                sapOrder.UDSORDR.row.IsCallBeDeli = orderExpandEntity[0].IsCallBeDeli;
                sapOrder.UDSORDR.row.CreateDate = pOrderInfo.OrderListInfo.OrderDate;
                sapOrder.UDSORDR.row.CreateUser = pOrderInfo.OrderListInfo.UserName;
                sapOrder.UDSORDR.row.Deduction = (pOrderInfo.OrderListInfo.TotalAmount - pOrderInfo.OrderListInfo.ActualDecimal).ToString("f2");
                sapOrder.UDSORDR.row.TotalGift = sapOrder.UDSORDR.row.Deduction;
                sapOrder.UDSORDR.row.TotalAmount = pOrderInfo.OrderListInfo.TotalAmount.ToString("f2");
                sapOrder.UDSORDR.row.TotalExpns = pOrderInfo.OrderListInfo.DeliveryAmount.ToString();
                sapOrder.UDSORDR.row.DeliveryInterval = pOrderInfo.OrderListInfo.reserveQuantum;
                sapOrder.UDSORDR.row.DoPoints = pOrderInfo.OrderListInfo.ReceivePoints.ToString();
                sapOrder.UDSORDR.row.DocDueType = pOrderInfo.OrderListInfo.DeliveryName;
                sapOrder.UDSORDR.row.Duration = pOrderInfo.OrderListInfo.reserveQuantumID;
                sapOrder.UDSORDR.row.IsInvoice = "N";// string.IsNullOrEmpty(pOrderInfo.OrderListInfo.Invoice) ? "N" : "Y";
                sapOrder.UDSORDR.row.IssuLoca = "上海市";
                sapOrder.UDSORDR.row.IssuLocaNo = "A0101";
                sapOrder.UDSORDR.row.Region = "上海市";
                sapOrder.UDSORDR.row.RegionNo = "A01";
                sapOrder.UDSORDR.row.RequDate = pOrderInfo.OrderListInfo.reserveDay;
                sapOrder.UDSORDR.row.SourceEntry = "-1";
                sapOrder.UDSORDR.row.SourceID = "-1";
                sapOrder.UDSORDR.row.SlpNo = string.Empty;// 第一期给空
                sapOrder.UDSORDR.row.SlpName = string.Empty;// 第一期给
                sapOrder.UDSORDR.row.ShipAddrCntct1 = pOrderInfo.OrderListInfo.ReceiverName;
                sapOrder.UDSORDR.row.ShipAddrInfo = pOrderInfo.OrderListInfo.DeliveryAddress;
                sapOrder.UDSORDR.row.ShipAddrPhone1 = pOrderInfo.OrderListInfo.Mobile;
                sapOrder.UDSORDR.row.ShipAddrTel1 = pOrderInfo.OrderListInfo.Mobile;
                sapOrder.UDSORDR.row.oBarCode = pOrderInfo.OrderListInfo.OrderCode;
                sapOrder.UDSORDR.row.oTypeNo = "3";// pOrderInfo.OrderListInfo.OrderReasonTypeInfo.reason_type_code;
                sapOrder.UDSORDR.row.oType = "配送订单";// pOrderInfo.OrderListInfo.OrderReasonTypeInfo.reason_type_name;

                #endregion

                #region 订单商品信息

                int lineNo = 1;
                foreach (OrderDetailEntity item in pOrderInfo.OrderListInfo.OrderDetailInfo)
                {
                    UDSORDR3Row tmpRow = new UDSORDR3Row();
                    tmpRow.ItemName = item.ItemName;
                    tmpRow.Discount = (item.DiscountRate / 100).ToString("f2");
                    tmpRow.DiscountAmount = (item.StdPrice - item.Enter_Price).ToString("f2");
                    tmpRow.DiscountPrice = item.Enter_Price.ToString("f2");
                    tmpRow.IsPreSort = string.Empty;
                    tmpRow.IsVirItem = item.IfService.Equals(1) ? "Y" : "N";//
                    tmpRow.ItemUnit = "";
                    tmpRow.StoreMthd = "";
                    tmpRow.ItemCode = item.ItemCode;
                    tmpRow.U_OldCode = item.ItemCode;
                    tmpRow.ItemType = "I";
                    tmpRow.LineRemarks = item.Remark;
                    tmpRow.LineStatus = "A";
                    tmpRow.LineTotal = item.Enter_Amount.ToString("f2");
                    tmpRow.ORDER_ITEM_ID = "0";
                    tmpRow.Qty = item.Qty.ToString();
                    tmpRow.Price = item.StdPrice.ToString("f2");
                    tmpRow.order_id = "-1";
                    tmpRow.WhsCode = string.Empty;
                    tmpRow.oBarCode = pOrderInfo.OrderListInfo.OrderCode;
                    tmpRow.ORDER_RUN_ID = "-1";
                    tmpRow.RUN_END_TIME = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    tmpRow.LineID = lineNo.ToString();
                    sapOrder.UDSORDR3.Add(tmpRow);
                }

                #endregion

                #region 订单商品BOM信息

                #endregion

                #region 订单发票信息
                List<UDSORDR1Row> invceList = null;
                if (invceList != null && invceList.Any())
                {
                    lineNo = 1;
                    //foreach (T_Payment_detailEntity item in invceList)
                    //{
                    //    UDSORDR2Row tmpRow = new UDSORDR2Row();
                    //    tmpRow.CheckDate = DateTime.Now.ToString();
                    //    tmpRow.Collected = item.Total_Amount.ToString();
                    //    tmpRow.Fchecked = "Y";
                    //    tmpRow.LineID = lineNo.ToString();
                    //    tmpRow.LineIDPayBoss = "0";
                    //    tmpRow.LineRemarks = "";
                    //    tmpRow.LineStatus = "-1";
                    //    tmpRow.Method = "";
                    //    tmpRow.MethodNo = "";
                    //    tmpRow.ORDER_ID = "-1";
                    //    tmpRow.ORDER_RUN_ID = "-1";
                    //    tmpRow.PayDate = item.CreateTime.ToString();
                    //    tmpRow.RecType = "收款";
                    //    tmpRow.VoucherNo = string.Empty;
                    //    tmpRow.oBarCodeBoss = pOrderInfo.OrderListInfo.OrderCode;
                    //    tmpRow.PayMode = string.Empty;
                    //    tmpRow.PayModeNo = string.Empty;
                    //    tmpRow.RUN_END_TIME = DateTime.Now.ToString();
                    //    tmpRow.RecTypeNo = "1";
                    //    sapOrder.UDSORDR2.row.Add(tmpRow);
                    //    lineNo++;
                    //}

                }
                #endregion

                #region 订单结算信息
                List<T_Payment_detailEntity> paymentDetail = null;

                //var Payment_detailBLL = new T_Payment_detailBLL(pLoggingSessionInfo);
                //paymentDetail = Payment_detailBLL.QueryByEntity(new T_Payment_detailEntity()
                //{
                //    Inout_Id = pOrderInfo.OrderListInfo.OrderID
                //}, new OrderBy[] { }).ToList();

                paymentDetail = new List<T_Payment_detailEntity>()
                {
                    new T_Payment_detailEntity()
                    {
                        Total_Amount = pOrderInfo.OrderListInfo.ActualDecimal,
                        Payment_Type_Name = "网上支付",
                        Payment_Type_Code = "5",
                        CreateTime = DateTime.Now
                    }
                };
                if (paymentDetail != null && paymentDetail.Any())
                {
                    lineNo = 1;
                    foreach (T_Payment_detailEntity item in paymentDetail)
                    {
                        UDSORDR2Row tmpRow = new UDSORDR2Row();
                        tmpRow.CheckDate = DateTime.Now.ToString();
                        tmpRow.Collected = item.Total_Amount.ToString();
                        tmpRow.Fchecked = "Y";
                        tmpRow.LineID = lineNo.ToString();
                        tmpRow.LineIDPayBoss = "0";
                        tmpRow.LineRemarks = "";
                        tmpRow.LineStatus = "-1";
                        tmpRow.Method = item.Payment_Type_Name;
                        tmpRow.MethodNo = item.Payment_Type_Code;
                        tmpRow.ORDER_ID = "-1";
                        tmpRow.ORDER_RUN_ID = "-1";
                        tmpRow.PayDate = item.CreateTime.ToString();
                        tmpRow.RecType = "收款";
                        tmpRow.VoucherNo = string.Empty;
                        tmpRow.oBarCodeBoss = pOrderInfo.OrderListInfo.OrderCode;
                        tmpRow.PayMode = string.Empty;
                        tmpRow.PayModeNo = string.Empty;
                        tmpRow.RUN_END_TIME = DateTime.Now.ToString();
                        tmpRow.RecTypeNo = "1";

                        sapOrder.UDSORDR2.Add(tmpRow);
                        lineNo++;
                    }

                }
                #endregion

                #region 拼接XML

                string xmlAdmInfo = XmlHelper.SerializeToXmlStr<SAPOrder>(sapOrder, true);
                string xmlData = string.Format("<BOM>{0}</BOM>", xmlAdmInfo);

                #endregion

                return xmlData;
            }
            catch (Exception ex)
            {
                Loggers.Exception(pLoggingSessionInfo, ex);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("订单插入到消息队列失败,订单id:{0},事务类型:{1},订单状态:{2},异常信息：{3}", pOrderID, pTransType, pState, ex)
                });

                throw ex;
            }

        }

        #endregion

        #region 根据账号和密码获取acctoken->Token
        /// <summary>
        /// 根据账号和密码获取acctoken
        /// </summary>
        /// <param name="pUserName"></param>
        /// <param name="pPwd"></param>
        /// <returns></returns>
        public static string Token(string pUserName, string pPwd)
        {
            string postData = string.Format("grant_type=password&username={0}&password={1}&ran={2}", pUserName, pPwd, Guid.NewGuid().ToString("N"));

            return PostResponse(postData, _tokenUrl);
        }
        #endregion

        #region 生产消息->AddMsgObj
        /// <summary>
        /// 生产消息
        /// </summary>
        /// <param name="pAddMsgObjRp">消息实体</param>
        /// <returns></returns>
        public static SapMessageApiRD AddMsgObj(AddMsgObjRP pAddMsgObjRp)
        {
            string result = PostResponse(pAddMsgObjRp.ToJSON(), _addMsgObjUrl);
            return result.DeserializeJSONTo<SapMessageApiRD>();
        }
        #endregion

        #region 重置消息->ResetMsgContent
        /// <summary>
        /// 重置消息
        /// </summary>
        /// <param name="pResetMsgContentRp">新的消息内容</param>
        /// <returns></returns>
        public static SapMessageApiRD ResetMsgContent(ResetMsgContentRP pResetMsgContentRp)
        {
            string result = PostResponse(pResetMsgContentRp.ToJSON(), _resetMsgContentUrl);
            return result.DeserializeJSONTo<SapMessageApiRD>();
        }
        #endregion

        #region 删除消息->DeleteMsgObj
        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="pSequenceID">消息ID</param>
        /// <returns></returns>
        public static SapMessageApiRD DeleteMsgObj(int pSequenceID)
        {
            string result = PostResponse(string.Format("sequenceID={0}", pSequenceID), _deleteMsgObjUrl, "application/x-www-form-urlencoded");
            return result.DeserializeJSONTo<SapMessageApiRD>();
        }
        #endregion

        #region 获取一条消息->GetMsgObj
        /// <summary>
        /// 获取一条消息
        /// </summary>
        /// <returns></returns>
        public static GetMsgObjRD GetMsgObj()
        {
            string result = PostResponse(string.Empty, _getMsgObjUrl);
            return result.DeserializeJSONTo<GetMsgObjRD>();
        }
        #endregion

        #region 获取指定消息->GetMsgObjById
        /// <summary>
        /// 获取指定消息
        /// </summary>
        /// <param name="pSequenceID">消息ID</param>
        /// <returns></returns>
        public static GetMsgObjRD GetMsgObjById(int pSequenceID)
        {
            string result = PostResponse(string.Format("sequenceID={0}", pSequenceID), _getMsgObjByIdUrl, "application/x-www-form-urlencoded");
            return result.DeserializeJSONTo<GetMsgObjRD>();
        }
        #endregion

        #region 消费完消息后把结果写回到消息表->SetMsgHandleResult
        /// <summary>
        /// 消费完消息后把结果写回到消息表
        /// </summary>
        /// <param name="pSetMsgHandResultRp">消息结果实体</param>
        /// <returns></returns>
        public static string SetMsgHandleResult(SetMsgHandResultRP pSetMsgHandResultRp)
        {
            string result = PostResponse(pSetMsgHandResultRp.ToJSON(), _setMsgHandleResultUrl);
            return result;
        }
        #endregion

        #region 获取前N条消息->GetMessagesByTOPNumber
        /// <summary>
        /// 获取前N条消息
        /// </summary>
        public static Tuple<bool, string> GetMessagesByTOPNumber()
        {
            string paramStr = "top=400";
            var rest = GetResponse(paramStr, _getMessagesByTOPNumberUrl);
            Loggers.Debug(new DebugLogInfo() { Message = "查询sap信息：" + rest });
            List<GetMsgObjRD> objList = rest.DeserializeJSONTo<List<GetMsgObjRD>>();

            if (objList == null || objList.Count <= 0)
            {
                return new Tuple<bool, string>(false, "未查询到数据：" + rest);
            }

            int i = 0;
            int count = objList.Count;
            int id = 0;
            foreach (GetMsgObjRD obj in objList)
            {
                try
                {
                    if (obj.Omsg != null)
                    {
                        id = obj.Omsg.SequenceID;
                        var res = new SapMessageApiBLL().ConsumSAPMsg(obj);
                        string msg = string.Format("时间：{0},当前队列Id：{1},处理结果 ：【{2}】，当前参数：{3}", DateTime.Now.ToShortDateString(), obj.Omsg.SequenceID, res, obj.ToJSON());
                        Loggers.Debug(new DebugLogInfo() { Message = msg });
                    }
                    i++;
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex) { ErrorMessage = ex.StackTrace + "-------" + ex.Message + "------" + id });
                }
            }

            return new Tuple<bool, string>(true, "操作完成！" + i + ":" + count);

        }
        #endregion

        #region 获取队列Id列表->GetMessageSequenceIDList
        /// <summary>
        /// 获取队列Id列表
        /// </summary>
        public static string GetMessageSequenceIDList()
        {
            var rest = GetResponse("", _getMessageSequenceIDListUrl);
            return rest;
        }
        #endregion

        #region 根据队列Id获取待消费信息->GetMessageBySequenceID
        /// <summary>
        /// 根据队列Id获取待消费信息
        /// </summary>
        public static void GetMessageBySequenceID()
        {
            string sequenceIds = GetMessageSequenceIDList();
            string squenceIdStr = sequenceIds.Trim('[').Trim(']');
            string[] squenceIdArray = squenceIdStr.Split(',');
            int i = 0;
            int count = squenceIdArray.Length;
            foreach (var squenceId in squenceIdArray)
            {
                try
                {
                    string paramStr = "sequenceId=" + squenceId;
                    var rest = GetResponse(paramStr, _getMessageBySequenceIDUrl);
                    Loggers.Debug(new DebugLogInfo() { Message = rest });
                    GetMsgObjRD obj = rest.DeserializeJSONTo<GetMsgObjRD>();

                    if (obj.Omsg != null)
                    {
                        new SapMessageApiBLL().ConsumSAPMsg(obj);
                    }

                }
                catch (Exception ex)
                {
                    Loggers.Debug(new DebugLogInfo() { Message = ex.Message });
                }
                i++;
            }
        }
        #endregion

        #region 测试根据队列Id获取待消费信息->TestGetMessageBySequenceID
        /// <summary>
        /// 根据队列Id获取待消费信息
        /// </summary>
        public static void TestGetMessageBySequenceID()
        {
            string sequenceIds = GetMessageSequenceIDList();
            string squenceIdStr = sequenceIds.Trim('[').Trim(']');
            string[] squenceIdArray = squenceIdStr.Split(',');

            int i = 0;
            string cateType = "A,A01,A0101,A0102,A0103,A0104,A0199,A02,A0201,A0202,A0203,A0204,A0205,A0299,A03,A0301,A0302,A04,A0401,A05,A0501,A06,A0601,A0602,A0603,A0604,A0605,A0606,A0699,A07,A0701,A0702,A0799,A99,A9999,B,B01,B0101,B0102,B0103,B0104,B0105,B02,B0201,B0202,B0203,B03,B0301,B0302,B0303,B0304,B0305,B0306,B0307,B0399,B04,B0401,B0402,B0403,B0499,B05,B0501,B0502,B0503,B0504,B0505,B99,B9999,C,C01,C0101,C0102,C0103,C02,C0201,C0202,C0203,C03,C0301,C0302,C99,C9999,D,D01,D0101,D0102,D0103,D0104,D0105,D0106,D0107,D0108,D0199,D02,D0201,D03,D0301,D99,D9999,E,E01,E0101,E0102,E02,E0201,E0202,E03,E0301,E0302,F,F01,F0101,F0102,Z,Z99,Z9999";
            string cateName = "生鲜,海鲜水产,鱼类,虾类,蟹类,贝类,其它,禽蛋肉类,牛肉,猪肉,羊肉,禽类,蛋类,其它,新鲜水果,进口水果,精品水果,食药同源,食药同源,名特优品,名特优品,冷藏食品,鲜奶,酸奶,果汁,面制品,肉制品,黄油、奶酪,其它,冷冻美食,面制品,冰淇淋,其它,其它,其它,干货,粮油杂货,米、面、杂粮,食用油,意面,麦片,南北货,调味佳品,中式,西式,罐头,休闲食品,巧克力,糖果,饼干,糕点,零食,坚果,干果,其它,冲调饮品,咖啡,茶,蜂蜜,其它,酒水饮料,饮用水,果汁,奶制品,饮料,酒类,其它,其它,母婴、日用,母婴用品,奶粉,米粉、麦粉、辅食,用品,清洁用品,个人洗涤,家居洗涤,纸制品,厨房用具,厨房用品,家居用品,其它,其它,蔬菜,田园时蔬,茄果类,根茎类,豆类,葱蒜类,瓜果类,叶菜类,水生蔬菜类,盆栽,其它类,菌菇类,菌菇类,豆制品,豆制品,其它,其他,卡券礼盒,卡,会员卡,储值卡,礼券,常规礼券,定制礼券,礼盒,精品礼盒,特色套餐,包材辅料,包材辅料,包材,辅料,其他,其它,其它";
            string fatherCode = "-1,A,A01,A01,A01,A01,A01,A,A02,A02,A02,A02,A02,A02,A,A03,A03,A,A04,A,A05,A,A06,A06,A06,A06,A06,A06,A06,A,A07,A07,A07,A,A99,-1,B,B01,B01,B01,B01,B01,B,B02,B02,B02,B,B03,B03,B03,B03,B03,B03,B03,B03,B,B04,B04,B04,B04,B,B05,B05,B05,B05,B05,B,B99,-1,C,C01,C01,C01,C,C02,C02,C02,C,C03,C03,C,C03,-1,D,D01,D01,D01,D01,D01,D01,D01,D01,D01,D,D02,D,D03,D,D99,-1,E,E01,E01,E,E02,E02,E,E03,E03,-1,F,F01,F01,-1,Z,Z99";
            string[] cateA = cateType.Split(',');
            string[] nameA = cateName.Split(',');
            string[] fatherA = fatherCode.Split(',');
            while (i < cateA.Length)
            {
                try
                {
                    string paramStr = "sequenceId=" + 81444;
                    // var rest = GetResponse(paramStr, _getMessageBySequenceIDUrl);
                    string xml = @"<BOM> 
                              <BO> 
                                <AdmInfo> 
                                  <Object>UDAOITC</Object>  
                                  <Version>2</Version> 
                                </AdmInfo>
                                <UDAOITC>
                                  <row> 
                                    <Code>{0}</Code>  
                                    <Name>{1}</Name>  
                                    <U_FatherCode>{2}</U_FatherCode>  
                                    <ItmsGrpCod/>  
                                    <ItmsGrpNam/>  
                                    <Canceled>N</Canceled>  
                                    <U_Levels>3</U_Levels>  
                                    <DocEntry>37</DocEntry>  
                                    <ObjectCode>UDAOITC</ObjectCode>
                                  </row>
                                </UDAOITC>
                              </BO>
                            </BOM>";
                    xml = string.Format(xml, cateA[i], nameA[i], fatherA[i]);
                    var rest = "{\"OMSG\":{\"SequenceID\":72403,\"Timestamp\":\"2016-05-24T16:29:55.49\",\"FromSystem\":\"SAP\",\"FromCompany\":\"SBOTONYSFARMCN\",\"Flag\":\"1\",\"ObjectType\":\"UDAOITC\",\"TransType\":\"A\",\"FieldsInKey\":1,\"FieldNames\":\"Code\",\"FieldValues\":\"A01\",\"Status\":0,\"Comments\":null,\"UpDateTime\":\"2016-05-24T16:29:55.617\"},\"MSG1\":{\"SequenceID\":72403,\"Content\":\"" + xml + "\",\"iLength\":491}}";
                    GetMsgObjRD obj = rest.DeserializeJSONTo<GetMsgObjRD>();

                    if (obj.Omsg != null)
                    {
                        new SapMessageApiBLL().ConsumSAPMsg(obj);
                    }

                }
                catch (Exception ex)
                {
                    Loggers.Debug(new DebugLogInfo() { Message = ex.Message });
                }
                i++;
            }
        }
        #endregion

        #region 测试根据队列Id获取待消费信息->TestGetMessageBySequenceID
        /// <summary>
        /// 根据队列Id获取待消费信息
        /// </summary>
        public static void TestGetMessageBySequenceID2()
        {
            string paramStr = "sequenceId=" + 144952;
            var rest = GetResponse(paramStr, _getMessageBySequenceIDUrl);
            Loggers.Debug(new DebugLogInfo() { Message = rest });
            GetMsgObjRD obj = rest.DeserializeJSONTo<GetMsgObjRD>();

            if (obj.Omsg != null)
            {
                new SapMessageApiBLL().ConsumSAPMsg(obj);
            }
        }
        #endregion

        #region 消费sap消息主方法->ConsumSapMsg
        /// <summary>
        /// 消费sap消息主方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ConsumSAPMsg(GetMsgObjRD obj)
        {
            string msg = string.Empty;
            SapObjectType objType = (SapObjectType)Enum.Parse(typeof(SapObjectType), obj.Omsg.ObjectType);
            BaseSapMsg factory = null;
            switch (objType)
            {
                case SapObjectType.Items:
                    factory = new SapItemsMsg();
                    break;
                case SapObjectType.UDAOITC:
                    factory = new SapUDAOITCMsg();
                    break;
                case SapObjectType.UDAOREW:
                    factory = new SapUDAOREWMsg();
                    break;
                //case SapObjectType.ItemsLocation:
                //    factory = new SapItemsLocationMsg();
                //    break;
                //case SapObjectType.ItemOnHand:
                //    factory = new SapItemOnHandMsg();
                //    break;
                //case SapObjectType.BusinessPartners:
                //    factory = new SapBusinessPartnersMsg();
                //    break;
                //case SapObjectType.UDSORDR:
                //    factory = new SapUDSORDRMsg();
                //    break;
                //case SapObjectType.USORDERINVOICE:
                //    factory = new SapUSORDERINVOICEMsg();
                //    break;
                case SapObjectType.UDSORDER:
                    factory = new SapUDSORDERMsg();
                    break;
                //case SapObjectType.ProductTrees:
                //    factory = new SapProductTreesMsg();
                //    break;
                case SapObjectType.UDIOOSO:
                    factory = new SapUDIOOSOMsg();
                    break;
            }

            if (factory != null || objType == SapObjectType.ProductTrees)
            {
                var res = false;
                if (factory != null)
                {
                    res = factory.SpiltDiffOperation(obj);
                }
                msg = string.Format("{0}:{1}", obj.Omsg.ObjectType, res);
                SetMsgHandResultRP rp = new SetMsgHandResultRP();
                rp.SequenceID = obj.Omsg.SequenceID;
                rp.TargetDB = "tonysfarm";
                rp.TargetSystem = "zmind";
                rp.TargetType = "sap2zmind";
                rp.CreateTime = DateTime.Now;
                rp.UpdateTime = DateTime.Now;
                if (res)
                {
                    rp.Status = 0;
                }
                else
                {
                    rp.Status = obj.Omsg.Status + 1;
                    rp.ErrorMSG = factory != null ? (factory.Msg ?? "") : "";
                }

                var result = SetMsgHandleResult(rp);
                msg = string.Format("{0}，SetMsgHandleResult:{1},Msg:{2}", msg, result, factory != null ? (factory.Msg ?? "") : "");
            }
            else
            {

                msg = "false:factory==null，obj.Omsg.ObjectType--" + obj.Omsg.ObjectType;
            }
            return msg;
        }

        ///// <summary>
        ///// 读取消息成功回写sap结果
        ///// </summary>
        ///// <param name="sequenceId">队列Id</param>
        //private void NotifySapMsg(int sequenceId)
        //{
        //    DeleteMsgObj(sequenceId);
        //}

        /// <summary>
        /// 消费发货区域方法总如
        /// </summary>
        /// <param name="obj"></param>
        private void UdaorewOperation(GetMsgObjRD obj)
        {
            // throw new NotImplementedException();

        }
        #endregion

        #region 获取API响应信息->PostResponse
        /// <summary>
        /// 获取API响应信息
        /// </summary>
        /// <param name="pReqPar">请求参数</param>
        /// <param name="pUrl">请求action</param>
        /// <param name="pReqType">请求头</param>
        /// <returns></returns>
        private static string PostResponse(string pReqPar, string pUrl, string pReqType = "application/json")
        {
            // todo 记录日志
            try
            {
                Hashtable ht = new Hashtable();
                // todo 判断acctoken是否过期
                ht.Add("Authorization", string.Format("Bearer {0}", AccToken));

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "开始请求SAP接口,请求地址：" + pUrl + ",请求参数：" + pReqPar
                });
                string result = HttpHelper.PostSoapRequest(pReqPar, string.Format("{0}{1}", SapApiUrl, pUrl), _timeout, ht, pReqType, "text/json");
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "请求SAP接口结束,返回数据：" + result
                });

                return result;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = (HttpWebResponse)ex.Response;
                    HttpStatusCode code = response.StatusCode;
                    string statusDes = response.StatusDescription;
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        var result = stream.ReadToEnd();
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 获取API响应信息->GetResponse
        /// <summary>
        /// 获取API响应信息
        /// </summary>
        /// <param name="pReqPar">请求参数</param>
        /// <param name="pUrl">请求action</param>
        /// <param name="pReqType">请求头</param>
        /// <returns></returns>
        private static string GetResponse(string pReqPar, string pUrl, string pReqType = "application/json")
        {
            // todo 记录日志
            try
            {
                Hashtable ht = new Hashtable();
                // todo 判断acctoken是否过期
                ht.Add("Authorization", string.Format("Bearer {0}", AccToken));
                return HttpHelper.GetSoapRequest(pReqPar, string.Format("{0}{1}", SapApiUrl, pUrl), _timeout, ht, pReqType, "text/json");
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = (HttpWebResponse)ex.Response;
                    HttpStatusCode code = response.StatusCode;
                    string statusDes = response.StatusDescription;
                    using (var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        var result = stream.ReadToEnd();
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
