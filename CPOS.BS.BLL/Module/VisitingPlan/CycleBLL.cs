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
    /// 业务处理： 循环定义 
    /// </summary>
    public partial class CycleBLL
    {
        #region GetCycleList
        /// <summary>
        /// 获取所有周期列表,for combobox
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
        /// 删除周期
        /// </summary>
        /// <param name="cycleID"></param>
        public void Delete(Guid cycleID, out string res)
        {
            #region 删除限制判断
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
        /// 编辑周期信息
        /// </summary>
        public void Edit(CycleEntity entity, string selectedList)
        {
            bool isUpdate = false;
            isUpdate = (entity.CycleID != null);
            CycleDetailEntity[] oldDetailEntity = null;//数据库中的周期详细
            if (isUpdate)
            {
                oldDetailEntity = new CycleDetailDAO(CurrentUserInfo).GetCycleDetailByCID(entity.CycleID.Value);
            }

            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    #region 周期
                    if (isUpdate)
                    {
                        this._currentDAO.Update(entity, tran);
                    }
                    else
                    {
                        this._currentDAO.Create(entity, tran);
                    }
                    #endregion

                    #region 周期详细

                    List<Guid> frontSelectedIDS=new List<Guid>();//已有的ID
                    List<int> createList=new List<int>();//新增的day
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
                        //数据库已有id
                        IEnumerable<Guid> selectedIDS = oldDetailEntity.Select(x => x.CycleDetailID.Value);
                        //需要新增、删除的id
                        IEnumerable<Guid> deletedList = selectedIDS.Except(frontSelectedIDS);
                        
                        //删除
                        foreach (Guid id in deletedList)
                        {
                            Guid detailID = oldDetailEntity.Where(x => x.CycleDetailID == id).Select(x => x.CycleDetailID.Value).ToList()[0];
                            new CycleDetailBLL(CurrentUserInfo).Delete(detailID, tran);
                        }

                        //新增
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
                        //新增
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