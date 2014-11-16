using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using System.Net;
using JIT.CPOS.BS.Entity.Interface;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity.User;
using System.Data;
using JIT.CPOS.BS.Entity.WX;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;
using JIT.Utility.Web;
using JIT.CPOS.Web.SendSMSService;
using JIT.Utility.Notification;

namespace JIT.CPOS.Web.OnlineShopping.data
{
    /// <summary>
    /// DataOnlineShoppingHandler 的摘要说明
    /// </summary>
    public class DataOnlineShoppingHandler : IHttpHandler
    {
        string customerId = "29E11BDC6DAC439896958CC6866FF64E";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        #region GetItemList
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public string GetItemList()
        {
            string content = string.Empty;
            var respData = new getItemListRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

                Stopwatch sw = new Stopwatch(); sw.Start();
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                sw.Stop(); Loggers.Debug(new DebugLogInfo() { Message = "登录，执行时长：[" + sw.ElapsedMilliseconds.ToString() + "]毫秒" });

                //查询参数
                string userId = reqObj.common.userId;
                string itemName = reqObj.special.itemName;      //模糊查询商品名称
                string itemTypeId = reqObj.special.itemTypeId;  //活动标识
                string isExchange = reqObj.special.isExchange;
                string isGroupBy = reqObj.special.isGroupBy;    //是否获取团购商品列表 1=是，否则不是
                int page = reqObj.special.page;         //页码
                int pageSize = reqObj.special.pageSize; //页面数量
                string storeId = ToStr(reqObj.special.storeId);
                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 15;

                //初始化返回对象
                respData.content = new getItemListRespContentData();
                respData.content.itemList = new List<getItemListRespContentDataItem>();
                respData.content.ItemKeeps = new List<getItemListRespContentDataItem>();
                ShoppingCartBLL shoppingCartServer = new ShoppingCartBLL(loggingSessionInfo);

                sw = new Stopwatch(); sw.Start();
                respData.content.shoppingCartCount = shoppingCartServer.GetShoppingCartByVipId(userId);
                sw.Stop(); Loggers.Debug(new DebugLogInfo() { Message = "获取会员的购物车数量，执行时长：[" + sw.ElapsedMilliseconds.ToString() + "]毫秒" });

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                sw = new Stopwatch(); sw.Start();
                var dsItems = itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, false, isExchange, storeId, isGroupBy);
                sw.Stop(); Loggers.Debug(new DebugLogInfo() { Message = "获取所有藏商品列表，执行时长：[" + sw.ElapsedMilliseconds.ToString() + "]毫秒" });

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.itemList = DataTableToObject.ConvertToList<getItemListRespContentDataItem>(dsItems.Tables[0]);
                    //added by zhangwei 增加ItemStatus信息
                    if (!string.IsNullOrEmpty(reqObj.special.beginDate) && !string.IsNullOrEmpty(reqObj.special.endDate))
                    {
                        var dsStoreItemDailyStatus = itemService.GetStoreItemDailyStatuses(reqObj.special.beginDate,
                                                                                           reqObj.special.endDate,
                                                                                           reqObj.special.storeId,
                                                                                           "");
                        if (dsStoreItemDailyStatus.Tables[0].Rows.Count > 0)
                        {
                            var itemDailyStatuses =
                                DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(
                                    dsStoreItemDailyStatus.Tables[0]);
                            foreach (var item in respData.content.itemList)
                            {
                                item.storeItemDailyStatus =
                                    itemDailyStatuses.Where(item1 => item1.SkuID == item.itemId && item1.StoreID == reqObj.special.storeId).ToList();
                            }
                        }

                    }

                    sw = new Stopwatch(); sw.Start();
                    var totalCount = ToInt(dsItems.Tables[1].Rows[0][0].ToString());
                    sw.Stop(); Loggers.Debug(new DebugLogInfo() { Message = "获取商品总数列表，执行时长：[" + sw.ElapsedMilliseconds.ToString() + "]毫秒" });

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

                    foreach (var item in respData.content.itemList)
                    {
                        if (!string.IsNullOrEmpty(item.endTime))
                        {
                            var time = DateTime.Parse(item.endTime);
                            if (time > DateTime.Now)
                            {
                                TimeSpan d3 = time.Subtract(DateTime.Now);
                                item.deadlineTime = d3.Days.ToString() + "天" + d3.Hours.ToString() + "小时" + d3.Minutes.ToString() + "分钟";
                            }
                            else
                            {
                                item.deadlineTime = "0";
                            }
                        }
                    }
                }

                sw = new Stopwatch(); sw.Start();
                var dsItemKeeps = new DataSet();//itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, true, isExchange, storeId);
                sw.Stop(); Loggers.Debug(new DebugLogInfo() { Message = "获取已收藏商品列表，执行时长：[" + sw.ElapsedMilliseconds.ToString() + "]毫秒" });

                //if (dsItemKeeps != null && dsItemKeeps.Tables.Count > 0 && dsItemKeeps.Tables[0].Rows.Count > 0)
                //{
                //    respData.content.ItemKeeps = DataTableToObject.ConvertToList<getItemListRespContentDataItem>(dsItemKeeps.Tables[0]);
                //}
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

            public string TargetUrl { get; set; }

            public decimal price { get; set; }        //商品原价
            public decimal salesPrice { get; set; }   //商品零售价（优惠价）
            public decimal discountRate { get; set; } //商品折扣
            public Int64 displayIndex { get; set; }  //排序
            public string pTypeId { get; set; }      //福利类别标识（团购=2，优惠=1）
            public string pTypeCode { get; set; }    //福利类别缩写（券，团）
            public string CouponURL { get; set; }    //优惠券下载地址
            public string integralExchange { get; set; }    //兑换所需积分
            public string isExchange { get; set; }
            public string itemCategoryName { get; set; } //类别名称 Jermyn20131008
            public int salesPersonCount { get; set; }    //已购买人数量
            public int isShoppingCart { get; set; }      //是否已经加入购物车（1=已加入，0=未加入）
            public string skuId { get; set; }
            public string itemTypeDesc { get; set; } // 菜品特殊分类
            public string itemSortDesc { get; set; } // 菜品排行
            public int salesQty { get; set; } // 吃过人数
            public string createDate { get; set; }      //创建日期
            public string remark { get; set; }      //备注
            public string endTime { get; set; }         //结束日期
            public string deadlineTime { get; set; }    //还有多少时间截止（16天5小时34分）
            public IList<StoreItemDailyStatusEntity> storeItemDailyStatus;
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
            //added by zhangwei for hotel
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public string isGroupBy { get; set; }   //是否获取团购商品列表 1=是，否则不是
        }
        #endregion

        #region GetItemDetail
        /// <summary>
        /// 获取福利商品明细信息
        /// </summary>
        /// <returns></returns>
        public string GetItemDetail()
        {
            string content = string.Empty;
            var respData = new getItemDetailRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                //商品基本信息
                var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
                }
                #region
                else
                {
                    respData.code = "301";
                    respData.description = "没有商品";
                    respData.exception = "";
                }
                #endregion
                if (!string.IsNullOrEmpty(respData.content.endTime))
                {
                    var time = DateTime.Parse(respData.content.endTime);//判断过期时间的
                    if (time > DateTime.Now)
                    {
                        TimeSpan d3 = time.Subtract(DateTime.Now);
                        respData.content.deadlineTime = d3.Days.ToString() + "天" + d3.Hours.ToString() + "小时" + d3.Minutes.ToString() + "分钟";
                    }
                    else
                    {
                        respData.content.deadlineTime = "0";
                    }
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
                // var dsSkus = itemService.GetItemSkuList(itemId);

                // update by wzq 修改会员价
                ItemService itemServiceBll = new ItemService(loggingSessionInfo);
                //花间堂，要传入开始时间和结束时间
                DateTime dtBeginTime;
                DateTime dtEndTime;
                if (!string.IsNullOrEmpty(reqObj.special.beginDate))
                {
                    dtBeginTime = Convert.ToDateTime(reqObj.special.beginDate);
                }
                else
                {
                    dtBeginTime = Convert.ToDateTime("9999/01/01");
                }
                if (!string.IsNullOrEmpty(reqObj.special.beginDate))
                {
                    dtEndTime = Convert.ToDateTime(reqObj.special.endDate);
                }
                else {
                    dtEndTime = Convert.ToDateTime("9999/01/01");
                }

                var dsSkus = itemServiceBll.GetItemSkuList(itemId, userId, customerId,dtBeginTime, dtEndTime);

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

                //added by zhangwei 增加ItemStatus信息
                if (!string.IsNullOrEmpty(reqObj.special.beginDate) && !string.IsNullOrEmpty(reqObj.special.endDate))
                {
                    var dsStoreItemDailyStatus = itemService.GetStoreItemDailyStatuses(reqObj.special.beginDate,
                                                                                       reqObj.special.endDate,
                                                                                       reqObj.special.storeId,
                                                                                       reqObj.special.itemId
                                                                                       );
                    if (dsStoreItemDailyStatus.Tables[0].Rows.Count > 0)
                    {
                        var itemDailyStatuses = DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(dsStoreItemDailyStatus.Tables[0]);
                        respData.content.storeItemDailyStatus = itemDailyStatuses;
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
        //专门为花间堂写的房价

        public string getHotelItemDetail()
        {
            string content = string.Empty;
            var respData = new getItemDetailRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                //商品基本信息
                var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
                }
                #region
                else
                {
                    respData.code = "301";
                    respData.description = "没有商品";
                    respData.exception = "";
                }
                #endregion
                if (!string.IsNullOrEmpty(respData.content.endTime))
                {
                    var time = DateTime.Parse(respData.content.endTime);//判断过期时间的
                    if (time > DateTime.Now)
                    {
                        TimeSpan d3 = time.Subtract(DateTime.Now);
                        respData.content.deadlineTime = d3.Days.ToString() + "天" + d3.Hours.ToString() + "小时" + d3.Minutes.ToString() + "分钟";
                    }
                    else
                    {
                        respData.content.deadlineTime = "0";
                    }
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
                // var dsSkus = itemService.GetItemSkuList(itemId);

                // update by wzq 修改会员价
                ItemService itemServiceBll = new ItemService(loggingSessionInfo);
                //花间堂，要传入开始时间和结束时间
                DateTime dtBeginTime;
                DateTime dtEndTime;
                if (!string.IsNullOrEmpty(reqObj.special.beginDate))
                {
                    dtBeginTime = Convert.ToDateTime(reqObj.special.beginDate);
                }
                else
                {
                    dtBeginTime = Convert.ToDateTime("9999/01/01");
                }
                if (!string.IsNullOrEmpty(reqObj.special.beginDate))
                {
                    dtEndTime = Convert.ToDateTime(reqObj.special.endDate);
                }
                else
                {
                    dtEndTime = Convert.ToDateTime("9999/01/01");
                }

                var dsSkus = itemServiceBll.GetHotelItemSkuList(itemId, userId, customerId, dtBeginTime, dtEndTime);

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

                //added by zhangwei 增加ItemStatus信息
                if (!string.IsNullOrEmpty(reqObj.special.beginDate) && !string.IsNullOrEmpty(reqObj.special.endDate))
                {
                    var dsStoreItemDailyStatus = itemService.GetStoreItemDailyStatuses(reqObj.special.beginDate,
                                                                                       reqObj.special.endDate,
                                                                                       reqObj.special.storeId,
                                                                                       reqObj.special.itemId
                                                                                       );
                    if (dsStoreItemDailyStatus.Tables[0].Rows.Count > 0)
                    {
                        var itemDailyStatuses = DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(dsStoreItemDailyStatus.Tables[0]);
                        respData.content.storeItemDailyStatus = itemDailyStatuses;
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
            public string itemTypeDesc { get; set; }
            public string itemSortDesc { get; set; }
            public int salesQty { get; set; }
            public string remark { get; set; }
            public decimal Forpoints { get; set; }//购买商品获取的积分数
            public string RoomDesc { get; set; }//房间描述
            public string RoomImg { get; set; }//房间图片
            public string itemIntroduce { get; set; } //Jermyn20140217 商品介绍
            public string itemParaIntroduce { get; set; } //Jermyn20140217 商品参数介绍
            public string salesCount { get; set; }      //已购买数量(Jermyn20140318)
            public string deadlineTime { get; set; } //还有多少时间截止（16天5小时34分）
            public IList<getItemDetailRespContentDataImage> imageList { get; set; }     //图片集合
            public IList<getItemDetailRespContentDataSku> skuList { get; set; }         //sku集合
            public IList<getItemDetailRespContentDataSalesUser> salesUserList { get; set; }   //购买用户集合
            public getItemDetailRespContentDataStore storeInfo { get; set; }            //门店对象（一家门店）
            public getItemDetailRespContentDataBrand brandInfo { get; set; }            //品牌信息
            public getItemDetailRespContentDataSkuInfo skuInfo { get; set; }                //默认sku标识
            public IList<getItemDetailRespContentDataProp1> prop1List { get; set; }     //属性1集合
            public IList<StoreItemDailyStatusEntity> storeItemDailyStatus; //状态集合

            public string integralExchange { get; set; }    //兑换所需积分
            public string isExchange { get; set; }//是否积分兑换商品
            public decimal discountRate { get; set; }    //Jermyn20140321折扣

            public decimal GG { get; set; } //返现
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
            public string VipLevelName { get; set; }    //会员等级
            public decimal salesPrice { get; set; }  //优惠价（零售价格）
            public decimal discountRate { get; set; }//折扣
            public decimal integral { get; set; }    //获得积分

            public int integralExchange { get; set; }//需要积分值
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
            public string beginDate { get; set; } //开始日期
            public string endDate { get; set; } //结束日期
            public string storeId { get; set; }
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

        #region GetItemKeep
        public string GetItemKeep()
        {
            string content = string.Empty;
            var respData = new getItemKeepRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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
                #region update by wzq 20140611
                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).GetByID(reqObj.common.userId);

                //var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                //{
                //    WeiXinUserId = reqObj.common.openId
                //}, null);
                if (vipList != null) vipId = vipList.VIPID;
                if (vipId == null)
                {
                    respData.code = "2200";
                    respData.description = "未查询到匹配的VIP信息";
                    return respData.ToJSON().ToString();
                }
                #endregion
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

        #region SetItemKeep
        public string SetItemKeep()
        {
            string content = string.Empty;
            var respData = new setItemKeepRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

        #region GetShoppingCart
        public string GetShoppingCart()
        {
            string content = string.Empty;
            var respData = new getShoppingCartRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

        #region SetShoppingCart
        public string SetShoppingCart()
        {
            string content = string.Empty;
            var respData = new setShoppingCartRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

        #region SetShoppingCartList
        public string SetShoppingCartList()
        {
            string content = string.Empty;
            var respData = new setShoppingCartListRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

        #region GetOrderList
        public string GetOrderList()
        {
            string content = string.Empty;
            var respData = new getOrderListRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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
                    vipId, "", null);
                    tempList.Add(data);
                }

                int totalCount = tempList.Aggregate(0, (i, j) => i + j.ICount);
                var list = tempList.Aggregate(new List<InoutInfo> { }, (i, j) => { i.AddRange(j.InoutInfoList); return i; });

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
                        orderItem.mobile = item.carrier_tel;
                        orderItem.postcode = item.Field5;
                        orderItem.deliveryId = item.Field8;
                        orderItem.deliveryAddress = item.Field4;
                        orderItem.deliveryTime = item.Field9;
                        orderItem.remark = item.remark;
                        orderItem.optiontext = item.optiontext;
                        orderItem.optionvalue = item.optionvalue;
                        orderItem.deliveryName = item.DeliveryName;
                        orderItem.username = item.Field3;
                        orderItem.carrierAddress = item.carrier_address;
                        if (item.Field7 == null || item.Field7.Equals(""))
                        {
                            orderItem.status = item.status;
                            orderItem.statusDesc = item.status_desc;
                        }
                        else
                        {
                            orderItem.status = item.Field7;
                            orderItem.statusDesc = item.Field10;
                        }
                        orderItem.clinchTime = item.create_time;
                        orderItem.carrierName = item.carrier_name;
                        orderItem.receiptTime = item.accpect_time;
                        orderItem.storeId = ToStr(item.purchase_unit_id);
                        orderItem.couponsPrompt = ToStr(item.Field16);
                        orderItem.actualAmount = item.actual_amount;//ToStr(item.actual_amount.ToString("f2"));

                        orderItem.linkMan = item.linkMan;
                        orderItem.linkTel = item.linkTel;
                        orderItem.address = item.address;
                        orderItem.storeName = item.sales_unit_name;
                        orderItem.joinNo = ToStr(ToInt(item.print_times));
                        orderItem.isPayment = ToStr(item.Field1);
                        if (item.create_time != null && item.create_time.Length > 5)
                        {
                            orderItem.createTime = Convert.ToDateTime(item.create_time).ToString("yyyy-MM-dd HH:mm");
                        }
                        //#region update by wzq 添加优惠卷，余额，积分
                        //orderItem.integral = item.integral;
                        //orderItem.vipEndAmount = item.vipEndAmount;
                        //orderItem.couponAmount = item.couponAmount;
                        //#endregion
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
                                //added by zhangwei 2014-2-26预约时间
                                orderDetail.appointmentTime = ToStr(tmpOrderDetail.Field3);
                                orderDetail.imageList = new List<getOrderListRespDetailImageData>();
                                if (tmpOrderDetail.item_id != null && tmpOrderDetail.item_id.Length > 0)
                                {
                                    var tmpImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                                    {
                                        ObjectId = tmpOrderDetail.item_id
                                    }, new OrderBy[] { new OrderBy() { FieldName = "DisplayIndex", Direction = OrderByDirections.Asc } });
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
            public string deliveryId { get; set; }
            public string deliveryAddress { get; set; }
            public string deliveryTime { get; set; }
            public string remark { get; set; }
            public string optiontext { get; set; }
            public int optionvalue { get; set; }
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
            public decimal actualAmount { get; set; }    //实际支付金额

            public string linkMan { get; set; }
            public string linkTel { get; set; }
            public string address { get; set; }
            public string storeName { get; set; }   //门店名称
            public string joinNo { get; set; }      //人
            public string isPayment { get; set; }   //是否付款 1=是
            public string carrierAddress { get; set; }//到店自提地址

            #region update by wzq

            public decimal integral { get; set; }
            public decimal couponAmount { get; set; }
            public decimal vipEndAmount { get; set; }
            #endregion
        }
        public class getOrderListRespDetailData
        {
            public string deliveryId { get; set; }//配送方式,20140220添加
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
            public string appointmentTime { get; set; }//added by zhangwei 预约时间2014-2-26 
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

        #region getOrderList4Blossom
        public string getOrderList4Blossom()
        {
            string content = string.Empty;
            var respData = new getOrderListRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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
                    vipId, "", null);
                    tempList.Add(data);
                }

                int totalCount = tempList.Aggregate(0, (i, j) => i + j.ICount);
                var list = tempList.Aggregate(new List<InoutInfo> { }, (i, j) => { i.AddRange(j.InoutInfoList); return i; });
                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
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
                        orderItem.deliveryId = item.Field8;
                        orderItem.deliveryAddress = item.Field4;
                        orderItem.deliveryTime = item.Field9;
                        orderItem.remark = item.remark;
                        orderItem.optiontext = item.optiontext;
                        orderItem.optionvalue = item.optionvalue;
                        orderItem.deliveryName = item.DeliveryName;
                        orderItem.username = item.Field3;
                        if (item.Field7 == null || item.Field7.Equals(""))
                        {
                            orderItem.status = item.status;
                            orderItem.statusDesc = item.status_desc;
                        }
                        else
                        {
                            orderItem.status = item.Field7;
                            orderItem.statusDesc = item.Field10;
                        }
                        orderItem.clinchTime = item.create_time;
                        orderItem.carrierName = item.carrier_name;
                        orderItem.receiptTime = item.accpect_time;
                        orderItem.storeId = ToStr(item.purchase_unit_id);
                        orderItem.couponsPrompt = ToStr(item.Field16);
                        orderItem.actualAmount = item.actual_amount;//ToStr(item.actual_amount.ToString("f2"));

                        orderItem.linkMan = item.linkMan;
                        orderItem.linkTel = item.linkTel;
                        orderItem.address = item.address;
                        orderItem.storeName = "";//itemService.GetItemDetailByItemIdAndStoreID(storeId, itemId)//item.sales_unit_name;
                        orderItem.joinNo = ToStr(ToInt(item.print_times));
                        orderItem.isPayment = ToStr(item.Field1);
                        if (item.create_time != null && item.create_time.Length > 5)
                        {
                            orderItem.createTime = Convert.ToDateTime(item.create_time).ToString("yyyy-MM-dd HH:mm");
                        }

                        #region 添加优惠卷，余额，积分
                        orderItem.integral = item.integral;
                        orderItem.vipEndAmount = item.vipEndAmount;
                        orderItem.couponAmount = item.couponAmount;
                        #endregion

                        orderItem.orderDetailList = new List<getOrderListRespDetailData>();
                        item.InoutDetailList = inoutService.GetInoutDetailInfoByOrderId(orderItem.orderId);
                        if (item.InoutDetailList != null)
                        {
                            foreach (InoutDetailInfo tmpOrderDetail in item.InoutDetailList)
                            {
                                if (!string.IsNullOrEmpty(tmpOrderDetail.item_id))
                                {
                                    DataSet dataSet = itemService.GetItemDetailByItemIdAndStoreID(orderItem.storeId, tmpOrderDetail.item_id);
                                    if (Utils.IsDataSetValid(dataSet))
                                        orderItem.storeName = dataSet.Tables[0].Rows[0]["storeName"].ToString();
                                }
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
                                //added by zhangwei 2014-2-26预约时间
                                orderDetail.appointmentTime = ToStr(tmpOrderDetail.Field3);
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


        #endregion

        #region GetOrderInfo
        public string GetOrderInfo()
        {
            string content = string.Empty;
            var respData = new getOrderInfoRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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
                OnlineShoppingItemBLL serer = new OnlineShoppingItemBLL(loggingSessionInfo);
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

                    respData.content.linkMan = ToStr(orderInfo.linkMan);
                    respData.content.linkTel = ToStr(orderInfo.linkTel);
                    respData.content.address = ToStr(orderInfo.address);
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
            public string deliveryid { get; set; }  //
            public string actualAmount { get; set; }    //实际支付金额
            public string linkMan { get; set; }
            public string linkTel { get; set; }
            public string address { get; set; }

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

        #region SetOrderInfo
        /// <summary>
        /// 10 订单提交 20130924 新版本 多个商品 同Data.aspx =>setOrderInfoNew
        /// </summary>
        /// <returns></returns>
        public string SetOrderInfo()
        {
            string content = string.Empty;
            var respData = new setOrderInfoNewRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                //reqContent = "{"common":{"locale":"zh","userId":"c45f87741005ab3a4d9a6b6da21e9162","openId":"c45f87741005ab3a4d9a6b6da21e9162","customerId":"f6a7da3d28f74f2abedfc3ea0cf65c01"},"special":{"skuId":"","qty":"","storeId":"","salesPrice":"","stdPrice":"","totalAmount":"640","tableNumber":"","username":"","mobile":"","email":"","remark":"1","deliveryId":"1","deliveryAddress":"","deliveryTime":"","orderDetailList":[{"skuId":"648245A5233C48D9817B561139CE9548","salesPrice":"640","qty":"1"}]}}";
                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoNewReqData>();//解析request参数,转换成对象
                reqObj = reqObj == null ? new setOrderInfoNewReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderInfoNewReqSpecialData();
                }
                if (reqObj.special == null)//不会执行到这句吧，上面已经实例化了
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.orderDetailList == null || reqObj.special.orderDetailList.Count == 0)//商品数量为0
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
                //var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");//通过customerID,来获取用哪个数据库
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, reqObj.common.userId,reqObj.common.IsAToC);//通过customerID,来获取用哪个数据库
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

                string purchaseUnitId = string.Empty;
                orderInfo.TotalQty = ToInt(reqObj.special.qty);
                UnitService unitServer = new UnitService(loggingSessionInfo);

                orderInfo.CarrierID = reqObj.special.storeId;
                #region 如果选择到店自提sales_unit_id保存门店id，否则保存在线商城的Unit_id update by wzq
                if (reqObj.special.storeId == null || reqObj.special.storeId.Trim().Equals(""))
                {
                    orderInfo.StoreId = unitServer.GetUnitByUnitType("OnlineShopping", null).Id; //获取在线商城的门店标识
                }
                else    //送货到家***
                {
                    orderInfo.StoreId = ToStr(reqObj.special.storeId);
                    orderInfo.PurchaseUnitId = ToStr(reqObj.special.storeId);
                }
                #endregion

                if (orderInfo.StoreId == null || orderInfo.StoreId.Equals(""))//在线商城
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
                //orderInfo.CreateBy = ToStr(reqObj.common.userId);
                //orderInfo.LastUpdateBy = ToStr(reqObj.common.userId);

                //不能从common直接获取userId，ALD同步会员是userID已经重新被赋值到loggingSessionInfo.UserID中
                orderInfo.CreateBy = ToStr(loggingSessionInfo.UserID);
                orderInfo.LastUpdateBy = ToStr(loggingSessionInfo.UserID);

                orderInfo.DeliveryId = ToStr(reqObj.special.deliveryId);
                orderInfo.DeliveryTime = ToStr(reqObj.special.deliveryTime);
                orderInfo.DeliveryAddress = ToStr(reqObj.special.deliveryAddress);
                orderInfo.CustomerId = customerId;
                orderInfo.OpenId = ToStr(reqObj.common.openId);
                orderInfo.username = ToStr(reqObj.special.username);
                orderInfo.tableNumber = ToStr(reqObj.special.tableNumber);
                orderInfo.CouponsPrompt = ToStr(reqObj.special.couponsPrompt);
                orderInfo.Remark = ToStr(reqObj.special.remark);
                orderInfo.JoinNo = ToInt(reqObj.special.joinNo);
                orderInfo.IsALD = ToStr(reqObj.common.isALD);
                orderInfo.IsGroupBuy = ToStr(reqObj.special.isGroupBy);
                orderInfo.DataFromId = reqObj.special.dataFromId;

                if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals("") && !ToStr(reqObj.special.actualAmount).Equals(""))
                {
                    orderInfo.ActualAmount = Convert.ToDecimal(ToStr(reqObj.special.actualAmount));
                }
                //这里对没有订单做了判断
                if (orderInfo.OrderId == null || orderInfo.OrderId.Equals(""))
                {
                    orderInfo.OrderId = BaseService.NewGuidPub();
                    orderInfo.OrderDate = System.DateTime.Now.ToString("yyyy-MM-dd");
                    orderInfo.Status = (string.IsNullOrEmpty(reqObj.special.reqBy) || reqObj.special.reqBy == "0") ? "-99" : "100";   //Jermyn20140219 //haibo.zhou20140224,这里这么写，status=-99,是针对微信平台里的，在提交表单之前就生成了订单，所以状态设为-99，而状态设为100是针对app里提交订单时才生成真实订单***
                    orderInfo.StatusDesc = "未审核";
                    //orderInfo.DiscountRate = Convert.ToDecimal((orderInfo.SalesPrice / orderInfo.StdPrice) * 100);
                    //获取订单号
                    TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(loggingSessionInfo);
                    orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(loggingSessionInfo, orderInfo.StoreId);
                }

                if (orderInfo.Status == null || orderInfo.Status.Equals(""))
                {
                    orderInfo.Status = ToStr(reqObj.special.status);
                }


                int i = 1;
                orderInfo.OrderDetailInfoList = new List<InoutDetailInfo>();
                foreach (var detail in reqObj.special.orderDetailList)
                {
                    InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
                    orderDetailInfo.order_id = orderInfo.OrderId;
                    orderDetailInfo.item_name = detail.itemName;
                    orderDetailInfo.order_detail_id = BaseService.NewGuidPub();
                    orderDetailInfo.sku_id = ToStr(detail.skuId);
                    orderDetailInfo.enter_qty = ToInt(detail.qty);
                    orderDetailInfo.order_qty = ToInt(detail.qty);
                    orderDetailInfo.std_price = Convert.ToDecimal(detail.salesPrice);
                    orderDetailInfo.unit_id = orderInfo.StoreId;
                    orderDetailInfo.display_index = i;
                    #region update by wzq
                    //orderDetailInfo.enter_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.enter_price = orderDetailInfo.std_price;
                    #endregion
                    orderDetailInfo.enter_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.retail_price = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    orderDetailInfo.retail_amount = orderDetailInfo.order_qty * orderDetailInfo.std_price;
                    if (reqObj.common.customerId == "92a251898d63474f96b2145fcee2860c")
                    {
                        if ((!string.IsNullOrEmpty(ToStr(detail.beginDate))) || (!string.IsNullOrEmpty(ToStr(detail.endDate))))
                        {
                            DateTime start = new DateTime();
                            if (!DateTime.TryParse(detail.beginDate, out start))
                            {
                                respData.code = "103";
                                respData.description = "时间格式不正确";
                                return respData.ToJSON();
                            }
                            if (!DateTime.TryParse(detail.endDate, out start))
                            {
                                respData.code = "103";
                                respData.description = "时间格式不正确";
                                return respData.ToJSON();
                            }
                        }
                        else
                        {
                            respData.code = "103";
                            respData.description = "时间不能为空!";
                            return respData.ToJSON();
                        }
                    }
                    orderDetailInfo.Field1 = ToStr(detail.beginDate);
                    orderDetailInfo.Field2 = ToStr(detail.endDate);
                    orderDetailInfo.Field3 = ToStr(detail.appointmentTime);
                    orderDetailInfo.discount_rate = 100;
                    i++;
                    orderInfo.OrderDetailInfoList.Add(orderDetailInfo);
                }
                #endregion

                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);

                #region 记录日志

                var inoutStatusBll = new TInoutStatusBLL(loggingSessionInfo);
                inoutStatusBll.Create(new TInoutStatusEntity
                {
                    InoutStatusID = Guid.Parse(Utils.NewGuid()),
                    OrderID = orderInfo.OrderId,
                    OrderStatus = Convert.ToInt32(orderInfo.Status),
                    CustomerID = orderInfo.CustomerId,
                    Remark = "生成订单",
                    StatusRemark = "生成订单[操作人:" + loggingSessionInfo.CurrentUser.User_Name + "]"
                });

                #endregion

                #region 判断是否是阿拉丁平台的订单,如果是则向阿拉丁同步订单
                if (reqObj.common.isALD == "1")
                {
                    //同步阿拉丁平台的用户信息   qianzhi   2014-03-19
                    new VipBLL(loggingSessionInfo).SyncAladingUserInfo(reqObj.common.userId, reqObj.common.customerId);

                    var store = unitServer.GetUnitById(orderInfo.StoreId);
                    if (bReturn)
                    {//o2o下单成功后,将订单同时发送给ALD
                        ALDOrder aldOrder = new ALDOrder();
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
                        aldOrder.OrderDetails = new List<ALDOrderDetail>();
                        if (orderInfo.OrderDetailInfoList != null && orderInfo.OrderDetailInfoList.Count > 0)
                        {
                            List<string> skuIDs = new List<string>();
                            foreach (var detail in orderInfo.OrderDetailInfoList)
                            {
                                var aldOrderDetail = new ALDOrderDetail();
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
                                    if (dr["sku_id"] != DBNull.Value && Convert.ToString(dr["sku_id"]).ToLower() == detail.SourceSKUID.ToLower())
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
                            ALDOrderRequest aldRequest = new ALDOrderRequest();
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
                            var aldRsp = strAldRsp.DeserializeJSONTo<ALDResponse>();
                            if (aldRsp == null || aldRsp.IsSuccess() == false)
                            {
                                respData.code = "114";
                                respData.description = string.Format("向阿拉丁提交订单失败[{0}].", aldRsp != null ? aldRsp.Message : string.Empty);
                                content = respData.ToJSON();
                                return content;
                            }
                        }
                    }
                }

                #endregion

                if (!string.IsNullOrEmpty(reqObj.special.reqBy) && reqObj.special.reqBy.Equals("1"))
                {
                    //订单消息推送
                    var inoutServer = new InoutService(loggingSessionInfo);
                    inoutServer.OrderPushMessage(orderInfo.OrderId, "100");
                }

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
                
            
             //如果是送货到家，根据订单和用户ID来给总金额和实际支付金额加上运费
                    TOrderCustomerDeliveryStrategyMappingBLL tOrderCustomerDeliveryStrategyMappingBLL = new TOrderCustomerDeliveryStrategyMappingBLL(loggingSessionInfo);
                    tOrderCustomerDeliveryStrategyMappingBLL.UpdateOrderAddDeliveryAmount(orderInfo.OrderId, customerId);
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
        /// 根据custoerID和总金额、运送方式(DeliveryId)取得运费
        /// </summary>
        /// <returns></returns>
        public string GetDeliveryAmount()
        {

            string content = string.Empty;
            var respData = new GetDeliveryAmountRespData();//response数据对象
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetDeliveryAmount: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<GetDeliveryAmountReqData>();//request数据对象
                reqObj = reqObj == null ? new GetDeliveryAmountReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new GetDeliveryAmountReqSpecialData();//请求参数中特殊的部分
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.special.totalAmount == null || reqObj.special.totalAmount.Equals(""))//special数据里总金额total_amount不能为空
                {
                    respData.code = "2201";
                    respData.description = "totalAmount不能为空";
                    return respData.ToJSON().ToString();
                }
                if (reqObj.special.DeliveryId == null || reqObj.special.DeliveryId.Equals(""))//special数据里总金额total_amount不能为空
                {
                    respData.code = "2201";
                    respData.description = "DeliveryId不能为空";
                    return respData.ToJSON().ToString();
                }
                decimal totalAmount = 0;
                try
                {
                    totalAmount = Convert.ToDecimal(reqObj.special.totalAmount);//转换
                }
                catch
                {
                    respData.code = "2201";
                    respData.description = "totalAmount必须是数字";
                    return respData.ToJSON().ToString();
                }


                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");//通过用户ID判断使用哪个数据库
                #region 业务处理
                GetDeliveryAmountRespContentData getDeliveryAmountRespContentData = new GetDeliveryAmountRespContentData();//response的content是getOrderInfoRespContentData 对象
                //调用BLL层来取数据
                CustomerDeliveryStrategyBLL serer = new CustomerDeliveryStrategyBLL(loggingSessionInfo);
                CustomerDeliveryStrategyEntity customerDeliveryStrategyEntity = new CustomerDeliveryStrategyEntity();

                //商品基本信息,//下面这种方式比较简单
                //var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                //if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                //{
                //    respData.content = DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
                //}

                customerDeliveryStrategyEntity = serer.GetDeliveryAmount(reqObj.common.customerId, totalAmount, reqObj.special.DeliveryId);//
                //SetOrderEntity对象，比getOrderInfoRespContentData对象属性多，后者只用了自己需要的属性
                if (customerDeliveryStrategyEntity != null)
                {
                    getDeliveryAmountRespContentData.DSId = customerDeliveryStrategyEntity.Id.ToString();//guid转string
                    getDeliveryAmountRespContentData.AmountBegin = ToStr(customerDeliveryStrategyEntity.AmountBegin);
                    getDeliveryAmountRespContentData.AmountEnd = ToStr(customerDeliveryStrategyEntity.AmountEnd);
                    getDeliveryAmountRespContentData.DeliveryAmount = ToStr(customerDeliveryStrategyEntity.DeliveryAmount);

                    respData.Data = getDeliveryAmountRespContentData;
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
        /// 返回对象：根据custoerID和总金额取得运费数据
        /// </summary>
        public class GetDeliveryAmountRespData : Default.LowerRespData
        {
            
          public GetDeliveryAmountRespContentData Data { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class GetDeliveryAmountRespContentData
        {

            public string DSId { get; set; }
            public string AmountBegin { get; set; }
            public string AmountEnd { get; set; }
            public string DeliveryAmount { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class GetDeliveryAmountReqData : ReqData    //ReqData含有common参数对象
        {
            public GetDeliveryAmountReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class GetDeliveryAmountReqSpecialData
        {
            public string totalAmount { get; set; }		    //订单金额 ，数据库里对应着total_amount
            public string DeliveryId { get; set; }		//配送方式
        }


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
            public string couponsPrompt { get; set; } //优惠券提示语（Jermyn20131213--Field16）
            public string actualAmount { get; set; }    //实际需要支付的金额(去掉优惠券的金额Jermyn20131215)
            public string reqBy { get; set; }   //请求0-wap,1-手机.
            public string joinNo { get; set; }  //餐饮--人数
            public string status { get; set; }
            public string isGroupBy { get; set; }  //是否团购订单（Jermyn20140318—Field15）
            public IList<setOrderDetailClass> orderDetailList { get; set; }
            public IList<setOrderCouponClass> couponList { get; set; }  //优惠券集合 （Jermyn20131213--tordercouponmapping
            public int dataFromId { get; set; }
        }



        public class setOrderDetailClass
        {
            public string skuId { get; set; }       //商品SKU标识
            public string salesPrice { get; set; }  //商品销售单价
            public string qty { get; set; }         //商品数量
            public string beginDate { get; set; }
            public string endDate { get; set; }
            public string appointmentTime { get; set; }//added by zhangwei 2014-2-26预约时间
            public string itemName { get; set; }
        }

        public class setOrderCouponClass
        {
            public string couponId { get; set; }       //优惠券标识
        }
        #endregion
        #endregion

        #region SetOrderInfo4ALD
        /// <summary>
        /// 阿拉丁下单
        /// </summary>
        /// <returns></returns>
        public string SetOrderInfo4ALD()
        {
            string content = string.Empty;
            var respData = new setOrderInfoNewRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("setOrderInfo: {0}", reqContent)
                });

                //reqContent = "{\"common\":{\"locale\":\"zh\",\"userId\":\"c45f87741005ab3a4d9a6b6da21e9162\",\"openId\":\"c45f87741005ab3a4d9a6b6da21e9162\",\"customerId\":\"f6a7da3d28f74f2abedfc3ea0cf65c01\"},\"special\":{\"skuId\":\"\",\"aldMemberID\":\"CF15E7CD21604D6B98669270AEDADC67\",\"aldBusinessZoneID\":1,\"qty\":\"\",\"storeId\":\"\",\"salesPrice\":\"\",\"stdPrice\":\"\",\"totalAmount\":\"640\",\"tableNumber\":\"\",\"username\":\"\",\"mobile\":\"\",\"email\":\"\",\"remark\":\"1\",\"deliveryId\":\"1\",\"deliveryAddress\":\"\",\"deliveryTime\":\"\",\"orderDetailList\":[{\"skuId\":\"648245A5233C48D9817B561139CE9548\",\"salesPrice\":\"640\",\"qty\":\"1\"}]}}";
                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderInfoNewReqData4ALD>();
                reqObj = reqObj == null ? new setOrderInfoNewReqData4ALD() : reqObj;
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
                if (reqObj.special.actualAmount != null && !reqObj.special.actualAmount.Equals("") && !ToStr(reqObj.special.actualAmount).Equals(""))
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

                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);
                //阿拉丁平台新增一些订单属性:门店信息,订单类型
                var store = unitServer.GetUnitById(orderInfo.StoreId);
                if (bReturn)
                {//o2o下单成功后,将订单同时发送给ALD
                    ALDOrder aldOrder = new ALDOrder();
                    aldOrder.SourceOrdersID = orderInfo.OrderId;
                    aldOrder.SourceOrdersNO = orderInfo.OrderCode;
                    aldOrder.SourceOrderDate = orderInfo.OrderDate;
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
                    aldOrder.OrderDetails = new List<ALDOrderDetail>();
                    if (orderInfo.OrderDetailInfoList != null && orderInfo.OrderDetailInfoList.Count > 0)
                    {
                        List<string> skuIDs = new List<string>();
                        foreach (var detail in orderInfo.OrderDetailInfoList)
                        {
                            var aldOrderDetail = new ALDOrderDetail();
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
                                if (dr["sku_id"] != DBNull.Value && Convert.ToString(dr["sku_id"]).ToLower() == detail.SourceSKUID.ToLower())
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
                        ALDOrderRequest aldRequest = new ALDOrderRequest();
                        aldRequest.BusinessZoneID = reqObj.special.aldBusinessZoneID;

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
                        var postContent = string.Format("Action=TransportOrders&ReqContent={0}", aldRequest.ToJSON());
                        var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                        var aldRsp = strAldRsp.DeserializeJSONTo<ALDResponse>();
                        if (aldRsp == null || aldRsp.IsSuccess() == false)
                        {
                            respData.code = "114";
                            respData.description = string.Format("向阿拉丁提交订单失败[{0}].", aldRsp != null ? aldRsp.Message : string.Empty);
                            content = respData.ToJSON();
                            return content;
                        }
                    }
                }

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
        /// <summary>
        /// 阿拉丁下单请求
        /// </summary>
        public class setOrderInfoNewReqData4ALD : ReqData
        {
            public setOrderInfoNewReqSpecialData4ALD special;
        }

        /// <summary>
        /// 阿拉丁下单请求的特殊参数
        /// </summary>
        public class setOrderInfoNewReqSpecialData4ALD : setOrderInfoNewReqSpecialData
        {
            /// <summary>
            /// 阿拉丁会员ID
            /// </summary>
            public string aldMemberID { get; set; }

            /// <summary>
            /// 阿拉丁商圈ID
            /// </summary>
            public int? aldBusinessZoneID { get; set; }
        }

        public class ALDOrder
        {
            #region 属性集
            /// <summary>
            /// 会员ID
            /// </summary>
            public Guid? MemberID { get; set; }

            /// <summary>
            /// 订单来源的客户ID（对应o2omarketing的customer_id）
            /// </summary>
            public String SourceClientID { get; set; }

            /// <summary>
            /// 来源订单ID
            /// </summary>
            public string SourceOrdersID { get; set; }

            /// <summary>
            /// 来源订单号
            /// </summary>
            public String SourceOrdersNO { get; set; }

            /// <summary>
            /// 来源订单下单日期
            /// </summary>
            public string SourceOrderDate { get; set; }

            /// <summary>
            /// 来源的门店ID
            /// </summary>
            public String SourceStoreID { get; set; }

            /// <summary>
            /// 门店名称
            /// </summary>
            public string SourceStoreName { get; set; }

            /// <summary>
            /// 门店地址
            /// </summary>
            public string SourceStoreAddress { get; set; }

            /// <summary>
            /// 门店电话
            /// </summary>
            public string SourceStoreTel { get; set; }

            /// <summary>
            /// 客记订单类型(行业划分)
            /// </summary>
            public string DataDeployName { get; set; }

            /// <summary>
            /// 送货方式
            /// </summary>
            public String DeliverType { get; set; }

            /// <summary>
            /// 送货时间方式
            /// </summary>
            public int? DeliverTimeType { get; set; }

            /// <summary>
            /// 收货人
            /// </summary>
            public String ConsigneeName { get; set; }

            /// <summary>
            /// 订单状态
            /// </summary>
            public String Status { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public String Remark { get; set; }

            /// <summary>
            /// 收货人手机号
            /// </summary>
            public String ConsigneePhoneNO { get; set; }

            /// <summary>
            /// 收货人地址
            /// </summary>
            public String ConsigneeAddress { get; set; }

            /// <summary>
            /// 订单总金额
            /// </summary>
            public decimal? OrderTotalAmount { get; set; }

            /// <summary>
            /// 订单商品总数
            /// </summary>
            public int? OrderItemTotalCount { get; set; }

            /// <summary>
            /// 商品总金额
            /// </summary>
            public decimal? OrderItemTotalAmount { get; set; }

            /// <summary>
            /// 配送费
            /// </summary>
            public decimal? Shipment { get; set; }

            /// <summary>
            /// 活动优惠
            /// </summary>
            public decimal? ActivityOffer { get; set; }

            /// <summary>
            /// 代金卷优惠
            /// </summary>
            public decimal? VoucherOffer { get; set; }

            /// <summary>
            /// 订单应付金额
            /// </summary>
            public decimal? OrderFactAmount { get; set; }

            /// <summary>
            /// 订单明细
            /// </summary>
            public List<ALDOrderDetail> OrderDetails { get; set; }
            #endregion
        }

        public class ALDOrderDetail
        {
            #region 属性集
            /// <summary>
            /// 来源SKU ID
            /// </summary>
            public String SourceSKUID { get; set; }

            /// <summary>
            /// 来源商品ID
            /// </summary>
            public string SourceItemID { get; set; }

            /// <summary>
            /// 商品图片地址
            /// </summary>
            public String SourceItemImageUrl { get; set; }

            /// <summary>
            /// 商品名称
            /// </summary>
            public String SourceItemName { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public int? Quantity { get; set; }

            /// <summary>
            /// 单价
            /// </summary>
            public decimal? Price { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { get; set; }

            /// <summary>
            /// SKU的规格1
            /// </summary>
            public string SourceSKUProp1 { get; set; }

            /// <summary>
            /// SKU规格2
            /// </summary>
            public string SourceSKUProp2 { get; set; }

            /// <summary>
            /// SKU规格3
            /// </summary>
            public string SourceSKUProp3 { get; set; }

            /// <summary>
            /// SKU规格4
            /// </summary>
            public string SourceSKUProp4 { get; set; }
            #endregion
        }
        public class ALDResponse
        {
            public int? ResultCode { get; set; }
            public string Message { get; set; }

            public bool IsSuccess()
            {
                if (this.ResultCode.HasValue && this.ResultCode.Value >= 200 && this.ResultCode.Value < 300)
                    return true;
                else
                    return false;
            }

        }

        public class ALDOrderRequest
        {
            public int? Locale { get; set; }
            public Guid? UserID { get; set; }
            public int? BusinessZoneID { get; set; }
            public string Token { get; set; }
            public object Parameters { get; set; }
        }
        #endregion

        #region CancelOrders
        /// <summary>
        /// 用户取消订单
        /// </summary>
        /// <returns></returns>
        public string CancelOrders()
        {
            string content = string.Empty;
            var respData = new getOrdersRespData();
            string reqContent = HttpContext.Current.Request["ReqContent"];
            try
            {
                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getOrdersReqData>();
                reqObj = reqObj == null ? new getOrdersReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getOrdersReqSpecialData();
                }
                #endregion

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                #region 接收参数
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(reqObj.special.ordersId))
                {
                    pParams.Add("pOrdersID", reqObj.special.ordersId);
                }
                pParams.Add("pOrdersType", "2");
                pParams.Add("pOrdersStatus", "400");
                pParams.Add("pCheckResult", "");
                pParams.Add("pOrdersDesc", "已取消");
                pParams.Add("pRemark", "");
                #endregion

                new TInoutBLL(loggingSessionInfo).OrdersApprove(pParams);

                respData.code = "200";
                respData.description = "操作成功";
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
        public class getOrdersRespData : Default.LowerRespData
        {
            public getOrdersRespContentData content { get; set; }
        }
        public class getOrdersRespContentData
        {
            public string vipID { get; set; }
            public IList<getOrdersRespContentDataItem> ordersList { get; set; }
        }
        public class getOrdersRespContentDataItem
        {
            public string ordersId { get; set; }
        }
        public class getOrdersReqData : ReqData
        {
            public getOrdersReqSpecialData special;
        }
        public class getOrdersReqSpecialData
        {
            public string ordersId { get; set; }
        }
        #endregion
        #endregion

        #region SendMail
        /// <summary>
        /// 下单成功发送邮件
        /// </summary>
        public string SendMail()
        {
            string content = string.Empty;
            var respData = new getSendMailRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("getVipDetail: {0}", reqContent)
                });

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getSendMailReqData>();
                reqObj = reqObj == null ? new getSendMailReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                #endregion


                #region 业务处理
                respData.content = new getSendMailRespContentData();

                #region 邮件发送
                try
                {
                    Loggers.Debug(new DebugLogInfo() { ClientID = loggingSessionInfo.ClientID, UserID = loggingSessionInfo.UserID, Message = "发送邮件开始[" + DateTime.Now.ToString() + "]" });

                    XmlManager xml = new XmlManager(ConfigurationManager.AppSettings["xmlFile"]);

                    FromSetting fs = new FromSetting();
                    fs.SMTPServer = xml.SelectNodeText("//Root/BlossomMail//SMTPServer", 0);
                    fs.SendFrom = xml.SelectNodeText("//Root/BlossomMail//MailSendFrom", 0);
                    fs.UserName = xml.SelectNodeText("//Root/BlossomMail//MailUserName", 0);
                    fs.Password = xml.SelectNodeText("//Root/BlossomMail//MailUserPassword", 0);

                    if (!string.IsNullOrEmpty(reqObj.special.type) && reqObj.special.type == "submit")
                    {
                        Mail.SendMail(fs, xml.SelectNodeText("//Root/BlossomMail//MailTo", 0), xml.SelectNodeText("//Root/BlossomMail//MailTitle", 0), xml.SelectNodeText("//Root/BlossomMail/MailBody").Replace("#UserName#", reqObj.special.UserName).Replace("#Mobile#", reqObj.special.Mobile).Replace("#StoreName#", reqObj.special.StoreName).Replace("#RoomName#", reqObj.special.RoomName).Replace("#OrderNo#", reqObj.special.OrderNo).Replace("#In-Out-Date#", reqObj.special.OrderDate), null);

                    }
                    else if (!string.IsNullOrEmpty(reqObj.special.type) && reqObj.special.type == "cancel")
                    {
                        Mail.SendMail(fs, xml.SelectNodeText("//Root/BlossomMail//MailTo", 0), xml.SelectNodeText("//Root/BlossomMail//MailCancelTitle", 0), xml.SelectNodeText("//Root/BlossomMail/MailCancelBody").Replace("#UserName#", reqObj.special.UserName).Replace("#Mobile#", reqObj.special.Mobile).Replace("#StoreName#", reqObj.special.StoreName).Replace("#RoomName#", reqObj.special.RoomName).Replace("#OrderNo#", reqObj.special.OrderNo).Replace("#In-Out-Date#", reqObj.special.OrderDate), null);
                    }

                    Loggers.Debug(new DebugLogInfo() { ClientID = loggingSessionInfo.ClientID, UserID = loggingSessionInfo.UserID, Message = "发送邮件结束[" + DateTime.Now.ToString() + "]" });


                }
                catch
                {
                    respData.code = "111";
                    respData.description = "邮箱发送失败,请稍后重试";
                    content = respData.ToJSON();
                    return content;
                }
                #endregion

                respData.code = "200";
                respData.description = "操作成功";

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
        public class getSendMailRespData : Default.LowerRespData
        {
            public getSendMailRespContentData content { get; set; }
        }
        public class getSendMailRespContentData
        {
        }
        public class getSendMailReqData : ReqData
        {
            public getSendMailReqSpecialData special;
        }
        public class getSendMailReqSpecialData
        {
            //类型
            public string type { set; get; }

            public string mailTo { get; set; }
            //客户名称
            public string UserName { get; set; }
            //手机号码
            public string Mobile { get; set; }
            //酒店名称
            public string StoreName { get; set; }
            //房间名称
            public string RoomName { get; set; }
            //订单编号
            public string OrderNo { get; set; }
            //订单日期
            public string OrderDate { get; set; }
            public string title { get; set; }
            public string body { get; set; }
        }
        #endregion
        #endregion

        #region GetItemDetailForHotel
        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        /// <returns></returns>
        public string GetItemDetailForHotel()
        {
            string content = string.Empty;
            var respData = new getItemDetailRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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
                string itemId = reqObj.special.itemId;
                string storeId = reqObj.special.storeId;

                //初始化返回对象
                respData.content = new getItemDetailRespContentData();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                //商品基本信息
                var dsItems = itemService.GetItemDetailByItemId(itemId, userId);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content = DataTableToObject.ConvertToObject<getItemDetailRespContentData>(dsItems.Tables[0].Rows[0]);
                }
                respData.content.storeInfo = new getItemDetailRespContentDataStore();
                //门店信息
                var dsStore = itemService.GetItemDetailByItemIdAndStoreID(storeId, itemId);
                if (dsStore != null && dsStore.Tables.Count > 0 && dsStore.Tables[0].Rows.Count > 0)
                {
                    respData.content.storeInfo = DataTableToObject.ConvertToObject<getItemDetailRespContentDataStore>(dsStore.Tables[0].Rows[0]);
                }
                #region 获取商品属性集合
                //added by zhangwei 增加ItemStatus信息
                if (!string.IsNullOrEmpty(reqObj.special.beginDate) && !string.IsNullOrEmpty(reqObj.special.endDate))
                {
                    //var dsStoreItemDailyStatus = itemService.GetStoreItemDailyStatuses(reqObj.special.beginDate,
                    //                                                                   reqObj.special.endDate,
                    //                                                                   reqObj.special.storeId,
                    //                                                                   reqObj.special.itemId,
                    //                                                                   userId,
                    //                                                                   customerId
                    //                                                                   );
                    //update by wzq 添加会员价
                    var dsStoreItemDailyStatus = itemService.GetStoreItemDailyStatuses(reqObj.special.beginDate,
                                                                                       reqObj.special.endDate,
                                                                                       reqObj.special.storeId,
                                                                                       reqObj.special.itemId,
                                                                                       userId,
                                                                                       customerId
                                                                                       );
                    if (dsStoreItemDailyStatus.Tables[0].Rows.Count > 0)
                    {
                        var itemDailyStatuses = DataTableToObject.ConvertToList<StoreItemDailyStatusEntity>(dsStoreItemDailyStatus.Tables[0]);
                        respData.content.storeItemDailyStatus = itemDailyStatuses;
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
        #endregion

        #region SetOrderAddress
        public string SetOrderAddress()
        {
            string content = string.Empty;
            var respData = new setOrderInfoNewRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetOrderAddress: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderAddressReqData>();
                reqObj = reqObj == null ? new setOrderAddressReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderAddressReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
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
                if (string.IsNullOrEmpty(reqObj.special.orderID))
                {
                    respData.code = "2203";
                    respData.description = "orderId不能为空";
                    return respData.ToJSON().ToString();
                }
                #endregion

                OnlineShoppingItemBLL service = new OnlineShoppingItemBLL(loggingSessionInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.DeliveryId = reqObj.special.deliveryId;
                orderInfo.OrderId = ToStr(reqObj.special.orderID);
                orderInfo.linkMan = ToStr(reqObj.special.linkMan);
                orderInfo.linkTel = ToStr(reqObj.special.linkTel);
                orderInfo.address = ToStr(reqObj.special.address);

                if (service.SetOrderAddress(orderInfo))
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                }
                else
                {
                    respData.code = "111";
                    respData.description = "操作失败";
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
        /// 传输的参数对象
        /// </summary>
        public class setOrderAddressReqData : ReqData
        {
            public setOrderAddressReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOrderAddressReqSpecialData
        {
            public string deliveryId { get; set; }
            public string orderID { get; set; }
            public string linkMan { get; set; }
            public string linkTel { get; set; }
            public string address { get; set; }
        }
        #endregion

        #endregion

        #region SetOrderIntegralInfo
        public string SetOrderIntegralInfo()
        {
            string content = string.Empty;
            var respData = new setOrderInfoNewRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetOrderIntegralInfo: {0}", reqContent)
                });

                #region //解析请求字符串 chech
                var reqObj = reqContent.DeserializeJSONTo<setOrderIntegralReqData>();
                reqObj = reqObj == null ? new setOrderIntegralReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new setOrderIntegralReqSpecialData();
                }
                if (reqObj.special == null)
                {
                    respData.code = "102";
                    respData.description = "没有特殊参数";
                    return respData.ToJSON().ToString();
                }

                if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
                {
                    respData.code = "2206";
                    respData.description = "userId不能为空";
                    return respData.ToJSON().ToString();
                }

                if (string.IsNullOrEmpty(reqObj.special.quantity) || ToInt(reqObj.special.quantity) <= 0)
                {
                    respData.code = "2206";
                    respData.description = "quantity不能为空且需大于0";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.itemID))
                {
                    respData.code = "2206";
                    respData.description = "itemID不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.linkMan))
                {
                    respData.code = "2206";
                    respData.description = "linkMan不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.linkTel))
                {
                    respData.code = "2206";
                    respData.description = "linkTel不能为空";
                    return respData.ToJSON().ToString();
                }
                if (string.IsNullOrEmpty(reqObj.special.address))
                {
                    respData.code = "2206";
                    respData.description = "address不能为空";
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

                OnlineShoppingItemBLL service = new OnlineShoppingItemBLL(loggingSessionInfo);

                OrderIntegralEntity entity = new OrderIntegralEntity();
                entity.OrderNo = "W" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999);
                entity.ItemID = ToStr(reqObj.special.itemID);
                entity.Quantity = ToInt(reqObj.special.quantity);
                entity.VIPID = vipId;
                entity.LinkMan = ToStr(reqObj.special.linkMan);
                entity.LinkTel = ToStr(reqObj.special.linkTel);
                entity.CityID = ToStr(reqObj.special.cityID);
                entity.Address = ToStr(reqObj.special.address);

                string res = "";
                if (service.SetOrderIntegralInfo(entity, reqObj.common.customerId, out res))
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                }
                else
                {
                    respData.code = "111";
                    respData.description = res;
                }
                #region 返回信息设置
                //respData.content = new setOrderInfoNewRespContentData();
                //respData.content.orderId = orderInfo.OrderId;
                //respData.exception = strError;
                //respData.description = strMsg;
                //if (!bReturn)
                //{
                //    respData.code = "111";
                //}
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
        public class setOrderIntegralRespData : Default.LowerRespData
        {
            public setOrderIntegralRespContentData content { get; set; }
        }
        /// <summary>
        /// 返回的具体业务数据对象
        /// </summary>
        public class setOrderIntegralRespContentData
        {
            public string orderId { get; set; }
        }
        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class setOrderIntegralReqData : ReqData
        {
            public setOrderIntegralReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class setOrderIntegralReqSpecialData
        {
            public string itemID { get; set; }//商品ID
            public string quantity { get; set; }//商品数量

            public string linkMan { get; set; }//收货联系人
            public string linkTel { get; set; }//收货联系电话
            public string cityID { get; set; }//收货省市
            public string address { get; set; }//收货地址
        }
        #endregion
        #endregion

        #region GetVIPAddressList
        public string GetVIPAddressList()
        {
            string content = string.Empty;
            var respData = new getVipAddressRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetVIPAddressList: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipAddressReqData>();
                reqObj = reqObj == null ? new getVipAddressReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getVipAddressReqSpecialData();
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

                respData.content = new getVipAddressRespContentData();

                VipAddressBLL service = new VipAddressBLL(loggingSessionInfo);

                IList<VipAddressEntity> list = service.GetVIPAddressList(vipId);

                if (list != null)
                {
                    respData.content.itemList = new List<getVipAddressRespCourseData>();
                    foreach (var item in list)
                    {
                        respData.content.itemList.Add(new getVipAddressRespCourseData()
                        {
                            vipAddressID = ToStr(item.VipAddressID),
                            vipid = ToStr(item.VIPID),
                            linkMan = ToStr(item.LinkMan),
                            linkTel = ToStr(item.LinkTel),
                            cityID = ToStr(item.CityID),
                            isDefault = ToStr(item.IsDefault),
                            province = ToStr(item.Province),
                            cityName = ToStr(item.CityName),
                            districtName = item.DistrictName,
                            address = ToStr(item.Address),
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
        #region 参数类
        public class getVipAddressRespData : Default.LowerRespData
        {
            public getVipAddressRespContentData content { get; set; }
        }
        public class getVipAddressRespContentData
        {
            public string isNext { get; set; }
            public IList<getVipAddressRespCourseData> itemList { get; set; }
        }
        public class getVipAddressRespCourseData
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
        public class getVipAddressReqData : ReqData
        {
            public getVipAddressReqSpecialData special;
        }
        public class getVipAddressReqSpecialData
        {
            public int page { get; set; }
            public int pageSize { get; set; }

            public string vipAddressID { get; set; }
            public string deliveryId { get; set; }
            public string vipid { get; set; }
            public string linkMan { get; set; }
            public string linkTel { get; set; }
            public string cityID { get; set; }
            public string address { get; set; }
            public string isDefault { get; set; }
            public string isDelete { get; set; }
        }
        #endregion

        #endregion

        #region SetVIPAddress
        public string SetVIPAddress()
        {
            string content = string.Empty;
            var respData = new getVipAddressRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetVIPAddress: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipAddressReqData>();
                reqObj = reqObj == null ? new getVipAddressReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                if (reqObj.special == null)
                {
                    reqObj.special = new getVipAddressReqSpecialData();
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

                respData.content = new getVipAddressRespContentData();

                VipAddressBLL service = new VipAddressBLL(loggingSessionInfo);
                VipAddressEntity entity = new VipAddressEntity();
                entity = service.GetByID(reqObj.special.vipAddressID);
                if (entity == null)
                {
                    entity = new VipAddressEntity();
                }
                entity.VipAddressID = reqObj.special.vipAddressID;

                entity.VIPID = vipId;
                if (!string.IsNullOrEmpty(reqObj.special.linkMan))
                {
                    entity.LinkMan = reqObj.special.linkMan;
                }
                if (!string.IsNullOrEmpty(reqObj.special.linkTel))
                {
                    entity.LinkTel = reqObj.special.linkTel;
                }
                if (!string.IsNullOrEmpty(reqObj.special.cityID))
                {
                    entity.CityID = reqObj.special.cityID;
                }
                if (!string.IsNullOrEmpty(reqObj.special.address))
                {
                    entity.Address = reqObj.special.address;
                }
                if (!string.IsNullOrEmpty(reqObj.special.isDefault))
                {
                    entity.IsDefault = ToInt(reqObj.special.isDefault);
                }
                entity.IsDelete = ToInt(reqObj.special.isDelete);

                if (service.EditVipAddress(entity))
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                }
                else
                {
                    respData.code = "111";
                    respData.description = "操作失败";
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
        #endregion

        #region GetVIPAddressDefault
        //public string SetVIPAddress()
        //{
        //    string content = string.Empty;
        //    var respData = new getVipAddressRespData();
        //    try
        //    {
        //        string reqContent = HttpContext.Current.Request["ReqContent"];

        //        Loggers.Debug(new DebugLogInfo()
        //        {
        //            Message = string.Format("SetVIPAddress: {0}", reqContent)
        //        });

        //        //解析请求字符串
        //        var reqObj = reqContent.DeserializeJSONTo<getVipAddressReqData>();
        //        reqObj = reqObj == null ? new getVipAddressReqData() : reqObj;

        //        //判断客户ID是否传递
        //        if (!string.IsNullOrEmpty(reqObj.common.customerId))
        //        {
        //            customerId = reqObj.common.customerId;
        //        }
        //        var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

        //        if (reqObj.special == null)
        //        {
        //            reqObj.special = new getVipAddressReqSpecialData();
        //            reqObj.special.page = 1;
        //            reqObj.special.pageSize = 15;
        //        }
        //        if (reqObj.special == null)
        //        {
        //            respData.code = "102";
        //            respData.description = "没有特殊参数";
        //            return respData.ToJSON().ToString();
        //        }

        //        if (reqObj.common.openId == null || reqObj.common.openId.Equals(""))
        //        {
        //            respData.code = "2202";
        //            respData.description = "openId不能为空";
        //            return respData.ToJSON().ToString();
        //        }

        //        var vipId = "";
        //        var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
        //        {
        //            WeiXinUserId = reqObj.common.openId
        //        }, null);
        //        if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;
        //        if (vipId == null || vipId.Length == 0)
        //        {
        //            //respData.code = "2200";
        //            //respData.description = "未查询到匹配的VIP信息";
        //            //return respData.ToJSON().ToString();
        //            vipId = ToStr(reqObj.common.userId);
        //        }

        //        respData.content = new getVipAddressRespContentData();

        //        VipAddressBLL service = new VipAddressBLL(loggingSessionInfo);

        //        VipAddressEntity entity = new VipAddressEntity();
        //        entity.VipAddressID = reqObj.special.vipAddressID;
        //        entity.VIPID = vipId;
        //        entity.LinkMan = reqObj.special.linkMan;
        //        entity.LinkTel = reqObj.special.linkTel;
        //        entity.CityID = reqObj.special.cityID;
        //        entity.Address = reqObj.special.address;
        //        entity.IsDefault = ToInt(reqObj.special.isDefault);
        //        entity.IsDelete = ToInt(reqObj.special.isDelete);

        //        if (service.EditVipAddress(entity))
        //        {
        //            respData.code = "200";
        //            respData.description = "操作成功";
        //        }
        //        else
        //        {
        //            respData.code = "111";
        //            respData.description = "操作失败";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        respData.code = "103";
        //        respData.description = "数据库操作错误";
        //        respData.exception = ex.ToString();
        //    }
        //    content = respData.ToJSON();
        //    return content;
        //}
        #endregion

        #region GetVipValidIntegral
        /// <summary>
        /// 获取Vip会员积分
        /// </summary>
        /// <returns></returns>
        public string GetVipValidIntegral()
        {
            string content = string.Empty;
            var respData = new getItemListRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetVipValidIntegral: {0}", reqContent)
                });

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getItemListReqData>();
                reqObj = reqObj == null ? new getItemListReqData() : reqObj;

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
                //查询参数
                var vipId = "";
                var vipList = (new VipBLL(loggingSessionInfo)).QueryByEntity(new VipEntity()
                {
                    WeiXinUserId = reqObj.common.openId
                }, null);
                if (vipList != null && vipList.Length > 0) vipId = vipList[0].VIPID;

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetVipValidIntegral(vipId);
                respData.data = dsItems;
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
        #endregion

        #region OrderPay
        /// <summary>
        /// 订单支付
        /// </summary>
        /// <returns></returns>
        public string OrderPay()
        {
            var respData = new orderPayRespData();
            string reqContent = HttpContext.Current.Request["ReqContent"];

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("OrderPay: {0}", reqContent)
            });

            var reqObj = reqContent.DeserializeJSONTo<orderPayReqData>();
            reqObj = reqObj == null ? new orderPayReqData() : reqObj;
            if (reqObj.special == null)
            {
                reqObj.special = new orderPayReqSpecialData();
            }
            if (reqObj.special == null)
            {
                respData.code = "102";
                respData.description = "没有特殊参数";
                return respData.ToJSON().ToString();
            }
            if (reqObj.common.customerId == null || reqObj.common.customerId.Equals(""))
            {
                respData.code = "2206";
                respData.description = "customerId不能为空";
                return respData.ToJSON().ToString();
            }
            if (reqObj.common.userId == null || reqObj.common.userId.Equals(""))
            {
                respData.code = "2206";
                respData.description = "userId不能为空";
                return respData.ToJSON().ToString();
            }

            if (string.IsNullOrEmpty(reqObj.special.payChannelID))
            {
                respData.code = "2206";
                respData.description = "PayChannelID不能为空";
                return respData.ToJSON().ToString();
            }
            if (string.IsNullOrEmpty(reqObj.special.orderID))
            {
                respData.code = "2206";
                respData.description = "orderID不能为空";
                return respData.ToJSON().ToString();
            }

            if (!string.IsNullOrEmpty(reqObj.common.customerId))
            {
                customerId = reqObj.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            decimal appOrderAmount = new OnlineShoppingItemBLL(loggingSessionInfo).GetOrderAmmount(reqObj.special.orderID);

            #region 判断是否符合支付状态,是否已支付
            var inoutbll = new InoutService(loggingSessionInfo);
            var orderEntity = inoutbll.GetInoutInfoById(reqObj.special.orderID);
            if (orderEntity.status != "100" && orderEntity.status != "200" && orderEntity.status != "300" && orderEntity.status != "500")
            {
                respData.code = "401";
                respData.description = string.Format("当前状态:{0},不允许付款", orderEntity.status);
                return respData.ToJSON();
            }
            if (orderEntity.Field1 == "1")
            {
                respData.code = "402";
                respData.description = "此订单已经支付,支付请求被拒绝";
                return respData.ToJSON();
            }
            #endregion

            if (reqObj.special.isWeiXinPay)
            {//微信支付
                OrderPackage op = new OrderPackage();
                op.OrderNO = reqObj.special.orderID;
                op.TotalAmount = (appOrderAmount * 100).ToString("F0");
                op.ClientIP = HttpContext.Current.GetClientIP();
                var json = WeiXinPayGateway.GeneratePreOrderRequest(op);
                //
                respData.code = "200";
                respData.description = "操作成功";
                respData.content = new orderPayRespContentData();
                respData.content.OrderID = reqObj.special.orderID;
                respData.content.WeiXinPreOrderContent = json;
            }
            else
            {//非微信支付
                string pUrlPath = ConfigurationManager.AppSettings["paymentcenterUrl"];
                string pReturnUrl = reqObj.special.returnUrl;
                //测试是否正确跳转URL
                //pReturnUrl = "http://121.199.42.125:6001/CallBack.ashx";
                //Json参数准备
                string jsonString = string.Format("action=CreateOrder&request={{\"AppID\":{0},\"ClientID\":\"{1}\",\"UserID\":\"{2}\",\"Token\":null,\"Parameters\":{{\"PayChannelID\":{3},\"AppOrderID\":\"{4}\",\"AppOrderTime\":\"{5}\",\"AppOrderAmount\":{6},\"AppOrderDesc\":\"{7}\",\"Currency\":{8},\"MobileNO\":\"{9}\",\"ReturnUrl\":\"{10}\",\"DynamicID\":null,\"DynamicIDType\":null}}}}",
                    1,
                    reqObj.common.customerId,
                    reqObj.common.userId,
                    reqObj.special.payChannelID,
                    reqObj.special.orderID,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                    ToInt(appOrderAmount * 100),
                    //1,//测试用，支付一分钱
                    string.IsNullOrEmpty(reqObj.special.orderDesc) ? "jitmarketing" : reqObj.special.orderDesc,
                    string.IsNullOrEmpty(reqObj.special.currency) ? "1" : reqObj.special.currency,
                    reqObj.special.mobileNO,
                    pReturnUrl
                    );

                string httpResponse = SendHttpRequest(pUrlPath, jsonString);
                //反序列化
                var payres = httpResponse.DeserializeJSONTo<orderPayCenterContentData>();

                if (payres.ResultCode == 0)
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                    respData.content = new orderPayRespContentData();
                    respData.content.OrderID = payres.Datas.OrderID;
                    respData.content.PayUrl = payres.Datas.PayUrl;
                    new OnlineShoppingItemBLL(loggingSessionInfo).SetOrderPayCenterCode(reqObj.special.orderID, payres.Datas.OrderID);
                }
                else
                {
                    respData.code = payres.ResultCode.ToString();
                    respData.description = payres.Message;
                    //respData.content.PayUrl = payres.Datas.PayUrl;
                }
            }

            #region 更新订单支付类型

            TPaymentTypeCustomerMappingBLL mappingBll = new TPaymentTypeCustomerMappingBLL(loggingSessionInfo);
            var paymentTypeList = mappingBll.QueryByEntity(new TPaymentTypeCustomerMappingEntity { ChannelId = reqObj.special.payChannelID }, null);

            if (paymentTypeList != null && paymentTypeList.Length > 0)
            {
                OrderService service = new OrderService(loggingSessionInfo);
                service.SetOrderPaymentType(reqObj.special.orderID, paymentTypeList.FirstOrDefault().PaymentTypeID);
            }

            #endregion

            return respData.ToJSON();
        }


        /// <summary>
        /// 订单支付(提供给阿拉丁平台)
        /// </summary>
        /// <returns></returns>
        public string OrderPay4ALD()
        {
            var respData = new orderPayRespData();
            string reqContent = HttpContext.Current.Request["ReqContent"];

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("OrderPay: {0}", reqContent)
            });

            //reqContent = "{\"common\":{\"locale\":\"zh\",\"userId\":\"37785D9DACE44EDAAF5A1D7D7D519F48\",\"customerId\":\"be39de140f33488588a2eca81d1e4744\"},\"special\":{\"currency\":1,\"orderID\":\"d3a1322f54774a43ab960d453adad250\",\"mobileNO\":\"13888888888\",\"payChannelID\":1,\"orderDesc\":\"\"}}";

            var reqObj = reqContent.DeserializeJSONTo<orderPayReqData>();
            reqObj = reqObj == null ? new orderPayReqData() : reqObj;
            if (reqObj.special == null)
            {
                reqObj.special = new orderPayReqSpecialData();
            }
            if (reqObj.special == null)
            {
                respData.code = "102";
                respData.description = "没有特殊参数";
                return respData.ToJSON().ToString();
            }
            if (reqObj.common.customerId == null || reqObj.common.customerId.Equals(""))
            {
                respData.code = "2206";
                respData.description = "customerId不能为空";
                return respData.ToJSON().ToString();
            }

            if (string.IsNullOrEmpty(reqObj.special.payChannelID))
            {
                respData.code = "2206";
                respData.description = "PayChannelID不能为空";
                return respData.ToJSON().ToString();
            }
            if (string.IsNullOrEmpty(reqObj.special.orderID))
            {
                respData.code = "2206";
                respData.description = "orderID不能为空";
                return respData.ToJSON().ToString();
            }

            if (!string.IsNullOrEmpty(reqObj.common.customerId))
            {
                customerId = reqObj.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            decimal appOrderAmount = new OnlineShoppingItemBLL(loggingSessionInfo).GetOrderAmmount(reqObj.special.orderID);

            string pUrlPath = ConfigurationManager.AppSettings["paymentcenterUrl"];
            //Json参数准备
            string jsonString = string.Format("action=CreateOrder&request={{\"AppID\":{0},\"ClientID\":\"{1}\",\"UserID\":\"{2}\",\"Token\":null,\"Parameters\":{{\"PayChannelID\":{3},\"AppOrderID\":\"{4}\",\"AppOrderTime\":\"{5}\",\"AppOrderAmount\":{6},\"AppOrderDesc\":\"{7}\",\"Currency\":{8},\"MobileNO\":\"{9}\",\"ReturnUrl\":\"\",\"DynamicID\":null,\"DynamicIDType\":null}}}}",
                1,
                reqObj.common.customerId,
                reqObj.common.userId,
                reqObj.special.payChannelID,
                reqObj.special.orderID,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"),
                //ToInt(appOrderAmount * 100),
                1,//测试用，支付一分钱
                string.IsNullOrEmpty(reqObj.special.orderDesc) ? "jitmarketing" : reqObj.special.orderDesc,
                string.IsNullOrEmpty(reqObj.special.currency) ? "1" : reqObj.special.currency,
                reqObj.special.mobileNO
                );
            //向交易中心提交订单
            string httpResponse = SendHttpRequest(pUrlPath, jsonString);
            //反序列化
            var payres = httpResponse.DeserializeJSONTo<orderPayCenterContentData>();
            if (payres.ResultCode == 0)
            {
                respData.code = "200";
                respData.description = "操作成功";
                respData.content = new orderPayRespContentData();
                respData.content.OrderID = payres.Datas.OrderID;
                respData.content.PayUrl = payres.Datas.PayUrl;
                //new OnlineShoppingItemBLL(loggingSessionInfo).SetOrderPayCenterCode(reqObj.special.orderID, payres.Datas.OrderID);
            }
            else
            {
                respData.code = payres.ResultCode.ToString();
                respData.description = payres.Message;
                //respData.content.PayUrl = payres.Datas.PayUrl;
            }
            return respData.ToJSON();
        }

        public class ALDChangeOrderStatus
        {
            public string SourceOrdersID { get; set; }
            public int? Status { get; set; }
            public Guid? MemberID { get; set; }
            public bool IsPaid { get; set; }
        }

        public class ALDChangeOrderStatusRequest
        {
            public int? Locale { get; set; }
            public Guid? UserID { get; set; }
            public int? BusinessZoneID { get; set; }
            public string Token { get; set; }
            public bool IsPaid { get; set; }
            public ALDChangeOrderStatus Parameters { get; set; }
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
            //myRequest.Accept = "application/plain";
            myRequest.ContentType = "application/x-www-form-urlencoded";
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

        #region 设置参数各个对象集合
        public class orderPayRespData : Default.LowerRespData
        {
            public orderPayRespContentData content { get; set; }
        }
        public class orderPayRespContentData
        {
            public int ResultCode { get; set; }
            public string Message { get; set; }

            public string OrderID { get; set; }
            public string PayUrl { get; set; }
            public string QrCodeUrl { get; set; }
            public string WeiXinPreOrderContent { get; set; }//微信支付时的预订单JSON内容
        }

        public class orderPayCenterContentData
        {
            public int ResultCode { get; set; }
            public string Message { get; set; }
            public orderPayCenterContentDetailData Datas { get; set; }
            //public string OrderID { get; set; }
            //public string PayUrl { get; set; }
            //public string QrCodeUrl { get; set; }
        }
        public class orderPayCenterContentDetailData
        {
            public string OrderID { get; set; }
            public string PayUrl { get; set; }
        }

        /// <summary>
        /// 传输的参数对象
        /// </summary>
        public class orderPayReqData : ReqData
        {
            public orderPayReqSpecialData special;
        }
        /// <summary>
        /// 特殊参数对象
        /// </summary>
        public class orderPayReqSpecialData
        {
            public string payChannelID { get; set; }
            public string orderID { get; set; }

            public string orderDesc { get; set; }
            public string currency { get; set; }
            public string mobileNO { get; set; }
            public string dynamicID { get; set; }
            public string dynamicIDType { get; set; }
            public string returnUrl { get; set; } //支付成功跳转URL
            public bool isWeiXinPay { get; set; }   //是否微信支付
        }
        #endregion
        #endregion

        #region IsOrderPayed
        public class isOrderPayed4ALDReqSpecialData
        {
            public string orderID { get; set; }
        }
        public class isOrderPayed4ALDReqData : ReqData
        {
            public isOrderPayed4ALDReqSpecialData special;
        }
        public class PayCenterIsOrderPayedQueryRequest
        {
            public int? AppID { get; set; }
            public string ClientID { get; set; }
            public string UserID { get; set; }
            public string Token { get; set; }
            public PayCenterIsOrderPayedQuery Parameters { get; set; }
            public string tableNo { get; set; }
        }
        public class PayCenterIsOrderPayedQuery
        {
            public string AppOrderID { get; set; }
        }
        public class PayCenterResponse
        {
            public int? ResultCode { get; set; }
            public string Message { get; set; }
            public bool? Datas { get; set; }

            public bool IsSuccess()
            {
                if (this.ResultCode.HasValue && this.ResultCode.Value >= 0 && this.ResultCode.Value <= 99)
                    return true;
                else
                    return false;
            }
        }
        public class isOrderPayedRespContentData
        {
            public bool IsPayed { get; set; }
        }
        public class isOrderPayedRespData : Default.LowerRespData
        {
            public isOrderPayedRespContentData content { get; set; }
        }

        public string IsOrderPayed4ALD()
        {

            var respData = new isOrderPayedRespData();
            string reqContent = HttpContext.Current.Request["ReqContent"];


            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("OrderPay: {0}", reqContent)
            });

            //reqContent = "{\"common\":{\"baiduPushUserId\":null,\"plat\":\"android\",\"customerId\":\"be39de140f33488588a2eca81d1e4744\",\"baiduPushChannelId\":null,\"sessionId\":null,\"baiduPushAppId\":null,\"deviceToken\":null,\"userId\":\"0f33f9c56d9b4cda9c8ee9a6dd9d0f56\",\"locale\":1,\"channel\":null,\"osInfo\":null,\"version\":null,\"openId\":\"0f33f9c56d9b4cda9c8ee9a6dd9d0f56\"},\"special\":{\"orderID\":\"0fed034da96b4b6c998ddcf32dcb8a6a\"}}";

            var reqObj = reqContent.DeserializeJSONTo<isOrderPayed4ALDReqData>();
            reqObj = reqObj == null ? new isOrderPayed4ALDReqData() : reqObj;
            if (reqObj.special == null)
            {
                reqObj.special = new isOrderPayed4ALDReqSpecialData();
            }
            if (reqObj.special == null)
            {
                respData.code = "102";
                respData.description = "没有特殊参数";
                return respData.ToJSON().ToString();
            }
            if (reqObj.common.customerId == null || reqObj.common.customerId.Equals(""))
            {
                respData.code = "2206";
                respData.description = "customerId不能为空";
                return respData.ToJSON().ToString();
            }

            if (string.IsNullOrEmpty(reqObj.special.orderID))
            {
                respData.code = "2206";
                respData.description = "orderID不能为空";
                return respData.ToJSON().ToString();
            }
            if (!string.IsNullOrEmpty(reqObj.common.customerId))
            {
                customerId = reqObj.common.customerId;
            }
            var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");
            decimal appOrderAmount = new OnlineShoppingItemBLL(loggingSessionInfo).GetOrderAmmount(reqObj.special.orderID);

            //支付中心接口
            PayCenterIsOrderPayedQueryRequest pyRequest = new PayCenterIsOrderPayedQueryRequest();
            pyRequest.AppID = 1;
            pyRequest.ClientID = reqObj.common.customerId;
            pyRequest.UserID = reqObj.common.userId;
            pyRequest.Parameters = new PayCenterIsOrderPayedQuery();
            pyRequest.Parameters.AppOrderID = reqObj.special.orderID;

            string pyUrl = ConfigurationManager.AppSettings["paymentcenterUrl"];
            string pyPostContent = string.Format("action=IsOrderPaid&request={0}", pyRequest.ToJSON());
            var strPyRsp = HttpWebClient.DoHttpRequest(pyUrl, pyPostContent);
            var pyRsp = strPyRsp.DeserializeJSONTo<PayCenterResponse>();
            if (pyRsp != null && pyRsp.IsSuccess())
            {
                if (pyRsp.Datas.HasValue && pyRsp.Datas.Value == true)
                {
                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("订单支付成功[OrderID={0}].", reqObj.special.orderID) });
                    //支付中心支付成功
                    respData.code = "200";
                    respData.description = "操作成功";
                    respData.content = new isOrderPayedRespContentData();
                    respData.content.IsPayed = true;
                    //
                    var inoutService = new InoutService(loggingSessionInfo);
                    var isSuccess = inoutService.UpdateOrderDeliveryStatus(reqObj.special.orderID, "2", null, Utils.GetNow());
                    //更新阿拉丁的订单状态
                    if (isSuccess)
                    {
                        Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新O2OMarketing订单状态成功[OrderID={0}].", reqObj.special.orderID) });
                        //更新阿拉丁的订单状态
                        ALDChangeOrderStatus aldChangeOrder = new ALDChangeOrderStatus();
                        aldChangeOrder.MemberID = new Guid(reqObj.common.userId);
                        aldChangeOrder.SourceOrdersID = reqObj.special.orderID;
                        aldChangeOrder.Status = 2;
                        ALDChangeOrderStatusRequest aldRequest = new ALDChangeOrderStatusRequest();
                        aldRequest.BusinessZoneID = 1;//写死
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
                        aldRequest.Parameters = aldChangeOrder;
                        var url = ConfigurationManager.AppSettings["ALDGatewayURL"];
                        var postContent = string.Format("Action=ChangeOrderStatus&ReqContent={0}", aldRequest.ToJSON());
                        var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                        var aldRsp = strAldRsp.DeserializeJSONTo<ALDResponse>();
                        if (!aldRsp.IsSuccess())
                        {
                            respData.code = "117";
                            respData.description = "更新阿拉丁订单状态失败";
                            Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新阿拉丁订单状态失败[Request ={0}][Response={1}]", aldRequest.ToJSON(), strAldRsp) });
                        }
                    }
                    else
                    {
                        respData.code = "115";
                        respData.description = "更新O2OMarketing订单状态失败";
                        Loggers.Debug(new DebugLogInfo() { Message = string.Format("更新O2OMarketing订单状态失败[OrderID={0}].", reqObj.special.orderID) });
                    }
                }
                else
                {
                    respData.code = "200";
                    respData.description = "操作成功";
                    respData.content = new isOrderPayedRespContentData();
                    respData.content.IsPayed = false;
                }
            }
            else
            {
                respData.code = "116";
                respData.description = "调用支付中心失败.";
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("调用支付中心失败[Request ={0}][Response={1}]", pyRequest.ToJSON(), strPyRsp) });
            }
            //
            return respData.ToJSON();
        }
        #endregion

        #region SetOrderPayment
        public string SetOrderPayment()
        {
            string content = string.Empty;
            var respData = new setOrderPaymentRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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

                //从交易中心查询订单状态
                string payCenterCode = new OnlineShoppingItemBLL(loggingSessionInfo).GetOrderPayCenterCode(reqObj.special.orderId);
                string pUrlPath = ConfigurationManager.AppSettings["paymentcenterUrl"];
                string jsonString = string.Format("action=QueryOrder&request={{\"AppID\":{0},\"ClientID\":\"{1}\",\"UserID\":\"{2}\",\"Token\":null,\"Parameters\":{{\"OrderID\":{3}}}}}",
                1,
                reqObj.common.customerId,
                reqObj.common.userId,
                payCenterCode
                );

                string httpResponse = SendHttpRequest(pUrlPath, jsonString);
                var payres = httpResponse.DeserializeJSONTo<orderPayCenterSearchContentData>();
                if (payres.ResultCode == 0 && payres.Datas.Status == 2)
                {
                    var bll = new TInOutStatusNodeBLL(loggingSessionInfo);
                    string msg;
                    if (!bll.SetOrderPayment(reqObj.special.orderId, out msg))
                    {
                        respData.code = "112";
                        respData.description = "计算积分失败";
                    }
                    else
                    {
                        //记录日志  qianzhi 2014-03-17
                        var inoutStatus = new TInoutStatusBLL(loggingSessionInfo);
                        TInoutStatusEntity info = new TInoutStatusEntity();
                        info.InoutStatusID = Guid.Parse(Utils.NewGuid());
                        info.OrderID = reqObj.special.orderId;
                        info.CustomerID = reqObj.common.customerId;
                        info.OrderStatus = 10000; //支付方式
                        info.Remark = "支付成功";
                        info.StatusRemark = "订单支付成功[操作人:" + loggingSessionInfo.CurrentUser.User_Name + "]";
                        inoutStatus.Create(info);
                    }
                    respData.code = "0";
                    respData.description = "支付成功";

                }
                else
                {
                    respData.code = "111";
                    respData.description = "支付失败";
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
        public class orderPayCenterSearchContentData
        {
            public int ResultCode { get; set; }
            public string Message { get; set; }
            public orderPayCenterSearchContentDetailData Datas { get; set; }
        }
        public class orderPayCenterSearchContentDetailData
        {
            public int Status { get; set; }//0-初始,1-已提交,2-已支付
        }


        /// <summary>
        /// 返回对象
        /// </summary>
        public class setOrderPaymentRespData : Default.LowerRespData
        {

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
        }
        #endregion
        #endregion

        #region GetOrderPayStatus

        #endregion

        #region GetOrderIntegral
        /// <summary>
        /// 我的兑换记录
        /// </summary>
        /// <returns></returns>
        public string GetOrderIntegral()
        {
            string content = string.Empty;
            var respData = new getOrderIntegralRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("GetOrderIntegral: {0}", reqContent)
                });

                #region //解析请求字符串

                //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getOrderIntegralReqData>();
                reqObj = reqObj == null ? new getOrderIntegralReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getOrderIntegralReqSpecialData();
                }
                #endregion
                #region //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getOrderIntegralRespContentData();
                respData.content.itemList = new List<getOrderIntegralRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetOrderIntegral(userId);

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.itemList = DataTableToObject.ConvertToList<getOrderIntegralRespContentDataItem>(dsItems.Tables[0]);

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
        public class getOrderIntegralRespData : Default.LowerRespData
        {
            public getOrderIntegralRespContentData content { get; set; }
        }
        public class getOrderIntegralRespContentData
        {
            public IList<getOrderIntegralRespContentDataItem> itemList { get; set; }     //商品集合
        }
        public class getOrderIntegralRespContentDataItem
        {
            public string itemId { get; set; }       //商品标识
            public string itemName { get; set; }     //商品名称（譬如：浪漫主题房）
            public string OrderIntegralID { get; set; }     //主键GUID
            public int Num { get; set; }   //兑换数量
            public decimal Integral { get; set; } //商品积分
            public decimal IntegralAmmount { get; set; }  //总积分
            public string VIPID { get; set; }      //会员ID
            public string LinkMan { get; set; }    //收货人
            public string LinkTel { get; set; }    //收货人电话
            public string CityID { get; set; }    //省市信息
            public string Address { get; set; }//收货地址
            public string CreateTime { get; set; }//创建时间
            public IList<StoreItemDailyStatusEntity> storeItemDailyStatus;
        }
        public class getOrderIntegralReqData : ReqData
        {
            public getOrderIntegralReqSpecialData special;
        }
        public class getOrderIntegralReqSpecialData
        {

        }
        #endregion
        #endregion

        #region GetProvince
        /// <summary>
        /// 获取省份信息
        /// </summary>
        /// <returns></returns>
        public string GetProvince()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getProvinceRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getProvinceReqData>();
                reqObj = reqObj == null ? new getProvinceReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getProvinceReqSpecialData();
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getProvinceRespContentData();
                respData.content.provinceList = new List<getProvinceRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetProvince();

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.provinceList = DataTableToObject.ConvertToList<getProvinceRespContentDataItem>(dsItems.Tables[0]);

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
        public class getProvinceRespData : Default.LowerRespData
        {
            public getProvinceRespContentData content { get; set; }
        }
        public class getProvinceRespContentData
        {
            public IList<getProvinceRespContentDataItem> provinceList { get; set; }
            public IList<getProvinceRespContentDataItem> cityList { get; set; }
        }
        public class getProvinceRespContentDataItem
        {
            public string CityID { get; set; }          //城市ID
            public string Province { get; set; }        //省份名称
            public string CityName { get; set; }        //城市名
            public string CityCode { get; set; }        //城市Code
            public string School { get; set; }

        }
        public class getProvinceReqData : ReqData
        {
            public getProvinceReqSpecialData special;
        }
        public class getProvinceReqSpecialData
        {
            public string Province { get; set; }    //省份 城市 名称
            public string CityName { get; set; }//城市名称
        }
        #endregion
        #endregion
        #region GetCityByProvince
        /// <summary>
        /// 根据省份名称获取城市名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public string GetCityByProvince()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getProvinceRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getProvinceReqData>();
                reqObj = reqObj == null ? new getProvinceReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getProvinceReqSpecialData();
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string Province = reqObj.special.Province;

                //初始化返回对象
                respData.content = new getProvinceRespContentData();
                respData.content.cityList = new List<getProvinceRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetCityByProvince(Province);

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.cityList = DataTableToObject.ConvertToList<getProvinceRespContentDataItem>(dsItems.Tables[0]);
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
        #endregion
        #region GetProvinceCityName
        /// <summary>
        /// 花间堂_获取门店区域属性信息
        /// </summary>
        /// <returns></returns>
        public string GetStoreArea()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getProvinceRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getProvinceReqData>();
                reqObj = reqObj == null ? new getProvinceReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getProvinceReqSpecialData();
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getProvinceRespContentData();
                respData.content.cityList = new List<getProvinceRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetStoreArea();

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.cityList = DataTableToObject.ConvertToList<getProvinceRespContentDataItem>(dsItems.Tables[0]);
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
        #endregion

        #region GetStoreListByCityName
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public string GetStoreListByCityName()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getStoreListRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreListReqData>();
                reqObj = reqObj == null ? new getStoreListReqData() : reqObj;//容错措施
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreListReqSpecialData();//特定参数
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getStoreListRespContentData();
                respData.content.storeList = new List<getStoreListRespContentDataItem>();//门店列表
                respData.content.hotelList = new List<getHotelStatusRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                #region 组装参数
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(customerId))
                {
                    pParams.Add("pCustomerID", customerId);
                }
                if (!string.IsNullOrEmpty(reqObj.special.CityName))
                {
                    pParams.Add("pCityName", reqObj.special.CityName);
                }
                if (!string.IsNullOrEmpty(reqObj.special.Longitude))
                {
                    pParams.Add("pLng", reqObj.special.Longitude);
                }
                if (!string.IsNullOrEmpty(reqObj.special.Latitude))
                {
                    pParams.Add("pLat", reqObj.special.Latitude);
                }
                if (!string.IsNullOrEmpty(reqObj.special.BeginDate))
                {
                    pParams.Add("pBeginDate", reqObj.special.BeginDate);
                }
                if (!string.IsNullOrEmpty(reqObj.special.EndDate))
                {
                    pParams.Add("pEndDate", reqObj.special.EndDate);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    pParams.Add("pVipID", userId);
                }

                //预约开始结束时间
                DateTime pBeginDate = DateTime.Parse(pParams["pBeginDate"]);
                DateTime pEndDate = DateTime.Parse(pParams["pEndDate"]);
                //循环日期
                string pDate = "";
                for (; pBeginDate < pEndDate; pBeginDate += new TimeSpan(24, 0, 0))
                {
                    pDate += pBeginDate.ToString("yyyy-MM-dd") + ",";
                }
                pDate = pDate.TrimEnd(',');
                pParams.Add("pDate", pDate);
                #endregion

                var dsItems = itemService.GetStoreListByCityName(pParams);
                if (dsItems != null && dsItems.Count > 0)
                {
                    List<getStoreListRespContentDataItem> list = new List<getStoreListRespContentDataItem>();
                    foreach (var item in dsItems)
                    {
                        getStoreListRespContentDataItem info = new getStoreListRespContentDataItem();
                        info.StoreID = ToStr(item.StoreID);
                        info.StoreName = ToStr(item.StoreName);
                        info.ImageUrl = JIT.CPOS.Web.ApplicationInterface.Module.UnitAndItem.Item.Item.ImagePathUtil.GetImagePathStr(ToStr(item.imageURL), "240");
                        info.Address = ToStr(item.Address);
                        info.Tel = ToStr(item.Tel);
                        info.Longitude = ToStr(item.Longitude);
                        info.Latitude = ToStr(item.Latitude);
                        info.Distance = ToStr(item.Distance);
                        info.MinPrice = ToStr(item.MinPrice);
                        info.IsFull = ToInt(item.IsFull);
                        list.Add(info);
                    }
                    respData.content.storeList = list;
                }
                else
                {
                    respData.code = "111";
                    respData.description = "没有获取到数据.";
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
        public class getStoreListRespData : Default.LowerRespData
        {
            public getStoreListRespContentData content { get; set; }
        }
        public class getStoreListRespContentData
        {
            public IList<getStoreListRespContentDataItem> storeList { get; set; }
            public IList<getHotelStatusRespContentDataItem> hotelList { get; set; }
        }
        public class getStoreListRespContentDataItem
        {
            public string Tel { get; set; }//电话
            public string Address { get; set; }//地址
            public string StoreID { get; set; }//酒店ID
            public string ImageUrl { get; set; }//酒店图片地址
            public string Latitude { get; set; }//纬度
            public string Distance { get; set; }//距离
            public string MinPrice { get; set; }//起价
            public string Longitude { get; set; }//经度
            public string StoreName { get; set; }//酒店名称
            public int IsFull { get; set; }//是否满房
        }
        public class getHotelStatusRespContentDataItem
        {
            public string StoreID { get; set; }//酒店ID
            public string StatusDate { get; set; }//房态日期
        }

        public class getStoreListReqData : ReqData
        {
            public getStoreListReqSpecialData special;
        }
        public class getStoreListReqSpecialData
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public string StoreID { get; set; }//酒店ID
            public string CityName { get; set; }//省份 城市 名称
            public string Longitude { get; set; }//经度
            public string Latitude { get; set; }//纬度
            public string BeginDate { get; set; }//开始时间
            public string EndDate { get; set; }//结束时间
        }
        #endregion
        #endregion
        #region GetStoreDetailByStoreID
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetStoreDetailByStoreID()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getStoreDetailRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreDetailReqData>();
                reqObj = reqObj == null ? new getStoreDetailReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreDetailReqSpecialData();
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
                respData.content = new getStoreDetailRespContentData();

                //初始化返回对象
                respData.content = new getStoreDetailRespContentData();
                respData.content.StoreDetail = new List<StoreDetailViewEntity>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                #endregion

                #region //组装参数
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(customerId))
                {
                    pParams.Add("pCustomerID", customerId);
                }
                if (!string.IsNullOrEmpty(reqObj.special.BeginDate))
                {
                    pParams.Add("pBeginDate", reqObj.special.BeginDate);
                }
                if (!string.IsNullOrEmpty(reqObj.special.EndDate))
                {
                    pParams.Add("pEndDate", reqObj.special.EndDate);
                }
                if (!string.IsNullOrEmpty(reqObj.special.StoreID))
                {
                    pParams.Add("pStoreID", reqObj.special.StoreID);
                }
                if (!string.IsNullOrEmpty(userId))
                {
                    pParams.Add("pVipID", userId);
                }
                if (string.IsNullOrEmpty(userId))
                {
                    pParams.Add("pVipID", "");
                }
                if (!string.IsNullOrEmpty(reqObj.special.Page.ToString()))
                {
                    pParams.Add("pPage", reqObj.special.Page.ToString());
                }
                if (!string.IsNullOrEmpty(reqObj.special.PageSize.ToString()))
                {
                    pParams.Add("pPageSize", reqObj.special.PageSize.ToString());
                }
                //预约开始结束时间
                DateTime pBeginDate = DateTime.Parse(pParams["pBeginDate"]);
                DateTime pEndDate = DateTime.Parse(pParams["pEndDate"]);
                //循环日期
                string pDate = "";
                for (; pBeginDate < pEndDate; pBeginDate += new TimeSpan(24, 0, 0))
                {
                    pDate += pBeginDate.ToString("yyyy-MM-dd") + ",";
                }
                pDate = pDate.TrimEnd(',');
                pParams.Add("pDate", pDate);
                #endregion

                var dsItems = itemService.GetStoreDetailByStoreID(pParams);
                if (dsItems != null && dsItems.Count > 0)
                {
                    respData.content.StoreDetail = dsItems;
                    respData.content.imageList = new List<getItemDetailRespContentDataImage>();
                    //商品图片信息
                    var dsImages = itemService.GetItemImageList(pParams["pStoreID"]);
                    if (dsImages != null && dsImages.Tables.Count > 0 && dsImages.Tables[0].Rows.Count > 0)
                    {
                        respData.content.imageList = DataTableToObject.ConvertToList<getItemDetailRespContentDataImage>(dsImages.Tables[0]);
                    }
                }
                else
                {
                    respData.code = "111";
                    respData.description = "没有获取到数据.";
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
        public class getStoreDetailRespData : Default.LowerRespData
        {
            public getStoreDetailRespContentData content { get; set; }
        }
        public class getStoreDetailRespContentData
        {
            public IList<StoreDetailViewEntity> StoreDetail { get; set; }
            public IList<getItemDetailRespContentDataImage> imageList { get; set; }     //图片集合
        }
        public class getStoreDetailRespContentDataItem
        {
            public string ItemID { get; set; }
            public string Address { get; set; }
            public string StoreID { get; set; }
            public string ImageUrl { get; set; }
            public string Latitude { get; set; }
            public string Distance { get; set; }
            public string MinPrice { get; set; }
            public string Longitude { get; set; }
            public string StoreName { get; set; }
            public int IsFull { get; set; }
        }

        public class getStoreDetailReqData : ReqData
        {
            public getStoreDetailReqSpecialData special;
        }
        public class getStoreDetailReqSpecialData
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public string StoreID { get; set; }//酒店ID
            public string CityName { get; set; }//省份 城市 名称
            public string Longitude { get; set; }//经度
            public string Latitude { get; set; }//纬度
            public string BeginDate { get; set; }//开始时间
            public string EndDate { get; set; }//结束时间
        }
        #endregion
        #endregion
        #region GetStoreLIstByArea
        /// <summary>
        /// 根据区域获取门店列表
        /// </summary>
        /// <returns></returns>
        public string GetStoreListByArea()
        {
            string content = string.Empty;
            var respData = new getStoreListByCityRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region //解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getStoreListByCityReqData>();
                reqObj = reqObj == null ? new getStoreListByCityReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getStoreListByCityReqSpecialData();
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

                respData.content = new getStoreListByCityRespContentData();
                respData.content.storeList = new List<getStoreListByCityRespContentItemTypeData>();

                #region
                string strError = string.Empty;
                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                var storeInfo = itemService.GetStoreListByArea(reqObj.special.city);
                if (storeInfo != null && storeInfo.Tables.Count > 0)
                {
                    respData.content.storeList = DataTableToObject.ConvertToList<getStoreListByCityRespContentItemTypeData>(storeInfo.Tables[0]);
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
            public string storeId { get; set; }
            public string storeName { get; set; }
        }
        public class getStoreListByCityReqData : ReqData
        {
            public getStoreListByCityReqSpecialData special;
        }
        public class getStoreListByCityReqSpecialData
        {
            public string city { get; set; }
        }
        #endregion
        #endregion

        #region HS_SendCode
        /// <summary>
        /// 华硕校园_发送短信验证码
        /// </summary>
        /// <returns></returns>
        public string HS_SendCode()
        {
            string content = string.Empty;
            var respData = new Default.LowerRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];
                var reqObj = reqContent.DeserializeJSONTo<SendCodeReqData>();

                ReceiveService service = new ReceiveService();
                if (reqObj.special.mobile != "")
                {
                    //生成随机数 6位
                    Random rd = new Random();
                    string code = rd.Next(100000, 999999).ToString();
                    string message = ConfigurationManager.AppSettings["HS_ValidationMessage"].ToString();
                    message = string.Format(message, code);
                    //调用接口 发送验证码
                    var res = service.Recieve(reqObj.special.mobile, message, "华硕校园");

                    //将验证码保存到Session中
                    HttpContext.Current.Session["sendCode"] = code;

                    switch (res)
                    {
                        case "F":
                            respData.code = "101";
                            respData.description = "验证码发送失败，请稍候再试。";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    respData.code = "102";
                    respData.description = "手机号码不能为空！";
                }
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();

                Loggers.Exception(new ExceptionLogInfo()
                {
                    ErrorMessage = string.Format("HS_SendCode: {0}", ex.ToJSON())
                });
            }
            content = respData.ToJSON();
            return content;
        }

        public class SendCodeReqData : Default.ReqData
        {
            public SendCodeReqSpecialData special;
        }
        public class SendCodeReqSpecialData
        {
            public string mobile { get; set; }//手机号码
        }
        #endregion
        #region GetProvinceOfHS
        /// <summary>
        /// 华硕校园根据校园大使获取省份信息
        /// </summary>
        /// <returns></returns>
        public string GetProvinceOfHS()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getProvinceRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getProvinceReqData>();
                reqObj = reqObj == null ? new getProvinceReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getProvinceReqSpecialData();
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getProvinceRespContentData();
                respData.content.provinceList = new List<getProvinceRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetProvinceOfHS();

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.provinceList = DataTableToObject.ConvertToList<getProvinceRespContentDataItem>(dsItems.Tables[0]);

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
        #endregion
        #region GetCityByProvinceOfHS
        /// <summary>
        /// 华硕校园根据省份名称获取城市名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public string GetCityByProvinceOfHS()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getProvinceRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getProvinceReqData>();
                reqObj = reqObj == null ? new getProvinceReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getProvinceReqSpecialData();
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;
                string Province = reqObj.special.Province;

                //初始化返回对象
                respData.content = new getProvinceRespContentData();
                respData.content.cityList = new List<getProvinceRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.GetCityByProvinceOfHS(customerId, Province);

                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.cityList = DataTableToObject.ConvertToList<getProvinceRespContentDataItem>(dsItems.Tables[0]);
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
        #endregion
        #region GetSchoolListByCityNameOfHS
        /// <summary>
        /// 华硕校园获取校园名称
        /// </summary>
        /// <param name="pProvince"></param>
        /// <returns></returns>
        public string GetSchoolListByCityNameOfHS()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getProvinceRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getProvinceReqData>();
                reqObj = reqObj == null ? new getProvinceReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getProvinceReqSpecialData();
                }
                #endregion

                #region 判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                #endregion

                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getProvinceRespContentData();
                respData.content.cityList = new List<getProvinceRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                #region //组装参数
                Dictionary<string, string> pParams = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(customerId))
                {
                    pParams.Add("pCustomerID", customerId);
                }
                if (!string.IsNullOrEmpty(reqObj.special.CityName))
                {
                    pParams.Add("pCityName", reqObj.special.CityName);
                }
                #endregion

                var dsItems = itemService.GetSchoolListByCityNameOfHS(pParams);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.cityList = DataTableToObject.ConvertToList<getProvinceRespContentDataItem>(dsItems.Tables[0]);
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
        #endregion
        #region CreateVip
        /// <summary>
        /// 华硕_完善资料
        /// </summary>
        /// <returns></returns>
        public string CreateVip()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getVipRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                #region 解析请求字符串
                var reqObj = reqContent.DeserializeJSONTo<getVipReqData>();
                reqObj = reqObj == null ? new getVipReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getVipReqSpecialData();
                }
                #endregion

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                //初始化返回对象
                respData.content = new getVipRespContentData();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                #region 验证码
                //if (HttpContext.Current.Session["sendCode"] != null)
                //{
                //    if (HttpContext.Current.Session["sendCode"].ToString() != null)
                //    {
                //        if (reqObj.special.code != HttpContext.Current.Session["sendCode"].ToString())
                //        {
                //            respData.code = "201";
                //            respData.description = "验证码不正确.";
                //            content = respData.ToJSON();
                //            return content;
                //        }
                //    }
                //}
                //else
                //{
                //    respData.code = "202";
                //    respData.description = "请先发送验证码后再填写验证码";
                //    content = respData.ToJSON();
                //    return content;
                //}
                #endregion

                #region 放置用户手机加载不出数据 提交错误数据信息验证
                if (string.IsNullOrEmpty(reqObj.special.city))
                {
                    respData.code = "2001";
                    respData.description = "城市不可为空.";
                    content = respData.ToJSON();
                    return content;
                }
                else if (reqObj.special.city == "--请选择--")
                {
                    respData.code = "2001";
                    respData.description = "城市不可为空.";
                    content = respData.ToJSON();
                    return content;
                }
                if (string.IsNullOrEmpty(reqObj.special.school))
                {
                    respData.code = "2001";
                    respData.description = "学校不可为空.";
                    content = respData.ToJSON();
                    return content;
                }
                else if (reqObj.special.school == "--请选择--")
                {
                    respData.code = "2001";
                    respData.description = "学校不可为空.";
                    content = respData.ToJSON();
                    return content;
                }

                #endregion

                VipBLL vipBLL = new VipBLL(loggingSessionInfo);
                //是否有此人
                var vip = vipBLL.Query(new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "Phone", Value = reqObj.special.phone },
                    new EqualsCondition() { FieldName = "Status", Value = "1" },
                    new EqualsCondition() { FieldName = "IsDelete", Value = "0" },
                    new EqualsCondition() { FieldName = "ClientID", Value = customerId }
                }, null).FirstOrDefault();

                VipEntity entity = new VipEntity();
                VIPRoleMappingEntity vipRoleEntity = new VIPRoleMappingEntity();
                WEventUserMappingEntity eventEntity = new WEventUserMappingEntity();
                if (vip == null)
                {
                    //插入VIP表
                    entity.VIPID = Guid.NewGuid().ToString();
                    entity.VipName = reqObj.special.name;
                    entity.Phone = reqObj.special.phone;
                    entity.Col11 = reqObj.special.city;
                    entity.Status = 1;
                    entity.Col12 = reqObj.special.email;
                    entity.Col13 = reqObj.special.inviteCode;
                    entity.Col14 = reqObj.special.check;
                    entity.Col15 = reqObj.special.school;
                    entity.ClientID = customerId;
                    vipBLL.Create(entity);

                    //插入VIPRole关联表
                    vipRoleEntity.VIPID = entity.VIPID;
                    vipRoleEntity.RoleID = itemService.GetRoleID(customerId);
                    vipRoleEntity.ClientID = customerId;
                    new VIPRoleMappingBLL(loggingSessionInfo).Create(vipRoleEntity);

                    //插入活动表
                    eventEntity.Mapping = Guid.NewGuid().ToString();
                    eventEntity.UserName = reqObj.special.name;
                    eventEntity.Mobile = reqObj.special.phone;
                    eventEntity.Email = reqObj.special.city;
                    eventEntity.Class = reqObj.special.school;
                    eventEntity.UserID = entity.VIPID;
                    eventEntity.EventID = ConfigurationManager.AppSettings["HS_RegisterEventID"];
                    new WEventUserMappingBLL(loggingSessionInfo).Create(eventEntity);
                }
                else
                {
                    entity = vipBLL.GetByID(vip.VIPID);
                    entity.VIPID = vip.VIPID;
                    entity.VipName = reqObj.special.name;
                    entity.Phone = reqObj.special.phone;
                    entity.Col11 = reqObj.special.city;
                    entity.Col12 = reqObj.special.email;
                    entity.Col13 = reqObj.special.inviteCode;
                    entity.Col14 = reqObj.special.check;
                    entity.Col15 = reqObj.special.school;
                    entity.LastUpdateTime = DateTime.Now;
                    vipBLL.Update(entity);
                }

                respData.code = "200";
                respData.description = "操作成功";
                respData.content.vipID = entity.VIPID;
            }
            catch (Exception ex)
            {
                respData.code = "103";
                respData.description = "数据库操作错误";
                respData.exception = ex.ToString();
                Loggers.Exception(new ExceptionLogInfo()
                {
                    ErrorMessage = string.Format("CreateVip: {0}", ex.ToJSON())
                });
            }
            content = respData.ToJSON();
            return content;
        }
        #region 设置参数各个对象集合
        public class getVipRespData : Default.LowerRespData
        {
            public getVipRespContentData content { get; set; }
        }
        public class getVipRespContentData
        {
            public string vipID { get; set; }
            public IList<getVipRespContentDataItem> vipList { get; set; }
        }
        public class getVipRespContentDataItem
        {
            public string vipId { get; set; }
            public string phone { get; set; }
            public string name { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string school { get; set; }
            public string code { get; set; }
            public string Mapping { get; set; }
        }
        public class getVipReqData : ReqData
        {
            public getVipReqSpecialData special;
        }
        public class getVipReqSpecialData
        {
            public string vipID { get; set; }//用户ID
            public string phone { get; set; }//电话
            public string name { get; set; }//姓名
            public string city { get; set; }//城市
            public string school { get; set; }//学校
            public string code { get; set; }//验证码
            public string inviteCode { get; set; }//邀请码
            public string check { get; set; }
            public string email { get; set; }
        }
        #endregion
        #endregion
        #region joinGroupon
        /// <summary>
        /// 华硕_完善资料
        /// </summary>
        /// <returns></returns>
        public string joinGroupon()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getVipRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                var reqObj = reqContent.DeserializeJSONTo<getVipReqData>();
                reqObj = reqObj == null ? new getVipReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getVipReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //查询参数
                string userId = reqObj.common.userId;

                #region 放置用户手机加载不出数据 提交错误数据信息验证
                if (string.IsNullOrEmpty(reqObj.special.city))
                {
                    respData.code = "2001";
                    respData.description = "城市不可为空.";
                    content = respData.ToJSON();
                    return content;
                }
                else if (reqObj.special.city == "--请选择--")
                {
                    respData.code = "2001";
                    respData.description = "城市不可为空.";
                    content = respData.ToJSON();
                    return content;
                }
                if (string.IsNullOrEmpty(reqObj.special.school))
                {
                    respData.code = "2001";
                    respData.description = "学校不可为空.";
                    content = respData.ToJSON();
                    return content;
                }
                else if (reqObj.special.school == "--请选择--")
                {
                    respData.code = "2001";
                    respData.description = "学校不可为空.";
                    content = respData.ToJSON();
                    return content;
                }

                #endregion

                //初始化返回对象
                respData.content = new getVipRespContentData();

                WEventUserMappingBLL eventBLL = new WEventUserMappingBLL(loggingSessionInfo);
                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                var vip = eventBLL.Query(new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "Mobile", Value = reqObj.special.phone },
                    new EqualsCondition() { FieldName = "IsDelete", Value = "0" },
                    new EqualsCondition() { FieldName = "EventID", Value = ConfigurationManager.AppSettings["HS_EventID"].ToString() }
                }, null).FirstOrDefault();

                WEventUserMappingEntity pEntity = new WEventUserMappingEntity();
                if (vip == null)
                {
                    pEntity.Mapping = Guid.NewGuid().ToString();
                    pEntity.UserID = userId;
                    pEntity.Mobile = reqObj.special.phone;
                    pEntity.UserName = reqObj.special.name;
                    pEntity.Email = reqObj.special.city;
                    pEntity.Class = reqObj.special.school;
                    pEntity.EventID = ConfigurationManager.AppSettings["HS_EventID"];//团购活动固定活动ID
                    pEntity.IsDelete = 0;
                    new WEventUserMappingBLL(loggingSessionInfo).Create(pEntity);
                }
                else
                {
                    pEntity.UserID = userId;
                    pEntity.Mobile = reqObj.special.phone;
                    pEntity.UserName = reqObj.special.name;
                    pEntity.Email = reqObj.special.city;
                    pEntity.Class = reqObj.special.school;
                    pEntity.EventID = ConfigurationManager.AppSettings["HS_EventID"];//团购活动固定活动ID
                    pEntity.LastUpdateTime = DateTime.Now;
                    itemService.UpdateWEventByPhone(pEntity);
                }

                respData.code = "200";
                respData.description = "操作成功";

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
        #endregion
        #region getWEventByUserID
        public string getWEventByUserID()
        {
            //定义输出内容
            string content = string.Empty;
            var respData = new getVipRespData();
            try
            {
                //获取页面传递的参数
                string reqContent = HttpContext.Current.Request["ReqContent"];

                var reqObj = reqContent.DeserializeJSONTo<getVipReqData>();
                reqObj = reqObj == null ? new getVipReqData() : reqObj;
                if (reqObj.special == null)
                {
                    reqObj.special = new getVipReqSpecialData();
                }

                //判断客户ID是否传递
                if (!string.IsNullOrEmpty(reqObj.common.customerId))
                {
                    customerId = reqObj.common.customerId;
                }
                var loggingSessionInfo = Default.GetBSLoggingSession(customerId, "1");

                //初始化返回对象
                respData.content = new getVipRespContentData();
                respData.content.vipList = new List<getVipRespContentDataItem>();

                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);

                var dsItems = itemService.getWEventByPhone(reqObj.special.vipID);
                if (dsItems != null && dsItems.Tables.Count > 0 && dsItems.Tables[0].Rows.Count > 0)
                {
                    respData.content.vipList = DataTableToObject.ConvertToList<getVipRespContentDataItem>(dsItems.Tables[0]);

                }
                respData.code = "200";
                respData.description = "操作成功";

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
        #endregion
        #region 16.获取VIP用户的详细信息
        public string HS_GetVipDetail()
        {
            string content = string.Empty;
            var respData = new getVIPDetailRespData();
            try
            {
                string reqContent = HttpContext.Current.Request["ReqContent"];

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
                OnlineShoppingItemBLL itemService = new OnlineShoppingItemBLL(loggingSessionInfo);
                var dsItem = itemService.HS_GetVipDetail(vipId);
                if (dsItem != null && dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                {
                    respData.content.VipName = ToStr(dsItem.Tables[0].Rows[0]["VipName"].ToString());
                    respData.content.Phone = ToStr(dsItem.Tables[0].Rows[0]["Phone"].ToString());
                    respData.content.School = ToStr(dsItem.Tables[0].Rows[0]["School"].ToString());
                    respData.content.Code = ToStr(dsItem.Tables[0].Rows[0]["Code"].ToString());
                    respData.content.Province = ToStr(dsItem.Tables[0].Rows[0]["Province"].ToString());
                    respData.content.City = ToStr(dsItem.Tables[0].Rows[0]["City"].ToString());
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
            public string VipName { get; set; }
            public string Phone { get; set; }
            public string School { get; set; }
            public string Code { get; set; }
            public string Province { get; set; }
            public string City { get; set; }
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
