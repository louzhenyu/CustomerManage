/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/23 17:41:01
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
    public partial class EclubMicroTypeBLL
    {
        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public EclubMicroTypeEntity[] MicroIssueTypeGet(EclubMicroTypeEntity pQueryEntity)
        {
            OrderBy[] pOrderBys = new OrderBy[] { 
                new OrderBy { FieldName = "Sequence", Direction = OrderByDirections.Asc } 
            };
            if (string.IsNullOrEmpty(pQueryEntity.ParentID))
            {
                pQueryEntity.ParentID = null;
                return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys).Where(r => string.IsNullOrEmpty(r.ParentID)).ToArray();
            }
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }
        /// <summary>
        /// 获取板块列表
        /// </summary>
        /// <param name="typeEn">实体</param>
        /// <returns></returns>
        public DataTable GetMicroTypes(EclubMicroTypeEntity typeEn)
        {
            return _currentDAO.GetMicroTypes(typeEn).Tables[0];
        }

        /// <summary>
        /// 获取板块列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortOrder">排序方式：0 升序 1 降序</param>
        /// <returns></returns>
        public DataTable GetMicroTypeList(int sortOrder, string sortField, int pageIndex, int pageSize, ref int pageCount, ref int rowCount)
        {
            DataSet ds = _currentDAO.GetMicroTypeList(sortOrder, sortField, pageIndex, pageSize);
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            rowCount = 0;
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;

            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取板块详细：包含父类信息
        /// </summary>
        /// <param name="typeEn">板块实体信息</param>
        /// <returns></returns>
        public DataTable GetMicroTypeDtail(EclubMicroTypeEntity typeEn)
        {
            return _currentDAO.GetMicroTypeDtail(typeEn).Tables[0] ?? null;
        }

        /// <summary>
        /// 根据期数版块关联获取已关联的版块列表
        /// by yehua
        /// </summary>
        public DataTable MicroIssueTypeGet(string numberId, string parentId, string typeLevel)
        {
            return _currentDAO.MicroIssueTypeGet(numberId, parentId, typeLevel);
        }
    }
}