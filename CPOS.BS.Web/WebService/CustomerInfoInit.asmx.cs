using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.Web.WebService
{
    /// <summary>
    /// CustomerInfoInit 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class CustomerInfoInit : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "设置客户初始化信息")]
        public bool SetCustomerInfoInit(string sCustomerInfo, string strUnitInfo, string strMenu, string typeId)
        {
            InitialService initialService = new InitialService();
            return initialService.SetBSInitialInfo(sCustomerInfo, strUnitInfo, strMenu, typeId);
        }
    }
}
