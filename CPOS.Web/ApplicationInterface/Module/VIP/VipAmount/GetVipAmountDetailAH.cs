using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Request;
using JIT.CPOS.DTO.Module.VIP.VipAmount.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.VipAmount
{
    public class GetVipAmountDetailAH : BaseActionHandler<GetVipAmountDetailRP, GetVipAmountDetailRD>
    {
        protected override GetVipAmountDetailRD ProcessRequest(DTO.Base.APIRequest<GetVipAmountDetailRP> pRequest)
        {
            var rd = new GetVipAmountDetailRD();
            var para = pRequest.Parameters;
            var vipAmountBLL = new VipAmountBLL(CurrentUserInfo);
            var vipAmountDetailBLL = new VipAmountDetailBLL(CurrentUserInfo);

            //查询参数
            List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
            //商户条件
            complexCondition.Add(new EqualsCondition() { FieldName = "a.VipID", Value = CurrentUserInfo.UserID });
            complexCondition.Add(new EqualsCondition() { FieldName = "a.VipCardCode", Value = para.VipCardCode });

            if (para.Type == 1) //余额
                complexCondition.Add(new DirectCondition(" a.AmountSourceId in (1,4,10,17,20,21,23) "));
            else //返现
                complexCondition.Add(new DirectCondition(" a.AmountSourceId in (2,3,13,16,22,24)"));

            //排序参数
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "a.CreateTime", Direction = OrderByDirections.Desc });

            var tempList = vipAmountDetailBLL.GetVipAmountDetailList(complexCondition.ToArray(), lstOrder.ToArray(), para.PageSize, para.PageIndex+1);
            rd.TotalCount = tempList.RowCount;
            rd.TotalPageCount = tempList.PageCount;
            rd.VipAmountDetailList = tempList.Entities.Select(t => new VipAmountDetailInfo()
            {
                AmountSourceName = t.AmountSourceName,
                Amount = t.Amount.Value,
                CreateTime = t.CreateTime.Value.ToString("yyyy-MM-dd HH:mm")
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