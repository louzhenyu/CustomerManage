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

namespace JIT.CPOS.BS.Web.Module.Basic.Tags.Handler
{
    /// <summary>
    /// TagsHandler
    /// </summary>
    public class TagsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "search_tags":
                    content = GetTagsListData();
                    break;
                case "get_tags_by_id":
                    content = GetTagsInfoById();
                    break;
                case "tags_save":
                    content = SaveTagsData();
                    break;
                case "tags_delete":
                    content = TagsDeleteData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }


        #region GetTagsListData
        /// <summary>
        /// 查询标签
        /// </summary>
        public string GetTagsListData()
        {
            var form = Request("form").DeserializeJSONTo<TagsQueryEntity>();

            var tagsBLL = new TagsBLL(CurrentUserInfo);
            IList<TagsEntity> tagsList;
            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            TagsEntity queryEntity = new TagsEntity();
            queryEntity.TagsName = FormatParamValue(form.TagsName);
            queryEntity.TagsDesc = FormatParamValue(form.TagsDesc);
            queryEntity.TypeId = FormatParamValue(form.TypeId);
            queryEntity.StatusId = FormatParamValue(form.StatusId);

            tagsList = tagsBLL.GetWebTags(queryEntity, pageIndex, PageSize);
            var dataTotalCount = tagsBLL.GetWebTagsCount(queryEntity);

            var jsonData = new JsonData();
            jsonData.totalCount = dataTotalCount.ToString();
            jsonData.data = tagsList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);
            return content;
        }
        #endregion

        #region GetTagsInfoById
        /// <summary>
        /// 根据标签ID获取标签信息
        /// </summary>
        public string GetTagsInfoById()
        {
            var service = new TagsBLL(CurrentUserInfo);
            TagsEntity tags = new TagsEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("TagsID") != null && Request("TagsID") != string.Empty)
            {
                key = Request("TagsID").ToString().Trim();
            }

            tags = service.GetByID(key);

            var jsonData = new JsonData();
            jsonData.totalCount = tags == null ? "0" : "1";
            jsonData.data = tags;

            content = jsonData.ToJSON();
            return content;
        }
        #endregion

        #region SaveTagsData
        /// <summary>
        /// 保存标签
        /// </summary>
        public string SaveTagsData()
        {
            var service = new TagsBLL(CurrentUserInfo);
            TagsEntity tags = new TagsEntity();
            string content = string.Empty;
            var responseData = new ResponseData();

            string key = string.Empty;
            string tags_id = string.Empty;
            if (Request("tags") != null && Request("tags") != string.Empty)
            {
                key = Request("tags").ToString().Trim();
            }
            if (Request("TagsId") != null && Request("TagsId") != string.Empty)
            {
                tags_id = Request("TagsId").ToString().Trim();
            }

            tags = key.DeserializeJSONTo<TagsEntity>();

            if (tags.TagsName == null || tags.TagsName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "标签名称不能为空";
                return responseData.ToJSON();
            }

            bool status = true;
            string message = "保存成功";
            if (tags.TagsId.Trim().Length == 0)
            {
                tags.TagsId = Utils.NewGuid();
                tags.CustomerId = this.CurrentUserInfo.CurrentUser.customer_id;
                service.Create(tags);
            }
            else
            {
                tags.TagsId = tags_id;
                service.Update(tags, false);
            }

            responseData.success = status;
            responseData.msg = message;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region TagsDeleteData
        /// <summary>
        /// 删除
        /// </summary>
        public string TagsDeleteData()
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
            new TagsBLL(this.CurrentUserInfo).Delete(new TagsEntity()
            {
                TagsId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

    }

    #region QueryEntity
    public class TagsQueryEntity
    {
        public string TagsName;
        public string TagsDesc;
        public string TagsFormula;
        public string TagsStatus;
        public string TypeId;
        public string StatusId;
    }
    #endregion

}