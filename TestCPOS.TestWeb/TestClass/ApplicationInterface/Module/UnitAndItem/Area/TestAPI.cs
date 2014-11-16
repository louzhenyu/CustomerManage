using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.UnitAndItem.Area;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.Module.UnitAndItem.Area.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.UnitAndItem.Item.Request;
using JIT.CPOS.DTO.Module.UnitAndItem.Item.Response;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.UnitAndItem.Area
{
    [TestFixture]
    public class TestAPI
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void TestGetCityList()
        {
            var RP = new GetCityListRP();
          //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<GetCityListRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetCityListRP, GetCityListRD>(APITypes.Product, "UnitAndItem.Area.GetCityList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestPraiseItem()
        {
            var RP = new PraiseItemRP();
            //  RP.ID = new Guid("e19a3760-4d0c-40f4-b295-98d31c07abc0");
            var request = new APIRequest<PraiseItemRP>();
            request.CustomerID = customerId;
            RP.ItemID = "849BC8B35D3A41AE9863800B832EBEA4";
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<PraiseItemRP, PraiseItemRD>(APITypes.Product, "UnitAndItem.Item.PraiseItem", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
