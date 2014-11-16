using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.DataAccess
{
    public partial class UnitDAO : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public UnitDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        /// <summary>
        /// 获取门店列表
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public DataSet GetUnitList(Hashtable ht)
        {
            var parm = new SqlParameter[10];
            parm[0] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar) { Value = ht["MemberID"] };
            parm[1] = new SqlParameter("@PageIndex", System.Data.SqlDbType.Int) { Value = ht["PageIndex"] };
            parm[2] = new SqlParameter("@PageSize", System.Data.SqlDbType.Int) { Value = ht["PageSize"] };
            parm[3] = new SqlParameter("@Latitude", System.Data.SqlDbType.Decimal) { Value = ht["Latitude"] };
            parm[4] = new SqlParameter("@Longitude", System.Data.SqlDbType.Decimal) { Value = ht["Longitude"] };
            parm[5] = new SqlParameter("@Distance", System.Data.SqlDbType.Int) { Value = ht["Distance"] };
            parm[6] = new SqlParameter("@SortField", System.Data.SqlDbType.VarChar) { Value = ht["SortField"] };
            parm[7] = new SqlParameter("@SortType", System.Data.SqlDbType.VarChar) { Value = ht["SortType"] };
            parm[8] = new SqlParameter("@IndustryID", System.Data.SqlDbType.VarChar) { Value = ht["IndustryID"] };
            parm[9] = new SqlParameter("@SearchConditonal", System.Data.SqlDbType.VarChar) { Value = ht["UnitName"] };

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetUnitList", parm);
        }
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataSet GetUnitDetail(string memberId, string unitId)
        {
            var parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar) { Value = memberId };
            parm[1] = new SqlParameter("@UnitID", System.Data.SqlDbType.NVarChar) { Value = unitId };

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcUnitDetail", parm);
        }
        /// <summary>
        /// 获取我的积分
        /// </summary>
        /// <param name="hsPara"></param>
        /// <returns></returns>
        public DataSet GetMyIntegral(Hashtable hsPara)
        {
            var parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar) { Value = hsPara["MemberID"] };
            parm[1] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = hsPara["CustomerID"] };
            parm[2] = new SqlParameter("@PageSize", System.Data.SqlDbType.Int) { Value = hsPara["PageSize"] };
            parm[3] = new SqlParameter("@PageIndex", System.Data.SqlDbType.Int) { Value = hsPara["PageIndex"] };
         

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetVipIntegralDetail", parm);
        }
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="hsPara"></param>
        /// <returns></returns>
        public DataSet GetMyAccount(Hashtable hsPara)
        {
            var parm = new SqlParameter[4];
            parm[0] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar) { Value = hsPara["MemberID"] };
            parm[1] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = hsPara["CustomerID"] };
            parm[2] = new SqlParameter("@PageSize", System.Data.SqlDbType.NVarChar) { Value = hsPara["PageSize"] };
            parm[3] = new SqlParameter("@PageIndex", System.Data.SqlDbType.NVarChar) { Value = hsPara["PageIndex"] };
            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetVipAmountDetail", parm);
        }
        /// <summary>
        /// 获取店铺优惠券
        /// </summary>
        /// <param name="hsPara"></param>
        /// <returns></returns>
        public DataSet GetCouponList(Hashtable hsPara)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar) { Value = hsPara["MemberID"] };
            parm[1] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = hsPara["CustomerID"] };
            parm[2] = new SqlParameter("@Status", System.Data.SqlDbType.NVarChar) { Value = hsPara["Status"] };
            parm[3] = new SqlParameter("@PageSize", System.Data.SqlDbType.NVarChar) { Value = hsPara["PageSize"] };
            parm[4] = new SqlParameter("@PageIndex", System.Data.SqlDbType.NVarChar) { Value = hsPara["PageIndex"] };

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetVipCouponDetail", parm);
        }
        /// <summary>
        /// 根据商品业务类型获取商品信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="itemSortId"></param>
        /// <returns></returns>
        public DataSet GetItemBySortId(string customerId,int itemSortId)
        {
            string sql =string.Format(@"
            SELECT ItemId FROM dbo.T_Item a
            INNER JOIN ItemItemSortMapping b ON a.item_id=b.ItemId AND ItemSortId={0}
            WHERE CustomerId='{1}'", itemSortId,customerId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 根据SkuID获取价格信息
        /// </summary>
        /// <param name="skuId"></param>
        /// <returns></returns>
        public DataSet GetSkuPirce(string skuId)
        {
            string sql = string.Format(@"
            SELECT ReturnCash,SalesPrice FROM dbo.vw_sku_detail WHERE sku_id='{0}'", skuId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
    }

}
