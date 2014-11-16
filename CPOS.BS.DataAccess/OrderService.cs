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


namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 订单数据类
    /// </summary>
    public class OrderService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public OrderService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region  查询
        /// <summary>
        /// 获取查询总行数
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public int SearchOrderCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql)); ;
        }
        /// <summary>
        /// 获取查询当前页显示信息
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchOrderInfo(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo);
            #region
            sql = sql + "select a.order_id "
                        + " ,a.order_type_id "
                        + " ,a.order_reason_type_id "
                        + " ,a.red_flag "
                        + " ,a.order_no "
                        + " ,a.ref_order_no "
                        + " ,a.ref_order_type_id "
                        + " ,a.ref_order_id "
                        + " ,a.warehouse_id "
                        + " ,a.order_date "
                        + " ,a.request_date "
                        + " ,a.promise_date "
                        + " ,a.complete_date "
                        + " ,a.create_unit_id "
                        + " ,a.purchase_unit_id "
                        + " ,a.sales_unit_id "
                        + " ,a.unit_id "
                        + " ,a.pos_id "
                        + " ,a.total_amount "
                        + " ,a.total_qty "
                        + " ,a.discount_rate "
                        + " ,a.actual_amount "
                        + " ,a.receive_points "
                        + " ,a.pay_points "
                        + " ,a.address_1 "
                        + " ,a.address_2 "
                        + " ,a.zip "
                        + " ,a.phone "
                        + " ,a.fax "
                        + " ,a.email "
                        + " ,a.remark "
                        + " ,a.carrier_id "
                        + " ,a.print_times "
                        + " ,a.order_status "
                        + " ,a.order_status_desc "
                        + " ,a.create_time "
                        + " ,a.create_user_id "
                        + " ,a.modify_time "
                        + " ,a.modify_user_id "
                        + " ,a.send_user_id "
                        + " ,a.send_time "
                        + " ,a.accept_user_id "
                        + " ,a.accept_date "
                        + " ,a.approve_user_id "
                        + " ,a.approve_time "
                        + " ,a.customer_id "
                        + " ,(select order_type_code From T_Order_Type where order_type_id = a.order_type_id) order_type_code  "
                        + " ,(select reason_type_code From T_Order_Reason_Type where reason_type_id = a.order_reason_type_id) order_reason_code "
                        + " ,(select order_type_name From T_Order_Type where order_type_id = a.order_type_id) order_type_name "
                        + " ,(select reason_type_name From T_Order_Reason_Type where reason_type_id = a.order_reason_type_id) order_reason_name "
                        + " ,(select unit_name From t_unit where unit_id = a.create_unit_id) create_unit_name "
                        + " ,(select USER_NAME From T_User where user_id = a.create_user_id) create_user_name  "
                        + " ,(select USER_NAME From T_User where user_id = a.modify_user_id) modify_user_name "
                        + " ,(select USER_NAME From T_User where user_id = a.approve_user_id) approve_user_name "
                        + " ,(select unit_name From t_unit where unit_id = a.purchase_unit_id) purchase_unit_name  "
                        + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
                        + " ,(select unit_name From t_unit where unit_id = a.sales_unit_id) sales_unit_name"
                      + " ,b.row_no "
                      + " ,@iCount icount "
                      + " From T_Order a "
                      + " inner join @TmpTable b "
                      + " on(a.order_id = b.order_id) "
                      + " where 1=1 "
                      + " and b.row_no > '" + orderSearchInfo.StartRow + "' and b.row_no <= '" + orderSearchInfo.EndRow + "' order by a.order_date desc, a.order_no desc;";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 查询公共部分
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public string GetSearchPublicSql(OrderSearchInfo orderSearchInfo)
        {
            #region
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
                      + " from t_order a  where 1=1 ";
            sql = pService.GetLinkSql(sql, "a.order_id", orderSearchInfo.order_id, "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", orderSearchInfo.customer_id, "%");
            sql = pService.GetLinkSql(sql, "a.order_no", orderSearchInfo.order_no, "%");
            sql = pService.GetLinkSql(sql, "a.order_type_id", orderSearchInfo.order_type_id, "%");
            sql = pService.GetLinkSql(sql, "a.order_reason_type_id", orderSearchInfo.order_reason_id, "%");
            sql = pService.GetLinkSql(sql, "a.unit_id", orderSearchInfo.unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.red_flag", orderSearchInfo.red_flag, "=");
            sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_begin, ">=");
            sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_end, "<=");
            sql = pService.GetLinkSql(sql, "a.request_date", orderSearchInfo.request_date_begin, ">=");
            sql = pService.GetLinkSql(sql, "a.request_date", orderSearchInfo.request_date_end, "<=");
            //sql = pService.GetLinkSql(sql, "a.order_status", orderSearchInfo.status, "=");
            if (orderSearchInfo.status != null && orderSearchInfo.status.Length > 0)
                sql += string.Format("a.order_status in ({0})", orderSearchInfo.status.Aggregate("", (i, j) => i + string.Format("'{0}',", j)).Trim(','));
            sql = pService.GetLinkSql(sql, "a.warehouse_id", orderSearchInfo.warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.ref_order_no", orderSearchInfo.ref_order_no, "%");
            sql = pService.GetLinkSql(sql, "a.data_from_id", orderSearchInfo.data_from_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_unit_id", orderSearchInfo.sales_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_unit_id", orderSearchInfo.purchase_unit_id, "=");

            if (orderSearchInfo.item_name != null && !orderSearchInfo.item_name.Equals(""))
            {
                sql = sql + " and a.order_id in (select distinct x.order_id From t_order_detail x "
                      + " inner join T_Sku y"
                      + " on(x.sku_id = y.sku_id)"
                      + " inner join T_Item z"
                      + " on(y.item_id = z.item_id) ";

                if (orderSearchInfo.item_name != null && !orderSearchInfo.item_name.Equals(""))
                {
                    sql = sql + " z.item_name like '%' + '" + orderSearchInfo.item_name + "' + '%' ";
                    sql = sql + " or z.item_code like '%' + '" + orderSearchInfo.item_name + "' + '%' ";
                }


                sql = sql + " ) ";
            }

            sql = sql + " ) x ; ";

            sql = sql + " Declare @iCount int;";

            sql = sql + " select @iCount = COUNT(*) From @TmpTable;";
            #endregion
            return sql;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 订单保存
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="IsTrans"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetOrderInfo(OrderInfo orderInfo, bool IsTrans, out string strError)
        {
            using (IDbTransaction tran = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    //1.判断重复
                    if (!IsExistOrderCode(orderInfo.order_no, orderInfo.order_id, tran))
                    {
                        strError = "订单号码已经存在。";
                        throw (new System.Exception(strError));
                    }

                    //2.判断是否有上级订单信息
                    if (orderInfo.ref_order_no != null && (!orderInfo.ref_order_no.Equals("")) && (orderInfo.ref_order_id == null || orderInfo.ref_order_id.Equals("")))
                    {
                        string refOrderId = GetOrderIdByOrderCode(orderInfo.ref_order_no, tran);
                        if (refOrderId != null && (!refOrderId.Equals("")))
                        {
                            orderInfo.ref_order_id = refOrderId;
                        }
                    }



                    //4.提交inout与inoutdetail信息
                    if (!UpdateOrder(orderInfo, tran))
                    {
                        strError = "更新主单据主表失败";
                        throw (new System.Exception(strError));
                    }
                    if (!InsertOrder(orderInfo, tran))
                    {
                        strError = "插入主单据主表失败";
                        throw (new System.Exception(strError));
                    }
                    if (!DeleteOrderDetail(orderInfo, tran))
                    {
                        strError = "删除订单单据明细失败";
                        throw (new System.Exception(strError));
                    }
                    if (orderInfo.orderDetailList != null)
                    {
                        foreach (OrderDetailInfo orderDetailInfo in orderInfo.orderDetailList)
                        {
                            if (!InsertOrderDetail(orderDetailInfo, tran))
                            {
                                strError = "删除订单单据明细失败!";
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
        /// 判断调价单号码是否重复
        /// </summary>
        /// <param name="order_no">订单号码</param>
        /// <param name="order_id">订单标识</param>
        /// <param name="pTran">是否事物</param>
        /// <returns></returns>
        public bool IsExistOrderCode(string order_no, string order_id, IDbTransaction pTran)
        {
            try
            {
                string sql = "select isnull(count(*),0) From T_Order where 1=1 and order_no = '" + order_no + "' ";
                PublicService pService = new PublicService();
                sql = pService.GetLinkSql(sql, "order_id", order_id, "!=");

                int n = 0;
                if (pTran != null)
                {
                    n = Convert.ToInt32(this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, sql, null));
                }
                else
                {
                    n = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
                }
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 根据订单号，获取订单标识
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public string GetOrderIdByOrderCode(string orderNo, IDbTransaction pTran)
        {
            string order_id = string.Empty;
            string sql = "select top 1 order_id From ( "
                      + " select order_id From t_order where order_no = '" + orderNo + "' "
                      + " )x";
            if (pTran != null)
            {
                order_id = Convert.ToString(this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, sql, null));
            }
            else
            {
                order_id = Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
            }
            return order_id;

        }

        /// <summary>
        /// 更新出入库表主信息
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="pTran"></param>
        private bool UpdateOrder(OrderInfo orderInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "update t_order set order_no = '" + orderInfo.order_no + "',if_flag = '" + orderInfo.if_flag + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "order_type_id", orderInfo.order_type_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_reason_type_id", orderInfo.order_reason_type_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "red_flag", orderInfo.red_flag);
            sql = pService.GetIsNotNullUpdateSql(sql, "ref_order_id", orderInfo.ref_order_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "ref_order_no", orderInfo.ref_order_no);
            sql = pService.GetIsNotNullUpdateSql(sql, "warehouse_id", orderInfo.warehouse_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_date", orderInfo.order_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "request_date", orderInfo.request_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "complete_date", orderInfo.complete_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "promise_date", orderInfo.promise_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "create_unit_id", orderInfo.create_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_id", orderInfo.unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "pos_id", orderInfo.pos_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "total_amount", orderInfo.total_amount.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "total_qty", orderInfo.total_qty.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "discount_rate", orderInfo.discount_rate.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "actual_amount", orderInfo.actual_amount.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "receive_points", orderInfo.receive_points.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "pay_points", orderInfo.pay_points.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "print_times", orderInfo.print_times.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "carrier_id", orderInfo.carrier_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "remark", orderInfo.remark);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_status", orderInfo.order_status);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_status_desc", orderInfo.order_status_desc);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_time", orderInfo.approve_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_user_id", orderInfo.approve_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "send_user_id", orderInfo.send_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "send_time", orderInfo.send_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "accpect_time", orderInfo.accpect_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "accpect_user_id", orderInfo.accpect_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", orderInfo.create_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", orderInfo.create_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "sales_unit_id", orderInfo.sales_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "purchase_unit_id", orderInfo.purchase_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "customer_id", this.loggingSessionInfo.CurrentLoggingManager.Customer_Id);

            sql = sql + " where order_id = '" + orderInfo.order_id + "' ;";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 插入出入库表主信息
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool InsertOrder(OrderInfo orderInfo, IDbTransaction pTran)
        {
            #region

            string sql = "insert into t_order( "
                      + " order_id "
                      + " ,order_no "
                      + " ,order_type_id "
                      + " ,order_reason_type_id "
                      + " ,red_flag "
                      + " ,ref_order_id "
                      + " ,ref_order_no "
                      + " ,warehouse_id "
                      + " ,order_date "
                      + " ,request_date "
                      + " ,promise_date "
                      + " ,complete_date "
                      + " ,create_unit_id "
                      + " ,unit_id "
                      + " ,total_amount "
                      + " ,total_qty "
                      + " ,discount_rate "
                      + " ,actual_amount "
                      + " ,receive_points "
                      + " ,pay_points "
                      + " ,print_times "
                      + " ,carrier_id "
                      + " ,remark "
                      + " ,order_status "
                      + " ,order_status_desc "
                      + " ,create_time "
                      + " ,create_user_id "
                      + " ,approve_time "
                      + " ,approve_user_id "
                      + " ,send_time "
                      + " ,send_user_id "
                      + " ,modify_time "
                      + " ,modify_user_id "
                      + " ,sales_unit_id "
                      + " ,purchase_unit_id "
                      + " ,if_flag "
                      + " ,customer_id "
                      + " )  "
                      + " select a.* From ( "
                      + " select '" + orderInfo.order_id + "' order_id "
                      + " ,'" + orderInfo.order_no + "' order_no "
                      + " ,'" + orderInfo.order_type_id + "' order_type_id "
                      + " ,'" + orderInfo.order_reason_type_id + "' order_reason_id "
                      + " ,'" + orderInfo.red_flag + "' red_flag "
                      + " ,'" + orderInfo.ref_order_id + "' ref_order_id "
                      + " ,'" + orderInfo.ref_order_no + "' ref_order_no "
                      + " ,'" + orderInfo.warehouse_id + "' warehouse_id "
                      + " ,'" + orderInfo.order_date + "' order_date "
                      + " ,'" + orderInfo.request_date + "' request_date "
                      + " ,'" + orderInfo.promise_date + "' promise_date "
                      + " ,'" + orderInfo.complete_date + "' complete_date "
                      + " ,'" + orderInfo.create_unit_id + "' create_unit_id "
                      + " ,'" + orderInfo.unit_id + "' unit_id "
                      + " ,'" + orderInfo.total_amount + "' total_amount "
                      + " ,'" + orderInfo.total_qty + "' total_qty "
                      + " ,'" + orderInfo.discount_rate + "' discount_rate "
                      + " ,'" + orderInfo.actual_amount + "' actual_amount "
                      + " ,'" + orderInfo.receive_points + "' receive_points "
                      + " ,'" + orderInfo.pay_points + "' pay_points "
                      + " ,'" + orderInfo.print_times + "' print_times "
                      + " ,'" + orderInfo.carrier_id + "' carrier_id "
                      + " ,'" + orderInfo.remark + "' remark "
                      + " ,'" + orderInfo.order_status + "' status "
                      + " ,'" + orderInfo.order_status_desc + "' status_desc "
                      + " ,'" + orderInfo.create_time + "' create_time "
                      + " ,'" + orderInfo.create_user_id + "' create_user_id "
                      + " ,'" + orderInfo.approve_time + "' approve_time "
                      + " ,'" + orderInfo.approve_user_id + "' approve_user_id "
                      + " ,'" + orderInfo.send_time + "' send_time "
                      + " ,'" + orderInfo.send_user_id + "' send_user_id "
                      + " ,'" + orderInfo.modify_time + "' modify_time "
                      + " ,'" + orderInfo.modify_user_id + "' modify_user_id "
                      + " ,'" + orderInfo.sales_unit_id + "' sales_unit_id "
                      + " ,'" + orderInfo.purchase_unit_id + "' purchase_unit_id "
                      + " ,'" + orderInfo.if_flag + "' if_flag"
                      + " ,'" + orderInfo.customer_id + "' customer_id"
                      + " ) a"
                      + " left join T_Order b"
                      + " on(a.order_id = b.order_id)"
                      + " where b.order_id is null ; ";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 删除出入库单据明细
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool DeleteOrderDetail(OrderInfo orderInfo, IDbTransaction pTran)
        {
            string sql = "delete t_order_detail where order_id = '" + orderInfo.order_id + "'";
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        /// <summary>
        /// 插入出库人单据明细
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool InsertOrderDetail(OrderDetailInfo orderDetailInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_order_detail "
                        + " ( "
                        + " order_detail_id "
                        + " ,order_id "
                        + " ,ref_order_detail_id "
                        + " ,sku_id "
                        + " ,unit_id "
                        + " ,order_qty "
                        + " ,enter_qty "
                        + " ,enter_price "
                        + " ,enter_amount "
                        + " ,std_price "
                        + " ,discount_rate "
                        + " ,order_discount_rate "
                        + " ,retail_price "
                        + " ,retail_amount "
                        + " ,receive_points "
                        + " ,pay_points "
                        + " ,remark "
                        + " ,display_index "
                        + " ,create_time "
                        + " ,create_user_id "
                        + " ,modify_time "
                        + " ,modify_user_id "
                        + " ,if_flag "
                        + " )"
                        + "select  '" + orderDetailInfo.order_detail_id + "' "
                        + " ,'" + orderDetailInfo.order_id + "'  "
                        + " ,'" + orderDetailInfo.ref_order_detail_id + "'  "
                        + " ,'" + orderDetailInfo.sku_id + "'  "
                        + " ,'" + orderDetailInfo.unit_id + "'  "
                        + " ,'" + orderDetailInfo.order_qty + "'  "
                        + " ,'" + orderDetailInfo.enter_qty + "'  "
                        + " ,'" + orderDetailInfo.enter_price + "'  "
                        + " ,'" + orderDetailInfo.enter_amount + "'  "
                        + " ,'" + orderDetailInfo.std_price + "'  "
                        + " ,'" + orderDetailInfo.discount_rate + "'  "
                        + " ,'" + orderDetailInfo.order_discount_rate + "'  "
                        + " ,'" + orderDetailInfo.retail_price + "'  "
                        + " ,'" + orderDetailInfo.retail_amount + "'  "
                        + " ,'" + orderDetailInfo.receive_points + "'  "
                        + " ,'" + orderDetailInfo.pay_points + "'  "
                        + " ,'" + orderDetailInfo.remark + "'  "
                        + " ,'" + orderDetailInfo.display_index + "'  "
                        + " ,'" + orderDetailInfo.create_time + "'  "
                        + " ,'" + orderDetailInfo.create_user_id + "'  "
                        + " ,'" + orderDetailInfo.modify_time + "'  "
                        + " ,'" + orderDetailInfo.modify_user_id + "'  "
                        + " ,'" + orderDetailInfo.if_flag + "'  ";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
            return true;
        }
        #endregion

        #region 状态修改
        /// <summary>
        /// 修改出入库单据状态
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public bool SetOrderStatusUpdate(OrderInfo orderInfo)
        {
            string sql = "update T_Order set [order_status] = '" + orderInfo.order_status + "' ,order_status_desc = '" + orderInfo.order_status_desc + "'"
                       + " ,Modify_Time = '" + orderInfo.modify_time + "' ,Modify_User_Id = '" + orderInfo.modify_user_id + "' ";
            PublicService pService = new PublicService();
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_user_id", orderInfo.approve_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_time", orderInfo.approve_time);

            sql = sql + " ,if_flag = '0' where order_id = '" + orderInfo.order_id + "'";

            this.SQLHelper.ExecuteNonQuery(sql);

            return true;
        }
        #endregion

        #region
        /// <summary>
        /// 根据单据标识，获取单据详细信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public DataSet GetOrderInfoById(string order_id)
        {
            #region
            string sql = "select a.order_id "
                        + " ,a.order_type_id "
                        + " ,a.order_reason_type_id "
                        + " ,a.red_flag "
                        + " ,a.order_no "
                        + " ,a.ref_order_no "
                        + " ,a.ref_order_type_id "
                        + " ,a.ref_order_id "
                        + " ,a.warehouse_id "
                        + " ,a.order_date "
                        + " ,a.request_date "
                        + " ,a.promise_date "
                        + " ,a.complete_date "
                        + " ,a.create_unit_id "
                        + " ,a.purchase_unit_id "
                        + " ,a.sales_unit_id "
                        + " ,a.unit_id "
                        + " ,a.pos_id "
                        + " ,a.total_amount "
                        + " ,a.total_qty "
                        + " ,a.discount_rate "
                        + " ,a.actual_amount "
                        + " ,a.receive_points "
                        + " ,a.pay_points "
                        + " ,a.address_1 "
                        + " ,a.address_2 "
                        + " ,a.zip "
                        + " ,a.phone "
                        + " ,a.fax "
                        + " ,a.email "
                        + " ,a.remark "
                        + " ,a.carrier_id "
                        + " ,a.print_times "
                        + " ,a.order_status "
                        + " ,a.order_status_desc "
                        + " ,a.create_time "
                        + " ,a.create_user_id "
                        + " ,a.modify_time "
                        + " ,a.modify_user_id "
                        + " ,a.send_user_id "
                        + " ,a.send_time "
                        + " ,a.accept_user_id "
                        + " ,a.accept_date "
                        + " ,a.approve_user_id "
                        + " ,a.approve_time "
                        + " ,a.customer_id "
                        + " ,(select order_type_code From T_Order_Type where order_type_id = a.order_type_id) order_type_code  "
                        + " ,(select reason_type_code From T_Order_Reason_Type where reason_type_id = a.order_reason_type_id) order_reason_code "
                        + " ,(select order_type_name From T_Order_Type where order_type_id = a.order_type_id) order_type_name "
                        + " ,(select reason_type_name From T_Order_Reason_Type where reason_type_id = a.order_reason_type_id) order_reason_name "
                        + " ,(select unit_name From t_unit where unit_id = a.create_unit_id) create_unit_name "
                        + " ,(select USER_NAME From T_User where user_id = a.create_user_id) create_user_name  "
                        + " ,(select USER_NAME From T_User where user_id = a.modify_user_id) modify_user_name "
                        + " ,(select USER_NAME From T_User where user_id = a.approve_user_id) approve_user_name "
                        + " ,(select unit_name From t_unit where unit_id = a.purchase_unit_id) purchase_unit_name  "
                        + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
                        + " ,(select unit_name From t_unit where unit_id = a.sales_unit_id) sales_unit_name"
                      + " From T_Order a "

                      + " where 1=1 and a.order_id = '" + order_id + "';";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 根据单据标识，获取单据明细详细信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetOrderDetailInfoByOrderId(string orderId)
        {
            #region
            string sql = "select a.order_detail_id "
                      + " ,a.order_id "
                      + " ,a.ref_order_detail_id "
                      + " ,a.sku_id "
                      + " ,a.unit_id "
                      + " ,convert(decimal(18,4),a.order_qty) order_qty " //*c.red_flag
                      + " ,convert(decimal(18,4),a.enter_qty) enter_qty " //*c.red_flag
                      + " ,convert(decimal(18,4),a.enter_price) enter_price " //*c.red_flag
                      + " ,convert(decimal(18,4),a.enter_amount) enter_amount " //*c.red_flag
                      + " ,convert(decimal(18,4),a.std_price) std_price "
                      + " ,a.discount_rate "
                      + " ,convert(decimal(18,4),a.retail_price) retail_price " //*c.red_flag
                      + " ,convert(decimal(18,4),a.retail_amount) retail_amount " //*c.red_flag
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.remark "
                      + " ,a.order_discount_rate"
                      + " ,a.display_index "
                      + " ,a.create_time "
                      + " ,a.create_user_id "
                      + " ,a.modify_time "
                      + " ,a.modify_user_id "
                      + " ,a.if_flag "
                      + " ,b.item_code "
                      + " ,b.item_name "
                      + " ,b.prop_1_detail_name "
                      + " ,b.prop_2_detail_name "
                      + " ,b.prop_3_detail_name "
                      + " ,b.prop_4_detail_name "
                      + " ,b.prop_5_detail_name "
                      + " ,(select discount_rate from t_order where order_id = a.order_id)  order_discount_rate "
                      + " ,c.order_status "
                      + " From t_order_detail a "
                      + " inner join vw_sku b "
                      + " on(a.sku_id = b.sku_id) "
                      + " inner join t_order c "
                      + " on(a.order_id = c.order_id) where a.order_id= '" + orderId + "' order by b.item_code";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 根据用户ID返回订单统计
        public DataSet GetCountByUserAndStatus(string customerid, string userid, string status)
        {
            StringBuilder temp = new StringBuilder();
            if (!string.IsNullOrEmpty(status))
                temp.AppendFormat(" and field7='{0}'", status);
            string str = string.Format(@"select field7 TypeCode,field10 Description,COUNT(*) [Count] from T_Inout
                                         where customer_id='{0}' and vip_no='{1}' and status <> '-99' and Field7 <> '-99' {2}
                                         group by field7,field10", customerid, userid, temp.ToString());
            var ds = this.SQLHelper.ExecuteDataset(str);
            return ds;
        }
        #endregion

        #region 更新订单的支付方式
        public bool SetOrderPaymentType(string orderid, string paymentid)
        {
            var str = string.Format("update t_inout set field11='{0}' where order_id='{1}'", paymentid, orderid);
            var i = this.SQLHelper.ExecuteNonQuery(str);
            return i > 0;
        }
        #endregion
    }
}
