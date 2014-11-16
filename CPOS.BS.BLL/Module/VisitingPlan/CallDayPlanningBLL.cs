/*
 * Author		:jun.tian
 * EMail		:jun.tian@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/3/28 10:52:17
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
    /// CallDayPlanningBLL 
    /// </summary>
    public partial class CallDayPlanningBLL
    {
        #region GetUserCDPList
        public CallDayPlanningViewEntity_User[] GetUserCDPList(CallDayPlanningViewEntity_User entity, int pageIndex, int pageSize, out int rowCount)
        {
            

            PagedQueryResult<CallDayPlanningViewEntity_User> pEntity = this._currentDAO.GetUserCDPList(entity, null, pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region GetUserCDP
        public CallDayPlanningViewEntity_UserDate[] GetUserCDP(CallDayPlanningViewEntity_UserDate entity)
        {
            return this._currentDAO.GetUserCDP(entity);
        }
        #endregion

        #region EditCDP
        public void EditCDP(CallDayPlanningEntity entity,string poplist)
        {
            CallDayPlanningViewEntity_POP searchEntity = new CallDayPlanningViewEntity_POP();
            searchEntity.ClientUserID = entity.ClientUserID;
            searchEntity.CallDate = entity.CallDate;
            CallDayPlanningEntity[] oldEntity = this._currentDAO.GetCallDayPlanning(searchEntity);

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    foreach (string popid in poplist.Split(','))
                    {
                        if (!string.IsNullOrEmpty(popid)
                            && oldEntity.Where(m => m.POPID == popid).ToArray().Length == 0)
                        {
                            entity.PlanningID = null;
                            entity.POPID = popid;
                            this._currentDAO.Create(entity, tran);
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

        #region GetUserCDPPOPType
        /// <summary>
        /// 获取用户拜访计划终端类型，其中 ClientUserID  calldate 必传
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet GetUserCDPPOPType(CallDayPlanningEntity entity)
        {
            return this._currentDAO.GetUserCDPPOPType(entity);
        }
        #endregion
    }
}