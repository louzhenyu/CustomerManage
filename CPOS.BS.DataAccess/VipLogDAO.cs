/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/2 16:22:49
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
    /// 表VipLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipLogDAO : Base.BaseCPOSDAO, ICRUDable<VipLogEntity>, IQueryable<VipLogEntity>
    {
        /// <summary>
        /// 获取vip 操作日志列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public DataSet GetVipLogs(int pageIndex, int pageSize, string vipId)
        {
            string sql = "";
            if (pageIndex <= 0)
            {
                sql = @"select l.logid,l.createtime,u.cu_name,l.[action]
                            from viplog as l
                            left join [cpos_ap].[dbo].[t_customer_user] as u
                            on l.CreateBy = u.customer_user_id
                            where l.vipid='{0}' 
                            order by l.createtime desc";
                return SQLHelper.ExecuteDataset(string.Format(sql, vipId));
            }
            sql = @";with tmp as
                            (
                            select l.logid,l.createtime,u.cu_name,l.[action],row_number() over(order by l.createtime desc) as num
                            from viplog as l
                            left join [cpos_ap].[dbo].[t_customer_user] as u
                            on l.CreateBy = u.customer_user_id
                            where l.vipid='{0}'
                            )
                            select t.*,  p.totalCount
                            from tmp as t cross join (select count(*) as totalCount from tmp) as p
                            where t.num between {1} and {2}";
            
            return SQLHelper.ExecuteDataset(string.Format(sql, vipId, (pageIndex - 1) * pageSize, pageIndex * pageSize));
        }
    }
}
