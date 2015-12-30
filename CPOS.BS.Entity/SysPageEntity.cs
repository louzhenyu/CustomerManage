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
    /// ʵ�壺  
    /// </summary>
    public partial class SysPageEntity : BaseEntity
    {
        #region ���Լ�
        public string Node { get; set; }
        public string NodeValue { get; set; }
        #endregion

        #region ����
        /// <summary>
        /// ��ȡ��̬URL
        /// </summary>
        /// <param name="pPageParamJson">URL����JSON</param>
        /// <param name="pCustomerId">�ͻ�ID</param>
        /// <param name="pApplicationId">΢�Žӿ�ID</param>
        /// <param name="weixinId">΢��ID</param>
        /// <param name="IsAuth">�Ƿ���֤</param>
        /// <returns>��̬���ɵ�URL</returns>
        public string GetUrl(string pPageParamJson, string pCustomerId, string pApplicationId, string weixinId)
        {
            string URL = string.Empty;

            string path = string.Empty;//Ҫ�滻��·��
            string urlTemplate = this.URLTemplate;//ģ��URL
            string json = this.JsonValue;// JSON��
            var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//ת������ֵ�
            var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>();//���е�Htmlģ��
            Dictionary<string, object> html = null;//ѡ���html��Ϣ
            var defaultHtmlId = jsonDic["defaultHtml"].ToString();
            html = htmls.Where(t => t["id"].ToString() == defaultHtmlId).FirstOrDefault();//Ĭ�ϵ�htmlid
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
            //�滻URLģ��
            #region �滻URLģ��
            urlTemplate = urlTemplate.Replace("_pageName_", path);
            var paraDic = pPageParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
            foreach (var item in paraDic)
            {
                if (item.ContainsKey("key") && item.ContainsKey("value"))
                    urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
            }
            #endregion

            //���ݹ�����֯URL
            #region ��֯URL
            //��ȡ�����ļ��е�����
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            if (string.IsNullOrEmpty(Domain))
                throw new Exception("΢�Ź���:δ��������,����web.config�����<add key='host' value=''/>");
            if (IsAuth == 1)
            {
                //��Ҫ��֤
                URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&goUrl={2}&applicationId={3}&weixinId={4}", Domain.Trim('/'), pCustomerId, string.Format("{0}{1}", Domain.Trim('/'), urlTemplate), pApplicationId, weixinId);               // URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), pCustomerId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)));
            }
            else
            {
                //����Ҫ��֤
                URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}&applicationId={3}&weixinId={4}", Domain.Trim('/'), pCustomerId, string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/')), pApplicationId, weixinId);
              //  URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), pCustomerId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/'))));
            }
            #endregion
            return URL;
        }
        #endregion
    }

}