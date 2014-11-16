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

namespace JIT.CPOS.BS.BLL
{   
    /// <summary>
    /// 业务处理： 拜访步骤参数映射 
    /// </summary>
    public partial class VisitingTaskParameterMappingBLL
    {
        #region GetStepParameterList
        /// <summary>
        /// 获取拜访步骤采集参数列表
        /// </summary>
        /// <param name="entity"> VisitingTaskStepID 为必传项</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingParameterViewEntity[] GetStepParameterList(VisitingParameterViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "case when ParameterOrder is null then 1 else 0 end ,ParameterOrder", Direction = OrderByDirections.Asc });

            PagedQueryResult<VisitingParameterViewEntity> pEntity = new VisitingTaskParameterMappingDAO(this.CurrentUserInfo).GetStepParameterList(entity, null, orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
            //return da.Query(wheres.ToArray(), orderbys.ToArray());
        }
        #endregion

        #region EditStepParameter
        /// <summary>
        /// 拜访步骤选择拜访参数
        /// </summary>
        /// <param name="stepid">拜访步骤ID</param>
        /// <param name="allSelectorStatus">选择状态</param>
        /// <param name="defaultList">默认list</param>
        /// <param name="includeList">选择list</param>
        /// <param name="excludeList">排除list</param>
        /// <param name="updateEntity">修改信息</param>
        public void EditStepParameter(Guid stepid, int allSelectorStatus, string defaultList, string includeList, string excludeList,VisitingTaskParameterMappingEntity[] updateEntity)
        {
            /*
0默认勾选
添加新数据（判断是否存在）
删除缺掉的数据

1全选
添加， 不在excludeLists 排除id 中的数据(判断是否存在)
删除 excludeLists   排除id

2全不选
先删除 不在includeList 包含id中的数据
添加 includeList  中的数据（判断是否存在）
             */

            VisitingParameterViewEntity parameterEntity = new VisitingParameterViewEntity();
            parameterEntity.VisitingTaskStepID = stepid;
            int rowcount = 0;

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    List<VisitingParameterViewEntity> oldList = this.GetStepParameterList(parameterEntity, 1, 100000, out rowcount).ToList();

                    if (allSelectorStatus == 0)//默认,勾选
                    {
                        //添加
                        string[] defaultLists = defaultList.Split(',');//1,2,3
                        string[] includeLists = includeList.Split(',');//1,2,3,4   1,2   1,2,4
                        StringBuilder delList = new StringBuilder();
                        for (int i = 0; i < includeLists.Length; i++)
                        {
                            if (!defaultLists.Contains(includeLists[i]))
                            {
                                VisitingTaskParameterMappingEntity entity = new VisitingTaskParameterMappingEntity();
                                entity.VisitingParameterID = Guid.Parse(includeLists[i]);
                                entity.VisitingTaskStepID = stepid;
                                entity.ParameterOrder = 0;
                                entity.ClientID = CurrentUserInfo.ClientID;
                                entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                new VisitingTaskParameterMappingBLL(CurrentUserInfo).Create(entity, tran);
                            }
                        }
                        //删除
                        StringBuilder sbDelList = new StringBuilder();
                        for (int i = 0; i < defaultLists.Length; i++)
                        {
                            if (!includeLists.Contains(defaultLists[i]))
                            {
                                //if()
                                //{
                                sbDelList.Append("'" + defaultLists[i] + "',");
                                //}
                            }
                        }
                        if (!string.IsNullOrEmpty(sbDelList.ToString()))
                        {
                            this._currentDAO.DeleteStepParameterIn(stepid.ToString(),
                                sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(), tran);
                        }
                    }
                    else if (allSelectorStatus == 1)//全选
                    {
                        //添加
                        string[] excludeLists = excludeList.Split(',');
                        for (int i = 0; i < oldList.ToArray().Length; i++)
                        {
                            if (oldList[i].MappingID == null || string.IsNullOrEmpty(oldList[i].MappingID.ToString()))
                            {
                                if (!excludeLists.Contains(oldList[i].VisitingParameterID.ToString()))
                                {
                                    VisitingTaskParameterMappingEntity entity = new VisitingTaskParameterMappingEntity();
                                    entity.VisitingParameterID = oldList[i].VisitingParameterID;
                                    entity.VisitingTaskStepID = stepid;
                                    entity.ParameterOrder = 0;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingTaskParameterMappingBLL(CurrentUserInfo).Create(entity, tran);
                                }
                            }
                        }
                        //删除
                        if (excludeList != "")
                        {
                            this._currentDAO.DeleteStepParameterIn(
                                stepid.ToString(),
                                "'" + excludeList.Replace(",", "','") + "'",
                                tran);
                        }
                    }
                    else if (allSelectorStatus == 2)//全不选
                    {
                        //删除
                        if (includeList != "")
                        {
                            this._currentDAO.DeleteStepParameterNotIn(stepid.ToString(),
                                "'" + includeList.Replace(",", "','") + "'"
                                , tran);
                        }
                        else
                        {
                            this._currentDAO.DeleteStepParameterAll(stepid.ToString(), tran);
                        }
                        //添加
                        if (includeList.Trim().Length > 0)
                        {
                            string[] includeLists = includeList.Split(',');
                            foreach (string pid in includeLists)
                            {
                                if (oldList.Where(m =>
                                    m.MappingID != null
                                    && m.VisitingTaskStepID == stepid
                                    && m.VisitingParameterID == Guid.Parse(pid)).ToArray().Length == 0)
                                {
                                    VisitingTaskParameterMappingEntity entity = new VisitingTaskParameterMappingEntity();
                                    entity.VisitingParameterID = Guid.Parse(pid);
                                    entity.VisitingTaskStepID = stepid;
                                    entity.ParameterOrder = 0;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingTaskParameterMappingBLL(CurrentUserInfo).Create(entity, tran);
                                }
                            }
                        }
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }

            tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    //修改数据
                    List<VisitingParameterViewEntity> oldList = this.GetStepParameterList(parameterEntity, 1, 100000, out rowcount).ToList();
                    foreach (VisitingTaskParameterMappingEntity uEntity in updateEntity)
                    {
                        if (oldList.Where(m => m.VisitingParameterID == uEntity.VisitingParameterID && m.MappingID != null).ToArray().Length == 1)
                        {
                            VisitingParameterViewEntity entity = oldList.Where(m => m.VisitingParameterID == uEntity.VisitingParameterID && m.MappingID != null).ToArray()[0];

                            VisitingTaskParameterMappingEntity mEntity = new VisitingTaskParameterMappingEntity();
                            mEntity.MappingID = entity.MappingID;
                            mEntity.VisitingTaskStepID = entity.VisitingTaskStepID;
                            mEntity.VisitingParameterID = entity.VisitingParameterID;
                            mEntity.ParameterOrder = uEntity.ParameterOrder;
                            mEntity.ClientID = entity.ClientID;
                            mEntity.ClientDistributorID = entity.ClientDistributorID;
                            mEntity.CreateBy = entity.CreateBy;
                            mEntity.CreateTime = entity.CreateTime;

                            this.Update(mEntity, tran);
                        }
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
    }
}