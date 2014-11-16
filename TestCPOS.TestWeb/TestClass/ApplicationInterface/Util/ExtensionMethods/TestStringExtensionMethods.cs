/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/26 16:23:13
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

using NUnit.Framework;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface.Util.ExtensionMethods;
using JIT.CPOS.DTO.Module.Order.Order.Request;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.CPOS.DTO.ValueObject;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Util.ExtensionMethods
{
    /// <summary>
    /// TestStringExtensionMethods  
    /// </summary>
    [TestFixture]
    public class TestStringExtensionMethods
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestStringExtensionMethods()
        {
        }
        #endregion

        [Test]
        public void TestDeserializeJSONToAPIRequest()
        {
            var json1 = "{\"CustomerID\":\"abcdefgh\",\"Locale\":\"zh_CN\",\"Token\":\"t1111\",\"Parameters\":{\"PayChannelID\":1,\"OrderID\":\"O12345678\",\"OrderDesc\":\"测试订单\",\"MobileNO\":\"13817218367\"}}";
            var req1 = json1.DeserializeJSONToAPIRequest<OrderPayRP>();
            Assert.IsTrue(req1 != null);
            Assert.IsTrue(req1.CustomerID == "abcdefgh");
            Assert.IsTrue(req1.Locale == "zh_CN");
            Assert.IsTrue(req1.Token == "t1111");
            Assert.IsTrue(req1.Parameters != null);
            Assert.IsTrue(req1.Parameters.PayChannelID == 1);
            Assert.IsTrue(req1.Parameters.OrderID == "O12345678");
            Assert.IsTrue(req1.Parameters.OrderDesc == "测试订单");
            Assert.IsTrue(req1.Parameters.MobileNO == "13817218367");
        }

        [Test]
        public void TestToJSON()
        {
            var rsp1 = new APIResponse<OrderPayRD>();
            rsp1.ResultCode = 200;
            rsp1.Message = "成功";
            rsp1.Data = new OrderPayRD();
            rsp1.Data.OrderID = "O12345678";
            rsp1.Data.PayUrl = "http://www.jitmarketing.cn/orderpay?oid=O12345678";

            var json1 = rsp1.ToJSON();
            Assert.IsTrue(string.IsNullOrWhiteSpace(json1) == false);
        }
    }
}
