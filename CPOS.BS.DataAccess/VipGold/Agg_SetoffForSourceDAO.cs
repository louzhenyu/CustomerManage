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
    /// ���ݷ��ʣ�  
    /// ��Agg_SetoffForSource�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class Agg_SetoffForSourceDAO : Base.BaseCPOSDAO, ICRUDable<Agg_SetoffForSourceEntity>, IQueryable<Agg_SetoffForSourceEntity>
    {
        /// <summary>
        /// ��ȡ���������Ϣ ����ʱ�䡢�̻���� ��ȡ
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
        /// ������Դ�б� ��ҳ
        /// </summary>
        /// <param name="pWhereConditions">��������</param>
        /// <param name="pOrderBys">��������</param>
        /// <param name="PageSize">ÿҳ��ʾ����</param>
        /// <param name="PageIndex">ҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<Agg_SetoffForSourceEntity> FindAllByPage(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageSize, int PageIndex)
        {
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();


            #region ��ѯSQL
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

            #region ��ҳSQL
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

            #region ��ѯSQL
            pagedSql.AppendFormat(@"  select h.SetoffRole
                            ,h.CustomerId,h.UserId
                            ,sum(h.SetoffCount) as 'SetoffCount' --��������
                            ,sum(h.ShareCount) as 'ShareCount' --�������
                            ,sum(h.OrderAmount) AS 'OrderAmount' --����,
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
                            ,sum(h.SetoffCount) as 'SetoffCount' --��������
                            ,sum(h.ShareCount) as 'ShareCount' --�������
                            ,sum(h.OrderAmount) AS 'OrderAmount' --����,
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

            #region ��ҳSQL
            totalCountSql.AppendFormat(@"  select h.SetoffRole
                            ,h.CustomerId,h.UserId
                            ,sum(h.SetoffCount) as 'SetoffCount' --��������
                            ,sum(h.ShareCount) as 'ShareCount' --�������
                            ,sum(h.OrderAmount) AS 'OrderAmount' --����,
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
                            ,sum(h.SetoffCount) as 'SetoffCount' --��������
                            ,sum(h.ShareCount) as 'ShareCount' --�������
                            ,sum(h.OrderAmount) AS 'OrderAmount' --����,
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

            #region ִ��,ת��ʵ��,��ҳ���Ը�ֵ
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
                totalCount = Convert.ToInt32(ds.Tables[0].Rows.Count);    //����������
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
