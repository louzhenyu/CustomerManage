using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.Account.Request;
using JIT.CPOS.DTO.Module.WeiXin.Account.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.Account
{
    [TestFixture]
    public class TestAPI
    {
        private const string CustomerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void TestGetAccountList()
        {
            var rp = new GetAccountListRP();
            //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<GetAccountListRP> { CustomerID = CustomerId };


            var rsp = APIClientProxy.CallAPI<GetAccountListRP, GetAccountListRD>(APITypes.Product,
                "WX.Account.GetAccountList", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
