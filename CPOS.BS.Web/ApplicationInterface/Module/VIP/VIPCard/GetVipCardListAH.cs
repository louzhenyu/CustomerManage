using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VIPCard
{
    public class GetVipCardListAH : BaseActionHandler<GetVIPCardListRP, GetVIPCardListRD>
    {
        protected override GetVIPCardListRD ProcessRequest(DTO.Base.APIRequest<GetVIPCardListRP> pRequest)
        {
            var rd = new GetVIPCardListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBLL = new VipCardBLL(loggingSessionInfo);
            var unitBLL = new t_unitBLL(loggingSessionInfo);
            
            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //商户条件
            complexCondition.Add(new EqualsCondition() { FieldName = "a.CustomerID", Value = loggingSessionInfo.ClientID });
            //if (!string.IsNullOrEmpty(para.Phone))
            //    complexCondition.Add(new EqualsCondition() { FieldName = "g.Phone", Value = para.Phone });
            if (!string.IsNullOrEmpty(para.VipCardCode))
                complexCondition.Add(new EqualsCondition() { FieldName = "a.VipCardCode", Value = para.VipCardCode });
            if (!string.IsNullOrEmpty(para.VipCardTypeID))
                complexCondition.Add(new EqualsCondition() { FieldName = "a.VipCardTypeID", Value = para.VipCardTypeID });
            if (!string.IsNullOrEmpty(para.VipCardStatusId))
                complexCondition.Add(new EqualsCondition() { FieldName = "a.VipCardStatusId", Value = para.VipCardStatusId });

            if (!string.IsNullOrEmpty(para.BeginDate))
                complexCondition.Add(new DirectCondition("a.BeginDate>=" + para.BeginDate + " "));
            if (!string.IsNullOrEmpty(para.EndDate))
                complexCondition.Add(new DirectCondition("a.EndDate<=" + para.EndDate + " "));
            if (!string.IsNullOrEmpty(para.UnitID))
            {
                //门店对象
                t_unitEntity unitData = unitBLL.GetByID(para.UnitID);
                string type_id = unitData == null ? "" : unitData.type_id; 
                //总部
                if (type_id != "2F35F85CF7FF4DF087188A7FB05DED1D")
                    complexCondition.Add(new EqualsCondition() { FieldName = "u.Unit_ID", Value = para.UnitID });
            }
            else
            {
                //门店对象
                t_unitEntity D_unitData = unitBLL.GetByID(loggingSessionInfo.CurrentUserRole.UnitId);
                string D_type_id = D_unitData == null ? "" : D_unitData.type_id;

                if (D_type_id != "2F35F85CF7FF4DF087188A7FB05DED1D")
                    complexCondition.Add(new EqualsCondition() { FieldName = "u.Unit_ID", Value = loggingSessionInfo.CurrentUserRole.UnitId });
            }
            if (!string.IsNullOrWhiteSpace(para.VIPID))
            {
                complexCondition.RemoveAt(complexCondition.Count()-1);
            }

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "LastUpdateTime", Direction = OrderByDirections.Desc });
            //调用会员卡管理列表查询
            var tempList = VipCardBLL.GetVipCardList(para.VIPID, para.Phone, complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalPageCount = tempList.PageCount;
            rd.TotalCount = tempList.RowCount;
            rd.VipCardList = tempList.Entities.Select(t => new VipCardInfo()
            {
                VipCardID = t.VipCardID,
                VipCardCode = t.VipCardCode,
                VipCardName = t.VipCardTypeName,
                VipName = t.VipName,
                Phone = t.Phone,
                MembershipTime = t.MembershipTime == null ? "" : t.MembershipTime.Value.ToString("yyyy-MM-dd"),
                MembershipUnitName = t.UnitName == null ? "" : t.UnitName,
                VipCardStatusID = t.VipCardStatusId.Value,
                SalesUserName=t.SalesUserName==null?"":t.SalesUserName,
                ImageUrl = t.picUrl,
                BalanceAmount = t.BalanceAmount == null ? 0 : t.BalanceAmount.Value,
                VIPID = t.VIPID
            }).ToList();
            return rd;
        }
    }
}