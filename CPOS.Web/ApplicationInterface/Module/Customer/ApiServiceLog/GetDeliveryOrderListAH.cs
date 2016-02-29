using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Request;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.ApiServiceLog
{
    public class GetDeliveryOrderListAH : BaseActionHandler<GetDeliveryOrderListRP, GetDeliveryOrderListRD>
    {
        /// <summary>
        /// api获取当前门店提货单信息
        /// </summary>
        protected override GetDeliveryOrderListRD ProcessRequest(DTO.Base.APIRequest<GetDeliveryOrderListRP> pRequest)
        {
            var rd = new GetDeliveryOrderListRD();
            var para = pRequest.Parameters;
            var InoutBLL = new T_InoutBLL(CurrentUserInfo);
            var VipBLL = new VipBLL(CurrentUserInfo);
            var UserBLL = new T_UserBLL(CurrentUserInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            complexCondition.Add(new EqualsCondition() { FieldName = "Field8", Value = 2 });
            //已提货订单
            complexCondition.Add(new EqualsCondition() { FieldName = "Field7", Value = 610 });

            #region 门店条件处理
            string UnitId = "";
            if (CurrentUserInfo.CurrentUserRole != null)
                if (!string.IsNullOrWhiteSpace(CurrentUserInfo.CurrentUserRole.UnitId))
                    UnitId = CurrentUserInfo.CurrentUserRole.UnitId;

            if (!string.IsNullOrWhiteSpace(UnitId))
                complexCondition.Add(new EqualsCondition() { FieldName = "unit_id", Value = CurrentUserInfo.CurrentUserRole.UnitId });
            else
                return rd;
            #endregion
            
            

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "Field9", Direction = OrderByDirections.Desc });


            var Result = InoutBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.pageSize, para.pageIndex);
            rd.TotalPageCount = Result.PageCount;
            rd.TotalCount = Result.RowCount;

            rd.DeliveryOrderList = Result.Entities.Select(t => new DeliveryOrderData()
            {
                VipID = t.vip_no,
                VipName = "",
                DeliveryTime = t.Field9,
                OrderNo = t.order_no,
                SalesUserID = t.sales_user
            }).ToList();


            foreach (var item in rd.DeliveryOrderList)
            {
                //会员名称
                var VipData = VipBLL.GetByID(item.VipID);
                if (VipData != null)
                {
                    item.VipName = VipData.VipName ?? "";
                    item.HeadImgUrl = VipData.HeadImgUrl ?? "";
                }

                //服务人员名称
                var UserData = UserBLL.GetByID(item.SalesUserID);
                if (UserData != null)
                    item.SalesUserName = UserData.user_name ?? "";
            }

            return rd;
        }
    }
}