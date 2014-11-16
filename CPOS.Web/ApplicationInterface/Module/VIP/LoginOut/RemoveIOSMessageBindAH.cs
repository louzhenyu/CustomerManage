using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.LoginOut.Request;
using JIT.CPOS.DTO.Module.VIP.LoginOut.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.LoginOut
{
    public class RemoveIOSMessageBindAH : BaseActionHandler<RemoveIOSMessageBindRP, RemoveIOSMessageBindRD>
    {
        protected override RemoveIOSMessageBindRD ProcessRequest(DTO.Base.APIRequest<RemoveIOSMessageBindRP> pRequest)
        {
            RemoveIOSMessageBindRD rd = new RemoveIOSMessageBindRD();

            string userId = pRequest.Parameters.UserId;
            string customerId = pRequest.CustomerID;


            PushUserBasicBLL service = new PushUserBasicBLL(this.CurrentUserInfo);
            PushUserBasicEntity basicInfo = new PushUserBasicEntity();
           
            basicInfo = service.GetByID(userId);
            if (basicInfo == null)
            {
                throw new APIException("用户ID无效") { ErrorCode = 103 };
            }
            else
            {
                service.DeletePushUserBasicByUserId(userId);
            }


            return rd;
        }
    }
}