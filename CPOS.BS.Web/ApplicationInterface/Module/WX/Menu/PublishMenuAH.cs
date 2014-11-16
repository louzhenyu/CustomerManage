using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity.WX;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Request;
using JIT.CPOS.DTO.Module.WeiXin.Menu.Response;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WX.Menu
{
    public class PublishMenuAH : BaseActionHandler<PublishMenuRP, PublishMenuRD>
    {
        protected override PublishMenuRD ProcessRequest(DTO.Base.APIRequest<PublishMenuRP> pRequest)
        {
            var rd = new PublishMenuRD();
            string applicationId = pRequest.Parameters.ApplicationId;
            
            var bll = new CommonBLL();
            var result = bll.CreateMenu(CurrentUserInfo, applicationId);

            rd.resultEntity = result;

            if (result.errcode == "0")
            {
                return rd;
            }
            else
            {
                string errorMeg = result.errcode + result.errmsg;

                if (!string.IsNullOrEmpty(errorMeg))
                {
                    throw new APIException(errorMeg) { ErrorCode = 120 };
                }
                return rd;
            }
        }
    }
}