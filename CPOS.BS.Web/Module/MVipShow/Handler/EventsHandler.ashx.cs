using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using System.Configuration;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.Web.Module.MVipShow.Handler 
{
    /// <summary>
    /// EventsHandler
    /// </summary>
    public class EventsHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
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
                case "events_query":      //活动查询
                    content = GetEventsData();
                    break;
                case "events_delete":     //活动删除
                    content = EventsDeleteData();
                    break;
                case "prizes_delete":     //奖品删除
                    content = PrizesDeleteData();
                    break;
                case "get_events_by_id":  //根据ID获取活动信息
                    content = GetEventsById();
                    break;
                case "get_prizes_by_id":  //根据ID获取奖品信息
                    content = GetPrizesById();
                    break;
                case "events_save":       //保存活动信息
                    content = SaveEvents();
                    break;
                case "prizes_save":       //保存奖品信息
                    content = SavePrizes();
                    break;
                case "events_user_list_query":      //活动人员查询
                    content = GetEventsUserListData();
                    break;
                case "events_prizes_list_query":      //奖品查询
                    content = GetEventsPrizesListData();
                    break;
                case "events_lotterylog_list_query":      //抽奖日志查询
                    content = GetEventsLotteryLogListData();
                    break;
                case "get_images":
                    content = GetImagesData();
                    break;
                case "image_delete":
                    content = ImageDeleteData();
                    break;
                case "events_praise":
                    content = EventPraiseData();
                    break;
                case "events_pass":
                    content = EventPassData();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region EventsDeleteData 活动删除

        /// <summary>
        /// 活动删除
        /// </summary>
        public string EventsDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            MVipShowBLL service = new MVipShowBLL(CurrentUserInfo);

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

            var status = "-1";
            if (FormatParamValue(Request("status")) != null && FormatParamValue(Request("status")) != string.Empty)
            {
                status = FormatParamValue(Request("status")).ToString().Trim();
            }

            string[] ids = key.Split(',');
            foreach (var tmpId in ids)
            {
                if (tmpId.Trim().Length > 0)
                {
                    service.SetStatus(CurrentUserInfo, tmpId.Trim(), status);
                }
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region 奖品删除

        /// <summary>
        /// 奖品删除
        /// </summary>
        public string PrizesDeleteData()
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
                responseData.msg = "奖品ID不能为空";
                return responseData.ToJSON();
            }

            string[] ids = key.Split(',');
            new LPrizesBLL(this.CurrentUserInfo).Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetEventsData 查询活动列表

        /// <summary>
        /// 查询活动列表
        /// </summary>
        public string GetEventsData()
        {
            var form = Request("form").DeserializeJSONTo<EventsQueryEntity>();
            var eventsService = new MVipShowBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string VipName = FormatParamValue(form.VipName);
            string Experience = FormatParamValue(form.Experience);
            string ItemName = FormatParamValue(form.ItemName);
            string BeginTime = FormatParamValue(form.BeginTime);
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            MVipShowEntity queryEntity = new MVipShowEntity();
            queryEntity.VipName = VipName;
            queryEntity.Experience = Experience;
            queryEntity.ItemName = ItemName;
            queryEntity.BeginTime = BeginTime;
            queryEntity.UseDelete = false;
            queryEntity.CustomerId = CurrentUserInfo.CurrentUser.customer_id;
            queryEntity.OrderBy = "CreateTime";

            var list = eventsService.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = eventsService.GetListCount(queryEntity);

            var data = new List<MVipShowEntity>();
            foreach (var item in list)
            {
                if (item.Experience != null && item.Experience.Length > 25)
                {
                    item.Experience = item.Experience.Substring(0, 25) + "...";
                }
                item.ImageCount = item.ImageList.Count;
                data.Add(item);
            }

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region GetEventsById 根据ID获取活动信息

        /// <summary>
        /// 根据ID获取活动信息
        /// </summary>
        public string GetEventsById()
        {
            var eventsService = new MVipShowBLL(this.CurrentUserInfo);
            var vipBLL = new VipBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("EventID")) != null && FormatParamValue(Request("EventID")) != string.Empty)
            {
                key = FormatParamValue(Request("EventID")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "VipShowId", Value = key });
            }

            //var data = eventsService.Query(condition.ToArray(), null).ToList().FirstOrDefault();
            MVipShowEntity queryEntity = new MVipShowEntity();
            queryEntity.VipShowId = key;
            queryEntity.UseDelete = false;
            var list = eventsService.GetList(queryEntity, 1, 1);
            var data = new MVipShowEntity();
            if (list != null && list.Count > 0)
            {
                data = list[0];
                var tmpVip = vipBLL.GetByID(list[0].VipId);
                if (tmpVip != null)
                {
                    data.VipName = tmpVip.VipName;
                }
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

        #region 根据ID获取奖品信息

        /// <summary>
        /// 根据ID获取奖品信息
        /// </summary>
        public string GetPrizesById()
        {
            var eventsService = new LPrizesBLL(this.CurrentUserInfo);
            //var eventWeiXinMappingBLL = new EventWeiXinMappingBLL(this.CurrentUserInfo);
            string content = string.Empty;

            string key = string.Empty;
            if (FormatParamValue(Request("PrizesID")) != null && FormatParamValue(Request("PrizesID")) != string.Empty)
            {
                key = FormatParamValue(Request("PrizesID")).ToString().Trim();
            }

            var condition = new List<IWhereCondition>();
            if (!key.Equals(string.Empty))
            {
                condition.Add(new EqualsCondition() { FieldName = "PrizesID", Value = key });
            }

            var data = eventsService.Query(condition.ToArray(), null).ToList().FirstOrDefault();
            //if (data != null)
            //{
            //    data.StrPublishTime = data.PublishTime.Value.ToString("yyyy-MM-dd");
            //}

            //data.StartDateText = data.StartTime == null ? string.Empty :
            //    Convert.ToDateTime(data.StartTime).ToString("yyyy-MM-dd");
            //data.EndDateText = data.StartTime == null ? string.Empty :
            //    Convert.ToDateTime(data.StartTime).ToString("yyyy-MM-dd");

            //data.Description =  HttpUtility.HtmlDecode(data.Description);

            //var eventWeiXinMappings = eventWeiXinMappingBLL.QueryByEntity(new EventWeiXinMappingEntity()
            //{
            //    EventID = key
            //}, null);

            //if (eventWeiXinMappings != null && eventWeiXinMappings.Length > 0)
            //{
            //    data.WeiXinPublicID = eventWeiXinMappings[0].WeiXinPublicID;
            //}

            var jsonData = new JsonData();
            jsonData.totalCount = (data == null) ? "0" : "1";
            jsonData.data = data;

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                (data == null) ? "0" : "1");

            return content;
        }

        #endregion

        #region SaveEvents 保存活动信息

        /// <summary>
        /// 保存活动信息
        /// </summary>
        public string SaveEvents()
        {
            var eventsService = new MVipShowBLL(this.CurrentUserInfo);
            var mVipShowItemMappingBLL = new MVipShowItemMappingBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string EventID = string.Empty;
            var events = Request("events");

            if (FormatParamValue(events) != null && FormatParamValue(events) != string.Empty)
            {
                key = FormatParamValue(events).ToString().Trim();
            }
            if (FormatParamValue(Request("EventID")) != null && FormatParamValue(Request("EventID")) != string.Empty)
            {
                EventID = FormatParamValue(Request("EventID")).ToString().Trim();
            }

            var eventsEntity = key.DeserializeJSONTo<MVipShowEntity>();

            //if (eventsEntity.EventType == null || eventsEntity.EventType.ToString().Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "活动类型不能为空";
            //    return responseData.ToJSON();
            //}
            //if (eventsEntity.Title == null || eventsEntity.Title.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "活动标题不能为空";
            //    return responseData.ToJSON();
            //}
            //if (eventsEntity.StartTimeText == null || eventsEntity.StartTimeText.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "起始时间不能为空";
            //    return responseData.ToJSON();
            //}
            //if (eventsEntity.EndTimeText == null || eventsEntity.EndTimeText.Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "结束时间不能为空";
            //    return responseData.ToJSON();
            //}

            //try
            //{
            //    eventsEntity.BeginTime = eventsEntity.StartTimeText;
            //}
            //catch
            //{
            //    responseData.success = false;
            //    responseData.msg = "起始时间格式错误";
            //    return responseData.ToJSON();
            //}

            //try
            //{
            //    eventsEntity.EndTime = eventsEntity.EndTimeText;
            //}
            //catch
            //{
            //    responseData.success = false;
            //    responseData.msg = "结束时间格式错误";
            //    return responseData.ToJSON();
            //}

            eventsEntity.Experience = HttpUtility.HtmlEncode(eventsEntity.Experience);
            //eventsEntity.CustomerId = this.CurrentUserInfo.CurrentLoggingManager.Customer_Id.ToString().Trim();
            if (EventID.Trim().Length == 0)
            {
                eventsEntity.VipShowId = Utils.NewGuid();

                var tmpMapList = mVipShowItemMappingBLL.QueryByEntity(new MVipShowItemMappingEntity() {
                    VipShowId = eventsEntity.VipShowId
                }, null);
                if (tmpMapList != null)
                {
                    foreach (var tmpMapItem in tmpMapList)
                    {
                        mVipShowItemMappingBLL.Delete(tmpMapItem);
                    }
                }
                mVipShowItemMappingBLL.Create(new MVipShowItemMappingEntity() {
                    MappingId = Utils.NewGuid(),
                    VipShowId = eventsEntity.VipShowId,
                    ItemId = eventsEntity.ItemId
                });

                eventsService.Create(eventsEntity);
            }
            else
            {
                eventsEntity.VipShowId = EventID;

                var tmpMapList = mVipShowItemMappingBLL.QueryByEntity(new MVipShowItemMappingEntity()
                {
                    VipShowId = eventsEntity.VipShowId
                }, null);
                if (tmpMapList != null)
                {
                    foreach (var tmpMapItem in tmpMapList)
                    {
                        mVipShowItemMappingBLL.Delete(tmpMapItem);
                    }
                }
                mVipShowItemMappingBLL.Create(new MVipShowItemMappingEntity()
                {
                    MappingId = Utils.NewGuid(),
                    VipShowId = eventsEntity.VipShowId,
                    ItemId = eventsEntity.ItemId
                });

                eventsService.Update(eventsEntity, false);
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

        #region 查询活动人员列表2
        ///// <summary>
        ///// 查询活动人员列表2
        ///// </summary>
        //public string GetEventsUserListData2()
        //{
        //    var eventsService = new LEventsBLL(this.CurrentUserInfo);
        //    string content = string.Empty;

        //    string EventId = FormatParamValue(Request("EventID"));
        //    string searchSql = FormatParamValue(Request("SearchOptionValue")).Trim();

        //    Loggers.Debug(new DebugLogInfo() { Message = "GetEventsUserListData2 searchSql: " + searchSql });

        //    DataTable data = new DataTable();
        //    WEventUserMappingBLL service = new WEventUserMappingBLL(this.CurrentUserInfo);
        //    QuesQuestionsBLL quesQuestionsBLL = new QuesQuestionsBLL(this.CurrentUserInfo);
        //    data = service.SearchEventUserList(EventId, searchSql).Tables[0];

        //    var dataApplyQues = service.getEventApplyQues(EventId);
        //    int dataTotalCount = data.Rows.Count;

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("<tr>");
        //    for (var i = 1; i < data.Columns.Count; i++)
        //    {
        //        var col = data.Columns[i];
        //        string colName = service.GetQuestionsDesc(EventId, col.ColumnName);
        //        sb.AppendFormat("<th class=\"z_tb1_th\">{0}</th>", colName);
        //    }
        //    sb.AppendFormat("</tr>");

        //    for (var i = 0; i < data.Rows.Count; i++)
        //    {
        //        var dr = data.Rows[i];
        //        sb.AppendFormat("<tr>");
        //        for (var c = 1; c < data.Columns.Count; c++)
        //        {
        //            sb.AppendFormat("<td class=\"{1}\">{0}</td>",
        //                dr[c],
        //                (i % 2 == 0 ? "z_tb1_td" : "z_tb1_td z_tb1_td_alt"));
        //        }
        //        sb.AppendFormat("</tr>");
        //    }

        //    // options
        //    StringBuilder sbOptions = new StringBuilder();
        //    int colCount = 3;
        //    for (var i = 0; i < dataApplyQues.Params.QuesQuestionEntityList.Count; i++)
        //    {
        //        var obj = dataApplyQues.Params.QuesQuestionEntityList[i];
        //        if (i % colCount == 0)
        //        {
        //            sbOptions.AppendFormat("<tr>");
        //        }

        //        StringBuilder sbOp = new StringBuilder();
        //        if (obj.QuestionType == 3)
        //        {
        //            sbOp.AppendFormat("<select name=\"op\" cookie_name=\"{0}\" class=\"z_tb2_select\">", obj.CookieName);
        //            sbOp.AppendFormat("<option value=\"\" class=\"z_tb2_op\"></option>");
        //            for (var c = 0; c < obj.QuesOptionEntityList.Count; c++)
        //            {
        //                sbOp.AppendFormat("<option value=\"{1}\" class=\"z_tb2_op\">{0}</option>",
        //                    obj.QuesOptionEntityList[c].OptionsText,
        //                    obj.QuesOptionEntityList[c].OptionsText);
        //            }
        //            sbOp.AppendFormat("</select>");
        //        }
        //        else
        //        {
        //            sbOp.AppendFormat("<input name=\"op\" cookie_name=\"{0}\" class=\"z_tb2_text\" />", obj.CookieName);
        //        }

        //        sbOptions.AppendFormat("<td class=\"z_tb2_td\"><div class=\"z_tb2_title\" title=\"{2}\">{0}</div>" +
        //            "<div class=\"z_tb2_input\">{1}</div></td>",
        //            GetMaxStr(obj.QuestionDesc, 18),
        //            sbOp.ToString(),
        //            obj.QuestionDesc);

        //        if (i % colCount == colCount - 1 ||
        //            i + 1 == dataApplyQues.Params.QuesQuestionEntityList.Count)
        //        {
        //            sbOptions.AppendFormat("</tr>");
        //        }
        //    }

        //    content = string.Format("{{\"totalCount\":{1}, \"html\":{0}, \"projectName\":{3}, \"options\":{2}}}",
        //        sb.ToString().ToJSON(),
        //        dataTotalCount,
        //        sbOptions.ToString().ToJSON(),
        //        "\"WEvent\"");

        //    return content;
        //}

        //public string GetMaxStr(string str, int length)
        //{
        //    str = str.Trim();
        //    if (str.Length > length) return str.Substring(0, length) + "...";
        //    return str;
        //}
        #endregion

        #region GetEventsUserListDataFormEmba 查询活动人员列表（EMBA）
        ///// <summary>
        ///// 查询活动人员列表（EMBA）
        ///// </summary>
        //public string GetEventsUserListDataFormEmba()
        //{
        //    var service = new WEventUserMappingBLL(this.CurrentUserInfo);
        //    string content = string.Empty;
        //    string EventId = FormatParamValue(Request("EventID"));

        //    var data = service.SearchEventUserList(EventId).Tables[0];
        //    int dataTotalCount = data.Rows.Count;

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("<tr>");
        //    for (var i = 0; i < data.Columns.Count; i++)
        //    {
        //        sb.AppendFormat("<th class=\"z_tb1_th\">{0}</th>", data.Columns[i].ColumnName);
        //    }
        //    sb.AppendFormat("</tr>");

        //    for (var i = 0; i < data.Rows.Count; i++)
        //    {
        //        var dr = data.Rows[i];
        //        sb.AppendFormat("<tr>");
        //        for (var c = 0; c < data.Columns.Count; c++)
        //        {
        //            sb.AppendFormat("<td class=\"{1}\">{0}</td>",
        //                dr[c],
        //                (i % 2 == 0 ? "z_tb1_td" : "z_tb1_td z_tb1_td_alt"));
        //        }
        //        sb.AppendFormat("</tr>");
        //    }

        //    content = string.Format("{{\"totalCount\":{1}, \"html\":{0}, \"projectName\":{3}, \"options\":{2}}}",
        //        sb.ToString().ToJSON(),
        //        dataTotalCount,
        //        "\"\"",
        //        "\"EMBA\"");

        //    return content;
        //}

        #endregion

        #region 查询活动奖品列表

        /// <summary>
        /// 查询活动奖品列表
        /// </summary>
        public string GetEventsPrizesListData()
        {
            var service = new LPrizesBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string EventId = FormatParamValue(Request("EventId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            GetResponseParams<LPrizesEntity> dataList =
                service.GetEventPrizes(EventId, pageIndex, PageSize);
            IList<LPrizesEntity> data = new List<LPrizesEntity>();
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

        #region 查询活动抽奖列表

        /// <summary>
        /// 查询活动抽奖列表
        /// </summary>
        public string GetEventsLotteryLogListData()
        {
            var service = new LLotteryLogBLL(this.CurrentUserInfo);

            string content = string.Empty;

            string EventId = FormatParamValue(Request("EventId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;

            GetResponseParams<LLotteryLogEntity> dataList =
                service.GetEventLotteryLog(EventId, pageIndex, PageSize);
            IList<LLotteryLogEntity> data = new List<LLotteryLogEntity>();
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

        #region 保存奖品信息

        /// <summary>
        /// 保存奖品信息
        /// </summary>
        public string SavePrizes()
        {
            var eventsService = new LPrizesBLL(this.CurrentUserInfo);

            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();

            string key = string.Empty;
            string PrizesID = string.Empty;
            var events = Request("events");

            if (FormatParamValue(events) != null && FormatParamValue(events) != string.Empty)
            {
                key = FormatParamValue(events).ToString().Trim();
            }
            if (FormatParamValue(Request("PrizesID")) != null && FormatParamValue(Request("PrizesID")) != string.Empty)
            {
                PrizesID = FormatParamValue(Request("PrizesID")).ToString().Trim();
            }

            var eventsEntity = key.DeserializeJSONTo<LPrizesEntity>();

            //if (eventsEntity.EventType == null || eventsEntity.EventType.ToString().Trim().Length == 0)
            //{
            //    responseData.success = false;
            //    responseData.msg = "活动类型不能为空";
            //    return responseData.ToJSON();
            //}
            if (eventsEntity.PrizeName == null || eventsEntity.PrizeName.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "奖品名称不能为空";
                return responseData.ToJSON();
            }

            if (PrizesID.Trim().Length == 0)
            {
                eventsEntity.PrizesID = Utils.NewGuid();

                eventsService.Create(eventsEntity);
            }
            else
            {
                eventsEntity.PrizesID = PrizesID;

                eventsService.Update(eventsEntity, false);
            }

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region GetImagesData
        /// <summary>
        /// GetImagesData
        /// </summary>
        public string GetImagesData()
        {
            var objectImagesBLL = new ObjectImagesBLL(this.CurrentUserInfo);

            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page")));

            ObjectImagesEntity queryEntity = new ObjectImagesEntity();
            queryEntity.ObjectId = FormatParamValue(Request("EventId"));

            var data = objectImagesBLL.GetList(queryEntity, pageIndex, PageSize);
            var dataTotalCount = objectImagesBLL.GetListCount(queryEntity);

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region ImageDeleteData 

        /// <summary>
        /// ImageDeleteData
        /// </summary>
        public string ImageDeleteData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            ObjectImagesBLL service = new ObjectImagesBLL(CurrentUserInfo);

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
            service.Delete(ids);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region EventPraiseData

        /// <summary>
        /// EventPraiseData
        /// </summary>
        public string EventPraiseData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            LEventsEntriesPraiseBLL service = new LEventsEntriesPraiseBLL(CurrentUserInfo);

            string key = string.Empty;
            key = FormatParamValue(Request("id")).ToString().Trim();
            string vipId = FormatParamValue(Request("vipId")).ToString().Trim();

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "ID不能为空";
                return responseData.ToJSON();
            }

            service.Create(new LEventsEntriesPraiseEntity() {
                PraiseId = Utils.NewGuid(),
                SignUpID = vipId,
                EntriesId = key
            });

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

        #region EventPassData

        /// <summary>
        /// EventPassData
        /// </summary>
        public string EventPassData()
        {
            string content = string.Empty;
            string error = "";
            var responseData = new ResponseData();
            MVipShowBLL service = new MVipShowBLL(CurrentUserInfo);

            string key = string.Empty;
            key = FormatParamValue(Request("id")).ToString().Trim();
            string status = FormatParamValue(Request("status")).ToString().Trim();

            if (key == null || key.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "ID不能为空";
                return responseData.ToJSON();
            }

            var rnd = new Random();
            var rndNum = rnd.Next(1, 99999999);
            string lotteryCode = rndNum.ToString().PadLeft(8, '0');
            service.Update(new MVipShowEntity()
            {
                VipShowId = key,
                IsCheck = Convert.ToInt32(status),
                LotteryCode = lotteryCode
            }, false);

            responseData.success = true;
            responseData.msg = error;

            content = responseData.ToJSON();
            return content;
        }

        #endregion

    }

    #region QueryEntity

    public class EventsQueryEntity
    {
        public string VipShowId;
        public string VipName;
        public string Experience;
        public string ItemName;
        public string BeginTime;
    }

    #endregion
}