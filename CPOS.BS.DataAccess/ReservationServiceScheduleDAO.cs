/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/25 9:57:34
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
    /// 表ReservationServiceSchedule的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ReservationServiceScheduleDAO : Base.BaseCPOSDAO, ICRUDable<ReservationServiceScheduleEntity>, IQueryable<ReservationServiceScheduleEntity>
    {
        public DataSet QueryAllField(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            string sql = @"select a.*
,b.ItemTitle as PositionTitle
,c.ServiceTitle
,d.TermStart as BigClassTermStart
,d.TermEnd as BigClassTermEnd
,e.TermStart as SmallClassTermStart
,e.TermEnd as smallClassTermEnd
from dbo.ReservationServiceSchedule a
left join dbo.ReservationPosition b 
on a.PositionID=b.ReservationPositionID
left join ReservationService c
on a.ReservationServiceID=c.ReservationServiceID
left join ReservationServiceBigClassTerm d
on a.ReservationServiceBigClassTermID=d.ReservationServiceBigClassTermID 
left join ReservationServiceSmallClassTerm e
on a.ReservationServiceSmallClassTermID=e.ReservationServiceSmallClassTermID
where a.IsDelete=0";
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql=string.Format("{0} and {1}", sql, item.GetExpression());
                }
            }
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;

        }
    }
}
