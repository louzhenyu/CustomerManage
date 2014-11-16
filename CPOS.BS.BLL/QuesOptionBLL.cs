/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/7 10:56:10
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
    public partial class QuesOptionBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public IList<QuesOptionEntity> GetWebQuesOptions(QuesOptionEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<QuesOptionEntity> list = new List<QuesOptionEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebQuesOptions(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<QuesOptionEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebQuesOptionsCount(QuesOptionEntity entity)
        {
            return _currentDAO.GetWebQuesOptionsCount(entity);
        }
        #endregion
    }
}