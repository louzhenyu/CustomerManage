using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.BS.BLL
{

    /// <summary>
    /// 楼盘业务逻辑类定义。
    /// </summary>
    public partial class WXHouseBuildBLL
    {
        #region  楼盘展示。
        /// <summary>
        /// 获取楼盘列表数据。
        /// </summary>
        /// <param name="pageIndex">客户ID。</param>
        /// <param name="pageIndex">当前页。</param>
        /// <param name="pageSize">每页显示条数。</param>
        /// <returns></returns>
        public DataSet GetHouses(string customerID, int pageIndex, int pageSize)
        {
            DataSet ds = this._currentDAO.GetHouses(customerID, pageIndex, pageSize);

            return ds;
        }


        /// <summary>
        /// 根据客户ID获取所有楼盘数据。
        /// 
        /// </summary>
        /// <param name="customerID">客户ID。</param>
        /// <returns></returns>
        public int GetHousePageCount(string customerID, int pageSize)
        {
            int rows = this._currentDAO.GetHousePageCount(customerID);

            //计算页数:往上取整数(总书数/页大小)
            int pageCount = (int)Math.Ceiling(rows / (double)pageSize);
            return pageCount;
        }
        #endregion


        #region  我的楼盘
        /// <summary>
        /// 获取我的房产。
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="vipID">会员ID</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每天显示条数</param>
        /// <returns></returns>
        public DataSet GetMyHouse(string customerID, string vipID, int pageIndex, int pageSize)
        {
            DataSet ds = this._currentDAO.GetMyHouses(customerID, vipID, pageIndex, pageSize);

            return ds;
        }

        /// <summary>
        /// 获取我的房产总页数。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="vipID"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetMyHousePageCount(string customerID, string vipID, int pageSize)
        {
            int rows = this._currentDAO.GetMyHousePageCount(customerID, vipID);
            int pageCount = (int)Math.Ceiling(rows / (double)pageSize);

            return pageCount;
        }

        /// <summary>
        /// 获取我所购买的楼盘数。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="vipID"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int GetMyHouseCount(string customerID, string vipID)
        {
            int rows = this._currentDAO.GetMyHousePageCount(customerID, vipID);

            return rows;
        }
        #endregion


        /// <summary>
        /// 获取我的楼盘详细信息。
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="vipID">用户ID</param>
        /// <param name="detailID">房产ID</param>
        /// <returns>已购买楼盘详细信息。</returns>
        public DataSet GetMyHouseDetail(string customerID, string vipID, string pPrePaymentID, string pThirdOrderNo)
        {
            DataSet ds = this._currentDAO.GetMyHouseDetail(customerID, vipID, pPrePaymentID, pThirdOrderNo);

            return ds;
        }

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
            return this._currentDAO.GetVipBuyHouseNumber(pCustomerID, pVipID, pHouseDetailID);
        }
        #endregion

        #region 获取楼盘信息
        /// <summary>
        /// 获取楼盘信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pHouseDetailID"></param>
        /// <returns></returns>
        public DataSet GetHouseInfo(string pCustomerID, string pHouseDetailID)
        {
            return this._currentDAO.GetHouseInfo(pCustomerID, pHouseDetailID);
        }
        #endregion

        /// <summary>
        /// 获取工作日。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="dt"></param>
        /// <param name="OffPeriod"></param>
        /// <returns></returns>
        public DateTime? GetWorkDays(DateTime currDate, int OffPeriod)
        {
            DataSet ds = _currentDAO.GetWorkDays(currDate, OffPeriod);
            DateTime? dt = null;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["WorkDay"].ToString());
                //dt = Convert.ToDateTime(data.FirstOrDefault());
            }

            return dt;
        }
    }

}
