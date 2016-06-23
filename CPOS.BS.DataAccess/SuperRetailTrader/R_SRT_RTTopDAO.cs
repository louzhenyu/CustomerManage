/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/1 19:09:45
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
    /// 表R_SRT_RTTop的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_SRT_RTTopDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_RTTopEntity>, IQueryable<R_SRT_RTTopEntity>
    {
        /// <summary>
        /// 获取 分销商 排名统计
        /// </summary>
        /// <param name="BusiType">1=30天销售排名 2=30天新增下线排名</param>
        /// <param name="CustomerID">商户编号</param>
        /// <param name="SortOrder">排序方式</param>
        /// <returns></returns>
        public List<R_SRT_RTTopEntity> GetRsrtrtTopList(string CustomerID, string BusiType)
        {

            StringBuilder pagedSql = new StringBuilder();
            pagedSql.Append(@"select TOP 10 a.* from R_SRT_RTTop as a  ");
            pagedSql.Append("Where 1=1  AND a.DateCode=(SELECT TOP 1 DateCode from R_SRT_RTTop WHERE CustomerId=@CustomerId and TopType=1 AND BusiType=@BusiType ORDER BY DateCode DESC)");
            pagedSql.Append(" AND BusiType=@BusiType AND CustomerId=@CustomerId AND TopType=1 ");
            SqlParameter[] pagedparameter = new SqlParameter[]{
             new SqlParameter("@CustomerId",CustomerID),
             new SqlParameter("@BusiType",BusiType)
            };

            List<R_SRT_RTTopEntity> result = new List<R_SRT_RTTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), pagedparameter))
            {
                while (rdr.Read())
                {
                    R_SRT_RTTopEntity m;
                    this.Load(rdr, out m);
                    result.Add(m);
                }
            }

            return result;
        }
    }
}