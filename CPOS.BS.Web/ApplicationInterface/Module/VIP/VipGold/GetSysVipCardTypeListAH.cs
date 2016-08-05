using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    /// <summary>
    /// 获取在线销售的会员卡类型信息 作为 货源卡分润规则的下拉列表
    /// </summary>
    public class GetSysVipCardTypeListAH : BaseActionHandler<GetSysVipCardTypeListRP, GetSysVipCardTypeListRD>
    {
        protected override GetSysVipCardTypeListRD ProcessRequest(DTO.Base.APIRequest<GetSysVipCardTypeListRP> pRequest)
        {
            var rd = new GetSysVipCardTypeListRD();
            var parameter=pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            SysVipCardTypeBLL bll = new SysVipCardTypeBLL(loggingSessionInfo);
            var models = DataTableToObject.ConvertToList<SysVipCardTypeInfo>(bll.GetVipCardTypeByIsprepaid(loggingSessionInfo.ClientID,parameter.IsOnLineSale).Tables[0]);

            var lst = models.Select(m => new SysVipCardTypeInfo() { IsPrepaid = m.IsPrepaid, VipCardTypeID = m.VipCardTypeID, VipCardTypeName = m.VipCardTypeName, IsBuyUpgrade = m.IsBuyUpgrade, IsPurchaseUpgrade = m.IsPurchaseUpgrade, IsRecharge = m.IsRecharge}).ToList();
            rd.SysVipCardTypeList = lst.ToList();
            return rd;
        }
    }
}