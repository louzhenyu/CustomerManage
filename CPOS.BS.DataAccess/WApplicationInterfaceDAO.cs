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
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��WApplicationInterface�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WApplicationInterfaceDAO : Base.BaseCPOSDAO, ICRUDable<WApplicationInterfaceEntity>, IQueryable<WApplicationInterfaceEntity>
    {
        #region ��̨Web��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetWebWApplicationInterfaceCount(WApplicationInterfaceEntity entity)
        {
            string sql = GetWebWApplicationInterfaceSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
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

        #region Jermyn20131120�������ƽ̨΢�Ź��ں���Ϣ
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

        #region Jermyn20140515 �������ƽ̨΢�Ź��ں�Ĭ�ϲ˵�
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
        /// ���ݿͻ�ID���͹���ƽ̨ID��ѯ�Ƿ�֧�ֶ�ͷ�
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

        /// <summary>
        /// Trade-��ȡƽ̨΢�Ź��ں���Ϣ  add by Henry 2014-12-11
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <returns></returns>
        public WApplicationInterfaceEntity[] GetCloudWAppInterface(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WApplicationInterface] where isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //ִ��SQL
            List<WApplicationInterfaceEntity> list = new List<WApplicationInterfaceEntity>();
            string conn = ConfigurationManager.AppSettings["Conn_alading"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            using (SqlDataReader rdr = sqlHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WApplicationInterfaceEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }

        /// <summary>
        /// ��ȡoauth��֤��Ա�������ӵ�ַ
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public string GetOsVipMemberCentre(string OpenID)
        {
            string Url = "";
            if (!string.IsNullOrWhiteSpace(OpenID))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select menuurl from wmenu where pageid in ");
                sql.AppendFormat("(SELECT pageid from vwPageInfo WHERE customerid='{0}' AND pagekey='GetVipCard' and IsDelete=0) ", CurrentUserInfo.ClientID);
                sql.AppendFormat("and isdelete=0 and weixinid='{0}' AND ISNULL(parentid,'')!=''", OpenID);

                Url = (this.SQLHelper.ExecuteScalar(sql.ToString())).ToString();
            }
            return Url;
        }

        /// <summary>
        /// ���ݿ���ƽ̨id��ȡ�����Ϣ
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public DataSet GetWXOpenOAuth(string OpenOAuthID)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter(){ParameterName="@OpenOAuthAppid",Value=OpenOAuthID},

            };
            var sbSQL = new StringBuilder();
            sbSQL.Append("select * from cpos_ap..WXOpenOAuth  where  IsDelete=0 and Appid=@OpenOAuthAppid  ");
            return SQLHelper.ExecuteDataset(CommandType.Text, sbSQL.ToString(), paras.ToArray());
        }

    }
}
