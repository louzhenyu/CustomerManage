using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.BLL
{
    public class BuryingPoint_Gateway : BaseBuryingPoint
    {
        public BuryingPoint_Gateway(HttpContext context, string returnData)
            : base(context)
        {
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
                //用户
                buryingPoint.UserID = commonRequest.UserID;
                //OpenID
                buryingPoint.OpenID = commonRequest.OpenID;
                //频道
                buryingPoint.ChannelID = commonRequest.ChannelId;
            }
        }
    }
}
