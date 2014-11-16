using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.EventPrizes.Request;
using JIT.CPOS.DTO.Module.Event.EventPrizes.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.EventPrizes
{
    public class GetPrizeEventAH : BaseActionHandler<GetEventPrizesRP, GetEventPrizesRD>
    {
        protected override GetEventPrizesRD ProcessRequest(DTO.Base.APIRequest<GetEventPrizesRP> pRequest)
        {
            GetEventPrizesRD rd = new GetEventPrizesRD();

            //string vipID = pRequest.UserID;
            //string vipID = "f3d925e364e34bf69dfda34fcedc58f8";
            string vipName = string.Empty;
            string reCommandId = pRequest.Parameters.RecommandId;
            string eventId = pRequest.Parameters.EventId;
            float longitude = pRequest.Parameters.Longitude;
            float latitude = pRequest.Parameters.Latitude;
            string customerId = this.CurrentUserInfo.ClientID;
            int pointsLotteryFlag = pRequest.Parameters.PointsLotteryFlag;
            if (string.IsNullOrEmpty(customerId))
            {
                customerId = "f6a7da3d28f74f2abedfc3ea0cf65c01";
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            var leventsBll = new LEventsBLL(this.CurrentUserInfo);
            var vipService = new VipBLL(loggingSessionInfo);
            var levents = leventsBll.QueryByEntity(new LEventsEntity(){ EventID = eventId},null);
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("zk levent==null:{0}", null == levents) });
            if (null == levents || levents.Length == 0) return rd;
            var levent = levents.FirstOrDefault();
            rd.IsShare = levent.IsShare == null ? 0 : levent.IsShare.Value;
            rd.BootUrl = levent.BootURL;
            rd.PosterImageUrl = levent.PosterImageUrl;
            rd.ShareLogoUrl = levent.ShareLogoUrl;
            rd.ShareRemark = levent.ShareRemark;
            rd.OverRemark = levent.OverRemark;
            if (string.IsNullOrEmpty(levent.EventFlag))
                rd.SignFlag = 0;
            else
            {
                if (levent.EventFlag.Substring(0, 1) == "1")
                {
                    rd.SignFlag = 1;
                }
                else
                    rd.SignFlag = 0;
            }
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("zk levent.BootURL:{0}", levent.BootURL) });
            return rd;
        }
    }
}