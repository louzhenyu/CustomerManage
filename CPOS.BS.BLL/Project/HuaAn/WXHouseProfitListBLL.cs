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
    public partial class WXHouseProfitListBLL
    {
        #region 我的收益
        /// <summary>
        /// 获取我的收益
        /// </summary>
        /// <returns></returns>
        public DataSet GetWXHouseProfitList(string pAssignbuyer, string customerID)
        {
            return _currentDAO.GetWXHouseProfitList(pAssignbuyer, customerID);
        }
        #endregion

        /// <summary>
        /// 批了删除历史收益
        /// </summary>
        public int Delete()
        {
            return _currentDAO.Delete();
        }
    }
}
