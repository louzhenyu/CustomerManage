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

namespace JIT.CPOS.BS.Web.Module.CallSetting.Parameter.Handler
{
    /// <summary>
    /// ParameterHandler 的摘要说明
    /// </summary>
    public class ParameterHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                            case "GetByID":
                                res = GetByID(pContext.Request.Form["id"]);
                                break;
                            case "GetUnitList":
                                //res = GetUnitList();
                                break;
                            case "GetOptionList":
                                res = GetOptionList(pContext.Request.Form);
                                break;
                        }
                    }
                    
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["ids"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "Edit":
                                res = Edit(pContext.Request.Form);
                                break;
                            case "GetUnitList":
                                //res = GetUnitList();
                                break;
                            case "GetOptionList":
                                res = GetOptionList(pContext.Request.Form);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetByID":
                                res = GetByID(pContext.Request.Form["id"]);
                                break;
                            case "Edit":
                                res = Edit(pContext.Request.Form);
                                break;
                            case "GetUnitList":
                                //res = GetUnitList();
                                break;
                            case "GetOptionList":
                                res = GetOptionList(pContext.Request.Form);
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
            this.IsCompress = false;
            //Thread.Sleep(3000);
            VisitingParameterViewEntity entity = rParams["form"].DeserializeJSONTo<VisitingParameterViewEntity>();
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingParameterBLL(CurrentUserInfo).GetList(entity, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

       

        #region GetOptionList
        public string GetOptionList(NameValueCollection rParams)
        {
            return new JIT.CPOS.BS.Web.Module.VisitingSetting.ParameterOption.Handler.ParameterOptionHandler().GetList(rParams);
        }
        #endregion

        #region Delete
        private string Delete(string ids)
        {
            string res = "{success:false}";
            VisitingParameterEntity entity = new VisitingParameterEntity();
            entity.VisitingParameterID = Guid.Parse(ids);
            string checkRes = "";
            new VisitingParameterBLL(CurrentUserInfo).Delete(entity, out checkRes);
            if (!string.IsNullOrEmpty(checkRes))
            {
                res = "{success:false,msg:\"" + checkRes + "\"}";
            }
            else
            {
                res = "{success:true}";
            }
            return res;
        }
        #endregion

        #region GetByID
        private string GetByID(string id)
        {
            return new VisitingParameterBLL(CurrentUserInfo).GetByID(id).ToJSON();
        }
        #endregion

        #region Edit
        private string Edit(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'保存失败'}";
            VisitingParameterEntity entity = new VisitingParameterEntity();
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                entity = new VisitingParameterBLL(CurrentUserInfo).GetByID(rParams["id"]);
            }
            entity = DataLoader.LoadFrom<VisitingParameterEntity>(rParams, entity);
            if (string.IsNullOrEmpty(rParams["IsMustDo"]))
            {
                entity.IsMustDo = 0;
            }
            if (string.IsNullOrEmpty(rParams["IsRemember"]))
            {
                entity.IsRemember = 0;
            }
            if (string.IsNullOrEmpty(rParams["IsVerify"]))
            {
                entity.IsVerify = 0;
            }
            entity.ClientID = CurrentUserInfo.ClientID;
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
            //只有下拉选项类型才保存ControlType
            if (rParams["ControlType"].ToInt() != 5 && rParams["ControlType"].ToInt() != 6)
            {
                entity.ControlName = null;
            }
            string checkRes = "";
            if (!string.IsNullOrEmpty(rParams["id"]))
            {
                new VisitingParameterBLL(CurrentUserInfo).Update(entity, out checkRes);
            }
            else
            {
                new VisitingParameterBLL(CurrentUserInfo).Create(entity);
            }

            if (!string.IsNullOrEmpty(checkRes))
            {
                res = "{success:false,msg:\"" + checkRes + "\"}";
            }
            else
            {
                res = "{success:true,msg:'保存成功'}";
            }
            return res;
        }
        #endregion
    }
}