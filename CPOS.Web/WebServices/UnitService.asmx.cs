using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using System.Web.UI.WebControls;
using JIT.CPOS.Common;

namespace JIT.CPOS.Web.WebServices
{
    /// <summary>
    /// UnitService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class UnitService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetUnitIdList(string cityId, string timestamp)
        {
            var currentUser = Default.GetBSLoggingSession("f6a7da3d28f74f2abedfc3ea0cf65c01", null);//  03201e18799a47179d2f6bf8424d86c9
            string content = string.Empty;

            var dataStr = "";
            var url = "http://112.124.43.61:8009/GetNumService.asmx/GetNum?date=" + timestamp + "";
            dataStr = Utils.GetRemoteData(url, "GET", "");
            if (dataStr == null || dataStr.Equals("0"))
            {
                //根据区县ID + 时间戳获取这段时间内发生订单的门店ID
                if (!string.IsNullOrEmpty(cityId) && !string.IsNullOrEmpty(timestamp))
                {
                    //var inoutService = new InoutService(Default.GetLoggingSession());
                    var inoutService = new InoutService(currentUser);
                    var data = inoutService.GetUnitIdList(cityId, timestamp);

                    content = string.Format("{{\"UnitList\":{0}}}", data.ToJSON());
                }
            }
            else {
                IList<BS.Entity.Unit.UnitInfo> unitList = new List<BS.Entity.Unit.UnitInfo>();
                BS.Entity.Unit.UnitInfo unitInfo = new BS.Entity.Unit.UnitInfo();
                unitInfo.UnitId = "1";
                unitList.Add(unitInfo);
                content = string.Format("{{\"UnitList\":{0}}}", unitList.ToJSON());
            }
            return content;
        }
    }
}
