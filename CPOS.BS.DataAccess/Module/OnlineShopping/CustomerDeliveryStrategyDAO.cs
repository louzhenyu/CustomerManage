/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/25 17:27:27
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
    /// 表CustomerDeliveryStrategy的数据访问类     
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerDeliveryStrategyDAO : Base.BaseCPOSDAO, ICRUDable<CustomerDeliveryStrategyEntity>, IQueryable<CustomerDeliveryStrategyEntity>
    {
        #region 根据custoerID和总金额取得运费
        /// <summary>
        /// 根据custoerID和总金额取得运费
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataSet GetDeliveryAmount(string CustomerId, decimal total_amount, string DeliveryId)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
        
//            string sql = @"select * from CustomerDeliveryStrategy where CustomerId=@CustomerId
//                and  (@total_amount between AmountBegin  and AmountEnd)
//                 and DeliveryId=@DeliveryId ";
//            string sql = @"declare @deliveryCount int
//select @deliveryCount=count(1) from CustomerDeliveryStrategy where CustomerId=@CustomerId
//                and  (@total_amount between AmountBegin  and AmountEnd)
//                 and DeliveryId=@DeliveryId and IsDelete=0
//if(@deliveryCount!=0)
//	  select * from CustomerDeliveryStrategy where CustomerId=@CustomerId
//                and  (@total_amount between AmountBegin  and AmountEnd)
//                 and DeliveryId=@DeliveryId and IsDelete=0
//  else 
//	     select * from CustomerDeliveryStrategy where 
//	     (CustomerId='' or	CustomerId is null)
//                and  (@total_amount between AmountBegin  and AmountEnd)
//                 and DeliveryId=@DeliveryId and IsDelete=0
//";
            string sql = @"declare @deliveryCount int
                select * from CustomerDeliveryStrategy where CustomerId=@CustomerId
                and  (@total_amount > AmountBegin  and @total_amount< AmountEnd)
                and DeliveryId=@DeliveryId and IsDelete=0 ";
            ls.Add(new SqlParameter("@CustomerId", CustomerId));
            ls.Add(new SqlParameter("@total_amount", total_amount));
            ls.Add(new SqlParameter("@DeliveryId", DeliveryId));
            DataSet ds = new DataSet();

            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());
            return ds;
        }
        #endregion
    }
}
