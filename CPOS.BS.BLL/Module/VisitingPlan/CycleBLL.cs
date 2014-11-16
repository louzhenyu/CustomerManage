/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/3 14:19:42
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
    /// ҵ���� ѭ������ 
    /// </summary>
    public partial class CycleBLL
    {
        #region GetCycleList
        /// <summary>
        /// ��ȡ���������б�,for combobox
        /// </summary>
        /// <returns></returns>
        public CycleEntity[] GetCycleList()
        {
            return new CycleDAO(CurrentUserInfo).GetCycleList();
        }
        #endregion

        #region GetList
        public CycleEntity[] GetList(CycleEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> conditions = new List<IWhereCondition>();
            if (!string.IsNullOrEmpty(entity.CycleName))
            {
                conditions.Add(new EqualsCondition() { FieldName = "CycleName", Value = entity.CycleName });
            }
            conditions.Add(new EqualsCondition() { FieldName = "ClientID", Value = CurrentUserInfo.ClientID });
            conditions.Add(new EqualsCondition() { FieldName = "ClientDistributorID", Value = CurrentUserInfo.ClientDistributorID });

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Asc });

            PagedQueryResult<CycleEntity> pEntity = this._currentDAO.PagedQuery(conditions.ToArray(), orderbys.ToArray(), pageSize, pageIndex);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region Delete
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="cycleID"></param>
        public void Delete(Guid cycleID, out string res)
        {
            #region ɾ�������ж�
            if (new DataOperateBLL(CurrentUserInfo).CycleDeleteCheck(cycleID, out res) != 0)
            {
                return;
            }
            #endregion

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    this._currentDAO.Delete(cycleID, tran);

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

        #region Edit
        /// <summary>
        /// �༭������Ϣ
        /// </summary>
        public void Edit(CycleEntity entity, string selectedList)
        {
            bool isUpdate = false;
            isUpdate = (entity.CycleID != null);
            CycleDetailEntity[] oldDetailEntity = null;//���ݿ��е�������ϸ
            if (isUpdate)
            {
                oldDetailEntity = new CycleDetailDAO(CurrentUserInfo).GetCycleDetailByCID(entity.CycleID.Value);
            }

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    #region ����
                    if (isUpdate)
                    {
                        this._currentDAO.Update(entity, tran);
                    }
                    else
                    {
                        this._currentDAO.Create(entity, tran);
                    }
                    #endregion

                    #region ������ϸ

                    List<Guid> frontSelectedIDS=new List<Guid>();//���е�ID
                    List<int> createList=new List<int>();//������day
                    foreach(string id in 
                        selectedList.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries))
                    {
                        Guid gid;
                        if(Guid.TryParse(id,out gid))
                        {
                            frontSelectedIDS.Add(gid);
                        }
                        else
                        {
                            createList.Add(int.Parse(id));
                        }
                    }
                    
                    if (isUpdate)
                    {
                        //���ݿ�����id
                        IEnumerable<Guid> selectedIDS = oldDetailEntity.Select(x => x.CycleDetailID.Value);
                        //��Ҫ������ɾ����id
                        IEnumerable<Guid> deletedList = selectedIDS.Except(frontSelectedIDS);
                        
                        //ɾ��
                        foreach (Guid id in deletedList)
                        {
                            Guid detailID = oldDetailEntity.Where(x => x.CycleDetailID == id).Select(x => x.CycleDetailID.Value).ToList()[0];
                            new CycleDetailBLL(CurrentUserInfo).Delete(detailID, tran);
                        }

                        //����
                        foreach (int day in createList)
                        {
                            CycleDetailEntity detailEntity = new CycleDetailEntity();
                            detailEntity.CycleID = entity.CycleID;
                            detailEntity.DayOrder = detailEntity.DayOfCycle = day;
                            detailEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            detailEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            new CycleDetailDAO(CurrentUserInfo).Create(detailEntity, tran);
                        }
                    }
                    else
                    {
                        //����
                        foreach (int day in createList)
                        {
                            CycleDetailEntity detailEntity = new CycleDetailEntity();
                            detailEntity.CycleID = entity.CycleID;
                            detailEntity.DayOrder = detailEntity.DayOfCycle = day;
                            detailEntity.ClientID = Convert.ToInt32(CurrentUserInfo.ClientID);
                            detailEntity.ClientDistributorID = Convert.ToInt32(CurrentUserInfo.ClientDistributorID);
                            new CycleDetailDAO(CurrentUserInfo).Create(detailEntity, tran);
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
        }
        #endregion
    }
}