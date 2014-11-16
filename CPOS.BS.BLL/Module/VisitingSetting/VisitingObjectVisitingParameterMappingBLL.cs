/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
    /// ҵ���� �Զ������Ͱݷò���ӳ�� 
    /// </summary>
    public partial class VisitingObjectVisitingParameterMappingBLL
    {
        #region GetObjectParameterList
        public VisitingParameterViewEntity[] GetObjectParameterList(VisitingParameterViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "case when ParameterOrder is null then 1 else 0 end ,ParameterOrder", Direction = OrderByDirections.Asc });

            PagedQueryResult<VisitingParameterViewEntity> pEntity = this._currentDAO.GetObjectParameterList(entity, null, orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
            //return da.Query(wheres.ToArray(), orderbys.ToArray());
        }
        #endregion

        #region EditObjectParameter
        /// <summary>
        /// �Զ������ѡ��ݷò���
        /// </summary>
        /// <param name="objid">�Զ������ID</param>
        /// <param name="allSelectorStatus">ѡ��״̬</param>
        /// <param name="defaultList">Ĭ��list</param>
        /// <param name="includeList">ѡ��list</param>
        /// <param name="excludeList">�ų�list</param>
        /// <param name="updateEntity">�޸���Ϣ</param>
        public void EditObjectParameter(Guid objid, int allSelectorStatus, string defaultList, string includeList, string excludeList, VisitingObjectVisitingParameterMappingEntity[] updateEntity)
        {
            VisitingParameterViewEntity parameterEntity = new VisitingParameterViewEntity();
            parameterEntity.VisitingObjectID = objid;
            int rowcount = 0;

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    List<VisitingParameterViewEntity> oldList = this.GetObjectParameterList(parameterEntity, 1, 100000, out rowcount).ToList();

                    if (allSelectorStatus == 0)//Ĭ��,��ѡ
                    {
                        //���
                        string[] defaultLists = defaultList.Split(',');//1,2,3
                        string[] includeLists = includeList.Split(',');//1,2,3,4   1,2   1,2,4
                        StringBuilder delList = new StringBuilder();
                        for (int i = 0; i < includeLists.Length; i++)
                        {
                            if (!defaultLists.Contains(includeLists[i]))
                            {
                                VisitingObjectVisitingParameterMappingEntity entity = new VisitingObjectVisitingParameterMappingEntity();
                                entity.VisitingParameterID = Guid.Parse(includeLists[i]);
                                entity.VisitingObjectID = objid;
                                entity.ParameterOrder = 0;
                                entity.ClientID = CurrentUserInfo.ClientID;
                                entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                new VisitingObjectVisitingParameterMappingBLL(CurrentUserInfo).Create(entity, tran);
                            }
                        }
                        //ɾ��
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
                            this._currentDAO.DeleteObjectParameterIn(objid.ToString(),
                                sbDelList.Remove(sbDelList.ToString().Length - 1, 1).ToString(), tran);
                        }
                    }
                    else if (allSelectorStatus == 1)//ȫѡ
                    {
                        //���
                        string[] excludeLists = excludeList.Split(',');
                        for (int i = 0; i < oldList.ToArray().Length; i++)
                        {
                            if (oldList[i].MappingID == null || string.IsNullOrEmpty(oldList[i].MappingID.ToString()))
                            {
                                if (!excludeLists.Contains(oldList[i].VisitingParameterID.ToString()))
                                {
                                    VisitingObjectVisitingParameterMappingEntity entity = new VisitingObjectVisitingParameterMappingEntity();
                                    entity.VisitingParameterID = oldList[i].VisitingParameterID;
                                    entity.VisitingObjectID = objid;
                                    entity.ParameterOrder = 0;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingObjectVisitingParameterMappingBLL(CurrentUserInfo).Create(entity, tran);
                                }
                            }
                        }
                        //ɾ��
                        if (excludeList != "")
                        {
                            this._currentDAO.DeleteObjectParameterIn(
                                objid.ToString(),
                                "'" + excludeList.Replace(",", "','") + "'",
                                tran);
                        }
                    }
                    else if (allSelectorStatus == 2)//ȫ��ѡ
                    {
                        //ɾ��
                        if (includeList != "")
                        {
                            this._currentDAO.DeleteObjectParameterNotIn(objid.ToString(),
                                "'" + includeList.Replace(",", "','") + "'"
                                , tran);
                        }
                        else
                        {
                            this._currentDAO.DeleteObjectParameterAll(objid.ToString(), tran);
                        }
                        //���
                        if (includeList.Trim().Length > 0)
                        {
                            string[] includeLists = includeList.Split(',');
                            foreach (string pid in includeLists)
                            {
                                if (oldList.Where(m =>
                                    m.MappingID != null
                                    && m.VisitingObjectID == objid
                                    && m.VisitingParameterID == Guid.Parse(pid)).ToArray().Length == 0)
                                {
                                    VisitingObjectVisitingParameterMappingEntity entity = new VisitingObjectVisitingParameterMappingEntity();
                                    entity.VisitingParameterID = Guid.Parse(pid);
                                    entity.VisitingObjectID = objid;
                                    entity.ParameterOrder = 0;
                                    entity.ClientID = CurrentUserInfo.ClientID;
                                    entity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                                    new VisitingObjectVisitingParameterMappingBLL(CurrentUserInfo).Create(entity, tran);
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
                    //�޸�����
                    List<VisitingParameterViewEntity> oldList = this.GetObjectParameterList(parameterEntity, 1, 100000, out rowcount).ToList();
                    foreach (VisitingObjectVisitingParameterMappingEntity uEntity in updateEntity)
                    {
                        if (oldList.Where(m => m.VisitingParameterID == uEntity.VisitingParameterID && m.MappingID != null).ToArray().Length == 1)
                        {
                            VisitingParameterViewEntity entity = oldList.Where(m => m.VisitingParameterID == uEntity.VisitingParameterID && m.MappingID != null).ToArray()[0];

                            VisitingObjectVisitingParameterMappingEntity mEntity = new VisitingObjectVisitingParameterMappingEntity();
                            mEntity.MappingID = entity.MappingID;
                            mEntity.VisitingObjectID = entity.VisitingObjectID;
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