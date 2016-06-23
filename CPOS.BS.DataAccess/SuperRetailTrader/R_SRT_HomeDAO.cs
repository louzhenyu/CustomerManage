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
    /// 表R_SRT_Home的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_SRT_HomeDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_HomeEntity>, IQueryable<R_SRT_HomeEntity>
    {
        public R_SRT_HomeEntity GetNearest1DayEntity()
        {
            if (CurrentUserInfo == null)
                return null;
            string sql = @"select top 1 * from R_SRT_Home where DateCode=(
select DISTINCT top 1 DateCode from R_SRT_Home order by datecode desc) and CustomerId=@customerId";
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerId", CurrentUserInfo.ClientID));
            //读取数据
            R_SRT_HomeEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), pList.ToArray()))
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
        /// <summary>
        /// 获取7天会员、店员相关数据（销量/新增分销商人数）
        /// </summary>
        /// <returns></returns>
        public DataSet GetSevenDaySalesAndPersonCount()
        {
            string sql = string.Format(@"
             SELECT  a.DateStr ,
                ISNULL(DayUserRTSalesAmount,'0.00') as DayUserRTSalesAmount,ISNULL(DayVipRTSalesAmount,'0.00') as DayVipRTSalesAmount,
				ISNULL(DayAddUserRTCount,0) AS DayAddUserRTCount,ISNULL(DayAddVipRTCount,0) AS DayAddVipRTCount
                FROM    (                 
                          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -1, GETDATE()), 112) AS DateStr
                          UNION ALL
                          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -2, GETDATE()), 112) AS DateStr
                          UNION ALL
                          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -3, GETDATE()), 112) AS DateStr
                          UNION ALL
                          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -4, GETDATE()), 112) AS DateStr
				          UNION ALL
				          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -5, GETDATE()), 112) AS DateStr
				          UNION ALL
				          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -6, GETDATE()), 112) AS DateStr
				          UNION ALL
				          SELECT    CONVERT(VARCHAR(20), DATEADD(d, -7, GETDATE()), 112) AS DateStr
				          )AS a
                LEFT JOIN ( SELECT  CONVERT(VARCHAR(20), DateCode, 112) AS DateStr ,
                                    DayUserRTSalesAmount,DayVipRTSalesAmount,DayAddUserRTCount,DayAddVipRTCount
                            FROM    R_SRT_Home
                            WHERE   1 = 1 and CustomerId='" + CurrentUserInfo.ClientID + "'  GROUP BY CONVERT(VARCHAR(20), DateCode, 112),DayUserRTSalesAmount,DayVipRTSalesAmount,DayAddUserRTCount,DayAddVipRTCount ) b ON a.DateStr = b.DateStr ");
            return this.SQLHelper.ExecuteDataset(sql);
        }

    }
}
