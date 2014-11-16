using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.VIP.LoginOut.Request;
using JIT.CPOS.DTO.Module.VIP.LoginOut.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.LoginOut
{
    [TestFixture]
    public class TestAPI
    {
        public string customerId = "e703dbedadd943abacf864531decdac1";

        [Test]
        public void TestIOSRemoveBind()
        {
            var RP = new RemoveIOSMessageBindRP();
            RP.UserId = "0290078CF9964091950298AEE27838CC";
            var request = new APIRequest<RemoveIOSMessageBindRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<RemoveIOSMessageBindRP, RemoveIOSMessageBindRD>(DTO.ValueObject.APITypes.Product,
                "VIP.LoginOut.RemoveIOSMessageBind", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestAndriodRemoveBind()
        {
            var RP = new RemoveAndriodMessageBindRP();
            RP.UserId = "3245efba12f04a639e116f0b474e018e";
            var request = new APIRequest<RemoveAndriodMessageBindRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<RemoveAndriodMessageBindRP, RemoveAndriodMessageBindRD>(DTO.ValueObject.APITypes.Product,
                "VIP.LoginOut.RemoveAndriodMessageBind", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
