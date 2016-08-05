using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Request;
using JIT.CPOS.DTO.Module.VIP.WithdrawDeposit.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.WithdrawDeposit
{
    public class GetWithdrawDepositApplyListAH : BaseActionHandler<GetWithdrawDepositApplyListRP, GetWithdrawDepositApplyListRD>
    {
        protected override GetWithdrawDepositApplyListRD ProcessRequest(DTO.Base.APIRequest<GetWithdrawDepositApplyListRP> pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            if (loggingSessionInfo == null)
                throw new APIException("用户未登录") { ErrorCode = ERROR_CODES.INVALID_REQUEST };

            var parameter = pRequest.Parameters;
            //排序参数
            List<OrderBy> orderList = new List<OrderBy> { };
            orderList.Add(new OrderBy() { FieldName = "ApplyDate", Direction = OrderByDirections.Desc });
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.WithdrawNo))
                complexCondition.Add(new LikeCondition() { FieldName = "WithdrawNo", Value = "%" + parameter.WithdrawNo + "%" });
            if (pRequest.Parameters.VipType != null && pRequest.Parameters.VipType != -1)
                complexCondition.Add(new EqualsCondition() { FieldName = "VipType", Value = parameter.VipType });

            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.Name))
                complexCondition.Add(new LikeCondition() { FieldName = "Name", Value = "%" + parameter.Name + "%" });
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.Phone))
                complexCondition.Add(new LikeCondition() { FieldName = "Phone", Value = "%" + parameter.Phone + "%" });

            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ApplyStartDate))
                complexCondition.Add(new DirectCondition() { Expression = "ApplyDate>='" + parameter.ApplyStartDate + "'" });
            if (!string.IsNullOrWhiteSpace(pRequest.Parameters.ApplyEndDate))
            {
                if (parameter.ApplyStartDate == parameter.ApplyEndDate)
                {
                    parameter.ApplyEndDate = parameter.ApplyEndDate + " 23:59";
                }
                complexCondition.Add(new DirectCondition { Expression = "ApplyDate<='" + parameter.ApplyEndDate + "'" });
            }


            if (parameter.Status != null && parameter.Status != -1)
                complexCondition.Add(new EqualsCondition() { FieldName = "Status", Value = parameter.Status });
            if (!string.IsNullOrWhiteSpace(parameter.CompleteStartDate))
                complexCondition.Add(new DirectCondition() { Expression = "CompleteDate>='" + parameter.CompleteStartDate + "'" });
            if (!string.IsNullOrWhiteSpace(parameter.CompleteEndDate))
            {
                if (parameter.CompleteEndDate == parameter.CompleteStartDate)
                {
                    parameter.CompleteEndDate = parameter.CompleteEndDate + ": 23:59";
                }
                complexCondition.Add(new DirectCondition { Expression = "CompleteDate<='" + parameter.CompleteEndDate + "'" });
            }
            VipWithdrawDepositApplyBLL bll = new VipWithdrawDepositApplyBLL(CurrentUserInfo);
            int rowCount = 0;
            int pageCount = 0;
            var dbSet = bll.PagedQueryDbSet(complexCondition.ToArray(), orderList.ToArray(), parameter.PageSize, parameter.PageIndex, out rowCount, out pageCount);
            var dbList = DataTableToObject.ConvertToList<WithdrawDepositApplyInfo>(dbSet.Tables[0]);
            foreach (var m in dbList)
            {
                m.ApplyDate = string.IsNullOrWhiteSpace(m.ApplyDate) ? string.Empty : Convert.ToDateTime(m.ApplyDate).ToString("yyyy-MM-dd");
                m.CompleteDate = string.IsNullOrWhiteSpace(m.CompleteDate) ? string.Empty : Convert.ToDateTime(m.CompleteDate).ToString("yyyy-MM-dd");
            }
            var result = new GetWithdrawDepositApplyListRD();
            result.List = dbList;
            result.TotalCount = rowCount;
            result.TotalPage = pageCount;

            return result;
        }
    }
}