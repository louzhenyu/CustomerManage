/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/4/16 13:49:49
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
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 自定义拜访对象 
    /// 表VisitingObject的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VisitingObjectDAO
    {
        #region GetList
        public PagedQueryResult<VisitingObjectViewEntity> GetList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pageIndex, int pageSize)
        {
            string table = string.Format(@"
select A.*,
(select top 1 OptionText from Options where OptionName='ObjectGroup' and OptionValue=A.ObjectGroup and IsDelete=0 and Clientid='12' ) as ObjectGroupText
from VisitingObject A where A.ClientID='{0}' and isnull(A.ClientDistributorID,0)={1}
",
 CurrentUserInfo.ClientID,
 CurrentUserInfo.ClientDistributorID);
            return new UtilityDAO(CurrentUserInfo).GetList<VisitingObjectViewEntity>(pWhereConditions, pOrderBys, table, pageIndex, pageSize);
        }
        #endregion

        #region DeleteObject
        public void DeleteObject(Guid oid, IDbTransaction pTran)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
update VisitingObject set IsDelete=1 where VisitingObjectID='{0}' 
and ClientID='{1}' and isnull(ClientDistributorID,0)={2}
update VisitingObjectVisitingParameterMapping set IsDelete=1 where VisitingObjectID='{0}' 
and ClientID='{1}' and isnull(ClientDistributorID,0)={2}
            ");
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery(
                    (SqlTransaction)pTran,
                    CommandType.Text,
                    string.Format(sql.ToString(), oid.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID));
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format(sql.ToString(), oid.ToString(), CurrentUserInfo.ClientID, CurrentUserInfo.ClientDistributorID));
            }
        }
        #endregion
    }
}
