using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity.Pos;
using System.Collections;

namespace JIT.CPOS.BS.Web.Module.Vip.VipLevel.Handler
{
    /// <summary>
    /// VipHandler 的摘要说明
    /// </summary>
    public class VipHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "search_vip":
                    content = GetVipLevelListData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetVipLevelListData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetVipLevelListData()
        {
            string content = string.Empty;
            SysVipCardGradeBLL service = new SysVipCardGradeBLL(CurrentUserInfo);

            var listObj = service.QueryByEntity(new SysVipCardGradeEntity {
                CustomerID = CurrentUserInfo.CurrentUser.customer_id
            }, null) ;
            if (listObj != null && listObj.Length > 0){

                foreach (SysVipCardGradeEntity info in listObj)
                {
                    info.VipLevelCount = service.GetVipLevelCount(Convert.ToString(info.VipCardGradeID));
                }
            }
            var jsonData = new JsonData();
            jsonData.totalCount = listObj.Length.ToString();
            jsonData.data = listObj;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
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