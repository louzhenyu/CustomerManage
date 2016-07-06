using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;
using System.Collections;


namespace JIT.CPOS.BS.DataAccess
{
    public class StockBalanceService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public StockBalanceService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 根据条件，返回库存
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public int GetSearchStockBalanceCount(Hashtable _ht)
        {
            string sql = GetSearchPublicSql(_ht);
            sql += " select  isnull(COUNT(*),0)  From @TmpTable";
            int icount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return icount;
        }
        /// <summary>
        /// 查询库存获取详细信息
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public DataSet GetSearchStockBalanceInfoList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = GetSearchPublicSql(_ht);
            #region
            sql += " select a.* "
                  + " ,(select unit_name From t_unit where unit_id = a.unit_id) unit_name "
                  + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id)  warehouse_name "
                  + " ,b.item_label_type_code "
                  + " ,b.item_label_type_name "
                  + " ,c.item_code "
                  + " ,c.item_name "
                  + " ,c.prop_1_detail_name "
                  + " ,c.prop_2_detail_name "
                  + " ,c.prop_3_detail_name "
                  + " ,c.prop_4_detail_name "
                  + " ,c.prop_5_detail_name "
                  + " From @TmpTable a "
                  + " left join T_Item_Label_Type b "
                  + " on(a.item_label_type_id = b.item_label_type_id) "
                  + " inner join vw_sku c "
                  + " on(a.sku_id = c.sku_id) "
                  + " where 1=1 "
                  + " and a.row_no >= '" + _ht["StartRow"] + "' and a.row_no <= '" + _ht["EndRow"] + "'; ";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 库存查询公共脚本
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public string GetSearchPublicSql(Hashtable _ht)
        {
            #region
            string sql = "Declare @TmpTable Table "
                      + " (stock_balance_id nvarchar(100) "
                      + " ,unit_id nvarchar(100) "
                      + " ,warehouse_id nvarchar(100) "
                      + " ,sku_id nvarchar(100) "
                      + " ,item_label_type_id nvarchar(100) "
                      + " ,begin_qty decimal(18,4) "
                      + " ,in_qty decimal(18,4) "
                      + " ,out_qty decimal(18,4) "
                      + " ,adjust_in_qty decimal(18,4) "
                      + " ,adjust_out_qty decimal(18,4) "
                      + " ,reserver_qty decimal(18,4) "
                      + " ,on_way_qty decimal(18,4) "
                      + " ,end_qty decimal(18,4) "
                      + " ,price decimal(18,4) "
                      + " ,row_no int "
                      + " );"
                      + " insert into @TmpTable "
                      + " select a.stock_balance_id,a.unit_id,a.warehouse_id,a.sku_id,a.item_label_type_id,a.begin_qty,a.in_qty,a.out_qty,a.adjust_in_qty,a.adjust_out_qty,a.reserver_qty,a.on_way_qty,a.end_qty,a.price "
                      + " ,row_number() over(order by a.warehouse_id,a.unit_id,a.item_label_type_id,a.sku_id) row_no "
                      + " From T_Stock_Balance a "
                      + " inner join vw_sku b "
                      + " on(a.sku_id = b.sku_id) "
                      + " where 1=1 "
                      + " and a.status = '1' "
                //+ " and a.unit_id = '" + _ht["UnitId"].ToString() + "' "
                //+ " and a.warehouse_id = '" + _ht["WarehouseId"].ToString() + "' "
                      + " and (b.item_code like '%' + '" + _ht["ItemName"].ToString() + "' + '%' "
                      + " or b.item_name like '%' + '" + _ht["ItemName"].ToString() + "' + '%') ";

            if (_ht["UnitId"] != null && _ht["UnitId"].ToString().Length > 0)
            {
                sql += " and a.unit_id = '" + _ht["UnitId"].ToString() + "' ";
            }
            if (_ht["WarehouseId"] != null && _ht["WarehouseId"].ToString().Length > 0)
            {
                sql += " and a.warehouse_id = '" + _ht["WarehouseId"].ToString() + "' ";
            }
            if (_ht["SkuId"] != null && _ht["SkuId"].ToString().Length > 0)
            {
                sql += " and a.sku_id = '" + _ht["SkuId"].ToString() + "' ";
            }
            #endregion
            return sql;
        }
        #endregion

        #region 口库存
        /// <summary>
        /// 扣库存
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public bool SetStockBalance(string order_id)
        {
            string sql = "exec proc_Set_StockBalance '" + order_id + "','" + this.loggingSessionInfo.CurrentUser.User_Id + "'";
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);

            return true;
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
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);

            return true;
        }
        #endregion
    }
}
