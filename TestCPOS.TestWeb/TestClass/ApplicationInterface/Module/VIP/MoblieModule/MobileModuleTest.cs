using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Request;
using JIT.CPOS.DTO.Module.VIP.MobileModule.Response;
using JIT.CPOS.DTO.ValueObject;
using JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.Login;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.MoblieModule
{
    [TestFixture]
    public class MobileModuleTest
    {
        public string customerId = "75a232c2cf064b45b1b6393823d2431e";

        [Test]
        public void TestGetForms()
        {
            //new TestMemberLogin().TestSetSignIn();
            var RP = new GetFormsRP();
            RP.Type = 1;
            var request = new APIRequest<GetFormsRP>();
            request.Parameters = RP;
            //request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<GetFormsRP, GetFormsRD>(APITypes.Product, "VIP.MobileModule.GetForms", request);
            Console.WriteLine(rsp.ToJSON());
        }


        [Test]
        public void TestDeleteForm()
        {
            //new TestMemberLogin().TestSetSignIn();
            var RP = new DeleteFormRP();
            RP.MobileModuleID = Guid.NewGuid().ToString();
            var request = new APIRequest<DeleteFormRP>();
            request.Parameters = RP;
            
            //request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<DeleteFormRP, DeleteFormRD>(APITypes.Product, "VIP.MobileModule.DeleteForm", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestReNameForm()
        {
            //new TestMemberLogin().TestSetSignIn();
            var RP = new ReNameFormRP();
            RP.MobileModuleID = "90920285-B3DF-4EE2-AC19-4BC62875646A";
            RP.Name = "test2";
            RP.Type = 1;
            var request = new APIRequest<ReNameFormRP>();
            request.Parameters = RP;
           
            //request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<ReNameFormRP, ReNameFormRD>(APITypes.Product, "VIP.MobileModule.ReNameForm", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestFormSave()
        {
            //new TestMemberLogin().TestSetSignIn();
            var RP = new FormSaveRP();
            RP.MobileModuleID = "90920285-B3DF-4EE2-AC19-4BC62875646A";
            var items = new List<MobileBunessDefinedSubInfo>();
            items.Add( new MobileBunessDefinedSubInfo
            {
                ColumnName="Name",
                ColumnDesc = "姓名--1-1",
                ControlType = 1,
                ListOrder = 1,
            });
            RP.Items = items.ToArray();
            var request = new APIRequest<FormSaveRP>();
            request.Parameters = RP;
            
            //request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<FormSaveRP, FormSaveRD>(APITypes.Product, "VIP.MobileModule.FormSave", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetClientBunessDefined()
        {
            //new TestMemberLogin().TestSetSignIn();
            var RP = new GetClientBusinessDefinedRP();
            RP.Type = 1;
            var request = new APIRequest<GetClientBusinessDefinedRP>();
            request.Parameters = RP;
            
            //request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<GetClientBusinessDefinedRP, GetClientBusinessDefinedRD>(APITypes.Product, "VIP.MobileModule.GetClientBunessDefined", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestGetMobileBunessDefined()
        {
            //new TestMemberLogin().TestSetSignIn();
            var RP = new GetMobileBusinessDefinedRP();
            var request = new APIRequest<GetMobileBusinessDefinedRP>();
            RP.MobileModuleID = Guid.NewGuid().ToString();
            request.Parameters = RP;
            //request.CustomerID = customerId;
            var rsp = APIClientProxy.CallAPI<GetMobileBusinessDefinedRP, GetMobileBusinessDefinedRD>(APITypes.Product, "VIP.MobileModule.GetMobileBunessDefined", request);
            Console.WriteLine(rsp.ToJSON());
        }


    }
}
