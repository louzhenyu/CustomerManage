using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.WeiXin.News.Request;
using JIT.CPOS.DTO.Module.WeiXin.News.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.News
{
    [TestFixture]
    public class TestAPI
    {
        private const string CustomerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void TestGetNewsList()
        {
            var rp = new GetNewsListRP { NewsTypeId = "1"};

            var request = new APIRequest<GetNewsListRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetNewsListRP, GetNewsListRD>(APITypes.Product, "WX.News.GetNewsList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetNewsTypeList()
        {
            var rp = new GetNewsTypeListRP { };
            
            var request = new APIRequest<GetNewsTypeListRP> { CustomerID = CustomerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetNewsTypeListRP, GetNewsTypeListRD>(APITypes.Product, "WX.News.GetNewsTypeList", request);
            Console.WriteLine(rsp.ToJSON());
        }

    }
}
