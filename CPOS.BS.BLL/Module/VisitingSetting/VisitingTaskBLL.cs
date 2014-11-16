/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/27 11:59:32
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

using JIT.Utility.Reflection;
using JIT.CPOS.BS.DataAccess.Base;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// VisitingTaskBLL 
    /// </summary>
    public partial class VisitingTaskBLL
    {
        #region GetList
        /// <summary>
        /// 获取拜访任务列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingTaskViewEntity[] GetList(VisitingTaskViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && entity.ClientPositionID != null && Convert.ToInt32(entity.ClientPositionID) > 0)
            {
                wheres.Add(new EqualsCondition() { FieldName = "ClientPositionID", Value = entity.ClientPositionID });
            }

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            PagedQueryResult<VisitingTaskViewEntity> pEntity = new VisitingTaskDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion
        
        #region EditTask
        /// <summary>
        /// 编辑拜访任务
        /// </summary>
        /// <param name="entity"></param>
        public void EditTask(VisitingTaskEntity entity)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VisitingTaskEntity oldEntity = this._currentDAO.GetByID(entity.VisitingTaskID);
                    if (oldEntity.POPType == entity.POPType)
                    {
                        this._currentDAO.Update(entity, tran);
                    }
                    else
                    {
                        new VisitingPOPMappingDAO(CurrentUserInfo).DeleteVisitingPOPAll(entity.VisitingTaskID.Value, tran);
                        this._currentDAO.Update(entity, tran);
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region DeleteTask
        /// <summary>
        /// 删除拜访任务
        /// </summary>
        /// <param name="ids"></param>
        public void DeleteTask(Guid id)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    this._currentDAO.DeleteTask(id, tran);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region GetTaskPOP_SearchConditions
        public POPGroupEntity GetTaskPOP_SearchConditions(Guid taskid)
        {
            return new POPGroupBLL(CurrentUserInfo).GetPOPGroupByTaskID(taskid);
        }
        #endregion
        #region EditTaskPOP_SearchConditions
        public void EditTaskPOP_SearchConditions(Guid taskid, List<DefindControlEntity> pSearch,string conditions,int isAutoFill)
        {
            VisitingTaskEntity taskEntity = new VisitingTaskBLL(CurrentUserInfo).GetByID(taskid);

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    POPGroupEntity entity = new POPGroupBLL(CurrentUserInfo).GetPOPGroupByTaskID(taskid);
                    if (entity != null)
                    {//修改
                        entity.SqlTemplate = GetPOPSqlTemplate(pSearch, taskEntity.POPType.Value, taskid);
                        entity.GroupCondition = conditions;
                        entity.IsAutoFill = isAutoFill;
                        entity.POPType = taskEntity.POPType;
                        new POPGroupBLL(CurrentUserInfo).Update(entity, tran);
                    }
                    else
                    {//新增
                        entity = new POPGroupEntity();
                        entity.SqlTemplate = GetPOPSqlTemplate(pSearch, taskEntity.POPType.Value, taskid);
                        entity.GroupCondition = conditions;
                        entity.IsAutoFill = isAutoFill;
                        entity.POPType = taskEntity.POPType;

                        entity.ClientID = CurrentUserInfo.ClientID;
                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        new POPGroupBLL(CurrentUserInfo).Create(entity, tran);
                    }

                    taskEntity.POPGroupID = entity.POPGroupID;
                    new VisitingTaskBLL(CurrentUserInfo).Update(taskEntity, tran);


                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public string GetPOPSqlTemplate(List<DefindControlEntity> pSearch, int popType, Guid taskid)
        {
            string sql = "";
            if (popType == 1)
            {
                sql = new VisitingPOPMappingBLL_Store(CurrentUserInfo, "Store").GetTaskPOPGroupSqlTemplate(pSearch, taskid.ToString());
            }
            else if (popType == 2)
            {
                //TODO:Visiting 经销商sql = new VisitingPOPMappingBLL_Distributor(CurrentUserInfo, "Distributor").GetTaskPOPGroupSqlTemplate(pSearch, taskid.ToString());
            }
            return sql;
        }
        #endregion
        
    }
}