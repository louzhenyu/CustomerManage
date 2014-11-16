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
    public class CCService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public CCService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询总记录数量
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public int SearchCCCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql)); ;
        }

        /// <summary>
        /// 根据查询条件获取出入库主信息
        /// </summary>
        /// <param name="orderSearchInfo">查询条件对象</param>
        /// <returns></returns>
        public DataSet SearchCCInfo(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo);
            #region
            sql = sql + "select a.order_id "
                      + " ,a.order_no "
                      + " ,a.order_type_id "
                      + " ,a.order_reason_id "
                      + " ,a.ref_order_id "
                      + " ,a.ref_order_no "
                      + " ,a.warehouse_id "
                      + " ,a.order_date "
                      + " ,a.request_date "
                      + " ,a.complete_date "
                      + " ,a.unit_id "
                      + " ,a.pos_id "
                      + " ,a.remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,a.create_time "
                      + " ,a.create_user_id "
                      + " ,a.approve_time "
                      + " ,a.approve_user_id "
                      + " ,a.send_time "
                      + " ,a.send_user_id "
                      + " ,a.accpect_time "
                      + " ,a.accpect_user_id "
                      + " ,a.modify_time "
                      + " ,a.modify_user_id "
                      + " ,(select unit_name From t_unit where unit_id = a.unit_id) unit_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
                      + " ,(select convert(decimal(18,4),sum(order_qty)) From t_cc_detail x where x.order_id = a.order_id group by x.order_id) total_qty "
                      + " ,b.row_no "
                      + " ,@iCount icount "
                      + " From T_cc a "
                      + " inner join @TmpTable b "
                      + " on(a.order_id = b.order_id) "
                      + " where 1=1 "
                      + " and b.row_no > '" + orderSearchInfo.StartRow + "' and b.row_no <= '" + orderSearchInfo.EndRow + "' ;";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 根据单据标识，获取单据详细信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public DataSet GetCCInfoById(string order_id)
        {
            #region
            string sql = " select a.order_id "
                       + " ,a.order_no "
                       + " ,a.order_date "
                       + " ,a.order_type_id "
                       + " ,a.order_reason_id "
                       + " ,a.ref_order_id "
                       + " ,a.ref_order_no "
                       + " ,a.request_date "
                       + " ,a.complete_date "
                       + " ,a.unit_id "
                       + " ,a.pos_id "
                       + " ,a.warehouse_id "
                       + " ,a.remark "
                       + " ,a.data_from_id "
                       + " ,a.status "
                       + " ,a.status_desc "
                       + " ,a.create_time "
                       + " ,a.create_user_id "
                       + " ,a.modify_time "
                       + " ,a.modify_user_id "
                       + " ,a.send_time "
                       + " ,a.send_user_id "
                       + " ,a.approve_time "
                       + " ,a.approve_user_id "
                       + " ,a.accpect_time "
                       + " ,a.accpect_user_id "
                       + " ,(select prop_name from T_Prop where prop_id = a.data_from_id) data_from_name "
                       + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
                       + " ,(select unit_name From t_unit where unit_id = a.unit_id) unit_name "
                       + " ,(select USER_NAME From T_User where user_id = a.create_user_id) create_user_name "
                       + " ,(select USER_NAME From T_User where user_id = a.modify_user_id) modify_user_name "
                       + " ,(select USER_NAME From T_User where user_id = a.approve_user_id) approve_user_name "
                       + " ,(select USER_NAME From T_User where user_id = a.accpect_user_id) accpect_user_name "
                       + " ,(select USER_NAME From T_User where user_id = a.send_user_id) send_user_name "
                       + " ,(select convert(decimal(18,4),sum(order_qty)) From t_cc_detail x where x.order_id = a.order_id group by x.order_id) total_qty "
                       + " From T_CC a"

                      + " where 1=1 and a.order_id = '" + order_id + "';";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取CC库单据查询脚本公共部分
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        private string GetSearchPublicSql(OrderSearchInfo orderSearchInfo)
        {
            PublicService pService = new PublicService();
            string sql = "Declare @TmpTable Table "
                      + " (order_id nvarchar(100) "
                      + " ,row_no int "
                      + " ); "

                      + " insert into @TmpTable (order_id,row_no) "
                      + " select x.order_id ,x.rownum_ From ( "
                      + " select "
                      + " rownum_=row_number() over(order by a.order_date desc,a.order_no desc) "
                      + " ,order_id "
                      + " from t_cc a  where 1=1 and a.status != '-1'";
            sql = pService.GetLinkSql(sql, "a.order_id", orderSearchInfo.order_id, "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", orderSearchInfo.customer_id, "%");
            sql = pService.GetLinkSql(sql, "a.order_no", orderSearchInfo.order_no, "%");
            sql = pService.GetLinkSql(sql, "a.order_type_id", orderSearchInfo.order_type_id, "%");
            sql = pService.GetLinkSql(sql, "a.order_reason_id", orderSearchInfo.order_reason_id, "%");
            sql = pService.GetLinkSql(sql, "a.unit_id", orderSearchInfo.unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_begin, ">=");
            sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_end, "<=");
            sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_begin, ">=");
            sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_end, "<=");
            sql = pService.GetLinkSql(sql, "a.status", orderSearchInfo.status, "=");
            sql = pService.GetLinkSql(sql, "a.warehouse_id", orderSearchInfo.warehouse_id, "=");


           

            sql = sql + "  ) x ; ";

            sql = sql + " Declare @iCount int;";

            sql = sql + " select @iCount = COUNT(*) From @TmpTable;";

            return sql;
        }

        #region 明细查询
        /// <summary>
        /// 获取盘点单数量
        /// </summary>
        /// <param name="order_id">订单标识</param>
        /// <returns></returns>
        public int GetCCDetailCountByOrderId(string order_id)
        { 
            int n = 0;
            string sql = "select isnull(count(*),0) from t_cc_detail where order_id = '"+order_id+"';";
            n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return n;
        }
        /// <summary>
        /// 根据订单标识获取盘点单明细
        /// </summary>
        /// <param name="order_id">订单标识</param>
        /// <param name="StartRow">开始行号</param>
        /// <param name="EndRow">结束行号</param>
        /// <returns></returns>
        public DataSet GetCCDetailInfoListByOrderId(string order_id, int StartRow, int EndRow)
        {
            DataSet ds = new DataSet();
            string sql = GetCCDetailPublicSql(order_id);
            #region
            sql = sql + "select a.order_detail_id "
                     + " ,a.order_id "
                     + "  ,a.ref_order_detail_id "
                     + "  ,a.order_no "
                     + "  ,a.stock_balance_id "
                     + "  ,a.sku_id "
                     + "  ,a.warehouse_id "
                     + "  ,a.end_qty "
                     + "  ,a.order_qty "
                     + "  ,a.remark "
                     + "  ,a.display_index "
                     + "  ,a.create_time "
                     + "  ,a.create_user_id "
                     + "  ,a.modify_time "
                     + "  ,a.modify_user_id "
                     + "  ,a.if_flag "
                     + "  ,a.end_qty - a.order_qty difference_qty "
                     + "  ,b.item_code "
                     + "  ,b.item_name "
                     + "  ,b.prop_1_detail_name "
                     + "  ,b.prop_2_detail_name "
                     + "  ,b.prop_3_detail_name "
                     + "  ,b.prop_4_detail_name "
                     + "  ,b.prop_5_detail_name "
                     + "  ,c.row_no "
                     + "  ,@iCount icount "
                     + "  From t_cc_detail a "
                     + "  inner join vw_sku b "
                     + "  on(a.sku_id = b.sku_id) "
                     + "  inner join @TmpTable c "
                     + "  on(a.order_detail_id = c.order_detail_id) "
                     + "  where 1=1 "
                     + "  and c.row_no > '"+StartRow+"' and c.row_no <= '"+EndRow+"' "
                     + "  order by b.item_code";
            #endregion
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 公共的明细sql
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public string GetCCDetailPublicSql(string order_id)
        {
            string sql = " Declare @TmpTable Table "
                     + " (order_detail_id nvarchar(100) "
                     + "  ,row_no int "
                     + "  ); "

                     + "  insert into @TmpTable (order_detail_id,row_no) "
                     + "  select x.order_detail_id ,x.rownum_ From ( "
                     + "  select "
                     + "  rownum_=row_number() over(order by a.order_detail_id) "
                     + "  ,order_detail_id "
                     + "  from t_cc_detail a "

                     + "  where 1=1 "
                     + "  and a.order_id = '" + order_id + "') x; "

                     + "  Declare @iCount int; "

                     + "  select @iCount = COUNT(*) From @TmpTable;"; 
          return sql;
        }
        #endregion

        #region 库存明细
        /// <summary>
        /// 商品明细数量
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public int GetStockBalanceCount(Hashtable _ht)
        {
            int n = 0;
            string sql = GetStockBalancePublicString(_ht);
            sql = sql + " select @iCount;";
            n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return n;
        }
        /// <summary>
        /// 获取库存商品集合
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public DataSet GetStockBanlanceList(Hashtable _ht)
        {
            DataSet ds = new DataSet();
            string sql = GetStockBalancePublicString(_ht);
            sql = sql + "select REPLACE(newid(),'-','') order_detail_id "
                      + " ,'" + _ht["OrderId"].ToString() +"' order_id "
                         + " ,'' ref_order_detail_id "
                         + " ,'' order_no "
                         + " ,a.stock_balance_id "
                         + " ,a.sku_id "
                         + " ,a.warehouse_id "
                         + " ,a.end_qty "
                         + " ,a.end_qty order_qty "
                         + " ,'' remark "
                         + " ,null display_index "
                         + " ,a.create_time "
                         + " ,a.create_user_id "
                         + " ,a.modify_time "
                         + " ,a.modify_user_id "
                         + " ,0 if_flag "
                         + " ,null difference_qty "
                         + " ,b.item_code "
                         + " ,b.item_name "
                         + " ,b.prop_1_detail_name "
                         + " ,b.prop_2_detail_name "
                         + " ,b.prop_3_detail_name "
                         + " ,b.prop_4_detail_name "
                         + " ,b.prop_5_detail_name "
                         + " ,c.row_no "
                         + " ,@iCount icount "
                         + " From T_Stock_Balance a "
                         + " inner join vw_sku b "
                         + " on(a.sku_id = b.sku_id) "
                         + " inner join @TmpTable c "
                         + " on(a.stock_balance_id = c.stock_balance_id) "
                         + " where 1=1 "
                         + " and c.row_no > '" + _ht["StartRow"].ToString() + "' and c.row_no <= '" + _ht["EndRow"].ToString() + "' "
                         + " order by b.item_code;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取库存商品明细公共sql部分
        /// </summary>
        /// <param name="_ht"></param>
        /// <returns></returns>
        public string GetStockBalancePublicString(Hashtable _ht)
        {
            PublicService pService = new PublicService();
            string sql = "Declare @TmpTable Table "
                      + " (stock_balance_id nvarchar(100) "
                      + " ,row_no int "
                      + " ); "

                      + " insert into @TmpTable (stock_balance_id,row_no) "
                      + " select x.stock_balance_id ,x.rownum_ From ( "

                      + " select a.stock_balance_id "
                      + " ,rownum_=row_number() over(order by a.stock_balance_id) "
                      + " From T_Stock_Balance a "
                      + " where 1=1 "
                      + " and a.status = '1' ";
            sql = pService.GetLinkSql(sql, "a.unit_id", _ht["UnitId"].ToString(), "=");

            sql = pService.GetLinkSql(sql, "a.warehouse_id", _ht["WarehouseId"].ToString(), "=");  
                     

            sql = sql + " ) x ; "
       
                      + " Declare @iCount int; "

                      + " select @iCount = COUNT(*) From @TmpTable;";

            return sql;
        }
        #endregion
        #endregion

        #region 保存
        /// <summary>
        /// 盘点单保存
        /// </summary>
        /// <param name="ccInfo"></param>
        /// <param name="IsTrans"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetCCInfo(CCInfo ccInfo, bool IsTrans, out string strError)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    if(ccInfo.CCInfoList != null)
                    {
                        foreach (CCInfo ccInfo1 in ccInfo.CCInfoList)
                        {
                            //1.判断重复
                            if (!IsExistOrderCode(ccInfo1.order_no, ccInfo1.order_id))
                            {
                                strError = "订单号码已经存在。";
                                throw (new System.Exception(strError));
                            }

                            if (ccInfo1.operate.Equals("Create"))
                            {
                                if (loggingSessionInfo.CurrentLoggingManager.IsApprove == null || loggingSessionInfo.CurrentLoggingManager.IsApprove.Equals("0"))
                                {
                                    ccInfo1.status = "1";
                                    ccInfo1.status_desc = "未审批";
                                }
                                else
                                {
                                    //2.提交表单
                                    if (!SetInoutOrderInsertBill(ccInfo))
                                    {
                                        strError = "盘点单表单提交失败。";
                                        throw (new System.Exception(strError));
                                    }
                                    //3.更改状态
                                    DataSet ds = new BillService(loggingSessionInfo).GetBillById(ccInfo1.order_id);
                                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                                    {
                                        ccInfo1.status = ds.Tables[0].Rows[0]["Status"].ToString();
                                        ccInfo.status_desc = ds.Tables[0].Rows[0]["BillStatusDescription"].ToString();
                                    }
                                }
                            }

                            //4.提交cc与ccdetail信息
                            if (!SetCCTableInfo(ccInfo1))
                            {
                                strError = "提交主表失败";
                                throw (new System.Exception(strError));
                            }

                        }
                    }
                    
                    tran.Commit();
                    strError = "成功";
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw (ex);
                }
            }
        }

        /// <summary>
        /// 提交盘点单据
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="ccInfo">盘点单信息</param>
        /// <returns></returns>
        private bool SetCCTableInfo( CCInfo ccInfo)
        {
            PublicService pService = new PublicService();
            string sql = "";
            //修改盘点单主表
            #region
            sql = " update t_cc set order_no = '"+ccInfo.order_no+"' ";
            sql = pService.GetIsNotNullUpdateSql(sql,"order_date",ccInfo.order_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_type_id", ccInfo.order_type_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_reason_id", ccInfo.order_reason_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "ref_order_id", ccInfo.ref_order_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "ref_order_no", ccInfo.ref_order_no);
            sql = pService.GetIsNotNullUpdateSql(sql, "request_date", ccInfo.request_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "complete_date", ccInfo.complete_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_id", ccInfo.unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "pos_id", ccInfo.pos_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "warehouse_id", ccInfo.warehouse_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "remark", ccInfo.remark);
            sql = pService.GetIsNotNullUpdateSql(sql, "data_from_id", ccInfo.data_from_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "status", ccInfo.status);
            sql = pService.GetIsNotNullUpdateSql(sql, "status_desc", ccInfo.status_desc);
            sql = pService.GetIsNotNullUpdateSql(sql, "create_time", ccInfo.create_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "create_user_id", ccInfo.create_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", ccInfo.modify_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", ccInfo.modify_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "send_time", ccInfo.send_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "send_user_id", ccInfo.send_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_time", ccInfo.approve_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_user_id", ccInfo.approve_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "accpect_time", ccInfo.accpect_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "accpect_user_id", ccInfo.accpect_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "if_flag", ccInfo.if_flag);
            sql = sql + " where order_id = '" + ccInfo.order_id + "' ; " ;
            #endregion
            //插入盘点单
            #region
            sql = sql + "insert into t_cc( "
                      + " order_id "
                      + " ,order_no "
                      + " ,order_date "
                      + " ,order_type_id "
                      + " ,order_reason_id "
                      + " ,ref_order_id "
                      + " ,ref_order_no "
                      + " ,request_date "
                      + " ,complete_date "
                      + " ,unit_id "
                      + " ,pos_id "
                      + " ,warehouse_id "
                      + " ,remark "
                      + " ,data_from_id "
                      + " ,status "
                      + " ,status_desc "
                      + " ,create_time "
                      + " ,create_user_id "
                      + " ,modify_time "
                      + " ,modify_user_id "
                      + " ,send_time "
                      + " ,send_user_id "
                      + " ,approve_time "
                      + " ,approve_user_id "
                      + " ,accpect_time "
                      + " ,accpect_user_id "
                      + " ,if_flag "
                      + " ,customer_id "
                      + " ) "
                      + " select a.* From ( "
                      + " select '"+ccInfo.order_id+"' order_id "
                      + " ,'" + ccInfo.order_no + "' order_no "
                      + " ,'" + ccInfo.order_date + "' order_date "
                      + " ,'" + ccInfo.order_type_id + "' order_type_id "
                      + " ,'" + ccInfo.order_reason_id + "' order_reason_id "
                      + " ,'" + ccInfo.ref_order_id + "' ref_order_id "
                      + " ,'" + ccInfo.ref_order_no + "' ref_order_no "
                      + " ,'" + ccInfo.request_date + "' request_date "
                      + " ,'" + ccInfo.complete_date + "' complete_date "
                      + " ,'" + ccInfo.unit_id + "' unit_id "
                      + " ,'" + ccInfo.pos_id + "' pos_id "
                      + " ,'" + ccInfo.warehouse_id + "' warehouse_id "
                      + " ,'" + ccInfo.remark + "' remark "
                      + " ,'" + ccInfo.data_from_id + "' data_from_id "
                      + " ,'" + ccInfo.status + "' status "
                      + " ,'" + ccInfo.status_desc + "' status_desc "
                      + " ,'" + ccInfo.create_time + "' create_time "
                      + " ,'" + ccInfo.create_user_id + "' create_user_id "
                      + " ,'" + ccInfo.modify_time + "' modify_time "
                      + " ,'" + ccInfo.modify_user_id + "' modify_user_id "
                      + " ,'" + ccInfo.send_time + "' send_time "
                      + " ,'" + ccInfo.send_user_id + "' send_user_id "
                      + " ,'" + ccInfo.approve_time + "' approve_time "
                      + " ,'" + ccInfo.approve_user_id + "' approve_user_id "
                      + " ,'" + ccInfo.accpect_time + "' accpect_time "
                      + " ,'" + ccInfo.accpect_user_id + "' accpect_user_id "
                      + " ,'" + ccInfo.if_flag + "' if_flag "
                      + " ,'" + ccInfo.customer_id + "' customer_id "
                      + " ) a "
                      + " left join T_cc b "
                      + " on(a.order_id = b.order_id) "
                      + " where b.order_id is null ; " ;
            #endregion
            //删除盘点单明细
            #region
            sql = sql + "delete t_cc_detail where order_id = '" + ccInfo.order_id + "' ;";
            #endregion
            //插入明细          
            if (ccInfo.CCDetailInfoList != null) {
                foreach (CCDetailInfo ccDetailInfo in ccInfo.CCDetailInfoList)
                {
                    #region
                    sql = sql + "insert into t_cc_detail "
                                + " ( "
                                + " order_detail_id "
                                + " ,order_id "
                                + " ,ref_order_detail_id "
                                + " ,order_no "
                                + " ,stock_balance_id "
                                + " ,sku_id "
                                + " ,warehouse_id "
                                + " ,end_qty "
                                + " ,order_qty "
                                + " ,remark "
                                + " ,display_index "
                                + " ,create_time "
                                + " ,create_user_id "
                                + " ,modify_time "
                                + " ,modify_user_id "
                                + " ,if_flag "
                                + " ) "
                                + " SELECT '" + ccDetailInfo .order_detail_id+ "' order_detail_id "
                                + " ,'" + ccDetailInfo.order_id + "' order_id "
                                + " ,'" + ccDetailInfo.ref_order_detail_id + "' ref_order_detail_id "
                                + " ,'" + ccDetailInfo.order_no + "' order_no "
                                + " ,'" + ccDetailInfo.stock_balance_id + "' stock_balance_id "
                                + " ,'" + ccDetailInfo.sku_id + "' sku_id "
                                + " ,'" + ccDetailInfo.warehouse_id + "' warehouse_id "
                                + " ,'" + ccDetailInfo.end_qty + "' end_qty "
                                + " ,'" + ccDetailInfo.order_qty + "' order_qty "
                                + " ,'" + ccDetailInfo.remark + "' remark "
                                + " ,'" + ccDetailInfo.display_index + "' display_index "
                                + " ,'" + ccDetailInfo.create_time + "' create_time "
                                + " ,'" + ccDetailInfo.create_user_id + "' create_user_id "
                                + " ,'" + ccDetailInfo.modify_time + "' modify_time "
                                + " ,'" + ccDetailInfo.modify_user_id + "' modify_user_id "
                                + " ,'" + ccDetailInfo.if_flag + "' if_flag ;";
                    #endregion
                }
            }
            
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;

        }

        /// <summary>
        /// 判断盘点单号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_no">订单号</param>
        /// <param name="order_id">订单标识</param>
        /// <returns></returns>
        public bool IsExistOrderCode( string order_no, string order_id)
        {
            try
            {
                PublicService pService = new PublicService();
                string sql = "select count(*) From T_CC  where 1=1  and order_no = '"+order_no+"'";
                sql = pService.GetLinkSql(sql, "order_id", order_id, "!=");

                int n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 盘点单提交到表单中
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="ccInfo"></param>
        /// <returns></returns>
        private bool SetInoutOrderInsertBill( CCInfo ccInfo)
        {
            try
            {
                //BillModel bill = new BillModel();
                //BillService bs = new BillService(loggingSessionInfo);

                //bill.Id = ccInfo.order_id;
                //string order_type_id = bs.GetBillKindByCode(loggingSessionInfo, ccInfo.BillKindCode).Id.ToString(); //loggingSession, OrderType
                //bill.Code = bs.GetBillNextCode(loggingSessionInfo, ccInfo.BillKindCode); //BillKindCode
                //bill.KindId = order_type_id;
                //bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                //bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                //BillOperateStateService state = bs.InsertBill(loggingSessionInfo, bill);

                //if (state == BillOperateStateService.CreateSuccessful)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                return false;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 更新状态
        #region 状态修改
        /// <summary>
        /// 修改出入库单据状态
        /// </summary>
        /// <param name="ccInfo"></param>
        /// <returns></returns>
        public bool SetCCStatusUpdate(CCInfo  ccInfo)
        {
            string sql = "update T_CC set [status] = '" + ccInfo.status + "' ,status_desc = '" + ccInfo.status_desc + "'"
                       + " ,Modify_Time = '" + ccInfo.modify_time + "' ,Modify_User_Id = '" + ccInfo.modify_user_id + "' ";
            PublicService pService = new PublicService();
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_user_id", ccInfo.approve_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_time", ccInfo.approve_time);

            sql = sql + " ,if_flag = '0' where order_id = '" + ccInfo.order_id + "'";

            this.SQLHelper.ExecuteNonQuery(sql);

            return true;
        }
        #endregion
        #endregion
    }
}
