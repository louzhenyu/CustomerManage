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

namespace JIT.CPOS.BS.Web.Module.Basic.ItemCategory.Handler
{
    /// <summary>
    /// ItemCategoryHandler
    /// </summary>
    public class ItemCategoryHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_item_category":    //获取商品分类
                    content = GetItemCategoryListData();
                    break;
                case "get_item_category_by_id": //根据ID获取商品分类信息****
                    content = GetItemCategoryByID();
                    break;
                case "save_item_category":      //保存商品分类信息
                    content = SaveItemCategory();
                    break;
                case "delete_item_category":    //删除商品分类信息
                    content = DeleteItemCategory();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetItemCategoryListData
        /// <summary>
        /// 获取商品分类
        /// </summary>
        public string GetItemCategoryListData()
        {
            var form = Request("form").DeserializeJSONTo<ItemCategoryQueryEntity>();

            var itemCategoryService = new ItemCategoryService(CurrentUserInfo);
            ItemCategoryInfo data = new ItemCategoryInfo();
            string content = string.Empty;

            string item_category_code = FormatParamValue(form.item_category_code);
            string item_category_name = FormatParamValue(form.item_category_name);
            string pyzjm = FormatParamValue(form.pyzjm);
            string item_category_status = FormatParamValue(Request("item_category_status"));
            string item_category_id = FormatParamValue(Request("item_category_id"));
            int maxRowCount = PageSize;
            int startRowIndex = Utils.GetIntVal(Request("start"));

            string key = string.Empty;
            if (Request("item_category_id") != null && Request("item_category_id") != string.Empty)
            {
                key = Request("item_category_id").ToString().Trim();
            }

            data = itemCategoryService.SearchItemCategoryList(
                item_category_code, item_category_name, pyzjm, item_category_status,
                maxRowCount, startRowIndex, item_category_id);

            var jsonData = new JsonData();
            jsonData.totalCount = data.ItemCategoryInfoList.Count.ToString();
            jsonData.data = data.ItemCategoryInfoList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ItemCategoryInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region GetItemCategoryByID
        /// <summary>
        /// 根据ID获取商品分类信息
        /// </summary>
        public string GetItemCategoryByID()
        {
            var itemCategoryService = new ItemCategoryService(CurrentUserInfo);
            ItemCategoryInfo data = new ItemCategoryInfo();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("ItemCategoryId") != null && Request("ItemCategoryId") != string.Empty)
            {
                key = Request("ItemCategoryId").ToString().Trim();
            }

            data = itemCategoryService.GetItemCategoryById(key);

            var jsonData = new JsonData();
            jsonData.totalCount = data == null ? "0" : "1";
            jsonData.data = data;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveItemCategory
        /// <summary>
        /// 商品分类信息
        /// </summary>
        public string SaveItemCategory()
        {
            var itemCategoryService = new ItemCategoryService(CurrentUserInfo);
            ItemCategoryInfo itemCategory = new ItemCategoryInfo();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string ItemCategoryId = string.Empty;
            if (Request("itemCategorys") != null && Request("itemCategorys") != string.Empty)
            {
                key = Request("itemCategorys").ToString().Trim();
            }
            if (Request("ItemCategoryId") != null && Request("ItemCategoryId") != string.Empty)
            {
                ItemCategoryId = Request("ItemCategoryId").ToString().Trim();
            }

            itemCategory = key.DeserializeJSONTo<ItemCategoryInfo>();

            if (itemCategory.Item_Category_Code == null || itemCategory.Item_Category_Code.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "类型编码不能为空";
                return responseData.ToJSON();
            }
            if (itemCategory.Item_Category_Name == null || itemCategory.Item_Category_Name.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "类型名称不能为空";
                return responseData.ToJSON();
            }
            if (itemCategory.Status == null || itemCategory.Status.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "状态不能为空";
                return responseData.ToJSON();
            }
            if (itemCategory.Parent_Id == null || itemCategory.Parent_Id.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "上级商品名称不能为空";
                return responseData.ToJSON();
            }

            itemCategory.Create_User_Id = CurrentUserInfo.CurrentUser.User_Id;
            itemCategory.Create_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemCategory.Create_User_Name = CurrentUserInfo.CurrentUser.User_Name;
            itemCategory.CustomerID = CurrentUserInfo.CurrentUser.customer_id;

            if (ItemCategoryId.Trim().Length != 0)
            {
                itemCategory.Item_Category_Id = ItemCategoryId;
            }

            bool status = true;
            string message = "保存成功";
            try
            {
                itemCategoryService.SetItemCategoryInfo(CurrentUserInfo, itemCategory);
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region DeleteItemCategory 删除商品分类信息
        /// <summary>
        /// 删除商品分类信息
        /// </summary>
        public string DeleteItemCategory()
        {
            var service = new ItemCategoryService(CurrentUserInfo);

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
                responseData.msg = "商品分类ID不能为空";
                return responseData.ToJSON();
            }

            var status = "-1";
            if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
            {
                status = FormatParamValue(Request("status")).ToString().Trim();
            }

            string[] ids = key.Split(',');
            foreach (var id in ids)
            {
                //删除商品分类信息
                string res="";
                service.SetItemCategoryStatus(CurrentUserInfo, key, status,out res);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion
    }

    #region QueryEntity
    public class ItemCategoryQueryEntity
    {
        public string item_category_code;
        public string item_category_name;
        public string pyzjm;
        public string item_category_status;
    }
    #endregion

}