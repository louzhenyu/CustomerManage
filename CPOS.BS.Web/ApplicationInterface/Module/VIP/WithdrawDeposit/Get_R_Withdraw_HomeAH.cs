using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.WithdrawDeposit
{
    public class Get_R_Withdraw_HomeAH : BaseActionHandler<EmptyRequestParameter, Get_R_Withdraw_HomeRD>
    {
        protected override Get_R_Withdraw_HomeRD ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };
            R_Withdraw_HomeBLL bll = new R_Withdraw_HomeBLL(loggingSessionInfo);
            Get_R_Withdraw_HomeRD result = new Get_R_Withdraw_HomeRD();

            //1=提现金额 2=可提现金额 3=当年已完成提现金额 4=超级分销商 5=待批准提现笔数
            var vipEntity = bll.GetTopListByCustomer(loggingSessionInfo.ClientID, 1);
            var userEntity = bll.GetTopListByCustomer(loggingSessionInfo.ClientID, 2);
            var srtEntity = bll.GetTopListByCustomer(loggingSessionInfo.ClientID, 4);
            result.List.Add(new WithdrawInfo() { Type = 1, Vip = vipEntity.BalanceTotal, User = userEntity.BalanceTotal, SRT = srtEntity.BalanceTotal, Count = vipEntity.BalanceTotal + userEntity.BalanceTotal + srtEntity.BalanceTotal });
            result.List.Add(new WithdrawInfo() { Type = 2, Vip = vipEntity.CanWithdrawTotal, User = userEntity.CanWithdrawTotal, SRT = srtEntity.CanWithdrawTotal, Count = vipEntity.CanWithdrawTotal + userEntity.CanWithdrawTotal + srtEntity.CanWithdrawTotal });
            result.List.Add(new WithdrawInfo() { Type = 4, Vip = vipEntity.CurrYearFinishWithdrawTotal, User = userEntity.CurrYearFinishWithdrawTotal, SRT = srtEntity.CurrYearFinishWithdrawTotal, Count = vipEntity.CurrYearFinishWithdrawTotal + userEntity.CurrYearFinishWithdrawTotal + srtEntity.CurrYearFinishWithdrawTotal });
            result.List.Add(new WithdrawInfo() { Type = 5, Vip = vipEntity.PreAuditWithdrawNumber, User = userEntity.PreAuditWithdrawNumber, SRT = srtEntity.PreAuditWithdrawNumber, Count = vipEntity.PreAuditWithdrawNumber + userEntity.PreAuditWithdrawNumber + srtEntity.PreAuditWithdrawNumber });

            if (srtEntity == null)
            {
                srtEntity = new R_Withdraw_HomeEntity();
            }

            #region 余额非空判断
            if (vipEntity.BalanceTotal == null)
            {
                vipEntity.BalanceTotal = 0;
            }
            if (userEntity.BalanceTotal == null)
            {
                userEntity.BalanceTotal = 0;
            }
            if (srtEntity.BalanceTotal == null)
            {
                srtEntity.BalanceTotal = 0;
            }
            #endregion

            #region 可提现金额总计 非空判断
            if (vipEntity.CanWithdrawTotal == null)
            {
                vipEntity.CanWithdrawTotal = 0;
            }

            if (userEntity.CanWithdrawTotal == null)
            {
                userEntity.CanWithdrawTotal = 0;
            }

            if (srtEntity.CanWithdrawTotal == null)
            {
                srtEntity.CanWithdrawTotal = 0;
            }
            #endregion

            #region 待批准提现金额总计 非空判断
            if (vipEntity.PreAuditWithdrawNumber == null)
            {
                vipEntity.PreAuditWithdrawNumber = 0;
            }

            if (userEntity.PreAuditWithdrawNumber == null)
            {
                userEntity.PreAuditWithdrawNumber = 0;
            }

            if (srtEntity.PreAuditWithdrawNumber == null)
            {
                srtEntity.PreAuditWithdrawNumber = 0;
            }
            #endregion

            #region 待批准提现笔数总计 非空判断
            if (vipEntity.PreAuditWithdrawTotal == null)
            {
                vipEntity.PreAuditWithdrawTotal = 0;
            }

            if (userEntity.PreAuditWithdrawTotal == null)
            {
                userEntity.PreAuditWithdrawTotal = 0;
            }

            if (srtEntity.PreAuditWithdrawTotal == null)
            {
                srtEntity.PreAuditWithdrawTotal = 0;
            }
            #endregion

            #region 当年已完成提现笔数总计 非空判断
            if (vipEntity.CurrYearFinishWithdrawNumber == null)
            {
                vipEntity.CurrYearFinishWithdrawNumber = 0;
            }

            if (userEntity.CurrYearFinishWithdrawNumber == null)
            {
                userEntity.CurrYearFinishWithdrawNumber = 0;
            }

            if (srtEntity.CurrYearFinishWithdrawNumber == null)
            {
                srtEntity.CurrYearFinishWithdrawNumber = 0;
            }
            #endregion

            #region 当年已完成提现笔数总计 非空判断

            if (vipEntity.CurrYearFinishWithdrawTotal == null)
            {
                vipEntity.CurrYearFinishWithdrawTotal = 0;
            }

            if (userEntity.CurrYearFinishWithdrawTotal == null)
            {
                userEntity.CurrYearFinishWithdrawTotal = 0;
            }

            if (srtEntity.CurrYearFinishWithdrawTotal == null)
            {
                srtEntity.CurrYearFinishWithdrawTotal = 0;
            }
            #endregion

            result.List.Add(new WithdrawInfo() { Type = 1, Vip = vipEntity.BalanceTotal, User = userEntity.BalanceTotal, SRT = srtEntity.BalanceTotal, Count = vipEntity.BalanceTotal + userEntity.BalanceTotal + srtEntity.BalanceTotal });
            result.List.Add(new WithdrawInfo() { Type = 2, Vip = vipEntity.CanWithdrawTotal, User = userEntity.CanWithdrawTotal, SRT = srtEntity.CanWithdrawTotal, Count = vipEntity.CanWithdrawTotal + userEntity.CanWithdrawTotal + srtEntity.CanWithdrawTotal });
            result.List.Add(new WithdrawInfo() { Type = 4, Vip = vipEntity.CurrYearFinishWithdrawTotal, User = userEntity.CurrYearFinishWithdrawTotal, SRT = srtEntity.CurrYearFinishWithdrawTotal, Count = vipEntity.CurrYearFinishWithdrawTotal + userEntity.CurrYearFinishWithdrawTotal + srtEntity.CurrYearFinishWithdrawTotal });
            result.List.Add(new WithdrawInfo() { Type = 5, Vip = vipEntity.PreAuditWithdrawNumber, User = userEntity.PreAuditWithdrawNumber, SRT = srtEntity.PreAuditWithdrawNumber, Count = vipEntity.PreAuditWithdrawNumber + userEntity.PreAuditWithdrawNumber + srtEntity.PreAuditWithdrawNumber });
            result.List.Add(new WithdrawInfo() { Type = 3, Vip = vipEntity.PreAuditWithdrawTotal, User = userEntity.PreAuditWithdrawTotal, SRT = srtEntity.PreAuditWithdrawTotal, Count = vipEntity.PreAuditWithdrawTotal + userEntity.PreAuditWithdrawTotal + srtEntity.PreAuditWithdrawTotal });
            result.List.Add(new WithdrawInfo() { Type = 6, Vip = vipEntity.CurrYearFinishWithdrawNumber, User = userEntity.CurrYearFinishWithdrawNumber, SRT = srtEntity.CurrYearFinishWithdrawNumber, Count = vipEntity.CurrYearFinishWithdrawNumber + userEntity.CurrYearFinishWithdrawNumber + srtEntity.CurrYearFinishWithdrawNumber });
            result.isConfigRule = bll.IsConfigRule(loggingSessionInfo.CurrentUser.customer_id);
            return result;
        }
    }
}