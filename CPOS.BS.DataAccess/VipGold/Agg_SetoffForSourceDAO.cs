/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:25:10
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
    /// 表Agg_SetoffForSource的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class Agg_SetoffForSourceDAO : Base.BaseCPOSDAO, ICRUDable<Agg_SetoffForSourceEntity>, IQueryable<Agg_SetoffForSourceEntity>
    {
        /// <summary>
        /// 获取最近几天信息 根据时间、商户编号 获取
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="createTime"></param>
        /// <returns></returns>
        public DataSet GetSetofSourcesListByCustomerId(string CustomerId, int? SetoffRoleId, string beginTime, string endTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT  Sum(SetoffCount) AS 'SetoffCount',Sum(ShareCount) AS 'ShareCount' FROM Agg_SetoffForSource 
                      WHERE CustomerId=@CustomerId ");

            if (SetoffRoleId != null)
            {
                sb.Append(" AND  SetoffRole=" + SetoffRoleId);
            }
            sb.Append(" AND DateCode>=@startTime AND DateCode <@endTime");

            SqlParameter[] parameter = new SqlParameter[]{
                   new SqlParameter("@CustomerId",CustomerId),
                new SqlParameter("@startTime",beginTime),
                new SqlParameter("@endTime",endTime)
            };
            string sql = string.Format(sb.ToString(), CustomerId, beginTime, endTime);
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }

        /// <summary>
        /// 集客来源列表 分页
        /// </summary>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序数组</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForSourceEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();


            #region 查询SQL
            pagedSql.AppendFormat("SELECT top " + PageSize + " * FROM ( select t.*,row_number() over(order by ");


            if (pOrderBys != null)
            {
                foreach (var item in pOrderBys)
                {
                    pagedSql.AppendFormat(item.FieldName + "  "+item.Direction);

                }
            }

            pagedSql.AppendFormat(" ) as RowNumber from (");

            #endregion

            #region 分页SQL
            totalCountSql.AppendFormat("SELECT * FROM ( select t.*,row_number() over(order by ");


            if (pOrderBys != null)
            {
                foreach (var item in pOrderBys)
                {
                    if (item.Direction.ToString() == "2")
                    {
                        totalCountSql.AppendFormat(item.FieldName + "  DESC");
                    }
                    else
                    {
                        totalCountSql.AppendFormat(item.FieldName + "  ASC");
                    }

                }
            }
            else
            {
                totalCountSql.AppendFormat("PushMessageCount  DESC");
            }

            totalCountSql.AppendFormat(" ) as RowNumber from (");
            #endregion

            #region 查询SQL
            pagedSql.AppendFormat(@"  select h.SetoffRole
                            ,h.CustomerId,h.UserId
                            ,sum(h.SetoffCount) as 'SetoffCount' --集客人数
                            ,sum(h.ShareCount) as 'ShareCount' --分享次数
                            ,sum(h.OrderAmount) AS 'OrderAmount' --销量,
                            ,case when isnull(v.viprealname,'') = '' then v.VipName else v.viprealname end as 'UserName'
                            ,tunit.Unit_Name AS 'unitname',
                            SUM(PushMessageCount) AS 'PushMessageCount'
                            from Agg_SetoffForSource as h  
                            LEFT JOIN T_UNIT AS tunit ON tunit.unit_id=h.unitID
                            LEFT JOIN Vip AS v ON v.VIPID=h.UserId
                            Where 1=1   and h.SetoffRole=3");
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
            pagedSql.AppendFormat("    GROUP BY h.SetoffRole,h.CustomerId,h.UserId,tunit.Unit_Name,v.viprealname,v.VipName");



            pagedSql.AppendFormat(@"  UNION ALL  ");

            pagedSql.AppendFormat(@"   select h.SetoffRole
                            ,h.CustomerId,h.UserId
                            ,sum(h.SetoffCount) as 'SetoffCount' --集客人数
                            ,sum(h.ShareCount) as 'ShareCount' --分享次数
                            ,sum(h.OrderAmount) AS 'OrderAmount' --销量,
                            ,tu.user_name as 'UserName',tunit.Unit_Name AS 'unitname',
                            SUM(PushMessageCount) AS 'PushMessageCount'
                            from Agg_SetoffForSource as h  
                            LEFT JOIN T_UNIT AS tunit ON tunit.unit_id=h.unitID
                            LEFT JOIN T_User AS tu ON tu.user_id=h.UserId
                            Where 1=1  and h.SetoffRole <> 3");

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
            pagedSql.AppendFormat(@"      GROUP BY h.SetoffRole,h.CustomerId,h.UserId,tunit.Unit_Name,tu.user_name");

            pagedSql.AppendFormat(@"   ) t 
                        ) AS Temp
                        where  Temp.RowNumber >" + PageSize * (PageIndex - 1) + "");

            #endregion

            #region 分页SQL
            totalCountSql.AppendFormat(@"  select h.SetoffRole
                            ,h.CustomerId,h.UserId
                            ,sum(h.SetoffCount) as 'SetoffCount' --集客人数
                            ,sum(h.ShareCount) as 'ShareCount' --分享次数
                            ,sum(h.OrderAmount) AS 'OrderAmount' --销量,
                            ,case when isnull(v.viprealname,'') = '' then v.VipName else v.viprealname end as 'UserName'
                            ,tunit.Unit_Name AS 'unitname',
                            SUM(PushMessageCount) AS 'PushMessageCount'
                            from Agg_SetoffForSource as h  
                            LEFT JOIN T_UNIT AS tunit ON tunit.unit_id=h.unitID
                            LEFT JOIN Vip AS v ON v.VIPID=h.UserId
                            Where 1=1   and h.SetoffRole=3");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            totalCountSql.AppendFormat("    GROUP BY h.SetoffRole,h.CustomerId,h.UserId,tunit.Unit_Name,v.viprealname,v.VipName");



            totalCountSql.AppendFormat(@"  UNION ALL  ");

            totalCountSql.AppendFormat(@"   select h.SetoffRole
                            ,h.CustomerId,h.UserId
                            ,sum(h.SetoffCount) as 'SetoffCount' --集客人数
                            ,sum(h.ShareCount) as 'ShareCount' --分享次数
                            ,sum(h.OrderAmount) AS 'OrderAmount' --销量,
                            ,tu.user_name as 'UserName',tunit.Unit_Name AS 'unitname',
                            SUM(PushMessageCount) AS 'PushMessageCount'
                            from Agg_SetoffForSource as h  
                            LEFT JOIN T_UNIT AS tunit ON tunit.unit_id=h.unitID
                            LEFT JOIN T_User AS tu ON tu.user_id=h.UserId
                            Where 1=1  and h.SetoffRole <> 3");

            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            totalCountSql.AppendFormat(@"      GROUP BY h.SetoffRole,h.CustomerId,h.UserId,tunit.Unit_Name,tu.user_name");

            totalCountSql.AppendFormat(@"   ) t 
                        ) AS Temp
                        ");

            #endregion

            #region 执行,转换实体,分页属性赋值
            PagedQueryResult<Agg_SetoffForSourceEntity> result = new PagedQueryResult<Agg_SetoffForSourceEntity>();
            List<Agg_SetoffForSourceEntity> list = new List<Agg_SetoffForSourceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    Agg_SetoffForSourceEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            var ds = this.SQLHelper.ExecuteDataset(totalCountSql.ToString());
            int totalCount = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                totalCount = Convert.ToInt32(ds.Tables[0].Rows.Count);    //计算总行数
            }
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, PageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            #endregion

            return result;
        }
    }
}
