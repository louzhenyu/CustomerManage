using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;


namespace JIT.CPOS.BS.Web.Module.Basic.ItemTag.Handler
{
    /// <summary>
    /// ItemTagHandler1 的摘要说明
    /// </summary>
    public class ItemTagHandler1 : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 异步请求获取数据。
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = string.Empty;
            switch (pContext.Request.QueryString["method"])
            {
                case "toggleStatus":
                    content = this.ToggoleItemTagStatus();
                    break;
                case "getAll":
                    content = this.GetItemTags();
                    break;
                case "update":
                    content = this.UpdateItemTag();
                    break;
                case "add":
                    content = this.UpdateItemTag();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }


        #region
        /// <summary>
        /// 切换商品标签分类状态
        /// </summary>
        protected string ToggoleItemTagStatus()
        {
            string res = "{success:false}";
            string checkRes = "";

            var bll = new TItemTagBLL(this.CurrentUserInfo);
            var id = this.Request("id");
            var status = this.Request("status");
            if (!string.IsNullOrWhiteSpace(id))
            {
               // bll.SetItemCategoryStatus(CurrentUserInfo, id, status, out checkRes);

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
        /// 获取所有商品标签分类
        /// </summary>
        /// <returns></returns>
        protected string GetItemTags()
        {
            var bll = new TItemTagBLL(this.CurrentUserInfo);
            var list = bll.GetAll();

            return list.ToJSON();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        protected string UpdateItemTag()
        {
            var bll = new TItemTagBLL(this.CurrentUserInfo);
            var data = this.DeserializeJSONContent<TItemTagEntity>();
            var rsp = new ResponseData();
            if (data != null)
            {
                if (string.IsNullOrWhiteSpace(data.ItemTagCode))
                {
                    rsp.success = false;
                    rsp.msg = "类型标签编码不能为空";
                    return rsp.ToJSON();
                }
                if (string.IsNullOrWhiteSpace(data.ItemTagName))
                {
                    rsp.success = false;
                    rsp.msg = "类型标签名称不能为空";
                    return rsp.ToJSON();
                }
                if (!data.Status.HasValue)
                {
                    rsp.success = false;
                    rsp.msg = "状态不能为空";
                    return rsp.ToJSON();
                }
                if (!data.ParentID.HasValue)
                {
                    rsp.success = false;
                    rsp.msg = "上级分类不能为空";
                    return rsp.ToJSON();
                }
               
                data.CreateTime = DateTime.Now;
                data.LastUpdateTime =DateTime.Now;
                data.CustomerID = CurrentUserInfo.CurrentUser.customer_id;
               
               // bll(this.CurrentUserInfo, data);
            }
            
            return rsp.ToJSON();
        }
        #endregion

    }


}
