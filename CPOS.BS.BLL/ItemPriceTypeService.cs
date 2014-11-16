using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.Pos;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 商品价格类型
    /// </summary>
    public class ItemPriceTypeService : BaseService
    {
        JIT.CPOS.BS.DataAccess.ItemPriceTypeService itemPriceTypeService = null;
        #region 构造函数
        public ItemPriceTypeService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            itemPriceTypeService = new DataAccess.ItemPriceTypeService(loggingSessionInfo);
        }
        #endregion

        /// <summary>
        /// 获取所有价格类型
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<ItemPriceTypeInfo> GetItemPriceTypeList()
        {
            IList<ItemPriceTypeInfo> itemPriceTypeInfoList = new List<ItemPriceTypeInfo>();
            DataSet ds = new DataSet();
            ds = itemPriceTypeService.GetItemPriceTypeList();
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                itemPriceTypeInfoList = DataTableToObject.ConvertToList<ItemPriceTypeInfo>(ds.Tables[0]);
            }
            return itemPriceTypeInfoList;
        }
        /// <summary>
        /// 根据商品价格类型标识获取商品价格类型信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="item_price_type_id"></param>
        /// <returns></returns>
        public ItemPriceTypeInfo GetItemPriceTypeById(string item_price_type_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("ItemPriceTypeId", item_price_type_id);
            ItemPriceTypeInfo itemPriceTypeInfo = new ItemPriceTypeInfo();
            DataSet ds = new DataSet();
            ds = itemPriceTypeService.GetItemPriceTypeById(item_price_type_id);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                itemPriceTypeInfo = DataTableToObject.ConvertToObject<ItemPriceTypeInfo>(ds.Tables[0].Rows[0]);
            }
            return itemPriceTypeInfo;
        }
    }
}
