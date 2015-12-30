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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

using JIT.CPOS.DTO.Module.WeiXin.SysPage.Request;
using JIT.CPOS.DTO.Module.WeiXin.SysPage.Response;
using System.Configuration;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class SysPageBLL
    {
        /// <summary>
        /// 根据客户来获取配置页模板,如果客户ID为空,则获取所有
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public SysPageEntity[] GetPagesByCustomerID(string pCustomerID = null)
        {
            return this._currentDAO.GetPagesByCustomerID(pCustomerID);
        }

        public SysPageEntity[] GetPageByID(Guid? pPageID)
        {
            return this._currentDAO.GetPageByID(pPageID);
        }
        /// <summary>
        /// 查询数据库表SysPage 中是否存在该PageKey数据 Add By changjian.tian 2014-05-16 
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public bool GetExistsPageKey(string pPageKey)
        {
            return _currentDAO.GetExistsPageKey(pPageKey);
        }

        public bool GetExistsPageKey(string pPageKey, string PageId)
        {
            return _currentDAO.GetExistsPageKey(pPageKey, PageId);
        }

        /// <summary>
        /// 获取模板页列表
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Name"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public GetSysPageListRD GetSysPageList(string pKey, string pName, int pPageIndex, int pPageSize, string pCustomerId)
        {
            GetSysPageListRD rd = new GetSysPageListRD();
            List<ListPageInfo> list = new List<ListPageInfo> { };
            int count = 0;

            DataSet ds = _currentDAO.GetSysPageList(pKey, pName, pPageIndex, pPageSize, pCustomerId);
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                count = ds.Tables[1].Rows.Count;
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var pageinfo = new ListPageInfo()
                    {
                        PageId = item["PageId"].ToString(),
                        Title = item["ModuleName"].ToString(),
                        PageKey = item["PageKey"].ToString(),
                        Version = item["Version"].ToString(),
                        LastUpdateTime = item["LastUpdateTime"].ToString()
                    };
                    list.Add(pageinfo);

                }
            }
            rd.TotalPageCount = count;
            rd.PageList = list.ToArray();
            return rd;
        }
        /// <summary>
        /// 获取模块客户化配置
        /// </summary>
        /// <returns></returns>
        public GetCustomerPageSettingRD GetCustomerPageSetting(string pPageKey, string pCustomerId)
        {
            GetCustomerPageSettingRD rd = new GetCustomerPageSettingRD();
            DataSet ds = _currentDAO.GetCustomerPageSetting(pPageKey, pCustomerId);
            List<CustomerPageSettingInfo> customerPageSettingInfo = new List<CustomerPageSettingInfo>() { };
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                string tempHtmls = string.Empty, tempPara = string.Empty; string url = string.Empty;
                var list = ds.Tables[0].AsEnumerable();
                var listSetting = ds.Tables[1].AsEnumerable();
                string JsonValue = listSetting.ToArray()[0].Table.Rows[0]["JsonValue"].ToString();
                var tempdic = JsonValue.DeserializeJSONTo<Dictionary<string, object>>(); //将Json体反序列化成Dictionary对象
                var tempPageSettingInfo = list.Select(t => new CustomerPageSettingInfo()
                {
                    ModuleName = t["ModuleName"].ToString(),//模块名称
                    Version = t["Version"].ToString(),  //版本
                    Auther = t["Author"].ToString(),//作者
                    LastUpdateTime = t["LastUpdateTime"].ToString(),  //时间
                    DoMainUrl = ConfigurationManager.AppSettings["interfacehost"].ToString()
                });

                var tempPageTitle = listSetting.ToArray()[0].Table.Rows[0]["NodeValue"].ToString();

                if (!ds.Tables[1].AsEnumerable().ToList().Exists(t => t["Node"].ToString() == "2"))
                {

                    var htmls = tempdic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//所有的Html模板
                    var defaultHtmlId = tempdic["defaultHtml"].ToString();
                    tempHtmls = defaultHtmlId;

                }
                else if (!ds.Tables[1].AsEnumerable().ToList().Exists(t => t["Node"].ToString() == "3"))
                {
                    var param = tempdic["params"].ToJSON().DeserializeJSONTo<object[]>().ToList();
                    tempPara = param.Aggregate(new List<Dictionary<string, object>> { }, (i, j) =>
                        {
                            var temp = j.ToJSON().DeserializeJSONTo<Dictionary<string, object>>();
                            i.Add(temp);
                            return i;
                        }).Aggregate(new Dictionary<string, object>(), (i, j) =>
                            {
                                i[j["Key"].ToString()] = j["defaultValue"];
                                return i;
                            }).ToJSON();

                }
                else
                {
                    tempHtmls = listSetting.ToArray()[0].Table.Rows[1]["NodeValue"].ToString();
                    tempPara = listSetting.ToArray()[0].Table.Rows[2]["NodeValue"].ToString();
                }

                //var pageId = Guid.Parse(list.ToArray()[0].Table.Rows[0]["PageId"].ToString());
                //var pages = GetPageByID(pageId);

                //if (pages.Length > 0)
                //{
                //    var page = pages.FirstOrDefault();

                //    if (page != null)
                //    {
                //        //获取生成的URL
                //         url = page.GetUrl(JsonValue, CurrentUserInfo.ClientID, "");
                //    }
                //}
                rd.PageBaseInfo = tempPageSettingInfo.ToArray();//基本信息
                rd.PageTitle = tempPageTitle.ToString(); //Title块
                rd.PageHtmls = tempHtmls.ToString(); //html块
                rd.PagePara = tempPara.ToString();//参数块
                rd.JsonValue = listSetting.ToArray()[0].Table.Rows[0]["JsonValue"].ToString();
                // rd.URL = url;
            }
            return rd;
        }

        /// <summary>
        /// 设置模块客户化配置
        /// </summary>
        /// <returns></returns>
        public int SetCustomerPageSetting(string pCustomerId, string MappingId, string PageKey, string Node, string NodeValue, string UserID)
        {
            int ResultCode = _currentDAO.SetCustomerPageSetting(pCustomerId, MappingId, PageKey, Node, NodeValue, UserID);
            return ResultCode;
        }
        /// <summary>
        /// 获取客户配置也列表
        /// </summary>
        /// <returns></returns>
        public GetSysPageListRD GetCustomerPageList(string pKey, string pName, int pPageIndex, int pPageSize, string pCustomerId)
        {
            GetSysPageListRD rd = new GetSysPageListRD();
            List<ListPageInfo> list = new List<ListPageInfo> { };

            DataSet ds = _currentDAO.GetCustomerPageList(pKey, pName, pPageIndex, pPageSize, pCustomerId);
            int count = 0;
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                count = ds.Tables[1].Rows.Count;
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var pageinfo = new ListPageInfo()
                    {
                        PageId = item["PageId"].ToString(),
                        MappingID = item["MappingID"].ToString(),
                        Title = item["ModuleName"].ToString(),
                        PageKey = item["PageKey"].ToString(),
                        Version = item["Version"].ToString(),
                        LastUpdateTime = item["LastUpdateTime"].ToString()
                    };
                    list.Add(pageinfo);
                }
            }
            rd.TotalPageCount = count;
            rd.PageList = list.ToArray();
            return rd;
        }
        /// <summary>
        /// 获取全部客户信息。用于生成全部客户的Config文件
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomerInfo()
        {
            DataSet ds = _currentDAO.GetCustomerInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
            else return null;
        }

        ///// <summary>
        ///// 获取可配置Config文件数据
        ///// </summary>
        ///// <returns></returns>
        //public string GetCreateCustomerConfig(string pCustomerId)
        //{
        //    CreateCustomerConfigRD rd = new CreateCustomerConfigRD();
        //    Dictionary<string, object> Dic = new Dictionary<string, object>();
        //    DataSet ds = _currentDAO.GetCreateCustomerConfig(pCustomerId);
        //    string PageKey = string.Empty;
        //    if (ds != null && ds.Tables.Count >0 )
        //    {
        //        var listNode0 = ds.Tables[0].AsEnumerable();  //默认有Title
        //        var listNode1 = ds.Tables[1].AsEnumerable();  //默认有html
        //        var listNode2 = ds.Tables[2].AsEnumerable();  //默认有para
        //        var listNode4=ds.Tables[3].AsEnumerable();    //没有
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            ConfigInfo configinfo = new ConfigInfo();
        //            configinfo.title = row["Title"].ToString();//直接取Node=1的 Title
        //            PageKey = row["PageKey"].ToString();
        //            string path = string.Empty;//路径
        //            string plugin = string.Empty;//插件
        //            string strscript = string.Empty;//Script
        //            string Style = string.Empty;//CSS
        //            string strparams = string.Empty;//Node=3时，取para中数据根据DefaultValue判断
        //            string jsonValue = row["jsonValue"].ToString();
        //            var JsonDic = jsonValue.DeserializeJSONTo<Dictionary<string, object>>();  //将Json字符串反序列化成对象
        //            var htmls = JsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//获取所
        //            Dictionary<string, object> html = null; //选择HTML的信息
        //            var temp1list = listNode1.Where(t => t["PageKey"].ToString() == PageKey);
        //            if (temp1list.Count() > 0)
        //            {
        //                html = htmls.Find(t => t["id"].ToString() == temp1list.ToArray()[0]["NodeValue"].ToString());
        //                if (html != null)
        //                {
        //                    path = html["path"].ToString();
        //                    Style = html["css"].ToString();
        //                }
        //            }
        //            else
        //            {
        //                var defaultHtmlId = JsonDic["defaultHtml"].ToString();//默认的HTMLID
        //                html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);
        //                if (html != null)
        //                {
        //                    path = html["path"].ToString();
        //                    Style = html["css"].ToString();
        //                }
        //            }
        //            plugin = JsonDic["plugin"].ToString();
        //            strscript = JsonDic["script"].ToString();
        //            var temp2list = listNode2.Where(t => t["PageKey"].ToString() == PageKey).ToList();
        //            if (temp2list.Count() > 0)
        //            {
        //                string ParaValue = temp2list[0]["NodeValue"].ToString();
        //                strparams = ParaValue.ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "");
        //            }
        //            else
        //            {
        //                //jsonvalue里取
        //                var param = JsonDic["params"].ToJSON().DeserializeJSONTo<object[]>().ToList();
        //                strparams = param.Aggregate(new List<Dictionary<string, object>> { }, (i, j) =>
        //                {
        //                    var temp = j.ToJSON().DeserializeJSONTo<Dictionary<string, object>>();
        //                    i.Add(temp);
        //                    return i;
        //                }).Aggregate(new Dictionary<string, object>(), (i, j) =>
        //                {
        //                    i[j["Key"].ToString()] = j["defaultValue"];
        //                    return i;
        //                }).ToJSON();
        //            }
        //            configinfo.path = path.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "");
        //            configinfo.plugin = plugin.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object[]>();
        //            configinfo.script = strscript.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object[]>();
        //            configinfo.style = Style.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object[]>();
        //            configinfo.param = strparams.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object>(); ;
        //            Dic.Add(PageKey, configinfo);
        //        }
        //    }
        //    return Dic.ToJSON();
        //}


        /// <summary>
        /// 获取可配置Config文件数据
        /// </summary>
        /// <returns></returns>
        public string GetCreateCustomerConfig(string pCustomerId)
        {
            CreateCustomerConfigRD rd = new CreateCustomerConfigRD();
            Dictionary<string, object> Dic = new Dictionary<string, object>();
            DataSet ds = _currentDAO.GetCreateCustomerConfig(pCustomerId);
            string PageKey = string.Empty;
            if (ds != null && ds.Tables.Count > 0)
            {
                var listNode = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ConfigInfo configinfo = new ConfigInfo();

                    string jsonValue = row["jsonValue"].ToString(); //先取出JsonValue
                    var JsonDic = jsonValue.DeserializeJSONTo<Dictionary<string, object>>();  //将Json字符串反序列化成对象
                    var htmls = JsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//获取htmls信息
                    string path = string.Empty;//路径
                    string plugin = string.Empty;//插件
                    string strscript = string.Empty;//Script
                    string Style = string.Empty;//CSS
                    string strparams = string.Empty;//Node=3时，取para中数据根据DefaultValue判断
                    PageKey = row["PageKey"].ToString();
                    try
                    {
                        if (string.IsNullOrWhiteSpace(row["Node"].ToString()))  //如果Node节点为空取。默认Json字符串中的Title
                        {
                            //configinfo.title = row["Title"].ToString();//直接取Node=1的 Title
                            configinfo.title = JsonDic["title"].ToString();
                            Dictionary<string, object> html = null; //选择HTML的信息
                            var temp1list = listNode.Where(t => t["PageKey"].ToString() == PageKey);
                            var defaultHtmlId = JsonDic["defaultHtml"].ToString();//默认的HTMLID
                            html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);
                            if (html != null)
                            {
                                path = html["path"].ToString();
                                Style = html["css"].ToString();
                            }
                            plugin = JsonDic["plugin"].ToString();
                            strscript = JsonDic["script"].ToString();
                            var temp2list = listNode.Where(t => t["PageKey"].ToString() == PageKey).ToList();
                            //jsonvalue里取
                            var param = JsonDic["params"].ToJSON().DeserializeJSONTo<object[]>().ToList();
                            strparams = param.Aggregate(new List<Dictionary<string, object>> { }, (i, j) =>
                            {
                                var temp = j.ToJSON().DeserializeJSONTo<Dictionary<string, object>>();
                                i.Add(temp);
                                return i;
                            }).Aggregate(new Dictionary<string, object>(), (i, j) =>
                            {
                                if (!j.Keys.Contains("defaultValue"))
                                {
                                    throw new Exception(string.Format("PageKey='{0}'params块缺少defaultValue", PageKey));
                                }
                                else
                                {
                                    i[j["Key"].ToString()] = j["defaultValue"];
                                    return i;
                                }
                            }).ToJSON();

                        }
                        else
                        {
                            //configinfo.title = JsonDic["title"].ToString();
                            //configinfo.title = row["NodeValue"].ToString();//直接取Node=1的 Title
                            var PageTitle = listNode.Where(t => t["PageKey"].ToString() == PageKey && t["Node"].ToString() == "1");
                            if (PageTitle.Count() > 0)
                            {
                                configinfo.title = PageTitle.ToArray()[0]["NodeValue"].ToString();
                            }
                            Dictionary<string, object> html = null; //选择HTML的信息
                            var temp1list = listNode.Where(t => t["PageKey"].ToString() == PageKey && t["Node"].ToString() == "2");
                            if (temp1list.Count() > 0)
                            {
                                html = htmls.Find(t => t["id"].ToString() == temp1list.ToArray()[0]["NodeValue"].ToString());
                                if (html != null)
                                {
                                    path = html["path"].ToString();
                                    Style = html["css"].ToString();
                                }
                            }
                            else
                            {
                                var defaultHtmlId = JsonDic["defaultHtml"].ToString();//默认的HTMLID
                                html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);
                                if (html != null)
                                {
                                    path = html["path"].ToString();
                                    Style = html["css"].ToString();
                                }
                            }
                            plugin = JsonDic["plugin"].ToString();
                            strscript = JsonDic["script"].ToString();
                            var temp2list = listNode.Where(t => t["PageKey"].ToString() == PageKey && t["Node"].ToString() == "3").ToList();
                            if (temp2list.Count() > 0)
                            {
                                string ParaValue = temp2list[0]["NodeValue"].ToString();
                                strparams = ParaValue.ToString().Replace("\n", "").Replace("\t", "").Replace("\r", "");
                            }
                            else
                            {
                                //jsonvalue里取
                                var param = JsonDic["params"].ToJSON().DeserializeJSONTo<object[]>().ToList();
                                strparams = param.Aggregate(new List<Dictionary<string, object>> { }, (i, j) =>
                                {
                                    var temp = j.ToJSON().DeserializeJSONTo<Dictionary<string, object>>();
                                    i.Add(temp);
                                    return i;
                                }).Aggregate(new Dictionary<string, object>(), (i, j) =>
                                {
                                    i[j["Key"].ToString()] = j["defaultValue"];
                                    return i;
                                }).ToJSON();
                            }
                        }
                        configinfo.path = path.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "");
                        configinfo.plugin = plugin.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object[]>();
                        configinfo.script = strscript.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object[]>();
                        configinfo.style = Style.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object[]>();
                        configinfo.param = strparams.Trim().Replace("\n", "").Replace("\t", "").Replace("\r", "").DeserializeJSONTo<object>(); ;
                        if (Dic.ContainsKey(PageKey))
                        {

                        }
                        else
                        {
                            Dic.Add(PageKey, configinfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return Dic.ToJSON();
        }

        /// <summary>
        /// 获取可配置Version文件数据
        /// </summary>
        /// <returns></returns>
        public string GetCreateCustomerVersion(string pCustomerId, string pClientName)
        {
            Dictionary<string, object> DicVersion = new Dictionary<string, object>();
            DataSet ds = _currentDAO.GetCreateCustomerVersion(pCustomerId);
            string Key = string.Empty;
            string APP_TYPE = string.Empty;
            if (ds.Tables[1].Rows.Count > 0)
            {
                APP_TYPE = ds.Tables[1].Rows[0]["APP_TYPE"].ToString();
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];
                    if (row["SettingCode"].ToString().Trim() == "ForwardingMessageLogo")
                    {
                        DicVersion.Add("APP_WX_ICO", row["SettingValue"].ToString().Replace("\n", "").Replace("\t", ""));
                    }
                    else if (row["SettingCode"].ToString().Trim() == "ForwardingMessageTitle")
                    {
                        DicVersion.Add("APP_WX_TITLE", row["SettingValue"].ToString().Replace("\n", "").Replace("\t", ""));
                    }
                    //转发消息默认摘要文字
                    else if (row["SettingCode"].ToString().Trim() == "ForwardingMessageSummary")
                    {
                        DicVersion.Add("APP_WX_DES", row["SettingValue"].ToString().Replace("\n", "").Replace("\t", ""));
                    }
                    else
                    {
                        DicVersion[row["SettingCode"].ToString()] = row["SettingValue"].ToString().Replace("\n", "").Replace("\t", "");
                    }
                    if (row["SettingValue"].ToString() == "0")
                    {
                        DicVersion[row["SettingCode"].ToString()] = false;
                    }
                    if (row["SettingValue"].ToString() == "1")
                    {
                        DicVersion[row["SettingCode"].ToString()] = true;
                    }
                }

                if (DicVersion.Keys.Contains("ForwardingMessageTitle"))
                {
                    DicVersion.Remove("ForwardingMessageTitle");
                }
                if (DicVersion.Keys.Contains("ForwardingMessageLogo"))
                {
                    DicVersion.Remove("ForwardingMessageLogo");
                }
                if (DicVersion.Keys.Contains("ForwardingMessageSummary"))
                {
                    DicVersion.Remove("ForwardingMessageSummary");
                }
                if (!DicVersion.Keys.Contains("AJAX_PARAMS"))
                {
                     DicVersion.Add("AJAX_PARAMS", "openId,customerId,applicationId,userId,locale");
                }
                if (!DicVersion.Keys.Contains("APP_JSLIB"))
                {
                    DicVersion.Add("APP_JSLIB", "jquery");
                }
                if (!DicVersion.Keys.Contains("APP_DEBUG"))
                {
                    DicVersion.Add("APP_DEBUG", false);
                }
                if (!DicVersion.Keys.Contains("APP_OPTION_MENU"))
                {
                    DicVersion.Add("APP_OPTION_MENU", true);
                }
                if (!DicVersion.Keys.Contains("APP_TOOL_BAR"))
                {
                    DicVersion.Add("APP_TOOL_BAR", false);
                }
                if (!DicVersion.Keys.Contains("APP_CACHE"))
                {
                    DicVersion.Add("APP_CACHE", true);
                }
                if (!DicVersion.Keys.Contains("APP_WX_ICO"))
                {
                    DicVersion.Add("APP_WX_ICO", "");
                }
                if (!DicVersion.Keys.Contains("APP_WX_TITLE"))
                {
                    DicVersion.Add("APP_WX_TITLE", "");
                }
                if (!DicVersion.Keys.Contains("APP_WX_DES"))
                {
                    DicVersion.Add("APP_WX_DES", "");
                }
            }
            else
            {
                DicVersion.Add("AJAX_PARAMS", "openId,customerId,applicationId,userId,locale");
                DicVersion.Add("APP_JSLIB", "jquery");
                //DicVersion.Add("APP_TYPE", APP_TYPE);
                DicVersion.Add("APP_DEBUG", false);
                DicVersion.Add("APP_OPTION_MENU", true);
                DicVersion.Add("APP_TOOL_BAR", false);
                DicVersion.Add("APP_CACHE", true);
                DicVersion.Add("APP_WX_ICO","");
                DicVersion.Add("APP_WX_TITLE","");
                DicVersion.Add("APP_WX_DES","");
            }
            DicVersion.Add("APP_CODE", "JIT_" + pCustomerId);//JIT_+CustomerID
            DicVersion.Add("APP_VERSION", DateTime.Now.ToString());//取时间戳
            DicVersion.Add("APP_NAME", pClientName);//客户名称
            DicVersion.Add("APP_TYPE", APP_TYPE); //公众号 or 服务号
            return DicVersion.ToJSON();
        }

        /// <summary>
        /// 获取套餐列表
        /// </summary>
        /// <returns></returns>
        public GetVocationVersionMappingListRD GetVocationVersionMappingList(int pPageIndex, int pPageSize)
        {

            GetVocationVersionMappingListRD rd = new GetVocationVersionMappingListRD();
            List<VocationVersionInfo> list = new List<VocationVersionInfo> { };

            DataSet ds = _currentDAO.GetVocationVersionMappingList(pPageIndex, pPageSize);
            int count = 0;
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                count = ds.Tables[1].Rows.Count;
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    var pageinfo = new VocationVersionInfo()
                    {
                        VocaVerMappingID = item["VocaVerMappingID"].ToString(),
                        VocationDesc = item["VocationDesc"].ToString(),
                        VersionDesc = item["VersionDesc"].ToString(),
                    };
                    list.Add(pageinfo);
                }
            }
            rd.TotalPageCount = count;
            rd.VocationVersionList = list.ToArray();
            return rd;
        }
    }
}