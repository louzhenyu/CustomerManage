using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class WXHouseProfitListDAO : BaseCPOSDAO
    {
        #region 我的收益
        /// <summary>
        /// 获取我的收益
        /// </summary>
        /// <returns></returns>
        public DataSet GetWXHouseProfitList(string pAssignbuyer, string customerID)
        {
            //获取昨日收益（最新收益）、总金额、累计收益
            string sql = @"select sum(NewProfit) YesterdayProfit,sum(TotalAssetsMoney) TotalAssetsMoney,sum(GrandProfit) GrandProfit from WXHouseProfitList";
            sql += " where Assignbuyer='{0}' and CustomerID='{1}' and IsDelete='0'";
            sql += " and ThirdOrderNo not in";
            sql += " (select f.ThirdOrderNo from WXHouseProfitList f,WXHouseReservationPay r, WXHouseOrder o ";
            sql += " where f.Assignbuyer='{0}' and f.CustomerID='{1}' and f.IsDelete='0'";
            sql += " and f.ThirdOrderNo = o.ThirdOrderNo and o.PrePaymentID=r.PrePaymentID and r.FundState = 1)";
            sql += " and ThirdOrderNo not in";
            sql += " (select f.ThirdOrderNo from WXHouseProfitList f,WXHouseReservationRedeem r, WXHouseOrder o ";
            sql += " where f.Assignbuyer='{0}' and f.CustomerID='{1}' and f.IsDelete='0'";
            sql += " and f.ThirdOrderNo = o.ThirdOrderNo and o.PrePaymentID=r.PrePaymentID and r.FundState = 1)";
            sql = string.Format(sql, pAssignbuyer, customerID);
            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        public int Delete()
        {
            string sql = "delete from WXHouseProfitList";
            int result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);
            return result;
        }
    }
}
