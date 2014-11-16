using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.BS.Web.Module.LNews.Handler
{
    /// <summary>
    /// NewsHandler
    /// </summary>
    public class NewsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "news_query":      //新闻查询
                    content = GetNewsData();
                    break;
                case "news_delete":     //新闻删除
                    content = NewsDeleteData();
                    break;
                case "get_news_by_id":  //根据ID获取新闻信息
                    content = GetNewsById();
                    break;
                case "news_save":       //保存新闻信息
                    content = SaveNews();
                    break;
                case "GetNewsTags":
                    content = GetNewsTags();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region NewsDeleteData 新闻删除

        /// <summary>
        /// 新闻删除
        /// </summary>
        public string NewsDeleteData()
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
                responseData.msg = "新闻ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new LNewsBLL(CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetNewsData 查询新闻列表

        /// <summary>
        /// 查询新闻列表
        /// </summary>
        public string GetNewsData()
        {
            var form = Request("form").DeserializeJSONTo<NewsQueryEntity>();
            var newsService = new LNewsBLL(CurrentUserInfo);

            string content = string.Empty;

            string NewsType = FormatParamValue(form.NewsType);
            string NewsTitle = FormatParamValue(form.NewsTitle);
            string PublishTimeBegin = FormatParamValue(form.PublishTimeBegin);
            string PublishTimeEnd = FormatParamValue(form.PublishTimeEnd);
            int startRowIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            var condition = new List<IWhereCondition>();

            if (!NewsType.Equals(string.Empty))
            {
                //entity.parentTypeID = rParams["typeid"].ToString();
                NewsType = Request("typeid").ToString();
                condition.Add(new EqualsCondition() { FieldName = "NewsType", Value = NewsType });
            }
            if (!NewsTitle.Equals(string.Empty))
            {
                condition.Add(new LikeCondition() { FieldName = "NewsTitle", Value = NewsTitle, HasLeftFuzzMatching = true, HasRightFuzzMathing = true });
            }
            if (!PublishTimeBegin.Equals(string.Empty))
            {
                condition.Add(new MoreThanCondition() { FieldName = "PublishTime", Value = PublishTimeBegin, IncludeEquals = true });
            }
            if (!PublishTimeEnd.Equals(string.Empty))
            {
                condition.Add(new LessThanCondition() { FieldName = "PublishTime", Value = PublishTimeEnd, IncludeEquals = true });
            }


            var orderBy = new OrderBy[]{
                new OrderBy{FieldName="IsTop",Direction=OrderByDirections.Desc},
                new OrderBy{ FieldName = "DisplayIndex", Direction=OrderByDirections.Asc }
            };

            var data = newsService.PagedQueryNews(condition.ToArray(), orderBy, PageSize, startRowIndex);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.Entities.ToJSON(),
                data.RowCount);

            return content;
        }

        #endregion

        #region GetNewsById 根据ID获取新闻信息

        /// <summary>
        /// 根据ID获取新闻信息
        /// </summary>
        public string GetNewsById()
        {
            var newsService = new LNewsBLL(CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("NewsId")) != null && FormatParamValue(Request("NewsId")) != string.Empty)
            {
                key = FormatParamValue(Request("NewsId")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "NewsId", Value = key });
            }

            LNewsEntity data = new LNewsEntity();
            var news = newsService.Query(condition.ToArray(), null);

            if (news != null && news.Length > 0)
            {
                data = news.ToList().FirstOrDefault();
                data.StrPublishTime = data.PublishTime.Value.ToString("yyyy-MM-dd");
                LNewsTypeEntity typeentity = new LNewsTypeBLL(CurrentUserInfo).QueryByEntity(new LNewsTypeEntity { NewsTypeId = data.NewsType }, null).FirstOrDefault();
                data.NewsTypeName = typeentity.NewsTypeName;
            }

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveNews 保存新闻信息

        /// <summary>
        /// 保存新闻信息
        /// </summary>
        public string SaveNews()
        {
            var newsService = new LNewsBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string NewsId = string.Empty;
            var news = Request("news");

            if (FormatParamValue(news) != null && FormatParamValue(news) != string.Empty)
            {
                key = FormatParamValue(news).ToString().Trim();
            }
            if (FormatParamValue(Request("NewsId")) != null && FormatParamValue(Request("NewsId")) != string.Empty)
            {
                NewsId = FormatParamValue(Request("NewsId")).ToString().Trim();
            }

            var newsEntity = key.DeserializeJSONTo<LNewsEntity>();

            if (newsEntity.NewsType == null || newsEntity.NewsType.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "新闻类型不能为空";
                return responseData.ToJSON();
            }
            if (newsEntity.NewsTitle == null || newsEntity.NewsTitle.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "新闻标题不能为空";
                return responseData.ToJSON();
            }
            if (newsEntity.PublishTime == null)
            {
                responseData.success = false;
                responseData.msg = "发布时间不能为空";
                return responseData.ToJSON();
            }
            if (newsEntity.Content == null || newsEntity.Content.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "新闻内容不能为空";
                return responseData.ToJSON();
            }

            string host = ConfigurationManager.AppSettings["host"];

            if (NewsId.Trim().Length == 0)
            {
                newsEntity.NewsId = Utils.NewGuid();
                newsEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
                if (newsEntity.ContentUrl.Trim().Length == 0)
                {
                    newsEntity.ContentUrl = host + "newsdetail.aspx?news_id=" + newsEntity.NewsId + "&customerId=" + CurrentUserInfo.CurrentUser.customer_id;
                }
                newsService.Create(newsEntity);
            }
            else
            {
                newsEntity.NewsId = NewsId;
                if (newsEntity.ContentUrl.Trim().Length == 0)
                {
                    newsEntity.ContentUrl = host + "newsdetail.aspx?news_id=" + newsEntity.NewsId + "&customerId=" + CurrentUserInfo.CurrentUser.customer_id;
                }
                newsEntity.CustomerId = this.CurrentUserInfo.ClientID;
                newsService.Update(newsEntity);
            }
            #region Jermyn20131027
            if (newsEntity.StrTags != null && !newsEntity.StrTags.Equals(""))
            {
                LNewsTagMappingBLL mappingServer = new LNewsTagMappingBLL(this.CurrentUserInfo);
                mappingServer.DeleteByNewsId(newsEntity.NewsId);
                string strTags = newsEntity.StrTags;
                string[] ids = strTags.Split(',');
                foreach (string id in ids)
                {
                    LNewsTagMappingEntity info = new LNewsTagMappingEntity();
                    info.MappingId = BaseService.NewGuidPub();
                    info.NewsId = newsEntity.NewsId;
                    info.TagId = id;
                    mappingServer.Create(info);
                }
            }
            #endregion
            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region 获取新闻标签集合
        public string GetNewsTags()
        {
            string newsTags = string.Empty;
            string key = string.Empty;
            if (FormatParamValue(Request("NewsId")) != null && FormatParamValue(Request("NewsId")) != string.Empty)
            {
                key = FormatParamValue(Request("NewsId")).ToString().Trim();
            }
            var servers = new LNewsTagBLL(CurrentUserInfo);
            IList<LNewsTagEntity> list = new List<LNewsTagEntity>();
            list = servers.GetNewsTagsList(key);
            //var strTags = "[{ id: 'chk1', text: 'checkbox1' },{ id: 'chk2', text: 'checkbox2' }]";
            newsTags = "[";
            if (list != null && list.Count > 0)
            {
                var i = 0;
                foreach (var info in list)
                {
                    newsTags += "{";
                    newsTags += "id: 'chk" + info.DisplayIndex.ToString() + "' , ";
                    newsTags += "text: '" + info.TagName.ToString() + "' , ";
                    newsTags += "value: '" + info.TagId.ToString() + "' , ";
                    if (info.IsCheck == 1)
                    {
                        newsTags += "checked: true";
                    }
                    else
                    {
                        newsTags += "checked: false";
                    }
                    newsTags += "}";
                    i = i + 1;
                    if (i != list.Count)
                    {
                        newsTags += ",";
                    }
                }
            }
            newsTags += "]";

            return newsTags;
        }
        #endregion
    }

    #region QueryEntity

    public class NewsQueryEntity
    {
        public string NewsId;
        public string NewsType;
        public string NewsTitle;
        public string NewsSubTitle;
        public string PublishTimeBegin;
        public string PublishTimeEnd;
        public string ContentUrl;
        public string Conten;
        public string ImageUrl;
        public string ThumbnailImageUrl;
        public string APPId;
    }

    #endregion
}