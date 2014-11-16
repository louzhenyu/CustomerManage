using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.Event.EventPrizes.Request;
using JIT.CPOS.DTO.Module.Event.EventPrizes.Response;


namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Event.EventPrizes
{
    [TestFixture]
    public class TestAPI
    {
        string customerId = "e703dbedadd943abacf864531decdac1";//gift
        [Test]
        public void TestGetEventPrizesList()
        {
            var RP = new GetEventPrizesRP();
            var request = new APIRequest<GetEventPrizesRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;

            request.Parameters.EventId = "17614ef0d19db89bb6d9a12fb8117faa";
            request.Parameters.Longitude = 1.00f;
            request.Parameters.Latitude = 1.2f;
            var rsp = APIClientProxy.CallAPI<GetEventPrizesRP, GetEventPrizesRD>(APITypes.Product, "Event.EventPrizes.GetEventPrizes", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}

