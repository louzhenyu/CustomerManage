using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using JIT.Utility.ExtensionMethod;
using NUnit.Framework;
using System.Web;

namespace Test.CPOS.Web
{
    [TestFixture]
    public class TestAPI
    {
        //public string url = "http://112.124.68.147:9104/OnlineShopping/data/Data.aspx";
        public string url = "http://localhost:23130/OnlineShopping/data/Data.aspx";
        //public string url = "http://localhost:23130/Interface/data/ItemData.aspx";
        //public string url = "http://www.o2omarketing.cn:9004/OnlineShopping/data/Data.aspx";
        //public string url = "http://localhost:23130/Interface/Data/OrderData.aspx";
        //public string url = "http://www.o2omarketing.cn:9015/OnlineShopping/data/Data.aspx";
        //public string url = "http://localhost:23130/Interface/data/OrderData.aspx";
        //e77755c233634602873170a627d083cb
        //86a575e616044da3ac2c3ab492e44445
        //7ba0d0bc2c13403892deb6499d2c7266
        //e703dbedadd943abacf864531decdac1 张良测试 老窖
        //1d5039568a204391a417734cccd14fa4 生源

        public string customerid = "6c1ce52aa43441a3a13c87b41fcafd54";

        #region 具体接口
        [Test]
        public void TestGetAD()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getADReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getADReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.customerId = customerid;
            string str = string.Format("action=getADList&ReqContent={0}", req.ToJSON());
            //string str = "Action=getADList&ReqContent={\"common\":{\"locale\":\"zh\",\"userId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"customerId\":\"7ba0d0bc2c13403892deb6499d2c7266\"},\"special\":{}}";
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestgetDistrictList()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getADReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getADReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.customerId = customerid;
            string str = string.Format("action=getDistrictList&ReqContent={0}", req.ToJSON());
            //string str = "Action=getDistrictList&ReqContent={\"common\":{\"locale\":\"zh\",\"userId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"customerId\":\"7ba0d0bc2c13403892deb6499d2c7266\"},\"special\":{}}";
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestGetCategory()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getCategoriesReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getCategoriesReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getCategoriesSpecialData();
            req.special.page = 0;
            req.special.pageSize = 20;
            req.common.customerId = customerid;
            string str = string.Format("action=getIndexCategoryList&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestSearchStores()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.searchStoresReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.searchStoresReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.searchStoresSpecialData();
            req.special.position = "121,31";
            req.special.page = 0;
            req.special.pageSize = 100;
            req.common.customerId = customerid;
            string str = string.Format("action=searchStores&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestmodifyPWD()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.modifyPWDReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.modifyPWDReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.modifyPWDSpecialData();
            req.special.phone = "13611999929";
            req.special.sourcePWD = "111111";
            req.special.newPWD = "111111";
            req.common.customerId = customerid;
            string str = string.Format("action=modifyPWD&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestdeleteShoppingCart()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.deleteShoppingCartReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.deleteShoppingCartReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.deleteShoppingCartSpecialData();
            req.special.vipid = "00da749cbb4f4de8acae74c85448e95d";
            req.common.customerId = customerid;
            string str = string.Format("action=deleteShoppingCart&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestIsOrderPaid()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.isOrderPaidReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.isOrderPaidReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.isOrderPaidSpecialData();
            req.special.orderid = "fdb2ba8d4f6b4d5b9e6e6a3473803a04";
            req.common.customerId = customerid;
            string str = string.Format("action=isOrderPaid&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestgetVipAddressList()
        {
            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqData req = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData()
            {
                customerId = customerid,
                userId = "e77755c233634602873170a627d081cb",
                openId = "e77755c233634602873170a627d081cb"
            };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqSpecialData()
            {
                vipid = "00fcb9f52a1c4273a7aca100a030f99d"
            };
            string str = string.Format("action=getVipAddressList&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestsetVipAddress()
        {
            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqData req = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData()
            {
                customerId = customerid,
                userId = "e77755c233634602873170a627d081cb",
                openId = "e77755c233634602873170a627d081cb"
            };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqSpecialData()
            {
                address = "测试",
                cityID = "1",
                isDefault = "1",
                isDelete = "0",
                linkMan = "Test",
                linkTel = "13333333333",
                vipid = "00fcb9f52a1c4273a7aca100a030f99d"
            };
            string str = string.Format("action=setVipAddress&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestgetOrderList()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getOrderListReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getOrderListReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { openId = "add73ef71c2c480c89b5a6941cb0dfc9" };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getOrderListReqSpecialData() { status = "2,3", page = 1, pageSize = 100 };
            req.common.userId = "add73ef71c2c480c89b5a6941cb0dfc9";
            req.common.customerId = customerid;
            string str = string.Format("action=getOrderList&ReqContent={0}", req.ToJSON());
            //str = HttpUtility.UrlDecode("action=getOrderList&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22asdf%22%2C%22customerId%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22getOrderList%22%2C%22page%22%3A%221%22%2C%22pageSize%22%3A%2299%22%2C%22status%22%3A1%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestsetOrderInfo()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.setOrderInfoReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.setOrderInfoReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.customerId = customerid;
            //string str = string.Format("action=getOrderList&ReqContent={0}", req.ToJSON());
            string str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22b98b3d055f574662a28b545d27ab868a%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%22b98b3d055f574662a28b545d27ab868a%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%221%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E6%9D%B0%E5%AE%B6%E5%B8%B8%E8%8F%9C%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2211%3A42%3A28%22%2C%22email%22%3A%22%22%2C%22totalAmount%22%3A15.0%2C%22aldMemberID%22%3A%2292a0640d416a420f9e51e975b4b3dc16%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22endDate%22%3Anull%2C%22qty%22%3A1%2C%22salesPrice%22%3A0.0%2C%22skuId%22%3Anull%7D%5D%2C%22storeId%22%3A%22be67768a-38c1-4999-904b-e51306d5071b%22%2C%22deliveryAddress%22%3A%22%E5%92%8C%E7%A8%8B%E5%BA%8F%22%2C%22mobile%22%3A%22683838383838%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestGetItemList()
        {
            var str = HttpUtility.UrlDecode("action=getItemList&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%228c6979db4acb4dd3909f5be67f433e67%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22getItemList%22%2C%22isExchange%22%3A0%2C%22page%22%3A1%2C%22pageSize%22%3A99%2C%22itemTypeId%22%3A%22670073c4a21244339eb2148ce795bcf5%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestgetShoppingCartCount()
        {
            TestSetShoppingCart();
            JIT.CPOS.Web.OnlineShopping.data.Data.getShoppingCartCountReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getShoppingCartCountReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getShoppingCartCountSpecialData() { vipid = "1e0ed52ba8d03abb81326f5ca8119983" };
            req.common.customerId = customerid;
            string str = string.Format("action=getShoppingCartCount&ReqContent={0}", req.ToJSON());
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestSetShoppingCart()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.setShoppingCartReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.setShoppingCartReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.userId = "1e0ed52ba8d03abb81326f5ca8119983";
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.setShoppingCartReqSpecialData()
            {
                skuId = "2F17F451E9EC483FB1F635BAD8CD0137",
                qty = 1
            };
            req.common.customerId = customerid;
            string str = string.Format("action=getShoppingCartCount&ReqContent={0}", req.ToJSON());
            //var str = HttpUtility.UrlDecode("action=setShoppingCart&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22e1934e57bac048489e90a86d1d46a98d%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%22e1934e57bac048489e90a86d1d46a98d%22%7D%2C%22special%22%3A%7B%22beginData%22%3Anull%2C%22skuId%22%3A%22FEC2F108F8D04353B213C75C881DD8A6%22%2C%22endData%22%3Anull%2C%22qty%22%3A2%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestgetRecentlyUsedStore()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getRecentlyUsedStoreReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getRecentlyUsedStoreReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.userId = "1e0ed52ba8d03abb81326f5ca8119983";
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getRecentlyUsedStoreSpecialData()
            {

            };
            req.common.customerId = customerid;
            string str = string.Format("action=getRecentlyUsedStore&ReqContent={0}", req.ToJSON());
            str = HttpUtility.UrlDecode("action=getRecentlyUsedStore&ReqContent={\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"9262d15abb11454aa713bbc9037d5df3\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"9262d15abb11454aa713bbc9037d5df3\"},\"special\":{\"position\":\"\"}}");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestgetStoreListByItem()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getStoreListByItemReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getStoreListByItemReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.userId = "1e0ed52ba8d03abb81326f5ca8119983";
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getStoreListByItemReqSpecialData
            {
                page = 1,
                pageSize = 10
            };
            req.common.customerId = "7ba0d0bc2c13403892deb6499d2c7266";
            string str = string.Format("action=getStoreListByItem&ReqContent={0}", req.ToJSON());
            str = HttpUtility.UrlDecode("action=getStoreListByItem&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%220653d86c8f1c40309b7aaa7be2cf02b3%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%220653d86c8f1c40309b7aaa7be2cf02b3%22%7D%2C%22special%22%3A%7B%22pageSize%22%3A10%2C%22longitude%22%3A0.0%2C%22latitude%22%3A0.0%2C%22itemId%22%3Anull%2C%22page%22%3A1%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestsetOrderStatus4ALD()
        {

            JIT.CPOS.Web.OnlineShopping.data.Data.setOrderStatusReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.setOrderStatusReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { userId = "8df47259f6e64699bff256dae5f31b60", openId = "8df47259f6e64699bff256dae5f31b60" };
            req.common.userId = "1e0ed52ba8d03abb81326f5ca8119983";
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.setOrderStatusReqSpecialData()
            {
                orderId = "0297d64a47004d1f9c2c99a2e42ad676",
                status = "0"
            };
            req.common.customerId = "7ba0d0bc2c13403892deb6499d2c7266";
            string str = string.Format("action=setOrderStatus4ALD&ReqContent={0}", req.ToJSON());
            //str = HttpUtility.UrlDecode("action=getStoreListByItem&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%220653d86c8f1c40309b7aaa7be2cf02b3%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%220653d86c8f1c40309b7aaa7be2cf02b3%22%7D%2C%22special%22%3A%7B%22pageSize%22%3A10%2C%22longitude%22%3A0.0%2C%22latitude%22%3A0.0%2C%22itemId%22%3Anull%2C%22page%22%3A1%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void setOrderPaymentType()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.setOrderPaymentTypeReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.setOrderPaymentTypeReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.customerId = customerid;
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.setOrderPaymentTypeSpecialData()
            {
                orderId = "0199e9ef0e1b4b5bb3d82b34e815e426",
                paymentId = "6D3739E493B2416EA8C3DC44D388BC8C"
            };
            string str = string.Format("action=setOrderPaymentType&ReqContent={0}", req.ToJSON());
            //string str = "Action=getDistrictList&ReqContent={\"common\":{\"locale\":\"zh\",\"userId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"customerId\":\"7ba0d0bc2c13403892deb6499d2c7266\"},\"special\":{}}";
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void getOrderStatistics()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getOrderStatisticsReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getOrderStatisticsReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData();
            req.common.customerId = customerid;
            req.common.userId = "8df47259f6e64699bff256dae5f31b60";
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getOrderStatisticsSpecialData()
            {
                status = "1"
            };
            string str = string.Format("action=getOrderStatistics&ReqContent={0}", req.ToJSON());
            //string str = "Action=getDistrictList&ReqContent={\"common\":{\"locale\":\"zh\",\"userId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"customerId\":\"7ba0d0bc2c13403892deb6499d2c7266\"},\"special\":{}}";
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void getPaymentTypeList()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getPaymentTypeListReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getPaymentTypeListReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { openId = "40b9cf55cce5470a8404a9038c149c5f" };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getPaymentTypeListReqSpecialData();
            req.common.customerId = customerid;
            string str = string.Format("action=getPaymentTypeList&ReqContent={0}", req.ToJSON());
            //string str = HttpUtility.UrlDecode("action=getOrderList&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22b8657fb2edff42718be397e535d7925a%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%22b8657fb2edff42718be397e535d7925a%22%7D%2C%22special%22%3A%7B%22pageSize%22%3A100%2C%22endDate%22%3A%22%22%2C%22beginDate%22%3A%22%22%2C%22status%22%3A1%2C%22page%22%3A0%2C%22orderId%22%3A%22%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void getVipAddressList()
        {
            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqData req = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { openId = "40b9cf55cce5470a8404a9038c149c5f" };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.getVipAddressReqSpecialData { };
            req.common.customerId = customerid;
            string str = string.Format("action=getVipAddressList&ReqContent={0}", req.ToJSON());
            str = HttpUtility.UrlDecode("action=getItemDetail&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%222a717133725b463a948a7467d67d4873%22%2C%22userId%22%3A%222a717133725b463a948a7467d67d4873%22%2C%22Locale%22%3A1%7D%2C%22special%22%3A%7B%22itemId%22%3A%2220DA4DC9899A48E6885E5309F1E68EA6%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void GetStoreDetail()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getStoreDetailReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getStoreDetailReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { openId = "40b9cf55cce5470a8404a9038c149c5f" };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getStoreDetailReqSpecialData();
            req.common.customerId = customerid;
            req.common.customerId = "f8988876c5b34cd5923661881ddfc928";
            string str = string.Format("action=getStoreDetail&ReqContent={0}", req.ToJSON());
            //string str = HttpUtility.UrlDecode("action=getOrderList&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22b8657fb2edff42718be397e535d7925a%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%22b8657fb2edff42718be397e535d7925a%22%7D%2C%22special%22%3A%7B%22pageSize%22%3A100%2C%22endDate%22%3A%22%22%2C%22beginDate%22%3A%22%22%2C%22status%22%3A1%2C%22page%22%3A0%2C%22orderId%22%3A%22%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void setEvaluation()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.setEvaluationReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.setEvaluationReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { openId = "40b9cf55cce5470a8404a9038c149c5f" };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.setEvaluationSpecialData();
            req.common.customerId = customerid;
            req.special.content = "adfadkkasf";
            req.special.objectId = "";
            req.special.platform = "1";
            req.special.memberId = req.common.userId;
            string str = string.Format("action=setEvaluation&ReqContent={0}", req.ToJSON());
            //string str = HttpUtility.UrlDecode("action=getOrderList&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22b8657fb2edff42718be397e535d7925a%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%22b8657fb2edff42718be397e535d7925a%22%7D%2C%22special%22%3A%7B%22pageSize%22%3A100%2C%22endDate%22%3A%22%22%2C%22beginDate%22%3A%22%22%2C%22status%22%3A1%2C%22page%22%3A0%2C%22orderId%22%3A%22%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void GetEvaluationList()
        {
            JIT.CPOS.Web.OnlineShopping.data.Data.getEvaluationListReqData req = new JIT.CPOS.Web.OnlineShopping.data.Data.getEvaluationListReqData();
            req.common = new JIT.CPOS.Web.OnlineShopping.data.ReqCommonData() { openId = "40b9cf55cce5470a8404a9038c149c5f" };
            req.special = new JIT.CPOS.Web.OnlineShopping.data.Data.getEvaluationListSpecialData();
            req.common.customerId = customerid;
            string str = string.Format("action=getEvaluationList&ReqContent={0}", req.ToJSON());
            //string str = HttpUtility.UrlDecode("action=getOrderList&ReqContent=%7B%22common%22%3A%7B%22baiduPushUserId%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%227ba0d0bc2c13403892deb6499d2c7266%22%2C%22baiduPushChannelId%22%3Anull%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22b8657fb2edff42718be397e535d7925a%22%2C%22locale%22%3A1%2C%22channel%22%3Anull%2C%22osInfo%22%3Anull%2C%22version%22%3Anull%2C%22openId%22%3A%22b8657fb2edff42718be397e535d7925a%22%7D%2C%22special%22%3A%7B%22pageSize%22%3A100%2C%22endDate%22%3A%22%22%2C%22beginDate%22%3A%22%22%2C%22status%22%3A1%2C%22page%22%3A0%2C%22orderId%22%3A%22%22%7D%7D");
            var res = SendHttpRequest(url, str);
            Console.WriteLine(res);
        }
        #endregion

        [Test]
        public void Test()
        {
            var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
            str = HttpUtility.UrlDecode("action=setAndroidBasic&reqContent={\"common\":{\"baiduPushUserId\":\"943111860957220380\",\"plat\":\"android\",\"baiduPushChannelId\":\"3574609464308132221\",\"CustomerId\":\"6c1ce52aa43441a3a13c87b41fcafd54\",\"sessionId\":\"\",\"baiduPushAppId\":\"2393097\",\"deviceToken\":\"\",\"UserId\":\"6C25C4400BA444F6A0DF3AAB6F8367A6\",\"Locale\":\"zh\",\"channel\":\"1\",\"osInfo\":\"4.0.4\",\"OpenId\":\"6C25C4400BA444F6A0DF3AAB6F8367A6\",\"version\":\"1.1.9\"},\"Special\":{\"channelId\":\"3574609464308132221\",\"deviceToken\":\"\",\"userId\":\"943111860957220380\",\"appId\":\"2393097\"}}");
            //str = "action=getItemList&ReqContent={\"common\":{\"isAld\":\"0\",\"baiduPushUserId\":null,\"baiduPushChannelId\":null,\"channelId\":\"4\",\"locale\":1,\"version\":null,\"plat\":\"android\",\"customerId\":\"13010a73777d4db2b2a84f01f6f29de0\",\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"03b543a0dcab4e5e907a8d3381644bdc\",\"businessZoneId\":null,\"osInfo\":null,\"openId\":\"03b543a0dcab4e5e907a8d3381644bdc\"},\"special\":{\"itemName\":null,\"isExchange\":0,\"page\":1,\"storeId\":\"\",\"PageSize\":100,\"isGroupBy\":\"\",\"itemTypeId\":\"131a5716a1c74480a4b49f3afd81330b\"}}";
            //str = HttpUtility.UrlDecode("action=setPayOrder&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%226c1ce52aa43441a3a13c87b41fcafd54%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22setPayOrder%22%2C%22paymentId%22%3A%22541DF4A0DFF84015924E1C60C33FBA27%22%2C%22orderID%22%3A%22bba6b9749a6747db97c26358ba080792%22%2C%22returnUrl%22%3A%22http%3A%2F%2Fdev.o2omarketing.cn%3A9004%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3FcustomerId%3D6c1ce52aa43441a3a13c87b41fcafd54%22%2C%22mobileNo%22%3A%22%22%2C%22amount%22%3A317%2C%22dataFromId%22%3A2%7D%7D");
            //var str ="ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22orderPay%22%2C%22payChannelID%22%3A%223%22%2C%22orderID%22%3A%22c14fd36eff2446428eabf8baa8f59299%22%2C%22returnUrl%22%3A%22http%3A%2F%2F192.168.0.77%3A8001%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3ForderId%3Dc14fd36eff2446428eabf8baa8f59299%26dsf%3D1%22%2C%22mobileNO%22%3A%22%22%7D%7D&action=orderPay";
            //str = "ReqContent=%7B"common"%3A%7B"openId"%3A"ofUHqjgSme_qaNQN0oohrM7kX_Ck"%2C"customerId"%3A"e703dbedadd943abacf864531decdac1"%2C"userId"%3A"add73ef71c2c480c89b5a6941cb0dfc9"%2C"locale"%3A"ch"%7D%2C"special"%3A%7B"action"%3A"getOrderStatistics"%7D%7D&action=getOrderStatistics";
            //str = "action=setOrderInfoTwo&ReqContent={\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"f9580b2fb5b74c3aa2526021f435609a\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"f9580b2fb5b74c3aa2526021f435609a\"},\"special\":{\"tableNumber\":\"\",\"status\":\"100\",\"remark\":\"\",\"qty\":1,\"username\":\"李四\",\"deliveryId\":\"1\",\"deliveryTime\":\"18:53:33\",\"email\":\"\",\"totalAmount\":0.01,\"aldMemberID\":\"\",\"orderDetailList\":[{\"beginDate\":null,\"endDate\":null,\"qty\":1,\"salesPrice\":0.01,\"skuId\":\"d0bd5fbb233b6cb15bf15c716066c0cf\"}],\"storeId\":\"1cb675b011f14d388538ef2c60bba8b8\",\"deliveryAddress\":\"哈哈\",\"mobile\":\"123456\"}}";
            var res = SendHttpRequest2(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestGetOrderList()
        {
            var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
            str = HttpUtility.UrlDecode("{\"common\":{\"baiduPushUserId\":\"943111860957220380\",\"plat\":\"android\",\"baiduPushChannelId\":\"3574609464308132221\",\"CustomerId\":\"6c1ce52aa43441a3a13c87b41fcafd54\",\"sessionId\":\"\",\"baiduPushAppId\":\"2393097\",\"deviceToken\":\"\",\"UserId\":\"6C25C4400BA444F6A0DF3AAB6F8367A6\",\"Locale\":\"zh\",\"channel\":\"1\",\"osInfo\":\"4.0.4\",\"OpenId\":\"6C25C4400BA444F6A0DF3AAB6F8367A6\",\"version\":\"1.1.9\"},\"Special\":{\"channelId\":\"3574609464308132221\",\"deviceToken\":\"\",\"userId\":\"943111860957220380\",\"appId\":\"2393097\"}}");
            //str = "action=getItemList&ReqContent={\"common\":{\"isAld\":\"0\",\"baiduPushUserId\":null,\"baiduPushChannelId\":null,\"channelId\":\"4\",\"locale\":1,\"version\":null,\"plat\":\"android\",\"customerId\":\"13010a73777d4db2b2a84f01f6f29de0\",\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"03b543a0dcab4e5e907a8d3381644bdc\",\"businessZoneId\":null,\"osInfo\":null,\"openId\":\"03b543a0dcab4e5e907a8d3381644bdc\"},\"special\":{\"itemName\":null,\"isExchange\":0,\"page\":1,\"storeId\":\"\",\"PageSize\":100,\"isGroupBy\":\"\",\"itemTypeId\":\"131a5716a1c74480a4b49f3afd81330b\"}}";
            //str = HttpUtility.UrlDecode("action=setPayOrder&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%226c1ce52aa43441a3a13c87b41fcafd54%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22setPayOrder%22%2C%22paymentId%22%3A%22541DF4A0DFF84015924E1C60C33FBA27%22%2C%22orderID%22%3A%22bba6b9749a6747db97c26358ba080792%22%2C%22returnUrl%22%3A%22http%3A%2F%2Fdev.o2omarketing.cn%3A9004%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3FcustomerId%3D6c1ce52aa43441a3a13c87b41fcafd54%22%2C%22mobileNo%22%3A%22%22%2C%22amount%22%3A317%2C%22dataFromId%22%3A2%7D%7D");
            //var str ="ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22orderPay%22%2C%22payChannelID%22%3A%223%22%2C%22orderID%22%3A%22c14fd36eff2446428eabf8baa8f59299%22%2C%22returnUrl%22%3A%22http%3A%2F%2F192.168.0.77%3A8001%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3ForderId%3Dc14fd36eff2446428eabf8baa8f59299%26dsf%3D1%22%2C%22mobileNO%22%3A%22%22%7D%7D&action=orderPay";
            //str = "ReqContent=%7B"common"%3A%7B"openId"%3A"ofUHqjgSme_qaNQN0oohrM7kX_Ck"%2C"customerId"%3A"e703dbedadd943abacf864531decdac1"%2C"userId"%3A"add73ef71c2c480c89b5a6941cb0dfc9"%2C"locale"%3A"ch"%7D%2C"special"%3A%7B"action"%3A"getOrderStatistics"%7D%7D&action=getOrderStatistics";
            //str = "action=setOrderInfoTwo&ReqContent={\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"f9580b2fb5b74c3aa2526021f435609a\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"f9580b2fb5b74c3aa2526021f435609a\"},\"special\":{\"tableNumber\":\"\",\"status\":\"100\",\"remark\":\"\",\"qty\":1,\"username\":\"李四\",\"deliveryId\":\"1\",\"deliveryTime\":\"18:53:33\",\"email\":\"\",\"totalAmount\":0.01,\"aldMemberID\":\"\",\"orderDetailList\":[{\"beginDate\":null,\"endDate\":null,\"qty\":1,\"salesPrice\":0.01,\"skuId\":\"d0bd5fbb233b6cb15bf15c716066c0cf\"}],\"storeId\":\"1cb675b011f14d388538ef2c60bba8b8\",\"deliveryAddress\":\"哈哈\",\"mobile\":\"123456\"}}";
            var res = SendHttpRequest2(url, str);
            Console.WriteLine(res);
        }
        [Test]
        public void TestChangeOrderStatus()
        {
            JIT.CPOS.Web.LJ.data.Data.setOrderStatusReqData req = new JIT.CPOS.Web.LJ.data.Data.setOrderStatusReqData();
            req.common = new JIT.CPOS.Web.LJ.data.ReqCommonData()
            {
                customerId = "e703dbedadd943abacf864531decdac1",
                userId = "f9580b2fb5b74c3aa2526021f435609a",
                openId = "f9580b2fb5b74c3aa2526021f435609a"
            };
            req.special = new JIT.CPOS.Web.LJ.data.Data.setOrderStatusReqSpecialData()
            {
                orderId = "ab59ea62ca6e416a87bb2848cf94251e",
                status = "500"
            };
            string sub = "{\"common\":{\"deviceToken\":\"0436644720e93d830afff7d345db58c85908fba7e9dc5c33dbf804bf258215e3\",\"osInfo\":\"7.0\",\"userId\":\"83EBEEEE1C82451C98BE753218FA6EEC\",\"sessionId\":\"\",\"openId\":\"83EBEEEE1C82451C98BE753218FA6EEC\",\"channel\":\"1\",\"locale\":\"zh\",\"version\":\"1.0.9\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"plat\":\"iPhone\"},\"special\":{\"orderId\":\"ab59ea62ca6e416a87bb2848cf94251e\",\"tableNo\":\"\",\"status\":\"500\"}}";
            //string str = string.Format("action=setOrderStatus&ReqContent={0}", req.ToJSON());
            string str = string.Format("action=setOrderStatus&ReqContent={0}", sub);
            var res = SendHttpRequest2(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestSetOrderInfo()
        {
            var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
            str = HttpUtility.UrlDecode("action=searchStores&ReqContent={\"common\":{\"isAld\":\"0\",\"baiduPushUserId\":null,\"baiduPushChannelId\":null,\"channelId\":\"4\",\"locale\":1,\"version\":null,\"plat\":\"android\",\"customerId\":\"6c1ce52aa43441a3a13c87b41fcafd54\",\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"352acfb57370451e965005cfb2b8d6c4\",\"businessZoneId\":null,\"osInfo\":null,\"openId\":\"352acfb57370451e965005cfb2b8d6c4\"},\"special\":{\"includeHQ\":true,\"pageSize\":10,\"position\":\"\",\"districtId\":\"\",\"storeId\":\"\",\"page\":0,\"nameLike\":\"\"}}");
            //str = "action=getItemList&ReqContent={\"common\":{\"isAld\":\"0\",\"baiduPushUserId\":null,\"baiduPushChannelId\":null,\"channelId\":\"4\",\"locale\":1,\"version\":null,\"plat\":\"android\",\"customerId\":\"13010a73777d4db2b2a84f01f6f29de0\",\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"03b543a0dcab4e5e907a8d3381644bdc\",\"businessZoneId\":null,\"osInfo\":null,\"openId\":\"03b543a0dcab4e5e907a8d3381644bdc\"},\"special\":{\"itemName\":null,\"isExchange\":0,\"page\":1,\"storeId\":\"\",\"PageSize\":100,\"isGroupBy\":\"\",\"itemTypeId\":\"131a5716a1c74480a4b49f3afd81330b\"}}";
            //str = HttpUtility.UrlDecode("action=setPayOrder&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%226c1ce52aa43441a3a13c87b41fcafd54%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22setPayOrder%22%2C%22paymentId%22%3A%22541DF4A0DFF84015924E1C60C33FBA27%22%2C%22orderID%22%3A%22bba6b9749a6747db97c26358ba080792%22%2C%22returnUrl%22%3A%22http%3A%2F%2Fdev.o2omarketing.cn%3A9004%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3FcustomerId%3D6c1ce52aa43441a3a13c87b41fcafd54%22%2C%22mobileNo%22%3A%22%22%2C%22amount%22%3A317%2C%22dataFromId%22%3A2%7D%7D");
            //var str ="ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22orderPay%22%2C%22payChannelID%22%3A%223%22%2C%22orderID%22%3A%22c14fd36eff2446428eabf8baa8f59299%22%2C%22returnUrl%22%3A%22http%3A%2F%2F192.168.0.77%3A8001%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3ForderId%3Dc14fd36eff2446428eabf8baa8f59299%26dsf%3D1%22%2C%22mobileNO%22%3A%22%22%7D%7D&action=orderPay";
            //str = "ReqContent=%7B"common"%3A%7B"openId"%3A"ofUHqjgSme_qaNQN0oohrM7kX_Ck"%2C"customerId"%3A"e703dbedadd943abacf864531decdac1"%2C"userId"%3A"add73ef71c2c480c89b5a6941cb0dfc9"%2C"locale"%3A"ch"%7D%2C"special"%3A%7B"action"%3A"getOrderStatistics"%7D%7D&action=getOrderStatistics";
            //str = "action=setOrderInfoTwo&ReqContent={\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"f9580b2fb5b74c3aa2526021f435609a\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"f9580b2fb5b74c3aa2526021f435609a\"},\"special\":{\"tableNumber\":\"\",\"status\":\"100\",\"remark\":\"\",\"qty\":1,\"username\":\"李四\",\"deliveryId\":\"1\",\"deliveryTime\":\"18:53:33\",\"email\":\"\",\"totalAmount\":0.01,\"aldMemberID\":\"\",\"orderDetailList\":[{\"beginDate\":null,\"endDate\":null,\"qty\":1,\"salesPrice\":0.01,\"skuId\":\"d0bd5fbb233b6cb15bf15c716066c0cf\"}],\"storeId\":\"1cb675b011f14d388538ef2c60bba8b8\",\"deliveryAddress\":\"哈哈\",\"mobile\":\"123456\"}}";
            var res = SendHttpRequest2(url, str);
            Console.WriteLine(res);
        }

        [Test]
        public void TestGetStoreDetail()
        {
            var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
            str = HttpUtility.UrlDecode("action=getStoreDetail&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%2229E11BDC6DAC439896958CC6866FF64E%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22getStoreDetail%22%2C%22storeId%22%3A%22bae1ed3ce4db4524a6d2398299075fbf%22%7D%7D");
            //str = "action=getItemList&ReqContent={\"common\":{\"isAld\":\"0\",\"baiduPushUserId\":null,\"baiduPushChannelId\":null,\"channelId\":\"4\",\"locale\":1,\"version\":null,\"plat\":\"android\",\"customerId\":\"13010a73777d4db2b2a84f01f6f29de0\",\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"03b543a0dcab4e5e907a8d3381644bdc\",\"businessZoneId\":null,\"osInfo\":null,\"openId\":\"03b543a0dcab4e5e907a8d3381644bdc\"},\"special\":{\"itemName\":null,\"isExchange\":0,\"page\":1,\"storeId\":\"\",\"PageSize\":100,\"isGroupBy\":\"\",\"itemTypeId\":\"131a5716a1c74480a4b49f3afd81330b\"}}";
            //str = HttpUtility.UrlDecode("action=setPayOrder&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%226c1ce52aa43441a3a13c87b41fcafd54%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22setPayOrder%22%2C%22paymentId%22%3A%22541DF4A0DFF84015924E1C60C33FBA27%22%2C%22orderID%22%3A%22bba6b9749a6747db97c26358ba080792%22%2C%22returnUrl%22%3A%22http%3A%2F%2Fdev.o2omarketing.cn%3A9004%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3FcustomerId%3D6c1ce52aa43441a3a13c87b41fcafd54%22%2C%22mobileNo%22%3A%22%22%2C%22amount%22%3A317%2C%22dataFromId%22%3A2%7D%7D");
            //var str ="ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22orderPay%22%2C%22payChannelID%22%3A%223%22%2C%22orderID%22%3A%22c14fd36eff2446428eabf8baa8f59299%22%2C%22returnUrl%22%3A%22http%3A%2F%2F192.168.0.77%3A8001%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3ForderId%3Dc14fd36eff2446428eabf8baa8f59299%26dsf%3D1%22%2C%22mobileNO%22%3A%22%22%7D%7D&action=orderPay";
            //str = "ReqContent=%7B"common"%3A%7B"openId"%3A"ofUHqjgSme_qaNQN0oohrM7kX_Ck"%2C"customerId"%3A"e703dbedadd943abacf864531decdac1"%2C"userId"%3A"add73ef71c2c480c89b5a6941cb0dfc9"%2C"locale"%3A"ch"%7D%2C"special"%3A%7B"action"%3A"getOrderStatistics"%7D%7D&action=getOrderStatistics";
            //str = "action=setOrderInfoTwo&ReqContent={\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"f9580b2fb5b74c3aa2526021f435609a\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"f9580b2fb5b74c3aa2526021f435609a\"},\"special\":{\"tableNumber\":\"\",\"status\":\"100\",\"remark\":\"\",\"qty\":1,\"username\":\"李四\",\"deliveryId\":\"1\",\"deliveryTime\":\"18:53:33\",\"email\":\"\",\"totalAmount\":0.01,\"aldMemberID\":\"\",\"orderDetailList\":[{\"beginDate\":null,\"endDate\":null,\"qty\":1,\"salesPrice\":0.01,\"skuId\":\"d0bd5fbb233b6cb15bf15c716066c0cf\"}],\"storeId\":\"1cb675b011f14d388538ef2c60bba8b8\",\"deliveryAddress\":\"哈哈\",\"mobile\":\"123456\"}}";
            var res = SendHttpRequest2(url, str);
            Console.WriteLine(res);
        }

        //[Test]
        //public void TestGetItemList()
        //{
        //    var str = HttpUtility.UrlDecode("action=setOrderInfo&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22ef89a5af350f41128d4f85297967ab20%22%7D%2C%22special%22%3A%7B%22tableNumber%22%3A%22%22%2C%22status%22%3A%22100%22%2C%22remark%22%3A%22%22%2C%22qty%22%3A1%2C%22username%22%3A%22%E5%95%8A%E5%95%8A%E5%95%8A%22%2C%22deliveryId%22%3A%221%22%2C%22deliveryTime%22%3A%2217%3A14%3A16%22%2C%22email%22%3A%22%22%2C%22reqBy%22%3A%221%22%2C%22totalAmount%22%3A49800.0%2C%22aldMemberID%22%3A%22%22%2C%22orderDetailList%22%3A%5B%7B%22beginDate%22%3Anull%2C%22dayCount%22%3A0%2C%22discountRate%22%3A0.0%2C%22displayIndex%22%3A0%2C%22endDate%22%3Anull%2C%22gg%22%3Anull%2C%22imageUrl%22%3A%22http%3A%2F%2Fwww.o2omarketing.cn%2Fimages%2FitemImg%2F1%2F1.jpg%22%2C%22itemCategoryName%22%3Anull%2C%22itemId%22%3A%2208766886E0DE4C6B95A4E0706C93B4AE%22%2C%22itemName%22%3Anull%2C%22price%22%3A0.0%2C%22qty%22%3A1%2C%22salesPrice%22%3A49800.0%2C%22selDate%22%3Anull%2C%22skuId%22%3Anull%2C%22selected%22%3Atrue%7D%5D%2C%22storeId%22%3A%22%22%2C%22deliveryAddress%22%3A%22%E6%B9%96%E5%8C%97%E7%9C%81%E8%8D%86%E9%97%A8%E5%B8%82%E4%B8%9C%E5%AE%9D%E5%8C%BA%E8%B5%A4%E6%9E%9C%E6%9E%9C%22%2C%22mobile%22%3A%22123456%22%7D%7D");
        //    str = HttpUtility.UrlDecode("action=getItemList&ReqContent=%7B%22common%22%3A%7B%22isAld%22%3A%220%22%2C%22baiduPushUserId%22%3Anull%2C%22baiduPushChannelId%22%3Anull%2C%22channelId%22%3A%224%22%2C%22locale%22%3A1%2C%22version%22%3Anull%2C%22plat%22%3A%22android%22%2C%22customerId%22%3A%2286a575e616044da3ac2c3ab492e44445%22%2C%22sessionId%22%3Anull%2C%22baiduPushAppId%22%3Anull%2C%22deviceToken%22%3Anull%2C%22userId%22%3A%22352acfb57370451e965005cfb2b8d6c4%22%2C%22businessZoneId%22%3Anull%2C%22osInfo%22%3Anull%2C%22openId%22%3A%22352acfb57370451e965005cfb2b8d6c4%22%7D%2C%22special%22%3A%7B%22itemName%22%3Anull%2C%22isExchange%22%3A0%2C%22page%22%3A1%2C%22storeId%22%3A%22%22%2C%22PageSize%22%3A100%2C%22isGroupBy%22%3A%22%22%2C%22itemTypeId%22%3A%22ed2d7f40a01c495c864f5c533e1d1ec7%22%7D%7D");
        //    //str = "action=getItemList&ReqContent={\"common\":{\"isAld\":\"0\",\"baiduPushUserId\":null,\"baiduPushChannelId\":null,\"channelId\":\"4\",\"locale\":1,\"version\":null,\"plat\":\"android\",\"customerId\":\"13010a73777d4db2b2a84f01f6f29de0\",\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"03b543a0dcab4e5e907a8d3381644bdc\",\"businessZoneId\":null,\"osInfo\":null,\"openId\":\"03b543a0dcab4e5e907a8d3381644bdc\"},\"special\":{\"itemName\":null,\"isExchange\":0,\"page\":1,\"storeId\":\"\",\"PageSize\":100,\"isGroupBy\":\"\",\"itemTypeId\":\"131a5716a1c74480a4b49f3afd81330b\"}}";
        //    //str = HttpUtility.UrlDecode("action=setPayOrder&ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%226c1ce52aa43441a3a13c87b41fcafd54%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3Anull%7D%2C%22special%22%3A%7B%22action%22%3A%22setPayOrder%22%2C%22paymentId%22%3A%22541DF4A0DFF84015924E1C60C33FBA27%22%2C%22orderID%22%3A%22bba6b9749a6747db97c26358ba080792%22%2C%22returnUrl%22%3A%22http%3A%2F%2Fdev.o2omarketing.cn%3A9004%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3FcustomerId%3D6c1ce52aa43441a3a13c87b41fcafd54%22%2C%22mobileNo%22%3A%22%22%2C%22amount%22%3A317%2C%22dataFromId%22%3A2%7D%7D");
        //    //var str ="ReqContent=%7B%22common%22%3A%7B%22openId%22%3A%22ofUHqjgSme_qaNQN0oohrM7kX_Ck%22%2C%22customerId%22%3A%22e703dbedadd943abacf864531decdac1%22%2C%22userId%22%3A%22add73ef71c2c480c89b5a6941cb0dfc9%22%2C%22locale%22%3A%22ch%22%7D%2C%22special%22%3A%7B%22action%22%3A%22orderPay%22%2C%22payChannelID%22%3A%223%22%2C%22orderID%22%3A%22c14fd36eff2446428eabf8baa8f59299%22%2C%22returnUrl%22%3A%22http%3A%2F%2F192.168.0.77%3A8001%2FHtmlApps%2Fhtml%2Fpublic%2Fshop%2Fpay_success.html%3ForderId%3Dc14fd36eff2446428eabf8baa8f59299%26dsf%3D1%22%2C%22mobileNO%22%3A%22%22%7D%7D&action=orderPay";
        //    //str = "ReqContent=%7B"common"%3A%7B"openId"%3A"ofUHqjgSme_qaNQN0oohrM7kX_Ck"%2C"customerId"%3A"e703dbedadd943abacf864531decdac1"%2C"userId"%3A"add73ef71c2c480c89b5a6941cb0dfc9"%2C"locale"%3A"ch"%7D%2C"special"%3A%7B"action"%3A"getOrderStatistics"%7D%7D&action=getOrderStatistics";
        //    //str = "action=setOrderInfoTwo&ReqContent={\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"e703dbedadd943abacf864531decdac1\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"f9580b2fb5b74c3aa2526021f435609a\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"f9580b2fb5b74c3aa2526021f435609a\"},\"special\":{\"tableNumber\":\"\",\"status\":\"100\",\"remark\":\"\",\"qty\":1,\"username\":\"李四\",\"deliveryId\":\"1\",\"deliveryTime\":\"18:53:33\",\"email\":\"\",\"totalAmount\":0.01,\"aldMemberID\":\"\",\"orderDetailList\":[{\"beginDate\":null,\"endDate\":null,\"qty\":1,\"salesPrice\":0.01,\"skuId\":\"d0bd5fbb233b6cb15bf15c716066c0cf\"}],\"storeId\":\"1cb675b011f14d388538ef2c60bba8b8\",\"deliveryAddress\":\"哈哈\",\"mobile\":\"123456\"}}";
        //    var res = SendHttpRequest2(url, str);
        //    Console.WriteLine(res);
        //}

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

        public static string SendHttpRequest2(string requestURI, string json)
        {
            //拼接URL
            string serviceUrl = requestURI + "?" + json;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

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
