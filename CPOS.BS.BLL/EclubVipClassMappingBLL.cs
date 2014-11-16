/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:57
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
    public partial class EclubVipClassMappingBLL
    {

        #region  获取实例
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public EclubVipClassMappingEntity GetModel(string strWhere)
        {
            return _currentDAO.GetModel(strWhere);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public EclubVipClassMappingEntity GetModel_V1(string strWhere)
        {
            return _currentDAO.GetModel_V1(strWhere);
        }
        #endregion

    }
}