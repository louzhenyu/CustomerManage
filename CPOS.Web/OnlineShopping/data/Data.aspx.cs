

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using JIT.CPOS.BLL;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.Eliya;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.Utility.Notification;
using JIT.Utility.Web;
using ThoughtWorks.QRCode.Codec;
using JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item;
using JIT.Utility.ExtensionMethod;
using System.Net;
using System.IO;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    public partial class Data : System.Web.UI.Page
    {
        private string customerId = "29E11BDC6DAC439896958CC6866FF64E";
        private string customerId_Lj = "e703dbedadd943abacf864531decdac1";




        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;

            try
            {
                string dataType = Request["action"].ToString().Trim();
                //测试
                //var dataType = "getADList";
                //
                JIT.CPOS.Web.Module.Log.InterfaceWebLog.Logger.Log(Context, Request, dataType);
                switch (dataType)
                {
                    case "GetChildCategories"://获取全部商品分类
                        content = new Item().GetItemCategoriesByParentId(HttpContext.Current.Request["ReqContent"]);
                        break;
                    case "GetItemCategoryList"://根据parentid 获取 商品子分类
                        content = new Item().GetItemCategories(HttpContext.Current.Request["ReqContent"]);
                        break;
                    case "getItemList": //1.获取商品列表
                        content = new DataOnlineShoppingHandler().GetItemList();
                        break;
                    case "getVipValidIntegral": //1.获取商品列表
                        content = new DataOnlineShoppingHandler().GetVipValidIntegral();
                        break;
                    case "getOrderIntegral": //我的兑换记录
                        content = new DataOnlineShoppingHandler().GetOrderIntegral();
                        break;
                    case "getProvince": //获取省份集合
                        content = new DataOnlineShoppingHandler().GetProvince();
                        break;
                    case "getCityByProvince": //根据省份名称获取城市数据集合
                        content = new DataOnlineShoppingHandler().GetCityByProvince();
                        break;
                    case "getStoreArea": //花间堂_获取门店区域属性信息
                        content = new DataOnlineShoppingHandler().GetStoreArea();
                        break;
                    case "getStoreListByCityName": //花间堂 根据城市获取酒店列表    ---ok
                        content = new DataOnlineShoppingHandler().GetStoreListByCityName();
                        break;
                    case "getStoreDetailByStoreID": //花间堂 根据酒店标识获取具体房间的信息  
                        content = new DataOnlineShoppingHandler().GetStoreDetailByStoreID();
                        break;
                    case "sendCode": //华硕校园_发送验证码
                        content = new DataOnlineShoppingHandler().HS_SendCode();
                        break;
                    case "getProvinceList": //华硕校园_根据校园大使获取省份列表
                        content = new DataOnlineShoppingHandler().GetProvinceOfHS();
                        break;
                    case "getCityByProvinceOfHS": //华硕校园__根据省份名称获取城市列表
                        content = new DataOnlineShoppingHandler().GetCityByProvinceOfHS();
                        break;
                    case "getSchoolList": //华硕校园_根据城市名称获取学校列表
                        content = new DataOnlineShoppingHandler().GetSchoolListByCityNameOfHS();
                        break;
                    case "setVip": //华硕校园_完善资料
                        content = new DataOnlineShoppingHandler().CreateVip();
                        break;
                    case "joinGroupon": //华硕校园_团购报名
                        content = new DataOnlineShoppingHandler().joinGroupon();
                        break;
                    case "getWEventByUserID":
                        content = new DataOnlineShoppingHandler().getWEventByUserID();
                        break;
                    case "HS_GetVipDetail":
                        content = new DataOnlineShoppingHandler().HS_GetVipDetail();
                        break;
                    case "cancelOrders":
                        content = new DataOnlineShoppingHandler().CancelOrders();
                        break;
                    case "sendMail":
                        content = new DataOnlineShoppingHandler().SendMail();
                        break;
                    //case "sendMailCancel":
                    //    content = new DataOnlineShoppingHandler().SendMailCancel();
                    //    break;
                    case "getItemDetailForHotel":
                        content = new DataOnlineShoppingHandler().GetItemDetailForHotel();//这个用到了吗？
                        break;
                    case "getStoreListByArea":
                        content = new DataOnlineShoppingHandler().GetStoreListByArea();
                        break;
                    case "getItemTypeList": //2.获取福利商品类别集合
                        content = getItemTypeList();
                        break;
                    case "getItemDetail": //4.获取福利商品明细信息
                        content = new DataOnlineShoppingHandler().GetItemDetail();
                        break;
                    case "getHotelItemDetail":
                        content = new DataOnlineShoppingHandler().getHotelItemDetail();//花间堂房价信息
                        break;
                    case "getSkuProp2List": //20.获取商品属性2的集合
                        content = getSkuProp2List();
                        break;
                    case "getStoreListByItem":
                        content = getStoreListByItem(); //5.获取福利商品的门店集合 Jermyn
                        break;
                    case "getStoreDetail": // 6.获取门店详细信息 Jermyn
                        content = getStoreDetail();
                        break;
                    case "getBrandDetail":
                        content = getBrandDetail(); //7.获取品牌详细信息 Jermyn
                        break;
                    case "setDownloadItem": //8.立即下载提交 setDownloadItem Jermyn
                        content = setDownloadItem();
                        break;
                    case "getDownloadUsersByItem": //9.获取商品的下载用户集合  Jermyn
                        content = getDownloadUsersByItem();
                        break;
                    case "setOrderInfo":
                        content = new DataOnlineShoppingHandler().SetOrderInfo(); //10 提交订单 Jermyn
                        break;
                    case "setOrderInfo4ALD":
                        content = new DataOnlineShoppingHandler().SetOrderInfo4ALD();
                        break;
                    case "isOrderPayed4ALD":
                        content = new DataOnlineShoppingHandler().IsOrderPayed4ALD();
                        break;
                    case "setOrderIntegralInfo":
                        content = new DataOnlineShoppingHandler().SetOrderIntegralInfo(); //提交积分订单
                        break;
                    case "orderPay":
                        content = new DataOnlineShoppingHandler().OrderPay(); //提交积分订单
                        break;
                    case "orderPay4ALD":
                        content = new DataOnlineShoppingHandler().OrderPay4ALD(); //提交订单4 ALD
                        break;
                    case "setOrderAddress":
                        content = new DataOnlineShoppingHandler().SetOrderAddress(); //修改订单地址信息
                        break;
                    case "setOrderInfoOne":
                        content = setOrderInfo(); //10 提交订单 Jermyn 20131023打开，单个商品提交
                        break;
                    case "setOrderInfoTwo": //10 提交订单 Jermyn 20131225开发，多个商品重复提交订单
                        content = setOrderInfoTwo();
                        break;
                    case "setUpdateOrderDelivery": //20131024 处理订单配送信息修改
                        content = setUpdateOrderDelivery();
                        break;
                    case "setOrderPayment": //职改订单支付信息
                        content = new DataOnlineShoppingHandler().SetOrderPayment();
                        break;
                    case "getPaymentTypeList":
                        content = getPaymentTypeList(); //12 获取支付方式的集合 Jermyn
                        break;
                    case "getDeliveryList": //13.配送方式 Jermyn
                        content = getDeliveryList();
                        break;
                    case "getOrderInfo": //14.获取订单详细信息 Jermyn
                        content = new DataOnlineShoppingHandler().GetOrderInfo();
                        break;
                    case "setItemKeep": //15.设置福利订单收藏与取消收藏 Jermyn
                        content = setItemKeep();
                        break;
                    case "getVipDetail": //16.获取VIP用户的详细信息 Jermyn
                        content = getVipDetail();
                        break;
                    case "setVipDetail": //17.设置VIP用户的详细信息 Jermyn
                        content = setVipDetail();
                        break;
                    case "getEmbaVipList": //获取VIP用户集合
                        content = getEmbaVipList();
                        break;
                    case "getCustomerDetail": //获取在线商城的基本设置信息
                        content = getCustomerDetail();
                        break;
                    case "GetDeliveryAmount"://根据custoerID和总金额取得运费
                        content = new DataOnlineShoppingHandler().GetDeliveryAmount();
                        break;
                    #region 泸州老窖达人秀接口

                    case "getEventsEntriesList": //1.获取当天的参赛作品
                        content = getEventsEntriesList();
                        break;
                    case "setEventSignUp": //2.报名提交（评论报名）
                        content = setEventSignUp();
                        break;
                    case "setEventsEntriesPraise": //3.赞提交（对作品赞）
                        content = setEventsEntriesPraise();
                        break;
                    case "setEventsEntriesComment": //4.评论提交（对作品评论）
                        content = setEventsEntriesComment();
                        break;
                    case "getEventsEntriesCommentList": //5.获取参赛作品的评论与赞
                        content = getEventsEntriesCommentList();
                        break;
                    case "getEventsEntriesWinners": //6.获取获奖名单
                        content = getEventsEntriesWinners();
                        break;
                    case "getEventsEntriesMonthDaren": //7.获取品味达人作品集
                        content = getEventsEntriesMonthDaren();
                        break;

                    #endregion

                    #region 中欧接口

                    case "getForumEntriesList": //获取大型论坛或者招生活动的标题集合 
                        content = getForumEntriesList();
                        break;
                    case "getForumDetail": //获取论坛或者招生活动的明细  
                        content = getForumDetail();
                        break;
                    case "getCourseDetail": //获取课程管理的详细信息   
                        content = getCourseDetail();
                        break;
                    case "setCourseApply": //课程报名提交   
                        content = setCourseApply();
                        break;
                    case "getHighLevelCourseList": //获取高级课程集合 
                        content = getHighLevelCourseList();
                        break;
                    case "getNewsListByCourseId": //获取课程新闻
                        content = getNewsListByCourseId();
                        break;
                    case "getNewsById": //获取新闻详细信息
                        content = getNewsById();
                        break;
                    case "getZONewsOrZKList":
                        content = getZONewsOrZKList();
                        break;
                    case "getZONewsOrZKDetail":
                        content = getZONewsOrZKDetail();
                        break;

                    #endregion

                    case "setBrowseHistory": //浏览历史提交   
                        content = setBrowseHistory();
                        break;
                    case "setShoppingCart": //提交购物车信息   单个商品
                        content = setShoppingCart();
                        break;
                    case "setShoppingCartList": //提交购物车信息   多个商品
                        content = setShoppingCartList();
                        break;
                    case "getShoppingCart": //获取购物车商品信息 
                        content = getShoppingCart();
                        break;
                    case "getBrowseHistory": //获取浏览历史商品信息 
                        content = getBrowseHistory();
                        break;
                    case "getItemKeep": //获取收藏商品信息 
                        content = new DataOnlineShoppingHandler().GetItemKeep();
                        break;
                    case "getOrderList": //获取各种状态的订单信息 
                        content = new DataOnlineShoppingHandler().GetOrderList();
                        break;
                    case "getOrderList4Blossom":
                        content = new DataOnlineShoppingHandler().getOrderList4Blossom();
                        break;
                    case "getVipAddressList":
                        content = new DataOnlineShoppingHandler().GetVIPAddressList();
                        break;
                    case "setVipAddress":
                        content = new DataOnlineShoppingHandler().SetVIPAddress();
                        break;
                    case "getItemExchangeList": //获取积分兑换商品信息集合 
                        content = getItemExchangeList();
                        break;
                    case "setOrderStatus": //订单状态修改 
                        content = setOrderStatus();
                        break;
                    case "setOrderStatus4ALD": //订单状态修改 
                        content = setOrderStatus4ALD();
                        break;
                    case "setSignUp":
                        content = setSignUp();
                        break;
                    case "setSignIn":
                        content = setSignIn();
                        break;
                    case "getCityList": //城市集合
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
                    case "getVipShowById": //获取单个会员秀的详细信息
                        content = getVipShowById();
                        break;
                    case "getHairStylistByStoreId": //根据门店获取发型师
                        content = getHairStylistByStoreId();
                        break;
                    case "setSMSPush": //设置密码推送
                        content = setSMSPush();
                        break;
                    case "setVipPassword": //设置密码修改
                        content = setVipPassword();
                        break;
                    case "setVipShow":
                        content = setVipShow();
                        break;
                    case "setIOSDeviceToken": //设置IOS的deviceToken
                        content = setIOSDeviceToken();
                        break;
                    case "setAndroidBasic": //设置Android的参数设置
                        content = setAndroidBasic();
                        break;
                    case "checkVersion": //APP版本更新
                        content = checkVersion();
                        break;
                    case "getNewsList": //发布会集合
                        content = getNewsList();
                        break;
                    case "getLotteryList": //个人抽奖信息集合
                        content = getLotteryList();
                        break;
                    case "setLottery": //抽奖日志
                        content = setLottery();
                        break;
                    case "getEventPrizes": //奖项
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
                    case "setContact": //3.2.2 提交联系方式
                        content = setContact();
                        break;
                    case "getOnlinePosOrder": //3.2.3 获取门店的销售数据（在线）
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
                    case "getPoll": //3.3.2 获取二维码生成参数
                        content = getPoll();
                        break;

                    #endregion

                    case "getClientUserInfo": //2.2.21获取客户端信息
                        content = getClientUserInfo();
                        break;
                    case "setToBase64String": //压缩
                        content = setToBase64String();
                        break;
                    case "getEventWinnerList": //2.9.13 活动中奖名单（Jermyn20131211）
                        content = getEventWinnerList();
                        break;

                    #region 优惠券

                    case "myCouponList": //3.4.1 用户优惠券查询接口  qianzhi20131214
                        content = myCouponList();
                        break;
                    case "orderCouponSum": //3.4.2 订单使用的优惠券总计查询接口  qianzhi20131214
                        content = orderCouponSum();
                        break;
                    case "orderCouponList": //3.4.3 订单使用的优惠券列表查询接口  qianzhi20131214
                        content = orderCouponList();
                        break;
                    case "cancelCoupon": //3.4.4 订单中取消使用优惠券接口  qianzhi20131214
                        content = cancelCoupon();
                        break;
                    case "selectCoupon": //3.4.5 订单中选择优惠券接口  qianzhi20131214
                        content = selectCoupon();
                        break;

                    #endregion

                    #region 抽奖

                    case "getEventPrizesBySales":
                        content = getEventPrizesBySales();
                        break;

                    #endregion

                    case "getUnitCommentList": //获取点评列表
                        content = getUnitCommentList();
                        break;
                    case "setSeatOrderInfo":
                        content = setSeatOrderInfo(); //提交订座
                        break;

                    #region 泸州老窖wap

                    case "getNewsListWeekly":
                        content = getNewsListWeekly();
                        break;
                    case "setSignUpLzlj":
                        content = setSignUpLzlj();
                        break;

                    #endregion

                    #region 复星

                    case "setSignUpFosun":
                        content = setSignUpFosun(); //注册
                        break;
                    case "GetSearchVipStaff":
                        content = GetSearchVipStaff(); //复星的员工查询
                        break;
                    case "SetVipStaff":
                        content = SetVipStaff(); //修改会员
                        break;
                    case "SetUserMessageData": //发送微信
                        content = SetUserMessageData();
                        break;
                    case "checkSign":
                        content = CheckSign(); //查询是否已注册、签到
                        break;
                    case "createQrcode":
                        content = createQrcode(); //生成二维码
                        break;
                    case "GetMobileList": //获取移动采招
                        content = GetMobileList();
                        break;

                    #endregion

                    case "getEventDetail":
                        content = getEventDetail();
                        break;
                    case "getEventApplyQues":  //获取活动的问卷
                        content = getEventApplyQues();
                        break;
                    case "submitEventApply":    //提交问卷
                        content = submitEventApply();
                        break;
                    case "getUserSignInEvent":
                        content = getUserSignInEvent();
                        break;
                    case "getEvents":
                        content = getEvents();
                        break;
                    case "setUserMessageDataWap":
                        content = SetUserMessageDataWap();
                        break;
                    case "getIntegralRuleList":
                        content = GetIntegralRule();
                        break;
                    case "getIntegralDetailList":
                        content = GetVipIntegralDetail();
                        break;
                    case "getADList": //获取首页广告信息
                        content = GetADList();
                        break;
                    case "getIndexCategoryList":
                        content = GetCategoryList();
                        break;
                    case "getCategoryList":
                        content = GetCategoryList();
                        break;
                    case "searchStores":
                        content = SearchStores();
                        break;
                    case "modifyPWD":
                        content = ModifyPWD();
                        break;
                    case "deleteShoppingCart":
                        content = DeleteShoppingCart();
                        break;
                    case "isOrderPaid":
                        content = IsOrderPaid();
                        break;
                    case "getShoppingCartCount":
                        content = GetShoppingCartCount();
                        break;
                    case "getDistrictList":
                        content = GetDistrictList();
                        break;
                    case "getRecentlyUsedStore":
                        content = GetRecentlyUsedStore();
                        break;
                    case "getOrderStatistics":
                        content = GetOrderStatistics();
                        break;
                    case "setOrderPaymentType":
                        content = SetOrderPaymentType();
                        break;
                    case "setEvaluation":
                        content = SetEvaluation();
                        break;
                    case "getEvaluationList":
                        content = GetEvaluationList();
                        break;
                    case "getDistrictsByDistricID":
                        content = GetDistrictsByDistricID();
                        break;

                    default:
                        throw new Exception("未定义的接口:" + dataType);
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
                string itemName = reqObj.special.itemName; //模糊查询商品名称
                string itemTypeId = reqObj.special.itemTypeId; //活动标识
                string isExchange = reqObj.special.isExchange;
                int page = reqObj.special.page; //页码
                int pageSize = reqObj.special.pageSize; //页面数量
                string storeId = ToStr(reqObj.special.storeId);
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 15;

                //初始化返回对象
                respData.content = new getItemListRespContentData();
                respData.content.itemList = new List<getItemListRespContentDataItem>();
                respData.content.ItemKeeps = new List<getItemListRespContentDataItem>();
                ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(loggingSessionInfo);
                respData.content.shoppingCartCount = shoppingCartServer.GetShoppingCartByVipId(userId);
                ItemService itemService = new ItemService(loggingSessionInfo);

                var dsItems = itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, false,
                    isExchange, storeId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.itemList =
                        DataTableToObject.ConvertToList<getItemListRespContentDataItem>(dsItems.Tables[0]);
                    var totalCount = itemService.GetWelfareItemListCount(userId, itemName, itemTypeId, false, isExchange,
                        storeId);
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

                var dsItemKeeps = itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, true,
                    isExchange, storeId);
                if (dsItemKeeps != null && dsItemKeeps.Tables.Count > 0 && dsItemKeeps.Tables[0].Rows.Count > 0)
                {
                    respData.content.ItemKeeps =
                        DataTableToObject.ConvertToList<getItemListRespContentDataItem>(dsItemKeeps.Tables[0]);
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
            public string isNext { get; set; } //是否有下一页
            public int shoppingCartCount { get; set; } //购物车数量
            public IList<getItemListRespContentDataItem> itemList { get; set; } //商品集合
            public IList<getItemListRespContentDataItem> ItemKeeps { get; set; } //已收藏的福利列表
        }

        public class getItemListRespContentDataItem
        {
            public string itemId { get; set; } //商品标识
            public string itemName { get; set; } //商品名称（譬如：浪漫主题房）
            /// <summary>
            /// 图片链接地址 update by Henry 2014-12-8
            /// </summary>
            private string imageurl;
            public string imageUrl
            {
                get { return ImagePathUtil.GetImagePathStr(this.imageurl, "240"); }  //请求图片缩略图 
                set { this.imageurl = value; }
            }
            public decimal price { get; set; } //商品原价
            public decimal salesPrice { get; set; } //商品零售价（优惠价）
            public decimal everyoneSalesPrice { get; set; }   //人人销售价
            public decimal discountRate { get; set; } //商品折扣
            public Int64 displayIndex { get; set; } //排序
            public string pTypeId { get; set; } //福利类别标识（团购=2，优惠=1）
            public string pTypeCode { get; set; } //福利类别缩写（券，团）
            public string CouponURL { get; set; } //优惠券下载地址
            public string integralExchange { get; set; } //积分兑换的数量
            public string IsExchange { get; set; } //是否需要积分兑换商品
            public string itemCategoryName { get; set; } //类别名称 Jermyn20131008
            public int salesPersonCount { get; set; } //已购买人数量
            public int isShoppingCart { get; set; } //是否已经加入购物车（1=已加入，0=未加入）
            public string skuId { get; set; }
            public string itemTypeDesc { get; set; } // 菜品特殊分类
            public string itemSortDesc { get; set; } // 菜品排行
            public int salesQty { get; set; } // 吃过人数
            public string createDate { get; set; } //创建日期
            public string remark { get; set; } //备注
        }

        public class getItemListReqData : ReqData
        {
            public getItemListReqSpecialData special;
        }

        public class getItemListReqSpecialData
        {
            public string itemName { get; set; } //模糊查询商品名称
            public string itemTypeId { get; set; } //商品类别标识
            public int page { get; set; } //页码
            public int pageSize { get; set; } //页面数量
            public string isExchange { get; set; } //兑换商品
            public string storeId { get; set; } //门店标识 Jermyn20131008
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
                    respData.content.itemTypeList =
                        DataTableToObject.ConvertToList<getItemTypeListRespContentDataItemType>(dsItems.Tables[0]);
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
            public IList<getItemTypeListRespContentDataItemType> itemTypeList { get; set; } //商品类别集合
        }

        public class getItemTypeListRespContentDataItemType
        {
            public string itemTypeId { get; set; } //商品类别标识
            public string itemTypeName { get; set; } //商品类别名称（譬如：日常用品）
            public string itemTypeCode { get; set; } //商品号码
            public Int64 displayIndex { get; set; } //排序
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
        /// 获取福利商品明细信息(暂时没用到)
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
                string itemId = reqObj.special.itemId; //商品标识

                //初始化返回对象
                respData.content = new getItemDetailRespContentData();

                ItemService itemService = new ItemService(loggingSessionInfo);
                //商品基本信息
                var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content =
                        DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
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
                    respData.content.imageList =
                        DataTableToObject.ConvertToList<getItemDetailRespContentDataImage>(dsImages.Tables[0]);
                }
                //商品sku信息
                //update by wzq 修改会员价
                var dsSkus = itemService.GetItemSkuList(itemId, userId, customerId, DateTime.Now, DateTime.Now);//暂时这个方法没有地方调用
                if (dsSkus != null && dsSkus.Tables.Count > 0 && dsSkus.Tables[0].Rows.Count > 0)
                {
                    respData.content.skuList =
                        DataTableToObject.ConvertToList<getItemDetailRespContentDataSku>(dsSkus.Tables[0]);
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
                    respData.content.salesUserList =
                        DataTableToObject.ConvertToList<getItemDetailRespContentDataSalesUser>(dsSalesUsers.Tables[0]);
                }

                //门店信息
                var dsStore = itemService.GetItemStoreInfo(itemId);
                if (dsStore != null && dsStore.Tables.Count > 0 && dsStore.Tables[0].Rows.Count > 0)
                {
                    respData.content.storeInfo =
                        DataTableToObject.ConvertToObject<getItemDetailRespContentDataStore>(dsStore.Tables[0].Rows[0]);
                }
                //品牌信息
                var dsBrand = itemService.GetItemBrandInfo(itemId);
                if (dsBrand != null && dsBrand.Tables.Count > 0 && dsBrand.Tables[0].Rows.Count > 0)
                {
                    respData.content.brandInfo =
                        DataTableToObject.ConvertToObject<getItemDetailRespContentDataBrand>(dsBrand.Tables[0].Rows[0]);
                }

                #region 获取商品属性集合

                var dsProp1 = itemService.GetItemProp1List(itemId);
                if (dsProp1 != null && dsProp1.Tables.Count > 0 && dsProp1.Tables[0].Rows.Count > 0)
                {
                    respData.content.prop1List =
                        DataTableToObject.ConvertToList<getItemDetailRespContentDataProp1>(dsProp1.Tables[0]);
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
            public string itemId { get; set; } //商品标识
            public string itemName { get; set; } //商品名称（譬如：浪漫主题房）
            public string pTypeId { get; set; } //福利类别标识（团购=2，优惠=1）
            public string pTypeCode { get; set; } //福利类别缩写（券，团）
            public string buyType { get; set; } //优惠团购类型（1=预定2=购买；特别注意判断是否卖光，卖光了，但是没有下架，则为3，表示卖完啦）
            public string buyTypeDesc { get; set; } //根据buyType,显示1=马上预订，2=立即抢购，3=卖完啦
            public int salesPersonCount { get; set; } //已购买人数量
            public int downloadPersonCount { get; set; } //已下载数量
            public decimal overCount { get; set; } //剩余数量
            public string useInfo { get; set; } //使用须知
            public string tel { get; set; } //联系电话
            public string endTime { get; set; } //下架日期
            public string couponURL { get; set; } //优惠券下载地址
            public string offersTips { get; set; } //优惠提示
            public int isKeep { get; set; } //是否已收藏 1=是，0=否
            public int isShoppingCart { get; set; } //是否已经加入购物车（1=已加入，0=未加入）
            public string prop1Name { get; set; } //属性1名称
            public string prop2Name { get; set; } //属性2名称
            public string isProp2 { get; set; } //是否有属性2；1=有，0=无
            public string itemIntroduce { get; set; } //商品介绍
            public string itemParaIntroduce { get; set; } //商品参数介绍
            public string itemCategoryId { get; set; }
            public string itemCategoryName { get; set; }
            public string itemTypeDesc { get; set; }
            public string itemSortDesc { get; set; }
            public int salesQty { get; set; }
            public string remark { get; set; }
            public IList<getItemDetailRespContentDataImage> imageList { get; set; } //图片集合
            public IList<getItemDetailRespContentDataSku> skuList { get; set; } //sku集合
            public IList<getItemDetailRespContentDataSalesUser> salesUserList { get; set; } //购买用户集合
            public getItemDetailRespContentDataStore storeInfo { get; set; } //门店对象（一家门店）
            public getItemDetailRespContentDataBrand brandInfo { get; set; } //品牌信息
            public getItemDetailRespContentDataSkuInfo skuInfo { get; set; } //默认sku标识
            public IList<getItemDetailRespContentDataProp1> prop1List { get; set; } //属性1集合
        }

        public class getItemDetailRespContentDataImage
        {
            public string imageId { get; set; } //图片标识
            public string imageURL { get; set; } //图片链接地址
        }

        public class getItemDetailRespContentDataSku
        {
            public string skuId { get; set; } //sku标识
            public string skuProp1 { get; set; } //规格
            public string skuProp2 { get; set; }
            public decimal price { get; set; } //原价
            public decimal salesPrice { get; set; } //优惠价（零售价格）
            public decimal discountRate { get; set; } //折扣
            public decimal integral { get; set; } //获得积分
        }

        public class getItemDetailRespContentDataSalesUser
        {
            public string userId { get; set; } //用户标识
            public string imageURL { get; set; } //用户头像链接地址
        }

        public class getItemDetailRespContentDataStore
        {
            public string storeId { get; set; } //门店标识
            public string storeName { get; set; } //门店名称
            public string address { get; set; } //门店地址
            public string imageURL { get; set; } //门店图片连接地址
            public int storeCount { get; set; } //门店数量
        }

        public class getItemDetailRespContentDataBrand
        {
            public string brandId { get; set; } //品牌标识
            public string brandLogoURL { get; set; } //品牌logo图片链接地址
            public string brandName { get; set; } //品牌名称
            public string brandEngName { get; set; } //品牌英文名
        }

        public class getItemDetailReqData : ReqData
        {
            public getItemDetailReqSpecialData special;
        }

        public class getItemDetailReqSpecialData
        {
            public string itemId { get; set; } //商品标识
        }

        /// <summary>
        /// 默认的sku信息
        /// </summary>
        public class getItemDetailRespContentDataSkuInfo
        {
            public string skuId { get; set; } //sku标识
            public string prop1DetailId { get; set; } //属性1明细标识
            public string prop2DetailId { get; set; } //属性2明细标识
        }

        public class getItemDetailRespContentDataProp1
        {
            public string skuId { get; set; } //sku标识
            public string prop1DetailId { get; set; } //属性1明细标识
            public string prop1DetailName { get; set; } //属性1明细名称
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

                //respData.content.DeliveryDateList = GetDeliveryDate(loggingSessionInfo);

                #region

                string strError = string.Empty;

                #region 获取门店的图片集合

                ObjectImagesBLL objectServer = new ObjectImagesBLL(loggingSessionInfo);
                var objectList =
                    objectServer.GetObjectImagesByCustomerId(loggingSessionInfo.CurrentLoggingManager.Customer_Id);
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
                    IList<getStoreListByItemRespContentItemTypeData> list =
                        new List<getStoreListByItemRespContentItemTypeData>();
                    foreach (var store in storeInfo.StoreBrandList)
                    {
                        getStoreListByItemRespContentItemTypeData info = new getStoreListByItemRespContentItemTypeData();
                        info.storeId = ToStr(store.StoreId);
                        info.storeName = ToStr(store.StoreName);
                        info.imageURL = ToStr(store.ImageUrl);
                        info.address = ToStr(store.Address);
                        info.tel = ToStr(store.Tel);
                        info.displayIndex = ToStr(store.DisplayIndex);
                        info.lng = ToStr(store.Longitude) == "" ? "0" : ToStr(store.Longitude);
                        info.lat = ToStr(store.Latitude) == "" ? "0" : ToStr(store.Latitude);
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

        /// <summary>
        /// 获取用户的配送时间
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public List<getTakeDeliveryDate> GetDeliveryDate(LoggingSessionInfo loggingSessionInfo)
        {
            /*  配送时间算法
                 *  
                 *  1.提货日期：
                 *      开始提货日期 = 当前时间+备货期（小时）
                 *      结束提货日期 = 开始提货日期 + 提货最长时间（天）
                 *      如："2014-10-24","2014-10-25","2014-10-26"
                 * 
                 *  2.提货日期中，每天提货时间：
                 *      开始提货时间 = 门店开始工作时间 （最小开始提货日期）
                 *      结束提货时间 = 门店结束工作时间 （最大结束提货日期）
                 *      然后从开始到结束分割成 间隔为2小时的时间段
                 *      如：   "9:100-11:00","11:100-13:00","13:100-15:00","15:100-17:00" 
                 */

            CustomerTakeDeliveryBLL takeDeliveryBLL = new CustomerTakeDeliveryBLL(loggingSessionInfo);
            CustomerTakeDeliveryEntity takeDeliveryEntity = takeDeliveryBLL.QueryByEntity(
                    new CustomerTakeDeliveryEntity()
                    {
                        CustomerId = loggingSessionInfo.ClientID
                    }
                    , null
            ).FirstOrDefault();

            List<getTakeDeliveryDate> DeliverList = new List<getTakeDeliveryDate>();

            try
            {
                if (takeDeliveryEntity != null
                && takeDeliveryEntity.EndWorkTime.HasValue
                && takeDeliveryEntity.BeginWorkTime.HasValue
                && DateTime.Parse(takeDeliveryEntity.EndWorkTime.ToString()).Hour >
                   DateTime.Parse(takeDeliveryEntity.BeginWorkTime.ToString()).Hour
                && takeDeliveryEntity.StockUpPeriod.HasValue
                && takeDeliveryEntity.StockUpPeriod > 0
                && takeDeliveryEntity.MaxDelivery.HasValue
                && takeDeliveryEntity.MaxDelivery > 0
                )
                {
                    //当前时间
                    DateTime now = DateTime.Now;  //当前时间

                    //门店工作开始时间（小时）
                    int beginWorkDay = DateTime.Parse(takeDeliveryEntity.BeginWorkTime.ToString()).Hour;
                    //门店工作结束时间（小时）
                    int endWorkDay = DateTime.Parse(takeDeliveryEntity.EndWorkTime.ToString()).Hour;
                    //备货时间 （小时）
                    int takeDeliveryDay = int.Parse(takeDeliveryEntity.StockUpPeriod.ToString());
                    //提货期最长 （天）
                    int MaxDeliveryDay = int.Parse(takeDeliveryEntity.MaxDelivery.ToString());

                    //备货天数 = 备货时间/每天工作小时 取整
                    int bDay = takeDeliveryDay / (endWorkDay - beginWorkDay);
                    //备货小时 = 备货时间/每天工作小时 取余
                    int bHour = takeDeliveryDay % (endWorkDay - beginWorkDay);
                    if (now.Hour + bHour > endWorkDay)
                    {
                        //如果当前时间加上 备货小时 大于 门店工作结束时间
                        //那么 时间加上工作日之间间隔时间
                        bHour += 24 - endWorkDay + beginWorkDay;
                    }

                    //开始提货时间
                    DateTime beginDate = now.AddDays(bDay).AddHours(bHour);
                    //结束提货日期
                    DateTime endDate = beginDate.AddDays(int.Parse(takeDeliveryEntity.MaxDelivery.ToString()));
                    ////门店开始时间
                    //int beginHour, endHour;
                    //    beginHour = flagFristDay ? beginDate.Hour : beginWorkDay;
                    //    endHour = endWorkDay;

                    bool flagFristDay = true;
                    do
                    {
                        getTakeDeliveryDate takeDeliveryDate = new getTakeDeliveryDate();

                        //当天配送信息
                        //1.当天  2.当天开始时间   3.当天结束时间

                        DateTime dtB;
                        DateTime dtE;

                        //当天开始时间
                        if (flagFristDay)
                        {
                            dtB = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, beginDate.Hour, 0, 0);
                            dtE = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, endWorkDay, 0, 0);
                        }
                        else if (beginDate.Day == endDate.Day)
                        {
                            dtB = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, beginWorkDay, 0, 0);
                            dtE = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, endDate.Hour, 0, 0);
                        }
                        else
                        {
                            dtB = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, beginWorkDay, 0, 0);
                            dtE = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, endWorkDay, 0, 0);
                        }

                        if (dtB.Hour >= beginWorkDay && dtB.Hour < endWorkDay && dtE.Hour <= endWorkDay && dtE.Hour > beginWorkDay)
                        {
                            DeliverList.Add(
                                new getTakeDeliveryDate()
                                {
                                    DeliveryDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss"),
                                    beginTime = dtB.ToString("yyyy-MM-dd HH:mm:ss"),
                                    endTime = dtE.ToString("yyyy-MM-dd HH:mm:ss")
                                }
                            );
                        }


                        flagFristDay = false;
                        beginDate = beginDate.AddDays(1);
                    }
                    while (beginDate.Day <= endDate.Day);
                }
            }
            catch (Exception e)
            {
            }

            return DeliverList;
        #endregion
        }

        #region

        public class getStoreListByItemRespData : Default.LowerRespData
        {
            public getStoreListByItemRespContentData content { get; set; }
        }

        public class getStoreListByItemRespContentData
        {
            public int totalCount { get; set; }
            public IList<getStoreListByItemRespContentItemTypeData> storeList { get; set; } //商品类别集合
            public IList<getStoreListByItemImageList> imageList { get; set; }
            public IList<getTakeDeliveryDate> DeliveryDateList { get; set; } //到店提货可选日期
        }

        public class getStoreListByItemRespContentItemTypeData
        {
            public string storeId { get; set; } //支付方式标识
            public string storeName { get; set; } //支付产品类别
            public string imageURL { get; set; }
            public string displayIndex { get; set; }
            public string address { get; set; }
            public string tel { get; set; }
            public string distance { get; set; } //距离
            public string lng { get; set; } //经度
            public string lat { get; set; } //维度
        }

        public class getStoreListByItemImageList
        {
            public string imageURL { get; set; }
        }

        /// <summary>
        /// 到点提货可选日期
        /// </summary>
        public class getTakeDeliveryDate
        {
            public string DeliveryDate { get; set; }
            public string beginTime { get; set; }
            public string endTime { get; set; }
        }


        public class getStoreListByItemReqData : ReqData
        {
            public getStoreListByItemReqSpecialData special;
        }

        public class getStoreListByItemReqSpecialData
        {
            public string itemId { get; set; } //商品标识
            public int page { get; set; }
            public int pageSize { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
        }

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
                    respData.content.fax = ToStr(orderInfo.Fax);
                    respData.content.longitude = ToStr(orderInfo.Longitude);
                    respData.content.latitude = ToStr(orderInfo.Latitude);
                    respData.content.address = ToStr(orderInfo.Address);
                    respData.content.imageURL = ToStr(orderInfo.ImageUrl);
                    respData.content.remark = ToStr(orderInfo.UnitRemark);
                    respData.content.supportingContent = ToStr(orderInfo.SupportingContent);
                    respData.content.hotContent = ToStr(orderInfo.HotContent);
                    respData.content.introduceContent = ToStr(orderInfo.IntroduceContent);
                    respData.content.unitTypeContent = ToStr(orderInfo.UnitTypeContent);
                    respData.content.minPrice = ToStr(orderInfo.MinPrice);
                    respData.content.starLevel = ToInt(orderInfo.StarLevel);
                    respData.content.HotelType = ToStr(orderInfo.HotelType);
                    respData.content.personAvg = ToStr(orderInfo.PersonAvg);
                    respData.content.otherUnitCount = orderInfo.OtherUnitCount;
                    respData.content.isApp = orderInfo.IsApp;
                    if (orderInfo.ImageList != null && orderInfo.ImageList.Count > 0)
                    {
                        foreach (var imageInfo in orderInfo.ImageList.OrderBy(t => t.DisplayIndex))
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
            public string brandId { get; set; } //品牌标识
            public string brandName { get; set; } //品牌名称
            public string brandEngName { get; set; } //品牌英文名
            public string storeId { get; set; } //门店标识
            public string storeName { get; set; } //门店名称
            public string displayIndex { get; set; } //序号
            public string address { get; set; }
            public string tel { get; set; } //客服电话
            public string fax { get; set; }
            public string longitude { get; set; }
            public string latitude { get; set; }
            public string imageURL { get; set; }
            public string remark { get; set; }
            public string supportingContent { get; set; } //关于我们
            public string hotContent { get; set; } //?热点 品牌故事（逸马）
            public string introduceContent { get; set; } //品牌相关
            public string unitTypeContent { get; set; }
            public string minPrice { get; set; }
            public int starLevel { get; set; }
            public string HotelType { get; set; }
            public string personAvg { get; set; }
            public int otherUnitCount { get; set; }
            public IList<gSDRespContentDataImageList> imageList { get; set; } //门店图片集合
            public int? isApp { get; set; } //是否提供预约服务
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
            public string brandId { get; set; } //品牌标识
            public string brandName { get; set; } //品牌名称
            public string brandEngName { get; set; } //品牌英文名
            public string brandDesc { get; set; } //品牌描述
            public string brandLogoURL { get; set; } //品牌logo
            public string tel { get; set; } //客服电话
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
                IList<getDownloadUsersByItemRespContentItemTypeData> list =
                    new List<getDownloadUsersByItemRespContentItemTypeData>();
                if (strError.Equals("ok"))
                {
                    respData.content.totalCount = ToStr(itemDownloadLogInfo.TotalCount);
                    foreach (var itemInfo in itemDownloadLogInfo.ItemDownloadLogList)
                    {
                        getDownloadUsersByItemRespContentItemTypeData info =
                            new getDownloadUsersByItemRespContentItemTypeData();
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
            public IList<getDownloadUsersByItemRespContentItemTypeData> userList { get; set; } //商品类别集合
        }

        public class getDownloadUsersByItemRespContentItemTypeData
        {
            public string userId { get; set; } //用户标识标识
            public string userName { get; set; } //用户名称
            public string imageURL { get; set; } //用户头像
            public string displayIndex { get; set; } //排序
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
                    orderInfo.Status = "100";
                    orderInfo.StatusDesc = "待审核";
                    orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo, orderInfo.StoreId);
                }

                #endregion

                string strError = string.Empty;
                string strMsg = string.Empty;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", "开始保存")
                });

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
            public string skuId { get; set; } //商品SKU标识
            public string qty { get; set; } //商品数量
            public string storeId { get; set; } //门店标识
            public string salesPrice { get; set; } //商品单价
            public string stdPrice { get; set; } //商品标准价格
            public string totalAmount { get; set; } //订单总价
            public string mobile { get; set; } //手机号码
            public string deliveryId { get; set; } //		配送方式标识
            public string deliveryAddress { get; set; } //	配送地址
            public string deliveryTime { get; set; } //		提货时间（配送时间）
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
                orderInfo.CouponsPrompt = ToStr(reqObj.special.couponsPrompt);
                if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals("") &&
                    !ToStr(reqObj.special.actualAmount).Equals(""))
                {
                    orderInfo.ActualAmount = Convert.ToDecimal(ToStr(reqObj.special.actualAmount));
                }
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

                #region 处理订单的优惠券关系

                //orderInfo.CouponList = new List<TOrderCouponMappingEntity>();
                //if (reqObj.special.couponList != null && reqObj.special.couponList.Count > 0)
                //{
                //    foreach (var couponInfo in reqObj.special.couponList)
                //    {
                //        TOrderCouponMappingEntity mappingInfo = new TOrderCouponMappingEntity();
                //        mappingInfo.MappingId = BaseService.NewGuidPub();
                //        mappingInfo.OrderId = orderInfo.OrderId;
                //        orderInfo.CouponList.Add(mappingInfo);
                //    }
                //}

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
            public string qty { get; set; } //商品数量
            public string storeId { get; set; } //门店标识
            public string totalAmount { get; set; } //订单总价
            public string mobile { get; set; } //手机号码
            public string deliveryId { get; set; } //		配送方式标识
            public string deliveryAddress { get; set; } //	配送地址
            public string deliveryTime { get; set; } //		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
            public string tableNumber { get; set; }
            public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213--Field16）
            public string actualAmount { get; set; } //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)

            public IList<setOrderDetailClass> orderDetailList { get; set; }
            public IList<setOrderCouponClass> couponList { get; set; } //优惠券集合 （Jermyn20131213--tordercouponmapping
        }

        public class setOrderDetailClass
        {
            public string skuId { get; set; } //商品SKU标识
            public string salesPrice { get; set; } //商品销售单价
            public string qty { get; set; } //商品数量
            public string beginDate { get; set; }
            public string endDate { get; set; }
        }

        public class setOrderCouponClass
        {
            public string couponId { get; set; } //优惠券标识
        }

        #endregion

        #endregion

        #region 10 订单提交 20131225 新版本 多个商品

        public string setOrderInfoTwo()
        {
            string content = string.Empty;
            var respData = new setOrderInfoTwoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                //reqContent = "{"common":{"locale":"zh","userId":"c45f87741005ab3a4d9a6b6da21e9162","openId":"c45f87741005ab3a4d9a6b6da21e9162","customerId":"f6a7da3d28f74f2abedfc3ea0cf65c01"},"special":{"skuId":"","qty":"","storeId":"","salesPrice":"","stdPrice":"","totalAmount":"640","tableNumber":"","username":"","mobile":"","email":"","remark":"1","deliveryId":"1","deliveryAddress":"","deliveryTime":"","orderDetailList":[{"skuId":"648245A5233C48D9817B561139CE9548","salesPrice":"640","qty":"1"}]}}";

                #region //解析请求字符串 chech

                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoTwoReqData>();
                reqObj = reqObj == null ? new setOrderInfoTwoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderInfoTwoReqSpecialData();
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
                orderInfo.CouponsPrompt = ToStr(reqObj.special.couponsPrompt);
                orderInfo.Status = ToStr(reqObj.special.status);
                orderInfo.IsALD = reqObj.common.isALD;
                if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals("") &&
                    !ToStr(reqObj.special.actualAmount).Equals(""))
                {
                    orderInfo.ActualAmount = Convert.ToDecimal(ToStr(reqObj.special.actualAmount));
                }
                if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
                {
                    orderInfo.OrderId = BaseService.NewGuidPub();
                    orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    orderInfo.Status = "100";
                    orderInfo.StatusDesc = "待审核";
                    //orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo, orderInfo.StoreId);

                    #region 处理阿拉丁需要的参数

                    if (reqObj.common.channelId == null
                        || reqObj.common.businessZoneId == null
                        || reqObj.common.channelId.Trim().Equals("")
                        || reqObj.common.businessZoneId.Trim().Equals(""))
                    {
                    }
                    else
                    {
                        AParaObjectEntity paraObjectInfo = new AParaObjectEntity();
                        paraObjectInfo.ObjectId = orderInfo.OrderId;
                        paraObjectInfo.BusinessZoneID = ToStr(reqObj.common.businessZoneId);
                        paraObjectInfo.ChannelId = ToStr(reqObj.common.channelId);
                        AParaObjectBLL paraObjectBll = new AParaObjectBLL(loggingSessionInfo);
                        bool bObjectReturn = paraObjectBll.SetParaObjectInfo(paraObjectInfo);
                    }

                    #endregion
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

                #region 处理订单的优惠券关系

                #endregion

                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderOnlineShoppingTwo(orderInfo, out strError, out strMsg);

                #region 判断是否是阿拉丁平台的订单,如果是则向阿拉丁同步订单

                if (reqObj.common.isALD == "1")
                {
                    var store = unitServer.GetUnitById(orderInfo.StoreId);
                    if (bReturn)
                    {
                        //o2o下单成功后,将订单同时发送给ALD
                        JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrder aldOrder =
                            new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrder();
                        aldOrder.SourceOrdersID = orderInfo.OrderId;
                        aldOrder.SourceOrdersNO = orderInfo.OrderCode;
                        aldOrder.SourceOrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        aldOrder.SourceStoreID = orderInfo.StoreId;
                        aldOrder.SourceStoreName = store != null ? store.Name : "阿拉丁";
                        aldOrder.SourceStoreAddress = store.Address;
                        aldOrder.SourceStoreTel = store.Telephone;
                        aldOrder.DataDeployName = unitServer.GetCustomerDataDeploy(orderInfo.CustomerId);
                        aldOrder.SourceClientID = orderInfo.CustomerId;
                        aldOrder.Status = orderInfo.Status;
                        aldOrder.Remark = orderInfo.Remark;
                        aldOrder.DeliverType = orderInfo.DeliveryId;
                        aldOrder.ConsigneeAddress = orderInfo.DeliveryAddress;
                        aldOrder.ConsigneePhoneNO = orderInfo.Mobile;
                        aldOrder.ConsigneeName = orderInfo.username;
                        aldOrder.OrderTotalAmount = orderInfo.TotalAmount;
                        aldOrder.OrderFactAmount = orderInfo.TotalAmount;
                        aldOrder.OrderItemTotalCount = itemTotalQty;
                        aldOrder.MemberID = new Guid(reqObj.common.userId);

                        //订单明细
                        aldOrder.OrderDetails =
                            new List<JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderDetail>();
                        if (orderInfo.OrderDetailInfoList != null && orderInfo.OrderDetailInfoList.Count > 0)
                        {
                            List<string> skuIDs = new List<string>();
                            foreach (var detail in orderInfo.OrderDetailInfoList)
                            {
                                var aldOrderDetail =
                                    new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderDetail();
                                aldOrderDetail.SourceSKUID = detail.sku_id;
                                aldOrderDetail.Quantity = Convert.ToInt32(detail.order_qty);
                                aldOrderDetail.Price = detail.std_price;
                                aldOrder.OrderDetails.Add(aldOrderDetail);
                                //
                                skuIDs.Add(detail.sku_id);
                            }
                            //根据SKU查找订单商品项的SKU详细信息：商品名称、商品图片、规格 etc..
                            var skuService = new SkuService(loggingSessionInfo);
                            var skuInfos = skuService.GetSKUAndItemBySKUIDs(skuIDs.ToArray());
                            foreach (var detail in aldOrder.OrderDetails)
                            {
                                foreach (DataRow dr in skuInfos.Rows)
                                {
                                    if (dr["sku_id"] != DBNull.Value &&
                                        Convert.ToString(dr["sku_id"]).ToLower() == detail.SourceSKUID.ToLower())
                                    {
                                        if (dr["item_id"] != DBNull.Value)
                                        {
                                            detail.SourceItemID = Convert.ToString(dr["item_id"]);
                                        }
                                        if (dr["item_name"] != DBNull.Value)
                                        {
                                            detail.SourceItemName = Convert.ToString(dr["item_name"]);
                                        }
                                        if (dr["imageurl"] != DBNull.Value)
                                        {
                                            detail.SourceItemImageUrl = Convert.ToString(dr["imageurl"]);
                                        }
                                        if (dr["prop_1_detail_name"] != DBNull.Value)
                                        {
                                            detail.SourceSKUProp1 = Convert.ToString(dr["prop_1_detail_name"]);
                                        }
                                        if (dr["prop_2_detail_name"] != DBNull.Value)
                                        {
                                            detail.SourceSKUProp2 = Convert.ToString(dr["prop_2_detail_name"]);
                                        }
                                        if (dr["prop_3_detail_name"] != DBNull.Value)
                                        {
                                            detail.SourceSKUProp3 = Convert.ToString(dr["prop_3_detail_name"]);
                                        }
                                        if (dr["prop_4_detail_name"] != DBNull.Value)
                                        {
                                            detail.SourceSKUProp4 = Convert.ToString(dr["prop_4_detail_name"]);
                                        }
                                        break;
                                    }
                                }
                            }
                            //向阿拉丁提交订单
                            JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderRequest aldRequest =
                                new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderRequest();
                            aldRequest.BusinessZoneID = 1;

                            if (!string.IsNullOrWhiteSpace(reqObj.common.locale))
                            {
                                switch (reqObj.common.locale.ToLower())
                                {
                                    case "zh":
                                        aldRequest.Locale = 1;
                                        break;
                                }
                            }
                            else
                            {
                                aldRequest.Locale = 1;
                            }
                            aldRequest.UserID = new Guid(reqObj.common.userId);
                            aldRequest.Parameters = aldOrder;

                            var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                            if (string.IsNullOrEmpty(url))
                                throw new Exception("未配置阿拉丁平台接口URL:ALDGatewayURL");
                            var postContent = string.Format("Action=TransportOrders&ReqContent={0}", aldRequest.ToJSON());
                            var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                            var aldRsp =
                                strAldRsp
                                    .DeserializeJSONTo
                                    <JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                            if (aldRsp == null || aldRsp.IsSuccess() == false)
                            {
                                respData.code = "114";
                                respData.description = string.Format("向阿拉丁提交订单失败[{0}].",
                                    aldRsp != null ? aldRsp.Message : string.Empty);
                                content = respData.ToJSON();
                                return content;
                            }
                        }
                    }
                }

                #endregion

                #region 返回信息设置

                respData.content = new setOrderInfoTwoRespContentData();
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
        public class setOrderInfoTwoRespData : Default.LowerRespData
        {
            public setOrderInfoTwoRespContentData content { get; set; }
        }

        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setOrderInfoTwoRespContentData
        {
            public string orderId { get; set; }
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setOrderInfoTwoReqData : ReqData
        {
            public setOrderInfoTwoReqSpecialData special;
        }

        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOrderInfoTwoReqSpecialData
        {
            public string qty { get; set; } //商品数量
            public string storeId { get; set; } //门店标识
            public string totalAmount { get; set; } //订单总价
            public string mobile { get; set; } //手机号码
            public string deliveryId { get; set; } //		配送方式标识
            public string deliveryAddress { get; set; } //	配送地址
            public string deliveryTime { get; set; } //		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
            public string tableNumber { get; set; }
            public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213--Field16）
            public string actualAmount { get; set; } //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
            public string status { get; set; }

            public IList<setOrderDetailTwoClass> orderDetailList { get; set; }
            public IList<setOrderCouponTwoClass> couponList { get; set; } //优惠券集合 （Jermyn20131213--tordercouponmapping
        }

        public class setOrderDetailTwoClass
        {
            public string skuId { get; set; } //商品SKU标识
            public string salesPrice { get; set; } //商品销售单价
            public string qty { get; set; } //商品数量
            public string beginDate { get; set; }
            public string endDate { get; set; }
        }

        public class setOrderCouponTwoClass
        {
            public string couponId { get; set; } //优惠券标识
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
                orderInfo.CarrierID = ToStr(reqObj.special.storeId);
                orderInfo.OrderId = ToStr(reqObj.special.orderId);
                orderInfo.CouponsPrompt = ToStr(reqObj.special.couponsPrompt);
                orderInfo.Invoice = ToStr(reqObj.special.invoice);
                if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals(""))
                {
                    orderInfo.ActualAmount = Convert.ToDecimal(ToStr(reqObj.special.actualAmount));
                }

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
            public string mobile { get; set; } //手机号码
            public string deliveryId { get; set; } //		配送方式标识
            public string deliveryAddress { get; set; } //	配送地址
            public string deliveryTime { get; set; } //		提货时间（配送时间）
            public string email { get; set; }
            public string remark { get; set; }
            public string username { get; set; }
            public string orderId { get; set; } //订单标识
            public string storeId { get; set; } //到店标识
            public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213--Field16）
            public string actualAmount { get; set; } //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
            public string invoice { get; set; } //发票号码
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
            public string invoice { get; set; } //Jermyn20131008 
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

                #region 获得支付列表

                TPaymentTypeEntity[] paymentTypeList = null;
                if (string.IsNullOrEmpty(reqObj.common.channelId))
                {
                    string strSource = reqObj.common.plat;
                    if (string.IsNullOrEmpty(strSource))
                    {
                        if (reqObj.special.dataFromId == "3")
                        {
                            strSource = "IsJSPay";
                        }
                    }

                    paymentTypeList = paymentTypeService.GetByCustomerBySource(reqObj.common.customerId, strSource);
                }
                else
                {
                    paymentTypeList = paymentTypeService.GetByCustomerByChanel(reqObj.common.customerId, reqObj.common.channelId);
                }

                #endregion

                IList<getPaymentTypeListRespContentItemTypeData> list =
                    new List<getPaymentTypeListRespContentItemTypeData>();
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
            public IList<getPaymentTypeListRespContentItemTypeData> paymentTypeList { get; set; } //商品类别集合
        }

        public class getPaymentTypeListRespContentItemTypeData
        {
            public string paymentTypeId { get; set; } //支付方式标识
            public string paymentItemType { get; set; } //支付产品类别
            public string LogoURL { get; set; } //产品logo
            public string paymentDesc { get; set; } //支付描述
        }

        public class getPaymentTypeListReqData : ReqData
        {
            public getPaymentTypeListReqSpecialData special;
        }

        public class getPaymentTypeListReqSpecialData
        {
            public string dataFromId { get; set; }      //来源：2=Pad，3=微信【必须】

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
                var deliveryList = dService.QueryByEntity(
                        new DeliveryEntity()
                        {
                            Status = 1
                        }
                        , null
                    );
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

                //增加会员地址返回
                var vipAddressBLL = new VipAddressBLL(loggingSessionInfo);
                OrderBy[] orderbys = new OrderBy[]{
                    new OrderBy(){FieldName="IsDefault",Direction=OrderByDirections.Desc},
                    new OrderBy(){FieldName="LastUpdateTime",Direction=OrderByDirections.Desc}
                };
                var vipAddress = vipAddressBLL.QueryByEntity(new VipAddressEntity() { VIPID = userId }, orderbys).FirstOrDefault();
                var vipAddressInfo = new VipAddressInfo() { };
                if (vipAddress != null)
                {
                    vipAddressInfo.vipAddressID = vipAddress.VipAddressID;
                    vipAddressInfo.vipid = vipAddress.VIPID;
                    vipAddressInfo.linkMan = vipAddress.LinkMan;
                    vipAddressInfo.linkTel = vipAddress.LinkTel;
                    vipAddressInfo.cityID = vipAddress.CityID;
                    vipAddressInfo.province = vipAddress.Province;
                    vipAddressInfo.cityName = vipAddress.CityName;
                    vipAddressInfo.districtName = vipAddress.DistrictName;
                    vipAddressInfo.address = vipAddress.Address;
                    vipAddressInfo.isDefault = vipAddress.IsDefault.ToString();
                    respData.content.vipAddressInfo = vipAddressInfo;
                }
                //增加会员会集店信息
                var unitBLL = new TUnitBLL(loggingSessionInfo);
                var vipBLL = new VipBLL(loggingSessionInfo);
                var vipInfo = vipBLL.GetByID(userId);
                VipUnitInfo vipUnitInfo = null;
                if (vipInfo != null)
                {
                    if (!string.IsNullOrEmpty(vipInfo.CouponInfo))
                    {
                        var unitInfo = unitBLL.GetByID(vipInfo.CouponInfo);
                        vipUnitInfo = new VipUnitInfo();
                        if (unitInfo != null)
                        {
                            vipUnitInfo.unitID = unitInfo.UnitID;
                            vipUnitInfo.unitName = unitInfo.UnitName;
                            vipUnitInfo.province = unitInfo.Province;
                            vipUnitInfo.cityName = unitInfo.CityName;
                            vipUnitInfo.districtName = unitInfo.DistrictName;
                            vipUnitInfo.address = unitInfo.UnitAddress;
                        }
                    }
                }
                respData.content.vipUnitInfo = vipUnitInfo;


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
            public IList<getDeliveryListRespContentItemTypeData> deliveryList { get; set; } //商品类别集合
            public VipAddressInfo vipAddressInfo { get; set; }//会员地址信息
            public VipUnitInfo vipUnitInfo { get; set; }//会员会集店信息
        }

        public class getDeliveryListRespContentItemTypeData
        {
            public string deliveryId { get; set; } //支付方式标识
            public string deliveryName { get; set; } //支付产品类别
            public string isAddress { get; set; }

        }
        /// <summary>
        /// 地址信息
        /// </summary>
        public class VipAddressInfo
        {
            public string vipAddressID { get; set; }
            public string vipid { get; set; }
            public string linkMan { get; set; }
            public string linkTel { get; set; }
            public string cityID { get; set; }
            public string districtName { get; set; }
            public string address { get; set; }
            public string isDefault { get; set; }
            public string province { get; set; }//省
            public string cityName { get; set; }//市
        }
        public class VipUnitInfo
        {
            public string unitID { get; set; }
            public string unitName { get; set; }
            public string province { get; set; }//省
            public string cityName { get; set; }//市
            public string districtName { get; set; }//区
            public string address { get; set; }//地址
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
                    respData.content.actualAmount = ToStr(orderInfo.ActualAmount);
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
            public string itemname { get; set; } //商品名称
            public string ordercode { get; set; } //订单code
            public string skuid { get; set; } //sku id
            public string totalqty { get; set; } //数量
            public string salesprice { get; set; } //实际出售价格
            public string stdprice { get; set; } //标准价
            public string discountrate { get; set; } //折扣率
            public string totalamount { get; set; } //总价
            public string mobile { get; set; } //手机
            public string email { get; set; } //邮箱
            public string deliveryaddress { get; set; } //配送地址
            public string deliverytime { get; set; } //配送时间
            public string remark { get; set; } //备注			
            public string deliveryname { get; set; } //配送方式名称
            public string username { get; set; }
            public string itemId { get; set; } //商品标识
            public string deliveryid { get; set; } //
            public string actualAmount { get; set; } //实际支付金额
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
                bool bReturn = itemKeepServer.SetItemKeep(reqObj.special.itemId, reqObj.special.keepStatus,
                    reqObj.common.userId, out strError);
                if (!bReturn)
                {
                    respData.code = "111";
                    respData.exception = strError;
                }
                else // 收藏/取消收藏成功
                {
                    if (reqObj.common.isALD == "1") //来自ALD的收藏
                    {
                        string url = ConfigurationManager.AppSettings["ALDSyncWCF"]; //调用路径
                        if (string.IsNullOrEmpty(url))
                            throw new Exception("未配置阿拉丁实现数据同步的WCF服务URL:ALDSyncWCF");

                        string method = "SyncLog";//调用方法
                        //传输参数[客户ID，商品ID，收藏状态，会员ID]
                        string json = "{\"sRequest\":{\"ClientID\":\"" + reqObj.common.customerId
                                       + "\",\"SourceItemID\":\"" + reqObj.special.itemId
                                       + "\",\"SourceType\":\"" + reqObj.special.keepStatus
                                       + "\",\"MemberID\":\"" + reqObj.common.userId + "\"}}";

                        SendHttpRequest(url, method, json);
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

        /// <summary>
        /// 访问WCF服务方法
        /// </summary>
        /// <param name="requestURI"></param>
        /// <param name="requestMethod"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string SendHttpRequest(string requestURI, string requestMethod, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = string.Format("{0}/{1}", requestURI, requestMethod);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求 
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.Accept = "application/json";
            myRequest.ContentType = "application/json";

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
            //public string aldItemId { get; set; }  //ALD对应上的商品ID
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

                var vipId = reqObj.common.userId;
                if (reqObj.special.vipId != null && reqObj.special.vipId.Trim().Length > 0)
                {
                    vipId = reqObj.special.vipId;
                }

                vipInfo = server.GetByID(vipId);
                if (vipInfo != null)
                {
                    respData.content.vipName = ToStr(vipInfo.VipName);
                    respData.content.vipCode = ToStr(vipInfo.VipCode);
                    respData.content.address = ToStr(vipInfo.DeliveryAddress);
                    respData.content.phone = ToStr(vipInfo.Phone);
                    respData.content.email = ToStr(vipInfo.Email);
                    respData.content.vipId = ToStr(vipInfo.VIPID);
                    respData.content.imageUrl = ToStr(vipInfo.HeadImgUrl);
                    respData.content.openId = ToStr(vipInfo.WeiXinUserId);

                    respData.content.isAuth = 0;
                    respData.content.vipRealName = ToStr(vipInfo.VipRealName);
                    respData.content.school = ToStr(vipInfo.Col41);
                    respData.content.className = ToStr(vipInfo.Col42);
                    respData.content.company = ToStr(vipInfo.Col43);
                    respData.content.position = ToStr(vipInfo.Col44);
                    respData.content.hobby = ToStr(vipInfo.Col46);
                    respData.content.myValue = ToStr(vipInfo.Col47);
                    respData.content.needValue = ToStr(vipInfo.Col48);
                    respData.content.sinaMBlog = ToStr(vipInfo.SinaMBlog);
                    respData.content.weixin = ToStr(vipInfo.WeiXin);
                    respData.content.qRVipCode = ToStr(vipInfo.QRVipCode);

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
            public string vipName { get; set; } //品牌标识
            public string vipCode { get; set; } //品牌名称
            public string phone { get; set; } //品牌英文名
            public string address { get; set; } //门店标识
            public string email { get; set; }
            public string vipId { get; set; }
            public string validIntegral { get; set; }
            public string outIntegral { get; set; }
            public string imageUrl { get; set; }
            public string itemKeepCount { get; set; }
            public string integration { get; set; }
            public string couponCount { get; set; }
            public string shoppingCartCount { get; set; }
            public string lotteryCount { get; set; } //抽奖次数 20131017
            public string openId { get; set; }

            public int isAuth { get; set; }
            public string vipRealName { get; set; }
            public string school { get; set; }
            public string className { get; set; }
            public string company { get; set; }
            public string position { get; set; }
            public string hobby { get; set; }
            public string myValue { get; set; }
            public string needValue { get; set; }
            public string sinaMBlog { get; set; }
            public string weixin { get; set; }
            public string qRVipCode { get; set; }
        }


        public class getVIPDetailReqData : ReqData
        {
            public getVIPDetailReqSpecialData special;
        }

        public class getVIPDetailReqSpecialData
        {
            public string vipId { get; set; }
        }

        #endregion

        #endregion

        #region 16.获取VIP用户集合

        public string getEmbaVipList()
        {
            string content = string.Empty;
            var respData = new getEmbaVipListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEmbaVipList: {0}", reqContent)
                });

                #region //解析请求字符串

                var reqObj = reqContent.DeserializeJSONTo<getEmbaVipListReqData>();
                reqObj = reqObj == null ? new getEmbaVipListReqData() : reqObj;

                if (reqObj.special == null)
                {
                    reqObj.special = new getEmbaVipListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special.pageSize == 0)
                {
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special.page == 0)
                {
                    reqObj.special.page = 1;
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                #endregion

                #region 业务处理

                respData.content = new getEmbaVipListRespContentData();

                VipBLL server = new VipBLL(loggingSessionInfo);
                var vwVipCenterInfoBLL = new VwVipCenterInfoBLL(loggingSessionInfo);
                VipEntity queryInfo = new VipEntity();
                var vipList = server.GetList_Emba(reqObj.special.keyword, reqObj.special.page, reqObj.special.pageSize);

                int totalCount = server.GetListCount_Emba(reqObj.special.keyword);
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                respData.content.totalCount = totalCount;

                if (vipList != null && vipList.Count > 0)
                {
                    respData.content.vipList = new List<getEmbaVipListRespContentItemData>();
                    foreach (var vipInfo in vipList)
                    {
                        var tmpVipObj = new getEmbaVipListRespContentItemData();
                        tmpVipObj.vipName = ToStr(vipInfo.VipName);
                        tmpVipObj.vipCode = ToStr(vipInfo.VipCode);
                        tmpVipObj.address = ToStr(vipInfo.DeliveryAddress);
                        tmpVipObj.phone = ToStr(vipInfo.Phone);
                        tmpVipObj.email = ToStr(vipInfo.Email);
                        tmpVipObj.vipId = ToStr(vipInfo.VIPID);
                        tmpVipObj.imageUrl = ToStr(vipInfo.HeadImgUrl);
                        tmpVipObj.isAuth = 0;
                        tmpVipObj.vipRealName = ToStr(vipInfo.VipRealName);
                        tmpVipObj.school = ToStr(vipInfo.Col41);
                        tmpVipObj.className = ToStr(vipInfo.Col42);
                        tmpVipObj.company = ToStr(vipInfo.Col43);
                        tmpVipObj.position = ToStr(vipInfo.Col44);
                        respData.content.vipList.Add(tmpVipObj);
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

        public class getEmbaVipListRespData : Default.LowerRespData
        {
            public getEmbaVipListRespContentData content { get; set; }
        }

        public class getEmbaVipListRespContentData
        {
            public IList<getEmbaVipListRespContentItemData> vipList { get; set; }
            public int totalCount { get; set; }
            public string isNext { get; set; }
        }

        public class getEmbaVipListRespContentItemData
        {
            public string vipName { get; set; } //品牌标识
            public string vipCode { get; set; } //品牌名称
            public string phone { get; set; } //品牌英文名
            public string address { get; set; } //门店标识
            public string email { get; set; }
            public string vipId { get; set; }
            public string imageUrl { get; set; }
            public int isAuth { get; set; }
            public string vipRealName { get; set; }
            public string school { get; set; }
            public string className { get; set; }
            public string company { get; set; }
            public string position { get; set; }
        }

        public class getEmbaVipListReqData : ReqData
        {
            public getEmbaVipListReqSpecialData special;
        }

        public class getEmbaVipListReqSpecialData
        {
            public string keyword { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
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
            public string vipName { get; set; } //品牌标识
            public string phone { get; set; } //品牌英文名
            public string address { get; set; } //门店标识
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
                    respData.content.prop2List =
                        DataTableToObject.ConvertToList<getSkuProp2ListRespContentItemTypeData>(dsProp2.Tables[0]);
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
            public IList<getSkuProp2ListRespContentItemTypeData> prop2List { get; set; } //商品类别集合
        }

        public class getSkuProp2ListRespContentItemTypeData
        {
            public string skuId { get; set; } //支付方式标识
            public string prop2DetailId { get; set; } //支付产品类别
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
            public string unitId { get; set; } //门店标识
            public string customerId { get; set; } //客户标识
            public string unitName { get; set; } //门店/客户名称
            public string isApp { get; set; } //是否需要下载APP
            public string firstPageImage { get; set; } //首页图片
            public string loginImage { get; set; } //登录图片
            public string productsBackgroundImage { get; set; } //首页最新商品背景图
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
                string strDate = reqObj.special.strDate; //查询日期

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
                    respData.content.entriesList =
                        DataTableToObject.ConvertToList<getEventsEntriesRespContentDataItem>(dsEntries.Tables[0]);
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
            public string strDate { get; set; } //作品日期
            public IList<getEventsEntriesRespContentDataItem> entriesList { get; set; } //爱秀达人作品集合
        }

        public class getEventsEntriesRespContentDataItem
        {
            public string entriesId { get; set; } //作品标识
            public string workTitle { get; set; } //作品名称
            public string workUrl { get; set; } //作品图片链接地址
            public Int64 displayIndex { get; set; } //序号
            public int praiseCount { get; set; } //赞的数量
            public int commentCount { get; set; } //评论数量
        }

        public class getEventsEntriesReqData : ReqData
        {
            public getEventsEntriesReqSpecialData special;
        }

        public class getEventsEntriesReqSpecialData
        {
            public string strDate { get; set; } //查询日期
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
                string signUpId = reqObj.common.signUpId; //报名ID
                string userName = reqObj.special.userName; //用户名称
                string phone = reqObj.special.phone; //用户手机号码
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
            public string signUpId { get; set; } //报名Id
        }

        public class setEventSignUpReqData : ReqData
        {
            public setEventSignUpReqSpecialData special;
        }

        public class setEventSignUpReqSpecialData
        {
            public string userName { get; set; } //用户名称
            public string phone { get; set; } //用户手机号码
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
                string entriesId = reqObj.special.entriesId; //作品标识

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
            public string entriesId { get; set; } //作品标识
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
                string signUpId = reqObj.common.signUpId; //报名ID
                string comment = reqObj.special.content; //评论内容
                string entriesId = reqObj.special.entriesId; //作品标识

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
            public string content { get; set; } //评论内容
            public string entriesId { get; set; } //作品标识
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
                string entriesId = reqObj.special.entriesId; //作品标识
                int page = reqObj.special.page; //页码
                int pageSize = reqObj.special.pageSize; //页面数量

                var entriesService = new LEventsEntriesBLL(loggingSessionInfo);

                //初始化返回对象
                respData.content = new getEventsEntriesCommentListRespContentData();
                var dsEntries = entriesService.GetEventsEntriesCommentList(entriesId);

                if (dsEntries != null && dsEntries.Tables.Count > 0 && dsEntries.Tables[0].Rows.Count > 0)
                {
                    respData.content =
                        DataTableToObject.ConvertToObject<getEventsEntriesCommentListRespContentData>(
                            dsEntries.Tables[0].Rows[0]);

                    //初始化返回对象
                    respData.content.commentList = new List<getEventsEntriesCommentListRespContentDataItem>();

                    var dsComment = entriesService.GetEventsEntriesCommentList(entriesId, page, pageSize);
                    if (dsComment != null && dsComment.Tables.Count > 0 && dsComment.Tables[0].Rows.Count > 0)
                    {
                        respData.content.commentList =
                            DataTableToObject.ConvertToList<getEventsEntriesCommentListRespContentDataItem>(
                                dsComment.Tables[0]);
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
            public string entriesId { get; set; } //作品标识
            public string workTitle { get; set; } //作品名称
            public string workUrl { get; set; } //作品图片链接地址
            public int praiseCount { get; set; } //赞的数量
            public string nextEntriesId { get; set; } //下一个作品标识
            public string preEntriesId { get; set; } //上一个作品标识
            public int commentCount { get; set; } //评论数量
            public IList<getEventsEntriesCommentListRespContentDataItem> commentList { get; set; } //评论集合
        }

        public class getEventsEntriesCommentListRespContentDataItem
        {
            public string content { get; set; } //评论内容
            public string userName { get; set; } //用户名称
            public string phone { get; set; } //手机号码
            public Int64 displayIndex { get; set; } //序号
            public string createTime { get; set; } //评论时间
        }

        public class getEventsEntriesCommentListReqData : ReqData
        {
            public getEventsEntriesCommentListReqSpecialData special;
        }

        public class getEventsEntriesCommentListReqSpecialData
        {
            public string entriesId { get; set; } //作品标识
            public int page { get; set; } //页码
            public int pageSize { get; set; } //页面数量
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
                string strDate = reqObj.special.strDate; //获奖日期(yyyy-MM-dd)

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
                    respData.content.crowdDarentList =
                        DataTableToObject.ConvertToList<getEventsEntriesWinnersRespContentDataItem>(dsCrows.Tables[0]);
                }

                var dsWorks = entriesService.GetWorkDarenList(strDate);
                if (dsWorks != null && dsWorks.Tables.Count > 0 && dsWorks.Tables[0].Rows.Count > 0)
                {
                    respData.content.workDarenList =
                        DataTableToObject.ConvertToList<getEventsEntriesWinnersRespContentDataItem>(dsWorks.Tables[0]);
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
            public int crowdDarenCount { get; set; } //围观达人数量
            public int workDarenCount { get; set; } //爱秀达人数量
            public IList<getEventsEntriesWinnersRespContentDataItem> crowdDarentList { get; set; } //围观达人集合
            public IList<getEventsEntriesWinnersRespContentDataItem> workDarenList { get; set; } //爱秀达人集合
        }

        public class getEventsEntriesWinnersRespContentDataItem
        {
            public string userName { get; set; } //用户名称
            public string content { get; set; } //评论
            public int prizeCount { get; set; } //获奖作品数量
        }

        public class getEventsEntriesWinnersReqData : ReqData
        {
            public getEventsEntriesWinnersReqSpecialData special;
        }

        public class getEventsEntriesWinnersReqSpecialData
        {
            public string strDate { get; set; } //获奖日期(yyyy-MM-dd)
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
                    respData.content.entriesList =
                        DataTableToObject.ConvertToList<getEventsEntriesMonthDarenRespContentDataItem>(
                            dsEntries.Tables[0]);
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
            public IList<getEventsEntriesMonthDarenRespContentDataItem> entriesList { get; set; } //品味达人作品集合
        }

        public class getEventsEntriesMonthDarenRespContentDataItem
        {
            public string entriesId { get; set; } //作品标识
            public string workTitle { get; set; } //作品名称
            public string workUrl { get; set; } //作品图片链接地址
            public int displayIndex { get; set; } //序号
            public int praiseCount { get; set; } //赞的数量
            public int commentCount { get; set; } //评论数量
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
                    respData.content.isDesc = 1; // ToInt(obj.IsDesc); 
                }
                if (obj.Food == null || obj.Food.Trim().Equals(""))
                {
                    respData.content.isFood = 0;
                }
                else
                {
                    respData.content.isFood = 1; // ToInt(obj.IsFood);
                }
                if (obj.ContactUs == null || obj.ContactUs.Trim().Equals(""))
                {
                    respData.content.isContactUs = 0;
                }
                else
                {
                    respData.content.isContactUs = 1; //ToInt(obj.IsContactUs);
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
            public string contactUs { get; set; } //联系我们
            public int isDesc { get; set; } //是否需要显示简介 1=显示 0=不显示
            public int isOrganizer { get; set; } //是否需要显示组织者 1=显示 0=不显示
            public int isSchedule { get; set; } //是否需要显示日程安排 1=显示 0=不显示
            public int isSpeakers { get; set; } //是否需要显示演讲人 1=显示 0=不显示
            public int isRoundtable { get; set; } //是否需要显示圆桌会议  1=显示 0=不显示
            public int isSponsor { get; set; } //是否需要显示赞助 1=显示 0=不显示
            public int isFood { get; set; } //是否需要显示场地膳食 1=显示 0=不显示
            public int isPreviousForum { get; set; } //是否需要显示往届论坛 1=显示 0=不显示
            public int isContactUs { get; set; } //是否需要显示联系我们 1=显示 0=不显示
            public int isSignUp { get; set; } //是否需要显示报名参加1=显示 0=不显示
            public string register { get; set; } //注册参加
            public int isRegister { get; set; } //是否显示注册参加
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
            respData.description = "感谢关注本课程，我们将尽快与您联系！";
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
                FromSetting fromSetting = new FromSetting();
                fromSetting.SMTPServer = "smtp.exmail.qq.com";
                fromSetting.SendFrom = "ceibs.service@jitmarketing.cn";
                fromSetting.UserName = "ceibs.service@jitmarketing.cn";
                fromSetting.Password = "jit12345";
                var success = JIT.Utility.Notification.Mail.SendMail(fromSetting, mailto, mailsubject, mailBody, null);
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
            public string emailId { get; set; } //邮件标识
            public string remark { get; set; } //备注 20131017
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
                ds = service.GetNewsByCourseList(reqObj.special.courseTypeId, reqObj.special.page,
                    reqObj.special.pageSize);
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
                                getSchoolEventListRespContentDataItem rInfo =
                                    new getSchoolEventListRespContentDataItem();
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
                                        getSchoolEventListRespContentDataItem rInfo1 =
                                            new getSchoolEventListRespContentDataItem();
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
                                                getSchoolEventListRespContentDataItem rInfo2 =
                                                    new getSchoolEventListRespContentDataItem();
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
            public string haveSignedCount { get; set; } //已报名数量
            public int isSignUp { get; set; } //是否已报名	1=是，0=否
            public string eventName { get; set; } //活动中文名
            public string eventEnName { get; set; } //活动英文名
            public string userName { get; set; } //姓名
            public string userClass { get; set; } //班级
            public string userPhone { get; set; } //手机
            public string userEmail { get; set; } //邮件
            public IList<getSchoolEventListRespContentDataItem> eventLeven1List { get; set; } //活动集合
        }

        public class getSchoolEventListRespContentDataItem
        {
            public string eventId { get; set; } //标识
            public string displayIndex { get; set; } //排序
            public string eventLevel { get; set; } //次序
            public string eventName { get; set; } // 活动中文名
            public string eventEnName { get; set; } //活动英文名
            public string allowCount { get; set; } //可以报名的数量
            public string haveCount { get; set; } //已报名数量
            public string overCount { get; set; } //剩余数量
            public string isSignUp { get; set; } //是否报名
            public string parentId { get; set; } //父节点
            public IList<getSchoolEventListRespContentDataItem> eventLeven2List { get; set; } //活动集合
        }

        public class getSchoolEventListReqData : ReqData
        {
            public getSchoolEventListReqSpecialData special;
        }

        public class getSchoolEventListReqSpecialData
        {
            public string eventId { get; set; } //活动标识 10000
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
            public string userName { get; set; } //姓名
            public string userClass { get; set; } //班级
            public string userPhone { get; set; } //手机
            public string userEmail { get; set; } //邮件
            public string dataFrom { get; set; }
            public string company { get; set; } //单位
            public string position { get; set; } //职务
            public IList<SchoolEventListClass> eventList { get; set; }
        }

        public class SchoolEventListClass
        {
            public string eventId { get; set; } //活动标识
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
                var condition = new List<IWhereCondition>
                {
                    new EqualsCondition() {FieldName = "TextId", Value = reqObj.special.textId.ToString()}
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
                    scObj.Qty = scObj.Qty + reqObj.special.qty; //取消注释 by Willie Yan    购物车中同一SKU被多次添加应当累计
                    //scObj.Qty = reqObj.special.qty;   //注释 by Willie Yan    购物车中同一SKU被多次添加应当累计
                    if (scObj.Qty > 0)
                        service.Update(scObj, false);
                    else
                        service.Delete(scObj);
                }
                var cnt = service.GetShoppingCartByVipId(scObj.VipId);
                respData.content = new { count = cnt };
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
            public object content { get; set; }
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
                string skuIdList = "";
                if (list != null)
                {
                    respData.content.itemList = new List<getShoppingCartRespCourseData>();

                    foreach (var item in list)
                    {
                        skuIdList += "'" + item.SkuId + "',";
                    }
                    skuIdList = skuIdList.Trim(',');
                    if (!string.IsNullOrEmpty(skuIdList) && skuIdList != "")
                    {
                        var ggDs = service.GetShoppingGgBySkuId(skuIdList);
                        foreach (var item in list)
                        {
                            getShoppingCartRespCourseData resList = new getShoppingCartRespCourseData();
                            if (item.ItemDetail != null)
                            {
                                resList.itemId = ToStr(item.ItemDetail.item_id);
                                resList.itemName = ToStr(item.ItemDetail.item_name);
                                resList.imageUrl = ImagePathUtil.GetImagePathStr(ToStr(item.ItemDetail.imageUrl), "240"); //获取缩略图 update by Henry 2014-12-8
                                resList.price = ToDouble(item.ItemDetail.Price);
                                resList.salesPrice = ToDouble(item.ItemDetail.SalesPrice);
                                resList.discountRate = ToDouble(item.ItemDetail.DiscountRate);
                                resList.itemCategoryName = ToStr(item.ItemDetail.itemCategoryName);
                            }
                            resList.displayIndex = item.DisplayIndex;
                            resList.qty = ToInt(item.Qty);
                            resList.selDate = ToStr(item.LastUpdateTime);
                            resList.skuId = ToStr(item.SkuId);
                            //resList.gg = ToStr(item.GG);
                            resList.beginDate = ToStr(item.BeginDate);
                            resList.endDate = ToStr(item.EndDate);
                            resList.dayCount = ToInt(item.DayCount);
                            resList.salesPrice = ToDouble(item.SalesPrice);

                            //TO DO 如果渠道是人人销售 取人人销售价  add by donal 2014-9-23 11:08:53
                            if (reqObj.common.channelId == "6")
                            {
                                resList.salesPrice = ToDouble(item.EveryoneSalesPrice);
                            }

                            resList.gg =
                                ggDs.Tables[0].AsEnumerable()
                                    .Where(t => t["sku_id"].ToString() == item.SkuId.ToString())
                                    .Select(t => new GuiGeInfo()
                                    {
                                        PropName1 = t["prop_1_name"].ToString(),
                                        PropDetailName1 = t["prop_1_detail_name"].ToString(),
                                        PropName2 = t["prop_2_name"].ToString(),
                                        PropDetailName2 = t["prop_2_detail_name"].ToString(),
                                        PropName3 = t["prop_3_name"].ToString(),
                                        PropDetailName3 = t["prop_3_detail_name"].ToString(),
                                        PropName4 = t["prop_4_name"].ToString(),
                                        PropDetailName4 = t["prop_4_detail_name"].ToString(),
                                        PropName5 = t["prop_5_name"].ToString(),
                                        PropDetailName5 = t["prop_5_detail_name"].ToString()
                                    }).FirstOrDefault();

                            respData.content.itemList.Add(resList);

                            //添加规格对象
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

            /// <summary>
            ///  规格对象
            /// </summary>
            public GuiGeInfo gg { get; set; }

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

                var tempstatus = (reqObj.special.status ?? "").Split(',');
                List<InoutInfo> tempList = new List<InoutInfo> { };
                foreach (var item in tempstatus)
                {
                    var data = inoutService.SearchInoutInfo(
                        "", // order_no
                        order_reason_type_id,
                        "", //sales_unit_id,
                        "", //warehouse_id,
                        "", //purchase_unit_id,
                        "", //status,
                        reqObj.special.beginDate, //order_date_begin,
                        reqObj.special.endDate, //order_date_end,
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
                        item, //Field7, 
                        "", //DeliveryId, 
                        "", //DefrayTypeId, 
                        "", //Field9_begin, 
                        "", //Field9_end, 
                        "", //ModifyTime_begin, 
                        "", //ModifyTime_end
                        reqObj.special.orderId,
                        vipId, "", null, false);
                    tempList.Add(data);
                }

                int totalCount = tempList.Aggregate(0, (i, j) => i + j.ICount);
                var list = tempList.Aggregate(new List<InoutInfo> { }, (i, j) =>
                {
                    i.AddRange(j.InoutInfoList);
                    return i;
                });
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
                        orderItem.deliverType = item.Field8;
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
                        orderItem.couponsPrompt = ToStr(item.Field16);
                        orderItem.actualAmount = item.actual_amount; //ToStr(item.actual_amount.ToString("f2"));

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
            public string deliverType { get; set; }
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
            public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213）
            public decimal actualAmount { get; set; } //实际支付金额
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
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public string vipId { get; set; }
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

                var flag = inoutService.UpdateOrderDeliveryStatus(reqObj.special.orderId, reqObj.special.status,
                    Utils.GetNow(), reqObj.special.tableNo, null);
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
                    //判断是否是ALD的订单
                    var orderInfo = inoutService.GetInoutInfoById(reqObj.special.orderId);

                    #region 佣金处理 add by Henry 2014-11-26
                    //确认收货时，如果销售者(sales_user)不为空,则将商品佣金*购买的数量保存到余额表中
                    if (reqObj.special.status == "700" && !string.IsNullOrEmpty(orderInfo.sales_user))
                    {
                        var skuPriceBll = new SkuPriceService(loggingSessionInfo);              //sku价格
                        var vipAmountBll = new VipAmountBLL(loggingSessionInfo);                //账户余额 
                        decimal totalAmount = 0;                                                //订单总佣金
                        List<OrderDetail> orderDetailList = skuPriceBll.GetSkuPrice(orderInfo.order_id);
                        if (orderDetailList.Count > 0)
                        {
                            foreach (var detail in orderDetailList)
                            {
                                totalAmount += decimal.Parse(detail.salesPrice) * decimal.Parse(detail.qty);
                            }
                            if (totalAmount > 0)
                            {
                                IDbTransaction tran = new TransactionHelper(loggingSessionInfo).CreateTransaction();
                                using (tran.Connection)
                                {
                                    try
                                    {
                                        vipAmountBll.AddVipEndAmount(reqObj.common.userId, totalAmount, tran, "10", orderInfo.order_id, loggingSessionInfo);  //变更余额和余额记录
                                    }
                                    catch (Exception ex)
                                    {
                                        tran.Rollback();
                                        throw new APIException(ex.Message);
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region 订单状态同步到ALD
                    if (orderInfo.Field3 == "1")
                    {
                        var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                        var request = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderRequest()
                        {
                            Parameters =
                                new
                                {
                                    SourceOrdersID = reqObj.special.orderId,
                                    Status = reqObj.special.status,
                                    MemberID = orderInfo.vip_no
                                }
                        };
                        try
                        {
                            var resstr = JIT.Utility.Web.HttpClient.GetQueryString(url,
                                string.Format("Action=ChangeOrderStatus&ReqContent={0}", request.ToJSON()));
                            Loggers.Debug(new DebugLogInfo() { Message = "调用ALD更改状态接口:" + resstr });
                            var res =
                                resstr
                                    .DeserializeJSONTo
                                    <JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                            if (!res.IsSuccess())
                            {
                                respData.code = "105";
                                respData.description = res.Message;
                            }
                        }
                        catch (Exception ex)
                        {
                            Loggers.Exception(new ExceptionLogInfo(ex));
                            throw new Exception("调用ALD平台失败:" + ex.Message);
                        }
                    }
                    #endregion
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

        public string setOrderStatus4ALD()
        {
            string content = string.Empty;
            var respData = new setOrderStatusRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderStatus4ALD: {0}", reqContent)
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

                var flag = inoutService.UpdateOrderDeliveryStatus(reqObj.special.orderId, reqObj.special.status,
                    Utils.GetNow(), reqObj.special.tableNo, null);
                if (!flag)
                {
                    respData.code = "103";
                    respData.description = "更新订单状态失败";
                }
                else
                {
                    var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                    var request = new JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDOrderRequest()
                    {
                        Parameters =
                            new
                            {
                                SourceOrdersID = reqObj.special.orderId,
                                Status = reqObj.special.status,
                                MemberID = reqObj.common.userId
                            }
                    };
                    try
                    {
                        var resstr = JIT.Utility.Web.HttpClient.GetQueryString(url,
                            string.Format("Action=ChangeOrderStatus&ReqContent={0}", request.ToJSON()));
                        var res =
                            resstr
                                .DeserializeJSONTo
                                <JIT.CPOS.Web.OnlineShopping.data.DataOnlineShoppingHandler.ALDResponse>();
                        if (!res.IsSuccess())
                        {
                            respData.code = "105";
                            respData.description = res.Message;
                        }
                    }
                    catch (Exception ex)
                    {
                        Loggers.Exception(new ExceptionLogInfo(ex));
                        throw new Exception("调用ALD平台失败:" + ex.Message);
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
                vipObj.VipSourceId = reqObj.special.vipSource;
                vipObj.UserName = reqObj.special.phone;
                vipObj.WeiXinUserId = reqObj.common.userId;
                vipObj.Phone = reqObj.special.phone;
                vipObj.VipPasswrod = reqObj.special.password;
                vipObj.VipCode = vipBLL.GetVipCode();
                vipObj.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                vipObj.Status = 1;
                vipBLL.Create(vipObj);
                respData.content.userId = vipObj.VIPID;
                respData.content.username = vipObj.UserName;
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
            public string username { get; set; }
        }

        public class setSignUpReqData : ReqData
        {
            public setSignUpReqSpecialData special;
        }

        public class setSignUpReqSpecialData
        {
            public string phone { get; set; }
            public string password { get; set; }

            /// <summary>
            /// VIP来源
            /// <remarks>
            /// <para>Ø  1=会员APP    </para>
            /// <para>Ø  2=门店PAD    </para>
            /// <para>Ø  3=微信       </para>
            /// <para>Ø  4=活动二维码 </para>
            /// <para>Ø  5=电话客服   </para>
            /// <para>Ø  6=导入数据   </para>
            /// <para>Ø  7=微博       </para>
            /// <para>Ø  8=网站注册   </para>
            /// <para>Ø  9=门店二维码 </para>
            /// <para>Ø  10=微信转发  </para>
            /// <para>Ø  11=阿拉丁平台</para>
            /// </remarks>
            /// </summary>
            public string vipSource { get; set; }
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

                    #region VIP来源更新

                    switch (reqObj.special.vipSource)
                    {
                        case "4":
                        case "9":
                            vip.VipSourceId = reqObj.special.vipSource;
                            vipBLL.Update(vip);
                            break;
                    }

                    #endregion
                    decimal EndAmount = 0;
                    VipAmountBLL AmountBLL = new VipAmountBLL(loggingSessionInfo);
                    VipAmountEntity amountEntity = new VipAmountEntity();
                    amountEntity.VipId = vip.VIPID;
                    var tempAmountEntity = AmountBLL.QueryByEntity(amountEntity, null);
                    if (tempAmountEntity.Length > 0)
                    {
                        EndAmount = tempAmountEntity.FirstOrDefault().EndAmount.HasValue ? Convert.ToDecimal(tempAmountEntity.FirstOrDefault().EndAmount) : 0;
                    }
                    respData.content = new { userId = vip.VIPID, username = vip.UserName, phone = vip.Phone, headURL = vip.HeadImgUrl, balance = EndAmount, integral = vip.Integration };
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
            public object content { get; set; }
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

            /// <summary>
            /// VIP来源
            /// <remarks>
            /// <para>Ø  1=会员APP    </para>
            /// <para>Ø  2=门店PAD    </para>
            /// <para>Ø  3=微信       </para>
            /// <para>Ø  4=活动二维码 </para>
            /// <para>Ø  5=电话客服   </para>
            /// <para>Ø  6=导入数据   </para>
            /// <para>Ø  7=微博       </para>
            /// <para>Ø  8=网站注册   </para>
            /// <para>Ø  9=门店二维码 </para>
            /// <para>Ø  10=微信转发  </para>
            /// <para>Ø  11=阿拉丁平台</para>
            /// </remarks>
            /// </summary>
            public string vipSource { get; set; }

            /// <summary>
            /// 头像地址
            /// </summary>
            public string headURL { get; set; }

            /// <summary>
            /// 余额
            /// </summary>
            public decimal balance { get; set; }

            /// <summary>
            /// 积分
            /// </summary>
            public decimal integral { get; set; }
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
                CityInfo cityInfo5 = new CityInfo();
                cityInfo5.city = "云南";
                respData.content.cityList.Add(cityInfo5);
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
            public IList<CityInfo> cityList { get; set; } //商品集合
        }

        public class CityInfo
        {
            public string city { get; set; } //商品标识
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
                    , reqObj.special.beginDate
                    , reqObj.special.endDate
                    , out strError);
                if (strError.Equals("ok"))
                {
                    IList<getStoreListByCityRespContentItemTypeData> list =
                        new List<getStoreListByCityRespContentItemTypeData>();
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
                        info.unitTypeContent = ToStr(store.UnitTypeContent);
                        info.minPrice = ToStr(store.MinPrice);
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
            public IList<getStoreListByCityRespContentItemTypeData> storeList { get; set; } //商品类别集合
        }

        public class getStoreListByCityRespContentItemTypeData
        {
            public string storeId { get; set; } //支付方式标识
            public string storeName { get; set; } //支付产品类别
            public string imageURL { get; set; }
            public string displayIndex { get; set; }
            public string address { get; set; }
            public string tel { get; set; }
            public string distance { get; set; } //距离
            public string unitTypeContent { get; set; } //门店类型说明
            public string minPrice { get; set; } //价格起始
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
            //added by zhangwei for hotel
            public string beginDate { get; set; }
            public string endDate { get; set; }
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
                DateTime maxdt =
                    DateTime.Parse(
                        DateTime.Parse(DateTime.Now.AddMonths(3).ToString("yyyy-MM") + "-01")
                            .AddDays(-1)
                            .ToString("yyyy-MM-dd"));
                //GetDate(mindt, maxdt); //循环打印这个月日期
                while (mindt <= maxdt)
                {
                    getDateListRespContentItemTypeData dataInfo = new getDateListRespContentItemTypeData();
                    //Response.Write(mindt + "");
                    dataInfo.strDate = mindt.ToString("yyyy-MM-dd");
                    dataInfo.strYear = mindt.ToString("yyyy");
                    dataInfo.strMonth = mindt.ToString("MM");
                    dataInfo.week = getWeekDay(ToInt(dataInfo.strYear), ToInt(dataInfo.strMonth),
                        ToInt(mindt.ToString("dd")));
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
            public IList<getDateListRespContentItemTypeData> dateList { get; set; } //商品类别集合
        }

        public class getDateListRespContentItemTypeData
        {
            public string strDate { get; set; } //支付方式标识
            public int week { get; set; } //支付产品类别
            public string strYear { get; set; } //产品logo
            public string strMonth { get; set; } //支付描述
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
            public string phone { get; set; } //手机号
            public string qty { get; set; } //数量
            public string address { get; set; } //地址
            public string lng { get; set; } //经度
            public string lat { get; set; } //维度
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
                        bool bReturn = service.GetReceiptConfirm(ToStr(reqObj.special.orderId),
                            ToStr(reqObj.common.userId), ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim(),
                            out strError);
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
            public string orderId { get; set; } //手机号
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
            public string isPraise { get; set; } //是否赞过
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
            public int isMe { get; set; } //是否要查询我的 1=是，0=否
            public int isNewest { get; set; } //是否最新更新 1=是，0=否
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
            public string isPraise { get; set; } //是否赞过
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
            public string experience { get; set; } //心得

            public IList<setVipShowImage> imageList { get; set; }
        }

        public class setVipShowImage
        {
            public string imageURL { get; set; } //图片地址标识
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
                string msgSMSText = "" + vipName + "服装提醒您，重置密码请点击：" + webUrl +
                                    "OnlineClothing/forgetpwd.html?customerId=" + customerId + "";
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
                    basicInfo1.Channel = ToStr(reqObj.common.channelId);
                    basicInfo1.Plat = ToStr(reqObj.common.plat);
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.DeviceToken = ToStr(reqObj.special.deviceToken);
                    basicInfo.Channel = ToStr(reqObj.common.channelId);
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
                    basicInfo1.Channel = ToStr(reqObj.common.channelId);
                    basicInfo1.ChannelIDBaiDu = ToStr(reqObj.special.channelId);
                    basicInfo1.CustomerId = ToStr(customerId);
                    basicInfo1.BaiduPushAppID = ToStr(reqObj.special.appId);
                    basicInfo1.UserIDBaiDu = ToStr(reqObj.special.userId);
                    basicInfo1.Plat = ToStr(reqObj.common.plat);
                    service.Create(basicInfo1);
                }
                else
                {
                    basicInfo.ChannelIDBaiDu = ToStr(reqObj.special.channelId);
                    basicInfo.BaiduPushAppID = ToStr(reqObj.special.appId);
                    basicInfo.UserIDBaiDu = ToStr(reqObj.special.userId);
                    basicInfo.Channel = ToStr(reqObj.common.channelId);
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
                newVersionInfo = versionManagerServer.GetNewVersionEntity(reqObj.special.plat, reqObj.special.channel,
                    reqObj.special.version, reqObj.common.userId);
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
            public string isNewVersionAvailable { get; set; } //是否有新版本可用  0、1
            public string message { get; set; } //文本格式。提示信息：有新版本提供下载。增加了****等功能
            public string canSkip { get; set; } //该版本是否可被忽略不强制下载。1：可忽略，0：不可忽略
            public string updateUrl { get; set; } //升级的url
            public string version { get; set; }
        }

        public class checkVersionReqData : ReqData
        {
            public checkVersionReqSpecialData special;
        }

        public class checkVersionReqSpecialData
        {
            public string plat { get; set; } //版本：iPhone, iPad, Android
            public string channel { get; set; } //渠道	1=企业版，0=市场版
            public string version { get; set; } //当前版本
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
                            newsInfo.noHtmlContent =
                                (DataTableToObject.NoHTML(newsInfo.content)).Replace("\n", "").Replace("\t", "");
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

        #region 2.9.13 活动中奖名单（Jermyn20131211）

        public string getEventWinnerList()
        {
            string content = string.Empty;
            var respData = new getEventWinnerListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventWinnerList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEventWinnerListReqData>();
                reqObj = reqObj == null ? new getEventWinnerListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getEventWinnerListReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
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
                    respData.description = "活动标识不能为空";
                    return respData.ToJSON().ToString();
                }

                respData.content = new getEventWinnerListRespContentData();
                LPrizeWinnerBLL service = new LPrizeWinnerBLL(loggingSessionInfo);
                LPrizeWinnerEntity winnerInfo = new LPrizeWinnerEntity();
                winnerInfo = service.GetPrizeWinnerListByEventId(reqObj.special.eventId, reqObj.special.page,
                    reqObj.special.pageSize);
                int totalCount = winnerInfo.ICount;
                IList<LPrizeWinnerEntity> list = winnerInfo.PrizeWinnerList;
                respData.content.isNext = "0";
                if (totalCount > 0 && reqObj.special.page > 0 &&
                    totalCount > (reqObj.special.page * reqObj.special.pageSize))
                {
                    respData.content.isNext = "1";
                }
                if (list != null)
                {
                    respData.content.winnerList = new List<getEventWinnerListRespVipShowData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getEventWinnerListRespVipShowData()
                        {
                            vipName = ToStr(item.VipName),
                            winningDesc = ToStr(item.PrizeDesc),
                        };
                        respData.content.winnerList.Add(tmpItemObj);
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

        public class getEventWinnerListRespData : Default.LowerRespData
        {
            public getEventWinnerListRespContentData content { get; set; }
        }

        public class getEventWinnerListRespContentData
        {
            public string isNext { get; set; }
            public IList<getEventWinnerListRespVipShowData> winnerList { get; set; }
        }

        public class getEventWinnerListRespVipShowData
        {
            public string vipName { get; set; }
            public string winningDesc { get; set; }
        }

        public class getEventWinnerListReqData : ReqData
        {
            public getEventWinnerListReqSpecialData special;
        }

        public class getEventWinnerListReqSpecialData
        {
            public string eventId { get; set; }
            public int page { get; set; }
            public int pageSize { get; set; }
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
                string eventID = reqObj.special.EventId; //活动ID
                string longitude = reqObj.special.Longitude; //经度
                string latitude = reqObj.special.Latitude; //纬度
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
                    EventId = eventID
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
            public string isSite; //是否在现场		1=是，0=否
            public string isLottery; //是否抽奖 1= 是，0=否
            public string isWinning; //是否中奖  1= 是，0=否
            public string winningDesc; //奖品名称
            public string winningExplain; //奖品说明 Jermyn20131017
            public string eventRound; //第几轮 （如果为0或者空，则代表不在抽奖范围）
            public IList<PrizesEntity> prizes; //奖品集合
            public IList<getEventPrizesWinnerList> winnerList;
        }

        public class PrizesEntity
        {
            public string prizesID; //奖品标识
            public string prizeName; //奖品名称
            public string prizeDesc; //奖品描述
            public string displayIndex; //排序
            public string countTotal; //奖品数量
            public string imageUrl; //图片
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
            public string EventId; //活动标识
            public string Longitude; //经度
            public string Latitude; //纬度
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
                    if (roundList != null && roundList.Length > 0 && roundList[0] != null &&
                        roundList[0].RoundId != null)
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
                IList<getMarketEventAnalysisRespPrizeListData> List =
                    new List<getMarketEventAnalysisRespPrizeListData>();
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
                        info.prizeBrand = ToStr(prizesBrandInfo.PrizeShortDesc) + " " + ToStr(prizesBrandInfo.PrizeDesc);
                        info.prizeDesc = ToStr(prizesBrandInfo.PrizeShortDesc) + " " + ToStr(prizesBrandInfo.PrizeDesc);
                        info.imageUrl = ToStr(prizesBrandInfo.LogoURL);
                        info.prizeWinnerList = new List<PrizeWinnerListRespData>();
                        info.id = ToStr(i + 1);
                        var winnerList = serverWinner.GetPrizesWinnerByGroupBrand(ToStr(prizesBrandInfo.PrizeName),
                            EventId, Convert.ToInt64(timestamp), roundId);
                        if (winnerList != null && winnerList.Count > 0)
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

                if (respData.content.timestamp == null || respData.content.timestamp.Equals("0"))
                {
                    LPrizeWinnerBLL serverWinner = new LPrizeWinnerBLL(loggingSessionInfo);
                    respData.content.timestamp = ToStr(serverWinner.GetMaxTimestamp(EventId, roundId));
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
            public string storeCount { get; set; } //参与门店数量
            public string responseStoreCount { get; set; } //响应门店数量
            public string responseStoreRate { get; set; } //门店响应率
            public string personCount { get; set; } //邀约人数
            public string pesponsePersonCount { get; set; } //响应人数
            public string responsePersonRate { get; set; } //会员响应率
            public string eventId { get; set; } //活动标识
            public string timestamp { get; set; } //时间戳
            public IList<getMarketEventAnalysisRespPrizeListData> prizeList { get; set; } //中奖名单集合
            public IList<getMarketEventAnalysisRespRoundListData> roundList { get; set; } //轮次集合
        }

        public class getMarketEventAnalysisRespPrizeListData
        {
            public string prizeBrand { get; set; } //奖品名称
            public string prizeDesc { get; set; } //奖品描述
            public string imageUrl { get; set; } //图片链接
            public string id { get; set; }
            public IList<PrizeWinnerListRespData> prizeWinnerList { get; set; } //获奖名单
        }

        public class getMarketEventAnalysisRespRoundListData
        {
            public string roundId { get; set; } //轮次标识
            public string roundDesc { get; set; } //轮次说明
            public int roundStatus { get; set; } //轮次状态
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
            public string orderNo { get; set; } //订单编号
            public string itemCount { get; set; } //商品数量
            public string createTime { get; set; } //交易时间
            public string phone { get; set; } //手机号
            public string totalAmount { get; set; } //交易金额
            public string vipName { get; set; }
            public string deliveryName { get; set; } //配送方式
            public string address { get; set; } //地址
        }

        public class getOnlinePosOrderReqData : ReqData
        {
            public getOnlinePosOrderReqSpecialData special;
        }

        public class getOnlinePosOrderReqSpecialData
        {
            public string storeId { get; set; } //门店标识
            public string status { get; set; } //订单状态
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

                #region 解析传入参数

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
                loggingSessionInfo.UserID = ToStr(reqObj.common.userId);    //Jermyn20140603 判断导购员，对应保存到CreateBy
                info.ImageUrl = imageUrl;
                info.DCodeType = Convert.ToInt32(reqObj.special.VipDCode);
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

            /// <summary>
            /// 二维码类型
            /// </summary>
            public string VipDCode { get; set; } // add by donal 2014-9-22 13:19:37
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
                //Updated By Willie Yan on 2014-05-06   由于CodeId有重复的概率，因此只取出最新的一条记录
                info = vipDCodeServer.QueryByEntity(
                    new VipDCodeEntity() { DCodeId = ToStr(reqObj.special.paraTmp) }
                    , new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }
                    )[0];
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
                        #region Jermyn20140603 处理会员的会籍店，导购员
                        VipUnitMappingBLL vipUnitServer = new VipUnitMappingBLL(loggingSessionInfo);
                        //先删除现有的关系
                        vipUnitServer.SetDeleteByVipId(vipId);
                        //建立新的会员与门店与导购员关系
                        VipUnitMappingEntity vipUnitInfo = new VipUnitMappingEntity();
                        vipUnitInfo.UnitId = info.UnitId;
                        vipUnitInfo.VipUnitID = Guid.NewGuid().ToString().Replace("-", string.Empty);
                        vipUnitInfo.VIPID = vipId;
                        vipUnitInfo.CreateBy = vipId;
                        vipUnitInfo.UserId = info.CreateBy;

                        vipUnitServer.Create(vipUnitInfo);

                        #endregion
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
                string strState = reqObj.special.customerId + "," + reqObj.special.applicationId + "," +
                                  reqObj.special.userId + "," + reqObj.special.openId + "," + reqObj.special.eventId +
                                  "," + reqObj.special.goUrl; //
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

        #region 优惠券 qianzhi20131214

        #region 3.4.1 用户优惠券查询接口  qianzhi20131214

        public string myCouponList()
        {
            string content = string.Empty;
            var respData = new myCouponListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("myCouponList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<myCouponListReqData>();
                reqObj = (reqObj == null) ? new myCouponListReqData() : reqObj;

                if (reqObj.common == null)
                {
                    respData.code = "301";
                    respData.description = "没有基本参数";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new myCouponListRespContentData();
                respData.content.couponList = new List<myCouponListRespVipShowData>();

                var server = new CouponBLL(loggingSessionInfo);
                var vipId = ToStr(reqObj.common.userId);
                var ds = server.GetMyCouponList(vipId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.couponList =
                        DataTableToObject.ConvertToList<myCouponListRespVipShowData>(ds.Tables[0]);
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

        public class myCouponListRespData : Default.LowerRespData
        {
            public myCouponListRespContentData content { get; set; }
        }

        public class myCouponListRespContentData
        {
            public IList<myCouponListRespVipShowData> couponList { get; set; }
        }

        public class myCouponListRespVipShowData
        {
            public string couponType { get; set; } //优惠券类型
            public decimal parValue { get; set; } //面值
            public decimal conditionValue { get; set; } //使用限制
            public string couponSource { get; set; } //获取方式
        }

        public class myCouponListReqData : ReqData
        {
            public myCouponListReqSpecialData special;
        }

        public class myCouponListReqSpecialData
        {
        }

        #endregion

        #region 3.4.2 订单使用的优惠券总计查询接口  qianzhi20131214

        public string orderCouponSum()
        {
            string content = string.Empty;
            var respData = new orderCouponSumRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("orderCouponSum: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<orderCouponSumReqData>();
                reqObj = (reqObj == null) ? new orderCouponSumReqData() : reqObj;

                if (reqObj.common == null)
                {
                    respData.code = "301";
                    respData.description = "没有基本参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special == null)
                {
                    respData.code = "302";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.orderId))
                {
                    respData.code = "303";
                    respData.description = "订单ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new orderCouponSumRespContentData();

                var server = new CouponBLL(loggingSessionInfo);
                var vipId = ToStr(reqObj.common.userId);
                var orderId = ToStr(reqObj.special.orderId);
                var loadFlag = ToStr(reqObj.special.loadFlag);

                if (loadFlag.Equals("0"))
                {
                    var ds = server.CheckCouponForOrder(vipId, orderId, string.Empty);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        int amount = 0;
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            amount += Convert.ToInt32(item["ParValue"]);
                        }

                        respData.content.count = ds.Tables[0].Rows.Count;
                        respData.content.amount = amount;
                    }
                }
                else if (loadFlag.Equals("1"))
                {
                    var ds = server.OrderCouponSum(vipId, orderId);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        respData.content =
                            DataTableToObject.ConvertToObject<orderCouponSumRespContentData>(ds.Tables[0].Rows[0]);
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

        public class orderCouponSumRespData : Default.LowerRespData
        {
            public orderCouponSumRespContentData content { get; set; }
        }

        public class orderCouponSumRespContentData
        {
            public int count { get; set; } //使用的优惠券使用总数
            public decimal amount { get; set; } //使用的优惠券价值总数
        }

        public class orderCouponSumReqData : ReqData
        {
            public orderCouponSumReqSpecialData special;
        }

        public class orderCouponSumReqSpecialData
        {
            public string orderId { get; set; } //订单ID
            public string loadFlag { get; set; } //加载标识 0：初始  1：用户选择
        }

        #endregion

        #region 3.4.3 订单使用的优惠券列表查询接口  qianzhi20131214

        public string orderCouponList()
        {
            string content = string.Empty;
            var respData = new orderCouponListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("orderCouponList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<orderCouponListReqData>();
                reqObj = (reqObj == null) ? new orderCouponListReqData() : reqObj;

                if (reqObj.common == null)
                {
                    respData.code = "301";
                    respData.description = "没有基本参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special == null)
                {
                    respData.code = "302";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.orderId))
                {
                    respData.code = "303";
                    respData.description = "订单ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new orderCouponListRespContentData();
                respData.content.couponList = new List<orderCouponListRespVipShowData>();

                var server = new CouponBLL(loggingSessionInfo);
                var vipId = ToStr(reqObj.common.userId);
                var orderId = ToStr(reqObj.special.orderId);
                var ds = server.OrderCouponList(vipId, orderId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.couponList =
                        DataTableToObject.ConvertToList<orderCouponListRespVipShowData>(ds.Tables[0]);
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

        public class orderCouponListRespData : Default.LowerRespData
        {
            public orderCouponListRespContentData content { get; set; }
        }

        public class orderCouponListRespContentData
        {
            public IList<orderCouponListRespVipShowData> couponList { get; set; }
        }

        public class orderCouponListRespVipShowData
        {
            public string couponId { get; set; } //优惠券ID
            public int isChecked { get; set; } //是否已选中
            public string couponType { get; set; } //优惠券类型
            public decimal parValue { get; set; } //面值
            public decimal conditionValue { get; set; } //使用限制
            public string couponSource { get; set; } //获取方式
        }

        public class orderCouponListReqData : ReqData
        {
            public orderCouponListReqSpecialData special;
        }

        public class orderCouponListReqSpecialData
        {
            public string orderId { get; set; } //订单ID
        }

        #endregion

        #region 3.4.4 订单中取消使用优惠券接口  qianzhi20131214

        public string cancelCoupon()
        {
            string content = string.Empty;
            var respData = new cancelCouponRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("cancelCoupon: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<cancelCouponReqData>();
                reqObj = (reqObj == null) ? new cancelCouponReqData() : reqObj;

                if (reqObj.common == null)
                {
                    respData.code = "301";
                    respData.description = "没有基本参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special == null)
                {
                    respData.code = "302";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.orderId))
                {
                    respData.code = "303";
                    respData.description = "订单ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var server = new CouponBLL(loggingSessionInfo);
                var orderId = ToStr(reqObj.special.orderId);
                var couponId = ToStr(reqObj.special.couponId);

                server.CancelCouponMapping(orderId, couponId);

                if (string.IsNullOrEmpty(couponId))
                {
                    server.CancelCouponOrder(orderId);
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

        public class cancelCouponRespData : Default.LowerRespData
        {
        }

        public class cancelCouponReqData : ReqData
        {
            public cancelCouponReqSpecialData special;
        }

        public class cancelCouponReqSpecialData
        {
            public string orderId { get; set; } //订单ID
            public string couponId { get; set; } //优惠券ID
        }

        #endregion

        #region 3.4.5 订单中选择优惠券接口  qianzhi20131214

        public string selectCoupon()
        {
            string content = string.Empty;
            var respData = new selectCouponRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("selectCoupon: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<selectCouponReqData>();
                reqObj = (reqObj == null) ? new selectCouponReqData() : reqObj;

                if (reqObj.common == null)
                {
                    respData.code = "301";
                    respData.description = "没有基本参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special == null)
                {
                    respData.code = "302";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.orderId))
                {
                    respData.code = "303";
                    respData.description = "订单ID不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.couponId))
                {
                    respData.code = "304";
                    respData.description = "优惠券ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                respData.content = new selectCouponRespContentData();

                var server = new CouponBLL(loggingSessionInfo);
                var vipId = ToStr(reqObj.common.userId);
                var orderId = ToStr(reqObj.special.orderId);
                var couponId = ToStr(reqObj.special.couponId);

                var ds = server.CheckCouponForOrder(vipId, orderId, couponId);

                respData.content.result = "0";
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    respData.content.result = "1";
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

        public class selectCouponRespData : Default.LowerRespData
        {
            public selectCouponRespContentData content { get; set; }
        }

        public class selectCouponRespContentData
        {
            public string result { get; set; } //0: 不能使用；1：可以使用
        }

        public class selectCouponReqData : ReqData
        {
            public selectCouponReqSpecialData special;
        }

        public class selectCouponReqSpecialData
        {
            public string orderId { get; set; } //订单ID
            public string couponId { get; set; } //优惠券ID
        }

        #endregion

        #endregion

        #region getEventPrizesBySales 消费抽奖

        /// <summary>
        /// 获取活动奖项信息（缺中奖的存储过程，稍后实现）
        /// </summary>
        public string getEventPrizesBySales()
        {
            string content = string.Empty;
            var respData = new getEventPrizesBySalesRespData();
            try
            {
                string reqContent = Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<getEventPrizesBySalesReqData>();

                string OpenID = reqObj.common.openId;
                string WeiXin = reqObj.common.weiXinId;
                string eventID = reqObj.special.eventId; //活动ID
                if (eventID == null || eventID.Equals(""))
                {
                    eventID = "BFC41A8BF8564B6DB76AE8A8E43557BA";
                }
                string longitude = reqObj.special.longitude; //经度
                string latitude = reqObj.special.latitude; //纬度
                string vipID = string.Empty;
                string vipName = string.Empty;
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getEventPrizesBySales: {0}", reqContent)
                });
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //var loggingSessionInfo = Default.GetLjLoggingSession();
                Default.WriteLog(loggingSessionInfo, "getEventPrizesBySales", reqObj, respData, reqObj.special.ToJSON());

                respData.content = new getEventPrizesBySalesRespContentData();
                respData.content.prizes = new List<PrizesEntityBySales>();
                respData.content.prizeList = new List<PrizesEntity>();

                #region 是否在活动现场

                respData.content.isSite = "1";

                #endregion

                #region 获取VIPID

                VipBLL vipService = new VipBLL(loggingSessionInfo);
                var vipList = vipService.QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = OpenID
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
                    vipName = vipList.FirstOrDefault().VipName;
                }

                #endregion

                #region 是否抽奖

                LEventsVipObjectBLL lotteryService = new LEventsVipObjectBLL(loggingSessionInfo);
                IList<LEventsVipObjectEntity> LotteryList = new List<LEventsVipObjectEntity>();
                if (ToStr(reqObj.special.eventId).Equals(""))
                {
                    LotteryList = lotteryService.QueryByEntity(new LEventsVipObjectEntity()
                    {
                        VipId = vipID
                        ,
                        IsLottery = 0
                        ,
                        IsDelete = 0
                    }, null);
                }
                else
                {
                    //added by zhangwei 20140110 泸州老窖天天有礼
                    if (reqObj.special.eventId.ToUpper() == "BFC41A8BF8564B6DB76AE8A8E43557BA")
                    {
                        LotteryList = lotteryService.QueryObjectList(eventID, vipID);
                        if (LotteryList.Count == 0)
                        {
                            LEventsVipObjectEntity entity = new LEventsVipObjectEntity();
                            entity.MappingId = Guid.NewGuid().ToString();
                            entity.EventId = ToStr(reqObj.special.eventId);
                            entity.VipId = vipID;
                            entity.IsCheck = 0;
                            entity.IsLottery = 0;
                            entity.IsDelete = 0;
                            lotteryService.Create(entity);
                        }
                    }
                    LotteryList = lotteryService.QueryByEntity(new LEventsVipObjectEntity()
                    {
                        VipId = vipID
                        ,
                        IsLottery = 0
                        ,
                        IsDelete = 0
                        ,
                        EventId = ToStr(reqObj.special.eventId)
                    }, null);
                }

                #region 中奖奖品信息

                LPrizesBLL prizesService = new LPrizesBLL(loggingSessionInfo);

                var prizesList = prizesService.GetEventPrizesByVipId(reqObj.special.eventId, vipID);

                if (prizesList != null && prizesList.Count > 0)
                {
                    foreach (var item in prizesList)
                    {
                        var entity = new PrizesEntityBySales()
                        {
                            prizesID = item.PrizesID,
                            prizeName = item.PrizeName,
                            prizeDesc = item.PrizeDesc,
                            displayIndex = item.DisplayIndex.ToString(),
                            countTotal = item.CountTotal.ToString()
                            ,
                            imageUrl = item.ImageUrl
                            ,
                            sponsor = item.ContentText
                        };

                        respData.content.prizes.Add(entity);
                    }
                }

                #endregion

                #region 奖项信息 added by zhangwei

                var prizesList1 = prizesService.QueryByEntity(new LPrizesEntity() { EventId = eventID },
                    new OrderBy[] { new OrderBy { FieldName = " DisplayIndex ", Direction = OrderByDirections.Asc } });

                if (prizesList1 != null && prizesList1.Length > 0)
                {
                    foreach (var item in prizesList1)
                    {
                        var entity = new PrizesEntity()
                        {
                            prizesID = item.PrizesID,
                            prizeName = item.PrizeName,
                            prizeDesc = item.PrizeDesc,
                            displayIndex = item.DisplayIndex.ToString(),
                            countTotal = item.CountTotal.ToString(),
                            imageUrl = item.ImageUrl
                        };

                        respData.content.prizeList.Add(entity);
                    }
                }

                #endregion

                if (LotteryList != null && LotteryList.Count > 0)
                {
                    respData.content.isLottery = "1"; //Jermyn20131225
                    LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);
                    GetResponseParams<JIT.CPOS.BS.Entity.Interface.ShakeOffLotteryResult> returnDataObj = poolsServer
                        .SetShakeOffLotteryBySales(
                            vipName,
                            vipID,
                            eventID,
                            ToFloat(longitude),
                            ToFloat(latitude));

                    #region 是否中奖

                    if (returnDataObj.Params.result_code.Equals("1")) //中奖这次是隐藏，否则，就是显示刮奖
                    {
                        respData.content.isLottery = "0"; ////Jermyn20131108 没有中奖，一直能刮奖
                        respData.content.isWinning = "1";
                        respData.content.winningDesc = returnDataObj.Params.resultPrizeName.ToString();
                        respData.content.resultMessage = returnDataObj.Params.result_message;
                        //TODO:added by zhangwei 如果是泸州老窖，判断是否是优惠券、积分并插入会员积分或优惠券表待稳定后扩展到所有客户
                        if (customerId == "e703dbedadd943abacf864531decdac1")
                        {
                            CouponBLL couponService = new CouponBLL(loggingSessionInfo);
                            couponService.SetEventPrizes(vipID, eventID);
                        }
                    }
                    else
                    {
                        respData.content.isLottery = "1";
                        respData.content.isWinning = "0";
                        respData.content.winningDesc = "谢谢你";
                        //respData.content.isLottery = "0";
                    }

                    #endregion
                }
                else
                {
                    respData.content.isWinning = "0";
                    respData.content.winningDesc = "谢谢你";
                    respData.content.isLottery = "0";
                }

                //LPrizeWinnerBLL winnerService1 = new LPrizeWinnerBLL(loggingSessionInfo);
                //var prizeName1 = winnerService1.GetWinnerInfo(vipID, eventID);

                //if (prizeName1 == null)
                //{//没中奖，就一直能刮奖，直到能中奖为止

                //Jermyn20131108 抽奖
                //LPrizePoolsBLL poolsServer = new LPrizePoolsBLL(loggingSessionInfo);
                //GetResponseParams<JIT.CPOS.BS.Entity.Interface.ShakeOffLotteryResult> returnDataObj = poolsServer.SetShakeOffLottery(
                //vipName,
                //vipID,
                //eventID,
                //ToFloat(longitude),
                //ToFloat(latitude));
                //}
                //if (returnDataObj.Params.result_code.Equals("1")) //中奖这次是隐藏，否则，就是显示刮奖
                //{
                //    respData.content.isLottery = "0"; ////Jermyn20131108 没有中奖，一直能刮奖
                //}
                //else
                //{
                //    respData.content.isLottery = "1";
                //}
                //}
                //else
                //{
                //    respData.content.isLottery = "1";
                //}

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

        public class getEventPrizesBySalesRespData : Default.LowerRespData
        {
            public getEventPrizesBySalesRespContentData content;
        }

        public class getEventPrizesBySalesRespContentData
        {
            public string isSite; //是否在现场		1=是，0=否
            public string isLottery; //是否抽奖 1= 是，0=否
            public string isWinning; //是否中奖  1= 是，0=否
            public string winningDesc; //奖品名称
            public string resultMessage; //中奖秒睡
            public IList<PrizesEntityBySales> prizes; //奖品集合
            public IList<PrizesEntity> prizeList; //奖项集合
        }

        public class PrizesEntityBySales
        {
            public string prizesID; //奖品标识
            public string prizeName; //奖品名称
            public string prizeDesc; //奖品描述
            public string displayIndex; //排序
            public string countTotal; //奖品数量
            public string imageUrl; //图片
            public string sponsor; //赞助对应ContentText
        }

        public class getEventPrizesBySalesReqData : Default.ReqData
        {
            public getEventPrizesBySalesReqSpecialData special;
        }

        public class getEventPrizesBySalesReqSpecialData
        {
            public string eventId; //活动标识
            public string longitude; //经度
            public string latitude; //纬度
        }

        #endregion

        #region 获取点评列表

        /// <summary>
        /// 获取点评列表
        /// </summary>
        /// <returns></returns>
        public string getUnitCommentList()
        {
            string content = string.Empty;
            var respData = new getUnitCommentListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getUnitCommentList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getUnitCommentListReqData>();
                reqObj = reqObj == null ? new getUnitCommentListReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getUnitCommentListReqSpecialData();
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
                int page = reqObj.special.page; //页码
                int pageSize = reqObj.special.pageSize; //页面数量
                string storeId = ToStr(reqObj.special.storeId);
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 15;

                //初始化返回对象
                respData.content = new getUnitCommentListRespContentData();
                UnitCommentBLL unitCommentBLL = new UnitCommentBLL(loggingSessionInfo);

                UnitCommentEntity queryEntity = new UnitCommentEntity();
                queryEntity.UnitId = reqObj.special.storeId;

                var list = unitCommentBLL.GetList(queryEntity, page, pageSize);
                var totalCount = unitCommentBLL.GetListCount(queryEntity);
                if (list != null && list.Count > 0)
                {
                    respData.content.commentList = new List<getUnitCommentListRespContentDataItem>();
                    foreach (var item in list)
                    {
                        respData.content.commentList.Add(new getUnitCommentListRespContentDataItem()
                        {
                            commentId = item.UnitCommentId,
                            userName = item.UserName,
                            starLevel = ToInt(item.StarLevel),
                            personAvg = Convert.ToDecimal(item.PersonAvg),
                            content = item.Content
                        });
                    }

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

        public class getUnitCommentListRespData : Default.LowerRespData
        {
            public getUnitCommentListRespContentData content { get; set; }
        }

        public class getUnitCommentListRespContentData
        {
            public string isNext { get; set; } //是否有下一页
            public IList<getUnitCommentListRespContentDataItem> commentList { get; set; } //评论集合
        }

        public class getUnitCommentListRespContentDataItem
        {
            public string commentId { get; set; } //评论标识
            public string userName { get; set; } //点评人
            public int starLevel { get; set; } //星级
            public decimal personAvg { get; set; } //人均
            public string content { get; set; } //点评内容
        }

        public class getUnitCommentListReqData : ReqData
        {
            public getUnitCommentListReqSpecialData special;
        }

        public class getUnitCommentListReqSpecialData
        {
            public string storeId { get; set; } //门店标识
            public int page { get; set; } //页码
            public int pageSize { get; set; } //页面数量
        }

        #endregion

        #region 提交订座

        public string setSeatOrderInfo()
        {
            string content = string.Empty;
            var respData = new setSeatOrderInfoRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSeatOrderInfo: {0}", reqContent)
                });

                #region //解析请求字符串 chech

                var reqObj = reqContent.DeserializeJSONTo<setSeatOrderInfoReqData>();
                reqObj = reqObj == null ? new setSeatOrderInfoReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setSeatOrderInfoReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                //if (reqObj.special.qty == null || reqObj.special.qty.Equals(""))
                //{
                //    respData.code = "2201";
                //    respData.description = "座位数不能为空";
                //    return respData.ToJSON().ToString();
                //}
                if (reqObj.special.orderDate == null || reqObj.special.orderDate.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "日期不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.contact == null || reqObj.special.contact.Equals(""))
                {
                    respData.code = "2203";
                    respData.description = "联系人不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.tel == null || reqObj.special.tel.Equals(""))
                {
                    respData.code = "2204";
                    respData.description = "联系电话不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.storeId == null || reqObj.special.storeId.Equals(""))
                {
                    respData.code = "2205";
                    respData.description = "门店标识不能为空";
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

                SeatOrderBLL service = new SeatOrderBLL(loggingSessionInfo);
                SeatOrderEntity orderInfo = new SeatOrderEntity();
                orderInfo.OrderId = reqObj.special.orderId;
                orderInfo.UnitId = reqObj.special.storeId;
                orderInfo.Qty = reqObj.special.qty;
                orderInfo.OrderDate = ToStr(reqObj.special.orderDate);
                orderInfo.OrderTime = ToStr(reqObj.special.orderTime);
                orderInfo.Contact = ToStr(reqObj.special.contact);
                orderInfo.Gender = ToInt(reqObj.special.gender);
                orderInfo.Tel = ToStr(reqObj.special.tel);
                orderInfo.Remark = ToStr(reqObj.special.remark);

                if (orderInfo.OrderId == null || orderInfo.OrderId.Trim().Length == 0)
                    orderInfo.OrderId = Utils.NewGuid();
                var tmpObj = service.GetByID(orderInfo.OrderId);
                if (tmpObj != null)
                {
                    service.Update(orderInfo, false);
                }
                else
                {
                    orderInfo.CustomerId = customerId;
                    service.Create(orderInfo);
                }

                respData.content = new setSeatOrderInfoRespContentData();
                respData.content.orderId = orderInfo.OrderId;
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
        public class setSeatOrderInfoRespData : Default.LowerRespData
        {
            public setSeatOrderInfoRespContentData content { get; set; }
        }

        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setSeatOrderInfoRespContentData
        {
            public string orderId { get; set; }
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setSeatOrderInfoReqData : ReqData
        {
            public setSeatOrderInfoReqSpecialData special;
        }

        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setSeatOrderInfoReqSpecialData
        {
            public string storeId { get; set; }
            public string orderId { get; set; }
            public decimal qty { get; set; } // 座位数
            public string orderDate { get; set; } // 日期
            public string orderTime { get; set; } //时间 
            public string contact { get; set; } //联系人 
            public int gender { get; set; } //联系人性别
            public string tel { get; set; } //联系电话 
            public string remark { get; set; } //备注 
        }

        #endregion

        #endregion

        #region 泸州老窖在线商城 Jermyn20140103

        #region 获取各种咨询信息(Jermyn20140103)

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getNewsListWeekly()
        {
            string content = string.Empty;
            var respData = new getNewsListWeeklyRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                #region

                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("getNewsListWeekly: {0}", reqContent)
                //});

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getNewsListWeeklyReqData>();
                reqObj = reqObj == null ? new getNewsListWeeklyReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getNewsListWeeklyReqSpecialData();
                    respData.code = "2201";
                    respData.description = "特殊参数集合不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.newsTypeId == null || reqObj.special.newsTypeId.Equals(""))
                {
                    respData.code = "2201";
                    respData.description = "newsTypeId不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                //处理参数
                int page = 0;
                int pageSize = 15;
                if (reqObj.special.page != null && !reqObj.special.page.Equals(""))
                {
                    page = Convert.ToInt32(reqObj.special.page);
                }
                if (reqObj.special.pageSize != null && !reqObj.special.pageSize.Equals("") &&
                    !reqObj.special.pageSize.Equals(0))
                {
                    pageSize = Convert.ToInt32(reqObj.special.pageSize);
                }

                #endregion

                respData.content = new getNewsListWeeklyRespContentData();
                respData.content.weeklyList = new List<getNewsListWeeklyListRespData>();

                #region

                LNewsBLL service = new LNewsBLL(loggingSessionInfo);
                int totalCount = service.getNewsListWeeklyCount(reqObj.special.newsTypeId, reqObj.special.businessType);
                respData.content.isNext = "0";
                if (totalCount > 0 && page > 0 && totalCount > (page * pageSize))
                {
                    respData.content.isNext = "1";
                }

                IList<LNewsEntity> newsList = new List<LNewsEntity>();
                newsList = service.getNewsListWeekly(reqObj.special.newsTypeId, reqObj.special.businessType, page,
                    pageSize);
                IList<getNewsListWeeklyListRespData> weeklyList = new List<getNewsListWeeklyListRespData>();
                if (newsList != null && newsList.Count > 0)
                {
                    foreach (LNewsEntity newsInfo in newsList)
                    {
                        getNewsListWeeklyListRespData info = new getNewsListWeeklyListRespData();
                        info.newsId = ToStr(newsInfo.NewsId);
                        info.newsLevel = ToInt(newsInfo.NewsLevel);
                        info.title = newsInfo.NewsTitle;
                        info.time = ToStr(newsInfo.PublishTime);
                        info.imageURL = ToStr(newsInfo.ImageUrl);
                        info.displayIndex = ToInt(newsInfo.DisplayIndex);
                        weeklyList.Add(info);
                    }

                    respData.content.weeklyList = weeklyList;
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

        public class getNewsListWeeklyRespData : Default.LowerRespData
        {
            public getNewsListWeeklyRespContentData content { get; set; }
        }

        public class getNewsListWeeklyRespContentData
        {
            public string isNext { get; set; }
            public IList<getNewsListWeeklyListRespData> weeklyList { get; set; }
        }

        public class getNewsListWeeklyListRespData
        {
            public string newsId { get; set; }
            public string title { get; set; }
            public string time { get; set; }
            public string imageURL { get; set; }
            public int displayIndex { get; set; }
            public int newsLevel { get; set; }
        }

        public class getNewsListWeeklyReqData : ReqData
        {
            public getNewsListWeeklyReqSpecialData special;
        }

        public class getNewsListWeeklyReqSpecialData //特殊参数
        {
            public string newsTypeId { get; set; } //咨询类型1=会员周刊2=品牌活动3=定制之旅
            public string businessType { get; set; } //会员周刊业务类型：1 现在（本期活动） 2往期（精彩回顾）3 活动预告4.马上参与
            public int page { get; set; }
            public int pageSize { get; set; }
        }

        #endregion

        #endregion

        #endregion

        #region 注册

        public string setSignUpFosun()
        {
            string content = string.Empty;
            var respData = new setSignUpFosunRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUpFosun: {0}", reqContent)
                });

                #region //解析请求字符串 chech

                var reqObj = reqContent.DeserializeJSONTo<setSignUpFosunReqData>();
                reqObj = reqObj == null ? new setSignUpFosunReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setSignUpFosunReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "电话不能为空";
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

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                FStaffBLL fStaffBLL = new FStaffBLL(loggingSessionInfo);

                var tmpStaffList = fStaffBLL.QueryByEntity(new FStaffEntity()
                {
                    Phone = reqObj.special.phone,
                    CustomerId = customerId
                }, null);


                var tmpVipList = vipBLL.QueryByEntity(new VipEntity()
                {
                    VIPID = reqObj.common.userId
                }, null);
                var tmpPhoneVip = vipBLL.GetVipByPhone(reqObj.special.phone, reqObj.common.userId, "2");
                if (tmpPhoneVip != null && tmpPhoneVip.VIPID != reqObj.common.userId)
                {
                    respData.code = "2202";
                    respData.description = "您填写的手机号码已经被其他参会人员认证。如需帮助，请联系现场工作人员。";
                    return respData.ToJSON().ToString();
                }

                if (tmpStaffList != null && tmpStaffList.Length > 0)
                {
                    if (tmpVipList != null && tmpVipList.Length > 0)
                    {
                        if (tmpVipList[0].Status == 2)
                        {
                            respData.code = "2202";
                            respData.description = "您填写的手机号码已经认证。如需帮助，请联系现场工作人员。";
                            return respData.ToJSON().ToString();
                        }
                        else if (tmpVipList[0].Status == 1)
                        {
                            vipBLL.Update(new VipEntity()
                            {
                                VIPID = tmpVipList[0].VIPID,
                                VipRealName = tmpStaffList[0].StaffName,
                                Phone = reqObj.special.phone,
                                Status = 2
                            }, false);
                        }
                        else
                        {
                            respData.code = "2202";
                            respData.description = "在参会嘉宾中未查询到您输入的手机号码，请确认您输入是否有误。如需帮助，请联系现场工作人员。";
                            return respData.ToJSON().ToString();
                        }
                    }
                    else
                    {
                        respData.code = "2202";
                        respData.description = "在参会嘉宾中未查询到您输入的手机号码，请确认您输入是否有误。如需帮助，请联系现场工作人员。";
                        return respData.ToJSON().ToString();
                    }
                }
                else
                {
                    respData.code = "2202";
                    respData.description = "在参会嘉宾中未查询到您输入的手机号码，请确认您输入是否有误。如需帮助，请联系现场工作人员。";
                    return respData.ToJSON().ToString();
                }

                respData.content = new setSignUpFosunRespContentData();
                respData.description = "您的座位是：" + tmpStaffList[0].Seats + "，请就坐。";
                string error = "";
                var sendFlag = fStaffBLL.SetStaffSeatsPush(tmpVipList[0].VIPID, reqObj.common.eventId, out error);
                if (!sendFlag)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetStaffSeatsPush: {0}", error)
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

        #region 参数对象

        /// <summary>
        /// 返回对象
        /// </summary>
        public class setSignUpFosunRespData : Default.LowerRespData
        {
            public setSignUpFosunRespContentData content { get; set; }
        }

        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setSignUpFosunRespContentData
        {
            public string vipId { get; set; }
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setSignUpFosunReqData : ReqData
        {
            public setSignUpFosunReqSpecialData special;
        }

        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setSignUpFosunReqSpecialData
        {
            public string phone { get; set; }
            public string staffName { get; set; }
            public string eventId { get; set; }
        }

        #endregion

        #endregion

        #region 复星的员工查询

        /// <summary>
        /// 复星的员工查询
        /// </summary>
        /// <returns></returns>
        public string GetSearchVipStaff()
        {
            string content = string.Empty;
            var respData = new GetSearchVipStaffRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetSearchVipStaff: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<GetSearchVipStaffReqData>();
                reqObj = reqObj == null ? new GetSearchVipStaffReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new GetSearchVipStaffReqSpecialData();
                    reqObj.special.page = 1;
                    reqObj.special.pageSize = 15;
                }
                if (reqObj.special.vipId == null || reqObj.special.vipId.Equals(""))
                {
                    if (reqObj.special.keyword == null || reqObj.special.keyword.Equals(""))
                    {
                        respData.code = "2202";
                        respData.description = "关键字不能为空";
                        return respData.ToJSON().ToString();
                    }
                }
                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                int page = reqObj.special.page; //页码
                int pageSize = reqObj.special.pageSize; //页面数量
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 15;

                //初始化返回对象
                respData.content = new GetSearchVipStaffRespContentData();
                FStaffBLL fStaffBLL = new FStaffBLL(loggingSessionInfo);

                FStaffEntity queryEntity = new FStaffEntity();
                queryEntity.StaffName = reqObj.special.keyword;
                queryEntity.VipId = reqObj.special.vipId;

                var list = fStaffBLL.GetList(queryEntity, page - 1, pageSize);
                var totalCount = fStaffBLL.GetListCount(queryEntity);
                if (list != null && list.Count > 0)
                {
                    respData.content.vipList = new List<GetSearchVipStaffRespContentDataItem>();
                    foreach (var item in list)
                    {
                        respData.content.vipList.Add(new GetSearchVipStaffRespContentDataItem()
                        {
                            vipId = item.VipId == null || item.VipId.Trim().Length == 0 ? item.StaffId : item.VipId,
                            staffName = item.StaffName,
                            staffCompany = item.StaffCompany,
                            staffPost = item.StaffPost,
                            phone = item.Phone,
                            seats = item.Seats,
                            profile = item.Profile,
                            status = item.Status,
                            displayIndex = item.DisplayIndex,
                            DCodeImageUrl = item.DCodeImageUrl,
                            HeadImageUrl = item.HeadImgUrl == null || item.HeadImgUrl.Trim().Length == 0
                                ? "http://o2oapi.aladingyidong.com/images/msg_vip.jpg"
                                : item.HeadImgUrl,
                            email = item.Email
                            ,
                            isSign = item.IsSign.ToString()
                        });
                    }

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

        public class GetSearchVipStaffRespData : Default.LowerRespData
        {
            public GetSearchVipStaffRespContentData content { get; set; }
        }

        public class GetSearchVipStaffRespContentData
        {
            public string isNext { get; set; } //是否有下一页
            public IList<GetSearchVipStaffRespContentDataItem> vipList { get; set; } //集合
        }

        public class GetSearchVipStaffRespContentDataItem
        {
            public string vipId { get; set; } //vip标识
            public string staffName { get; set; } //员工姓名
            public string staffCompany { get; set; } //公司
            public string staffPost { get; set; } //职务
            public string phone { get; set; } //手机
            public string seats { get; set; } //席次
            public string profile { get; set; } //个人简介
            public int status { get; set; } //状态
            public Int64 displayIndex { get; set; } //排序
            public string DCodeImageUrl { get; set; } //二维码图片
            public string HeadImageUrl { get; set; } //头像图片
            public string email { get; set; } //email
            public string isSign { get; set; }
        }

        public class GetSearchVipStaffReqData : ReqData
        {
            public GetSearchVipStaffReqSpecialData special;
        }

        public class GetSearchVipStaffReqSpecialData
        {
            public string keyword { get; set; } //名称
            public string vipId { get; set; } //vipId
            public int page { get; set; } //页码
            public int pageSize { get; set; } //页面数量
        }

        #endregion

        #region 修改会员

        public string SetVipStaff()
        {
            string content = string.Empty;
            var respData = new SetVipStaffRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetVipStaff: {0}", reqContent)
                });

                #region //解析请求字符串 chech

                var reqObj = reqContent.DeserializeJSONTo<SetVipStaffReqData>();
                reqObj = reqObj == null ? new SetVipStaffReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new SetVipStaffReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.phone == null || reqObj.special.phone.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "电话不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.staffName == null || reqObj.special.staffName.Equals(""))
                {
                    respData.code = "2202";
                    respData.description = "名称不能为空";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2202";
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

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                FStaffBLL fStaffBLL = new FStaffBLL(loggingSessionInfo);

                var tmpStaffList = fStaffBLL.QueryByEntity(new FStaffEntity()
                {
                    Phone = reqObj.special.phone,
                    CustomerId = customerId
                }, null);
                var tmpVipList = vipBLL.QueryByEntity(new VipEntity()
                {
                    VIPID = reqObj.common.userId
                }, null);
                if (tmpStaffList != null && tmpStaffList.Length > 0)
                {
                    if (tmpVipList != null && tmpVipList.Length > 0)
                    {
                        var tmpStaffObj = new FStaffEntity()
                        {
                            StaffId = tmpStaffList[0].StaffId,
                            StaffName = reqObj.special.staffName,
                            StaffCompany = reqObj.special.staffCompany,
                            StaffPost = reqObj.special.staffPost,
                            Phone = reqObj.special.phoneNew,
                            Seats = reqObj.special.seats,
                            Profile = reqObj.special.profile,
                            Email = reqObj.special.email
                        };
                        fStaffBLL.Update(tmpStaffObj, false);

                        vipBLL.Update(new VipEntity()
                        {
                            VIPID = tmpVipList[0].VIPID,
                            VipName = reqObj.special.staffName,
                            Phone = reqObj.special.phoneNew
                            ,
                            Status = 2
                        }, false);

                        // CreateQrcode
                        var qrImageUrl = "";
                        var qrFlag = cUserService.CreateQrcode(tmpStaffObj.StaffName, tmpStaffObj.Email, "",
                            tmpStaffObj.Phone, "",
                            tmpStaffObj.StaffCompany, tmpStaffObj.StaffPost, "", tmpStaffObj.Profile, ref qrImageUrl);
                        if (qrFlag)
                        {
                            fStaffBLL.Update(new FStaffEntity()
                            {
                                StaffId = tmpStaffObj.StaffId,
                                DCodeImageUrl = qrImageUrl
                            }, false);
                        }
                        else
                        {
                            respData.code = "2202";
                            respData.description = "用户二维码数据保存失败";
                            return respData.ToJSON().ToString();
                        }
                    }
                    else
                    {
                        respData.code = "2202";
                        respData.description = "未查询到匹配的信息";
                        return respData.ToJSON().ToString();
                    }
                }
                else
                {
                    respData.code = "2202";
                    respData.description = "未查询到匹配的信息";
                    return respData.ToJSON().ToString();
                }

                respData.content = new SetVipStaffRespContentData();
                respData.description = "您的个人信息修改成功！";
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
        public class SetVipStaffRespData : Default.LowerRespData
        {
            public SetVipStaffRespContentData content { get; set; }
        }

        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class SetVipStaffRespContentData
        {
            //public string vipId { get; set; }
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class SetVipStaffReqData : ReqData
        {
            public SetVipStaffReqSpecialData special;
        }

        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class SetVipStaffReqSpecialData
        {
            public string vipId { get; set; }
            public string staffName { get; set; }
            public string staffCompany { get; set; }
            public string staffPost { get; set; }
            public string phone { get; set; }
            public string seats { get; set; }
            public string profile { get; set; }
            public string email { get; set; }
            public string phoneNew { get; set; }
        }

        #endregion

        #endregion

        #region 提交微信平台用户留言接口

        /// <summary>
        /// 提交微信平台用户留言接口
        /// </summary>
        /// <returns></returns>
        public string SetUserMessageData()
        {
            SetUserMessageDataEntity response = new SetUserMessageDataEntity();
            response.content = new SetUserMessageDataContentEntity();
            var data = Request["ReqContent"].DeserializeJSONTo<SetUserMessageDataReqData>();
            if (data == null || data.special == null)
            {
                response.code = "103";
                response.description = "数据库操作错误";
                response.exception = "请求的数据不能为空";
                return response.ToJSON();
            }

            if (string.IsNullOrEmpty(data.special.toVipId) || string.IsNullOrEmpty(data.special.text))
            {
                response.code = "103";
                response.description = "数据库操作错误";
                response.exception = "接收人不能为空或消息不能为空";
                return response.ToJSON();
            }

            #region //判断客户ID是否传递

            if (!string.IsNullOrEmpty(data.common.customerId))
            {
                customerId = data.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

            #endregion

            string content = string.Empty;
            response.code = "200";
            response.description = "操作成功";
            try
            {
                //获取接收人
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                var toVip = vipBLL.Query(new IWhereCondition[]
                {
                    new EqualsCondition() {FieldName = "VipId", Value = data.special.toVipId}
                }
                    , null).FirstOrDefault();
                if (toVip != null)
                {
                    FStaffEntity fStaffEntity = new FStaffEntity();
                    fStaffEntity.VipId = data.common.userId;

                    FStaffBLL fStaffBLL = new FStaffBLL(loggingSessionInfo);
                    var fStaff = fStaffBLL.GetList(fStaffEntity, 0, 10);
                    //组织消息            
                    string message = ConfigurationManager.AppSettings["TransferMessage"].ToString();
                    message = string.Format(message
                        , fStaff[0].StaffName
                        , data.special.text
                        , data.common.customerId
                        , toVip.WeiXinUserId
                        , data.special.toVipId
                        , data.common.userId
                        , "783467B7A9F1425CA3DFC0B03A36B713"); //data.special.eventId

                    string code = CommonBLL.SendWeixinMessage(message, data.common.userId, loggingSessionInfo, toVip);
                    switch (code)
                    {
                        case "103":
                            response.code = "103";
                            response.description = "数据库操作错误";
                            response.exception = "未查询到匹配的公众账号信息";
                            break;
                        case "203":
                            response.code = "203";
                            response.description = "发送失败";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    response.code = "202";
                    response.description = "接收人无效";
                }
            }
            catch (Exception ex)
            {
                response.code = "201";
                response.description = "失败:" + ex.ToString();
            }
            content = response.ToJSON();
            return content;
        }

        public class SetUserMessageDataEntity : Default.LowerRespData
        {
            public SetUserMessageDataContentEntity content { get; set; }
        }

        public class SetUserMessageDataReqData : ReqData
        {
            public SetUserMessageDataReqSpecialData special;
        }

        public class SetUserMessageDataReqSpecialData
        {
            public string toVipId;
            public string text;
            public string toVipType;
            public string eventId;
        }

        public class SetUserMessageDataContentEntity
        {
            public string MessageId { get; set; }
        }

        public class SendWXMsgRespEntity
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
        }

        #endregion

        #region 注册、签到查询

        public string CheckSign()
        {
            string content = string.Empty;
            var respData = new CheckSignRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("CheckSign: {0}", reqContent)
                });

                var reqObj = reqContent.DeserializeJSONTo<CheckSignReqData>();
                if (reqObj.common == null)
                {
                    respData.code = "100";
                    respData.description = "没有基础参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common == null || reqObj.common.userId == null || reqObj.common.userId == "")
                {
                    respData.code = "101";
                    respData.description = "没有用户ID";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.eventId == null || reqObj.special.eventId.Equals(""))
                {
                    respData.code = "201";
                    respData.description = "活动ID不能为空";
                    return respData.ToJSON().ToString();
                }

                #region //判断客户ID是否传递

                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                #endregion

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                EventVipBLL eventVipBLL = new EventVipBLL(loggingSessionInfo);
                respData.content = new CheckSignRespDataRespContentData();

                LEventsEntity lEventsEntity = new LEventsEntity();
                lEventsEntity = new LEventsBLL(loggingSessionInfo).GetByID(reqObj.special.eventId);
                if (lEventsEntity.EventID == null || lEventsEntity.EventID != "")
                {
                    string eventFlag = lEventsEntity.EventFlag;

                    if (eventFlag != "")

                    #region 若配置了EventFlag字段则走新的逻辑

                    {
                        char[] flagArray = eventFlag.ToCharArray();

                        WEventUserMappingBLL wEventUserMappingBLL = new WEventUserMappingBLL(loggingSessionInfo);
                        WEventUserMappingEntity wEventUserMappingEntity = new WEventUserMappingEntity();

                        for (int i = 0; i < flagArray.Length; i++)
                        {
                            switch (i)
                            {
                                #region 是否需要注册

                                case 0:
                                    VipEntity vipEntity = new VipEntity();
                                    vipEntity = vipBLL.GetByID(reqObj.common.userId);

                                    //订阅号无VIP数据，自动生成
                                    if (vipEntity == null || vipEntity.VIPID == "")
                                    {
                                        vipBLL.Create(new VipEntity() { VIPID = reqObj.common.userId });
                                        wEventUserMappingEntity = new WEventUserMappingEntity()
                                        {
                                            Mapping = Utils.NewGuid(),
                                            EventID = reqObj.special.eventId,
                                            UserID = reqObj.common.userId,
                                            CreateBy = "自动生成",
                                            CreateTime = DateTime.Now,
                                            IsDelete = 0
                                        };
                                    }

                                    Loggers.Debug(new DebugLogInfo() { Message = "vipEntity:" + vipEntity.ToJSON() });

                                    if (flagArray[i] == '0')
                                    {
                                        respData.content.isRegistered = "1";
                                    }
                                    else
                                    {
                                        if (vipEntity != null && vipEntity.VIPID != "" && vipEntity.Status.Value == 2)
                                            respData.content.isRegistered = "1";
                                        else
                                            respData.content.isRegistered = "0";
                                    }

                                    Loggers.Debug(new DebugLogInfo()
                                    {
                                        Message =
                                            "是否需注册:" + flagArray[i] + ", respData.content.isRegistered =" +
                                            respData.content.isRegistered
                                    });
                                    break;

                                #endregion

                                #region 是否需要签到

                                case 1:
                                    if (flagArray[i] == '0')
                                    {
                                        respData.content.isSigned = "1";
                                    }
                                    else
                                    {
                                        respData.content.isSigned =
                                            wEventUserMappingBLL.GetUserSignIn(reqObj.special.eventId,
                                                reqObj.common.userId) > 0
                                                ? "1"
                                                : "0";
                                    }
                                    break;

                                #endregion

                                #region 是否需要验证

                                case 2:
                                    if (flagArray[i] == '0')
                                    {
                                        respData.content.isValidated = "1";
                                    }
                                    else
                                    {
                                        LEventSignUpBLL lEventSignUpBLL = new LEventSignUpBLL(loggingSessionInfo);
                                        respData.content.isValidated =
                                            lEventSignUpBLL.QueryByEntity(
                                                new LEventSignUpEntity()
                                                {
                                                    EventID = reqObj.special.eventId,
                                                    VipID = reqObj.common.userId
                                                }, null).Length > 0
                                                ? "1"
                                                : "0";
                                    }
                                    break;

                                #endregion

                                default:
                                    break;
                            }
                        }
                    }
                    #endregion

                    else
                    #region 否则走旧的逻辑

                    {
                        respData.content.isRegistered =
                            eventVipBLL.IsVipStaffMapping(reqObj.common.userId, "", reqObj.common.openId).ToString();

                        //没注册就等于没签到
                        if (respData.content.isRegistered == "1")
                        {
                            WEventUserMappingBLL wEventUserMappingBLL = new WEventUserMappingBLL(loggingSessionInfo);
                            respData.content.isSigned = wEventUserMappingBLL.Query(
                                new IWhereCondition[]
                                {
                                    new EqualsCondition() {FieldName = "UserId", Value = reqObj.common.userId},
                                    new EqualsCondition() {FieldName = "EventId", Value = reqObj.special.eventId}
                                }, null).Length > 0
                                ? "1"
                                : "0";
                        }
                        else
                            respData.content.isSigned = "0";
                    }

                    #endregion
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
        /// 传输的参数对象
        /// </summary>
        public class CheckSignReqData : ReqData
        {
            public CheckSignReqSpecialData special;
        }

        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class CheckSignReqSpecialData
        {
            public string eventId { get; set; }
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        public class CheckSignRespData : Default.LowerRespData
        {
            public CheckSignRespDataRespContentData content { get; set; }
        }

        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class CheckSignRespDataRespContentData
        {
            public string isRegistered { get; set; }
            public string isSigned { get; set; }
            public string isValidated { get; set; }
        }

        #endregion

        #endregion

        #region 生成二维码

        /// <summary>
        /// 生成二维码
        /// </summary>
        public string createQrcode()
        {
            string content = string.Empty;
            var respData = new createQrcodeRespData();
            try
            {
                string name = Request["name"] == null ? "" : HttpUtility.UrlDecode(Request["name"]); // 姓名
                string email = Request["email"] == null ? "" : HttpUtility.UrlDecode(Request["email"]); // 邮箱
                string tel = Request["tel"] == null ? "" : HttpUtility.UrlDecode(Request["tel"]); // 联系电话
                string cell = Request["cell"] == null ? "" : HttpUtility.UrlDecode(Request["cell"]); // 移动电话
                string address = Request["address"] == null ? "" : HttpUtility.UrlDecode(Request["address"]); // 联系地址
                string company = Request["company"] == null ? "" : HttpUtility.UrlDecode(Request["company"]); // 公司名称
                string title = Request["title"] == null ? "" : HttpUtility.UrlDecode(Request["title"]); // 职位
                string page_url = Request["page_url"] == null ? "" : HttpUtility.UrlDecode(Request["page_url"]); // 主页地址
                string remark = Request["remark"] == null ? "" : HttpUtility.UrlDecode(Request["remark"]); // 备注

                if (remark != null && remark.Length > 300) remark = remark.Substring(0, 300);

                //Loggers.Debug(new DebugLogInfo()
                //{
                //    Message = string.Format("name:{0}, email:{1}, tel:{2}, cell:{3}, address:{4}, company:{5}, title:{6}, url:{7}, remark:{8}",
                //        name, email, tel, cell, address, company, title, page_url, remark)
                //});

                var qrcode = new StringBuilder();
                qrcode.AppendFormat(
                    "BEGIN:VCARD\r\nVERSION:3.0\r\nN:{0}\r\nEMAIL:{1}\r\nTEL:{2}\r\nTEL;CELL:{3}\r\nADR:{4}\r\nORG:{5}\r\nTITLE:{6}\r\nURL:{7}\r\nNOTE:{8}\r\nEND:VCARD",
                    name, email, tel, cell, address, company, title, page_url, remark);

                #region 处理图片

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 5;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                System.Drawing.Image qrImage = qrCodeEncoder.Encode(qrcode.ToString(), Encoding.UTF8);
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(256, 256);
                System.Drawing.Graphics g2 = System.Drawing.Graphics.FromImage(bitmap);
                g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g2.Clear(System.Drawing.Color.Transparent);
                g2.DrawImage(qrImage, new System.Drawing.Rectangle(0, 0, 256, 256),
                    new System.Drawing.Rectangle(0, 0, qrImage.Width, qrImage.Height), System.Drawing.GraphicsUnit.Pixel);
                string fileName = System.Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string host = ConfigurationManager.AppSettings["website_WWW"].ToString();
                if (!host.EndsWith("/")) host += "/";
                string fileUrl = host + "qrcode_images/" + fileName;
                string newFilePath = string.Empty;
                string newFilename = string.Empty;
                string path = Server.MapPath("../../images/qrcode.jpg");
                System.Drawing.Image imgSrc = System.Drawing.Image.FromFile(path);
                System.Drawing.Image imgWarter = bitmap;
                using (Graphics g = Graphics.FromImage(imgSrc))
                {
                    g.DrawImage(imgWarter, new Rectangle(0, 0, imgWarter.Width, imgWarter.Height),
                        0, 0, imgWarter.Width, imgWarter.Height, GraphicsUnit.Pixel);
                }
                newFilePath = string.Format("../../qrcode_images/{0}", fileName);
                newFilename = Server.MapPath(newFilePath);
                imgSrc.Save(newFilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgWarter.Dispose();
                imgSrc.Dispose();
                qrImage.Dispose();
                bitmap.Dispose();
                g2.Dispose();

                #endregion

                respData.qrImageUrl = fileUrl;
            }
            catch (Exception ex)
            {
                respData.Code = "103";
                respData.Description = "数据库操作错误";
                respData.Exception = ex.ToString();
            }
            content = respData.ToJSON();
            return content;
        }

        public class createQrcodeRespData : Default.RespData
        {
            public string qrImageUrl;
        }

        #endregion

        #region 复星获取移动采招集合

        #region 获取会员秀集合

        public string GetMobileList()
        {
            string content = string.Empty;
            var respData = new GetMobileListRespData();
            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetMobileList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<GetMobileListReqData>();
                reqObj = reqObj == null ? new GetMobileListReqData() : reqObj;

                #region

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
                if (reqObj.special.eventId == null)
                {
                    respData.code = "104";
                    respData.description = "活动标识不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.modelId == null)
                {
                    respData.code = "104";
                    respData.description = "模板标识不能为空";
                    return respData.ToJSON().ToString();
                }

                #endregion

                WMaterialTextBLL materialTextServer = new WMaterialTextBLL(loggingSessionInfo);
                IList<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
                list = materialTextServer.GetMaterialTextListByModelId(ToStr(reqObj.special.modelId));
                string city = string.Empty;
                string textType = string.Empty;
                if (list != null)
                {
                    respData.content = new GetMobileListRespContentData();
                    respData.content.mobileList = new List<GetMobileListRespVipShowData>();
                    foreach (var item in list)
                    {
                        if (item.Author != null && !item.Author.Equals(""))
                        {
                            string[] array = item.Author.Split(',');
                            int i = 0;
                            foreach (var obj in array)
                            {
                                if (i == 0) city = array[0];
                                if (i == 1) textType = array[1];

                                i = i + 1;
                            }
                        }
                        var tmpItemObj = new GetMobileListRespVipShowData()
                        {
                            textId = ToStr(item.TextId),
                            title = ToStr(item.Title),
                            url = ToStr(item.OriginalUrl),
                            displayIndex = ToInt(item.DisplayIndex)
                            ,
                            imageUrl = ToStr(item.CoverImageUrl)
                            ,
                            city = ToStr(city)
                            ,
                            textType = ToStr(textType)
                        };
                        respData.content.mobileList.Add(tmpItemObj);
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

        public class GetMobileListRespData : Default.LowerRespData
        {
            public GetMobileListRespContentData content { get; set; }
        }

        public class GetMobileListRespContentData
        {
            public IList<GetMobileListRespVipShowData> mobileList { get; set; }
        }

        public class GetMobileListRespVipShowData
        {
            public string textId { get; set; }
            public string title { get; set; }
            public int displayIndex { get; set; }
            public string url { get; set; }
            public string textType { get; set; } //招标类别
            public string city { get; set; } //城市
            public string imageUrl { get; set; }
        }

        public class GetMobileListReqData : ReqData
        {
            public GetMobileListReqSpecialData special;
        }

        public class GetMobileListReqSpecialData
        {
            public string eventId { get; set; }
            public string modelId { get; set; }
        }

        #endregion

        #endregion

        #region 活动详细内容获取

        /// <summary>
        /// 活动详细内容获取
        /// </summary>
        public string getEventDetail()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getEventDetailRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getEventDetailReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventDetail ReqContent:{0}",
                        ReqContent)
                });

                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new LEventsBLL(loggingSessionInfo);

                //var service = new EventsBLL(Default.GetBasicUserInfo(reqContentObj));

                GetResponseParams<LEventsEntity> returnDataObj = service.WEventGetEventDetail(
                    reqContentObj.special.eventId,
                    reqContentObj.common.userId);

                var contentObj = new getEventDetailRespContentData();
                respObj.code = returnDataObj.Code;
                respObj.description = returnDataObj.Description;
                //
                if (returnDataObj.Flag == "1" && returnDataObj.Params != null)
                {
                    contentObj.eventId = returnDataObj.Params.EventID;
                    contentObj.title = returnDataObj.Params.Title;
                    contentObj.city = returnDataObj.Params.CityID;
                    contentObj.address = Default.ToStr(returnDataObj.Params.Address);
                    contentObj.contact = Default.ToStr(returnDataObj.Params.Content);
                    contentObj.email = Default.ToStr(returnDataObj.Params.Email);
                    //qianzhi 2013-05-25  添加结束时间
                    if (returnDataObj.Params.BeginTime == null ||
                        Convert.ToDateTime(returnDataObj.Params.BeginTime).ToString("yyyy-MM-dd").Equals("0001-01-01"))
                    {
                        contentObj.timeStr = "待定";
                    }
                    else
                    {
                        if (returnDataObj.Params.EndTime == null || returnDataObj.Params.EndTime.Equals(""))
                        {
                            contentObj.timeStr =
                                Default.ToStr(
                                    Convert.ToDateTime(returnDataObj.Params.BeginTime).ToString("yyyy-MM-dd HH:mm"));
                        }
                        else
                        {
                            contentObj.timeStr =
                                Default.ToStr(
                                    Convert.ToDateTime(returnDataObj.Params.BeginTime).ToString("yyyy-MM-dd HH:mm")) +
                                " 至 " +
                                Default.ToStr(
                                    Convert.ToDateTime(returnDataObj.Params.EndTime).ToString("yyyy-MM-dd HH:mm"));
                        }
                    }

                    contentObj.imageUrl = Default.ToStr(returnDataObj.Params.ImageURL);

                    contentObj.organizer = "";
                    contentObj.organizerType = "";
                    contentObj.applyCount = Default.ToStr(returnDataObj.Params.signUpCount); //报名数量
                    contentObj.checkinCount = Default.ToStr(returnDataObj.Params.CheckinsCount); //签到数量
                    contentObj.hasPrize = "";
                    contentObj.intervalDays = Default.ToStr(returnDataObj.Params.IntervalDays);
                    contentObj.description = HttpUtility.HtmlDecode(returnDataObj.Params.Description);

                    contentObj.longitude = Default.ToStr(returnDataObj.Params.Longitude);
                    contentObj.latitude = Default.ToStr(returnDataObj.Params.Latitude);
                }

                respObj.content = contentObj;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventDetail RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getEventDetailReqData : Default.ReqData
        {
            public getEventDetailReqSpecialData special;
        }

        public class getEventDetailReqSpecialData
        {
            public string eventId;
        }

        public class getEventDetailRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public getEventDetailRespContentData content;
        }

        public class getEventDetailRespContentData
        {
            public string eventId;
            public string title;
            public string city;
            public string address;
            public string contact;
            public string email;
            public string imageUrl;
            public string timeStr;
            public string organizer;
            public string organizerType;
            public string applyCount;
            public string checkinCount;
            public string hasPrize;
            public string intervalDays;
            public string description;
            public string longitude;
            public string latitude;
        }

        #endregion

        #region 活动报名表数据获取

        /// <summary>
        /// 活动报名表数据获取 
        /// </summary>
        public string getEventApplyQues()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getEventApplyQuesRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getEventApplyQuesReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventApplyQues ReqContent:{0}",
                        ReqContent)
                });
                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new WEventUserMappingBLL(loggingSessionInfo);

                GetResponseParams<QuestionnaireEntity> returnDataObj = service.getEventApplyQues(
                    reqContentObj.special.eventId);

                var contentObj = new getEventApplyQuesRespContentData();
                respObj.code = returnDataObj.Code;
                respObj.description = returnDataObj.Description;


                if (returnDataObj.Flag == "1" && returnDataObj.Params != null)
                {
                    contentObj.questionCount = Default.ToStr(returnDataObj.Params.QuestionCount);

                    // questions
                    if (returnDataObj.Params.QuesQuestionEntityList != null)
                    {
                        contentObj.questions = new List<getEventApplyQuesRespQuestionData>();
                        foreach (var tmpQuestion in returnDataObj.Params.QuesQuestionEntityList)
                        {
                            if (tmpQuestion == null) continue;
                            var tmpQues = new getEventApplyQuesRespQuestionData();
                            tmpQues.questionId = Default.ToStr(tmpQuestion.QuestionID);
                            tmpQues.isSaveOutEvent = Default.ToStr(tmpQuestion.IsSaveOutEvent);
                            tmpQues.cookieName = Default.ToStr(tmpQuestion.CookieName);
                            tmpQues.questionText = Default.ToStr(tmpQuestion.QuestionDesc);
                            tmpQues.questionType = Default.ToStr(tmpQuestion.QuestionType);
                            tmpQues.minSelected = Default.ToStr(tmpQuestion.MinSelected);
                            tmpQues.maxSelected = Default.ToStr(tmpQuestion.MaxSelected);
                            tmpQues.isRequired = Default.ToStr(tmpQuestion.IsRequired);
                            tmpQues.isFinished = Default.ToStr(tmpQuestion.IsFinished);

                            // options
                            if (tmpQuestion.QuesOptionEntityList != null)
                            {
                                tmpQues.options = new List<getEventApplyQuesRespOptionData>();
                                foreach (var tmpOption in tmpQuestion.QuesOptionEntityList)
                                {
                                    if (tmpOption == null) continue;
                                    var tmpOp = new getEventApplyQuesRespOptionData();
                                    tmpOp.optionId = Default.ToStr(tmpOption.OptionsID);
                                    tmpOp.optionText = Default.ToStr(tmpOption.OptionsText);
                                    tmpOp.isSelected = Default.ToStr(tmpOption.IsSelect);
                                    tmpQues.options.Add(tmpOp);
                                }
                            }

                            contentObj.questions.Add(tmpQues);
                        }
                    }
                }

                respObj.content = contentObj;
                LEventsBLL eventServer = new LEventsBLL(loggingSessionInfo);
                LEventsEntity eventInfo = new LEventsEntity();
                eventInfo = eventServer.GetByID(reqContentObj.special.eventId);
                respObj.content.imageUrl = eventInfo.URL;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEventApplyQues RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                //respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getEventApplyQuesReqData : Default.ReqData
        {
            public getEventApplyQuesReqSpecialData special;
        }

        public class getEventApplyQuesReqSpecialData
        {
            public string eventId;
        }

        public class getEventApplyQuesRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public getEventApplyQuesRespContentData content;
        }

        public class getEventApplyQuesRespContentData
        {
            public string userName;
            public string userId;
            public string classValue; // class
            public string mobile;
            public string email;
            public string questionCount;
            public string imageUrl; //标题图片
            public IList<getEventApplyQuesRespQuestionData> questions;
        }

        public class getEventApplyQuesRespQuestionData
        {
            public string questionId;
            public string isSaveOutEvent;
            public string cookieName;
            public string questionText;
            public string questionType;
            public string minSelected;
            public string maxSelected;
            public string isRequired;
            public string isFinished;
            public IList<getEventApplyQuesRespOptionData> options;
        }

        public class getEventApplyQuesRespOptionData
        {
            public string optionId;
            public string optionText;
            public string isSelected;
        }

        #endregion

        #region 活动报名表数据提交

        /// <summary>
        /// 活动报名表数据提交
        /// </summary>
        public string submitEventApply()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new submitEventApplyRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["Form"];

                //ReqContent = "{\"common\":{\"locale\":\"zh\",\"userId\":\"4f4ef63846f646b68e796cbc3604f2ed\",\"openId\":\"o8Y7Ejv3jR5fEkneCNu6N1_TIYIM\",\"customerId\":\"f6a7da3d28f74f2abedfc3ea0cf65c01\"},\"special\":{\"eventId\":\"8D41CDD7D5E4499195316E4645FCD7B9\",\"questions\":[{\"questionId\":\"87871FCE7117481DB2F72F28D627579F\",\"isSaveOutEvent\":\"0\",\"cookieName\":\"110801\",\"questionValue\":\"E9EAAE121543475EB57B1936EB98B4B7\"},{\"questionId\":\"CF21F654796F4E0B8F6F47D9D05B9407\",\"isSaveOutEvent\":\"0\",\"cookieName\":\"110802\",\"questionValue\":\"81E327E3252F4071AD9556F89580DCE2\"},{\"questionId\":\"4A73FEA6C1484ED4B1730A1EBC54E5B8\",\"isSaveOutEvent\":\"0\",\"cookieName\":\"110803\",\"questionValue\":\"11778879013148F2A424D5220FB02E09\"}],\"userName\":\"\",\"mobile\":\"\",\"email\":\"\"}}";

                //ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<submitEventApplyReqData>();

                #region

                if (reqContentObj.special == null)
                {
                    respObj.code = "102";
                    respObj.description = "没有特殊参数";
                    return respObj.ToJSON().ToString();
                }

                if (reqContentObj.common.userId == null || reqContentObj.common.userId.Equals(""))
                {
                    respObj.code = "2202";
                    respObj.description = "userId不能为空";
                    return respObj.ToJSON().ToString();
                }

                if (reqContentObj.common.customerId == null || reqContentObj.common.customerId.Equals(""))
                {
                    respObj.code = "2203";
                    respObj.description = "客户标识（customerId）不能为空";
                    return respObj.ToJSON().ToString();
                }

                if (reqContentObj.special.eventId == null || reqContentObj.special.eventId.Equals(""))
                {
                    respObj.code = "2204";
                    respObj.description = "活动标识（eventId）不能为空";
                    return respObj.ToJSON().ToString();
                }

                if (reqContentObj.special.userName == null || reqContentObj.special.userName.Equals(""))
                {
                    respObj.code = "2205";
                    respObj.description = "用户名称（userName）不能为空";
                    return respObj.ToJSON().ToString();
                }

                #endregion

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "submitEventApply ReqContent:{0}",
                        ReqContent)
                });
                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new LEventsBLL(loggingSessionInfo);

                // WEventUserMappingEntity
                WEventUserMappingEntity userMappingEntity = new WEventUserMappingEntity();
                userMappingEntity.UserName = reqContentObj.special.userName;
                userMappingEntity.Mobile = reqContentObj.special.mobile;
                userMappingEntity.Email = reqContentObj.special.email;

                // quesAnswerList
                IList<QuesAnswerEntity> quesAnswerList = new List<QuesAnswerEntity>();
                if (reqContentObj.special.questions != null)
                {
                    foreach (var question in reqContentObj.special.questions)
                    {
                        QuesAnswerEntity quesAnswerEntity = new QuesAnswerEntity();
                        quesAnswerEntity.QuestionID = question.questionId;
                        quesAnswerEntity.QuestionValue = question.questionValue;
                        quesAnswerList.Add(quesAnswerEntity);
                    }
                }

                GetResponseParams<bool> returnDataObj = service.WEventSubmitEventApply(
                    reqContentObj.special.eventId,
                    reqContentObj.common.userId,
                    userMappingEntity,
                    quesAnswerList);

                respObj.code = returnDataObj.Code;
                respObj.description = returnDataObj.Description;
                //Jermyn20131108 提交问题之后微信推送 Jermyn20131209 更改了业务逻辑，暂时关闭
                //PushWeiXin(reqContentObj.common.openId, loggingSessionInfo, reqContentObj.special.eventId, reqContentObj.common.userId);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "submitEventApply RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                //respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class submitEventApplyReqData : Default.ReqData
        {
            public submitEventApplyReqSpecialData special;
        }

        public class submitEventApplyReqSpecialData
        {
            public string eventId;
            public string userName;
            public string mobile;
            public string email;
            public IList<submitEventApplyReqQuestionData> questions;
        }

        public class submitEventApplyReqQuestionData
        {
            public string questionId;
            public string questionValue;
        }

        public class submitEventApplyRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public submitEventApplyRespContentData content;
        }

        public class submitEventApplyRespContentData
        {
        }

        #endregion

        #region 用户是否已经注册活动

        /// <summary>
        /// 用户是否已经注册活动
        /// </summary>
        public string getUserSignInEvent()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getUserSignInEventRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getUserSignInEventReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getUserSignInEvent ReqContent:{0}",
                        ReqContent)
                });

                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                var service = new WEventUserMappingBLL(loggingSessionInfo);

                int returnDataObj = service.GetUserSignIn(
                    reqContentObj.special.eventId,
                    reqContentObj.common.userId);

                var contentObj = new getUserSignInEventRespContentData();
                if (returnDataObj > 0)
                    contentObj.isSignIn = 1;
                else
                    contentObj.isSignIn = 0;

                respObj.content = contentObj;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getUserSignInEvent RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getUserSignInEventReqData : Default.ReqData
        {
            public getUserSignInEventReqSpecialData special;
        }

        public class getUserSignInEventReqSpecialData
        {
            public string eventId;
        }

        public class getUserSignInEventRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public getUserSignInEventRespContentData content;
        }

        public class getUserSignInEventRespContentData
        {
            public int isSignIn;
        }

        #endregion

        #region 获取活动

        /// <summary>
        /// 获取活动 
        /// </summary>
        public string getEvents()
        {
            string ReqContent = string.Empty;
            string content = string.Empty;
            var respObj = new getEventsRespData();
            string respStr = string.Empty;
            try
            {
                ReqContent = Request["ReqContent"];
                ReqContent = HttpUtility.HtmlDecode(ReqContent);
                var reqContentObj = ReqContent.DeserializeJSONTo<getEventsReqData>();

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEvents ReqContent:{0}",
                        ReqContent)
                });
                if (!string.IsNullOrEmpty(reqContentObj.common.customerId))
                {
                    customerId = reqContentObj.common.customerId;
                }
                LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var service = new LEventsBLL(loggingSessionInfo);

                if (reqContentObj.special.parentEventID == null)
                {
                    reqContentObj.special.parentEventID = "783467B7A9F1425CA3DFC0B03A36B713";
                }

                IList<LEventsEntity> returnDataObj = service.WEventGetWebEvents(
                    new LEventsEntity()
                    {
                        ParentEventID = reqContentObj.special.parentEventID
                    }, 0, 1000);

                var contentObj = new getEventsRespContentData();

                if (returnDataObj != null && returnDataObj.Count > 0)
                {
                    contentObj.eventList = new List<getEventsRespContentItemData>();
                    foreach (var tmpItem in returnDataObj)
                    {
                        if (tmpItem == null) continue;
                        var tmpQues = new getEventsRespContentItemData();
                        tmpQues.eventID = Default.ToStr(tmpItem.EventID);
                        tmpQues.title = Default.ToStr(tmpItem.Title);
                        tmpQues.eventLevel = tmpItem.EventLevel;
                        tmpQues.parentEventID = Default.ToStr(tmpItem.ParentEventID);
                        tmpQues.beginTime = Default.ToStr(tmpItem.BeginTime);
                        tmpQues.endTime = Default.ToStr(tmpItem.EndTime);
                        tmpQues.address = Default.ToStr(tmpItem.Address);
                        tmpQues.cityID = Default.ToStr(tmpItem.CityID);
                        //tmpQues.description = Default.ToStr(tmpItem.Description);
                        tmpQues.imageURL = Default.ToStr(tmpItem.ImageURL);
                        tmpQues.displayIndex = tmpItem.DisplayIndex;
                        tmpQues.customerId = Default.ToStr(tmpItem.CustomerId);

                        contentObj.eventList.Add(tmpQues);
                    }
                    contentObj.totalCount = contentObj.eventList.Count;
                }

                respObj.content = contentObj;

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format(
                        "getEvents RespContent:{0}",
                        respObj.ToJSON())
                });
            }
            catch (Exception ex)
            {
                respObj.code = "103";
                respObj.description = "数据库操作错误";
                respObj.exception = ex.ToString();
            }
            content = respObj.ToJSON();
            return content;
        }

        public class getEventsReqData : Default.ReqData
        {
            public getEventsReqSpecialData special;
        }

        public class getEventsReqSpecialData
        {
            public string parentEventID;
        }

        public class getEventsRespData
        {
            public string code = "200";
            public string description = "操作成功";
            public string exception = null;
            public getEventsRespContentData content;
        }

        public class getEventsRespContentData
        {
            public IList<getEventsRespContentItemData> eventList { get; set; }
            public int totalCount;
        }

        public class getEventsRespContentItemData
        {
            public string eventID;
            public string title;
            public int? eventLevel;
            public string parentEventID;
            public string beginTime;
            public string endTime;
            public string address;
            public string cityID;
            public string description;
            public string imageURL;
            public int? displayIndex;
            public string customerId;
        }

        #endregion

        #region 提交微信平台用户留言接口多美滋妈妈问答

        /// <summary>
        /// 提交微信平台用户留言接口
        /// </summary>
        /// <returns></returns>
        public string SetUserMessageDataWap()
        {
            SetUserMessageDataEntity response = new SetUserMessageDataEntity();
            response.content = new SetUserMessageDataContentEntity();
            var data = Request["ReqContent"].DeserializeJSONTo<SetUserMessageDataReqData>();
            if (data == null || data.special == null)
            {
                response.code = "103";
                response.description = "数据库操作错误";
                response.exception = "请求的数据不能为空";
                return response.ToJSON();
            }

            if (string.IsNullOrEmpty(data.special.toVipType) || string.IsNullOrEmpty(data.special.text))
            {
                response.code = "103";
                response.description = "数据库操作错误";
                response.exception = "接收人类型不能为空或消息不能为空";
                return response.ToJSON();
            }

            #region //判断客户ID是否传递

            if (!string.IsNullOrEmpty(data.common.customerId))
            {
                customerId = data.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

            #endregion

            string content = string.Empty;
            response.code = "200";
            response.description = "操作成功";
            try
            {
                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                WUserMessageEntity queryObj;
                var wUserMessageBLL = new WUserMessageBLL(loggingSessionInfo);

                queryObj = new WUserMessageEntity();
                queryObj.MessageId = JIT.CPOS.Common.Utils.NewGuid();
                queryObj.VipId = data.common.userId;
                queryObj.Text = data.special.text;
                queryObj.OpenId = data.common.openId;
                queryObj.DataFrom = 1;
                queryObj.ToVipType = int.Parse(data.special.toVipType);
                queryObj.CreateTime = DateTime.Now;
                queryObj.CreateBy = "";
                queryObj.LastUpdateBy = "";
                queryObj.LastUpdateTime = DateTime.Now;
                queryObj.MaterialTypeId = "1";
                queryObj.IsDelete = 0;

                wUserMessageBLL.Create(queryObj);
            }
            catch (Exception ex)
            {
                response.code = "201";
                response.description = "失败:" + ex.ToString();
            }
            content = response.ToJSON();
            return content;
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
                    respData.content.VipIntegralDetailList =
                        new List<getVipIntegralDetailListRespVipIntegralDetailData>();
                    foreach (var item in list)
                    {
                        var tmpItemObj = new getVipIntegralDetailListRespVipIntegralDetailData()
                        {
                            VipIntegralDetailId = ToStr(item.VipIntegralDetailID),
                            VipIntegralSourceID = ToStr(item.IntegralSourceID),
                            Integral = ToInt(item.Integral),
                            createTime = ToStr(item.Create_Time),
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

        #region 客户定制APP接口、请求、响应

        #region 接口方法

        private string GetCategoryList()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getCategoriesReqData>();
                reqObj = reqObj == null ? new getCategoriesReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var bll = new ItemCategoryService(loggingSessionInfo);
                var temps = bll.GetCategoriesAndItems(reqObj.common.customerId, reqObj.special.parentId,
                    reqObj.special.pageSize, reqObj.special.page);
                resData.content = new { CategoryInfos = temps };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = "数据库操作错误";
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string GetADList()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getADReqData>();
                reqObj = reqObj == null ? new getADReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                Loggers.Debug(new DebugLogInfo() { Message = "配置库:" + loggingSessionInfo.Conn });
                LNewsBLL bll = new LNewsBLL(loggingSessionInfo);
                var entitys = bll.GetIndexNewsList(reqObj.common.customerId);
                List<object> list = new List<object> { };
                foreach (var item in entitys)
                {
                    list.Add(new
                    {
                        Title = item.NewsTitle,
                        ImageUrl = item.ImageUrl,
                        ContentUrl = item.ContentUrl
                    });
                }
                resData.content = new { ADinfos = list.ToArray() };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = "数据库操作错误";
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string ModifyPWD()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<modifyPWDReqData>();
                reqObj = reqObj == null ? new modifyPWDReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                VipBLL bll = new VipBLL(loggingSessionInfo);
                bll.ModifyPWD(reqObj.common.customerId, reqObj.special.phone, reqObj.special.sourcePWD,
                    reqObj.special.newPWD);
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string SearchStores()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<searchStoresReqData>();
                reqObj = reqObj == null ? new searchStoresReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                UnitService bll = new UnitService(loggingSessionInfo);
                var temps = bll.FuzzyQueryStores(reqObj.special.position, reqObj.common.customerId,
                    reqObj.special.nameLike, reqObj.special.storeId, reqObj.special.districtId,
                    reqObj.special.includeHQ ?? true, reqObj.special.pageSize, reqObj.special.page);
                resData.content = new { StoreInfos = temps };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = "数据库操作错误";
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string DeleteShoppingCart()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<deleteShoppingCartReqData>();
                reqObj = reqObj == null ? new deleteShoppingCartReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                ShoppingCartBLL bll = new ShoppingCartBLL(loggingSessionInfo);
                bll.DeleteShoppingCart(reqObj.special.vipid, reqObj.special.skuIds);
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string IsOrderPaid()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<isOrderPaidReqData>();
                reqObj = reqObj == null ? new isOrderPaidReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                //支付中心接口
                try
                {
                    var inoutService = new InoutService(loggingSessionInfo);
                    var entity = inoutService.GetInoutInfoById(reqObj.special.orderid);
                    resData.content = new { Status = entity.Field1 };
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string GetShoppingCartCount()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getShoppingCartCountReqData>();
                reqObj = reqObj == null ? new getShoppingCartCountReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                ShoppingCartBLL service = new ShoppingCartBLL(loggingSessionInfo);
                var cnt = service.GetShoppingCartByVipId(reqObj.special.vipid);
                resData.content = new { count = cnt };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string GetDistrictList()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getDistrictListReqData>();
                reqObj = reqObj == null ? new getDistrictListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                CityService service = new CityService(loggingSessionInfo);
                var temps = service.GetCityInfoList();
                //var list = temps.Select(t => new
                //    {
                //        DistrictID = t.City_Id,
                //        Province = t.City1_Name,
                //        City = t.City2_Name,
                //        District = t.City3_Name,
                //        ZipCode = t.City_Code
                //    });
                var provinces = temps.GroupBy(t => t.City1_Name).Select(t => new { Name = t.Key });

                var citys =
                    temps.GroupBy(t => new { t.City1_Name, t.City2_Name })
                        .Select(t => new { Province = t.Key.City1_Name, Name = t.Key.City2_Name });
                List<Province> list = new List<Province> { };
                foreach (var item in provinces)
                {
                    Province p = new Province() { Citys = new List<City> { } };
                    p.Name = item.Name;
                    var pcitys = citys.Where(t => t.Province == item.Name).Select(t => t);
                    foreach (var item1 in pcitys)
                    {
                        var city = new City() { Name = item1.Name, Districts = new List<District> { } };
                        var districts =
                            temps.Where(t => t.City1_Name == item.Name && t.City2_Name == item1.Name)
                                .Select(t => new { DistrictID = t.City_Id, Name = t.City3_Name, Code = t.City_Code });
                        foreach (var item2 in districts)
                        {
                            var district = new District()
                            {
                                DistrictID = item2.DistrictID,
                                Code = item2.Code,
                                Name = item2.Name
                            };
                            city.Districts.Add(district);
                        }
                        p.Citys.Add(city);
                    }
                    list.Add(p);
                }
                resData.content = new { Districts = list.ToArray() };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string GetRecentlyUsedStore()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getRecentlyUsedStoreReqData>();
                reqObj = reqObj == null ? new getRecentlyUsedStoreReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                UnitService service = new UnitService(loggingSessionInfo);
                var temp = service.GetRecentlyUsedStore(reqObj.special.position, reqObj.common.customerId,
                    reqObj.common.userId, reqObj.common.openId);
                resData.content = new
                {
                    Store = temp,
                    DeliveryDateList = GetDeliveryDate(loggingSessionInfo)
                };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string GetOrderStatistics()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getOrderStatisticsReqData>();
                reqObj = reqObj == null ? new getOrderStatisticsReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                OrderService service = new OrderService(loggingSessionInfo);
                var temp = service.GetCountByUserAndStatus(reqObj.common.customerId, reqObj.common.userId,
                    reqObj.special.status);
                resData.content = new { Statistics = temp };
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        private string SetOrderPaymentType()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setOrderPaymentTypeReqData>();
                reqObj = reqObj == null ? new setOrderPaymentTypeReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                OrderService service = new OrderService(loggingSessionInfo);
                var temp = service.SetOrderPaymentType(reqObj.special.orderId, reqObj.special.paymentId);
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }
        /// <summary>
        /// 增加评论
        /// </summary>
        /// <returns></returns>
        private string SetEvaluation()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setEvaluationReqData>();
                reqObj = reqObj == null ? new setEvaluationReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var bll = new ObjectEvaluationBLL(loggingSessionInfo);
                var entity = new ObjectEvaluationEntity()
                {
                    //ClientID = reqObj.common.customerId,
                    //MemberID = reqObj.special.memberId,
                    //ItemEvaluationID = Guid.NewGuid().ToString("N"),
                    ObjectID = reqObj.special.objectId,
                    Content = reqObj.special.content,
                    Platform = reqObj.special.platform
                };
                bll.Create(entity);
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }
        /// <summary>
        ///  /// <summary>
        /// 获取评论列表//现在数据库里没有这个表了
        /// </summary>
        /// <returns></returns>
        /// </summary>
        /// <returns></returns>
        private string GetEvaluationList()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];//请求参数
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEvaluationListReqData>();
                reqObj = reqObj == null ? new getEvaluationListReqData() : reqObj;//含有一个special对象，对象里含有objectID，page，pageSize属性

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))//还含有common的属性
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");//获取BS用户登录信息
                var bll = new ObjectEvaluationBLL(loggingSessionInfo);//封装用户登录信息
                //TODO:获取列表信息
                var entitys = bll.GetByVIPAndObject(reqObj.common.customerId, reqObj.common.userId,//怎么还要根据用户的标识？userId是指会员ID
                    reqObj.special.objectID, reqObj.special.page, reqObj.special.pageSize);
                if (entitys.Length > 0)
                {
                    var list = entitys.Select(t => new
                    {
                        Content = t.Content,
                        StarLevel = t.StarLevel,
                        MemberName = t.VipName,
                        EvaluationTime = t.CreateTime
                    });
                    resData.content = new { Count = entitys[0].Count, EvaluationList = list.ToArray() };
                }
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }
        /// <summary>
        /// 根据商品标识获取商品评论集合
        /// </summary>
        /// <returns></returns>
        private string GetItemCommentByItemId()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];//请求参数
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getEvaluationListReqData>();
                reqObj = reqObj == null ? new getEvaluationListReqData() : reqObj;//含有一个special对象，对象里含有objectID，page，pageSize属性

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))//还含有common的属性
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");//获取BS用户登录信息
                var bll = new ObjectEvaluationBLL(loggingSessionInfo);//封装用户登录信息
                //TODO:获取列表信息
                var entitys = bll.GetByVIPAndObject(reqObj.common.customerId, reqObj.common.userId,//怎么还要根据用户的标识？userId是指会员ID
                    reqObj.special.objectID, reqObj.special.page, reqObj.special.pageSize);
                if (entitys.Length > 0)
                {
                    var list = entitys.Select(t => new
                    {
                        Content = t.Content,
                        StarLevel = t.StarLevel,
                        MemberName = t.VipName,
                        EvaluationTime = t.CreateTime
                    });
                    resData.content = new { Count = entitys[0].Count, EvaluationList = list.ToArray() };
                }
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }






        private string GetDistrictsByDistricID()
        {
            EliyaResponseData resData = new EliyaResponseData();
            try
            {
                string reqContent = Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getIntegralRuleList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getDistrictsByDistricIDReqData>();
                reqObj = reqObj == null ? new getDistrictsByDistricIDReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                var bll = new ObjectEvaluationBLL(loggingSessionInfo);
                //TODO:获取列表信息
                var service = new CityService(loggingSessionInfo);
                var list = service.GetSameLevelDistrictsByDistrictID(reqObj.special.districtId);
                resData.content =
                    list.Select(t => new { DistrictID = t.City_Id, Name = t.City3_Name, Code = t.City_Code }).ToArray();
            }
            catch (Exception ex)
            {
                resData.code = "103";
                resData.description = ex.Message;
                resData.exception = ex.ToString();
            }
            return resData.ToJSON();
        }

        #endregion

        #region 请求和响应

        #region 首页广告请求

        public class getADReqData : ReqData
        {
            public object special { get; set; }
        }

        #endregion

        #region 获取首页种类请求

        public class getCategoriesReqData : ReqData
        {
            public getCategoriesSpecialData special { get; set; }
        }

        public class getCategoriesSpecialData
        {
            public Guid? parentId { get; set; }
            public int? pageSize { get; set; }
            public int? page { get; set; }
        }

        #endregion

        #region 商户查询请求

        public class searchStoresReqData : ReqData
        {
            public searchStoresSpecialData special { get; set; }
        }

        public class searchStoresSpecialData
        {
            public string nameLike { get; set; } //模糊查询参数
            public string position { get; set; } //坐标
            public int? pageSize { get; set; }
            public int? page { get; set; }
            public string storeId { get; set; } //StoreID
            public string districtId { get; set; } //区域ID
            public bool? includeHQ { get; set; } //是否包涵总部
        }

        #endregion

        #region 修改密码请求

        public class modifyPWDReqData : ReqData
        {
            public modifyPWDSpecialData special { get; set; }
        }

        public class modifyPWDSpecialData
        {
            public string phone { get; set; }
            public string sourcePWD { get; set; }
            public string newPWD { get; set; }
        }

        #endregion

        #region 删除购物车请求

        public class deleteShoppingCartReqData : ReqData
        {
            public deleteShoppingCartSpecialData special { get; set; }
        }

        public class deleteShoppingCartSpecialData
        {
            public string vipid { get; set; }
            public string[] skuIds { get; set; }
        }

        #endregion

        #region 获取购物车数量请求

        public class getShoppingCartCountReqData : ReqData
        {
            public getShoppingCartCountSpecialData special { get; set; }
        }

        public class getShoppingCartCountSpecialData
        {
            public string vipid { get; set; }
        }

        #endregion

        #region 订单支付查询请求

        public class isOrderPaidReqData : ReqData
        {
            public isOrderPaidSpecialData special { get; set; }
        }

        public class isOrderPaidSpecialData
        {
            public string orderid { get; set; }
        }

        #endregion

        #region 获取区县列表请求

        public class getDistrictListReqData : ReqData
        {
            public getDistrictListSpecialData special { get; set; }
        }

        public class getDistrictListSpecialData
        {
        }

        #endregion

        #region 获取最近门店请求

        public class getRecentlyUsedStoreReqData : ReqData
        {
            public getRecentlyUsedStoreSpecialData special { get; set; }
        }

        public class getRecentlyUsedStoreSpecialData
        {
            public string position { get; set; }
        }

        #endregion

        #region 获取订单统计信息请求

        public class getOrderStatisticsReqData : ReqData
        {
            public getOrderStatisticsSpecialData special { get; set; }
        }

        public class getOrderStatisticsSpecialData
        {
            public string status { get; set; }
        }

        #endregion

        #region 设置订单支付方式请求

        public class setOrderPaymentTypeReqData : ReqData
        {
            public setOrderPaymentTypeSpecialData special { get; set; }
        }

        public class setOrderPaymentTypeSpecialData
        {
            public string orderId { get; set; }
            public string paymentId { get; set; }
        }

        #endregion

        #region 获取评价列表请求

        public class getEvaluationListReqData : ReqData
        {
            public getEvaluationListSpecialData special { get; set; }
        }

        public class getEvaluationListSpecialData
        {
            public string objectID { get; set; }
            public int? page { get; set; }
            public int? pageSize { get; set; }
        }

        #endregion

        #region 设置评价请求

        public class setEvaluationReqData : ReqData
        {
            public setEvaluationSpecialData special { get; set; }
        }

        public class setEvaluationSpecialData
        {
            public string objectId { get; set; }
            public string content { get; set; }
            public string memberId { get; set; }
            public string platform { get; set; }
        }

        #endregion

        #region 根据区县ID获取同市的区县信息请求

        public class getDistrictsByDistricIDReqData : ReqData
        {
            public getDistrictsByDistricIDSpecialData special { get; set; }
        }

        public class getDistrictsByDistricIDSpecialData
        {
            public string districtId { get; set; }
        }

        #endregion

        #region 响应

        public class EliyaResponseData : Default.LowerRespData
        {
            public object content { get; set; }
        }

        #endregion

        #endregion

        #endregion

        #region 泸州老窖抽奖活动注册

        public string setSignUpLzlj()
        {
            string content = string.Empty;
            var respData = new setSignUpLzljRespData();

            try
            {
                string reqContent = Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setSignUpLzlj: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<setSignUpLzljReqData>();
                reqObj = reqObj == null ? new setSignUpLzljReqData() : reqObj;

                //判断客户ID是否传递
                if (string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    respData.code = "2201";
                    respData.description = "customerId不能为空";
                    return respData.ToJSON().ToString();
                }

                customerId = reqObj.common.customerId;
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    respData.code = "2202";
                    respData.description = "特殊参数不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.phone))
                {
                    respData.code = "2203";
                    respData.description = "手机号码不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.name))
                {
                    respData.code = "2204";
                    respData.description = "姓名不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.common.userId))
                {
                    respData.code = "2205";
                    respData.description = "用户ID不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.eventId))
                {
                    respData.code = "2206";
                    respData.description = "活动ID不能为空";
                    return respData.ToJSON().ToString();
                }

                //判断用户是否已经注册活动
                var service = new WEventUserMappingBLL(loggingSessionInfo);

                int returnDataObj = service.GetUserSignIn(reqObj.special.eventId, reqObj.common.userId);
                if (returnDataObj > 0)
                {
                    respData.code = "2207";
                    respData.description = "用户已经注册过该活动";
                    return respData.ToJSON().ToString();
                }

                //判断用户是否存在
                var vipBLL = new VipBLL(loggingSessionInfo);
                var mappingBll = new WEventUserMappingBLL(loggingSessionInfo);

                var vipList = vipBLL.QueryByEntity(new VipEntity { VIPID = reqObj.common.userId, ClientID = customerId },
                    null);
                if (vipList != null && vipList.Length > 0)
                {
                    //用户已存在
                    respData.code = "2208";
                    respData.description = "用户ID已存在";
                    return respData.ToJSON().ToString();
                }
                else
                {
                    vipList = vipBLL.QueryByEntity(new VipEntity { Phone = reqObj.special.phone, ClientID = customerId },
                        null);

                    if (vipList != null && vipList.Length > 0)
                    {
                        //手机号已注册
                        respData.code = "2209";
                        respData.description = "您填写的手机号码已经注册。";
                        return respData.ToJSON().ToString();
                    }
                    else
                    {
                        //用户不存在，插入用户信息
                        VipEntity vipObj = new VipEntity();
                        vipObj.VIPID = reqObj.common.userId;
                        vipObj.WeiXinUserId = reqObj.common.openId;
                        vipObj.VipName = reqObj.special.name;
                        vipObj.VipRealName = reqObj.special.name;
                        vipObj.Phone = reqObj.special.phone;
                        vipObj.VipPasswrod = JIT.Utility.MD5Helper.Encryption("123456");
                        vipObj.VipCode = vipBLL.GetVipCode();
                        vipObj.ClientID = loggingSessionInfo.CurrentUser.customer_id;
                        vipObj.Status = 2;
                        vipBLL.Create(vipObj);

                        //插入用户与活动关联信息                
                        WEventUserMappingEntity mappingEntity = new WEventUserMappingEntity
                        {
                            Mapping = Utils.NewGuid(),
                            UserName = reqObj.special.name,
                            Mobile = reqObj.special.phone,
                            UserID = reqObj.common.userId,
                            EventID = reqObj.special.eventId
                        };
                        mappingBll.Create(mappingEntity);
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
                Message = string.Format("setSignUpLzlj content: {0}", content)
            });
            return content;
        }

        public class setSignUpLzljRespData : Default.LowerRespData
        {
            public setSignUpLzljRespContentData content { get; set; }
        }

        public class setSignUpLzljRespContentData
        {
        }

        public class setSignUpLzljReqData : ReqData
        {
            public setSignUpLzljReqSpecialData special;
        }

        public class setSignUpLzljReqSpecialData
        {
            public string phone { get; set; }
            public string name { get; set; }
            public string eventId { get; set; }
        }

        #endregion
    }

    #region ReqData

    public class ReqData
    {
        public ReqCommonData common { get; set; }
    }

    public class ReqCommonData
    {
        public string locale;
        public string userId;
        public string openId;
        public string signUpId;
        public string customerId;
        public string businessZoneId; //商图ID
        public string channelId; //渠道ID
        public string eventId;
        public string isALD; //1-是
        public string plat;
        public int IsAToC;//是否将ALD会员同步到客户库
    }

    #endregion
}
