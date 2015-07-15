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
    public class SetRefundOrderAH : BaseActionHandler<GetRefundOrderDetailRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<GetRefundOrderDetailRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var salesReturnBLL = new T_SalesReturnBLL(loggingSessionInfo);
            var historyBLL = new T_SalesReturnHistoryBLL(loggingSessionInfo);
            var refundOrderBLL = new T_RefundOrderBLL(loggingSessionInfo);

            var pTran = salesReturnBLL.GetTran();//事务
            T_SalesReturnEntity salesReturnEntity = null;
            T_SalesReturnHistoryEntity historyEntity = null;

            T_RefundOrderEntity refundEntity = null;

            var vipBll = new VipBLL(loggingSessionInfo);        //会员BLL实例化
            var userBll = new T_UserBLL(loggingSessionInfo);    //店员BLL实例化
            T_UserEntity userEntity = null;   //店员信息

            refundEntity = refundOrderBLL.GetByID(para.RefundID);
            userEntity = userBll.GetByID(loggingSessionInfo.UserID);
            using (pTran.Connection)
            {
                try
                {
                    if (refundEntity != null)
                    {
                        refundEntity.Status = 2;//已退款
                        refundOrderBLL.Update(refundEntity,pTran);
                        salesReturnEntity = salesReturnBLL.GetByID(refundEntity.SalesReturnID);
                        if (salesReturnEntity != null)
                        {
                            salesReturnEntity.Status = 7;//已完成
                            salesReturnBLL.Update(salesReturnEntity,pTran);
                            historyEntity = new T_SalesReturnHistoryEntity()
                            {
                                SalesReturnID = salesReturnEntity.SalesReturnID,
                                OperationType = 7,
                                OperationDesc = "退款",
                                OperatorID = loggingSessionInfo.UserID,
                                HisRemark = "您的服务单财务已退款，请注意查收",
                                OperatorName = userEntity.user_name,
                                OperatorType = 1
                            };
                            historyBLL.Create(historyEntity, pTran);
                        }
                    }
                    pTran.Commit();  //提交事物
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