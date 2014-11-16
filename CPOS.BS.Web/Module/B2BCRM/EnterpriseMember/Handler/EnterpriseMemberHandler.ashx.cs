using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Extension;
using System.Collections.Specialized;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using System.Web.Script.Serialization;

namespace JIT.CPOS.BS.Web.Module.EnterpriseMember.Handler
{
    /// <summary>
    /// EnterpriseMemberHandler 的摘要说明
    /// </summary>
    public class EnterpriseMemberHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (this.BTNCode)
            {
                case "search":
                    switch (this.Method)
                    {
                        case "GetEnterpriseMemberList":
                            res = GetEnterpriseMemberList(pContext.Request.Form);
                            break;
                        case "GetEnterpriseMemberByID":
                            res = GetEnterpriseMemberByID(pContext.Request.Form["id"]);
                            break;
                    }
                    break;
                case "delete":
                    res = DeleteEnterpriseMember(pContext.Request.Form["id"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetEnterpriseMemberByID":
                                res = GetEnterpriseMemberByID(pContext.Request.Form["id"]);
                                break;
                            case "EditEnterpriseMember":
                                res = EditEnterpriseMember(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetEnterpriseMemberByID":
                                res = GetEnterpriseMemberByID(pContext.Request.Form["id"]);
                                break;
                            case "EditEnterpriseMember":
                                res = EditEnterpriseMember(pContext.Request.Form);
                                break;

                        }
                    }
                    break;

            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetEnterpriseMemberList
        private string GetEnterpriseMemberList(NameValueCollection rParams)
        {
            EnterpriseMemberEntity entity = new EnterpriseMemberEntity();
            string memberName = Request("memberName").Trim('"');
            if (!string.IsNullOrEmpty(memberName))
            {
                entity.MemberName = memberName;
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                new EnterpriseMemberBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region GetEnterpriseMemberByID
        private string GetEnterpriseMemberByID(string id)
        {
            return "[" + new EnterpriseMemberBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion

        #region EditEnterpriseMember
        private string EditEnterpriseMember(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            //组装参数
            EnterpriseMemberEntity entity = new EnterpriseMemberEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new EnterpriseMemberBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<EnterpriseMemberEntity>(rParams, entity);

            entity.ClientID = CurrentUserInfo.ClientID;
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new EnterpriseMemberBLL(CurrentUserInfo).Update(entity);
            }
            else
            {
                new EnterpriseMemberBLL(CurrentUserInfo).Create(entity);
            }
            res = "{success:true,msg:'编辑成功',id:'" + entity.EnterpriseMemberID + "'}";
            return res;
        }
        #endregion

        #region DeleteEnterpriseMember
        private string DeleteEnterpriseMember(string id)
        {
            string res = "{success:false}";
            EnterpriseMemberEntity entity = new EnterpriseMemberBLL(CurrentUserInfo).GetByID(Guid.Parse(id));
            new EnterpriseMemberBLL(CurrentUserInfo).Delete(entity);
            res = "{success:true}";
            return res;
        }
        #endregion
    }
}