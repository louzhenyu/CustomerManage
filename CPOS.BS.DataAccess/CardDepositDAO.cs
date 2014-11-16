/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/9 15:19:33
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
    /// 数据访问：  
    /// 表CardDeposit的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CardDepositDAO : Base.BaseCPOSDAO, ICRUDable<CardDepositEntity>, IQueryable<CardDepositEntity>
    {

        public string BulkInsertCard(string channelID, decimal amount, decimal bonus, int qty, string userID, string clientID, DataTable dataTable)
        {
            List<SqlParameter> lsp = new List<SqlParameter>();
            lsp.Add(new SqlParameter("ChannelID", channelID));
            lsp.Add(new SqlParameter("Amount", amount));
            lsp.Add(new SqlParameter("Bonus", bonus));
            lsp.Add(new SqlParameter("Qty", qty));
            lsp.Add(new SqlParameter("UserID", userID));
            lsp.Add(new SqlParameter("CustomerID", clientID));
            lsp.Add(new SqlParameter("Password", SqlDbType.Structured) { Value = dataTable });

            return this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "CardBulkInsert", lsp.ToArray()).ToString();
        }

        public DataSet PagedSearch(string channelID, string cardNoStart, string cardNoEnd, string cardStatus, string useStatus, string amount, string dateRange, DateTime? createTimeStart, DateTime? createTimeEnd, string customerID, int pageIndex, int pageSize, string cardNo = null)
        {
            List<SqlParameter> lsp = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(channelID))
                lsp.Add(new SqlParameter("ChannelID", channelID));

            if (!string.IsNullOrEmpty(cardNoStart))
                lsp.Add(new SqlParameter("CardNoStart", cardNoStart));

            if (!string.IsNullOrEmpty(cardNoEnd))
                lsp.Add(new SqlParameter("CardNoEnd", cardNoEnd));

            if (!string.IsNullOrEmpty(cardStatus))
                lsp.Add(new SqlParameter("CardStatus", cardStatus));

            if (!string.IsNullOrEmpty(useStatus))
                lsp.Add(new SqlParameter("UseStatus", useStatus));

            if (!string.IsNullOrEmpty(amount))
                lsp.Add(new SqlParameter("Amount", amount));

            if (!string.IsNullOrEmpty(dateRange))
                lsp.Add(new SqlParameter("DateRange", dateRange));

            if (createTimeStart.HasValue)
                lsp.Add(new SqlParameter("CreateTimeStart", createTimeStart));
            
            if (createTimeEnd.HasValue)
                lsp.Add(new SqlParameter("CreateTimeEnd", createTimeEnd));

            if (!string.IsNullOrEmpty(cardNo))
                lsp.Add(new SqlParameter("CardNo", cardNo));

            lsp.Add(new SqlParameter("CustomerID", customerID));
            lsp.Add(new SqlParameter("PageIndex", pageIndex));
            lsp.Add(new SqlParameter("PageSize", pageSize));

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "CardDepositPagedSearch", lsp.ToArray());
        }

        public int SetCardStatus(string cardIDs, int cardStatus, string userID, string customerID)
        {
            string sql="update CardDeposit set CardStatus = {0}, LastUpdateBy = '{1}', LastUpdateTime = '{2}' where CustomerID = '{3}' and CardDepositID in ({4})";
            sql = string.Format(sql, cardStatus, userID, DateTime.Now, customerID, cardIDs);
            return this.SQLHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public DataSet QueryDataSet(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select CardChannelInfo.ChannelTitle, CardDeposit.* from [CardDeposit] left join CardChannelInfo on CardDeposit.ChannelID = CardChannelInfo.ChannelID where CardDeposit.isdelete=0 and CardChannelInfo.isdelete = 0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //执行SQL
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            //返回结果
            return ds;
        }

        public int ActiveCard(string cardNo, string vipID, string userID, string customerID, SqlTransaction tran = null)
        {
            int result = 0;
            string sql = "update CardDeposit set UseStatus = 1, DepositTime = '{0}', LastUpdateBy = '{1}', LastUpdateTime = '{2}' where CustomerID = '{3}' and CardNo = '{4}' and UseStatus = 0";
            sql = string.Format(sql, DateTime.Now, userID, DateTime.Now, customerID, cardNo);
            
            Loggers.Debug(new DebugLogInfo() { Message = "激活充值卡：sql=" + sql });
            
            if (tran != null)
                result = this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
            else
                result = this.SQLHelper.ExecuteNonQuery(sql);

            return result;
        }

        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        public DataSet CardSummary(int pageSize, int pageIndex)
        {
            List<SqlParameter> lsp = new List<SqlParameter>();
            lsp.Add(new SqlParameter("PageSize", pageSize));
            lsp.Add(new SqlParameter("PageIndex", pageIndex));
            lsp.Add(new SqlParameter("CustomerID", this.CurrentUserInfo.ClientID));

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "CardSummary", lsp.ToArray());
        }

        public DataSet TransactionList(int pageSize, int pageIndex)
        {
            List<SqlParameter> lsp = new List<SqlParameter>();
            lsp.Add(new SqlParameter("PageSize", pageSize));
            lsp.Add(new SqlParameter("PageIndex", pageIndex));
            lsp.Add(new SqlParameter("CustomerID", this.CurrentUserInfo.ClientID));

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "TransactionList", lsp.ToArray());
        }
    }
}
