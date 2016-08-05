/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 17:09:07
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
    /// 表R_Withdraw_Home的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_Withdraw_HomeDAO : Base.BaseCPOSDAO, ICRUDable<R_Withdraw_HomeEntity>, IQueryable<R_Withdraw_HomeEntity>
    {
        /// <summary>
        /// 根据商户编码和 会员类型 获取最近一条统计信息
        /// </summary>
        /// <param name="CustomerId">商户编码</param>
        /// <param name="VipTypeId">1=会员 2=员工 3=旧分销商 4=超级分销商</param>
        /// <returns></returns>
        public R_Withdraw_HomeEntity GetTopListByCustomer(string CustomerId, int VipTypeId)
        {
            string sql = @"  SELECT TOP 1 * FROM R_Withdraw_Home WHERE VipType=@VipTypeId  AND CustomerId=@customerId ORDER BY DateCode desc  ";
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerId", CustomerId));
            pList.Add(new SqlParameter("@VipTypeId", VipTypeId));
            //执行SQL
            List<R_Withdraw_HomeEntity> list = new List<R_Withdraw_HomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql, pList.ToArray()))
            {
                while (rdr.Read())
                {
                    R_Withdraw_HomeEntity m;
                    this.Load(rdr, out m);
                    return m;
                }
            }
            //返回结果
            return new R_Withdraw_HomeEntity();
        }
    }
}
