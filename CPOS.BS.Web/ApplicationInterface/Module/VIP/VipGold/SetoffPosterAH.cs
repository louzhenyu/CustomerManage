using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.VipGold.Request;
using JIT.CPOS.DTO.Module.VIP.VipGold.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VipGold
{
    public class SetoffPosterAH : BaseActionHandler<SetoffPosterRP, SetoffPosterRD>
    {
        protected override SetoffPosterRD ProcessRequest(DTO.Base.APIRequest<SetoffPosterRP> pRequest)
        {
            var rd = new SetoffPosterRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var SetoffPosterBll = new SetoffPosterBLL(this.CurrentUserInfo);
            var ObjectImagesBll = new ObjectImagesBLL(this.CurrentUserInfo);
            //
            var pTran = SetoffPosterBll.GetTran();
            var Data = SetoffPosterBll.GetByID(para.SetoffPosterID);
            if(Data==null)
                throw new APIException("数据异常，找不到集客海报！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
            using (pTran.Connection)
            {
                try
                {
                    //图片表
                    var ObjectImageData = ObjectImagesBll.GetByID(Data.ImageId);
                    if (Data == null)
                        throw new APIException("数据异常，找不到集客海报Url！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS }; 
                  
                    ObjectImageData.ImageURL = para.ImageUrl;
                    ObjectImagesBll.Update(ObjectImageData, pTran);//

                    Data.Name = para.Name;
                    Data.ImageId = ObjectImageData.ImageId;
                    SetoffPosterBll.Update(Data, pTran);//

                    //
                    pTran.Commit();
                    //
                    rd.SetoffPosterID = Data.SetoffPosterID.ToString();
                    rd.Name = Data.Name;

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