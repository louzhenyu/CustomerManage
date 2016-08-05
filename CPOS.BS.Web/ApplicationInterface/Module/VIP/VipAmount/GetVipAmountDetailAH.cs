using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Request;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Response;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipAmount
{
    public class GetVipAmountDetailAH : BaseActionHandler<GetVipAmountDetailRP, GetVipAmountDetailRD>
    {
        protected override GetVipAmountDetailRD ProcessRequest(DTO.Base.APIRequest<GetVipAmountDetailRP> pRequest)
        {
            var rd = new GetVipAmountDetailRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo; //登录状态信息
            var vipAmountBLL = new VipAmountBLL(loggingSessionInfo);
            var vipAmountDetailBLL = new VipAmountDetailBLL(loggingSessionInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //商户条件
            complexCondition.Add(new EqualsCondition() { FieldName = "a.VipID", Value = para.VipId });
            complexCondition.Add(new EqualsCondition() { FieldName = "a.VipCardCode", Value = para.VipCardCode });

            if (para.Type == 1) //余额
            {
                complexCondition.Add(new DirectCondition(" a.AmountSourceId in (1,4,6,10,17,20,21,23) "));
            }
            else if (para.Type == 2) //返现
            {
                complexCondition.Add(new DirectCondition(" a.AmountSourceId in (2,3,13,16,22,24)"));
            }
           


            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "a.CreateTime", Direction = OrderByDirections.Desc });

            var tempList = vipAmountDetailBLL.GetVipAmountDetailList(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex);
            rd.TotalCount = tempList.RowCount;
            rd.TotalPageCount = tempList.PageCount;
            rd.VipAmountDetailList = tempList.Entities.Select(t => new VipAmountDetailInfo()
            {
                UnitName = t.UnitName,
                AmountSourceName = t.AmountSourceName,
                Amount = t.Amount.Value,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd HH:mm"),
                Reason = t.Reason == null ? "" : t.Reason,
                Remark = t.Remark == null ? "" : t.Remark,
                ImageUrl = t.ImageUrl,
                CreateByName = t.CreateByName,
            }).ToArray();

            //查询当前余额/返现
            rd.CurrentAmount = 0; //当前余额/返现（默认值）
            var vipAmountInfo = vipAmountBLL.QueryByEntity(new VipAmountEntity() { VipId = para.VipId, VipCardCode = para.VipCardCode }, null).FirstOrDefault();

            if (vipAmountInfo != null)
            {
                if (para.Type == 1) //余额
                    rd.CurrentAmount = vipAmountInfo.EndAmount.Value;
                else
                    rd.CurrentAmount = vipAmountInfo.ValidReturnAmount.Value;
            }
            return rd;
        }
    }
}