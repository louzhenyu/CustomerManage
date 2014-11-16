/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/18 13:46:22
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
    /// ���ݷ��ʣ� ������֧���� 
    /// ��WXHouseReservationPay�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseReservationPayDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseReservationPayEntity>, IQueryable<WXHouseReservationPayEntity>
    {
        /// <summary>
        /// ����prePaymentID ��ȡ������Ϣ
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseReservationPayEntity GetByPrePaymentID(string customerID, string prePaymentID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select *  from WXHouseReservationPay  where PrePaymentID='{0}' and CustomerID='{1}' and IsDelete=0 ", prePaymentID, customerID);
            //��ȡ����
            WXHouseReservationPayEntity m = null;
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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="fundType"></param>
        /// <param name="assignbuyer"></param>
        /// <param name="orderNO"></param>
        /// <param name="seqNO"></param>
        /// <returns></returns>
        public WXHouseReservationPayEntity GetBy(string customerID, string assignbuyer, string thirdOrderNO)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
                            select fund.*,yer.Assignbuyer,hOrder.ThirdOrderNo from WXHouseOrder  hOrder 
                            inner join WXHouseAssignbuyer yer on yer.AssignbuyerID=hOrder.AssignbuyerID  and yer.CustomerID=hOrder.CustomerID
                            inner join WXHouseReservationPay fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  where 
                            yer.Assignbuyer='{0}' and hOrder.ThirdOrderNo='{1}' and  fund.CustomerID='{2}' and fund.IsDelete=0 and fund.FundState=3", assignbuyer, thirdOrderNO, customerID);

            //��ȡ����
            WXHouseReservationPayEntity m = null;
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
        /// ����customerID�õ���WXHouseBuyFundEntity
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public DataSet Get(string customerID, int fundState)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"
                            select fund.*,yer.Assignbuyer,hOrder.ThirdOrderNo,hOrder.OrderNO from WXHouseOrder  hOrder 
                            inner join WXHouseAssignbuyer yer on yer.AssignbuyerID=hOrder.AssignbuyerID  and yer.CustomerID=hOrder.CustomerID
                            inner join WXHouseReservationPay fund on fund.PrePaymentID=hOrder.PrePaymentID  and fund.CustomerID=hOrder.CustomerID  where 
                            fund.CustomerID='{0}' and fund.FundState='{1}' and fund.IsDelete=0", customerID, fundState);
            //��ȡ����
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());

            return ds;
        }
    }
}
