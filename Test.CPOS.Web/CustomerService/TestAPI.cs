/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/29 11:59:34
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
using System.Web;

using NUnit.Framework;
using JIT.Utility.Web;

namespace Test.CPOS.Web.CustomerService
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


        private string Url = "http://localhost:23130/CustomerService/Data.aspx";

        [Test]
        public void TestSendMessage()
        {
            //string req = HttpUtility.UrlDecode("action=sendMessage&reqContent=%7B%22common%22%3A%7B%22openId%22%3A%22%22%2C%22userId%22%3A%22007D78BE87D84137A38545B41D378DEF%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22locale%22%3A%22%22%7D%2C%22special%22%3A%7B%22messageContent%22%3A%22Ttttttttt%22%2C%22csPipelineId%22%3A3%2C%22serviceTypeId%22%3A%22%22%2C%22messageId%22%3A%2258ac3071-3892-4bf3-a4d4-9c8e74778fac%22%2C%22objectId%22%3A%22%22%2C%22messageTypeId%22%3A%22%22%2C%22isCS%22%3A1%7D%7D");
            string req = HttpUtility.UrlDecode("action=sendMessage&reqContent=%7B%22common%22%3A%7B%22openId%22%3A%22%22%2C%22userId%22%3A%22007D78BE87D84137A38545B41D378DEF%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22locale%22%3A%22%22%7D%2C%22special%22%3A%7B%22messageContent%22%3A%22Tutu%22%2C%22csPipelineId%22%3A3%2C%22serviceTypeId%22%3A%22%22%2C%22messageId%22%3A%2258ac3071-3892-4bf3-a4d4-9c8e74778fac%22%2C%22objectId%22%3A%22%22%2C%22messageTypeId%22%3A5%2C%22isCS%22%3A1%7D%7D");
            
            var res = HttpClient.PostQueryString(Url, req);
            Assert.IsTrue(res != null);
        }
        //[]
        public void TestSendMessage2()
        {
        }
    }
}
