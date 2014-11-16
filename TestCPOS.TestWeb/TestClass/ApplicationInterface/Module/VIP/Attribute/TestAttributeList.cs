using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.DTO.Module.VIP.Attribute.Request;
using JIT.CPOS.DTO.Module.VIP.Attribute.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.Attribute
{
    [TestFixture]
    public class TestAttributeList
    {
        public string customerId = "75a232c2cf064b45b1b6393823d2431e";

        [Test]
        public void TestGetAttributeList()
        {
            var RP = new GetAttributeListRP(); 
            var request = new APIRequest<GetAttributeListRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetAttributeListRP, GetAttributeListRD>(DTO.ValueObject.APITypes.Product,
                "VIP.Attribute.GetAttributeList", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestSetAttribute()
        {
            var RP = new SetAttributeRP();
           // RP.AttributeFormID = Guid.Parse("B68F8A5A-085C-412C-A6D2-05D212B4B91A");
            RP.AttributeTypeID = 3;
            RP.Name = "测试";
            RP.Sequence = 10000;
            RP.OptionRemark = "选项1,选项2";
            RP.Remark = "测试信息";
            var request = new APIRequest<SetAttributeRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<SetAttributeRP, SetAttributeRD>(DTO.ValueObject.APITypes.Product,
                "VIP.Attribute.SetAttribute", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetForms()
        {
            // new TestMemberLogin().TestSetSignIn();
            var RP = new GetFormsRP();
            var request = new APIRequest<GetFormsRP>();
            request.Parameters = RP;
            request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<GetFormsRP, JIT.CPOS.DTO.Module.VIP.MobileModule.Response.GetFormsRD>(DTO.ValueObject.APITypes.Product, "VIP.MobileModule.GetForms", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
