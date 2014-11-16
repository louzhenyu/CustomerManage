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
    /// 数据访问： 世联华安客户关系表 
    /// 表WXHouseAssignbuyer的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXHouseAssignbuyerDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseAssignbuyerEntity>, IQueryable<WXHouseAssignbuyerEntity>
    {
        /// <summary>
        /// 获取客户协议号
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public WXHouseAssignbuyerEntity GetWXHouseAssignbuyer(string customerID, string userID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseAssignbuyer] where  UserID='{0}' and  CustomerID='{1}' and IsDelete=0 ", userID, customerID);
            //读取数据
            WXHouseAssignbuyerEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }


        /// <summary>
        /// 获取最大的客户协议号
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        public string GetWXHouseMaxAssignbuyer(string customerID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("Select  MAX(Assignbuyer) as MaxAssignbuyer from WXHouseAssignbuyer where CustomerID='{0}' and IsDelete=0 ", customerID);
            //读取数据
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
            //返回
            return maxValue;
        }

    }
}
