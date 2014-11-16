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
    /// ҵ���� �ݷö���(�ŵ�/������) 
    /// </summary>
    public partial class VisitingPOPMappingBLL_Store : StoreDefindModuleBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new VisitingPOPMappingDAO _currentDAO;
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pUserInfo">��ǰ�û���Ϣʵ��</param>
        /// <param name="pTableName">ģ������</param>
        public VisitingPOPMappingBLL_Store(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new VisitingPOPMappingDAO(pUserInfo);
        }
        #endregion

        #region GetTaskStoreList
        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="pSearch">��ѯ����</param>
        /// <param name="pPageSize">��ҳ��</param>
        /// <param name="pPageIndex">��ǰҳ</param>
        /// <returns>���ݣ���¼����ҳ��</returns>
        public PageResultEntity GetTaskStoreList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string taskid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());//��Ҫ���ɵ���ʱ��
            sql.AppendLine("select ");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo,"Store").GetStoreGridFildSQL()); //��ȡ��SQL

            //new
            sql.AppendFormat(" VPOPM.MappingID,cast('{0}' as uniqueidentifier) as VisitingTaskID,VPOPM.POPID, ", taskid);

            sql.AppendLine("ROW_NUMBER() OVER( order by case when VPOPM.MappingID is null then 1 else 0 end asc,main.LastUpdateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.StoreID into #outTemp");
            sql.AppendLine("from Store main");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreLeftGridJoinSQL()); //��ȡ����SQL
            sql.AppendLine("");
            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "Store").GetStoreSearchJoinSQL(pSearch)); //��ȡ��������SQL

            //new
            sql.AppendFormat(" left join VisitingPOPMapping VPOPM on cast(main.StoreID as nvarchar(200))=VPOPM.POPID and VPOPM.isdelete=0 and cast(VPOPM.VisitingTaskID as nvarchar(200))='{0}' and VPOPM.ClientID='{1}' and VPOPM.ClientDistributorID={2}",
                taskid,
                CurrentUserInfo.ClientID,
                CurrentUserInfo.ClientDistributorID);

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'",
                base._pUserInfo.ClientID));

            sql.Append(new StoreDefindModuleBLL(CurrentUserInfo, "").GetStroeGridSearchSQL(pSearch)); //��ȡ����
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            sql.Append(base.GetDropTempSql()); //��Ҫɾ������ʱ��
            return base.GetPageData(sql.ToString());

        }
        #endregion
        #region EditTaskPOP_Store
        /// <summary>
        /// �ݷ�����ѡ���ŵ�
        /// </summary>
        /// <param name="pSearch">��ѯ����</param>
        /// <param name="taskid">�ݷ�����ID</param>
        /// <param name="allSelectorStatus">ѡ��״̬</param>
        /// <param name="defaultList">Ĭ��list</param>
        /// <param name="includeList">ѡ��list</param>
        /// <param name="excludeList">�ų�list</param>
        public void EditTaskPOP_Store(int isAutoFill, List<DefindControlEntity> pSearch, string conditions, Guid taskid, int allSelectorStatus, string defaultList, string includeList, string excludeList)
        {
            VisitingTaskEntity taskEntity = new VisitingTaskBLL(CurrentUserInfo).GetByID(taskid);

            List<VisitingPOPMappingViewEntity> oldList = DataLoader.LoadFrom<VisitingPOPMappingViewEntity>(this.GetTaskStoreList(pSearch, int.MaxValue, 0, taskid.ToString()).GridData).ToList();

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    #region ������Ϣ����
                    bool changeCondition = false;//������������޸��ˣ���ô���POP������
                    POPGroupEntity entity = new POPGroupBLL(CurrentUserInfo).GetPOPGroupByTaskID(taskid);
                    if (entity != null)
                    {//�޸�
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
                    {//����
                        changeCondition = true;
                        entity = new POPGroupEntity();
                        entity.SqlTemplate = new VisitingTaskBLL(CurrentUserInfo).GetPOPSqlTemplate(pSearch, taskEntity.POPType.Value, taskid);
                        entity.GroupCondition = conditions;
                        entity.IsAutoFill = isAutoFill;
                        entity.POPType = taskEntity.POPType;

                        entity.ClientID = CurrentUserInfo.ClientID;
                        entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                        new POPGroupBLL(CurrentUserInfo).Create(entity, tran);
                    }

                    taskEntity.POPGroupID = entity.POPGroupID;
                    new VisitingTaskBLL(CurrentUserInfo).Update(taskEntity, tran);
                    #endregion

                    #region ����ն���Ϣ
                    if (isAutoFill == 1)
                    {
                        #region ���Ŀǰ�������������ն�
                        //ɾ��
                        if (changeCondition)
                        {
                            //ɾ�����У���������
                            this._currentDAO.DeleteVisitingPOPAll(taskid, tran);

                            this._currentDAO.CreatePOP_StoreList(
                                taskid,
                                oldList
                                .Select(m => m.StoreID.ToString()).ToArray().ToJoinString("','"),
                                tran);
                        }
                        else
                        {
                            //�������ݿ�û�е�
                            this._currentDAO.CreatePOP_StoreList(
                                taskid,
                                 oldList
                                .Where(m => !m.MappingID.HasValue).ToArray()
                                .Select(m => m.StoreID.ToString()).ToArray().ToJoinString("','"),
                                tran);
                        }
                        #endregion
                    }
                    else
                    {
                        #region ��ѡ�ն�
                        //��ѯ�����ݿ�����ѡ��Ĳ�Ʒ
                        IEnumerable<string> selectedIDS = oldList.Where(x => x.MappingID != null).Select(x => x.StoreID.ToString());
                        //ɾ��ID����
                        IEnumerable<string> deletedList = null;
                        //����ID����
                        IEnumerable<string> createList = null;
                        //ȫѡĬ��״̬(û�е�ȫѡcheckbox)
                        if (allSelectorStatus == 0)
                        {
                            //ǰ��ѡ��Ĳ�ƷID����
                            string[] frontSelectedIDS = includeList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                            deletedList = selectedIDS.Except(frontSelectedIDS);
                            createList = frontSelectedIDS.Except(selectedIDS);
                        }

                        //����ȫѡ
                        else if (allSelectorStatus == 1)
                        {
                            //�ų���ID����
                            string[] excludeIDS = excludeList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            //ȡ�������ó���Ҫɾ��ID����
                            deletedList = selectedIDS.Intersect(excludeIDS);
                            //ȡ����ó���Ҫ����ID����
                            createList = oldList.Where(x => x.MappingID == null).Select(x => x.StoreID.ToString()).Except(excludeIDS);
                        }

                        //ȡ����ȫѡ
                        else if (allSelectorStatus == 2)
                        {
                            //ѡ���ID����
                            string[] includeIDS =includeList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            //ȡ����ó���Ҫ������ID����
                            createList = includeIDS.Except(selectedIDS);
                            //ȡ����ó���Ҫɾ����ID����
                            deletedList = oldList.Where(x => x.MappingID != null).Select(x => x.StoreID.ToString()).Except(includeIDS);
                        }

                        #region ɾ����������
                        //ɾ������
                        if (deletedList != null && deletedList.Count() > 0)
                        {
                            this._currentDAO.DeleteVisitingPOPIn(taskid, string.Join("','", deletedList), tran);
                        }

                        //��������
                        if (createList != null && createList.Count() > 0)
                        {
                            this._currentDAO.CreatePOP_StoreList(taskid, string.Join("','", createList), tran);
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
        public string GetTaskPOPGroupSqlTemplate(List<DefindControlEntity> pSearch,string taskid)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(base.GetTempSql());//��Ҫ���ɵ���ʱ��
            sql.AppendLine("select main.StoreID as POPID,1 as POPType");

            sql.AppendLine("from Store main");
            sql.Append(GetStoreLeftGridJoinSQL()); //��ȡ����SQL
            sql.AppendLine("");
            sql.Append(GetStoreSearchJoinSQL(pSearch)); //��ȡ��������SQL

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'",
                base._pUserInfo.ClientID));

            sql.Append(GetStroeGridSearchSQL(pSearch)); //��ȡ����
            sql.Append(base.GetDropTempSql()); //��Ҫɾ������ʱ��
            return sql.ToString();
        }
        #endregion
    }
}