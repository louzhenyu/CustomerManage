/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/30 14:42:33
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
    /// 表VipEnterpriseExpand的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipEnterpriseExpandDAO : Base.BaseCPOSDAO, ICRUDable<VipEnterpriseExpandEntity>, IQueryable<VipEnterpriseExpandEntity>
    {

        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(VipEnterpriseExpandEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(VipEnterpriseExpandEntity entity, int Page, int PageSize)
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
        private string GetListSql(VipEnterpriseExpandEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.LastUpdateTime desc) ";
            sql += " ,CreateByName=(select user_name from t_user where user_id=a.createBy) ";
            sql += " ,b.VipName, b.Gender, b.Phone, b.Email ";
            sql += " ,(case when b.Gender='1' then '男' when b.Gender='2' then '女' else '' end) GenderName ";
            sql += " ,(case when a.Status='1' then '在职' when a.Status='0' then '离职' else '' end) StatusName ";
            sql += " ,PDRoleName=(select PDRoleName from EPolicyDecisionRole where PDRoleId=a.PDRoleId) ";
            sql += " ,EnterpriseCustomerName=(select EnterpriseCustomerName from EEnterpriseCustomers where EnterpriseCustomerId=a.EnterpriseCustomerId) ";
            sql += " into #tmp ";
            sql += " from [VipEnterpriseExpand] a ";
            sql += " inner join [Vip] b on a.vipId=b.vipId";
            sql += " where 1=1 ";
            //sql += " and a.IsDelete='0' ";
            sql += " and a.CustomerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            if (entity.EnterpriseCustomerId != null && entity.EnterpriseCustomerId.Length > 0)
            {
                sql += " and a.EnterpriseCustomerId = '" + entity.EnterpriseCustomerId + "' ";
            }
            if (entity.VipId != null && entity.VipId.Length > 0)
            {
                sql += " and a.VipId = '" + entity.VipId + "' ";
            }
            if (entity.VipName != null && entity.VipName.Length > 0)
            {
                sql += " and b.VipName like '%" + entity.VipName + "%' ";
            }
            if (entity.Status != null)
            {
                sql += " and a.Status = '" + entity.Status + "' ";
            }
            if (entity.IsDelete != null)
            {
                sql += " and a.IsDelete = '" + entity.IsDelete + "' ";
            }
            return sql;
        }
        #endregion

        #region 修改状态
        /// <summary>
        /// 修改状态
        /// </summary>
        public bool SetStatus(VipEnterpriseExpandEntity obj)
        {
            string sql = "update VipEnterpriseExpand "
                      + " set [Status] = '" + obj.Status + "'"
                      + " ,LastUpdateTime = '" + obj.LastUpdateTime + "' "
                      + " ,LastUpdateBy = '" + obj.LastUpdateBy + "' "
                      + " where vipId = '" + obj.VipId + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        public bool SetIsDelete(VipEnterpriseExpandEntity obj)
        {
            string sql = "update VipEnterpriseExpand "
                      + " set [IsDelete] = '" + obj.IsDelete + "'"
                      + " ,LastUpdateTime = '" + obj.LastUpdateTime + "' "
                      + " ,LastUpdateBy = '" + obj.LastUpdateBy + "' "
                      + " where vipId = '" + obj.VipId + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion
        
    }
}
