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

namespace JIT.CPOS.BS.Web.Module.VisitingSetting.ParameterOption.Handler
{
    /// <summary>
    /// OptionHandler 的摘要说明
    /// </summary>
    public class ParameterOptionHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                            case "GetOptionByName":
                                res = GetOptionByName(pContext.Request.Form["name"]);
                                break;
                        }
                    }
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["name"]);
                    break;
                case "create":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "Edit":
                                res = Edit(pContext.Request.Form, 1);
                                break;
                        }
                    }
                    break;
                case "update":
                    if (!string.IsNullOrEmpty(this.Method))
                    {
                        switch (this.Method)
                        {
                            case "GetOptionByName":
                                res = GetOptionByName(pContext.Request.Form["name"]);
                                break;
                            case "Edit":
                                res = Edit(pContext.Request.Form, 2);
                                break;
                        }
                    }
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region GetList
        public string GetList(NameValueCollection rParams)
        {
            VisitingParameterOptionsViewEntity entity = rParams["form"].DeserializeJSONTo<VisitingParameterOptionsViewEntity>();
            if (entity == null)
            {
                entity = new VisitingParameterOptionsViewEntity();
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            string ClientID = "";
            string ClientDistributorID = "";
            if (rParams["ClientID"] != null) {
                ClientID = rParams["ClientID"].ToString();
            }
            if (rParams["ClientDistributorID"] != null)
            {
                ClientDistributorID = rParams["ClientDistributorID"].ToString();
            }
            int rowCount = 0;
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
               new VisitingParameterOptionsBLL(CurrentUserInfo).GetOptionNameList(entity, ClientID, ClientDistributorID, pageIndex, pageSize, out rowCount).ToJSON(),
                rowCount);
        }
        #endregion

        #region Delete
        private string Delete(string name)
        {
            string res = "{success:false}";
            VisitingParameterOptionsEntity entity = new VisitingParameterOptionsEntity();
            entity.OptionName = name;
            entity.ClientID = CurrentUserInfo.ClientID;

            string checkRes = "";
            new VisitingParameterOptionsBLL(CurrentUserInfo).DeleteOptionByName(entity, out checkRes);
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

        #region GetOptionByName
        private string GetOptionByName(string name)
        {
            VisitingParameterOptionsEntity oEntity = new VisitingParameterOptionsEntity();
            oEntity.ClientID = CurrentUserInfo.ClientID; 
            oEntity.OptionName = name;
            return new  VisitingParameterOptionsBLL(CurrentUserInfo).GetOptionByName(oEntity).ToJSON();
        }
        #endregion

        #region Edit
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="rParams"></param>
        /// <param name="type">1新增 2修改</param>
        /// <returns></returns>
        private string Edit(NameValueCollection rParams,int type)
        {
            string res = "{success:false,msg:'保存失败'}";

            VisitingParameterOptionsEntity[] optionEntity = rParams["options"].DeserializeJSONTo<VisitingParameterOptionsEntity[]>();

            VisitingParameterOptionsEntity entity = new VisitingParameterOptionsEntity();
            entity.ClientID = CurrentUserInfo.ClientID;
            entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
            entity.OptionName = rParams["OptionName"];
            //entity.Sequence = Convert.ToInt32(rParams["Sequence"]);

            //传递参数至BLL parameterentity optionentity

            string checkRes = "";
            int result = new VisitingParameterOptionsBLL(CurrentUserInfo).Edit(optionEntity, entity, type, out checkRes);
            switch (result)
            {
                case 101:
                    res = "{success:false,msg:'保存失败,名称已经存在,请换一个名称'}";
                    break;
                case 102:
                    res = "{success:false,msg:\"" + checkRes + "\"}";
                    break;
                case 0:
                    res = "{success:true,msg:'保存成功'}";
                    break;
            }
            return res;
        }
        #endregion
    }
}