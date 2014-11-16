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
    /// 业务处理： 自定义拜访对象 
    /// </summary>
    public partial class VisitingObjectBLL
    {
        #region GetList
        public VisitingObjectViewEntity[] GetList(VisitingObjectViewEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && entity.ObjectGroup != null && Convert.ToInt32(entity.ObjectGroup) > 0)
            {
                wheres.Add(new EqualsCondition() { FieldName = "ObjectGroup", Value = entity.ObjectGroup });
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ObjectName))
            {
                wheres.Add(new LikeCondition() { FieldName = "ObjectName", HasLeftFuzzMatching = true, HasRightFuzzMathing = true, Value = entity.ObjectName });
            }
            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "ObjectGroup", Direction = OrderByDirections.Desc });
            orderbys.Add(new OrderBy() { FieldName = "Sequence", Direction = OrderByDirections.Asc });

            PagedQueryResult<VisitingObjectViewEntity> pEntity = this._currentDAO.GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);

            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        #region DeleteObject
        public void DeleteObject(Guid oid)
        {
            IDbTransaction tran = new TransactionHelper(this.CurrentUserInfo).CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    this._currentDAO.DeleteObject(oid, tran);

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

        #region GetParentObject
        /// <summary>
        /// 获取所有自定义对象
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public VisitingObjectEntity[] GetParentObject(Guid? oid)
        {
            int rowcount = 0;
            List<VisitingObjectViewEntity> list = this.GetList(null, 1, 1000000, out rowcount).ToList();
            if (oid != null)
            {
                //去除本身
                var query = list.Where(m => m.VisitingObjectID == oid);
                if (query.Count() == 1)
                {
                    list.Remove(query.ToArray()[0]);
                }
            }
            return list.ToArray();
        }
        #endregion
    }
}