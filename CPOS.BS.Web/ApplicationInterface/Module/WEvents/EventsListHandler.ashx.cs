using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.Web.ApplicationInterface.Vip;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Web.Module.WEvents.Handler;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents
{
    /// <summary>
    /// EventsListHandler 的摘要说明
    /// </summary>
    public class EventsListHandler : BaseGateway
    {

        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_VIP_SOURCE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        const int ERROR_MEMBER_REGISTERED = 314;

        const int ERROR_VIPCARD_EXISTS = 340;   //会员已办卡
        #endregion


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetEventList":  //活动列表 
                    rst = this.GetEventList(pRequest);
                    break;
            
                case "DeleteEvent"://删除活动
                    rst = this.DeleteEvent(pRequest);
                    break;

                case "StartOrStopEvent":
                    rst = this.StartOrStopEvent(pRequest);
                    break;
                case "GetWorkingEventList":
                    rst = this.GetWorkingEventList(pRequest);
                    break;
                case "ExportJoinData":
                    rst = this.ExportJoinData(pRequest);
                    break;
                case "ExportWinnerData":
                    rst = this.ExportJoinData(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            //HttpContext.Current.Response.ContentType = "text/html;charset=UTF-8";  
            return rst;
        }
        #region  活动列表
        public string GetEventList(string pRequest)
        {
            EventListRD rd = new EventListRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);
            DataSet ds = bll.GetEventList(reqObj.Parameters.PageIndex,reqObj.Parameters.PageSize,reqObj.Parameters.Title,reqObj.Parameters.DrawMethodName,reqObj.Parameters.BeginTime,reqObj.Parameters.EndTime);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.LEventsList = DataTableToObject.ConvertToList<EventsList>(ds.Tables[0]).ToArray();//直接根据所需要的字段反序列化
                rd.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString());
                rd.PageCount = Convert.ToInt32(ds.Tables[1].Rows[0]["PageCount"].ToString());
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
      
        #endregion
        #region 获取运行中的活动
        public string GetWorkingEventList(string pRequest)
        {
            EventListRD rd = new EventListRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);
            var ds = bll.GetWorkingEventList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.LEventsList = DataTableToObject.ConvertToList<EventsList>(ds.Tables[0]).ToArray();//直接根据所需要的字段反序列化

            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 删除活动(改变IsDelete状态)
        public string DeleteEvent(string pRequest)
        {
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);

            bll.DeleteByProc(reqObj.Parameters.EventId);
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 启用/停用活动
        public string StartOrStopEvent(string pRequest)
        {
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventStatus>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);

            bll.UpdateEventStatus(reqObj.Parameters.EventId, reqObj.Parameters.Status);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region   ExportJoinData
        public string ExportJoinData(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var couponBLL = new CouponBLL(loggingSessionInfo);

            var rd = new CouponManagePagedSearchRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<CouponManagePagedSearchRP>>();
            rp.Parameters.Validate();

            string fileName = "";

            DataTable dataTable = couponBLL.GetExportData(rp.Parameters);
            //var rsp = new SuccessResponse<IAPIResponseData>(rd);


            fileName = Utils.DataTableToExcel(dataTable, "list", "使用记录", "post");

            return fileName;
        }
        #endregion
    }
    public class EventListRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public string Title { get; set; }
        public string DrawMethodName { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }

      
        public void Validate()
        {
        }
    }
    public class EventListRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public IList<EventsList> LEventsList { get; set; }//活动方式列表       

    }
    public class EventStatus : IAPIRequestParameter
    {
        public int Status { get; set; }
        public string EventId { get; set; }
        public void Validate()
        {
        }
    }
    public class EventsList
    {
        public string EventID { get; set; }
        public string Title {get;set;}
        public string BeginTime{get;set;}
        public string EndTime { get; set; }
        public int DrawMethodID { get; set; }
        public string DrawMethodName { get; set; }

        public int JoinCount { get; set; }
        public int WinnerCount { get; set; }
        public int VipCardType { get; set; }
        public int VipCardGrade { get; set; }
        public string VipCardTypeName { get; set; }
        public string VipCardGradeName { get; set; }
        /// <summary>
        /// 二维码地址
        /// </summary>
        public string ImageUrl { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}