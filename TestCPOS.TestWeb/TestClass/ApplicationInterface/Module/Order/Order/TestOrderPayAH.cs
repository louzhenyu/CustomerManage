/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/27 10:53:57
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
using System.Text;
using System.Web;

using NUnit.Framework;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Order.Order
{
    /// <summary>
    /// TestOrderPayHandler  
    /// </summary>
    [TestFixture]
    public class TestOrderPayAH
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestOrderPayAH()
        {
        }
        #endregion

        [Test]
        public void TestProcessRequest()
        {
            APIRequest<OrderPayRP> req = new APIRequest<OrderPayRP>();
            req.Parameters.MobileNO = "13011111111";
            req.Parameters.OrderDesc = "测试订单";
            req.Parameters.OrderID = "Ord0001";
            req.Parameters.PayChannelID = 1;
            string queryString = string.Format("?type=Product&action=Order.Order.OrderPay&req={0}",HttpUtility.UrlEncode(req.ToJSON()));
            var strRsp1 = APIClientProxy.CallAPI(queryString, string.Empty);
            Assert.IsTrue(string.IsNullOrWhiteSpace(strRsp1) == false);
            var rsp1 = strRsp1.DeserializeJSONTo<APIResponse<OrderPayRD>>();
            Assert.IsTrue(rsp1 != null);
            Assert.IsTrue(rsp1.IsSuccess);
            Assert.IsTrue(rsp1.Data.OrderID == req.Parameters.OrderID);
        }
        [Test] //Add by Alex Tian 2014-04-16  获取订单列表
        public void TestGetOrderList()
        {
            GetOrderListRP req = new GetOrderListRP();
            req.IsIncludeOrderDetails = true;
            //req.OrderID = "0b47f6bbf5cc41d38abd0f45fb528cd5";
            req.OrderStatuses =new int[]{100,500,700};
            //req.VIPID = "0290078CF9964091950298AEE27838CC";
            req.PageIndex = 0;
            req.PageSize = 15;
            var request = new APIRequest<GetOrderListRP>();
            request.CustomerID = "e703dbedadd943abacf864531decdac1";
            request.Parameters = req;
            var rsp = APIClientProxy.CallAPI<GetOrderListRP, GetOrderListRD>(DTO.ValueObject.APITypes.Product, "Order.Order.GetOrderList", request);
            Console.WriteLine(rsp.ToJSON());    
        }
        [Test] //Add by changjian.tian 2014-04-21  获取订单可执行操作
        public void TestGetOrderActions()
        {
            GetOrderActionsRP req = new GetOrderActionsRP();
            req.OrderID = "031986449dad4ee48faf8187061a66f5";
            var request = new APIRequest<GetOrderActionsRP>();
            request.CustomerID = "e703dbedadd943abacf864531decdac1";
            request.Parameters = req;
            var rsp = APIClientProxy.CallAPI<GetOrderActionsRP, GetOrderActionsRD>(DTO.ValueObject.APITypes.Product, "Order.Order.GetOrderActions", request);
            Console.WriteLine(rsp.ToJSON());
        }
        [Test] //Add by changjian.tian 2014-04-22  订单-执行订单操作
        public void TestProcessAction()
        {
            ProcessActionRP RP = new ProcessActionRP();
            RP.OrderID = "0057e0e9ae944868b70a0aaf477e1f4e";
            RP.ActionCode = "500";
            var request = new APIRequest<ProcessActionRP>();
            request.CustomerID = "e703dbedadd943abacf864531decdac1";
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<ProcessActionRP, ProcessActionRD>(DTO.ValueObject.APITypes.Product, "Order.Order.ProcessAction", request);
            Console.WriteLine(rsp.ToJSON());
        }


        [Test] 
        public void TestGetOrderListByUserId()
        {
            GetOrderListByUserIdRP RP = new GetOrderListByUserIdRP();
            //RP.OrderID = "0057e0e9ae944868b70a0aaf477e1f4e";
            RP.OrderID = "6961688b2b674bd295a5b39cec57c49c";
            
            RP.PageIndex = 0;
            RP.PageSize = 10;
            var request = new APIRequest<GetOrderListByUserIdRP>();
            request.CustomerID = "13010a73777d4db2b2a84f01f6f29de0";
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetOrderListByUserIdRP, GetOrderListByUserIdRD>(DTO.ValueObject.APITypes.Product, "Order.Order.GetOrderListByUserId", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetOrderDetailLIst()
        {
            GetOrderDetailRP RP = new GetOrderDetailRP();
            RP.OrderId = "8a92f7ff42614f63bf9e4d1803eff044";
            var request = new APIRequest<GetOrderDetailRP>();
            request.CustomerID = "7ba0d0bc2c13403892deb6499d2c7266";
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetOrderDetailRP, GetOrderDetailRD>(DTO.ValueObject.APITypes.Product, "Order.Order.GetOrderDetail", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetOrderActions2()
        {
            GetOrderActionsRP rp = new GetOrderActionsRP();
            rp.OrderID = "a65f449a5abd49d8a3b012acf20c64b8";
            var request = new APIRequest<GetOrderActionsRP>();
            request.CustomerID = "86a575e616044da3ac2c3ab492e44445";
            request.Parameters = rp;

            var rsp = APIClientProxy.CallAPI<GetOrderActionsRP, GetOrderActionsRD>(DTO.ValueObject.APITypes.Product, "Order.Order.GetOrderActions", request);
            Console.WriteLine(rsp.ToJSON());
        }

    }
}
