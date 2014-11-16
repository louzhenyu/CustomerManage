/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
    /// 表WApplicationInterface的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WApplicationInterfaceDAO : Base.BaseCPOSDAO, ICRUDable<WApplicationInterfaceEntity>, IQueryable<WApplicationInterfaceEntity>
    {
        #region 后台Web获取列表
        /// <summary>
        /// 获取列表数量
        /// </summary>
        public int GetWebWApplicationInterfaceCount(WApplicationInterfaceEntity entity)
        {
            string sql = GetWebWApplicationInterfaceSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public DataSet GetWebWApplicationInterface(WApplicationInterfaceEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetWebWApplicationInterfaceSql(entity);
            sql += " select * From #tmp a where 1=1 and a.displayindex between '" +
                beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public DataSet GetWebWApplicationDelivery(string clientId)
        {
            string sql = @"select a.*,c.WeiXinTypeName,t.customer_name,d.SettingValue
                        from  cpos_ap..t_customer t inner join 
                        [WApplicationInterface] a on t.customer_id=a.CustomerId
                        left join [t_user] b on a.createBy=b.User_Id
                        left join WeiXinType c on a.WeiXinTypeId = c.WeiXinTypeId 
                        left join CustomerBasicSetting d on a.customerid = d.customerid and d.SettingCode='CustomerMobile'
                        where a.customerid='{0}'";
            return SQLHelper.ExecuteDataset(string.Format(sql, clientId));
        }
        private string GetWebWApplicationInterfaceSql(WApplicationInterfaceEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.User_Name CreateByName ";
            sql += " into #tmp ";
            sql += " from [WApplicationInterface] a";
            sql += " left join [t_user] b on a.createBy=b.User_Id ";
            sql += " where a.IsDelete='0' ";
            if (entity.WeiXinName != null && entity.WeiXinName.Trim().Length > 0)
            {
                sql += " and a.WeiXinName like '%" + entity.WeiXinName.Trim() + "%' ";
            }
            if (entity.WeiXinID != null && entity.WeiXinID.Trim().Length > 0)
            {
                sql += " and a.WeiXinID like '%" + entity.WeiXinID.Trim() + "%' ";
            }
            if (entity.CustomerId != null && entity.CustomerId.Trim().Length > 0)
            {
                sql += " and a.CustomerId like '%" + entity.CustomerId.Trim() + "%' ";
            }
            if (entity.AppID != null && entity.AppID.Trim().Length > 0)
            {
                sql += " and a.AppID = '" + entity.AppID.Trim() + "' ";
            }
            if (entity.ApplicationId != null && entity.ApplicationId.Trim().Length > 0)
            {
                sql += " and a.ApplicationId = '" + entity.ApplicationId.Trim() + "' ";
            }
            if (entity.AppSecret != null && entity.AppSecret.Trim().Length > 0)
            {
                sql += " and a.AppSecret = '" + entity.AppSecret.Trim() + "' ";
            }
            sql += " order by a.WeiXinName desc ";
            return sql;
        }
        #endregion

        #region Jermyn20131120处理管理平台微信公众号信息
        public bool setCposApMapping(WApplicationInterfaceEntity info)
        {
            //string sql = "exec ProcSetCustomerWeiXinMapping '" + UserID + "','" + UserName + "'," + Longitude + "," + Latitude + ",'" + EventID + "' ";

            SqlParameter[] Parm = new SqlParameter[5];
            Parm[0] = new SqlParameter("@CustomerId", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = this.CurrentUserInfo.CurrentUser.customer_id;
            Parm[1] = new SqlParameter("@WeiXinId", System.Data.SqlDbType.NVarChar, 100);
            Parm[1].Value = info.WeiXinID;
            Parm[2] = new SqlParameter("@WeiXinTypeId", System.Data.SqlDbType.Float, 4);
            Parm[2].Value = info.WeiXinTypeId;
            Parm[3] = new SqlParameter("@AppId", System.Data.SqlDbType.NVarChar, 100);
            Parm[3].Value = info.AppID;


            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ProcSetCustomerWeiXinMapping", Parm);
            return true;
        }
        #endregion

        #region Jermyn20140515 处理管理平台微信公众号默认菜单
        public bool setCreateWXMenu(WApplicationInterfaceEntity info)
        {
            //string sql = "exec ProcSetCustomerWeiXinMapping '" + UserID + "','" + UserName + "'," + Longitude + "," + Latitude + ",'" + EventID + "' ";

            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@WXid", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = info.WeiXinID;

            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "spCreateWXMenuByWXiD", Parm);
            return true;
        }
        #endregion

        public DataSet GetAccountList(string customerId, int pageIndex, int pageSize)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId}
            };
            var sql = new StringBuilder();
            sql.Append("select * from (");
            sql.Append(
                "select  row_number()over(order by createTime desc) as _row,ApplicationId,WeiXinName from WApplicationInterface where IsDelete=0 and customerId = @pCustomerId  ");
            sql.Append(") t");
            sql.AppendFormat(" where _row>={0} and _row<={1}"
                , pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }

        public int GetTotalcount(string customerId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pCustomerId", Value = customerId}
            };
            var sql = new StringBuilder();

            sql.Append(
                "select isnull(count(1),0) as num from WApplicationInterface where IsDelete=0 and customerId = @pCustomerId  ");

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql.ToString(), paras.ToArray()));
        }

        /// <summary>
        /// 根据客户ID，和公众平台ID查询是否支持多客服
        /// </summary>
        /// <param name="pCustomerId"></param>
        /// <param name="pWeixinID"></param>
        /// <returns></returns>
        public DataSet GetIsMoreCs(string pCustomerId, string pWeixinID)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter(){ParameterName="@pCustomerId",Value=pCustomerId},
                new SqlParameter(){ParameterName="@pWeixinId",Value=pWeixinID}
            };
            var sbSQL = new StringBuilder();
            sbSQL.Append("select * from WApplicationInterface where  IsDelete='0' and CustomerId=@pCustomerId and	 WeiXinID=@pWeixinId ");
            return SQLHelper.ExecuteDataset(CommandType.Text, sbSQL.ToString(), paras.ToArray());
        }

        #region RemoveSession
        public void RemoveSession(string Id)
        {
            string sql = "update WApplicationInterface set RequestToken=null,ExpirationTime=null where ApplicationId='" + Id + "'";
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion
    }
}
