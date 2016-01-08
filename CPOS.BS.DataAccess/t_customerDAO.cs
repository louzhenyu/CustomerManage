/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/17 14:26:19
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
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表t_customer的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class t_customerDAO : Base.BaseCPOSDAO, ICRUDable<t_customerEntity>, IQueryable<t_customerEntity>
    {
        public t_customerEntity GetCustomer(string customerCode)
        {
            string sql = string.Format("select * from dbo.t_customer where customer_code='{0}'"
                ,customerCode.Replace("'","''"));
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            using (SqlDataReader rdr = sqlHelper.ExecuteReader(sql))
            {
                if(rdr.Read())
                {
                    t_customerEntity m;
                    this.Load(rdr, out m);
                    return m;
                }
            }
            return null;
        }
        /// <summary>
        /// 根据CustomerId获取商户信息
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public t_customerEntity GetByCustomerID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where customer_id='{0}'  ", id.ToString());
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            //读取数据
            t_customerEntity m = null;
            using (SqlDataReader rdr = sqlHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }
    }
}
