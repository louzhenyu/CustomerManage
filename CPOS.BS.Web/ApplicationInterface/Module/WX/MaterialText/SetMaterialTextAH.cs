using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Request;
using JIT.CPOS.DTO.Module.WeiXin.MaterialText.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.ExtensionMethod;
using System.Configuration;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.MaterialText
{
    public class SetMaterialTextAH : BaseActionHandler<SetMaterialTextRP, SetMaterialTextRD>
    {
        #region 错误码
        #endregion

        protected override SetMaterialTextRD ProcessRequest(APIRequest<SetMaterialTextRP> pRequest)
        {
            SetMaterialTextRD rd = new SetMaterialTextRD();
            var para = pRequest.Parameters;

            #region 获取Page信息
            var pageBll = new SysPageBLL(CurrentUserInfo);
            var textBll = new WMaterialTextBLL(CurrentUserInfo);

            //组织图文实体
            var entity = new WMaterialTextEntity()
            {
                ApplicationId = para.MaterialText.ApplicationId,
                CoverImageUrl = para.MaterialText.ImageUrl,
                PageId = para.MaterialText.PageID,

                PageParamJson = para.MaterialText.PageParamJson,
                Text = para.MaterialText.Text,
                TextId = para.MaterialText.TextId,
                Title = para.MaterialText.Title,
                TypeId = para.MaterialText.TypeId,
                Author=para.MaterialText.Abstract//摘要使用原来的字段
            };
            #endregion

            #region 生成URL
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");

            var Domain1 = ConfigurationManager.AppSettings["interfacehost1"].Replace("http://", "");
            string URL = string.Empty;
            switch (para.MaterialText.UnionTypeId)
            {
                case "1"://链接
                    #region 链接
                    URL = para.MaterialText.OriginalUrl;
                    #endregion
                    break;
                case "2"://详情
                    #region 详情
                    URL = string.Format("http://{0}/Module/WeiXin/MaterialTextDetail.aspx?news_id=#TextId#&customerId=#cutomerId#&interfacehost={1}", Domain1.Trim('/'), ConfigurationManager.AppSettings["interfacehost1"]);
                    #endregion
                    break;
                case "3"://系统模块
                    #region 系统模块
                    var pages = pageBll.GetPageByID(para.MaterialText.PageID);//通过pageid查找syspage信息
                    if (pages.Length == 0)
                        throw new APIException("未找到Page:" + para.MaterialText.PageID) { ErrorCode = 341 };
                    SysPageEntity CurrentPage;
                    string path = string.Empty;//要替换的路径
                    string urlTemplate = pages[0].URLTemplate;//模板URL
                    string json = pages[0].JsonValue;// JSON体
                    var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//转换后的字典
                    var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//所有的Html模板
                    Dictionary<string, object> html = null;//选择的html信息
                    var defaultHtmlId = jsonDic["defaultHtml"].ToString();
                    html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);//默认的htmlid*****
                    if (html != null)
                        path = html["path"].ToString();
                    //判断高级oauth认证
                    var scope = "snsapi_base";
                    if (jsonDic.ContainsKey("scope"))
                    {
                        scope = (jsonDic["scope"] == null || jsonDic["scope"] == "") ? "snsapi_base" : "snsapi_userinfo";
                    }
                  

                    //判断是否有定制,没有则取JSON体中的默认
                    //找出订制内容
                    var customerPages = pages.ToList().FindAll(t => t.CustomerID == CurrentUserInfo.ClientID);
                    if (customerPages.Count > 0)
                    {
                        //看是否有htmls的定制(Node值=2)
                        CurrentPage = customerPages.Find(t => t.Node == "2");
                        if (CurrentPage != null)
                        {
                            var nodeValue = CurrentPage.NodeValue;
                            //在Json解析后的集合中找到path
                            html = htmls.Find(t => t["id"].ToString() == nodeValue);
                            if (html != null)
                            {
                                path = html["path"].ToString();
                            }
                        }
                        else
                        {
                            CurrentPage = pages[0];
                        }
                    }
                    else
                    {
                        CurrentPage = pages[0];
                    }
                    //读取配置信息域名,检查不用http://开头,如果有则去除
                    var IsAuth = false;
                    //TODO:判断页面是否需要Auth认证,如果页面需要证则再判断这个客户有没有Auth认证,Type=3
                    if (CurrentPage.IsAuth == 1)
                    {
                        //判断这个客户是否是认证客户,type=3
                        var applicationBll = new WApplicationInterfaceBLL(CurrentUserInfo);
                        var application = applicationBll.GetByID(para.MaterialText.ApplicationId);
                        if (application.WeiXinTypeId == "3")
                        {
                            IsAuth = true;
                        }
                    }

                    //替换URL模板
                    #region 替换URL模板
                    urlTemplate = urlTemplate.Replace("_pageName_", path);
                    var paraDic = para.MaterialText.PageParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
                    foreach (var item in paraDic)   //这里key和value对于活动来说，其实就是活动的eventId，和eventId的值
                    {
                        if (item.ContainsKey("key") && item.ContainsKey("value"))
                            urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
                    }
                    #endregion

                    //根据规则组织URL
                    #region 组织URL
                    //读取配置文件中的域名

                    if (string.IsNullOrEmpty(Domain))
                        throw new APIException("微信管理:未配置域名,请在web.config中添加<add key='host' value=''/>") { ErrorCode = 342 };
                    if (IsAuth)
                    {
                        //需要认证,并加上scope类型
                        URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&applicationId={2}&goUrl={3}&scope={4}", Domain.Trim('/'), CurrentUserInfo.ClientID, para.MaterialText.ApplicationId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)),scope);
                    }
                    else
                    {
                        //不需要认证
                        URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), CurrentUserInfo.ClientID, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/'))));
                    }
                    entity.IsAuth = Convert.ToInt32(IsAuth);
                    entity.PageParamJson = para.MaterialText.PageParamJson;
                    #endregion
                    #endregion
                    break;
                default:
                    break;
            }

            entity.OriginalUrl = URL;//图文素材
            #endregion

            #region 保存
            var unionMappingBll = new WModelTextMappingBLL(CurrentUserInfo);
            if (string.IsNullOrEmpty(entity.TextId))
            {
                entity.TextId = Guid.NewGuid().ToString("N");
                #region 图文详情要对占位符#TextId#和#customerId#进行替换
                entity.OriginalUrl = entity.OriginalUrl.Replace("#TextId#", entity.TextId).Replace("#cutomerId#", CurrentUserInfo.ClientID);
                #endregion
                textBll.Create(entity);
                var mapping = new WModelTextMappingEntity()
                {
                    MappingId = Guid.NewGuid().ToString("N"),
                    ModelId = entity.TypeId,
                    TextId = entity.TextId,
                };
                unionMappingBll.Create(mapping);
            }
            else
            {
                #region 图文详情要对占位符#textId和#customerId进行替换
                entity.OriginalUrl = entity.OriginalUrl.Replace("#TextId#", entity.TextId).Replace("#cutomerId#", CurrentUserInfo.ClientID);
                #endregion
                textBll.Update(entity);
            }

            #region 图文分类和图文Mapping关系,先删除再增加
            var modelMappingBll = new WModelTextMappingBLL(CurrentUserInfo);
            modelMappingBll.DeleteByTextId(entity.TextId);
            var modelMapping = new WModelTextMappingEntity()
            {
                MappingId = Guid.NewGuid().ToString("N"),
                ModelId = entity.TypeId,   //模板的标识竟然是和图文类型的标识
                TextId = entity.TextId
            };
            modelMappingBll.Create(modelMapping);
            #endregion

            #endregion

            rd.MaterialTextId = entity.TextId;
            return rd;
        }
    }
}