/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/7 14:57:03
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
    /// 表ClientBussinessDefined的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ClientBussinessDefinedDAO : Base.BaseCPOSDAO, ICRUDable<ClientBussinessDefinedEntity>, IQueryable<ClientBussinessDefinedEntity>
    {

        public string DynamicVipPropertySave(string propertyID, string displayType, string propertyName, DataTable dataTable)
        {
            List<SqlParameter> lsp = new List<SqlParameter>();
            lsp.Add(new SqlParameter("@PropertyID", propertyID));
            lsp.Add(new SqlParameter("@DisplayType", displayType));
            lsp.Add(new SqlParameter("@PropertyName", propertyName));
            lsp.Add(new SqlParameter("@TableName", "Vip"));
            lsp.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            lsp.Add(new SqlParameter("@UserID", CurrentUserInfo.UserID));
            lsp.Add(new SqlParameter("@TableParameterCommon", SqlDbType.Structured) { Value = dataTable});

            return this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "DynamicVipPropertySave", lsp.ToArray()).ToString();
        }
    }
}
