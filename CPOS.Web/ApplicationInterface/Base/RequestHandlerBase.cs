using System;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Base
{
    public abstract class RequestHandlerBase<TRp, TRd> : IRequestHandler
        where TRp : IAPIRequestParameter, new()
        where TRd : IAPIResponseData, new()
    {
        public string DoAction(string pRequest)
        {
            var rd = new APIResponse<TRd>();
            try
            {
                var req = pRequest.DeserializeJSONTo<APIRequest<TRp>>();
                if (req.Parameters == null)
                {
                    throw new ArgumentException();
                }

                req.Parameters.Validate();

                rd.Data = Operate(req);
                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 101;
            }

            return rd.ToJSON();
        }

        protected virtual TRd Operate(APIRequest<TRp> p)
        {
            return new TRd();
        }
    }
}