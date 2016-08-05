/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/27 14:14:27
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

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问： 10：支付成功   90：支付失败 
    /// 表ReceiveAmountOrder的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class ReceiveAmountOrderDAO : BaseCPOSDAO, ICRUDable<ReceiveAmountOrderEntity>, IQueryable<ReceiveAmountOrderEntity>
    {
        /// <summary>
        /// 获取订单总数,应付总金额，实付总金额
        /// </summary>
        /// <param name="ServiceUserId"></param>
        /// <returns></returns>
        public List<ReceiveAmountOrderEntity> GetOrderCount(string ServiceUserId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"select (select count(1) from ReceiveAmountOrder) as OrderCount,
                              (select sum(TotalAmount) from ReceiveAmountOrder) as SumTotalAmount,
                              (select sum(TransAmount) from ReceiveAmountOrder) as SumTransAmount");
            //执行sql
            List<ReceiveAmountOrderEntity> list = new List<ReceiveAmountOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ReceiveAmountOrderEntity m = new ReceiveAmountOrderEntity();
                    if (rdr["OrderCount"] != DBNull.Value)
                    {
                        m.OrderCount = Convert.ToInt32(rdr["OrderCount"]);
                    }
                    if (rdr["SumTotalAmount"] != DBNull.Value)
                    {
                        m.SumTotalAmount = Convert.ToInt32(rdr["SumTotalAmount"]);
                    }
                    if (rdr["SumTransAmount"] != DBNull.Value)
                    {
                        m.SumTransAmount = Convert.ToInt32(rdr["SumTransAmount"]);
                    }
                    list.Add(m);
                }
            }
            return list;
        }
    }
}
