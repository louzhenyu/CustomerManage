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

namespace JIT.CPOS.BS.Web.Module.EnterpriseMemberStructure.Handler
{
    /// <summary>
    /// EnterpriseMemberStructureHandler 的摘要说明
    /// </summary>
    public class EnterpriseMemberStructureHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                        case "GetEnterpriseMemberStructureList":
                            res = GetEnterpriseMemberStructureList(pContext.Request.Form);
                            break;
                        case "GetEnterpriseMemberStructureByID":
                            res = GetEnterpriseMemberStructureByID(pContext.Request.Form["id"]);
                            break;
                    }
                    break;
                case "delete":
                    res = DeleteEnterpriseMemberStructure(pContext.Request.Form["id"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetEnterpriseMemberStructureByID":
                                res = GetEnterpriseMemberStructureByID(pContext.Request.Form["id"]);
                                break;
                            case "EditEnterpriseMemberStructure":
                                res = EditEnterpriseMemberStructure(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetEnterpriseMemberStructureByID":
                                res = GetEnterpriseMemberStructureByID(pContext.Request.Form["id"]);
                                break;
                            case "EditEnterpriseMemberStructure":
                                res = EditEnterpriseMemberStructure(pContext.Request.Form);
                                break;

                        }
                    }
                    break;

            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetEnterpriseMemberStructureList
        private string GetEnterpriseMemberStructureList(NameValueCollection rParams)
        {
            EnterpriseMemberStructureEntity entity = new EnterpriseMemberStructureEntity();
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                new EnterpriseMemberStructureBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region GetEnterpriseMemberStructureByID
        private string GetEnterpriseMemberStructureByID(string id)
        {
            return "[" + new EnterpriseMemberStructureBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion

        #region EditEnterpriseMemberStructure
        private string EditEnterpriseMemberStructure(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            //组装参数
            EnterpriseMemberStructureEntity entity = new EnterpriseMemberStructureEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new EnterpriseMemberStructureBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<EnterpriseMemberStructureEntity>(rParams, entity);

            entity.ClientID = CurrentUserInfo.ClientID;
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new EnterpriseMemberStructureBLL(CurrentUserInfo).Update(entity);
            }
            else
            {
                new EnterpriseMemberStructureBLL(CurrentUserInfo).Create(entity);
            }
            res = "{success:true,msg:'编辑成功',id:'" + entity.EnterpriseMemberStructureID + "'}";
            return res;
        }
        #endregion

        #region DeleteEnterpriseMemberStructure
        private string DeleteEnterpriseMemberStructure(string id)
        {
            string res = "{success:false}";
            EnterpriseMemberStructureEntity entity = new EnterpriseMemberStructureBLL(CurrentUserInfo).GetByID(Guid.Parse(id));
            new EnterpriseMemberStructureBLL(CurrentUserInfo).Delete(entity);
            res = "{success:true}";
            return res;
        }
        #endregion
    }
}