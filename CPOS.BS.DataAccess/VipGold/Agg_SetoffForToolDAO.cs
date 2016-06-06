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
    /// ���ݷ��ʣ�  
    /// ��Agg_SetoffForTool�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class Agg_SetoffForToolDAO : Base.BaseCPOSDAO, ICRUDable<Agg_SetoffForToolEntity>, IQueryable<Agg_SetoffForToolEntity>
    {
        /// <summary>
        /// ��ȡ���������Ϣ ����ʱ�䡢�̻���� ��ȡ
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="DateCode">ͳ������</param>
        /// <param name="SetoffToolTypeId">���͹�������</param>
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
        /// ������Դ�б� ��ҳ
        /// </summary>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="PageSize">ÿҳ��ʾ����</param>
        /// <param name="PageIndex">ҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForToolEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {

            #region ��֯SQL
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
                pagedSql.AppendFormat(" a.OrderAmount desc"); //Ĭ��Ϊ����ֵ����
                totalCountSql.AppendFormat(" a.OrderAmount desc"); //Ĭ��Ϊ����ֵ����
            }

            pagedSql.AppendFormat(" ) AS RowNumber from (");

            pagedSql.AppendFormat(@" ");
            totalCountSql.AppendFormat(@" ");

            #region ��ѯSQL���
            #region �Ż�ȯ
            pagedSql.AppendFormat(@"           SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               CouponType.CouponTypeName AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN CouponType ON CouponType.CouponTypeID=a.ObjectId
                          Where 1=1  AND SetoffToolType='Coupon'");   //�Ż�ȯ


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

            #region ����
            pagedSql.AppendFormat(@"      UNION ALL     SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               SetoffPoster.Name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN SetoffPoster ON SetoffPoster.SetoffPosterID=a.ObjectId
                          Where 1=1  AND SetoffToolType='SetoffPoster'");  //����

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

            #region ��Ʒ
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
                        ");  //��Ʒ

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

            #region ����ֿ�
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
                              ");  //����ֿ�

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


            #region ��ҳSQL���
            #region �Ż�ȯ
            totalCountSql.AppendFormat(@"           SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               CouponType.CouponTypeName AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN CouponType ON CouponType.CouponTypeID=a.ObjectId
                          Where 1=1  AND SetoffToolType='Coupon'");   //�Ż�ȯ


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

            #region ����
            totalCountSql.AppendFormat(@"      UNION ALL     SELECT
                               sum(a.SetoffCount) as 'SetoffCount' ,
                               sum(a.ShareCount) as 'ShareCount',
                               sum(a.OrderAmount) AS 'OrderAmount',
                               a.SetoffToolType,
                               a.CustomerId,
                               SetoffPoster.Name AS 'ObjectName'
                               from Agg_SetoffForTool as a  
                               LEFT JOIN SetoffPoster ON SetoffPoster.SetoffPosterID=a.ObjectId
                          Where 1=1  AND SetoffToolType='SetoffPoster'");  //����

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

            #region ��Ʒ
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
                        ");  //��Ʒ

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

            #region ����ֿ�
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
                              ");  //����ֿ�

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

            #region ִ��,ת��ʵ��,��ҳ���Ը�ֵ
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
                totalCount = ds.Tables[0].Rows.Count;   //����������
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
