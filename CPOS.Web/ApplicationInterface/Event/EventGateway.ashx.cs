using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Events;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Event
{
    /// <summary>
    /// EventGateway 的摘要说明
    /// </summary>
    public class EventGateway : BaseGateway
    {
        protected string GetEventCommentList(string pRequest)
        {
            EventCommentListRD rd = new EventCommentListRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<EventCommentListRP>>();
                
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                QuestionnaireBLL bll = new QuestionnaireBLL(loggingSessionInfo);
                var ds = bll.GetCommentList(rp.Parameters.QuestionnaireID, rp.UserID);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string VIPName = ds.Tables[0].Rows[0]["VipName"].ToString();
                    var list1 = ds.Tables[0].AsEnumerable().Where(t => t["QuestionType"].ToString() == "3"); //查内容
                    var list2 = ds.Tables[0].AsEnumerable().Where(t => t["QuestionType"].ToString() == "6");//查询评分
                    var Grade = list2.Aggregate(0, (a, b) => a + Convert.ToInt32(b["OptionsText"])) / list2.Count();
                    var Commentcontent = list1.Aggregate("", (a, b) => a + b["Content"].ToString() + "\r\n ");
                    rd.VipName = VIPName;
                    rd.Grade = Grade;
                    rd.Commentcontent = Commentcontent;
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {
                throw new APIException(ex.Message);
            }

        }

        /// <summary>
        /// 活动列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetPanicbuyingEvent(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetPanicbuyingEventRP>>();
             var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            //var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new PanicbuyingEventRD();
            var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (rp.Parameters.EventTypeId != 0)
            {
                complexCondition.Add(new EqualsCondition() { FieldName = "EventTypeId", Value = rp.Parameters.EventTypeId });
                complexCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = loggingSessionInfo.ClientID });
            }

            //IsCTW是1时是创意活动，为0或者null时为非创意活动****
            if (rp.Parameters.EventTypeId == 1)
            {
                complexCondition.Add(new EqualsCondition() { FieldName = "IsCTW", Value = rp.Parameters.IsCTW });
            }
            else
            {
                complexCondition.Add(new DirectCondition(" (IsCTW is null or  IsCTW=0) "));
            }
            if (!string.IsNullOrEmpty(rp.Parameters.CTWEventId))
            {
                complexCondition.Add(new DirectCondition(" (Eventid in  (select leventid from T_CTW_LEventInteraction where  convert(varchar(50), ctwEventid)='" + rp.Parameters.CTWEventId + "' )) "));
            }


            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "StatusValue", Direction = OrderByDirections.Desc });
            //查询

            var tempEvent = eventBll.GetPanicbuyingEvent(complexCondition.ToArray(), lstOrder.ToArray(), rp.Parameters.PageSize, rp.Parameters.PageIndex + 1);
            List<PanicbuyingEvent> eventList = new List<PanicbuyingEvent> { };
            eventList.AddRange(tempEvent.Entities.Select(t => new PanicbuyingEvent()
            {
                EventId = t.EventId,
                EventName = t.EventName,
                EventTypeId = t.EventTypeId,
                BeginTime = t.BeginTime.ToString("yyyy-MM-dd HH:mm"),
                EndTime = t.EndTime.ToString("yyyy-MM-dd HH:mm"),
                BeginTimeName =  t.BeginTime.Month+"月"+t.BeginTime.Day+"日",  //t.BeginTime.ToString("MM-dd HH:mm"),
                CustomerID = t.CustomerID,
                Qty = t.Qty,
                RemainQty = t.RemainQty,
                EventStatus = t.EventStatusStr
            }));
            rd.PanicbuyingEventList = eventList.ToArray();
            rd.TotalPage = tempEvent.PageCount;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"IsSuccess\":true,\"Data\":{\"TotalPage\":1,\"PanicbuyingEventList\":[{\"CustomerID\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventTypeId\":1,\"EventId\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventName\":\"\u9524\u5B50\u624B\u673A\u56E2\u8D2D\",\"BeginTime\":\"2014-07-25 10:00\",\"EndTime\":\"2014-07-29 20:00\",\"Qty\":100,\"RemainQty\":10,\"EventStatus\":\"\u5DF2\u4E0A\u67B6\"},{\"CustomerID\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventTypeId\":1,\"EventId\":\"1E8CFF7F-214A-4DC2-BA1D-F61576A39824\",\"EventName\":\"\u82F9\u679C\u624B\u673A\u56E2\u8D2D\",\"BeginTime\":\"2014-07-25 10:00\",\"EndTime\":\"2014-07-29 20:00\",\"Qty\":100,\"RemainQty\":10,\"EventStatus\":\"\u5DF2\u4E0A\u67B6\"}]}}";
        }

        public string GetEventMerchandise(string pRequest)
        {
            EventMerchandiseRD rd = new EventMerchandiseRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<EventMerchandiseRP>>();

                //var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                DataSet ds = bll.GetEventMerchandise(rp.Parameters.EventId);
                if (ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    rd.ItemList = DataTableToObject.ConvertToList<Item>(ds.Tables[0]);

                    foreach (var item in rd.ItemList)
                    {
                        DataSet skuds = bll.GetGetEventMerchandiseSku(item.EventItemMappingId.ToString());
                        if (skuds.Tables.Count > 0 && skuds.Tables[0] != null)
                        {
                            item.SkuList = DataTableToObject.ConvertToList<Sku>(skuds.Tables[0]);
                        }
                    }
                }

                //查询
                var eventBll = new PanicbuyingEventBLL(loggingSessionInfo);
                var tempEvent = eventBll.GetPanicbuyingEventDetails(rp.Parameters.EventId);
                PanicbuyingEvent eventEntity = new PanicbuyingEvent();
                eventEntity.EventId = tempEvent.EventId;
                eventEntity.EventName = tempEvent.EventName;
                eventEntity.EventTypeId = tempEvent.EventTypeId;
                eventEntity.BeginTime = tempEvent.BeginTime.ToString("yyyy-MM-dd HH:mm");
                eventEntity.EndTime = tempEvent.EndTime.ToString("yyyy-MM-dd HH:mm");
                eventEntity.CustomerID = tempEvent.CustomerID;
                eventEntity.Qty = tempEvent.Qty;
                eventEntity.RemainQty = tempEvent.RemainQty;
                eventEntity.EventStatus = tempEvent.EventStatus.ToString();
                TimeSpan nowSpan = DateTime.Now - tempEvent.EndTime;

                eventEntity.DeadlineTime = nowSpan.Days + "天" + nowSpan.Hours + "时" + nowSpan.Minutes + "分" + nowSpan.Seconds;
                    eventEntity.DeadlineSecond =Convert.ToInt32( nowSpan.TotalSeconds);

                rd.PanicbuyingEvent = eventEntity;


                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"Data\":{\"ItemList\":[{\"ItemID\":\"22\",\"ItemName\":\"美的\",\"ImageUrl\":\"http://www.o2omarketing.cn: 8400/Framework/Javascript/Other/kindeditor/attached/image/lzlj/album1.jpg\",\"SkuList\":[{\"kuID\":\"1111\",\"SkuName\":\"138L(银灰色)\",\"Qty\":\"250\",\"KeepQty\":\"200\",\"SoldQty\":\"30\",\"InverTory\":\"20\"}]}]}}";
        }

        public string GetTCTWPanicbuyingEventKV(string pRequest)
        {
            TCTWPanicbuyingEventKVRD rd = new TCTWPanicbuyingEventKVRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<APIRequest<GetTCTWPanicbuyingEventKVRP>>();

                //var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                var bll = new PanicbuyingEventSkuMappingBLL(loggingSessionInfo);
                DataSet ds = bll.GetTCTWPanicbuyingEventKV(rp.Parameters.CTWEventId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count!=0)
                {
                    rd.TCTWPanicbuyingEventKVInfo = DataTableToObject.ConvertToObject<T_CTW_PanicbuyingEventKVEntity>(ds.Tables[0].Rows[0]);

                  
                }
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception ex)
            {

                throw new APIException(ex.Message);
            }
            //return "{\"ResultCode\":0,\"Message\":\"OK\",\"Data\":{\"ItemList\":[{\"ItemID\":\"22\",\"ItemName\":\"美的\",\"ImageUrl\":\"http://www.o2omarketing.cn: 8400/Framework/Javascript/Other/kindeditor/attached/image/lzlj/album1.jpg\",\"SkuList\":[{\"kuID\":\"1111\",\"SkuName\":\"138L(银灰色)\",\"Qty\":\"250\",\"KeepQty\":\"200\",\"SoldQty\":\"30\",\"InverTory\":\"20\"}]}]}}";
        }


        //protected string SendQrCodeWxMessage(string pRequest)
        //{
        //    var rp = pRequest.DeserializeJSONTo<APIRequest<SendQrCodeWxMessageRP>>();

        //    if (rp.CustomerID == null || string.IsNullOrEmpty(rp.CustomerID))
        //    {
        //        throw new APIException("缺少参数【CustomerID】或参数值为空") { ErrorCode = 121 };
        //    }
        //    if (rp.OpenID == null || string.IsNullOrEmpty(rp.OpenID))
        //    {
        //        throw new APIException("缺少参数【OpenID】或参数值为空") { ErrorCode = 122 };
        //    }

        //    if (rp.UserID == null || string.IsNullOrEmpty(rp.UserID))
        //    {
        //        throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 123 };
        //    }
        //    if (rp.Parameters.QrCodeId == null || string.IsNullOrEmpty(rp.Parameters.QrCodeId))
        //    {
        //        throw new APIException("缺少参数【QrCodeId】或参数值为空") { ErrorCode = 124 };
        //    }

        //    var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

        //    var eventsBll = new LEventsBLL(loggingSessionInfo);
                    

        //    var qrCodeBll = new WQRCodeManagerBLL(loggingSessionInfo);

        //    var qrCodeEntity = qrCodeBll.QueryByEntity(new WQRCodeManagerEntity()
        //        {
        //            CustomerId = rp.CustomerID,
        //            QRCode = rp.Parameters.QrCodeId
        //        }, null).FirstOrDefault();

        //    if (qrCodeEntity != null)
        //    {
        //        var wapplicationBll = new WApplicationInterfaceBLL(loggingSessionInfo);

        //        var wappEntity = wapplicationBll.QueryByEntity(new WApplicationInterfaceEntity()
        //        {
        //            CustomerId = rp.CustomerID
        //        }, null).FirstOrDefault();

        //        var weixinId = "";

        //        if (wappEntity != null)
        //        {
        //            weixinId = wappEntity.WeiXinID;
        //        }

        //        if (weixinId != "")
        //        {
        //            eventsBll.QrCodeHandlerText(qrCodeEntity.QRCodeId.ToString(), loggingSessionInfo,
        //                weixinId, 4, rp.OpenID, base.httpContext);
        //        }
        //    }       

        //    var rd = new EmptyResponseData();
        //    var rsp = new SuccessResponse<IAPIResponseData>(rd);

        //    return rsp.ToJSON();
            
        //}


        #region 获取红包列表

        public string GetEventUserPrizeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetEventUserPrizeListRP>>();
            var rd = new GetEventUserPirzeListRD();

          
            if (rp.Parameters.EventId == "" || string.IsNullOrEmpty(rp.Parameters.EventId))
            {
                throw new APIException("活动标识不能为空") { ErrorCode = 121 };                
            }
            if (rp.UserID == "" || string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("会员标识不能为空") { ErrorCode = 121 };             
            }
            if (rp.CustomerID == "" || string.IsNullOrEmpty(rp.CustomerID))
            {
                throw new APIException("客户标识不能为空") { ErrorCode = 121 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);

            var ds = poolsServer.GetUserPrizeWinnerLog(rp.Parameters.EventId, rp.UserID);

            if(ds.Tables[0].Rows.Count>0)
            {
                var temp = ds.Tables[0].AsEnumerable().Select(t => new GetEventUserPrizeListInfo()
                {
                    PrizeDesc = t["PrizeDesc"].ToString(),
                    CreateTime = t["CreateTime"].ToString()
                });
                rd.GetEventUserPirzeList = temp.ToArray();

            }
            
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetEventCommentList":   //获取评论列表  Add by changjian.tian 2014-5-27
                    rst = GetEventCommentList(pRequest);
                    break;
                case "GetEventUserPrizeList":
                    rst = GetEventUserPrizeList(pRequest);
                    break;
                case "GetTCTWPanicbuyingEventKV":
                    rst = this.GetTCTWPanicbuyingEventKV(pRequest);
                    break;
                //case "SendQrCodeWxMessage":
                //    rst = SendQrCodeWxMessage(pRequest);
                //    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }
    }
    public class EventCommentListRD : IAPIResponseData
    {
        /// <summary>
        /// 会员名称
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 评分 由所有评分取平均值
        /// </summary>
        public int Grade { get; set; }
        /// <summary>
        /// 品论内容 由所有问题加答案拼接而成
        /// </summary>
        public string Commentcontent { get; set; }
    }
    public class EventCommentListRP : IAPIRequestParameter
    {
        /// <summary>
        /// 问卷ID
        /// </summary>
        public string QuestionnaireID { get; set; }

        public void Validate()
        {
        }
    }


    public class GetEventUserPrizeListRP : IAPIRequestParameter
    {
        public string EventId { get; set; }

        public void Validate()
        {            
        }
    }

    public class GetEventUserPirzeListRD : IAPIResponseData
    {
        public GetEventUserPrizeListInfo[] GetEventUserPirzeList { get; set; }
    }
    public class GetEventUserPrizeListInfo
    {
        public string PrizeDesc { get; set; }
        public string CreateTime { get; set; }
    }

    public class SendQrCodeWxMessageRP : IAPIRequestParameter
    {
        public string QrCodeId { get; set; }

        public void Validate()
        {
        }
    }


    public class GetTCTWPanicbuyingEventKVRP : IAPIRequestParameter
    {
        public string CTWEventId { get; set; }

        public void Validate()
        {
        }
    }



    public class TCTWPanicbuyingEventKVRD : IAPIResponseData
    {
        public T_CTW_PanicbuyingEventKVEntity TCTWPanicbuyingEventKVInfo { get; set; }
    }
}