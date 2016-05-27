using CPOS.Common.Core;
using JIT.Utility.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    public class CustomerBLL : LazyInstance<CustomerBLL>
    {
        /// <summary>
        /// 获取所有商户ID
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetCustomerList()
        {
            var Result = new Dictionary<string, string>();
            string sql = @"select a.customer_id as CustomerID,'user id='+b.db_user+';password='+b.db_pwd+';data source='+b.db_server+';database='+b.db_name+';' as conn 
                        from  cpos_ap..t_customer as a left join cpos_ap..t_customer_connect as b on a.customer_id=b.customer_id 
						where a.customer_status=1";

            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            DataSet ds = sqlHelper.ExecuteDataset(sql);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Result.Add(ds.Tables[0].Rows[i]["CustomerID"].ToString(), ds.Tables[0].Rows[i]["conn"].ToString());
                }
            }
            return Result;
        }
    }
}
