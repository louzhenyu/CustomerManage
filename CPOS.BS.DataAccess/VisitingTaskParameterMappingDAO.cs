/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:20
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
    public partial class VisitingTaskParameterMappingDAO
    {
        #region GetStepParameterList
        /// <summary>
        /// 获取拜访步骤采集参数列表
        /// </summary>
        /// <param name="entity"> VisitingTaskStepID 为必传项</param>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VisitingParameterViewEntity> GetStepParameterList(VisitingParameterViewEntity entity,IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            StringBuilder sqlWhere = new StringBuilder();
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sqlWhere.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            StringBuilder sqlOrder = new StringBuilder();
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    sqlOrder.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sqlOrder.Remove(sqlOrder.Length - 1, 1);
            }

            //通用分页查询
            UtilityEntity model = new UtilityEntity();
            model.TableName = "(" + string.Format(SqlMap.SQL_GETSTEPPARALIST,
                entity.VisitingTaskStepID,
                CurrentUserInfo.ClientID,
                CurrentUserInfo.ClientDistributorID) + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<VisitingParameterViewEntity> pEntity = new PagedQueryResult<VisitingParameterViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VisitingParameterViewEntity>(model.PageDataSet.Tables[0]);
            }
            return pEntity;
        }
        #endregion

        #region DeleteStepParameterIn
        /// <summary>
        /// 删除关联表数据
        /// </summary>
        /// <param name="deleteMappingIds"></param>
        /// <param name="pTran"></param>
        public int DeleteStepParameterIn(string stepID, string parameterIds, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingTaskParameterMapping] set isdelete=1 where VisitingTaskStepID='{0}' and VisitingParameterID in ({1}) and ClientID='{2}' and ClientDistributorID={3} and isdelete=0", stepID, parameterIds, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
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
        #region DeleteStepParameterNotIn
        /// <summary>
        /// 删除关联表数据
        /// </summary>
        /// <param name="stepID"></param>
        /// <param name="parameterIds"></param>
        /// <param name="pTran"></param>
        public int DeleteStepParameterNotIn(string stepID, string parameterIds, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingTaskParameterMapping] set isdelete=1 where VisitingTaskStepID='{0}' and VisitingParameterID not in ({1}) and ClientID='{2}' and ClientDistributorID={3} and isdelete=0", stepID, parameterIds, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
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
        #region DeleteStepParameterAll
        public int DeleteStepParameterAll(string stepID, IDbTransaction pTran)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [VisitingTaskParameterMapping] set isdelete=1 where VisitingTaskStepID='{0}' and ClientID='{1}' and ClientDistributorID={2} and isdelete=0", stepID, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
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