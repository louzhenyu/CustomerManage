using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Module.WeiXin.Event.Request;
using JIT.CPOS.DTO.Module.WeiXin.Event.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestBS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;

using JIT.CPOS.DTO.Base;
using NUnit.Framework;

namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.WX.Event
{
    [TestFixture]
    public class TestAPI
    {
        private const string customerId = "e703dbedadd943abacf864531decdac1";

        [Test]
        public void TestGetEventTypeList()
        {
            var rp = new GetEventTypeListRP();
            //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<GetEventTypeListRP> { CustomerID = customerId };
            
            var rsp = APIClientProxy.CallAPI<GetEventTypeListRP, GetEventTypeListRD>(APITypes.Product, "WX.Event.GetEventTypeList", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test]
        public void TestGetEventList()
        {
            var rp = new GetEventListRP { EndFlag = false, BeginFlag = true };

            var request = new APIRequest<GetEventListRP> { CustomerID = customerId };
            request.Parameters = rp;


            var rsp = APIClientProxy.CallAPI<GetEventListRP, GetEventListRD>(APITypes.Product, "WX.Event.GetEventList", request);
            Console.WriteLine(rsp.ToJSON());

        }
        [Test]
        public void TestGetDrawMethodList()
        {
            var rp = new GetDrawMethodListRP { EventTypeId = null };

            var request = new APIRequest<GetDrawMethodListRP> { CustomerID = customerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetDrawMethodListRP, GetDrawMethodListRD>(APITypes.Product, "WX.Event.GetDrawMethodList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetRecommend()
        {
            var rp = new GetRecommendRP { };

            var request = new APIRequest<GetRecommendRP> { CustomerID = customerId, Parameters = rp };

            var rsp = APIClientProxy.CallAPI<GetRecommendRP, GetRecommendRD>(APITypes.Product, "WX.Event.GetRecommend", request);
            Console.WriteLine(rsp.ToJSON());
        }

    }
}
