using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using JIT.Utility.Log;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 商品服务
    /// </summary>
    public class ItemService:BaseService
    {
        JIT.CPOS.BS.DataAccess.ItemService itemService = null;
        #region 构造函数
        public ItemService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            itemService = new DataAccess.ItemService(loggingSessionInfo);
        }
        #endregion

        #region 查询商品
        /// <summary>
        /// 查询商品
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="item_code">商品号码</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="item_category_id">类型标识</param>
        /// <param name="status">状态</param>
        /// <param name="item_can_redeem">是否积分商品</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public ItemInfo SearchItemList(   string item_code
                                        , string item_name
                                        , string item_category_id
                                        , string status
                                        , string item_can_redeem
                                        , int maxRowCount
                                        , int startRowIndex)
        {
            Hashtable _ht = new Hashtable();
            if (item_code == null) item_code = "";
            if (item_name == null) item_name = "";
            if (item_category_id == null) item_category_id = "";
            if (status == null) status = "";
            if (item_can_redeem == null) item_can_redeem = "";
            //if (maxRowCount == null) maxRowCount = 0;
            //if (startRowIndex == null) startRowIndex = 0;

            _ht.Add("item_code", item_code);
            _ht.Add("item_name", item_name);
            _ht.Add("item_category_id", item_category_id);
            _ht.Add("status", status);
            _ht.Add("item_can_redeem", item_can_redeem);

            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);

            ItemInfo itemInfo = new ItemInfo();
            int iCount = itemService.SearchCount(_ht);

            IList<ItemInfo> ItemInfoList = new List<ItemInfo>();
            DataSet ds = new DataSet();
            ds = itemService.SearchList(_ht);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ItemInfoList = DataTableToObject.ConvertToList<ItemInfo>(ds.Tables[0]);
            }

            itemInfo.ICount = iCount;
            itemInfo.ItemInfoList = ItemInfoList;
            return itemInfo;

        }
        /// <summary>
        /// 获取所有的商品
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<ItemInfo> GetItemAllList()
        {
            IList<ItemInfo> itemInfoList = new List<ItemInfo>();
            
            DataSet ds = new DataSet();
            ds = itemService.GetItemAllList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                itemInfoList = DataTableToObject.ConvertToList<ItemInfo>(ds.Tables[0]);
            }

            return itemInfoList;
        }
        /// <summary>
        /// 获取单个商品信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public ItemInfo GetItemInfoById(LoggingSessionInfo loggingSessionInfo, string item_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("Item_Id", item_id);
            ItemInfo itemInfo = new ItemInfo();
            //获取基础信息
            DataSet ds = new DataSet();
            ds = itemService.GetItemById(item_id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                itemInfo = DataTableToObject.ConvertToObject<ItemInfo>(ds.Tables[0].Rows[0]);
            }
            //获取商品与属性关系信息
            itemInfo.ItemPropList = new ItemPropService(loggingSessionInfo).GetItemPropListByItemId(item_id);
            //获取商品与商品价格类型信息
            itemInfo.ItemPriceList = new ItemPriceService(loggingSessionInfo).GetItemPriceListByItemId(item_id);

            //获取商品与sku关系信息
            itemInfo.SkuList = new SkuService(loggingSessionInfo).GetSkuListByItemId(item_id);
            //遍历添加sku价格集合(jifeng.cao 20140221)
            if (itemInfo.SkuList != null && itemInfo.SkuList.Count > 0)
            {
                foreach (var skuInfo in itemInfo.SkuList)
                {
                    skuInfo.sku_price_list = new SkuPriceService(loggingSessionInfo).GetSkuPriceListBySkuId(skuInfo.sku_id);
                }
            }

            //获取商品与门店信息
            itemInfo.ItemUnitList = new ItemStoreMappingBLL(loggingSessionInfo).GetItemUnitListByItemId(item_id);
            //获取商品与分类关系
            itemInfo.ItemCategoryMappingList = new ItemCategoryMappingBLL(loggingSessionInfo).GetItemCategoryListByItemId(item_id);
            return itemInfo;
        }
        #endregion

        #region 处理商品状态
        /// <summary>
        /// 停用启用商品
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="Item_Id">商品标识</param>
        /// <param name="Status">状态（停用-1,启用1）</param>
        /// <returns></returns>
        public string SetItemStatus(LoggingSessionInfo loggingSessionInfo, string Item_Id, string Status)
        {
            string strResult = string.Empty;
            try
            {
                //设置要改变的用户信息
                ItemInfo itenInfo = new ItemInfo();
                itenInfo.Status = Status;
                itenInfo.Item_Id = Item_Id;
                itenInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                itenInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交
                itemService.SetItemStatus(itenInfo);
                strResult = "状态修改成功.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return strResult;
        }
        #endregion

        #region 商品保存
        /// <summary>
        /// 设置商品信息（修改,新建）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo">商品对象</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool SetItemInfo(ItemInfo itemInfo, out string strError)
        {
            try
            {
                if (itemInfo.Item_Id == null || itemInfo.Item_Id.Equals(""))
                {
                    itemInfo.Item_Id = NewGuid();
                }

                if (itemInfo != null)
                {
                    itemInfo.Status = "1";
                    itemInfo.Status_Desc = "正常";
                    if (itemInfo.Create_User_Id == null || itemInfo.Create_User_Id.Equals(""))
                    {
                        itemInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (itemInfo.Modify_User_Id == null || itemInfo.Modify_User_Id.Equals(""))
                    {
                        itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemInfo.Modify_Time = GetCurrentDateTime();
                    }

                    if (itemInfo.OperationType =="ADD")
                    {
                        //遍历删除sku相关价格(jifeng.cao 20140224)
                        IList<SkuInfo> skuList = new SkuService(loggingSessionInfo).GetSkuListByItemId(itemInfo.Item_Id);
                        if (skuList != null)
                        {
                            foreach (var skuInfo in skuList)
                            {
                                if (!new SkuPriceService(loggingSessionInfo).DeleteSkuPriceInfo(skuInfo))
                                {
                                    strError = "删除sku相关价格失败";
                                    throw (new System.Exception(strError));
                                }
                            }
                        }
                    }                    

                    //处理富文本编辑内容中的图片
                    ImageHandler(itemInfo);

                    itemService.SetItemInfo(itemInfo, out strError);
                }
                strError = "保存成功!";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 操作单个sku 
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="strError"></param>
        /// <returns>skuID</returns>
        /// add by doanl 2014-10-11 18:03:28
        public string SetSkuInfo(ItemInfo itemInfo, out string strError)
        {
            try
            {
                               
                if (itemInfo.Create_User_Id == null || itemInfo.Create_User_Id.Equals(""))
                {
                    itemInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                    itemInfo.Create_Time = GetCurrentDateTime();
                }
                if (itemInfo.Modify_User_Id == null || itemInfo.Modify_User_Id.Equals(""))
                {
                    itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                    itemInfo.Modify_Time = GetCurrentDateTime();
                }
                return  itemService.SetSkuInfo(itemInfo, out strError);        
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 判断商品号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_code"></param>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public bool IsExistItemCode(LoggingSessionInfo loggingSessionInfo, string item_code, string item_id)
        {
            try
            {
                int n = itemService.IsExistItemCode(item_code,item_id);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        

        /// <summary>
        /// 设置商品主表信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemTableInfo(LoggingSessionInfo loggingSessionInfo, ItemInfo itemInfo)
        {
            try
            {
                if (itemInfo != null)
                {
                    itemInfo.Status = "1";
                    itemInfo.Status_Desc = "正常";
                    if (itemInfo.Create_User_Id == null || itemInfo.Create_User_Id.Equals(""))
                    {
                        itemInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemInfo.Create_Time = GetCurrentDateTime();
                    }
                    if (itemInfo.Modify_User_Id == null || itemInfo.Modify_User_Id.Equals(""))
                    {
                        itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                        itemInfo.Modify_Time = GetCurrentDateTime();
                    }
                    //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.InsertOrUpdate", itemInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        


        #endregion

        #region 根据商品号码或者名称模糊查询商品
        /// <summary>
        /// 模糊查询商品信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public IList<ItemInfo> GetItemListLikeItemName(string itemName)
        {
            try
            {
                IList<ItemInfo> itemInfoList = new List<ItemInfo>();
                DataSet ds = new DataSet();
                ds = itemService.GetItemListLikeItemName(itemName);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    itemInfoList = DataTableToObject.ConvertToList<ItemInfo>(ds.Tables[0]);
                }
                return itemInfoList;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 中间层
        #region 商品数据处理
        ///// <summary>
        ///// 获取未打包的商品数量
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <returns></returns>
        //public int GetItemNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        //{
        //    return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Item.SelectUnDownloadCount", "");
        //}
        ///// <summary>
        ///// 需要打包的商品信息
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="maxRowCount">当前页数量</param>
        ///// <param name="startRowIndex">开始数量</param>
        ///// <returns></returns>
        //public IList<ItemInfo> GetItemListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        //{
        //    Hashtable _ht = new Hashtable();
        //    _ht.Add("StartRow", startRowIndex);
        //    _ht.Add("EndRow", startRowIndex + maxRowCount);
        //    return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemInfo>("Item.SelectUnDownload", _ht);
        //}

        ///// <summary>
        ///// 设置打包批次号
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次号</param>
        ///// <param name="ItemInfoList">商品集合</param>
        ///// <param name="strError">错误信息返回</param>
        ///// <returns></returns>
        //public bool SetItemBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<ItemInfo> ItemInfoList,out string strError)
        //{
        //        ItemInfo itemInfo = new ItemInfo();
        //        itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        //        itemInfo.Modify_Time = GetCurrentDateTime();
        //        itemInfo.bat_id = bat_id;
        //        itemInfo.ItemInfoList = ItemInfoList;
        //        cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.UpdateUnDownloadBatId", itemInfo);
        //        strError = "Success";
        //        return true;
        //}
        ///// <summary>
        ///// 更新Item表打包标识方法
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次标识</param>
        ///// <param name="strError">错误信息返回</param>
        ///// <returns></returns>
        //public bool SetItemIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        //{
        //        ItemInfo itemInfo = new ItemInfo();
        //        itemInfo.bat_id = bat_id;
        //        itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        //        itemInfo.Modify_Time = GetCurrentDateTime();
        //        cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Item.UpdateUnDownloadIfFlag", itemInfo);
        //        strError = "Success";
        //        return true;
        //}
        #endregion
        #endregion

        #region 获取校友福利商品列表
        /// <summary>
        /// 获取校友福利商品列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemName"></param>
        /// <param name="itemTypeId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="isKeep"></param>
        /// <returns></returns>
        public DataSet GetWelfareItemList(string userId, string itemName, string itemTypeId, int page, int pageSize, bool isKeep, string isExchange, string storeId)
        {
            return itemService.GetWelfareItemList(userId, itemName, itemTypeId, page, pageSize, isKeep, isExchange, storeId);
        }

        public int GetWelfareItemListCount(string userId, string itemName, string itemTypeId, bool isKeep, string isExchange, string storeId)
        {
            return itemService.GetWelfareItemListCount(userId, itemName, itemTypeId, isKeep, isExchange, storeId);
        }
        #endregion

        #region 获取福利商品明细信息

        /// <summary>
        /// 获取福利商品明细信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataSet GetItemDetailByItemId(string itemId, string userId)
        {
            return itemService.GetItemDetailByItemId(itemId, userId);
        }

        /// <summary>
        /// 获取商品图片集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemImageList(string itemId)
        {
            return itemService.GetItemImageList(itemId);
        }

        /// <summary>
        /// 获取商品sku集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSkuList(string itemId)
        {
            return itemService.GetItemSkuList(itemId);
        }

        public DataSet GetItemSkuList(string itemId,string userId,string customerId,DateTime beginDate,DateTime endDate)
        {
            return itemService.GetItemSkuList(itemId,userId,customerId,beginDate,endDate);
        }
        /// <summary>
        /// 花间堂房价的数据
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetHotelItemSkuList(string itemId, string userId, string customerId, DateTime beginDate, DateTime endDate)
        {
            return itemService.GetHotelItemSkuList(itemId, userId, customerId, beginDate, endDate);
        }
        /// <summary>
        /// 购买用户集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemSalesUserList(string itemId)
        {
            return itemService.GetItemSalesUserList(itemId);
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemStoreInfo(string itemId)
        {
            return itemService.GetItemStoreInfo(itemId);
        }

        /// <summary>
        /// 获取品牌信息
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemBrandInfo(string itemId)
        {
            return itemService.GetItemBrandInfo(itemId);
        }

        #endregion

        #region 获取同步福利商品

        /// <summary>
        /// 获取同步福利商品
        /// </summary>
        /// <returns></returns>
        public DataSet GetItemTypeList(string latestTime)
        {
            return itemService.GetItemTypeList(latestTime);
        }

        #endregion

        #region 获取门店与商品Mapping集合

        /// <summary>
        /// 获取门店与商品Mapping集合
        /// </summary>
        /// <param name="itemId">商品ID</param>
        /// <returns></returns>
        public DataSet GetItemStoreMapping(string itemId)
        {
            return itemService.GetItemStoreMapping(itemId);
        }

        #endregion

        #region 获取商品信息
        /// <summary>
        /// 获取商品信息
        /// </summary>
        public VwItemDetailEntity GetVwItemDetailById(string itemId, string vipId)
        {
            VwItemDetailEntity itemDetailObj = null;
            var ds = itemService.GetVwItemDetailById(itemId, vipId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemDetailObj = DataTableToObject.ConvertToObject<VwItemDetailEntity>(ds.Tables[0].Rows[0]);
            }
            return itemDetailObj;
        } 
        #endregion

        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<VwItemDetailEntity> GetVwItemDetailList(VwItemDetailEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<VwItemDetailEntity> eventsList = new List<VwItemDetailEntity>();
            DataSet ds = new DataSet();
            ds = itemService.GetVwItemDetailList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<VwItemDetailEntity>(ds.Tables[0]);
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetVwItemDetailListCount(VwItemDetailEntity entity)
        {
            return itemService.GetVwItemDetailListCount(entity);
        }
        #endregion

        #region Jermyn20131121获取 商品属性集合
        /// <summary>
        /// 获取商品属性1集合
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="Prop1DetailId"></param>
        /// <returns></returns>
        public DataSet GetItemProp1List(string itemId)
        {
            return itemService.GetItemProp1List(itemId);
        }
        /// <summary>
        /// 获取商品属性2集合
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="propDetailId"></param>
        /// <returns></returns>
        public DataSet GetItemProp2List(string itemId, string propDetailId)
        {
            return itemService.GetItemProp2List(itemId,propDetailId);
        }
        #endregion

        #region 查询[vw_item_detail]
        public DataRow GetVWItemDetailByItemCode(string itemCode, string customerId)
        {
            DataRow detail;
            DataSet dataSet = new DataSet();
            dataSet = itemService.GetVWItemDetailByItemCode(itemCode, customerId);

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                detail = dataSet.Tables[0].Rows[0];
            else
                detail = null;

            return detail;
        }
        #endregion

        #region 查询[vwAllItemDetail]
        public DataRow GetvwAllItemDetailByItemCode(string itemCode, string customerId)
        {
            DataRow detail;
            DataSet dataSet = new DataSet();
            dataSet = itemService.GetvwAllItemDetailByItemCode(itemCode, customerId);

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                detail = dataSet.Tables[0].Rows[0];
            else
                detail = null;

            return detail;
        }
        #endregion

        #region 自动生成商品编号
        public string GetGreatestItemCode(LoggingSessionInfo CurrentUserInfo)
        {
            string item_code = CurrentUserInfo.CurrentLoggingManager.Customer_Code + "0000001";
            SqlDataReader sdr = itemService.GetGreatestItemCode(CurrentUserInfo.CurrentLoggingManager.Customer_Code, CurrentUserInfo.ClientID);

            Loggers.Debug(new DebugLogInfo() { Message = "当前客户号为：" + CurrentUserInfo.CurrentLoggingManager.Customer_Code });

            if (sdr.Read())
            {
                item_code = sdr["item_code"].ToString();
                Loggers.Debug(new DebugLogInfo() { Message = "最大商品编码为：" + item_code });
                int code = 0;
                int.TryParse(item_code.Substring(CurrentUserInfo.CurrentLoggingManager.Customer_Code.Length), out code);
                Loggers.Debug(new DebugLogInfo() { Message = "最大商品编码编号为：" + code });
                if (code == 0)
                {
                    Loggers.Debug(new DebugLogInfo() { Message = "自动生成产品编码失败，最大编码为" + item_code });
                    item_code = "";
                }
                else
                {
                    item_code = CurrentUserInfo.CurrentLoggingManager.Customer_Code + (code + 1).ToString().PadLeft(7, '0');
                }
            }

            return item_code;
        }
        #endregion

        #region 处理富文本编辑器中的图片
        public void ImageHandler(ItemInfo itemInfo)
        {
            foreach (var item in itemInfo.ItemPropList)
            {
                System.Text.RegularExpressions.MatchCollection matches = null;

                //正则表达式获取<img src=>图片url  
                if (item.IsEditor && item.PropertyCodeValue != "")
                    matches = System.Text.RegularExpressions.Regex.Matches(item.PropertyCodeValue, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                if (matches != null && matches.Count > 0)
                {
                    foreach (System.Text.RegularExpressions.Match match in matches)
                    {
                        string imgTag = match.Value;

                        //去除已有的width
                        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("width=\"\\d+\"");
                        imgTag = r.Replace(imgTag, "", 10);

                        int srcStartIndex = match.Value.ToLower().IndexOf("src=\"");
                        int srcEndIndex = match.Value.ToLower().IndexOf("\"", srcStartIndex + 5);

                        if (!(imgTag.IndexOf("width=\"100%\"") > 0))
                        {
                            //add width='100%'
                            imgTag = imgTag.Insert(srcEndIndex + 1, " width=\"100%\"");
                        }

                        item.PropertyCodeValue = item.PropertyCodeValue.Replace(match.Value, imgTag);
                    }
                }
            }
        }
        #endregion

        #region 获取首页商品
        /// <summary>
        /// 首页商品列表获取
        /// </summary>
        public DataSet GetItemList(string customerId, string categoryId, string itemName, int pageIndex, int pageSize)
        {
            return itemService.GetItemList(customerId, categoryId, itemName, pageIndex, pageSize);
        }
        /// <summary>
        /// 首页商品数量获取
        /// </summary>
        public int GetItemListCount(string customerId, string categoryId, string itemName)
        {
            return itemService.GetItemListCount(customerId, categoryId, itemName);
        }
        #endregion

        /// <summary>
        /// 活动商品列表获取
        /// </summary>
        public DataSet GetItemAreaList(string customerId, string eventTypeId, int pageIndex, int pageSize)
        {
            return itemService.GetItemAreaList(customerId, eventTypeId, pageIndex, pageSize);
        }
        /// <summary>
        /// 活动商品数量获取
        /// </summary>
        public int GetItemAreaListCount(string customerId, string eventTypeId)
        {
            return itemService.GetItemAreaListCount(customerId, eventTypeId);
        }

        public void UpdateMHCategoryAreaByGroupId(string customerId, int groupIdFrom, int groupIdTo)
        {
            itemService.UpdateMHCategoryAreaByGroupId(customerId,groupIdFrom, groupIdTo);
        }


        public DataSet GetLNewsTypeList(string customerId)
        {
            return itemService.GetLNewsTypeList(customerId);
        }
        public DataSet GetLNewsList(string customerId, string newsTypeId, string publishTimeFrom, string publishTimeTo, string newsTitle)
        {
            return itemService.GetLNewsList(customerId, newsTypeId, publishTimeFrom, publishTimeTo, newsTitle);
        }
        public DataSet GetLEventsList(string customerId, string eventTypeId, string title, string eventBeginTime, string eventEndTime)
        {
            return itemService.GetLEventsList(customerId, eventTypeId, title, eventBeginTime, eventEndTime);
        }

        #region 获取评论信息
        public DataSet GetItemCommentList(string pItemId)
        {
            return itemService.GetItemCommentList(pItemId);
        }
        #endregion

        #region 商品详情接口 获取最近一家门店
        public DataSet GetNearbyOneStore(string pLng, string pDim)
        {
            return itemService.GetNearbyOneStore(pLng, pDim);
        }
        #endregion

        #region 获取店铺首页
        /// <summary>
        /// 获取店铺首页   Add by changjian.tian 2014-06-25
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <returns></returns>
        public DataSet GetItemHomePageList(string pCustomerId)
        {
            return itemService.GetItemHomePageList(pCustomerId);
        }
        #endregion

        public DataSet GetItemCommentByItemId(string customerId, string ItemId,  int pageIndex, int pageSize)
        {
            return itemService.GetItemCommentByItemId(customerId, ItemId,
                pageIndex, pageSize);
        }
        public DataSet GetInoutOrderByItemId(string customerId, string ItemId, int pageIndex, int pageSize)
        {
            return itemService.GetInoutOrderByItemId(customerId, ItemId,
                pageIndex, pageSize);
        }

    }
}
