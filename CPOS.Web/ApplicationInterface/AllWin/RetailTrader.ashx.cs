using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using System.Configuration;
using JIT.Utility.Log;
using JIT.Utility.Web;
using JIT.Utility.DataAccess.Query;
using JIT.Utility;
using JIT.CPOS.DTO.Module.RetailTrader.Request;
using JIT.CPOS.DTO.Module.RetailTrader.Response;


namespace JIT.CPOS.Web.ApplicationInterface.AllWin
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
                case "GetMultiLevelSalerQuery":
                    rst = this.GetMultiLevelSalerQuery(pRequest);
                    break;

                case "GetRewards"://本月奖励金额、发展会员奖励、会员消费奖励接口
                    rst = this.GetRewards(pRequest);
                    break;
                case "MonthDayRewards"://本月每日奖励
                    rst = this.MonthDayRewards(pRequest);
                    break;
                case "GetRewardsDayRiseList"://最近15天奖励趋势
                    rst = this.GetRewardsDayRiseList(pRequest);
                    break;
                case "GetVipDayRiseList"://最近15天新增会员数趋势
                    rst = this.GetVipDayRiseList(pRequest);
                    break;
                case "GetRetailVipInfos"://分销商今日新增会员数、本月新增会员数、总会员数
                    rst = this.GetRetailVipInfos(pRequest);
                    break;
                case "RetailTraderMain"://兰博士为我发券数量、核销数量、我为兰博士带多少会员、奖励多少钱
                    rst = this.RetailTraderMain(pRequest);
                    break;
                case "RetailSetPassWord"://修改分销商密码
                    rst = this.RetailSetPassWord(pRequest);
                    break;
                case "RetailCoupon":// 优惠券已核销人数列表、未核销人数列表、总数
                    rst = this.RetailCoupon(pRequest);
                    break;
                case "SetPreRetailTraderToNomal"://将潜在经销商更新为正常经销商
                    rst = this.SetPreRetailTraderToNomal(pRequest);
                    break;
                case "GetVipMultiLevelSalerConfig"://获取经销上配置
                    rst = this.GetVipMultiLevelSalerConfig(pRequest);
                    break;

                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        #region 橙果财商
        /// <summary>
        /// 获取经销商统计、等级列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetMultiLevelSalerQuery(string pRequest)
        {
            var RD = new MultiLevelSalerQueryRD();
            var rsp = new SuccessResponse<IAPIResponseData>(RD);
            var rp = pRequest.DeserializeJSONTo<APIRequest<MultiLevelSalerQueryRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var RetailTraderBLL = new RetailTraderBLL(loggingSessionInfo);

            DataSet ds = RetailTraderBLL.GetMultiLevelSalerQuery(rp.UserID);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RD.RetailerList = new List<Retailer>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        #region 请求参数赋值
                        var Data = new Retailer();
                        if (ds.Tables[0].Rows[i]["RetailTraderID"] != DBNull.Value)
                        {
                            Data.RetailTraderID = ds.Tables[0].Rows[i]["RetailTraderID"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["HigheRetailTraderID"] != DBNull.Value)
                        {
                            Data.HigheRetailTraderID = ds.Tables[0].Rows[i]["HigheRetailTraderID"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["RetailTraderName"] != DBNull.Value)
                        {
                            Data.RetailTraderName = ds.Tables[0].Rows[i]["RetailTraderName"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["Parentname"] != DBNull.Value)
                        {
                            Data.Parentname = ds.Tables[0].Rows[i]["Parentname"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["Hierarchy"] != DBNull.Value)
                        {
                            Data.Hierarchy = ds.Tables[0].Rows[i]["Hierarchy"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["CreateTime"] != DBNull.Value)
                        {
                            Data.CreateTime = ds.Tables[0].Rows[i]["CreateTime"].ToString();
                        }
                        if (ds.Tables[0].Rows[i]["Idx"] != DBNull.Value)
                        {
                            Data.Idx = Convert.ToInt32(ds.Tables[0].Rows[i]["Idx"]);
                        }
                        if (ds.Tables[0].Rows[i]["LV"] != DBNull.Value)
                        {
                            Data.LV = Convert.ToInt32(ds.Tables[0].Rows[i]["LV"]);
                        }
                        if (ds.Tables[0].Rows[i]["Tree"] != DBNull.Value)
                        {
                            Data.Tree = Convert.ToInt32(ds.Tables[0].Rows[i]["Tree"]);
                        }
                        if (ds.Tables[0].Rows[i]["processed"] != DBNull.Value)
                        {
                            Data.processed = Convert.ToInt32(ds.Tables[0].Rows[i]["processed"]);
                        }

                        Data.HeadImage = RetailTraderBLL.GetRetailHeadImage(Data.RetailTraderID);

                        RD.RetailerList.Add(Data);
                        #endregion

                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    #region 请求参数赋值
                    RD.RetailerCount = new RetailerCountInfo();
                    if (ds.Tables[1].Rows[0]["ToadyAddRetailNum"] != DBNull.Value)
                    {
                        RD.RetailerCount.ToadyAddRetailNum = Convert.ToInt32(ds.Tables[1].Rows[0]["ToadyAddRetailNum"]);
                    }
                    else
                    {
                        RD.RetailerCount.ToadyAddRetailNum = 0;
                    }

                    if (ds.Tables[1].Rows[0]["MonthAddRetailNum"] != DBNull.Value)
                        RD.RetailerCount.MonthAddRetailNum = Convert.ToInt32(ds.Tables[1].Rows[0]["MonthAddRetailNum"]);
                    else
                        RD.RetailerCount.MonthAddRetailNum = 0;

                    if (ds.Tables[1].Rows[0]["RetailNum"] != DBNull.Value)
                        RD.RetailerCount.RetailNum = Convert.ToInt32(ds.Tables[1].Rows[0]["RetailNum"]);
                    else
                        RD.RetailerCount.RetailNum = 0;
                    #endregion
                }
            }

            return rsp.ToJSON();
        }

        #endregion

        //修改分销商密码
        private string RetailSetPassWord(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailSetPassWordRP>>();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            string pNewPass = MD5Helper.Encryption(rp.Parameters.pNewPWD);
            //pOldPWD = MD5Helper.Encryption(pOldPWD);
            rp.Parameters.pOldPWD = EncryptManager.Hash(rp.Parameters.pOldPWD, HashProviderType.MD5);

            //组装参数
            var rd = new RetailSetPassWordRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var serviceBll = new RetailTraderBLL(loggingSessionInfo);
            var entity = serviceBll.GetByID(rp.Parameters.pID);//获取分销商实体

            //if (pOldPWD == entity.User_Password)
            if (rp.Parameters.pOldPWD == entity.RetailTraderPass)
            {

                entity.RetailTraderPass = pNewPass;
                entity.LastUpdateBy = "1";
                entity.LastUpdateTime = DateTime.Now;
                //new cUserService(CurrentUserInfo).SetUserInfo(entity, entity.userRoleInfoList, out error);
                serviceBll.Update(entity);
                rsp.Message = "";

            }
            else
            {
                rsp.ResultCode = 135;
                rsp.Message = "旧密码不正确";
            }
            return rsp.ToJSON();
        }

        #region   	今日奖励、本月奖励、累计奖励
        //本月奖励金额、发展会员奖励（17）、会员消费奖励接口(消费奖励分为14会员首次消费奖励和15关注三个月内奖励)
        public string GetRewards(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.UserOrRetailID))
            {
                throw new APIException("缺少参数【UserOrRetailID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetRewardsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //本月新增会员数量
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            var UserOrRetailID = rp.Parameters.UserOrRetailID;
            ////本月首次关注奖励
            //decimal attenAmount = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, month, year, "17");   //获取
            ////本月首次消费奖励
            //decimal firstTradeAmount = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, month, year, "14");   //获取 = MonthTradeCount;
            ////本月三月内消费奖励
            //decimal threeMonthAmount = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, month, year, "15");   //获取 = MonthTradeCount;
            //今日奖励
            decimal DayRewards = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, day, "17")
                                    + bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, day, "14")
                                    + bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, day, "15");   //获取
            //本月奖励
            decimal MonthRewards = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, -1, "17")
                                    + bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, -1, "14")
                                    + bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, year, month, -1, "15");   //获取
            //累积奖励
            decimal TotalRewards = bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, -1, -1, -1, "17")
                                    + bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, -1, -1, -1, "14")
                                    + bll.RetailRewardByAmountSource(UserOrRetailID, loggingSessionInfo.ClientID, -1, -1, -1, "15");   //获取

            rd.DayRewards = DayRewards;
            rd.MonthRewards = MonthRewards;
            rd.TotalRewards = TotalRewards;
            return rsp.ToJSON();
        }

        #endregion


        #region 	 本月每日奖励
        public string MonthDayRewards(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.UserOrRetailID))
            {
                throw new APIException("缺少参数【UserOrRetailID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new MonthDayRewardsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            // int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;//目前只取今年的
            int month = DateTime.Now.Month;//目前只取今年的
            var ds = bll.MonthDayRewards(rp.Parameters.UserOrRetailID, loggingSessionInfo.ClientID, year, month);   //获取
            var VipRiseList = new List<RewardsDayRiseTrand>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                VipRiseList = DataTableToObject.ConvertToList<RewardsDayRiseTrand>(tempDt);//直接根据所需要的字段反序列化
            }
            int currentDay = DateTime.Now.Day;
            //判断账号是否存在
            rd.MonthRewardList = new List<RewardsDayRiseTrand>();
            for (int i = 1; i <= currentDay; i++)
            {
                RewardsDayRiseTrand temp = VipRiseList.Where(p => p.Day == i).SingleOrDefault();//直接根据所需要的字段反序列化
                if (temp == null)
                {
                    temp = new RewardsDayRiseTrand();
                    temp.Year = year;
                    temp.Month = month;
                    temp.Day = i;
                    temp.formatDate = year + "-" + month + "-" + i;

                    temp.DayAmount = 0;
                    temp.DayVipAmount = 0;
                    temp.DayTradeAmount = 0;

                }
                rd.MonthRewardList.Add(temp);

            }
            rd.MonthRewardList.Reverse();//按照日期倒序
            return rsp.ToJSON();
        }

        #endregion




        #region 	 最近15天奖励趋势
        public string GetRewardsDayRiseList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.UserOrRetailID))
            {
                throw new APIException("缺少参数【UserOrRetailID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new MonthDayRewardsRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            // int month = DateTime.Now.Month;
            //int year = DateTime.Now.Year;//目前只取今年的
            //int month = DateTime.Now.Month;//目前只取今年的
            DateTime nowDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()); //
            var ds = bll.GetRewardsDayRiseList(rp.Parameters.UserOrRetailID, loggingSessionInfo.ClientID, nowDay.AddDays(-14), nowDay.AddDays(1).AddSeconds(-1));   //获取今天和前面14天的数据
            var VipRiseList = new List<RewardsDayRiseTrand>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                VipRiseList = DataTableToObject.ConvertToList<RewardsDayRiseTrand>(tempDt);//直接根据所需要的字段反序列化
            }
            int currentDay = DateTime.Now.Day;
            //判断账号是否存在
            rd.MonthRewardList = new List<RewardsDayRiseTrand>();
            for (DateTime i = nowDay.AddDays(-14); i <= nowDay; i = i.AddDays(1))
            {
                RewardsDayRiseTrand temp = VipRiseList.Where(p => p.formatDate == i.ToString("yyyy-MM-dd")).SingleOrDefault();//直接根据所需要的字段反序列化
                if (temp == null)
                {
                    temp = new RewardsDayRiseTrand();
                    temp.Year = i.Year;
                    temp.Month = i.Month;
                    temp.Day = i.Day;
                    temp.formatDate = temp.Year + "-" + temp.Month + "-" + temp.Day;

                    temp.DayAmount = 0;
                    temp.DayVipAmount = 0;
                    temp.DayTradeAmount = 0;

                }
                temp.myDate = temp.Month + "/" + temp.Day;
                rd.MonthRewardList.Add(temp);

            }
            return rsp.ToJSON();
        }

        #endregion



        #region 	 最近15天新增会员数趋势,此接口只能用于分销商
        public string GetVipDayRiseList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SellUserMainAchieveRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetVipDayRiseListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取分销商的信息，loggingSessionInfo.ClientID
            // int month = DateTime.Now.Month;
            //int year = DateTime.Now.Year;//目前只取今年的
            //int month = DateTime.Now.Month;//目前只取今年的
            DateTime nowDay = Convert.ToDateTime(DateTime.Now.ToShortDateString()); //RetailTraderID
            var ds = bll.GetVipDayRiseList(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, nowDay.AddDays(-14), nowDay.AddDays(1).AddSeconds(-1));   //获取今天和前面14天的数据
            var VipRiseList = new List<VipDayRiseTrand>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                VipRiseList = DataTableToObject.ConvertToList<VipDayRiseTrand>(tempDt);//直接根据所需要的字段反序列化
            }
            int currentDay = DateTime.Now.Day;
            //判断账号是否存在
            rd.VipDayRiseList = new List<VipDayRiseTrand>();
            for (DateTime i = nowDay.AddDays(-14); i <= nowDay; i = i.AddDays(1))
            {
                VipDayRiseTrand temp = VipRiseList.Where(p => p.formatDate == i.ToString("yyyy-MM-dd")).SingleOrDefault();//直接根据所需要的字段反序列化
                if (temp == null)
                {
                    temp = new VipDayRiseTrand();
                    temp.Year = i.Year;
                    temp.Month = i.Month;
                    temp.Day = i.Day;
                    temp.formatDate = temp.Year + "-" + temp.Month + "-" + temp.Day;

                    temp.vipCount = 0;

                }
                temp.myDate = temp.Month + "/" + temp.Day;
                rd.VipDayRiseList.Add(temp);

            }
            return rsp.ToJSON();
        }

        #endregion



        #region  分销商今日新增会员数、本月新增会员数、总会员数
        public string GetRetailVipInfos(string pRequest)//	获取本月新增会员数量、累计会员数量、本月交易量接口(取该会员的订单数)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetRetailVipInfosRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }



            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new GetRetailVipInfosRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            //今日新增会员数量           
            var ds = bll.GetRetailVipInfos(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, year, month, day);   //获取       
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds.Tables[0];
                rd.DayVipCount = tempDt.Rows.Count;
            }
            //本月新增会员数量
            var ds2 = bll.GetRetailVipInfos(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, year, month, -1);   //获取        
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds2.Tables[0];
                rd.MonthVipCount = tempDt.Rows.Count;
            }
            //累计会员数量
            var ds3 = bll.GetRetailVipInfos(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, -1, -1, -1);   //获取        
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds3.Tables[0];
                rd.TotalVipCount = tempDt.Rows.Count;
            }
            return rsp.ToJSON();
        }

        #endregion



        #region   兰博士为我发券数量、核销数量、我为兰博士带多少会员、奖励多少钱
        public string RetailTraderMain(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetRetailVipInfosRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new RetailTraderMainRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            //  BringVipCount//我为兰博士带多少会员
            //  RewardAmount//奖励多少钱
            //累计会员数量
            var ds3 = bll.GetRetailVipInfos(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, -1, -1, -1);   //获取        
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds3.Tables[0];
                rd.BringVipCount = tempDt.Rows.Count;
            }
            //累积奖励
            decimal TotalRewards = bll.RetailRewardByAmountSource(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, -1, -1, -1, "17")
                                    + bll.RetailRewardByAmountSource(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, -1, -1, -1, "14")
                                    + bll.RetailRewardByAmountSource(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, -1, -1, -1, "15");   //获取
            rd.RewardAmount = TotalRewards;

            int? pageIndex = 1;
            int? pageSize = 10000;
            //未核销优惠券列表
            int NoWriteOffCouponCount = 0;
            var ds4 = bll.GetRetailCoupon(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, 0, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取        
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds4.Tables[0];
               NoWriteOffCouponCount = tempDt.Rows.Count;
                //  rd.WriteOffCouponList = DataTableToObject.ConvertToList<RetailCouponInfo>(ds4.Tables[1]);
            }

            var ds5 = bll.GetRetailCoupon(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, 1, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取        
            if (ds5 != null && ds5.Tables.Count > 0 && ds5.Tables[0].Rows.Count > 0)
            {
                var tempDt = ds5.Tables[0];
                rd.UsedCoupon = tempDt.Rows.Count;
                //  rd.WriteOffCouponList = DataTableToObject.ConvertToList<RetailCouponInfo>(ds3.Tables[1]);
            }


            //    rd.AllCouponCouponCount = rd.WriteOffCouponCount + rd.NoWriteOffCouponCount;


            rd.SendCouponCount = rd.UsedCoupon + NoWriteOffCouponCount;//	兰博士为我发券数量



            return rsp.ToJSON();
        }

        #endregion



        #region    优惠券已核销人数列表、未核销人数列表、总数
        public string RetailCoupon(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetRetailVipInfosRP>>();
            if (string.IsNullOrEmpty(rp.Parameters.RetailTraderID))
            {
                throw new APIException("缺少参数【RetailTraderID】或参数值为空") { ErrorCode = 135 };
            }
            var pageSize = rp.Parameters.PageSize;
            var pageIndex = rp.Parameters.PageIndex;

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var bll = new RetailTraderBLL(loggingSessionInfo);
            var rd = new RetailCouponRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            //优惠券状态为1代表已经使用，为0
            //已核销优惠券列表
            var ds3 = bll.GetRetailCoupon(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, 1, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取        
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                rd.WriteOffCouponCount = Convert.ToInt32(ds3.Tables[0].Rows[0]["TotalCount"].ToString());
                rd.WriteOffCouponList = DataTableToObject.ConvertToList<RetailCouponInfo>(ds3.Tables[1]);
            }



            //未核销优惠券列表
            var ds4 = bll.GetRetailCoupon(rp.Parameters.RetailTraderID, loggingSessionInfo.ClientID, 0, pageIndex ?? 1, pageSize ?? 15, rp.Parameters.OrderBy, rp.Parameters.OrderType);   //获取        
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                rd.NoWriteOffCouponCount = Convert.ToInt32(ds4.Tables[0].Rows[0]["TotalCount"].ToString());
                rd.NoWriteOffCouponList = DataTableToObject.ConvertToList<RetailCouponInfo>(ds4.Tables[1]);
            }

            rd.AllCouponCouponCount = rd.WriteOffCouponCount + rd.NoWriteOffCouponCount;

            return rsp.ToJSON();
        }

        #endregion


        #region 用户接受协议成为分经销商
        /// <summary>
        /// 用户接受协议成为分经销商
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SetPreRetailTraderToNomal(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<RetailTraderRP>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var RetailTraderBLL = new RetailTraderBLL(loggingSessionInfo);
            var VipBLL = new VipBLL(loggingSessionInfo);

            var Data = RetailTraderBLL.GetByID(rp.UserID);
            if (Data == null)
            {
                throw new APIException("参数【rp.Parameters.RetailTraderID】错误找不到对应经销商") { ErrorCode = 136 };
            }
            var vipData = VipBLL.GetByID(Data.MultiLevelSalerFromVipId);
            if (vipData != null)
            {


                if (!string.IsNullOrWhiteSpace(vipData.SetoffUserId))//经销商二维码扫码关注的会员，成为经销商时,自动被集客
                {
                    DataSet ds = RetailTraderBLL.GetMultiLevelBeAddNode(vipData.SetoffUserId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Data.HigheRetailTraderID = ds.Tables[0].Rows[0]["BeAddNode"].ToString();
                        if (ds.Tables[0].Rows[0]["BeAddNodeFlag"] != DBNull.Value)
                        {
                            int Flag = Convert.ToInt32(ds.Tables[0].Rows[0]["BeAddNodeFlag"]);
                            if (Flag == 1)//添加经销商拓展节点记录
                            {
                                var T_HierarchySystemExNodeBLL = new T_HierarchySystemExNodeBLL(loggingSessionInfo);
                                T_HierarchySystemExNodeBLL.AddHierarchySystemExNode(Data.RetailTraderID);
                            }
                        }

                    }

                }

            }
            Data.Status = "1";

            RetailTraderBLL.Update(Data);
            var response = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(response);
            return rsp.ToJSON();
        }

        #endregion

        /// <summary>
        /// 根据CustomerId获取成为经销商条件
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetVipMultiLevelSalerConfig(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            if (string.IsNullOrEmpty(rp.UserID))
            {
                throw new APIException("缺少参数【UserID】或参数值为空") { ErrorCode = 135 };
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var RetailTraderBLL = new RetailTraderBLL(loggingSessionInfo);
            var ds = RetailTraderBLL.GetVMultiLevelSalerConfigByCId(rp.CustomerID);

            var rd = new VipMultiLevelSalerConfigRP();
            rd.config = new VipMultiLevelSalerConfigEntity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                rd.config.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                rd.config.MustBuyAmount = ds.Tables[0].Rows[0]["MustBuyAmount"].ToString();
                rd.config.Agreement = ds.Tables[0].Rows[0]["Agreement"].ToString();
                rd.config.CustomerId = ds.Tables[0].Rows[0]["CustomerId"].ToString();
            }
            var rsp = new SuccessResponse<VipMultiLevelSalerConfigRP>(rd);
            return rsp.ToJSON();
        }
    }


    public class GetRewardsRD : IAPIResponseData
    {
        public decimal DayRewards { get; set; }
        public decimal MonthRewards { get; set; }
        public decimal TotalRewards { get; set; }
    }


    public class MonthDayRewardsRD : IAPIResponseData
    {
        public List<RewardsDayRiseTrand> MonthRewardList { get; set; }
    }
    public class GetVipDayRiseListRD : IAPIResponseData
    {
        public List<VipDayRiseTrand> VipDayRiseList { get; set; }
    }

    public class EmptyRD : IAPIResponseData
    {

    }

    public class RewardsDayRiseTrand
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string formatDate { get; set; }
        /// <summary>
        /// 引流
        /// </summary>
        public decimal DayAmount { get; set; }
        public decimal DayVipAmount { get; set; }
        public decimal DayTradeAmount { get; set; }
        /// <summary>
        /// 销售
        /// </summary>
        public decimal DaySalesAmount { get; set; }
        public string myDate { get; set; }
    }

    public class VipDayRiseTrand
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string formatDate { get; set; }
        public int vipCount { get; set; }

        public string myDate { get; set; }
    }



    public class GetRetailVipInfosRP : IAPIRequestParameter
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
        public string RetailTraderID { get; set; }
        public void Validate()
        {
        }
    }

    public class GetRetailVipInfosRD : IAPIResponseData
    {
        public int DayVipCount { get; set; }
        public int MonthVipCount { get; set; }
        public int TotalVipCount { get; set; }

    }




    public class RetailTraderMainRD : IAPIResponseData
    {
        public int SendCouponCount { get; set; }//发券数量
        public int UsedCoupon { get; set; }//核销数量（使用过优惠券数量）
        public int BringVipCount { get; set; }//我为兰博士带多少会员
        public decimal RewardAmount { get; set; }//奖励多少钱


    }

    public class MultiLevelSalerQueryRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class MultiLevelSalerQueryRD : IAPIResponseData
    {
        public List<Retailer> RetailerList { get; set; }
        public RetailerCountInfo RetailerCount { get; set; }
    }

    public class Retailer
    {
        public string RetailTraderID { get; set; }

        public string HigheRetailTraderID { get; set; }
        public string RetailTraderName { get; set; }
        public string Parentname { get; set; }
        public string Hierarchy { get; set; }
        public string CreateTime { get; set; }
        public int Idx { get; set; }
        public int LV { get; set; }
        public int Tree { get; set; }
        public int processed { get; set; }

        public string HeadImage { get; set; }
    }

    public class RetailerCountInfo
    {
        /// <summary>
        /// 当天集客
        /// </summary>
        public int ToadyAddRetailNum { get; set; }
        /// <summary>
        /// 当月集客
        /// </summary>
        public int MonthAddRetailNum { get; set; }
        /// <summary>
        /// 总集客
        /// </summary>
        public int RetailNum { get; set; }
    }
    public class RetailSetPassWordRP : IAPIRequestParameter
    {
        public string pID { get; set; }
        public string pOldPWD { get; set; }
        public string pNewPWD { get; set; }


        public void Validate()
        {
        }
    }


    public class RetailSetPassWordRD : IAPIResponseData
    {


    }



    public class RetailCouponRD : IAPIResponseData
    {

        public int AllCouponCouponCount { get; set; }//优惠券总数量
        public int WriteOffCouponCount { get; set; }//已经核销数量
        public int NoWriteOffCouponCount { get; set; }//未核销数量
        public List<RetailCouponInfo> WriteOffCouponList { get; set; }//已核销优惠券列表
        public List<RetailCouponInfo> NoWriteOffCouponList { get; set; }//未核销优惠券列表


    }

    public class RetailCouponInfo : IAPIResponseData
    {

        public string HeadImgUrl { get; set; }//优惠券总数量
        public string VipName { get; set; }//已经核销数量
        public string VipRealName { get; set; }//未核销数量
        public string CoupnName { get; set; }//已核销优惠券列表
        public string CouponCode { get; set; }//未核销优惠券列表
        public string UseTime { get; set; }//未核销优惠券列表
        public string StatusDesc { get; set; }//未核销优惠券列表








    }
}