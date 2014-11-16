using System;
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
    public partial class ItemData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            var response = new APIResponse() { code = "200", description = "操作成功" };
            try
            {

                string action = Request["action"].ToString().Trim();
                //JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, action);

                string reqContent = Request["ReqContent"];
                var request = reqContent.DeserializeJSONTo<APIRequest>();

                switch (action)
                {
                    case "getPanicbuyingItemList": 
                        response.content = DataAPI.GetPanicbuyingItemList(request);
                        break;
                    case "getPanicbuyingEventList"://新版获取活动列表
                        response.content = DataAPI.GetPanicbuyingEventList(request);
                        break;
                    case "getPanicbuyingItemDetail": 
                        response.content = DataAPI.GetPanicbuyingItemDetail(request);
                        break;
                    case "setOrderInfo":
                        response.content = DataAPI.SetOrderInfo(request);
                        break;
                    default:
                        throw new Exception("错误的接口:"+action);
                }
            }
            catch (Exception ex)
            {
                response.code = "500";
                response.description = ex.Message;
                JIT.Utility.Log.Loggers.Exception(new Utility.Log.ExceptionLogInfo(ex));
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(response.ToJSON());
            Response.End();
        }
    }
}