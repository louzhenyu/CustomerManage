/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 10:02:29
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
    /// 数据访问： 下拉选项枚举 
    /// 表Options的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class OptionsDAO : Base.BaseCPOSDAO, ICRUDable<OptionsEntity>, IQueryable<OptionsEntity>
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="pOptionName"></param>
        /// <returns></returns>
        public DataTable GetCategory(string pOptionName)
        {
            //MLCourseCategory
            string sql = "SELECT OptionValue AS CategoryID, OptionText AS CategoryName,OptionTextEn AS CategoryNameEn,CustomerID ";
            sql += " FROM Options WHERE  OptionName=@OptionName AND CustomerID=@CustomerID AND IsDelete=0";

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerID", this.CurrentUserInfo.CurrentUser.customer_id));
            param.Add(new SqlParameter("@OptionName", pOptionName));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, param.ToArray());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
    }
}
