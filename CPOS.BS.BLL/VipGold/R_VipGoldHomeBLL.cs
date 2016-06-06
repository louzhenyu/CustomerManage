/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/18 14:40:25
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
    public partial class R_VipGoldHomeBLL
    {
        /// <summary>
        /// 查找最新一条记录 作为会员主页的 展示报表
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetReceiveRecodsByCustomerId(string customerId)
        {
            return _currentDAO.GetReceiveRecodsByCustomerId(customerId);
        }
    }
}