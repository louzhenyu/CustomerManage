using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.CPOS.BS.Entity.Pos;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class ItemPriceTypeService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public ItemPriceTypeService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region
        public DataSet GetItemPriceTypeList()
        {
            DataSet ds = new DataSet();
            string sql = "select item_price_type_id "
                      + " ,item_price_type_code "
                      + " ,item_price_type_name "
                      + " ,status "
                      + " ,create_time "
                      + " ,create_user_id "
                      + " ,modify_time "
                      + " ,modify_user_id  "
                      + " From T_Item_Price_Type a where a.status='1' ORDER BY create_user_id ";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetItemPriceTypeById(string item_price_type_id)
        {
            DataSet ds = new DataSet();
            string sql = "select item_price_type_id "
                      + " ,item_price_type_code "
                      + " ,item_price_type_name "
                      + " ,status "
                      + " ,create_time "
                      + " ,create_user_id "
                      + " ,modify_time "
                      + " ,modify_user_id  "
                      + " From T_Item_Price_Type a where a.item_price_type_id= '" + item_price_type_id  + "'";

            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

    }
}
