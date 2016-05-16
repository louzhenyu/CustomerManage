/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/4/27 13:54:52
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表PanicbuyingKJEventSkuMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingKJEventSkuMappingDAO : BaseCPOSDAO, ICRUDable<PanicbuyingKJEventSkuMappingEntity>, IQueryable<PanicbuyingKJEventSkuMappingEntity>
    {
        public DataSet GetKJItemSkuInfo(string eventId,string skuId, string KJEventJoinId)
        {
            string sql = @"select 
                            a.EventSKUMappingId,
                            a.skuId,
                            a.Qty - a.SoldQty as Stock,
                            a.price,
                            a.BasePrice,
                            c.SalesPrice,
                            b.prop_1_detail_name as skuProp1,
                            b.prop_2_detail_name as skuProp2,
                            c.CreateTime
                            from PanicbuyingKJEventSkuMapping a 
                            inner join vw_sku_detail b on a.skuId = b.sku_id
                            inner join PanicbuyingKJEventJoin c on a.skuId = c.skuid
                            inner join PanicbuyingKJEventItemMapping d on a.EventItemMappingID = d.EventItemMappingID
                            where a.skuId = '" + skuId + @"' 
                            and c.KJEventJoinId = '" + KJEventJoinId + @"'
                            and d.EventId = '"+ eventId +"'";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取Sku关系对象配置的底价、原价
        /// </summary>
        /// <param name="EventItemMappingID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public decimal GetConfigPrice(string EventItemMappingID, string Code)
        {
            decimal Money = 0;
            string sql=string.Empty;
            if (Code.Equals("MinPrice"))
            {
                sql = string.Format("select MIN(Price) from PanicbuyingKJEventSkuMapping where IsDelete=0 and EventItemMappingID='{0}'",EventItemMappingID);
            }
            if (Code.Equals("MinBasePrice"))
            {
                sql = string.Format("select MIN(BasePrice) from PanicbuyingKJEventSkuMapping where IsDelete=0 and EventItemMappingID='{0}'", EventItemMappingID);
            }
            var Result = this.SQLHelper.ExecuteScalar(sql);
            if (Result != null)
                Money = Convert.ToDecimal(Result);

            return Money;
        }
    }
}
