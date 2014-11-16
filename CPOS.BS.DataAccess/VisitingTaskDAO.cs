/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/7 18:31:26
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
    public partial class VisitingTaskDAO
    {
        #region GetList
        public PagedQueryResult<VisitingTaskViewEntity> GetList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
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
            model.TableName = "(" + string.Format(SqlMap.SQL_GETTASKLIST, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID)
                + sqlWhere.ToString() + ") as A";
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;
            model.PageSort = sqlOrder.ToString();
            new UtilityDAO(this.CurrentUserInfo).PagedQuery(model);

            //返回值
            PagedQueryResult<VisitingTaskViewEntity> pEntity = new PagedQueryResult<VisitingTaskViewEntity>();
            pEntity.RowCount = model.PageTotal;
            if (model.PageDataSet != null
                && model.PageDataSet.Tables != null
                && model.PageDataSet.Tables.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VisitingTaskViewEntity>(model.PageDataSet.Tables[0], new DirectPropertyNameMapping());
            }
            return pEntity;
        }
        #endregion

        #region DeleteTask
        public void DeleteTask(Guid id, IDbTransaction pTran)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
--拜访任务
update VisitingTask set IsDelete=1 where VisitingTaskID='{0}' 
and ClientID='{1}' and ClientDistributorID={2}
update VisitingPOPMapping set IsDelete=1 where VisitingTaskID='{0}' 
and ClientID='{1}' and ClientDistributorID={2}

--拜访步骤
update VisitingTaskStep set IsDelete=1 where VisitingTaskID='{0}' 
and ClientID='{1}' and ClientDistributorID={2}

update VisitingTaskStepObject set IsDelete=1 where VisitingTaskStepID in(
select VisitingTaskStepID from VisitingTaskStep where VisitingTaskID='{0}' 
and ClientID='{1}' and ClientDistributorID={2}
) and ClientID='{1}' and ClientDistributorID={2}

update VisitingTaskParameterMapping set IsDelete=1 where VisitingTaskStepID in(
select VisitingTaskStepID from VisitingTaskStep where VisitingTaskID='{0}' 
and ClientID='{1}' and ClientDistributorID={2}
) and ClientID='{1}' and ClientDistributorID={2}
            ");
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text,
                    string.Format(sql.ToString(), id.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID));
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(sql.ToString(), id.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID));
            }
        }
        #endregion
    }
}
