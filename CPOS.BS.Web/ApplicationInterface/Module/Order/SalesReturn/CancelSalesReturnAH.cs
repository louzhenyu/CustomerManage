using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    public class CancelSalesReturnAH : BaseActionHandler<SetSalesReturnRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetSalesReturnRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var salesReturnBLL = new T_SalesReturnBLL(loggingSessionInfo);
            var historyBLL = new T_SalesReturnHistoryBLL(loggingSessionInfo);
            var vipBll = new VipBLL(loggingSessionInfo);
            var salesReturnEntity = salesReturnBLL.GetByID(para.SalesReturnID);
            var vipEntity = vipBll.GetByID(CurrentUserInfo.UserID);
            var pTran = salesReturnBLL.GetTran();//事务
            using (pTran.Connection)
            {
                try
                {
                    if (salesReturnEntity != null)
                    {
                        salesReturnEntity.Status = 2;   //取消申请
                        salesReturnBLL.Update(salesReturnEntity, pTran);

                        var historyEntity = new T_SalesReturnHistoryEntity()
                        {
                            SalesReturnID = salesReturnEntity.SalesReturnID,
                            OperationType = 2,
                            OperationDesc = "取消申请",
                            OperatorID = CurrentUserInfo.UserID,
                            HisRemark = "取消申请",
                            OperatorName = vipEntity.VipName,
                            OperatorType = 0
                        };
                        historyBLL.Create(historyEntity, pTran);
                    }
                }
                catch (Exception ex)
                {

                    pTran.Rollback();//回滚事务
                    throw new APIException(ex.Message);
                }


            }
            return rd;
        }
    }
}