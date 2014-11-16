using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.Web.Interface.Data.Base;
using System.Net;
using System.IO;
using JIT.Utility.ExtensionMethod;
using System.Web;

namespace Test.CPOS.Web.Event
{
    [TestFixture]
    public class TestAPI
    {
        //string url = "http://112.124.68.147:9004/Interface/Data/ItemData.aspx";
        string url = "http://localhost:23130/Interface/Data/ItemData.aspx";
        string temporary_url = "http://localhost:23130/Interface/Data/Temporary.aspx";
        //string customerid = "6c1ce52aa43441a3a13c87b41fcafd54";
        string customerid = "86a575e616044da3ac2c3ab492e44445";
        [Test]
        public void getPanicbuyingItemList()
        {
            APIRequest request = new APIRequest();
            request.common = new CommonReqPara();
            request.common.customerId = customerid;

            GetPanicbuyingItemListReqPara para = new GetPanicbuyingItemListReqPara();
            para.eventTypeId = "2";
            para.page = "1";
            para.pageSize = "10";
            request.special = para;

            string json = string.Format("action=getPanicbuyingItemList&ReqContent={0}", request.ToJSON());
            //string json = "action=getPanicbuyingItemList&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%226c1ce52aa43441a3a13c87b41fcafd54%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22getPanicbuyingItemList%22%2C%22eventTypeId%22%3A2%2C%22eventId%22%3A%22188660f3-6aa9-4686-a3d0-1c1d2a9772ee%22%2C%22page%22%3A1%2C%22pageSize%22%3A99%7D%7D";

            json = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%227d882214265a4c41b1ab607adfef3c9f%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%227d882214265a4c41b1ab607adfef3c9f%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22isGroupBy%22%3A0%2C%22joinNo%22%3A1%2C%22qty%22%3A1%2C%22username%22%3A%22%22%2C%22couponsPrompt%22%3A%22%22%2C%22deliveryId%22%3A%222%22%2C%22deliveryTime%22%3A%2213%3A44%3A04%22%2C%22eventId%22%3A%22061e0a01-3ec6-4471-bdf7-9ba9e6db4638%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A1%2C%22totalAmount%22%3A229.0%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3A%222014-04-01+00%3A00%3A00%22%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3A%222015-04-03+00%3A00%3A00%22%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%3A8400%2FFramework%2FUpload%2FImage%2F20140321%2F7ACDEEB3A26E479397D82F2FB38C89E4.png%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%221664035A8087491BB2B74D776334BF38%22%2C%22itemName%22%3A%22%E8%BF%9E%E9%94%81%E5%AF%86%E7%A0%81%E4%B9%A6%22%2C%22price%22%3A229.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A194.65%2C%22selDate%22%3Anull%2C%22skuId%22%3A%227716d8389245544842456bc7f2b88d9e%22%2C%22selected%22%3Atrue%7D%5D%2C%22actualAmount%22%3A194.65%2C%22storeId%22%3Anull%2C%22deliveryAddress%22%3A%22%22%2C%22isPanicbuying%22%3A1%2C%22mobile%22%3A%22%22%");

            var rsp = SendHttpRequest(url, json);
            Console.WriteLine(rsp);
        }

        [Test]
        public void getPanicbuyingItemDetail()
        {
            APIRequest request = new APIRequest();
            request.common = new CommonReqPara();
            request.common.customerId = "6c1ce52aa43441a3a13c87b41fcafd54";

            GetPanicbuyingItemDetailReqPara para = new GetPanicbuyingItemDetailReqPara();
            para.eventId = new Guid("188660F3-6AA9-4686-A3D0-1C1D2A9772EE");
            para.itemId = "0B71FF47A6634BC69598FBEA18B66010";
            request.special = para;

            string json = string.Format("action=getPanicbuyingItemDetail&ReqContent={0}", request.ToJSON());

            var rsp = SendHttpRequest(url, json);
            Console.WriteLine(rsp);
        }

        [Test]
        public void setOrderInfo()
        {
            APIRequest request = new APIRequest();
            request.common = new CommonReqPara();
            request.common.customerId = "6c1ce52aa43441a3a13c87b41fcafd54";
            SetOrderInfoReqPara para = new SetOrderInfoReqPara();
            para.eventId = new Guid("188660F3-6AA9-4686-A3D0-1C1D2A9772EE");
            para.qty = "1";
            para.storeId = "";
            para.totalAmount = "2";
            para.mobile = "";
            para.remark = "";
            para.reqBy = "1";
            para.joinNo = "3";
            para.isPanicbuying = "1";
            para.salesPrice = "1";
            para.stdPrice = "1";
            para.orderDetailList = new OrderDetail[]{
                new OrderDetail(){ salesPrice="1", qty="1", skuId="0736cd34d0334e61232d058f9a96c491"}
            };
            request.special = para;

            string json = string.Format("action=setOrderInfo&ReqContent={0}", request.ToJSON());
            json = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%229825938c3dc54c5ea172eea667115971%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%229825938c3dc54c5ea172eea667115971%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22isGroupBy%22%3A0%2C%22joinNo%22%3A1%2C%22qty%22%3A3%2C%22username%22%3A%22%22%2C%22couponsPrompt%22%3A%22%22%2C%22deliveryId%22%3A%222%22%2C%22deliveryTime%22%3A%2218%3A35%3A35%22%2C%22eventId%22%3A%22061e0a01-3ec6-4471-bdf7-9ba9e6db4638%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A1%2C%22totalAmount%22%3A687.0%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3A%222014-04-01+00%3A00%3A00%22%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3A%222015-04-03+00%3A00%3A00%22%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%3A8400%2FFramework%2FUpload%2FImage%2F20140321%2F7ACDEEB3A26E479397D82F2FB38C89E4.png%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%221664035A8087491BB2B74D776334BF38%22%2C%22itemName%22%3A%22%E8%BF%9E%E9%94%81%E5%AF%86%E7%A0%81%E4%B9%A6%22%2C%22price%22%3A229.0%2C%22qty%22%3A3%2C%22salesPrice%22%3A195.0%2C%22selDate%22%3Anull%2C%22skuId%22%3A%227716d8389245544842456bc7f2b88d9e%22%2C%22selected%22%3Atrue%7D%5D%2C%22actualAmount%22%3A585.0%2C%22storeId%22%3Anull%2C%22deliveryAddress%22%3A%22%22%2C%22isPanicbuying%22%3A1%2C%22mobile%22%3A%22%22%7D%7D");
            var rsp = SendHttpRequest(url, json);
            Console.WriteLine(rsp);
        }

        [Test]
        public void getItemList()
        {
            APIRequest request = new APIRequest();
            request.common = new CommonReqPara();
            request.common.customerId = "6c1ce52aa43441a3a13c87b41fcafd54";

            string json = string.Format("action=getItemList&ReqContent={0}", request.ToJSON());

            var rsp = SendHttpRequest(temporary_url, json);
            Assert.IsTrue(rsp != string.Empty);
        }

        public static string SendHttpRequest(string requestURI, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = requestURI;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            //myRequest.Accept = "application/json";
            myRequest.ContentType = "application/x-www-form-urlencoded";

            //Content-type: application/json; charset=utf-8

            //myRequest.ContentType = "text/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }
    }
}
