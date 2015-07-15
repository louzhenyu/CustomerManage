using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.SalesReturn.Request;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Order.SalesReturn
{
    public class SetSalesReturnAH : BaseActionHandler<SetSalesReturnRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetSalesReturnRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var salesReturnBLL = new T_SalesReturnBLL(CurrentUserInfo);
            var historyBLL = new T_SalesReturnHistoryBLL(CurrentUserInfo);
            var pTran = salesReturnBLL.GetTran();//事务
            T_SalesReturnEntity salesReturnEntity = null;
            T_SalesReturnHistoryEntity historyEntity = null;


            var vipBll = new VipBLL(CurrentUserInfo);        //会员BLL实例化
            var userBll = new T_UserBLL(CurrentUserInfo);    //店员BLL实例化
            VipEntity vipEntity = null;       //会员信息
            T_UserEntity userEntity = null;   //店员信息

            using (pTran.Connection)
            {
                try
                {
                    switch (para.OperationType)
                    {
                        case 1://申请
                            salesReturnEntity = new T_SalesReturnEntity();
                            salesReturnEntity.SalesReturnNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                            salesReturnEntity.VipID = CurrentUserInfo.UserID;
                            salesReturnEntity.ServicesType = para.ServicesType;
                            salesReturnEntity.DeliveryType = para.DeliveryType;
                            salesReturnEntity.OrderID = para.OrderID;
                            salesReturnEntity.ItemID = para.ItemID;
                            salesReturnEntity.SkuID = para.SkuID;
                            salesReturnEntity.Qty = para.Qty;
                            salesReturnEntity.ActualQty = para.Qty;

                            salesReturnEntity.UnitID = para.UnitID;
                            salesReturnEntity.UnitName = para.UnitName;
                            salesReturnEntity.UnitTel = para.UnitTel;
                            salesReturnEntity.Address = para.Address;

                            salesReturnEntity.Contacts = para.Contacts;
                            salesReturnEntity.Phone = para.Phone;

                            salesReturnEntity.Reason = para.Reason;
                            salesReturnEntity.Status = 1;   //待审核
                            salesReturnEntity.CustomerID = CurrentUserInfo.ClientID;

                            salesReturnBLL.Create(salesReturnEntity, pTran);

                            vipEntity = vipBll.GetByID(CurrentUserInfo.UserID);

                            historyEntity = new T_SalesReturnHistoryEntity()
                            {
                                SalesReturnID = salesReturnEntity.SalesReturnID,
                                OperationType = 1,
                                OperationDesc = "申请",
                                OperatorID = CurrentUserInfo.UserID,
                                HisRemark = "申请",
                                OperatorName = vipEntity.VipName,
                                OperatorType = 0
                            };
                            historyBLL.Create(historyEntity, pTran);
                            break;
                        case 2://取消
                            salesReturnEntity = salesReturnBLL.GetByID(para.SalesReturnID);
                            vipEntity = vipBll.GetByID(CurrentUserInfo.UserID);
                            if (salesReturnEntity != null)
                            {
                                salesReturnEntity.Status = 2;   //取消申请
                                salesReturnBLL.Update(salesReturnEntity, pTran);
                                historyEntity = new T_SalesReturnHistoryEntity()
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
                            break;
                        default:
                            break;
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