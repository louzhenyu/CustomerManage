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
    /// ҵ���� ·�ߡ��ն�(������)ӳ�� 
    /// </summary>
    public partial class RoutePOPMappingBLL_Store : StoreDefindModuleBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new RoutePOPMappingDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pUserInfo">��ǰ�û���Ϣʵ��</param>
        /// <param name="pTableName">ģ������</param>
        public RoutePOPMappingBLL_Store(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new RoutePOPMappingDAO(pUserInfo);
        }
        #endregion

        #region GetRouteStoreList
        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="pSearch">��ѯ����</param>
        /// <param name="pPageSize">��ҳ��</param>
        /// <param name="pPageIndex">��ǰҳ</param>
        /// <returns>���ݣ���¼����ҳ��</returns>
        public PageResultEntity GetRouteStoreList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string routeid)
        {
            //ͨ��routeid ��ȡ clientuserid
            RouteUserMappingEntity userEntity = new RouteBLL(CurrentUserInfo).GetRouteUser(Guid.Parse(routeid));

            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());//��Ҫ���ɵ���ʱ��
            sql.AppendLine("select ");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreGridFildSQL()); //��ȡ��SQL

            //new
            sql.AppendFormat(" RPOPM.MappingID,cast('{0}' as uniqueidentifier) as RouteID,RPOPM.POPID,RPOPM.Sequence, ", routeid);

            sql.AppendLine("ROW_NUMBER() OVER( order by case when RPOPM.MappingID is null then 1 else 0 end asc,RPOPM.Sequence asc,RPOPM.CreateTime asc) ROW_NUMBER,");
            sql.AppendLine("main.StoreID into #outTemp");
            sql.AppendLine("from Store main");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreLeftGridJoinSQL()); //��ȡ����SQL
            sql.AppendLine("");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreSearchJoinSQL(pSearch)); //��ȡ��������SQL

            //new
            sql.AppendLine(string.Format(" inner join (select distinct StoreID from dbo.fnGetStoreByUserID('{0}',1)) fn_sad on  CONVERT(nvarchar(100),main.StoreID) = CONVERT(nvarchar(100),fn_sad.StoreID) ", userEntity.ClientUserID));

            sql.AppendFormat(" left join RoutePOPMapping RPOPM on cast(main.StoreID as nvarchar(200))=RPOPM.POPID and RPOPM.isdelete=0 and cast(RPOPM.RouteID as nvarchar(200))='{0}' and RPOPM.ClientID={1} and RPOPM.ClientDistributorID={2}",
                routeid,
                CurrentUserInfo.ClientID,
                CurrentUserInfo.ClientDistributorID);

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID={0}",
                base._pUserInfo.ClientID));

            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "").GetStroeGridSearchSQL(pSearch)); //��ȡ����
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            sql.Append(base.GetDropTempSql()); //��Ҫɾ������ʱ��
            return base.GetPageData(sql.ToString());

        }
        #endregion
        #region EditRoutePOPList_Store
        /// <summary>
        /// ·���б�ѡ���ն�
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
                                //ɾ��
                                this._currentDAO.Delete(entity, tran);
                            }
                            else
                            {
                                //�޸�
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
                            //����
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
        /// ·�ߵ�ͼѡ���ն�
        /// </summary>
        /// <param name="routeid"></param>
        /// <param name="mappingEntity"></param>
        /// <param name="deletelist">ɾ����ID���ϣ���Ҫ���û�������ѡ��ģ�</param>
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
                            //�޸�
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
                            //����
                            RoutePOPMappingEntity uEntity = new RoutePOPMappingEntity();
                            uEntity.RouteID = routeid;
                            uEntity.POPID = entity.StoreID.ToString().ToLower();
                            uEntity.Sequence = (i + 1);
                            uEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            uEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            this._currentDAO.Create(uEntity, tran);
                        }
                    }
                    //ɾ��
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