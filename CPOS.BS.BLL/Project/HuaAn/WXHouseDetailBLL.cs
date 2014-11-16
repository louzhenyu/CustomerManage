using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 楼盘详细业务逻辑扩展类定义。
    /// </summary>
    public partial class WXHouseDetailBLL
    {
        /// <summary>
        /// 根据楼盘详细ID获取该楼盘详细信息。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="houseDetaliID"></param>
        /// <returns></returns>
        public WXHouseDetailEntity GetDetailByID(string customerID, string detailID)
        {
            DataSet ds = this._currentDAO.GetDetails(customerID, detailID);
            WXHouseDetailEntity entity = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                entity = DataTableToObject.ConvertToObject<WXHouseDetailEntity>(ds.Tables[0].Rows[0]);
            }
            return entity;
        }

       /// <summary>
        /// 根据楼盘详细ID获取该楼盘详细信息。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public DataSet GetHouseDetailByDetailID(string customerID, string detailID)
        {
            return this._currentDAO.GetHouseDetailByDetailID(customerID, detailID);
        }
    }
}
