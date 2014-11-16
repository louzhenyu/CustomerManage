/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/18 19:10:46
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
    /// 表StoreItemDailyStatus的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class StoreItemDailyStatusDAO : Base.BaseCPOSDAO, ICRUDable<StoreItemDailyStatusEntity>, IQueryable<StoreItemDailyStatusEntity>
    {
        public DataSet GetList(string beginDate, string endDate, string storeID, string skuID, int pageIndex, int pageSize, out int totalCount)
        {
            string sql = "SELECT * From StoreItemDailyStatus where 1=1 ";
            if (!string.IsNullOrEmpty(beginDate))
            {
                sql += string.Format(" AND StatusDate>='{0}'", beginDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                sql += string.Format(" AND StatusDate<='{0}'", endDate);
            }
            if (!string.IsNullOrEmpty(storeID))
            {
                sql += string.Format(" AND StoreID='{0}'", storeID);
            }
            if (!string.IsNullOrEmpty(skuID))
            {
                sql += string.Format(" AND SkuID='{0}'", skuID);
            }
            totalCount = Convert.ToInt32(SQLHelper.ExecuteScalar(string.Format("select count(*) from ({0}) a", sql)));
            int beginSize = (pageIndex - 1) * pageSize + 1;
            int endSize = beginSize + pageSize - 1; ;
            string pagedSql = string.Format("SELECT ISNULL(StockAmount,0) StockAmount,ISNULL(UsedAmount,0) UsedAmount,* FROM (SELECT a.*,rowNo=row_number() over(order by a.StatusDate asc) from ({0}) a) t WHERE 1=1 and IsDelete=0 and t.rowNo between '" + beginSize + "' and '" + endSize + "' order by  t.rowNo ", sql);
            DataSet ds = SQLHelper.ExecuteDataset(pagedSql);
            return ds;
        }

        /// <summary>
        /// 获取 商品名称，单品ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetItemList(string storeID)
        {
            string sql = @"select item_name,sku_id from T_Item i 
                            inner join T_Sku s on i.item_id=s.item_id
                            inner join ItemStoreMapping ISM ON ISM.ItemId=I.item_id AND ISM.IsDelete=0
                            where s.[status]=1 ";

            sql += string.Format("AND ISM.UnitId='{0}'", storeID);

            return SQLHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }
        /// <summary>
        /// 获取终端列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetStoreList()
        {
            string sql = string.Format(@"select unit_id,unit_name from t_unit  where customer_id='{0}' and [Status]=1",
                CurrentUserInfo.ClientID);

            return SQLHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }
    }
}
