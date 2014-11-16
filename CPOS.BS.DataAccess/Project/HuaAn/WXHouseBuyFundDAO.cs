/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/24 12:33:54
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

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表WXHouseBuyFund的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXHouseBuyFundDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseBuyFundEntity>, IQueryable<WXHouseBuyFundEntity>
    {
        /// <summary>
        /// 获取购买基金信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fundType"></param>
        /// <param name="assignbuyer"></param>
        /// <param name="orderNO"></param>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public WXHouseBuyFundEntity GetBy(string customerID, string assignbuyer, string thirdOrderNO)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
                            select fund.*,yer.Assignbuyer,hOrder.ThirdOrderNo from WXHouseOrder  hOrder 
                            inner join WXHouseAssignbuyer yer on yer.AssignbuyerID=hOrder.AssignbuyerID  and yer.CustomerID=hOrder.CustomerID
                            inner join WXHouseBuyFund fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  where 
                            yer.Assignbuyer='{0}' and hOrder.ThirdOrderNo='{1}' and  fund.CustomerID='{2}' and fund.IsDelete=0 and fund.FundState=3", assignbuyer, thirdOrderNO, customerID);
            //读取数据
            WXHouseBuyFundEntity m = null;
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
        /// 根据customerID得到用WXHouseBuyFundEntity
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public DataSet Get(string customerID, int fundState)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
                            select fund.*,yer.Assignbuyer,hOrder.ThirdOrderNo,hOrder.OrderNO from WXHouseOrder  hOrder 
                            inner join WXHouseAssignbuyer yer on yer.AssignbuyerID=hOrder.AssignbuyerID  and yer.CustomerID=hOrder.CustomerID
                            inner join WXHouseBuyFund fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  where 
                            fund.CustomerID='{0}' and fund.FundState='{1}' and fund.IsDelete=0", customerID, fundState);
            //读取数据
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());

            return ds;
        }

        /// <summary>
        /// 根据交易状态获取交易信息。
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public DataSet Get(string customerID, string userID, int fundState)
        {
            //组织SQL
            string sql = @"select fund.* from WXHouseOrder  hOrder 
                            inner join WXHouseAssignbuyer yer on yer.AssignbuyerID=hOrder.AssignbuyerID and yer.CustomerID=hOrder.CustomerID
                            inner join WXHouseBuyFund fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  
                            where 
                            fund.CustomerID=@CustomerID and yer.UserID=@UserID and fund.FundState=@FundState and fund.IsDelete=0
                            order by fund.PayDate asc";
            List<SqlParameter> para = new List<SqlParameter>() { 
                    new SqlParameter("@CustomerID",customerID),
                    new SqlParameter("@UserID",userID),
                       new SqlParameter("@FundState",fundState)
            };

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());

            return ds;
        }

        /// <summary>
        /// 获取当前用户各类型状态下的总金额。
        /// </summary>
        /// <returns></returns>
        public DataSet GetTotalMoney(string customerID, string userID, int fundState)
        {
            string sql = @"select SUM(hOrder.RealPay) as TotalRealPay from WXHouseBuyFund fund
                         inner join  WXHouseOrder hOrder on hOrder.PrePaymentID=fund.PrePaymentID
                         inner join WXHouseAssignbuyer buer on buer.AssignbuyerID=hOrder.AssignbuyerID
                         where fund.FundState=@FundState 
                         and  buer.UserID=@UserID and buer.CustomerID=@CustomerID and buer.IsDelete=0";
            sql += " and hOrder.ThirdOrderNo not in";
            sql += " (select o.ThirdOrderNo from WXHouseReservationRedeem r, WXHouseOrder o, WXHouseAssignbuyer b";
            sql += " where b.UserID=@UserID and o.CustomerID=@CustomerID and o.AssignbuyerID=b.AssignbuyerID and o.IsDelete='0'";
            sql += " and o.PrePaymentID=r.PrePaymentID and r.FundState = 1)";
            sql += " and hOrder.ThirdOrderNo not in";
            sql += " (select o.ThirdOrderNo from WXHouseReservationPay r, WXHouseOrder o, WXHouseAssignbuyer b";
            sql += " where b.UserID=@UserID and o.CustomerID=@CustomerID and o.AssignbuyerID=b.AssignbuyerID and o.IsDelete='0'";
            sql += " and o.PrePaymentID=r.PrePaymentID and r.FundState = 1)";

            List<SqlParameter> para = new List<SqlParameter>() { 
                    new SqlParameter("@CustomerID",customerID),
                    new SqlParameter("@FundState",fundState),
                    new SqlParameter("@UserID",userID)
            };

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());

            return ds;
        }


    }
}
