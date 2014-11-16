using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.LoginOut.Request;
using JIT.CPOS.DTO.Module.VIP.LoginOut.Response;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.LoginOut
{
    public class RemoveAndriodMessageBindAH : BaseActionHandler<RemoveAndriodMessageBindRP, RemoveAndriodMessageBindRD>
    {
        protected override RemoveAndriodMessageBindRD ProcessRequest(DTO.Base.APIRequest<RemoveAndriodMessageBindRP> pRequest)
        {
            RemoveAndriodMessageBindRD rd = new RemoveAndriodMessageBindRD();

            string userId = pRequest.Parameters.UserId;
            string customerId = pRequest.CustomerID;

            PushAndroidBasicBLL service = new PushAndroidBasicBLL(this.CurrentUserInfo);
            PushAndroidBasicEntity basicInfo = new PushAndroidBasicEntity();
            basicInfo = service.GetByID(userId);
            if (basicInfo == null)
            {
                throw new APIException("用户ID无效") { ErrorCode = 103 };
            }
            else
            {
                service.DeletePushAndroidBasicByUserId(userId);
            }
            return rd;
        }
    }
}