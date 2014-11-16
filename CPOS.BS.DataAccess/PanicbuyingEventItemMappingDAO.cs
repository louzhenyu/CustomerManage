/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:20
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表PanicbuyingEventItemMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingEventItemMappingDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingEventItemMappingEntity>, IQueryable<PanicbuyingEventItemMappingEntity>
    {
           public int GteDisIndex(string EventId)
        {
            string str = "select isnull(DisplayIndex,0) DisplayIndex from PanicbuyingEventItemMapping where IsDelete=0 and EventId='" + EventId + "'";

           object obj= this.SQLHelper.ExecuteScalar(str);
           if (obj==null)
           {
               return 1;
           }
           return (int)obj;
        }
        /// <summary>
        /// 获取商品名称和限购数量
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public DataSet GetEventItemInfo(string eventId, string itemId)
        {
            string sql = @"select b.item_name as ItemName,ISNULL(SinglePurchaseQty,0) as SinglePurchaseQty from PanicbuyingEventItemMapping a 
                        inner join T_Item b on a.ItemId=b.item_id
                        where a.ItemId='"+itemId+"' and a.EventId='"+eventId+"'";
            return this.SQLHelper.ExecuteDataset(sql);
        }
    }
}
