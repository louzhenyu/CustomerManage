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
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.Basic.Holiday
{
    public class DeleteHolidayAH : BaseActionHandler<DeleteHolidayRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<DeleteHolidayRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var HolidayBLL = new HolidayBLL(loggingSessionInfo);
            var SpecialDataBLL = new SpecialDateBLL(loggingSessionInfo);
            try
            {
                SpecialDateEntity SpecialDate = SpecialDataBLL.QueryByEntity(new SpecialDateEntity() { HolidayID = para.HolidayId }, null).FirstOrDefault();
                if (SpecialDate != null)
                {
                    throw new APIException("该假日正在使用中不能更改或删除哦！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                }
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