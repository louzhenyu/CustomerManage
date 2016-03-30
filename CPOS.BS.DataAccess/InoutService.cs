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
    public class InoutService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public InoutService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        #region 查询
        /// <summary>
        /// 根据查询条件获取订单数量
        /// </summary>
        /// <param name="orderSearchInfo">查询条件对象</param>
        /// <returns></returns>
        public int SearchInoutCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo, 1);
            sql = sql + " select @iCount; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }


        public bool SetGetToPay(string pOrderId)
        {
            string sql = string.Format(@"
update T_Inout
set
pay_id=(select max(Payment_Type_Id) from T_Payment_Type 
where Payment_Type_Code='GetToPay')
where order_id='{0}'
", pOrderId);
            int nReturn = this.SQLHelper.ExecuteNonQuery(sql);
            if (nReturn == 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 查询各个状态的数量 Jermyn20130906
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchStatusTypeCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo, 0);
            sql += "SELECT x.StatusType,ISNULL(y.StatusCount,0) StatusCount FROM ( "
                + " SELECT '1' StatusType "
                + " UNION ALL "
                + " SELECT '2' StatusType "
                + " UNION ALL "
                + " SELECT '3' StatusType "
                + " UNION ALL "
                + " SELECT '0' StatusType "
                + " UNION ALL "
                + " SELECT '100' StatusType "
                + " UNION ALL "
                + " SELECT '200' StatusType "
                + " UNION ALL "
                + " SELECT '300' StatusType "
                + " UNION ALL "
                + " SELECT '400' StatusType "
                + " UNION ALL "
                + " SELECT '500' StatusType "
                + " UNION ALL "
                + " SELECT '600' StatusType "
                + " UNION ALL "
                + " SELECT '700' StatusType "
                + " UNION ALL "
                + " SELECT '800' StatusType "
                + " UNION ALL "
                + " SELECT '900' StatusType "
                + " ) x LEFT JOIN ( "
                + " SELECT isnull(a.Field7,2) StatusType,COUNT(*) StatusCount FROM dbo.T_Inout a "
                + " INNER JOIN @TmpTable b on(a.order_id = b.order_id) "
                + " WHERE a.Field7 IS NOT NULL AND a.Field7 <> '' "
                + " GROUP BY a.Field7 ) y ON(x.StatusType = y.StatusType) ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 根据查询条件获取出入库主信息
        /// </summary>
        /// <param name="orderSearchInfo">查询条件对象</param>
        /// <returns></returns>
        public DataSet SearchInoutInfo(OrderSearchInfo orderSearchInfo, bool isHotel)
        {
            string sql = GetSearchPublicSql(orderSearchInfo, 1);

            string sqlNew = string.Empty;
            //isHotel = false;
            if (isHotel)
            {
                sqlNew = "select  sum(sis.LowestPrice) as  priceNew,i.order_id  "
                    + " into #tempsum "
                    + " from T_Inout i left join T_Inout_Detail ind on "
                    + " i.order_id=ind.order_id "
                    + " left join T_Sku s on ind.sku_id=s.sku_id "
                    + " left join StoreItemDailyStatus sis on  sis.SkuID=ind.sku_id"
                    + " where ( sis.StatusDate between ind.Field1 and DATEADD(DAY,-1,convert(date,ind.Field2)) "
                    + " ) "
                    + " and i.order_id in (select order_id from @TmpTable) and i.customer_id='" + orderSearchInfo.customer_id + "'   group by i.order_id ; ";
            }

            #region
            sql = sql + sqlNew + "select distinct a.order_id "
                      + " ,a.remark"
                      + " ,(select OptionText from options where a.status=OptionValue and a.customer_id=CustomerID and OptionName='OrdersStatus') optiontext"
                      + " ,(select OptionValue from options where a.status=OptionValue and a.customer_id=CustomerID and OptionName='OrdersStatus') optionvalue"
                      + " ,a.order_no "
                      + " ,a.order_type_id "
                      + " ,a.order_reason_id "
                      + " ,a.red_flag "
                      + " ,a.ref_order_id "
                      + " ,a.ref_order_no "
                      + " ,a.warehouse_id "
                      + " ,a.order_date "
                      + " ,a.request_date "
                      + " ,a.complete_date "
                      + " ,a.create_unit_id "
                      + " ,a.unit_id "
                      + " ,(select unit_name From t_unit where unit_id = a.unit_id) unit_name "
                      + " ,a.related_unit_id "
                      + " ,a.related_unit_code "
                      + " ,a.pos_id "
                      + " ,a.shift_id "
                      + " ,a.sales_user ";
            if (isHotel)
            {
                sql = sql + " , ((select priceNew from #tempsum  where order_id= a.order_id)* a.total_qty * tid.discount_rate /100)  total_amount   ";
            }
            else
            {
                sql = sql + " ,convert(decimal(18,4),a.total_amount) total_amount "; //*red_flag
            }
            sql = sql + " ,a.discount_rate ";
            if (!isHotel)
            {
                sql = sql + " ,convert(decimal(18,4),a.actual_amount) actual_amount "; //*red_flag
            }
            sql = sql + " ,a.receive_points "
            + " ,a.pay_points "
            + " ,a.pay_id "
            + " ,a.print_times "
            + " ,a.carrier_id "
            + " ,(select top 1 unit_name from t_unit where unit_id=a.carrier_id) carrier_name "
            + " ,(select top 1 unit_address from t_unit where unit_id=a.carrier_id) carrier_address "
            + " ,(select top 1 unit_tel from t_unit where unit_id=a.carrier_id) carrier_tel"
            + " ,a.remark "
            + " ,a.status "
            + " ,a.status_desc "
            + " ,convert(decimal(18,4),a.total_qty) total_qty " //*red_flag
            + " ,convert(decimal(18,4),a.total_retail) total_retail " //*red_flag
            + " ,a.keep_the_change "
            + " ,a.wiping_zero "
            + " ,a.vip_no "
            + " ,(select top 1 vipName from vip where vipId=a.vip_no) vip_name "
            + " ,(select top 1 vipLevel from vip where vipId=a.vip_no) vipLevel "
            + " ,(select top 1 svcg.VipCardGradeName from vip v LEFT JOIN dbo.SysVipCardGrade svcg ON v.VipLevel = svcg.VipCardGradeID where vipId = a.vip_no) vipLevelDesc "
            + " ,(select top 1 VIPID from vip where vipId=a.vip_no) vipId "
            + " ,(select top 1 WeiXinUserId from vip where vipId=a.vip_no) openId "
            + " ,(select top 1 Phone from vip where vipId=a.vip_no) phone "
            + " ,(select top 1 VipRealName from vip where vipId=a.vip_no) vipRealName "
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
            + " ,a.sales_unit_id "
            + " ,a.purchase_unit_id "
            + " ,a.data_from_id "
            + " ,a.sales_warehouse_id "
            + " ,a.purchase_warehouse_id "
            + " ,(select vipsourceName From SysVipSource where vipsourceId = a.data_from_id) data_from_name "
            + " ,(select order_type_code From T_Order_Type where order_type_id = a.order_type_id) order_type_code "
            + " ,(select reason_type_code From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_code "
            + " ,(select order_type_name From T_Order_Type where order_type_id = a.order_type_id) order_type_name "
            + " ,(select reason_type_name From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_name "
            + " ,(select unit_name From t_unit where unit_id = a.create_unit_id) create_unit_name "
            + " ,(select USER_NAME From T_User where user_id = a.create_user_id) create_user_name "
            + " ,(select USER_NAME From T_User where user_id = a.modify_user_id) modify_user_name "
            + " ,(select USER_NAME From T_User where user_id = a.approve_user_id) approve_user_name "
            + " ,(select USER_NAME From T_User where user_id = a.accpect_user_id) accpect_user_name "
            + " ,(select USER_NAME From T_User where user_id = a.send_user_id) send_user_name "
            + " ,(select unit_name From t_unit where unit_id = a.sales_unit_id) sales_unit_name "
            + " ,(select unit_name From t_unit where unit_id = a.purchase_unit_id) purchase_unit_name "
            + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
            + " ,(select wh_name From t_warehouse where warehouse_id = a.sales_warehouse_id) sales_warehouse_name "
            + " ,(select wh_name From t_warehouse where warehouse_id = a.purchase_warehouse_id) purchase_warehouse_name "
            + " ,b.row_no "
            + " ,@iCount icount "
            + " ,a.Field1 "
              + " ,a.Field2 "
              + " ,a.Field3 "
              + " ,a.Field4 as address"
              + " ,a.Field5 "
              + " ,a.Field6  as linkTel"
              + " ,a.Field7 "
              + " ,a.Field8 "
              + " ,a.Field9 "
              + " ,a.Field10 "
              + " ,a.Field12 "
              + " ,a.Field13 "
              + " ,a.Field14 as linkMan"
              + " ,a.Field15 "
              + " ,a.Field16 "
              + " ,a.Field17 "
              + " ,a.Field18 "
              + " ,a.Field19 "
              + " ,a.Field20 "
              + " ,(select DeliveryName From Delivery x WHERE x.DeliveryId = a.Field8 ) DeliveryName"
              + " ,(select DefrayTypeName From DefrayType x WHERE x.DefrayTypeId = a.Field11 ) DefrayTypeName "
              + " ,(SELECT dbo.DateToTimestamp(a.create_time)) timestamp "
              + " ,abs(isnull(c.integral,0)) as integral"
              + " ,f.ParValue couponAmount"
              + " ,abs(g.amount) as vipEndAmount";
            if (isHotel)
            {
                sql = sql + ", (((select priceNew from #tempsum  where order_id= a.order_id)-( case when  f.ParValue-(abs(isnull(c.integral,0))) is NULL then 0 else f.ParValue-(abs(isnull(c.integral,0))) end) - (CASE when  abs(g.amount) is NULL then 0 else abs(g.amount) end) ) * a.total_qty * tid.discount_rate /100) actual_amount   ";
            }
            sql = sql + " From T_Inout a "
            + " inner join @TmpTable b "
            + " on(a.order_id = b.order_id) "
            + " left join vipIntegralDetail c"
            + " on a.order_id = c.objectId and a.vip_no = c.VIPID and c.IntegralSourceID = 20"
            + " left join TOrderCouponMapping d"
            + " on a.order_id = d.orderId"
            + " left join Coupon e"
            + " on d.couponId = e.couponId"
            + " left join CouponType f"
            + " on CONVERT(nvarchar(200),e.couponTypeId)  = CONVERT(nvarchar(200),f.couponTypeId)"
            + " left join VipAmountDetail g"
            + " on a.order_id = g.objectId and a.vip_no = g.VipId and g.AmountSourceId =1 ";
            if (isHotel)
            {
                sql = sql + " left join T_Inout_Detail tid on a.order_id=tid.order_id ";
            }
            sql=sql+ " where 1=1 "
            + " and b.row_no > '" + orderSearchInfo.StartRow + "' and b.row_no <= '" + orderSearchInfo.EndRow + "' order by a.create_time desc, a.order_date desc,a.modify_time desc,a.order_no desc;";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 获取出入库单据查询脚本公共部分
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        private string GetSearchPublicSql(OrderSearchInfo orderSearchInfo, int IsStatus)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "Declare @TmpTable Table "
                      + " (order_id nvarchar(100) "
                      + " ,row_no int "
                      + " ); "

                      + " insert into @TmpTable (order_id,row_no) "
                      + " select distinct x.order_id,row_no=row_number() over(order by x.order_date desc,x.create_time DESC,x.order_no desc) From ( "
                      + " select distinct "
                      + " order_date  "
                      + " ,create_time  "
                      + " ,order_no  "
                      + " ,order_id  "
                      + " from t_inout a ";
            if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            {
                sql += " inner join vw_unit_level b on ((a.purchase_unit_id=b.unit_id or a.sales_unit_id=b.unit_id) and b.customer_id='" + orderSearchInfo.customer_id + "') ";
            }
            sql += "  where 1=1 and a.status != '-1'";
            if (orderSearchInfo.order_id == null || orderSearchInfo.order_id.Equals(""))
            {
                sql = sql + " and isnull(a.Field7,'0') <> '-99' ";
            }
            sql = pService.GetLinkSql(sql, "a.order_id", orderSearchInfo.order_id, "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", orderSearchInfo.customer_id, "%");
            sql = pService.GetLinkSql(sql, "a.order_no", orderSearchInfo.order_no, "%");
            sql = pService.GetLinkSql(sql, "a.order_type_id", orderSearchInfo.order_type_id, "%");
            //sql = pService.GetLinkSql(sql, "a.order_reason_id", orderSearchInfo.order_reason_id, "%");//由于前面写死，导致抢购看不到详细信息，所以先注释掉
            sql = pService.GetLinkSql(sql, "a.unit_id", orderSearchInfo.unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_begin, ">=");
            sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_end, "<=");
            sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_begin, ">=");
            sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_end, "<=");
            sql = pService.GetLinkSql(sql, "a.status", orderSearchInfo.status, "=");
            sql = pService.GetLinkSql(sql, "a.warehouse_id", orderSearchInfo.warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.ref_order_no", orderSearchInfo.ref_order_no, "%");
            sql = pService.GetLinkSql(sql, "a.data_from_id", orderSearchInfo.data_from_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_unit_id", orderSearchInfo.sales_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_unit_id", orderSearchInfo.purchase_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.red_flag", orderSearchInfo.red_flag, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_warehouse_id", orderSearchInfo.purchase_warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_warehouse_id", orderSearchInfo.sales_warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.vip_no", orderSearchInfo.vip_no, "=");
            //发货时间
            sql = pService.GetLinkSql(sql, "a.send_time", orderSearchInfo.DeliveryDateBegin, ">=");
            sql = pService.GetLinkSql(sql, "a.send_time", orderSearchInfo.DeliveryDateEnd, "<=");
            #region 取消时间
            if (orderSearchInfo.DeliveryStatus != null && orderSearchInfo.DeliveryStatus.Equals("0"))
            {
                sql = pService.GetLinkSql(sql, "a.modify_time", orderSearchInfo.CancelDateBegin, ">=");
                sql = pService.GetLinkSql(sql, "a.modify_time", orderSearchInfo.CancelDateEnd, "<=");
            }
            #endregion

            if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            {
                sql += " and b.path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' ";
            }

            //Jermyn20130905 订单配送状态
            if (IsStatus == 1)
            {
                sql = pService.GetLinkSql(sql, "a.status", orderSearchInfo.DeliveryStatus, "=");
            }
            sql = pService.GetLinkSql(sql, "a.Field8", orderSearchInfo.DeliveryId, "=");
            sql = pService.GetLinkSql(sql, "a.Field11", orderSearchInfo.DefrayTypeId, "=");

            if (orderSearchInfo.timestamp != null && orderSearchInfo.timestamp.Length > 0)
            {
                sql += string.Format(" and (a.modify_time < (SELECT dbo.TimestampToDate('{0}')) )", orderSearchInfo.timestamp);
            }

            if (orderSearchInfo.item_name != null && !orderSearchInfo.item_name.Equals(""))
            {
                sql = sql + " and a.order_id in (select distinct x.order_id From t_inout_detail x "
                      + " inner join T_Sku y"
                      + " on(x.sku_id = y.sku_id)"
                      + " inner join T_Item z"
                      + " on(y.item_id = z.item_id) ";
                sql = sql + " z.item_name like '%' + '" + orderSearchInfo.item_name + "' + '%' ";
                sql = sql + " or z.item_code like '%' + '" + orderSearchInfo.item_name + "' + '%'  )";
            }


            sql = sql + " ) x ; ";

            sql = sql + " Declare @iCount int;";

            sql = sql + " select @iCount = COUNT(*) From @TmpTable;";
            #endregion
            return sql;
        }

        /// <summary>
        /// 根据单据标识，获取单据详细信息
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public DataSet GetInoutInfoById(string order_id)
        {
            #region
            string sql = "select a.order_id "
                      + " ,a.customer_id"
                      + " ,a.order_no "
                      + " ,a.order_type_id "
                      + " ,a.order_reason_id "
                      + " ,a.red_flag "
                      + " ,a.ref_order_id "
                      + " ,a.ref_order_no "
                      + " ,a.warehouse_id "
                      + " ,a.order_date "
                      + " ,a.request_date "
                      + " ,a.complete_date "
                      + " ,a.create_unit_id "
                      + " ,a.unit_id "
                      + " ,(select unit_name From t_unit where unit_id = a.unit_id) unit_name "
                      + " ,a.related_unit_id "
                      + " ,a.related_unit_code "
                      + " ,a.pos_id "
                      + " ,a.shift_id "
                      + " ,a.sales_user "
                      + " ,convert(decimal(18,4),a.total_amount) total_amount " //*red_flag
                      + " ,a.discount_rate "
                      + " ,convert(decimal(18,4),a.actual_amount) actual_amount " //*red_flag
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.pay_id "
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.pay_id) payment_name "
                      + " ,a.print_times "
                      + " ,a.carrier_id "
                      + " ,(select top 1 unit_name From t_unit where unit_id = a.carrier_id) carrier_name "
                      + " ,a.remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,convert(decimal(18,4),a.total_qty) total_qty " //*red_flag
                      + " ,convert(decimal(18,4),a.total_retail) total_retail " //*red_flag
                      + " ,a.keep_the_change "
                      + " ,a.wiping_zero "
                      + " ,a.vip_no "
                      + " ,isnull(isnull((select top 1 vipName From vip where vipId = a.vip_no),a.Field3),a.Field6) vip_name "
                      + " ,(select top 1 vipCode From vip where vipId = a.vip_no) vip_code "
                      + " ,(select top 1 phone From vip where vipId = a.vip_no) vipPhone "
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
                      + " ,a.sales_unit_id "
                      + " ,a.purchase_unit_id "
                      + " ,a.sales_warehouse_id "
                      + " ,a.purchase_warehouse_id "
                      + " ,a.data_from_id "
                      + " ,(select vipsourceName From SysVipSource where vipsourceId = a.data_from_id) data_from_name "
                      + " ,(select order_type_code From T_Order_Type where order_type_id = a.order_type_id) order_type_code "
                      + " ,(select reason_type_code From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_code "
                      + " ,(select order_type_name From T_Order_Type where order_type_id = a.order_type_id) order_type_name "
                      + " ,(select reason_type_name From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_name "
                      + " ,(select unit_name From t_unit where unit_id = a.create_unit_id) create_unit_name "
                      + " ,(select USER_NAME From T_User where user_id = a.create_user_id) create_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.modify_user_id) modify_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.approve_user_id) approve_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.accpect_user_id) accpect_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.send_user_id) send_user_name "
                      + " ,(select unit_name From t_unit where unit_id = a.sales_unit_id) sales_unit_name "
                      + " ,(select unit_name From t_unit where unit_id = a.purchase_unit_id) purchase_unit_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.sales_warehouse_id) sales_warehouse_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.purchase_warehouse_id) purchase_warehouse_name "

                      + " ,a.Field1 "
                        + " ,a.Field2 "
                        + " ,a.Field3 "
                        + " ,a.Field4 "
                        + " ,a.Field5 "
                        + " ,a.Field6 "
                        + " ,a.Field7 "
                        + " ,a.Field8 "
                        + " ,a.Field9 "
                        + " ,a.Field10 "
                        + " ,a.Field12 "
                        + " ,a.Field13 "
                        + " ,a.Field14 "
                        + " ,a.Field15 "
                        + " ,a.Field16 "
                        + " ,a.Field17 "
                        + " ,a.Field18 "
                        + " ,a.Field19 "
                        + " ,a.Field20 "
                         + " ,(select DeliveryName From Delivery x WHERE x.DeliveryId = a.Field8 ) DeliveryName"
                        + " ,(select DefrayTypeName From DefrayType x WHERE x.DefrayTypeId = a.Field11 ) DefrayTypeName "
                      + " From T_Inout a "

                      + " where 1=1 and a.order_id = '" + order_id + "';";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }



        public string GetCustomerName(string customerId)
        {
            string sql = string.Format(
                @"select unit_name from t_unit a 
where a.customer_id='{0}' 
and a.type_id=(select MAX(type_id) from T_Type where type_code = '总部') ",
                customerId);
            var result = SQLHelper.ExecuteScalar(sql);
            return result == null || result == DBNull.Value ? string.Empty : result.ToString();
        }

        /// <summary>
        /// 根据单据号获取单据详细信息 Jermyn20130917
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public DataSet GetInoutInfoByOrderCode(string orderCode)
        {
            #region
            string sql = "select a.order_id "
                      + " ,a.order_no "
                      + " ,a.order_type_id "
                      + " ,a.order_reason_id "
                      + " ,a.red_flag "
                      + " ,a.ref_order_id "
                      + " ,a.ref_order_no "
                      + " ,a.warehouse_id "
                      + " ,a.order_date "
                      + " ,a.request_date "
                      + " ,a.complete_date "
                      + " ,a.create_unit_id "
                      + " ,a.unit_id "
                      + " ,(select unit_name From t_unit where unit_id = a.unit_id) unit_name "
                      + " ,a.related_unit_id "
                      + " ,a.related_unit_code "
                      + " ,a.pos_id "
                      + " ,a.shift_id "
                      + " ,a.sales_user "
                      + " ,convert(decimal(18,4),a.total_amount) total_amount " //*red_flag
                      + " ,a.discount_rate "
                      + " ,convert(decimal(18,4),a.actual_amount) actual_amount " //*red_flag
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.pay_id "
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.pay_id) payment_name "
                      + " ,a.print_times "
                      + " ,a.carrier_id "
                      + " ,(select top 1 unit_name From t_unit where unit_id = a.carrier_id) carrier_name "
                      + " ,a.remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,convert(decimal(18,4),a.total_qty) total_qty " //*red_flag
                      + " ,convert(decimal(18,4),a.total_retail) total_retail " //*red_flag
                      + " ,a.keep_the_change "
                      + " ,a.wiping_zero "
                      + " ,a.vip_no "
                      + " ,(select top 1 vipName From vip where vipId = a.vip_no) vip_name "
                      + " ,(select top 1 vipCode From vip where vipId = a.vip_no) vip_code "
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
                      + " ,a.sales_unit_id "
                      + " ,a.purchase_unit_id "
                      + " ,a.sales_warehouse_id "
                      + " ,a.purchase_warehouse_id "
                      + " ,a.data_from_id "
                      + " ,(select vipsourceName From SysVipSource where vipsourceId = a.data_from_id) data_from_name "
                      + " ,(select order_type_code From T_Order_Type where order_type_id = a.order_type_id) order_type_code "
                      + " ,(select reason_type_code From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_code "
                      + " ,(select order_type_name From T_Order_Type where order_type_id = a.order_type_id) order_type_name "
                      + " ,(select reason_type_name From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_name "
                      + " ,(select unit_name From t_unit where unit_id = a.create_unit_id) create_unit_name "
                      + " ,(select USER_NAME From T_User where user_id = a.create_user_id) create_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.modify_user_id) modify_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.approve_user_id) approve_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.accpect_user_id) accpect_user_name "
                      + " ,(select USER_NAME From T_User where user_id = a.send_user_id) send_user_name "
                      + " ,(select unit_name From t_unit where unit_id = a.sales_unit_id) sales_unit_name "
                      + " ,(select unit_name From t_unit where unit_id = a.purchase_unit_id) purchase_unit_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.warehouse_id) warehouse_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.sales_warehouse_id) sales_warehouse_name "
                      + " ,(select wh_name From t_warehouse where warehouse_id = a.purchase_warehouse_id) purchase_warehouse_name "

                      + " ,a.Field1 "
                        + " ,a.Field2 "
                        + " ,a.Field3 "
                        + " ,a.Field4 "
                        + " ,a.Field5 "
                        + " ,a.Field6 "
                        + " ,a.Field7 "
                        + " ,a.Field8 "
                        + " ,a.Field9 "
                        + " ,a.Field10 "
                        + " ,a.Field12 "
                        + " ,a.Field13 "
                        + " ,a.Field14 "
                        + " ,a.Field15 "
                        + " ,a.Field16 "
                        + " ,a.Field17 "
                        + " ,a.Field18 "
                        + " ,a.Field19 "
                        + " ,a.Field20 "
                         + " ,(select DeliveryName From Delivery x WHERE x.DeliveryId = a.Field8 ) DeliveryName"
                        + " ,(select DefrayTypeName From DefrayType x WHERE x.DefrayTypeId = a.Field11 ) DefrayTypeName "
                      + " From T_Inout a "

                      + " where 1=1 and a.order_no = '" + orderCode + "';";
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
        public DataSet GetInoutDetailInfoByOrderId(string orderId)
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
                      + " ,convert(decimal(18,4),a.plan_price) plan_price "
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.remark "
                      + " ,a.pos_order_code "
                      + " ,a.order_detail_status "
                      + " ,a.display_index "
                      + " ,a.create_time "
                      + " ,a.create_user_id "
                      + " ,a.modify_time "
                      + " ,a.modify_user_id "
                      + " ,a.ref_order_id "
                      + " ,a.if_flag "
                      + " ,b.item_id "
                      + " ,b.item_code "
                      + " ,b.item_name "
                      + " ,isnull(b.prop_1_detail_name,'') prop_1_detail_name "
                      + " ,isnull(b.prop_2_detail_name,'') prop_2_detail_name "
                      + " ,isnull(b.prop_3_detail_name,'') prop_3_detail_name "
                      + " ,isnull(b.prop_4_detail_name,'') prop_4_detail_name "
                      + " ,isnull(b.prop_5_detail_name,'') prop_5_detail_name "
                      + " ,(select discount_rate from t_inout where order_id = a.order_id)  order_discount_rate "
                      + " ,isnull((select i.ifservice from dbo.T_Item i where i.item_id in (select item_id from dbo.T_Sku s where s.sku_id = a.sku_id  )),0) as IfService "
                      + " ,a.Field1 "
                        + " ,a.Field2 "
                        + " ,a.Field3 "
                        + " ,a.Field4 "
                        + " ,a.Field5 "
                        + " ,a.Field6 "
                        + " ,a.Field7 "
                        + " ,a.Field8 "
                        + " ,a.Field9 "
                        + " ,a.Field10,SalesPrice "
                        + ",(SELECT y.item_category_name  FROM dbo.T_Item x INNER JOIN dbo.T_Item_Category y ON(x.item_category_id = y.item_category_id) WHERE x.item_id = b.item_id ) itemCategoryName "
                        + " ,isnull(datediff(day,a.Field1,a.Field2),0) DayCount "
                      + " From t_inout_detail a "
                      + " left join vw_sku b "
                      + " on(a.sku_id = b.sku_id) "
                      + " inner join t_inout c "
                      + @" on(a.order_id = c.order_id)  left join vw_item_detail d on d.item_id=b.item_id  where a.order_id= '" + orderId + "' order by b.item_code";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 获取订单详细列表中的商品规格
        public DataSet GetInoutDetailGgByOrderId(string orderId)
        {
            var paras = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@pOrderId", Value = orderId}
            };
            var sql = new StringBuilder();
            sql.Append(" select isnull(a.sku_id,'')sku_id,");
            sql.Append(" isnull(b.prop_1_name,'') prop_1_name,isnull(b.prop_1_detail_name,'') prop_1_detail_name");
            sql.Append(" ,isnull(b.prop_2_name,'') prop_2_name,isnull(b.prop_2_detail_name,'') prop_2_detail_name");
            sql.Append(" ,isnull(b.prop_3_name,'') prop_3_name,isnull(b.prop_3_detail_name,'') prop_3_detail_name");
            sql.Append(" ,isnull(b.prop_4_name,'') prop_4_name,isnull(b.prop_4_detail_name,'') prop_4_detail_name");
            sql.Append(" ,isnull(b.prop_5_name,'') prop_5_name,isnull(b.prop_5_detail_name,'') prop_5_detail_name");
            sql.Append("  from T_Inout_Detail a");
            sql.Append("  left join vw_sku b on a.sku_id = b.sku_id");
            sql.Append("  inner join t_inout c on a.order_id = c.order_id ");
            sql.Append("  where c.order_id = @pOrderId");
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }
        #endregion



        #region insert or update or delete
        /// <summary>
        /// inout 单据保存
        /// </summary>
        /// <param name="inoutInfo">inout model</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <param name="strError">输出错误信息</param>
        /// <returns></returns>
        public bool SetInoutInfo(InoutInfo inoutInfo, bool IsTrans, out string strError)
        {
            var tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    //1.判断重复
                    if (!IsExistOrderCode(inoutInfo.order_no, inoutInfo.customer_id, inoutInfo.order_id, tran))
                    {
                        strError = "订单号码已经存在。";
                        throw (new System.Exception(strError));
                    }

                    //2.判断是否有上级订单信息
                    if (inoutInfo.ref_order_no != null && (!inoutInfo.ref_order_no.Equals("")) && (inoutInfo.ref_order_id == null || inoutInfo.ref_order_id.Equals("")))
                    {
                        string refOrderId = GetOrderIdByOrderCode(inoutInfo.ref_order_no, tran);
                        if (refOrderId != null && (!refOrderId.Equals("")))
                        {
                            inoutInfo.ref_order_id = refOrderId;
                        }
                    }

                    //4.提交inout与inoutdetail信息
                    if (!UpdateInout(inoutInfo, tran))
                    {
                        strError = "更新出入库单据主表失败";
                        throw (new System.Exception(strError));
                    }
                    if (!InsertInout(inoutInfo, tran))
                    {
                        strError = "插入出入库单据主表失败";
                        throw (new System.Exception(strError));
                    }
                    
                    if (!DeleteInoutDetail(inoutInfo, tran))
                    {
                        strError = "删除出入库单据明细失败";
                        throw (new System.Exception(strError));
                    }
                    if (inoutInfo.InoutDetailList != null)
                    {
                        foreach (InoutDetailInfo inoutDetailInfo in inoutInfo.InoutDetailList)
                        {
                            if (!InsertInoutDetail(inoutDetailInfo, tran))
                            {
                                strError = "插入出入库单据明细失败!";
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
        public bool IsExistOrderCode(string order_no, string customerId, string order_id, IDbTransaction pTran)
        {
            try
            {
                //update by wzq 2014-07-16 添加客户过滤条件
                var sql =
                    string.Format("select isnull(count(*),0) From T_Inout where order_no ='{0}' and order_id !='{1}' and customer_id ='{2}' ",
                    order_no, order_id, customerId);

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
                      + " select order_id From t_inout where order_no = '" + orderNo + "' "
                      + " union all "
                      + " select order_id From T_CC where order_no = '" + orderNo + "' "
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

        #region 根据订单号获取订单ID 2014-10-21

        /// <summary>
        /// 根据订单号获取订单ID
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        public string GetOrderIDByOrderNo(string orderNo, string customerID)
        {
            string order_id = string.Empty;
            string sql = "   select order_id from t_inout where order_no= '" + orderNo + "' and customer_id='" + customerID + "' ";

            order_id = Convert.ToString(this.SQLHelper.ExecuteScalar(sql));
            return order_id;

        }
        #endregion

        /// <summary>
        /// 更新出入库表主信息
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        private bool UpdateInout(InoutInfo inoutInfo, IDbTransaction pTran)
        {
            PublicService pService = new PublicService();
            #region
            string sql = "update t_inout set order_no = '" + inoutInfo.order_no + "',if_flag = '" + inoutInfo.if_flag + "' ";
            sql = pService.GetIsNotNullUpdateSql(sql, "order_type_id", inoutInfo.order_type_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_reason_id", inoutInfo.order_reason_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "red_flag", inoutInfo.red_flag);
            sql = pService.GetIsNotNullUpdateSql(sql, "ref_order_id", inoutInfo.ref_order_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "ref_order_no", inoutInfo.ref_order_no);
            sql = pService.GetIsNotNullUpdateSql(sql, "warehouse_id", inoutInfo.warehouse_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "order_date", inoutInfo.order_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "request_date", inoutInfo.request_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "complete_date", inoutInfo.complete_date);
            sql = pService.GetIsNotNullUpdateSql(sql, "create_unit_id", inoutInfo.create_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "unit_id", inoutInfo.unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "related_unit_id", inoutInfo.related_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "related_unit_code", inoutInfo.related_unit_code);
            sql = pService.GetIsNotNullUpdateSql(sql, "pos_id", inoutInfo.pos_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "shift_id", inoutInfo.shift_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "sales_user", inoutInfo.sales_user);
            sql = pService.GetIsNotNullUpdateSql(sql, "total_amount", inoutInfo.total_amount.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "discount_rate", inoutInfo.discount_rate.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "actual_amount", inoutInfo.actual_amount.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "receive_points", inoutInfo.receive_points.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "pay_points", inoutInfo.pay_points.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "print_times", inoutInfo.print_times.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "carrier_id", inoutInfo.carrier_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "remark", inoutInfo.remark.Replace("'",""));
            sql = pService.GetIsNotNullUpdateSql(sql, "status", inoutInfo.status);
            sql = pService.GetIsNotNullUpdateSql(sql, "status_desc", inoutInfo.status_desc);
            sql = pService.GetIsNotNullUpdateSql(sql, "total_qty", inoutInfo.total_qty.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "total_retail", inoutInfo.total_retail.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "keep_the_change", inoutInfo.keep_the_change.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "wiping_zero", inoutInfo.wiping_zero.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "vip_no", inoutInfo.vip_no);
            sql = pService.GetIsNotNullUpdateSql(sql, "VipCardCode", inoutInfo.vip_code);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_time", inoutInfo.approve_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_user_id", inoutInfo.approve_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "send_user_id", inoutInfo.send_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "send_time", inoutInfo.send_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "accpect_time", inoutInfo.accpect_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "accpect_user_id", inoutInfo.accpect_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_time", inoutInfo.create_time);
            sql = pService.GetIsNotNullUpdateSql(sql, "modify_user_id", inoutInfo.create_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "sales_unit_id", inoutInfo.sales_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "purchase_unit_id", inoutInfo.purchase_unit_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "sales_warehouse_id", inoutInfo.sales_warehouse_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "purchase_warehouse_id", inoutInfo.purchase_warehouse_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "ReturnCash", inoutInfo.ReturnCash.ToString());

            sql = pService.GetIsNotNullUpdateSql(sql, "Field1", inoutInfo.Field1);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field2", inoutInfo.Field2);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field3", inoutInfo.Field3);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field4", inoutInfo.Field4);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field5", inoutInfo.Field5);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field6", inoutInfo.Field6);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field7", inoutInfo.Field7);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field8", inoutInfo.Field8);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field9", inoutInfo.Field9);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field10", inoutInfo.Field10);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field11", inoutInfo.Field11);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field12", inoutInfo.Field12);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field13", inoutInfo.Field13);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field14", inoutInfo.Field14);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field15", inoutInfo.Field15);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field16", inoutInfo.Field16);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field17", inoutInfo.Field17);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field18", inoutInfo.Field18);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field19", inoutInfo.Field19);
            sql = pService.GetIsNotNullUpdateSql(sql, "Field20", inoutInfo.Field20);

            sql = sql + " where order_id = '" + inoutInfo.order_id + "' ;";
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
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool InsertInout(InoutInfo inoutInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_inout( "
                      + " order_id "
                      + " ,order_no "
                      + " ,order_type_id "
                      + " ,order_reason_id "
                      + " ,red_flag "
                      + " ,ref_order_id "
                      + " ,ref_order_no "
                      + " ,warehouse_id "
                      + " ,order_date "
                      + " ,request_date "
                      + " ,complete_date "
                      + " ,create_unit_id "
                      + " ,unit_id "
                      + " ,related_unit_id "
                      + " ,related_unit_code "
                      + " ,pos_id "
                      + " ,shift_id "
                      + " ,sales_user "
                      + " ,total_amount "
                      + " ,discount_rate "
                      + " ,actual_amount "
                      + " ,receive_points "
                      + " ,pay_points "
                      + " ,pay_id "
                      + " ,print_times "
                      + " ,carrier_id "
                      + " ,remark "
                      + " ,status "
                      + " ,status_desc "
                      + " ,total_qty "
                      + " ,total_retail "
                      + " ,keep_the_change "
                      + " ,wiping_zero "
                      + " ,vip_no "
                      + " ,VipCardCode "
                      + " ,create_time "
                      + " ,create_user_id "
                      + " ,approve_time "
                      + " ,approve_user_id "
                      + " ,send_time "
                      + " ,send_user_id "
                      + " ,accpect_time "
                      + " ,accpect_user_id "
                      + " ,modify_time "
                      + " ,modify_user_id "
                      + " ,sales_unit_id "
                      + " ,purchase_unit_id "
                      + " ,data_from_id "
                      + " ,if_flag "
                      + " ,customer_id ,sales_warehouse_id,purchase_warehouse_id"
                      + " ,ReturnCash"
                      + " ,Field1"
                      + " ,Field2"
                      + " ,Field3"
                      + " ,Field4"
                      + " ,Field5"
                      + " ,Field6"
                      + " ,Field7"
                      + " ,Field8"
                      + " ,Field9"
                      + " ,Field10"
                      + " ,Field11"
                      + " ,Field12"
                      + " ,Field13"
                      + " ,Field14"
                      + " ,Field15"
                      + " ,Field16"
                      + " ,Field17"
                      + " ,Field18"
                      + " ,Field19"
                      + " ,Field20"
                      + " )  "
                      + " select a.* From ( "
                      + " select '" + inoutInfo.order_id + "' order_id "
                      + " ,'" + inoutInfo.order_no + "' order_no "
                      + " ,'" + inoutInfo.order_type_id + "' order_type_id "
                      + " ,'" + inoutInfo.order_reason_id + "' order_reason_id "
                      + " ,'" + inoutInfo.red_flag + "' red_flag "
                      + " ,'" + inoutInfo.ref_order_id + "' ref_order_id "
                      + " ,'" + inoutInfo.ref_order_no + "' ref_order_no "
                      + " ,'" + inoutInfo.warehouse_id + "' warehouse_id "
                      + " ,'" + inoutInfo.order_date + "' order_date "
                      + " ,'" + inoutInfo.request_date + "' request_date "
                      + " ,'" + inoutInfo.complete_date + "' complete_date "
                      + " ,'" + inoutInfo.create_unit_id + "' create_unit_id "
                      + " ,'" + inoutInfo.unit_id + "' unit_id "
                      + " ,'" + inoutInfo.related_unit_id + "' related_unit_id "
                      + " ,'" + inoutInfo.related_unit_code + "' related_unit_code "
                      + " ,'" + inoutInfo.pos_id + "' pos_id "
                      + " ,'" + inoutInfo.shift_id + "' shift_id "
                      + " ,'" + inoutInfo.sales_user + "' sales_user "
                      + " ,'" + inoutInfo.total_amount + "' total_amount "
                      + " ,'" + inoutInfo.discount_rate + "' discount_rate "
                      + " ,'" + inoutInfo.actual_amount + "' actual_amount "
                      + " ,'" + inoutInfo.receive_points + "' receive_points "
                      + " ,'" + inoutInfo.pay_points + "' pay_points "
                      + " ,'" + inoutInfo.pay_id + "' pay_id "
                      + " ,'" + inoutInfo.print_times + "' print_times "
                      + " ,'" + inoutInfo.carrier_id + "' carrier_id "
                      + " ,'" + inoutInfo.remark.Replace("'","") + "' remark "
                      + " ,'" + inoutInfo.status + "' status "
                      + " ,'" + inoutInfo.status_desc + "' status_desc "
                      + " ,'" + inoutInfo.total_qty + "' total_qty "
                      + " ,'" + inoutInfo.total_retail + "' total_retail "
                      + " ,'" + inoutInfo.keep_the_change + "' keep_the_change "
                      + " ,'" + inoutInfo.wiping_zero + "' wiping_zero "
                      + " ,'" + inoutInfo.vip_no + "' vip_no "
                      + " ,'" + inoutInfo.vip_code + "' VipCardCode "
                      + " ,'" + inoutInfo.create_time + "' create_time "
                      + " ,'" + inoutInfo.create_user_id + "' create_user_id "
                      + " ,'" + inoutInfo.approve_time + "' approve_time "
                      + " ,'" + inoutInfo.approve_user_id + "' approve_user_id "
                      + " ,'" + inoutInfo.send_time + "' send_time "
                      + " ,'" + inoutInfo.send_user_id + "' send_user_id "
                      + " ,'" + inoutInfo.accpect_time + "' accpect_time "
                      + " ,'" + inoutInfo.accpect_user_id + "' accpect_user_id "
                      + " ,'" + inoutInfo.modify_time + "' modify_time "
                      + " ,'" + inoutInfo.modify_user_id + "' modify_user_id "
                      + " ,'" + inoutInfo.sales_unit_id + "' sales_unit_id "
                      + " ,'" + inoutInfo.purchase_unit_id + "' purchase_unit_id "
                      + " ,'" + inoutInfo.data_from_id + "' data_from_id"
                      + " ,'" + inoutInfo.if_flag + "' if_flag"
                      + " ,'" + inoutInfo.customer_id + "' customer_id"
                      + " ,'" + inoutInfo.sales_warehouse_id + "' sales_warehouse_id"
                      + " ,'" + inoutInfo.purchase_warehouse_id + "' purchase_warehouse_id"
                      + " ,'" + inoutInfo.ReturnCash + "' ReturnCash"
                      + " ,'" + inoutInfo.Field1 + "' Field1 "
                      + " ,'" + inoutInfo.Field2 + "' Field2 "
                      + " ,'" + inoutInfo.Field3 + "' Field3 "
                      + " ,'" + inoutInfo.Field4 + "' Field4 "
                      + " ,'" + inoutInfo.Field5 + "' Field5 "
                      + " ,'" + inoutInfo.Field6 + "' Field6 "
                      + " ,'" + inoutInfo.Field7 + "' Field7 "
                      + " ,'" + inoutInfo.Field8 + "' Field8 "
                      + " ,'" + inoutInfo.Field9 + "' Field9 "
                      + " ,'" + inoutInfo.Field10 + "' Field10 "
                      + " ,'" + inoutInfo.Field11 + "' Field11 "
                      + " ,'" + inoutInfo.Field12 + "' Field12 "
                      + " ,'" + inoutInfo.Field13 + "' Field13 "
                      + " ,'" + inoutInfo.Field14 + "' Field14 "
                      + " ,'" + inoutInfo.Field15 + "' Field15 "
                      + " ,'" + inoutInfo.Field16 + "' Field16 "
                      + " ,'" + inoutInfo.Field17 + "' Field17 "
                      + " ,'" + inoutInfo.Field18 + "' Field18 "
                      + " ,'" + inoutInfo.Field19 + "' Field19 "
                      + " ,'" + inoutInfo.Field20 + "' Field20 "
                      + " ) a"
                      + " left join T_Inout b"
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
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool DeleteInoutDetail(InoutInfo inoutInfo, IDbTransaction pTran)
        {
            string sql = "delete t_inout_detail where order_id = '" + inoutInfo.order_id + "'";
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
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        /// <returns></returns>
        private bool InsertInoutDetail(InoutDetailInfo inoutDetailInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into t_inout_detail "
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
                        + " ,retail_price "
                        + " ,retail_amount "
                        + " ,plan_price "
                        + " ,receive_points "
                        + " ,pay_points "
                        + " ,remark "
                        + " ,pos_order_code "
                        + " ,order_detail_status "
                        + " ,display_index "
                        + " ,create_time "
                        + " ,create_user_id "
                        + " ,modify_time "
                        + " ,modify_user_id "
                        + " ,ref_order_id "
                        + " ,if_flag "
                        + " ,Field1"
                        + " ,Field2 "
                        + " ,Field3 "
                        + " ,Field4 "
                        + " ,Field5 "
                        + " ,Field6 "
                        + " ,Field7 "
                        + " ,Field8 "
                        + " ,Field9 "
                        + " ,Field10 "
                        + " ,ReturnCash "
                        + " )"
                        + "select  '" + inoutDetailInfo.order_detail_id + "' "
                        + " ,'" + inoutDetailInfo.order_id + "'  "
                        + " ,'" + inoutDetailInfo.ref_order_detail_id + "'  "
                        + " ,'" + inoutDetailInfo.sku_id + "'  "
                        + " ,'" + inoutDetailInfo.unit_id + "'  "
                        + " ,'" + inoutDetailInfo.order_qty + "'  "
                        + " ,'" + inoutDetailInfo.enter_qty + "'  "
                        + " ,'" + inoutDetailInfo.enter_price + "'  "
                        + " ,'" + inoutDetailInfo.enter_amount + "'  "
                        + " ,'" + inoutDetailInfo.std_price + "'  "
                        + " ,'" + inoutDetailInfo.discount_rate + "'  "
                        + " ,'" + inoutDetailInfo.retail_price + "'  "
                        + " ,'" + inoutDetailInfo.retail_amount + "'  "
                        + " ,'" + inoutDetailInfo.plan_price + "'  "
                        + " ,'" + inoutDetailInfo.receive_points + "'  "
                        + " ,'" + inoutDetailInfo.pay_points + "'  "
                        + " ,'" + inoutDetailInfo.remark + "'  "
                        + " ,'" + inoutDetailInfo.pos_order_code + "'  "
                        + " ,'" + inoutDetailInfo.order_detail_status + "'  "
                        + " ,'" + inoutDetailInfo.display_index + "'  "
                        + " ,'" + inoutDetailInfo.create_time + "'  "
                        + " ,'" + inoutDetailInfo.create_user_id + "'  "
                        + " ,'" + inoutDetailInfo.modify_time + "'  "
                        + " ,'" + inoutDetailInfo.modify_user_id + "'  "
                        + " ,'" + inoutDetailInfo.ref_order_id + "'  "
                        + " ,'" + inoutDetailInfo.if_flag + "'  "
                        + " ,'" + inoutDetailInfo.Field1 + "' Field1 "
                        + " ,'" + inoutDetailInfo.Field2 + "' Field2 "
                        + " ,'" + inoutDetailInfo.Field3 + "' Field3 "
                        + " ,'" + inoutDetailInfo.Field4 + "' Field4 "
                        + " ,'" + inoutDetailInfo.Field5 + "' Field5 "
                        + " ,'" + inoutDetailInfo.Field6 + "' Field6 "
                        + " ,'" + inoutDetailInfo.Field7 + "' Field7 "
                        + " ,'" + inoutDetailInfo.Field8 + "' Field8 "
                        + " ,'" + inoutDetailInfo.Field9 + "' Field9 "
                        + " ,'" + inoutDetailInfo.Field10 + "' Field10 "
                        + " ,'" + inoutDetailInfo.ReturnCash + "'  "
                        ;
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
        /// 修改订单明细
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <param name="pTran"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool UpdateInoutDetail(InoutInfo inoutInfo, out string strError)
        {
            var tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    if (!DeleteInoutDetail(inoutInfo, tran))
                    {
                        strError = "删除出入库单据明细失败";
                        return false;
                        throw (new System.Exception(strError));

                    }
                    if (inoutInfo.InoutDetailList != null)
                    {
                        foreach (InoutDetailInfo inoutDetailInfo in inoutInfo.InoutDetailList)
                        {
                            if (!InsertInoutDetail(inoutDetailInfo, tran))
                            {
                                strError = "插入出入库单据明细失败!";
                                return false;
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
        #endregion

        #region 状态修改
        /// <summary>
        /// 修改出入库单据状态
        /// </summary>
        /// <param name="inoutInfo"></param>
        /// <returns></returns>
        public bool SetInoutStatus(InoutInfo inoutInfo)
        {
            string sql = "update T_Inout set [status] = '" + inoutInfo.status + "' ,status_desc = '" + inoutInfo.status_desc + "'"
                       + " ,Modify_Time = '" + inoutInfo.modify_time + "' ,Modify_User_Id = '" + inoutInfo.modify_user_id + "' ";
            PublicService pService = new PublicService();
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_user_id", inoutInfo.approve_user_id);
            sql = pService.GetIsNotNullUpdateSql(sql, "approve_time", inoutInfo.approve_time);

            sql = sql + " ,if_flag = '0' where order_id = '" + inoutInfo.order_id + "'";

            this.SQLHelper.ExecuteNonQuery(sql);

            return true;
        }
        #endregion

        #region

        /// <summary>
        /// 获取房态表内容
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public string SearchInoutDetailSqlNew(OrderSearchInfo orderSearchInfo)
        {
            string sql = "select a.order_detail_id "
                      + " ,a.order_id "
                      + " ,a.ref_order_detail_id "
                      + " ,a.sku_id "
                      + " ,a.unit_id "
                      + " ,convert(decimal(18,4),a.order_qty) order_qty " //*c.red_flag
                      + " ,convert(decimal(18,4),a.enter_qty) enter_qty " //*c.red_flag
                      + " ,convert(decimal(18,4), SUM(sis.LowestPrice)*c.total_qty ) enter_price " //*c.red_flag
                      + " ,convert(decimal(18,4),a.enter_amount) enter_amount " //*c.red_flag
                      + " ,convert(decimal(18,4),avg(sis.LowestPrice)) std_price "
                      + " ,a.discount_rate "
                      + " ,convert(decimal(18,4),a.retail_price) retail_price " //*c.red_flag
                      + " ,convert(decimal(18,4), SUM(sis.LowestPrice)*c.total_qty*a.discount_rate/100 )- (abs(isnull(f.ParValue,0))) -(abs(isnull(vid.integral,0))) -abs(isnull(g.amount,0))   retail_amount " //*c.red_flag
                      + " ,convert(decimal(18,4),a.plan_price) plan_price "
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.remark "
                      + " ,a.pos_order_code "
                      + " ,a.order_detail_status "
                      + " ,a.display_index "
                      + " ,a.create_time "
                      + " ,a.create_user_id "
                      + " ,a.modify_time "
                      + " ,a.modify_user_id "
                      + " ,a.ref_order_id "
                      + " ,a.if_flag "
                      + " ,b.item_code "
                      + " ,b.item_name "
                      + " ,b.prop_1_detail_name "
                      + " ,b.prop_2_detail_name "
                      + " ,b.prop_3_detail_name "
                      + " ,b.prop_4_detail_name "
                      + " ,b.prop_5_detail_name "
                      + " ,(select discount_rate from t_inout where order_id = a.order_id)  order_discount_rate ";
            sql += " ,displayIndex=row_number() over(order by a.create_time desc ) ";
            sql += " into #tmp ";
            sql += " From t_inout_detail a "
                 + " inner join vw_sku b "
                 + " on(a.sku_id = b.sku_id) "
                 + " inner join t_inout c "
                 + " on(a.order_id = c.order_id) "
                 + " left join StoreItemDailyStatus sis on sis.SkuID=a.sku_id "
                 + " left join vipIntegralDetail vid on c.order_id = vid.objectId and c.vip_no = vid.VIPID and vid.IntegralSourceID = 20"
                 + " left join TOrderCouponMapping d on c.order_id = d.orderId "
                 + " left join Coupon e on d.couponId = e.couponId "
                 + " left join CouponType f on e.couponTypeId = f.couponTypeId"
                 + " left join VipAmountDetail g on c.order_id = g.objectId and c.vip_no = g.VipId and g.AmountSourceId =1 "

                 + " where 1=1 ";
            if (orderSearchInfo.vip_no != null && orderSearchInfo.vip_no.Trim().Length > 0)
            {
                sql += " and c.vip_no='" + orderSearchInfo.vip_no + "' ";
            }
            if (orderSearchInfo.order_type_id != null && orderSearchInfo.order_type_id.Trim().Length > 0)
            {
                sql += " and c.order_type_id='" + orderSearchInfo.order_type_id + "' ";
            }
            if (orderSearchInfo.order_reason_id != null && orderSearchInfo.order_reason_id.Trim().Length > 0)
            {
                sql += " and c.order_reason_id='" + orderSearchInfo.order_reason_id + "' ";
            }
            if (orderSearchInfo.red_flag != null && orderSearchInfo.red_flag.Trim().Length > 0)
            {
                sql += " and c.red_flag='" + orderSearchInfo.red_flag + "' ";
            }

            sql += " and ( sis.StatusDate between a.Field1 and DATEADD(DAY,-1,convert(date,a.Field2))) ";

            sql += " group by a.order_detail_id ,a.order_id ,a.ref_order_detail_id ,a.sku_id ,a.unit_id "
            + " ,order_qty,enter_qty,enter_price,enter_amount,std_price,a.discount_rate,retail_price,"
            + " plan_price,a.receive_points ,a.pay_points ,a.remark ,a.pos_order_code ,a.order_detail_status ,"
            + " a.display_index ,a.create_time ,a.create_user_id ,a.modify_time ,a.modify_user_id ,a.ref_order_id ,"
            + " a.if_flag ,b.item_code ,b.item_name ,b.prop_1_detail_name ,b.prop_2_detail_name ,b.prop_3_detail_name ,"
            + " b.prop_4_detail_name ,b.prop_5_detail_name,vid.integral,f.ParValue, g.amount,c.total_qty ";

            sql += ";";
            return sql;
        }

        public string SearchInoutDetailSql(OrderSearchInfo orderSearchInfo)
        {
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
                      + " ,convert(decimal(18,4),a.plan_price) plan_price "
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.remark "
                      + " ,a.pos_order_code "
                      + " ,a.order_detail_status "
                      + " ,a.display_index "
                      + " ,a.create_time "
                      + " ,a.create_user_id "
                      + " ,a.modify_time "
                      + " ,a.modify_user_id "
                      + " ,a.ref_order_id "
                      + " ,a.if_flag "
                      + " ,b.item_code "
                      + " ,b.item_name "
                      + " ,b.prop_1_detail_name "
                      + " ,b.prop_2_detail_name "
                      + " ,b.prop_3_detail_name "
                      + " ,b.prop_4_detail_name "
                      + " ,b.prop_5_detail_name "
                      + " ,(select discount_rate from t_inout where order_id = a.order_id)  order_discount_rate ";
            sql += " ,displayIndex=row_number() over(order by a.create_time desc ) ";
            sql += " into #tmp ";
            sql += " From t_inout_detail a "
                 + " inner join vw_sku b "
                 + " on(a.sku_id = b.sku_id) "
                 + " inner join t_inout c "
                 + " on(a.order_id = c.order_id) "
                 + " where 1=1 ";
            if (orderSearchInfo.vip_no != null && orderSearchInfo.vip_no.Trim().Length > 0)
            {
                sql += " and c.vip_no='" + orderSearchInfo.vip_no + "' ";
            }
            if (orderSearchInfo.order_type_id != null && orderSearchInfo.order_type_id.Trim().Length > 0)
            {
                sql += " and c.order_type_id='" + orderSearchInfo.order_type_id + "' ";
            }
            if (orderSearchInfo.order_reason_id != null && orderSearchInfo.order_reason_id.Trim().Length > 0)
            {
                sql += " and c.order_reason_id='" + orderSearchInfo.order_reason_id + "' ";
            }
            if (orderSearchInfo.red_flag != null && orderSearchInfo.red_flag.Trim().Length > 0)
            {
                sql += " and c.red_flag='" + orderSearchInfo.red_flag + "' ";
            }

            sql += ";";
            return sql;
        }
        /// <summary>
        /// 获取单据明细详细信息
        /// </summary>
        public DataSet SearchInoutDetailInfo(OrderSearchInfo orderSearchInfo)
        {

            string countSql = "select COUNT(*) from T_Inout i ";
            countSql += " left join T_Inout_Detail ind on i.order_id=ind.order_id "
            + " left join T_Sku s on ind.sku_id=s.sku_id "
            + " left join StoreItemDailyStatus sis on  sis.SkuID=ind.sku_id "
            + " where i.customer_id='" + orderSearchInfo.customer_id + "' ";

            string sql = string.Empty;

            DataSet dsTemp=this.SQLHelper.ExecuteDataset(countSql);

            if (dsTemp.Tables[0].Rows[0][0].ToString() != "0")
            {
                //房态表中有信息 花间堂定制
                sql += SearchInoutDetailSqlNew(orderSearchInfo);
            }
            else
            {
                sql += SearchInoutDetailSql(orderSearchInfo);
            }

            sql += " select * from #tmp a where a.displayindex between '" + orderSearchInfo.StartRow +
                "' and '" + orderSearchInfo.EndRow + "' order by a.displayindex";

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);

            if (dsTemp.Tables[0].Rows[0][0].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
            {
                sql = SearchInoutDetailSql(orderSearchInfo);
                sql += " select * from #tmp a where a.displayindex between '" + orderSearchInfo.StartRow +
                "' and '" + orderSearchInfo.EndRow + "' order by a.displayindex";
                ds = this.SQLHelper.ExecuteDataset(sql);
            }
            return ds;
        }



        /// <summary>
        /// 获取单据明细详细信息数量
        /// </summary>
        public int SearchInoutDetailCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = SearchInoutDetailSql(orderSearchInfo);
            sql += " select count(*) from #tmp ";
            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count;
        }
        #endregion

        #region 获取vip消费记录
        public DataSet SearchInoutDetailInfoByVipList(OrderSearchInfo orderSearchInfo)
        {
            string sql = SearchInoutDetailInfoByVipSql(orderSearchInfo);
            sql += " select * from #tmp a where a.displayindex between '" + orderSearchInfo.StartRow +
                "' and '" + orderSearchInfo.EndRow + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public int SearchInoutDetailInfoByVipCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = SearchInoutDetailInfoByVipSql(orderSearchInfo);
            sql += " select count(*) from #tmp ";
            int count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count;
        }
        private string SearchInoutDetailInfoByVipSql(OrderSearchInfo orderSearchInfo)
        {
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
                      + " ,convert(decimal(18,4),a.plan_price) plan_price "
                      + " ,a.receive_points "
                      + " ,a.pay_points "
                      + " ,a.remark "
                      + " ,a.pos_order_code "
                      + " ,a.order_detail_status "
                      + " ,a.display_index "
                      + " ,a.create_time "
                      + " ,a.create_user_id "
                      + " ,a.modify_time "
                      + " ,a.modify_user_id "
                      + " ,a.ref_order_id "
                      + " ,a.if_flag "
                      + " ,b.item_code "
                      + " ,b.item_name "
                      + " ,b.prop_1_detail_name "
                      + " ,b.prop_2_detail_name "
                      + " ,b.prop_3_detail_name "
                      + " ,b.prop_4_detail_name "
                      + " ,b.prop_5_detail_name "
                      + " ,(SELECT x.unit_name FROM t_unit x WHERE x.unit_id = c.unit_id) unit_name"
                      + " ,(select discount_rate from t_inout where order_id = a.order_id)  order_discount_rate ";
            //sql += " ,displayIndex=row_number() over(order by a.create_time desc ) ";
            sql += " into #tmp1 ";
            sql += " From t_inout_detail a "
                 + " inner join vw_sku b "
                 + " on(a.sku_id = b.sku_id) "
                 + " inner join t_inout c "
                 + " on(a.order_id = c.order_id) "
                 + " where 1=1 ";
            if (orderSearchInfo.vip_no != null && orderSearchInfo.vip_no.Trim().Length > 0)
            {
                sql += " and c.vip_no='" + orderSearchInfo.vip_no + "' ";
            }
            if (orderSearchInfo.order_type_id != null && orderSearchInfo.order_type_id.Trim().Length > 0)
            {
                sql += " and c.order_type_id='" + orderSearchInfo.order_type_id + "' ";
            }
            if (orderSearchInfo.order_reason_id != null && orderSearchInfo.order_reason_id.Trim().Length > 0)
            {
                sql += " and c.order_reason_id='" + orderSearchInfo.order_reason_id + "' ";
            }
            if (orderSearchInfo.red_flag != null && orderSearchInfo.red_flag.Trim().Length > 0)
            {
                sql += " and c.red_flag='" + orderSearchInfo.red_flag + "' ";
            }

            if (orderSearchInfo.unit_id == null || orderSearchInfo.unit_id.Equals(""))
            {

                sql += "; SELECT *,displayIndex=row_number() over(order by enter_amount desc ) into #tmp FROM #tmp1 WHERE unit_id IN ( SELECT TOP 3 unit_id FROM #tmp1 GROUP BY unit_id ORDER BY SUM(enter_amount) desc ) ; ";
            }
            else
            {
                sql += "; select *,displayIndex=row_number() over(order by enter_amount desc ) into #tmp From #tmp1 where unit_id = '" + orderSearchInfo.unit_id + "'; ";
            }

            sql += ";";

            return sql;
        }
        #endregion

        #region 根据区县ID + 时间戳获取这段时间内发生订单的门店ID

        /// <summary>
        /// 根据区县ID + 时间戳获取这段时间内发生订单的门店ID
        /// </summary>
        /// <param name="cityId">区县ID</param>
        /// <param name="timestamp">时间戳</param>
        public DataSet GetUnitIdList(string cityId, string timestamp)
        {
            string sql = string.Empty;

            sql += " SELECT DISTINCT UnitId into #tmp FROM ( ";
            sql += " SELECT DISTINCT sales_unit_id AS UnitId FROM dbo.T_Inout ";
            sql += " WHERE order_type_id = '1F0A100C42484454BAEA211D4C14B80F' ";
            sql += "   AND order_reason_id = '2F6891A2194A4BBAB6F17B4C99A6C6F5' ";
            sql += "   AND red_flag = '1' ";
            sql += "   AND (sales_unit_id IN (SELECT unit_id FROM dbo.t_unit WHERE unit_city_id = '" + cityId + "') " +
                "  or purchase_unit_id   IN (SELECT unit_id FROM dbo.t_unit WHERE unit_city_id = 'B4CEDBEE501443E4BA297B3332304F1B') )";

            //时间戳为0时， 查找最近5分钟的数据
            if (timestamp.Equals("0"))
            {
                sql += " AND modify_time >= DATEADD(MINUTE, -5, GETDATE()) ";
            }
            else
            {
                //sql += " AND modify_time >= dbo.TimestampToDate('" + timestamp + "') ";
                sql += " AND modify_time >= '" + timestamp + "'";
            }

            sql += " union all ";

            sql += " SELECT DISTINCT purchase_unit_id AS UnitId FROM dbo.T_Inout ";
            sql += " WHERE order_type_id = '1F0A100C42484454BAEA211D4C14B80F' ";
            sql += "   AND order_reason_id = '2F6891A2194A4BBAB6F17B4C99A6C6F5' ";
            sql += "   AND red_flag = '1' ";
            sql += "   AND (sales_unit_id IN (SELECT unit_id FROM dbo.t_unit WHERE unit_city_id = '" + cityId + "') " +
                "  or purchase_unit_id   IN (SELECT unit_id FROM dbo.t_unit WHERE unit_city_id = 'B4CEDBEE501443E4BA297B3332304F1B') )";

            //时间戳为0时， 查找最近5分钟的数据
            if (timestamp.Equals("0"))
            {
                sql += " AND modify_time >= DATEADD(MINUTE, -5, GETDATE()) ";
            }
            else
            {
                //sql += " AND modify_time >= dbo.TimestampToDate('" + timestamp + "') ";
                sql += " AND modify_time >= '" + timestamp + "'";
            }

            sql += " ) a; ";

            sql += "select a.UnitId From #tmp a inner join t_unit b on(a.unitId = b.unit_id) where b.type_id='EB58F1B053694283B2B7610C9AAD2742' ";
            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取销量前3的商品
        /// <summary>
        /// 获取销量前3的商品
        /// </summary>
        public DataSet GetInoutDetailInfoByTop3(OrderSearchInfo queryInfo)
        {
            #region
            string sql = "select a.*, d.item_code, d.item_name "
                + " from  "
                + " (select top 3 a.sku_id,sum(a.retail_amount) retail_amount "
                + " from t_inout_detail a "
                + " inner join t_inout b on (a.order_id = b.order_id ";
            if (queryInfo.unit_id != null && queryInfo.unit_id.Length > 0)
            {
                sql += string.Format(" and b.unit_id='{0}' ", queryInfo.unit_id);
            }
            if (queryInfo.order_type_id != null && queryInfo.order_type_id.Length > 0)
            {
                sql += string.Format(" and b.order_type_id='{0}' ", queryInfo.order_type_id);
            }
            if (queryInfo.order_reason_id != null && queryInfo.order_reason_id.Length > 0)
            {
                sql += string.Format(" and b.order_reason_id='{0}' ", queryInfo.order_reason_id);
            }
            if (queryInfo.red_flag != null && queryInfo.red_flag.Length > 0)
            {
                sql += string.Format(" and b.red_flag='{0}' ", queryInfo.red_flag);
            }
            if (queryInfo.order_date_begin != null && queryInfo.order_date_begin.Length > 0)
            {
                sql += string.Format(" and b.order_date>='{0}' ", queryInfo.order_date_begin);
            }
            sql += " ) ";

            sql += " where 1=1 group by a.sku_id) a "
                + " inner join vw_sku b on (a.sku_id = b.sku_id) "
                + " inner join t_item d on (b.item_id = d.item_id) "
                + " order by a.retail_amount desc "
                + "  ";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region GetInoutId
        /// <summary>
        /// 获取单据ID
        /// </summary>
        public string GetInoutId(InoutInfo queryInfo)
        {
            string sql = "select top 1 a.order_id ";
            sql += " from t_inout a where 1=1 ";
            if (queryInfo.sales_unit_id != null && queryInfo.sales_unit_id.Trim().Length > 0)
            {
                sql += string.Format(" and a.sales_unit_id='{0}' ", queryInfo.sales_unit_id.Trim());
            }
            if (queryInfo.customer_id != null && queryInfo.customer_id.Trim().Length > 0)
            {
                sql += string.Format(" and a.customer_id='{0}' ", queryInfo.customer_id.Trim());
            }
            if (queryInfo.order_type_id != null && queryInfo.order_type_id.Trim().Length > 0)
            {
                sql += string.Format(" and a.order_type_id='{0}' ", queryInfo.order_type_id.Trim());
            }
            if (queryInfo.order_reason_id != null && queryInfo.order_reason_id.Trim().Length > 0)
            {
                sql += string.Format(" and a.order_reason_id='{0}' ", queryInfo.order_reason_id.Trim());
            }
            if (queryInfo.red_flag != null && queryInfo.red_flag.Trim().Length > 0)
            {
                sql += string.Format(" and a.red_flag='{0}' ", queryInfo.red_flag.Trim());
            }
            //orderNo
            if (queryInfo.Field16 != null && queryInfo.Field16.Trim().Length > 0)
            {
                sql += string.Format(" and a.Field16='{0}' ", queryInfo.Field16.Trim());
            }
            var obj = this.SQLHelper.ExecuteScalar(sql);
            return obj == null || obj == DBNull.Value ? string.Empty : obj.ToString();
        }
        #endregion

        #region 已下订单数量(泸州老窖)
        public int GetHasOrderCount(string EventId)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_Inout WHERE Field18 = '" + EventId + "' ;";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取已付款订单数(泸州老窖)
        public int GetHasPayCount(string EventId)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_Inout WHERE Field18 = '" + EventId + "' AND ISNULL(total_retail,0) > 0 ;";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取已销售订单额(泸州老窖)
        public decimal GetHasSalesAmount(string EventId)
        {
            string sql = "SELECT SUM(total_amount) FROM dbo.T_Inout WHERE Field18 = '" + EventId + "' ;";
            return Convert.ToDecimal(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 获取已销售订单额(泸州老窖)
        public decimal GetHasTotalAmount()
        {
            string sql = "SELECT CONVERT(INT, SUM(total_amount)) FROM dbo.T_Inout ";
            return Convert.ToDecimal(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region 电子商城
        public void SetOrderPayment(JIT.CPOS.BS.Entity.Interface.SetOrderEntity SetOrderInfo)
        {
            string status = string.Empty;
            if (SetOrderInfo.Status == null)
            {
                status = "2";
            }
            else
            {
                status = SetOrderInfo.Status;
            }
            string sql = string.Empty;
            if (SetOrderInfo.OrderCode == null)
            {
                sql = " update t_inout set pay_id = '" + SetOrderInfo.PaymentTypeId + "' "
                           + ", total_retail = total_amount,Field15 = '" + SetOrderInfo.OutTradeNo + "' "
                           + ", Field7 = '" + status + "',Field10 = '" + SetOrderInfo.StatusDesc + "' "
                           + ", modify_time = '" + SetOrderInfo.PaymentTime + "' "
                           + " where order_id = '" + SetOrderInfo.OrderId + "'; ";

                if (status.Equals("0"))
                {
                    TOrderCouponMappingDAO mappingServer = new TOrderCouponMappingDAO(this.CurrentUserInfo);
                    mappingServer.DeleteOrderCouponMapping(SetOrderInfo.OrderId);
                }
            }
            else
            {
                sql = " update t_inout set pay_id = '" + SetOrderInfo.PaymentTypeId + "' "
                               + ", total_retail = total_amount,Field15 = '" + SetOrderInfo.OutTradeNo + "' "
                               + ", Field7 = '2',Field10 = '" + SetOrderInfo.StatusDesc + "' "
                               + ", modify_time = '" + SetOrderInfo.PaymentTime + "' "
                               + " where order_no = '" + SetOrderInfo.OrderCode + "'; ";
            }
            this.SQLHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取电商的订单
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public DataSet GetOrderOnline(string OrderId)
        {
            string sql = " select a.* ,(select x.actual_amount From T_Inout x where x.order_id = a.OrderId) ActualAmount From vw_online_order a  where a.OrderId = '" + OrderId + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet GetOrderOpenId(string order_code)
        {
            string sql = " select (SELECT WeiXinUserId FROM dbo.Vip x WHERE x.VIPID = a.vip_no ) openId,total_amount amount,a.order_id From t_inout a where a.order_No = '" + order_code + "' ";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region 修改订单配送状态
        /// <summary>
        /// 根据订单ID修改订单配送状态
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="status">状态</param>
        /// <param name="statusDesc">状态描述：1未付款/2待处理/3已发货/0已取消</param>
        /// <param name="tableNo">桌号</param>
        public void UpdateOrderDeliveryStatus(string orderId, string status, string statusDesc, string send_time, SqlTransaction tran, string tableNo)
        {
            string sql = " UPDATE dbo.T_Inout SET status='" + status + "', status_desc='" + statusDesc + "',Field7 = '" + status + "', Field10 = '" + statusDesc + "',modify_time= '" + base.GetCurrentDateTime() + "',modify_user_id = '" + this.loggingSessionInfo.UserID.ToString() + "' ";
            if (send_time != null && !send_time.Equals(""))
            {
                sql += ",send_time = '" + send_time + "'";
                sql += ",Field9 = '" + send_time + "'";
            }
            if (tableNo != null && !tableNo.Equals(""))
            {
                sql += ",Field20 = '" + tableNo + "'";
            }
            sql += " WHERE order_id = '" + orderId + "' ; ";

            if (status.Equals("0"))
            {
                TOrderCouponMappingDAO mappingServer = new TOrderCouponMappingDAO(this.CurrentUserInfo);
                mappingServer.DeleteOrderCouponMapping(orderId);
            }
            int result;
            if (tran == null)
                result = this.SQLHelper.ExecuteNonQuery(sql);
            else
                result = this.SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql);
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { Message = "更新订单状态SQL[影响行数=" + result.ToString() + "]：" + sql });
        }
        #endregion

        #region 修改订单配送
        /// <summary>
        /// 根据订单ID修改订单配送
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="carrier_id">配送商</param>
        /// <param name="Field2">配送单号</param>
        public void UpdateOrderDelivery(string orderId, string carrier_id, string Field2)
        {
            string sql = " UPDATE dbo.T_Inout SET carrier_id = '" + carrier_id + "', Field2 = '" + Field2 + "',modify_time= '" + base.GetCurrentDateTime() + "',modify_user_id = '" + this.loggingSessionInfo.UserID.ToString() + "' ";
            sql += " WHERE order_id = '" + orderId + "'";

            this.SQLHelper.ExecuteNonQuery(sql);
        }
        #endregion

        #region 餐饮 20131010
        #region 获取桌号信息
        /// <summary>
        /// 获取桌号信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public DataSet GetTableNumberList(string customerId, string unitId)
        {
            string sql = "select * From vwTableNumber where 1=1 order by create_time desc ";
            DataSet ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
        #endregion

        #region Update
        public bool Update(InoutInfo inoutInfo)
        {
            string sql = "update t_inout set order_id = '" + inoutInfo.order_id + "'  ";
            #region
            if (inoutInfo != null && inoutInfo.order_no != null)
            {
                sql += " ,order_no = '" + inoutInfo.order_no + "' ";
            }
            if (inoutInfo != null && inoutInfo.order_type_id != null)
            {
                sql += " ,order_type_id = '" + inoutInfo.order_type_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.order_reason_id != null)
            {
                sql += " ,order_reason_id = '" + inoutInfo.order_reason_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.red_flag != null)
            {
                sql += " ,red_flag = '" + inoutInfo.red_flag + "' ";
            }
            if (inoutInfo != null && inoutInfo.ref_order_id != null)
            {
                sql += " ,ref_order_id = '" + inoutInfo.ref_order_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.ref_order_no != null)
            {
                sql += " ,ref_order_no = '" + inoutInfo.ref_order_no + "' ";
            }
            if (inoutInfo != null && inoutInfo.warehouse_id != null)
            {
                sql += " ,warehouse_id = '" + inoutInfo.warehouse_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.order_date != null)
            {
                sql += " ,order_date = '" + inoutInfo.order_date + "' ";
            }
            if (inoutInfo != null && inoutInfo.request_date != null)
            {
                sql += " ,request_date = '" + inoutInfo.request_date + "' ";
            }
            if (inoutInfo != null && inoutInfo.complete_date != null)
            {
                sql += " ,complete_date = '" + inoutInfo.complete_date + "' ";
            }
            if (inoutInfo != null && inoutInfo.create_unit_id != null)
            {
                sql += " ,create_unit_id = '" + inoutInfo.create_unit_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.unit_id != null)
            {
                sql += " ,unit_id = '" + inoutInfo.unit_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.related_unit_id != null)
            {
                sql += " ,related_unit_id = '" + inoutInfo.related_unit_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.related_unit_code != null)
            {
                sql += " ,related_unit_code = '" + inoutInfo.related_unit_code + "' ";
            }
            if (inoutInfo != null && inoutInfo.pos_id != null)
            {
                sql += " ,pos_id = '" + inoutInfo.pos_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.shift_id != null)
            {
                sql += " ,shift_id = '" + inoutInfo.shift_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.sales_user != null)
            {
                sql += " ,sales_user = '" + inoutInfo.sales_user + "' ";
            }
            if (inoutInfo != null && inoutInfo.total_amount != null && inoutInfo.total_amount > 0)
            {
                sql += " ,total_amount = '" + inoutInfo.total_amount + "' ";
            }
            if (inoutInfo != null && inoutInfo.discount_rate != null && inoutInfo.discount_rate > 0)
            {
                sql += " ,discount_rate = '" + inoutInfo.discount_rate + "' ";
            }
            if (inoutInfo != null && inoutInfo.actual_amount != null && inoutInfo.actual_amount > 0)
            {
                sql += " ,actual_amount = '" + inoutInfo.actual_amount + "' ";
            }
            if (inoutInfo != null && inoutInfo.receive_points != null && inoutInfo.receive_points > 0)
            {
                sql += " ,receive_points = '" + inoutInfo.receive_points + "' ";
            }
            if (inoutInfo != null && inoutInfo.pay_points != null && inoutInfo.pay_points > 0)
            {
                sql += " ,pay_points = '" + inoutInfo.pay_points + "' ";
            }
            if (inoutInfo != null && inoutInfo.pay_id != null)
            {
                sql += " ,pay_id = '" + inoutInfo.pay_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.print_times != null && inoutInfo.print_times > 0)
            {
                sql += " ,print_times = '" + inoutInfo.print_times + "' ";
            }
            if (inoutInfo != null && inoutInfo.carrier_id != null)
            {
                sql += " ,carrier_id = '" + inoutInfo.carrier_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.remark != null)
            {
                sql += " ,remark = '" + inoutInfo.remark + "' ";
            }
            if (inoutInfo != null && inoutInfo.status != null)
            {
                sql += " ,status = '" + inoutInfo.status + "' ";
            }
            if (inoutInfo != null && inoutInfo.status_desc != null)
            {
                sql += " ,status_desc = '" + inoutInfo.status_desc + "' ";
            }
            if (inoutInfo != null && inoutInfo.total_qty != null && inoutInfo.total_qty > 0)
            {
                sql += " ,total_qty = '" + inoutInfo.total_qty + "' ";
            }
            if (inoutInfo != null && inoutInfo.total_retail != null && inoutInfo.total_retail > 0)
            {
                sql += " ,total_retail = '" + inoutInfo.total_retail + "' ";
            }
            if (inoutInfo != null && inoutInfo.keep_the_change != null && inoutInfo.keep_the_change > 0)
            {
                sql += " ,keep_the_change = '" + inoutInfo.keep_the_change + "' ";
            }
            if (inoutInfo != null && inoutInfo.wiping_zero != null && inoutInfo.wiping_zero > 0)
            {
                sql += " ,wiping_zero = '" + inoutInfo.wiping_zero + "' ";
            }
            if (inoutInfo != null && inoutInfo.vip_no != null)
            {
                sql += " ,vip_no = '" + inoutInfo.vip_no + "' ";
            }
            if (inoutInfo != null && inoutInfo.create_time != null)
            {
                sql += " ,create_time = '" + inoutInfo.create_time + "' ";
            }
            if (inoutInfo != null && inoutInfo.create_user_id != null)
            {
                sql += " ,create_user_id = '" + inoutInfo.create_user_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.approve_time != null)
            {
                sql += " ,approve_time = '" + inoutInfo.approve_time + "' ";
            }
            if (inoutInfo != null && inoutInfo.approve_user_id != null)
            {
                sql += " ,approve_user_id = '" + inoutInfo.approve_user_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.send_time != null)
            {
                sql += " ,send_time = '" + inoutInfo.send_time + "' ";
            }
            if (inoutInfo != null && inoutInfo.send_user_id != null)
            {
                sql += " ,send_user_id = '" + inoutInfo.send_user_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.accpect_time != null)
            {
                sql += " ,accpect_time = '" + inoutInfo.accpect_time + "' ";
            }
            if (inoutInfo != null && inoutInfo.accpect_user_id != null)
            {
                sql += " ,accpect_user_id = '" + inoutInfo.accpect_user_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.modify_time != null)
            {
                sql += " ,modify_time = '" + inoutInfo.modify_time.ToString() + "' ";
            }
            if (inoutInfo != null && inoutInfo.modify_user_id != null)
            {
                sql += " ,modify_user_id = '" + inoutInfo.modify_user_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.data_from_id != null)
            {
                sql += " ,data_from_id = '" + inoutInfo.data_from_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.sales_unit_id != null)
            {
                sql += " ,sales_unit_id = '" + inoutInfo.sales_unit_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.purchase_unit_id != null)
            {
                sql += " ,purchase_unit_id = '" + inoutInfo.purchase_unit_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.if_flag != null)
            {
                sql += " ,if_flag = '" + inoutInfo.if_flag + "' ";
            }
            if (inoutInfo != null && inoutInfo.customer_id != null)
            {
                sql += " ,customer_id = '" + inoutInfo.customer_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.bat_id != null)
            {
                sql += " ,bat_id = '" + inoutInfo.bat_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.sales_warehouse_id != null)
            {
                sql += " ,sales_warehouse_id = '" + inoutInfo.sales_warehouse_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.purchase_warehouse_id != null)
            {
                sql += " ,purchase_warehouse_id = '" + inoutInfo.purchase_warehouse_id + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field1 != null)
            {
                sql += " ,Field1 = '" + inoutInfo.Field1 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field2 != null)
            {
                sql += " ,Field2 = '" + inoutInfo.Field2 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field3 != null)
            {
                sql += " ,Field3 = '" + inoutInfo.Field3 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field4 != null)
            {
                sql += " ,Field4 = '" + inoutInfo.Field4 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field5 != null)
            {
                sql += " ,Field5 = '" + inoutInfo.Field5 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field6 != null)
            {
                sql += " ,Field6 = '" + inoutInfo.Field6 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field7 != null)
            {
                sql += " ,Field7 = '" + inoutInfo.Field7 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field8 != null)
            {
                sql += " ,Field8 = '" + inoutInfo.Field8 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field9 != null)
            {
                sql += " ,Field9 = '" + inoutInfo.Field9 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field10 != null)
            {
                sql += " ,Field10 = '" + inoutInfo.Field10 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field11 != null)
            {
                sql += " ,Field11 = '" + inoutInfo.Field11 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field12 != null)
            {
                sql += " ,Field12 = '" + inoutInfo.Field12 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field13 != null)
            {
                sql += " ,Field13 = '" + inoutInfo.Field13 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field14 != null)
            {
                sql += " ,Field14 = '" + inoutInfo.Field14 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field15 != null)
            {
                sql += " ,Field15 = '" + inoutInfo.Field15 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field16 != null)
            {
                sql += " ,Field16 = '" + inoutInfo.Field16 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field17 != null)
            {
                sql += " ,Field17 = '" + inoutInfo.Field17 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field18 != null)
            {
                sql += " ,Field18 = '" + inoutInfo.Field18 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field19 != null)
            {
                sql += " ,Field19 = '" + inoutInfo.Field19 + "' ";
            }
            if (inoutInfo != null && inoutInfo.Field20 != null)
            {
                sql += " ,Field20 = '" + inoutInfo.Field20 + "' ";
            }
            #endregion
            sql += " where order_id = '" + inoutInfo.order_id + "' ; ";
            //JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo()
            //    {
            //        Message = string.Format("更新: {0}", sql)
            //    });
            this.SQLHelper.ExecuteNonQuery(sql);
            return true;
        }
        #endregion


        #region GetUnitIdByVipId
        public string GetUnitIdByVipId(string vipId)
        {
            string sql = "select top 1 a.UnitId  ";
            sql += " from VipUnitMapping a";
            sql += " inner join t_unit b on a.UnitId=b.unit_id";
            sql += " inner join T_Type c on b.[type_id]=c.[type_id]";
            sql += " where 1=1 and a.IsDelete='0'";
            sql += " and c.[type_id]='EB58F1B053694283B2B7610C9AAD2742'";
            sql += " and a.VIPID='" + vipId + "'";
            var unitId = this.SQLHelper.ExecuteScalar(sql);
            return unitId != DBNull.Value ? string.Empty : unitId.ToString();
        }
        #endregion

        #region Jermyn20140307 生成虚拟订单
        /// <summary>
        /// 生成虚拟订单
        /// </summary>
        /// <param name="orderId">订单标识</param>
        /// <param name="customerId">客户标识</param>
        /// <param name="unitId">门店标识</param>
        /// <param name="vipId">用户标识</param>
        /// <param name="dataFromId">来源</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        public string SetVirtualOrderInfo(string orderId
                                         , string customerId
                                         , string unitId
                                         , string vipId
                                         , string dataFromId
                                         , string amount, string OffOrderNo,string remark,string UserID)
        {
            DataSet ds = new DataSet();

            SqlParameter[] Parm = new SqlParameter[9];
            Parm[0] = new SqlParameter("@OrderID ", System.Data.SqlDbType.NVarChar, 100);
            Parm[0].Value = orderId;
            Parm[1] = new SqlParameter("@OrderAmt", System.Data.SqlDbType.Decimal, 10);
            Parm[1].Value = Convert.ToDecimal(amount);
            Parm[2] = new SqlParameter("@StoreID", System.Data.SqlDbType.NVarChar, 100);
            Parm[2].Value = unitId;
            Parm[3] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar, 100);
            Parm[3].Value = vipId;
            Parm[4] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar, 100);
            Parm[4].Value = customerId;
            Parm[5] = new SqlParameter("@DataFrom", System.Data.SqlDbType.NVarChar, 100);
            Parm[5].Value = dataFromId;

            Parm[6] = new SqlParameter("@OffOrderNo", System.Data.SqlDbType.NVarChar, 100);
            Parm[6].Value = OffOrderNo;
            Parm[7] = new SqlParameter("@remark", System.Data.SqlDbType.NVarChar, 500);
            Parm[7].Value = remark;
            Parm[8] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar, 500);
            Parm[8].Value = UserID;

            ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "GenerateVirtualOrder", Parm);
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return "0";
            }
            else
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
        }
        #endregion



        public DataSet GetItemNameByOrderId(string orderId)
        {
            var sql = new StringBuilder();
            sql.Append(" select d.item_name from T_Inout a ");
            sql.Append(" inner join T_Inout_Detail b on a.order_id = b.order_id ");
            sql.Append(" inner join T_Sku c on b.sku_id = c.sku_id");
            sql.Append(" inner join T_Item d on c.item_id = d.item_id ");
            sql.AppendFormat(" where a.order_id = '{0}'", orderId);
            sql.Append(" and c.status = 1 ");
            sql.Append(" and d.status = 1");

            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }




    }
}
