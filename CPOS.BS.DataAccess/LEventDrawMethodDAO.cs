/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/8 15:11:12
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
    /// 表LEventDrawMethod的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventDrawMethodDAO : BaseCPOSDAO, ICRUDable<LEventDrawMethodEntity>, IQueryable<LEventDrawMethodEntity>
    {
        public DataSet GetDrawMethodList(string customerId,string eventTypeId, int pageIndex, int pageSize)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter {ParameterName = "@pEventTypeId", Value = eventTypeId},
            };

            var sql = new StringBuilder();

            sql.Append("select * from (");
            sql.Append(" select row_number()over(order by a.createTime desc) as _row," +
                       " isnull(a.DrawMethodID,'') DrawMethodID,isnull(a.DrawMethodName,'') DrawMethodName from LEventDrawMethod a ");
            sql.Append(" inner join LEventDrawMethodMapping b on a.DrawMethodID = b.DrawMethodId");
            sql.Append(" inner join LEvents c on b.EventId = c.EventID");
            sql.Append(" where a.IsDelete = 0 and b.IsDelete = 0 and c.IsDelete = 0");
            sql.Append(" and c.CustomerId=@pCustomerId");
            sql.Append(" and (c.EventTypeID=@pEventTypeId or isnull(@pEventTypeId,'') = '') ");
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }


        public int GetDrawTotalCount(string customerId, string eventTypeId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter {ParameterName = "@pCustomerId", Value = customerId},
                new SqlParameter {ParameterName = "@pEventTypeId", Value = eventTypeId},
            };

            var sql = new StringBuilder();
            
            sql.Append(" select isnull(count(1),0) num from LEventDrawMethod a ");
            sql.Append(" inner join LEventDrawMethodMapping b on a.DrawMethodID = b.DrawMethodId");
            sql.Append(" inner join LEvents c on b.EventId = c.EventID");
            sql.Append(" where a.IsDelete = 0 and b.IsDelete = 0 and c.IsDelete = 0");
            sql.Append(" and c.CustomerId=@pCustomerId");
            sql.Append(" and (c.EventTypeID=@pEventTypeId or isnull(@pEventTypeId,'') = '') ");

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));
        }
    }
}
