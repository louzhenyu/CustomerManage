using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 属性与组织关系
    /// </summary>
    public class PropertyUnitService:BaseService
    {
        JIT.CPOS.BS.DataAccess.PropertyUnitService propertyUnitService = null;
        #region 构造函数
         public PropertyUnitService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            propertyUnitService = new DataAccess.PropertyUnitService(loggingSessionInfo);
        }
        #endregion

        /// <summary>
        /// 获取组织与属性关系值
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IList<UnitPropertyInfo> GetUnitPropertyListByUnit(string unitId)
        {
            try
            {
                DataSet ds = propertyUnitService.GetUnitPropertyListByUnit(unitId);
                IList<UnitPropertyInfo> unitPropertyInfoList = new List<UnitPropertyInfo>();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    unitPropertyInfoList = DataTableToObject.ConvertToList<UnitPropertyInfo>(ds.Tables[0]);
                }
                return unitPropertyInfoList;
                //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UnitPropertyInfo>("PropertyUnit.SelectByUnitId", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
    }
}
