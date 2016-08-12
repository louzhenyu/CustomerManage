/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    /// 表T_SuperRetailTrader的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderDAO : Base.BaseCPOSDAO, ICRUDable<T_SuperRetailTraderEntity>, IQueryable<T_SuperRetailTraderEntity>
    {
        /// <summary>
        ///  获取分销商信息{注意成为分销商之前的信息要默认过滤掉}
        /// </summary>
        /// <param name="customerId">商户条件</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页数</param>
        /// <param name="HigheSuperRetailTraderID">父节点信息</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageIndex, int PageSize, string customerId)
        {

            #region 组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            pagedSql.Append(" SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY  ");

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
                pagedSql.AppendFormat(" a.JoinTime desc"); //默认为主键值倒序
            }


            pagedSql.Append(@") AS RowNumber,a.*,
                                            WithdrawCount=(SELECT COUNT(*) FROM VipWithdrawDepositApply WHERE VipId=a.SuperRetailTraderFromId),
                                            OrderCount=ISNULL((SELECT COUNT(*) FROM T_inout WHERE sales_user=cast(a.SuperRetailTraderID AS VARCHAR(500))),0),
                                            WithdrawTotalMoney=ISNULL((SELECT SUM(Amount) FROM VipWithdrawDepositApply WHERE status=3 AND VipId=cast(a.SuperRetailTraderID AS VARCHAR(500))),0),
                                            NumberOffline=ISNULL((SELECT COUNT(*) FROM T_SuperRetailTrader WHERE HigheSuperRetailTraderID=a.SuperRetailTraderID AND Status='10'),0)
                                 from T_SuperRetailTrader as a  ");
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",customerId)
            };
            pagedSql.Append("  Where 1=1 ");

            #region 记录总数sql
            totalCountSql.Append("select count(1) from T_SuperRetailTrader as a ");

            totalCountSql.Append("Where 1=1 ");
            #endregion

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
            pagedSql.AppendFormat(") as h where RowNumber >{0}*({1}-1) AND RowNumber<={0}*{1} ", PageSize, PageIndex);
            #endregion
            Loggers.Debug(new DebugLogInfo() { Message = pagedSql.ToString() });

            #region 执行,转换实体,分页属性赋值
            PagedQueryResult<T_SuperRetailTraderEntity> result = new PagedQueryResult<T_SuperRetailTraderEntity>();
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameter))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);

                    if (rdr["OrderCount"] != DBNull.Value)
                    {
                        m.OrderCount = Convert.ToInt32(rdr["OrderCount"] == null ? "0" : rdr["OrderCount"]);
                    }

                    if (rdr["WithdrawCount"] != DBNull.Value)
                    {
                        m.WithdrawCount = Convert.ToInt32(rdr["WithdrawCount"] == null ? "0" : rdr["WithdrawCount"]);
                    }

                    if (rdr["WithdrawTotalMoney"] != DBNull.Value)
                    {
                        m.WithdrawTotalMoney = Convert.ToDecimal(rdr["WithdrawTotalMoney"] == null ? "0" : rdr["WithdrawTotalMoney"]);
                    }

                    if (rdr["NumberOffline"] != DBNull.Value)
                    {
                        m.NumberOffline = Convert.ToInt32(rdr["NumberOffline"] == null ? "0" : rdr["NumberOffline"]);
                    }
                }
            }
            result.Entities = list.ToArray();

            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, PageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            #endregion
            return result;
        }
        /// <summary>
        ///  获取分销商信息{注意成为分销商之前的信息要默认过滤掉} 导出 不需要分页
        /// </summary>
        /// <param name="customerId">商户条件</param>
        /// <param name="HigheSuperRetailTraderID">父节点信息</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, string customerId)
        {

            #region 组织SQL
            StringBuilder pagedSql = new StringBuilder();
            pagedSql.Append(" SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY  ");

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
                pagedSql.AppendFormat(" a.JoinTime desc"); //默认为主键值倒序
            }


            pagedSql.Append(@") AS RowNumber,a.*,
                                            WithdrawCount=(SELECT COUNT(*) FROM VipWithdrawDepositApply WHERE VipId=a.SuperRetailTraderFromId),
                                            OrderCount=(SELECT COUNT(*) FROM T_inout WHERE sales_user=cast(a.SuperRetailTraderID as varchar(500))),
                                            WithdrawTotalMoney=(SELECT SUM(Amount) FROM VipWithdrawDepositApply WHERE status=3 AND VipId=cast(a.SuperRetailTraderID AS VARCHAR(500))),
                                           NumberOffline=(SELECT COUNT(*) FROM T_SuperRetailTrader WHERE HigheSuperRetailTraderID=a.SuperRetailTraderID AND Status='10')
                                 from T_SuperRetailTrader as a  ");
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",customerId)
            };
            pagedSql.Append("  Where 1=1 ");


            //过滤条件
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as h  ");
            #endregion


            #region 执行,转换实体,分页属性赋值
            PagedQueryResult<T_SuperRetailTraderEntity> result = new PagedQueryResult<T_SuperRetailTraderEntity>();
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameter))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);

                    if (rdr["OrderCount"] != DBNull.Value)
                    {
                        m.OrderCount = Convert.ToInt32(rdr["OrderCount"] == null ? "0" : rdr["OrderCount"]);
                    }

                    if (rdr["WithdrawCount"] != DBNull.Value)
                    {
                        m.WithdrawCount = Convert.ToInt32(rdr["WithdrawCount"] == null ? "0" : rdr["WithdrawCount"]);
                    }

                    if (rdr["WithdrawTotalMoney"] != DBNull.Value)
                    {
                        m.WithdrawTotalMoney = Convert.ToDecimal(rdr["WithdrawTotalMoney"] == null ? "0" : rdr["WithdrawTotalMoney"]);
                    }

                    if (rdr["NumberOffline"] != DBNull.Value)
                    {
                        m.NumberOffline = Convert.ToInt32(rdr["NumberOffline"] == null ? "0" : rdr["NumberOffline"]);
                    }
                }
            }
            result.Entities = list.ToArray();
            result.RowCount = 0;
            #endregion
            return result;
        }

        /// <summary>
        /// 获取所有父级
        /// </summary>
        /// <param name="strSuperRetailTraderId"></param>
        /// <returns></returns>
        public DataSet GetAllFather(string strSuperRetailTraderId)
        {
            string strSql = string.Format(@"  WITH SuperRetailTraderFather([SuperRetailTraderID], [HigheSuperRetailTraderID],level)
                                              AS
                                              ( 
                                                SELECT [SuperRetailTraderID],[HigheSuperRetailTraderID],1 
                                                FROM [T_SuperRetailTrader]
                                                WHERE [SuperRetailTraderID] = '{0}'
 
                                                UNION all
                                                SELECT e.[SuperRetailTraderID],e.[HigheSuperRetailTraderID],es.level+1
                                                FROM [T_SuperRetailTrader] AS e
                                                  JOIN SuperRetailTraderFather AS es
                                                    ON es.[HigheSuperRetailTraderID] = e.[SuperRetailTraderID]
                                              )
                                                SELECT * FROM SuperRetailTraderFather", strSuperRetailTraderId);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
    }
}
