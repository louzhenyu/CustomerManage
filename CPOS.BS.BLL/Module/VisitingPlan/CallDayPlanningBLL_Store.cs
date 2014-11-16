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
using JIT.Utility.Reflection;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// CallDayPlanningBLL 
    /// </summary>
    public partial class CallDayPlanningBLL_Store : StoreDefindModuleBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new CallDayPlanningDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public CallDayPlanningBLL_Store(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new CallDayPlanningDAO(pUserInfo);
        }
        #endregion

        #region GetUserCDPStoreList
        public PageResultEntity GetUserCDPStoreList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, CallDayPlanningViewEntity_POP entity)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreGridFildSQL()); //获取字SQL

            //new
            sql.AppendFormat(" CPOPM.PlanningID as MappingID,cast('{0}' as int) as ClientUserID,CPOPM.POPID,CPOPM.Sequence, ", entity.ClientUserID);

            sql.AppendLine("ROW_NUMBER() OVER( order by case when CPOPM.PlanningID is null then 1 else 0 end asc,CPOPM.Sequence asc,CPOPM.CreateTime asc) ROW_NUMBER,");
            sql.AppendLine("main.StoreID into #outTemp");
            sql.AppendLine("from Store main");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL

            //new
            sql.AppendLine(string.Format(" inner join (select distinct StoreID from dbo.fnGetStoreByUserID('{0}',1)) fn_sad on  CONVERT(nvarchar(100),main.StoreID) = CONVERT(nvarchar(100),fn_sad.StoreID) ", entity.ClientUserID));

            sql.AppendFormat(@" left join CallDayPlanning CPOPM 
                                on CPOPM.POPType=1 and cast(main.StoreID as nvarchar(200))=CPOPM.POPID 
                                and CPOPM.isdelete=0 and CONVERT(date,CPOPM.CallDate,23)='{0}' and CPOPM.ClientUserID={1} 
                                and CPOPM.ClientID={2} and CPOPM.ClientDistributorID={3}",
                entity.CallDate.Value.ToShortDateString(),
                entity.ClientUserID,
                CurrentUserInfo.ClientID,
                CurrentUserInfo.ClientDistributorID);

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0}",
                base._pUserInfo.ClientID));

            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "").GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            sql.Append(base.GetDropTempSql()); //需要删除的临时表
            return base.GetPageData(sql.ToString());

        }
        #endregion
        #region EditUserCDPPOPList_Store
        /// <summary>
        /// 拜访计划终端选择
        /// </summary>
        /// <param name="infoEntity">clientuserid,calldate 必传</param>
        /// <param name="pSearch"></param>
        /// <param name="mappingEntity"></param>
        public void EditUserCDPPOPList_Store(
            CallDayPlanningViewEntity_POP infoEntity,
            List<DefindControlEntity> pSearch,
            CallDayPlanningViewEntity_POP[] mappingEntity)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    List<CallDayPlanningEntity> oldList = this._currentDAO.GetCallDayPlanning(infoEntity).ToList();

                    foreach (CallDayPlanningViewEntity_POP entity in mappingEntity)
                    {
                        if (entity.MappingID != null && !string.IsNullOrEmpty(entity.MappingID.ToString()))
                        {
                            if (entity.IsDelete == 1)
                            {
                                //删除
                                this._currentDAO.Delete(entity.MappingID, tran);
                            }
                            else
                            {
                                //修改
                                CallDayPlanningEntity uEntity = oldList.Where(m => m.PlanningID == entity.MappingID).ToArray()[0];
                                uEntity.Sequence = entity.Sequence;
                                this._currentDAO.Update(uEntity, tran);
                            }
                        }
                        else
                        {
                            //新增
                            CallDayPlanningEntity uEntity = new CallDayPlanningEntity();

                            uEntity.CallDate = infoEntity.CallDate;
                            uEntity.POPID = entity.StoreID.ToString().ToLower();
                            uEntity.POPType = 1;
                            uEntity.ClientUserID = infoEntity.ClientUserID;
                            uEntity.Sequence = entity.Sequence;
                            uEntity.PlanningType = 2;
                            
                            uEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            uEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            this._currentDAO.Create(uEntity, tran);
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
        #region EditUserCDPPOPMap_Store
        /// <summary>
        /// 路线地图选择终端
        /// </summary>
        /// <param name="routeid"></param>
        /// <param name="mappingEntity"></param>
        /// <param name="deletelist">删除的ID集合（不要含用户后来又选择的）</param>
        public void EditUserCDPPOPMap_Store(CallDayPlanningViewEntity_POP infoEntity, List<DefindControlEntity> pSearch, CallDayPlanningViewEntity_POP[] mappingEntity, string deletelist)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    List<CallDayPlanningEntity> oldList = this._currentDAO.GetCallDayPlanning(infoEntity).ToList();

                    for (int i = 0; i < mappingEntity.Length; i++)
                    {
                        CallDayPlanningViewEntity_POP entity = mappingEntity[i];

                        if (entity.MappingID != null && !string.IsNullOrEmpty(entity.MappingID.ToString()))
                        {
                            //修改
                            CallDayPlanningEntity uEntity = oldList.Where(m => m.PlanningID == entity.MappingID).ToArray()[0];
                            uEntity.Sequence = (i + 1);
                            this._currentDAO.Update(uEntity, tran);
                        }
                        else
                        {
                            //新增
                            CallDayPlanningEntity uEntity = new CallDayPlanningEntity();

                            uEntity.CallDate = infoEntity.CallDate;
                            uEntity.POPID = entity.StoreID.ToString().ToLower();
                            uEntity.POPType = 1;
                            uEntity.ClientUserID = infoEntity.ClientUserID;
                            uEntity.Sequence = (i + 1);
                            uEntity.PlanningType = 2;

                            uEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            uEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            this._currentDAO.Create(uEntity, tran);
                        }
                    }
                    //删除
                    if (deletelist != null && !string.IsNullOrEmpty(deletelist))
                    {
                        foreach (string mapid in deletelist.Split(','))
                        {
                            if (!string.IsNullOrEmpty(mapid))
                            {
                                this._currentDAO.Delete(Guid.Parse(mapid), tran);
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