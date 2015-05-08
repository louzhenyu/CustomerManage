using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;

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
}