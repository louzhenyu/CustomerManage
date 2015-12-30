/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/7 11:10:05
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using System.Linq;
using System.Configuration;
using System.Web;



namespace JIT.CPOS.BS.Entity
{

    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class SysPageEntity : BaseEntity
    {
        #region 属性集
        public string Node { get; set; }
        public string NodeValue { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 获取动态URL
        /// </summary>
        /// <param name="pPageParamJson">URL参数JSON</param>
        /// <param name="pCustomerId">客户ID</param>
        /// <param name="pApplicationId">微信接口ID</param>
        /// <param name="weixinId">微信ID</param>
        /// <param name="IsAuth">是否认证</param>
        /// <returns>动态生成的URL</returns>
        public string GetUrl(string pPageParamJson, string pCustomerId, string pApplicationId, string weixinId)
        {
            string URL = string.Empty;

            string path = string.Empty;//要替换的路径
            string urlTemplate = this.URLTemplate;//模板URL
            string json = this.JsonValue;// JSON体
            var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//转换后的字典
            var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>();//所有的Html模板
            Dictionary<string, object> html = null;//选择的html信息
            var defaultHtmlId = jsonDic["defaultHtml"].ToString();
            html = htmls.Where(t => t["id"].ToString() == defaultHtmlId).FirstOrDefault();//默认的htmlid
            if (html != null)
                path = html["path"].ToString();
            if (Node == "2")
            {
                html = htmls.FirstOrDefault(t => t["id"] == NodeValue);
                if (html != null)
                {
                    path = html["path"].ToString();
                }
            }
            //替换URL模板
            #region 替换URL模板
            urlTemplate = urlTemplate.Replace("_pageName_", path);
            var paraDic = pPageParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
            foreach (var item in paraDic)
            {
                if (item.ContainsKey("key") && item.ContainsKey("value"))
                    urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
            }
            #endregion

            //根据规则组织URL
            #region 组织URL
            //读取配置文件中的域名
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            if (string.IsNullOrEmpty(Domain))
                throw new Exception("微信管理:未配置域名,请在web.config中添加<add key='host' value=''/>");
            if (IsAuth == 1)
            {
                //需要认证
                URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&goUrl={2}&applicationId={3}&weixinId={4}", Domain.Trim('/'), pCustomerId, string.Format("{0}{1}", Domain.Trim('/'), urlTemplate), pApplicationId, weixinId);               // URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), pCustomerId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)));
            }
            else
            {
                //不需要认证
                URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}&applicationId={3}&weixinId={4}", Domain.Trim('/'), pCustomerId, string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/')), pApplicationId, weixinId);
              //  URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), pCustomerId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/'))));
            }
            #endregion
            return URL;
        }
        #endregion
    }

}