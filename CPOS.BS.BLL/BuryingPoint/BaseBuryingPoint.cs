using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.Common;
using JIT.Utility.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIT.CPOS.BS.BLL
{
    public class BaseBuryingPoint
    {
        protected BuryingPointEntity buryingPoint = new BuryingPointEntity();//埋点数据
        protected HttpContext context;
        protected string returnData;//接口返回数据

        public BaseBuryingPoint()
        {
        }
        public BaseBuryingPoint(HttpContext context)
        {
            this.context = context;
        }

        protected void Process()
        {
            //get data from Context
            FromContext();

            //param,公众参数
            CommonParameters();

            //返回数据
            ReturnData();

            //Task task = new Task(() =>
            //{
            //写数据
            Commit();
            //});
            //task.Start();
        }

        //get data from Context
        private void FromContext()
        {
            //请求日期
            buryingPoint.RequestDate = context.Timestamp.Date;

            //请求时间
            buryingPoint.RequestTime = context.Timestamp;

            //请求端SessionID
            buryingPoint.SessionID = context.Request.Cookies["ASP.NET_SessionId"] == null ? "" : context.Request.Cookies["ASP.NET_SessionId"].Value;

            //Header
            Dictionary<string, string> dHeader = new Dictionary<string, string>();
            foreach (var item in context.Request.Headers.AllKeys)
            {
                dHeader.Add(item, context.Request.Headers[item]);
            }
            buryingPoint.Header = JsonHelper.Dictionary2Json(dHeader);

            //Body
            Dictionary<string, string> dBody = new Dictionary<string, string>();
            foreach (var item in context.Request.Form.AllKeys)
            {
                dBody.Add(item, context.Request.Form[item]);
            }
            buryingPoint.Body = JsonHelper.Dictionary2Json(dBody);

            //URL
            buryingPoint.URL = context.Request.Url.AbsoluteUri;

            //系统类型
            buryingPoint.SysType = context.Request.QueryString["type"];

            //action
            try
            {
                buryingPoint.Action = context.Request["action"];
            }
            catch { };
            if (string.IsNullOrEmpty(buryingPoint.Action))
            {
                buryingPoint.Action = context.Request["method"];
            }

            //请求参数
            try
            {
                buryingPoint.RequstParameters = context.Request["req"];
            }
            catch { };
            if (string.IsNullOrEmpty(buryingPoint.RequstParameters))
            {
                try
                {
                    buryingPoint.RequstParameters = context.Request["ReqContent"];
                }
                catch { };
            }
        }

        //param,公众参数
        public virtual void CommonParameters()
        {
        }

        //返回数据
        private void ReturnData()
        {
            buryingPoint.ReturnData = returnData;
            buryingPoint.ReturnTime = DateTime.Now;
        }

        //写数据
        private void Commit()
        {
            buryingPoint.CreateTime = DateTime.Now;
            string json = JsonHelper.JsonSerializerForRedis(buryingPoint);
            //string json = JsonHelper.JsonSerializerForRedis(new BuryingPointEntity
            //{
            //    Action = "yyyy",
            //    Body = "33333",
            //    ChannelID = "00000000",
            //    CreateTime = DateTime.Now,
            //    CustomerID = "ddddddddddddddddd",
            //    Header = "ddddddddxxxxxxxxx",
            //    OpenID = "oooooooo",
            //    RequestDate = DateTime.Now,
            //    RequestTime = DateTime.Now,
            //    RequstParameters = "aaaaaaaa",
            //    ReturnData = "rrrrrrrrrrrrrrr",
            //    ReturnTime = DateTime.Now,
            //    SessionID = "sssssssssss",
            //    SysType = "tttttttttttt",
            //    URL = "llllllllllllllllllll",
            //    UserID = "iiiiiiiiiiiiiiiiiiiiiiii"
            //});
            string url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RedisApiUrl"]);
            if (string.IsNullOrWhiteSpace(url))
            {
                return;
            }
            url += "BIStatistic/SetBIUserData";
            //string ret = HttpClient.PostQueryString(url, json);
            log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
            logger.Info(string.Format("请求数据:{0}", json));
            var ret = CommonBLL.GetRemoteDataForRedis(url, "POST", json);
            logger.Info(string.Format("请求结果:{0}", ret));
        }
    }
}
