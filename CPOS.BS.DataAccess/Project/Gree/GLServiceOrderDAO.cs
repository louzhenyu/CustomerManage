/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/3 18:46:08
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
    /// 表GL_ServiceOrder的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class GLServiceOrderDAO : Base.BaseCPOSDAO, ICRUDable<GLServiceOrderEntity>, IQueryable<GLServiceOrderEntity>
    {
        /// <summary>
        /// 根据订单号获取服务单号信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetServiceOrderByOrderNo(string pCustomerID, string pOrderNo)
        {
            string sql = "SELECT VipID,glso.ServiceOrderID,ProductOrderSN,CustomerName,CustomerPhone,CustomerGender,ServiceType,glso.ServiceDateEnd";
            sql += " ,ServiceAddress,ISNULL(Latitude,0) AS Latitude,ISNULL(Longitude,0) AS Longitude,CustomerMessage,glso.ServiceDate";
            sql += " ,ServiceTaskID,UserID,glst.ServiceDate AS InstallOrderDate FROM cpos_demo.dbo.GL_ProductOrder AS glpo";
            sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON glpo.ProductOrderID=glso.ProductOrderID";
            sql += " LEFT JOIN cpos_demo.dbo.GL_ServiceTask AS glst ON glst.ServiceOrderID=glso.ServiceOrderID";
            sql += " WHERE glpo.ProductOrderSN=@ProductOrderSN AND glso.CustomerID=@CustomerID AND glso.IsDelete=0";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ProductOrderSN", pOrderNo));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        /// <summary>
        /// 根据订单号获取服务单号信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetServiceOrderByServiceOrderNo(string pCustomerID, string pServiceOrderNo)
        {
            string sql = "SELECT VipID,glso.ServiceOrderID,ProductOrderSN,CustomerName,CustomerPhone,CustomerGender,ServiceType,glso.ServiceDateEnd";
            sql += " ,ServiceAddress,ISNULL(Latitude,0) AS Latitude,ISNULL(Longitude,0) AS Longitude,CustomerMessage,glso.ServiceDate";
            sql += " ,ServiceTaskID,UserID,glst.ServiceDate AS InstallOrderDate FROM cpos_demo.dbo.GL_ProductOrder AS glpo";
            sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON glpo.ProductOrderID=glso.ProductOrderID";
            sql += " LEFT JOIN cpos_demo.dbo.GL_ServiceTask AS glst ON glst.ServiceOrderID=glso.ServiceOrderID";
            sql += " WHERE glso.ServiceOrderID=@ServiceOrderID AND glso.CustomerID=@CustomerID AND glso.IsDelete=0";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ServiceOrderID", pServiceOrderNo));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        /// <summary>
        /// 根据OrderID获取订单服务信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pProductOrderID"></param>
        /// <returns></returns>
        public GLServiceOrderEntity GetGLServiceOrderEntityByOrderID(string pCustomerID, string pOrderID)
        {
            string sql = "SELECT * FROM cpos_demo.dbo.GL_ServiceOrder WHERE ProductOrderID=@ProductOrderID ";
            sql += " AND CustomerID=@CustomerID AND IsDelete=0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ProductOrderID", pOrderID));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            GLServiceOrderEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), para.ToArray()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            return m;
        }

        /// <summary>
        /// 获取所有未分配师傅的预约服务单
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetGrabingServiceOrderList(string pCustomerID)
        {
            string sql = "SELECT VipID,glso.ServiceOrderID,ProductOrderSN,CustomerName,CustomerPhone,CustomerGender,ServiceType,glso.ServiceDateEnd";
            sql += " ,ServiceAddress,ISNULL(Latitude,0) AS Latitude,ISNULL(Longitude,0) AS Longitude,CustomerMessage,glso.ServiceDate";
            sql += " ,ServiceTaskID,UserID,glst.ServiceDate AS InstallOrderDate FROM cpos_demo.dbo.GL_ProductOrder AS glpo";
            sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON glpo.ProductOrderID=glso.ProductOrderID";
            sql += " LEFT JOIN cpos_demo.dbo.GL_ServiceTask AS glst ON glst.ServiceOrderID=glso.ServiceOrderID";
            sql += " WHERE glso.ServiceOrderID NOT IN (SELECT ServiceOrderID FROM cpos_demo.dbo.GL_ServiceTask) ";
            sql += " AND glso.CustomerID=@CustomerID AND glso.IsDelete=0  ";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }


        /// <summary>
        /// 根据师傅ID任务列表
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetServiceOrderByServicePersonID(string pCustomerID, string pServicePersonID)
        {
            string sql = "SELECT VipID,glso.ServiceOrderID AS ServiceOrderNO,ProductOrderSN AS OrderNO,CustomerName,CustomerPhone,CustomerGender,ServiceType,glso.ServiceDateEnd AS ServiceOrderDateEnd";
            sql += " ,ServiceAddress,ISNULL(Latitude,0) AS Latitude,ISNULL(Longitude,0) AS Longitude,CustomerMessage AS Message,glso.ServiceDate AS ServiceOrderDate";
            sql += " ,ServiceTaskID,UserID AS ServicePersonID,glst.ServiceDate AS InstallOrderDate FROM cpos_demo.dbo.GL_ProductOrder AS glpo";
            sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON glpo.ProductOrderID=glso.ProductOrderID";
            sql += " LEFT JOIN cpos_demo.dbo.GL_ServiceTask AS glst ON glst.ServiceOrderID=glso.ServiceOrderID";
            sql += " WHERE glst.UserID=@UserID AND glso.CustomerID=@CustomerID AND glso.IsDelete=0";
            //sql += " AND glso.ServiceDateEnd>=CONVERT(NVARCHAR(10),GETDATE(),120)";
            sql += " AND glso.ServiceDate>=CONVERT(NVARCHAR(10),GETDATE(),120) AND glst.IsDelete=0 ";

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@UserID", pServicePersonID));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        /// <summary>
        /// 验证是否是格力项目
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public bool ValidateGree(string pCustomerID, string pDbName)
        {
            bool f = false;
            string sql = "SELECT tc.customer_id,tc.customer_code,tc.customer_name,tcc.db_name,tcc.db_pwd,tcc.db_server FROM cpos_ap..t_customer_connect AS tcc INNER JOIN cpos_ap..t_customer AS tc ";
            sql += " ON tc.customer_id = tcc.customer_id WHERE tc.customer_id=@CustomerID";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                f = ds.Tables[0].Rows[0]["db_Name"].ToString().ToLower() == pDbName ? true : false;
            return f;
        }
    }
}
