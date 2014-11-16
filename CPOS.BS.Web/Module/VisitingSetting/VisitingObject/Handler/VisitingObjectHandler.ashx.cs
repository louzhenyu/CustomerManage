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

namespace JIT.CPOS.BS.Web.Module.VisitingSetting.VisitingObject.Handler
{
    /// <summary>
    /// VisitingObjectHandler 的摘要说明
    /// </summary>
    public class VisitingObjectHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetList":
                                res = GetList(pContext.Request.Form);
                                break;
                            case "GetParentObject":
                                res = GetParentObject(pContext.Request["id"]);
                                break;
                            case "GetByID":
                                res = GetByID(pContext.Request["id"]);
                                break;
                            case "GetObjectParameterList":
                                res = GetObjectParameterList(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
                case "delete":
                    res = DeleteObject(pContext.Request.Form["ids"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetParentObject":
                                res = GetParentObject(pContext.Request["id"]);
                                break;
                            case "GetByID":
                                res = GetByID(pContext.Request["id"]);
                                break;
                            case "EditObject":
                                res = EditObject(pContext.Request.Form);
                                break;
                            case "GetObjectParameterList":
                                res = GetObjectParameterList(pContext.Request.Form);
                                break;
                            case "EditObjectParameter":
                                res = EditObjectParameter(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetParentObject":
                                res = GetParentObject(pContext.Request["id"]);
                                break;
                            case "GetByID":
                                res = GetByID(pContext.Request["id"]);
                                break;
                            case "EditObject":
                                res = EditObject(pContext.Request.Form);
                                break;
                            case "GetObjectParameterList":
                                res = GetObjectParameterList(pContext.Request.Form);
                                break;
                            case "EditObjectParameter":
                                res = EditObjectParameter(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
            }
            this.ResponseContent(res);
        }

        #region GetList
        private string GetList(NameValueCollection rParams)
        {
            VisitingObjectViewEntity entity = rParams["form"].DeserializeJSONTo<VisitingObjectViewEntity>();

            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingObjectBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region DeleteObject
        private string DeleteObject(string ids)
        {
            string res = "{success:false}";
            VisitingObjectEntity entity = new VisitingObjectEntity();
            entity.VisitingObjectID = Guid.Parse(ids);
            new VisitingObjectBLL(CurrentUserInfo).Delete(entity);
            res = "{success:true}";
            return res;
        }
        #endregion

        #region GetParentObject
        private string GetParentObject(string id)
        {
            Guid? oid = null;
            if (!string.IsNullOrEmpty(id))
            {
                oid = Guid.Parse(id);
            }
            return new VisitingObjectBLL(CurrentUserInfo).GetParentObject(oid).ToJSON();
        }
        #endregion

        #region GetByID
        private string GetByID(string id)
        {
            return new VisitingObjectBLL(CurrentUserInfo).GetByID(Guid.Parse(id)).ToJSON();
        }
        #endregion

        #region EditObject
        private string EditObject(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'保存失败'}";

            //组装参数
            VisitingObjectEntity entity = new VisitingObjectEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new VisitingObjectBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<VisitingObjectEntity>(rParams, entity);

            entity.ClientID = CurrentUserInfo.ClientID;
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);

            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new VisitingObjectBLL(CurrentUserInfo).Update(entity);
            }
            else
            {
                new VisitingObjectBLL(CurrentUserInfo).Create(entity);
            }
            res = "{success:true,msg:'保存成功',id:'" + entity.VisitingObjectID + "'}";
            return res;
        }
        #endregion

        #region GetObjectParameterList
        private string GetObjectParameterList(NameValueCollection rParams)
        {
            VisitingParameterViewEntity entity = new VisitingParameterViewEntity();
            entity.VisitingObjectID = Guid.Parse(rParams["id"]);
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingObjectVisitingParameterMappingBLL(CurrentUserInfo).GetObjectParameterList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region EditObjectParameter
        private string EditObjectParameter(NameValueCollection rParams)
        {
            Guid objid = Guid.Parse(rParams["id"]);
            int allSelectorStatus = rParams["allSelectorStatus"].ToInt();
            string defaultList = rParams["defaultList"];//关联表有的数据
            string includeList = rParams["includeList"];//新加的数据
            string excludeList = rParams["excludeList"];//排除的数据


            VisitingObjectVisitingParameterMappingEntity[] updateEntity = rParams["updateData"].ToString().DeserializeJSONTo<VisitingObjectVisitingParameterMappingEntity[]>();
            new VisitingObjectVisitingParameterMappingBLL(CurrentUserInfo).EditObjectParameter(objid, allSelectorStatus, defaultList, includeList, excludeList, updateEntity);
            return "{success:true,msg:'操作成功'}";
        }
        #endregion
    }
}