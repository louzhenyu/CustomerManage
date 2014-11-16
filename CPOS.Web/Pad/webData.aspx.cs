using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using System.Text;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;

namespace JIT.CPOS.Web.Pad
{
    public partial class webData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            try
            {
                string dataType = Request["dataType"].ToString().Trim();
                switch (dataType)
                {
                    case "GetShowCount"://获取数据
                        content = GetShowCount();
                        break;
                    default:
                        break;
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

        public string GetShowCount()
        {
            string Timestamp = Request["Timestamp"].ToString().Trim();
            if (Timestamp == null || Timestamp.Equals(""))
            {
                Timestamp = "0";
            }
            string content = string.Empty;
            VipBLL vipService = new VipBLL(Default.GetLoggingSession());
            var respData = new RespData();
            try
            {
                respData.Code = "200";
                respData.Description = "操作成功";
                respData.count = vipService.GetShowCount(Convert.ToInt64(Timestamp), out respData.NewTimestamp);
            }
            catch (Exception ex) {
                respData.Code = "201";
                respData.Description = "操作失败";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class RespData
        {
            public string Code;
            public string Description;
            public string Exception = null;
            public string Data;
            public int count;
            public long NewTimestamp;
        }
        #endregion
    }
}