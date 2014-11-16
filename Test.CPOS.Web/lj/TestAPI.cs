using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using System.Web;

namespace Test.CPOS.Web.lj
{
    [TestFixture]
    public class TestAPI
    {
        private string Url = "http://localhost:23130/Lj/Interface/AlbumData.aspx";
        //private string Url = "http://dev.o2omarketing.cn:9004/Lj/Interface/AlbumData.aspx";
        private string customerId = "6c1ce52aa43441a3a13c87b41fcafd54";
        [Test]
        public void getEventAlbum()
        {
            JIT.CPOS.Web.Lj.Interface.AlbumData.GetEventAlbumReqData req = new JIT.CPOS.Web.Lj.Interface.AlbumData.GetEventAlbumReqData();
            req.common = new JIT.CPOS.Web.Default.ReqCommonData() { customerId = customerId };
            req.special = new JIT.CPOS.Web.Lj.Interface.AlbumData.GetEventAlbumReqSpecialData() { eventId = "9BC1C8A73AC8485FA291DCD9DB31312D", eventType = 1 };
            string str = string.Format("action=getEventAlbum&ReqContent={0}", req.ToJSON());
            str = HttpUtility.UrlDecode("action=getEventAlbum&ReqContent={\"common\":{\"openId\":\"oxbbcjjeVHRZ-b37MVEgVBcGvrtk\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"userId\":\"884a6003a00949859bc653b21417ba19\",\"locale\":\"ch\"},\"special\":{\"action\":\"getEventAlbum\",\"eventId\":\"3DD35B9A122F41C8A0E5D5B78D72CE65\",\"eventType\":\"1\"}}");
            var res = HttpClient.PostQueryString(Url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void Test()
        {
            string str = HttpUtility.UrlDecode("action=getItemDetail&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%222a717133725b463a948a7467d67d4873%22%2C%22userId%22%3A%222a717133725b463a948a7467d67d4873%22%2C%22Locale%22%3A1%7D%2C%22special%22%3A%7B%22itemId%22%3A%2220DA4DC9899A48E6885E5309F1E68EA6%22%7D%7D");
            var res = HttpClient.PostQueryString(Url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestSaveAdsAPI()
        {
            string str = HttpUtility.UrlDecode("method=SaveAds&adList=[{\"adAreaId\":\"91901874-2896-4AE3-9F32-49B3444F091F\",\"objectId\":\"B48576B4-D0C2-4B2C-9279-831E161B0627\",\"ObjectTypeId\":\"1\",\"displayIndex\":\"1\",\"imageUrl\":\"http://dev.1534_1343.png\"},{\"adAreaId\":\"452C3566-7AB9-40D3-9EB3-78934D48E3EF\",\"objectId\":\"B48576B4-D0C2-4B2C-9279-831E161B0627\",\"ObjectTypeId\":\"1\",\"displayIndex\":\"2\",\"imageUrl\":\"http://dev.15134_1343.png\"}]");
            var res = HttpClient.PostQueryString("http://localhost:55142/Module/AppConfig/Handler/HomePageHandler.ashx", str);
            Console.WriteLine(res);
        }
    }
}
