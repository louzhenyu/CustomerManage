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
    /// 数据访问：  
    /// 表GL_ProductOrder的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class GLProductOrderDAO : Base.BaseCPOSDAO, ICRUDable<GLProductOrderEntity>, IQueryable<GLProductOrderEntity>
    {
        /// <summary>
        /// 根据订单号获取订单信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pOrderNo"></param>
        /// <returns></returns>
        public GLProductOrderEntity GetProductOrderByOrderNo(string pCustomerID, string pOrderNo)
        {
            //string sql = "SELECT ProductOrderID,ProductOrderSN,CustomerName,CustomerAddress,CustomerPhone";
            //sql += " ,CustomerGender,CustomerID FROM cpos_demo.dbo.GL_ProductOrder WHERE ProductOrderSN=@ProductOrderSN ";
            string sql = "SELECT * FROM cpos_demo.dbo.GL_ProductOrder WHERE ProductOrderSN=@ProductOrderSN ";
            sql += " AND CustomerID=@CustomerID AND IsDelete=0";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@ProductOrderSN", pOrderNo));
            para.Add(new SqlParameter("@CustomerID", pCustomerID));
            GLProductOrderEntity m = null;
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
    }
}
