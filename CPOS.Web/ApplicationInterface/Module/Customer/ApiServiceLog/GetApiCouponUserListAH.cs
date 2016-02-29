using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Request;
using JIT.CPOS.DTO.Module.VIP.ServicesLog.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Customer.ApiServiceLog
{
    public class GetApiCouponUserListAH : BaseActionHandler<GetCouponUseRP, GetApiCouponUserListRD>
    {
        /// <summary>
        /// api获取当前门店核销过的电子券
        /// </summary>
        protected override GetApiCouponUserListRD ProcessRequest(DTO.Base.APIRequest<GetCouponUseRP> pRequest)
        {
            var rd = new GetApiCouponUserListRD();
            var para = pRequest.Parameters;
            var CouponUseBLL = new CouponUseBLL(CurrentUserInfo);
            var VipBLL = new VipBLL(CurrentUserInfo);
            var CouponBLL = new CouponBLL(CurrentUserInfo);
            var UserBLL = new T_UserBLL(CurrentUserInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            #region 门店条件处理
            string UnitId = "";
            if (CurrentUserInfo.CurrentUserRole != null)
                if (!string.IsNullOrWhiteSpace(CurrentUserInfo.CurrentUserRole.UnitId))
                    UnitId = CurrentUserInfo.CurrentUserRole.UnitId;

            if (!string.IsNullOrWhiteSpace(UnitId))
                complexCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = CurrentUserInfo.CurrentUserRole.UnitId });
            else
                return rd;
            #endregion
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });


            var Result = CouponUseBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.pageSize, para.pageIndex);
            rd.TotalPageCount = Result.PageCount;
            rd.TotalCount = Result.RowCount;

            rd.ApiCouponDataList = Result.Entities.Select(t => new CouponUserData()
            {
                VipID = t.VipID,
                VipName = "",
                CreateTime = t.CreateTime.ToString(),
                CouponID = t.CouponID,
                CouponName = "",
                CouponCode = "",
                CreateBy=t.CreateBy
            }).ToList();


            foreach (var item in rd.ApiCouponDataList)
            {
                //会员名称
                var VipData = VipBLL.GetByID(item.VipID);
                if (VipData != null)
                {
                    item.VipName = VipData.VipName ?? "";
                    item.HeadImgUrl = VipData.HeadImgUrl ?? "";
                }
                //券名称，券号
                var CouponData = CouponBLL.GetByID(item.CouponID);
                if (CouponData != null)
                {
                    item.CouponName = CouponData.CouponName ?? "";
                    item.CouponCode = CouponData.CouponCode ?? "";
                }
                //员工名称
                var UserData = UserBLL.GetByID(item.CreateBy);
                if (UserData != null)
                    item.CreateByName = UserData.user_name ?? "";
            }

            return rd;
        }
    }
}