/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
    public partial class WMaterialImageBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<WMaterialImageEntity> GetWebList(WMaterialImageEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WMaterialImageEntity> list = new List<WMaterialImageEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WMaterialImageEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebListCount(WMaterialImageEntity entity)
        {
            return _currentDAO.GetWebListCount(entity);
        }
        #endregion
    }
}