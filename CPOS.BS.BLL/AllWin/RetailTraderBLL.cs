/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/5/17 17:27:33
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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理： 分销商包含门店和个人 
    /// </summary>
    public partial class RetailTraderBLL
    {
        /// <summary>
        /// 创建一个新实例到ap库
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create2Ap(RetailTraderEntity pEntity)
        {
            _currentDAO.Create2Ap(pEntity,null);//没用事务
        }
        public void Update2Ap(RetailTraderEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            _currentDAO.Update2Ap(pEntity, pTran, pIsUpdateNullField);
        }


        public int getMaxRetailTraderCode(string CustomerID)
        {
            return this._currentDAO.getMaxRetailTraderCode(CustomerID);
        }
        public DataSet getRetailTraderInfoByLogin(string LoginName, string RetailTraderID, string CustomerID)
        {
            return this._currentDAO.getRetailTraderInfoByLogin(LoginName, RetailTraderID, CustomerID);
        }

        //从ap库里取信息，用于统一保存分销商信息和登陆时取customerID
        public DataSet getRetailTraderInfoByLogin2(string LoginName, string RetailTraderID, string CustomerID)
        {
            return this._currentDAO.getRetailTraderInfoByLogin2(LoginName, RetailTraderID, CustomerID);
        }

        public DataSet GetRetailTradersBySellUser(string RetailTraderName, string UserID, string CustomerID)
        {
            return this._currentDAO.GetRetailTradersBySellUser(RetailTraderName, UserID, CustomerID);
        }





        public int GetVipCountBySellUser(string UserID, string CustomerID)
        {
            return this._currentDAO.GetVipCountBySellUser(UserID, CustomerID);
        }


        public DataSet GetMonthVipList(string UserID, string CustomerID, int month, int year)
        {
            return this._currentDAO.GetMonthVipList(UserID, CustomerID, month, year);
        }


        public int GetMonthTradeCount(string UserID, string CustomerID, int month, int year)
        {
            return this._currentDAO.GetMonthTradeCount(UserID, CustomerID, month, year);
        }


        public DataSet MonthVipRiseTrand(string UserID, string CustomerID, int year)
        {
            return this._currentDAO.MonthVipRiseTrand(UserID, CustomerID, year);
        }


        public decimal RetailRewardByAmountSource(string UserID, string CustomerID, int year, int month, int day, string AmountSourceID)
        {
            return this._currentDAO.RetailRewardByAmountSource(UserID, CustomerID, year, month, day, AmountSourceID);
        }


        public DataSet MonthRewards(string UserID, string CustomerID, int year)
        {
            return this._currentDAO.MonthRewards(UserID, CustomerID, year);
        }

        public DataSet MonthDayRewards(string UserID, string CustomerID, int year, int month)
        {
            return this._currentDAO.MonthDayRewards(UserID, CustomerID, year, month);
        }

        public DataSet GetRewardsDayRiseList(string UserID, string CustomerID, DateTime beginDate, DateTime endDate)
        {
            return this._currentDAO.GetRewardsDayRiseList(UserID, CustomerID, beginDate, endDate);
        }

        public DataSet GetVipDayRiseList(string RetailTraderID, string CustomerID, DateTime beginDate, DateTime endDate)
        {
            return this._currentDAO.GetVipDayRiseList(RetailTraderID, CustomerID, beginDate, endDate);
        }



        public DataSet GetRetailVipInfos(string RetailTraderID, string CustomerID, int year, int month, int day)
        {
            return this._currentDAO.GetRetailVipInfos(RetailTraderID, CustomerID, year, month, day);

        }

        public DataSet GetRetailTraders(string RetailTraderName, string RetailTraderAddress
, string RetailTraderMan, string Status, string CooperateType, string UnitID, string UserID, string CustomerID, string longinUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetRetailTraders(RetailTraderName, RetailTraderAddress
, RetailTraderMan, Status, CooperateType, UnitID, UserID, CustomerID, longinUserID, pageIndex, pageSize, OrderBy, sortType);
        }
        public DataSet GetSellerMonthRewardList(string UnitID, string SellerOrRetailName
, int Year, int Month, string CustomerID, string longinUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetSellerMonthRewardList(UnitID, SellerOrRetailName
, Year, Month, CustomerID, longinUserID, pageIndex, pageSize, OrderBy, sortType);
        }




        public DataSet GetRetailMonthRewardList(string UnitID, string SellerOrRetailName
, int Year, int Month, string CustomerID, string longinUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetRetailMonthRewardList(UnitID, SellerOrRetailName
, Year, Month, CustomerID, longinUserID, pageIndex, pageSize, OrderBy, sortType);
        }


        public DataSet GetRetailCoupon(string RetailTraderID, string CustomerID, int Status, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetRetailCoupon(RetailTraderID, CustomerID
, Status, pageIndex, pageSize, OrderBy, sortType);
        }

    }
}