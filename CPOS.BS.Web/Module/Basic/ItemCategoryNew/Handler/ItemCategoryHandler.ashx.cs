using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Extension;
using JIT.CPOS.Common;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using System.Data;

namespace JIT.CPOS.BS.Web.Module2.BaseData.ItemCategory.Handler
{
    /// <summary>
    /// 商品分类的后台处理
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
                case "toggleStatus":
                    content = this.ToggoleItemCategoryStatus();
                    break;
                case "getAll":
                    content = this.GetAllItemCategories();
                    break;
                case "update":
                    content = this.UpdateItemCategory();
                    break;
                case "add":
                    content = this.UpdateItemCategory();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region 操作
        /// <summary>
        /// 切换商品分类状态
        /// </summary>
        protected string ToggoleItemCategoryStatus()
        {
            string res = "{success:false}";
            string checkRes = "";

            var bll = new ItemCategoryService(this.CurrentUserInfo);
            var id = this.Request("id");
            var status = this.Request("status");
            if (!string.IsNullOrWhiteSpace(id))
            {
                bll.SetItemCategoryStatus(CurrentUserInfo, id, status, out checkRes);

                if (!string.IsNullOrEmpty(checkRes))
                {
                    res = "{success:false,msg:\"" + checkRes + "\"}";
                }
                else
                {
                    res = "{success:true}";
                }
            }
            return res;
        }

        /// <summary>
        /// 获取所有的商品分类
        /// </summary>
        /// <returns></returns>
        protected string GetAllItemCategories()
        {
            var bll = new ItemCategoryService(this.CurrentUserInfo);
            var list = bll.GetItemCagegoryList("");
            return list.ToJSON();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        protected string UpdateItemCategory()
        {
            var bll = new ItemCategoryService(this.CurrentUserInfo);
            var data = this.DeserializeJSONContent<ItemCategoryInfo>();
            var rsp = new ResponseData();
            if (data != null)
            {
                IList<ItemCategoryInfo> listdata = bll.GetItemCagegoryList("");

                if (Searchtype(listdata, data.Item_Category_Id, data.Parent_Id))
                {
                    rsp.success = false;
                    rsp.msg = "上级分类不能选择自身下级";
                    return rsp.ToJSON();

                }

                if (string.IsNullOrWhiteSpace(data.Item_Category_Code))
                {
                    rsp.success = false;
                    rsp.msg = "类型编码不能为空";
                    return rsp.ToJSON();
                }
                if (string.IsNullOrWhiteSpace(data.Status))
                {
                    rsp.success = false;
                    rsp.msg = "状态不能为空";
                    return rsp.ToJSON();
                }
                if (string.IsNullOrWhiteSpace(data.Parent_Id))
                {
                    rsp.success = false;
                    rsp.msg = "上级分类不能为空";
                    return rsp.ToJSON();
                }
                data.Create_User_Id = CurrentUserInfo.CurrentUser.User_Id;
                data.Create_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                data.Create_User_Name = CurrentUserInfo.CurrentUser.User_Name;
                data.CustomerID = CurrentUserInfo.CurrentUser.customer_id;
                //
                bll.SetItemCategoryInfo(this.CurrentUserInfo, data);
                rsp.success = true;
                rsp.msg = "保存成功";
            }
            //
            return rsp.ToJSON();
        }
        #endregion

        #region 判断是否选择自己的子类
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">所有信息</param>
        /// <param name="id">自身id</param>
        /// <param name="pid">选择的上一级id</param>
        /// <returns></returns>
        public bool Searchtype(IList<ItemCategoryInfo> list, string id, string pid)
        {
            List<ItemCategoryInfo> Itemlist = list.Where(op => op.Parent_Id == id).ToList();
            if (Itemlist != null)
            {
                foreach (ItemCategoryInfo item in Itemlist)
                {
                    if (item.Item_Category_Id == pid)
                    {
                        return true;
                    }
                    if (SonSearchtype(list, item.Item_Category_Id, pid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">所有信息</param>
        /// <param name="id">自身id</param>
        /// <param name="pid">选择的上一级id</param>
        /// <returns></returns>
        public bool SonSearchtype(IList<ItemCategoryInfo> list, string id, string pid)
        {
            List<ItemCategoryInfo> Itemlist = list.Where(op => op.Parent_Id == id).ToList();
            if (Itemlist != null)
            {
                foreach (ItemCategoryInfo item in Itemlist)
                {
                    if (item.Item_Category_Id == pid)
                    {
                        return true;
                    }
                    SonSearchtype(list, item.Item_Category_Id, pid);
                }
            }
            return false;
        }
        #endregion
    }
}