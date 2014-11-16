using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.Web.ApplicationInterface;
using JIT.CPOS.Web.ApplicationInterface;
using JIT.CPOS.Web.ApplicationInterface.Vip;
using JIT.CPOS.Web.Lj;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Vip
{
     [TestFixture]
    public class VipTest
    {
        string customerId = "e703dbedadd943abacf864531decdac1";


         [Test]
         public void TestGetVipInfo()
         {
             var rp = new EmptyRequestParameter();
             var request = new APIRequest<EmptyRequestParameter>
             {
                 CustomerID = customerId,
                 UserID = "5bacfc5d5e1b4d3685384ba0bccfca04",
                 Parameters = rp
             };

             var rsp = APIClientProxy.CallAPI<EmptyRequestParameter, GetVipInfoRD>(APITypes.Product, "GetVipInfo", request);
             Console.WriteLine(rsp.ToJSON());
         }

         [Test]
         public void GetVipCoupon()
         {
             var rp = new GetVipIntegralRP();
             var paras = new List<SkuIdAndQtyInfo>
             {
                 new SkuIdAndQtyInfo() {SkuId = "e31438aeb3c04dabbd56590bc27aa343", Qty = 1},
                 new SkuIdAndQtyInfo() {SkuId = "5eefddee28304752931169eb96f23c8b", Qty = 2},
                 new SkuIdAndQtyInfo() {SkuId = "7B165FBA093E4583A61F0A02AE5F0663", Qty = 3}
             };
             rp.SkuIdAndQtyList = paras.ToArray();
             var request = new APIRequest<GetVipIntegralRP>
             {
                 CustomerID = customerId,
                 UserID = "5bacfc5d5e1b4d3685384ba0bccfca04",
                 Parameters = rp
             };

             var rsp = APIClientProxy.CallAPI<GetVipIntegralRP, GetVipCouponRD>(APITypes.Product, "GetVipCoupon", request);
             Console.WriteLine(rsp.ToJSON());
         }

         [Test]
         public void TestSetOrderStatus()
         {
             var rp = new SetOrderStatusRP();
             rp.OrderId = "822c6ff06726460d91750238a99ec25d";
             rp.Status = "100";
             rp.CouponId = "3700CF4F31204A25BDE28B064A43913A";
             rp.IntegralFlag = 1;
             rp.CouponFlag = 1;
             rp.VipEndAmount = 500;
             rp.VipEndAmountFlag = 1;
             var request = new APIRequest<SetOrderStatusRP>
             {
                 CustomerID = customerId,
                 UserID = "5bacfc5d5e1b4d3685384ba0bccfca04",
                 Parameters = rp
             };

             var rsp = APIClientProxy.CallAPI<SetOrderStatusRP, EmptyResponseData>(APITypes.Product, "SetOrderStatus", request);
             Console.WriteLine(rsp.ToJSON());
         }


         [Test]
         public void TestCustomerBasicSetting()
         {
             var rp = new EmptyRequestParameter();
             var request = new APIRequest<EmptyRequestParameter>
             {
                 //CustomerID = customerId, //泸州老窖
                 CustomerID="6c1ce52aa43441a3a13c87b41fcafd54", //逸马顾问
                 Parameters = rp
             };
             var rsp = APIClientProxy.CallAPI<EmptyRequestParameter, CustomerBasicSettingRD>(APITypes.Product, "GetCustomerBasicSetting", request);
             Console.WriteLine(rsp.ToJSON());
         }
    }
}
