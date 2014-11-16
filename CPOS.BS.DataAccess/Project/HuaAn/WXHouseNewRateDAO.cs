using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;


namespace JIT.CPOS.BS.DataAccess
{
    public partial class WXHouseNewRateDAO : BaseCPOSDAO
    {
        #region GetWXHouseNewRate
        /// <summary>
        /// 获取最近一周年化率
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public DataSet GetWXHouseNewRate(string customerID)
        {
            string sql = string.Format(@"select top 7 Bonusbefdate,Bonusbefamt,Bonusbefratio  from WXHouseNewRate where CustomerID='{0}' and IsDelete='0' order by BonusBefDate desc", customerID);
            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 删除历史年化收益率数据。
        /// </summary>
        /// <param name="customerID"></param>
        public void Delete(string customerID)
        {
            string sql = string.Format("delete from WXHouseNewRate where customerID='{0}'", customerID);
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        #endregion
    }
}
