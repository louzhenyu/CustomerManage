using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using System.Text;

namespace JIT.CPOS.BS.Web.Module.LEventsEntries.Handler
{
    /// <summary>
    /// LEventsEntriesHandler
    /// </summary>
    public class LEventsEntriesHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "LEventsEntries_query":      //新闻查询
                    content = GetLEventsEntriesData();
                    break;
                case "LEventsEntries_delete":     //新闻删除
                    content = LEventsEntriesDeleteData();
                    break;
                case "get_LEventsEntries_by_id":  //根据ID获取新闻信息
                    content = GetLEventsEntriesById();
                    break;
                case "LEventsEntries_save":       //保存新闻信息
                    content = SaveLEventsEntries();
                    break;
                case "events_user_list_query":      //活动人员查询
                    content = GetEventsUserListData();
                    break;
                case "get_comment_list":        //获取评论列表
                    content = GetCommentList();
                    break;
                case "comment_delete":     //评论删除
                    content = LEventsCommentDeleteData();
                    break;
                case "set_crowdaren":     //设置围观达人
                    content = SetCrowDaren();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region LEventsEntriesDeleteData 新闻删除

        /// <summary>
        /// 新闻删除
        /// </summary>
        public string LEventsEntriesDeleteData()
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
            new LEventsEntriesBLL(CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetLEventsEntriesData 查询新闻列表

        /// <summary>
        /// 查询新闻列表
        /// </summary>
        public string GetLEventsEntriesData()
        {
            var service = new LEventsEntriesBLL(CurrentUserInfo);

            var WorkDate = FormatParamValue(Request("WorkDate"));
            var Creative = FormatParamValue(Request("Creative"));
            var Category = FormatParamValue(Request("Category"));
            var EventId = FormatParamValue(Request("EventId"));
            var page = Utils.GetIntVal(FormatParamValue(Request("page")));
            if (page <= 0) page = 1;
            page -= 1;

            var list = new List<LEventsEntriesEntity>();
            var totalCount = 0;
            var pageSize = 16;

            var queryEntity = new LEventsEntriesEntity();
            queryEntity.WorkDate = WorkDate;
            queryEntity.Creative = Creative;
            queryEntity.EventId = EventId;

            if (Category == "1") queryEntity.IsWorkDaren = 1;
            else if (Category == "2") queryEntity.IsMonthDaren = 1;

            list = service.GetWebList(queryEntity, page, pageSize).ToList();
            totalCount = service.GetWebListCount(queryEntity);
            var pageCount = totalCount / pageSize;
            if (totalCount % pageSize > 0) pageCount += 1;

            StringBuilder content = new StringBuilder();
            content.AppendFormat("<input id=\"hTotalCount\" type=\"hidden\" value=\"{0}\"/>", totalCount);
            content.AppendFormat("<input id=\"hPage\" type=\"hidden\" value=\"{0}\"/>", 1);
            content.AppendFormat("<input id=\"hPageCount\" type=\"hidden\" value=\"{0}\"/>", pageCount);
            content.AppendFormat("<div id=\"nodata\" style=\"display:none;\" class=\"DaRenNoData\">未查找到数据</div>");
            foreach (var item in list)
            {
                content.AppendFormat("<div class=\"DaRenImgBox\">");
                content.AppendFormat("<ul>");
                content.AppendFormat("<li class=\"DaRenImg\" onclick=\"fnView('{2}')\"><span class=\"DaRenImgIndex\">{1}</span><img src=\"{0}\" onload=\"photoSize(this,240,180)\" /></li>", item.WorkUrl, item.DisplayIndex, item.EntriesId);
                content.AppendFormat("<li class=\"DaRenImgT\">");
                content.AppendFormat("<div class=\"DaRenImgTLi\">");
                content.AppendFormat("<span class=\" DaRenIcnSkin {1}\" checked=\"{0}\" onclick=\"ClickCheckedEvent(this, '{2}')\"></span><em >爱秀达人</em>",
                    item.IsWorkDaren == 1 ? "true" : "false",
                    item.IsWorkDaren == 1 ? "DaRenCheckBoxTrue" : "DaRenCheckBoxfalse",
                    item.EntriesId);
                content.AppendFormat("</div>");
                content.AppendFormat("<div class=\"DaRenImgTLi\">");
                content.AppendFormat("<span class=\"DaRenIcnSkin {1}\"  checked=\"{0}\"  onclick=\"ClickCheckedEvent2(this, '{2}')\"></span><em>品味达人</em>",
                    item.IsMonthDaren == 1 ? "true" : "false",
                    item.IsMonthDaren == 1 ? "DaRenCheckBoxTrue" : "DaRenCheckBoxfalse",
                    item.EntriesId);
                content.AppendFormat("</div>");
                content.AppendFormat("</li>");
                content.AppendFormat("<li class=\"DaRenImgT\">");
                content.AppendFormat("<div class=\"DaRenImgTLi\">");
                content.AppendFormat("<span class=\"DaRenIcnSkin DaRenCheckBoxGood\"></span><i>({0})</i>", item.PraiseCount);
                content.AppendFormat("</div>");
                content.AppendFormat("<div class=\"DaRenImgTLi\">");
                content.AppendFormat("<span class=\"DaRenIcnSkin DaRenCheckBoxComment\"></span><i style=\"cursor:pointer;\" onclick=\"fnView3('{1}')\">({0})</i>", item.CommentCount, item.EntriesId);
                content.AppendFormat("</div>");
                content.AppendFormat("</li>");
                content.AppendFormat("</ul>");
                content.AppendFormat("</div>");
            }

            return content.ToString();
        }

        #endregion

        #region GetLEventsEntriesById 根据ID获取新闻信息

        /// <summary>
        /// 根据ID获取新闻信息
        /// </summary>
        public string GetLEventsEntriesById()
        {
            var service = new LEventsEntriesBLL(CurrentUserInfo);
            string content = string.Empty;

            LEventsEntriesEntity data = null;
            string key = string.Empty;
            key = FormatParamValue(Request("id")).ToString().Trim();

            var queryEntity = new LEventsEntriesEntity();
            queryEntity.EntriesId = key;
            var list = service.GetWebList(queryEntity, 0, 1).ToList();
            if (list.Count > 0) data = list[0];

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveLEventsEntries 保存新闻信息

        /// <summary>
        /// 保存新闻信息
        /// </summary>
        public string SaveLEventsEntries()
        {
            var newsService = new LEventsEntriesBLL(CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string LEventsEntriesId = string.Empty;
            var news = Request("item");

            if (FormatParamValue(news) != null && FormatParamValue(news) != string.Empty)
            {
                key = FormatParamValue(news).ToString().Trim();
            }
            if (FormatParamValue(Request("id")) != null && FormatParamValue(Request("id")) != string.Empty)
            {
                LEventsEntriesId = FormatParamValue(Request("id")).ToString().Trim();
            }

            var newsEntity = key.DeserializeJSONTo<LEventsEntriesEntity>();

            //string host = ConfigurationManager.AppSettings["host"];

            if (LEventsEntriesId.Trim().Length == 0)
            {
                newsEntity.EntriesId = Utils.NewGuid();
                //if (newsEntity.ContentUrl.Trim().Length == 0)
                //{
                //    newsEntity.ContentUrl = host + "newsdetail.aspx?news_id=" + newsEntity.LEventsEntriesId;
                //}
                newsService.Create(newsEntity);
            }
            else
            {
                newsEntity.EntriesId = LEventsEntriesId;
                //if (newsEntity.ContentUrl.Trim().Length == 0)
                //{
                //    newsEntity.ContentUrl = host + "newsdetail.aspx?news_id=" + newsEntity.LEventsEntriesId;
                //}
                newsService.Update(newsEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region 查询活动人员列表

        /// <summary>
        /// 查询活动人员列表
        /// </summary>
        public string GetEventsUserListData()
        {
            var service = new LEventSignUpBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string EventId = FormatParamValue(Request("EventId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            GetResponseParams<LEventSignUpEntity> dataList =
                service.GetEventApplies(EventId, pageIndex, PageSize);
            IList<LEventSignUpEntity> data = new List<LEventSignUpEntity>();
            int dataTotalCount = 0;

            if (dataList.Flag == "1")
            {
                data = dataList.Params.EntityList;
                dataTotalCount = dataList.Params.ICount;
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region 获取评论列表

        /// <summary>
        /// 获取评论列表
        /// </summary>
        public string GetCommentList()
        {
            string content = string.Empty;
            var service = new LEventsEntriesCommentBLL(this.CurrentUserInfo);

            string entriesId = FormatParamValue(Request("entriesId"));
            string date = FormatParamValue(Request("date"));
            string IsCrowdDaren = FormatParamValue(Request("IsCrowdDaren"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            var dataList = service.GetCommentList(entriesId, date, IsCrowdDaren, pageIndex, PageSize);
            IList<LEventsEntriesCommentEntity> data = new List<LEventsEntriesCommentEntity>();
            int dataTotalCount = 0;

            if (dataList.Flag == "1")
            {
                data = dataList.Params.CommentList;
                dataTotalCount = dataList.Params.ICount;
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region LEventsCommentDeleteData 评论删除

        /// <summary>
        /// 评论删除
        /// </summary>
        public string LEventsCommentDeleteData()
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
                responseData.msg = "评论ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new LEventsEntriesCommentBLL(CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region SetCrowDaren 设置围观达人

        /// <summary>
        /// 设置围观达人
        /// </summary>
        public string SetCrowDaren()
        {
            var service = new LEventsEntriesCommentBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string commentId = string.Empty;
            string isCrowdDaren = string.Empty;

            commentId = FormatParamValue(Request("commentId")).ToString().Trim();
            isCrowdDaren = FormatParamValue(Request("isCrowdDaren")).ToString().Trim();
            
            service.Update(new LEventsEntriesCommentEntity()
            {
                CommentId = commentId,
                IsCrowdDaren = int.Parse(isCrowdDaren)
            }, false);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion
    }

    #region QueryEntity

    public class LEventsEntriesQueryEntity
    {
        public string LEventsEntriesId;
        public string LEventsEntriesType;
        public string LEventsEntriesTitle;
        public string LEventsEntriesSubTitle;
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