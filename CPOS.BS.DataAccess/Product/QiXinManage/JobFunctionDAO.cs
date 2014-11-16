/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 16:26:26
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
    /// 表JobFunction的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class JobFunctionDAO : Base.BaseCPOSDAO, ICRUDable<JobFunctionEntity>, IQueryable<JobFunctionEntity>
    {
        #region 获取职衔-qxht
        /// <summary>
        /// 获取职衔-qxht
        /// </summary>
        /// <param name="pJobFuncID"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <param name="totalPage"></param>
        /// <returns></returns>
        public DataTable GetJobFuncList(string pJobFuncID, int pPageIndex, int pPageSize, string pJonFuncName, out int totalPage)
        {
            int begin = pPageIndex * pPageSize + 1;
            int end = (pPageIndex + 1) * pPageSize;

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalSql = new StringBuilder();

            pagedSql.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER");
            totalSql.AppendFormat("select count(1) ");

            pagedSql.AppendFormat(" (ORDER BY name) AS rowid,JobFunctionID,Name,Description,Status");

            string commSql = " FROM JobFunction WHERE CustomerID=@CustomerID AND IsDelete=0 ";
            pagedSql.AppendFormat(commSql);
            totalSql.AppendFormat(commSql);

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));

            if (!string.IsNullOrEmpty(pJobFuncID))
            {
                totalSql.AppendFormat(" AND JobFunctionID=@JobFunctionID ");
                pagedSql.AppendFormat(" AND JobFunctionID=@JobFunctionID ");
                param.Add(new SqlParameter("@JobFunctionID", pJobFuncID));
            }

            if (!string.IsNullOrEmpty(pJonFuncName))
            {
                totalSql.AppendFormat(" AND Name like @JobFunctionName ");
                pagedSql.AppendFormat(" AND Name like @JobFunctionName ");
                param.Add(new SqlParameter("@JobFunctionName", "%" + pJonFuncName + "%"));
            }

            pagedSql.AppendFormat(") tt WHERE tt.rowid BETWEEN @begin AND @end ");

            try
            {
                //计算总行数PageCount
                int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, totalSql.ToString(), param.ToArray()));
                int remainder = 0;
                //总页数totalPage
                totalPage = Math.DivRem(totalCount, pPageSize, out remainder);
                if (remainder > 0)
                    totalPage++;

                param.Add(new SqlParameter("@begin", begin));
                param.Add(new SqlParameter("@end", end));
                DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, pagedSql.ToString(), param.ToArray());
                if (ds != null)
                    return ds.Tables[0];
            }
            catch (Exception e)
            {
                totalPage = 0;
            }
            return null;
        }
        #endregion
    }
}
