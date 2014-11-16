/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/13 10:54:58
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
    /// 表TOrderCouponMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class TOrderCouponMappingDAO : Base.BaseCPOSDAO, ICRUDable<TOrderCouponMappingEntity>, IQueryable<TOrderCouponMappingEntity>
    {
        #region 删除跟订单相关的表
        public bool DeleteOrderCouponMapping(string OrderId)
        {
            string sql = "update tOrderCouponMapping set isdelete = '1',lastupdatetime=GETDATE() where isdelete = '0' and orderId = '" + OrderId + "' ";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion


        public decimal GetCouponParValue(string orderId)
        {
            var sql = new StringBuilder();
            sql.Append("select b.ParValue from TOrderCouponMapping a,CouponType b ,Coupon c");
            sql.Append(" where a.CouponId = c.CouponID");
            sql.Append(" and b.CouponTypeID = c.CouponTypeID");
            sql.Append(" and a.OrderId =@pOrderId");

            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pOrderId", Value = orderId}
            };

            var result = this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray());

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(result);
            }
        }
    }


    
}
