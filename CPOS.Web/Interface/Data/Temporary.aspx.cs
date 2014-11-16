using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.Web.Interface.Data.Base;

namespace JIT.CPOS.Web.Interface.Data
{
    public partial class Temporary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            var response = new APIResponse() { code = "200", description = "操作成功" };
            try
            {

                string action = Request["action"].ToString().Trim();
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, action);

                string reqContent = Request["ReqContent"];
                var request = reqContent.DeserializeJSONTo<APIRequest>();

                switch (action)
                {
                    case "getItemList":     //获取首页商品列表
                        {
                            var key = string.Format("index_list_for_{0}", request.common.customerId);
                            var key4EndTime = string.Format("index_list_for_{0}_end_time", request.common.customerId);
                            var content = ConfigurationManager.AppSettings[key];
                            var strEndTime = ConfigurationManager.AppSettings[key4EndTime];
                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                var endTimes = strEndTime.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                int index = 1;
                                foreach (var t in endTimes)
                                {
                                    DateTime dt;
                                    if (DateTime.TryParse(t, out dt))
                                    {
                                        var totalSecondsToEndTime = Convert.ToInt64((dt - DateTime.Now).TotalSeconds);
                                        if (totalSecondsToEndTime < 0)
                                            totalSecondsToEndTime = 0;
                                        content = content.Replace(string.Format("$deadlineSecond_{0}$", index), totalSecondsToEndTime.ToString());
                                    }
                                    index++;
                                }
                            }
                            var result = string.Format("{{\"code\":\"200\",\"content\":{0}}}", content);
                            Response.ContentEncoding = Encoding.UTF8;
                            Response.Write(result);
                            Response.End();
                        }
                        break;
                    default:
                        throw new Exception("未找到指定的接口：" + action);
                }
            }
            catch (Exception ex)
            {
                response.code = "500";
                response.description = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(response.ToJSON());
            Response.End();
        }
    }
}