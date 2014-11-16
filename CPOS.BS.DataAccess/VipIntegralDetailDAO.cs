/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
    /// 表VipIntegralDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipIntegralDetailDAO : Base.BaseCPOSDAO, ICRUDable<VipIntegralDetailEntity>, IQueryable<VipIntegralDetailEntity>
    {
        #region 获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetVipIntegralDetailListCount(VipIntegralDetailEntity entity)
        {
            string sql = GetVipIntegralDetailListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetVipIntegralDetailList(VipIntegralDetailEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetVipIntegralDetailListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetVipIntegralDetailListSql(VipIntegralDetailEntity entity)
        {
            //,CONVERT(varchar(100),a.CreateTime,23) Create_Time
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"
            SELECT 
            a.Integral
            ,a.IntegralSourceID
            ,a.FromVipID
            , a.CreateTime
            ,CONVERT(varchar(100),a.CreateTime,23) Create_Time
            ,DisplayIndex = row_number() over(order by a.CreateTime desc)  
            ,b.IntegralSourceName IntegralSourceName
            , v.VipName AS FromVipName  
            , u.UserName As CreateBy
            , a.Remark
            into #tmp  
            from dbo.VipIntegralDetail a  
            left join SysIntegralSource b on a.IntegralSourceID=b.IntegralSourceID  
            left join Vip v on a.FromVipId = v.VipId  
            left join Users u on a.CreateBy = u.UserId
            where a.IsDelete='0'  and a.VIPID = '{0}'  
            order by a.CreateTime desc 
            ", entity.VIPID);
            return sqlStr.ToString();
        }
        #endregion

        /// <summary>
        /// 获取总消费金额
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public decimal GetVipSalesAmount(string vipId)
        {
            string sql = "";
            sql += " select sum(a.SalesAmount) SalesAmount from VipIntegralDetail a ";
            sql += " where a.IsDelete='0' and a.VIPID='" + vipId + "' ";
            var value = this.SQLHelper.ExecuteScalar(sql);
            return value == DBNull.Value || value.ToString() == string.Empty ?
                0 : Convert.ToDecimal(value);
        }

        /// <summary>
        /// 获取总产生积分
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public decimal GetVipIntegralAmount(string vipId)
        {
            string sql = "";
            sql += " select sum(a.Integral) IntegralAmount from VipIntegralDetail a ";
            sql += " where a.IsDelete='0' and a.VIPID='" + vipId + "' ";
            var value = this.SQLHelper.ExecuteScalar(sql);
            return value == DBNull.Value || value.ToString() == string.Empty ?
                0 : Convert.ToDecimal(value);
        }

        /// <summary>
        /// 下线用户关注获取积分总数
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public decimal GetVipNextLevelIntegralAmount(string vipId)
        {
            string sql = "";
            sql += " select sum(a.Integral) IntegralAmount from VipIntegralDetail a ";
            sql += " where a.IsDelete='0' ";
            sql += " and (a.IntegralSourceID='8' or a.IntegralSourceID='9') ";
            sql += " and (a.fromVipId is not null and a.fromVipId != '" + vipId + "') ";
            sql += " and a.vipId='" + vipId + "' ";
            var value = this.SQLHelper.ExecuteScalar(sql);
            return value == DBNull.Value || value.ToString() == string.Empty ? 
                0 : Convert.ToDecimal(value);
        }

        /// <summary>
        /// 获取订单使用的积分
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public decimal GetVipIntegralByOrder(string orderId, string userId)
        {
            var sql = new StringBuilder();
            sql.Append("select isnull(integral,0) as integral from vipIntegralDetail");
            sql.Append(" where objectId = @pOrderId and vipId = @pVipId");
            sql.Append(" and IntegralSourceID=20 ");//获取类型20=消费 add by henry 2014-10-08
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pOrderId", Value = orderId},
                new SqlParameter() {ParameterName = "@pVipId", Value = userId}
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
