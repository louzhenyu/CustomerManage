using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity.User;
using JIT.Utility.Web;

namespace JIT.CPOS.Web.LJ.data
{
    public partial class Data : System.Web.UI.Page
    {
        string customerId = "29E11BDC6DAC439896958CC6866FF64E";
        string customerId_Lj = "e703dbedadd943abacf864531decdac1";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["action"].ToString().Trim();
                switch (dataType)
                {
                    case "getItemList":      //1.获取商品列表
                        content = getItemList();
                        break;
                    case "getItemTypeList": //2.获取福利商品类别集合
                        content = getItemTypeList();
                        break;
                    case "getItemDetail":   //4.获取福利商品明细信息
                        content = getItemDetail();
                        break;
                    case "getSkuProp2List": //20.获取商品属性2的集合
                        content = getSkuProp2List();
                        break;
                    case "getStoreListByItem":
                        content = getStoreListByItem(); //5.获取福利商品的门店集合 Jermyn
                        break;
                    case "getStoreDetail":  // 6.获取门店详细信息 Jermyn
                        content = getStoreDetail();
                        break;
                    case "getBrandDetail":
                        content = getBrandDetail(); //7.获取品牌详细信息 Jermyn
                        break;
                    case "setDownloadItem":     //8.立即下载提交 setDownloadItem Jermyn
                        content = setDownloadItem();
                        break;
                    case "getDownloadUsersByItem": //9.获取商品的下载用户集合  Jermyn
                        content = getDownloadUsersByItem();
                        break;
                    case "setOrderInfo":
                        content = setOrderInfoNew();   //10 提交订单 Jermyn
                        break;
                    case "setOrderInfoOne":
                        content = setOrderInfo();   //10 提交订单 Jermyn 20131023打开，单个商品提交
                        break;
                    case "setUpdateOrderDelivery":      //20131024 处理订单配送信息修改
                        content = setUpdateOrderDelivery();
                        break;
                    case "setOrderPayment":
                        content = setOrderPayment();  //11 订单支付提交 Jermyn
                        break;
                    case "getPaymentTypeList":
                        content = getPaymentTypeList(); //12 获取支付方式的集合 Jermyn
                        break;
                    case "getDeliveryList": //13.配送方式 Jermyn
                        content = getDeliveryList();
                        break;
                    case "getOrderInfo":    //14.获取订单详细信息 Jermyn
                        content = getOrderInfo();
                        break;
                    case "setItemKeep":     //15.设置福利订单收藏与取消收藏 Jermyn
                        content = setItemKeep();
                        break;
                    case "getVipDetail":     //16.获取VIP用户的详细信息 Jermyn
                        content = getVipDetail();
                        break;
                    case "setVipDetail":     //17.设置VIP用户的详细信息 Jermyn
                        content = setVipDetail();
                        break;
                    case "getCustomerDetail":       //获取在线商城的基本设置信息
                        content = getCustomerDetail();
                        break;
                    #region 泸州老窖达人秀接口
                    case "getEventsEntriesList":        //1.获取当天的参赛作品
                        content = getEventsEntriesList();
                        break;
                    case "setEventSignUp":              //2.报名提交（评论报名）
                        content = setEventSignUp();
                        break;
                    case "setEventsEntriesPraise":      //3.赞提交（对作品赞）
                        content = setEventsEntriesPraise();
                        break;
                    case "setEventsEntriesComment":     //4.评论提交（对作品评论）
                        content = setEventsEntriesComment();
                        break;
                    case "getEventsEntriesCommentList": //5.获取参赛作品的评论与赞
                        content = getEventsEntriesCommentList();
                        break;
                    case "getEventsEntriesWinners":     //6.获取获奖名单
                        content = getEventsEntriesWinners();
                        break;
                    case "getEventsEntriesMonthDaren":  //7.获取品味达人作品集
                        content = getEventsEntriesMonthDaren();
                        break;

                    #endregion

                    #region 中欧接口
                    case "getForumEntriesList":     //获取大型论坛或者招生活动的标题集合 
                        content = getForumEntriesList();
                        break;
                    case "getForumDetail":     //获取论坛或者招生活动的明细  
                        content = getForumDetail();
                        break;
                    case "getCourseDetail":     //获取课程管理的详细信息   
                        content = getCourseDetail();
                        break;
                    case "setCourseApply":     //课程报名提交   
                        content = setCourseApply();
                        break;
                    case "getHighLevelCourseList":     //获取高级课程集合 
                        content = getHighLevelCourseList();
                        break;
                    case "getNewsListByCourseId":  //获取课程新闻
                        content = getNewsListByCourseId();
                        break;
                    case "getNewsById":     //获取新闻详细信息
                        content = getNewsById();
                        break;
                    case "getZONewsOrZKList":
                        content = getZONewsOrZKList();
                        break;
                    case "getZONewsOrZKDetail":
                        content = getZONewsOrZKDetail();
                        break;
                    #endregion
                    case "setBrowseHistory":     //浏览历史提交   
                        content = setBrowseHistory();
                        break;
                    case "setShoppingCart":     //提交购物车信息   单个商品
                        content = setShoppingCart();
                        break;
                    case "setShoppingCartList":     //提交购物车信息   多个商品
                        content = setShoppingCartList();
                        break;
                    case "getShoppingCart":     //获取购物车商品信息 
                        content = getShoppingCart();
                        break;
                    case "getBrowseHistory":     //获取浏览历史商品信息 
                        content = getBrowseHistory();
                        break;
                    case "getItemKeep":     //获取收藏商品信息 
                        content = getItemKeep();
                        break;
                    case "getOrderList":     //获取各种状态的订单信息 
                        content = getOrderList();
                        break;
                    case "getItemExchangeList":     //获取积分兑换商品信息集合 
                        content = getItemExchangeList();
                        break;
                    case "setOrderStatus":     //订单状态修改 
                        content = setOrderStatus();
                        break;
                    case "setSignUp":
                        content = setSignUp();
                        break;
                    case "setSignIn":
                        content = setSignIn();
                        break;
                    case "getCityList":     //城市集合
                        content = getCityList();
                        break;
                    case "getStoreListByCity":
                        content = getStoreListByCity();
                        break;
                    case "getDateList": //获取日期集合
                        content = getDateList();
                        break;
                    #region 洗衣服系统
                    case "setGOrderInfo": //提交订单
                        content = setGOrderInfo();
                        break;
                    case "setGOrderUpdateStatus": //确认收单
                        content = setGOrderUpdateStatus();
                        break;
                    case "getGOrderInfo":
                        content = getGOrderInfo();
                        break;
                    #endregion
                    case "getVipShowList": //获取会员秀集合
                        content = getVipShowList();
                        break;
                    case "getVipTags": //获取会员标签集合 zhangwei 20130113
                        content = getVipTags();
                        break;
                    case "getVipShowById": //获取单个会员秀的详细信息
                        content = getVipShowById();
                        break;
                    case "getHairStylistByStoreId": //根据门店获取发型师
                        content = getHairStylistByStoreId();
                        break;
                    case "setSMSPush":      //设置密码推送
                        content = setSMSPush();
                        break;
                    case "setVipPassword":      //设置密码修改
                        content = setVipPassword();
                        break;
                    case "setVipShow":
                        content = setVipShow();
                        break;
                    case "setIOSDeviceToken":  //设置IOS的deviceToken
                        content = setIOSDeviceToken();
                        break;
                    case "setAndroidBasic":  //设置Android的参数设置
                        content = setAndroidBasic();
                        break;
                    case "checkVersion":    //APP版本更新
                        content = checkVersion();
                        break;
                    case "getNewsList":     //发布会集合
                        content = getNewsList();
                        break;
                    case "getLotteryList":  //个人抽奖信息集合
                        content = getLotteryList();
                        break;
                    case "setLottery":      //抽奖日志
                        content = setLottery();
                        break;
                    case "getEventPrizes":  //奖项
                        content = getEventPrizes();
                        break;
                    case "getSchoolEventList":
                        content = getSchoolEventList();
                        break;
                    case "setSchoolEventList":
                        content = setSchoolEventList();
                        break;
                    #region o2omarketing 系统
                    case "getMarketEventAnalysis": //3.2.1 获取分析数据
                        content = getMarketEventAnalysis();
                        break;
                    case "setEventRountStatus": //设置活动轮次刮奖状态
                        content = setEventRountStatus();
                        break;
                    case "setContact":          //3.2.2 提交联系方式
                        content = setContact();
                        break;
                    case "getOnlinePosOrder":   //3.2.3 获取门店的销售数据（在线）
                        content = getOnlinePosOrder();
                        break;
                    case "setOnlinePosOrderStatus": //3.2.4 提交未处理的订单完成操作
                        content = setOnlinePosOrderStatus();
                        break;
                    #endregion

                    #region pad html5
                    case "getDimensionalCode": //3.3.1 获取二维码生成参数
                        content = getDimensionalCode();
                        break;
                    case "getPoll":            //3.3.2 获取二维码生成参数
                        content = getPoll();
                        break;
                    #endregion

                    case "getClientUserInfo":       //2.2.21获取客户端信息
                        content = getClientUserInfo();
                        break;
                    case "setToBase64String":       //压缩
                        content = setToBase64String();
                        break;
                    case "bindRecommender":
                        content = BindRecommender();
                        break;
                    case "getIntegralRuleList":
                        content = GetIntegralRule();
                        break;
                    case "getIntegralDetailList":
                        content = GetVipIntegralDetail();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }

            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region 关联领奖人和推荐人
        /// <summary>
        /// 关联领奖人和推荐人
        /// </summary>
        /// <returns></returns>
        public string BindRecommender()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();

            string reqContent = Request["ReqContent"];

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("getItemList: {0}", reqContent)
            });

            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

            //解析请求字符串
            var reqObj = reqContent.DeserializeJSONTo<BindRecommenderReqData>();
            var vipBLL = new VipBLL(loggingSessionInfo);
            var vip = vipBLL.GetVipDetailByVipID(reqObj.common.userId);
            vip.HigherVipID = reqObj.special.Recommender;
            //vipBLL.Update(vip);

            return vip.ToJSON();
            //return content;
        }

        public class BindRecommenderReqData : ReqData
        {
            public BindRecommenderReqSpecialData special;
        }
        public class BindRecommenderReqSpecialData
        {
            public string Recommender { get; set; }	//推荐人openId
        }

        #endregion

        #region 1.获取商品列表
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public string getItemList()
        {
            string content = string.Empty;
            var respData = new getItemListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getItemListReqData>();
                reqObj = reqObj == null ? new getItemListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getItemListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string itemName = reqObj.special.itemName;      //模糊查询商品名称
                string itemTypeId = reqObj.special.itemTypeId;  //活动标识
                string isExchange = reqObj.special.isExchange;
                int page = reqObj.special.page;         //页码
                int pageSize = reqObj.special.pageSize; //页面数量
                string storeId = ToStr(reqObj.special.storeId);

                //初始化返回对象
                respData.content = new getItemListRespContentData();
                respData.content.itemList = new List<getItemListRespContentDataItem>();
                respData.content.ItemKeeps = new List<getItemListRespContentDataItem>();
                ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(loggingSessionInfo);
                respData.content.shoppingCartCount = shoppingCartServer.GetShoppingCartByVipId(userId);
                ItemService itemService = new ItemService(loggingSessionInfo);

                var dsItems = itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, false, isExchange, storeId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.itemList = DataTableToObject.ConvertToList<getItemListRespContentDataItem>(dsItems.Tables[0]);
                    var totalCount = itemService.GetWelfareItemListCount(userId, itemName, itemTypeId, false, isExchange, storeId);
                    int PageCount = totalCount / Convert.ToInt32(pageSize);
                    if (totalCount % Convert.ToInt32(pageSize) > 0)
                    {
                        PageCount += 1;
                    }
                    if (PageCount > Convert.ToInt32(page))
                    {
                        respData.content.isNext = "1";
                    }
                    else
                    {
                        respData.content.isNext = "0";
                    }
                }

                var dsItemKeeps = itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, true, isExchange, storeId);
                if (dsItemKeeps != null && dsItemKeeps.Tables.Count > 0 && dsItemKeeps.Tables[0].Rows.Count > 0)
                {
                    respData.content.ItemKeeps = DataTableToObject.ConvertToList<getItemListRespContentDataItem>(dsItemKeeps.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getItemListRespData : Default.LowerRespData
        {
            public getItemListRespContentData content { get; set; }
        }
        public class getItemListRespContentData
        {
            public string isNext { get; set; }      //是否有下一页
            public int shoppingCartCount { get; set; } //购物车数量
            public IList<getItemListRespContentDataItem> itemList { get; set; }     //商品集合
            public IList<getItemListRespContentDataItem> ItemKeeps { get; set; } //已收藏的福利列表
        }
        public class getItemListRespContentDataItem
        {
            public string itemId { get; set; }       //商品标识
            public string itemName { get; set; }     //商品名称（譬如：浪漫主题房）
            public string imageUrl { get; set; }     //图片链接地址
            public decimal price { get; set; }        //商品原价
            public decimal salesPrice { get; set; }   //商品零售价（优惠价）
            public decimal discountRate { get; set; } //商品折扣
            public Int64 displayIndex { get; set; }  //排序
            public string pTypeId { get; set; }      //福利类别标识（团购=2，优惠=1）
            public string pTypeCode { get; set; }    //福利类别缩写（券，团）
            public string CouponURL { get; set; }    //优惠券下载地址
            public int integralExchange { get; set; }    //优惠券下载地址
            public string itemCategoryName { get; set; } //类别名称 Jermyn20131008
            public int salesPersonCount { get; set; }    //已购买人数量
            public int isShoppingCart { get; set; }      //是否已经加入购物车（1=已加入，0=未加入）
            public string skuId { get; set; }
            public string createDate { get; set; }      //创建日期
        }
        public class getItemListReqData : ReqData
        {
            public getItemListReqSpecialData special;
        }
        public class getItemListReqSpecialData
        {
            public string itemName { get; set; }    //模糊查询商品名称
            public string itemTypeId { get; set; }  //商品类别标识
            public int page { get; set; }           //页码
            public int pageSize { get; set; }       //页面数量
            public string isExchange { get; set; }  //兑换商品
            public string storeId { get; set; }     //门店标识 Jermyn20131008
        }
        #endregion

        #region 2.获取福利商品类别集合
        /// <summary>
        /// 获取福利商品类别集合
        /// </summary>
        /// <returns></returns>
        public string getItemTypeList()
        {
            string content = string.Empty;
            var respData = new getItemTypeListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemTypeList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getItemTypeListReqData>();
                reqObj = reqObj == null ? new getItemTypeListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getItemTypeListReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getItemTypeListRespContentData();
                respData.content.itemTypeList = new List<getItemTypeListRespContentDataItemType>();

                ItemCategoryService categoryService = new ItemCategoryService(loggingSessionInfo);

                var dsItems = categoryService.GetItemTypeList(customerId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.itemTypeList = DataTableToObject.ConvertToList<getItemTypeListRespContentDataItemType>(dsItems.Tables[0]);
                    foreach (var itemTypeInfo in respData.content.itemTypeList)
                    {
                        if (itemTypeInfo.itemTypeName.Equals("全部"))
                        {
                            itemTypeInfo.itemTypeId = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getItemTypeListRespData : Default.LowerRespData
        {
            public getItemTypeListRespContentData content { get; set; }
        }
        public class getItemTypeListRespContentData
        {
            public IList<getItemTypeListRespContentDataItemType> itemTypeList { get; set; }     //商品类别集合
        }
        public class getItemTypeListRespContentDataItemType
        {
            public string itemTypeId { get; set; }      //商品类别标识
            public string itemTypeName { get; set; }    //商品类别名称（譬如：日常用品）
            public string itemTypeCode { get; set; }    //商品号码
            public Int64 displayIndex { get; set; }     //排序
        }
        public class getItemTypeListReqData : ReqData
        {
            public getItemTypeListReqSpecialData special;
        }
        public class getItemTypeListReqSpecialData
        {

        }
        #endregion

        #region 4.获取福利商品明细信息
        /// <summary>
        /// 获取福利商品明细信息
        /// </summary>
        /// <returns></returns>
        public string getItemDetail()
        {
            string content = string.Empty;
            var respData = new getItemDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemDetail: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getItemDetailReqData>();
                reqObj = reqObj == null ? new getItemDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getItemDetailReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string itemId = reqObj.special.itemId;    //商品标识

                //初始化返回对象
                respData.content = new getItemDetailRespContentData();

                ItemService itemService = new ItemService(loggingSessionInfo);
                //商品基本信息
                var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
                }

                respData.content.imageList = new List<getItemDetailRespContentDataImage>();
                respData.content.skuList = new List<getItemDetailRespContentDataSku>();
                respData.content.salesUserList = new List<getItemDetailRespContentDataSalesUser>();
                respData.content.storeInfo = new getItemDetailRespContentDataStore();
                respData.content.brandInfo = new getItemDetailRespContentDataBrand();
                respData.content.skuInfo = new getItemDetailRespContentDataSkuInfo();
                respData.content.prop1List = new List<getItemDetailRespContentDataProp1>();

                //商品图片信息
                var dsImages = itemService.GetItemImageList(itemId);
                if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
                {
                    respData.content.imageList = DataTableToObject.ConvertToList<getItemDetailRespContentDataImage>(dsImages.Tables[0]);
                }
                //商品sku信息
                var dsSkus = itemService.GetItemSkuList(itemId);
                if (dsSkus != null && dsSkus.Tables.Count > 0 && dsSkus.Tables[0].Rows.Count > 0)
                {
                    respData.content.skuList = DataTableToObject.ConvertToList<getItemDetailRespContentDataSku>(dsSkus.Tables[0]);
                    //Jermyn20131121 获取sku对象
                    SkuInfo skuInfo = new SkuInfo();
                    SkuService skuService = new SkuService(loggingSessionInfo);
                    skuInfo = skuService.GetSkuInfoById(respData.content.skuList[0].skuId);
                    respData.content.skuInfo.skuId = skuInfo.sku_id;
                    respData.content.skuInfo.prop1DetailId = skuInfo.prop_1_detail_id;
                    respData.content.skuInfo.prop2DetailId = skuInfo.prop_2_detail_id;
                    //---------------------------------------------------------------------
                }
                //购买用户集合
                var dsSalesUsers = itemService.GetItemSalesUserList(itemId);
                if (dsSalesUsers != null && dsSalesUsers.Tables.Count > 0 && dsSalesUsers.Tables[0].Rows.Count > 0)
                {
                    respData.content.salesUserList = DataTableToObject.ConvertToList<getItemDetailRespContentDataSalesUser>(dsSalesUsers.Tables[0]);
                }

                //门店信息
                var dsStore = itemService.GetItemStoreInfo(itemId);
                if (dsStore != null && dsStore.Tables.Count > 0 && dsStore.Tables[0].Rows.Count > 0)
                {
                    respData.content.storeInfo = DataTableToObject.ConvertToObject<getItemDetailRespContentDataStore>(dsStore.Tables[0].Rows[0]);
                }
                //品牌信息
                var dsBrand = itemService.GetItemBrandInfo(itemId);
                if (dsBrand != null && dsBrand.Tables.Count > 0 && dsBrand.Tables[0].Rows.Count > 0)
                {
                    respData.content.brandInfo = DataTableToObject.ConvertToObject<getItemDetailRespContentDataBrand>(dsBrand.Tables[0].Rows[0]);
                }
                #region 获取商品属性集合
                var dsProp1 = itemService.GetItemProp1List(itemId);
                if (dsProp1 != null && dsProp1.Tables.Count > 0 && dsProp1.Tables[0].Rows.Count > 0)
                {
                    respData.content.prop1List = DataTableToObject.ConvertToList<getItemDetailRespContentDataProp1>(dsProp1.Tables[0]);
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getItemDetailRespData : Default.LowerRespData
        {
            public getItemDetailRespContentData content { get; set; }
        }
        public class getItemDetailRespContentData
        {
            public string itemId { get; set; }      //商品标识
            public string itemName { get; set; }    //商品名称（譬如：浪漫主题房）
            public string pTypeId { get; set; }     //福利类别标识（团购=2，优惠=1）
            public string pTypeCode { get; set; }   //福利类别缩写（券，团）
            public string buyType { get; set; }     //优惠团购类型（1=预定2=购买；特别注意判断是否卖光，卖光了，但是没有下架，则为3，表示卖完啦）
            public string buyTypeDesc { get; set; } //根据buyType,显示1=马上预订，2=立即抢购，3=卖完啦
            public int salesPersonCount { get; set; }    //已购买人数量
            public int downloadPersonCount { get; set; } //已下载数量
            public decimal overCount { get; set; }  //剩余数量
            public string useInfo { get; set; }     //使用须知
            public string tel { get; set; }         //联系电话
            public string endTime { get; set; }     //下架日期
            public string couponURL { get; set; }   //优惠券下载地址
            public string offersTips { get; set; }  //优惠提示
            public int isKeep { get; set; }         //是否已收藏 1=是，0=否
            public int isShoppingCart { get; set; }  //是否已经加入购物车（1=已加入，0=未加入）
            public string prop1Name { get; set; }   //属性1名称
            public string prop2Name { get; set; }   //属性2名称
            public string isProp2 { get; set; }     //是否有属性2；1=有，0=无
            public string itemCategoryId { get; set; }
            public string itemCategoryName { get; set; }
            public IList<getItemDetailRespContentDataImage> imageList { get; set; }     //图片集合
            public IList<getItemDetailRespContentDataSku> skuList { get; set; }         //sku集合
            public IList<getItemDetailRespContentDataSalesUser> salesUserList { get; set; }   //购买用户集合
            public getItemDetailRespContentDataStore storeInfo { get; set; }            //门店对象（一家门店）
            public getItemDetailRespContentDataBrand brandInfo { get; set; }            //品牌信息
            public getItemDetailRespContentDataSkuInfo skuInfo { get; set; }                //默认sku标识
            public IList<getItemDetailRespContentDataProp1> prop1List { get; set; }     //属性1集合
        }
        public class getItemDetailRespContentDataImage
        {
            public string imageId { get; set; }     //图片标识
            public string imageURL { get; set; }    //图片链接地址
        }
        public class getItemDetailRespContentDataSku
        {
            public string skuId { get; set; }        //sku标识
            public string skuProp1 { get; set; }     //规格
            public string skuProp2 { get; set; }
            public decimal price { get; set; }       //原价
            public decimal salesPrice { get; set; }  //优惠价（零售价格）
            public decimal discountRate { get; set; }//折扣
            public decimal integral { get; set; }    //获得积分
        }
        public class getItemDetailRespContentDataSalesUser
        {
            public string userId { get; set; }      //用户标识
            public string imageURL { get; set; }    //用户头像链接地址
        }
        public class getItemDetailRespContentDataStore
        {
            public string storeId { get; set; }     //门店标识
            public string storeName { get; set; }   //门店名称
            public string address { get; set; }     //门店地址
            public string imageURL { get; set; }    //门店图片连接地址
            public int storeCount { get; set; }  //门店数量
        }
        public class getItemDetailRespContentDataBrand
        {
            public string brandId { get; set; }         //品牌标识
            public string brandLogoURL { get; set; }    //品牌logo图片链接地址
            public string brandName { get; set; }       //品牌名称
            public string brandEngName { get; set; }    //品牌英文名
        }
        public class getItemDetailReqData : ReqData
        {
            public getItemDetailReqSpecialData special;
        }
        public class getItemDetailReqSpecialData
        {
            public string itemId { get; set; }    //商品标识
        }
        /// <summary>
        /// 默认的sku信息
        /// </summary>
        public class getItemDetailRespContentDataSkuInfo
        {
            public string skuId { get; set; }               //sku标识
            public string prop1DetailId { get; set; }       //属性1明细标识
            public string prop2DetailId { get; set; }       //属性2明细标识
        }

        public class getItemDetailRespContentDataProp1
        {
            public string skuId { get; set; }               //sku标识
            public string prop1DetailId { get; set; }       //属性1明细标识
            public string prop1DetailName { get; set; }     //属性1明细名称
        }
        #endregion

        #region 5.获取福利商品的门店集合 getStoreListByItem();
        public string getStoreListByItem()
        {
            string content = string.Empty;
            var respData = new getStoreListByItemRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getStoreListByItem: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreListByItemReqData>();
                reqObj = reqObj == null ? new getStoreListByItemReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreListByItemReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 10;
                }
                //if (reqObj.special.itemId == null || reqObj.special.itemId.Equals(""))
                //{
                //    respData.code = "2201";
                //    respData.description = "itemId不能为空";
                //    return respData.ToJSON().ToString();
                //}
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion
                //查询参数
                string userId = reqObj.common.userId;

                respData.content = new getStoreListByItemRespContentData();
                respData.content.storeList = new List<getStoreListByItemRespContentItemTypeData>();
                respData.content.imageList = new List<getStoreListByItemImageList>();
                #region
                string strError = string.Empty;
                #region 获取门店的图片集合
                ObjectImagesBLL objectServer = new ObjectImagesBLL(loggingSessionInfo);
                var objectList = objectServer.GetObjectImagesByCustomerId(loggingSessionInfo.CurrentLoggingManager.Customer_Id);
                IList<getStoreListByItemImageList> ImageList = new List<getStoreListByItemImageList>();
                if (objectList != null && objectList.Count > 0)
                {
                    foreach (var objectInfo in objectList)
                    {
                        getStoreListByItemImageList imageInfo = new getStoreListByItemImageList();
                        imageInfo.imageURL = objectInfo.ImageURL;
                        ImageList.Add(imageInfo);
                    }
                    respData.content.imageList = ImageList;
                }
                #endregion
                StoreBrandMappingBLL server = new StoreBrandMappingBLL(loggingSessionInfo);
                var storeInfo = server.GetStoreListByItem(reqObj.special.itemId
                                                            , reqObj.special.page
                                                            , reqObj.special.pageSize
                                                            , reqObj.special.longitude
                                                            , reqObj.special.latitude
                                                            , out strError);
                if (strError.Equals("ok"))
                {
                    IList<getStoreListByItemRespContentItemTypeData> list = new List<getStoreListByItemRespContentItemTypeData>();
                    foreach (var store in storeInfo.StoreBrandList)
                    {
                        getStoreListByItemRespContentItemTypeData info = new getStoreListByItemRespContentItemTypeData();
                        info.storeId = ToStr(store.StoreId);
                        info.storeName = ToStr(store.StoreName);
                        info.imageURL = ToStr(store.ImageUrl);
                        info.address = ToStr(store.Address);
                        info.tel = ToStr(store.Tel);
                        info.displayIndex = ToStr(store.DisplayIndex);
                        info.lng = ToStr(store.Longitude);
                        info.lat = ToStr(store.Latitude);
                        if (store.Distance.ToString().Equals("0"))
                        {
                            info.distance = "";
                        }
                        else
                        {
                            info.distance = ToStr(store.Distance) + "km";
                        }
                        list.Add(info);
                    }
                    respData.content.storeList = list;
                    respData.content.totalCount = ToInt(storeInfo.TotalCount);
                }
                else
                {
                    respData.code = "111";
                    respData.description = "数据库操作错误";
                    respData.exception = strError;
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getStoreListByItemRespData : Default.LowerRespData
        {
            public getStoreListByItemRespContentData content { get; set; }
        }
        public class getStoreListByItemRespContentData
        {
            public int totalCount { get; set; }
            public IList<getStoreListByItemRespContentItemTypeData> storeList { get; set; }     //商品类别集合
            public IList<getStoreListByItemImageList> imageList { get; set; }
        }
        public class getStoreListByItemRespContentItemTypeData
        {
            public string storeId { get; set; }      //支付方式标识
            public string storeName { get; set; }    //支付产品类别
            public string imageURL { get; set; }
            public string displayIndex { get; set; }
            public string address { get; set; }
            public string tel { get; set; }
            public string distance { get; set; }        //距离
            public string lng { get; set; }     //经度
            public string lat { get; set; }     //维度
        }
        public class getStoreListByItemImageList
        {
            public string imageURL { get; set; }
        }
        public class getStoreListByItemReqData : ReqData
        {
            public getStoreListByItemReqSpecialData special;
        }
        public class getStoreListByItemReqSpecialData
        {
            public string itemId { get; set; }      //商品标识
            public int page { get; set; }
            public int pageSize { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
        }
        #endregion
        #endregion

        #region 6.获取门店详细信息
        public string getStoreDetail()
        {
            string content = string.Empty;
            var respData = new getStoreDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getBrandDetail: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreDetailReqData>();
                reqObj = reqObj == null ? new getStoreDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                if (reqObj.special.storeId == null || reqObj.special.storeId.Equals(""))
                {
                    //respData.code = "2201";
                    //respData.description = "storeId不能为空";
                    //return respData.ToJSON().ToString();
                    //Jermyn20131011 如果为空，获取总部id
                    UnitService unitServer = new UnitService(loggingSessionInfo);
                    reqObj.special.storeId = unitServer.GetHeadStoreId(customerId);
                }


                #endregion

                #region 业务处理
                respData.content = new getStoreDetailRespContentData();
                respData.content.imageList = new List<gSDRespContentDataImageList>();
                List<gSDRespContentDataImageList> list = new List<gSDRespContentDataImageList>();
                StoreBrandMappingBLL serer = new StoreBrandMappingBLL(loggingSessionInfo);
                StoreBrandMappingEntity orderInfo = new StoreBrandMappingEntity();
                orderInfo = serer.GetStoreDetail(reqObj.special.storeId);
                if (orderInfo != null)
                {
                    respData.content.brandId = ToStr(orderInfo.BrandId);
                    respData.content.brandName = ToStr(orderInfo.BrandName);
                    respData.content.brandEngName = ToStr(orderInfo.BrandEngName);
                    respData.content.storeId = ToStr(orderInfo.StoreId);
                    respData.content.storeName = ToStr(orderInfo.StoreName);
                    respData.content.displayIndex = ToStr(orderInfo.DisplayIndex);
                    respData.content.tel = ToStr(orderInfo.Tel);
                    respData.content.longitude = ToStr(orderInfo.Longitude);
                    respData.content.latitude = ToStr(orderInfo.Latitude);
                    respData.content.address = ToStr(orderInfo.Address);
                    respData.content.imageURL = ToStr(orderInfo.ImageUrl);
                    respData.content.remark = ToStr(orderInfo.UnitRemark);
                    if (orderInfo.ImageList != null && orderInfo.ImageList.Count > 0)
                    {

                        foreach (var imageInfo in orderInfo.ImageList)
                        {
                            gSDRespContentDataImageList info = new gSDRespContentDataImageList();
                            info.imageId = imageInfo.ImageId;
                            info.imageURL = imageInfo.ImageURL;
                            list.Add(info);
                        }
                        respData.content.imageList = list;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getStoreDetailRespData : Default.LowerRespData
        {
            public getStoreDetailRespContentData content { get; set; }
        }
        public class getStoreDetailRespContentData
        {
            public string brandId { get; set; }         //品牌标识
            public string brandName { get; set; }       //品牌名称
            public string brandEngName { get; set; }    //品牌英文名
            public string storeId { get; set; }         //门店标识
            public string storeName { get; set; }       //门店名称
            public string displayIndex { get; set; }    //序号
            public string address { get; set; }
            public string tel { get; set; }             //客服电话
            public string longitude { get; set; }
            public string latitude { get; set; }
            public string imageURL { get; set; }
            public string remark { get; set; }
            public IList<gSDRespContentDataImageList> imageList { get; set; } //门店图片集合
        }

        public class gSDRespContentDataImageList
        {
            public string imageId { get; set; }
            public string imageURL { get; set; }
        }

        public class getStoreDetailReqData : ReqData
        {
            public getStoreDetailReqSpecialData special;
        }
        public class getStoreDetailReqSpecialData
        {
            public string storeId { get; set; }
        }
        #endregion
        #endregion

        #region 7.获取品牌详细信息
        public string getBrandDetail()
        {
            string content = string.Empty;
            var respData = new getBrandDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getBrandDetail: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getBrandDetailReqData>();
                reqObj = reqObj == null ? new getBrandDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getBrandDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.brandId == null || reqObj.special.brandId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "brandId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 业务处理
                respData.content = new getBrandDetailRespContentData();
                BrandDetailBLL serer = new BrandDetailBLL(loggingSessionInfo);
                BrandDetailEntity orderInfo = new BrandDetailEntity();
                orderInfo = serer.GetByID(reqObj.special.brandId);
                if (orderInfo != null)
                {
                    respData.content.brandId = ToStr(orderInfo.BrandId);
                    respData.content.brandName = ToStr(orderInfo.BrandName);
                    respData.content.brandEngName = ToStr(orderInfo.BrandEngName);
                    respData.content.brandDesc = ToStr(orderInfo.BrandDesc);
                    respData.content.brandLogoURL = ToStr(orderInfo.BrandLogoURL);
                    respData.content.tel = ToStr(orderInfo.Tel);

                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getBrandDetailRespData : Default.LowerRespData
        {
            public getBrandDetailRespContentData content { get; set; }
        }
        public class getBrandDetailRespContentData
        {
            public string brandId { get; set; }         //品牌标识
            public string brandName { get; set; }       //品牌名称
            public string brandEngName { get; set; }    //品牌英文名
            public string brandDesc { get; set; }       //品牌描述
            public string brandLogoURL { get; set; }    //品牌logo
            public string tel { get; set; }             //客服电话
        }

        public class getBrandDetailReqData : ReqData
        {
            public getBrandDetailReqSpecialData special;
        }
        public class getBrandDetailReqSpecialData
        {
            public string brandId { get; set; }
        }
        #endregion
        #endregion

        #region 8.立即下载提交 setDownloadItem
        public string setDownloadItem()
        {
            string content = string.Empty;
            var respData = new setDownloadItemRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setDownloadItem: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setDownloadItemReqData>();
                reqObj = reqObj == null ? new setDownloadItemReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setDownloadItemReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.itemId == null || reqObj.special.itemId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "itemId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "common.userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                ItemDownloadLogBLL service = new ItemDownloadLogBLL(loggingSessionInfo);
                ItemDownloadLogEntity Info = new ItemDownloadLogEntity();
                Info.ItemId = ToStr(reqObj.special.itemId);
                Info.UserId = ToStr(reqObj.common.userId);
                Info.CreateBy = ToStr(reqObj.common.userId);
                Info.LastUpdateBy = ToStr(reqObj.common.userId);
                Info.CreateTime = System.DateTime.Now;
                Info.LastUpdateTime = System.DateTime.Now;

                #endregion
                string strError = string.Empty;
                bool bReturn = service.SetItemDownloadLog(Info, out strError);
                if (!bReturn)
                {
                    respData.code = "111";
                    respData.exception = strError;
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setDownloadItemRespData : Default.LowerRespData
        {
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setDownloadItemRespContentData
        {

        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setDownloadItemReqData : ReqData
        {
            public setDownloadItemReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setDownloadItemReqSpecialData
        {
            public string itemId { get; set; }
        }
        #endregion
        #endregion

        #region 9.获取商品的下载用户集合 getDownloadUsersByItem
        public string getDownloadUsersByItem()
        {
            string content = string.Empty;
            var respData = new getDownloadUsersByItemRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemTypeList: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getDownloadUsersByItemReqData>();
                reqObj = reqObj == null ? new getDownloadUsersByItemReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getDownloadUsersByItemReqSpecialData();
                    reqObj.special.page = "1";
                    reqObj.special.pageSize = "15";
                }
                if (reqObj.special.itemId == null || reqObj.special.itemId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "itemId不能为空";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                //查询参数
                string userId = reqObj.common.userId;

                respData.content = new getDownloadUsersByItemRespContentData();
                respData.content.userList = new List<getDownloadUsersByItemRespContentItemTypeData>();
                string strError = string.Empty;
                ItemDownloadLogBLL server = new ItemDownloadLogBLL(loggingSessionInfo);
                var itemDownloadLogInfo = server.GetDownloadUsersByItem(reqObj.special.itemId
                                                                        , Convert.ToInt32(reqObj.special.page)
                                                                        , Convert.ToInt32(reqObj.special.pageSize)
                                                                        , out strError
                                                                        );
                IList<getDownloadUsersByItemRespContentItemTypeData> list = new List<getDownloadUsersByItemRespContentItemTypeData>();
                if (strError.Equals("ok"))
                {
                    respData.content.totalCount = ToStr(itemDownloadLogInfo.TotalCount);
                    foreach (var itemInfo in itemDownloadLogInfo.ItemDownloadLogList)
                    {
                        getDownloadUsersByItemRespContentItemTypeData info = new getDownloadUsersByItemRespContentItemTypeData();
                        info.userId = ToStr(itemInfo.UserId);
                        info.userName = ToStr(itemInfo.UserName);
                        info.imageURL = ToStr(itemInfo.ImageUrl);
                        info.displayIndex = ToStr(itemInfo.DisplayIndex);
                        list.Add(info);
                    }
                }
                else
                {
                    respData.code = "111";
                    respData.description = "有错误";
                    respData.exception = strError;
                }
                respData.content.userList = list;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getDownloadUsersByItemRespData : Default.LowerRespData
        {
            public getDownloadUsersByItemRespContentData content { get; set; }
        }
        public class getDownloadUsersByItemRespContentData
        {
            public string totalCount { get; set; }
            public IList<getDownloadUsersByItemRespContentItemTypeData> userList { get; set; }     //商品类别集合
        }
        public class getDownloadUsersByItemRespContentItemTypeData
        {
            public string userId { get; set; }                  //用户标识标识
            public string userName { get; set; }                //用户名称
            public string imageURL { get; set; }                //用户头像
            public string displayIndex { get; set; }            //排序
        }
        public class getDownloadUsersByItemReqData : ReqData
        {
            public getDownloadUsersByItemReqSpecialData special;
        }
        public class getDownloadUsersByItemReqSpecialData
        {
            public string itemId { get; set; }
            public string page { get; set; }
            public string pageSize { get; set; }
        }
        #endregion
        #endregion

        #region 10 订单提交
        public string setOrderInfo()
        {
            string content = string.Empty;
            var respData = new setOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoReqData>();
                reqObj = reqObj == null ? new setOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderInfoReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.skuId == null || reqObj.special.skuId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "skuId不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.qty == null || reqObj.special.qty.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "qty购买的数量不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.salesPrice == null || reqObj.special.salesPrice.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "salesPrice销售价格不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.stdPrice == null || reqObj.special.stdPrice.Equals(""))
                {
                    respData.code = "2204";
                    respData.description = "stdPrice原价格不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.deliveryId == null || reqObj.special.deliveryId.Equals(""))
                {
                    respData.code = "2205";
                    respData.description = "deliveryId提货方式不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.SkuId = reqObj.special.skuId;
                orderInfo.TotalQty = ToInt(reqObj.special.qty);
                if (reqObj.special.storeId == null || reqObj.special.storeId.Trim().Equals(""))
                {
                    UnitService unitServer = new UnitService(loggingSessionInfo);
                    orderInfo.StoreId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //获取在线商城的门店标识
                }
                else
                {
                    orderInfo.StoreId = ToStr(reqObj.special.storeId);
                }
                orderInfo.SalesPrice = Convert.ToDecimal(reqObj.special.salesPrice);
                orderInfo.StdPrice = Convert.ToDecimal(reqObj.special.stdPrice);
                orderInfo.TotalAmount = Convert.ToDecimal(reqObj.special.totalAmount);
                orderInfo.Mobile = ToStr(reqObj.special.mobile);
                orderInfo.Email = ToStr(reqObj.special.email);
                orderInfo.Remark = ToStr(reqObj.special.remark);
                orderInfo.CreateBy = ToStr(reqObj.common.userId);
                orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.DeliveryId = ToStr(reqObj.special.deliveryId);
                orderInfo.DeliveryTime = ToStr(reqObj.special.deliveryTime);
                orderInfo.DeliveryAddress = ToStr(reqObj.special.deliveryAddress);
                orderInfo.CustomerId = customerId;
                orderInfo.OpenId = ToStr(reqObj.common.openId);
                orderInfo.username = ToStr(reqObj.special.username);
                if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
                {
                    orderInfo.OrderId = BaseService.NewGuidPub();
                    orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    orderInfo.Status = "1";
                    orderInfo.StatusDesc = "未支付";
                    orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo, orderInfo.StoreId);
                }
                #endregion
                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderOnlineShopping(orderInfo, out strError, out strMsg);

                #region 返回信息设置
                respData.content = new setOrderInfoRespContentData();
                respData.content.orderId = orderInfo.OrderId;
                respData.exception = strError;
                respData.description = strMsg;
                if (!bReturn)
                {
                    respData.code = "111";
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setOrderInfoRespData : Default.LowerRespData
        {
            public setOrderInfoRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setOrderInfoRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setOrderInfoReqData : ReqData
        {
            public setOrderInfoReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOrderInfoReqSpecialData
        {
            public string skuId { get; set; }		//商品SKU标识
            public string qty { get; set; }		//商品数量
            public string storeId { get; set; }		//门店标识
            public string salesPrice { get; set; }		//商品单价
            public string stdPrice { get; set; }		//商品标准价格
            public string totalAmount { get; set; }		//订单总价
            public string mobile { get; set; }		//手机号码
            public string deliveryId { get; set; }//		配送方式标识
            public string deliveryAddress { get; set; }//	配送地址
            public string deliveryTime { get; set; }//		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
        }
        #endregion
        #endregion

        #region 10 订单提交 20130924 新版本 多个商品
        public string setOrderInfoNew()
        {
            string content = string.Empty;
            var respData = new setOrderInfoNewRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                //reqContent = "{"common":{"locale":"zh","userId":"c45f87741005ab3a4d9a6b6da21e9162","openId":"c45f87741005ab3a4d9a6b6da21e9162","customerId":"f6a7da3d28f74f2abedfc3ea0cf65c01"},"special":{"skuId":"","qty":"","storeId":"","salesPrice":"","stdPrice":"","totalAmount":"640","tableNumber":"","username":"","mobile":"","email":"","remark":"1","deliveryId":"1","deliveryAddress":"","deliveryTime":"","orderDetailList":[{"skuId":"648245A5233C48D9817B561139CE9548","salesPrice":"640","qty":"1"}]}}";
                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoNewReqData>();
                reqObj = reqObj == null ? new setOrderInfoNewReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderInfoNewReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderDetailList == null || reqObj.special.orderDetailList.Count == 0)
                {
                    respData.code = "2201";
                    respData.description = "必须选择商品";
                    return respData.ToJSON().ToString();
                }



                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                //orderInfo.SkuId = reqObj.special.skuId;
                int itemTotalQty = 0;
                foreach (var detail in reqObj.special.orderDetailList)
                {
                    itemTotalQty += ToInt(detail.qty);
                }
                reqObj.special.qty = itemTotalQty.ToString();
                //////////////////////////////////////////////////////////////
                string purchaseUnitId = string.Empty;
                orderInfo.TotalQty = ToInt(reqObj.special.qty);
                UnitService unitServer = new UnitService(loggingSessionInfo);
                orderInfo.StoreId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //获取在线商城的门店标识

                if (reqObj.special.storeId == null || reqObj.special.storeId.Trim().Equals(""))
                {

                }
                else
                {
                    //orderInfo.StoreId = ToStr(reqObj.special.storeId);
                    orderInfo.PurchaseUnitId = ToStr(reqObj.special.storeId);
                }

                if (orderInfo.StoreId == null || orderInfo.StoreId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "该客户未配置在线商城.";
                    return respData.ToJSON().ToString();
                }

                //orderInfo.SalesPrice = Convert.ToDecimal(reqObj.special.salesPrice);
                //orderInfo.StdPrice = Convert.ToDecimal(reqObj.special.stdPrice);
                orderInfo.TotalAmount = Convert.ToDecimal(reqObj.special.totalAmount);
                orderInfo.Mobile = ToStr(reqObj.special.mobile);
                orderInfo.Email = ToStr(reqObj.special.email);
                orderInfo.Remark = ToStr(reqObj.special.remark);
                orderInfo.CreateBy = ToStr(reqObj.common.userId);
                orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.DeliveryId = ToStr(reqObj.special.deliveryId);
                orderInfo.DeliveryTime = ToStr(reqObj.special.deliveryTime);
                orderInfo.DeliveryAddress = ToStr(reqObj.special.deliveryAddress);
                orderInfo.CustomerId = customerId;
                orderInfo.OpenId = ToStr(reqObj.common.openId);
                orderInfo.username = ToStr(reqObj.special.username);
                orderInfo.tableNumber = ToStr(reqObj.special.tableNumber);
                if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
                {
                    orderInfo.OrderId = BaseService.NewGuidPub();
                    orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    orderInfo.Status = "1";
                    orderInfo.StatusDesc = "未支付";
                    //orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo, orderInfo.StoreId);
                }
                int i = 1;
                orderInfo.OrderDetailInfoList = new List<InoutDetailInfo>();
                foreach (var detail in reqObj.special.orderDetailList)
                {
                    InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
                    orderDetailInfo.order_id = orderInfo.OrderId;
                    orderDetailInfo.order_detail_id = BaseService.NewGuidPub();
                    orderDetailInfo.sku_id = ToStr(detail.skuId);
                    orderDetailInfo.enter_qty = ToInt(detail.qty);
                    orderDetailInfo.order_qty = ToInt(detail.qty);
                    orderDetailInfo.std_price = Convert.ToDecimal(detail.salesPrice);
                    orderDetailInfo.unit_id = orderInfo.StoreId;
                    orderDetailInfo.display_index = i;
                    orderDetailInfo.enter_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.enter_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.retail_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.retail_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.Field1 = ToStr(detail.beginDate);
                    orderDetailInfo.Field2 = ToStr(detail.endDate);
                    i++;
                    orderInfo.OrderDetailInfoList.Add(orderDetailInfo);
                }
                #endregion
                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);

                #region 返回信息设置
                respData.content = new setOrderInfoNewRespContentData();
                respData.content.orderId = orderInfo.OrderId;
                respData.exception = strError;
                respData.description = strMsg;
                if (!bReturn)
                {
                    respData.code = "111";
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setOrderInfoNewRespData : Default.LowerRespData
        {
            public setOrderInfoNewRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setOrderInfoNewRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setOrderInfoNewReqData : ReqData
        {
            public setOrderInfoNewReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOrderInfoNewReqSpecialData
        {
            public string qty { get; set; }		    //商品数量
            public string storeId { get; set; }		//门店标识
            public string totalAmount { get; set; }		//订单总价
            public string mobile { get; set; }		//手机号码
            public string deliveryId { get; set; }//		配送方式标识
            public string deliveryAddress { get; set; }//	配送地址
            public string deliveryTime { get; set; }//		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
            public string tableNumber { get; set; }

            public IList<setOrderDetailClass> orderDetailList { get; set; }
        }

        public class setOrderDetailClass
        {
            public string skuId { get; set; }       //商品SKU标识
            public string salesPrice { get; set; }  //商品销售单价
            public string qty { get; set; }         //商品数量
            public string beginDate { get; set; }
            public string endDate { get; set; }
        }
        #endregion
        #endregion

        #region 20131024 订单配送信息修改
        public string setUpdateOrderDelivery()
        {
            string content = string.Empty;
            var respData = new setUpdateOrderDeliveryRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setUpdateOrderDelivery: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setUpdateOrderDeliveryReqData>();
                reqObj = reqObj == null ? new setUpdateOrderDeliveryReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setUpdateOrderDeliveryReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }


                if (reqObj.special.deliveryId == null || reqObj.special.deliveryId.Equals(""))
                {
                    respData.code = "2205";
                    respData.description = "deliveryId提货方式不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();

                orderInfo.Mobile = ToStr(reqObj.special.mobile);
                orderInfo.Email = ToStr(reqObj.special.email);
                orderInfo.Remark = ToStr(reqObj.special.remark);
                orderInfo.CreateBy = ToStr(reqObj.common.userId);
                orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.DeliveryId = ToStr(reqObj.special.deliveryId);
                orderInfo.DeliveryTime = ToStr(reqObj.special.deliveryTime);
                orderInfo.DeliveryAddress = ToStr(reqObj.special.deliveryAddress);
                orderInfo.CustomerId = customerId;
                orderInfo.OpenId = ToStr(reqObj.common.openId);
                orderInfo.username = ToStr(reqObj.special.username);
                orderInfo.StoreId = ToStr(reqObj.special.storeId);
                orderInfo.OrderId = ToStr(reqObj.special.orderId);

                #endregion

                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetUpdateOrderDelivert(orderInfo, out strError, out strMsg);

                #region 返回信息设置

                respData.exception = strError;
                respData.description = "操作成功";
                if (!bReturn)
                {
                    respData.code = "111";
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setUpdateOrderDeliveryRespData : Default.LowerRespData
        {
            //public setUpdateOrderDeliveryRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        //public class setUpdateOrderDeliveryRespContentData
        //{
        //    public string orderId { get; set; }
        //}
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setUpdateOrderDeliveryReqData : ReqData
        {
            public setUpdateOrderDeliveryReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setUpdateOrderDeliveryReqSpecialData
        {
            //public string skuId { get; set; }		//商品SKU标识
            //public string qty { get; set; }		//商品数量
            //public string storeId { get; set; }		//门店标识
            //public string salesPrice { get; set; }		//商品单价
            //public string stdPrice { get; set; }		//商品标准价格
            //public string totalAmount { get; set; }		//订单总价
            public string mobile { get; set; }		//手机号码
            public string deliveryId { get; set; }//		配送方式标识
            public string deliveryAddress { get; set; }//	配送地址
            public string deliveryTime { get; set; }//		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
            public string orderId { get; set; }     //订单标识
            public string storeId { get; set; }     //到店标识
        }
        #endregion
        #endregion

        #region 11 订单支付提交 setOrderPayment
        public string setOrderPayment()
        {
            string content = string.Empty;
            var respData = new setOrderPaymentRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderPayment: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderPaymentReqData>();
                reqObj = reqObj == null ? new setOrderPaymentReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderPaymentReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.paymentTypeId == null || reqObj.special.paymentTypeId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "paymentTypeId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.paymentAmount == null || reqObj.special.paymentAmount.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "paymentAmount不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.OrderId = reqObj.special.orderId;
                orderInfo.PaymentTypeId = ToStr(reqObj.special.paymentTypeId);
                orderInfo.PaymentAmount = Convert.ToDecimal(reqObj.special.paymentAmount);
                orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.PaymentTime = System.DateTime.Now;
                orderInfo.Status = "2";
                orderInfo.StatusDesc = "已支付";
                orderInfo.Invoice = ToStr(reqObj.special.invoice);
                #endregion
                string strError = string.Empty;
                bool bReturn = service.SetOrderPayment(orderInfo, out strError);
                if (!bReturn)
                {
                    respData.code = "111";
                    respData.exception = strError;
                }


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region 参数对象
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setOrderPaymentRespData : Default.LowerRespData
        {
            public setOrderPaymentRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setOrderPaymentRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setOrderPaymentReqData : ReqData
        {
            public setOrderPaymentReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOrderPaymentReqSpecialData
        {
            public string orderId { get; set; }
            public string paymentTypeId { get; set; }
            public string paymentAmount { get; set; }
            public string invoice { get; set; }     //Jermyn20131008 
        }
        #endregion
        #endregion

        #region 12.获取支付方式的集合 getPaymentTypeList
        public string getPaymentTypeList()
        {
            string content = string.Empty;
            var respData = new getPaymentTypeListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemTypeList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getPaymentTypeListReqData>();
                reqObj = reqObj == null ? new getPaymentTypeListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getPaymentTypeListReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                respData.content = new getPaymentTypeListRespContentData();
                respData.content.paymentTypeList = new List<getPaymentTypeListRespContentItemTypeData>();

                TPaymentTypeBLL paymentTypeService = new TPaymentTypeBLL(loggingSessionInfo);
                var paymentTypeList = paymentTypeService.GetAll();
                IList<getPaymentTypeListRespContentItemTypeData> list = new List<getPaymentTypeListRespContentItemTypeData>();
                foreach (var paymentInfo in paymentTypeList)
                {
                    getPaymentTypeListRespContentItemTypeData info = new getPaymentTypeListRespContentItemTypeData();
                    info.paymentTypeId = ToStr(paymentInfo.PaymentTypeID);
                    info.paymentItemType = ToStr(paymentInfo.PaymentTypeName);
                    info.paymentDesc = ToStr(paymentInfo.PaymentDesc);
                    info.LogoURL = ToStr(paymentInfo.LogoURL);

                    list.Add(info);
                }
                respData.content.paymentTypeList = list;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getPaymentTypeListRespData : Default.LowerRespData
        {
            public getPaymentTypeListRespContentData content { get; set; }
        }
        public class getPaymentTypeListRespContentData
        {
            public IList<getPaymentTypeListRespContentItemTypeData> paymentTypeList { get; set; }     //商品类别集合
        }
        public class getPaymentTypeListRespContentItemTypeData
        {
            public string paymentTypeId { get; set; }      //支付方式标识
            public string paymentItemType { get; set; }    //支付产品类别
            public string LogoURL { get; set; }             //产品logo
            public string paymentDesc { get; set; }          //支付描述
        }
        public class getPaymentTypeListReqData : ReqData
        {
            public getPaymentTypeListReqSpecialData special;
        }
        public class getPaymentTypeListReqSpecialData
        {

        }
        #endregion
        #endregion

        #region 13.配送方式的集合 getDeliveryList
        public string getDeliveryList()
        {
            string content = string.Empty;
            var respData = new getDeliveryListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getDeliveryList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getDeliveryListReqData>();
                reqObj = reqObj == null ? new getDeliveryListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getDeliveryListReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                respData.content = new getDeliveryListRespContentData();
                respData.content.deliveryList = new List<getDeliveryListRespContentItemTypeData>();

                DeliveryBLL dService = new DeliveryBLL(loggingSessionInfo);
                var deliveryList = dService.GetAll();
                IList<getDeliveryListRespContentItemTypeData> list = new List<getDeliveryListRespContentItemTypeData>();
                foreach (var paymentInfo in deliveryList)
                {
                    getDeliveryListRespContentItemTypeData info = new getDeliveryListRespContentItemTypeData();
                    info.deliveryId = ToStr(paymentInfo.DeliveryId);
                    info.deliveryName = ToStr(paymentInfo.DeliveryName);
                    info.isAddress = ToStr(paymentInfo.IsDelete);
                    list.Add(info);
                }
                respData.content.deliveryList = list;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getDeliveryListRespData : Default.LowerRespData
        {
            public getDeliveryListRespContentData content { get; set; }
        }
        public class getDeliveryListRespContentData
        {
            public IList<getDeliveryListRespContentItemTypeData> deliveryList { get; set; }     //商品类别集合
        }
        public class getDeliveryListRespContentItemTypeData
        {
            public string deliveryId { get; set; }      //支付方式标识
            public string deliveryName { get; set; }    //支付产品类别
            public string isAddress { get; set; }
        }
        public class getDeliveryListReqData : ReqData
        {
            public getDeliveryListReqSpecialData special;
        }
        public class getDeliveryListReqSpecialData
        {

        }
        #endregion
        #endregion

        #region 14.获取订单详细信息
        public string getOrderInfo()
        {
            string content = string.Empty;
            var respData = new getOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getOrderInfo: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getOrderInfoReqData>();
                reqObj = reqObj == null ? new getOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getOrderInfoReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #region 业务处理
                respData.content = new getOrderInfoRespContentData();
                InoutService serer = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo = serer.GetOrderOnline(reqObj.special.orderId);
                if (orderInfo != null)
                {
                    respData.content.ordered = ToStr(orderInfo.OrderId);
                    respData.content.itemname = ToStr(orderInfo.ItemName);
                    respData.content.ordercode = ToStr(orderInfo.OrderCode);
                    respData.content.skuid = ToStr(orderInfo.SkuId);
                    respData.content.totalqty = ToStr(orderInfo.TotalQty);
                    respData.content.salesprice = ToStr(orderInfo.SalesPrice);
                    respData.content.stdprice = ToStr(orderInfo.StdPrice);
                    respData.content.discountrate = ToStr(orderInfo.DiscountRate);
                    respData.content.totalamount = ToStr(orderInfo.TotalAmount);
                    respData.content.mobile = ToStr(orderInfo.Mobile);
                    respData.content.email = ToStr(orderInfo.Email);
                    respData.content.deliveryaddress = ToStr(orderInfo.DeliveryAddress);
                    respData.content.deliverytime = ToStr(orderInfo.DeliveryTime);
                    respData.content.remark = ToStr(orderInfo.Remark);
                    respData.content.deliveryname = ToStr(orderInfo.deliveryName);
                    respData.content.username = ToStr(orderInfo.username);
                    respData.content.itemId = ToStr(orderInfo.itemId);
                    respData.content.deliveryid = ToStr(orderInfo.DeliveryId);
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getOrderInfoRespData : Default.LowerRespData
        {
            public getOrderInfoRespContentData content { get; set; }
        }
        public class getOrderInfoRespContentData
        {
            public string ordered { get; set; }
            public string itemname { get; set; }    //商品名称
            public string ordercode { get; set; }   //订单code
            public string skuid { get; set; }       //sku id
            public string totalqty { get; set; }    //数量
            public string salesprice { get; set; }  //实际出售价格
            public string stdprice { get; set; }    //标准价
            public string discountrate { get; set; }//折扣率
            public string totalamount { get; set; } //总价
            public string mobile { get; set; }      //手机
            public string email { get; set; }       //邮箱
            public string deliveryaddress { get; set; } //配送地址
            public string deliverytime { get; set; }    //配送时间
            public string remark { get; set; }          //备注			
            public string deliveryname { get; set; }    //配送方式名称
            public string username { get; set; }
            public string itemId { get; set; }      //商品标识
            public string deliveryid { get; set; }

        }

        public class getOrderInfoReqData : ReqData
        {
            public getOrderInfoReqSpecialData special;
        }
        public class getOrderInfoReqSpecialData
        {
            public string orderId { get; set; }
        }
        #endregion
        #endregion

        #region 15.设置福利订单收藏与取消收藏
        public string setItemKeep()
        {
            string content = string.Empty;
            var respData = new setItemKeepRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setItemKeep: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setItemKeepReqData>();
                #region check
                reqObj = reqObj == null ? new setItemKeepReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setItemKeepReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.itemId == null || reqObj.special.itemId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "itemId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.keepStatus == null || reqObj.special.keepStatus.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "keepStatus不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                ItemKeepBLL itemKeepServer = new ItemKeepBLL(loggingSessionInfo);
                string strError = string.Empty;
                bool bReturn = itemKeepServer.SetItemKeep(reqObj.special.itemId, reqObj.special.keepStatus, reqObj.common.userId, out strError);
                if (!bReturn)
                {
                    respData.code = "111";
                    respData.exception = strError;
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class setItemKeepRespData : Default.LowerRespData
        {

        }

        public class setItemKeepReqData : ReqData
        {
            public setItemKeepReqSpecialData special;
        }
        public class setItemKeepReqSpecialData
        {
            public string itemId { get; set; }
            public string keepStatus { get; set; } //收藏状态：1=收藏，0=取消收藏
        }
        #endregion
        #endregion

        #region 16.获取VIP用户的详细信息
        public string getVipDetail()
        {
            string content = string.Empty;
            var respData = new getVIPDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipDetail: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVIPDetailReqData>();
                reqObj = reqObj == null ? new getVIPDetailReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 业务处理
                respData.content = new getVIPDetailRespContentData();

                VipBLL server = new VipBLL(loggingSessionInfo);
                var vwVipCenterInfoBLL = new VwVipCenterInfoBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo = server.GetByID(reqObj.common.userId);
                if (vipInfo != null)
                {
                    respData.content.vipName = ToStr(vipInfo.VipName);
                    respData.content.vipCode = ToStr(vipInfo.VipCode);
                    respData.content.address = ToStr(vipInfo.DeliveryAddress);
                    respData.content.phone = ToStr(vipInfo.Phone);
                    respData.content.email = ToStr(vipInfo.Email);
                    respData.content.vipId = ToStr(vipInfo.VIPID);
                    respData.content.imageUrl = ToStr(vipInfo.HeadImgUrl);
                    respData.content.age = ToStr(vipInfo.Age);
                    respData.content.birthday = ToStr(vipInfo.Birthday);
                    if (vipInfo.Gender.HasValue)
                    {
                        respData.content.sex = ToStr(vipInfo.Gender == 1 ? "男" : "女");
                    }
                    var vwVipCenterInfoList = vwVipCenterInfoBLL.QueryByEntity(new VwVipCenterInfoEntity()
                    {
                        VIPID = vipInfo.VIPID
                    }, null);
                    if (vwVipCenterInfoList != null && vwVipCenterInfoList.Length > 0)
                    {
                        VwVipCenterInfoEntity vwVipCenterInfoObj = vwVipCenterInfoList[0];
                        respData.content.validIntegral = ToStr(vwVipCenterInfoObj.ValidIntegral);
                        respData.content.outIntegral = ToStr(vwVipCenterInfoObj.OutIntegral);
                        if (respData.content.imageUrl == null || respData.content.imageUrl.Equals(""))
                        {
                            respData.content.imageUrl = ToStr(vwVipCenterInfoObj.HigherVipID);
                        }
                        respData.content.itemKeepCount = ToStr(vwVipCenterInfoObj.ItemKeepCount);
                        respData.content.integration = ToStr(vwVipCenterInfoObj.Integration);
                        respData.content.couponCount = ToStr(vwVipCenterInfoObj.CouponCount);
                        respData.content.shoppingCartCount = ToStr(vwVipCenterInfoObj.ShoppingCartCount);
                        respData.content.lotteryCount = ToStr(vwVipCenterInfoObj.LotteryCount);
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getVIPDetailRespData : Default.LowerRespData
        {
            public getVIPDetailRespContentData content { get; set; }
        }
        public class getVIPDetailRespContentData
        {
            public string vipName { get; set; }         //品牌标识
            public string vipCode { get; set; }         //品牌名称
            public string phone { get; set; }           //品牌英文名
            public string address { get; set; }         //门店标识
            public string email { get; set; }
            public string vipId { get; set; }
            public string validIntegral { get; set; }
            public string outIntegral { get; set; }
            public string imageUrl { get; set; }
            public string itemKeepCount { get; set; }
            public string integration { get; set; }
            public string couponCount { get; set; }
            public string shoppingCartCount { get; set; }
            public string lotteryCount { get; set; }    //抽奖次数 20131017
            public string age { get; set; }
            public string sex { get; set; }
            public string birthday { get; set; }

        }


        public class getVIPDetailReqData : ReqData
        {

        }

        #endregion
        #endregion

        #region 17 vip信息提交 setVipDetail
        public string setVipDetail()
        {
            string content = string.Empty;
            var respData = new setVipDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderPayment: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setVipDetailReqData>();
                reqObj = reqObj == null ? new setVipDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setVipDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.vipName == null || reqObj.special.vipName.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "vipName不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "phone不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                VipBLL service = new VipBLL(loggingSessionInfo);
                VipEntity vipInfo = new VipEntity();
                vipInfo.VIPID = reqObj.common.userId;
                vipInfo.VipName = ToStr(reqObj.special.vipName);
                vipInfo.Phone = ToStr(reqObj.special.phone);
                vipInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                vipInfo.LastUpdateTime = System.DateTime.Now;
                vipInfo.DeliveryAddress = ToStr(reqObj.special.address);
                vipInfo.Email = ToStr(reqObj.special.email);
                #endregion
                string strError = string.Empty;
                service.Update(vipInfo, false);


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region 参数对象
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setVipDetailRespData : Default.LowerRespData
        {
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setVipDetailReqData : ReqData
        {
            public setVipDetailReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setVipDetailReqSpecialData
        {
            public string vipName { get; set; }         //品牌标识
            public string phone { get; set; }           //品牌英文名
            public string address { get; set; }         //门店标识
            public string email { get; set; }
        }
        #endregion
        #endregion

        #region 2.2.20 获取商品SKU的第二属性集合 (20131121添加)
        public string getSkuProp2List()
        {
            string content = string.Empty;
            var respData = new getSkuProp2ListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getSkuProp2List: {0}", reqContent)
                });
                #region
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getSkuProp2ListReqData>();
                reqObj = reqObj == null ? new getSkuProp2ListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.itemId == null || reqObj.special.itemId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "itemId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.propDetailId == null || reqObj.special.propDetailId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "propDetailId不能为空";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion
                //查询参数
                string userId = reqObj.common.userId;

                respData.content = new getSkuProp2ListRespContentData();
                respData.content.prop2List = new List<getSkuProp2ListRespContentItemTypeData>();

                ItemService itemService = new ItemService(loggingSessionInfo);

                #region 获取商品属性集合
                var dsProp2 = itemService.GetItemProp2List(reqObj.special.itemId, reqObj.special.propDetailId);
                if (dsProp2 != null && dsProp2.Tables.Count > 0 && dsProp2.Tables[0].Rows.Count > 0)
                {
                    respData.content.prop2List = DataTableToObject.ConvertToList<getSkuProp2ListRespContentItemTypeData>(dsProp2.Tables[0]);
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getSkuProp2ListRespData : Default.LowerRespData
        {
            public getSkuProp2ListRespContentData content { get; set; }
        }
        public class getSkuProp2ListRespContentData
        {
            public IList<getSkuProp2ListRespContentItemTypeData> prop2List { get; set; }     //商品类别集合
        }
        public class getSkuProp2ListRespContentItemTypeData
        {
            public string skuId { get; set; }      //支付方式标识
            public string prop2DetailId { get; set; }    //支付产品类别
            public string prop2DetailName { get; set; }
        }
        public class getSkuProp2ListReqData : ReqData
        {
            public getSkuProp2ListReqSpecialData special;
        }
        public class getSkuProp2ListReqSpecialData
        {
            public string itemId { get; set; }
            public string propDetailId { get; set; }
        }
        #endregion
        #endregion

        #region 29.获取客户详细信息
        public string getCustomerDetail()
        {
            string content = string.Empty;
            var respData = new getCustomerDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getBrandDetail: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getCustomerDetailReqData>();
                reqObj = reqObj == null ? new getCustomerDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getCustomerDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                if (reqObj.special.storeId == null || reqObj.special.storeId.Equals(""))
                {
                    //Jermyn20131011 如果为空，获取总部id
                    UnitService unitServer = new UnitService(loggingSessionInfo);
                    reqObj.special.storeId = unitServer.GetHeadStoreId(customerId);
                }


                #endregion

                #region 业务处理
                respData.content = new getCustomerDetailRespContentData();

                VwUnitPropertyBLL serer = new VwUnitPropertyBLL(loggingSessionInfo);
                VwUnitPropertyEntity orderInfo = new VwUnitPropertyEntity();
                orderInfo = serer.GetByID(reqObj.special.storeId);
                if (orderInfo != null)
                {
                    respData.content.unitId = ToStr(orderInfo.UnitId);
                    respData.content.customerId = ToStr(orderInfo.CustomerId);
                    respData.content.unitName = ToStr(orderInfo.UnitName);
                    respData.content.isApp = ToStr(orderInfo.IsAPP);
                    respData.content.firstPageImage = ToStr(orderInfo.FirstPageImage);
                    respData.content.loginImage = ToStr(orderInfo.LoginImage);
                    respData.content.productsBackgroundImage = ToStr(orderInfo.ProductsBackgroundImage);

                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getCustomerDetailRespData : Default.LowerRespData
        {
            public getCustomerDetailRespContentData content { get; set; }
        }
        public class getCustomerDetailRespContentData
        {
            public string unitId { get; set; }           //门店标识
            public string customerId { get; set; }       //客户标识
            public string unitName { get; set; }        //门店/客户名称
            public string isApp { get; set; }         //是否需要下载APP
            public string firstPageImage { get; set; }       //首页图片
            public string loginImage { get; set; }    //登录图片
            public string productsBackgroundImage { get; set; }//首页最新商品背景图
        }



        public class getCustomerDetailReqData : ReqData
        {
            public getCustomerDetailReqSpecialData special;
        }
        public class getCustomerDetailReqSpecialData
        {
            public string storeId { get; set; }
        }
        #endregion
        #endregion

        #region 泸州老窖达人秀接口

        #region 1.获取当天的参赛作品
        /// <summary>
        /// 获取当天的参赛作品
        /// </summary>
        /// <returns></returns>
        public string getEventsEntriesList()
        {
            string content = string.Empty;
            var respData = new getEventsEntriesRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventsEntries: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEventsEntriesReqData>();
                reqObj = reqObj == null ? new getEventsEntriesReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventsEntriesReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId_Lj = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId_Lj, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string strDate = reqObj.special.strDate;   //查询日期

                //初始化返回对象
                respData.content = new getEventsEntriesRespContentData();
                respData.content.entriesList = new List<getEventsEntriesRespContentDataItem>();
                respData.content.strDate = string.IsNullOrEmpty(strDate)
                    ? DateTime.Now.AddDays(-1).ToString("yyyy年MM月dd日")
                    : Convert.ToDateTime(strDate).ToString("yyyy年MM月dd日");

                var entriesService = new LEventsEntriesBLL(loggingSessionInfo);

                var dsEntries = entriesService.GetEventsEntriesList(strDate);
                if (dsEntries != null && dsEntries.Tables.Count > 0 && dsEntries.Tables[0].Rows.Count > 0)
                {
                    respData.content.entriesList = DataTableToObject.ConvertToList<getEventsEntriesRespContentDataItem>(dsEntries.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventsEntriesRespData : Default.LowerRespData
        {
            public getEventsEntriesRespContentData content { get; set; }
        }
        public class getEventsEntriesRespContentData
        {
            public string strDate { get; set; }     //作品日期
            public IList<getEventsEntriesRespContentDataItem> entriesList { get; set; }     //爱秀达人作品集合
        }
        public class getEventsEntriesRespContentDataItem
        {
            public string entriesId { get; set; }   //作品标识
            public string workTitle { get; set; }   //作品名称
            public string workUrl { get; set; }     //作品图片链接地址
            public Int64 displayIndex { get; set; }   //序号
            public int praiseCount { get; set; }    //赞的数量
            public int commentCount { get; set; }   //评论数量
        }
        public class getEventsEntriesReqData : ReqData
        {
            public getEventsEntriesReqSpecialData special;
        }
        public class getEventsEntriesReqSpecialData
        {
            public string strDate { get; set; }    //查询日期
        }
        #endregion

        #region 2.报名提交（评论报名）
        /// <summary>
        /// 报名提交（评论报名）
        /// </summary>
        /// <returns></returns>
        public string setEventSignUp()
        {
            string content = string.Empty;
            var respData = new setEventSignUpRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setEventSignUpReqData>();
                reqObj = reqObj == null ? new setEventSignUpReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setEventSignUpReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId_Lj = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId_Lj, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string signUpId = reqObj.common.signUpId;   //报名ID
                string userName = reqObj.special.userName;  //用户名称
                string phone = reqObj.special.phone;        //用户手机号码
                string eventId = "AC1DFF316EE44E72B11BB416A641E726"; //活动ID

                //初始化返回对象
                respData.content = new setEventSignUpRespContentData();

                var entriesService = new LEventSignUpBLL(loggingSessionInfo);
                respData.content.signUpId = entriesService.SetEventSignUp(signUpId, userName, phone, eventId);
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setEventSignUpRespData : Default.LowerRespData
        {
            public setEventSignUpRespContentData content { get; set; }
        }
        public class setEventSignUpRespContentData
        {
            public string signUpId { get; set; }   //报名Id
        }
        public class setEventSignUpReqData : ReqData
        {
            public setEventSignUpReqSpecialData special;
        }
        public class setEventSignUpReqSpecialData
        {
            public string userName { get; set; }    //用户名称
            public string phone { get; set; }       //用户手机号码
        }
        #endregion

        #region 3.赞提交（对作品赞）
        /// <summary>
        /// 赞提交（对作品赞）
        /// </summary>
        /// <returns></returns>
        public string setEventsEntriesPraise()
        {
            string content = string.Empty;
            var respData = new setEventsEntriesPraiseRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventsEntriesPraise: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setEventsEntriesPraiseReqData>();
                reqObj = reqObj == null ? new setEventsEntriesPraiseReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setEventsEntriesPraiseReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId_Lj = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId_Lj, "1");

                //查询参数
                string userId = reqObj.common.userId;
                // string signUpId = reqObj.common.signUpId;       //报名ID
                string signUpId = string.Empty;
                if (reqObj.common.signUpId == null || reqObj.common.signUpId.Equals(""))
                {
                    signUpId = reqObj.common.userId;
                }
                else
                {
                    signUpId = reqObj.common.signUpId;
                }
                string entriesId = reqObj.special.entriesId;    //作品标识

                var praiseService = new LEventsEntriesPraiseBLL(loggingSessionInfo);
                var queryList = praiseService.QueryByEntity(new LEventsEntriesPraiseEntity()
                {
                    SignUpID = signUpId,
                    EntriesId = entriesId
                }, null);

                if (queryList != null && queryList.Length > 0)
                {
                    //已经赞过一次，不能重复
                    respData.code = "101";
                    respData.description = "亲，您已经赞过了哦。";
                }
                else
                {
                    //新增
                    praiseService.Create(new LEventsEntriesPraiseEntity()
                    {
                        PraiseId = Utils.NewGuid(),
                        SignUpID = signUpId,
                        EntriesId = entriesId
                    });
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setEventsEntriesPraiseRespData : Default.LowerRespData
        {
            public setEventsEntriesPraiseRespContentData content { get; set; }
        }
        public class setEventsEntriesPraiseRespContentData
        {
        }
        public class setEventsEntriesPraiseReqData : ReqData
        {
            public setEventsEntriesPraiseReqSpecialData special;
        }
        public class setEventsEntriesPraiseReqSpecialData
        {
            public string entriesId { get; set; }    //作品标识
        }
        #endregion

        #region 4.评论提交（对作品评论）
        /// <summary>
        /// 评论提交（对作品评论）
        /// </summary>
        /// <returns></returns>
        public string setEventsEntriesComment()
        {
            string content = string.Empty;
            var respData = new setEventsEntriesCommentRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventsEntriesComment: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setEventsEntriesCommentReqData>();
                reqObj = reqObj == null ? new setEventsEntriesCommentReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setEventsEntriesCommentReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId_Lj = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId_Lj, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string signUpId = reqObj.common.signUpId;      //报名ID
                string comment = reqObj.special.content;        //评论内容
                string entriesId = reqObj.special.entriesId;    //作品标识

                var commentService = new LEventsEntriesCommentBLL(loggingSessionInfo);

                //新增评论
                commentService.Create(new LEventsEntriesCommentEntity()
                {
                    CommentId = Utils.NewGuid(),
                    EntriesId = entriesId,
                    SignUpID = signUpId,
                    Content = comment
                });
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setEventsEntriesCommentRespData : Default.LowerRespData
        {
            public setEventsEntriesCommentRespContentData content { get; set; }
        }
        public class setEventsEntriesCommentRespContentData
        {
        }
        public class setEventsEntriesCommentReqData : ReqData
        {
            public setEventsEntriesCommentReqSpecialData special;
        }
        public class setEventsEntriesCommentReqSpecialData
        {
            public string content { get; set; }     //评论内容
            public string entriesId { get; set; }   //作品标识
        }
        #endregion

        #region 5.获取参赛作品的评论与赞
        /// <summary>
        /// 获取参赛作品的评论与赞
        /// </summary>
        /// <returns></returns>
        public string getEventsEntriesCommentList()
        {
            string content = string.Empty;
            var respData = new getEventsEntriesCommentListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventsEntriesCommentList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEventsEntriesCommentListReqData>();
                reqObj = reqObj == null ? new getEventsEntriesCommentListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventsEntriesCommentListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId_Lj = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId_Lj, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string entriesId = reqObj.special.entriesId;    //作品标识
                int page = reqObj.special.page;                 //页码
                int pageSize = reqObj.special.pageSize;         //页面数量

                var entriesService = new LEventsEntriesBLL(loggingSessionInfo);

                //初始化返回对象
                respData.content = new getEventsEntriesCommentListRespContentData();
                var dsEntries = entriesService.GetEventsEntriesCommentList(entriesId);

                if (dsEntries != null && dsEntries.Tables.Count > 0 && dsEntries.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<getEventsEntriesCommentListRespContentData>(dsEntries.Tables[0].Rows[0]);

                    //初始化返回对象
                    respData.content.commentList = new List<getEventsEntriesCommentListRespContentDataItem>();

                    var dsComment = entriesService.GetEventsEntriesCommentList(entriesId, page, pageSize);
                    if (dsComment != null && dsComment.Tables.Count > 0 && dsComment.Tables[0].Rows.Count > 0)
                    {
                        respData.content.commentList = DataTableToObject.ConvertToList<getEventsEntriesCommentListRespContentDataItem>(dsComment.Tables[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventsEntriesCommentListRespData : Default.LowerRespData
        {
            public getEventsEntriesCommentListRespContentData content { get; set; }
        }
        public class getEventsEntriesCommentListRespContentData
        {
            public string entriesId { get; set; }       //作品标识
            public string workTitle { get; set; }       //作品名称
            public string workUrl { get; set; }         //作品图片链接地址
            public int praiseCount { get; set; }        //赞的数量
            public string nextEntriesId { get; set; }   //下一个作品标识
            public string preEntriesId { get; set; }    //上一个作品标识
            public int commentCount { get; set; }       //评论数量
            public IList<getEventsEntriesCommentListRespContentDataItem> commentList { get; set; }  //评论集合
        }
        public class getEventsEntriesCommentListRespContentDataItem
        {
            public string content { get; set; }         //评论内容
            public string userName { get; set; }        //用户名称
            public string phone { get; set; }           //手机号码
            public Int64 displayIndex { get; set; }     //序号
            public string createTime { get; set; }      //评论时间
        }
        public class getEventsEntriesCommentListReqData : ReqData
        {
            public getEventsEntriesCommentListReqSpecialData special;
        }
        public class getEventsEntriesCommentListReqSpecialData
        {
            public string entriesId { get; set; }   //作品标识
            public int page { get; set; }           //页码
            public int pageSize { get; set; }       //页面数量
        }
        #endregion

        #region 6.获取获奖名单
        /// <summary>
        /// 获取获奖名单
        /// </summary>
        /// <returns></returns>
        public string getEventsEntriesWinners()
        {
            string content = string.Empty;
            var respData = new getEventsEntriesWinnersRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventsEntriesWinners: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEventsEntriesWinnersReqData>();
                reqObj = reqObj == null ? new getEventsEntriesWinnersReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventsEntriesWinnersReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string strDate = reqObj.special.strDate;    //获奖日期(yyyy-MM-dd)

                var entriesService = new LEventsEntriesBLL(loggingSessionInfo);

                //初始化返回对象
                respData.content = new getEventsEntriesWinnersRespContentData();
                respData.content.crowdDarentList = new List<getEventsEntriesWinnersRespContentDataItem>();
                respData.content.workDarenList = new List<getEventsEntriesWinnersRespContentDataItem>();

                respData.content.crowdDarenCount = entriesService.GetCrowdDarenCount(strDate);
                respData.content.workDarenCount = entriesService.GetWorkDarenCount(strDate);

                var dsCrows = entriesService.GetCrowdDarenList(strDate);
                if (dsCrows != null && dsCrows.Tables.Count > 0 && dsCrows.Tables[0].Rows.Count > 0)
                {
                    respData.content.crowdDarentList = DataTableToObject.ConvertToList<getEventsEntriesWinnersRespContentDataItem>(dsCrows.Tables[0]);
                }

                var dsWorks = entriesService.GetWorkDarenList(strDate);
                if (dsWorks != null && dsWorks.Tables.Count > 0 && dsWorks.Tables[0].Rows.Count > 0)
                {
                    respData.content.workDarenList = DataTableToObject.ConvertToList<getEventsEntriesWinnersRespContentDataItem>(dsWorks.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventsEntriesWinnersRespData : Default.LowerRespData
        {
            public getEventsEntriesWinnersRespContentData content { get; set; }
        }
        public class getEventsEntriesWinnersRespContentData
        {
            public int crowdDarenCount { get; set; }    //围观达人数量
            public int workDarenCount { get; set; }     //爱秀达人数量
            public IList<getEventsEntriesWinnersRespContentDataItem> crowdDarentList { get; set; }  //围观达人集合
            public IList<getEventsEntriesWinnersRespContentDataItem> workDarenList { get; set; }    //爱秀达人集合
        }
        public class getEventsEntriesWinnersRespContentDataItem
        {
            public string userName { get; set; }    //用户名称
            public string content { get; set; }     //评论
            public int prizeCount { get; set; }     //获奖作品数量
        }
        public class getEventsEntriesWinnersReqData : ReqData
        {
            public getEventsEntriesWinnersReqSpecialData special;
        }
        public class getEventsEntriesWinnersReqSpecialData
        {
            public string strDate { get; set; }    //获奖日期(yyyy-MM-dd)

        }
        #endregion

        #region 7.获取品味达人作品集
        /// <summary>
        /// 获取品味达人作品集
        /// </summary>
        /// <returns></returns>
        public string getEventsEntriesMonthDaren()
        {
            string content = string.Empty;
            var respData = new getEventsEntriesMonthDarenRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventsEntriesMonthDaren: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEventsEntriesMonthDarenReqData>();
                reqObj = reqObj == null ? new getEventsEntriesMonthDarenReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getEventsEntriesMonthDarenReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId_Lj = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId_Lj, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getEventsEntriesMonthDarenRespContentData();
                respData.content.entriesList = new List<getEventsEntriesMonthDarenRespContentDataItem>();

                var entriesService = new LEventsEntriesBLL(loggingSessionInfo);

                var dsEntries = entriesService.GetEventsEntriesList();
                if (dsEntries != null && dsEntries.Tables.Count > 0 && dsEntries.Tables[0].Rows.Count > 0)
                {
                    respData.content.entriesList = DataTableToObject.ConvertToList<getEventsEntriesMonthDarenRespContentDataItem>(dsEntries.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventsEntriesMonthDarenRespData : Default.LowerRespData
        {
            public getEventsEntriesMonthDarenRespContentData content { get; set; }
        }
        public class getEventsEntriesMonthDarenRespContentData
        {
            public IList<getEventsEntriesMonthDarenRespContentDataItem> entriesList { get; set; }     //品味达人作品集合
        }
        public class getEventsEntriesMonthDarenRespContentDataItem
        {
            public string entriesId { get; set; }   //作品标识
            public string workTitle { get; set; }   //作品名称
            public string workUrl { get; set; }     //作品图片链接地址
            public int displayIndex { get; set; }   //序号
            public int praiseCount { get; set; }    //赞的数量
            public int commentCount { get; set; }   //评论数量
        }
        public class getEventsEntriesMonthDarenReqData : ReqData
        {
            public getEventsEntriesMonthDarenReqSpecialData special;
        }
        public class getEventsEntriesMonthDarenReqSpecialData
        {
        }
        #endregion

        #endregion

        #region 公共方法
        #region ToStr
        public string ToStr(string obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString();
        }

        public string ToStr(float obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(double obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(double? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(decimal? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(long obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int obj)
        {
            //if (obj == null) return "0";
            return obj.ToString();
        }

        public string ToStr(int? obj)
        {
            if (obj == null) return "0";
            return obj.ToString();
        }
        public string ToStr(DateTime? obj)
        {
            if (obj == null) return null;
            return obj.ToString();
        }
        #endregion
        public int ToInt(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt32(obj);
        }
        public Int64 ToInt64(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToInt64(obj);
        }
        public double ToDouble(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return Convert.ToDouble(obj);
        }
        public float ToFloat(object obj)
        {
            if (obj == null) return 0;
            else if (obj.ToString() == "") return 0;
            return float.Parse(obj.ToString());
        }
        #endregion

        #region 中欧菜单功能
        #region 获取大型论坛或者招生活动的标题集合
        public string getForumEntriesList()
        {
            string content = string.Empty;
            var respData = new getForumEntriesListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getForumEntriesList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getForumEntriesListReqData>();
                reqObj = reqObj == null ? new getForumEntriesListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getForumEntriesListReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.forumTypeId == null || reqObj.special.forumTypeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "forumTypeId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getForumEntriesListRespContentData();
                ZLargeForumBLL service = new ZLargeForumBLL(loggingSessionInfo);

                ZLargeForumEntity queryEntity = new ZLargeForumEntity();
                queryEntity.ForumTypeId = ToInt(reqObj.special.forumTypeId);
                IList<ZLargeForumEntity> list = service.GetForums(queryEntity, 0, 20);
                if (list != null && list.Count > 0)
                {
                    respData.content.pageTitle = list[0].ForumTypeName.Trim();
                    respData.content.forumList = new List<getForumEntriesListRespForumData>();
                    foreach (var item in list)
                    {
                        respData.content.forumList.Add(new getForumEntriesListRespForumData()
                        {
                            forumId = ToStr(item.ForumId),
                            title = ToStr(item.Title),
                            displayIndex = ToStr(item.DisplayIndex),
                            beginTime = ToStr(item.BeginTime)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getForumEntriesListRespData : Default.LowerRespData
        {
            public getForumEntriesListRespContentData content { get; set; }
        }
        public class getForumEntriesListRespContentData
        {
            public string pageTitle { get; set; } //页面标题(Jermyn20131012)
            public IList<getForumEntriesListRespForumData> forumList { get; set; }
        }
        public class getForumEntriesListRespForumData
        {
            public string forumId { get; set; }
            public string title { get; set; }
            public string displayIndex { get; set; }
            public string beginTime { get; set; }
        }
        public class getForumEntriesListReqData : ReqData
        {
            public getForumEntriesListReqSpecialData special;
        }
        public class getForumEntriesListReqSpecialData
        {
            public string forumTypeId { get; set; }
        }
        #endregion

        #region 获取论坛或者招生活动的明细
        public string getForumDetail()
        {
            string content = string.Empty;
            var respData = new getForumDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getForumDetail: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getForumDetailReqData>();
                reqObj = reqObj == null ? new getForumDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getForumDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.forumId == null || reqObj.special.forumId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "forumId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getForumDetailRespForumData();
                ZLargeForumBLL service = new ZLargeForumBLL(loggingSessionInfo);
                ZLargeForumEntity obj = service.GetByID(ToStr(reqObj.special.forumId));
                //获取抬头  
                if (reqObj.special.forumTypeId != null && !reqObj.special.forumTypeId.Equals(""))
                {
                    ZForumTypeBLL typeService = new ZForumTypeBLL(loggingSessionInfo);
                    ZForumTypeEntity typeObj = typeService.GetByID(ToStr(reqObj.special.forumTypeId));
                    respData.content.pageTitle = ToStr(typeObj.ForumTypeName);
                }
                else
                {
                    respData.content.pageTitle = "待取";
                }
                #region 赋值
                respData.content.forumId = ToStr(obj.ForumId);
                respData.content.title = ToStr(obj.Title);
                respData.content.desc = ToStr(obj.Desc);
                respData.content.organizer = ToStr(obj.Organizer);
                respData.content.schedule = ToStr(obj.Schedule);
                respData.content.food = ToStr(obj.Food);
                respData.content.speakers = ToStr(obj.Speakers);
                respData.content.courseId = ToStr(obj.CourseId);
                respData.content.emailId = ToStr(obj.ForumId);
                respData.content.sponsor = ToStr(obj.Sponsor);
                respData.content.roundtable = ToStr(obj.Roundtable);
                respData.content.previousForum = ToStr(obj.PreviousForum);
                respData.content.contactUs = ToStr(obj.ContactUs);
                respData.content.register = ToStr(obj.Register);

                if (obj.Desc == null || obj.Desc.Trim().Equals(""))
                {
                    respData.content.isDesc = 0;
                }
                else
                {
                    respData.content.isDesc = 1;// ToInt(obj.IsDesc); 
                }
                if (obj.Food == null || obj.Food.Trim().Equals(""))
                {
                    respData.content.isFood = 0;
                }
                else
                {
                    respData.content.isFood = 1;// ToInt(obj.IsFood);
                }
                if (obj.ContactUs == null || obj.ContactUs.Trim().Equals(""))
                {
                    respData.content.isContactUs = 0;
                }
                else
                {
                    respData.content.isContactUs = 1;//ToInt(obj.IsContactUs);
                }
                if (obj.Organizer == null || obj.Organizer.Trim().Equals(""))
                {
                    respData.content.isOrganizer = 0;
                }
                else
                {
                    respData.content.isOrganizer = 1; //ToInt(obj.IsOrganizer);
                }
                if (obj.PreviousForum == null || obj.PreviousForum.Trim().Equals(""))
                {
                    respData.content.isPreviousForum = 0;
                }
                else
                {
                    respData.content.isPreviousForum = 1; //ToInt(obj.IsPreviousForum);
                }
                if (obj.Roundtable == null || obj.Roundtable.Trim().Equals(""))
                {
                    respData.content.isRoundtable = 0;
                }
                else
                {
                    respData.content.isRoundtable = 1; //ToInt(obj.IsRoundtable);
                }
                if (obj.Schedule == null || obj.Schedule.Trim().Equals(""))
                {
                    respData.content.isSchedule = 0;
                }
                else
                {
                    respData.content.isSchedule = 1; //ToInt(obj.isSchedule);
                }

                if (obj.Speakers == null || obj.Speakers.Trim().Equals(""))
                {
                    respData.content.isSpeakers = 0;
                }
                else
                {
                    respData.content.isSpeakers = 1; //ToInt(obj.isSpeakers);
                }
                if (obj.Sponsor == null || obj.Sponsor.Trim().Equals(""))
                {
                    respData.content.isSponsor = 0;
                }
                else
                {
                    respData.content.isSponsor = 1; //ToInt(obj.isSponsor);
                }
                if (obj.Register == null || obj.Register.Trim().Equals(""))
                {
                    respData.content.isRegister = 0;
                }
                else
                {
                    respData.content.isRegister = 1; //ToInt(obj.isRegister);
                }
                respData.content.isSignUp = ToInt(obj.IsSignUp);

                //获取图片
                ObjectImagesBLL objectServer = new ObjectImagesBLL(loggingSessionInfo);
                var imageList = objectServer.QueryByEntity(new ObjectImagesEntity
                {
                    ObjectId = obj.ForumId
                    ,
                    IsDelete = 0
                }, null);
                if (imageList != null && imageList.Length > 0)
                {
                    respData.content.imageURL = ToStr(imageList[0].ImageURL);
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getForumDetailRespData : Default.LowerRespData
        {
            public getForumDetailRespForumData content { get; set; }
        }
        //public class getForumDetailRespContentData
        //{
        //    public getForumDetailRespForumData forum { get; set; }
        //}
        public class getForumDetailRespForumData
        {
            public string pageTitle { get; set; }
            public string forumId { get; set; }
            public string title { get; set; }
            public string desc { get; set; }
            public string organizer { get; set; }
            public string schedule { get; set; }
            public string food { get; set; }
            public string speakers { get; set; }
            public string courseId { get; set; } //课程标识
            public string emailId { get; set; } //发送邮件的邮件标识 Jermyn20131015
            public string sponsor { get; set; } //赞助
            public string roundtable { get; set; } //圆桌会议
            public string previousForum { get; set; } //往届论坛
            public string contactUs { get; set; }   //联系我们
            public int isDesc { get; set; }         //是否需要显示简介 1=显示 0=不显示
            public int isOrganizer { get; set; }    //是否需要显示组织者 1=显示 0=不显示
            public int isSchedule { get; set; }     //是否需要显示日程安排 1=显示 0=不显示
            public int isSpeakers { get; set; }     //是否需要显示演讲人 1=显示 0=不显示
            public int isRoundtable { get; set; }   //是否需要显示圆桌会议  1=显示 0=不显示
            public int isSponsor { get; set; }      //是否需要显示赞助 1=显示 0=不显示
            public int isFood { get; set; }         //是否需要显示场地膳食 1=显示 0=不显示
            public int isPreviousForum { get; set; } //是否需要显示往届论坛 1=显示 0=不显示
            public int isContactUs { get; set; }    //是否需要显示联系我们 1=显示 0=不显示
            public int isSignUp { get; set; }       //是否需要显示报名参加1=显示 0=不显示
            public string register { get; set; }    //注册参加
            public int isRegister { get; set; }     //是否显示注册参加
            public string imageURL { get; set; }

        }
        public class getForumDetailReqData : ReqData
        {
            public getForumDetailReqSpecialData special;
        }
        public class getForumDetailReqSpecialData
        {
            public string forumId { get; set; }
            public string forumTypeId { get; set; }
        }
        #endregion

        #region 获取课程管理的详细信息
        public string getCourseDetail()
        {
            string content = string.Empty;
            var respData = new getCourseDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getCourseDetail: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getCourseDetailReqData>();
                reqObj = reqObj == null ? new getCourseDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getCourseDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.courseTypeId == null || reqObj.special.courseTypeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "courseTypeId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getCourseDetailRespContentData();
                ZCourseBLL service = new ZCourseBLL(loggingSessionInfo);
                ZCourseEntity queryEntity = new ZCourseEntity();
                queryEntity.CourseTypeId = ToInt(reqObj.special.courseTypeId);
                queryEntity.CourseId = ToStr(reqObj.special.courseId);
                var list = service.GetCourses(queryEntity, 0, 1);
                ZCourseEntity obj = new ZCourseEntity();
                if (list != null && list.Count > 0)
                {
                    obj = list[0];
                    respData.content.courseId = ToStr(obj.CourseId);
                    respData.content.couseDesc = ToStr(obj.CouseDesc);
                    respData.content.courseName = ToStr(obj.CourseName);
                    respData.content.courseSummary = ToStr(obj.CourseSummary);
                    respData.content.courseFee = ToStr(obj.CourseFee);
                    respData.content.courseStartTime = ToStr(obj.CourseStartTime);
                    respData.content.couseCapital = ToStr(obj.CouseCapital);
                    respData.content.couseContact = ToStr(obj.CouseContact);

                    if (obj.CourseReflectionsList != null)
                    {
                        respData.content.courseReflectionsList = new List<getCourseDetailRespCourseReflectionData>();
                        foreach (var item in obj.CourseReflectionsList)
                        {
                            respData.content.courseReflectionsList.Add(new getCourseDetailRespCourseReflectionData()
                            {
                                reflectionsId = ToStr(item.ReflectionsId),
                                studentName = ToStr(item.StudentName),
                                studentPost = ToStr(item.StudentPost),
                                content = ToStr(item.Content),
                                videoURL = ToStr(item.VideoURL),
                                imageURL = ToStr(item.ImageURL),
                                displayIndex = ToStr(item.DisplayIndex)
                            });
                        }
                    }
                    if (obj.NewsList != null)
                    {
                        respData.content.newsList = new List<getCourseDetailRespNewsData>();
                        foreach (var item in obj.NewsList)
                        {
                            respData.content.newsList.Add(new getCourseDetailRespNewsData()
                            {
                                newsId = ToStr(item.NewsId),
                                newsTitle = ToStr(item.NewsTitle),
                                publishTime = ToStr(item.PublishTime),
                                displayIndex = ToStr(item.displayIndex)
                            });
                        }
                    }
                    if (obj.ImageList != null)
                    {
                        respData.content.imageList = new List<getCourseDetailRespImageData>();
                        foreach (var item in obj.ImageList)
                        {
                            respData.content.imageList.Add(new getCourseDetailRespImageData()
                            {
                                imageId = ToStr(item.ImageId),
                                imageUrl = ToStr(item.ImageURL)
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getCourseDetailRespData : Default.LowerRespData
        {
            public getCourseDetailRespContentData content { get; set; }
        }
        public class getCourseDetailRespContentData
        {
            public string courseId { get; set; }
            public string couseDesc { get; set; }
            public string courseName { get; set; }
            public string courseSummary { get; set; }
            public string courseFee { get; set; }
            public string courseStartTime { get; set; }
            public string couseCapital { get; set; }
            public string couseContact { get; set; }
            public IList<getCourseDetailRespCourseReflectionData> courseReflectionsList { get; set; }
            public IList<getCourseDetailRespNewsData> newsList { get; set; }
            public IList<getCourseDetailRespImageData> imageList { get; set; }
        }
        public class getCourseDetailRespCourseReflectionData
        {
            public string reflectionsId { get; set; }
            public string studentName { get; set; }
            public string studentPost { get; set; }
            public string content { get; set; }
            public string videoURL { get; set; }
            public string displayIndex { get; set; }
            public string imageURL { get; set; }
        }
        public class getCourseDetailRespNewsData
        {
            public string newsId { get; set; }
            public string newsTitle { get; set; }
            public string publishTime { get; set; }
            public string displayIndex { get; set; }
        }
        public class getCourseDetailRespImageData
        {
            public string imageId { get; set; }
            public string imageUrl { get; set; }
        }
        public class getCourseDetailReqData : ReqData
        {
            public getCourseDetailReqSpecialData special;
        }
        public class getCourseDetailReqSpecialData
        {
            public string courseTypeId { get; set; }
            public string courseId { get; set; }
        }
        #endregion

        #region 课程报名提交
        public string setCourseApply()
        {
            string content = string.Empty;
            var respData = new setCourseApplyRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setCourseApply: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setCourseApplyReqData>();
                reqObj = reqObj == null ? new setCourseApplyReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setCourseApplyReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                //if (reqObj.special.emailId == null || reqObj.special.emailId.Equals(""))
                //{
                //    respData.code = "2201";
                //    respData.description = "emailId不能为空";
                //    return respData.ToJSON().ToString();
                //}
                if (reqObj.special.applyName == null || reqObj.special.applyName.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "报名人姓名不能为空";
                    return respData.ToJSON().ToString();
                }
                //if (reqObj.special.company == null || reqObj.special.company.Equals(""))
                //{
                //    respData.code = "2203";
                //    respData.description = "公司不能为空";
                //    return respData.ToJSON().ToString();
                //}
                //if (reqObj.special.email == null || reqObj.special.email.Equals(""))
                //{
                //    respData.code = "2204";
                //    respData.description = "邮件不能为空";
                //    return respData.ToJSON().ToString();
                //}
                //if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                //{
                //    respData.code = "2205";
                //    respData.description = "电话不能为空";
                //    return respData.ToJSON().ToString();
                //}

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new setCourseApplyRespContentData();
                ZCourseApplyBLL service = new ZCourseApplyBLL(loggingSessionInfo);
                ZCourseBLL zCourseBLL = new ZCourseBLL(loggingSessionInfo);
                ZCourseApplyEntity obj = new ZCourseApplyEntity();
                obj.ApplyId = Utils.NewGuid();
                obj.CouseId = ToStr(reqObj.special.courseId);
                obj.ApplyName = ToStr(reqObj.special.applyName);
                obj.Company = ToStr(reqObj.special.company);
                obj.Post = ToStr(reqObj.special.post);
                obj.Email = ToStr(reqObj.special.email);
                obj.Phone = ToStr(reqObj.special.phone);
                obj.ObjectId = ToStr(reqObj.special.emailId);
                obj.Remark = ToStr(reqObj.special.remark);
                obj.DataFrom = ToStr(reqObj.special.dataFrom);
                service.Create(obj);

                string mailtoStr = string.Empty;
                string mailBodyStr = string.Empty;
                var courseName = "";
                var couseObj = zCourseBLL.GetByID(obj.ObjectId);
                if (couseObj != null)
                {
                    mailtoStr = couseObj.Email;
                    courseName = couseObj.CourseName;
                    if (couseObj.EmailTitle != null && !couseObj.EmailTitle.Equals(""))
                    {
                        mailBodyStr = couseObj.EmailTitle;
                    }
                }
                else
                {
                    ZLargeForumBLL forumServer = new ZLargeForumBLL(loggingSessionInfo);
                    var forumObj = forumServer.GetByID(ToStr(obj.ObjectId));
                    if (forumObj != null)
                    {
                        mailtoStr = forumObj.Email;
                        courseName = forumObj.Title;
                        if (forumObj.EmailTitle != null && !forumObj.EmailTitle.Equals(""))
                        {
                            mailBodyStr = forumObj.EmailTitle;
                        }
                    }
                }
                if (mailtoStr == null && mailtoStr.Equals(""))
                {
                    respData.code = "2204";
                    respData.description = "不存在对应的发送邮件地址";
                    return respData.ToJSON().ToString();
                }
                if (mailBodyStr == null && mailBodyStr.Equals(""))
                {
                    mailBodyStr = "EMBA课程咨询（微信）";
                }


                // send mail
                //var mailto = "yingyu.cai@jitmarketing.cn";
                var mailto = mailtoStr;
                var mailsubject = mailBodyStr;
                var mailBody = "<div>" + mailBodyStr + "</div><br/>";
                mailBody += string.Format("<div>报名时间：{0} </div><br/> ", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                mailBody += string.Format("<div>课程名称：{0} </div><br/> ", courseName);
                mailBody += string.Format("<div>报名人姓名：{0} </div><br/> ", obj.ApplyName);
                mailBody += string.Format("<div>公司：{0} </div><br/> ", obj.Company);
                mailBody += string.Format("<div>职位：{0} </div><br/> ", obj.Post);
                mailBody += string.Format("<div>邮件：{0} </div><br/> ", obj.Email);
                mailBody += string.Format("<div>电话：{0} </div><br/> ", obj.Phone);
                mailBody += string.Format("<div>备注：{0} </div><br/> ", obj.Remark);
                var success = JIT.Utility.Notification.Mail.SendMail(mailto, mailsubject, mailBody);
                if (success)
                {
                    obj.IsPushEmail = 1;
                }
                else
                {
                    obj.IsPushEmail = 0;
                    obj.PushEmailFailure = "发送失败";
                }
                service.Update(obj, false);

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setCourseApplyRespData : Default.LowerRespData
        {
            public setCourseApplyRespContentData content { get; set; }
        }
        public class setCourseApplyRespContentData
        {

        }
        public class setCourseApplyReqData : ReqData
        {
            public setCourseApplyReqSpecialData special;
        }
        public class setCourseApplyReqSpecialData
        {
            public string courseId { get; set; }
            public string applyName { get; set; }
            public string company { get; set; }
            public string post { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string emailId { get; set; }     //邮件标识
            public string remark { get; set; }      //备注 20131017
            public string dataFrom { get; set; }
        }
        #endregion

        #region 获取高级课程集合
        public string getHighLevelCourseList()
        {
            string content = string.Empty;
            var respData = new getHighLevelCourseListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getHighLevelCourseList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getHighLevelCourseListReqData>();
                reqObj = reqObj == null ? new getHighLevelCourseListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getHighLevelCourseListReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.courseTypeId == null || reqObj.special.courseTypeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "courseTypeId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getHighLevelCourseListRespContentData();
                ZCourseBLL service = new ZCourseBLL(loggingSessionInfo);
                ZCourseEntity queryEntity = new ZCourseEntity();
                queryEntity.CourseTypeId = ToInt(reqObj.special.courseTypeId);
                IList<ZCourseEntity> list = service.GetCourses(queryEntity, 0, 1000);
                if (list != null)
                {
                    respData.content.courseList = new List<getHighLevelCourseListRespCourseData>();
                    foreach (var item in list)
                    {
                        respData.content.courseList.Add(new getHighLevelCourseListRespCourseData()
                        {
                            courseId = ToStr(item.CourseId),
                            courseName = ToStr(item.CourseName),
                            displayIndex = ToStr(item.DisplayIndex),
                            imageUrl = ToStr(item.ImageUrl)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getHighLevelCourseListRespData : Default.LowerRespData
        {
            public getHighLevelCourseListRespContentData content { get; set; }
        }
        public class getHighLevelCourseListRespContentData
        {
            public IList<getHighLevelCourseListRespCourseData> courseList { get; set; }
        }
        public class getHighLevelCourseListRespCourseData
        {
            public string courseId { get; set; }
            public string courseName { get; set; }
            public string displayIndex { get; set; }
            public string imageUrl { get; set; }
        }
        public class getHighLevelCourseListReqData : ReqData
        {
            public getHighLevelCourseListReqSpecialData special;
        }
        public class getHighLevelCourseListReqSpecialData
        {
            public string courseTypeId { get; set; }
        }
        #endregion

        #region 获取新闻集合
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getNewsListByCourseId()
        {
            string content = string.Empty;
            var respData = new getNewsListByCourseIdRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getNewsListByCourseId: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getNewsListByCourseIdReqData>();
                reqObj = reqObj == null ? new getNewsListByCourseIdReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getNewsListByCourseIdReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    reqObj.special = new getNewsListByCourseIdReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 10;
                }

                if (reqObj.special.courseTypeId == null || reqObj.special.courseTypeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "courseTypeId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getNewsListByCourseIdRespContentData();
                respData.content.newsList = new List<getNewsListByCourseIdRespForumData>();
                string pateTitle = getNewsTitleByCourseId(reqObj.special.courseTypeId);
                respData.content.pageTitle = pateTitle;
                #region
                LNewsBLL service = new LNewsBLL(loggingSessionInfo);
                int totalCount = service.GetNewsByCourseCount(reqObj.special.courseTypeId);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }

                System.Data.DataSet ds = new System.Data.DataSet();
                ds = service.GetNewsByCourseList(reqObj.special.courseTypeId, reqObj.special.page, reqObj.special.pageSize);
                IList<getNewsListByCourseIdRespForumData> newsList = new List<getNewsListByCourseIdRespForumData>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    newsList = DataTableToObject.ConvertToList<getNewsListByCourseIdRespForumData>(ds.Tables[0]);
                    respData.content.newsList = newsList;
                }
                #endregion


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getNewsListByCourseIdRespData : Default.LowerRespData
        {
            public getNewsListByCourseIdRespContentData content { get; set; }
        }
        public class getNewsListByCourseIdRespContentData
        {
            public string pageTitle { get; set; } //页面标题(Jermyn20131012)
            public string isNext { get; set; }
            public IList<getNewsListByCourseIdRespForumData> newsList { get; set; }
        }
        public class getNewsListByCourseIdRespForumData
        {
            public string newsId { get; set; }
            public string title { get; set; }
            public string time { get; set; }
            public string imageURL { get; set; }
            public Int64 displayIndex { get; set; }
        }
        public class getNewsListByCourseIdReqData : ReqData
        {
            public getNewsListByCourseIdReqSpecialData special;
        }
        public class getNewsListByCourseIdReqSpecialData
        {
            public string courseTypeId { get; set; } //课程类型 1=MBA 2=EMBA 3=FMBA 4=高级经理课程
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion
        #endregion

        #region 获取新闻详细信息
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getNewsById()
        {
            string content = string.Empty;
            var respData = new getNewsByIdRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getNewsById: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getNewsByIdReqData>();
                reqObj = reqObj == null ? new getNewsByIdReqData() : reqObj;


                if (reqObj.special.newsId == null || reqObj.special.newsId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "newsId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getNewsByIdRespContentData();
                respData.content.imageList = new List<getNewsByIdImageData>();
                #region
                LNewsBLL service = new LNewsBLL(loggingSessionInfo);
                var newsInfo = service.GetByID(ToStr(reqObj.special.newsId));
                if (newsInfo != null)
                {
                    respData.content.newsId = ToStr(newsInfo.NewsId);
                    respData.content.title = ToStr(newsInfo.NewsTitle);
                    if (newsInfo.PublishTime == null)
                    {
                        respData.content.time = "";
                    }
                    else
                    {
                        respData.content.time = Convert.ToDateTime(newsInfo.PublishTime).ToString("yyyy-MM-dd HH");
                    }
                    respData.content.description = ToStr(newsInfo.Content);
                    IList<getNewsByIdImageData> imageList = new List<getNewsByIdImageData>();
                    getNewsByIdImageData imageInfo = new getNewsByIdImageData();
                    imageInfo.imageId = "1";
                    imageInfo.imageURL = ToStr(newsInfo.ImageUrl);
                    imageList.Add(imageInfo);
                    respData.content.imageList = imageList;
                }
                #endregion


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getNewsByIdRespData : Default.LowerRespData
        {
            public getNewsByIdRespContentData content { get; set; }
        }
        public class getNewsByIdRespContentData
        {
            public string newsId { get; set; }
            public string title { get; set; }
            public string time { get; set; }
            public string description { get; set; }
            public IList<getNewsByIdImageData> imageList { get; set; }
        }
        public class getNewsByIdImageData
        {
            public string imageId { get; set; }
            public string imageURL { get; set; }
        }
        public class getNewsByIdReqData : ReqData
        {
            public getNewsByIdReqSpecialData special;
        }
        public class getNewsByIdReqSpecialData
        {
            public string newsId { get; set; }
        }
        #endregion
        #endregion

        #region 获取新闻页面标题
        /// <summary>
        /// 获取新闻页面标题 1=MBA 2=EMBA 3=FMBA 4=高级经理课程
        /// </summary>
        /// <param name="courseTypeId"></param>
        /// <returns></returns>
        private string getNewsTitleByCourseId(string courseTypeId)
        {
            string newsTitle = string.Empty;
            switch (courseTypeId)
            {
                case "1":
                    newsTitle = "MBA";
                    break;
                case "2":
                    newsTitle = "EMBA";
                    break;
                case "3":
                    newsTitle = "FMBA";
                    break;
                case "4":
                    newsTitle = "高级经理课程";
                    break;
            }
            return newsTitle + "新闻";
        }
        #endregion

        #region 2.4.8 获取返校日活动
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public string getSchoolEventList()
        {
            string content = string.Empty;
            var respData = new getSchoolEventListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getSchoolEventList: {0}", reqContent)
                });
                #region
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getSchoolEventListReqData>();
                reqObj = reqObj == null ? new getSchoolEventListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.eventId == null || reqObj.special.eventId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "eventId不能为空";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string eventId = reqObj.special.eventId;
                //eventId = "098058E068AB4DF1BC823EA5DAC7F36D";
                #endregion
                //初始化返回对象
                respData.content = new getSchoolEventListRespContentData();
                respData.content.eventLeven1List = new List<getSchoolEventListRespContentDataItem>();
                LEventsBLL server = new LEventsBLL(loggingSessionInfo);
                LEventsEntity eventInfo = server.getSchoolEventList(eventId, userId);
                if (eventInfo == null)
                {
                    respData.content.haveSignedCount = "0";
                    respData.content.isSignUp = 0;
                    respData.code = "103";
                    respData.description = "不存在对应的数据记录";
                }
                else
                {
                    respData.content.haveSignedCount = eventInfo.HaveSignedCount.ToString();
                    respData.content.isSignUp = eventInfo.IsSignUp;
                    respData.content.eventName = ToStr(eventInfo.Title);
                    respData.content.eventEnName = ToStr(eventInfo.Description);
                    ZCourseApplyBLL applyServer = new ZCourseApplyBLL(loggingSessionInfo);
                    var userList = applyServer.QueryByEntity(new ZCourseApplyEntity
                    {
                        OpenId = userId
                    }, new OrderBy[] { new OrderBy { FieldName = " CreateTime ", Direction = OrderByDirections.Desc } });

                    if (userList != null && userList.Length > 0)
                    {
                        var zcourseApplyInfo = userList[0];
                        respData.content.userName = ToStr(zcourseApplyInfo.ApplyName);
                        respData.content.userEmail = ToStr(zcourseApplyInfo.Email);
                        respData.content.userClass = ToStr(zcourseApplyInfo.Class);
                        respData.content.userPhone = ToStr(zcourseApplyInfo.Phone);
                    }


                    if (eventInfo.EventList != null && eventInfo.EventList.Count > 0)
                    {
                        foreach (LEventsEntity info in eventInfo.EventList)
                        {
                            if (info.EventLevel == 1)
                            {
                                getSchoolEventListRespContentDataItem rInfo = new getSchoolEventListRespContentDataItem();
                                rInfo.eventId = ToStr(info.EventID);
                                rInfo.displayIndex = ToStr(info.DisplayIndex);
                                rInfo.eventLevel = ToStr(info.EventLevel);
                                rInfo.eventName = ToStr(info.Title);
                                rInfo.eventEnName = ToStr(info.Description);
                                rInfo.allowCount = ToStr(info.AllowCount);
                                rInfo.haveCount = ToStr(info.AppliesCount);
                                rInfo.overCount = ToStr(info.OverCount);
                                rInfo.parentId = ToStr(info.ParentEventID);
                                rInfo.isSignUp = ToStr(info.IsSignUp);
                                rInfo.eventLeven2List = new List<getSchoolEventListRespContentDataItem>();
                                foreach (LEventsEntity info1 in eventInfo.EventList)
                                {
                                    if (info1.EventLevel == 2 && info.EventID.Equals(info1.ParentEventID))
                                    {
                                        getSchoolEventListRespContentDataItem rInfo1 = new getSchoolEventListRespContentDataItem();
                                        rInfo1.eventId = ToStr(info1.EventID);
                                        rInfo1.displayIndex = ToStr(info1.DisplayIndex);
                                        rInfo1.eventLevel = ToStr(info1.EventLevel);
                                        rInfo1.eventName = ToStr(info1.Title);
                                        rInfo1.eventEnName = ToStr(info1.Description);
                                        rInfo1.allowCount = ToStr(info1.AllowCount);
                                        rInfo1.haveCount = ToStr(info1.AppliesCount);
                                        rInfo1.overCount = ToStr(info1.OverCount);
                                        rInfo1.parentId = ToStr(info1.ParentEventID);
                                        rInfo1.isSignUp = ToStr(info1.IsSignUp);
                                        rInfo1.eventLeven2List = new List<getSchoolEventListRespContentDataItem>();
                                        foreach (LEventsEntity info2 in eventInfo.EventList)
                                        {
                                            if (info2.EventLevel == 3 && info1.EventID.Equals(info2.ParentEventID))
                                            {
                                                getSchoolEventListRespContentDataItem rInfo2 = new getSchoolEventListRespContentDataItem();
                                                rInfo2.eventId = ToStr(info2.EventID);
                                                rInfo2.displayIndex = ToStr(info2.DisplayIndex);
                                                rInfo2.eventLevel = ToStr(info2.EventLevel);
                                                rInfo2.eventName = ToStr(info2.Title);
                                                rInfo2.eventEnName = ToStr(info2.Description);
                                                rInfo2.allowCount = ToStr(info2.AllowCount);
                                                rInfo2.haveCount = ToStr(info2.AppliesCount);
                                                rInfo2.overCount = ToStr(info2.OverCount);
                                                rInfo2.parentId = ToStr(info2.ParentEventID);
                                                rInfo2.isSignUp = ToStr(info2.IsSignUp);
                                                rInfo1.eventLeven2List.Add(rInfo2);
                                            }
                                        }
                                        rInfo.eventLeven2List.Add(rInfo1);
                                    }

                                }
                                respData.content.eventLeven1List.Add(rInfo);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getSchoolEventListRespData : Default.LowerRespData
        {
            public getSchoolEventListRespContentData content { get; set; }
        }
        public class getSchoolEventListRespContentData
        {
            public string haveSignedCount { get; set; }      //已报名数量
            public int isSignUp { get; set; }                //是否已报名	1=是，0=否
            public string eventName { get; set; }            //活动中文名
            public string eventEnName { get; set; }          //活动英文名
            public string userName { get; set; }		//姓名
            public string userClass { get; set; }		//班级
            public string userPhone { get; set; }		//手机
            public string userEmail { get; set; }		//邮件
            public IList<getSchoolEventListRespContentDataItem> eventLeven1List { get; set; }     //活动集合
        }
        public class getSchoolEventListRespContentDataItem
        {
            public string eventId { get; set; }          //标识
            public string displayIndex { get; set; }     //排序
            public string eventLevel { get; set; }       //次序
            public string eventName { get; set; }        // 活动中文名
            public string eventEnName { get; set; }     //活动英文名
            public string allowCount { get; set; }      //可以报名的数量
            public string haveCount { get; set; }       //已报名数量
            public string overCount { get; set; }      //剩余数量
            public string isSignUp { get; set; }        //是否报名
            public string parentId { get; set; }        //父节点
            public IList<getSchoolEventListRespContentDataItem> eventLeven2List { get; set; }     //活动集合

        }
        public class getSchoolEventListReqData : ReqData
        {
            public getSchoolEventListReqSpecialData special;
        }
        public class getSchoolEventListReqSpecialData
        {
            public string eventId { get; set; }    //活动标识 10000
        }
        #endregion

        #region 2.4.9 设置返校日活动报名
        public string setSchoolEventList()
        {
            string content = string.Empty;
            var respData = new setSchoolEventListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSchoolEventList: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setSchoolEventListReqData>();
                reqObj = reqObj == null ? new setSchoolEventListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setSchoolEventListReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.eventList == null || reqObj.special.eventList.Count == 0)
                {
                    respData.code = "2201";
                    respData.description = "必须选择活动";
                    return respData.ToJSON().ToString();
                }
                //if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                //{
                //    respData.code = "2206";
                //    respData.description = "链接有误，请尝试http://t.cn/8kU7EJS \r\n Your confirmation is not successfully, please try again at http://t.cn/8kU7EJS";
                //    return respData.ToJSON().ToString();
                //}
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                string userId = ToStr(reqObj.common.userId);
                LEventsBLL service = new LEventsBLL(loggingSessionInfo);
                //删除已经报名的信息
                bool bReturn = service.SetApplyEventDelete(userId);
                ZCourseApplyBLL applyServer = new ZCourseApplyBLL(loggingSessionInfo);
                foreach (var info in reqObj.special.eventList)
                {
                    ZCourseApplyEntity applyInfo = new ZCourseApplyEntity();
                    applyInfo.ApplyId = BaseService.NewGuidPub();
                    applyInfo.OpenId = userId;
                    applyInfo.CouseId = "098058E068AB4DF1BC823EA5DAC7F36D";
                    applyInfo.ObjectId = ToStr(info.eventId);
                    applyInfo.ApplyName = ToStr(reqObj.special.userName);
                    applyInfo.Email = ToStr(reqObj.special.userEmail);
                    applyInfo.Phone = ToStr(reqObj.special.userPhone);
                    applyInfo.Class = ToStr(reqObj.special.userClass);
                    applyInfo.CreateBy = ToStr(userId);
                    applyInfo.DataFrom = ToStr(reqObj.special.dataFrom);
                    applyInfo.Company = ToStr(reqObj.special.company);
                    applyInfo.Post = ToStr(reqObj.special.position);
                    applyServer.Create(applyInfo);
                }

                #endregion
                #region 返回信息设置
                #endregion
                respData.description = "恭喜您报名成功！";
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setSchoolEventListRespData : Default.LowerRespData
        {
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setSchoolEventListReqData : ReqData
        {
            public setSchoolEventListReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setSchoolEventListReqSpecialData
        {
            public string userName { get; set; }		//姓名
            public string userClass { get; set; }		//班级
            public string userPhone { get; set; }		//手机
            public string userEmail { get; set; }		//邮件
            public string dataFrom { get; set; }
            public string company { get; set; }         //单位
            public string position { get; set; }        //职务
            public IList<SchoolEventListClass> eventList { get; set; }
        }

        public class SchoolEventListClass
        {
            public string eventId { get; set; }       //活动标识
        }
        #endregion
        #endregion

        #region 2.4.10获取中欧智库与中欧新闻集合
        public string getZONewsOrZKList()
        {
            string content = string.Empty;
            var respData = new getZONewsOrZKListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getZONewsOrZKList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getZONewsOrZKListReqData>();
                reqObj = reqObj == null ? new getZONewsOrZKListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getZONewsOrZKListReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.typeId == null || reqObj.special.typeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "TypeId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getZONewsOrZKListRespContentData();
                string modelId = string.Empty;
                string pageTitle = string.Empty;
                switch (reqObj.special.typeId.ToString().Trim())
                {
                    case "1":
                        modelId = "D6F39B42EBF3463BA45AC0812D0A61EF";
                        pageTitle = "中欧智库";
                        break;
                    case "2":
                        modelId = "9402FA79589E4E788EC00AE3521C7275";
                        pageTitle = "中欧新闻";
                        break;
                }
                WMaterialTextBLL server = new WMaterialTextBLL(loggingSessionInfo);
                WMaterialTextEntity info = new WMaterialTextEntity();
                info.ModelId = modelId;
                var list = server.GetWebList(info, 0, 100);

                if (list != null)
                {
                    respData.content.pageTitle = pageTitle;
                    respData.content.zoNewsList = new List<getZONewsOrZKListRespForumData>();
                    foreach (var item in list)
                    {
                        respData.content.zoNewsList.Add(new getZONewsOrZKListRespForumData()
                        {
                            textId = ToStr(item.TextId),
                            title = ToStr(item.Title),
                            displayIndex = ToStr(item.DisplayIndex),
                            imageList = ToStr(item.CoverImageUrl)
                            ,
                            oriUrl = ToStr(item.OriginalUrl)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getZONewsOrZKListRespData : Default.LowerRespData
        {
            public getZONewsOrZKListRespContentData content { get; set; }
        }
        public class getZONewsOrZKListRespContentData
        {
            public string pageTitle { get; set; } //页面标题(Jermyn20131012)
            public IList<getZONewsOrZKListRespForumData> zoNewsList { get; set; }
        }
        public class getZONewsOrZKListRespForumData
        {
            public string textId { get; set; }
            public string title { get; set; }
            public string displayIndex { get; set; }
            public string imageList { get; set; }
            public string oriUrl { get; set; }
        }
        public class getZONewsOrZKListReqData : ReqData
        {
            public getZONewsOrZKListReqSpecialData special;
        }
        public class getZONewsOrZKListReqSpecialData
        {
            public string typeId { get; set; }
        }
        #endregion

        #region 获取中欧智库与中欧新闻的明细
        public string getZONewsOrZKDetail()
        {
            string content = string.Empty;
            var respData = new getZONewsOrZKDetailRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getZONewsOrZKDetail: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getZONewsOrZKDetailReqData>();
                reqObj = reqObj == null ? new getZONewsOrZKDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getZONewsOrZKDetailReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.textId == null || reqObj.special.textId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "textId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getZONewsOrZKDetailRespContentData();
                WMaterialTextBLL server = new WMaterialTextBLL(loggingSessionInfo);
                var condition = new List<IWhereCondition> { 
                        new EqualsCondition() { FieldName = "TextId", Value = reqObj.special.textId.ToString() }
                    };
                var data = server.Query(condition.ToArray(), null).ToList();
                if (data != null && data.Count > 0)
                {
                    var newsEntity = data.FirstOrDefault();
                    respData.content.title = newsEntity.Title;
                    respData.content.imageUrl = newsEntity.CoverImageUrl;
                    respData.content.createTime = newsEntity.CreateTime.Value.ToString("yyyy-MM-dd");
                    respData.content.text = newsEntity.Text;
                }


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getZONewsOrZKDetailRespData : Default.LowerRespData
        {
            public getZONewsOrZKDetailRespContentData content { get; set; }
        }
        public class getZONewsOrZKDetailRespContentData
        {
            public string textId { get; set; }
            public string title { get; set; }
            public string imageUrl { get; set; }
            public string createTime { get; set; }
            public string text { get; set; }
        }

        public class getZONewsOrZKDetailReqData : ReqData
        {
            public getZONewsOrZKDetailReqSpecialData special;
        }
        public class getZONewsOrZKDetailReqSpecialData
        {
            public string textId { get; set; }
        }
        #endregion

        #endregion

        #region 浏览历史提交
        public string setBrowseHistory()
        {
            string content = string.Empty;
            var respData = new setBrowseHistoryRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setBrowseHistory: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setBrowseHistoryReqData>();
                reqObj = reqObj == null ? new setBrowseHistoryReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setBrowseHistoryReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.itemId == null || reqObj.special.itemId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "商品标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }

                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                if (vipId == null || vipId.Length == 0)
                {
                    //respData.code = "2200";
                    //respData.description = "未查询到匹配的VIP信息";
                    //return respData.ToJSON().ToString();
                    vipId = ToStr(reqObj.common.userId);
                }

                respData.content = new setBrowseHistoryRespContentData();
                BrowseHistoryBLL service = new BrowseHistoryBLL(loggingSessionInfo);
                BrowseHistoryEntity obj = new BrowseHistoryEntity();
                obj.BrowserHistoryId = Utils.NewGuid();
                obj.ItemId = ToStr(reqObj.special.itemId);
                obj.VipId = vipId;
                service.Create(obj);
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setBrowseHistory content: {0}", content)
            });
            return content;
        }

        public class setBrowseHistoryRespData : Default.LowerRespData
        {
            public setBrowseHistoryRespContentData content { get; set; }
        }
        public class setBrowseHistoryRespContentData
        {

        }
        public class setBrowseHistoryReqData : ReqData
        {
            public setBrowseHistoryReqSpecialData special;
        }
        public class setBrowseHistoryReqSpecialData
        {
            public string itemId { get; set; }
        }
        #endregion

        #region 提交购物车信息
        public string setShoppingCart()
        {
            string content = string.Empty;
            var respData = new setShoppingCartRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setShoppingCart: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setShoppingCartReqData>();
                reqObj = reqObj == null ? new setShoppingCartReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setShoppingCartReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.skuId == null || reqObj.special.skuId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "商品标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }
                //if (reqObj.special.qty == null || reqObj.special.qty == 0)
                //{
                //    respData.code = "2203";
                //    respData.description = "数量不能为0";
                //    return respData.ToJSON().ToString();
                //}

                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                if (vipId == null || vipId.Length == 0)
                {
                    vipId = reqObj.common.userId;
                    //respData.code = "2200";
                    //respData.description = "未查询到匹配的VIP信息";
                    //return respData.ToJSON().ToString();
                }

                respData.content = new setShoppingCartRespContentData();
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);

                ShoppingCartEntity scObj = null;
                var scList = service.QueryByEntity(new ShoppingCartEntity()
                {
                    VipId = vipId,
                    SkuId = ToStr(reqObj.special.skuId)
                    ,
                    BeginDate = ToStr(reqObj.special.beginDate)
                    ,
                    EndDate = ToStr(reqObj.special.endDate)
                }, null);
                if (scList == null || scList.Length == 0)
                {
                    scObj = new ShoppingCartEntity();
                    scObj.ShoppingCartId = Utils.NewGuid();
                    scObj.VipId = vipId;
                    scObj.SkuId = ToStr(reqObj.special.skuId);
                    scObj.Qty = reqObj.special.qty;
                    scObj.BeginDate = ToStr(reqObj.special.beginDate);
                    scObj.EndDate = ToStr(reqObj.special.endDate);
                    service.Create(scObj);
                }
                else
                {
                    scObj = scList[0];
                    if (reqObj.special.qty > 0)
                    {
                        //scObj.Qty = scObj.Qty  + reqObj.special.qty;
                        scObj.Qty = reqObj.special.qty;
                        service.Update(scObj, false);
                    }
                    else
                    {
                        service.Delete(scObj);
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setShoppingCart content: {0}", content)
            });
            return content;
        }

        public class setShoppingCartRespData : Default.LowerRespData
        {
            public setShoppingCartRespContentData content { get; set; }
        }
        public class setShoppingCartRespContentData
        {

        }
        public class setShoppingCartReqData : ReqData
        {
            public setShoppingCartReqSpecialData special;
        }
        public class setShoppingCartReqSpecialData
        {
            public string skuId { get; set; }
            public int qty { get; set; }
            public string beginDate { get; set; }
            public string endDate { get; set; }
        }
        #endregion

        #region 提交购物车信息 多个商品
        public string setShoppingCartList()
        {
            string content = string.Empty;
            var respData = new setShoppingCartListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setShoppingCartList: {0}", reqContent)
                });
                #region
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setShoppingCartListReqData>();
                reqObj = reqObj == null ? new setShoppingCartListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.itemList == null || reqObj.special.itemList.Count.Equals("0"))
                {
                    respData.code = "2201";
                    respData.description = "商品不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region
                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                if (vipId == null || vipId.Length == 0)
                {
                    vipId = reqObj.common.userId;
                }

                respData.content = new setShoppingCartListRespContentData();
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);

                if (reqObj.special.itemList != null && reqObj.special.itemList.Count > 0)
                {
                    foreach (var info in reqObj.special.itemList)
                    {
                        ShoppingCartEntity scObj = null;
                        var scList = service.QueryByEntity(new ShoppingCartEntity()
                        {
                            VipId = vipId,
                            SkuId = ToStr(info.skuId)
                            ,
                            BeginDate = ToStr(info.beginDate)
                            ,
                            EndDate = ToStr(info.endDate)
                        }, null);
                        if (scList == null || scList.Length == 0)
                        {
                            scObj = new ShoppingCartEntity();
                            scObj.ShoppingCartId = Utils.NewGuid();
                            scObj.VipId = vipId;
                            scObj.SkuId = ToStr(info.skuId);
                            scObj.Qty = info.qty;
                            scObj.BeginDate = ToStr(info.beginDate);
                            scObj.EndDate = ToStr(info.endDate);
                            service.Create(scObj);
                        }
                        else
                        {
                            scObj = scList[0];
                            if (info.qty > 0)
                            {
                                scObj.Qty = info.qty;
                                service.Update(scObj, false);
                            }
                            else
                            {
                                service.Delete(scObj);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setShoppingCart content: {0}", content)
            });
            return content;
        }

        public class setShoppingCartListRespData : Default.LowerRespData
        {
            public setShoppingCartListRespContentData content { get; set; }
        }
        public class setShoppingCartListRespContentData
        {

        }
        public class setShoppingCartListReqData : ReqData
        {
            public setShoppingCartListReqSpecialData special;
        }
        public class setShoppingCartListReqSpecialData
        {
            public IList<setShoppingItemInfo> itemList;
        }

        public class setShoppingItemInfo
        {
            public string skuId { get; set; }
            public int qty { get; set; }
            public string beginDate { get; set; }
            public string endDate { get; set; }
        }
        #endregion


        #region 获取购物车商品信息
        public string getShoppingCart()
        {
            string content = string.Empty;
            var respData = new getShoppingCartRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getShoppingCart: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getShoppingCartReqData>();
                reqObj = reqObj == null ? new getShoppingCartReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getShoppingCartReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }

                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                if (vipId == null || vipId.Length == 0)
                {
                    vipId = reqObj.common.userId;
                }

                respData.content = new getShoppingCartRespContentData();
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);
                ShoppingCartEntity queryEntity = new ShoppingCartEntity();
                queryEntity.VipId = vipId;
                int totalCount = service.GetListCount(queryEntity);
                decimal totalAmount = service.GetListAmount(queryEntity);
                int totalQty = service.GetListQty(queryEntity);
                IList<ShoppingCartEntity> list = service.GetList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                respData.content.totalQty = totalQty;
                respData.content.totalAmount = totalAmount;
                if (list != null)
                {
                    respData.content.itemList = new List<getShoppingCartRespCourseData>();
                    foreach (var item in list)
                    {
                        respData.content.itemList.Add(new getShoppingCartRespCourseData()
                        {
                            itemId = ToStr(item.ItemDetail.item_id),
                            itemName = ToStr(item.ItemDetail.item_name),
                            imageUrl = ToStr(item.ItemDetail.imageUrl),
                            price = ToDouble(item.ItemDetail.Price),
                            salesPrice = ToDouble(item.ItemDetail.SalesPrice),
                            discountRate = ToDouble(item.ItemDetail.DiscountRate),
                            displayIndex = item.DisplayIndex,
                            qty = ToInt(item.Qty),
                            selDate = ToStr(item.LastUpdateTime),
                            skuId = ToStr(item.SkuId),
                            gg = ToStr(item.GG)
                            ,
                            itemCategoryName = ToStr(item.ItemDetail.itemCategoryName)
                            ,
                            beginDate = ToStr(item.BeginDate)
                            ,
                            endDate = ToStr(item.EndDate)
                            ,
                            dayCount = ToInt(item.DayCount)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getShoppingCartRespData : Default.LowerRespData
        {
            public getShoppingCartRespContentData content { get; set; }
        }
        public class getShoppingCartRespContentData
        {
            public string isNext { get; set; }
            public int totalQty { get; set; }
            public decimal totalAmount { get; set; }
            public IList<getShoppingCartRespCourseData> itemList { get; set; }
        }
        public class getShoppingCartRespCourseData
        {
            public string itemId { get; set; }
            public string itemName { get; set; }
            public string imageUrl { get; set; }
            public double price { get; set; }
            public double salesPrice { get; set; }
            public double discountRate { get; set; }
            public Int64 displayIndex { get; set; }
            public int qty { get; set; }
            public string selDate { get; set; }
            public string skuId { get; set; }
            public string gg { get; set; }
            public string itemCategoryName { get; set; }
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public int dayCount { get; set; } //入住天数
        }
        public class getShoppingCartReqData : ReqData
        {
            public getShoppingCartReqSpecialData special;
        }
        public class getShoppingCartReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion

        #region 获取浏览历史商品信息
        public string getBrowseHistory()
        {
            string content = string.Empty;
            var respData = new getBrowseHistoryRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getBrowseHistory: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getBrowseHistoryReqData>();
                reqObj = reqObj == null ? new getBrowseHistoryReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getBrowseHistoryReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }

                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                if (vipId == null || vipId.Length == 0)
                {
                    vipId = ToStr(reqObj.common.userId);
                }

                respData.content = new getBrowseHistoryRespContentData();
                BrowseHistoryBLL service = new BrowseHistoryBLL(loggingSessionInfo);
                BrowseHistoryEntity queryEntity = new BrowseHistoryEntity();
                queryEntity.VipId = vipId;
                int totalCount = service.GetListCount(queryEntity);
                IList<BrowseHistoryEntity> list = service.GetList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.itemList = new List<getBrowseHistoryRespCourseData>();
                    foreach (var item in list)
                    {
                        respData.content.itemList.Add(new getBrowseHistoryRespCourseData()
                        {
                            itemId = ToStr(item.ItemId),
                            itemName = ToStr(item.ItemDetail.item_name),
                            imageUrl = ToStr(item.ItemDetail.imageUrl),
                            price = ToDouble(item.ItemDetail.Price),
                            salesPrice = ToDouble(item.ItemDetail.SalesPrice),
                            discountRate = ToDouble(item.ItemDetail.DiscountRate),
                            displayIndex = item.DisplayIndex,
                            monthSalesCount = ToInt(item.ItemDetail.MonthSalesCount),
                            selDate = ToStr(item.LastUpdateTime)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getBrowseHistoryRespData : Default.LowerRespData
        {
            public getBrowseHistoryRespContentData content { get; set; }
        }
        public class getBrowseHistoryRespContentData
        {
            public string isNext { get; set; }
            public IList<getBrowseHistoryRespCourseData> itemList { get; set; }
        }
        public class getBrowseHistoryRespCourseData
        {
            public string itemId { get; set; }
            public string itemName { get; set; }
            public string imageUrl { get; set; }
            public double price { get; set; }
            public double salesPrice { get; set; }
            public double discountRate { get; set; }
            public Int64 displayIndex { get; set; }
            public int qty { get; set; }
            public string selDate { get; set; }
            public int monthSalesCount { get; set; }
        }
        public class getBrowseHistoryReqData : ReqData
        {
            public getBrowseHistoryReqSpecialData special;
        }
        public class getBrowseHistoryReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion

        #region 获取收藏商品信息
        public string getItemKeep()
        {
            string content = string.Empty;
            var respData = new getItemKeepRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemKeep: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getItemKeepReqData>();
                reqObj = reqObj == null ? new getItemKeepReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getItemKeepReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                //if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                //{
                //    respData.code = "2202";
                //    respData.description = "openId不能为空";
                //    return respData.ToJSON().ToString();
                //}

                var vipId = "";

                #region 判断是否传入userId.如果未传入，根据OpenId从数据库中取
               
                if (!string.IsNullOrWhiteSpace(reqObj.common.userId))
                {
                    var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                    {
                        VIPID = reqObj.common.userId
                    }, null);
                    if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                }
                else
                {
                    var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                    {
                        WeiXinUserId=reqObj.common.openId
                    },null);
                    if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                }
                #endregion
                if (vipId == null || vipId.Length == 0)
                {
                    respData.code = "2200";
                    respData.description = "未查询到匹配的VIP信息";
                    return respData.ToJSON().ToString();
                }


                respData.content = new getItemKeepRespContentData();
                ItemKeepBLL service = new ItemKeepBLL(loggingSessionInfo);
                ItemKeepEntity queryEntity = new ItemKeepEntity();
                queryEntity.VipId = vipId;
                int totalCount = service.GetListCount(queryEntity);
                IList<ItemKeepEntity> list = service.GetList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.itemList = new List<getItemKeepRespCourseData>();
                    foreach (var item in list)
                    {
                        if (item.ItemDetail != null && item.ItemDetail.item_name != null)
                        {
                            respData.content.itemList.Add(new getItemKeepRespCourseData()
                            {
                                itemId = ToStr(item.ItemId),
                                itemName = ToStr(item.ItemDetail.item_name),
                                imageUrl = ToStr(item.ItemDetail.imageUrl),
                                price = ToDouble(item.ItemDetail.Price),
                                salesPrice = ToDouble(item.ItemDetail.SalesPrice),
                                discountRate = ToDouble(item.ItemDetail.DiscountRate),
                                displayIndex = item.DisplayIndex,
                                monthSalesCount = ToInt(item.ItemDetail.MonthSalesCount),
                                selDate = ToStr(item.LastUpdateTime)
                                ,
                                skuId = ToStr(item.SkuId)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getItemKeepRespData : Default.LowerRespData
        {
            public getItemKeepRespContentData content { get; set; }
        }
        public class getItemKeepRespContentData
        {
            public string isNext { get; set; }
            public IList<getItemKeepRespCourseData> itemList { get; set; }
        }
        public class getItemKeepRespCourseData
        {
            public string itemId { get; set; }
            public string itemName { get; set; }
            public string imageUrl { get; set; }
            public double price { get; set; }
            public double salesPrice { get; set; }
            public double discountRate { get; set; }
            public Int64 displayIndex { get; set; }
            public int qty { get; set; }
            public string selDate { get; set; }
            public int monthSalesCount { get; set; }
            public string skuId { get; set; }
        }
        public class getItemKeepReqData : ReqData
        {
            public getItemKeepReqSpecialData special;
        }
        public class getItemKeepReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion

        #region 获取各种状态的订单信息
        public string getOrderList()
        {
            string content = string.Empty;
            var respData = new getOrderListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getOrderList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getOrderListReqData>();
                reqObj = reqObj == null ? new getOrderListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getOrderListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }

                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                if (vipId == null || vipId.Length == 0)
                {
                    //respData.code = "2200";
                    //respData.description = "未查询到匹配的VIP信息";
                    //return respData.ToJSON().ToString();
                    vipId = ToStr(reqObj.common.userId);
                }

                respData.content = new getOrderListRespContentData();
                var inoutService = new InoutService(loggingSessionInfo);
                var skuService = new SkuService(loggingSessionInfo);
                var objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
                ItemKeepEntity queryEntity = new ItemKeepEntity();
                queryEntity.VipId = vipId;

                var order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
                var order_reason_type_id = "2F6891A2194A4BBAB6F17B4C99A6C6F5";
                var red_flag = "1";
                int maxRowCount = reqObj.special.pageSize;
                int startRowIndex = (reqObj.special.page - 1) * reqObj.special.pageSize;

                var data = inoutService.SearchInoutInfo(
                    "", // order_no
                    order_reason_type_id,
                    "", //sales_unit_id,
                    "", //warehouse_id,
                    "", //purchase_unit_id,
                    "", //status,
                    "", //order_date_begin,
                    "", //order_date_end,
                    "", //complete_date_begin,
                    "", //complete_date_end,
                    "", //data_from_id,
                    "", //ref_order_no,
                    order_type_id,
                    red_flag,
                    maxRowCount,
                    startRowIndex,
                    "", //purchase_warehouse_id
                    "", //sales_warehouse_id
                    reqObj.special.status, //Field7, 
                    "", //DeliveryId, 
                    "", //DefrayTypeId, 
                    "", //Field9_begin, 
                    "", //Field9_end, 
                    "", //ModifyTime_begin, 
                    "", //ModifyTime_end
                    reqObj.special.orderId,
                    vipId, "", null,false);

                int totalCount = data.ICount;
                var list = data.InoutInfoList;
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.orderList = new List<getOrderListRespCourseData>();
                    foreach (var item in list)
                    {
                        var orderItem = new getOrderListRespCourseData();
                        orderItem.orderId = item.order_id;
                        orderItem.orderCode = item.order_no;
                        orderItem.orderDate = item.order_date;
                        orderItem.totalQty = item.total_qty;
                        orderItem.totalAmount = item.total_amount;
                        orderItem.mobile = item.Field6;
                        orderItem.postcode = item.Field5;
                        orderItem.deliveryAddress = item.Field4;
                        orderItem.deliveryTime = item.Field9;
                        orderItem.remark = item.remark;
                        orderItem.deliveryName = item.DeliveryName;
                        orderItem.username = item.Field3;
                        orderItem.status = item.status;
                        orderItem.statusDesc = item.status_desc;
                        orderItem.clinchTime = item.create_time;
                        orderItem.carrierName = item.carrier_name;
                        orderItem.receiptTime = item.accpect_time;
                        orderItem.storeId = ToStr(item.purchase_unit_id);

                        if (item.create_time != null && item.create_time.Length > 5)
                        {
                            orderItem.createTime = Convert.ToDateTime(item.create_time).ToString("yyyy-MM-dd HH:mm");
                        }

                        orderItem.orderDetailList = new List<getOrderListRespDetailData>();
                        item.InoutDetailList = inoutService.GetInoutDetailInfoByOrderId(orderItem.orderId);
                        if (item.InoutDetailList != null)
                        {
                            foreach (InoutDetailInfo tmpOrderDetail in item.InoutDetailList)
                            {
                                var orderDetail = new getOrderListRespDetailData();
                                orderDetail.skuId = tmpOrderDetail.sku_id;
                                orderDetail.itemId = tmpOrderDetail.item_id;
                                orderDetail.itemName = tmpOrderDetail.item_name;
                                orderDetail.GG = tmpOrderDetail.prop_1_detail_name;
                                orderDetail.salesPrice = tmpOrderDetail.enter_price;
                                orderDetail.stdPrice = tmpOrderDetail.std_price;
                                orderDetail.discountRate = tmpOrderDetail.discount_rate;
                                orderDetail.qty = tmpOrderDetail.enter_qty;
                                orderDetail.itemCategoryName = tmpOrderDetail.itemCategoryName;
                                orderDetail.beginDate = ToStr(tmpOrderDetail.Field1);
                                orderDetail.endDate = ToStr(tmpOrderDetail.Field2);
                                orderDetail.dayCount = ToInt(tmpOrderDetail.DayCount);
                                orderDetail.imageList = new List<getOrderListRespDetailImageData>();
                                if (tmpOrderDetail.item_id != null && tmpOrderDetail.item_id.Length > 0)
                                {
                                    var tmpImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                                    {
                                        ObjectId = tmpOrderDetail.item_id
                                    }, null);
                                    if (tmpImageList != null)
                                    {
                                        foreach (var tmpImageItem in tmpImageList)
                                        {
                                            var orderDetailImage = new getOrderListRespDetailImageData();
                                            orderDetailImage.imageId = tmpImageItem.ImageId;
                                            orderDetailImage.imageUrl = tmpImageItem.ImageURL;
                                            orderDetail.imageList.Add(orderDetailImage);
                                        }
                                    }
                                }
                                orderItem.orderDetailList.Add(orderDetail);
                            }
                        }
                        respData.content.orderList.Add(orderItem);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getOrderListRespData : Default.LowerRespData
        {
            public getOrderListRespContentData content { get; set; }
        }
        public class getOrderListRespContentData
        {
            public string isNext { get; set; }
            public IList<getOrderListRespCourseData> orderList { get; set; }
        }
        public class getOrderListRespCourseData
        {
            public string orderId { get; set; }
            public string orderCode { get; set; }
            public string orderDate { get; set; }
            public decimal totalQty { get; set; }
            public decimal totalAmount { get; set; }
            public string mobile { get; set; }
            public string postcode { get; set; }
            public string deliveryAddress { get; set; }
            public string deliveryTime { get; set; }
            public string remark { get; set; }
            public string deliveryName { get; set; }
            public string username { get; set; }
            public string status { get; set; }
            public string statusDesc { get; set; }
            public string clinchTime { get; set; }
            public string carrierName { get; set; }
            public string receiptTime { get; set; }
            public string createTime { get; set; }
            public IList<getOrderListRespDetailData> orderDetailList { get; set; }
            public string storeId { get; set; }
        }
        public class getOrderListRespDetailData
        {
            public string skuId { get; set; }
            public string itemId { get; set; }
            public string itemName { get; set; }
            public string GG { get; set; }
            public decimal salesPrice { get; set; }
            public decimal stdPrice { get; set; }
            public decimal discountRate { get; set; }
            public decimal qty { get; set; }
            public IList<getOrderListRespDetailImageData> imageList { get; set; }
            public string itemCategoryName { get; set; }
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public int dayCount { get; set; }
        }
        public class getOrderListRespDetailImageData
        {
            public string imageId { get; set; }
            public string imageUrl { get; set; }
        }
        public class getOrderListReqData : ReqData
        {
            public getOrderListReqSpecialData special;
        }
        public class getOrderListReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
            public string status { get; set; }
            public string orderId { get; set; }
        }
        #endregion

        #region 获取积分兑换商品信息集合
        public string getItemExchangeList()
        {
            string content = string.Empty;
            var respData = new getItemExchangeListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemExchangeList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getItemExchangeListReqData>();
                reqObj = reqObj == null ? new getItemExchangeListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getItemExchangeListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }

                //var vipId = "";
                //var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                //{
                //    WeiXinUserId = reqObj.common.openId
                //}, null);
                //if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
                //if (vipId == null || vipId.Length == 0)
                //{
                //    respData.code = "2200";
                //    respData.description = "未查询到匹配的VIP信息";
                //    return respData.ToJSON().ToString();
                //}

                respData.content = new getItemExchangeListRespContentData();
                ItemService service = new ItemService(loggingSessionInfo);
                VwItemDetailEntity queryEntity = new VwItemDetailEntity();
                queryEntity.create_user_id = reqObj.common.userId.ToString().Trim();
                int totalCount = service.GetVwItemDetailListCount(queryEntity);
                IList<VwItemDetailEntity> list = service.GetVwItemDetailList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.itemList = new List<getItemExchangeListRespCourseData>();
                    foreach (var item in list)
                    {
                        int Integral = 0;
                        if (item.Integral == null || item.Integral.Equals("") || item.Integral.Equals("0"))
                        {

                        }
                        else
                        {
                            Integral = ToInt(item.Integral);
                        }
                        respData.content.itemList.Add(new getItemExchangeListRespCourseData()
                        {
                            itemId = ToStr(item.item_id),
                            itemName = ToStr(item.item_name),
                            price = ToDouble(item.Price),

                            integralExchange = ToStr(Integral),
                            displayIndex = ToInt(item.DisplayIndexLast),
                            selDate = ToStr(item.create_time)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getItemExchangeListRespData : Default.LowerRespData
        {
            public getItemExchangeListRespContentData content { get; set; }
        }
        public class getItemExchangeListRespContentData
        {
            public string isNext { get; set; }
            public IList<getItemExchangeListRespCourseData> itemList { get; set; }
        }
        public class getItemExchangeListRespCourseData
        {
            public string itemId { get; set; }
            public string itemName { get; set; }
            public double price { get; set; }
            public string integralExchange { get; set; }
            public int displayIndex { get; set; }
            public string selDate { get; set; }
        }
        public class getItemExchangeListReqData : ReqData
        {
            public getItemExchangeListReqSpecialData special;
        }
        public class getItemExchangeListReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion

        #region 订单状态修改
        public string setOrderStatus()
        {
            string content = string.Empty;
            var respData = new setOrderStatusRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderStatus: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setOrderStatusReqData>();
                reqObj = reqObj == null ? new setOrderStatusReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderStatusReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "订单标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.status == null || reqObj.special.status.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "状态不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new setOrderStatusRespContentData();
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);
                InoutService inoutService = new InoutService(loggingSessionInfo);

                var flag = inoutService.UpdateOrderDeliveryStatus(reqObj.special.orderId, reqObj.special.status, reqObj.special.tableNo,Utils.GetNow());
                if (reqObj.special.status != "-99")
                {
                    var shopchatbll = new ShoppingCartBLL(loggingSessionInfo);
                    shopchatbll.DeleteShoppingCart(reqObj.special.orderId);
                }
                if (!flag)
                {
                    respData.code = "103";
                    respData.description = "更新订单状态失败";
                }
                else
                {
                    var OrderID = reqObj.special.orderId;
                    var orderInfo = inoutService.GetInoutInfoById(OrderID);
                    if (orderInfo.Field3 == "1")
                    {
                        Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新O2OMarketing订单状态成功[OrderID={0}].", OrderID) });
                        //更新阿拉丁的订单状态
                        JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatus aldChangeOrder = new OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatus();
                        if (string.IsNullOrEmpty(orderInfo.vipId))
                            throw new Exception("会员ID不能为空,OrderID:" + OrderID);
                        aldChangeOrder.MemberID = string.IsNullOrEmpty(orderInfo.vipId) ? Guid.NewGuid() : new Guid(orderInfo.vipId);
                        aldChangeOrder.SourceOrdersID = OrderID;
                        aldChangeOrder.Status = int.Parse(orderInfo.status);
                        JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest aldRequest = new OnlineShopping.data.DataOnlineShoppingHandler.ALDChangeOrderStatusRequest();
                        aldRequest.BusinessZoneID = 1;//写死
                        aldRequest.Locale = 1;

                        aldRequest.UserID = new Guid(orderInfo.vipId);
                        aldRequest.Parameters = aldChangeOrder;
                        var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                        var postContent = string.Format("Action=ChangeOrderStatus&ReqContent={0}", aldRequest.ToJSON());
                        var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                        var aldRsp = strAldRsp.DeserializeJSONTo<JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                        if (!aldRsp.IsSuccess())
                        {
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新阿拉丁订单状态失败[Request ={0}][Response={1}]", aldRequest.ToJSON(), strAldRsp) });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setOrderStatus content: {0}", content)
            });
            return content;
        }

        public class setOrderStatusRespData : Default.LowerRespData
        {
            public setOrderStatusRespContentData content { get; set; }
        }
        public class setOrderStatusRespContentData
        {

        }
        public class setOrderStatusReqData : ReqData
        {
            public setOrderStatusReqSpecialData special;
        }
        public class setOrderStatusReqSpecialData
        {
            public string orderId { get; set; }
            public string status { get; set; }
            public string tableNo { get; set; }
        }
        #endregion

        #region 注册
        public string setSignUp()
        {
            string content = string.Empty;
            var respData = new setSignUpRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setSignUpReqData>();
                reqObj = reqObj == null ? new setSignUpReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setSignUpReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "手机号码不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "openId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.password == null || reqObj.special.password.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "密码不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new setSignUpRespContentData();
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);
                InoutService inoutService = new InoutService(loggingSessionInfo);
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity vipObj1 = new VipEntity();
                vipObj1.Phone = reqObj.special.phone;
                vipObj1.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                //vipObj1.VipPasswrod = reqObj.special.password;
                var list = vipBLL.QueryByEntity(vipObj1, null);
                if (list != null && list.Length > 0)
                {
                    respData.code = "2203";
                    respData.description = "用户名已经存在";
                    return respData.ToJSON().ToString();
                }
                VipEntity vipObj2 = new VipEntity();
                vipObj2.VIPID = ToStr(reqObj.common.userId);
                var list2 = vipBLL.QueryByEntity(vipObj2, null);
                if (list2 != null && list2.Length > 0)
                {
                    respData.code = "2204";
                    respData.description = "用户已经存在";
                    return respData.ToJSON().ToString();
                }

                VipEntity vipObj = new VipEntity();
                vipObj.VIPID = reqObj.common.userId;
                vipObj.UserName = reqObj.special.phone;
                vipObj.WeiXinUserId = reqObj.common.userId;
                vipObj.Phone = reqObj.special.phone;
                vipObj.VipPasswrod = reqObj.special.password;
                vipObj.VipCode = vipBLL.GetVipCode();
                vipObj.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                vipObj.Status = 1;
                vipBLL.Create(vipObj);
                respData.content.userId = vipObj.VIPID;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }

        public class setSignUpRespData : Default.LowerRespData
        {
            public setSignUpRespContentData content { get; set; }
        }
        public class setSignUpRespContentData
        {
            public string userId { get; set; }
        }
        public class setSignUpReqData : ReqData
        {
            public setSignUpReqSpecialData special;
        }
        public class setSignUpReqSpecialData
        {
            public string phone { get; set; }
            public string password { get; set; }
        }
        #endregion

        #region 登录
        public string setSignIn()
        {
            string content = string.Empty;
            var respData = new setSignInRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignIn: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setSignInReqData>();
                reqObj = reqObj == null ? new setSignInReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setSignInReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "手机号码不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.password == null || reqObj.special.password.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "密码不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new setSignInRespContentData();
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);
                InoutService inoutService = new InoutService(loggingSessionInfo);
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity vipObj = new VipEntity();
                vipObj.Phone = reqObj.special.phone;
                vipObj.VipPasswrod = reqObj.special.password;
                vipObj.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                var list = vipBLL.QueryByEntity(vipObj, null);
                if (list != null && list.Length > 0)
                {
                    var vip = list[0];
                    respData.content.userId = vip.VIPID;
                }
                else
                {
                    respData.code = "103";
                    respData.description = "你的用户名或者密码错误.";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignIn content: {0}", content)
            });
            return content;
        }

        public class setSignInRespData : Default.LowerRespData
        {
            public setSignInRespContentData content { get; set; }
        }
        public class setSignInRespContentData
        {
            public string userId { get; set; }
        }
        public class setSignInReqData : ReqData
        {
            public setSignInReqSpecialData special;
        }
        public class setSignInReqSpecialData
        {
            public string phone { get; set; }
            public string password { get; set; }
        }
        #endregion

        #region Jermyn20131008 酒店系统 城市
        public string getCityList()
        {
            string content = string.Empty;
            var respData = new getCityListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getItemList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getCityListReqData>();
                reqObj = reqObj == null ? new getCityListReqData() : reqObj;


                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;


                //初始化返回对象
                respData.content = new getCityListRespContentData();
                respData.content.cityList = new List<CityInfo>();
                CityInfo cityInfo1 = new CityInfo();
                cityInfo1.city = "上海";
                respData.content.cityList.Add(cityInfo1);
                CityInfo cityInfo2 = new CityInfo();
                cityInfo2.city = "北京";
                respData.content.cityList.Add(cityInfo2);
                CityInfo cityInfo3 = new CityInfo();
                cityInfo3.city = "广州";
                respData.content.cityList.Add(cityInfo3);
                CityInfo cityInfo4 = new CityInfo();
                cityInfo4.city = "深圳";
                respData.content.cityList.Add(cityInfo4);
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getCityListRespData : Default.LowerRespData
        {
            public getCityListRespContentData content { get; set; }
        }
        public class getCityListRespContentData
        {
            public IList<CityInfo> cityList { get; set; }     //商品集合
        }
        public class CityInfo
        {
            public string city { get; set; }       //商品标识

        }
        public class getCityListReqData : ReqData
        {
        }
        #endregion
        #endregion

        #region 获取城市的门店集合
        public string getStoreListByCity()
        {
            string content = string.Empty;
            var respData = new getStoreListByCityRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getStoreListByCity: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreListByCityReqData>();
                reqObj = reqObj == null ? new getStoreListByCityReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreListByCityReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 10;
                }
                if (reqObj.special.city == null || reqObj.special.city.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "城市不能为空";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion
                //查询参数
                string userId = reqObj.common.userId;

                respData.content = new getStoreListByCityRespContentData();
                respData.content.storeList = new List<getStoreListByCityRespContentItemTypeData>();

                #region
                string strError = string.Empty;
                StoreBrandMappingBLL server = new StoreBrandMappingBLL(loggingSessionInfo);
                var storeInfo = server.GetStoreListByCity(reqObj.special.city
                                                            , reqObj.special.page
                                                            , reqObj.special.pageSize
                                                            , reqObj.special.longitude
                                                            , reqObj.special.latitude
                                                            , ""
                                                            , ""
                                                            , out strError);
                if (strError.Equals("ok"))
                {
                    IList<getStoreListByCityRespContentItemTypeData> list = new List<getStoreListByCityRespContentItemTypeData>();
                    foreach (var store in storeInfo.StoreBrandList)
                    {
                        getStoreListByCityRespContentItemTypeData info = new getStoreListByCityRespContentItemTypeData();
                        info.storeId = ToStr(store.StoreId);
                        info.storeName = ToStr(store.StoreName);
                        info.imageURL = ToStr(store.ImageUrl);
                        info.address = ToStr(store.Address);
                        info.tel = ToStr(store.Tel);
                        info.displayIndex = ToStr(store.DisplayIndex);
                        if (store.Distance.ToString().Equals("0"))
                        {
                            info.distance = "";
                        }
                        else
                        {
                            info.distance = ToStr(store.Distance) + "km";
                        }
                        list.Add(info);
                    }
                    respData.content.storeList = list;
                    respData.content.totalCount = ToInt(storeInfo.TotalCount);
                }
                else
                {
                    respData.code = "111";
                    respData.description = "数据库操作错误";
                    respData.exception = strError;
                }
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getStoreListByCityRespData : Default.LowerRespData
        {
            public getStoreListByCityRespContentData content { get; set; }
        }
        public class getStoreListByCityRespContentData
        {
            public int totalCount { get; set; }
            public IList<getStoreListByCityRespContentItemTypeData> storeList { get; set; }     //商品类别集合
        }
        public class getStoreListByCityRespContentItemTypeData
        {
            public string storeId { get; set; }      //支付方式标识
            public string storeName { get; set; }    //支付产品类别
            public string imageURL { get; set; }
            public string displayIndex { get; set; }
            public string address { get; set; }
            public string tel { get; set; }
            public string distance { get; set; }        //距离
        }
        public class getStoreListByCityReqData : ReqData
        {
            public getStoreListByCityReqSpecialData special;
        }
        public class getStoreListByCityReqSpecialData
        {
            public string city { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
        }
        #endregion
        #endregion

        #region 获取日期集合 getDateList
        public string getDateList()
        {
            string content = string.Empty;
            var respData = new getDateListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getDateList: {0}", reqContent)
                });

                respData.content = new getDateListRespContentData();
                respData.content.dateList = new List<getDateListRespContentItemTypeData>();
                IList<getDateListRespContentItemTypeData> dateList = new List<getDateListRespContentItemTypeData>();
                //这个月最小值
                DateTime mindt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
                //这个月最大值
                DateTime maxdt = DateTime.Parse(DateTime.Parse(DateTime.Now.AddMonths(3).ToString("yyyy-MM") + "-01").AddDays(-1).ToString("yyyy-MM-dd"));
                //GetDate(mindt, maxdt); //循环打印这个月日期
                while (mindt <= maxdt)
                {
                    getDateListRespContentItemTypeData dataInfo = new getDateListRespContentItemTypeData();
                    //Response.Write(mindt + "");
                    dataInfo.strDate = mindt.ToString("yyyy-MM-dd");
                    dataInfo.strYear = mindt.ToString("yyyy");
                    dataInfo.strMonth = mindt.ToString("MM");
                    dataInfo.week = getWeekDay(ToInt(dataInfo.strYear), ToInt(dataInfo.strMonth), ToInt(mindt.ToString("dd")));
                    dateList.Add(dataInfo);
                    mindt = mindt.AddDays(1);

                }
                respData.content.dateList = dateList;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class getDateListRespData : Default.LowerRespData
        {
            public getDateListRespContentData content { get; set; }
        }
        public class getDateListRespContentData
        {
            public IList<getDateListRespContentItemTypeData> dateList { get; set; }     //商品类别集合
        }
        public class getDateListRespContentItemTypeData
        {
            public string strDate { get; set; }      //支付方式标识
            public int week { get; set; }    //支付产品类别
            public string strYear { get; set; }             //产品logo
            public string strMonth { get; set; }          //支付描述
        }
        public class getDateListReqData : ReqData
        {
            public getDateListReqSpecialData special;
        }
        public class getDateListReqSpecialData
        {

        }
        #endregion
        #region 根据日期，获得星期几
        /// <summary>根据日期，获得星期几</summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns>星期几，1代表星期一；7代表星期日</returns>
        public static int getWeekDay(int y, int m, int d)
        {
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            return week;
        }
        #endregion
        #endregion

        #region 洗衣服务
        #region  订单提交
        public string setGOrderInfo()
        {
            string content = string.Empty;
            var respData = new setGOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setGOrderInfoReqData>();
                reqObj = reqObj == null ? new setGOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setGOrderInfoReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "手机号码不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.qty == null || reqObj.special.qty.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "数量不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.address == null || reqObj.special.address.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "地址不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.lng == null || reqObj.special.lng.Equals(""))
                {
                    respData.code = "2204";
                    respData.description = "维度不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.lat == null || reqObj.special.lat.Equals(""))
                {
                    respData.code = "2205";
                    respData.description = "经度不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数

                GOrderBLL service = new GOrderBLL(loggingSessionInfo);
                GOrderEntity orderInfo = new GOrderEntity();
                orderInfo.OrderId = BaseService.NewGuidPub();
                orderInfo.OrderCode = service.GetGOrderCode();
                orderInfo.VipId = ToStr(reqObj.common.userId);
                orderInfo.OpenId = ToStr(reqObj.common.openId);
                orderInfo.Address = ToStr(reqObj.special.address);
                orderInfo.Lat = ToStr(reqObj.special.lat);
                orderInfo.Lng = ToStr(reqObj.special.lng);
                orderInfo.Phone = ToStr(reqObj.special.phone);
                orderInfo.Qty = ToStr(reqObj.special.qty);
                orderInfo.Status = "1";
                orderInfo.StatusDesc = "用户提交订单";
                orderInfo.CreateBy = ToStr(reqObj.common.userId);
                orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);
                orderInfo.CreateTime = System.DateTime.Now;
                #endregion
                #region 返回信息设置
                service.Create(orderInfo);
                respData.exception = "成功";
                respData.description = "成功";

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setGOrderInfoRespData : Default.LowerRespData
        {
            public setGOrderInfoRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setGOrderInfoRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setGOrderInfoReqData : ReqData
        {
            public setGOrderInfoReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setGOrderInfoReqSpecialData
        {
            public string phone { get; set; }		//手机号
            public string qty { get; set; }		    //数量
            public string address { get; set; }		//地址
            public string lng { get; set; }		    //经度
            public string lat { get; set; }		    //维度
        }
        #endregion
        #endregion

        #region  收单人确认收单
        public string setGOrderUpdateStatus()
        {
            string content = string.Empty;
            var respData = new setGOrderUpdateStatusRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setGOrderUpdateStatusReqData>();
                reqObj = reqObj == null ? new setGOrderUpdateStatusReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setGOrderUpdateStatusReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "订单号码不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数

                GOrderBLL service = new GOrderBLL(loggingSessionInfo);
                GOrderEntity orderInfo = new GOrderEntity();
                orderInfo = service.GetByID(reqObj.special.orderId);
                #endregion

                if (orderInfo == null)
                {
                    respData.code = "202";
                    respData.description = "订单不存在";
                }
                else
                {

                    if (Convert.ToInt32(orderInfo.Status) > 3)
                    {
                        respData.code = "201";
                        respData.description = "已有收单员收单确认了，不能重复收单.";
                    }
                    else
                    {

                        #region 返回信息设置
                        //service.Update(orderInfo);
                        string strError = string.Empty;
                        bool bReturn = service.GetReceiptConfirm(ToStr(reqObj.special.orderId), ToStr(reqObj.common.userId), ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim(), out strError);
                        respData.exception = strError;
                        respData.description = "成功";

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setGOrderUpdateStatusRespData : Default.LowerRespData
        {
            public setGOrderUpdateStatusRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setGOrderUpdateStatusRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setGOrderUpdateStatusReqData : ReqData
        {
            public setGOrderUpdateStatusReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setGOrderUpdateStatusReqSpecialData
        {
            public string orderId { get; set; }		//手机号
        }
        #endregion
        #endregion

        #region 2.8.3获取洗衣订单详细信息
        public string getGOrderInfo()
        {
            string content = string.Empty;
            var respData = new getGOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getCourseDetail: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getGOrderInfoReqData>();
                reqObj = reqObj == null ? new getGOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getGOrderInfoReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getGOrderInfoRespContentData();

                GOrderBLL service = new GOrderBLL(loggingSessionInfo);
                GOrderEntity orderInfo = new GOrderEntity();

                orderInfo = service.GetByID(ToStr(reqObj.special.orderId));

                if (orderInfo != null)
                {
                    respData.content.orderId = ToStr(orderInfo.OrderId);
                    respData.content.orderCode = ToStr(orderInfo.OrderCode);
                    respData.content.phone = ToStr(orderInfo.Phone);
                    respData.content.qty = ToStr(orderInfo.Qty);
                    respData.content.status = ToStr(orderInfo.Status);
                    respData.content.vipId = ToStr(orderInfo.VipId);
                    respData.content.openId = ToStr(orderInfo.OpenId);
                    respData.content.receiptVipId = ToStr(orderInfo.ReceiptVipId);
                    respData.content.address = ToStr(orderInfo.Address);
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getGOrderInfoRespData : Default.LowerRespData
        {
            public getGOrderInfoRespContentData content { get; set; }
        }
        public class getGOrderInfoRespContentData
        {
            public string orderId { get; set; }
            public string orderCode { get; set; }
            public string phone { get; set; }
            public string qty { get; set; }
            public string status { get; set; }
            public string vipId { get; set; }
            public string openId { get; set; }
            public string receiptVipId { get; set; }
            public string address { get; set; }
        }


        public class getGOrderInfoReqData : ReqData
        {
            public getGOrderInfoReqSpecialData special;
        }
        public class getGOrderInfoReqSpecialData
        {
            public string orderId { get; set; }
        }
        #endregion

        #endregion

        #region 获取会员秀集合
        public string getVipShowList()
        {
            string content = string.Empty;
            var respData = new getVipShowListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipShowList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipShowListReqData>();
                reqObj = reqObj == null ? new getVipShowListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getVipShowListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getVipShowListRespContentData();
                MVipShowBLL service = new MVipShowBLL(loggingSessionInfo);
                MVipShowEntity queryEntity = new MVipShowEntity();
                queryEntity.Experience = reqObj.special.experience;
                queryEntity.UserId = reqObj.common.userId;
                queryEntity.CustomerId = customerId;
                queryEntity.IsTop = ToInt(reqObj.special.isTop);
                queryEntity.IsMe = ToInt(reqObj.special.isMe);
                queryEntity.IsNewest = ToInt(reqObj.special.isNewest);
                queryEntity.IsCheck = 1;
                int totalCount = service.GetListCount(queryEntity);
                IList<MVipShowEntity> list = service.GetList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.vipShowList = new List<getVipShowListRespVipShowData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getVipShowListRespVipShowData()
                        {
                            vipShowId = ToStr(item.VipShowId),
                            experience = ToStr(item.Experience),
                            storeName = ToStr(item.UnitName),
                            praiseCount = ToInt(item.PraiseCount),
                            createTime = ToStr(Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd")),
                            hairStylistName = ToStr(item.VipName),
                            displayIndex = item.DisplayIndex
                            ,
                            isPraise = ToStr(item.IsPraise)
                            ,
                            itemId = ToStr(item.ItemId)
                        };
                        if (item.ImageList != null)
                        {
                            tmpItemObj.imageList = new List<ImageRespData>();
                            foreach (var imageItem in item.ImageList)
                            {
                                tmpItemObj.imageList.Add(new ImageRespData()
                                {
                                    imageId = imageItem.ImageId,
                                    imageURL = imageItem.ImageURL
                                });
                            }
                        }

                        respData.content.vipShowList.Add(tmpItemObj);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getVipShowListRespData : Default.LowerRespData
        {
            public getVipShowListRespContentData content { get; set; }
        }
        public class getVipShowListRespContentData
        {
            public string isNext { get; set; }
            public IList<getVipShowListRespVipShowData> vipShowList { get; set; }
        }
        public class getVipShowListRespVipShowData
        {
            public string vipShowId { get; set; }
            public string experience { get; set; }
            public string storeName { get; set; }
            public int praiseCount { get; set; }
            public string createTime { get; set; }
            public string hairStylistName { get; set; }
            public Int64 displayIndex { get; set; }
            public string isPraise { get; set; }    //是否赞过
            public string itemId { get; set; }
            public IList<ImageRespData> imageList { get; set; }
        }
        public class ImageRespData
        {
            public string imageId { get; set; }
            public string imageURL { get; set; }
        }
        public class getVipShowListReqData : ReqData
        {
            public getVipShowListReqSpecialData special;
        }
        public class getVipShowListReqSpecialData
        {
            public string experience { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
            public int isTop { get; set; } //是否要查询最热 1=是，0=否
            public int isMe { get; set; }  //是否要查询我的 1=是，0=否
            public int isNewest { get; set; }   //是否最新更新 1=是，0=否
        }
        #endregion

        #region 获取会员标签集合
        public string getVipTags()
        {
            string content = string.Empty;
            var respData = new getVipTagListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipTagList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipTagListReqData>();
                reqObj = reqObj == null ? new getVipTagListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new getVipTagListRespContentData();
                VipTagsMappingBLL service = new VipTagsMappingBLL(loggingSessionInfo);
                VipTagsMappingEntity queryEntity = new VipTagsMappingEntity();
                queryEntity.VipId = reqObj.common.userId;
                IList<TagsEntity> list = service.GetList(queryEntity);

                if (list != null)
                {
                    respData.content.vipTagList = new List<getVipTagListRespVipTagData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getVipTagListRespVipTagData()
                        {
                            TagsID = ToStr(item.TagsId),
                            TagsName = ToStr(item.TagsName),
                        };
                        respData.content.vipTagList.Add(tmpItemObj);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getVipTagListRespData : Default.LowerRespData
        {
            public getVipTagListRespContentData content { get; set; }
        }
        public class getVipTagListRespContentData
        {
            public IList<getVipTagListRespVipTagData> vipTagList { get; set; }
        }
        public class getVipTagListRespVipTagData
        {
            public string TagsID { get; set; }
            public string TagsName { get; set; }
        }
        public class getVipTagListReqData : ReqData
        {
        }
        #endregion

        #region 获取单个会员秀的详细信息
        public string getVipShowById()
        {
            string content = string.Empty;
            var respData = new getVipShowByIdRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipShowById: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipShowByIdReqData>();
                reqObj = reqObj == null ? new getVipShowByIdReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getVipShowByIdReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.vipShowId == null || reqObj.special.vipShowId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "vipShowId不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getVipShowByIdRespContentData();
                MVipShowBLL service = new MVipShowBLL(loggingSessionInfo);
                MVipShowEntity queryEntity = new MVipShowEntity();
                queryEntity.VipShowId = reqObj.special.vipShowId;
                queryEntity.UserId = reqObj.common.userId;
                int totalCount = service.GetListCount(queryEntity);
                IList<MVipShowEntity> list = service.GetList(queryEntity, 1, 1);
                if (list != null && list.Count > 0)
                {
                    respData.content.vipShowId = ToStr(list[0].VipShowId);
                    respData.content.experience = ToStr(list[0].Experience);
                    respData.content.storeName = ToStr(list[0].UnitName);
                    respData.content.praiseCount = ToInt(list[0].PraiseCount);
                    respData.content.createTime = ToStr(list[0].CreateTime);
                    respData.content.isPraise = ToStr(list[0].IsPraise);
                    respData.content.hairStylistName = ToStr(list[0].VipName);
                    respData.content.itemId = ToStr(list[0].ItemId);
                    if (list[0].ImageList != null)
                    {
                        respData.content.imageList = new List<ImageRespData>();
                        foreach (var imageItem in list[0].ImageList)
                        {
                            respData.content.imageList.Add(new ImageRespData()
                            {
                                imageId = imageItem.ImageId,
                                imageURL = imageItem.ImageURL
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getVipShowByIdRespData : Default.LowerRespData
        {
            public getVipShowByIdRespContentData content { get; set; }
        }
        public class getVipShowByIdRespContentData
        {
            public string vipShowId { get; set; }
            public string experience { get; set; }
            public string storeName { get; set; }
            public int praiseCount { get; set; }
            public string createTime { get; set; }
            public string hairStylistName { get; set; }
            public string isPraise { get; set; }    //是否赞过
            public Int64 displayIndex { get; set; }
            public string itemId { get; set; }
            public IList<ImageRespData> imageList { get; set; }
        }
        public class getVipShowByIdReqData : ReqData
        {
            public getVipShowByIdReqSpecialData special;
        }
        public class getVipShowByIdReqSpecialData
        {
            public string vipShowId { get; set; }
        }
        #endregion

        #region 根据门店获取发型师
        public string getHairStylistByStoreId()
        {
            string content = string.Empty;
            var respData = new getHairStylistByStoreIdRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getHairStylistByStoreId: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getHairStylistByStoreIdReqData>();
                reqObj = reqObj == null ? new getHairStylistByStoreIdReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getHairStylistByStoreIdReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.storeId == null || reqObj.special.storeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "storeId不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getHairStylistByStoreIdRespContentData();
                MVipShowBLL service = new MVipShowBLL(loggingSessionInfo);
                int totalCount = service.GetHairStylistByStoreIdCount(customerId, reqObj.special.storeId);
                IList<UserInfo> list = service.GetHairStylistByStoreId(customerId, reqObj.special.storeId,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.userList = new List<getHairStylistByStoreIdRespVipShowData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getHairStylistByStoreIdRespVipShowData()
                        {
                            userId = ToStr(item.User_Id),
                            userName = ToStr(item.User_Name),
                            userCode = ToStr(item.User_Code),
                            userNameEn = ToStr(item.User_Name_En),
                            post = ToStr(item.User_Postcode),
                            storeName = ToStr(item.UnitName),
                            imageURL = ToStr(item.Blog),
                            displayIndex = item.DisplayIndex
                        };
                        respData.content.userList.Add(tmpItemObj);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getHairStylistByStoreIdRespData : Default.LowerRespData
        {
            public getHairStylistByStoreIdRespContentData content { get; set; }
        }
        public class getHairStylistByStoreIdRespContentData
        {
            public string isNext { get; set; }
            public IList<getHairStylistByStoreIdRespVipShowData> userList { get; set; }
        }
        public class getHairStylistByStoreIdRespVipShowData
        {
            public string userId { get; set; }
            public string userName { get; set; }
            public string userCode { get; set; }
            public string userNameEn { get; set; }
            public string post { get; set; }
            public string storeName { get; set; }
            public Int64 displayIndex { get; set; }
            public string imageURL { get; set; }
        }
        public class getHairStylistByStoreIdReqData : ReqData
        {
            public getHairStylistByStoreIdReqSpecialData special;
        }
        public class getHairStylistByStoreIdReqSpecialData
        {
            public string storeId { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion

        #region 2.9.7 会员秀提交

        public string setVipShow()
        {
            string content = string.Empty;
            var respData = new setVipShowRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setVipShow: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setVipShowReqData>();
                reqObj = reqObj == null ? new setVipShowReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setVipShowReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.experience == null || reqObj.special.experience.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "必须输入心得";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.imageList == null || reqObj.special.imageList.Count == 0)
                {
                    respData.code = "2201";
                    respData.description = "必须图片信息";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "客户不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                MVipShowBLL service = new MVipShowBLL(loggingSessionInfo);
                MVipShowEntity showInfo = new MVipShowEntity();
                showInfo.VipShowId = BaseService.NewGuidPub();
                showInfo.Experience = ToStr(reqObj.special.experience);
                showInfo.CustomerId = customerId;
                showInfo.VipId = ToStr(reqObj.common.userId);
                showInfo.IsCheck = 1; //20131018 小将要求该
                var rnd = new Random();
                var rndNum = rnd.Next(1, 99999999);
                string lotteryCode = rndNum.ToString().PadLeft(8, '0');
                showInfo.LotteryCode = lotteryCode;
                service.Create(showInfo);
                //////////////////////////////////////////////////////////////
                ObjectImagesBLL objectServer = new ObjectImagesBLL(loggingSessionInfo);
                int i = 1;
                foreach (var detail in reqObj.special.imageList)
                {
                    ObjectImagesEntity objectInfo = new ObjectImagesEntity();
                    objectInfo.ImageId = BaseService.NewGuidPub();
                    objectInfo.ImageURL = detail.imageURL;
                    objectInfo.ObjectId = showInfo.VipShowId;
                    objectInfo.DisplayIndex = i;
                    objectInfo.CustomerId = loggingSessionInfo.CurrentUser.customer_id;
                    objectServer.Create(objectInfo);
                    i = i + 1;
                }
                #endregion
                string strError = string.Empty;
                string strMsg = string.Empty;


                #region 返回信息设置
                respData.exception = strError;
                respData.description = "上传成功.";
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setVipShowRespData : Default.LowerRespData
        {

        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setVipShowReqData : ReqData
        {
            public setVipShowReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setVipShowReqSpecialData
        {
            public string experience { get; set; }		    //心得

            public IList<setVipShowImage> imageList { get; set; }
        }

        public class setVipShowImage
        {
            public string imageURL { get; set; }       //图片地址标识
        }
        #endregion

        #endregion

        #region 密码处理

        #region 2.9.5 找回密码短信推送
        public string setSMSPush()
        {
            string content = string.Empty;
            var respData = new setSMSPushRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setVipPassword: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setSMSPushReqData>();
                reqObj = reqObj == null ? new setSMSPushReqData() : reqObj;
                #region 处理参数
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setSMSPushReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "手机号码不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region 处理业务
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity vipObj1 = new VipEntity();
                vipObj1.Phone = reqObj.special.phone;
                vipObj1.IsDelete = 0;
                var list = vipBLL.QueryByEntity(vipObj1, null);
                if (list == null && list.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "手机号不存在";
                    return respData.ToJSON().ToString();
                }
                if (list.Length > 1)
                {
                    respData.code = "2203";
                    respData.description = "该手机号对应多个客户，请直接登录";
                    return respData.ToJSON().ToString();
                }
                //3.判断发送次数
                string key = "发送消息获取密码";
                MarketSendLogBLL sendServer = new MarketSendLogBLL(loggingSessionInfo);
                int sendCount = sendServer.GetSendCountByPhone(list[0].Phone, "1", key);
                if (sendCount > 3)
                {
                    respData.code = "2203";
                    respData.description = "该手机今天发送消息获取密码，超过三次，请明天在使用该功能.";
                    return respData.ToJSON().ToString();
                }

                #region //4.记录发送日志
                string vipName = string.Empty;
                if (list[0].VipName == null || list[0].VipName.Equals(""))
                {
                    vipName = list[0].Phone;
                }
                else
                {
                    vipName = list[0].VipName;
                }
                string webUrl = ConfigurationManager.AppSettings["website_url"];
                string msgSMSText = "" + vipName + "服装提醒您，重置密码请点击：" + webUrl + "/OnlineClothing/forgetpwd.html?customerId=" + customerId + "";
                var sendFlag2 = Common.Utils.SMSSend(ToStr(list[0].Phone).Trim(), msgSMSText);
                (new MarketSendLogBLL(loggingSessionInfo)).Create(new MarketSendLogEntity()
                {
                    LogId = Common.Utils.NewGuid(),
                    VipId = list[0].VIPID.ToString(),
                    MarketEventId = key,
                    TemplateContent = msgSMSText,
                    SendTypeId = "1",
                    Phone = ToStr(list[0].Phone).Trim(),
                    IsSuccess = sendFlag2 ? 1 : 0
                });
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        #region
        public class setSMSPushRespData : Default.LowerRespData
        {
        }
        public class setSMSPushRespContentData
        {
        }
        public class setSMSPushReqData : ReqData
        {
            public setSMSPushReqSpecialData special;
        }
        public class setSMSPushReqSpecialData
        {
            public string phone { get; set; }
        }
        #endregion
        #endregion

        #region 2.9.6 设置修改会员密码
        public string setVipPassword()
        {
            string content = string.Empty;
            var respData = new setVipPasswordRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setVipPassword: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setVipPasswordReqData>();
                reqObj = reqObj == null ? new setVipPasswordReqData() : reqObj;
                #region 处理参数
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setVipPasswordReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "手机号码不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.password == null || reqObj.special.password.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "密码不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                #region 处理业务
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                VipEntity vipObj1 = new VipEntity();
                vipObj1.Phone = reqObj.special.phone;
                var list = vipBLL.QueryByEntity(vipObj1, null);
                if (list == null && list.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "手机号不存在";
                    return respData.ToJSON().ToString();
                }
                VipEntity vipObj = new VipEntity();
                vipObj.VIPID = list[0].VIPID;
                vipObj.VipPasswrod = ToStr(reqObj.special.password);
                vipBLL.Update(vipObj, false);
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        #region
        public class setVipPasswordRespData : Default.LowerRespData
        {
        }
        public class setVipPasswordRespContentData
        {
        }
        public class setVipPasswordReqData : ReqData
        {
            public setVipPasswordReqSpecialData special;
        }
        public class setVipPasswordReqSpecialData
        {
            public string phone { get; set; }
            public string password { get; set; }
        }
        #endregion
        #endregion

        #endregion

        #region 3.0 APP push devicetoken
        public string setIOSDeviceToken()
        {
            string content = string.Empty;
            var respData = new setIOSDeviceTokenRespData();
            try
            {
                #region
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setIOSDeviceTokenReqData>();
                reqObj = reqObj == null ? new setIOSDeviceTokenReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setIOSDeviceTokenReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.deviceToken == null || reqObj.special.deviceToken.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "deviceToken不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                PushUserBasicBLL service = new PushUserBasicBLL(loggingSessionInfo);
                PushUserBasicEntity basicInfo = new PushUserBasicEntity();
                basicInfo = service.GetByID(ToStr(reqObj.common.userId));
                if (basicInfo == null)
                {
                    PushUserBasicEntity basicInfo1 = new PushUserBasicEntity();
                    basicInfo1.UserId = ToStr(reqObj.common.userId);
                    basicInfo1.DeviceToken = ToStr(reqObj.special.deviceToken);
                    basicInfo1.CustomerId = ToStr(customerId);
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.DeviceToken = ToStr(reqObj.special.deviceToken);
                    service.Update(basicInfo);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        #region
        public class setIOSDeviceTokenRespData : Default.LowerRespData
        {
        }

        public class setIOSDeviceTokenReqData : ReqData
        {
            public setIOSDeviceTokenReqSpecialData special;
        }
        public class setIOSDeviceTokenReqSpecialData
        {
            public string deviceToken { get; set; }
        }
        #endregion
        #endregion

        #region 3.0.2 APP push Android basic
        public string setAndroidBasic()
        {
            string content = string.Empty;
            var respData = new setAndroidBasicRespData();
            try
            {
                #region
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUp: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setAndroidBasicReqData>();
                reqObj = reqObj == null ? new setAndroidBasicReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setAndroidBasicReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.channelId == null || reqObj.special.channelId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "channelId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.appId == null || reqObj.special.appId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "appId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.userId == null || reqObj.special.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "百度的userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                PushAndroidBasicBLL service = new PushAndroidBasicBLL(loggingSessionInfo);
                PushAndroidBasicEntity basicInfo = new PushAndroidBasicEntity();
                basicInfo = service.GetByID(ToStr(reqObj.common.userId));
                if (basicInfo == null)
                {
                    PushAndroidBasicEntity basicInfo1 = new PushAndroidBasicEntity();
                    basicInfo1.UserID = ToStr(reqObj.common.userId);
                    basicInfo1.Channel = "1";
                    basicInfo1.ChannelIDBaiDu = ToStr(reqObj.special.channelId);
                    basicInfo1.CustomerId = ToStr(customerId);
                    basicInfo1.BaiduPushAppID = ToStr(reqObj.special.appId);
                    basicInfo1.UserIDBaiDu = ToStr(reqObj.special.userId);
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.ChannelIDBaiDu = ToStr(reqObj.special.channelId);
                    basicInfo.BaiduPushAppID = ToStr(reqObj.special.appId);
                    basicInfo.UserIDBaiDu = ToStr(reqObj.special.userId);
                    service.Update(basicInfo, false);
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }
        #region
        public class setAndroidBasicRespData : Default.LowerRespData
        {
        }

        public class setAndroidBasicReqData : ReqData
        {
            public setAndroidBasicReqSpecialData special;
        }
        public class setAndroidBasicReqSpecialData
        {
            public string appId { get; set; }
            public string channelId { get; set; }
            public string userId { get; set; }
        }
        #endregion
        #endregion

        #region 3.0.3 版本更新功能
        public string checkVersion()
        {
            string content = string.Empty;
            var respData = new checkVersionRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("checkVersion: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<checkVersionReqData>();
                reqObj = reqObj == null ? new checkVersionReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new checkVersionReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                if (reqObj.special.plat == null || reqObj.special.plat.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "plat版本类型不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.channel == null || reqObj.special.channel.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "channel不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.version == null || reqObj.special.version.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "version不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.common.customerId == null || reqObj.common.customerId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                #region 业务处理
                respData.content = new checkVersionRespContentData();
                VersionManagerBLL versionManagerServer = new VersionManagerBLL(loggingSessionInfo);
                NewVersionEntity newVersionInfo = new NewVersionEntity();
                newVersionInfo = versionManagerServer.GetNewVersionEntity(reqObj.special.plat, reqObj.special.channel, reqObj.special.version, reqObj.common.userId);
                if (newVersionInfo != null)
                {
                    checkVersionRespContentData info = new checkVersionRespContentData();
                    info.canSkip = ToStr(newVersionInfo.canSkip);
                    info.isNewVersionAvailable = ToStr(newVersionInfo.isNewVersionAvailable);
                    info.message = ToStr(newVersionInfo.message);
                    info.updateUrl = ToStr(newVersionInfo.updateUrl);
                    info.version = ToStr(newVersionInfo.Version);
                }

                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class checkVersionRespData : Default.LowerRespData
        {
            public checkVersionRespContentData content { get; set; }
        }
        public class checkVersionRespContentData
        {
            public string isNewVersionAvailable { get; set; }            //是否有新版本可用  0、1
            public string message { get; set; }         //文本格式。提示信息：有新版本提供下载。增加了****等功能
            public string canSkip { get; set; }         //该版本是否可被忽略不强制下载。1：可忽略，0：不可忽略
            public string updateUrl { get; set; }       //升级的url
            public string version { get; set; }
        }

        public class checkVersionReqData : ReqData
        {
            public checkVersionReqSpecialData special;
        }
        public class checkVersionReqSpecialData
        {
            public string plat { get; set; }            //版本：iPhone, iPad, Android
            public string channel { get; set; }         //渠道	1=企业版，0=市场版
            public string version { get; set; }         //当前版本
        }
        #endregion
        #endregion

        #region 获取发布会集合(20131017)
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getNewsList()
        {
            string content = string.Empty;
            var respData = new getNewsListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getNewsListByCourseId: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getNewsListReqData>();
                reqObj = reqObj == null ? new getNewsListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getNewsListReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    reqObj.special = new getNewsListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 10;
                }

                if (reqObj.special.newsType == null || reqObj.special.newsType.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "newsType不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getNewsListRespContentData();
                respData.content.newsList = new List<getNewsListRespForumData>();

                #region
                LNewsBLL service = new LNewsBLL(loggingSessionInfo);
                int totalCount = service.GetNewsListByTypeCount(reqObj.special.newsType);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }

                System.Data.DataSet ds = new System.Data.DataSet();
                ds = service.GetNewsListByType(reqObj.special.newsType, reqObj.special.page, reqObj.special.pageSize);
                IList<getNewsListRespForumData> newsList = new List<getNewsListRespForumData>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    newsList = DataTableToObject.ConvertToList<getNewsListRespForumData>(ds.Tables[0]);
                    foreach (var newsInfo in newsList)
                    {
                        if (newsInfo.content != null)
                        {
                            newsInfo.noHtmlContent = (DataTableToObject.NoHTML(newsInfo.content)).Replace("\n", "").Replace("\t", "");
                        }
                    }
                    respData.content.newsList = newsList;
                }
                #endregion


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        public class getNewsListRespData : Default.LowerRespData
        {
            public getNewsListRespContentData content { get; set; }
        }
        public class getNewsListRespContentData
        {
            public string isNext { get; set; }
            public IList<getNewsListRespForumData> newsList { get; set; }
        }
        public class getNewsListRespForumData
        {
            public string newsId { get; set; }
            public string newsTitle { get; set; }
            public string content { get; set; }
            public string publishTime { get; set; }
            public string imageURL { get; set; }
            public Int64 displayIndex { get; set; }
            public string noHtmlContent { get; set; }
        }
        public class getNewsListReqData : ReqData
        {
            public getNewsListReqSpecialData special;
        }
        public class getNewsListReqSpecialData
        {
            public string newsType { get; set; } //类型 3= 发布会
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion
        #endregion

        #region 2.9.10 刮奖机会集合（20131017）
        /// <summary>
        /// 刮奖机会集合
        /// </summary>
        /// <returns></returns>
        public string getLotteryList()
        {
            string content = string.Empty;
            var respData = new getLotteryListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getLotteryList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getLotteryListReqData>();
                reqObj = reqObj == null ? new getLotteryListReqData() : reqObj;

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new getLotteryListRespContentData();
                MVipShowBLL service = new MVipShowBLL(loggingSessionInfo);
                var list = service.GetLotteryCount(customerId, ToStr(reqObj.common.userId));
                if (list != null && list.Count > 0)
                {
                    respData.content.lotteryCount = list.Count.ToString();
                    respData.content.vipshowList = new List<getLotteryListRespVipShowData>();
                    foreach (var info in list)
                    {
                        getLotteryListRespVipShowData showInfo = new getLotteryListRespVipShowData();
                        showInfo.vipShowId = info.VipShowId;
                        showInfo.lotteryCode = info.LotteryCode;
                        respData.content.vipshowList.Add(showInfo);
                    }
                }
                else
                {
                    respData.content.lotteryCount = "0";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = ex.ToString();
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getLotteryListRespData : Default.LowerRespData
        {
            public getLotteryListRespContentData content { get; set; }
        }
        public class getLotteryListRespContentData
        {
            public string lotteryCount { get; set; }
            public IList<getLotteryListRespVipShowData> vipshowList { get; set; }
        }
        public class getLotteryListRespVipShowData
        {
            public string vipShowId { get; set; }
            public string lotteryCode { get; set; }
        }
        public class getLotteryListReqData : ReqData
        {

        }

        #endregion


        #region 2.9.12 刮奖完成记录日志
        public string setLottery()
        {
            string content = string.Empty;
            var respData = new setLotteryRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setLottery: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setLotteryReqData>();
                reqObj = reqObj == null ? new setLotteryReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setLotteryReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.vipShowId == null || reqObj.special.vipShowId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "vipShowId不能为空";
                    return respData.ToJSON().ToString();
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                MVipShowBLL server = new MVipShowBLL(loggingSessionInfo);
                MVipShowEntity showInfo = new MVipShowEntity();
                showInfo = server.GetByID(ToStr(reqObj.special.vipShowId));
                if (showInfo == null)
                {
                    respData.code = "2201";
                    respData.description = "不存在对应的我要秀信息";
                    return respData.ToJSON().ToString();
                }
                else
                {
                    showInfo.IsLottery = 1;
                    server.Update(showInfo, false);
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class setLotteryRespData : Default.LowerRespData
        {

        }

        public class setLotteryReqData : ReqData
        {
            public setLotteryReqSpecialData special;
        }
        public class setLotteryReqSpecialData
        {
            public string vipShowId { get; set; }
        }
        #endregion

        #region getEventPrizes 2.9.11 获取活动奖项信息
        /// <summary>
        /// 获取活动奖项信息（缺中奖的存储过程，稍后实现）  2.9.11 获取活动奖项信息
        /// </summary>
        public string getEventPrizes()
        {
            string content = string.Empty;
            var respData = new getEventPrizesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventPrizesReqData>();

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.userId;
                string eventID = reqObj.special.EventId;    //活动ID
                string longitude = reqObj.special.Longitude;   //经度
                string latitude = reqObj.special.Latitude;     //纬度
                string vipID = string.Empty;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventPrizes: {0}", reqContent)
                });

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                //Default.WriteLog(loggingSessionInfo, "getEventPrizes", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventPrizesRespContentData();
                respData.content.prizes = new List<PrizesEntity>();
                respData.content.winnerList = new List<getEventPrizesWinnerList>();

                #region 是否在活动现场
                respData.content.isSite = "1";
                #endregion

                #region 获取VIPID

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    VIPID = reqObj.common.userId
                }, null);

                if (vipList == null || vipList.Length == 0)
                {
                    respData.code = "103";
                    respData.description = "未查找到匹配的VIP信息";
                    return respData.ToJSON();
                }
                else
                {
                    vipID = vipList.FirstOrDefault().VIPID;
                }

                #endregion

                #region 是否抽奖

                //LLotteryLogBLL lotteryService = new LLotteryLogBLL(loggingSessionInfo);
                //var lotterys = lotteryService.QueryByEntity(new LLotteryLogEntity()
                //{
                //    EventId = eventID,
                //    VipId = vipID
                //}, null);

                //if (lotterys != null && lotterys.Length > 0)
                //{
                //    respData.content.isLottery = "0"; //Jermyn20130730 一直能抽奖，暂时改为0，本来为1
                //}
                //else
                //{
                respData.content.isLottery = "0";
                //}

                #endregion

                #region 是否中奖

                //LPrizeWinnerBLL winnerService = new LPrizeWinnerBLL(loggingSessionInfo);
                //var prizeName = winnerService.GetWinnerInfo(vipID, eventID);

                //if (prizeName != null)
                //{
                //    respData.content.isWinning = "1";
                //    respData.content.winningDesc = prizeName.ToString();
                //}
                //else
                //{
                respData.content.isWinning = "0";
                respData.content.winningDesc = "谢谢你";
                //}
                respData.content.winningExplain = "请在当天内及时刮奖，如逾期将自动失效。亲，祝您好运！";
                #endregion

                #region 奖品信息

                LPrizesBLL prizesService = new LPrizesBLL(loggingSessionInfo);
                var prizesList = prizesService.QueryByEntity(new LPrizesEntity()
                {
                    EventId = "87747791A95442F5B8D5AC205D51BDC3"//eventID
                }, null);

                if (prizesList != null && prizesList.Length > 0)
                {
                    foreach (var item in prizesList)
                    {
                        var entity = new PrizesEntity()
                        {
                            prizesID = item.PrizesID,
                            prizeName = item.PrizeName,
                            prizeDesc = item.PrizeDesc,
                            displayIndex = item.DisplayIndex.ToString(),
                            countTotal = item.CountTotal.ToString()
                        };

                        respData.content.prizes.Add(entity);
                    }
                }

                #endregion

                #region 中奖信息
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                var list = vipServer.GetLotteryVipList();
                if (list != null)
                {
                    foreach (var info in list)
                    {
                        getEventPrizesWinnerList winnerInfo = new getEventPrizesWinnerList();
                        winnerInfo.vipName = info.VipName;
                        winnerInfo.phone = info.Phone;
                        respData.content.winnerList.Add(winnerInfo);
                    }
                }
                #endregion

                #region 轮数 20131128
                respData.content.eventRound = "";
                #endregion
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                //respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getEventPrizesRespData : Default.LowerRespData
        {
            public getEventPrizesRespContentData content;
        }
        public class getEventPrizesRespContentData
        {
            public string isSite;       //是否在现场		1=是，0=否
            public string isLottery;    //是否抽奖 1= 是，0=否
            public string isWinning;    //是否中奖  1= 是，0=否
            public string winningDesc;  //奖品名称
            public string winningExplain;		//奖品说明 Jermyn20131017
            public string eventRound; //第几轮 （如果为0或者空，则代表不在抽奖范围）
            public IList<PrizesEntity> prizes;  //奖品集合
            public IList<getEventPrizesWinnerList> winnerList;

        }
        public class PrizesEntity
        {
            public string prizesID;     //奖品标识
            public string prizeName;    //奖品名称
            public string prizeDesc;    //奖品描述
            public string displayIndex; //排序
            public string countTotal;   //奖品数量
        }
        public class getEventPrizesWinnerList
        {
            public string vipName { get; set; }
            public string phone { get; set; }
        }
        public class getEventPrizesReqData : Default.ReqData
        {
            public getEventPrizesReqSpecialData special;
        }
        public class getEventPrizesReqSpecialData
        {
            public string EventId;      //活动标识
            public string Longitude;    //经度
            public string Latitude;     //纬度
        }
        #endregion

        #region o2omarketing
        #region 3.2.1 获取分析数据
        public string getMarketEventAnalysis()
        {
            string content = string.Empty;
            var respData = new getMarketEventAnalysisRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getMarketEventAnalysis: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getMarketEventAnalysisReqData>();
                reqObj = reqObj == null ? new getMarketEventAnalysisReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #region
                string EventId = string.Empty;
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.marketEventId == null || reqObj.special.marketEventId.Length == 0)
                {
                    //reqObj.special.marketEventId = "793150439CF94190A70CF2EC229A951D";

                    EventId = "793150439CF94190A70CF2EC229A951D"; // "8D41CDD7D5E4499195316E4645FCD7B9";
                }
                else
                {
                    EventId = reqObj.special.marketEventId.Trim();
                }
                if (reqObj.special.marketEventId == null)
                {
                    respData.code = "102";
                    respData.description = "marketEventId不能为空";
                    return respData.ToJSON().ToString();
                }
                string timestamp = string.Empty;
                if (reqObj.special.timestamp == null || reqObj.special.timestamp.Equals(""))
                {
                    timestamp = "0";
                }
                else
                {
                    timestamp = reqObj.special.timestamp;
                }
                #endregion

                #region 获取轮次 Jermyn20131212
                string roundId = ToStr(reqObj.special.roundId);
                if (roundId.Equals(""))
                {
                    LEventRoundBLL eventRoundServer = new LEventRoundBLL(loggingSessionInfo);
                    var roundList = eventRoundServer.QueryByEntity(new LEventRoundEntity
                    {
                        EventId = EventId
                        ,
                        Round = "1"
                        ,
                        IsDelete = 0
                    }, null);
                    if (roundList != null && roundList.Length > 0 && roundList[0] != null && roundList[0].RoundId != null)
                    {
                        roundId = roundList[0].RoundId;
                    }
                }
                #endregion


                respData.content = new getMarketEventAnalysisRespContentData();
                respData.content.prizeList = new List<getMarketEventAnalysisRespPrizeListData>();
                VwMarketEventAnalysisBLL server = new VwMarketEventAnalysisBLL(loggingSessionInfo);
                VwMarketEventAnalysisEntity vwInfo = new VwMarketEventAnalysisEntity();
                vwInfo = server.GetByID(reqObj.special.marketEventId);
                if (vwInfo != null && !vwInfo.MarketEventID.Equals(""))
                {
                    respData.content.storeCount = ToStr(vwInfo.StoreCount);
                    respData.content.responseStoreCount = ToStr(vwInfo.ResponseStoreCount);
                    respData.content.responseStoreRate = ToStr(vwInfo.ResponseStoreRate);
                    respData.content.personCount = ToStr(vwInfo.PersonCount);
                    respData.content.pesponsePersonCount = ToStr(vwInfo.PesponsePersonCount);
                    respData.content.responsePersonRate = ToStr(vwInfo.ResponseStoreRate);
                    respData.content.eventId = EventId;
                }
                else
                {
                    respData.content.eventId = EventId;
                }
                IList<getMarketEventAnalysisRespPrizeListData> List = new List<getMarketEventAnalysisRespPrizeListData>();
                LPrizesBLL serverPrizes = new LPrizesBLL(loggingSessionInfo);

                var prizesBrandList = serverPrizes.GetLPrizesGroupBrand(EventId, roundId);
                if (prizesBrandList != null && prizesBrandList.Count > 0)
                {
                    LPrizeWinnerBLL serverWinner = new LPrizeWinnerBLL(loggingSessionInfo);
                    respData.content.timestamp = ToStr(serverWinner.GetMaxTimestamp(EventId, roundId));
                    int i = 0;
                    foreach (var prizesBrandInfo in prizesBrandList)
                    {
                        getMarketEventAnalysisRespPrizeListData info = new getMarketEventAnalysisRespPrizeListData();
                        info.prizeBrand = ToStr(prizesBrandInfo.PrizeShortDesc);
                        info.prizeDesc = ToStr(prizesBrandInfo.PrizeShortDesc);
                        info.imageUrl = ToStr(prizesBrandInfo.LogoURL);
                        info.prizeWinnerList = new List<PrizeWinnerListRespData>();
                        info.id = ToStr(i + 1);
                        var winnerList = serverWinner.GetPrizesWinnerByGroupBrand(ToStr(prizesBrandInfo.PrizeName), EventId, Convert.ToInt64(timestamp), roundId);
                        if (winnerList != null)
                        {
                            foreach (var vipInfo in winnerList)
                            {
                                info.prizeWinnerList.Add(new PrizeWinnerListRespData()
                                {
                                    vipId = vipInfo.VIPID
                                    ,
                                    vipName = vipInfo.VipName
                                });
                            }
                        }
                        i++;
                        List.Add(info);
                    }
                }
                respData.content.prizeList = List;

                respData.content.roundList = new List<getMarketEventAnalysisRespRoundListData>();
                LEventRoundBLL lEventRoundBLL = new LEventRoundBLL(loggingSessionInfo);
                var eventRoundList = lEventRoundBLL.GetList(new LEventRoundEntity()
                {
                    EventId = reqObj.special.marketEventId
                }, 1, 1000);
                if (eventRoundList != null && eventRoundList.Count > 0)
                {
                    foreach (var eventRoundItem in eventRoundList)
                    {
                        getMarketEventAnalysisRespRoundListData info = new getMarketEventAnalysisRespRoundListData();
                        info.roundId = ToStr(eventRoundItem.RoundId);
                        info.roundDesc = ToStr(eventRoundItem.RoundDesc);
                        info.roundStatus = ToInt(eventRoundItem.RoundStatus);
                        info.round = ToStr(eventRoundItem.Round);
                        info.prizesCount = ToInt(eventRoundItem.PrizesCount);
                        info.winnerCount = ToInt(eventRoundItem.WinnerCount);
                        respData.content.roundList.Add(info);
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getMarketEventAnalysisRespData : Default.LowerRespData
        {
            public getMarketEventAnalysisRespContentData content { get; set; }
        }
        public class getMarketEventAnalysisRespContentData
        {
            public string storeCount { get; set; }  //参与门店数量
            public string responseStoreCount { get; set; }  //响应门店数量
            public string responseStoreRate { get; set; }   //门店响应率
            public string personCount { get; set; }         //邀约人数
            public string pesponsePersonCount { get; set; } //响应人数
            public string responsePersonRate { get; set; }  //会员响应率
            public string eventId { get; set; }  //活动标识
            public string timestamp { get; set; }           //时间戳
            public IList<getMarketEventAnalysisRespPrizeListData> prizeList { get; set; } //中奖名单集合
            public IList<getMarketEventAnalysisRespRoundListData> roundList { get; set; } //轮次集合
        }
        public class getMarketEventAnalysisRespPrizeListData
        {
            public string prizeBrand { get; set; }  //奖品名称
            public string prizeDesc { get; set; }   //奖品描述
            public string imageUrl { get; set; }    //图片链接
            public string id { get; set; }
            public IList<PrizeWinnerListRespData> prizeWinnerList { get; set; } //获奖名单
        }
        public class getMarketEventAnalysisRespRoundListData
        {
            public string roundId { get; set; }  //轮次标识
            public string roundDesc { get; set; }   //轮次说明
            public int roundStatus { get; set; }    //轮次状态
            public string round { get; set; } // 轮次
            public int prizesCount { get; set; } //奖品数量
            public int winnerCount { get; set; } //已中奖数量
        }
        public class PrizeWinnerListRespData
        {
            public string vipId { get; set; }
            public string vipName { get; set; }
        }
        public class getMarketEventAnalysisReqData : ReqData
        {
            public getMarketEventAnalysisReqSpecialData special;
        }
        public class getMarketEventAnalysisReqSpecialData
        {
            public string marketEventId { get; set; }
            public string timestamp { get; set; }
            public string roundId { get; set; }
        }
        #endregion

        #region 设置活动轮次刮奖状态
        public string setEventRountStatus()
        {
            string content = string.Empty;
            var respData = new setEventRountStatusRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setEventRountStatus: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setEventRountStatusReqData>();
                reqObj = reqObj == null ? new setEventRountStatusReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new setEventRountStatusReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.eventId == null || reqObj.special.eventId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "eventId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.roundId == null || reqObj.special.roundId.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "roundId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.roundStatus == null)
                {
                    respData.code = "2203";
                    respData.description = "状态不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new setEventRountStatusRespContentData();
                LEventRoundBLL service = new LEventRoundBLL(loggingSessionInfo);
                service.Update(new LEventRoundEntity()
                {
                    EventId = reqObj.special.eventId,
                    RoundId = reqObj.special.roundId,
                    RoundStatus = reqObj.special.roundStatus
                }, false);

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setEventRountStatus content: {0}", content)
            });
            return content;
        }

        public class setEventRountStatusRespData : Default.LowerRespData
        {
            public setEventRountStatusRespContentData content { get; set; }
        }
        public class setEventRountStatusRespContentData
        {

        }
        public class setEventRountStatusReqData : ReqData
        {
            public setEventRountStatusReqSpecialData special;
        }
        public class setEventRountStatusReqSpecialData
        {
            public string eventId { get; set; }
            public string roundId { get; set; }
            public int roundStatus { get; set; }
        }
        #endregion

        #region 3.2.2.提交联系方式
        public string setContact()
        {
            string content = string.Empty;
            var respData = new setItemKeepRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setContact: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setContactReqData>();
                #region check
                reqObj = reqObj == null ? new setContactReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setContactReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                ContactWeBLL contactServer = new ContactWeBLL(loggingSessionInfo);
                ContactWeEntity contactInfo = new ContactWeEntity();
                contactInfo.ContactId = BaseService.NewGuidPub();
                contactInfo.Company = ToStr(reqObj.special.company);
                contactInfo.UserName = ToStr(reqObj.special.userName);
                contactInfo.Email = ToStr(reqObj.special.email);
                contactInfo.Industry = ToStr(reqObj.special.industry);
                contactInfo.Phone = ToStr(reqObj.special.phone);
                contactInfo.Tel = ToStr(reqObj.special.tel);
                contactServer.Create(contactInfo);


                // send mail
                var mailto = "cs@o2omarketing.cn";
                var mailsubject = "微讯网上客户";
                var mailBody = "<div>微讯网上客户</div><br/>";
                mailBody += string.Format("<div>用户名：{0} </div><br/> ", contactInfo.UserName);
                mailBody += string.Format("<div>公司名：{0} </div><br/> ", contactInfo.Company);
                mailBody += string.Format("<div>电话：{0} </div><br/> ", contactInfo.Tel);
                mailBody += string.Format("<div>手机：{0} </div><br/> ", contactInfo.Phone);
                mailBody += string.Format("<div>邮件：{0} </div><br/> ", contactInfo.Email);
                mailBody += string.Format("<div>行业：{0} </div><br/> ", contactInfo.Industry);
                var success = JIT.Utility.Notification.Mail.SendMail(mailto, mailsubject, mailBody);
                //if (success)
                //{
                //    contactInfo.IsPushEmail = 1;
                //}
                //else
                //{
                //    contactInfo.IsPushEmail = 0;
                //    contactInfo.PushEmailFailure = "发送失败";
                //}
                //contactServer.Update(contactInfo, false);

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region
        public class setContactRespData : Default.LowerRespData
        {

        }

        public class setContactReqData : ReqData
        {
            public setContactReqSpecialData special;
        }
        public class setContactReqSpecialData
        {
            public string userName { get; set; } //		用户名
            public string company { get; set; } //		公司名
            public string tel { get; set; } //				电话
            public string email { get; set; } //			邮件
            public string phone { get; set; } //			电话
            public string industry { get; set; } //			行业
        }
        #endregion
        #endregion

        #region 3.2.3 获取门店的销售数据（在线）
        public string getOnlinePosOrder()
        {
            string content = string.Empty;
            var respData = new getOnlinePosOrderRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getOnlinePosOrder: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getOnlinePosOrderReqData>();
                reqObj = reqObj == null ? new getOnlinePosOrderReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.storeId == null || reqObj.special.storeId.Equals(""))
                {
                    respData.code = "102";
                    respData.description = "没有门店信息";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getOnlinePosOrderRespContentData();
                respData.content.orderList = new List<getOnlinePosOrderRespVipShowData>();
                string storeId = ToStr(reqObj.special.storeId);
                string status = ToStr(reqObj.special.status);
                IList<VwVipPosOrderEntity> list = new List<VwVipPosOrderEntity>();
                VwVipPosOrderBLL server = new VwVipPosOrderBLL(loggingSessionInfo);
                list = server.GetPosOrderList(storeId, status);
                if (list != null && list.Count > 0)
                {
                    foreach (var info in list)
                    {
                        getOnlinePosOrderRespVipShowData rInfo = new getOnlinePosOrderRespVipShowData();
                        rInfo.orderId = ToStr(info.OrderId);
                        rInfo.orderNo = ToStr(info.OrderNo);
                        rInfo.createTime = ToStr(info.CreateTime);
                        rInfo.deliveryName = ToStr(info.DeliveryName);
                        rInfo.address = ToStr(info.DeliveryAddress);
                        rInfo.vipName = ToStr(info.VipName);
                        rInfo.totalAmount = ToStr(info.TotalAmount);
                        rInfo.deliveryName = ToStr(info.DeliveryName);
                        rInfo.itemCount = ToStr(info.TotalQty);
                        rInfo.phone = ToStr(info.Phone);
                        respData.content.orderList.Add(rInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getOnlinePosOrderRespData : Default.LowerRespData
        {
            public getOnlinePosOrderRespContentData content { get; set; }
        }
        public class getOnlinePosOrderRespContentData
        {
            //public string isNext { get; set; }
            public IList<getOnlinePosOrderRespVipShowData> orderList { get; set; }
        }
        public class getOnlinePosOrderRespVipShowData
        {
            public string orderId { get; set; }
            public string orderNo { get; set; }         //订单编号
            public string itemCount { get; set; }       //商品数量
            public string createTime { get; set; }      //交易时间
            public string phone { get; set; }           //手机号
            public string totalAmount { get; set; }     //交易金额
            public string vipName { get; set; }
            public string deliveryName { get; set; }    //配送方式
            public string address { get; set; }         //地址

        }

        public class getOnlinePosOrderReqData : ReqData
        {
            public getOnlinePosOrderReqSpecialData special;
        }
        public class getOnlinePosOrderReqSpecialData
        {
            public string storeId { get; set; }     //门店标识
            public string status { get; set; }         //订单状态
        }
        #endregion

        #region 3.2.4 提交未处理的订单完成操作
        public string setOnlinePosOrderStatus()
        {
            string content = string.Empty;
            var respData = new setOnlinePosOrderStatusRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOnlinePosOrderStatus: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOnlinePosOrderStatusReqData>();
                reqObj = reqObj == null ? new setOnlinePosOrderStatusReqData() : reqObj;

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.orderId == null || reqObj.special.orderId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }

                #endregion

                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                #region 设置参数
                InoutService service = new InoutService(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.OrderId = reqObj.special.orderId;
                orderInfo.LastUpdateBy = ToStr("caf31108ddf245bc969ac2d173637c52");
                orderInfo.PaymentTime = System.DateTime.Now;
                orderInfo.Status = "3";
                orderInfo.StatusDesc = "已处理";
                #endregion
                string strError = string.Empty;
                bool bReturn = service.SetOrderPayment(orderInfo, out strError);
                if (!bReturn)
                {
                    respData.code = "111";
                    respData.exception = strError;
                }


            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        #region 参数对象
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setOnlinePosOrderStatusRespData : Default.LowerRespData
        {

        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setOnlinePosOrderStatusReqData : ReqData
        {
            public setOnlinePosOrderStatusReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOnlinePosOrderStatusReqSpecialData
        {
            public string orderId { get; set; }
        }
        #endregion
        #endregion

        #endregion

        #region Pad html5 接口 3.3 Pad版HTML5接口
        #region 3.3.1 获取二维码生成参数
        public string getDimensionalCode()
        {
            string content = string.Empty;
            var respData = new getDimensionalCodeRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getDimensionalCode: {0}", reqContent)
                });
                #region
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getDimensionalCodeReqData>();
                reqObj = reqObj == null ? new getDimensionalCodeReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.unitId == null || reqObj.special.unitId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "门店标识不能为空";
                    return respData.ToJSON().ToString();
                }

                #endregion
                respData.content = new getDimensionalCodeRespContentData();

                string imageUrl = string.Empty;
                Random ro = new Random();
                int iResult;
                int iUp = 10000000;
                int iDown = 5000000;
                iResult = ro.Next(iDown, iUp);

                #region 获取微信帐号
                WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(loggingSessionInfo);
                var wxObj = server.QueryByEntity(new WApplicationInterfaceEntity
                {
                    CustomerId = customerId
                    ,
                    IsDelete = 0
                }, null);
                if (wxObj == null || wxObj.Length == 0)
                {
                    respData.code = "2203";
                    respData.description = "不存在对应的微信帐号";
                    return respData.ToJSON().ToString();
                }
                else
                {
                    JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                    imageUrl = commonServer.GetQrcodeUrl(ToStr(wxObj[0].AppID)
                                                            , ToStr(wxObj[0].AppSecret)
                                                            , ToStr("0")
                                                            , iResult, loggingSessionInfo);
                    if (imageUrl != null && !imageUrl.Equals(""))
                    {
                        CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                        string downloadImageUrl = ConfigurationManager.AppSettings["website_WWW"];
                        imageUrl = downloadServer.DownloadFile(imageUrl, downloadImageUrl);
                    }
                }
                #endregion

                #region 创建临时匹配表
                VipDCodeBLL vipDCodeServer = new VipDCodeBLL(loggingSessionInfo);
                VipDCodeEntity info = new VipDCodeEntity();
                info.DCodeId = iResult.ToString();
                info.CustomerId = customerId;
                info.UnitId = ToStr(reqObj.special.unitId);
                info.Status = "0";
                info.ImageUrl = imageUrl;
                vipDCodeServer.Create(info);
                #endregion

                respData.content.imageUrl = imageUrl;
                respData.content.paraTmp = iResult.ToString();
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }

        public class getDimensionalCodeRespData : Default.LowerRespData
        {
            public getDimensionalCodeRespContentData content { get; set; }
        }
        public class getDimensionalCodeRespContentData
        {
            public string imageUrl { get; set; }
            public string paraTmp { get; set; }
        }
        public class getDimensionalCodeReqData : ReqData
        {
            public getDimensionalCodeReqSpecialData special;
        }
        public class getDimensionalCodeReqSpecialData
        {
            public string unitId { get; set; }
        }
        #endregion

        #region 3.3.2 根据参数获取用户信息
        public string getPoll()
        {
            string content = string.Empty;
            var respData = new getPollRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getPoll: {0}", reqContent)
                });
                #region
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getPollReqData>();
                reqObj = reqObj == null ? new getPollReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.unitId == null || reqObj.special.unitId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "门店标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.paraTmp == null || reqObj.special.paraTmp.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "paraTmp不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion
                respData.content = new getPollRespContentData();


                #region 创建临时匹配表
                VipDCodeBLL vipDCodeServer = new VipDCodeBLL(loggingSessionInfo);
                VipDCodeEntity info = new VipDCodeEntity();
                info = vipDCodeServer.GetByID(ToStr(reqObj.special.paraTmp));
                string status = string.Empty;
                string vipId = string.Empty;
                string openId = string.Empty;
                if (info == null || info.DCodeId == null)
                {
                    respData.code = "2201";
                    respData.description = "不存在对应的记录";
                    return respData.ToJSON().ToString();
                }
                else
                {
                    status = info.Status;
                    openId = info.OpenId;
                    VipBLL vipServer = new VipBLL(loggingSessionInfo);
                    var vipObj = vipServer.QueryByEntity(new VipEntity
                    {
                        WeiXinUserId = info.OpenId
                        ,
                        IsDelete = 0
                    }, null);
                    if (vipObj == null && vipObj.Length == 0)
                    {

                    }
                    else
                    {
                        vipId = ToStr(vipObj[0].VIPID);
                    }
                }
                respData.content.status = status;
                respData.content.userId = vipId;
                respData.content.openId = openId;
                #endregion

            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("setSignUp content: {0}", content)
            });
            return content;
        }

        public class getPollRespData : Default.LowerRespData
        {
            public getPollRespContentData content { get; set; }
        }
        public class getPollRespContentData
        {
            public string status { get; set; }
            public string userId { get; set; }
            public string openId { get; set; }
        }
        public class getPollReqData : ReqData
        {
            public getPollReqSpecialData special;
        }
        public class getPollReqSpecialData
        {
            public string unitId { get; set; }
            public string paraTmp { get; set; }
        }
        #endregion

        #endregion

        #region 2.2.21 获取当前的用户客户端信息 getClientUserInfo
        public string getClientUserInfo()
        {
            string content = string.Empty;
            var respData = new getClientUserInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getClientUserInfo: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getClientUserInfoReqData>();

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.vipId == null || reqObj.special.vipId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "需要先获取Auth认证的vipId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.openId == null || reqObj.special.openId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "需要先获取Auth认证的openId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                respData.content = new getClientUserInfoRespContentData();
                //查询参数
                string userId = reqObj.common.userId;
                VipBLL vipServer = new VipBLL(loggingSessionInfo);
                bool bReturn = vipServer.SetMergerVipInfo(reqObj.common.userId.Trim()
                                                        , reqObj.special.vipId.Trim()
                                                        , reqObj.special.openId.Trim());
                if (!bReturn)
                {
                    respData.code = "103";
                    respData.description = "处理过程有错，请联系管理员.";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        /// <summary>
        /// 返回对象
        /// </summary>
        public class getClientUserInfoRespData : Default.LowerRespData
        {
            public getClientUserInfoRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class getClientUserInfoRespContentData
        {
            //public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class getClientUserInfoReqData : ReqData
        {
            public getClientUserInfoReqSpecialData special;
        }
        public class getClientUserInfoReqSpecialData
        {
            public string vipId { get; set; }
            public string openId { get; set; }
        }


        #endregion
        #endregion

        #region Jermyn20131209
        public string setToBase64String()
        {
            string content = string.Empty;
            var respData = new setToBase64StringRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getClientUserInfo: {0}", reqContent)
                });

                #region
                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setToBase64StringReqData>();

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.userId == null || reqObj.special.userId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "需要先获取Auth认证的userId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.openId == null || reqObj.special.openId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "需要先获取Auth认证的openId不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.eventId == null || reqObj.special.eventId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "需要先获取Auth认证的userId不能为空";
                    return respData.ToJSON().ToString();
                }

                if (!string.IsNullOrEmpty(reqObj.special.customerId))
                {
                    customerId = reqObj.special.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion

                respData.content = new setToBase64StringRespContentData();
                string strState = reqObj.special.customerId + "," + reqObj.special.applicationId + "," + reqObj.special.userId + "," + reqObj.special.openId + "," + reqObj.special.eventId + "," + reqObj.special.goUrl; //
                strState = HttpUtility.UrlEncode(strState, Encoding.UTF8);
                byte[] buff = Encoding.UTF8.GetBytes(strState);
                strState = Convert.ToBase64String(buff);

                WApplicationInterfaceBLL server = new WApplicationInterfaceBLL(loggingSessionInfo);
                WApplicationInterfaceEntity info = server.GetByID(reqObj.special.applicationId);
                if (info == null)
                {

                }
                else
                {
                    respData.content.appId = info.AppID;
                }
                respData.content.state = strState;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }
        #region
        /// <summary>
        /// 返回对象
        /// </summary>
        public class setToBase64StringRespData : Default.LowerRespData
        {
            public setToBase64StringRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setToBase64StringRespContentData
        {
            public string state { get; set; }
            public string appId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setToBase64StringReqData : ReqData
        {
            public setToBase64StringReqSpecialData special;
        }
        public class setToBase64StringReqSpecialData
        {
            public string customerId { get; set; }
            public string openId { get; set; }
            public string eventId { get; set; }
            public string userId { get; set; }
            public string applicationId { get; set; }
            public string goUrl { get; set; }
        }


        #endregion
        #endregion

        #region 商品认证
        /// <summary>
        /// 商品认证
        /// 
        /// 返回结果
        /// 1.	已验证过
        /// 2.	验证成功
        /// 3.	验证失败
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userId"></param>
        /// <param name="authCode"></param>
        /// <param name="captchaCode"></param>
        /// <returns></returns>
        public string CheckAuthCode(LoggingSessionInfo loggingSessionInfo, string userId, string authCode, string captchaCode)
        {
            string result = "";
            LItemAuthBLL lItemAuthBLL = new LItemAuthBLL(loggingSessionInfo);
            var itemAuthList = lItemAuthBLL.QueryByEntity(new LItemAuthEntity()
            {
                AuthCode = authCode
            }, null);
            LItemAuthEntity itemAuthObj = null;
            if (itemAuthList != null && itemAuthList.Length > 0)
            {
                itemAuthObj = itemAuthList[0];
                itemAuthObj.AuthCount += 1;
                lItemAuthBLL.Update(itemAuthObj, false);
                result = "1";
                return result;
            }
            else
            {
                SaturnServiceReference.SaturnServiceSoapClient client = new SaturnServiceReference.SaturnServiceSoapClient();
                var resultStr = client.getAuthenticationCode("{\"common\":{\"userId\":\"" + userId + "\",\"version\":\"3.1\"},\"special\":{\"authCode\":\"" + authCode + "\",\"captchaCode\":\"" + captchaCode + "\"}}");
                var authObj = resultStr.DeserializeJSONTo<CheckAuthCodeResp>();
                if (authObj.code == "200")
                {
                    if (authObj.content.isAuthCode == "1")
                    {
                        result = "2";
                    }
                    else
                    {
                        result = "3";
                    }
                    var newItemAuthObj = new LItemAuthEntity();
                    newItemAuthObj.ItemAuthId = Utils.NewGuid();
                    newItemAuthObj.AuthCode = authCode;
                    newItemAuthObj.CaptchaCode = captchaCode;
                    newItemAuthObj.ItemName = authObj.content.itemName;
                    newItemAuthObj.Norm = authObj.content.norm;
                    newItemAuthObj.Alcohol = authObj.content.alcohol;
                    newItemAuthObj.BaseWineYear = authObj.content.baseWineYear;
                    newItemAuthObj.AgePitPits = authObj.content.agePitPits;
                    newItemAuthObj.Barcode = authObj.content.barcode;
                    newItemAuthObj.IsAuthCode = ToInt(authObj.content.isAuthCode);
                    newItemAuthObj.CategoryName = authObj.content.categoryName;
                    newItemAuthObj.CategoryId = authObj.content.categoryId;
                    newItemAuthObj.BrandName = authObj.content.brandName;
                    newItemAuthObj.DealerName = authObj.content.dealerName;
                    newItemAuthObj.DealerId = authObj.content.dealerId;
                    newItemAuthObj.AuthCount = 1;
                    newItemAuthObj.StoreCode = authObj.content.storeCode;
                    lItemAuthBLL.Create(newItemAuthObj);
                }
                else
                {
                    result = authObj.description;
                }
            }
            return result;
        }
        public class CheckAuthCodeResp
        {
            public string LItemAuthEntity;
            public string code;
            public string description;
            public CheckAuthCodeRespContent content;
        }
        public class CheckAuthCodeRespContent
        {
            public string itemName;
            public string norm;
            public string alcohol;
            public string baseWineYear;
            public string agePitPits;
            public string barcode;
            public string isAuthCode;
            public string categoryName;
            public string categoryId;
            public string brandName;
            public string dealerName;
            public string dealerId;
            public string storeCode;
            public CheckAuthCodeRespContentImage imageList;
        }
        public class CheckAuthCodeRespContentImage
        {
            public string imageURL;
            public string displayIndex;
        }
        #endregion

        #region 获取积分规则
        private string GetIntegralRule()
        {
            string content = string.Empty;
            var respData = new getIntegralRuleListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getIntegralRuleListReqData>();
                reqObj = reqObj == null ? new getIntegralRuleListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getIntegralRuleListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getIntegralRuleListRespContentData();
                IntegralRuleBLL service = new IntegralRuleBLL(loggingSessionInfo);
                IntegralRuleEntity queryEntity = new IntegralRuleEntity();
                queryEntity.CustomerId = customerId;

                int totalCount = service.GetListCount(queryEntity);
                IList<IntegralRuleEntity> list = service.GetList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.IntegralRuleList = new List<getIntegralRuleListRespIntegralRuleData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getIntegralRuleListRespIntegralRuleData()
                        {
                            IntegralRuleId = ToStr(item.IntegralRuleID),
                            IntegralSourceID = ToStr(item.IntegralSourceID),
                            IntegralDesc = ToStr(item.IntegralDesc),
                            Integral = ToInt(item.Integral),
                            createTime = ToStr(Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd")),
                            IntegralSourceName = ToStr(item.IntegralSourceName),
                        };

                        respData.content.IntegralRuleList.Add(tmpItemObj);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getIntegralRuleListRespData : Default.LowerRespData
        {
            public getIntegralRuleListRespContentData content { get; set; }
        }
        public class getIntegralRuleListRespContentData
        {
            public string isNext { get; set; }
            public IList<getIntegralRuleListRespIntegralRuleData> IntegralRuleList { get; set; }
        }
        public class getIntegralRuleListRespIntegralRuleData
        {
            public string IntegralRuleId { get; set; }
            public string IntegralSourceID { get; set; }
            public string IntegralSourceName { get; set; }
            public int Integral { get; set; }
            public string createTime { get; set; }
            public string IntegralDesc { get; set; }
        }

        public class getIntegralRuleListReqData : ReqData
        {
            public getIntegralRuleListReqSpecialData special;
        }
        public class getIntegralRuleListReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion

        #region 获取积分明细
        private string GetVipIntegralDetail()
        {
            string content = string.Empty;
            var respData = new getVipIntegralDetailListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipIntegralDetailList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipIntegralDetailListReqData>();
                reqObj = reqObj == null ? new getVipIntegralDetailListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getVipIntegralDetailListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getVipIntegralDetailListRespContentData();
                VipIntegralDetailBLL service = new VipIntegralDetailBLL(loggingSessionInfo);
                VipIntegralDetailEntity queryEntity = new VipIntegralDetailEntity();
                queryEntity.VIPID = reqObj.common.userId;


                int totalCount = service.GetVipIntegralDetailListCount(queryEntity);
                IList<VipIntegralDetailEntity> list = service.GetVipIntegralDetailList(queryEntity,
                    reqObj.special.page, reqObj.special.pageSize);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.VipIntegralDetailList = new List<getVipIntegralDetailListRespVipIntegralDetailData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getVipIntegralDetailListRespVipIntegralDetailData()
                        {
                            VipIntegralDetailId = ToStr(item.VipIntegralDetailID),
                            VipIntegralSourceID = ToStr(item.IntegralSourceID),
                            Integral = ToInt(item.Integral),
                            createTime = ToStr(Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd")),
                            VipIntegralSourceName = ToStr(item.IntegralSourceName),
                        };

                        respData.content.VipIntegralDetailList.Add(tmpItemObj);
                    }
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class getVipIntegralDetailListRespData : Default.LowerRespData
        {
            public getVipIntegralDetailListRespContentData content { get; set; }
        }
        public class getVipIntegralDetailListRespContentData
        {
            public string isNext { get; set; }
            public IList<getVipIntegralDetailListRespVipIntegralDetailData> VipIntegralDetailList { get; set; }
        }
        public class getVipIntegralDetailListRespVipIntegralDetailData
        {
            public string VipIntegralDetailId { get; set; }
            public string VipIntegralSourceID { get; set; }
            public string VipIntegralSourceName { get; set; }
            public int Integral { get; set; }
            public string createTime { get; set; }
        }

        public class getVipIntegralDetailListReqData : ReqData
        {
            public getVipIntegralDetailListReqSpecialData special;
        }
        public class getVipIntegralDetailListReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }
        }
        #endregion
    }

    #region ReqData

    public class ReqData
    {
        public ReqCommonData common;
    }
    public class ReqCommonData
    {
        public string locale;
        public string userId;
        public string openId;
        public string signUpId;
        public string customerId;
    }

    #endregion


}