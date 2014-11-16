/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/27 11:34:46
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
    /// 业务处理： 路线、终端(经销商)映射 
    /// </summary>
    public partial class RoutePOPMappingBLL_Store : StoreDefindModuleBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new RoutePOPMappingDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public RoutePOPMappingBLL_Store(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new RoutePOPMappingDAO(pUserInfo);
        }
        #endregion

        #region GetRouteStoreList
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetRouteStoreList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string routeid)
        {
            //通过routeid 获取 clientuserid
            RouteUserMappingEntity userEntity = new RouteBLL(CurrentUserInfo).GetRouteUser(Guid.Parse(routeid));

            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreGridFildSQL()); //获取字SQL

            //new
            sql.AppendFormat(" RPOPM.MappingID,cast('{0}' as uniqueidentifier) as RouteID,RPOPM.POPID,RPOPM.Sequence, ", routeid);

            sql.AppendLine("ROW_NUMBER() OVER( order by case when RPOPM.MappingID is null then 1 else 0 end asc,RPOPM.Sequence asc,RPOPM.CreateTime asc) ROW_NUMBER,");
            sql.AppendLine("main.StoreID into #outTemp");
            sql.AppendLine("from Store main");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL

            //new
            sql.AppendLine(string.Format(" inner join (select distinct StoreID from dbo.fnGetStoreByUserID('{0}',1)) fn_sad on  CONVERT(nvarchar(100),main.StoreID) = CONVERT(nvarchar(100),fn_sad.StoreID) ", userEntity.ClientUserID));

            sql.AppendFormat(" left join RoutePOPMapping RPOPM on cast(main.StoreID as nvarchar(200))=RPOPM.POPID and RPOPM.isdelete=0 and cast(RPOPM.RouteID as nvarchar(200))='{0}' and RPOPM.ClientID={1} and RPOPM.ClientDistributorID={2}",
                routeid,
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
        #region EditRoutePOPList_Store
        /// <summary>
        /// 路线列表选择终端
        /// </summary>
        /// <param name="routeid"></param>
        /// <param name="mappingEntity"></param>
        public void EditRoutePOPList_Store(Guid routeid, RoutePOPMappingViewEntity[] mappingEntity)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    foreach (RoutePOPMappingViewEntity entity in mappingEntity)
                    {
                        if (entity.MappingID != null && !string.IsNullOrEmpty(entity.MappingID.ToString()))
                        {
                            if (entity.IsDelete == 1)
                            {
                                //删除
                                this._currentDAO.Delete(entity, tran);
                            }
                            else
                            {
                                //修改
                                RoutePOPMappingEntity uEntity = new RoutePOPMappingEntity();
                                uEntity.MappingID = entity.MappingID;
                                uEntity.RouteID = routeid;
                                uEntity.POPID = entity.StoreID.ToString().ToLower();
                                uEntity.Sequence = entity.Sequence;
                                uEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                                uEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                this._currentDAO.Update(uEntity, tran);
                            }
                        }
                        else
                        {
                            //新增
                            RoutePOPMappingEntity uEntity = new RoutePOPMappingEntity();
                            uEntity.RouteID = routeid;
                            uEntity.POPID = entity.StoreID.ToString().ToLower();
                            uEntity.Sequence = entity.Sequence;
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

            new RouteBLL(CurrentUserInfo).GenerateCallDayPlanning(routeid);
        }
        #endregion
        #region EditRoutePOPMap_Store
        /// <summary>
        /// 路线地图选择终端
        /// </summary>
        /// <param name="routeid"></param>
        /// <param name="mappingEntity"></param>
        /// <param name="deletelist">删除的ID集合（不要含用户后来又选择的）</param>
        public void EditRoutePOPMap_Store(Guid routeid, RoutePOPMappingViewEntity[] mappingEntity,string deletelist)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    for (int i = 0; i < mappingEntity.Length;i++ )
                    {
                        RoutePOPMappingViewEntity entity = mappingEntity[i];

                        if (entity.MappingID != null && !string.IsNullOrEmpty(entity.MappingID.ToString()))
                        {
                            //修改
                            RoutePOPMappingEntity uEntity = new RoutePOPMappingEntity();
                            uEntity.MappingID = entity.MappingID;
                            uEntity.RouteID = routeid;
                            uEntity.POPID = entity.StoreID.ToString().ToLower();
                            uEntity.Sequence = (i + 1);
                            uEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            uEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            this._currentDAO.Update(uEntity, tran);
                        }
                        else
                        {
                            //新增
                            RoutePOPMappingEntity uEntity = new RoutePOPMappingEntity();
                            uEntity.RouteID = routeid;
                            uEntity.POPID = entity.StoreID.ToString().ToLower();
                            uEntity.Sequence = (i + 1);
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

            new RouteBLL(CurrentUserInfo).GenerateCallDayPlanning(routeid);
        }
        #endregion

    }
}