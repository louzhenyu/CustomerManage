using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Request;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipAmount
{
    /// <summary>
    /// 红利列表   业务逻辑  思路 将VipAmountDetail 和 VipWithdrawDepositApply 整合成一个临时表 然后再对临时表操作
    /// </summary>
    public class GetBonusAH : BaseActionHandler<GetVipAmountDetailRP, GetVipAmountDetailRD>
    {
        protected override GetVipAmountDetailRD ProcessRequest(DTO.Base.APIRequest<GetVipAmountDetailRP> pRequest)
        {
            var rd = new GetVipAmountDetailRD();
            var parameter = pRequest.Parameters;
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(pRequest.CustomerID, pRequest.UserID);
            VipAmountBLL vipAmountBLL = new VipAmountBLL(loggingSessionInfo);
            var vipAmountDetailBLL = new VipAmountDetailBLL(loggingSessionInfo);
            //查询参数
            List<IWhereCondition> Condition = new List<IWhereCondition> { };
            Condition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = loggingSessionInfo.ClientID });
            List<OrderBy> Orders = new List<OrderBy> { };
            //商户条件

            if (parameter.DdividendType == 1) //全部=全部收入+提现明细
            {
                Orders.Add(new OrderBy() { FieldName = "createTime", Direction = OrderByDirections.Asc });
                Condition.Add(new DirectCondition(" (AmountSourceId = -1 OR AmountSourceId=35 OR AmountSourceId=36 OR AmountSourceId=20) "));  //全部收入+提现信息
            }
            else if (parameter.DdividendType == 2)
            {
                Orders.Add(new OrderBy() { FieldName = "createTime", Direction = OrderByDirections.Asc });
                Condition.Add(new DirectCondition(" AmountSourceId IN (36,35,20)"));  //全部收入
                Condition.Add(new DirectCondition(" TableType != 'VipWithdrawDepositApply'")); //去除提现
            }
            else if (parameter.DdividendType == 3)
            {
                Orders.Add(new OrderBy() { FieldName = "createTime", Direction = OrderByDirections.Asc });
                Condition.Add(new DirectCondition(" TableType = 'VipWithdrawDepositApply'")); //只获取提现 
            }

            //获取红利余额+提现记录
            var tempList = vipAmountDetailBLL.GetVipAmountDetailAndWithdrawList(Condition.ToArray(), loggingSessionInfo.UserID, Orders.ToArray(), parameter.PageSize, parameter.PageIndex);
            rd.TotalCount = tempList.RowCount;
            rd.TotalPageCount = tempList.PageCount;
            rd.VipAmountDetailList = tempList.Entities.Select(t => new VipAmountDetailInfo()
            {
                AmountSourceName = t.AmountSourceName,
                Amount = t.Amount.Value,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd"),
                Reason = t.Reason
            }).ToArray();

            #region 统计信息
            //查询 提现余额+当前余额+总收入
            decimal[] array = vipAmountBLL.GetVipSumAmountByCondition(CurrentUserInfo.UserID, CurrentUserInfo.ClientID, true);
            rd.CurrentAmount = array[0];  //红利余额{总金额}
            rd.WithdrawAmount = array[1]; //支出余额
            rd.InAmount = array[2]; //提现金额
            #endregion
            return rd;
        }
    }
}