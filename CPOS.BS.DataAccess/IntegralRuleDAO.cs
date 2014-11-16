/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
    /// ���ݷ��ʣ�  
    /// ��IntegralRule�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class IntegralRuleDAO : Base.BaseCPOSDAO, ICRUDable<IntegralRuleEntity>, IQueryable<IntegralRuleEntity>
    {
        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListCount(IntegralRuleEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetList(IntegralRuleEntity entity, int Page, int PageSize)
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
        private string GetListSql(IntegralRuleEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by b.TypeCode ,a.IntegralSourceID ) ";
            sql += " ,b.IntegralSourceName ";
            sql += ",case b.typecode when '1' then '��Ա' "
                + " when '2' then '����Ա' "
                + " when '3' then '�ŵ�' "
                + " else '' "
                + " end TypeCodeDesc ";
            sql += " into #tmp ";
            sql += " from IntegralRule a ";
            sql += " inner join SysIntegralSource b on a.IntegralSourceID=b.IntegralSourceID ";
            sql += " where a.IsDelete='0' and b.IsDelete = '0' and b.TypeCode is not null and b.typeCode <> '' and a.customerId = '" + this.CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.IntegralRuleID != null && entity.IntegralRuleID.Trim().Length > 0)
            {
                sql += " and a.IntegralRuleID = '" + entity.IntegralRuleID + "' ";
            }
            if (entity.IntegralSourceID != null && entity.IntegralSourceID.Trim().Length > 0)
            {
                sql += " and a.IntegralSourceID = '" + entity.IntegralSourceID + "' ";
            }
            if (entity.Integral != null && entity.Integral.Trim().Length > 0)
            {
                sql += " and a.Integral like '%" + entity.Integral + "%' ";
            }
            sql += " order by b.TypeCode ,a.IntegralSourceID ";
            return sql;
        }
        #endregion
    }
}
