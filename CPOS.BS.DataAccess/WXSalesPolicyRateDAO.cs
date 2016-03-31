/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/15 13:43:24
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
    /// 表WXSalesPolicyRate的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXSalesPolicyRateDAO : BaseCPOSDAO, ICRUDable<WXSalesPolicyRateEntity>, IQueryable<WXSalesPolicyRateEntity>
    {
        /// <summary>
        /// 获取返现金额。客户推送消息内容
        /// </summary>
        /// <param name="SalesAmount"></param>
        /// <param name="pCustomerId"></param>
        /// <returns></returns>
        public DataSet getReturnAmount(decimal SalesAmount,string pCustomerId)
        {
            DataSet ds = new DataSet();
            StringBuilder sbSQL = new StringBuilder();
            //sbSQL.Append(string.Format("select (CardinalNumber+("+SalesAmount+" * Coefficient)) as ReturnAmount,PushInfo,* From [WXSalesPolicyRate]  where 1=1 and IsDelete=0 and CustomerId='{0}' ;", pCustomerId));
            //老的 没用到新的返现配置  WXSalesPolicyRate
            //sbSQL.Append(string.Format("select (CardinalNumber+({1} * Coefficient)) as ReturnAmount,PushInfo,* From [WXSalesPolicyRate]  where 1=1 and IsDelete=0 and CustomerId='{0}' and AmountBegin<={1} and AmountEnd>={1} ;", pCustomerId, SalesAmount));
            //sbSQL.Append(string.Format(" select (CardinalNumber+({0} * Coefficient)) as ReturnAmount,PushInfo,* From [WXSalesPolicyRate]  where 1=1 and IsDelete=0 and AmountBegin<={0} and AmountEnd>={0} and ([CustomerId]='' or CustomerId is NULL) ", SalesAmount));
            //新的返现配置CustomerBasicSetting
            SqlParameter[] parameters = new SqlParameter[2] 
            { 
                new SqlParameter{ParameterName="@Amount",SqlDbType=SqlDbType.Decimal,Value=SalesAmount},
                new SqlParameter{ParameterName="@CustomerId",SqlDbType=SqlDbType.NVarChar,Value=pCustomerId}
            };
            ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_CalculateReturnCash", parameters);
            return ds;
        }

        public DataSet GetWxSalesPolicyRateList()
        {
            var sql = "select * from WXSalesPolicyRate where isdelete = 0 and isnull(customerId,'') = ''";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 批量处理返现规则
        /// </summary>
        /// <param name="dt"></param>
        public bool BatchProcess(string customerId,string userId,DataTable dt)
        {
            string procName = "ProcBatchSalesPolicy";
            var ps = new[] { new SqlParameter("@tbl", dt),new SqlParameter("@customerId",customerId),
                    new SqlParameter("@userId",userId)};
            return SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, procName, ps) > 0;
        }
        /// <summary>
        /// 根据用户和客户id，获取会籍店列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetUnitList(string userId, string customerId)
        {
            string sql = @" SELECT DISTINCT R.*              
                       FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList ('{0}',UR.unit_id,205)  R                
                       WHERE user_id='{1}'";
            sql = string.Format(sql, customerId, userId);
            return SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 建立虚拟订单时，根据客户和用户ID，获取门店ID
        /// 取获取的第一个门店ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string GetUnitIDByUserId(string userId, string customerId)
        {
//            string sql = @" SELECT top 1 R.UnitID              
//                        FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList ('{0}',UR.unit_id,205)  R                
//                        WHERE user_id='{1}' order by create_time desc";
            string sql = @"select top 1 UR.Unit_ID from T_User_Role UR where user_id = '{0}'and status = 1 and default_flag = 1";
            sql = string.Format(sql, userId);
            return SQLHelper.ExecuteScalar(sql) as string;
        }
        /// <summary>
        /// 根据条件查询返利列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="searchColumns">查询条件</param>
        /// <returns></returns>
        public DataSet GetRebateList(string userId,string customerId,int pageIndex, 
            int pageSize,SearchColumn[] searchColumns)
        {
            var sqlHeader = string.Format(@"declare @headUnit nvarchar(50);
                    select @headUnit = unit_id FROM t_unit WHERE customer_id='{0}' AND type_id='2F35F85CF7FF4DF087188A7FB05DED1D'
                    select u.cu_name,d.lastupdatetime,o.sales_unit_id,d.dcodeid as DCodeId, 
                    convert(varchar(10),d.lastupdatetime,120) as HandleTime,u.cu_name as Operator,
                    isnull(d.SalesAmount,0) as SalesAmount,isnull(d.ReturnAmount,0) as ReturnAmount,
                    v.vipName as VipName,o.order_no as OrderNo
                    into #tmp_rebate
                    from vipdcode as d 
                    inner join t_inout as o on d.objectid=o.order_id
                    inner join vip as v on d.vipid=v.vipid
                    left join cpos_ap.dbo.t_customer_user as u on d.createby=u.customer_user_id and d.customerid=u.customer_id
                    --left join t_unit as n on o.sales_unit_id=n.unit_id
                    where dcodetype = 1 and d.CustomerId='{0}'
                    update #tmp_rebate
                    set sales_unit_id=@headUnit
                    where isnull(sales_unit_id,'') = ''", customerId);
            var prev = ";with cte as (";
            var sql = string.Format(@"select row_number() over(order by r.lastupdatetime desc,r.dcodeid) as row_num,
                        r.*,isnull(n.unit_name,'') as UnitName
                        from #tmp_rebate as r
                        inner join ( SELECT DISTINCT R.UnitID              
                        FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList ('{0}',UR.unit_id,205)  R                
                        WHERE user_id='{1}') as cu on r.sales_unit_id = cu.UnitID
                        left join t_unit as n on r.sales_unit_id=n.unit_id where 1=1 ",customerId,userId);
            string conditionSql = "";
            if(null != searchColumns && searchColumns.Length>0){
                foreach (var c in searchColumns)
                {
                    switch (c.ColumnName.ToLower())
                    {
                        case "unitid":
                            if (!string.IsNullOrEmpty(c.ColumnValue1))
                            {
                                conditionSql += string.Format(@" and r.sales_unit_id='{0}' ", c.ColumnValue1);
                            }
                            break;
                        case "time":
                            if (!string.IsNullOrEmpty(c.ColumnValue1))
                            {
                                conditionSql += string.Format(@" and r.lastupdatetime>='{0}' ", c.ColumnValue1);
                            }
                            if (!string.IsNullOrEmpty(c.ColumnValue2))
                            {
                                conditionSql += string.Format(@" and r.lastupdatetime<='{0}' ", c.ColumnValue2);
                            }
                            break;
                        case "operator":
                            if (!string.IsNullOrEmpty(c.ColumnValue1))
                            {
                                conditionSql += string.Format(@" and r.cu_name like '%{0}%' ", c.ColumnValue1.Replace("'", "''"));
                            }
                            break;
                        case "amount":
                            if (!string.IsNullOrEmpty(c.ColumnValue1))
                            {
                                conditionSql += string.Format(@" and r.salesamount>={0} ", c.ColumnValue1);
                            }
                            if (!string.IsNullOrEmpty(c.ColumnValue2))
                            {
                                conditionSql += string.Format(@" and r.salesamount<={0} ", c.ColumnValue2);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            var footSql = string.Format(@") select totalCount=(select count(*) from cte as c),*
                            from cte where row_num between {0} and {1};drop table #tmp_rebate; "
                            , (pageIndex - 1) * pageSize + 1, (pageIndex * pageSize));
            sql = sqlHeader + prev + sql + conditionSql + footSql;
            return SQLHelper.ExecuteDataset(sql);
        }
    }
}
