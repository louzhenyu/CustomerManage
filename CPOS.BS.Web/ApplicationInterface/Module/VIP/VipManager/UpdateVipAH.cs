using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipManager.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipManager
{
    public class UpdateVipAH : BaseActionHandler<UpdateVipRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<UpdateVipRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //会员
            var VipBLL = new VipBLL(loggingSessionInfo);

            
            try
            {
                VipEntity UpData = VipBLL.GetByID(para.VipID);
                if(UpData==null)
                    throw new APIException("会员不存在！") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };


                //UpData.VipName = para.VipName;
                UpData.VipRealName = para.VipName;
                UpData.Phone = para.Phone;

                if (!string.IsNullOrWhiteSpace(para.Col22))
                {
                    if (para.Col22.Equals("Y"))
                    {
                        UpData.Birthday = para.Birthday;
                        UpData.Col22 = "N";
                    }
                }
                UpData.Gender = para.Gender;
                UpData.IDNumber = para.IDNumber;
                
                VipBLL.Update(UpData);

            }
            catch (APIException apiEx)
            {
                throw new APIException(apiEx.ErrorCode, apiEx.Message);
            }


            return rd;
        }
    }
}