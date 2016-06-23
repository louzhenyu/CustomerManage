/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/1 19:09:45
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class R_SRT_RTTopBLL
    {
        /// <summary>
        /// 获取 分销商 排名统计
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">煤业显示条数</param>
        /// <param name="BusiType">排序名称</param>
        /// <param name="CustomerID">商户编号</param>
        /// <param name="SortOrder">排序方式</param>
        /// <returns></returns>
        public List<R_SRT_RTTopEntity> GetRsrtrtTopList(string CustomerID, string BusiType)
        {
            return _currentDAO.GetRsrtrtTopList(CustomerID, BusiType);
        }
    }
}