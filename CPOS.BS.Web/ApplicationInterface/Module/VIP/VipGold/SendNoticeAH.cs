using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class SendNoticeAH : BaseActionHandler<SendNoticeRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SendNoticeRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var InnerGroupNewsBll = new InnerGroupNewsBLL(this.CurrentUserInfo);

            foreach (var item in para.NoticeInfoList)
            {
                var Data = new InnerGroupNewsEntity();
                Data.GroupNewsId = System.Guid.NewGuid().ToString();
                Data.ObjectId = item.SetoffEventID;
                Data.NoticePlatformType = item.NoticePlatformType;
                Data.Text = item.Text;
                Data.Title = item.Title;
                Data.SentType = 1;
                Data.BusType = 2;
                Data.CustomerID = this.CurrentUserInfo.ClientID;
                //
                InnerGroupNewsBll.Create(Data);
            } 


            return rd;
        }
    }
}