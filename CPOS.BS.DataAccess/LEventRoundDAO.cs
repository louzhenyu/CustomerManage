/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/29 10:14:39
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
    /// ��LEventRound�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LEventRoundDAO : Base.BaseCPOSDAO, ICRUDable<LEventRoundEntity>, IQueryable<LEventRoundEntity>
    {

        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListCount(LEventRoundEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetList(LEventRoundEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndexLast between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(LEventRoundEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.Round asc) ";
            sql += " ,(case when RoundStatus=10 then 'δ��ʼ' when RoundStatus=20 then '������' when RoundStatus=30 then '��ͣ' when RoundStatus=40 then '����' else '' end) RoundStatusName ";
            sql += " into #tmp ";
            sql += " from [LEventRound] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            if (entity.EventId != null && entity.EventId.Length > 0)
            {
                sql += " and a.EventId = '" + entity.EventId + "' ";
            }
            return sql;
        }

        public int PrizePoolSetting(string eventId)
        {
            string sql = string.Empty;
            sql = " exec PrizePoolSetting '" + eventId + "','" + CurrentUserInfo.CurrentUser.User_Id + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion
    }
}
