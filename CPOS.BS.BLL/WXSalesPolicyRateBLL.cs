/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/15 13:43:24
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
    /// 业务处理：  
    /// </summary>
    public partial class WXSalesPolicyRateBLL
    {
        public DataSet getReturnAmount(decimal SaleAmount,string pcustomerId)
        {
            return _currentDAO.getReturnAmount(SaleAmount,pcustomerId);
        }
        /// <summary>
        /// 获取返利规则设置列表
        /// </summary>
        /// <returns></returns>
        public List<WXSalesPolicyRateEntity> GetWxSalesPolicyRateList()
        {
            var ds = this._currentDAO.GetWxSalesPolicyRateList();
            var policyList = new List<WXSalesPolicyRateEntity>();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                policyList = DataTableToObject.ConvertToList<WXSalesPolicyRateEntity>(ds.Tables[0]);
            }
            return policyList;
        }
        /// <summary>
        /// 批量操作返利规则
        /// </summary>
        /// <param name="items"></param>
        public bool BatchProcess(string customerId,string userId,WXSalesPolicyRateEntity[] items)
        {
            var dt = BuildDataTable(items);
            return this._currentDAO.BatchProcess(customerId,userId,dt);
        }
       
        private DataTable BuildDataTable(WXSalesPolicyRateEntity[] items)
        {
            var dt = new DataTable();
            dt.Columns.Add("RateId", typeof(string));
            dt.Columns.Add("AmountBegin", typeof(int));
            dt.Columns.Add("AmountEnd", typeof(int));
            dt.Columns.Add("CardinalNumber", typeof(decimal));
            dt.Columns.Add("Coefficient", typeof(decimal));
            dt.Columns.Add("CustomerId", typeof(string));
            dt.Columns.Add("PushInfo", typeof(string));
            dt.Columns.Add("CreateBy", typeof(string));
            dt.Columns.Add("LastUpdateBy", typeof(string));
            foreach (var e in items)
            {
                var r = dt.NewRow();
                r["RateId"] = e.RateId;
                r["AmountBegin"] = e.AmountBegin;
                r["AmountEnd"] = e.AmountEnd;
                r["CardinalNumber"] = e.CardinalNumber;
                r["Coefficient"] = e.Coefficient;
                r["CustomerId"] = e.CustomerId;
                r["PushInfo"] = e.PushInfo;
                r["CreateBy"] = e.CreateBy;
                r["LastUpdateBy"] = e.LastUpdateBy;
                dt.Rows.Add(r);
            }
            return dt;
        }
        /// <summary>
        /// 根据用户和客户ID，获取会籍店列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataSet GetUnitList(string userId, string customerId)
        {
            return this._currentDAO.GetUnitList(userId, customerId);
        }

        /// <summary>
        /// 建立虚拟订单时，根据客户和用户ID，获取门店ID
        /// 取获取的第一个门店ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public string GetUnitIDByUserId(string userId, string customerId)
        {
            return this._currentDAO.GetUnitIDByUserId(userId, customerId);
        }

        /// <summary>
        /// 根据条件查询返利列表
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <param name="searchColumns">查询条件</param>
        /// <returns></returns>
        public DataSet GetRebateList(string userId,string customerId, int pageIndex,
            int pageSize, SearchColumn[] searchColumns)
        {
            return this._currentDAO.GetRebateList(userId,customerId,pageIndex,pageSize, searchColumns);
        }
    }
}