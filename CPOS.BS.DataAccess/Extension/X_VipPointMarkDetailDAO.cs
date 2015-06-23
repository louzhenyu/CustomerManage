/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-6 16:15:14
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
    /// 表X_VipPointMarkDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class X_VipPointMarkDetailDAO : Base.BaseCPOSDAO, ICRUDable<X_VipPointMarkDetailEntity>, IQueryable<X_VipPointMarkDetailEntity>
    {
        /// <summary>
        /// 本周获得点标个数（判断是否可继续答题；大于0表示本周有答题）
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="startWeek"></param>
        /// <param name="endWeek"></param>
        /// <returns></returns>
        public X_VipPointMarkDetailEntity GetPointMarkByWeek(string vipId, DateTime startWeek, DateTime endWeek)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [X_VipPointMarkDetail] where VipID='{0}' and CreateTime>'{1}' and CreateTime<'{2}' and Source=1 and isdelete=0 ", vipId, startWeek, endWeek);
            //读取数据
            X_VipPointMarkDetailEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            return m;
        }
    }
}
