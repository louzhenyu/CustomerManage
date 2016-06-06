using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class UpdateSetoffToolsStatusAH : BaseActionHandler<UpdateSetoffToolsStatusRP, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(DTO.Base.APIRequest<UpdateSetoffToolsStatusRP> pRequest)
        {
            var rd = new EmptyResponseData();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var SetoffToolsBll = new SetoffToolsBLL(this.CurrentUserInfo);
            //
            var pTran = SetoffToolsBll.GetTran();
            using (pTran.Connection)
            {
                try
                {
                    foreach (var item in para.SetoffToolIDList)
                    {
                        var Data = SetoffToolsBll.GetByID(item);
                        if (Data == null)
                            throw new APIException("数据异常，找不到集客工具！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        Data.Status="90";

                        SetoffToolsBll.Update(Data,pTran);//
                    }
                    //
                    pTran.Commit();
                }
                catch (Exception ex)
                {
                    pTran.Rollback();
                    throw ex;
                }
            }

            return rd;
        }
    }
}