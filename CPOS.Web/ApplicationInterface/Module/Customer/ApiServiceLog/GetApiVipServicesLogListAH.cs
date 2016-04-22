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
    /// <summary>
    /// api客服记录列表
    /// </summary>
    public class GetApiVipServicesLogListAH : BaseActionHandler<GetVipServicesLogListRP, GetVipServicesLogListRD>
    {
        protected override GetVipServicesLogListRD ProcessRequest(DTO.Base.APIRequest<GetVipServicesLogListRP> pRequest)
        {
            var rd = new GetVipServicesLogListRD();
            var para = pRequest.Parameters;
            var vipServicesLogBLL = new VipServicesLogBLL(CurrentUserInfo);
            var VipBLL = new VipBLL(CurrentUserInfo);
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            #region 门店条件处理
            string UnitId = "";
            if (CurrentUserInfo.CurrentUserRole != null)
                if (!string.IsNullOrWhiteSpace(CurrentUserInfo.CurrentUserRole.UnitId))
                    UnitId = CurrentUserInfo.CurrentUserRole.UnitId;

            if (!string.IsNullOrWhiteSpace(para.VipID))
            {
                complexCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = para.VipID });
            }
            //if (!string.IsNullOrWhiteSpace(UnitId))   //过滤门店的要去掉
            //    complexCondition.Add(new EqualsCondition() { FieldName = "UnitID", Value = CurrentUserInfo.CurrentUserRole.UnitId });
            //else
            //    return rd;
            #endregion
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "ServicesTime", Direction = OrderByDirections.Desc });
            if (para.PageIndex <= 0)
            {
                para.PageIndex = 1;
            }
            var tempList = vipServicesLogBLL.PagedQuery(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.VipServicesLogList = tempList.Entities.Select(t => new VipServicesLogInfo()
            {
                ServicesLogID = t.ServicesLogID.ToString(),
                ServicesTime = t.ServicesTime == null ? "" : t.ServicesTime.Value.ToString("yyyy-MM-dd HH:mm"),
                ServicesMode = t.ServicesMode,
                UnitName = t.UnitName,
                UserName = t.UserName,
                Content = t.Content,
                VipID=t.VipID
            }).ToArray();

            foreach (var item in rd.VipServicesLogList)
            {
                //会员名称
                var VipData = VipBLL.GetByID(item.VipID);
                if (VipData != null)
                {
                    item.VipName = VipData.VipName ?? "";
                    item.HeadImgUrl = VipData.HeadImgUrl ?? "";
                }
                
            }

            return rd;
        }
    }
}