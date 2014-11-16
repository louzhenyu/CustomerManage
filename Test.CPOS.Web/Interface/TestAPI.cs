/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/5/9 16:03:02
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
using JIT.Utility.Web;
using JIT.Utility.ExtensionMethod;
using System.Web;

namespace Test.CPOS.Web.Interface
{
    /// <summary>
    /// TestAPI  
    /// </summary>
    [TestFixture]
    public class TestAPI
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestAPI()
        {
        }
        #endregion


        private string Url = "http://localhost:23130/Interface/Data/OrderData.aspx";

        [Test]
        public void TestSetPayOrder()
        {
            var str = HttpUtility.UrlDecode("Action=setPayOrder&ReqContent={\"common\":{\"deviceToken\":\"\",\"osInfo\":\"6.1\",\"userId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"sessionId\":\"\",\"openId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"channel\":\"1\",\"locale\":\"zh\",\"version\":\"1.5.2\",\"customerId\":\"f6a7da3d28f74f2abedfc3ea0cf65c01\",\"plat\":\"iPhone\"},\"special\":{\"unitId\":\"8c41446fe80d4f2e9e3d659df01641fa\",\"dataFromId\":\"2\",\"paymentId\":\"318529D4E4F24AEFBBE5B26191A3B584\",\"dynamicIdType\":\"\",\"returnUrl\":\"\",\"OrderID\":\"0e8180dfb4b7473eb1a45d48f6a800b4\",\"amount\":\"8800.00\",\"dynamicId\":\"\",\"orderDesc\":\"jitmarketing\",\"mobileNo\":\"\"}}");
            var res = HttpClient.PostQueryString(Url, str);
            Console.WriteLine(res);
        }
    }
}
