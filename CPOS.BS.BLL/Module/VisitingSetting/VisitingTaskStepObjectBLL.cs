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
    /// 业务处理： 拜访步骤中的对象 
    /// </summary>
    public partial class VisitingTaskStepObjectBLL
    {
        #region GetStepObjectLevel
        public string GetStepObjectLevel(Guid stepID)
        {
            return this._currentDAO.GetStepObjectLevel(stepID);
        }
        #endregion
        #region GetStepBrandList
        /// <summary>
        /// 获取拜访步骤品牌列表
        /// </summary>
        /// <param name="brandLevel">品牌等级</param>
        /// <param name="categoryLevel">分类等级</param>
        /// <param name="stepID">拜访步骤ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingTaskStepObjectViewEntity[] GetStepBrandList(int brandLevel, int categoryLevel, Guid stepID, int pageIndex, int pageSize, out int rowCount)
        {
            PagedQueryResult<VisitingTaskStepObjectViewEntity> pEntity = new VisitingTaskStepObjectDAO(CurrentUserInfo).GetStepBrandList(brandLevel, categoryLevel, stepID, pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion
        #region GetStepCategoryList
        /// <summary>
        /// 获取拜访步骤分类列表
        /// </summary>
        /// <param name="brandLevel">品牌等级</param>
        /// <param name="categoryLevel">分类等级</param>
        /// <param name="stepID">拜访步骤ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingTaskStepObjectViewEntity[] GetStepCategoryList(int brandLevel, int categoryLevel, Guid stepID, int pageIndex, int pageSize, out int rowCount)
        {
            PagedQueryResult<VisitingTaskStepObjectViewEntity> pEntity = new VisitingTaskStepObjectDAO(CurrentUserInfo).GetStepCategoryList(brandLevel, categoryLevel, stepID, pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion
        #region GetStepPositionList
        /// <summary>
        /// 获取拜访步骤职位列表
        /// </summary>
        /// <param name="stepid">拜访步骤ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public VisitingTaskStepObjectViewEntity[] GetStepPositionList(Guid stepid, int pageIndex, int pageSize, out int rowCount)
        {
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "case when ObjectID is null then 1 else 0 end ", Direction = OrderByDirections.Asc });

            PagedQueryResult<VisitingTaskStepObjectViewEntity> pEntity = new VisitingTaskStepObjectDAO(CurrentUserInfo).GetStepPositionList(stepid, null, orderbys.ToArray(), pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion
        #region GetStepObjectList
        /// <summary>
        /// 获取自定义对象，分组类型(关联表只存一行数据)
        /// </summary>
        /// <param name="stepid"></param>
        /// <returns></returns>
        public VisitingTaskStepObjectEntity GetStepObjectList(Guid stepid)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (stepid != null )
            {
                wheres.Add(new EqualsCondition() { FieldName = "VisitingTaskStepID", Value = stepid.ToString() });
            }

            PagedQueryResult<VisitingTaskStepObjectEntity> pEntity = this._currentDAO.PagedQuery(wheres.ToArray(), null, 2, 1);
            int rowCount = pEntity.RowCount;
            if (pEntity.RowCount == 1)
            {
                return pEntity.Entities[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region EditStepObject_Brand
        /// <summary>
        /// 拜访步骤选择品牌
        /// </summary>
        /// <param name="brandLevel">品牌等级</param>
        /// <param name="categoryLevel">分类等级</param>
        /// <param name="stepid">拜访步骤ID</param>
        /// <param name="allSelectorStatus">选择状态</param>
        /// <param name="defaultList">默认list</param>
        /// <param name="includeList">选择list</param>
        /// <param name="excludeList">排除list</param>
        public void EditStepObject_Brand(int brandLevel, int categoryLevel, Guid stepid, int allSelectorStatus, string defaultList, string includeList, string excludeList)
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
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VisitingTaskStepObjectEntity objectEntity = new VisitingTaskStepObjectEntity();
                    objectEntity.VisitingTaskStepID = stepid;
                    int rowcount = 0;
                    List<VisitingTaskStepObjectViewEntity> oldList = this.GetStepBrandList(brandLevel, categoryLevel, stepid, 1, 100000, out rowcount).ToList();

                    //清空其它等级数据
                    this._currentDAO.DeleteStepObjectLevelNotIn(stepid.ToString(), brandLevel + "," + categoryLevel, tran);
                    if (allSelectorStatus == 0)//默认,勾选
                    {
                        //添加
                        string[] defaultLists = defaultList.Split(',');//1,2,3
                        string[] includeLists = includeList.Split(',');//1,2,3,4   1,2   1,2,4
                        for (int i = 0; i < includeLists.Length; i++)
                        {
                            if (!defaultLists.Contains(includeLists[i]))
                            {
                                VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                entity.Target1ID = includeLists[i].Split('|')[0];
                                if (categoryLevel > 0)
                                {
                                    entity.Target2ID = includeLists[i].Split('|')[1];
                                }
                                entity.VisitingTaskStepID = stepid;
                                entity.Level = brandLevel + "," + categoryLevel;
                                entity.ClientID = CurrentUserInfo.ClientID;
                                entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                            }
                        }
                        //删除
                        StringBuilder sbDelList = new StringBuilder();
                        for (int i = 0; i < defaultLists.Length; i++)
                        {
                            if (!includeLists.Contains(defaultLists[i]))
                            {
                                sbDelList.Append(defaultLists[i] + ",");
                            }
                        }
                        if (!string.IsNullOrEmpty(sbDelList.ToString()))
                        {
                            if (categoryLevel > 0)
                            {
                                this._currentDAO.DeleteStepObjectIn2(
                                    stepid.ToString(),
                                    oldList,
                                    sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(),
                                    tran);
                            }
                            else
                            {
                                this._currentDAO.DeleteStepObjectIn(
                                    stepid.ToString(),
                                    sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(),
                                    tran);
                            }
                        }
                    }
                    else if (allSelectorStatus == 1)//全选
                    {
                        //添加
                        string[] excludeLists = excludeList.Split(',');
                        for (int i = 0; i < oldList.ToArray().Length; i++)
                        {
                            if (oldList[i].ObjectID == null || string.IsNullOrEmpty(oldList[i].ObjectID.ToString()))
                            {
                                if (!excludeLists.Contains(oldList[i].Target1ID.ToString()))
                                {
                                    VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                    entity.Target1ID = oldList[i].Target1ID.ToString().Split('|')[0];
                                    if (categoryLevel > 0)
                                    {
                                        entity.Target2ID = oldList[i].Target1ID.ToString().Split('|')[1];
                                    }
                                    entity.VisitingTaskStepID = stepid;
                                    entity.Level = brandLevel + "," + categoryLevel;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                }
                            }
                        }
                        //删除
                        if (excludeList != "")
                        {
                            if (categoryLevel > 0)
                            {
                                this._currentDAO.DeleteStepObjectIn2(
                                                                stepid.ToString(),
                                                                oldList,
                                                                excludeList,
                                                                tran);
                            }
                            else
                            {
                                this._currentDAO.DeleteStepObjectIn(
                                                                stepid.ToString(),
                                                                excludeList,
                                                                tran);
                            }
                        }
                    }
                    else if (allSelectorStatus == 2)//全不选
                    {
                        //删除
                        if (includeList != "")
                        {
                            if (categoryLevel > 0)
                            {
                                this._currentDAO.DeleteStepObjectNotIn2(
                                                                stepid.ToString(),
                                                                oldList,
                                                                includeList,
                                                                tran);
                            }
                            else
                            {
                                this._currentDAO.DeleteStepObjectNotIn(stepid.ToString(),
                                    oldList,
                                    includeList,
                                    tran);
                            }
                        }
                        else
                        {
                            //这里不能删除全部，只删除当前条件下所有列表
                            this._currentDAO.Delete(oldList.Where(m => m.ObjectID != null).ToArray());
                            //this._currentDAO.DeleteStepObjectAll(stepid.ToString(), tran);
                        }
                        //添加
                        if (includeList.Trim().Length > 0)
                        {
                            string[] includeLists = includeList.Split(',');
                            foreach (string pid in includeLists)
                            {
                                if (categoryLevel > 0)
                                {
                                    if (oldList.Where(m =>
                                        m.ObjectID != null
                                        && m.VisitingTaskStepID == stepid
                                        && m.Target1ID == pid
                                        && !string.IsNullOrEmpty(m.Target2ID)).ToArray().Length == 0)
                                    {
                                        VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                        entity.Target1ID = pid.Split('|')[0];
                                        entity.Target2ID = pid.Split('|')[1];
                                        entity.VisitingTaskStepID = stepid;
                                        entity.Level = brandLevel + "," + categoryLevel;
                                        entity.ClientID = CurrentUserInfo.ClientID;
                                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                        new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                    }
                                }
                                else
                                {
                                    var query = oldList.Where(m =>
                                        m.ObjectID != null
                                        && m.VisitingTaskStepID == stepid
                                        && m.Target1ID == pid
                                        && string.IsNullOrEmpty(m.Target2ID));
                                    if (oldList.Where(m =>
                                        m.ObjectID != null
                                        && m.VisitingTaskStepID == stepid
                                        && m.Target1ID == pid
                                        && string.IsNullOrEmpty(m.Target2ID)).ToArray().Length == 0)
                                    {
                                        VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                        entity.Target1ID = pid;
                                        entity.VisitingTaskStepID = stepid;
                                        entity.Level = brandLevel + "," + categoryLevel;
                                        entity.ClientID = CurrentUserInfo.ClientID;
                                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                        new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                    }
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
        }
        #endregion
        #region EditStepObject_Category
        /// <summary>
        /// 拜访步骤选择分类
        /// </summary>
        /// <param name="brandLevel">品牌等级</param>
        /// <param name="categoryLevel">分类等级</param>
        /// <param name="stepid">拜访步骤ID</param>
        /// <param name="allSelectorStatus">选择状态</param>
        /// <param name="defaultList">默认list</param>
        /// <param name="includeList">选择list</param>
        /// <param name="excludeList">排除list</param>
        public void EditStepObject_Category(int brandLevel, int categoryLevel, Guid stepid, int allSelectorStatus, string defaultList, string includeList, string excludeList)
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
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VisitingTaskStepObjectEntity objectEntity = new VisitingTaskStepObjectEntity();
                    objectEntity.VisitingTaskStepID = stepid;
                    int rowcount = 0;
                    List<VisitingTaskStepObjectViewEntity> oldList = this.GetStepCategoryList(brandLevel, categoryLevel, stepid, 1, 100000, out rowcount).ToList();

                    //清空其它等级数据
                    this._currentDAO.DeleteStepObjectLevelNotIn(stepid.ToString(), categoryLevel + "," + brandLevel, tran);
                    if (allSelectorStatus == 0)//默认,勾选
                    {
                        //添加
                        string[] defaultLists = defaultList.Split(',');//1,2,3
                        string[] includeLists = includeList.Split(',');//1,2,3,4   1,2   1,2,4
                        for (int i = 0; i < includeLists.Length; i++)
                        {
                            if (!defaultLists.Contains(includeLists[i]))
                            {
                                VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                entity.Target1ID = includeLists[i].Split('|')[0];
                                if (brandLevel > 0)
                                {
                                    entity.Target2ID = includeLists[i].Split('|')[1];
                                }
                                entity.VisitingTaskStepID = stepid;
                                entity.Level = categoryLevel + "," + brandLevel;
                                entity.ClientID = CurrentUserInfo.ClientID;
                                entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                            }
                        }
                        //删除
                        StringBuilder sbDelList = new StringBuilder();
                        for (int i = 0; i < defaultLists.Length; i++)
                        {
                            if (!includeLists.Contains(defaultLists[i]))
                            {
                                sbDelList.Append(defaultLists[i] + ",");
                            }
                        }
                        if (!string.IsNullOrEmpty(sbDelList.ToString()))
                        {
                            if (brandLevel > 0)
                            {
                                this._currentDAO.DeleteStepObjectIn2(
                                    stepid.ToString(),
                                    oldList,
                                    sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(),
                                    tran);
                            }
                            else
                            {
                                this._currentDAO.DeleteStepObjectIn(
                                    stepid.ToString(),
                                    sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(),
                                    tran);
                            }
                        }
                    }
                    else if (allSelectorStatus == 1)//全选
                    {
                        //添加
                        string[] excludeLists = excludeList.Split(',');
                        for (int i = 0; i < oldList.ToArray().Length; i++)
                        {
                            if (oldList[i].ObjectID == null || string.IsNullOrEmpty(oldList[i].ObjectID.ToString()))
                            {
                                if (!excludeLists.Contains(oldList[i].Target1ID.ToString()))
                                {
                                    VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                    entity.Target1ID = oldList[i].Target1ID.ToString().Split('|')[0];
                                    if (brandLevel > 0)
                                    {
                                        entity.Target2ID = oldList[i].Target1ID.ToString().Split('|')[1];
                                    }
                                    entity.VisitingTaskStepID = stepid;
                                    entity.Level = categoryLevel + "," + brandLevel;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                }
                            }
                        }
                        //删除
                        if (excludeList != "")
                        {
                            if (brandLevel > 0)
                            {
                                this._currentDAO.DeleteStepObjectIn2(
                                                                stepid.ToString(),
                                                                oldList,
                                                                excludeList,
                                                                tran);
                            }
                            else
                            {
                                this._currentDAO.DeleteStepObjectIn(
                                                                stepid.ToString(),
                                                                excludeList,
                                                                tran);
                            }
                        }
                    }
                    else if (allSelectorStatus == 2)//全不选
                    {
                        //删除
                        if (includeList != "")
                        {
                            if (brandLevel > 0)
                            {
                                this._currentDAO.DeleteStepObjectNotIn2(
                                                                stepid.ToString(),
                                                                oldList,
                                                                includeList,
                                                                tran);
                            }
                            else
                            {
                                this._currentDAO.DeleteStepObjectNotIn(stepid.ToString(),
                                    oldList,
                                    includeList,
                                    tran);
                            }
                        }
                        else
                        {
                            //这里不能删除全部，只删除当前条件下所有列表
                            this._currentDAO.Delete(oldList.Where(m => m.ObjectID != null).ToArray());
                            //this._currentDAO.DeleteStepObjectAll(stepid.ToString(), tran);
                        }
                        //添加
                        if (includeList.Trim().Length > 0)
                        {
                            string[] includeLists = includeList.Split(',');
                            foreach (string pid in includeLists)
                            {
                                if (brandLevel > 0)
                                {
                                    if (oldList.Where(m =>
                                        m.ObjectID != null
                                        && m.VisitingTaskStepID == stepid
                                        && m.Target1ID == pid
                                        && !string.IsNullOrEmpty(m.Target2ID)).ToArray().Length == 0)
                                    {
                                        VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                        entity.Target1ID = pid.Split('|')[0];
                                        entity.Target2ID = pid.Split('|')[1];
                                        entity.VisitingTaskStepID = stepid;
                                        entity.Level = categoryLevel + "," + brandLevel;
                                        entity.ClientID = CurrentUserInfo.ClientID;
                                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                        new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                    }
                                }
                                else
                                {
                                    var query = oldList.Where(m =>
                                        m.ObjectID != null
                                        && m.VisitingTaskStepID == stepid
                                        && m.Target1ID == pid
                                        && string.IsNullOrEmpty(m.Target2ID));
                                    if (oldList.Where(m =>
                                        m.ObjectID != null
                                        && m.VisitingTaskStepID == stepid
                                        && m.Target1ID == pid
                                        && string.IsNullOrEmpty(m.Target2ID)).ToArray().Length == 0)
                                    {
                                        VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                        entity.Target1ID = pid;
                                        entity.VisitingTaskStepID = stepid;
                                        entity.Level = categoryLevel + "," + brandLevel;
                                        entity.ClientID = CurrentUserInfo.ClientID;
                                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                        new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                    }
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
        }
        #endregion
        #region EditStepObject_Position
        /// <summary>
        /// 拜访步骤选择职位
        /// </summary>
        /// <param name="stepid">拜访步骤ID</param>
        /// <param name="allSelectorStatus">选择状态</param>
        /// <param name="defaultList">默认list</param>
        /// <param name="includeList">选择list</param>
        /// <param name="excludeList">排除list</param>
        public void EditStepObject_Position(Guid stepid, int allSelectorStatus, string defaultList, string includeList, string excludeList)
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
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    VisitingTaskStepObjectEntity objectEntity = new VisitingTaskStepObjectEntity();
                    objectEntity.VisitingTaskStepID = stepid;
                    int rowcount = 0;
                    List<VisitingTaskStepObjectViewEntity> oldList = this.GetStepPositionList(stepid, 1, 100000, out rowcount).ToList();

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
                                VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                entity.Target1ID = includeLists[i];
                                entity.VisitingTaskStepID = stepid;
                                entity.ClientID = CurrentUserInfo.ClientID;
                                entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                            }
                        }
                        //删除
                        StringBuilder sbDelList = new StringBuilder();
                        for (int i = 0; i < defaultLists.Length; i++)
                        {
                            if (!includeLists.Contains(defaultLists[i]))
                            {
                                //if (int.TryParse(defaultLists[i], out idhold))
                                //{
                                sbDelList.Append(defaultLists[i] + ",");
                                //}
                                //else
                                //{
                                //    sbDelList.Append("'" + defaultLists[i] + "',");
                                //}
                            }
                        }
                        if (!string.IsNullOrEmpty(sbDelList.ToString()))
                        {
                            this._currentDAO.DeleteStepObjectIn(stepid.ToString(),
                                sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(), tran);
                        }
                    }
                    else if (allSelectorStatus == 1)//全选
                    {
                        //添加
                        string[] excludeLists = excludeList.Split(',');
                        for (int i = 0; i < oldList.ToArray().Length; i++)
                        {
                            if (oldList[i].ObjectID == null || string.IsNullOrEmpty(oldList[i].ObjectID.ToString()))
                            {
                                if (!excludeLists.Contains(oldList[i].Target1ID.ToString()))
                                {
                                    VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                    entity.Target1ID = oldList[i].Target1ID.ToString();
                                    entity.VisitingTaskStepID = stepid;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
                                }
                            }
                        }
                        //删除
                        if (excludeList != "")
                        {
                            //if (int.TryParse(excludeList.Split(',')[0], out idhold))
                            //{
                            this._currentDAO.DeleteStepObjectIn(
                                                            stepid.ToString(),
                                                            excludeList,
                                                            tran);
                            //}
                            //else
                            //{
                            //    this._currentDAO.DeleteStepObjectIn(
                            //                                    stepid.ToString(),
                            //                                    "'" + excludeList.Replace(",", "','") + "'",
                            //                                    tran);
                            //}
                        }
                    }
                    else if (allSelectorStatus == 2)//全不选
                    {
                        //删除
                        if (includeList != "")
                        {
                            //if (int.TryParse(includeList.Split(',')[0], out idhold))
                            //{
                            this._currentDAO.DeleteStepObjectNotIn(
                                                            stepid.ToString(),
                                                            oldList,
                                                            includeList,
                                                            tran);
                            //}
                            //else
                            //{
                            //    this._currentDAO.DeleteStepObjectNotIn(stepid.ToString(),
                            //        oldList,
                            //    "'" + includeList.Replace(",", "','") + "'"
                            //    , tran);
                            //}
                        }
                        else
                        {
                            this._currentDAO.DeleteStepObjectAll(stepid.ToString(), tran);
                        }
                        //添加
                        if (includeList.Trim().Length > 0)
                        {
                            string[] includeLists = includeList.Split(',');
                            foreach (string pid in includeLists)
                            {
                                if (oldList.Where(m =>
                                    m.ObjectID != null
                                    && m.VisitingTaskStepID == stepid
                                    && m.Target1ID == pid).ToArray().Length == 0)
                                {
                                    VisitingTaskStepObjectEntity entity = new VisitingTaskStepObjectEntity();
                                    entity.Target1ID = pid;
                                    entity.VisitingTaskStepID = stepid;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingTaskStepObjectBLL(CurrentUserInfo).Create(entity, tran);
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
        }
        #endregion
    }
}