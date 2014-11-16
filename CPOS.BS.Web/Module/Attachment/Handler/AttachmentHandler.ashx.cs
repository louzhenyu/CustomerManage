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

namespace JIT.CPOS.BS.Web.Module.Attachment.Handler
{
    /// <summary>
    /// AttachmentHandler 的摘要说明
    /// </summary>
    public class AttachmentHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                        case "GetAttachmentList":
                            res = GetAttachmentList(pContext.Request.Form);
                            break;
                        case "GetAttachmentByID":
                            res = GetAttachmentByID(pContext.Request.Form["id"]);
                            break;
                    }
                    break;
                case "delete":
                    res = DeleteAttachment(pContext.Request.Form["id"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetAttachmentByID":
                                res = GetAttachmentByID(pContext.Request.Form["id"]);
                                break;
                            case "EditAttachment":
                                res = EditAttachment(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetAttachmentByID":
                                res = GetAttachmentByID(pContext.Request.Form["id"]);
                                break;
                            case "EditAttachment":
                                res = EditAttachment(pContext.Request.Form);
                                break;

                        }
                    }
                    break;

            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetAttachmentList
        private string GetAttachmentList(NameValueCollection rParams)
        {
            AttachmentEntity entity = new AttachmentEntity();
            string title = Request("AttachmentTitle").Trim('"');
            if (!string.IsNullOrEmpty(title))
            {
                entity.AttachmentTitle = title;
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                new AttachmentBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region GetAttachmentByID
        private string GetAttachmentByID(string id)
        {
            return "[" + new AttachmentBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion

        #region EditAttachment
        private string EditAttachment(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            //组装参数
            AttachmentEntity entity = new AttachmentEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new AttachmentBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<AttachmentEntity>(rParams, entity);

            entity.ClientID = CurrentUserInfo.ClientID;
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new AttachmentBLL(CurrentUserInfo).Update(entity);
            }
            else
            {
                new AttachmentBLL(CurrentUserInfo).Create(entity);
            }
            res = "{success:true,msg:'编辑成功',id:'" + entity.AttachmentID + "'}";
            return res;
        }
        #endregion

        #region DeleteAttachment
        private string DeleteAttachment(string id)
        {
            string res = "{success:false}";
            AttachmentEntity entity = new AttachmentBLL(CurrentUserInfo).GetByID(Guid.Parse(id));
            new AttachmentBLL(CurrentUserInfo).Delete(entity);
            res = "{success:true}";
            return res;
        }
        #endregion
    }
}