using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.BLL
{
    public class BuryingPoint_BSGateway : BaseBuryingPoint
    {
        private UserInfo userInfo;//用户信息

        public BuryingPoint_BSGateway(HttpContext context, UserInfo userInfo, string returnData)
            : base(context)
        {
            this.userInfo = userInfo;
            this.returnData = returnData;
            Process();
        }

        //获取请求参数的公共参数部分
        public override void CommonParameters()
        {
            var commonRequest = buryingPoint.RequstParameters.DeserializeJSONTo<EmptyRequest>();   //将请求反序列化为空接口请求,获得接口的公共参数
            if (commonRequest != null)
            {
                //商户
                buryingPoint.CustomerID = commonRequest.CustomerID;
                if (string.IsNullOrEmpty(buryingPoint.CustomerID) && userInfo != null)
                {
                    buryingPoint.CustomerID = userInfo.customer_id;
                }

                //用户
                buryingPoint.UserID = commonRequest.UserID;
                if (string.IsNullOrEmpty(buryingPoint.UserID) && userInfo != null)
                {
                    buryingPoint.UserID = userInfo.User_Id;
                }

                //OpenID
                buryingPoint.OpenID = commonRequest.OpenID;
                //频道
                buryingPoint.ChannelID = commonRequest.ChannelId;
            }
        }
    }
}
