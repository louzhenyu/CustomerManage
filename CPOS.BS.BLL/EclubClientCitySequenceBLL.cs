/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/19 13:53:56
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
    public partial class EclubClientCitySequenceBLL
    {

        #region 获取省市县数据
        /// <summary>
        /// 获取省市县数据
        /// </summary>
        /// <param name="cityType">1.为省，2.为市，3.为县</param>
        /// <param name="privinceName">省名称</param>
        /// <param name="cityName">市名称</param>
        /// <returns></returns>
        public DataSet GetCityList(int? cityType, string cityCode)
        {
            return _currentDAO.GetCityList(cityType, cityCode);
        }

         /// <summary>
        /// 获取省市县数据：修改
        /// </summary>
        /// <param name="cityType">1.为省，2.为市，3.为县</param>
        /// <param name="cityCode">编号</param>
        /// <returns></returns>
        public DataSet GetCityList_V1(int? cityType, string cityCode)
        {
            return _currentDAO.GetCityList_V1(cityType, cityCode);
        }
        #endregion

    }
}