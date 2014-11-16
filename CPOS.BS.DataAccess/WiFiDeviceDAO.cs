/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表WiFiDevice的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WiFiDeviceDAO : Base.BaseCPOSDAO, ICRUDable<WiFiDeviceEntity>, IQueryable<WiFiDeviceEntity>
    {

        #region  根据节点编号获取实例
        /// <summary>
        /// 根据节点编号获取实例
        /// </summary>
        /// <param name="NodeSn">节点编号</param>
        /// <returns></returns>
        public WiFiDeviceEntity GetByNodeSn(string NodeSn)
        {
            //参数检查
            if (string.IsNullOrEmpty(NodeSn))
                return null;
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WiFiDevice] where NodeSn='{0}' and IsDelete=0 ", NodeSn);
            //读取数据
            WiFiDeviceEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
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
        #endregion


    }
}
