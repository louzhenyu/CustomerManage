using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VipManager.Request;
using JIT.CPOS.DTO.Module.VIP.VipManager.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipManager
{
    public class GetVipListAH : BaseActionHandler<GetVipListRP, GetVipListRD>
    {
        protected override GetVipListRD ProcessRequest(DTO.Base.APIRequest<GetVipListRP> pRequest)
        {
            var rd = new GetVipListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBLL = new VipCardBLL(loggingSessionInfo);
            var unitBLL = new t_unitBLL(loggingSessionInfo);
            //条件参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            if (!string.IsNullOrWhiteSpace(para.VipCardCode) || !string.IsNullOrWhiteSpace(para.VipName) || !string.IsNullOrWhiteSpace(para.Phone))
            {
                //跨门店查询
                if (!string.IsNullOrEmpty(para.VipCardCode))
                {
                    if (para.VipTypeID == 1)
                    {
                        complexCondition.Add(new DirectCondition("a.VipCardISN='" + para.VipCardCode + "' "));
                    }
                    else
                    {
                        complexCondition.Add(new DirectCondition("a.VipCardCode like '" + para.VipCardCode + "%' "));
                    }

                }
                if (!string.IsNullOrEmpty(para.VipName))
                    complexCondition.Add(new DirectCondition("g.VipName like '" + para.VipName + "%' "));
                if (!string.IsNullOrEmpty(para.Phone))
                    complexCondition.Add(new DirectCondition("g.Phone like '" + para.Phone + "%' "));
            }
            else
            {
                //门店对象
                t_unitEntity unitData = unitBLL.GetByID(loggingSessionInfo.CurrentUserRole.UnitId);
                string type_id = unitData == null ? "" : unitData.type_id;
                //总部
                if (type_id != "2F35F85CF7FF4DF087188A7FB05DED1D")
                    complexCondition.Add(new EqualsCondition() { FieldName = "u.Unit_ID", Value = loggingSessionInfo.CurrentUserRole.UnitId });
            }
            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "MembershipTime", Direction = OrderByDirections.Desc });

            //调用会员卡管理列表查询
            var tempList = VipCardBLL.GetVipCardList(null, null, complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.VipInfoList = tempList.Entities.Select(t => new VipInfo()
            {
                VipCardID = t.VipCardID,
                VIPID = t.VIPID,
                VipCode = t.VipCode,
                VipCardCode = t.VipCardCode,
                VipName = t.VipRealName == null ? t.VipName : t.VipRealName,
                Phone = t.Phone,
                Gender = t.Gender,
                VipCardTypeName = t.VipCardTypeName,
                VipCardStatusId = t.VipCardStatusId.Value,
                MembershipTime = t.MembershipTime == null ? "" : t.MembershipTime.Value.ToString("yyyy-MM-dd"),
                MembershipUnitName = t.UnitName == null ? "" : t.UnitName

            }).ToList();

            return rd;

        }
    }
}