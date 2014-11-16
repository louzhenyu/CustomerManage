using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.Order.Response;
using JIT.CPOS.DTO.Module.VIP.Order.Request;


namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.Order
{
    [TestFixture]
    public class TestAPI
    {
        public string customerId = "e703dbedadd943abacf864531decdac1";
        public string vip_no = "add73ef71c2c480c89b5a6941cb0dfc9";
        [Test]
        public void GetOrdersAH()
        {
            var RP = new GetOrdersRP();
            RP.GroupingType = 1;
            RP.PageIndex = 1;
            RP.PageSize = 10;
            var request = new APIRequest<GetOrdersRP>();
            request.CustomerID = customerId;
            request.UserID = vip_no;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetOrdersRP, GetOrdersRD>(APITypes.Product, "VIP.Order.GetOrders", request);
            Console.WriteLine(rsp.ToJSON());
        }

    }
}
