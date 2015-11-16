using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.Basic.Holiday.Request;
using JIT.CPOS.DTO.Module.Basic.Holiday.Response;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.BasicSet
{
    public class SetHolidayAH : BaseActionHandler<SetHolidayRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<SetHolidayRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var HolidayBLL = new HolidayBLL(loggingSessionInfo);
            try
            {
                if (!string.IsNullOrWhiteSpace(para.HolidayId))
                {
                    //更新
                    HolidayEntity UpdateData = HolidayBLL.GetByID(para.HolidayId);
                    if (UpdateData == null)
                    {
                        throw new APIException("假日对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                    UpdateData.HolidayName = para.HolidayName;
                    UpdateData.BeginDate = para.BeginDate;
                    UpdateData.EndDate = para.EndDate;
                    UpdateData.Desciption = para.Desciption;
                    //执行
                    HolidayBLL.Update(UpdateData);
                }
                else
                {
                    
                    //新增
                    HolidayEntity AddData = new HolidayEntity();
                    //AddData.HolidayId = System.Guid.NewGuid().ToString(); 
                    AddData.HolidayName = para.HolidayName;
                    AddData.BeginDate = para.BeginDate;
                    AddData.EndDate = para.EndDate;
                    AddData.Desciption = para.Desciption;
                    AddData.CustomerID = loggingSessionInfo.ClientID;
                    //执行
                    HolidayBLL.Create(AddData);
                }
            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }
            

            return rd;
        }
    }
}