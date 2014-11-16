/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/24 10:08:00
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

using System.Data.SqlClient;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ� ���������ͻ���ϵ�� 
    /// ��WXHouseAssignbuyer�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseAssignbuyerDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseAssignbuyerEntity>, IQueryable<WXHouseAssignbuyerEntity>
    {
        /// <summary>
        /// ��ȡ�ͻ�Э���
        /// </summary>
        /// <param name="customerID">�ͻ�ID</param>
        /// <param name="userID">�û�ID</param>
        /// <returns></returns>
        public WXHouseAssignbuyerEntity GetWXHouseAssignbuyer(string customerID, string userID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseAssignbuyer] where  UserID='{0}' and  CustomerID='{1}' and IsDelete=0 ", userID, customerID);
            //��ȡ����
            WXHouseAssignbuyerEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
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


        /// <summary>
        /// ��ȡ���Ŀͻ�Э���
        /// </summary>
        /// <param name="customerID">�ͻ�ID</param>
        /// <returns></returns>
        public string GetWXHouseMaxAssignbuyer(string customerID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Select  MAX(Assignbuyer) as MaxAssignbuyer from WXHouseAssignbuyer where CustomerID='{0}' and IsDelete=0 ", customerID);
            //��ȡ����
            string maxValue = string.Empty;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    if (rdr["MaxAssignbuyer"] != System.DBNull.Value)
                    {
                        maxValue = rdr["MaxAssignbuyer"].ToString();
                    }
                    break;
                }
            }
            //����
            return maxValue;
        }

    }
}
