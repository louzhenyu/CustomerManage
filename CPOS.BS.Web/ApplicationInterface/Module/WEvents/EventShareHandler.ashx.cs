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
    /// EventShareHandler 的摘要说明
    /// </summary>
    public class EventShareHandler : BaseGateway
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
                case "GetEventShareList":  //活动列表 
                    rst = this.GetEventShareList(pRequest);
                    break;
                case "AddEventShare"://添加分享设置
                    rst = this.AddEventShare(pRequest);
                    break;
                case "AppendEventShare":
                    rst = this.AppendEventShare(pRequest);
                    break;
                case "StartOrStopShare":
                    rst = this.StartOrStopShare(pRequest);
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
        #region 设置列表
        /// <summary>
        /// 分享设置列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetEventShareList(string pRequest)
        {


            var rp = pRequest.DeserializeJSONTo<APIRequest<ShareListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new EventShareRD();//返回值
            LEventsShareBLL bll = new LEventsShareBLL(loggingSessionInfo);
            var ds = bll.GetShareList(rp.Parameters.PageIndex, rp.Parameters.PageSize);
            rd.EventShareList = DataTableToObject.ConvertToList<EventShare>(ds.Tables[0]).ToArray(); ;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 添加分享
        /// <summary>
        /// 添加活动分享
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string AddEventShare(string pRequest)
        {
            var rd = new EmptyRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<EventShareRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsShareBLL bllShare = new LEventsShareBLL(loggingSessionInfo);
            PrizeCouponTypeMappingBLL bllPrize = new PrizeCouponTypeMappingBLL(loggingSessionInfo);

            if (bllShare.HasShare(rp.Parameters.EventId) > 0)
            {
                var errRsp = new ErrorResponse();
                errRsp.Message = "该活动已设置了分享";
                return errRsp.ToJSON();
            }
            var entityShare = new LEventsShareEntity();


            string strGuid = Guid.NewGuid().ToString();

            entityShare.ShareId = strGuid;
            entityShare.EventId = rp.Parameters.EventId;
            //entityShare.PrizeTypeID = rp.Parameters.PrizeTypeID;
            //entityShare.TotalCount = rp.Parameters.TotalCount;
            entityShare.IsDelete = 0;
            entityShare.State = 1;
            entityShare.ShareTimes = rp.Parameters.ShareTimes;

            bllShare.Create(entityShare);

            var entityPrize = new LPrizesEntity();

            entityPrize.EventId = strGuid;
            entityPrize.PrizeName = rp.Parameters.PrizeName;
            entityPrize.PrizeTypeId = rp.Parameters.PrizeTypeID;
            entityPrize.Point = rp.Parameters.Point;
            entityPrize.CouponTypeID = rp.Parameters.CouponTypeID;
            entityPrize.CountTotal = rp.Parameters.TotalCount;
            entityPrize.CreateBy = loggingSessionInfo.UserID;
            bllShare.SaveSharePrize(entityPrize);

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 追加分享
        public string AppendEventShare(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AppendShareRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsShareBLL bllShare = new LEventsShareBLL(loggingSessionInfo);
            PrizeCouponTypeMappingBLL bllPrize = new PrizeCouponTypeMappingBLL(loggingSessionInfo);

            var entityPrize = new LPrizesEntity();

            entityPrize.PrizesID = rp.Parameters.PrizesId;
            entityPrize.EventId = rp.Parameters.ShareId;
            entityPrize.CountTotal = rp.Parameters.AppendQty;
            entityPrize.LastUpdateBy = loggingSessionInfo.UserID;

            bllShare.AppendSharePrize(entityPrize);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 启用/停用活动
        public string StartOrStopShare(string pRequest)
        {
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventShareRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsShareBLL bll = new LEventsShareBLL(loggingSessionInfo);

            bll.UpdateEventShareStatus(reqObj.Parameters.ShareId, reqObj.Parameters.State);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion



    }
    public class ShareListRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public void Validate()
        {
        }
    }

    public class AppendShareRP : IAPIRequestParameter
    {

        public string PrizesId { get; set; }

        public int AppendQty { get; set; }
        public string ShareId { get; set; }

        public void Validate()
        {
        }
    }
    public class EventShareRP : IAPIRequestParameter
    {
        public string ShareId { get; set; }

        public string EventId { get; set; }
        public string PrizeTypeID { get; set; }

        public string CouponTypeID { get; set; }
        public string PrizeName { get; set; }
        public int TotalCount { get; set; }
        /// <summary>
        /// 每次分享奖励积分数量
        /// </summary>
        public int? Point { get; set; }
        /// <summary>
        /// 分享奖励次数（仅一次：OnlyOne，无限制：NoLimit）
        /// </summary>
        public string ShareTimes { get; set; }

        public int State { get; set; }
        public void Validate()
        {
        }
    }
    public class EventShareRD : IAPIResponseData
    {
        public string ShareId { get; set; }

        public IList<EventShare> EventShareList { get; set; }
    }
    public class EventShare
    {
        public string ShareId { get; set; }
        public string PrizesID { get; set; }
        public string PrizeName { get; set; }
        public string EventName { get; set; }
        public int CountTotal { get; set; }

        public int WinnerCount { get; set; }

        public string ShareState { get; set; }
        public string EventState { get; set; }

    }
}