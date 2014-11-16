using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using NUnit.Framework;
using JIT.CPOS.DTO.Module.VIP.Login.Request;
using JIT.CPOS.DTO.Module.VIP.Login.Response;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.CPOS.DTO.ValueObject;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.VIP.Login
{
    [TestFixture]
    public class TestLogin
    {
        //public string customerId = "29E11BDC6DAC439896958CC6866FF64E";
        public string customerId = "8c6979db4acb4dd3909f5be67f433e67";

        public string AuthCode = string.Empty;
        [Test]
        public void TestGetAuthCode()
        {
            //1st
            var RP = new GetAuthCodeRP();
            //RP.Mobile = "18019438327";
            RP.Mobile = "15856973921";
            var request = new APIRequest<GetAuthCodeRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<GetAuthCodeRP, GetAuthCodeRD>(APITypes.Product, "VIP.Login.GetAuthCode", request);
            Assert.IsTrue(rsp.IsSuccess);
            
            Console.WriteLine(rsp.ToJSON());

            //2st
            //var req = HttpUtility.UrlDecode("%7B%22Locale%22%3Anull%2C%22CustomerID%22%3A%2257374a5f53f54191a728810618ec8276%22%2C%22UserID%22%3A%228f5595ac7c3541f39be5e7c6ff415d04%22%2C%22OpenID%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22Token%22%3Anull%2C%22Parameters%22%3A%7B%22Mobile%22%3A%2218616153083%22%7D%7D").DeserializeJSONTo<APIRequest<GetAuthCodeRP>>();
            //var rsp = APIClientProxy.CallAPI<GetAuthCodeRP, GetAuthCodeRD>(APITypes.Product, "VIP.Login.GetAuthCode", req);
            //Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestAuthCodeLogin()
        {
            //var RP = new AuthCodeLoginRP();
            //RP.AuthCode = "755288";
            //RP.Mobile = "18019438327";
            //RP.VipSource = 3;
            //var request = new APIRequest<AuthCodeLoginRP>();
            //request.CustomerID = customerId;
            //request.Parameters = RP;

            var request = "{\"Locale\":null,\"CustomerID\":\"8c6979db4acb4dd3909f5be67f433e67\",\"UserID\":\"062d8012f9bd4169b64c9ee0a8ede991\",\"OpenID\":\"oRBZQuHgInP2aE00zbIf8zx25zdY\",\"Token\":null,\"Parameters\":{\"Mobile\":\"18019438327\",\"AuthCode\":\"739986\",\"VipSource\":3}}".DeserializeJSONTo<APIRequest<AuthCodeLoginRP>>();

            var rsp = APIClientProxy.CallAPI<AuthCodeLoginRP, AuthCodeLoginRD>(APITypes.Product, "VIP.Login.AuthCodeLogin", request);
            Console.WriteLine(rsp.ToJSON());
        }

        [Test]
        public void TestChangePWD()
        {
            var RP = new ChangeVipPWDRP();
            RP.VipID = "10d57b738e8949a88500a113e36ae55f";
            RP.SourcePWD = "123456";
            RP.NewPWD = "111111";
            var request = new APIRequest<ChangeVipPWDRP>();
            request.CustomerID = customerId;
            request.Parameters = RP;
            var rsp = APIClientProxy.CallAPI<ChangeVipPWDRP, ChangeVipPWDRD>(APITypes.Product, "VIP.Login.ChangeVipPwd", request);
            Console.WriteLine(rsp.ToJSON());
        }
    }
}
