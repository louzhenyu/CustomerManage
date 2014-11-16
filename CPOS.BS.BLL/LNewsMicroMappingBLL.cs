/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/15 13:37:22
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
    public partial class LNewsMicroMappingBLL
    {
        #region 获取微刊资讯关联列表
        /// <summary>
        /// 获取微刊资讯关联列表
        /// </summary>
        /// <param name="microNumberId">刊号ID</param>
        /// <param name="microTypeId">类别ID</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortOrder">排序方式：0 升序 1 降序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public DataTable GetNewsMappList(string microNumberId, string microTypeId, string sortField, int sortOrder, int pageIndex, int pageSize, ref int rowCount, ref int pageCount)
        {
            DataSet ds = _currentDAO.GetNewsMappList(microNumberId, microTypeId, sortField, sortOrder, pageIndex, pageSize);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return ds.Tables[0];
        }
        
        #endregion

        #region 设置资讯微刊关联列表
        /// <summary>
        /// 设置资讯微刊关联列表
        /// </summary>
        /// <param name="numberId">刊号ID</param>
        /// <param name="typeId">类别ID</param>
        /// <param name="newsIds">资讯ID</param>
        /// <returns>受影响的行数</returns>
        public int SetNewsMap(string numberId, string typeId, string newsIds)
        {
            if (string.IsNullOrEmpty(newsIds))
            {
                return 0;
            }
            return _currentDAO.SetNewsMap(numberId, typeId, newsIds.Split(','));
        } 
        #endregion

        #region 刊号、类别统计：关系映射表
        /// <summary>
        /// 刊号、类别统计
        /// 该方法已废弃，现在的TypeCount根据关联表统计所得 by yehua
        /// </summary>
        /// <param name="numberId">刊号Id</param>
        /// <param name="typeId">类别Id</param>
        /// <returns>返回受影响的行数</returns>
        public int MicroNumberTypeCollect(string numberId, string typeId)
        {
            return _currentDAO.MicroNumberTypeCollect(numberId, typeId);
        } 
        #endregion

    }
}