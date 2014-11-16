/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 自定义对象和拜访参数映射 
    /// 表VisitingObjectVisitingParameterMapping的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VisitingObjectVisitingParameterMappingDAO
    {
        #region GetObjectParameterList
        /// <summary>
        /// 获取拜访步骤采集参数列表
        /// </summary>
        /// <param name="entity"> VisitingTaskStepID 为必传项</param>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VisitingParameterViewEntity> GetObjectParameterList(VisitingParameterViewEntity entity, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            string sql = string.Format(@"
select A.*,
            B.MappingID,
            B.ParameterOrder ,
            B.VisitingObjectID,
(select OptionText 
            from Options 
            where OptionName='ParameterType' and OptionValue= A.ParameterType and IsDelete=0) as ParameterTypeText,
            (select OptionText 
            from Options 
            where OptionName='ControlType' and OptionValue= A.ControlType and IsDelete=0) as ControlTypeText
            from VisitingParameter A
  left join VisitingObjectVisitingParameterMapping B on A.VisitingParameterID=B.VisitingParameterID and B.IsDelete=0 and B.VisitingObjectID='{0}' and B.ClientID='{1}' and isnull(B.ClientDistributorID,0)={2}
            where A.IsDelete=0 and A.ClientID='{1}' and isnull(A.ClientDistributorID,0)={2}
", entity.VisitingObjectID,
 CurrentUserInfo.ClientID,
 CurrentUserInfo.ClientDistributorID
 );
            return new UtilityDAO(CurrentUserInfo).GetList<VisitingParameterViewEntity>(pWhereConditions, pOrderBys, sql, pageIndex, pageSize);
        }
        #endregion

        #region DeleteObjectParameterIn
        /// <summary>
        /// 删除关联表数据
        /// </summary>
        /// <param name="deleteMappingIds"></param>
        /// <param name="pTran"></param>
        public int DeleteObjectParameterIn(string objID, string parameterIds, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingObjectVisitingParameterMapping] set isdelete=1 where VisitingObjectID='{0}' and VisitingParameterID in ({1}) and ClientID='{2}' and isnull(ClientDistributorID,0)={3} and isdelete=0", objID, parameterIds, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
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
        #region DeleteObjectParameterNotIn
        /// <summary>
        /// 删除关联表数据
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="parameterIds"></param>
        /// <param name="pTran"></param>
        public int DeleteObjectParameterNotIn(string objID, string parameterIds, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingObjectVisitingParameterMapping] set isdelete=1 where VisitingObjectID='{0}' and VisitingParameterID not in ({1}) and ClientID='{2}' and isnull(ClientDistributorID,0)={3} and isdelete=0", objID, parameterIds, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
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
        #region DeleteObjectParameterAll
        public int DeleteObjectParameterAll(string objID, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingObjectVisitingParameterMapping] set isdelete=1 where VisitingObjectID='{0}' and ClientID='{1}' and isnull(ClientDistributorID,0)={2} and isdelete=0", objID, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
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
