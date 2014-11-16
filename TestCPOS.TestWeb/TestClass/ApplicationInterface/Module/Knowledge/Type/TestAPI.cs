using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.Knowledge.Type.Request;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Knowledge.Type.Response;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Knowledge.Type
{
    [TestFixture]
    public class TestAPI
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void GetTypeList()
        {
            var RP = new GetTypeListRP();
            var request = new APIRequest<GetTypeListRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetTypeListRP, GetTypeListRD>(APITypes.Product, "Knowledge.Type.GetTypeList", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
