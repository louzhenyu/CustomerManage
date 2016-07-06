using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 库存类
    /// </summary>
    public class StockBalanceService : BaseService
    {
        JIT.CPOS.BS.DataAccess.StockBalanceService stockBalanceService = null;
        #region 构造函数
        public StockBalanceService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            stockBalanceService = new DataAccess.StockBalanceService(loggingSessionInfo);
        }
        #endregion

        #region 根据组织门店获取库存
        /// <summary>
        /// 根据组织门店获取库存
        /// </summary>
        /// <param name="unit_id">组织标识</param>
        /// <param name="warehouse_id">门店标识</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public StockBalanceInfo GetStockBalanceListByUnitIdWarehouseId(
            string unit_id, string warehouse_id, string item_name,
            string sku_id, int maxRowCount, int startRowIndex)
        {
            try
            {
                if (item_name == null) item_name = "";
                Hashtable _ht = new Hashtable();
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);
                _ht.Add("UnitId", unit_id);
                _ht.Add("WarehouseId", warehouse_id);
                _ht.Add("SkuId", sku_id);
                _ht.Add("ItemName", item_name);
                _ht.Add("TypeValue", "1");

                StockBalanceInfo stockBalanceInfo = new StockBalanceInfo();
                int iCount = stockBalanceService.GetSearchStockBalanceCount(_ht);


                IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
                DataSet ds = stockBalanceService.GetSearchStockBalanceInfoList(_ht);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    stockBalanceInfoList = DataTableToObject.ConvertToList<StockBalanceInfo>(ds.Tables[0]);
                }
                stockBalanceInfo.icount = iCount;
                stockBalanceInfo.StockBalanceInfoList = stockBalanceInfoList;

                return stockBalanceInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public StockBalanceInfo GetStockBalanceListByUnitIdWarehouseId(
            string unit_id, string warehouse_id, string item_name,
            int maxRowCount, int startRowIndex)
        {
            return GetStockBalanceListByUnitIdWarehouseId(
                unit_id, warehouse_id, item_name, "", maxRowCount, startRowIndex);
        }
        #endregion

        #region 库存查询
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="unit_id">组织标识</param>
        /// <param name="warehouse_id">仓库标识</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public StockBalanceInfo SearchStockBalance(string unit_id
                                                  , string warehouse_id
                                                  , string item_name
                                                  , string sku_id
                                                  , int maxRowCount
                                                  , int startRowIndex
                                                  )
        {
            StockBalanceInfo stockBalanceInfo = new StockBalanceInfo();
            if (item_name == null) item_name = "";
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("UnitId", unit_id);
            _ht.Add("WarehouseId", warehouse_id);
            //_ht.Add("ItemCode", item_code);
            _ht.Add("ItemName", item_name);
            //_ht.Add("TypeValue", typeValue);
            //_ht.Add("YearMonth", yearMonth);

            int iCount = stockBalanceService.GetSearchStockBalanceCount(_ht);


            IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
            DataSet ds = stockBalanceService.GetSearchStockBalanceInfoList(_ht);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                stockBalanceInfoList = DataTableToObject.ConvertToList<StockBalanceInfo>(ds.Tables[0]);
            }
            stockBalanceInfo.icount = iCount;
            stockBalanceInfo.StockBalanceInfoList = stockBalanceInfoList;

            return stockBalanceInfo;
        }

        public StockBalanceInfo SearchStockBalance(
            string unit_id, string warehouse_id, string item_name,
            int maxRowCount, int startRowIndex)
        {
            return SearchStockBalance(
                unit_id, warehouse_id, item_name, "", maxRowCount, startRowIndex);
        }
        #endregion

        #region 中间层
        #region 库存下载
        /// <summary>
        /// 根据组织获取该组织的有库存记录的实时库存总数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public int GetStockBalanceCountByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("StockBalance.SelectByUnitIdCount", _ht);
            return 0;
        }
        /// <summary>
        /// 根据组织获取该组织的有库存记录的实时库存
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <param name="maxRowCount">最大数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <returns></returns>
        public IList<StockBalanceInfo> GetStockBalanceByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId, int maxRowCount, int startRowIndex)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitId", unitId);
                _ht.Add("StartRow", startRowIndex);
                _ht.Add("EndRow", startRowIndex + maxRowCount);
                //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<StockBalanceInfo>("StockBalance.SelectByUnitId", _ht);
                IList<StockBalanceInfo> stockBalanceInfoList = new List<StockBalanceInfo>();
                return stockBalanceInfoList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #endregion

        #region 扣库存
        /// <summary>
        /// 扣库存
        /// </summary>
        /// <param name="order_id">单据标识</param>
        /// <returns></returns>
        public bool SetStockBalance(string order_id)
        {
            try
            {
                return stockBalanceService.SetStockBalance(order_id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 保存库存
        /// <summary>
        /// 保存库存
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public bool InsertStockBalance(string sql)
        {
            try
            {
                return stockBalanceService.InsertStockBalance(sql);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

    }
}
