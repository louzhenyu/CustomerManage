using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 楼盘仓储类定义。
    /// </summary>
    public partial class WXHouseBuildDAO
    {
        #region   楼盘展示
        /// <summary>
        /// 获取楼盘列表数据。
        /// </summary>
        /// <param name="pageIndex">客户ID。</param>
        /// <param name="pageIndex">当前页。</param>
        /// <param name="pageSize">每页显示条数。</param>
        /// <returns></returns>
        public DataSet GetHouses(string pCustomerID, int pPageIndex, int pPageSize)
        {
            int beginSize = (pPageIndex - 1) * pPageSize + 1;
            int endSize = pPageIndex * pPageSize;
            string sql = @"SELECT * FROM (SELECT wxhb.HouseID,wxhb.HouseCode,wxhb.HouseName,wxhb.HouseImgURL,wxhb.Coordinate,wxhb.HouseOpenDate,wxhb.LowestPrice";
            //启用虚拟数量
            bool isVirtual = true;
            if (isVirtual)
                sql += " ,(wxhb.SaleHoseNum+ISNULL(wxhd.VirtualSaleNum,0)) AS SaleHoseNum ";
            else
                sql += " ,wxhb.SaleHoseNum ";

            sql += @",ROW_NUMBER() OVER ( ORDER BY HouseName ) RowNumber,wxhd.RealPay,wxhd.DeductionPay,wxhd.DetailID
                        FROM WXHouseBuild wxhb 
                            INNER JOIN WXHouseDetail wxhd ON wxhb.HouseID = wxhd.HouseID
                        WHERE wxhb.CustomerID =@CustomerID and wxhb.HouseState in (1,2)  and wxhd.OnlineStatus=1 AND wxhb.IsDelete = '0' and wxhd.IsDelete='0') t
                        WHERE   t.RowNumber BETWEEN @BeginSize AND @EndSize ";

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@CustomerID",pCustomerID),
                new SqlParameter("@BeginSize",beginSize),
                new SqlParameter("@EndSize",endSize)
            };
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        /// <summary>
        /// 根据客户ID获取所有楼盘的条数。
        /// 
        /// </summary>
        /// <param name="customerID">客户ID。</param>
        /// <returns></returns>
        public int GetHousePageCount(string pCustomerID)
        {
            string sql = @"SELECT COUNT(*) AS num  FROM WXHouseBuild wxhb INNER JOIN WXHouseDetail wxhd ON wxhb.HouseID = wxhd.HouseID
                            WHERE wxhb.CustomerID =@CustomerID and wxhb.HouseState in (1,2) AND wxhb.IsDelete = '0'  and wxhd.OnlineStatus=1  ";

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@CustomerID",pCustomerID )
            };
            return (int)this.SQLHelper.ExecuteScalar(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region   我的楼盘
        /// <summary>
        /// 获取我的楼盘列表数据。
        /// </summary>
        /// <param name="customerID">客户ID。</param>
        /// <param name="vipID">会员ID</param>
        /// <param name="pageIndex">当前页。</param>
        /// <param name="pageSize">每页显示条数。</param>
        /// <returns>楼盘列表数据</returns>
        public DataSet GetMyHouses(string customerID, string vipID, int pageIndex, int pageSize)
        {
            int beginSize = (pageIndex - 1) * pageSize + 1;
            int endSize = pageIndex * pageSize;

            string sql = @"SELECT * FROM (SELECT wxhvm.MappingID,wxhd.DetailID,wxgb.HouseID,wxgb.HouseCode,wxgb.HouseName,wxgb.HouseImgURL,wxgb.HouseState,wxgb.Coordinate,fund.Fundtype,
                                                wxgb.LowestPrice ,hOrder.CreateTime AS BuyHouseDate,hOrder.PrePaymentID,hOrder.ThirdOrderNo,ROW_NUMBER() OVER ( ORDER BY wxgb.CreateTime ) RowNumber
                                      FROM      WXHouseVipMapping wxhvm
                                                INNER JOIN WXHouseOrder hOrder ON hOrder.MappingID = wxhvm.MappingID
                                                inner join dbo.WXHouseBuyFund fund on fund.PrePaymentID=hOrder.PrePaymentID
                                                INNER JOIN WXHouseDetail wxhd ON wxhd.DetailID = wxhvm.DetailID
                                                INNER JOIN WXHouseBuild wxgb ON wxgb.HouseID = wxhd.HouseID
                                      WHERE     wxhvm.CustomerID = @customerID AND wxhvm.VIPID = @vipID  and fund.FundState in(1,3)) t
                            WHERE   t.RowNumber BETWEEN @beginSize AND @endSize ";
            List<SqlParameter> para = new List<SqlParameter>() { 
                    new SqlParameter("@customerID",customerID ),
                    new SqlParameter("@vipID",vipID),
                    new SqlParameter("@beginSize",beginSize),
                    new SqlParameter("@endSize",endSize)
            };
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取我的房产总条数。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="vipID"></param>
        /// <returns></returns>
        public int GetMyHousePageCount(string customerID, string vipID)
        {
            string sql = @"SELECT count(*) FROM (SELECT ROW_NUMBER() OVER ( ORDER BY wxgb.CreateTime ) RowNumber
                                      FROM      WXHouseVipMapping wxhvm
                                                 INNER JOIN WXHouseOrder hOrder ON hOrder.MappingID = wxhvm.MappingID
                                                inner join dbo.WXHouseBuyFund fund on fund.PrePaymentID=hOrder.PrePaymentID
                                                INNER JOIN WXHouseDetail wxhd ON wxhd.DetailID = wxhvm.DetailID
                                                INNER JOIN WXHouseBuild wxgb ON wxgb.HouseID = wxhd.HouseID
                                      WHERE     wxhvm.CustomerID = @customerID AND wxhvm.VIPID = @vipID  and fund.FundState in(1,3)) t";

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@customerID",customerID ),
                new SqlParameter("@vipID",vipID )
            };

            return (int)this.SQLHelper.ExecuteScalar(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region  我的楼盘详细
        /// <summary>
        /// 获取我的楼盘详细信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="vipID"></param>
        /// <param name="pPrePaymentID"></param>
        /// <param name="pThirdOrderNo"></param>
        /// <returns></returns>
        public DataSet GetMyHouseDetail(string customerID, string vipID, string pPrePaymentID, string pThirdOrderNo)
        {
            string sql = @"SELECT  fund.PrePaymentID,wxhd.DetailID,wxhb.HouseID,wxhb.HouseCode,wxhb.HouseName,wxhb.HouseImgURL,wxhb.HouseState,
                                    wxhb.Coordinate,wxhb.LowestPrice,hOrder.CreateTime AS BuyHouseDate,wxhd.RealPay,hOrder.ThirdOrderNo,hOrder.OrderNo,wxhvm.MappingID,
                                    ISNULL((SELECT GrandProfit FROM WXHouseProfitList WHERE ThirdOrderNo = hOrder.ThirdOrderNo),0) GrandProfit,
                                    ISNULL(fund.FundState,0) as BuyFundState,ISNULL(pay.FundState,0) as PayFundState,ISNULL(redeem.FundState,0) as RedeemFundState,
                                    fund.Retmsg as BuyRetMsg,pay.Retmsg as PayRetMsg, redeem.Retmsg as RedeemRetMsg
                           FROM    WXHouseVipMapping wxhvm
                                    INNER JOIN WXHouseOrder hOrder ON hOrder.MappingID = wxhvm.MappingID
                                    inner join dbo.WXHouseBuyFund fund on fund.PrePaymentID=hOrder.PrePaymentID
                                    INNER JOIN WXHouseDetail wxhd ON wxhvm.DetailID = wxhd.DetailID
                                    INNER JOIN WXHouseBuild wxhb ON wxhb.HouseID = wxhd.HouseID
                                    left join dbo.WXHouseReservationPay  pay on pay.PrePaymentID=hOrder.PrePaymentID
                                    left join dbo.WXHouseReservationRedeem redeem on redeem.PrePaymentID=hOrder.PrePaymentID
                            WHERE   fund.PrePaymentID = @PrePaymentID AND wxhvm.VIPID = @VIPID AND wxhvm.CustomerID = @CustomerID and fund.FundState in(1,3)";
            List<SqlParameter> para = new List<SqlParameter>() { 
                    new SqlParameter("@ThirdOrderNo",pThirdOrderNo),
                    new SqlParameter("@PrePaymentID",pPrePaymentID),
                    new SqlParameter("@VIPID",vipID),
                    new SqlParameter("@CustomerID",customerID)
            };
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        #endregion

        #region 会员购买单楼盘次数
        /// <summary>
        /// 会员购买单楼盘次数
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pVipID"></param>
        /// <param name="pHouseDetailID"></param>
        /// <returns></returns>
        public int GetVipBuyHouseNumber(string pCustomerID, string pVipID, string pHouseDetailID)
        {
            string sql = @"SELECT COUNT(*) AS BuyNum FROM WXHouseVipMapping wxhvm
                            INNER JOIN WXHouseOrder wOrder ON wOrder.MappingID = wxhvm.MappingID
                            inner join dbo.WXHouseBuyFund fund on fund.PrePaymentID=wOrder.PrePaymentID
                            INNER JOIN WXHouseDetail wxhd ON wxhd.DetailID = wxhvm.DetailID
                            INNER JOIN WXHouseBuild wxgb ON wxgb.HouseID = wxhd.HouseID
                        WHERE wxhd.DetailID=@HouseDetailID AND wxhvm.VIPID = @VipID 
                            AND wxhvm.CustomerID = @CustomerID and fund.FundState=1";

            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@HouseDetailID",pHouseDetailID),
                new SqlParameter("@VipID",pVipID ),
                new SqlParameter("@CustomerID",pCustomerID)
            };

            object obj = this.SQLHelper.ExecuteScalar(CommandType.Text, sql, para.ToArray());
            int rObj = 0;
            int.TryParse(obj.ToString(), out rObj);

            return rObj;
        }

        #region 获取楼盘信息
        /// <summary>
        /// 获取楼盘信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pHouseDetailID"></param>
        /// <returns></returns>
        public DataSet GetHouseInfo(string pCustomerID, string pHouseDetailID)
        {
            string sql = @"SELECT * FROM WXHouseBuild wxhb INNER JOIN WXHouseDetail wxhd ON wxhb.HouseID = wxhd.HouseID
                            WHERE wxhd.DetailID=@HouseDetailID AND wxhb.CustomerID =@CustomerID AND wxhb.IsDelete = '0'";

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@HouseDetailID",pHouseDetailID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }
        #endregion

        #endregion

        /// <summary>
        /// 获取工作日。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="dt"></param>
        /// <param name="OffPeriod"></param>
        /// <returns></returns>
        public DataSet GetWorkDays(DateTime dt, int OffPeriod)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from (");
            sb.Append("select top(@OffPeriod+1)  (WorkDay+' 23:59:59') as WorkDay ");
            sb.Append("from [cpos_ap].[dbo].WorkDays ");
            sb.Append("where isdelete = 0 and WorkDay > @CurrDate order by WorkDay asc)");
            sb.Append(" w  order by WorkDay desc");

            List<SqlParameter> para = new List<SqlParameter>() { 
                    new SqlParameter("@CurrDate",dt.Date),
                    new SqlParameter("@OffPeriod",OffPeriod)
            };

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sb.ToString(), para.ToArray());

            return ds;
        }
    }
}
