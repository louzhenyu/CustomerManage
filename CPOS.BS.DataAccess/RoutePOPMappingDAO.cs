/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/27 11:37:06
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


namespace JIT.CPOS.BS.DataAccess
{
    public partial class RoutePOPMappingDAO
    {
        #region DeleteRoutePOPMappingByRouteID
        public void DeleteRoutePOPMappingByRouteID(Guid routeid, IDbTransaction pTran)
        {

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
update [RoutePOPMapping] set isdelete=1 where [routeid]='{0}' and ClientID='{1}' and ClientDistributorID={2} and isdelete=0", routeid.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID);
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text, sql.ToString());
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString());
            }
            
        }
        #endregion
    }
}
