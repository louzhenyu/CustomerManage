using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    /// <summary>
    /// WeiXinOrderPaidNotification 的摘要说明
    /// </summary>
    public class WeiXinOrderPaidNotification : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var signType = context.Request["sign_type"];
            var sign = context.Request["sign"];
            //var signKeyIndex =context.Request[
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}