using System.Web;
using System.Web.SessionState;
using JIT.CPOS.BS.BLL.Control;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;


namespace JIT.CPOS.BS.Web.Framework.Javascript.Biz.Handler
{
    /// <summary>
    /// OptionsHandler 的摘要说明
    /// </summary>
    public class OptionsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        protected override void AjaxRequest(HttpContext pContext)
        {
            //if (!new JIT.TenantPlatform.Web.PageBase.JITPage().CheckUserLogin())
            //{
            //    HttpContext.Current.Response.Write("{success:false,msg:'未登录，请先登录'}");
            //    HttpContext.Current.Response.End();
            //}
            string res = "";
            switch (pContext.Request["method"])
            {
                case "GetOptionsByName":
                    res = GetOptionsByName(pContext.Request.QueryString["OptionName"]);
                    break;
                case "GetLEventsType":
                    res = GetLEventsType();
                    break;
                case "GetMobileModule":
                    res = GetMobileModule();
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetOptionsByName
        /// <summary>
        /// 根据OptionName查询Options表的基础信息
        /// </summary>
        /// <param name="pOptionName">options表中的OptionName</param>
        /// <returns>string</returns>
        private string GetOptionsByName(string pOptionName)
        {
         
           // return pOptionName; 
            //这里要传入一个customer_id，只筛选当前的用户的信息
            return new ControlBLL(CurrentUserInfo).GetOptionsByClientID(pOptionName, CurrentUserInfo.CurrentUser.customer_id).ToJSON();
        }
        #endregion

        #region GetLEventsType
        /// <summary>
        /// 获取EventType数据
        /// </summary>
        /// <returns></returns>
        private string GetLEventsType()
        {            
            return new ControlBLL(CurrentUserInfo).GetLEventsType().Tables[0].ToJSON();
        }
        #endregion

        #region GetMobileModule
        /// <summary>
        /// 获取MobileModule数据
        /// </summary>
        /// <returns></returns>
        private string GetMobileModule()
        {
            return new ControlBLL(CurrentUserInfo).GetMobileModule().Tables[0].ToJSON();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}