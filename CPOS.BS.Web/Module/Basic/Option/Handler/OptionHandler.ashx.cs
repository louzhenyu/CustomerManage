using System;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.BS.Web.PageBase;
using JIT.Utility.ExtensionMethod;
using System.Collections.Specialized;
using JIT.Utility.Reflection;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.Web.Module.BasicData.Option.Handler
{
    /// <summary>
    /// OptionHandler 的摘要说明
    /// </summary>
    public class OptionHandler : JITCPOSAjaxHandler
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
                    if (!string.IsNullOrEmpty(pContext.Request.QueryString["method"]))
                    {
                        switch (pContext.Request.QueryString["method"])
                        {
                            case "GetListByActive":
                                res = GetListByActive(pContext.Request.Form);
                                break;
                            case "GetListByFixed":
                                res = GetListByFixed(pContext.Request.Form);
                                break;
                            case "GetOptionByName":
                                res = GetOptionByName(pContext.Request.Form["name"], pContext.Request.Form["definedID"].ToInt());
                                break;
                        }
                    } 
                    break;
                case "delete":
                    res = Delete(pContext.Request.Form["definedID"].ToInt());
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
                            case "GetOptionByName":
                                res = GetOptionByName(pContext.Request.Form["name"], pContext.Request.Form["definedID"].ToInt());
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

        #region GetListByOptionType      
        public string GetListByOptionType(NameValueCollection rParams, int OptionType)
        {
            OptionsDefinedEntity entity = rParams["form"].DeserializeJSONTo<OptionsDefinedEntity>();
            if (entity == null)
            {
                entity = new OptionsDefinedEntity();
            }
            int pageSize = rParams["limit"].ToInt();
            int pageIndex = rParams["page"].ToInt();
            IWhereCondition[] pWhereConditions = new IWhereCondition[3];
            if (entity != null)
            {
                if (entity.Title != null && entity.Title != "")
                {
                    LikeCondition like = new LikeCondition();
                    like.FieldName = "Title";
                    like.HasLeftFuzzMatching = true;
                    like.HasRightFuzzMathing = true;
                    like.Value = entity.Title;
                    pWhereConditions[0] = like;
                }
            }
            EqualsCondition equalCondition = new EqualsCondition();
            equalCondition.FieldName = "OptionType";
            equalCondition.Value = OptionType;
            pWhereConditions[1] = equalCondition;
            EqualsCondition equalCondition2 = new EqualsCondition();
            equalCondition2.FieldName = "ClientID";
            equalCondition2.Value = CurrentUserInfo.ClientID;
            pWhereConditions[2] = equalCondition2;
            OrderBy[] pOrderBys = new OrderBy[1];
            OrderBy order = new OrderBy();
            order.Direction = OrderByDirections.Asc;
            order.FieldName = "CreateTime";
            pOrderBys[0] = order;
            PagedQueryResult<OptionsDefinedEntity> entitys = new OptionsDefinedBLL(CurrentUserInfo).PagedQuery(pWhereConditions, pOrderBys, pageSize, pageIndex);
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
            entitys.Entities.ToJSON(),
                entitys.RowCount);
        }
        #endregion

        #region GetListByActive
        public string GetListByActive(NameValueCollection rParams)
        {
            return GetListByOptionType(rParams,3);
        }
        #endregion


        #region GetListByFixed  
        public string GetListByFixed(NameValueCollection rParams)
        {
            return GetListByOptionType(rParams, 2);
        }
        #endregion
        #region Delete
        private string Delete(int definedID)
        {
            string res = "[{success:false}]";
            OptionsEntity entity = new OptionsEntity();           
            entity.DefinedID = definedID;
            entity.ClientID = CurrentUserInfo.ClientID;
            //这里为单个删除
            new OptionsDefinedBLL(CurrentUserInfo).Delete(definedID, null);
            new OptionsBLL(CurrentUserInfo).DeleteOptionByName(entity);
            res = "[{success:true}]";
            return res;
        }
        #endregion

        #region GetOptionByName
        private string GetOptionByName(string name,int definedID)
        {
            OptionsEntity oEntity = new OptionsEntity();
            oEntity.ClientID = CurrentUserInfo.ClientID;
            oEntity.OptionName = name;
            oEntity.DefinedID = definedID;
            return new OptionsBLL(CurrentUserInfo).GetOptionByName(oEntity).ToJSON();
        }
        #endregion

        #region Edit
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="rParams"></param>
        /// <param name="type">1新增 2修改</param>
        /// <returns></returns>
        private string Edit(NameValueCollection rParams)
        {
            string res = "{success:false,msg:'保存失败'}";
            OptionsDefinedEntity optionsDefinedEntity = new OptionsDefinedEntity();
            if (!string.IsNullOrEmpty(rParams["definedID"]))
            {
                optionsDefinedEntity = new OptionsDefinedBLL(CurrentUserInfo).GetByID(rParams["definedID"].ToString());
            }
            optionsDefinedEntity = DataLoader.LoadFrom<OptionsDefinedEntity>(rParams, optionsDefinedEntity);
            OptionsEntity[] optionEntity = rParams["options"].DeserializeJSONTo<OptionsEntity[]>();
            int result = new OptionsBLL(CurrentUserInfo).OptionsDefinedEdit(optionsDefinedEntity, optionEntity);
            if (result == 1)
            {
                res = "{success:true,msg:'保存成功'}";
            }
            else if (result == 2)
            {
                res = "{success:false,msg:'名称已经存在,请换一个名称'}";
            }
            return res;
        }
        #endregion
    }
}