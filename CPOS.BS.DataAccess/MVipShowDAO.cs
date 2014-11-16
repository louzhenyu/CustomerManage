/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/10 10:06:07
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
    /// 表MVipShow的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MVipShowDAO : Base.BaseCPOSDAO, ICRUDable<MVipShowEntity>, IQueryable<MVipShowEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(MVipShowEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(MVipShowEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(MVipShowEntity entity)
        {
            string orderBy = string.Empty;
            orderBy = "CreateTime";
            if (entity.OrderBy == null && entity.OrderBy.Length == 0)
            {
                orderBy = entity.OrderBy;
            }
            if (entity.IsMe == 1)
            {
                orderBy = "CreateTime";
            }
            if (entity.IsNewest == 1)
            {
                orderBy = "CreateTime";
            }
            if (entity.IsTop == 1)
            {
                orderBy = "PraiseCount";
            }
            
            string sql = string.Empty;
            sql = "select xx.*,DisplayIndex = row_number() over(order by xx." + orderBy + " desc) "
                + " into #tmp "
                + " From ( "
                + " select distinct a.VipShowId,a.VipId,a.UnitId,a.Experience,a.HairStylistId,a.CreateBy,a.CreateTime,a.IsDelete,a.IsCheck,a.LotteryCode  "
                + " ,(SELECT COUNT(*) FROM LEventsEntriesPraise x WHERE x.entriesId = a.VipShowId) PraiseCount "
                + " ,(SELECT ISNULL(COUNT(*),0) FROM LEventsEntriesPraise x WHERE x.entriesId = a.VipShowId AND x.signupid = '"+entity.UserId+"') isPraise ";

            sql += " ,b.unit_name UnitName, c.VipName ";
            sql += " ,d2.Item_Name as ItemName, d.ItemId ";
            sql += " from [MVipShow] a ";
            sql += " left join t_unit b on a.unitId=b.unit_id ";
            sql += " left join Vip c on a.vipId=c.vipId ";
            sql += " left join MVipShowItemMapping d on (a.VipShowId=d.VipShowId and d.isDelete='0') ";
            sql += " left join t_Item d2 on (d.itemId=d2.item_Id) ";
            sql += " where 1=1 ";
            if (entity.UseDelete)
            {
                sql += " and a.IsDelete='0' ";
            }
            if (entity.VipId != null)
            {
                sql += " and a.VipId = '" + entity.VipId + "' ";
            }
            if (entity.VipShowId != null && entity.VipShowId.Length > 0)
            {
                sql += " and a.vipShowId = '" + entity.VipShowId + "' ";
            }
            if (entity.Experience != null && entity.Experience.Length > 0)
            {
                sql += " and a.Experience like '%" + entity.Experience + "%' ";
            }
            if (entity.IsMe == 1)
            {
                sql += " and a.vipId = '" + entity.UserId + "'";
            }
            if (entity.VipName != null && entity.VipName.Length > 0)
            {
                sql += " and c.VipName like '%" + entity.VipName + "%' ";
            }
            if (entity.ItemName != null && entity.ItemName.Length > 0)
            {
                sql += " and d2.Item_Name like '%" + entity.ItemName + "%' ";
            }
            if (entity.BeginTime != null && entity.BeginTime.Length > 0)
            {
                var tmpBeginTime = Convert.ToDateTime(entity.BeginTime).ToString("yyyy-MM-dd");
                sql += " and CONVERT(CHAR(19), a.CreateTime, 120) like '%" + tmpBeginTime + "%' ";
            }
            if (entity.CustomerId != null && entity.CustomerId.Length > 0)
            {
                sql += " and a.CustomerId = '" + entity.CustomerId + "' ";
            }
            if (entity.IsCheck == 1)
            {
                sql += " and a.IsCheck = '1' ";
            }

            sql += " ) xx ";
            

            return sql;
        }
        #endregion

        #region 根据门店获取发型师
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetHairStylistByStoreIdCount(string customerId, string unitId)
        {
            string sql = GetHairStylistByStoreIdSql(customerId, unitId);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetHairStylistByStoreId(string customerId, string unitId, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetHairStylistByStoreIdSql(customerId, unitId);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetHairStylistByStoreIdSql(string customerId, string unitId)
        {
            string sql = string.Empty;
            sql = "select c.* ";
            sql += " ,DisplayIndex = row_number() over(order by c.user_Name asc) ";
            sql += " ,UnitName=(select unit_name from t_unit where unit_id='" + unitId + "') ";
            sql += " into #tmp ";
            sql += " from T_User_Role a ";
            sql += " INNER JOIN T_Role b ON(a.role_id = b.role_id) ";
            sql += " inner join t_user c on a.user_id=c.user_id ";
            sql += " where 1=1 ";
            sql += " AND b.role_code = 'HairStylist' ";
            sql += " and b.customer_id = '" + customerId + "' ";
            sql += " and a.unit_id = '" + unitId + "' ";
            return sql;
        }
        #endregion

        #region 修改状态
        /// <summary>
        /// 修改状态
        /// </summary>
        public bool SetStatus(MVipShowEntity obj)
        {
            string sql = "update MVipShow "
                      + " set [IsDelete] = '" + obj.IsDelete + "'"
                      + " ,LastUpdateTime = '" + obj.LastUpdateTime + "' "
                      + " ,LastUpdateBy = '" + obj.LastUpdateBy + "' "
                      + " where VipShowId = '" + obj.VipShowId + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region  获取抽奖机会
        public DataSet GetLotteryCount(string customerId, string vipId)
        {
            DataSet ds = new DataSet();
            string sql = " select * From dbo.MVipShow a WHERE a.IsDelete = '0' AND IsCheck = '1' AND ISNULL(IsLottery,0) = '0' AND customerId = '" + customerId + "' AND VipId='" + vipId + "' ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

    }
}
