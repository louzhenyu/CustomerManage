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
using JIT.Utility.Log;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.Module.MarketEvent.Templates.Handler
{
    /// <summary>
    /// TemplateHandler 的摘要说明
    /// </summary>
    public class TemplateHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("method:{0}", pContext.Request.QueryString["method"])
            });
            switch (pContext.Request.QueryString["method"])
            {
                case "GetTemplateListByType": //获取模板集合根据类型
                    content = GetTemplateListByType();
                    break;
                case "template_delete":     //删除模板
                    content = TemplateDeleteData();
                    break;
                case "templae_save": //修改保存
                    content = SaveTemplate();
                    break;
                case "GetEventById":
                    content = GetEventById();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region //获取模板集合根据类型
        public string GetTemplateListByType()
        {
            var marketTemplate = new MarketTemplateBLL(CurrentUserInfo);
            MarketTemplateEntity data = new MarketTemplateEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("TemplateType") != null && Request("TemplateType") != string.Empty)
            {
                key =  Request("TemplateType").ToString().Trim();
            }

            data.MarketTemplateList = marketTemplate.GetTemplateListByType(key);
            if (data.MarketTemplateList == null) data.MarketTemplateList = new List<MarketTemplateEntity>();

            var jsonData = new JsonData();
            jsonData.totalCount = data.MarketTemplateList.Count.ToString();
            jsonData.data = data.MarketTemplateList;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.MarketTemplateList.ToJSON(),
                data.MarketTemplateList.Count);
            
            return content;
        }
        #endregion

        #region
        public string TemplateDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                key = FormatParamValue(Request("id")).ToString().Trim();
            }

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动ID不能为空";
                return responseData.ToJSON();
            }
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("模板标识:{0}", key)
            });
            string[] ids = key.Split(',');
            MarketTemplateEntity model = new MarketTemplateEntity();
            model.TemplateID = key;
            new MarketTemplateBLL(this.CurrentUserInfo).Delete(model);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region 修改模板保存
        /// <summary>
        /// 保存活动信息
        /// </summary>
        public string SaveTemplate()
        {
            var templateService = new MarketTemplateBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string TemplateID = string.Empty;
            var template = Request("template");

            if (FormatParamValue(template) != null && FormatParamValue(template) != string.Empty)
            {
                key = FormatParamValue(template).ToString().Trim();
            }
            if (FormatParamValue(Request("TemplateID")) != null && FormatParamValue(Request("TemplateID")) != string.Empty)
            {
                TemplateID = FormatParamValue(Request("TemplateID")).ToString().Trim();
            }

            var templateEntity = key.DeserializeJSONTo<MarketTemplateEntity>();


            if (templateEntity.TemplateContent == null || templateEntity.TemplateContent.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "模板内容不能为空";
                return responseData.ToJSON();
            }


            // templateEntity.TemplateContent = HttpUtility.HtmlEncode(templateEntity.TemplateContent);

            if (TemplateID.Trim().Length == 0)
            {
                templateEntity.TemplateID = Utils.NewGuid();

                templateService.Create(templateEntity);
            }
            else
            {
                templateEntity.TemplateID = TemplateID;
                templateService.Update(templateEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 获取单个模板信息
        public string GetEventById()
        {
            var marketTemplate = new MarketTemplateBLL(CurrentUserInfo);
            MarketTemplateEntity data = new MarketTemplateEntity();
            string content = string.Empty;

            string key = string.Empty;
            if (Request("TemplateId") != null && Request("TemplateId") != string.Empty)
            {
                key = Request("TemplateId").ToString().Trim();
            }

            data = marketTemplate.GetByID(key);
            if (data == null) data = new MarketTemplateEntity();

            var jsonData = new JsonData();
            jsonData.totalCount = "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                jsonData.data.ToJSON(),
                jsonData.totalCount);

            return content;
        }
        #endregion
    }
}