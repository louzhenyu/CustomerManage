/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
    /// 表VipWithdrawDepositApply的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipWithdrawDepositApplyDAO : Base.BaseCPOSDAO, ICRUDable<VipWithdrawDepositApplyEntity>, IQueryable<VipWithdrawDepositApplyEntity>
    {
        /// <summary>
        /// 获取今日提款情况
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public VipWithdrawDepositApplyEntity[] GetVipWDApplyByToday(string vipId)
        {
            //组织SQL
            string sql = string.Format(@"
            SELECT [ApplyID]
              ,[VipID]
              ,[WithdrawNo]
              ,[Amount]
              ,[Status]
              ,[ApplyDate]
              ,[ConfirmDate]
              ,[VipBankID]
              ,[CustomerID]
              ,[CreateTime]
              ,[CreateBy]
              ,[LastUpdateTime]
              ,[LastUpdateBy]
              ,[IsDelete]
,CompleteDate
          FROM  [VipWithdrawDepositApply]
          WHERE VipID='{0}' AND  CONVERT(varchar(100),ApplyDate,23)=CONVERT(varchar(100),GETDATE(),23) AND IsDelete=0
            ", vipId);
            //执行SQL

            List<VipWithdrawDepositApplyEntity> list = new List<VipWithdrawDepositApplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositApplyEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }


        public VipWithdrawDepositApplyEntity[] GetVipWDApplyByNumType(string vipId, int WithDrawNumType)
        {
            //组织SQL
            string sql = string.Format(@"
            SELECT [ApplyID]
              ,[VipID]
              ,[WithdrawNo]
              ,[Amount]
              ,[Status]
              ,[ApplyDate]
              ,[ConfirmDate]
              ,[VipBankID]
              ,[CustomerID]
              ,[CreateTime]
              ,[CreateBy]
              ,[LastUpdateTime]
              ,[LastUpdateBy]
              ,[IsDelete]
,CompleteDate
          FROM  [VipWithdrawDepositApply]
          WHERE VipID='{0}'  AND IsDelete=0
            ", vipId);
            //    ---提现次数限制类型 --- （0：不限制  1：日  2：周  3：月）
            if (WithDrawNumType == 1)//
            {
                sql += " AND  CONVERT(varchar(100),ApplyDate,23)=CONVERT(varchar(100),GETDATE(),23)";
            }
            else if (WithDrawNumType == 2)//
            {
                sql += " AND  year( ApplyDate )=year(GETDATE())  and   DATEPART ( WEEK ,  getdate() ) =DATEPART ( WEEK ,  ApplyDate )   ";  //当前年份，，当前星期
            }
            else if (WithDrawNumType == 3)
            {
                sql += " AND  year( ApplyDate )=year(GETDATE())   AND  month( ApplyDate )=month(GETDATE())    ";
            }


            //执行SQL

            List<VipWithdrawDepositApplyEntity> list = new List<VipWithdrawDepositApplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositApplyEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }


        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        /// <summary>
        /// 根据会员名称执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<VipWithdrawDepositApplyEntity> PagedQueryByVipName(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [ApplyID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(@") as ___rn,a.*,v.VipName,CardNo,BankName,AccountName FROM VipWithdrawDepositApply a INNER JOIN dbo.Vip v ON v.VIPID=a.VipID
                              left join VipBank d on a.vipbankid=d.vipbankid 
                                left  join bank e on d.bankid=e.bankid where 1=1  and a.isdelete=0 ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1)  FROM VipWithdrawDepositApply a INNER JOIN dbo.Vip v ON v.VIPID=a.VipID where 1=1  and a.isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<VipWithdrawDepositApplyEntity> result = new PagedQueryResult<VipWithdrawDepositApplyEntity>();
            List<VipWithdrawDepositApplyEntity> list = new List<VipWithdrawDepositApplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositApplyEntity m;
                    this.Load(rdr, out m);
                    if (rdr["VipName"] != DBNull.Value)
                    {
                        m.VipName = Convert.ToString(rdr["VipName"]);
                    }
                    if (rdr["CardNo"] != DBNull.Value)
                    {
                        m.CardNo = Convert.ToString(rdr["CardNo"]);
                    }
                    if (rdr["AccountName"] != DBNull.Value)
                    {
                        m.AccountName = Convert.ToString(rdr["AccountName"]);
                    }
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }
        public bool MultiCheck(string[] ids, int status, string remark = "已完成")
        {
            if (ids == null || ids.Length == 0)
                return false;

            string sql = "update VipWithdrawDepositApply set status=@status";

            if (status == 3)
            {
                sql += " ,CompleteDate=@CompleteDate ";
            }
            if (!string.IsNullOrWhiteSpace(remark))
            {
                sql += ",Remark=@remark";
            }
            sql += " where isdelete=0 and CustomerId=@customerId and applyId in (";
            List<SqlParameter> pList = new List<SqlParameter>();
            for (int i = 0; i < ids.Length; i++)
            {
                sql += "@id" + i + ",";
                pList.Add(new SqlParameter("@id" + i, new Guid(ids[i])));
                if (i == ids.Length - 1)
                {
                    sql = sql.Substring(0, sql.Length - 1);
                }
            }
            sql += ")";
            pList.Add(new SqlParameter("@customerId", CurrentUserInfo.ClientID));
            pList.Add(new SqlParameter("@status", status));
            pList.Add(new SqlParameter("@remark", remark));
            if (status == 3)
            {
                pList.Add(new SqlParameter("@CompleteDate", DateTime.Now));
            }

            int result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, pList.ToArray());
            return result > 0;
        }
        public DataSet PagedQueryDbSet(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex, out int rowCount, out int pageCount)
        {
            var result = new PagedQueryResult<VipWithdrawDepositApplyEntity>();
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerid", CurrentUserInfo.ClientID));
            pList.Add(new SqlParameter("@pageIndex", pCurrentPageIndex));
            pList.Add(new SqlParameter("@pageSize", pPageSize));
            var dbSet = this.SQLHelper.ExecuteDataset(CommandType.Text, PageSql(PageSqlType.Page, pWhereConditions, pOrderBys), pList.ToArray());
            rowCount = (int)this.SQLHelper.ExecuteScalar(CommandType.Text, PageSql(PageSqlType.Count, pWhereConditions, pOrderBys), pList.ToArray());
            int remainder = 0;
            pageCount = Math.DivRem(rowCount, pPageSize, out remainder);
            if (remainder > 0)
            {
                result.PageCount++;
                pageCount++;
            }
            return dbSet;
        }
        public enum PageSqlType { Page, Count };
        public string PageSql(PageSqlType type, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            StringBuilder sql = new StringBuilder(@"SELECT
a.ApplyID,
a.WithdrawNo,
	CASE a.VipType
WHEN 1 THEN
	v.vipname
WHEN 2 THEN
	u.user_name
when 4 THEN
s.SuperRetailTraderName
END AS Name,
 CASE a.VipType
WHEN 1 THEN
	v.Phone
WHEN 2 THEN
	u.user_telephone
when 4 THEN
s.SuperRetailTraderPhone
END AS Phone,
a.VipType,
a.Amount,
c.BankName,
b.CardNo,
b.AccountName,
a.Status,
a.ApplyDate,
a.CompleteDate ");

            sql.Append(@"FROM
	VipWithdrawDepositApply a
LEFT JOIN vip v ON a.vipid = v.vipid
AND v.IsDelete = 0
LEFT JOIN T_User u ON a.VipID = u.user_id
AND u.user_status = 1
left join T_SuperRetailTrader s on a.VipID=cast(s.SuperRetailTraderID as varchar(40))
and s.IsDelete=0 
left join VipBank b on a.VipBankID=b.VipBankID and b.IsDelete=0
left join Bank c on b.BankID=c.BankID and c.IsDelete=0
where a.customerid=@customerid and a.IsDelete=0 ");
            sql = new StringBuilder(" select t.* from (" + sql.ToString() + ")t where 1=1 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        sql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            string blackSql = sql.ToString();
            if (type == PageSqlType.Count)
            {
                sql = new StringBuilder("select count(*) from (" + blackSql + ")tt ");
            }
            else
            {
                sql = new StringBuilder(@"select tt.* ");
                sql.Append(",Row_Number() OVER ( order by ");
                if (pOrderBys != null && pOrderBys.Length > 0)
                {
                    foreach (var item in pOrderBys)
                    {
                        if (item != null)
                        {
                            sql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                        }
                    }
                    sql.Remove(sql.Length - 1, 1);
                }
                else
                {
                    sql.AppendFormat(" [ApplyID] desc"); //默认为主键值倒序
                }
                sql.Append(") rowNumber from (" + blackSql + ")tt");
                sql = new StringBuilder("select * from (" + sql.ToString() + ")ttt where rowNumber between (((@pageIndex-1)*@pageSize)+1) and (@pageIndex*@pageSize)");
            }

            return sql.ToString();
        }

        /// <summary>
        /// 根据店员名称执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<VipWithdrawDepositApplyEntity> PagedQueryByUserName(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [ApplyID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(@") as ___rn,a.*,u.user_name,CardNo,BankName,AccountName FROM VipWithdrawDepositApply a INNER JOIN dbo.T_User u ON u.user_id=a.VipID
                               left join VipBank d on a.vipbankid=d.vipbankid 
                                left  join bank e on d.bankid=e.bankid where 1=1  and a.isdelete=0  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1)  FROM VipWithdrawDepositApply a INNER JOIN dbo.T_User u ON  u.user_id=a.VipID where 1=1  and a.isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<VipWithdrawDepositApplyEntity> result = new PagedQueryResult<VipWithdrawDepositApplyEntity>();
            List<VipWithdrawDepositApplyEntity> list = new List<VipWithdrawDepositApplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositApplyEntity m;
                    this.Load(rdr, out m);
                    if (rdr["user_name"] != DBNull.Value)
                    {
                        m.VipName = Convert.ToString(rdr["user_name"]);
                    }
                    if (rdr["CardNo"] != DBNull.Value)
                    {
                        m.CardNo = Convert.ToString(rdr["CardNo"]);
                    }
                    if (rdr["AccountName"] != DBNull.Value)
                    {
                        m.AccountName = Convert.ToString(rdr["AccountName"]);
                    }
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }



        /// <summary>
        /// 根据分销商名称执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<VipWithdrawDepositApplyEntity> PagedQueryByRetailName(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页SQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [ApplyID] desc"); //默认为主键值倒序
            }
            pagedSql.AppendFormat(@") as ___rn,a.*,u.RetailTraderName,CardNo,BankName,AccountName FROM VipWithdrawDepositApply a INNER JOIN dbo.RetailTrader u ON u.RetailTraderID=a.VipID 
                             left join VipBank d on a.vipbankid=d.vipbankid 
                                left  join bank e on d.bankid=e.bankid where 1=1  and a.isdelete=0  ");
            //总记录数SQL
            totalCountSql.AppendFormat("select count(1)  FROM VipWithdrawDepositApply a INNER JOIN dbo.RetailTrader u ON  u.RetailTraderID=a.VipID where 1=1  and a.isdelete=0 ");
            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //取指定页的数据
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //执行语句并返回结果
            PagedQueryResult<VipWithdrawDepositApplyEntity> result = new PagedQueryResult<VipWithdrawDepositApplyEntity>();
            List<VipWithdrawDepositApplyEntity> list = new List<VipWithdrawDepositApplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositApplyEntity m;
                    this.Load(rdr, out m);
                    if (rdr["RetailTraderName"] != DBNull.Value)
                    {
                        m.VipName = Convert.ToString(rdr["RetailTraderName"]);
                    }
                    if (rdr["CardNo"] != DBNull.Value)
                    {
                        m.CardNo = Convert.ToString(rdr["CardNo"]);
                    }
                    if (rdr["AccountName"] != DBNull.Value)
                    {
                        m.AccountName = Convert.ToString(rdr["AccountName"]);
                    }
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }


    }
}
