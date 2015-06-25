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
namespace JIT.CPOS.BS.Web.ApplicationInterface.AllWin
{
    /// <summary>
    /// RetailTrader 的摘要说明
    /// </summary>
    public class RetailTrader : BaseGateway
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
            string rst = "";
            switch (pAction)
            {
                case "GetRetailTraders":// 获取某个销售员下的分销商的列表信息
                    rst = this.GetRetailTraders(pRequest);
                    break;
                case "ToggleRetailStatus":// 停用启用分销商接口
                    rst = this.ToggleRetailStatus(pRequest);
                    break;
                case "GetSellerMonthRewardList":// 销售员月度奖励情况
                    rst = this.GetSellerMonthRewardList(pRequest);
                    break;
                case "GetRetailMonthRewardList":// 分销商月度奖励情况
                    rst = this.GetRetailMonthRewardList(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        #region   获取 分销商的列表信息
        public string GetRetailTraders(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();

            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;


            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetRetailTradersBySellUserRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            var ds = bll.GetRetailTraders(rp.Parameters.RetailTraderName, rp.Parameters.RetailTraderAddress, rp.Parameters.RetailTraderMan,
                       rp.Parameters.Status, rp.Parameters.CooperateType, rp.Parameters.UnitID, "", loggingSessionInfo.ClientID, loggingSessionInfo.UserID, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取



            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[1];
                rd.RetailTraderList = DataTableToObject.ConvertToList<RetailTraderInfo>(tempDt);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));


            }

            return rsp.ToJSON();
        }
        #endregion


        #region  停用启用分销商接口
        public string ToggleRetailStatus(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<ToggleRetailStatusRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }
            if (string.IsNullOrEmpty(rp.Parameters.Status))
            {
                throw new APIException("缺少参数【Status】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new RetailTraderBLL(loggingSessionInfo);

            RetailTraderEntity en = new RetailTraderEntity();
            en.RetailTraderID = rp.Parameters.RetailTraderID;
            en.Status = rp.Parameters.Status;
            bll.Update(en, null, false);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);


            return rsp.ToJSON();
        }
        #endregion






        #region 获取分销商某个月的奖励情况
        public string GetSellerMonthRewardList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetSellUserRewardListRP>>();

            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;


            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetSellerMonthRewardListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID，必须要传customerID
            var ds = bll.GetSellerMonthRewardList(rp.Parameters.UnitID, rp.Parameters.SellerOrRetailName, rp.Parameters.Year,
                       rp.Parameters.Month, loggingSessionInfo.ClientID, loggingSessionInfo.UserID, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取



            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[1];
                rd.SellerMonthRewardList = DataTableToObject.ConvertToList<SellerMonthRewards>(tempDt);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));

            }

            return rsp.ToJSON();
        }
        #endregion


        #region 获取分销商某个月的奖励情况
        public string GetRetailMonthRewardList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetSellUserRewardListRP>>();

            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;


            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetRetailMonthRewardListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID，必须要传customerID
            var ds = bll.GetRetailMonthRewardList(rp.Parameters.UnitID, rp.Parameters.SellerOrRetailName, rp.Parameters.Year,
                       rp.Parameters.Month, loggingSessionInfo.ClientID, loggingSessionInfo.UserID, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取

            //判断账号是否存在
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[1];
                rd.RetailMonthRewardList = DataTableToObject.ConvertToList<RetailMonthRewards>(tempDt);//直接根据所需要的字段反序列化
                rd.TotalCount = ds.Tables[0].Rows.Count;
                rd.TotalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(ds.Tables[0].Rows.Count * 1.00 / (pageSize ?? 15) * 1.00)));


            }

            return rsp.ToJSON();
        }
        #endregion

    }
    public class SellUserMainAchieveRP : IAPIRequestParameter
    {

        #region 分页需要
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// ASC / DESC
        /// </summary>
        public string OrderType { get; set; }
        #endregion
        public string UserOrRetailID { get; set; }
        public string RetailTraderName { get; set; }

        public string RetailTraderAddress { get; set; }
        public string RetailTraderMan { get; set; }
        public string Status { get; set; }
        public string CooperateType { get; set; }
        public string UnitID { get; set; }




        public void Validate()
        {
        }
    }

    public class GetRetailTradersBySellUserRD : IAPIResponseData
    {
        public List<RetailTraderInfo> RetailTraderList { get; set; }

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }


    public class ToggleRetailStatusRP : IAPIRequestParameter
    {


        public string Status { get; set; }
        public string RetailTraderID { get; set; }




        public void Validate()
        {
        }
    }

    public class RetailTraderInfo
    {

        public string StatusDesc { get; set; }  //状态描述
        public string CooperateTypeDesc { get; set; } //合作方式
        public decimal EndAmount { get; set; }//余额


        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string RetailTraderID { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? RetailTraderCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderPass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderMan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RetailTraderAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CooperateType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SellUserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UnitID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerId { get; set; }


        #endregion

        public String UnitName { get; set; }
        public String UserName { get; set; }
        public String ImageURL { get; set; }

        public int VipCount { get; set; }

        public string QRImageUrl { get; set; }
    }

    public class GetSellUserRewardListRP : IAPIRequestParameter
    {
        #region 分页需要
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public string OrderBy { get; set; }
        /// <summary>
        /// ASC / DESC
        /// </summary>
        public string OrderType { get; set; }
        #endregion

        public string UnitID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public string SellerOrRetailName { get; set; }

        public void Validate()
        {
        }

    }
    public class GetSellerMonthRewardListRD : IAPIResponseData
    {
        public List<SellerMonthRewards> SellerMonthRewardList { get; set; }//销售员奖励信息

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }


    }
    public class GetRetailMonthRewardListRD : IAPIResponseData
    {
        public List<RetailMonthRewards> RetailMonthRewardList { get; set; }//销售员奖励信息

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }

    public class RetailMonthRewards
    {
        public string RetailTraderName { get; set; }
        public string RetailTraderAddress { get; set; }
        public string UnitName { get; set; }
        public string UnitID { get; set; }
        public string YearAndMonth { get; set; }
        public decimal MonthAmount { get; set; }
        public int MonthVipCount { get; set; }
        public int MonthOrderCount { get; set; }

    }

    public class SellerMonthRewards
    {
        public string user_name { get; set; }
        public string user_telephone { get; set; }
        public string UnitName { get; set; }
       // public string UnitID { get; set; }
        public string YearAndMonth { get; set; }
        public decimal MonthAmount { get; set; }
      
    }

}