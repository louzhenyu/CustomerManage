using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Basic.Holiday.Request;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.BasicSet
{
    public class DeleteHolidayAH : BaseActionHandler<DeleteHolidayRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<DeleteHolidayRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var HolidayBLL = new HolidayBLL(loggingSessionInfo);
            try
            {
                //删除
                HolidayEntity DeleteData = HolidayBLL.GetByID(para.HolidayId);
                if (DeleteData == null)
                {
                    throw new APIException("假日对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
                //执行
                HolidayBLL.Delete(DeleteData);
            }
            catch (APIException apiEx)
            {

                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }


            return rd;
        }
    }
}