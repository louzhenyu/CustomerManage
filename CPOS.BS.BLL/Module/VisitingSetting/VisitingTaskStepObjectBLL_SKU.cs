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
using JIT.Utility.Reflection;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理： 拜访步骤中的对象 
    /// </summary>
    public partial class VisitingTaskStepObjectBLL_SKU : SKUContorlSelectBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new VisitingTaskStepObjectDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public VisitingTaskStepObjectBLL_SKU(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new VisitingTaskStepObjectDAO(pUserInfo);
        }
        #endregion

        #region GetStepSKUList
        /// <summary>
        /// 获取拜访步骤产品列表
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetStepSKUList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string stepid)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetSKUGridFildSQL()); //获取字SQL

            //new
            sql.AppendFormat(" VTSO.ObjectID,VTSO.VisitingTaskStepID,CAST(main.SKU_ID as nvarchar(200)) as Target1ID,VTSO.Target2ID, ");

            sql.AppendLine("ROW_NUMBER() OVER( order by case when VTSO.ObjectID is null then 1 else 0 end asc,main.modify_time desc) ROW_NUMBER");
            sql.AppendLine(" into #outTemp");
            sql.AppendLine("from t_SKU main");
            sql.Append(GetSKULeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetSKUSearchJoinSQL(pSearch)); //获取条件联接SQL

            //new
            sql.AppendFormat(" left join VisitingTaskStepObject VTSO on main.SKU_ID=VTSO.Target1ID and VTSO.Target2ID is null and VTSO.isdelete=0 and VTSO.VisitingTaskStepID='{0}' and VTSO.ClientID='{1}' and VTSO.ClientDistributorID={2}", stepid, CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);

            sql.AppendLine(string.Format("Where main.if_flag=0", base._pUserInfo.ClientID));
            sql.Append(GetSKUGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            return base.GetPageData(sql.ToString());
            
        }
        #endregion
        #region EditStepObject_SKU
        /// <summary>
        /// 拜访步骤选择产品
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="stepid">拜访步骤ID</param>
        /// <param name="allSelectorStatus">选择状态</param>
        /// <param name="defaultList">默认list</param>
        /// <param name="includeList">选择list</param>
        /// <param name="excludeList">排除list</param>
        public void EditStepObject_SKU(List<DefindControlEntity> pSearch, Guid stepid, int allSelectorStatus, string defaultList, string includeList, string excludeList)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    List<VisitingTaskStepObjectViewEntity> oldList = DataLoader.LoadFrom<VisitingTaskStepObjectViewEntity>(this.GetStepSKUList(pSearch, 10000000, 0, stepid.ToString()).GridData).ToList();

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
                                entity.ClientID =CurrentUserInfo.ClientID;
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