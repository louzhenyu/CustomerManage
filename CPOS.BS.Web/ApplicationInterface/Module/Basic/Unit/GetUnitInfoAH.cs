using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.Unit.Request;
using JIT.CPOS.DTO.Module.Basic.Unit.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Unit
{
    public class GetUnitInfoAH : BaseActionHandler<GetUnitInfoRP, GetUnitInfoRD>
    {
        protected override GetUnitInfoRD ProcessRequest(DTO.Base.APIRequest<GetUnitInfoRP> pRequest)
        {
            var rd = new GetUnitInfoRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var UnitBLL = new t_unitBLL(loggingSessionInfo);

            #region 
            var Data = UnitBLL.GetByID(para.UnitID);
            if (Data != null)
            {
                rd.UnitID = Data.unit_id;
                rd.UnitName = Data.unit_name;
                rd.UnitAddress = Data.unit_address;
                rd.UnitPhone = Data.unit_tel;
            }
            #endregion

            return rd;
        }
    }
}