/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 11:40:35
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
    /// ��VipAmountDetail�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipAmountDetailDAO : BaseCPOSDAO, ICRUDable<VipAmountDetailEntity>, IQueryable<VipAmountDetailEntity>
    {
        /// <summary>
        /// ��ȡ����ʹ�õ����֧��/�����ҵֿ۽�� update by Henry 2014-10-13
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <param name="AmountSourceId"></param>
        /// <returns></returns>
        public decimal GetVipAmountByOrderId(string orderId, string userId, int amountSourceId)
        {
            var sql = new StringBuilder();
            sql.Append("select isnull(Amount,0) as Amount from VipAmountDetail ");
            sql.Append("where ObjectId = @pOrderId and VipId = @pVipId ");
            sql.Append(" and AmountSourceId=@AmountSourceId "); //�������֧�� add by Henry 2014-10-13

            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pOrderId", Value = orderId},
                new SqlParameter() {ParameterName = "@pVipId", Value = userId},
                new SqlParameter() {ParameterName = "@AmountSourceId", Value = amountSourceId}
            };

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }

        }

        /// <summary>
        /// ��ȡ���/���ֱ����ϸ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VipAmountDetailEntity> GetVipAmountDetailList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
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
                pagedSql.AppendFormat(" [VipAmountDetailId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,a.*,b.AmountSourceName,isnull(c.ImageURL,'') ImageURL from VipAmountDetail a ");
            pagedSql.AppendFormat(" inner join SysAmountSource b on a.AmountSourceId=b.AmountSourceId ");
            pagedSql.AppendFormat(" left join ObjectImages c on c.ObjectID=cast(a.VipAmountDetailID as varchar(50)) ");
            pagedSql.AppendFormat(" where 1=1  and a.isdelete=0 ");

            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from VipAmountDetail a ");
            totalCountSql.AppendFormat(" inner join SysAmountSource b on a.AmountSourceId=b.AmountSourceId ");
            totalCountSql.AppendFormat(" left join ObjectImages c on c.ObjectID=cast(a.VipAmountDetailID as varchar(50)) ");
            totalCountSql.AppendFormat(" where 1=1  and a.isdelete=0 ");
            //��������
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
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<VipAmountDetailEntity> result = new PagedQueryResult<VipAmountDetailEntity>();
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipAmountDetailEntity m;
                    this.Load(rdr, out m);

                    if (rdr["AmountSourceName"] != DBNull.Value)
                        m.AmountSourceName = rdr["AmountSourceName"].ToString();
                    if (rdr["ImageURL"] != DBNull.Value)
                        m.ImageUrl = rdr["ImageURL"].ToString();
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }

        /// <summary>
        /// ��ȡ���/���ֱ����ϸ   {ʵ��˼·��ʹ�� Union ALL �� ��� ������ ��ϲ�Ϊһ����ʱ�� Ȼ���ٷ�ҳ ɸѡ}
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VipAmountDetailEntity> GetVipAmountDetailAndWithdrawList(IWhereCondition[] pWhereConditions, string VipId, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();

            pagedSql.Append(@"SELECT * INTO #Temp  FROM ( 
            SELECT * FROM ( ");

            #region ��ȡ��������б���Ϣ
            pagedSql.Append(@" SELECT vad.VipAmountDetailId  AS Id,CustomerId, vad.Amount AS Amount,vad.AmountSourceId AS AmountSourceId,sas.AmountSourceName  AS Reason,TableType='VipAmountDetail',");
            pagedSql.Append(" CAST(vad.CreateTime AS DATE) AS CreateTime FROM   VipAmountDetail AS vad ");
            pagedSql.Append(" left join SysAmountSource AS sas on sas.AmountSourceID=vad.AmountSourceId  ");
            pagedSql.Append("WHERE 1=1 AND VipId='" + VipId + "' ) AS P WHERE 1=1 ");
            foreach (var item in pWhereConditions)
            {
                pagedSql.Append(" AND " + item.GetExpression());
            }
            #endregion

            #region ��ȡ���ּ�¼��Ϣ
            pagedSql.Append(" UNION ALL ");
            pagedSql.Append(" SELECT * FROM (  SELECT vwda.ApplyID  AS Id,CustomerId,-vwda.Amount   AS Amount,'-1' AS AmountSourceId,ISNULL(vwda.Remark,'����') AS Reason,TableType='VipWithdrawDepositApply',CAST(vwda.ApplyDate AS DATE) AS CreateTime FROM   VipWithdrawDepositApply AS vwda");
            pagedSql.Append(" WHERE 1=1 AND VipId='" + VipId + "'  AND [Status]=3 ) AS M WHERE 1=1 ");

            foreach (var item in pWhereConditions)
            {
                pagedSql.Append(" AND " + item.GetExpression());
            }
            #endregion

            #region ��ʱ���ҳ����+��ȡ��ҳ��
            pagedSql.Append("  ) AS T SELECT * FROM ( SELECT Id,CustomerId,AmountSourceId,Amount,Reason,TableType,CreateTime,ROW_NUMBER() OVER(ORDER BY ");
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
                pagedSql.AppendFormat(" [CreateTime] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.Append(" ) AS RowNumber FROM #Temp ) AS tem WHERE tem.RowNumber>" + (pCurrentPageIndex) * pPageSize + " AND tem.RowNumber<= " + (pCurrentPageIndex + 1) * pPageSize);
            foreach (var item in pWhereConditions)
            {
                pagedSql.Append(" AND " + item.GetExpression());
            }
            pagedSql.Append("  SELECT COUNT(*) FROM #Temp WHERE 1=1 ");
            foreach (var item in pWhereConditions)
            {
                pagedSql.Append(" AND " + item.GetExpression());
            }
            pagedSql.Append("   DROP TABLE #Temp");
            #endregion

            //ִ����䲢���ؽ��
            PagedQueryResult<VipAmountDetailEntity> result = new PagedQueryResult<VipAmountDetailEntity>();
            List<VipAmountDetailEntity> list = new List<VipAmountDetailEntity>();
            DataSet ds = this.SQLHelper.ExecuteDataset(pagedSql.ToString());
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                VipAmountDetailEntity m = new VipAmountDetailEntity();
                if (row["Amount"] != DBNull.Value) { m.Amount = Convert.ToDecimal(row["Amount"]); }
                if (row["Reason"] != DBNull.Value) { m.Reason = Convert.ToString(row["Reason"]); }
                if (row["CreateTime"] != DBNull.Value) { m.CreateTime = Convert.ToDateTime(row["CreateTime"]); }
                list.Add(m);
            }
            result.Entities = list.ToArray();
            int totalCount = 0;
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0) { totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]); }
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        }
    }
}
