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
using System.Configuration;

namespace JIT.CPOS.BS.Web.Module.MarketEvent.EventList.Handler
{
    /// <summary>
    /// EventListHandler
    /// </summary>
    public class EventListHandler : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        public string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
        /// <summary>
        /// 页面入口
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("method:{0}", pContext.Request.QueryString["method"])
            });
            switch (pContext.Request.QueryString["method"])
            {
                case "eventlist_query":      //活动列表查询
                    content = GetEventListData();
                    break;
                case "eventInfo": //活动详细信息
                    content = GetEventInfoData();
                    break;
                case "waveBand_query":    //活动时间校准查询
                    content = GetWaveBandData();
                    break;
                case "testSend": //启动界面的--测试发送
                    content = SetTestSendData();
                    break;
                case "runSend": //启动界面的--启动发送2500人
                    content = SetRunSendData();
                    break;
                case "waveBand_save":   //时间校验提交
                    content = SetWaveBandData();
                    break;
                case "eventResponse_query": //活动响应
                    content = GetEventRequestData();
                    break;
                case "eventAnalysis_query": //活动分析查询
                    content = GetEventAnalysisData();
                    break;
                case "eventPerson_query": //获取邀约客户数量
                    content = GetMarketPersonCount();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }

        #region GetEventListData 查询活动列表

        /// <summary>
        /// 查询活动列表
        /// </summary>
        public string GetEventListData()
        {
            var eventService = new MarketEventBLL(this.CurrentUserInfo);

            string content = string.Empty;

            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("GetEventListData:{0}", pageIndex)
            });
            var data = eventService.GetEventList(pageIndex, PageSize);
            var dataTotalCount = eventService.GetEventListCount();

            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                dataTotalCount);

            return content;
        }

        #endregion

        #region 活动详细信息
        public string GetEventInfoData()
        {
            string content = string.Empty;
            var eventService = new MarketEventBLL(this.CurrentUserInfo);
            string EventID = FormatParamValue(Request("eventId"));
            //Loggers.Debug(new DebugLogInfo()
            //{
            //    Message = string.Format("eventId:{0}", EventID)
            //});
            if (EventID != null && !EventID.Equals(""))
            {
                var data = eventService.GetMarketEventInfoById(EventID);
                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("GetEventInfoData:{0}", data.ToJSON())
                //});
                content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                1);
            }
            return content;
        }
        #endregion

        #region 获取活动校准信息
        public string GetWaveBandData() {
            string content = string.Empty;
            var waveBandServer = new MarketWaveBandBLL(this.CurrentUserInfo);
            string EventID = FormatParamValue(Request("eventId"));  //活动标识
            int Page = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;//页码
            MarketWaveBandEntity data = new MarketWaveBandEntity();
            if (EventID != null && !EventID.Equals(""))
            {
                data = waveBandServer.GetMarketWaveBandByEventID(EventID, Page, PageSize);

            }
            content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.MarketWaveBandInfoList.ToJSON(),
                data.ICount);
            return content;
        }
        #endregion

        #region 测试发送
        /// <summary>
        /// 测试发送
        /// </summary>
        /// <returns></returns>
        public string SetTestSendData()
        {
            var responseData = new ResponseData();
            string content = string.Empty;
            string EventID = FormatParamValue(Request("eventId"));  //活动标识
            if (EventID == null || EventID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动标识不能为空";
                return responseData.ToJSON();
            }

            MarketPersonBLL bll = new MarketPersonBLL(this.CurrentUserInfo);
            bool b = bll.SetEventPush(EventID, msgUrl, "1", true, true, true);

            responseData.success = true;
            responseData.msg = "测试发送成功";

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 启动界面的--启动发送2500人
        public string SetRunSendData() {
            string content = string.Empty;
            var responseData = new ResponseData();
            string EventID = FormatParamValue(Request("eventId"));  //活动标识
            if (EventID == null || EventID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动标识不能为空";
                return responseData.ToJSON();
            }
            MarketPersonBLL bll = new MarketPersonBLL(this.CurrentUserInfo);
            bool b = bll.SetEventPush(EventID, msgUrl, "2", true, true, true);

            MarketEventBLL eventBll = new MarketEventBLL(this.CurrentUserInfo);
            bool bReturn = eventBll.SetMarketEventStatus(2, EventID);

            responseData.success = true;
            responseData.msg = "启用发送正式客户成功";

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 获取邀约客户数量
        public string GetMarketPersonCount()
        {
            string content = string.Empty;
            var responseData = new ResponseData();
            string EventID = FormatParamValue(Request("eventId"));  //活动标识
            if (EventID == null || EventID.Trim().Length == 0)
            {
                responseData.success = false;
                responseData.msg = "活动标识不能为空";
                return responseData.ToJSON();
            }
            MarketPersonBLL bll = new MarketPersonBLL(this.CurrentUserInfo);
            int count = bll.GetMarketPersonByEventID(EventID);

            
            responseData.success = true;
            responseData.msg = count.ToString();

            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 时间校验提交
        public string SetWaveBandData()
        {
            string content = string.Empty;
            var responseData = new ResponseData();
            string key = string.Empty;
            if (Request("data") != null && Request("data") != string.Empty)
            {
                key = Request("data").ToString().Trim();
            }
            MarketWaveBandEntity marketWaveBandEntity = new MarketWaveBandEntity();
            marketWaveBandEntity = key.DeserializeJSONTo<MarketWaveBandEntity>();
            if (marketWaveBandEntity == null) {
                responseData.success = false;
                responseData.msg = "没有传递波段信息";
                return responseData.ToJSON();
            }
            if (marketWaveBandEntity.MarketWaveBandInfoList == null && marketWaveBandEntity.MarketWaveBandInfoList.Count == 0)
            {
                responseData.success = false;
                responseData.msg = "没有传递波段内容";
                return responseData.ToJSON();
            }
            MarketWaveBandBLL server = new MarketWaveBandBLL(this.CurrentUserInfo);
            foreach (MarketWaveBandEntity info in marketWaveBandEntity.MarketWaveBandInfoList)
            {
                if (info.WaveBandID == null || info.WaveBandID.Equals(""))
                {
                    responseData.success = false;
                    responseData.msg = "没有传递主标识";
                    return responseData.ToJSON();
                }
                else
                {
                    if (info.WaveBandID.Equals("xxxxx"))
                    {
                        MarketEventEntity eventInfo = new MarketEventEntity();
                        eventInfo.MarketEventID = info.MarketEventID;
                        eventInfo.BeginTime = info.FactBeginTime;
                        eventInfo.EndTime = info.FactEndTime;
                        MarketEventBLL eventBll = new MarketEventBLL(this.CurrentUserInfo);
                        eventBll.Update(eventInfo, false);
                    }
                    else
                    {
                        server.Update(info,false);
                    }
                }
            }
            responseData.success = true;
            responseData.msg = "保存成功.";
            content = responseData.ToJSON();
            return content;
        }
        #endregion

        #region 活动响应
        /// <summary>
        /// 活动响应
        /// </summary>
        /// <returns></returns>
        public string GetEventRequestData()
        { 
            string content = string.Empty;
            var responseService = new MarketEventResponseBLL(this.CurrentUserInfo);
            string EventID = FormatParamValue(Request("eventId"));
            int pageIndex = Utils.GetIntVal(FormatParamValue(Request("page"))) - 1;
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("EventID:{0}", EventID)
            });
            if (EventID != null && !EventID.Equals(""))
            {
                var data = responseService.GetEventResponseInfo(EventID,pageIndex,PageSize);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetEventRequestData:{0}", data.ToJSON())
                });
                content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.MarketEventResponseInfoList.ToJSON(),
                data.ICount);
            }

            return content;
        }
        #endregion

        #region 活动分析
        /// <summary>
        /// 活动分析
        /// </summary>
        /// <returns></returns>
        public string GetEventAnalysisData()
        {
            string content = string.Empty;
            string EventID = FormatParamValue(Request("eventId"));
            var eventService = new MarketEventBLL(this.CurrentUserInfo);
            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("EventID:{0}", EventID)
            });
            if (EventID != null && !EventID.Equals(""))
            {
                var data = eventService.GetEventAnalysisInfo(EventID);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetEventAnalysisData:{0}", data.ToJSON())
                });
                content = string.Format("{{\"totalCount\":{1},\"topics\":{0}}}",
                data.ToJSON(),
                1);
            }
            return content;
        }
        #endregion
    }
}