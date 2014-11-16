/*
 * Author		:Alex.Tian  
 * EMail		:changjian.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/18 13:11
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using NUnit.Framework;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.DTO.Module.Order.Delivery.Request;
using JIT.CPOS.DTO.Module.Order.Delivery.Response;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Order.Delivery
{
    [TestFixture]
   public class TestAPI
    {
        string customerId = "e703dbedadd943abacf864531decdac1";
        [Test]
        public void GetOrderDeliveryRangeAH()
        { 
            var RP = new GetOrderDeliveryTimeRangeRP();
            var request = new APIRequest<GetOrderDeliveryTimeRangeRP>();
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetOrderDeliveryTimeRangeRP, GetOrderDeliveryTimeRangeRD>(APITypes.Product, "Order.Delivery.GetOrderDeliveryTimeRange", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]  //add by changjian.tian 2014-04-23
        public void UpdateDeliveryInfo()
        {
            var RP = new UpdateOrderDeliveryInfoRP();
            var request = new APIRequest<UpdateOrderDeliveryInfoRP>();
            RP.OrderID = "0057e0e9ae944868b70a0aaf477e1f4e";
            //RP.DeliveryTypeID = 2;
            RP.Email = "test@163.com";  //邮箱对应字段Filed5
            RP.Mobile = "15221682882";  //电话对应Filed6
            RP.ReceiverAddress = "北京市北京市门头沟区地址Test"; //送货地址对应字段Filed4
            request.Parameters = RP;
            request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<UpdateOrderDeliveryInfoRP, UpdateOrderDeliveryInfoRD>(APITypes.Product, "Order.Delivery.UpdateOrderDeliveryInfo", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
