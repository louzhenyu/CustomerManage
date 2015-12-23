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
    public class Inout3Service : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public Inout3Service(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

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


        /// <summary>
        /// 查询各个状态的数量 Jermyn20130906
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchStatusTypeCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo, 0);
            sql += @"SELECT x.StatusType,ISNULL(y.StatusCount,0) StatusCount FROM ( 
                  select OptionValue as StatusType,OptionText From Options where OptionName ='OrdersStatus' and isdelete=0               
                  ) x LEFT JOIN (
                  SELECT isnull(a.Field7,2) StatusType,COUNT(*) StatusCount FROM dbo.T_Inout a 
                  INNER JOIN @TmpTable b on(a.order_id = b.order_id) 
                  WHERE a.Field7 IS NOT NULL AND a.Field7 <> '' 
                  GROUP BY a.Field7 ) y ON(x.StatusType = y.StatusType) ";
            // this.CurrentUserInfo.CurrentLoggingManager.Customer_Id
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 查询各个状态的数量 jifeng.cao 20140319
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchStatusTypeCount_lj(OrderSearchInfo orderSearchInfo)
        {
            string sql = GetSearchPublicSql(orderSearchInfo, 0);
            sql += string.Format(@"SELECT x.StatusType,x.OptionText StatusTypeName,ISNULL(y.StatusCount,0) StatusCount FROM ( 
                  select OptionValue as StatusType,OptionText From Options where OptionName ='TInOutStatus' and CustomerID='{0}' and isdelete=0               
                  ) x LEFT JOIN (
                  SELECT isnull(a.Field7,-99) StatusType,COUNT(*) StatusCount FROM dbo.T_Inout a 
                  INNER JOIN @TmpTable b on(a.order_id = b.order_id) 
                  WHERE a.Field7 IS NOT NULL AND a.Field7 <> '' 
                  GROUP BY a.Field7 ) y ON(x.StatusType = y.StatusType) ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);


            //未配置发货门店

            sql += string.Format(@"
                UNION                  
                  SELECT    1234567890 ,
                            '未选择门店' ,
                            COUNT(*)
                  FROM      @TmpTable aa
                            INNER JOIN T_Inout bb ON aa.order_id = bb.order_id
                  WHERE     bb.Field7 NOT IN ( '600', '700', '800', '900' )
                            AND (bb.sales_unit_id = '' or bb.sales_unit_id is null)
            ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            // this.CurrentUserInfo.CurrentLoggingManager.Customer_Id
            return this.SQLHelper.ExecuteDataset(sql);
        }


        /// <summary>
        /// 根据查询条件获取订单数量优化后的代码
        /// </summary>
        /// <param name="orderSearchInfo">查询条件对象</param>
        /// <returns></returns>
        public int SearchInoutCount2(OrderSearchInfo orderSearchInfo)
        {
            string sqlTemp = GetSearchPublicSql2(orderSearchInfo, 1);//利用优化后的sql

            string sql = "";
            //先把需要全表扫描的数据查出来，后面就不需要查了
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " select * into #unitTemp from vw_unit_level where path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' and customer_id='"
            //        + orderSearchInfo.customer_id + "' ";
            //}

            sql += string.Format(@"DECLARE @AllUnit NVARCHAR(200)

                CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
                 INSERT #UnitSET (UnitID)                  
                   SELECT DISTINCT R.UnitID                   
                   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  ('{0}',UR.unit_id,205)  R                  
                   WHERE user_id='{1}'       ---根据账户的角色去查角色对应的  所有门店unit_id
   ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, CurrentUserInfo.UserID);

            sql += " select count(1) from ( " + sqlTemp + " ) y";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 查询各个状态的数量 jifeng.cao 20140319
        /// </summary>
        /// <param name="orderSearchInfo"></param>
        /// <returns></returns>
        public DataSet SearchStatusTypeCount_lj2(OrderSearchInfo orderSearchInfo)
        {
            string sqlTemp = GetSearchPublicSql2(orderSearchInfo, 0);
            string sql = "";
            //先把需要全表扫描的数据查出来，后面就不需要查了
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " select * into #unitTemp from vw_unit_level where path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' and customer_id='"
            //        + orderSearchInfo.customer_id + "' ";
            //}

            sql +=string.Format( @"DECLARE @AllUnit NVARCHAR(200)

                CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
                 INSERT #UnitSET (UnitID)                  
                   SELECT DISTINCT R.UnitID                   
                   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  ('{0}',UR.unit_id,205)  R                  
                   WHERE user_id='{1}'          ---根据账户的角色去查角色对应的  所有门店unit_id
   ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, CurrentUserInfo.UserID);

            sql += string.Format(@"SELECT x.StatusType,x.OptionText StatusTypeName,ISNULL(y.StatusCount,0) StatusCount FROM ( 
                  select OptionValue as StatusType,OptionText From Options where OptionName ='TInOutStatus' and CustomerID='{0}' and isdelete=0               
                  ) x LEFT JOIN (
                  SELECT isnull(a.Field7,-99) StatusType,COUNT(*) StatusCount FROM dbo.T_Inout a 
                  INNER JOIN ( " + sqlTemp + @" ) b on(a.order_id = b.order_id) 
                  WHERE a.Field7 IS NOT NULL AND a.Field7 <> '' 
                  GROUP BY a.Field7 ) y ON(x.StatusType = y.StatusType) ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);


            //未配置发货门店

            sql += string.Format(@"
                UNION                  
                  SELECT    1234567890 ,
                            '未选择门店' ,
                            COUNT(*)
                  FROM     ( " + sqlTemp + @" ) aa
                            INNER JOIN T_Inout bb ON aa.order_id = bb.order_id
                  WHERE     bb.Field7 NOT IN ( '600', '700', '800', '900' )
                            AND (bb.sales_unit_id = '' or bb.sales_unit_id is null)
            ", CurrentUserInfo.CurrentLoggingManager.Customer_Id);

            // this.CurrentUserInfo.CurrentLoggingManager.Customer_Id
            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 修改订单门店
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public int SetOrderUnit(string orderList, string unitID)
        {
            string sql = string.Format("UPDATE T_Inout SET sales_unit_id='{0}' WHERE order_no IN ({1})", unitID, orderList);
            return SQLHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询未审核订单数 
        /// </summary> 
        /// <returns></returns>
        /// add by donal 2014-10-10 17:19:37
        public int SearchUnAuditTypeCount(OrderSearchInfo orderSearchInfo)
        {
            string sql = string.Format(@"select COUNT(*) from
                (
                select distinct  order_date,create_time,order_no,order_id ,modify_time   
                from t_inout a  inner join vw_unit_level b 
                on ((a.purchase_unit_id=b.unit_id or a.sales_unit_id=b.unit_id) and b.customer_id='{0}')   
                where 
                a.customer_id like  '%{0}%'
                and a.order_type_id like  '%{1}%' 
                and a.red_flag = '1' and b.path_unit_id like '%{2}%'
                and a.Field7='100' and a.status<>'-1'  
                ) x", orderSearchInfo.customer_id, orderSearchInfo.order_type_id, orderSearchInfo.path_unit_id);

            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }


        /// <summary>
        /// 根据查询条件获取出入库主信息
        /// </summary>
        /// <param name="orderSearchInfo">查询条件对象</param>
        /// <returns></returns>
        public DataSet SearchInoutInfo(OrderSearchInfo orderSearchInfo)
        {
            string orderby = " order by a.order_date desc,a.modify_time desc,a.order_no desc";
            if (!string.IsNullOrEmpty(orderSearchInfo.InoutSort))
            {
                if (orderSearchInfo.InoutSort == "1")
                {
                    orderby = " order by a.order_date desc,a.modify_time desc,a.order_no desc";
                }
                else if (orderSearchInfo.InoutSort == "2")
                {
                    orderby = " order by a.modify_time desc,a.order_date DESC,a.order_no desc";
                }
            }
            string sql = GetSearchPublicSql(orderSearchInfo, 1);
            #region
            sql = sql + "select distinct a.customer_id,a.order_id "
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
                      + " ,a.print_times "
                      + " ,a.carrier_id "
                      + " ,(select top 1 unit_name from t_unit where unit_id=a.carrier_id) carrier_name "
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
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.Field11) payment_name "
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
                        + " ,a.Field11 "
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
                        + " ,(SELECT dbo.DateToTimestamp(a.modify_time)) timestamp "
                      + " From T_Inout a "
                      + " inner join @TmpTable b "
                      + " on(a.order_id = b.order_id) "
                      + " where 1=1 "
                      + " and b.row_no > '" + orderSearchInfo.StartRow + "' and b.row_no <= '" + orderSearchInfo.EndRow + "' " + orderby + ";";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 根据查询条件获取出入库主信息 jifeng.cao 20140319
        /// </summary>
        /// <param name="orderSearchInfo">查询条件对象</param>
        /// <returns></returns>
        public DataSet SearchInoutInfo_lj(OrderSearchInfo orderSearchInfo)
        {
            string orderby = " order by a.order_date desc,a.modify_time desc,a.order_no desc";
            if (!string.IsNullOrEmpty(orderSearchInfo.InoutSort))
            {
                if (orderSearchInfo.InoutSort == "1")
                {
                    orderby = " order by a.order_date desc,a.modify_time desc,a.order_no desc";
                }
                else if (orderSearchInfo.InoutSort == "2")
                {
                    orderby = " order by a.modify_time desc,a.order_date DESC,a.order_no desc";
                }
            }
            string sql = GetSearchPublicSql(orderSearchInfo, 1);
            #region
            sql = sql + "select distinct a.customer_id,a.order_id "
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
                      + " ,a.print_times "
                      + " ,a.carrier_id "
                      + " ,(select top 1 unit_name from t_unit where unit_id=a.carrier_id) carrier_name "
                      + " ,a.remark "
                      + " ,a.status "
                      + " ,(select top 1 OptionText from Options where OptionValue=a.Field7 and OptionName='TInOutStatus' and IsDelete=0 and CustomerID=a.customer_id) status_desc "
                      + " ,convert(decimal(18,4),a.total_qty) total_qty " //*red_flag
                      + " ,convert(decimal(18,4),a.total_retail) total_retail " //*red_flag
                      + " ,a.keep_the_change "
                      + " ,a.wiping_zero "
                      + " ,a.vip_no "
                      + " ,(select top 1 vipName from vip where vipId=a.vip_no) vip_name "
                      + " ,(select top 1 vipLevel from vip where vipId=a.vip_no) vipLevel "
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
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.Field11) payment_name "
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
                        + " ,a.Field11 "
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
                //有一个客户，时间里含有.却没有带毫秒
                        + @" ,dbo.Datetotimestamp(replace(case when a.modify_time like '%.' then REPLACE(a.modify_time,'.','') 
                                        when a.modify_time IS null then '1975/01/01' else a.modify_time end ,'.000','')) timestamp "
                //返的积分，是21
                        + @",isnull((SELECT SUM(ABS(Integral)) FROM dbo.VipIntegralDetail	 WHERE IntegralSourceID=21 AND ObjectId=a.order_id ),0) as IntegralBack"
                //返回的现金，是用的2
                        + @",isnull((SELECT  ISNULL(SUM(Amount),0) FROM dbo.VipAmountDetail	WHERE AmountSourceId=2  AND ObjectId=a.order_id),0) as AmountBack"
                      + " From T_Inout a "
                      + " inner join @TmpTable b "
                      + " on(a.order_id = b.order_id) "
                      + " where 1=1 "
                      + " and b.row_no > '" + orderSearchInfo.StartRow + "' and b.row_no <= '" + orderSearchInfo.EndRow + "' " + orderby + ";";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        public DataSet SearchInoutInfo_lj2(OrderSearchInfo orderSearchInfo)
        {
            string orderby = " order by a.order_date desc,a.create_time desc";
            if (!string.IsNullOrEmpty(orderSearchInfo.InoutSort))
            {
                if (orderSearchInfo.InoutSort == "1")
                {
                    orderby = " order by a.order_date desc,a.create_time desc";
                }
                else if (orderSearchInfo.InoutSort == "2")
                {
                    orderby = " order by a.order_date desc,a.modify_time desc";
                }
            }
            string sqlTemp = GetSearchPublicSql2(orderSearchInfo, 1);
            #region
            string sql = "";
            //先把需要全表扫描的数据查出来，后面就不需要查了
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " select * into #unitTemp from vw_unit_level where path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' and customer_id='"
            //        + orderSearchInfo.customer_id + "' ";
            //}

            sql += string.Format(@"DECLARE @AllUnit NVARCHAR(200)

                CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
                 INSERT #UnitSET (UnitID)                  
                   SELECT DISTINCT R.UnitID                   
                   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  ('{0}',UR.unit_id,205)  R                  
                   WHERE user_id='{1}'       ---根据账户的角色去查角色对应的  所有门店unit_id
   ", CurrentUserInfo.CurrentLoggingManager.Customer_Id, CurrentUserInfo.UserID);

            sql += "select distinct a.customer_id,a.order_id "
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
                      + " ,a.print_times "
                      + " ,a.carrier_id "
                      + " ,(select top 1 unit_name from t_unit where unit_id=a.carrier_id) carrier_name "
                      + " ,a.remark "
                      + " ,a.status "
                      + " ,(select top 1 OptionText from Options where OptionValue=a.Field7 and OptionName='TInOutStatus' and IsDelete=0 and CustomerID=a.customer_id) status_desc "
                      + " ,convert(decimal(18,4),a.total_qty) total_qty " //*red_flag
                      + " ,convert(decimal(18,4),a.total_retail) total_retail " //*red_flag
                      + " ,a.keep_the_change "
                      + " ,a.wiping_zero "
                      + " ,a.vip_no "
                      + " ,(select top 1 vipName from vip where vipId=a.vip_no) vip_name "
                      + " ,(select top 1 vipLevel from vip where vipId=a.vip_no) vipLevel "
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
                      + " ,a.data_from_id "
                      + " ,a.sales_warehouse_id "
                      + " ,a.purchase_warehouse_id "
                      + " ,(select vipsourceName From SysVipSource where vipsourceId = a.data_from_id) data_from_name "
                      + " ,(select order_type_code From T_Order_Type where order_type_id = a.order_type_id) order_type_code "
                      + " ,(select reason_type_code From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_code "
                      + " ,(select order_type_name From T_Order_Type where order_type_id = a.order_type_id) order_type_name "
                      + " ,(select reason_type_name From T_Order_Reason_Type where reason_type_id = a.order_reason_id) order_reason_name "
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.Field11) payment_name "
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
                //     + " ,@iCount icount "
                    + " ,0 icount "
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
                        + " ,a.Field11 "
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
                //有一个客户，时间里含有.却没有带毫秒
                        + @" ,dbo.Datetotimestamp(replace(case when a.modify_time like '%.' then REPLACE(a.modify_time,'.','') 
                                        when a.modify_time IS null then '1975/01/01' else a.modify_time end ,'.000','')) timestamp "
                //返的积分，是21
                        + @",isnull((SELECT SUM(ABS(Integral)) FROM dbo.VipIntegralDetail	 WHERE IntegralSourceID=21 AND ObjectId=a.order_id ),0) as IntegralBack"
                //返回的现金，是用的2
                        + @",isnull((SELECT  ISNULL(SUM(Amount),0) FROM dbo.VipAmountDetail	WHERE AmountSourceId=2  AND ObjectId=a.order_id),0) as AmountBack"
                      + " From T_Inout a "
                      + " inner join ( " + sqlTemp + " ) b "
                      + " on(a.order_id = b.order_id) "
                      + " where 1=1 "
                      + " and b.row_no > '" + orderSearchInfo.StartRow + "' and b.row_no <= '" + orderSearchInfo.EndRow + "' " + orderby + " drop table #UnitSET;";
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
            string orderby = "order by x.order_date desc,x.create_time DESC,x.order_no desc";
            if (!string.IsNullOrEmpty(orderSearchInfo.InoutSort))
            {
                if (orderSearchInfo.InoutSort == "1")
                {
                    orderby = "order by x.order_date desc,x.create_time DESC,x.order_no desc";
                }
                else if (orderSearchInfo.InoutSort == "2")
                {
                    orderby = "order by x.modify_time desc,x.create_time DESC,x.order_no desc";
                }
            }

            PublicService pService = new PublicService();
            #region
            string sql = "Declare @TmpTable Table "
                      + " (order_id nvarchar(100) "
                      + " ,row_no int "
                      + " ); "

                      + " insert into @TmpTable (order_id,row_no) "
                      + " select distinct x.order_id,row_no=row_number() over(" + orderby + ") From ( "
                      + " select distinct "
                      + " order_date  "
                      + " ,create_time  "
                      + " ,order_no  "
                      + " ,order_id ,modify_time "
                      + @" from   (select order_date,create_time,order_no,order_id ,modify_time ,purchase_unit_id
                            ,sales_unit_id  from  t_inout a where 1=1 and a.status != '-1' and a.Field7 != '-99' ";
            //判断是否有付款状态条件(jifeng.cao 20140320)
            if (!string.IsNullOrEmpty(orderSearchInfo.PayStatus))
            {
                if (orderSearchInfo.PayStatus == "1")
                {
                    sql += " and a.Field1 = '1' ";
                }
                else
                {
                    sql += " and a.Field1 != '1' ";
                }
            }

            sql = pService.GetLinkSql(sql, "a.order_id", orderSearchInfo.order_id, "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", orderSearchInfo.customer_id, "=");//原来是%
            sql = pService.GetLinkSql(sql, "a.order_no", orderSearchInfo.order_no, "%");
            sql = pService.GetLinkSql(sql, "a.order_type_id", orderSearchInfo.order_type_id, "=");//原来是%
            //sql = pService.GetLinkSql(sql, "a.order_reason_id", orderSearchInfo.order_reason_id, "%");
            sql = pService.GetLinkSql(sql, "a.unit_id", orderSearchInfo.unit_id, "=");
            if (!string.IsNullOrEmpty(orderSearchInfo.order_date_begin))//判断是否为空
            {
                sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_begin, ">=");  //订单开始日期
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.order_date_end))
            {
                sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_end, "<=");  //订单开始日期
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.complete_date_begin))
            {
                sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_begin, ">=");
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.complete_date_end))
            {
                sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_end, "<=");
            }
            sql = pService.GetLinkSql(sql, "a.status", orderSearchInfo.status, "=");
            sql = pService.GetLinkSql(sql, "a.warehouse_id", orderSearchInfo.warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.ref_order_no", orderSearchInfo.ref_order_no, "%");
            sql = pService.GetLinkSql(sql, "a.data_from_id", orderSearchInfo.data_from_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_unit_id", orderSearchInfo.sales_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_unit_id", orderSearchInfo.purchase_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.red_flag", orderSearchInfo.red_flag, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_warehouse_id", orderSearchInfo.purchase_warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_warehouse_id", orderSearchInfo.sales_warehouse_id, "=");
            // sql = pService.GetLinkSql(sql, "a.vip_no", orderSearchInfo.vip_no, "=");
            if (string.IsNullOrEmpty(orderSearchInfo.data_from_id))
            {
                sql = pService.GetLinkSql(sql, "a.data_from_name", orderSearchInfo.data_from_id, "=");
            }

            //发货时间
            if (!string.IsNullOrEmpty(orderSearchInfo.DeliveryDateBegin))
            {
                sql = pService.GetLinkSql(sql, "a.send_time", orderSearchInfo.DeliveryDateBegin, ">=");
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.DeliveryDateEnd))
            {
                sql = pService.GetLinkSql(sql, "a.send_time", orderSearchInfo.DeliveryDateEnd, "<=");
            }
            #region 取消时间
            if (orderSearchInfo.DeliveryStatus != null && orderSearchInfo.DeliveryStatus.Equals("0"))
            {
                if (!string.IsNullOrEmpty(orderSearchInfo.CancelDateBegin))   //时间
                {
                    sql = pService.GetLinkSql(sql, "a.modify_time", orderSearchInfo.CancelDateBegin, ">=");
                }
                if (!string.IsNullOrEmpty(orderSearchInfo.CancelDateEnd))
                {
                    sql = pService.GetLinkSql(sql, "a.modify_time", orderSearchInfo.CancelDateEnd, "<=");
                }
            }
            #endregion
            //改变这句话****
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " and b.path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' ";
            //}

            //Jermyn20130905 订单配送状态
            if (IsStatus == 1)
            {
                if ((!string.IsNullOrEmpty(orderSearchInfo.DeliveryStatus)) && orderSearchInfo.DeliveryStatus != "0")
                {
                    if (orderSearchInfo.DeliveryStatus == "1234567890")
                    //未分配门店
                    {
                        sql += " AND( a.sales_unit_id = '' OR  a.sales_unit_id is null) AND a.Field7 NOT IN ('600','700','800','900') ";
                    }
                    else
                    {
                        sql = pService.GetLinkSql(sql, "a.Field7", orderSearchInfo.DeliveryStatus, "=");
                    }
                }
                else
                {
                    sql += " and isnull(a.Field7,'')!='' and a.Field7!='0' ";
                }
            }
            sql = pService.GetLinkSql(sql, "a.Field8", orderSearchInfo.DeliveryId, "=");
            sql = pService.GetLinkSql(sql, "a.Field11", orderSearchInfo.DefrayTypeId, "=");

            if (orderSearchInfo.timestamp != null && orderSearchInfo.timestamp.Length > 0)
            {
                sql += string.Format(" and (a.modify_time < (SELECT dbo.TimestampToDate('{0}')) )", orderSearchInfo.timestamp);
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.vip_no))
            {
                sql += string.Format(@" 
            and  a.vip_no in(select VIPID from VIP where (VipName like'%" + orderSearchInfo.vip_no + "%' or WeiXin like '%" + orderSearchInfo.vip_no + "%' or Phone like '%" + orderSearchInfo.vip_no + "%') and IsDelete=0)");
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
            //上面先把内部条件放在里面单独的表里进行查询，然后再去连接查询

            sql += " ) c";
            if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            {
                sql += " inner join (select * from vw_unit_level where path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' and customer_id='" + orderSearchInfo.customer_id + "' ) b on   (c.purchase_unit_id=b.unit_id or c.sales_unit_id=b.unit_id) ";
            }
            //改变这句话****  ,把模糊查询的放在了上面，在表连接之前先进行查询
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " and b.path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' ";
            //}

            sql += "  where 1=1 "; // ----and a.status != '-1' and a.Field7 != '-99'



            sql = sql + " ) x ; ";

            sql = sql + " Declare @iCount int;";

            sql = sql + " select @iCount = COUNT(1) From @TmpTable;";
            #endregion
            return sql;
        }
        //优化方法
        //另外创建一个方法，不要再插入临时表了，先创建基础的sql语句（带order by的，带rownumber的，然后组成基础语句）
        //取数量的和取各状态订单的数量，和订单列表都从这里取****
        //并且PosOrder_lj只取总订单数量和订单列表，GetPosOrderTotalCount_lj只取各状态的数量********

        private string GetSearchPublicSql2(OrderSearchInfo orderSearchInfo, int IsStatus)
        {
            string orderby = "order by x.order_date desc,x.create_time DESC,x.order_no desc";
            if (!string.IsNullOrEmpty(orderSearchInfo.InoutSort))
            {
                if (orderSearchInfo.InoutSort == "1")
                {
                    orderby = "order by c.order_date desc,c.create_time DESC,c.order_no desc";
                }
                else if (orderSearchInfo.InoutSort == "2")
                {
                    orderby = "order by c.modify_time desc,c.create_time DESC,c.order_no desc";
                }
            }

            PublicService pService = new PublicService();
            #region
            string sql = "";

            //先把需要全表扫描的数据查出来，后面就不需要查了
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " select * into #unitTemp from vw_unit_level where path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' and customer_id='"
            //        + orderSearchInfo.customer_id + "' ";
            //}

            //"Declare @TmpTable Table "
            //  + " (order_id nvarchar(100) "
            //  + " ,row_no int "
            //  + " ); "

            //  + " insert into @TmpTable (order_id,row_no) " + 
            // + " select distinct x.order_id,row_no=row_number() over(" + orderby + ") From ( "  //不要再重复查多次了

            sql += " select distinct order_id,row_no=row_number() over(" + orderby + ")  from ("//只取唯一的order_id，链接后重复的不要重复取
                //row_no要放在表链接后，distinct之后才行，不然，row_no按照重复的数据计数
            + " select  distinct order_id"
            + " ,order_date  "
            + " ,create_time  "
            + " ,order_no  "
            + " ,modify_time "
            + @" from   (select order_date,create_time,order_no,order_id ,modify_time ,purchase_unit_id
                            ,sales_unit_id  from  t_inout a where 1=1 and a.status != '-1' and a.Field7 != '-99' ";
            //判断是否有付款状态条件(jifeng.cao 20140320)
            if (!string.IsNullOrEmpty(orderSearchInfo.PayStatus))
            {
                if (orderSearchInfo.PayStatus == "1")
                {
                    sql += " and a.Field1 = '1' ";
                }
                else
                {
                    sql += " and a.Field1 != '1' ";
                }
            }

            sql = pService.GetLinkSql(sql, "a.order_id", orderSearchInfo.order_id, "%");
            sql = pService.GetLinkSql(sql, "a.customer_id", orderSearchInfo.customer_id, "=");//原来是%
            sql = pService.GetLinkSql(sql, "a.order_no", orderSearchInfo.order_no, "%");
            sql = pService.GetLinkSql(sql, "a.order_type_id", orderSearchInfo.order_type_id, "=");//原来是%
            //sql = pService.GetLinkSql(sql, "a.order_reason_id", orderSearchInfo.order_reason_id, "%");
            sql = pService.GetLinkSql(sql, "a.unit_id", orderSearchInfo.unit_id, "=");
            if (!string.IsNullOrEmpty(orderSearchInfo.order_date_begin))//判断是否为空
            {
                sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_begin, ">=");  //订单开始日期
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.order_date_end))
            {
                sql = pService.GetLinkSql(sql, "a.order_date", orderSearchInfo.order_date_end, "<=");  //订单开始日期
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.complete_date_begin))
            {
                sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_begin, ">=");
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.complete_date_end))
            {
                sql = pService.GetLinkSql(sql, "a.complete_date", orderSearchInfo.complete_date_end, "<=");
            }
            sql = pService.GetLinkSql(sql, "a.status", orderSearchInfo.status, "=");
            sql = pService.GetLinkSql(sql, "a.warehouse_id", orderSearchInfo.warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.ref_order_no", orderSearchInfo.ref_order_no, "%");
            sql = pService.GetLinkSql(sql, "a.data_from_id", orderSearchInfo.data_from_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_unit_id", orderSearchInfo.sales_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_unit_id", orderSearchInfo.purchase_unit_id, "=");
            sql = pService.GetLinkSql(sql, "a.red_flag", orderSearchInfo.red_flag, "=");
            sql = pService.GetLinkSql(sql, "a.purchase_warehouse_id", orderSearchInfo.purchase_warehouse_id, "=");
            sql = pService.GetLinkSql(sql, "a.sales_warehouse_id", orderSearchInfo.sales_warehouse_id, "=");
            // sql = pService.GetLinkSql(sql, "a.vip_no", orderSearchInfo.vip_no, "=");
            if (string.IsNullOrEmpty(orderSearchInfo.data_from_id))
            {
                sql = pService.GetLinkSql(sql, "a.data_from_name", orderSearchInfo.data_from_id, "=");
            }

            //发货时间
            if (!string.IsNullOrEmpty(orderSearchInfo.DeliveryDateBegin))
            {
                sql = pService.GetLinkSql(sql, "a.send_time", orderSearchInfo.DeliveryDateBegin, ">=");
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.DeliveryDateEnd))
            {
                sql = pService.GetLinkSql(sql, "a.send_time", orderSearchInfo.DeliveryDateEnd, "<=");
            }
            #region 取消时间
            if (orderSearchInfo.DeliveryStatus != null && orderSearchInfo.DeliveryStatus.Equals("0"))
            {
                if (!string.IsNullOrEmpty(orderSearchInfo.CancelDateBegin))   //时间
                {
                    sql = pService.GetLinkSql(sql, "a.modify_time", orderSearchInfo.CancelDateBegin, ">=");
                }
                if (!string.IsNullOrEmpty(orderSearchInfo.CancelDateEnd))
                {
                    sql = pService.GetLinkSql(sql, "a.modify_time", orderSearchInfo.CancelDateEnd, "<=");
                }
            }
            #endregion
            //改变这句话****
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " and b.path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' ";
            //}

            //Jermyn20130905 订单配送状态
            if (IsStatus == 1)
            {
                if ((!string.IsNullOrEmpty(orderSearchInfo.DeliveryStatus)) && orderSearchInfo.DeliveryStatus != "0")
                {
                    if (orderSearchInfo.DeliveryStatus == "1234567890")
                    //未分配门店
                    {
                        sql += " AND( a.sales_unit_id = '' OR  a.sales_unit_id is null) AND a.Field7 NOT IN ('600','700','800','900') ";
                    }
                    else
                    {
                        sql = pService.GetLinkSql(sql, "a.Field7", orderSearchInfo.DeliveryStatus, "=");
                    }
                }
                else
                {
                    sql += " and isnull(a.Field7,'')!='' and a.Field7!='0' ";
                }
            }
            sql = pService.GetLinkSql(sql, "a.Field8", orderSearchInfo.DeliveryId, "=");
            sql = pService.GetLinkSql(sql, "a.Field11", orderSearchInfo.DefrayTypeId, "=");

            if (orderSearchInfo.timestamp != null && orderSearchInfo.timestamp.Length > 0)
            {
                sql += string.Format(" and (a.modify_time < (SELECT dbo.TimestampToDate('{0}')) )", orderSearchInfo.timestamp);
            }
            if (!string.IsNullOrEmpty(orderSearchInfo.vip_no))
            {
                sql += string.Format(@" 
            and  a.vip_no in(select VIPID from VIP where (VipName like'%" + orderSearchInfo.vip_no + "%' or WeiXin like '%" + orderSearchInfo.vip_no + "%' or Phone like '%" + orderSearchInfo.vip_no + "%') and IsDelete=0)");
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
            //上面先把内部条件放在里面单独的表里进行查询，然后再去连接查询

            sql += " ) c";
            //连接门店关系表
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //sql += " inner join (select * from vw_unit_level where path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' and customer_id='" 
            //    + orderSearchInfo.customer_id + "' ) b on   (c.purchase_unit_id=b.unit_id or c.sales_unit_id=b.unit_id) ";
            //先把模糊查询的数据查出来，然后再关联到复杂的表里，在复杂查询时就会较少运算时间，提高速度
            //sql += " inner join #unitTemp b on   (c.purchase_unit_id=b.unit_id or c.sales_unit_id=b.unit_id) ";
            sql += " inner join #UnitSET b on   (c.purchase_unit_id=b.UnitID or c.sales_unit_id=b.UnitID) ";

            //}
            //改变这句话****  ,把模糊查询的放在了上面，在表连接之前先进行查询
            //if (orderSearchInfo.path_unit_id != null && orderSearchInfo.path_unit_id.Length > 0)
            //{
            //    sql += " and b.path_unit_id like '%" + orderSearchInfo.path_unit_id + "%' ";
            //}

            sql += "  where 1=1 "; // ----and a.status != '-1' and a.Field7 != '-99'

            sql += "  ) x "; // ----and a.status != '-1' and a.Field7 != '-99'

            //sql = sql + " ) x ; ";

            //sql = sql + " Declare @iCount int;";

            //sql = sql + " select @iCount = COUNT(1) From @TmpTable;";
            #endregion
            return sql;
        }

        /// <summary>
        /// 获取物流公司名称
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public string GetCompanyName(string CompanyID)
        {
            string Result = "";
            string sql = string.Format("select LogisticsName from T_LogisticsCompany where LogisticsID='{0}'", CompanyID);
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["LogisticsName"] != DBNull.Value)
                    {
                        Result = ds.Tables[0].Rows[0]["LogisticsName"].ToString();
                    }
                }
            }
            return Result;
        }
        /// <summary>
        /// 获取单据，打印配送单
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public DataSet GetInoutInfoByIdDelivery(string order_id)
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
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.Pay_Id) payment_name "
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
                        + " ,a.Field11 "
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
        /// <summary>
        /// 获取订单运费
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public decimal? GetDeliveryAmountByOrderId(string orderId)
        {
            var sql = @"select DeliveryAmount from [⁭TOrderCustomerDeliveryStrategyMapping] where OrderId='{0}'";
            var amount = SQLHelper.ExecuteScalar(string.Format(sql, orderId)) as decimal?;
            return amount;
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
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.Field11) payment_name "
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
                      + " ,convert(varchar(10), a.create_time, 120) create_time "
                      + " ,a.create_user_id "
                      + " ,a.approve_time "
                      + " ,a.approve_user_id "
                      + " ,convert(varchar(10), a.send_time, 120) send_time "
                      + " ,a.send_user_id "
                      + " ,a.accpect_time "
                      + " ,a.accpect_user_id "
                      + " ,convert(varchar(10), a.modify_time, 120) modify_time "
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
                        + " ,a.Field11 "
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

        /// <summary>
        /// 根据单据标识，获取单据详细信息 jifeng.cao 20140320
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public DataSet GetInoutInfoById_lj(string order_id)
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
                      + " ,(select payment_type_name From T_Payment_Type where payment_type_id = a.pay_id) payment_name "   //支付方式
                      + " ,a.print_times "
                      + " ,a.carrier_id "
                //+ " ,(select top 1 unit_name From t_unit where unit_id = a.carrier_id) carrier_name "
                      + " ,(select top 1 LogisticsName From T_LogisticsCompany where CAST(LogisticsID AS varchar(50)) = a.carrier_id) carrier_name "
                      + " ,a.remark "
                      + " ,a.status "
                      + " ,a.status_desc "
                      + " ,convert(decimal(18,4),a.total_qty) total_qty " //*red_flag
                      + " ,convert(decimal(18,4),a.total_retail) total_retail " //*red_flag
                      + " ,a.keep_the_change "
                      + " ,a.wiping_zero "
                      + " ,a.vip_no "
                      //+ " ,isnull(isnull((select top 1 vipName From vip where vipId = a.vip_no),a.Field3),a.Field6) vip_name "
                      + " ,(select top 1 vipName From vip where vipId = a.vip_no) vip_name "
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
                //配送费,查不到数据就变为0
                      + ",isnull((select DeliveryAmount From TOrderCustomerDeliveryStrategyMapping TOCDSM WHERE TOCDSM.OrderID = a.Order_ID and isdelete=0 ),0) DeliveryAmount"

                //积分折扣,下单是消费的积分20，订单返回给的积分	 21
                      + @",isnull( CONVERT(decimal(18,2),  ISNULL(   ISNULL((select CAST(SettingValue AS decimal(18,2))  from CustomerBasicSetting  where SettingCode  ='IntegralAmountPer' and customerid=a.customer_id and isdelete=0)
	                    ,(select CAST(SettingValue AS decimal(18,2))  from CustomerBasicSetting  where SettingCode  ='IntegralAmountPer' and customerid is null  and isdelete=0) )
	                    ,'0')*(      select top 1 abs( isnull(integral,0) )  as integral from vipIntegralDetail 
                                   where objectId = a.order_id     and vipId =vip_no  and  IntegralSourceID=20 order by createtime  )   )     ,0)as	   pay_pointsAmount"
                //优惠券折扣
                + @"   ,(select isnull(b.ParValue,0) from TOrderCouponMapping d,CouponType b ,Coupon c
   where d.CouponId = c.CouponID
   and b.CouponTypeID =CONVERT(NVARCHAR(200), c.CouponTypeID ) and d.orderId=a.order_id ) as couponAmount"
                //余额支付 AmountSourceId=1 才是订单消费的，AmountSourceId=2是订单返现的
                + @", (  select isnull(Amount,0) as Amount from VipAmountDetail
      where ObjectId =a.order_id and VipId =a.vip_no and AmountSourceId=1) as vipEndAmount"

                      + " ,a.Field1 "
                        + " ,a.Field2 "
                        + " ,a.Field3 "
                        + " ,a.Field4 "
                        + " ,a.Field5 "
                        + " ,a.Field6 "
                        + " ,a.Field7 "
                        + " ,a.Field8 "
                        + " ,cast(a.Field9 as varchar(19)) as Field9 "
                        + " ,a.Field10 "
                        + " ,a.Field11 "
                        + " ,a.Field12 "
                        + " ,a.Field13 "
                        + " ,a.Field14 "
                        + " ,a.Field15 "
                        + " ,a.Field16 "
                        + " ,a.Field17 "
                        + " ,a.Field18 "
                        + " ,a.Field19"
                        + " ,a.Field20 "
                        + " ,paymentcenter_id "
                         + " ,(select DeliveryName From Delivery x WHERE x.DeliveryId = a.Field8 ) DeliveryName"  //配送方式
                        + " ,(select DefrayTypeName From DefrayType x WHERE x.DefrayTypeId = a.Field11 ) DefrayTypeName "
                        + "  ,( SELECT ISNULL(Amount, 0) AS Amount FROM VipAmountDetail WHERE ObjectId = a.order_id AND VipId = a.vip_no AND AmountSourceId = 13 ) AS ReturnAmount "//返现抵扣 AmountSourceId=13
                      + " From T_Inout a "

                      + " where 1=1 and a.order_id = '" + order_id + "';";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
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
        /// 获取配送单 对应的订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetDeliveryDetail(string orderId)
        {
            string sql1 = @"(SELECT   v.status as s1,S.status as s2,TI.status  as s3, ISNULL(I.order_id, N'') AS OrderId, ISNULL(V.VIPID, N'') AS VipId, ISNULL(OI.ImageURL, N'') AS HeadImgUrl, ISNULL(V.VipName, N'游客') AS VipName, 
                      ISNULL(V.VipLevel, 0) AS VipLevel, ISNULL(CONVERT(DECIMAL(18, 2), ID.retail_price), 0) AS Price, ISNULL(ID.order_qty, 0) AS Qty, ISNULL(T.ItemType, N'') 
                      AS ItemDesc, I.modify_time AS PayTime, TI.item_id AS ItemId, S.sku_id AS SkuId, I.customer_id AS CustomerId, TI.item_code AS ItemCode, S.barcode, 
                      TI.item_name AS ItemName, U.unit_name AS SalesUnitName, ID.enter_price AS EnterPrice, ID.enter_amount AS EnterAmount, I.remark, ISNULL(ISS.ItemSortId, 1) 
                      AS ItemSort, I.Field1 AS IsPay
FROM         dbo.T_Inout AS I INNER JOIN
                      dbo.T_Inout_Detail AS ID ON I.order_id = ID.order_id 
                      LEFT OUTER JOIN
                      dbo.Vip AS V ON I.vip_no = V.VIPID INNER JOIN
                      dbo.T_Sku AS S ON ID.sku_id = S.sku_id INNER JOIN
                      dbo.T_Item AS TI ON S.item_id = TI.item_id LEFT OUTER JOIN
                      dbo.t_unit AS U ON I.sales_unit_id = U.unit_id LEFT OUTER JOIN
                      dbo.ItemItemSortMapping AS IIS ON IIS.ItemId = TI.item_id LEFT OUTER JOIN
                      dbo.ItemSort AS ISS ON IIS.ItemSortId = ISS.ItemSortId LEFT OUTER JOIN
                          (SELECT     ObjectId, MAX(ImageURL) AS ImageURL
                            FROM          dbo.ObjectImages
                            WHERE      (IsDelete = 0) AND (ISNULL(ImageURL, N'') <> '')
                            GROUP BY ObjectId) AS OI ON V.VIPID = OI.ObjectId LEFT OUTER JOIN
                          (SELECT     sku_id, item_id, CASE WHEN prop_1_id IS NOT NULL 
                                                   THEN prop_1_name + N':' + prop_1_detail_code + N'.' ELSE '' END + CASE WHEN prop_2_id IS NOT NULL 
                                                   THEN prop_2_name + N':' + prop_2_detail_code + N'.' ELSE '' END + CASE WHEN prop_3_id IS NOT NULL 
                                                   THEN prop_3_name + N':' + prop_3_detail_code + N'.' ELSE '' END + CASE WHEN prop_4_id IS NOT NULL 
                                                   THEN prop_4_name + N':' + prop_4_detail_code + N'.' ELSE '' END + CASE WHEN prop_5_id IS NOT NULL 
                                                   THEN prop_5_name + N':' + prop_5_detail_code + N'.' ELSE '' END AS ItemType
                            FROM          dbo.vw_sku) AS T ON S.sku_id = T.sku_id
WHERE     1=1) as t";
            //VwInoutOrderItems
            string sql = @"select ItemCode,BarCode,SalesUnitName,EnterPrice,qty,EnterAmount,Remark,ItemName from " + sql1
                + @" where orderid='{0}'
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null
                            union all select null,null,null,null,null,null,null,null";
            return SQLHelper.ExecuteDataset(string.Format(sql, orderId));
        }
        /// <summary>
        /// 根据单据标识，获取单据明细详细信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetInoutDetailInfoByOrderId(string orderId,string strCustomerId)
        {
            #region
            string sql = "select a.order_detail_id "
                      + " ,a.order_id "
                      + " ,a.ref_order_detail_id "
                      + "  ,oi.ImageUrl "
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
                      + " ,b.prop_1_detail_name "
                      + " ,b.prop_2_detail_name "
                      + " ,b.prop_3_detail_name "
                      + " ,b.prop_4_detail_name "
                      + " ,b.prop_5_detail_name "
                      + " ,(select discount_rate from t_inout where order_id = a.order_id)  order_discount_rate "

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
                        + ",(SELECT y.item_category_name  FROM dbo.T_Item x INNER JOIN dbo.T_Item_Category y ON(x.item_category_id = y.item_category_id) WHERE x.item_id = b.item_id ) itemCategoryName "
                        + " ,isnull(datediff(day,a.Field1,a.Field2),0) DayCount "
                      + " From t_inout_detail a WITH(NOLOCK)  "
                      + " inner join vw_sku b "
                      + " on(a.sku_id = b.sku_id) "
                      + " inner join t_inout c WITH(NOLOCK)  "
                      + " on(a.order_id = c.order_id)"
                      + " LEFT JOIN (SELECT *,ROW_NUMBER() OVER(PARTITION BY ObjectId  ORDER BY DisplayIndex ASC ) rowIndex FROM  ObjectImages  WHERE CustomerId='" + strCustomerId + "') oi ON oi.objectID=b.item_id AND oi.rowIndex=1 "
                      + " where a.order_id= '" + orderId + "' order by b.item_code";
            #endregion
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
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
                    if (!IsExistOrderCode(inoutInfo.order_no, inoutInfo.order_id, tran))
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
        public bool IsExistOrderCode(string order_no, string order_id, IDbTransaction pTran)
        {
            try
            {
                string sql = "select isnull(count(*),0) From T_Inout where 1=1 and order_no = '" + order_no + "' ";
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
            sql = pService.GetIsNotNullUpdateSql(sql, "remark", inoutInfo.remark);
            sql = pService.GetIsNotNullUpdateSql(sql, "status", inoutInfo.status);
            sql = pService.GetIsNotNullUpdateSql(sql, "status_desc", inoutInfo.status_desc);
            sql = pService.GetIsNotNullUpdateSql(sql, "total_qty", inoutInfo.total_qty.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "total_retail", inoutInfo.total_retail.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "keep_the_change", inoutInfo.keep_the_change.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "wiping_zero", inoutInfo.wiping_zero.ToString());
            sql = pService.GetIsNotNullUpdateSql(sql, "vip_no", inoutInfo.vip_no);
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
                      + " ,'" + inoutInfo.remark + "' remark "
                      + " ,'" + inoutInfo.status + "' status "
                      + " ,'" + inoutInfo.status_desc + "' status_desc "
                      + " ,'" + inoutInfo.total_qty + "' total_qty "
                      + " ,'" + inoutInfo.total_retail + "' total_retail "
                      + " ,'" + inoutInfo.keep_the_change + "' keep_the_change "
                      + " ,'" + inoutInfo.wiping_zero + "' wiping_zero "
                      + " ,'" + inoutInfo.vip_no + "' vip_no "
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
            string sql = SearchInoutDetailSql(orderSearchInfo);
            sql += " select * from #tmp a where a.displayindex between '" + orderSearchInfo.StartRow +
                "' and '" + orderSearchInfo.EndRow + "' order by a.displayindex";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
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
        public void UpdateOrderDeliveryStatus(string orderId, string status, string statusDesc, string send_time)
        {
            string sql = " UPDATE dbo.T_Inout SET Field7 = '" + status + "', Field10 = '" +
                statusDesc + "',modify_time= '" + base.GetCurrentDateTime() + "',modify_user_id = '" +
                this.loggingSessionInfo.UserID.ToString() + "' ";
            if (send_time != null && !send_time.Equals(""))
            {
                sql += ",send_time = '" + send_time + "'";
                sql += ",Field9 = '" + send_time + "'";
            }
            sql += " WHERE order_id = '" + orderId + "' ; ";
            sql += string.Format(@"  insert T_Bill(bill_id,bill_Status,bill_remark,add_user_id)
            values('{0}','{1}','订单状态修改','{2}');", orderId, status, this.loggingSessionInfo.UserID.ToString());
            if (status.Equals("0"))
            {
                TOrderCouponMappingDAO mappingServer = new TOrderCouponMappingDAO(this.CurrentUserInfo);
                mappingServer.DeleteOrderCouponMapping(orderId);
            }
            this.SQLHelper.ExecuteNonQuery(sql);
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
            //if (inoutInfo != null && inoutInfo.create_time != null)
            //{
            //    sql += " ,create_time = '" + inoutInfo.create_time + "' ";
            //}
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

        #region 统计用户信息

        public DataSet GetVipSummerInfo(string orderid)
        {
            // loggingSessionInfo.CurrentUser.User_Id //VIPID

            var sql = string.Format(@"
declare @vipid varchar(50) = '';

select @vipid = vip_no from T_Inout where order_id = '{0}';

select 
	COUNT(1) 
from Coupon C 
inner join VipCouponMapping V on C.CouponID = V.VIPID
where C.IsDelete = 0 and V.IsDelete = 0
and V.VIPID = @vipid
and Not Exists 
(
	select 
		top 1 1 
	from TOrderCouponMapping C1
	inner join T_Inout I on C1.OrderId = I.order_id and I.status <> 6
	where C1.CouponId = C.CouponID
);

select 
	T.TagsDesc 
from Tags T
inner join VipTagsMapping M on T.TagsId = M.TagsId and M.IsDelete = 0
where T.IsDelete = 0 
and M.VipId = @vipid;

select SUM(ISNULL( I.actual_amount, 0)) TotalSum from T_Inout I where I.status = 5 
and I.vip_no = @vipid;

select VipCode, VipName, WeiXin, TencentMBlog, SinaMBlog, Integration from Vip where IsDelete = 0
and VipID = @vipid;
", orderid);
            return SQLHelper.ExecuteDataset(CommandType.Text, sql);
        }

        #endregion

        #region 保存配送信息

        public void SaveDeliveryInfo(Dictionary<string, string> dict, string order_id)
        {
            if (dict == null)
            {
                throw new ArgumentNullException("dict");
            }
            var sql = new StringBuilder(" Update T_InOut Set");
            sql.AppendFormat(" modify_time = getdate(), modify_user_id ='{0}'", loggingSessionInfo.CurrentUser.User_Id);
            foreach (var kv in dict)
            {
                sql.AppendFormat(", {0} = '{1}'", kv.Key, kv.Value);
            }
            sql.AppendFormat(" where order_id = '{0}'", order_id);
            SQLHelper.ExecuteNonQuery(sql.ToString());
        }

        #endregion

        #region 保存付款方式

        public void SaveDefrayType(string defrayType, string order_id)
        {

            var sql = new StringBuilder(" Update T_InOut Set");
            sql.AppendFormat(" modify_time = getdate(), modify_user_id ='{0}'", loggingSessionInfo.CurrentUser.User_Id);
            sql.AppendFormat(" ,Field11= '{0}'", defrayType);
            sql.AppendFormat(" where order_id = '{0}'", order_id).AppendLine();

            /*sql.AppendFormat(@"
declare @delivery varchar(100);
declare @status varchar(100);
select @delivery = Field8, @status= Status from T_Inout where order_id = '{0}'
if(@delivery = 2 and @status = 2)
update T_Inout set Status = 3 , status_desc = '未发货', Field7='3', Field10 = '未发货' where order_id = '{0}'
if(@delivery = 1 and @status = 2)
update T_Inout set Status = 2 , status_desc = '未付款', Field7='2', Field10 = '未付款' where order_id = '{0}'

", order_id);*/
            SQLHelper.ExecuteNonQuery(sql.ToString());
        }

        #endregion
    }
}
