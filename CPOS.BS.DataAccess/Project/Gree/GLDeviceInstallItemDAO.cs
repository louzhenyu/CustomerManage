/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/4 11:56:05
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
    /// ���ݷ��ʣ�  
    /// ��GL_DeviceInstallItem�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class GLDeviceInstallItemDAO : Base.BaseCPOSDAO, ICRUDable<GLDeviceInstallItemEntity>, IQueryable<GLDeviceInstallItemEntity>
    {
        /// <summary>
        /// ���ݶ����Ż�ȡ����װ�豸
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetDeviceInstallItemByOrderNo(string pCustomerID, string pOrderNo)
        {
            string sql = "SELECT DeviceItemID AS DeviceID,DeviceFullName AS DeviceName,InstallPosition,1 AS DeviceCount,gldi.ServiceOrderID,ProductOrderSN";
            sql += " ,CustomerName,CustomerAddress,CustomerPhone,ServiceAddress FROM cpos_demo.dbo.GL_DeviceInstallItem AS gldi";
            sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON gldi.ServiceOrderID=glso.ServiceOrderID";
            sql += " INNER JOIN cpos_demo.dbo.GL_ProductOrder AS glpo ON glso.ProductOrderID=glpo.ProductOrderID";
            sql += " WHERE glpo.ProductOrderSN=@ProductOrderSN AND gldi.CustomerID=@CustomerID AND glpo.IsDelete=0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ProductOrderSN", pOrderNo));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        //public DataSet GetDeviceInstallItemByServiceOrderNo(string pCustomerID, string pServiceOrderNo)
        //{
        //    string sql = "SELECT DeviceItemID AS DeviceID,DeviceFullName AS DeviceName,InstallPosition,1 AS DeviceCount,gldi.ServiceOrderID,ProductOrderSN";
        //    sql += " ,CustomerName,CustomerAddress,CustomerPhone,ServiceAddress FROM cpos_demo.dbo.GL_DeviceInstallItem AS gldi";
        //    sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON gldi.ServiceOrderID=glso.ServiceOrderID";
        //    sql += " INNER JOIN cpos_demo.dbo.GL_ProductOrder AS glpo ON glso.ProductOrderID=glpo.ProductOrderID";
        //    sql += " WHERE glso.ServiceOrderID=@ServiceOrderID AND gldi.CustomerID=@CustomerID AND glpo.IsDelete=0";
        //    List<SqlParameter> para = new List<SqlParameter>();
        //    para.Add(new SqlParameter("@ServiceOrderID", pServiceOrderNo));
        //    para.Add(new SqlParameter("@CustomerID", pCustomerID));
        //    return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        //}

        /// <summary>
        /// ���ݷ��񵥺Ż�ȡ����װ�豸
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet GetDeviceInstallItemByServiceOrderID(string pCustomerID, string pServiceOrderID)
        {
            string sql = "SELECT DeviceItemID AS DeviceID,DeviceFullName AS DeviceName,InstallPosition,1 AS DeviceCount,gldi.ServiceOrderID,ProductOrderSN";
            sql += " ,CustomerName,CustomerAddress,CustomerPhone,ServiceAddress FROM cpos_demo.dbo.GL_DeviceInstallItem AS gldi";
            sql += " INNER JOIN cpos_demo.dbo.GL_ServiceOrder AS glso ON gldi.ServiceOrderID=glso.ServiceOrderID";
            sql += " INNER JOIN cpos_demo.dbo.GL_ProductOrder AS glpo ON glso.ProductOrderID=glpo.ProductOrderID";
            sql += " WHERE glso.ServiceOrderID=@ServiceOrderID AND gldi.CustomerID=@CustomerID AND glpo.IsDelete=0 AND gldi.IsDelete=0 ";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ServiceOrderID", pServiceOrderID));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }

        /// <summary>
        /// ɾ��ԤԼ���µ��豸
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public DataSet DelDeviceInstallItemByServerOrderID(string pCustomerID, string pServiceOrderID)
        {
            string sql = "UPDATE cpos_demo.dbo.GL_DeviceInstallItem SET IsDelete=1 WHERE ServiceOrderID=@ServiceOrderID AND IsDelete=0 AND CustomerID=@CustomerID";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ServiceOrderID", pServiceOrderID));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), para.ToArray());
        }
    }
}
