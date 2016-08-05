using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

using JIT.CPOS.Common;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.PageBase;
using JIT.CPOS.BS.Web.Extension;

using JIT.Utility.Web;
using JIT.Utility.Reflection;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using System.Configuration;

namespace JIT.CPOS.BS.Web.Project.CEIBS.EventStats.Handler
{
    /// <summary>
    /// EventStatsHander 的摘要说明
    /// </summary>
    public class EventStatsHander : JITCPOSAjaxHandler
    {

        protected override void AjaxRequest(HttpContext pContext)
        {
            string res = string.Empty;
            switch (this.Method)
            {
                case "EventStatsPageData":
                    res = EventStatsPageData(pContext);
                    break;
                case "GetOptionID":
                    res = GetOptionID(pContext);
                    break;
                case "EventStatsSave":
                    res = EventStatsSave(pContext);
                    break;
                case "EventStatsRemove":
                    res = DelEventStats(pContext);
                    break;
                case "GetEventStatsDetail":
                    res = GetEventStatsDetail(pContext);
                    break;
                default:
                    break;
            }
            pContext.Response.Write(res);
            pContext.Response.End();
        }

        #region EventStatsPageData
        public string EventStatsPageData(HttpContext pContext)
        {
            string objectType = pContext.Request.Form["objectType"].ToString();
            string title = pContext.Request["title"];
            int pageSize = pContext.Request["limit"].ToInt();
            int pageIndex = pContext.Request["page"].ToInt();
            CEIBSBLL bsbll = new CEIBSBLL(CurrentUserInfo, "EventStats");
            // return ;
            // return string.Format("{{\"topics\":{0}}}", bsbll.EventStatsPageData("", pageSize, pageIndex - 1).Tables[0].ToJSON());
            DataSet ds = bsbll.EventStatsPageData(objectType, title, pageSize, pageIndex - 1);
            return string.Format("{{\"totalCount\":{1},\"topics\":{0}}}", ds.Tables[0].ToJSON(), ds.Tables[1].Rows[0][0]);
        }
        #endregion

        #region GetOptionID
        /// <summary>
        /// 获取类型对应的标题
        /// </summary>
        /// <param name="pContext"></param>
        /// <returns></returns>
        public string GetOptionID(HttpContext pContext)
        {
            string objectType = pContext.Request["objecttype"];
            CEIBSBLL bsbll = new CEIBSBLL(CurrentUserInfo, "EventStats");
            if (!string.IsNullOrEmpty(objectType))
            {
                DataSet ds = bsbll.GetOptionID(objectType);

                return string.Format("{0}", ds.Tables[0].ToJSON());
            }
            return string.Empty;
        }
        #endregion

        #region EventStatsSave
        public string EventStatsSave(HttpContext pContext)
        {
            string evenStatsID = pContext.Request["eventStatsId"];
            string objectType = pContext.Request["objectType"];
            string title = pContext.Request["title"];
            string sequence = pContext.Request["sequence"];
            CEIBSBLL bsbll = new CEIBSBLL(CurrentUserInfo, "EventStats");
            int res = bsbll.EventStatsSave(evenStatsID, objectType, title, sequence);
            if (res > 0)
            {
                return "{success:true,msg:'保存成功'}";
            }
            else
            {
                return "{success:false,msg:'保存失败,标题已存在'}";
            }

        }
        #endregion

        #region DelEventStats
        public string DelEventStats(HttpContext pContext)
        {
            string id = pContext.Request["eventStatid"];
            CEIBSBLL bsbll = new CEIBSBLL(CurrentUserInfo, "EventStats");
            int res = bsbll.DelEventStats(id);
            if (res > 0)
            {
                return "{success:true,msg:'删除成功'}";
            }
            else
            {
                return "{success:false,msg:'删除失败'}";
            }
        }
        #endregion

        #region GetEventStatsDetail
        public string GetEventStatsDetail(HttpContext pContext)
        {
            string id = pContext.Request["eventStatid"];
            CEIBSBLL bsbll = new CEIBSBLL(CurrentUserInfo, "EventStats");
            DataSet ds = bsbll.GetEventStatsDetail(id);
            return string.Format("{{\"eventsDetail\":{0}}}", ds.Tables[0].ToJSON()); ;

        }
        #endregion
    }
}