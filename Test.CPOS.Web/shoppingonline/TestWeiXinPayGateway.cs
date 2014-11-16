/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/6 13:56:44
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
using JIT.CPOS.Web.OnlineShopping.data;

namespace Test.CPOS.Web.shoppingonline
{
    /// <summary>
    /// TestWeiXinPayGateway  
    /// </summary>
    [TestFixture]
    public class TestWeiXinPayGateway
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestWeiXinPayGateway()
        {
        }
        #endregion

        [Test]
        public void TestGeneratePackageContent()
        {
            OrderPackage op = new OrderPackage();
            op.TotalAmount = "1";
            op.ClientIP = "127.0.0.1";
            op.OrderNO = "16642817866003386000";

            var pk = op.GeneratePackageContent();
            Assert.IsTrue(pk=="bank_type=WX&body=XXX&fee_type=1&input_charset=GBK&notify_url=http%3a%2f%2fwww.qq.com&out_trade_no=16642817866003386000&partner=1900000109&spbill_create_ip=127.0.0.1&total_fee=1&sign=BEEF37AD19575D92E191C1E4B1474CA9");
        }

        [Test]
        public void TestGenereatePaySign()
        {
            OrderPackage op = new OrderPackage();
            op.TotalAmount = "1";
            op.ClientIP = "127.0.0.1";
            op.OrderNO = "16642817866003386000";

            PreOrderRequest por = new PreOrderRequest();
            por.Package = op;
            por.GeneratePaySign();

            Assert.IsTrue(por.PaySign == "7717231c335a05165b1874658306fa431fe9a0de");
        }
    }
}
