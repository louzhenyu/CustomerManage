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
    /// 商品属性关系
    /// </summary>
    public class ItemPropService:BaseService
    {
        JIT.CPOS.BS.DataAccess.ItemPropService itemPropService = null;
        #region 构造函数
        public ItemPropService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            itemPropService = new DataAccess.ItemPropService(loggingSessionInfo);
        }
        #endregion

        /// <summary>
        /// 获取商品的属性集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public IList<ItemPropInfo> GetItemPropListByItemId(string itemId)
        {
            DataSet ds = new DataSet();
            ds = itemPropService.GetItemPropListByItemId(itemId);
            IList<ItemPropInfo> itemPropInfoList = new List<ItemPropInfo>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                itemPropInfoList = DataTableToObject.ConvertToList<ItemPropInfo>(ds.Tables[0]);
            }
            return itemPropInfoList;
        }

        /// <summary>
        /// 设置商品与商品属性关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool SetItemPropInfo(LoggingSessionInfo loggingSessionInfo, ItemInfo itemInfo)
        {
            try
            {
                if (itemInfo.ItemPropList != null)
                {
                    foreach (ItemPropInfo itemPropInfo in itemInfo.ItemPropList)
                    {
                        if (itemPropInfo.Item_Property_Id == null || itemPropInfo.Item_Property_Id.Equals(""))
                        {
                            itemPropInfo.Item_Property_Id = NewGuid();
                        }
                    }
                    //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("ItemProp.InsertOrUpdate", itemInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取商品级别的属性集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <returns></returns>
        public IList<PropByItemInfo> GetPropByItemList()
        {
            DataSet ds = new DataSet();
            ds = itemPropService.GetItemPropInfoList();
            IList<PropByItemInfo> itemPropInfoList = new List<PropByItemInfo>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                itemPropInfoList = DataTableToObject.ConvertToList<PropByItemInfo>(ds.Tables[0]);
            }
            return itemPropInfoList;
        }
    }
}
