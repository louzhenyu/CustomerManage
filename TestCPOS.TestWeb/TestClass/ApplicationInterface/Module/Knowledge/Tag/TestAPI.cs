using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.Knowledge.Tag.Request;
using JIT.CPOS.DTO.Module.Knowledge.Tag.Response;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Knowledge.Tag
{
    [TestFixture]
    public class TestAPI
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void GetHotTags()
        {
            var RP = new GetHotTagsRP();
            var request = new APIRequest<GetHotTagsRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetHotTagsRP, GetHotTagsRD>(APITypes.Product, "Knowledge.Tag.GetHotTags", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetHotTags()
        {
            var rsp = APIClientProxy.CallAPI("type=Product&action=Knowledge.Tag.GetHotTags", "req=%7B%22Locale%22%3Anull%2C%22CustomerID%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22UserID%22%3A%228f3b4eb9733e45bc9982a86d81302048%22%2C%22OpenID%22%3A%22oxbbcjvo1rNFZq5s4GfbgBXEKOdc%22%2C%22Token%22%3Anull%2C%22Parameters%22%3A%7B%22page%22%3A0%2C%22pageSize%22%3A99%7D%7D");
            Assert.IsTrue(string.IsNullOrWhiteSpace(rsp) == false);
        }
    }
}
