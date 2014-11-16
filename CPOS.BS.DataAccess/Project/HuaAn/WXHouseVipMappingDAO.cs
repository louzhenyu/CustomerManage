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
using System.Data.SqlClient;

namespace JIT.CPOS.BS.DataAccess
{
    public partial class WXHouseVipMappingDAO : BaseCPOSDAO
    {
        #region VerifWXHouseVipMapping
        /// <summary>
        /// 验证会员楼盘明细映射表记录是否存在
        /// </summary>
        /// <param name="vipID"></param>
        /// <param name="houseDetailID"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public DataSet VerifWXHouseVipMapping(string vipID, string houseDetailID, string customerID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from WXHouseVipMapping ");
            sb.Append(" where CustomerID=@customerID and VIPID=@vipID");
            if (!string.IsNullOrEmpty(houseDetailID))
            {
                sb.Append(" and DetailID=@housedetailID ");
            }
            sb.Append(" and IsDelete='0'");

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@customerID",customerID),
                new SqlParameter("@vipID",vipID)
            };

            if (!string.IsNullOrEmpty(houseDetailID))
            {
                para.Add(new SqlParameter("@housedetailID", houseDetailID));
            }

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sb.ToString(), para.ToArray());

            return ds;
        }

        #endregion
    }
}
