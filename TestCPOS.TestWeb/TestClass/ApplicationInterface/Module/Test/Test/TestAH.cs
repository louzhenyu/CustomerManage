using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using NUnit.Framework;
using JIT.CPOS.TestCPOS.TestWeb.TestMaterial;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.ValueObject;
using System.Diagnostics;
using System.Web;

namespace JIT.CPOS.TestCPOS.TestWeb.TestClass.ApplicationInterface.Module.Test.Test
{
    [TestFixture]
    public class TestAPI
    {
        [Test]
        public void TestAH()
        {
            Dictionary<int, long> times1 = new Dictionary<int, long> { };
            Dictionary<int, long> times2 = new Dictionary<int, long> { };
            for (int i = 0; i < 100; i++)
            {
                var request = new APIRequest<EmptyRequestParameter>();
                request.Parameters = new EmptyRequestParameter();
                Stopwatch stop = new Stopwatch();
                stop.Start();
                var rsp = APIClientProxy.CallAPI<EmptyRequestParameter, EmptyResponseData>(APITypes.Product, "Test.Test.Test", request);
                stop.Stop();
                times1.Add(i, stop.ElapsedMilliseconds);
            }
            string url = "http://localhost:23130/OnlineShopping/data/Data.aspx";
            var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
            for (int i = 0; i < 100; i++)
            {
                Stopwatch stop = new Stopwatch();
                stop.Start();
                var res = JIT.Utility.Web.HttpClient.PostQueryString(url, str);
                stop.Stop();
                times2.Add(i, stop.ElapsedMilliseconds);
            }
            var avg1 = times1.Sum(t => t.Value) / times1.Keys.Count;
            Console.WriteLine(times1.ToJSON());
            Console.WriteLine("反射调用平均耗时:{0}", avg1);
            var avg2 = times2.Sum(t => t.Value) / times2.Keys.Count;
            Console.WriteLine(times2.ToJSON());
            Console.WriteLine("未使用反射平均耗时:{0}",avg2);
            //Console.WriteLine(rsp.ToJSON());
        }

        public void Test()
        {
            string url = "http://localhost:23130/OnlineShopping/data/Data.aspx";
            var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
            var res = JIT.Utility.Web.HttpClient.PostQueryString(url, str);
            Console.WriteLine(res);
        }

    }
}
