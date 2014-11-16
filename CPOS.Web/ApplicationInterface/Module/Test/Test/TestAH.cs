using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Base;

namespace JIT.CPOS.Web.ApplicationInterface.Module.Test.Test
{
    public class TestAH : BaseActionHandler<EmptyRequestParameter, EmptyResponseData>
    {
        protected override EmptyResponseData ProcessRequest(APIRequest<EmptyRequestParameter> pRequest)
        {
            return new EmptyResponseData();
        }
    }
}