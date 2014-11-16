using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;

namespace JIT.CPOS.Web.Pad
{
    public partial class Data_Lj : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["dataType"].ToString().Trim();
                if (!string.IsNullOrEmpty(dataType))
                {
                    switch (dataType)
                    {
                        case "GetVipDetail":    //根据条件获取会员详细信息
                            content = GetVipDetail();
                            break;
                        case "SetOrderPush":    //主动给用户推送订单消息
                            content = SetOrderPush();
                            break;
                        case "GetOrderNo":      //获取订单号码(每次返回10个订单号)
                            content = GetOrderNo();
                            break;
                        case "GetOrderInfo":
                            content = GetOrderInfo();
                            break;
                        case "getEventTotalInfo":
                            content = getEventTotalInfo(); //3.2 活动实时信息
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    content = "dataType不能为空。";
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

        #region 获取会员详细信息
        /// <summary>
        /// 会员详细信息
        /// </summary>
        /// <returns></returns>
        public string GetVipDetail()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }
            string content = string.Empty;
            GetResponseParams<VipEntity> response = new GetResponseParams<VipEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                string Weixin = Request["Weixin"].ToString().Trim();
                VipBLL vipBll = new VipBLL(loggingSessionInfo);
                response.Params = vipBll.GetVipDetail(Weixin);
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"Vip\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            return content;
        }
        #endregion

        #region SetOrderPush 主动给用户推送订单消息

        /// <summary>
        /// 主动给用户推送订单消息
        /// </summary>
        /// <returns></returns>
        public string SetOrderPush()
        {
            var respData = new RespData();
            if (string.IsNullOrEmpty(Request["WeiXinId"]) ||
                string.IsNullOrEmpty(Request["OpenId"]) ||
                string.IsNullOrEmpty(Request["OrdeNo"]))
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = "请求的数据不能为空";
                return respData.ToJSON();
            }

            string content = string.Empty;
            try
            {
                string vipID = string.Empty;
                string vipName = string.Empty;
                var loggingSessionInfo = Default.GetLjLoggingSession();
                //根据客户标识获取连接字符串  qianzhi  2013-07-30
                if (!string.IsNullOrEmpty(Request["customerId"]))
                {
                    loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
                }

                #region 获取VIP信息

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = Request["OpenId"],
                    WeiXin = Request["WeiXinId"]
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.Code = "103";
                    respData.Description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                    vipName = vipList.FirstOrDefault().VipName;
                }

                #endregion

                // 推送消息
                string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                string msgText = string.Format("亲爱的会员{1}，您单号为{0}的购买请求我们已经收到，请您到指定渠道下交纳钱款。谢谢再次惠顾！", Request["OrdeNo"], vipName);
                string msgData = "<xml><OpenID><![CDATA[" + Request["OpenId"] + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                #region 发送日志
                MarketSendLogBLL logServer = new MarketSendLogBLL(loggingSessionInfo);
                MarketSendLogEntity logInfo = new MarketSendLogEntity();
                logInfo.LogId = BaseService.NewGuidPub();
                logInfo.IsSuccess = 1;
                logInfo.MarketEventId = Request["OrdeNo"];
                logInfo.SendTypeId = "2";
                logInfo.TemplateContent = msgData;
                logInfo.VipId = vipID;
                logInfo.WeiXinUserId = Request["OpenId"];
                logInfo.CreateTime = System.DateTime.Now;
                logServer.Create(logInfo);
                #endregion
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("PushMsgResult:{0}", msgResult)
                });
            }
            catch (Exception ex)
            {
                respData.Code = "201";
                respData.Description = "操作失败";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class RespData
        {
            public string Code = "200";
            public string Description = "操作成功";
            public string Exception = null;
            public string Data;
        }

        #endregion

        #region GetOrderNo 获取订单号码(每次返回10个订单号)
        /// <summary>
        /// 获取订单号码(每次返回10个订单号)
        /// </summary>
        /// <returns></returns>
        public string GetOrderNo()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<GetOrderNoEntity> response = new GetResponseParams<GetOrderNoEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            response.Params = new GetOrderNoEntity();
            response.Params.orderNos = new List<OrderNoEntity>();

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"]))
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"orderNos\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识

                TUnitExpandBLL service = new TUnitExpandBLL(loggingSessionInfo);
                int orderNo = service.GetOrderNo(loggingSessionInfo, unitId, 10);

                OrderNoEntity orderEntity = null;
                for (int i = 0; i < 10; i++)
                {
                    orderEntity = new OrderNoEntity();
                    var len = orderNo.ToString().Length;
                    switch (len)
                    {
                        case 1: orderEntity.orderNo = "000" + orderNo; break;
                        case 2: orderEntity.orderNo = "00" + orderNo; break;
                        case 3: orderEntity.orderNo = "0" + orderNo; break;
                        case 4: orderEntity.orderNo = orderNo.ToString(); break;
                    }

                    response.Params.orderNos.Add(orderEntity);

                    orderNo++;
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"orderNos\":{0}}}",
                response.Params.orderNos.ToJSON(), response.Code, response.Description);
            return content;
        }

        public class GetOrderNoEntity
        {
            public IList<OrderNoEntity> orderNos { get; set; }
        }
        public class OrderNoEntity
        {
            public string orderNo { get; set; }
        }
        #endregion

        #region GetOrderInfo
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns></returns>
        public string GetOrderInfo()
        {
            string content = string.Empty;
            GetResponseParams<GetOrderInfoEntity> response = new GetResponseParams<GetOrderInfoEntity>();
            response.Code = "200";
            response.Description = "操作成功";
            response.Params = new GetOrderInfoEntity();

            if (string.IsNullOrEmpty(Request["UnitId"]) ||
                string.IsNullOrEmpty(Request["CustomerId"]) ||
                string.IsNullOrEmpty(Request["OrderNo"]))
            {
                response.Code = "201";
                response.Description = "请求的参数不能为空";
                return string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\"}}",
                    "", response.Code, response.Description);
            }

            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["CustomerId"].Trim(), "");
            }

            try
            {
                string unitId = Request["UnitId"].ToString().Trim();    //门店标识
                string customerId = Request["CustomerId"].ToString().Trim();    //客户标识
                string orderNo = Request["OrderNo"].ToString().Trim();    //订单号

                InoutService inoutService = new InoutService(loggingSessionInfo);
                //TUnitExpandBLL service = new TUnitExpandBLL(loggingSessionInfo);
                //var unitExpandEntity = new TUnitExpandEntity() { UnitId = unitId };
                //var unitExpandEntitys = service.QueryByEntity(unitExpandEntity, null).ToList();
                //if (unitExpandEntitys != null && unitExpandEntitys.Count > 0)
                //{
                var orderId = inoutService.GetInoutId(new InoutInfo()
                {
                    sales_unit_id = unitId,
                    //customer_id = customerId,
                    order_type_id = "1F0A100C42484454BAEA211D4C14B80F",
                    order_reason_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5",
                    red_flag = "1",
                    Field16 = orderNo
                });
                if (orderId != null && orderId.Trim().Length > 0)
                {
                    response.Params.order = inoutService.GetInoutInfoById(orderId);
                }
                //}
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "操作失败：" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"content\":{0}}}",
                response.Params.order.ToJSON(), response.Code, response.Description);
            return content;
        }
        public class GetOrderInfoEntity
        {
            public InoutInfo order { get; set; }
        }
        #endregion

        #region getEventTotalInfo 3.2 活动实时信息
        public string getEventTotalInfo()
        {
            var loggingSessionInfo = Default.GetLjLoggingSession();
            //根据客户标识获取连接字符串  qianzhi  2013-07-30
            if (!string.IsNullOrEmpty(Request["customerId"]))
            {
                loggingSessionInfo = Default.GetBSLoggingSession(Request["customerId"].Trim(), "");
            }

            string content = string.Empty;
            GetResponseParams<EventTotalInfo> response = new GetResponseParams<EventTotalInfo>();
            response.Code = "200";
            response.Description = "操作成功";
            try
            {
                string WeiXinId = Request["WeiXinId"].ToString().Trim();
                string EventId = Request["EventId"].ToString().Trim();
                LEventsEntity eventInfo = new LEventsEntity();
                LEventsBLL inoutServer = new LEventsBLL(loggingSessionInfo);
                string strError = string.Empty;
                eventInfo = inoutServer.GetEventTotalInfo(WeiXinId, EventId, loggingSessionInfo, out strError);
                if (eventInfo == null)
                {
                    response.Code = "201";
                    response.Description = strError;
                }
                else
                {
                    EventTotalInfo eventTotalInfo = new EventTotalInfo();
                    eventTotalInfo.hasOrderCount = eventInfo.hasOrderCount;
                    eventTotalInfo.hasPayCount = eventInfo.hasPayCount;
                    eventTotalInfo.hasSalesAmount = eventInfo.hasSalesAmount;
                    eventTotalInfo.hasVipCount = eventInfo.hasVipCount;
                    eventTotalInfo.newVipCount = eventInfo.newVipCount;
                    response.Params = eventTotalInfo;
                }
            }
            catch (Exception ex)
            {
                response.Code = "201";
                response.Description = "失败" + ex.ToString();
            }
            content = string.Format("{{\"Description\":\"{2}\",\"Code\":\"{1}\",\"Vip\":{0}}}",
                response.Params.ToJSON(), response.Code, response.Description);
            return content;
        }

        public class EventTotalInfo
        {
            /// <summary>
            /// 已关注会员
            /// </summary>
            public int hasVipCount { get; set; }
            /// <summary>
            /// 新采集会员
            /// </summary>
            public int newVipCount { get; set; }
            /// <summary>
            /// 已下订单数
            /// </summary>
            public int hasOrderCount { get; set; }
            /// <summary>
            /// 已付款订单数
            /// </summary>
            public int hasPayCount { get; set; }
            /// <summary>
            /// 已销售订单额
            /// </summary>
            public decimal hasSalesAmount { get; set; }
        }
        #endregion
    }
}