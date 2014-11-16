using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 仓库数据方法集
    /// </summary>
    public class WarehouseService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public WarehouseService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region
        /// <summary>
        /// 根据组织获取仓库
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public DataSet GetWarehouseByUnitID(string unitID)
        {
            DataSet ds = new DataSet();
            string sql = GetSql("") + " and b.unit_id = '" + unitID + "' and a.wh_status = '1' ;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 公共sql
        private string GetSql(string sql)
        { 
            sql = "select a.warehouse_id, a.wh_code, a.wh_name, a.wh_name_en, a.wh_address, "
                  + " a.wh_contacter, a.wh_tel, a.wh_fax, a.wh_status, a.wh_remark, a.is_default, "
                  + " a.create_user_id, a.create_user_name, a.create_time, "
                  + " a.modify_user_id, a.modify_user_name, a.modify_time, a.sys_modify_stamp, "
                  + " case a.wh_status when 1 then '正常' else  '停用' end as wh_status_desc, "
                  + " case a.is_default when 1 then '是' else '否' end as is_default_desc, "
                  + " c.unit_id, c.unit_name, c.unit_code, c.unit_name_short "
                  + " from t_warehouse a, t_unit_warehouse b, t_unit c "
                  + " where a.warehouse_id=b.warehouse_id and b.unit_id=c.unit_id ";
          return sql;
        }
        #endregion
    }
}
