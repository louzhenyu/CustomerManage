using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
namespace JIT.CPOS.BS.BLL
{
    public partial class WXHouseNewRateBLL
    {
        #region GetWXHouseNewRate
        /// <summary>
        /// 获取最近一周年化率
        /// </summary>
        /// <returns></returns>
        public DataSet GetWXHouseNewRate(string customerID)
        {
            return this._currentDAO.GetWXHouseNewRate(customerID);
        }
        #endregion

        /// <summary>
        /// 删除历史年化收益率数据。
        /// </summary>
        /// <param name="customerID"></param>
        public void Delete(string customerID)
        {
            _currentDAO.Delete(customerID);
        }
    }
}
