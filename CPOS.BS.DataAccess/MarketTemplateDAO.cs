/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    /// 表MarketTemplate的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class MarketTemplateDAO : Base.BaseCPOSDAO, ICRUDable<MarketTemplateEntity>, IQueryable<MarketTemplateEntity>
    {
        #region
        /// <summary>
        /// 根据类型获取模板信息集合
        /// </summary>
        /// <param name="templateType"></param>
        /// <returns></returns>
        public DataSet GetTemplateListByType(string templateType)
        {
            DataSet ds = new DataSet();
            string sql = "SELECT a.* FROM dbo.MarketTemplate a WHERE a.IsDelete = 0 AND a.TemplateType='" + templateType + "';";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
