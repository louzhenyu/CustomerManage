/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/12 11:04:18
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 拜访对象(门店/经销商) 
    /// </summary>
    public partial class VisitingPOPMappingDAO 
    {
        #region CreatePOP_StoreList
        /// <summary>
        /// 批量插入门店数据
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="insertList"></param>
        /// <param name="tran"></param>
        public void CreatePOP_StoreList(Guid taskid, string insertList, IDbTransaction pTran)
        {
            if (insertList.Length == 0)
            {
                return;
            }

            //IEnumerable<string> storeids = insertList.Select(m => m.StoreID.Value.ToString().ToLower());

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
insert into [VisitingPOPMapping] (VisitingTaskID,POPID,ClientID,ClientDistributorID,CreateBy,CreateTime,IsDelete)
select '{1}',Lower(StoreID),'{2}',{3},'{4}',getdate(),0 from Store where storeid in ('{0}')
",
 insertList,
 taskid.ToString(),
 CurrentUserInfo.ClientID,
 CurrentUserInfo.ClientDistributorID,
 CurrentUserInfo.UserID);
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
        }
        #endregion

        #region CreatePOP_DistributorList
        /// <summary>
        /// 批量插入经销商数据
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="insertList"></param>
        /// <param name="tran"></param>
        public void CreatePOP_DistributorList(Guid taskid, string insertList, IDbTransaction pTran)
        {
            if (insertList.Length == 0)
            {
                return;
            }

            //IEnumerable<string> storeids = insertList.Select(m => m.StoreID.Value.ToString().ToLower());

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
insert into [VisitingPOPMapping] (VisitingTaskID,POPID,ClientID,ClientDistributorID,CreateBy,CreateTime,IsDelete)
select '{1}',DistributorID,'{2}',{3},'{4}',getdate(),0 from Distributor where DistributorID in ({0})
",
 insertList,
 taskid.ToString(),
 CurrentUserInfo.ClientID,
 CurrentUserInfo.ClientDistributorID,
 CurrentUserInfo.UserID);
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
        }
        #endregion


        #region DeleteVisitingPOPIn
        public int DeleteVisitingPOPIn(Guid taskID, string POPIds, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
 update [VisitingPOPMapping] set IsDelete=1 where [VisitingTaskID]='{0}' and POPID in ('{1}') and ClientID='{2}' and isdelete=0", taskID.ToString(), POPIds, CurrentUserInfo.ClientID);
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
            return result;
        }
        #endregion
        
        #region DeleteVisitingPOPAll
        public int DeleteVisitingPOPAll(Guid taskID, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingPOPMapping] set isdelete=1 where [VisitingTaskID]='{0}' and ClientID='{1}' and isdelete=0", taskID.ToString(), CurrentUserInfo.ClientID);
            if (pTran != null)
            {
                result = this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
            return result;
        }
        #endregion
    }
}