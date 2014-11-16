/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/12 11:07:30
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Reflection;


namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// 业务处理： 拜访对象(门店/经销商) 
    /// </summary>
    public partial class VisitingPOPMappingBLL_Distributor : DistributorModuleCRUDBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new VisitingPOPMappingDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public VisitingPOPMappingBLL_Distributor(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new VisitingPOPMappingDAO(pUserInfo);
        }
        #endregion

        #region GetTaskDistributorList
        /// <summary>
        /// 获取拜访任务经销商列表
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">页码</param>
        /// <param name="taskid">拜访任务ID</param>
        /// <returns></returns>
        public PageResultEntity GetTaskDistributorList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string taskid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());
            sql.AppendLine("select ");
            sql.Append(GetDistributorGridFildSQL()); //获取字SQL

            //new
            sql.AppendFormat(" VPOPM.MappingID,cast('{0}' as uniqueidentifier) as VisitingTaskID,VPOPM.POPID, ", taskid);

            sql.AppendLine("ROW_NUMBER() OVER( order by case when VPOPM.MappingID is null then 1 else 0 end asc,main.LastUpdateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.DistributorID into #outTemp");
            sql.AppendLine("from Distributor main");
            sql.Append(GetDistributorLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetDistributorSearchJoinSQL(pSearch)); //获取条件联接SQL

            //new 这里不需要渠道查询条件
            sql.AppendFormat(" left join VisitingPOPMapping VPOPM on cast(main.DistributorID as nvarchar(20))=VPOPM.POPID and VPOPM.isdelete=0 and VPOPM.VisitingTaskID='{0}' and VPOPM.ClientID={1} ",
                taskid,
                CurrentUserInfo.ClientID);

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0} ", 
                base._pUserInfo.ClientID));
            sql.Append(GetDistributorGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            sql.Append(base.GetDropTempSql()); //需要删除的临时表
            return base.GetPageData(sql.ToString());
        }
        #endregion
        #region EditTaskPOP_Distributor
        /// <summary>
        /// 拜访任务选择经销商
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="taskid">拜访任务ID</param>
        /// <param name="allSelectorStatus">选择状态</param>
        /// <param name="defaultList">默认list</param>
        /// <param name="includeList">选择list</param>
        /// <param name="excludeList">排除list</param>
        public void EditTaskPOP_Distributor(int isAutoFill, List<DefindControlEntity> pSearch, string conditions, Guid taskid, int allSelectorStatus, string defaultList, string includeList, string excludeList)
        {
            VisitingTaskEntity taskEntity = new VisitingTaskBLL(CurrentUserInfo).GetByID(taskid);

            List<VisitingPOPMappingViewEntity> oldList = DataLoader.LoadFrom<VisitingPOPMappingViewEntity>(this.GetTaskDistributorList(pSearch, int.MaxValue, 0, taskid.ToString()).GridData).ToList();
            
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    #region 条件信息设置
                    bool changeCondition = false;//如果设置条件修改了，那么清空POP表数据
                    POPGroupEntity entity = new POPGroupBLL(CurrentUserInfo).GetPOPGroupByTaskID(taskid);
                    if (entity != null)
                    {//修改
                        if (entity.GroupCondition != conditions)
                        {
                            changeCondition = true;
                        }
                        entity.SqlTemplate = new VisitingTaskBLL(CurrentUserInfo).GetPOPSqlTemplate(pSearch, taskEntity.POPType.Value, taskid);
                        entity.GroupCondition = conditions;
                        entity.IsAutoFill = isAutoFill;
                        entity.POPType = taskEntity.POPType;
                        new POPGroupBLL(CurrentUserInfo).Update(entity, tran);
                    }
                    else
                    {//新增
                        changeCondition = true;
                        entity = new POPGroupEntity();
                        entity.SqlTemplate = new VisitingTaskBLL(CurrentUserInfo).GetPOPSqlTemplate(pSearch, taskEntity.POPType.Value, taskid);
                        entity.GroupCondition = conditions;
                        entity.IsAutoFill = isAutoFill;
                        entity.POPType = taskEntity.POPType;

                        entity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        new POPGroupBLL(CurrentUserInfo).Create(entity, tran);                        
                    }

                    taskEntity.POPGroupID = entity.POPGroupID;
                    new VisitingTaskBLL(CurrentUserInfo).Update(taskEntity, tran);
                    #endregion

                    #region 添加终端信息
                    if (isAutoFill == 1)
                    {
                        #region 添加目前符合条件所有终端
                        //删除
                        if (changeCondition)
                        {
                            //删除所有，新增所有
                            this._currentDAO.DeleteVisitingPOPAll(taskid, tran);

                            this._currentDAO.CreatePOP_DistributorList(
                                taskid,
                                oldList
                                .Select(m => m.DistributorID.Value.ToString()).ToArray().ToJoinString(","),
                                tran);
                        }
                        else
                        {
                            //新增数据库没有的
                            this._currentDAO.CreatePOP_DistributorList(
                                taskid,
                                 oldList
                                .Where(m => !m.MappingID.HasValue).ToArray()
                                .Select(m => m.DistributorID.Value.ToString()).ToArray().ToJoinString(","),
                                tran);
                        }
                        #endregion
                    }
                    else
                    {
                        #region 点选终端
                        //查询出数据库中已选择的产品
                        IEnumerable<string> selectedIDS = oldList.Where(x => x.MappingID != null).Select(x => x.DistributorID.Value.ToString());
                        //删除ID集合
                        IEnumerable<string> deletedList = null;
                        //新增ID集合
                        IEnumerable<string> createList = null;
                        //全选默认状态(没有点全选checkbox)
                        if (allSelectorStatus == 0)
                        {
                            //前端选择的产品ID数组
                            string[] frontSelectedIDS = includeList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                            deletedList = selectedIDS.Except(frontSelectedIDS);
                            createList = frontSelectedIDS.Except(selectedIDS);
                        }

                        //点了全选
                        else if (allSelectorStatus == 1)
                        {
                            //排除的ID数组
                            string[] excludeIDS = excludeList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            //取交集，得出需要删除ID集合
                            deletedList = selectedIDS.Intersect(excludeIDS);
                            //取差集，得出需要新增ID集合
                            createList = oldList.Where(x => x.MappingID == null).Select(x => x.DistributorID.Value.ToString()).Except(excludeIDS);
                        }

                        //取消了全选
                        else if (allSelectorStatus == 2)
                        {
                            //选择的ID数组
                            string[] includeIDS = includeList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            //取差集，得出需要新增的ID集合
                            createList = includeIDS.Except(selectedIDS);
                            //取差集，得出需要删除的ID集合
                            deletedList = oldList.Where(x => x.MappingID != null).Select(x => x.DistributorID.Value.ToString()).Except(includeIDS);
                        }

                        #region 删除新增操作
                        //删除操作
                        if (deletedList != null && deletedList.Count() > 0)
                        {
                            this._currentDAO.DeleteVisitingPOPIn(taskid, string.Join(",", deletedList), tran);
                        }

                        //新增操作
                        if (createList != null && createList.Count() > 0)
                        {
                            this._currentDAO.CreatePOP_DistributorList(taskid, string.Join(",", createList), tran);
                        }
                        #endregion

                        #endregion
                    }
                    #endregion

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

        #region GetTaskPOPGroupSqlTemplate
        public string GetTaskPOPGroupSqlTemplate(List<DefindControlEntity> pSearch, string taskid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());

            sql.AppendLine("select main.DistributorID as POPID,2 as POPType");

            sql.AppendLine("from Distributor main");
            sql.Append(GetDistributorLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetDistributorSearchJoinSQL(pSearch)); //获取条件联接SQL

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0} ",
                base._pUserInfo.ClientID));
            sql.Append(GetDistributorGridSearchSQL(pSearch)); //获取条件

            sql.Append(base.GetDropTempSql()); //需要删除的临时表
            return sql.ToString();
        }
        #endregion
    }
}