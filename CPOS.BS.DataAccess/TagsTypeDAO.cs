/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/31 15:57:02
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
    /// 表TagsType的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TagsTypeDAO : Base.BaseCPOSDAO, ICRUDable<TagsTypeEntity>, IQueryable<TagsTypeEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(TagsTypeEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(TagsTypeEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(TagsTypeEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " into #tmp ";
            sql += " from TagsType a ";
            sql += " left join TagsTypeMapping b on (a.TypeId=b.TypeId and b.IsDelete='0') ";
            sql += " where a.IsDelete='0' ";
            if (entity.TypeId != null && entity.TypeId.Trim().Length > 0)
            {
                sql += " and a.TypeId = '" + entity.TypeId + "' ";
            }
            if (entity.CustomerId != null && entity.CustomerId.Trim().Length > 0)
            {
                sql += " and b.CustomerId = '" + entity.CustomerId + "' ";
            }
            return sql;
        }
        #endregion


        public DataSet GetAll2( int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.CREATETIME";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";
            var sqlCon = "";
            //if (!string.IsNullOrEmpty(VipCardCode))
            //{
            //    sqlCon += " and a.vipcardcode like '%" + VipCardCode + "%'";
            //}
            //if (!string.IsNullOrEmpty(Phone))
            //{
            //    sqlCon += " and c.Phone like '%" + Phone + "%'";
            //}
            //if (ContinueExpensesStatus == 1)
            //{
            //    //and操作，如果前面已经是false了，后面的判断就不执行了
            //    sqlCon += " and isnull(a.enddate,'')!='' and convert(datetime,a.enddate)<	DateAdd (month,2,getdate())";//两个月内到期的，即当前日期加上两个月就大于过期日的
            //}

            //if (!string.IsNullOrEmpty(ContinueExpensesStatus))//支付状态
            //{
            //    sqlCon += " and (case  when a.haspay=0 then '未付款' when haspay=1 then '已付款' end)= '" + PayStatus + "'";

            //}
            List<SqlParameter> ls = new List<SqlParameter>();
            //ls.Add(new SqlParameter("@UserID", UserID));
            //ls.Add(new SqlParameter("@CustomerId", customerId));

            //办卡人是vip本身
            var sql = @" 
select *  from TagsType a 
                                 WHERE 1 = 1 AND    a.isdelete = 0 
   {4}
                 ";  //总数据的表tab[0]
            sql = sql + @"select * from ( select ROW_NUMBER()over(order by {0} {3}) _row,a.*
                                    from TagsType a 
                                 WHERE 1 = 1 AND    a.isdelete = 0 
   {4}  
                                ) t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}
";

            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), ls.ToArray());
        }


          public DataSet HasUse(string TypeId)
        {

            string sql = @"select * from VipTagsMapping where TagsId in 
(select TagsId from Tags where TypeId=@TypeId) ";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@TypeId", TypeId));

         DataSet   ds = this.SQLHelper.ExecuteDataset(CommandType.Text,sql,ls.ToArray());
            return ds;
        }

          public void DeleteTagsType(string TypeId)
        {
            string sql = @"update  Tags set isdelete=1 where TypeId=@TypeId
                update  TagsType set isdelete=1 where TypeId=@TypeId ";  
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@TypeId",TypeId));

        this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());
            
        }

    }
}
