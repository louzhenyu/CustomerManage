/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/9/17 14:26:19
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��t_customer�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class t_customerDAO : Base.BaseCPOSDAO, ICRUDable<t_customerEntity>, IQueryable<t_customerEntity>
    {
        public t_customerEntity GetCustomer(string customerCode)
        {
            string sql = string.Format("select * from dbo.t_customer where customer_code='{0}'"
                ,customerCode.Replace("'","''"));
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            using (SqlDataReader rdr = sqlHelper.ExecuteReader(sql))
            {
                if(rdr.Read())
                {
                    t_customerEntity m;
                    this.Load(rdr, out m);
                    return m;
                }
            }
            return null;
        }
        /// <summary>
        /// ����CustomerId��ȡ�̻���Ϣ
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public t_customerEntity GetByCustomerID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [t_customer] where customer_id='{0}'  ", id.ToString());
            string conn = ConfigurationManager.AppSettings["Conn_ap"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            //��ȡ����
            t_customerEntity m = null;
            using (SqlDataReader rdr = sqlHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }
    }
}
