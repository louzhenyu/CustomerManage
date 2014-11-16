/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/7 10:56:10
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
    /// ��QuesOption�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class QuesOptionDAO : Base.BaseCPOSDAO, ICRUDable<QuesOptionEntity>, IQueryable<QuesOptionEntity>
    {
        #region ��̨Web��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetWebQuesOptionsCount(QuesOptionEntity entity)
        {
            string sql = GetWebQuesOptionsSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetWebQuesOptions(QuesOptionEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebQuesOptionsSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex desc ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetWebQuesOptionsSql(QuesOptionEntity entity)
        {
            string sql = string.Empty;
            sql = "SELECT a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex desc) ";
            sql += " into #tmp ";
            sql += " from QuesOption a ";
            sql += " where a.IsDelete='0' ";
            if (entity.QuestionID != null && entity.QuestionID.Trim().Length > 0)
            {
                sql += " and a.QuestionID = '" + entity.QuestionID + "' ";
            }
            sql += " order by a.DisplayIndex desc ";
            return sql;
        }
        #endregion
        
    }
}
