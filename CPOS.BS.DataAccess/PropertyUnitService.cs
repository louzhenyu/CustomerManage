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
    public class PropertyUnitService : Base.BaseCPOSDAO
    {
         #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public PropertyUnitService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        /// <summary>
        /// 获取组织的属性集合
        /// </summary>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public DataSet GetUnitPropertyListByUnit(string unitId)
        {
            string sql = "select a.unit_property_id Id"
                      + " ,a.unit_id UnitId"
                      + " ,(select prop_id From t_prop where t_prop.prop_id = b.parent_prop_id) PropertyCodeGroupId "
                      + " ,(select prop_code From t_prop where t_prop.prop_id = b.parent_prop_id) PropertyCodeGroupCode "
                      + " ,(select prop_name From t_prop where t_prop.prop_id = b.parent_prop_id) PropertyCodeGroupName "
                      + " ,a.property_id PropertyCodeId "
                      + " ,b.prop_name PropertyCodeName "
                      + " ,b.prop_code PropertyCodeCode "
                      + " ,(select prop_id From t_prop where t_prop.prop_id = a.property_value) PropertyDetailId "
                      + " ,case when (select prop_code From t_prop where t_prop.prop_id = a.property_value) is null "
                      + " then a.property_value " 
                      + " else (select prop_code From t_prop where t_prop.prop_id = a.property_value) "
                      + " end PropertyDetailCode "
                      + " ,case when (select prop_name From t_prop where t_prop.prop_id = a.property_value) is null "
                      + " then a.property_value "
                      + " else (select prop_name From t_prop where t_prop.prop_id = a.property_value) "
                      + " end PropertyDetailName "
                      + " ,a.Status "
                      + " From T_Unit_Property a "
                      + " inner join T_Prop b "
                      + " on(a.property_id = b.prop_id) "
                      + " where 1=1 "
                      + " and a.[status] = '1' "
                      + " and b.[status] = '1' "
                      + "   and a.unit_id = '" + unitId + "'";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
    }
}
