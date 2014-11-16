/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/4 19:48:46
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
    /// StepBLL 
    /// </summary>
    public partial class VisitingTaskStepBLL
    {
        #region GetList
        /// <summary>
        /// 获取拜访步骤列表
        /// </summary>
        /// <param name="entity">taskid 为必传项</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingTaskStepEntity[] GetList(VisitingTaskStepViewEntity entity,int pageIndex,int pageSize,out int rowCount)
        {
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "StepPriority", Direction = OrderByDirections.Asc });

            PagedQueryResult<VisitingTaskStepViewEntity> pEntity = new VisitingTaskStepDAO(this.CurrentUserInfo).GetList(entity, null, orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region EditStep
        /// <summary>
        /// 编辑拜访步骤
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>2成功，1已经存在订单相关步骤</returns>
        public int EditStep(VisitingTaskStepEntity entity,string ObjectGroup)
        {
            int res = 2;
            Guid? visitingTaskStepID = entity.VisitingTaskStepID;
            VisitingTaskStepObjectEntity objectEntity =null;
            if (visitingTaskStepID != null)
            {
                objectEntity = new VisitingTaskStepObjectBLL(CurrentUserInfo).GetStepObjectList(visitingTaskStepID.Value);
            }
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    /* 1.一个拜访任务只能有一个拜访步骤可以为订单相关
                     * 2.当拜访任务为自定义对象(7)时，将选择的对象分组插入 拜访步骤对象关联表 VisitingTaskStepObject
                     */
                    #region 添加拜访步骤
                    VisitingTaskStepEntity oldEntity = this._currentDAO.GetByID(entity.VisitingTaskStepID);
                    if (visitingTaskStepID != null
                        && !string.IsNullOrEmpty(visitingTaskStepID.ToString()))
                    {//修改
                        if (oldEntity.StepType == entity.StepType)
                        {
                            this._currentDAO.Update(entity, tran);
                        }
                        else
                        {
                            //判断是否已经存在订单相关的步骤
                            if (entity.StepType == 6)
                            {
                                VisitingTaskStepViewEntity viewEntity = new VisitingTaskStepViewEntity();
                                viewEntity.VisitingTaskID = entity.VisitingTaskID;
                                VisitingTaskStepViewEntity[] pEntityList = this._currentDAO.GetList(viewEntity, null, null, 1, 100000).Entities;

                                if (pEntityList.Where(m => m.StepType == 6).ToArray().Length > 0)
                                {
                                    res = 1;
                                    return res;
                                }
                            }
                            //清空关联数据
                            new VisitingTaskStepObjectDAO(CurrentUserInfo).DeleteStepObjectAll(entity.VisitingTaskStepID.ToString(), tran);
                            this._currentDAO.Update(entity, tran);
                        }
                    }
                    else
                    {//添加
                        //判断是否已经存在订单相关的步骤
                        if (entity.StepType == 6)
                        {
                            VisitingTaskStepViewEntity viewEntity = new VisitingTaskStepViewEntity();
                            viewEntity.VisitingTaskID = entity.VisitingTaskID;
                            VisitingTaskStepViewEntity[] pEntityList = this._currentDAO.GetList(viewEntity, null, null, 1, 100000).Entities;

                            if (pEntityList.Where(m => m.StepType == 6).ToArray().Length > 0)
                            {
                                res = 1;
                                return res;
                            }
                        }
                        this._currentDAO.Create(entity, tran);

                        res = 2;
                    }
                    #endregion

                    #region 添加自定义对象，分组类型
                    if (entity.StepType == 7)
                    {
                        if (visitingTaskStepID != null
                        && !string.IsNullOrEmpty(visitingTaskStepID.ToString()))
                        {//修改

                            if (objectEntity == null)
                            {
                                VisitingTaskStepObjectEntity objEntity = new VisitingTaskStepObjectEntity();
                                objEntity.VisitingTaskStepID = entity.VisitingTaskStepID;
                                objEntity.Target1ID = ObjectGroup;
                                objEntity.ClientID = CurrentUserInfo.ClientID;
                                objEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(objEntity, tran);
                            }
                            else
                            {
                                objectEntity.Target1ID = ObjectGroup;
                                new VisitingTaskStepObjectBLL(CurrentUserInfo).Update(objectEntity, tran);
                            }
                        }
                        else
                        {//添加
                            VisitingTaskStepObjectEntity objEntity = new VisitingTaskStepObjectEntity();
                            objEntity.VisitingTaskStepID = entity.VisitingTaskStepID;
                            objEntity.Target1ID = ObjectGroup;
                            objEntity.ClientID = CurrentUserInfo.ClientID;
                            objEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(objEntity, tran);
                        }
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
            return res;
        }
        #endregion

        #region DeleteStep
        /// <summary>
        /// 删除拜访步骤
        /// </summary>
        /// <param name="ids"></param>
        public void DeleteStep(string ids)
        {
            IDbTransaction tran =  new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    new VisitingTaskStepDAO(this.CurrentUserInfo).DeleteStep(ids, tran);

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
    }
}