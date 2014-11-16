/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/7 11:10:18
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
    /// 表FStaff的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class FStaffDAO : Base.BaseCPOSDAO, ICRUDable<FStaffEntity>, IQueryable<FStaffEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetListCount(FStaffEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetList(FStaffEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(FStaffEntity entity)
        {
            string sql = string.Empty;
            //sql = "SELECT a.* ";
            //sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            //sql += " ,b.VipId ";
            //sql += " ,b.Status ";
            //sql += " ,b.Weixin ";
            //sql += " ,b.WeixinUserId ";
            //sql += " ,b.HeadImgUrl ";
            //sql += " into #tmp ";
            //sql += " from FStaff a ";
            //sql += " inner join vip b on (a.phone=b.phone and b.isDelete='0') ";
            //sql += " where a.IsDelete='0' ";
            //sql += " and a.customerId='" + CurrentUserInfo.CurrentUser.customer_id + "' ";
            //if (entity.StaffId != null && entity.StaffId.Trim().Length > 0)
            //{
            //    sql += " and a.StaffId = '" + entity.StaffId + "' ";
            //}
            //if (entity.VipId != null && entity.VipId.Trim().Length > 0)
            //{
            //    sql += " and (b.VipId = '" + entity.VipId + "' or a.StaffId='" + entity.VipId + "') ";
            //}
            //if (entity.Phone != null && entity.Phone.Trim().Length > 0)
            //{
            //    sql += " and a.Phone = '" + entity.Phone + "' ";
            //}
            //if (entity.StaffName != null && entity.StaffName.Trim().Length > 0)
            //{
            //    sql += " and a.StaffName = '" + entity.StaffName + "' ";
            //}

            string sql1 = " select * into #tmp1 From vip where phone is not null and phone <> '' ";
            if (entity.VipId != null && entity.VipId.Trim().Length > 0)
            {
                sql1 += " and VipId = '" + entity.VipId + "' ; ";
            }
            sql = sql1;
            sql += "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.VipId ";
            sql += " ,b.Status ";
            sql += " ,b.Weixin ";
            sql += " ,b.WeixinUserId ";
            sql += " ,b.HeadImgUrl,case when b.vipid is null then 0 else 1 end IsSign ";
            sql += " into #tmp ";
            sql += " from FStaff a ";
            sql += " left join #tmp1 b on (a.phone=b.phone and b.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            sql += " and a.customerId='" + CurrentUserInfo.CurrentUser.customer_id + "' and a.phone is not null and a.phone <> '' ";
            if (entity.StaffId != null && entity.StaffId.Trim().Length > 0)
            {
                sql += " and a.StaffId = '" + entity.StaffId + "' ";
            }
            if (entity.VipId != null && entity.VipId.Trim().Length > 0)
            {
                sql += " and (b.VipId = '" + entity.VipId + "' or a.StaffId='" + entity.VipId + "') ";
            }
            if (entity.Phone != null && entity.Phone.Trim().Length > 0)
            {
                sql += " and a.Phone = '" + entity.Phone + "' ";
            }
            if (entity.StaffName != null && entity.StaffName.Trim().Length > 0)
            {
                sql += " and a.StaffName = '" + entity.StaffName + "' ";
            }
            return sql;
        }
        #endregion

        #region 盘点用户是否已经关联
        /// <summary>
        /// 复星注册
        /// </summary>
        /// <param name="VipId"></param>
        /// <param name="Phone"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public int IsVipStaffMapping(string VipId, string Phone, string OpenId)
        {
            string sql = "select COUNT(*) icount From Vip a "
                        + " inner join FStaff b on(a.Phone = b.Phone) "
                        + " where a.VIPID='" + VipId + "' and WeiXinUserId = '" + OpenId + "' ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 搜索员工
        public DataSet GetStaffJoinVip(string eventId, string staffName, string phone, string register, string sign, int pageNo, int pageSize)
        {
            DataSet dataSet = new DataSet();
            string sql = "GetStaffJoinVip";

            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@EventId", eventId));
            parameter.Add(new SqlParameter("@StaffName", staffName));
            parameter.Add(new SqlParameter("@Phone", phone));
            parameter.Add(new SqlParameter("@IsRegistered", register));
            parameter.Add(new SqlParameter("@IsSigned", sign));
            parameter.Add(new SqlParameter("@PageSize", pageSize));
            parameter.Add(new SqlParameter("@PageNo", pageNo));

            dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql, parameter.ToArray());

            return dataSet;
        }
        #endregion
    }
}
