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

namespace JIT.CPOS.BS.Web.Module.CallSetting.Brand.Handler
{
    /// <summary>
    /// BrandHandler 的摘要说明
    /// </summary>
    public class BrandHandler : JIT.CPOS.BS.Web.PageBase.JITTPAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = "";
            switch (pContext.Request.QueryString["btncode"])
            {
                case "search":
                    res = GetList(pContext.Request.Form);
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["ids"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "Edit":
                                res = Edit(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetBrandByID":
                                res = GetBrandByID(pContext.Request.Form["id"]);
                                break;
                            case "Edit":
                                res = Edit(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetList
        private string GetList(NameValueCollection rParams)
        {
            BrandViewEntity entity = rParams["form"].DeserializeJSONTo<BrandViewEntity>();
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new BrandBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region Delete
        private string Delete(string ids)
        {
            string res = "[{success:false}]";
            if (new BrandBLL(CurrentUserInfo).Delete(ids))
            {
                res = "[{success:true}]";
            }
            return res;
        }
        #endregion

        #region GetBrandByID
        #region GetBrandByID
        private string GetBrandByID(string id)
        {
            return "[" + new BrandBLL(CurrentUserInfo).GetByID(id).ToJSON() + "]";
        }
        #endregion
        #endregion

        #region Edit
        private string Edit(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'编辑失败'}";

            //组装参数
            BrandEntity entity = new BrandEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new BrandBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<BrandEntity>(rParams, entity);
            entity.ClientID = CurrentUserInfo.ClientID;
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new BrandBLL(CurrentUserInfo).Update(entity);
                res = "{success:true,msg:'编辑成功'}";
            }
            else
            {
                new BrandBLL(CurrentUserInfo).Create(entity);
                res = "{success:true,msg:'编辑成功'}";
            }
            return res;
        }
        #endregion

    }
}