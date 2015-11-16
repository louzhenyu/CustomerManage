using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.SysVipCardType.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.SysVipCardType
{
    public class SetSpecialDateAH : BaseActionHandler<SetSpecialDateRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetSpecialDateRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var sysVipCardTypeBLL = new SysVipCardTypeBLL(loggingSessionInfo);
            var specialDateBLL = new SpecialDateBLL(loggingSessionInfo);

            var specialDateEntity = specialDateBLL.QueryByEntity(new SpecialDateEntity() { VipCardTypeID = para.VipCardTypeID, HolidayID = para.HolidayID }, null).FirstOrDefault();
            if (specialDateEntity != null)
                throw new APIException("该卡类型已添加此特殊日期") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            specialDateEntity = new SpecialDateEntity()
            {
                VipCardTypeID = para.VipCardTypeID,
                HolidayID = para.HolidayID,
                NoAvailablePoints = para.NoAvailablePoints,   //不可用积分（0=不可用；1=可用）
                NoAvailableDiscount = para.NoAvailableDiscount, //不可用折扣（0=不可用；1=可用）
                NoRewardPoints = para.NoRewardPoints,           //不可回馈积分（0=不可用；1=可用）
                CustomerID = loggingSessionInfo.ClientID
            };
            specialDateBLL.Create(specialDateEntity);
            return rd;
        }
    }
}