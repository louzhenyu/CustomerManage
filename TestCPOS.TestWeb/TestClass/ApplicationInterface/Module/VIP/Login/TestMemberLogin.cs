using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.Login
{
   [TestFixture]
   public class TestMemberLogin
    {
       public string customerId = "6c1ce52aa43441a3a13c87b41fcafd54";
         [Test]
         public void MemberLoginAH()
         {
             var RP = new MemberLoginRP();
             RP.VipNo = "10d57b738e8949a88500a113e36ae55f";
             RP.Password = "111111";
             var request = new APIRequest<MemberLoginRP>();
             request.CustomerID = customerId;
             request.Parameters = RP;
             var rsp = APIClientProxy.CallAPI<MemberLoginRP, MemberLoginRD>(DTO.ValueObject.APITypes.Product, "VIP.Login.MemberLogin", request);
             Console.WriteLine(rsp.ToJSON());         
         }


         [Test]
         public void TestSetSignIn()
         {
             var RP = new SetSignInRP();
             RP.CustomerCode = "alading";
             RP.LoginName = "18601659608";
             RP.Password = "e10adc3949ba59abbe56e057f20f883e";
             var request = new APIRequest<SetSignInRP>();
             request.CustomerID = "";
             request.Parameters = RP;
             var rsp = APIClientProxy.CallAPI<SetSignInRP, SetSignInRD>(DTO.ValueObject.APITypes.Product, "VIP.Login.SetSignIn", request);
             Console.WriteLine(rsp.ToJSON());
         }

       [Test]
       public void TestGetMemberInfo()
       {
            var rp = new GetMemberInfoRP();
           
             var request = new APIRequest<GetMemberInfoRP>();
             request.CustomerID = "e703dbedadd943abacf864531decdac1";
             request.UserID = "4862f9082dca4ce09fdb9bbc936f6c65";
             request.Parameters = rp;
             var rsp = APIClientProxy.CallAPI<GetMemberInfoRP, GetMemberInfoRD>(DTO.ValueObject.APITypes.Product, "VIP.Login.GetMemberInfo", request);
             Console.WriteLine(rsp.ToJSON());
           
       }



       [Test]
       public void TestGetMemberInfo1()
       {
           var rp = new GetMemberInfoRP();

           var request = new APIRequest<GetMemberInfoRP>();
           request.CustomerID = "e703dbedadd943abacf864531decdac1";
           request.UserID = "5404a629f0604edbb8244686d4e9ed8c";
           request.Parameters = rp;
           var rsp = APIClientProxy.CallAPI<GetMemberInfoRP, GetMemberInfoRD>(DTO.ValueObject.APITypes.Product, "VIP.Login.GetMemberInfo", request);
           Console.WriteLine(rsp.ToJSON());

       }
    }
}
