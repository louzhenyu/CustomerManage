/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/30 9:30:35
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
    /// 表ItemDownloadLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ItemDownloadLogDAO : Base.BaseCPOSDAO, ICRUDable<ItemDownloadLogEntity>, IQueryable<ItemDownloadLogEntity>
    {
        #region 获取已下载用户集合
        public int GetDownloadUsersByItemCount(string ItemId)
        {
            string sql = GetDownloadUsersByItemSql(ItemId);
            sql = " select count(*) From #tmp ; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        public DataSet GetDownloadUsersByItem(string ItemId, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = Page * PageSize;
            DataSet ds = new DataSet();
            string sql = GetDownloadUsersByItemSql(ItemId);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetDownloadUsersByItemSql(string ItemId)
        {
            string sql = "SELECT a.* "
                    + " ,b.VipName UserName "
                    + " ,'' ImageURL "
                    + " ,DisplayIndex = row_number() over(order by a.LastUpdateTime DESC) "
                    + " INTO #tmp "
                    + " FROM ItemDownloadLog a "
                    + " INNER JOIN dbo.Vip b ON(a.UserId = b.VIPID) "
                    + " WHERE a.IsDelete = '0' "
                    + " AND b.IsDelete = '0' and a.ItemId = '"+ItemId+"' ; ";

            return sql;
        }
        #endregion
    }
}
