using JIT.CPOS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JIT.CPOS.BS.BLL
{
    public class BaseBuryingPoint
    {
        protected BuryingPointEntity buryingPoint = new BuryingPointEntity();

        public BaseBuryingPoint()
        {
        }

        public BaseBuryingPoint(HttpContext context)
        {
            //请求日期
            buryingPoint.RequestDate = DateTime.Now.Date;

            //请求时间
            buryingPoint.RequestTime = DateTime.Now;

            //请求端SessionID
            buryingPoint.SessionID = context.Request.Cookies["ASP.NET_SessionId"] == null ?"":context.Request.Cookies["ASP.NET_SessionId"].ToString();

            //Header
            buryingPoint.Header = context.Request.RequestContext.ToString();

            //Body
            //buryingPoint.Body = JsonHelper.JsonSerializer(context.Request.b);

            //URL
            buryingPoint.URL = context.Request.Url.AbsoluteUri;

            //

        }
    }
}
