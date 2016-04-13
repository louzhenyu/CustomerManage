/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-06-08 20:59:54
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问：  
    /// 表T_User的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_UserDAO : Base.BaseCPOSDAO, ICRUDable<T_UserEntity>, IQueryable<T_UserEntity>
    {

        /// <summary>
        /// 得到店门用户
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <returns></returns>
        public T_UserEntity[] GetEntitiesByCustomerIdUnitId(string customerId, string unitId)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" SELECT u.*  ");
            sql.AppendFormat(" FROM t_user u INNER JOIN T_User_Role r ON u.user_id = r.user_id ");
            sql.AppendFormat(" where 1=1 ");
            sql.AppendFormat("           AND u.customer_id = '" + customerId + "' AND r.unit_id = '" + unitId + "'");
            sql.AppendFormat("           AND u.user_status = '1' AND r.status = 1 ");

            //执行SQL
            List<T_UserEntity> list = new List<T_UserEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_UserEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回结果
            return list.ToArray();
        }
    }
}
