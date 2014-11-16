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
    public partial class WXHouseVipMappingBLL
    {
        #region VerifWXHouseVipMapping
        /// <summary>
        /// 验证会员楼盘明细映射表记录是否存在
        /// </summary>
        /// <returns></returns>
        public DataSet VerifWXHouseVipMapping(string VIPID, string houseDetailID, string customerID)
        {
            return this._currentDAO.VerifWXHouseVipMapping(VIPID, houseDetailID, customerID);
        }


        /// <summary>
        /// 验证会员楼盘明细映射表记录是否存在。
        /// </summary>
        /// <returns></returns>
        public List<WXHouseVipMappingEntity> VerifWXHouseVipMapping(string VIPID, string customerID)
        {
            DataSet ds = this._currentDAO.VerifWXHouseVipMapping(VIPID, "", customerID);
            List<WXHouseVipMappingEntity> list = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WXHouseVipMappingEntity>(ds.Tables[0]);
            }

            return list;
        }


        #endregion
    }
}
