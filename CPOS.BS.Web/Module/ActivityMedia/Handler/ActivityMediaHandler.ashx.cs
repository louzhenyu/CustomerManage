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

namespace JIT.CPOS.BS.Web.Module.ActivityMedia.Handler
{
    /// <summary>
    /// ActivityMediaHandler 的摘要说明
    /// </summary>
    public class ActivityMediaHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                        case "GetActivityMediaList":
                            res = GetActivityMediaList(pContext.Request.Form);
                            break;
                        case "GetActivityMediaByID":
                            res = GetActivityMediaByID(pContext.Request.Form["id"]);
                            break;
                    }
                    break;
                case "delete":
                    res = DeleteActivityMedia(pContext.Request.Form["id"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetActivityMediaByID":
                                res = GetActivityMediaByID(pContext.Request.Form["id"]);
                                break;
                            case "EditActivityMedia":
                                res = EditActivityMedia(pContext.Request.Form);
                                break;

                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetActivityMediaByID":
                                res = GetActivityMediaByID(pContext.Request.Form["id"]);
                                break;
                            case "EditActivityMedia":
                                res = EditActivityMedia(pContext.Request.Form);
                                break;

                        }
                    }
                    break;

            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetActivityMediaList 
        private string GetActivityMediaList(NameValueCollection rParams)
        {
            ActivityMediaEntity entity = new ActivityMediaEntity();
            string title = Request("MediaTitle").Trim('"');
            if (!string.IsNullOrEmpty(title))
            {
                entity.MediaTitle = title;
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                new ActivityMediaBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region GetActivityMediaByID
        private string GetActivityMediaByID(string id)
        {
            return "[" + new ActivityMediaBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion

        #region EditActivityMedia
        private string EditActivityMedia(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            //组装参数
            ActivityMediaEntity entity = new ActivityMediaEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new ActivityMediaBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<ActivityMediaEntity>(rParams, entity);

            entity.ClientID = CurrentUserInfo.ClientID;
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new ActivityMediaBLL(CurrentUserInfo).Update(entity);
            }
            else
            {
                new ActivityMediaBLL(CurrentUserInfo).Create(entity);
            }
            res = "{success:true,msg:'编辑成功',id:'" + entity.ActivityMediaID + "'}";
            return res;
        }
        #endregion

        #region DeleteActivityMedia
        private string DeleteActivityMedia(string id)
        {
            string res = "{success:false}";
            ActivityMediaEntity entity = new ActivityMediaBLL(CurrentUserInfo).GetByID(Guid.Parse(id));
            new ActivityMediaBLL(CurrentUserInfo).Delete(entity);
            res = "{success:true}";
            return res;
        }
        #endregion
    }
}