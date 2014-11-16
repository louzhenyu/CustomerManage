
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using JIT.CPOS.DTO.Module.Sys.InterfaceLog.Request;
using JIT.CPOS.DTO.Module.Sys.InterfaceLog.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Sys.InterfaceLog
{
    [TestFixture]
   public class TestAPI
    {
        public string customerId = "e703dbedadd943abacf864531decdac1";

        [Test]
        public void RecordInterfaceLogAH()
        {
            var RP = new RecordInterfaceLogRP();
            RP.PageName = "RecordInterfaceLog";
            RP.Action = "Access";
            var request = new APIRequest<RecordInterfaceLogRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<RecordInterfaceLogRP,RecordInterfaceLogRD>(DTO.ValueObject.APITypes.Product, "Sys.InterfaceLog.RecordInterfaceLog", request);
            Console.WriteLine(rsp.ToJSON());  
        }
    }
}
