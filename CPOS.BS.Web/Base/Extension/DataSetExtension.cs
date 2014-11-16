using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace JIT.CPOS.BS.Web.Base.Extension
{
    public static class DataSetExtension
    {
        //通用方法
        #region 验证ds的方法
        /// <summary>
        /// 验证ds dtable的方法
        /// </summary>
        /// <param name="ds">dataset</param>
        /// <param name="index">datatableindex</param>
        /// <returns></returns>
        public static bool CheckDataSet(this DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 验证dtable的方法
        /// <summary>
        /// 验证ds dtable的方法
        /// </summary>
        /// <returns></returns>
        public static bool CheckDataTable(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}