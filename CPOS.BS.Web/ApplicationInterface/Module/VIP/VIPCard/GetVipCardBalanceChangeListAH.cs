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
    public class GetVipCardBalanceChangeListAH : BaseActionHandler<GetVipCardBalanceChangeListRP, GetVipCardBalanceChangeListRD>
    {
        protected override GetVipCardBalanceChangeListRD ProcessRequest(DTO.Base.APIRequest<GetVipCardBalanceChangeListRP> pRequest)
        {
            var rd = new GetVipCardBalanceChangeListRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var VipCardBalanceChangeBLL = new VipCardBalanceChangeBLL(loggingSessionInfo);

            if (!string.IsNullOrWhiteSpace(para.VipCardCode))
            {
                //调用会员卡管理列表查询
                var tempList = VipCardBalanceChangeBLL.GetVipCardBalanceChangeList(para.VipCardCode, para.PageSize, para.PageIndex);
                rd.TotalPageCount = tempList.PageCount;
                rd.TotalCount = tempList.RowCount;
                rd.VipCardList = tempList.Entities.Select(t => new VipCardBalanceInfo()
                {
                    CreateTime=t.CreateTime.Value.ToString("yyyy-MM-dd"),
                    UnitName=t.UnitName==null?"":t.UnitName,
                    ChangeAmount = t.ChangeAmount == null ? 0 : t.ChangeAmount.Value,
                    Reason=t.ChangeReason==null?"":t.ChangeReason,
                    Remark = t.Remark == null ? "" : t.Remark,
                    CreateBy = t.CreateByName == null ? "" : t.CreateByName,
                    ImageUrl = t.ImageURL == null ? "" : t.ImageURL
                }).ToList();
            }

            return rd;
        }
    }
}