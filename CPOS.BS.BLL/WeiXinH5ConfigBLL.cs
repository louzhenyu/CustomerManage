/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/20 11:37:39
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
using System.Linq;
using System.IO;
using System.Text;
using System.Data;
using System.Reflection;

using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理： 微信Html5的页面配置信息 
    /// </summary>
    public partial class WeiXinH5ConfigBLL
    {
        #region 内部类

        class H5ConfigInfo
        {
            public ConfigSection Config { get; set; }
            public List<PageSection> Pages { get; set; }
            public static H5ConfigInfo Parse(string pJSON)
            {
                JObject jo = JObject.Parse(pJSON);
                var properties = jo.Properties();
                H5ConfigInfo config = new H5ConfigInfo();
                foreach (var p in properties)
                {
                    if (p.Name == "Config")
                    {//配置项
                        ConfigSection cs = new ConfigSection();
                        config.Config = cs;
                        var configSection = p.Value as JObject;
                        if (configSection != null)
                        {
                            var sh = configSection.Property("Shorthand").Value as JObject;
                            if (sh != null)
                            {
                                var items = sh.Properties();
                                cs.Shorthand = new Dictionary<string, string>();
                                foreach (var item in items)
                                {
                                    cs.Shorthand.Add(item.Name, item.Value.ToObject<string>());
                                }
                            }

                            //var common = configSection.Property("Common").Value as JObject;
                            //if (common != null)
                            //{
                            //    var script = common.Property("script").Value;
                            //    if (script != null)
                            //    {
                            //        cs.Script = script.ToObject<string[]>();
                            //    }
                            //    var style = common.Property("style").Value;
                            //    if (style != null)
                            //    {
                            //        cs.Style = style.ToObject<string[]>();
                            //    }
                            //}
                        }
                    }
                    else
                    {//页面
                        PageSection ps = new PageSection();
                        if (config.Pages == null)
                            config.Pages = new List<PageSection>();
                        config.Pages.Add(ps);
                        //
                        ps.Name = p.Name;
                        var psSection = p.Value as JObject;
                        if (psSection != null)
                        {
                            if (psSection.Property("path") != null && psSection.Property("path").Value != null)
                                ps.Path = psSection.Property("path").Value.ToObject<string>();
                            //if (psSection.Property("title") != null && psSection.Property("title").Value != null)
                            //    ps.Title = psSection.Property("title").Value.ToObject<string>();
                            //if (psSection.Property("plugin") != null && psSection.Property("plugin").Value != null)
                            //    ps.Plugin = psSection.Property("plugin").Value.ToObject<string[]>();
                            //if (psSection.Property("script") != null && psSection.Property("script").Value != null)
                            //    ps.Script = psSection.Property("script").Value.ToObject<string[]>();
                            //if (psSection.Property("style") != null && psSection.Property("style").Value != null)
                            //    ps.Style = psSection.Property("style").Value.ToObject<string[]>();
                            //var param = psSection.Property("param");
                            //if (param != null)
                            //{
                            //    var pObject = param.Value as JObject;
                            //    var items = pObject.Properties();
                            //    ps.Param = new Dictionary<string, string>();
                            //    foreach (var item in items)
                            //    {
                            //        ps.Param.Add(item.Name, item.Value.ToObject<string>());
                            //    }
                            //}
                        }
                    }
                }
                //
                return config;
            }
        }

        class ConfigSection
        {
            public Dictionary<string, string> Shorthand { get; set; }
            public string[] Script { get; set; }
            public string[] Style { get; set; }
        }

        class PageSection
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string Title { get; set; }
            public string[] Plugin { get; set; }
            public string[] Script { get; set; }
            public string[] Style { get; set; }
            public Dictionary<string, string> Param { get; set; }
        }
        #endregion

        /// <summary>
        /// 根据PageName获取指定客户下的H5页面的路径
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPageName"></param>
        /// <returns></returns>
        public string GetPagePathByPageName(string pCustomerID, string pPageName)
        {
            WeiXinH5ConfigEntity queryEntity = new WeiXinH5ConfigEntity();
            queryEntity.CustomerID = pCustomerID;
            queryEntity.IsDelete =0;
            var queryResult = this._currentDAO.QueryByEntity(queryEntity, null);
            //
            var configFilePath = string.Empty;
            if (queryResult != null && queryResult.Length > 0)
            {//找到属于该客户的H5页面配置记录
                configFilePath = queryResult[0].ConfigFilePath;
                if (configFilePath.StartsWith(@"\"))
                {
                    configFilePath = configFilePath.Substring(1);
                }
            }
            else
            {//如果数据库中没有指定配置，则按规则默认
                configFilePath = string.Format(@"\HtmlApps\config\{0}.js", pCustomerID);
            }
            if (!string.IsNullOrWhiteSpace(configFilePath))
            {
                var fullPath = AppDomain.CurrentDomain.BaseDirectory + configFilePath;
                if (File.Exists(fullPath))
                {//获取配置文件的路径
                    var configContent = File.ReadAllText(fullPath);  //读取配置文件中的所有信息
                    var h5ConfigInfo = H5ConfigInfo.Parse(configContent);
                    if (h5ConfigInfo != null && h5ConfigInfo.Pages != null && h5ConfigInfo.Pages.Count > 0)
                    {//解析JSON格式的配置文件内容,获取指定PageName的路径配置
                        foreach (var page in h5ConfigInfo.Pages)
                        {
                            if (page != null && page.Name == pPageName)
                            {
                                var path = page.Path;
                                if (h5ConfigInfo.Config != null && h5ConfigInfo.Config.Shorthand != null && h5ConfigInfo.Config.Shorthand.Count > 0)
                                {
                                    foreach (var item in h5ConfigInfo.Config.Shorthand)
                                    {
                                        path = path.Replace(string.Format("%{0}%", item.Key), item.Value);
                                    }
                                }
                                //返回H5页面路径
                                return path;
                            }
                        }
                    }
                }
            }
            //
            return string.Empty;
        }
    }
}