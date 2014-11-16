/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 13:46:23
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

using System.Data;
using System.Data.SqlClient;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问： 基金赎回 
    /// 表WXHouseReservationRedeem的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WXHouseReservationRedeemDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseReservationRedeemEntity>, IQueryable<WXHouseReservationRedeemEntity>
    {
        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public WXHouseReservationRedeemEntity GetByPrePaymentID(string customerID, string prePaymentID)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationRedeem] where  PrePaymentID='{0}' and CustomerID='{1}' and IsDelete=0 ", prePaymentID, customerID);
            //读取数据
            WXHouseReservationRedeemEntity m = null;
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
        /// 获取赎回信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fundType"></param>
        /// <param name="assignbuyer"></param>
        /// <param name="orderNO"></param>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public WXHouseReservationRedeemEntity GetBy(string customerID, string assignbuyer, string thirdOrderNO)
        {
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
                            select fund.*,yer.Assignbuyer,hOrder.ThirdOrderNo from WXHouseOrder  hOrder 
                            inner join WXHouseAssignbuyer yer on yer.AssignbuyerID=hOrder.AssignbuyerID  and yer.CustomerID=hOrder.CustomerID
                            inner join WXHouseReservationRedeem fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  where 
                            yer.Assignbuyer='{0}' and hOrder.ThirdOrderNo='{1}' and  fund.CustomerID='{2}' and fund.IsDelete=0 and fund.FundState=3", assignbuyer, thirdOrderNO, customerID);

            //读取数据
            WXHouseReservationRedeemEntity m = null;
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
                            inner join WXHouseReservationRedeem fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  where 
                            fund.CustomerID='{0}' and fund.FundState='{1}' and fund.IsDelete=0", customerID, fundState);
            //读取数据
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());

            return ds;
        }

    }
}
