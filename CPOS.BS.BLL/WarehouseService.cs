using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;
using JIT.CPOS.BS.Entity.Pos;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 仓库方法类
    /// </summary>
    public class WarehouseService : BaseService
    {
        JIT.CPOS.BS.DataAccess.WarehouseService warehouseService = null;
        #region 构造函数
        public WarehouseService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            warehouseService = new DataAccess.WarehouseService(loggingSessionInfo);
        }
        #endregion

        #region 查询仓库
        /// <summary>
        /// 查询某个单位下的仓库列表
        /// </summary>
        /// <param name="unitID">单位ID</param>
        /// <returns></returns>
        public IList<WarehouseInfo> GetWarehouseByUnitID(string unitID)
        {
            IList<WarehouseInfo> warehouseInfoList = new List<WarehouseInfo>();
            DataSet ds = new DataSet();
            ds = warehouseService.GetWarehouseByUnitID(unitID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                warehouseInfoList = DataTableToObject.ConvertToList<WarehouseInfo>(ds.Tables[0]);
            }
            return warehouseInfoList;
        }
        #endregion
    }
}
