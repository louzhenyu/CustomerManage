using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Test.CPOS.Web.Pay
{
    [TestFixture]
    public class TestAPI
    {
        string url = "http://localhost:23130/ApplicationInterface/Pay/WeiXinPay/GetOrderPackage.ashx";
        [Test]
        public void Test()
        {
            string xmlstr = @"<xml><OpenId><![CDATA[oUcanju-XbWR0IJmdF_Y68Kt0szw]]></OpenId>
<AppId><![CDATA[wx8f74386d57405ec5]]></AppId>
<IsSubscribe>1</IsSubscribe>
<ProductId><![CDATA[2rm7WgzkKxEOgNVs6 ow3eQ==]]></ProductId>
<TimeStamp>1398219824</TimeStamp>
<NonceStr><![CDATA[i54wke5H5OYH4epB]]></NonceStr>
<AppSignature><![CDATA[cfddf40a079036f8462f4a455b845df91a50d5c4]]></AppSignature>
<SignMethod><![CDATA[sha1]]></SignMethod>
</xml>";
            var res = JIT.Utility.Web.HttpClient.PostQueryString(url, xmlstr);
            Console.WriteLine(res);
        }
    }
}
