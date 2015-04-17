﻿using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.VipList.Request;
using JIT.CPOS.DTO.Module.VIP.VipList.Response;
using JIT.Utility.ExtensionMethod;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;
/********************************************************************************

    * 创建时间: 2014-9-25 11:29:40
    * 作    者：donal
    * 说    明：人人销售
    * 修改时间：2014-9-25 11:30:02
    * 修 改 人：donal

*********************************************************************************/

namespace JIT.CPOS.Web.ApplicationInterface.EveryoneSale
{
    /// <summary>
    /// 人人销售接口EveryoneGetway
    /// </summary>
    public class EveryoneGetway : BaseGateway
    {
        #region 错误码
        private const int ERROR_USERID_NOTNULL=801;        //USerID不能为空
        private const int ERROR_WDAMOUNT_TOOBIG = 802;     //日累计提现金额等能大于设置金额
        private const int ERROR_WDAMOUNT_NOTWDTIME = 803;  //超出提现次数限制
        #endregion
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetEveryOneAmount":
                    rst = GetEveryOneAmount(pRequest);
                    break;
                case "GetOrderAmount":
                    rst = GetOrderAmount(pRequest);
                    break;
                case "GetVipCount":
                    rst = GetVipCount(pRequest);
                    break;
                case "GetVipAccount":
                    rst = GetVipAccount(pRequest);
                    break;
                case "GetAmountDetail":
                    rst = GetAmountDetail(pRequest);
                    break;
                case "GetWDBasicInfo":  //获取提现基本信息
                    rst = GetWDBasicInfo(pRequest);
                    break;
                case "GetBank":         //获取支持提现的银行
                    rst = GetBank(pRequest);
                    break;
                case "GetVipBank":      //获取我的银行卡
                    rst = GetVipBank(pRequest);
                    break;
                case "UpdateVipBank":   //增加/修改银行卡
                    rst = UpdateVipBank(pRequest);
                    break;
                case "ApplyWithdrawDeposit": //提现申请
                    rst = ApplyWithdrawDeposit(pRequest);
                    break;
                case "GetWithdrawDeposit"://提现申请记录查询
                    rst = GetWithdrawDeposit(pRequest);
                    break;
                case "GetMyVipList"://获取我的会员列表
                    rst = GetMyVipList(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }

        #region 方法

        /// <summary>
        /// 店员账户统计
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetEveryOneAmount(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var everyoneBll = new EveryoneSalesBLL(loggingSessionInfo);
            DataSet dt = everyoneBll.GetEveryOneAmount(rp.ChannelId, rp.UserID, rp.CustomerID);
            var rd = DataTableToObject.ConvertToObject<EveryOneAmountRD>(dt.Tables[0].Rows[0]);

            //查询订单信息
            List<GroupingOrderCount> ListGrouping = new List<GroupingOrderCount>();
            var GroupingOrderCounts = new T_InoutBLL(loggingSessionInfo).GetOrder(rp.UserID, 0, 0, rp.CustomerID, 0, rp.ChannelId, rp.UserID).GroupingOrderCounts;
            foreach (var item in GroupingOrderCounts)
            {
                ListGrouping.Add(
                    new GroupingOrderCount()
                    {
                        GroupingType = item.GroupingType,
                        OrderCount = item.OrderCount
                    }
                    );
            }
            rd.GroupingOrderCounts = ListGrouping.ToArray();

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 订单月统计列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetOrderAmount(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetOrderAmountRP>>();

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var everyoneBll = new EveryoneSalesBLL(loggingSessionInfo);

            //查询结果
            DataSet dt = everyoneBll.GetOrderAmount(rp.CustomerID, rp.UserID, rp.Parameters.PageSize, rp.Parameters.PageIndex, rp.ChannelId);

            //把查询的数据转化为返回的数据
            OrderMonth OrderMonth = null;
            List<OrderMonth> orderMothList = new List<OrderMonth>();
            int TotalPage = 0;
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    OrderMonth = new OrderMonth();
                    OrderMonth.YearID = int.Parse(dt.Tables[0].Rows[i]["YearID"].ToString());
                    OrderMonth.MonthID = int.Parse(dt.Tables[0].Rows[i]["MonthID"].ToString());
                    OrderMonth.OrderCount = int.Parse(dt.Tables[0].Rows[i]["OrderCount"].ToString());
                    OrderMonth.OrderAmount = decimal.Parse(dt.Tables[0].Rows[i]["OrderAmount"].ToString());
                    OrderMonth.OrderMonthIncome = decimal.Parse(dt.Tables[0].Rows[i]["Income"].ToString());
                    orderMothList.Add(OrderMonth);

                    if (TotalPage == 0)
                        TotalPage = int.Parse(dt.Tables[0].Rows[i]["TotalPage"].ToString());
                }
            }

            //返回参数
            var rd = new GetOrderAmountRD();
            rd.OrderMonthList = orderMothList.ToArray();
            rd.TotalPage = TotalPage;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }


        /// <summary>
        /// 集客榜
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetVipCount(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipCountRP>>();

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var everyoneBll = new EveryoneSalesBLL(loggingSessionInfo);

            //查询结果
            DataSet dt = everyoneBll.GetVipCount(rp.CustomerID, rp.Parameters.PageSize, rp.Parameters.PageIndex, rp.ChannelId);

            //把查询的数据转化为返回的数据
            UserInfo userinfo = null;
            List<UserInfo> UserInfoList = new List<UserInfo>();
            int TotalPage = 0;
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    userinfo = new UserInfo();
                    userinfo.OrdinaID = int.Parse(dt.Tables[0].Rows[i]["Ordinal"].ToString());
                    userinfo.VipName = dt.Tables[0].Rows[i]["VipName"].ToString();
                    userinfo.GetVipCount = int.Parse(dt.Tables[0].Rows[i]["GetVipCount"].ToString());
                    UserInfoList.Add(userinfo);

                    if (TotalPage == 0)
                        TotalPage = int.Parse(dt.Tables[0].Rows[i]["TotalPage"].ToString());
                }
            }

            //返回参数
            var rd = new GetVipCountRD();
            rd.UserList = UserInfoList.ToArray();
            rd.TotalPage = TotalPage;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 收入榜
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetVipAccount(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipAccountRP>>();

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var everyoneBll = new EveryoneSalesBLL(loggingSessionInfo);

            //查询结果
            DataSet dt = everyoneBll.GetVipAccount(rp.CustomerID, rp.Parameters.PageSize, rp.Parameters.PageIndex, rp.ChannelId);

            //把查询的数据转化为返回的数据
            UserAccountInfo useraccountinfo = null;
            List<UserAccountInfo> UserAccountList = new List<UserAccountInfo>();
            int TotalPage = 0;
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    useraccountinfo = new UserAccountInfo();
                    useraccountinfo.OrdinaID = int.Parse(dt.Tables[0].Rows[i]["Ordinal"].ToString());
                    useraccountinfo.VipName = dt.Tables[0].Rows[i]["VipName"].ToString();
                    useraccountinfo.GetVipAmount = decimal.Parse(dt.Tables[0].Rows[i]["GetVipCount"].ToString());
                    UserAccountList.Add(useraccountinfo);

                    if (TotalPage == 0)
                        TotalPage = int.Parse(dt.Tables[0].Rows[i]["TotalPage"].ToString());
                }
            }

            //返回参数
            var rd = new GetVipAccountRD();
            rd.UserAccountList = UserAccountList.ToArray();
            rd.TotalPage = TotalPage;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 月收入统计列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetAmountDetail(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetAmountDetailRP>>();

            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            var everyoneBll = new EveryoneSalesBLL(loggingSessionInfo);

            //查询结果
            DataSet dt = everyoneBll.GetAmountDetail(rp.CustomerID, rp.UserID, rp.Parameters.PageSize, rp.Parameters.PageIndex, rp.ChannelId);

            //把查询的数据转化为返回的数据
            AmountMonth amountmonth = null;
            List<AmountMonth> AmountMonthList = new List<AmountMonth>();
            int TotalPage = 0;
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    amountmonth = new AmountMonth();
                    amountmonth.YearID = int.Parse(dt.Tables[0].Rows[i]["YearID"].ToString());
                    amountmonth.MonthID = int.Parse(dt.Tables[0].Rows[i]["MonthID"].ToString());
                    amountmonth.GetVipAmount = decimal.Parse(dt.Tables[0].Rows[i]["GetVipAmount"].ToString());
                    amountmonth.OrderAmount = decimal.Parse(dt.Tables[0].Rows[i]["OrderAmount"].ToString());
                    amountmonth.TotalAmount = decimal.Parse(dt.Tables[0].Rows[i]["TotalAmount"].ToString());
                    AmountMonthList.Add(amountmonth);

                    if (TotalPage == 0)
                        TotalPage = int.Parse(dt.Tables[0].Rows[i]["TotalPage"].ToString());
                }
            }

            //返回参数
            var rd = new GetAmountDetailRD();
            rd.AmountMonthList = AmountMonthList.ToArray();
            rd.TotalPage = TotalPage;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取提现基本信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetWDBasicInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var basicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);  //客户基本信息BLL实例化
            var vipBankBll = new VipBankBLL(loggingSessionInfo);                    //客户银行卡信息BLL实例化
            var vipWDBll = new VipWithdrawDepositBLL(loggingSessionInfo);           //提现信息表BLL实例化
            var bankBll = new BankBLL(loggingSessionInfo);                          //支持提现银行BLL实例化
            var wdApplyBll = new VipWithdrawDepositApplyBLL(loggingSessionInfo);    //提现申请BLL实例化
            var wdBasicInfo = new WDBasiciInfoRD();

            if (string.IsNullOrEmpty(rp.UserID))
            {
                var rst = new SuccessResponse<EmptyRD>();
                rst.ResultCode = ERROR_USERID_NOTNULL;
                rst.Message = "UserID不能传空值";
                return rst.ToJSON();
            }
            //今天提现次数统计
            var wdApplyToday = wdApplyBll.GetVipWDApplyByToday(rp.UserID);

            //每天可提现次数
            string wdTime = basicSettingBll.GetSettingValueByCode("WithdrawDepositTime");
            wdBasicInfo.WDTime = string.IsNullOrEmpty(wdTime) == true ? 1 - wdApplyToday.Length : int.Parse(wdTime) - wdApplyToday.Length;

            //日累计提现金额
            var wdAmountInfo = basicSettingBll.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "WithdrawDepositAmount", CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
            if (wdAmountInfo != null)
            {
                wdBasicInfo.WDAmountDay = decimal.Parse(wdAmountInfo.SettingValue);
                wdBasicInfo.WDAmountDayDesc = wdAmountInfo.SettingDesc;
            }
            //获取会员银行卡信息
            var vipBankInfo = vipBankBll.QueryByEntity(new VipBankEntity() { VipID = rp.UserID }, null).FirstOrDefault();
            if (vipBankInfo != null)
            {
                wdBasicInfo.VipBankID = vipBankInfo.VipBankID;
                wdBasicInfo.CardNo = vipBankInfo.CardNo;
                wdBasicInfo.AccountName = vipBankInfo.AccountName;
                var bankInfo = bankBll.GetByID(vipBankInfo.BankID);
                if (bankInfo != null)
                {
                    wdBasicInfo.BankName = bankInfo.BankName;
                    wdBasicInfo.LogoUrl = bankInfo.LogoUrl;
                }
            }
            //获取可提现金额
            var vipWD = vipWDBll.QueryByEntity(new VipWithdrawDepositEntity() { VIPID = rp.UserID }, null).FirstOrDefault();
            if (vipWD != null)
            {
                wdBasicInfo.WDCurrentAmount = vipWD.EndAmount;
            }
            var rsp = new SuccessResponse<IAPIResponseData>(wdBasicInfo);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取支持提现的银行
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetBank(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var bankBll = new BankBLL(loggingSessionInfo);  //支持提现银行BLL实例化
            var bankList = bankBll.GetAll();                 //获取所有支持提现银行

            var rd = new BankListRD();
            if (bankList != null && bankList.Count() > 0)
                rd.BankList = bankList.Select(t => new BankInfo() { BankID = t.BankID, BankName = t.BankName, LogoUrl = t.LogoUrl }).ToArray();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取我的银行卡
        /// </summary>
        private string GetVipBank(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var vipBankBll = new VipBankBLL(loggingSessionInfo);   //银行卡BLL实例化
            DataSet dsVipBank = vipBankBll.GetVipBankList(rp.UserID);//获取我的银行卡列表
            var rd = new VipBankListRD();
            rd.VipBankList = DataTableToObject.ConvertToList<VipBankInfo>(dsVipBank.Tables[0]);
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 增加/修改银行卡
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string UpdateVipBank(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<UpdateVipBankRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var vipBankBll = new VipBankBLL(loggingSessionInfo);   //银行卡BLL实例化
            if (string.IsNullOrEmpty(rp.Parameters.VipBankID.ToString()))//增加
            {
                var vipBankEntity = new VipBankEntity()
                {
                    VipID = rp.UserID,
                    BankID = rp.Parameters.BankID,
                    AccountName = rp.Parameters.AccountName,
                    CardNo = rp.Parameters.CardNo,
                    CustomerID = rp.CustomerID
                };
                vipBankBll.Create(vipBankEntity);
            }
            else//修改
            {
                var vipBankEntity = vipBankBll.GetByID(rp.Parameters.VipBankID);
                if (vipBankEntity != null)
                {
                    var vipbank = vipBankBll.QueryByEntity(new VipBankEntity() { BankID = rp.Parameters.BankID, CardNo = rp.Parameters.CardNo, AccountName = rp.Parameters.AccountName }, null).FirstOrDefault();
                    if (vipbank == null)
                    {
                        vipBankBll.Delete(vipBankEntity);//逻辑删除，然后新增

                        if (!string.IsNullOrEmpty(rp.Parameters.BankID.ToString()))
                            vipBankEntity.BankID = rp.Parameters.BankID;
                        if (!string.IsNullOrEmpty(rp.Parameters.AccountName))
                            vipBankEntity.AccountName = rp.Parameters.AccountName;
                        if (!string.IsNullOrEmpty(rp.Parameters.CardNo))
                            vipBankEntity.CardNo = rp.Parameters.CardNo;
                        vipBankEntity.VipBankID = null;
                        vipBankBll.Create(vipBankEntity);//新增
                    }
                }
            }
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            rsp.ResultCode = 0;
            return rsp.ToJSON();
        }
        /// <summary>
        /// 提现申请
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string ApplyWithdrawDeposit(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<WDApplyRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            var vipWDBll = new VipWithdrawDepositBLL(loggingSessionInfo);              //提现表Bll实例化
            var wdApplyBll = new VipWithdrawDepositApplyBLL(loggingSessionInfo);    //提现申请BLL实例化
            var unitExpandBll = new TUnitExpandBLL(loggingSessionInfo);             //获取提现申请单号BLL实例化
            var basicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);  //客户基本信息BLL实例化

            int wdTimeSettings = 1;             //每天可提现次数
            decimal wdAmountSettings = 1000;    //日累计提现金额

            string wdTimeTemp = basicSettingBll.GetSettingValueByCode("WithdrawDepositTime");       //配置的日提现次数
            string wdAmountTemp = basicSettingBll.GetSettingValueByCode("WithdrawDepositAmount");   //配置的日累计提现金额

            if (!string.IsNullOrEmpty(wdTimeTemp))
                wdTimeSettings = int.Parse(wdTimeTemp);
            if (!string.IsNullOrEmpty(wdAmountTemp))
                wdAmountSettings = decimal.Parse(wdAmountTemp);

            //判断日累计提现总额是否超过限制
            if (wdAmountSettings < rp.Parameters.Amount)
            {
                rsp.ResultCode = ERROR_WDAMOUNT_TOOBIG;
                rsp.Message = "日累计提现金额等能大于" + wdAmountSettings;
                return rsp.ToJSON();
            }
            //判断日提现次数和日累计提现总额是否超过限制
            var wdApplyToday = wdApplyBll.GetVipWDApplyByToday(rp.UserID);
            if (wdApplyToday.Length > 0)
            {
                if (wdTimeSettings <= wdApplyToday.Length)
                {
                    rsp.ResultCode = ERROR_WDAMOUNT_NOTWDTIME;
                    rsp.Message = "今天已不能提现";
                    return rsp.ToJSON();
                }
                decimal? totalAmount = 0; //今日提现累计金额
                foreach (var item in wdApplyToday)
                {
                    totalAmount += item.Amount;
                }
                if (totalAmount > wdAmountSettings)
                {
                    rsp.ResultCode = ERROR_WDAMOUNT_TOOBIG;
                    rsp.Message = "日累计提现金额不能大于" + wdAmountSettings;
                    return rsp.ToJSON();
                }
            }
            var pTran = wdApplyBll.GetTran();    //事务
            using (pTran.Connection)
            {
                try
                {
                    //提现主表去除提现
                    var vipWDInfo = vipWDBll.QueryByEntity(new VipWithdrawDepositEntity() { VIPID = rp.UserID }, null).FirstOrDefault();
                    if (vipWDInfo.EndAmount < rp.Parameters.Amount)
                    {
                        rsp.ResultCode = ERROR_WDAMOUNT_TOOBIG;
                        rsp.Message = "提现金额不能大于可提现金额";
                        return rsp.ToJSON();
                    }
                    vipWDInfo.OutAmount = vipWDInfo.OutAmount + rp.Parameters.Amount;   //期中提现金额
                    vipWDInfo.EndAmount = vipWDInfo.EndAmount - rp.Parameters.Amount;    //期末金额修改
                    vipWDBll.Update(vipWDInfo,pTran);//修改

                    //提现申请保存
                    string withdrawNo = unitExpandBll.GetUnitOrderNo(loggingSessionInfo, rp.CustomerID);
                    VipWithdrawDepositApplyEntity entity = new VipWithdrawDepositApplyEntity
                    {
                        VipID = rp.UserID,
                        WithdrawNo = DateTime.Now.ToString("yyyyMMddhhmmss") + withdrawNo,
                        Amount = rp.Parameters.Amount,
                        Status = 0,
                        ApplyDate = DateTime.Now,
                        VipBankID = rp.Parameters.VipBankID,
                        CustomerID = rp.CustomerID
                    };
                    wdApplyBll.Create(entity,pTran);
                    pTran.Commit();//提交事物
                }
                catch (Exception ex)
                {
                    pTran.Rollback();
                    throw new APIException(ex.Message);
                }
            }
            rsp.ResultCode = 0;
            return rsp.ToJSON();
        }
        /// <summary>
        /// 提现申请记录
        /// </summary>
        /// <param name="rRequest"></param>
        /// <returns></returns>
        private string GetWithdrawDeposit(string pRequest)
        {

            var rp = pRequest.DeserializeJSONTo<APIRequest<GetWithdrawDepositRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            var wdApplyBll = new VipWithdrawDepositApplyBLL(loggingSessionInfo);  //提现申请记录BLL实例化

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = rp.UserID });
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "ApplyDate", Direction = OrderByDirections.Desc });
            //分页查询
            var tempList = wdApplyBll.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), rp.Parameters.PageSize, rp.Parameters.PageIndex + 1);

            var rd = new WithdrawDepositListRD();
            if (tempList.Entities.Length > 0)
            {
                rd.WithdrawDepositList = tempList.Entities.Select(t => new WithdrawDepositInfo() { WithdrawNo = t.WithdrawNo, ApplyDate = t.ApplyDate.Value.ToString("yyyy-MM-dd"), Amount = t.Amount, BankName = t.BankName, Status = t.Status }).ToArray();
                rd.TotalPageCount = tempList.PageCount;
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 获取我的会员列表
        /// </summary>
        /// <param name="rRequest"></param>
        /// <returns></returns>
        private string GetMyVipList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetMyVipListRP>>();
            rp.Parameters.Validate();//验证传值
            var rd = new GetMyVipListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            var vipBll = new VipBLL(loggingSessionInfo);                      //会员BLL实例化
            VipEntity vipEntity = null;

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "SetoffUserId", Value = rp.UserID });//会员上线ID
            switch (rp.Parameters.Status)
            {
                case "0":
                    complexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = 0 });
                    break;
                case "1":
                    complexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = 1 });
                    break;
                case "2"://注册会员
                    complexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = 2 });
                    break;
            }

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            //默认根据创建时间倒序
            if (string.IsNullOrEmpty(rp.Parameters.OrderBy))
                lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            else
                lstOrder.Add(new OrderBy() { FieldName = rp.Parameters.OrderBy, Direction = OrderByDirections.Desc });

            if (rp.Parameters.PageSize == 0)
            {
                rp.Parameters.PageSize = 15; //如未提供此参数，设置一个默认值
            }
            //会员列表
            var tempVipList = vipBll.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), rp.Parameters.PageSize, rp.Parameters.PageIndex + 1);

            List<IWhereCondition> allComplexCondition = new List<IWhereCondition> { };
            List<IWhereCondition> registeredComplexCondition = new List<IWhereCondition> { };
            List<IWhereCondition> latentComplexCondition = new List<IWhereCondition> { };
            List<IWhereCondition> disabledComplexCondition = new List<IWhereCondition> { };

            allComplexCondition.Add(new EqualsCondition() { FieldName = "SetoffUserId", Value = rp.UserID });//会员上线ID

            registeredComplexCondition.Add(new EqualsCondition() { FieldName = "SetoffUserId", Value = rp.UserID });//会员上线ID
            registeredComplexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = 2 });//注册会员状态

            latentComplexCondition.Add(new EqualsCondition() { FieldName = "SetoffUserId", Value = rp.UserID });//会员上线ID
            latentComplexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = 1 });//潜在会员状态

            disabledComplexCondition.Add(new EqualsCondition() { FieldName = "SetoffUserId", Value = rp.UserID });//会员上线ID
            disabledComplexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = 0 });//停用会员状态

            rd.MyVipCount = vipBll.Query(allComplexCondition.ToArray(), lstOrder.ToArray()).Length;  //注册会员行数
            rd.Registered = vipBll.Query(registeredComplexCondition.ToArray(), lstOrder.ToArray()).Length;  //注册会员行数
            rd.Latent = vipBll.Query(latentComplexCondition.ToArray(), lstOrder.ToArray()).Length;    //潜在会员行数
            rd.Disabled = vipBll.Query(disabledComplexCondition.ToArray(), lstOrder.ToArray()).Length;    //停用会员行数

            #region 排名
            var everyoneBll = new EveryoneSalesBLL(loggingSessionInfo);
            DataSet dt = everyoneBll.GetRankingByUserID(rp.CustomerID, rp.UserID, "6");
            if (dt.Tables[0].Rows.Count > 0)
                rd.Ranking = Convert.ToInt32(dt.Tables[0].Rows[0]["Ordinal"]);

            #endregion
            #region 响应数据
            rd.TotalCount = tempVipList.RowCount;
            rd.TotalPageCount = tempVipList.PageCount;
            //会员列表
            rd.MyVipList = tempVipList.Entities.Select(t => new MyVipInfo()
            {
                VipID = t.VIPID,
                VipName = string.IsNullOrEmpty(t.VipRealName) ? t.VipName : t.VipRealName,
                VipPhoto = t.HeadImgUrl,
                Phone = t.Phone,
                Birthday = string.IsNullOrEmpty(t.Birthday) ? "" : t.Birthday,
                City = (string.IsNullOrEmpty(t.City) && t.City == "0") ? "" : t.City,
                CreateTime = t.CreateTime.ToString(),
                Status = t.Status
            }).ToArray();
            #endregion
            return rsp.ToJSON();

        }

        #endregion
    }
    #region 请求/返回参数

    /// <summary>
    /// 获取店员账户统计返回内容
    /// </summary>
    public class EveryOneAmountRD : IAPIResponseData
    {
        /// <summary>
        /// 我的订单数量
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 本月订单数量
        /// </summary>
        public int OrderMonthCount { get; set; }
        /// <summary>
        /// 我的收入
        /// </summary>
        public decimal Account { get; set; }
        /// <summary>
        /// 本月收入
        /// </summary>
        public decimal MonthAccount { get; set; }
        //订单信息
        public GroupingOrderCount[] GroupingOrderCounts { get; set; }
    }

    public class GroupingOrderCount
    {
        /// <summary>
        /// 分组方式。1=待付款;2=待收货/提货;3=已完成
        /// </summary>
        public int GroupingType { get; set; }
        /// <summary>
        /// 分组内订单数量
        /// </summary>
        public int OrderCount { get; set; }

    }

    /// <summary>
    /// 订单月统计列表请求参数
    /// </summary>
    public class GetOrderAmountRP : IAPIRequestParameter
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public void Validate()
        {
        }
    }

    /// <summary>
    /// 订单月统计列表返回内容
    /// </summary>
    public class GetOrderAmountRD : IAPIResponseData
    {
        public OrderMonth[] OrderMonthList { get; set; }
        public int TotalPage { get; set; }
    }

    /// <summary>
    /// 订单月统计列表--每月对象
    /// </summary>
    public class OrderMonth
    {
        public int YearID { get; set; }
        public int MonthID { get; set; }
        public int OrderCount { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal OrderMonthIncome { get; set; }
    }

    /// <summary>
    /// 集客榜请求参数
    /// </summary>
    public class GetVipCountRP : IAPIRequestParameter
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public void Validate()
        {
        }
    }

    /// <summary>
    /// 集客榜返回参数
    /// </summary>
    public class GetVipCountRD : IAPIResponseData
    {
        public UserInfo[] UserList { get; set; }
        public int TotalPage { get; set; }
    }

    /// <summary>
    /// 集客榜对象
    /// </summary>
    public class UserInfo
    {
        public int OrdinaID { get; set; }
        public string VipName { get; set; }
        public int GetVipCount { get; set; }
    }

    /// <summary>
    /// 收入榜请求参数
    /// </summary>
    public class GetVipAccountRP : IAPIRequestParameter
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public void Validate()
        {
        }
    }

    /// <summary>
    /// 收入榜返回参数
    /// </summary>
    public class GetVipAccountRD : IAPIResponseData
    {
        public UserAccountInfo[] UserAccountList { get; set; }
        public int TotalPage { get; set; }
    }

    /// <summary>
    /// 收入榜对象
    /// </summary>
    public class UserAccountInfo
    {
        public int OrdinaID { get; set; }
        public string VipName { get; set; }
        public decimal GetVipAmount { get; set; }
    }

    /// <summary>
    /// 月收入列表统计请求参数
    /// </summary>
    public class GetAmountDetailRP : IAPIRequestParameter
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public void Validate()
        {
        }
    }

    /// <summary>
    /// 月收入列表统计返回参数
    /// </summary>
    public class GetAmountDetailRD : IAPIResponseData
    {
        public AmountMonth[] AmountMonthList { get; set; }
        public int TotalPage { get; set; }
    }

    /// <summary>
    /// 月收入列表统计对象
    /// </summary>
    public class AmountMonth
    {
        public int YearID { get; set; }
        public int MonthID { get; set; }
        public decimal GetVipAmount { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    /// <summary>
    /// 提现基本信息
    /// </summary>
    public class WDBasiciInfoRD : IAPIResponseData
    {
        /// <summary>
        /// 可提现金额
        /// </summary>
        public decimal? WDCurrentAmount { get; set; }
        /// <summary>
        /// 银行标识
        /// </summary>
        public Guid? VipBankID { get; set; }
        /// <summary>
        /// 银行卡所属银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 银行卡所属银行Logo图片路径
        /// </summary>
        public string LogoUrl { get; set; }
        /// <summary>
        /// 日累计提现金额
        /// </summary>
        public decimal WDAmountDay { get; set; }
        /// <summary>
        /// 日累计提现金额描述
        /// </summary>
        public string WDAmountDayDesc { get; set; }
        /// <summary>
        /// 每天提现次数
        /// </summary>
        public int WDTime { get; set; }
    }
    /// <summary>
    /// 支持提现的银行信息返回对象
    /// </summary>
    public class BankListRD : IAPIResponseData
    {
        public BankInfo[] BankList { get; set; }
    }
    /// <summary>
    /// 银行信息
    /// </summary>
    public class BankInfo
    {
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
        public string LogoUrl { get; set; }
    }
    /// <summary>
    /// 银行卡列表
    /// </summary>
    public class VipBankListRD : IAPIResponseData
    {
        public List<VipBankInfo> VipBankList { get; set; }
    }
    /// <summary>
    /// 银行卡信息
    /// </summary>
    public class VipBankInfo
    {
        public Guid VipBankID { get; set; }
        public Guid BankID { get; set; }
        public string BankName { get; set; }
        public string LogoUrl { get; set; }
        public string AccountName { get; set; }
        public string CardNo { get; set; }
    }
    /// <summary>
    /// 新增/修改银行卡信息请求参数
    /// </summary>
    public class UpdateVipBankRP : IAPIRequestParameter
    {
        public Guid? VipBankID { get; set; }
        public String VipID { get; set; }
        public Guid? BankID { get; set; }
        public String AccountName { get; set; }
        public String CardNo { get; set; }

        public String CustomerID { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 提现申请参数
    /// </summary>
    public class WDApplyRP : IAPIRequestParameter
    {
        public Guid? VipBankID { get; set; }
        public decimal Amount { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 提现申请记录请求参数
    /// </summary>
    public class GetWithdrawDepositRP : IAPIRequestParameter
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public void Validate()
        {
        }
    }
    /// <summary>
    /// 提现申请返回数据
    /// </summary>
    public class WithdrawDepositListRD : IAPIResponseData
    {
        public WithdrawDepositInfo[] WithdrawDepositList { get; set; }
        public int TotalPageCount { get; set; }
    }
    /// <summary>
    /// 提现申请信息
    /// </summary>
    public class WithdrawDepositInfo
    {
        public string WithdrawNo { get; set; }
        public string ApplyDate { get; set; }
        public string BankName { get; set; }
        public decimal? Amount { get; set; }
        public int? Status { get; set; }
    }
    #endregion
}