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
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{
    public class TypeService : Base.BaseCPOSDAO
    {
         #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public TypeService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region
        public DataSet GetTypeInfoListByDomain(string type_domaion)
        {
            string sql = "select a.type_id "
                      + " ,a.type_code "
                      + " ,a.type_name "
                      + " ,a.type_name_en "
                      + " ,a.type_name_en "
                      + " ,a.type_domain "
                      + " ,a.type_system_flag "
                      + " ,a.status "
                      + " ,case when a.type_system_flag = '1' then '是' else '否' end type_system_flag_desc "
                      + " ,case when a.status = '1' then '正常' else '删除' end status_desc "
                      + " From t_type a where 1=1 "
                      + " and a.[status] = 1 "
                      + " and a.type_domain = '"+type_domaion+"';";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetTypeInfoById(string type_id)
        {
            string sql = "select a.type_id "
                      + " ,a.type_code "
                      + " ,a.type_name "
                      + " ,a.type_name_en "
                      + " ,a.type_name_en "
                      + " ,a.type_domain "
                      + " ,a.type_system_flag "
                      + " ,a.status "
                      + " ,case when a.type_system_flag = '1' then '是' else '否' end type_system_flag_desc "
                      + " ,case when a.status = '1' then '正常' else '删除' end status_desc "
                      + " From t_type a where 1=1 "
                      + " and a.[status] = 1 "
                      + " and a.type_id = '" + type_id + "';";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

    }
}
