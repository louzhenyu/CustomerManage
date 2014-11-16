/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/28 10:12:41
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
    /// 表EEnterpriseCustomers的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class EEnterpriseCustomersDAO : Base.BaseCPOSDAO, ICRUDable<EEnterpriseCustomersEntity>, IQueryable<EEnterpriseCustomersEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(EEnterpriseCustomersEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(EEnterpriseCustomersEntity entity, int Page, int PageSize)
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
        private string GetListSql(EEnterpriseCustomersEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.EnterpriseCustomerName asc) ";
            sql += " ,CreateByName=(select user_name from t_user where user_id=a.createBy) ";
            sql += " ,TypeName=(select TypeName from EEnterpriseCustomerType where TypeId=a.TypeId) ";
            sql += " ,IndustryName=(select IndustryName from EIndustry where IndustryId=a.IndustryId) ";
            sql += " ,ScaleName=(select ScaleName from EScale where ScaleId=a.ScaleId) ";
            sql += " ,ECSourceName=(select ECSourceName from EEnterpriseCustomerSource where ECSourceId=a.ECSourceId) ";
            sql += " ,CityName=(select city3_name from t_city where city_id=a.CityId) ";
            sql += " into #tmp ";
            sql += " from [EEnterpriseCustomers] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            sql += " and a.CustomerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.EnterpriseCustomerId != null && entity.EnterpriseCustomerId.Length > 0)
            {
                sql += " and a.EnterpriseCustomerId = '" + entity.EnterpriseCustomerId + "' ";
            }
            if (entity.EnterpriseCustomerName != null && entity.EnterpriseCustomerName.Length > 0)
            {
                sql += " and a.EnterpriseCustomerName like '%" + entity.EnterpriseCustomerName + "%' ";
            }
            if (entity.TypeId != null && entity.TypeId.Length > 0)
            {
                sql += " and a.TypeId = '" + entity.TypeId + "' ";
            }
            if (entity.IndustryId != null && entity.IndustryId.Length > 0)
            {
                sql += " and a.IndustryId = '" + entity.IndustryId + "' ";
            }
            if (entity.ScaleId != null && entity.ScaleId.Length > 0)
            {
                sql += " and a.ScaleId = '" + entity.ScaleId + "' ";
            }
            if (entity.CityId != null && entity.CityId.Length > 0)
            {
                sql += " and a.CityId = '" + entity.CityId + "' ";
            }
            if (entity.ECSourceId != null && entity.ECSourceId.Length > 0)
            {
                sql += " and a.ECSourceId = '" + entity.ECSourceId + "' ";
            }
            if (entity.Status != null)
            {
                sql += " and a.Status = '" + entity.Status + "' ";
            }
            return sql;
        }
        #endregion

        #region 修改状态
        /// <summary>
        /// 修改状态
        /// </summary>
        public bool SetStatus(EEnterpriseCustomersEntity obj)
        {
            string sql = "update EEnterpriseCustomers "
                      + " set [Status] = '" + obj.Status + "'"
                      + " ,LastUpdateTime = '" + obj.LastUpdateTime + "' "
                      + " ,LastUpdateBy = '" + obj.LastUpdateBy + "' "
                      + " where EnterpriseCustomerId = '" + obj.EnterpriseCustomerId + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion

        #region 获取Top列表
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetTopList(EEnterpriseCustomersEntity entity)
        {
            DataSet ds = new DataSet();
            string sql = GetTopListSql(entity);
            sql += " select * From #tmp ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetTopListSql(EEnterpriseCustomersEntity entity)
        {
            string sql = string.Empty;
            sql = "select top 10 a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.EnterpriseCustomerName asc) ";
            sql += " ,TypeName=(select TypeName from EEnterpriseCustomerType where TypeId=a.TypeId) ";
            sql += " into #tmp ";
            sql += " from [EEnterpriseCustomers] a ";
            sql += " where 1=1 ";
            sql += " and a.IsDelete='0' ";
            sql += " and a.CustomerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            
            if (entity.EnterpriseCustomerName != null && entity.EnterpriseCustomerName.Length > 0)
            {
                if (entity.EnterpriseCustomerName.Contains("*"))
                {
                    entity.EnterpriseCustomerName = entity.EnterpriseCustomerName.Replace("*", "");
                    sql += " and a.EnterpriseCustomerName like '" + entity.EnterpriseCustomerName + "%' ";
                }
                else
                {
                    sql += " and a.EnterpriseCustomerName like '%" + entity.EnterpriseCustomerName + "%' ";
                }
            }
            return sql;
        }
        #endregion

    }
}
