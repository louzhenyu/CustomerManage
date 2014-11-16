/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 15:41:40
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class EnterpriseMemberStructureBLL
    {
        #region GetList
        /// <summary>
        /// 获取拜访任务列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public EnterpriseMemberStructureEntity[] GetList(EnterpriseMemberStructureEntity entity, int pageIndex, int pageSize, out int rowCount)
        {
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (entity != null && entity.ParentID != null)
            {
                wheres.Add(new EqualsCondition { FieldName = "a.ParentID", Value = entity.ParentID });
            }

            List<OrderBy> orderbys = new List<OrderBy>();
            orderbys.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });

            PagedQueryResult<EnterpriseMemberStructureEntity> pEntity = new EnterpriseMemberStructureDAO(this.CurrentUserInfo).GetList(wheres.ToArray(), orderbys.ToArray(), pageIndex, pageSize);
            rowCount = pEntity.RowCount;
            return pEntity.Entities;
        }
        #endregion

        public IList<EnterpriseMemberStructureEntity> GetEnterpriseMemberStructureListByParentId(string parentID)
        {
            int rowCount;
            EnterpriseMemberStructureEntity entity = new EnterpriseMemberStructureEntity();
               
            if (!string.IsNullOrEmpty(parentID))
            {
                entity.ParentID = Guid.Parse(parentID);
            }
            return GetList(entity, 1, 10000, out rowCount);
        }
    }
}