/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/11 17:00:10
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
    /// ��LEventsEntriesComment�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LEventsEntriesCommentDAO : Base.BaseCPOSDAO, ICRUDable<LEventsEntriesCommentEntity>, IQueryable<LEventsEntriesCommentEntity>
    {
        #region ��ȡ�����б�
        /// <summary>
        /// ��ȡ�����б�����
        /// </summary>
        /// <param name="entriesId">��ƷID</param>
        /// <param name="date">��ѯ����</param>
        /// <returns></returns>
        public int GetCommentCount(string entriesId, string date, string IsCrowdDaren)
        {
            string sql = GetCommentSql(entriesId, date, IsCrowdDaren);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        public DataSet GetCommentList(string entriesId, string date, string IsCrowdDaren, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetCommentSql(entriesId, date, IsCrowdDaren);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetCommentSql(string entriesId, string date, string IsCrowdDaren)
        {
            string sql = string.Empty;
            sql += " SELECT a.*, b.UserName, b.Phone ";
            sql += " , displayindex = row_number() over(order by a.CreateTime DESC) ";
            sql += " into #tmp ";
            sql += " FROM dbo.LEventsEntriesComment a ";
            sql += " INNER JOIN dbo.LEventSignUp b ON a.SignUpID = b.SignUpID ";
            sql += " WHERE a.IsDelete = 0 ";

            if (!string.IsNullOrEmpty(entriesId))
            {
                sql += " AND a.EntriesId = '" + entriesId + "' ";
            }

            if (!string.IsNullOrEmpty(IsCrowdDaren))
            {
                sql += " AND a.IsCrowdDaren = '" + IsCrowdDaren + "' ";
            }
            if (!string.IsNullOrEmpty(date))
            {
                sql += " AND CONVERT(VARCHAR(10), a.CreateTime, 120) = '" + date + "' ";
            }

            return sql;
        }
        #endregion
    }
}
