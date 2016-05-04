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
using System.Web;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Net;

using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 商品服务
    /// </summary>
    public class ItemService : BaseService
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
        public ItemInfo SearchItemList(string item_code
                                        , string item_name
                                        , string item_category_id
                                        , string status
                                        , string item_can_redeem,string SalesPromotion_id
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
            _ht.Add("SalesPromotion_id", SalesPromotion_id);

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
            itemInfo.SalesPromotionList = itemInfo.ItemCategoryMappingList;
          
            //对于新板块
           // 商品sku名 (基础数据)
            itemInfo.T_ItemSkuProp = new T_ItemSkuPropBLL(loggingSessionInfo).GetItemSkuPropByItemId(item_id);

            //虚拟商品
            T_VirtualItemTypeSettingEntity entityVirtualItemType=new T_VirtualItemTypeSettingBLL(loggingSessionInfo).QueryByEntity(new T_VirtualItemTypeSettingEntity() {ItemId =item_id}, null).FirstOrDefault();
            if (entityVirtualItemType != null)
            {
                itemInfo.VirtualItemTypeId = entityVirtualItemType.VirtualItemTypeId.ToString();
                itemInfo.ObjecetTypeId = entityVirtualItemType.ObjecetTypeId;
            }
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

                    if (itemInfo.OperationType == "ADD")
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
                    bool isOld = true;
                    itemService.SetItemInfo(itemInfo, out strError, isOld, loggingSessionInfo.ClientID);
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
                return itemService.SetSkuInfo(itemInfo, out strError);
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
                int n = itemService.IsExistItemCode(item_code, item_id, loggingSessionInfo.ClientID);
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
            catch (Exception ex)
            {
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

        public DataSet GetItemSkuList(string itemId, string userId, string customerId, DateTime beginDate, DateTime endDate)
        {
            return itemService.GetItemSkuList(itemId, userId, customerId, beginDate, endDate);
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
        public DataSet GetItemProp2List(string itemId, string propDetailId,int type,string eventId)
        {
            return itemService.GetItemProp2List(itemId, propDetailId, type, eventId);
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
        /// 根据活动ID获取商品
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="eventId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetItemListByEventId(string customerId, string eventId, int pageIndex, int pageSize)
        {
            return itemService.GetItemListByEventId(customerId, eventId, pageIndex, pageSize);
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
            itemService.UpdateMHCategoryAreaByGroupId(customerId, groupIdFrom, groupIdTo);
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

        public DataSet GetItemCommentByItemId(string customerId, string ItemId, int pageIndex, int pageSize)
        {
            return itemService.GetItemCommentByItemId(customerId, ItemId,
                pageIndex, pageSize);
        }
        public DataSet GetInoutOrderByItemId(string customerId, string ItemId, int pageIndex, int pageSize)
        {
            return itemService.GetInoutOrderByItemId(customerId, ItemId,
                pageIndex, pageSize);
        }


        #region 新版本保存商品信息



        /// <summary>
        /// 设置商品信息（修改,新建）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo">商品对象</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool SetItemInfoNew(ItemInfo itemInfo, out string strError)
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

                    if (itemInfo.OperationType == "ADD")
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
                    //处理促销分组
                    //先把之前的删除掉
                    ItemCategoryMappingBLL itemCategoryMappingBLL = new ItemCategoryMappingBLL(loggingSessionInfo);
                    itemCategoryMappingBLL.DeleteByItemID(itemInfo.Item_Id);
                    //然后加新的                 ;
                    foreach (var SalesPromotion in itemInfo.SalesPromotionList)
                    {
                        SalesPromotion.MappingId = Guid.NewGuid();
                        SalesPromotion.ItemId = itemInfo.Item_Id;

                     //   SalesPromotion.status = "1";
                        SalesPromotion.IsDelete = 0;
                        SalesPromotion.CreateBy = "";
                        SalesPromotion.CreateTime = DateTime.Now;
                        SalesPromotion.LastUpdateTime = DateTime.Now;
                        SalesPromotion.LastUpdateBy = "";
                        itemCategoryMappingBLL.Create(SalesPromotion);
                    }
                   

                    //处理商品的sku基础数据属性
                    T_ItemSkuPropBLL t_ItemSkuPropBLL = new T_ItemSkuPropBLL(loggingSessionInfo);
                    //先删除之前的sku基础数据
                    t_ItemSkuPropBLL.DeleteByItemID(itemInfo.Item_Id);
                    //再添加新的
                    if (itemInfo.T_ItemSkuProp != null)
                    {
                        T_ItemSkuPropEntity en = new T_ItemSkuPropEntity();
                        en.ItemSku_prop_1_id = itemInfo.T_ItemSkuProp.prop_1_id;
                        en.ItemSku_prop_2_id = itemInfo.T_ItemSkuProp.prop_2_id;
                        en.ItemSku_prop_3_id = itemInfo.T_ItemSkuProp.prop_3_id;
                        en.ItemSku_prop_4_id = itemInfo.T_ItemSkuProp.prop_4_id;
                        en.ItemSku_prop_5_id = itemInfo.T_ItemSkuProp.prop_5_id;
                        en.Item_id = itemInfo.Item_Id;
                        en.ItemSkuPropID = Guid.NewGuid().ToString();//创建新的ID
                        en.status = "1";
                        en.IsDelete = 0;
                        en.create_user_id = "";
                        en.create_time = DateTime.Now;
                        en.modify_time = DateTime.Now;
                        en.modify_user_id = "";
                        t_ItemSkuPropBLL.Create(en);
                    }

                    bool isOld = false;//是否旧版本
                    itemService.SetItemInfo(itemInfo, out strError, isOld, loggingSessionInfo.ClientID);//使用原来的保存商品的方法
                }
                strError = "保存成功!";
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
                    
        }
        #endregion

        //更新上下架状态
        /// <summary>
        /// 根据分类类型(分类，分组)获取商品信息
        /// </summary>
        /// <param name="strBat_id">2:分组1:分类</param>
        /// <returns></returns>
        public DataSet GetItemTreeByCategoryType(string strBat_id)
        {
            return itemService.GetItemTreeByCategoryType(strBat_id);
        }
        /// <summary>
        /// 根据分组Id查询是否有关联商品
        /// </summary>
        /// <param name="strCategoryId"></param>
        /// <param name="strBatId">1:分类，2：分组</param>
        /// <returns></returns>
        public int GetItemCountByCategory(string strCategoryId, string strBatId)
        {
            return itemService.GetItemCountByCategory(strCategoryId, strBatId);
        }
        #region 生成分销商销售商品的二维码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCustomerId">商户标识</param>
        /// <param name="strItemId">商品标识</param>
        /// <param name="strItemName">商品名称</param>
        /// <param name="strRetailTraderId">分销商标识</param>
        /// <param name="strRTIMId">分销商商品关联标识</param>
        public void CreateTraderQRCode(string strCustomerId, string strItemId, string strItemName, string strRetailTraderId, string strRTIMId)
        {
            string weixinDomain = ConfigurationManager.AppSettings["original_url"];
            string sourcePath = HttpContext.Current.Server.MapPath("/QRCodeImage/qrcode.jpg");
            string targetPath = HttpContext.Current.Server.MapPath("/QRCodeImage/");
            string currentDomain = HttpContext.Current.Request.Url.Host;//当前项目域名
            string itemId = strItemId;//商品ID
            string itemName = strItemName;//商品名
            string imageURL;

            ObjectImagesBLL objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
            //查找是否已经生成了二维码
            ObjectImagesEntity[] objectImagesEntityArray = objectImagesBLL.QueryByEntity(new ObjectImagesEntity() { ObjectId = strRTIMId, Description = "自动生成的产品二维码" }, null);

            if (objectImagesEntityArray.Length == 0)
            {
                string applicationId = "";
                //根据CustomerId去取公众号信息

                var list = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { CustomerId = loggingSessionInfo.ClientID, IsDelete = 0 }, null).ToList();

                if (list != null && list.Count > 0)
                {
                    applicationId = list.FirstOrDefault().ApplicationId;
                }
                else
                {
                    //Response.Write("<br>");
                    //Response.Write("没有获取微信标识");
                    throw new Exception("该商户没有绑定微信公众号，无法生成商品二维码");
                }
                //http://api.dev.chainclouds.com
                //    http://api.dev.chainclouds.com/WXOAuth/AuthUniversal.aspx?customerId=049b0a8f641f4ca7b17b0b7b6291de1f&applicationId=1D7A01FC1E7D41ECBAC2696D0D363315&goUrl=api.dev.chainclouds.com/HtmlApps/html/public/shop/goods_detail.html?rootPage=true&rootPage=true&goodsId=DBF5326F4C5B4B0F8508AB54B0B0EBD4&ver=1448273310707&scope=snsapi_userinfo

                string itemUrl = weixinDomain + "/WXOAuth/AuthUniversal.aspx?customerId=" + loggingSessionInfo.ClientID
                    + "&applicationId=" + applicationId
                    + "&goUrl=" + weixinDomain + "/HtmlApps/html/public/shop/goods_detail.html?goodsId="
                    + itemId + "&RetailTraderId=" + strRetailTraderId + "&ChannelID=7&scope=snsapi_userinfo";
                imageURL = Utils.GenerateQRCode(itemUrl, currentDomain, sourcePath, targetPath);
                //如果名称不为空， 就把图片放在一定的背景下面
                if (!string.IsNullOrEmpty(itemName))
                {
                    //  string apiDomain = ConfigurationManager.AppSettings["original_url"];
                    if (imageURL.IndexOf("http://") < 0)
                    {
                        imageURL = "http://" + imageURL;
                    }
                    imageURL = CombinImage(weixinDomain + @"/QRCodeImage/qrcodeBack.jpg", imageURL, itemName);
                }

                //把下载下来的图片的地址存到ObjectImages
                objectImagesBLL.Create(new ObjectImagesEntity()
                {
                    ImageId = Utils.NewGuid(),
                    CustomerId = loggingSessionInfo.ClientID,
                    ImageURL = imageURL,
                    ObjectId = strRTIMId,
                    Title = itemName,
                    Description = "自动生成的分销商销售产品二维码"
                });

                Loggers.Debug(new DebugLogInfo() { Message = "二维码已生成，imageURL:" + imageURL });
            }
            else
            {
                imageURL = objectImagesEntityArray[0].ImageURL;
            }
            string imagePath = "";
            if (imageURL.IndexOf("QRCodeImage") > -1)
            {
                string dirPath = System.AppDomain.CurrentDomain.BaseDirectory;
                var imageName = imageURL.Substring(imageURL.IndexOf("QRCodeImage")).Replace("/", @"\");
                imagePath = dirPath + imageName;//整个
            }
            else
            {  //兼容老的存放地址
                imagePath = targetPath + imageURL.Substring(imageURL.LastIndexOf("/"));
            }

            Loggers.Debug(new DebugLogInfo() { Message = "二维码路径，imagePath:" + imageURL });

        }
        public static string CombinImage(string imgBack, string destImg, string strData)
        {
            //1、上面的图片部分
            HttpWebRequest request_qrcode = (HttpWebRequest)WebRequest.Create(destImg);
            WebResponse response_qrcode = null;
            Stream qrcode_stream = null;
            response_qrcode = request_qrcode.GetResponse();
            qrcode_stream = response_qrcode.GetResponseStream();//把要嵌进去的图片转换成流


            Bitmap _bmpQrcode1 = new Bitmap(qrcode_stream);//把流转换成Bitmap
            Bitmap _bmpQrcode = new Bitmap(_bmpQrcode1, 327, 327);//缩放图片           
            //把二维码由八位的格式转为24位的
            Bitmap bmpQrcode = new Bitmap(_bmpQrcode.Width, _bmpQrcode.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); //并用上面图片的尺寸做了一个位图
            //用上面空的位图生成了一个空的画板
            Graphics g3 = Graphics.FromImage(bmpQrcode);
            g3.DrawImageUnscaled(_bmpQrcode, 0, 0);//把原来的图片画了上去


            //2、背景部分
            HttpWebRequest request_backgroup = (HttpWebRequest)WebRequest.Create(imgBack);
            WebResponse response_keleyi = null;
            Stream backgroup_stream = null;
            response_keleyi = request_backgroup.GetResponse();
            backgroup_stream = response_keleyi.GetResponseStream();//把背景图片转换成流

            Bitmap bmp = new Bitmap(backgroup_stream);
            Graphics g = Graphics.FromImage(bmp);//生成背景图片的画板

            //3、画上文字
            //  String str = "文峰美容";
            System.Drawing.Font font = new System.Drawing.Font("黑体", 25);
            SolidBrush sbrush = new SolidBrush(Color.White);
            SizeF sizeText = g.MeasureString(strData, font);

            g.DrawString(strData, font, sbrush, (bmp.Width - sizeText.Width) / 2, 490);


            // g.DrawString(str, font, sbrush, new PointF(82, 490));


            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);//又把背景图片的位图画在了背景画布上。必须要这个，否则无法处理阴影

            //4.合并图片
            g.DrawImage(bmpQrcode, 130, 118, bmpQrcode.Width, bmpQrcode.Height);

            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            System.Drawing.Image newImg = Image.FromStream(ms);//生成的新的图片
            //把新图片保存下来
            string DownloadUrl = ConfigurationManager.AppSettings["website_WWW"];
            string host = DownloadUrl + "/QRCodeImage/";
            //创建下载根文件夹
            //var dirPath = @"C:\DownloadFile\";
            var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "QRCodeImage\\";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            //根据年月日创建下载子文件夹
            var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            dirPath += ymd + @"\";
            host += ymd + "/";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            //下载到本地文件
            var fileExt = Path.GetExtension(destImg).ToLower();
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + ".jpg";//+ fileExt;
            var filePath = dirPath + newFileName;
            host += newFileName;

            newImg.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return host;
        }
        #endregion
        /// <summary>
        /// 根据分类/分组查询商品sku信息
        /// </summary>
        /// <param name="strCategory"></param>
        /// <param name="strItemName"></param>
        /// <param name="strBatId"></param>
        /// <returns></returns>
        public DataSet GetItemSkuInfoByCategory(string strCategory, string strItemName, string strBatId)
        {
            return itemService.GetItemSkuInfoByCategory(strCategory, strItemName, strBatId);
        }
    }
}
