/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:46
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
    /// 表PanicbuyingKJEventItemMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingKJEventItemMappingDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingKJEventItemMappingEntity>, IQueryable<PanicbuyingKJEventItemMappingEntity>
    {
        public PanicbuyingKJEventItemMappingEntity GetPanicbuyingEventEntity(string eventId, string Skuid)
        {
            string sql = @"select top 1 a.* from PanicbuyingKJEventItemMapping a 
                           inner join PanicbuyingKJEventSkuMapping b on a.EventItemMappingID = b.EventItemMappingID
                           where a.EventId = '" + eventId + "' and b.SkuId = '" + Skuid + "'";
            PanicbuyingKJEventItemMappingEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            return m;
        }

        public DataSet GetKJItemProp1List(string itemId,string EventId)
        {
            string sql = "SELECT x.prop1DetailId,x.prop1DetailName,MAX(skuId) skuId,sum(x.Stock) Stock,sum(x.SalesCount) SalesCount FROM ( SELECT DISTINCT a.sku_id skuId,a.sku_prop_id1 prop1DetailId,ISNULL(b.prop_name,a.sku_prop_id1) prop1DetailName,isnull(b.display_index,0) display_index,c.Stock,c.SalesCount FROM dbo.T_Sku a "
                        + " LEFT JOIN dbo.T_Prop b "
                        + " ON(a.sku_prop_id1 = b.prop_id "
                        + " AND b.status = '1') "
                        + " LEFT JOIN vw_sku_detail c ON a.sku_id=c.sku_id "
                        + " inner join PanicbuyingKJEventSkuMapping d on a.sku_id = d.skuId"
                        + " inner join PanicbuyingKJEventItemMapping e on e.EventItemMappingID = d.EventItemMappingID"
                        + " WHERE a.status = '1' "
                        + " AND e.EventId = '" + EventId +"'"
                        + " AND a.item_id = '" + itemId + "' ) x GROUP BY x.prop1DetailId,x.prop1DetailName,x.display_index order by x.display_index ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
    }
}
