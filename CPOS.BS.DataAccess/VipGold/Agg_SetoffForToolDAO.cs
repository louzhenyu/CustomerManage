/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/20 9:22:52
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
    /// 表Agg_SetoffForTool的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class Agg_SetoffForToolDAO : Base.BaseCPOSDAO, ICRUDable<Agg_SetoffForToolEntity>, IQueryable<Agg_SetoffForToolEntity>
    {
        /// <summary>
        /// 获取最近几天信息 根据时间、商户编号 获取
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="DateCode">统计日期</param>
        /// <param name="SetoffToolTypeId">集客工具类型</param>
        /// <returns></returns>
        public DataSet GetSetofToolListByCustomerId(string CustomerId, string SetoffToolTypeId, string begintime, string endtime, string DateCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT  Sum(SetoffCount) AS 'SetoffCount',Sum(ShareCount) AS 'ShareCount'  FROM Agg_SetoffForTool 
                      WHERE CustomerId=@CustomerId ");

            if (!String.IsNullOrEmpty(SetoffToolTypeId))
            {
                sb.Append(" AND  SetoffToolType='" + SetoffToolTypeId + "'");
            }

            sb.Append(" AND DateCode>=@startTime and DateCode<@endTime");

            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId),
                 new SqlParameter("@startTime",begintime),
                  new SqlParameter("@endTime",endtime),
            };

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sb.ToString(), parameter);
        }
        /// <summary>
        /// 集客来源列表 分页
        /// </summary>
        /// <param name="pWhereConditions">条件数组</param>
        /// <param name="pOrderBys">排序数组</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForToolEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {

            #region 组织SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();

            pagedSql.AppendFormat("select top " + PageSize + " * from (  select T.*,ROW_NUMBER() OVER (ORDER BY ");

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
                pagedSql.AppendFormat(" a.OrderAmount desc"); //默认为主键值倒序
                totalCountSql.AppendFormat(" a.OrderAmount desc"); //默认为主键值倒序
            }

            pagedSql.AppendFormat(" ) AS RowNumber from (");

            pagedSql.AppendFormat(@" ");
            totalCountSql.AppendFormat(@" ");

            #region 查询SQL语句
            #region 优惠券
            pagedSql.AppendFormat(@"           SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               CouponType.CouponTypeName AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN CouponType ON CouponType.CouponTypeID=a.ObjectId
                          Where 1=1  AND SetoffToolType='Coupon'");   //优惠券


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

            pagedSql.AppendFormat(@"GROUP BY a.SetoffToolType,a.CustomerId,CouponType.CouponTypeName");
            #endregion

            #region 海报
            pagedSql.AppendFormat(@"      UNION ALL     SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               SetoffPoster.Name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN SetoffPoster ON SetoffPoster.SetoffPosterID=a.ObjectId
                          Where 1=1  AND SetoffToolType='SetoffPoster'");  //海报

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
            pagedSql.AppendFormat(@"  GROUP BY a.SetoffToolType,a.CustomerId,SetoffPoster.Name ");
            #endregion

            #region 商品
            pagedSql.AppendFormat(@"      UNION ALL      SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               T_Item.item_name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN T_Item ON T_Item.item_id=a.ObjectId
                          Where 1=1   AND SetoffToolType='Goods'
                        ");  //商品

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
            pagedSql.AppendFormat(@"   GROUP BY a.SetoffToolType,a.CustomerId,T_Item.item_name");
            #endregion

            #region 任意仓库活动
            pagedSql.AppendFormat(@"      UNION ALL     SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               T_CTW_LEvent.Name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN T_CTW_LEvent ON T_CTW_LEvent.CTWEventId=a.ObjectId
                               Where 1=1   AND SetoffToolType='CTW'
                              ");  //创意仓库活动

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
            pagedSql.AppendFormat(@"    GROUP BY a.SetoffToolType,a.CustomerId,T_CTW_LEvent.Name");
            #endregion

            #endregion


            #region 分页SQL语句
            #region 优惠券
            totalCountSql.AppendFormat(@"           SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               CouponType.CouponTypeName AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN CouponType ON CouponType.CouponTypeID=a.ObjectId
                          Where 1=1  AND SetoffToolType='Coupon'");   //优惠券


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

            totalCountSql.AppendFormat(@"GROUP BY a.SetoffToolType,a.CustomerId,CouponType.CouponTypeName");
            #endregion

            #region 海报
            totalCountSql.AppendFormat(@"      UNION ALL     SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               SetoffPoster.Name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN SetoffPoster ON SetoffPoster.SetoffPosterID=a.ObjectId
                          Where 1=1  AND SetoffToolType='SetoffPoster'");  //海报

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
            totalCountSql.AppendFormat(@"  GROUP BY a.SetoffToolType,a.CustomerId,SetoffPoster.Name ");
            #endregion

            #region 商品
            totalCountSql.AppendFormat(@"      UNION ALL      SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               T_Item.item_name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN T_Item ON T_Item.item_id=a.ObjectId
                          Where 1=1   AND SetoffToolType='SetoffPoster'
                        ");  //商品

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
            totalCountSql.AppendFormat(@"   GROUP BY a.SetoffToolType,a.CustomerId,T_Item.item_name");
            #endregion

            #region 任意仓库活动
            totalCountSql.AppendFormat(@"      UNION ALL     SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               T_CTW_LEvent.Name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN T_CTW_LEvent ON T_CTW_LEvent.CTWEventId=a.ObjectId
                               Where 1=1   AND SetoffToolType='CTW'
                              ");  //创意仓库活动

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
            totalCountSql.AppendFormat(@"    GROUP BY a.SetoffToolType,a.CustomerId,T_CTW_LEvent.Name");
            #endregion

            #endregion

            pagedSql.AppendFormat(@"
                             ) t
                        ) as h where RowNumber >" + PageSize * (PageIndex - 1) + " ");


            #endregion

            #region 执行,转换实体,分页属性赋值
            PagedQueryResult<Agg_SetoffForToolEntity> result = new PagedQueryResult<Agg_SetoffForToolEntity>();
            List<Agg_SetoffForToolEntity> list = new List<Agg_SetoffForToolEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    Agg_SetoffForToolEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            var ds = this.SQLHelper.ExecuteDataset(totalCountSql.ToString());
            int totalCount = 0;
            if (ds != null && ds.Tables.Count > 0)
            {
                totalCount = ds.Tables[0].Rows.Count;   //计算总行数
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
