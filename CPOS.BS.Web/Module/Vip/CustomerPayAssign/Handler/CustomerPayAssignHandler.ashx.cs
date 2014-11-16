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

namespace JIT.CPOS.BS.Web.Module.Vip.CustomerPayAssign.Handler
{
    /// <summary>
    /// VipHandler
    /// </summary>
    public class CustomerPayAssignHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "search_CustomerPayAssign":
                    content = GetCustomerPayAssignListData();
                    break;
                case "get_CustomerPayAssign_by_id":
                    content = GetCustomerPayAssignInfoById();
                    break;
                case "CustomerPayAssign_delete":
                    content = CustomerPayAssignDeleteData();
                    break;
                case "CustomerPayAssign_save":
                    content = SaveCustomerPayAssign();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetCustomerPayAssignListData
        /// <summary>
        /// 查询
        /// </summary>
        public string GetCustomerPayAssignListData()
        {
            var form = Request("form").DeserializeJSONTo<VipQueryEntity>();

            var service = new CustomerPayAssignBLL(CurrentUserInfo);
            string content = string.Empty;

            CustomerPayAssignEntity queryEntity = new CustomerPayAssignEntity();
            queryEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
            queryEntity.PaymentTypeId = FormatParamValue(form.PaymentTypeId);
            queryEntity.CustomerAccountNumber = FormatParamValue(form.CustomerAccountNumber);
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            var listObj = service.GetList(queryEntity, pageIndex, PageSize);
            var totalCount = service.GetListCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = totalCount.ToString();
            jsonData.data = listObj;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetCustomerPayAssignInfoById
        /// <summary>
        /// 获取Vip信息
        /// </summary>
        public string GetCustomerPayAssignInfoById()
        {
            var service = new CustomerPayAssignBLL(CurrentUserInfo);
            CustomerPayAssignEntity obj = null;
            string content = string.Empty;

            string key = string.Empty;
            if (Request("AssignId") != null && Request("AssignId") != string.Empty)
            {
                key = Request("AssignId").ToString().Trim();
            }

            CustomerPayAssignEntity queryEntity = new CustomerPayAssignEntity();
            queryEntity.AssignId = key;
            var tmpList = service.GetList(queryEntity, 1, 1);
            if (tmpList != null && tmpList.Count > 0)
            {
                obj = tmpList[0];
            }

            var jsonData = new JsonData();
            jsonData.totalCount = obj == null ? "0" : "1";
            jsonData.data = obj;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region CustomerPayAssignDeleteData

        /// <summary>
        /// 删除
        /// </summary>
        public string CustomerPayAssignDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("ids")) != null && FormatParamValue(Request("ids")) != string.Empty)
            {
                key = FormatParamValue(Request("ids")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "ID不能为空";
                return responseData.ToJSON();
            }
            
            string[] ids = key.Split(',');
            new CustomerPayAssignBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SaveCustomerPayAssign

        /// <summary>
        /// 
        /// </summary>
        public string SaveCustomerPayAssign()
        {
            var service = new CustomerPayAssignBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string AssignId = string.Empty;
            var item = Request("item");

            if (FormatParamValue(item) != null && FormatParamValue(item) != string.Empty)
            {
                key = FormatParamValue(item).ToString().Trim();
            }
            if (FormatParamValue(Request("AssignId")) != null && FormatParamValue(Request("AssignId")) != string.Empty)
            {
                AssignId = FormatParamValue(Request("AssignId")).ToString().Trim();
            }

            var obj = key.DeserializeJSONTo<CustomerPayAssignEntity>();

            if (Convert.ToInt32(obj.CustomerProportion) + Convert.ToInt32(obj.JITProportion) > 100)
            {
                responseData.success = false;
                responseData.msg = "分成比例合计不能大于100";
                return responseData.ToJSON();
            }

            if (AssignId == null || AssignId.Trim().Length == 0)
            {
                obj.AssignId = Utils.NewGuid();
                obj.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                obj.CreateBy = CurrentUserInfo.CurrentUser.User_Id;
                obj.CreateTime = DateTime.Now;
                obj.LastUpdateBy = CurrentUserInfo.CurrentUser.User_Id;
                obj.LastUpdateTime = DateTime.Now;
                service.Create(obj);
            }
            else
            {
                obj.AssignId = AssignId;
                obj.LastUpdateBy = CurrentUserInfo.CurrentUser.User_Id;
                obj.LastUpdateTime = DateTime.Now;
                service.Update(obj, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

    }

    #region QueryEntity
    public class VipQueryEntity
    {
        public string PaymentTypeId;
        public string CustomerAccountNumber;
        public string CustomerId;
    }
    #endregion

    #region
    public class RespData
    {
        public string Code;
        public string Description;
        public string Exception = null;
        public string Data;
        public int count;
        public long NewTimestamp;
    }
    #endregion

}