/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:49
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
    /// 表R_SRT_RTProductTop的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_SRT_RTProductTopDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_RTProductTopEntity>, IQueryable<R_SRT_RTProductTopEntity>
    {
        /// <summary>
        /// 获取商品排行信息
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="SortName"></param>
        /// <param name="SortOrder"></param>
        /// <returns></returns>
        public List<R_SRT_RTProductTopEntity> GetSRT_RTProductTopList(string CustomerID, string SortName, string SortOrder)
        {

            StringBuilder strSbSql = new StringBuilder();
            SqlParameter[] parameters = new SqlParameter[]{
             new SqlParameter("@CustomerId",CustomerID)
            };
            strSbSql.Append(@"select a.* from R_SRT_RTProductTop as a  ");
            strSbSql.Append("Where 1=1  AND a.DateCode in (SELECT TOP 1 DateCode from R_SRT_RTProductTop WHERE CustomerId=@CustomerId ORDER BY DateCode DESC)");
            strSbSql.Append("ORDER BY " + SortName + " " + SortOrder);           

            List<R_SRT_RTProductTopEntity> result = new List<R_SRT_RTProductTopEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, strSbSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    R_SRT_RTProductTopEntity m;
                    this.Load(rdr, out m);
                    result.Add(m);
                }
            }

            return result;
        }
    }
}
