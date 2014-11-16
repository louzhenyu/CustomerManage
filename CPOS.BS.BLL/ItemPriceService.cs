using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 商品价格
    /// </summary>
    public class ItemPriceService:BaseService
    {
        JIT.CPOS.BS.DataAccess.ItemPriceService itemProceService = null;
        #region 构造函数
        public ItemPriceService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            itemProceService = new DataAccess.ItemPriceService(loggingSessionInfo);
        }
        #endregion

        /// <summary>
        /// 根据商品获取商品价格集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public IList<ItemPriceInfo> GetItemPriceListByItemId(string itemId)
        {
            DataSet ds = new DataSet();
            IList<ItemPriceInfo> itemPriceInfoList = new List<ItemPriceInfo>();
            ds = itemProceService.GetItemPriceListByItemId(itemId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemPriceInfoList = DataTableToObject.ConvertToList<ItemPriceInfo>(ds.Tables[0]);
            }
            return itemPriceInfoList;

        }

        /// <summary>
        /// 设置商品与商品类型与价格关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemPriceInfo(LoggingSessionInfo loggingSessionInfo, ItemInfo itemInfo)
        {
            try
            {
                itemInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                if (itemInfo.ItemPriceList != null)
                {
                    foreach (ItemPriceInfo itemPriceInfo in itemInfo.ItemPriceList)
                    {
                        if (itemPriceInfo.item_price_id == null || itemPriceInfo.item_price_id.Equals(""))
                        {
                            itemPriceInfo.item_price_id = NewGuid();
                        }
                        itemPriceInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                    }
                    //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemPrice.InsertOrUpdate", itemInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region 下载

        ///// <summary>
        ///// 获取未打包的商品零售价格数量
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <returns></returns>
        //public int GetItemPriceNotPackagedCount(LoggingSessionInfo loggingSessionInfo)
        //{
        //    Hashtable _ht = new Hashtable();
        //    _ht.Add("ItemPriceTypeId", "75412168A16C4D2296B92CA0E596A188");
        //    _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
        //    return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("ItemPrice.SelectUnDownloadCount", _ht);
        //}
        ///// <summary>
        ///// 需要打包的商品零售价格信息
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="maxRowCount">当前页数量</param>
        ///// <param name="startRowIndex">开始数量</param>
        ///// <returns></returns>
        //public IList<ItemPriceInfo> GetItemPriceListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex)
        //{
        //    Hashtable _ht = new Hashtable();
        //    _ht.Add("StartRow", startRowIndex);
        //    _ht.Add("EndRow", startRowIndex + maxRowCount);
        //    _ht.Add("ItemPriceTypeId", "75412168A16C4D2296B92CA0E596A188");
        //    _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
        //    return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<ItemPriceInfo>("ItemPrice.SelectUnDownload", _ht);
        //}

        ///// <summary>
        ///// 设置零售价格打包批次号
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次号</param>
        ///// <param name="ItemPriceInfoList">商品集合</param>
        ///// <param name="strError">错误信息返回</param>
        ///// <returns></returns>
        //public bool SetItemPriceBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<ItemPriceInfo> ItemPriceInfoList, out string strError)
        //{
        //    ItemPriceInfo itemPriceInfo = new ItemPriceInfo();
        //    itemPriceInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
        //    itemPriceInfo.modify_time = GetCurrentDateTime();
        //    itemPriceInfo.bat_id = bat_id;
        //    itemPriceInfo.ItemPriceInfoList = ItemPriceInfoList;
        //    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemPrice.UpdateUnDownloadBatId", itemPriceInfo);
        //    strError = "Success";
        //    return true;
        //}
        ///// <summary>
        ///// 更新Item零售价格表打包标识方法
        ///// </summary>
        ///// <param name="loggingSessionInfo">登录model</param>
        ///// <param name="bat_id">批次标识</param>
        ///// <param name="strError">错误信息返回</param>
        ///// <returns></returns>
        //public bool SetItemPriceIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        //{
        //    ItemPriceInfo itemPriceInfo = new ItemPriceInfo();
        //    itemPriceInfo.bat_id = bat_id;
        //    itemPriceInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
        //    itemPriceInfo.modify_time = GetCurrentDateTime();
        //    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemPrice.UpdateUnDownloadIfFlag", itemPriceInfo);
        //    strError = "Success";
        //    return true;
        //}

        #endregion
    }
}
