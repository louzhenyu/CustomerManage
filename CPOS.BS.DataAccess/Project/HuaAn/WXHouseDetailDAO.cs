using System.Collections.Generic;
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 获取楼盘详细。
    /// </summary>
    public partial class WXHouseDetailDAO
    {
        /// <summary>
        /// 根据楼盘详细ID获取该楼盘详细信息。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public DataSet GetDetails(string customerID, string detailID)
        {
            string sql = "select * from [WXHouseDetail] where  DetailID='{0}' and customerID='{1}' and isdelete=0";
            sql = string.Format(sql, detailID, customerID);
            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 根据楼盘详细ID获取该楼盘详细信息。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public DataSet GetHouseDetailByDetailID(string customerID, string detailID)
        {
            string sql = "select *  from WXHouseDetail detail inner join WXHouseBuild build on build.HouseID=detail.HouseID and build.isdelete=detail.isdelete and build.customerID=detail.customerID where detail.DetailID='{0}' and detail.customerID='{1}' and detail.isdelete='0'";
            sql = string.Format(sql, detailID, customerID);
            return this.SQLHelper.ExecuteDataset(sql);
        }
    }
}