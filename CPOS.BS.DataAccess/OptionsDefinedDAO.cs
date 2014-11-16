/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:17
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public partial class OptionsDefinedDAO
    {
        #region GetByOptionName
        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public OptionsDefinedEntity GetByOptionName(object pOptionName,string ClientID)
        {
            //参数检查
            if (pOptionName == null)
                return null;
            string OptionName = pOptionName.ToString();
            string Where = "";
            if (!string.IsNullOrEmpty(ClientID) && ClientID!="0") {
                Where = "   and ( ClientID='" + ClientID +"'" +" or ClientID='0')";  
            }
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [OptionsDefined] where OptionName='{0}' and IsDelete=0  {1}", OptionName, Where);
            //读取数据
            OptionsDefinedEntity m = null;
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