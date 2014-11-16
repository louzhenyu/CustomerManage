/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/2 11:19:54
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
    /// 表CustomerPayAssign的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CustomerPayAssignDAO : Base.BaseCPOSDAO, ICRUDable<CustomerPayAssignEntity>, IQueryable<CustomerPayAssignEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(CustomerPayAssignEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(CustomerPayAssignEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(CustomerPayAssignEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CustomerAccountNumber asc) ";
            sql += " ,CreateByName=(select user_name from t_user where user_id=a.createBy) ";
            sql += " ,PaymentTypeName=(select Payment_Type_Name from T_Payment_Type where Payment_Type_Id=a.PaymentTypeId) ";
            sql += " into #tmp ";
            sql += " from [CustomerPayAssign] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            sql += " and a.CustomerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.PaymentTypeId != null && entity.PaymentTypeId.Length > 0)
            {
                sql += " and a.PaymentTypeId = '" + entity.PaymentTypeId + "' ";
            }
            if (entity.CustomerAccountNumber != null && entity.CustomerAccountNumber.Length > 0)
            {
                sql += " and a.CustomerAccountNumber like '%" + entity.CustomerAccountNumber + "%' ";
            }
            if (entity.AssignId != null && entity.AssignId.Length > 0)
            {
                sql += " and a.AssignId = '" + entity.AssignId + "' ";
            }
            return sql;
        }
        #endregion

        public DataSet GetPaymentTypeList()
        {
            DataSet ds = new DataSet();
            var sql = " select * From T_Payment_Type a where [status]=1  ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

    }
}
