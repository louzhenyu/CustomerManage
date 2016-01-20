/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:14
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表T_Inout的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_InoutDAO : Base.BaseCPOSDAO, ICRUDable<T_InoutEntity>, IQueryable<T_InoutEntity>
    {
        public void NewLoad(IDataReader rd, out T_InoutEntity m)
        {
            this.Load(rd, out m);
        }

        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        public DataSet GetItemEventSalesUserList(string itemId, Guid eventId)
        {
            string sql = string.Format(@" SELECT DISTINCT a.vip_no AS userId, imageURL = '' from T_Inout a
            INNER JOIN dbo.T_Inout_Detail b ON a.order_id = b.order_id 
            INNER JOIN dbo.T_Sku c ON b.sku_id = c.sku_id 
            INNER JOIN dbo.PanicbuyingEventOrderMapping d on a.order_id=d.OrderID
            WHERE (a.vip_no IS NOT NULL AND a.vip_no <> '') 
             AND c.item_id = '{0}' and d.eventId='{1}'", itemId, eventId);
            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetEventStoreByItemAndEvent(string itemId, Guid eventId)
        {
            string sql = string.Empty;
            sql += " SELECT TOP 1 storeId = a.UnitId ";
            sql += " ,storeName = b.unit_name ";
            sql += " ,address = b.unit_address ";
            sql += " ,imageURL = b.imageURL ";
            sql += " ,phone=b.unit_tel ";
            sql += " ,storeCount = (SELECT COUNT(*) FROM dbo.ItemStoreMapping WHERE ItemId = '" + itemId + "' and IsDelete=0) ";
            sql += " FROM dbo.ItemStoreMapping a ";
            sql += " INNER JOIN dbo.t_unit b ON a.UnitId = b.unit_id ";
            sql += " WHERE a.ItemId = '" + itemId + "' and a.IsDelete=0";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetItemBrandInfo(string itemId)
        {
            string sql = string.Format(@" SELECT brandId = a.BrandId ,brandLogoURL = b.BrandLogoURL 
            ,brandName = b.BrandName 
            ,brandEngName = b.BrandEngName 
            FROM dbo.vw_item_detail a 
            INNER JOIN dbo.BrandDetail b ON a.BrandId = b.BrandId 
            WHERE a.item_id = '{0}' ", itemId);
            return this.SQLHelper.ExecuteDataset(sql);
        }

        public DataSet GetItemProp1List(string skuId)
        {
            string sql = "SELECT DISTINCT a.sku_id skuId,a.sku_prop_id1 prop1DetailId,ISNULL(b.prop_name,a.sku_prop_id1) prop1DetailName,c.stock,c.salesCount "
                        + " FROM dbo.T_Sku a  LEFT JOIN dbo.T_Prop b "
                        + " ON(a.sku_prop_id1 = b.prop_id "
                        + " AND b.status = '1') "
                        + " LEFT JOIN vw_sku_detail c ON a.sku_id=c.sku_id "
                        + " WHERE "
                        + " a.sku_id = '" + skuId + "' ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 订单查询状态为待付款  Add by Alex Tian 2014-04-09
        /// 配送方式是到店提货的订单：未支付的订单（判断订单表中Field1是否付款 1=是）（款到发货）配送方式是货到付款的(1.货到付款)订单：都不属于 （货到付款）
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderByObligation(string vipno, int PageIndex, int PageSize, string customer_id)
        {
            StringBuilder sbSQL = new StringBuilder();
            DataSet ds = new DataSet();
            string strWhere = "";

            if (!string.IsNullOrEmpty(customer_id))
            {
                strWhere += " and customer_id='" + customer_id + "'";
            }
            strWhere += " and isnull(Field1,0) !='1' and Field7<>-99  and status not in ('-1','700','800','900') ";  //已取消的订单,已完成，审核未通过的订单都不在待付款里面
            sbSQL.AppendLine(string.Format("select count(*) from T_Inout where 1=1 and vip_no='" + vipno + "' " + strWhere + ""));

            //sql中添加了支付方式Payment_Type_Code
            sbSQL.AppendLine(string.Format("select p.Payment_Type_Code,t.* from (select row_number()over(order by create_time desc) _row,* from T_Inout where 1=1 and vip_no='" + vipno + "' " + strWhere + ") t left join T_Payment_Type p on t.pay_id=p.Payment_Type_Id where t._row>{0}*{1} and t._row<=({0}+1)*{1}",
                 (Convert.ToInt32(PageIndex) - 1) < 0 ? 0 : (Convert.ToInt32(PageIndex)), PageSize));
            ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        /// <summary>
        /// 订单查询状态为待收货/提货  Add by Alex Tian 2014-04-10
        /// 配送方式是到店提货的订单：已支付的订单（判断订单表中Field1是否付款 1=是），但是未结束（结束状态为：已取消，已完成，审核未通过）。配送方式是货到付款的订单：除了已结束订单都属于。（订单状态为：已取消，已完成，审核未通过）。

        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderByNodelivery(string vipno, int PageIndex, int PageSize, string customer_id)
        {
            StringBuilder sbSQL = new StringBuilder();
            DataSet ds = new DataSet();
            string strWhere = "";
            if (!string.IsNullOrEmpty(customer_id))
            {
                strWhere += " and customer_id='" + customer_id + "'";
            }
            strWhere += " and isNULL(Field1,0) ='1' and status not in('700','800','900') and Field7<>-99 ";
            sbSQL.AppendLine(string.Format("select COUNT(1) from T_Inout where 1=1 and vip_no='" + vipno + "'  " + strWhere + " "));
            sbSQL.AppendLine(string.Format("select p.Payment_Type_Code,t.*  from (select row_number()over(order by create_time desc) _row,* from T_Inout where 1=1 and vip_no='" + vipno + "'  " + strWhere + " ) t left join T_Payment_Type p on t.pay_id=p.Payment_Type_Id where t._row>{0}*{1} and t._row<=({0}+1)*{1}",
                 (Convert.ToInt32(PageIndex) - 1) < 0 ? 0 : (Convert.ToInt32(PageIndex)), PageSize));
            ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        /// <summary>
        /// 订单查询状态已完成的  Add by Alex Tian 2014-04-10
        /// 订单状态为：已取消，已完成，审核未通过的所有订单。
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrderBydonedeal(string vipno, int PageIndex, int PageSize, string customer_id)
        {
            StringBuilder sbSQL = new StringBuilder();
            DataSet ds = new DataSet();
            string strWhere = "";
            //if (!string.IsNullOrEmpty(vipno))
            //{
            //    strWhere += "and vip_no='" + vipno + "'";
            //}
            if (!string.IsNullOrEmpty(customer_id))
            {
                strWhere += " and customer_id='" + customer_id + "'";
            }
            strWhere += " and status in ('700','800','900') and Field7<>-99 ";
            sbSQL.AppendLine(string.Format("select COUNT(1) from T_Inout where 1=1 and vip_no='" + vipno + "' " + strWhere + " "));
            sbSQL.AppendLine(string.Format("select p.Payment_Type_Code,t.*  from (select row_number()over(order by create_time desc) _row,* from T_Inout where 1=1 and vip_no='" + vipno + "' " + strWhere + " ) t left join T_Payment_Type p on t.pay_id=p.Payment_Type_Id where t._row>{0}*{1} and t._row<=({0}+1)*{1}",
                 (Convert.ToInt32(PageIndex) - 1) < 0 ? 0 : (Convert.ToInt32(PageIndex)), PageSize));
            ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }
        /// <summary>
        /// 根据订单状态获取订单
        /// </summary>
        /// <param name="vipno"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="customer_id"></param>
        /// <param name="groupingType"></param>
        /// <returns></returns>
        public DataSet GetOrderByGroupingType(string vipno, int PageIndex, int PageSize, string customer_id, int groupingType)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@VipID", System.Data.SqlDbType.NVarChar) { Value = vipno };
            parm[1] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = customer_id };
            parm[2] = new SqlParameter("@GroupingType", System.Data.SqlDbType.Int) { Value = groupingType };
            parm[3] = new SqlParameter("@PageIndex", System.Data.SqlDbType.Int) { Value = PageIndex + 1 };
            parm[4] = new SqlParameter("@PageSize", System.Data.SqlDbType.Int) { Value = PageSize };

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetOrderDetail", parm);

        }

        /// <summary>
        /// 根据订单状态,店员ID获取人人销售订单
        /// </summary>
        public DataSet GetOrderByGroupingTypeEvery(string UserID, int PageIndex, int PageSize, string customer_id, int groupingType)
        {
            var parm = new SqlParameter[5];
            parm[0] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = UserID };
            parm[1] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = customer_id };
            parm[2] = new SqlParameter("@GroupingType", System.Data.SqlDbType.Int) { Value = groupingType };
            parm[3] = new SqlParameter("@PageIndex", System.Data.SqlDbType.Int) { Value = PageIndex + 1 };
            parm[4] = new SqlParameter("@PageSize", System.Data.SqlDbType.Int) { Value = PageSize };

            Loggers.Debug(new DebugLogInfo()
            {
                Message = parm.ToJSON()
            });
            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "ProcGetUserOrderGrouping", parm);

        }
        /// <summary>
        /// 获取每种状态下的订单数目
        /// </summary>
        /// <param name="vipno"></param>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public DataSet GetGroupOrderCount(string vipno, string customer_id)
        {
            StringBuilder sbSQL = new StringBuilder();
            DataSet ds = new DataSet();
            string strWhere = "";
            string strWhere1 = "";
            string strWhere2 = "";
            string strWhere3 = "";
            if (!string.IsNullOrEmpty(customer_id))
            {
                strWhere += " and customer_id='" + customer_id + "'";
            }
            strWhere1 = strWhere + " and vip_no='" + vipno + "' and isnull(Field1,0) !='1' and Field7<>-99  and status not in ('-1','700','800','900')";
            strWhere2 = strWhere + " and vip_no='" + vipno + "' and isNULL(Field1,0) ='1' and status not in('700','800','900') and Field7<>-99 ";
            strWhere3 = strWhere + " and vip_no='" + vipno + "' and status in ('700','800','900') and Field7<>-99";
            sbSQL.AppendLine(string.Format("select count(1) from T_Inout where 1=1 " + strWhere1 + ""));
            sbSQL.AppendLine(string.Format("select COUNT(1) from T_Inout where 1=1 " + strWhere2 + " "));
            sbSQL.AppendLine(string.Format("select COUNT(1) from T_Inout where 1=1 " + strWhere3 + " "));
            ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }

        /// <summary>
        /// 查询订单列表 Add by Alex Tian 2014-04-16
        /// </summary>
        /// <param name="pOrderStatuses">订单状态</param>
        /// <param name="pOrderID">订单ID</param>
        /// <param name="pVIPID">会员ID</param>
        /// <param name="pPageSize">页显示条数</param>
        /// <param name="pPageIndex">当前页码</param>
        /// <returns></returns>
        public DataSet GetOrderList(int[] pOrderStatuses, string pOrderID, int pPageSize, int pPageIndex)
        {
            StringBuilder sbStrWhere = new StringBuilder();
            StringBuilder sbSQL = new StringBuilder();
            DataSet ds = new DataSet();
            if (pOrderStatuses.Length > 0)
            {
                StringBuilder sbStatus = new StringBuilder();
                foreach (var item in pOrderStatuses)
                {
                    sbStatus.AppendFormat("'{0}',", item);
                }
                sbStrWhere.AppendFormat(" and [status] in ({0})", sbStatus.ToString().Trim(','));
            }
            if (!string.IsNullOrEmpty(pOrderID))
            {
                sbStrWhere.AppendFormat(" and order_id ='{0}'", pOrderID);
            }
            sbSQL.AppendFormat("select * from (select row_number()over(order by customer_id) _row,* from T_Inout where 1=1 and customer_id='{0}'  " + sbStrWhere + ") t where t._row>{1}*{2} and t._row<=({1}+1)*{2}", CurrentUserInfo.ClientID,
                 (Convert.ToInt32(pPageIndex) - 1) < 0 ? 0 : (Convert.ToInt32(pPageIndex)), pPageSize);
            ds = this.SQLHelper.ExecuteDataset(sbSQL.ToString());
            return ds;
        }


        #region 获取订单详细列表中的商品规格 Add by changjian.tian
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
        public DataSet GetOrdersList(string orderId, string userId, string orderStatusList,string isPayment, string orderNo, string customerId
            , int pageSize, int pageIndex, string OrderChannelID)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });
            paras.Add(new SqlParameter() { ParameterName = "@pUserId", Value = userId });
            //      paras.Add(new SqlParameter() { ParameterName = "@pOrderId", Value = orderId });
            paras.Add(new SqlParameter() { ParameterName = "@pPageSize", Value = pageSize });
            paras.Add(new SqlParameter() { ParameterName = "@pPageIndex", Value = pageIndex });
            paras.Add(new SqlParameter() { ParameterName = "@pOrderNo", Value = orderNo });
            string sqlWhere = "";
            if (string.IsNullOrEmpty(orderStatusList))
            {
                sqlWhere = " and 1 = 1";
            }
            else
            {
                sqlWhere = "and a.status in (" + orderStatusList + ")";
            }
            if (!string.IsNullOrEmpty(isPayment))
            {
                sqlWhere = " and a.Field1 =" + isPayment;
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                sqlWhere += " and a.order_no like '%" + orderNo + "%'";
            }
            //订单来源
            if (!string.IsNullOrEmpty(OrderChannelID) && OrderChannelID != "-1")
            {
                if (OrderChannelID == "19")
                {
                    sqlWhere += " and  col20 in (select retailtraderID from retailtrader  where CustomerID=@pCustomerId ) ";
                }
                else
                {
                    sqlWhere += " and a.data_from_id = '" + OrderChannelID + "'";
                }
            }

            StringBuilder sql = new StringBuilder();

            sql.Append(" create table #tmp(unit_id nvarchar(2000))");
            sql.Append(" insert into #tmp");
            sql.Append(" select distinct a.unit_id  from vw_unit_level a inner join T_User_Role b");
            sql.Append(" on a.path_unit_id like '%' +b.unit_id + '%' and (isnull(@pUserId,'') = '' or b.user_id = @pUserId) and a.customer_id = @pCustomerId");

            sql.Append(" select * from (");
            sql.Append(" select row_number() over(order by a.create_time desc) _row,");
            sql.Append(" a.order_id,a.order_no,isnull(a.Field8,'0') as DeliveryTypeId,a.create_time OrderDate,");
            sql.Append(" a.status_desc OrderStatusDesc,c.vipName as VipName,create_time,");
            sql.Append(" a.status OrderStatus,isnull(a.total_qty,0) TotalQty,isnull(a.total_retail,0) TotalAmount,total_amount");//total_retail是商品零售价，total_amount才是总额
            sql.Append(@",ISNULL((select RetailTraderName from RetailTrader  where RetailTraderID=c.Col20),'') as RetailTraderName
,ISNULL((select user_name from T_User  where user_id=a.sales_user),'') as ServiceMan
,ISNULL((select top 1 Amount from VipAmountDetail where ObjectId=a.order_id and VipId= c.SetoffUserId and AmountSourceId='10'),0)
+   -----OBJECT_ID是订单号，vipID是获取收益的人(集客员工),防止出现脏数据才用了top 1的方式
ISNULL((select top 1 Amount from VipAmountDetail  where ObjectId=a.order_id and AmountSourceId in ('14','15') and VipId=(select SellUserID from RetailTrader where RetailTraderID=c.Col20)),0)
as CollectIncome");
            sql.Append(" from t_inout a inner join #tmp b");
            sql.Append(" on a.sales_unit_id = b.unit_id ");
            sql.Append(" left join vip c on a.vip_no = c.vipId");
            sql.AppendFormat(" where a.status not in( '-1' ) and field7 <> 0 and field7<>-99 and (isnull('{0}','')='' or order_id like '%{0}%' )", orderId);
            //, '700','800'
            sql.AppendFormat("  {0}", sqlWhere);
            sql.Append(" ) t where t._row>@pPageIndex*@pPageSize and t._row<=(@pPageIndex+1)*@pPageSize");
            sql.Append("  ORDER BY create_time DESC ");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

        }



        public DataSet GetServiceOrderList(string order_no, string OrderChannelID, string userId, string customerId, int? pageSize, int? pageIndex)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });
            paras.Add(new SqlParameter() { ParameterName = "@pUserId", Value = userId });
            //      paras.Add(new SqlParameter() { ParameterName = "@pOrderId", Value = orderId });
            paras.Add(new SqlParameter() { ParameterName = "@pPageSize", Value = pageSize });
            paras.Add(new SqlParameter() { ParameterName = "@pPageIndex", Value = pageIndex });
            paras.Add(new SqlParameter() { ParameterName = "@pOrderNo", Value = order_no });
            string sqlWhere = " and 1 = 1 and a.create_time is not null";//时间不能为空
            sqlWhere += "  and sales_user=@pUserId and customer_id=@pCustomerId ";//当前登录帐户销售的

            if (!string.IsNullOrEmpty(order_no))
            {
                sqlWhere += " and a.order_no like '%" + order_no + "%'";
            }
            //订单来源
            if (!string.IsNullOrEmpty(OrderChannelID) && OrderChannelID != "-1")
            {
                if (OrderChannelID == "19")
                {
                    sqlWhere += " and  col20 in (select retailtraderID from retailtrader  where CustomerID=@pCustomerId ) ";
                }
                else
                {
                    sqlWhere += " and a.data_from_id = '" + OrderChannelID + "'";
                }
            }

            StringBuilder sql = new StringBuilder();

            //sql.Append(" create table #tmp(unit_id nvarchar(2000))");
            //sql.Append(" insert into #tmp");
            //sql.Append(" select distinct a.unit_id  from vw_unit_level a inner join T_User_Role b");
            //sql.Append(" on a.path_unit_id like '%' +b.unit_id + '%' and (isnull(@pUserId,'') = '' or b.user_id = @pUserId) and a.customer_id = @pCustomerId");

            sql.Append(" select * from (");
            sql.Append(" select row_number() over(order by a.create_time desc) _row,");
            sql.Append(" a.order_id as OrderID,a.order_no as OrderNO,isnull(a.Field8,'0') as DeliveryTypeId,a.create_time OrderDate,create_time,");
            sql.Append(" a.status_desc OrderStatusDesc,c.vipName  as VipName,");
            sql.Append(" a.status OrderStatus,isnull(a.total_qty,0) TotalQty,isnull(a.total_retail,0) TotalAmount");
            sql.Append(@",ISNULL((select RetailTraderName from RetailTrader  where RetailTraderID=c.Col20),'') as RetailTraderName
,ISNULL((select user_name from T_User  where user_id=a.sales_user),'') as ServiceMan
,ISNULL((select top 1 Amount from VipAmountDetail where ObjectId=a.order_id and VipId= c.SetoffUserId and AmountSourceId='10'),0)
+   -----OBJECT_ID是订单号，vipID是获取收益的人(集客员工),防止出现脏数据才用了top 1的方式
ISNULL((select top 1 Amount from VipAmountDetail  where ObjectId=a.order_id and AmountSourceId in ('14','15') and VipId=(select SellUserID from RetailTrader where RetailTraderID=c.Col20)),0)
as CollectIncome");
            sql.Append(" from t_inout a ");
          //  sql.Append("inner join #tmp bon a.sales_unit_id = b.unit_id ");
            sql.Append(" left join vip c on a.vip_no = c.vipId");
            sql.AppendFormat(" where a.status not in( '-1') and field7 <> 0 and field7<>-99 ");
            //and (isnull('{0}','')='' or order_id like '%{0}%' ),, orderId
            // //a.status not in( '-1' , '700','800') and
            sql.AppendFormat("  {0}", sqlWhere);
            sql.Append(" ) t where t._row>@pPageIndex*@pPageSize and t._row<=(@pPageIndex+1)*@pPageSize");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

        }


        public DataSet GetCollectOrderList(string order_no, string OrderChannelID, string userId, string customerId, int? pageSize, int? pageIndex)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pCustomerId", Value = customerId });
            paras.Add(new SqlParameter() { ParameterName = "@pUserId", Value = userId });
            //      paras.Add(new SqlParameter() { ParameterName = "@pOrderId", Value = orderId });
            paras.Add(new SqlParameter() { ParameterName = "@pPageSize", Value = pageSize });
            paras.Add(new SqlParameter() { ParameterName = "@pPageIndex", Value = pageIndex });
            paras.Add(new SqlParameter() { ParameterName = "@pOrderNo", Value = order_no });
            string sqlWhere = " and 1 = 1 and a.create_time is not null";//时间不能为空
            sqlWhere += "  and customer_id=@pCustomerId ";
            //是集客的
            sqlWhere += @"and (SetoffUserId=@pUserId    ---是该会员的集客员工
                                    or Col20 in (select retailtraderid from retailtrader where SellUserID=@pUserId ) )";

            if (!string.IsNullOrEmpty(order_no))
            {
                sqlWhere += " and a.order_no like '%" + order_no + "%'";
            }
            //订单来源
            if (!string.IsNullOrEmpty(OrderChannelID) && OrderChannelID != "-1")
            {
                if (OrderChannelID == "19")
                {
                    sqlWhere += " and  col20 in (select retailtraderID from retailtrader  where CustomerID=@pCustomerId ) ";
                }
                else
                {
                    sqlWhere += " and a.data_from_id = '" + OrderChannelID + "'";
                }
            }

            StringBuilder sql = new StringBuilder();

            //sql.Append(" create table #tmp(unit_id nvarchar(2000))");
            //sql.Append(" insert into #tmp");
            //sql.Append(" select distinct a.unit_id  from vw_unit_level a inner join T_User_Role b");
            //sql.Append(" on a.path_unit_id like '%' +b.unit_id + '%' and (isnull(@pUserId,'') = '' or b.user_id = @pUserId) and a.customer_id = @pCustomerId");

            sql.Append(" select * from (");
            sql.Append(" select row_number() over(order by a.create_time desc) _row,");
            sql.Append("a.order_id as OrderID,a.order_no as OrderNO,isnull(a.Field8,'0') as DeliveryTypeId,a.create_time OrderDate,create_time,");
            sql.Append(" a.status_desc OrderStatusDesc,c.vipName as VipName,");
            sql.Append(" a.status OrderStatus,isnull(a.total_qty,0) TotalQty,isnull(a.total_retail,0) TotalAmount");
            sql.Append(@",ISNULL((select RetailTraderName from RetailTrader  where RetailTraderID=c.Col20),'') as RetailTraderName
,ISNULL((select user_name from T_User  where user_id=a.sales_user),'') as ServiceMan
,ISNULL((select top 1 Amount from VipAmountDetail where ObjectId=a.order_id and VipId= c.SetoffUserId and AmountSourceId in ('10','20')),0)
+   -----OBJECT_ID是订单号，vipID是获取收益的人(集客员工),防止出现脏数据才用了top 1的方式
ISNULL((select top 1 Amount from VipAmountDetail  where ObjectId=a.order_id and AmountSourceId in ('14','15') and VipId=(select SellUserID from RetailTrader where RetailTraderID=c.Col20)),0)
as CollectIncome");
            sql.Append(" from t_inout a ");
          //  sql.Append("inner join #tmp bon a.sales_unit_id = b.unit_id ");
            sql.Append(" left join vip c on a.vip_no = c.vipId");
            sql.AppendFormat(" where a.status not in( '-1' ) and field7 <> 0 and field7<>-99 ");
            //and (isnull('{0}','')='' or order_id like '%{0}%' ),, orderId
            //a.status not in( '-1' , '700','800') and
            sql.AppendFormat("  {0}", sqlWhere);
            sql.Append(" ) t where t._row>@pPageIndex*@pPageSize and t._row<=(@pPageIndex+1)*@pPageSize");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());

        }

        public string GetPayTypeByOrderId(string orderId)
        {
            var sql = string.Format("select isnull(pay_id,'') from t_inout where order_id ='{0}' ", orderId);
            var result = this.SQLHelper.ExecuteScalar(sql);

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return "";
            }
            else
            {
                return result.ToString();
            }
        }

        #region 根据订单获取发货通知参数

        public string GetTranCenterOrderId(string orderId, string customerId)
        {
            var sql = "select isnull(paymentcenter_id,0) from T_Inout where order_id = '" + orderId + "'";
            var result = this.SQLHelper.ExecuteScalar(sql.ToString());
            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return "0";
            }
            else
            {
                return result.ToString();
            }
        }

        public string GetAppKeyByAppId(string appId)
        {
            var sql = new StringBuilder();

            sql.Append(" select top(1) isnull(PayEncryptedPwd,'') from TPaymentTypeCustomerMapping ");
            sql.AppendFormat(" where AccountIdentity = '{0}' and IsDelete = 0 ", appId);
            sql.Append(" order by CreateTime desc  ");
            var result = this.SQLHelper.ExecuteScalar(sql.ToString());

            if (result == null || string.IsNullOrEmpty(result.ToString()) || result.ToString() == "")
            {
                return "0";
            }
            else
            {
                return result.ToString();
            }

        }

        #endregion

        public DataSet GetOrdersByVipID(string vipID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.create_time";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";
            var sqlCon = "";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@VipID",vipID));
            //Loggers.Debug(new DebugLogInfo()
            //{
            //    Message = parm.ToJSON()
            //});
          

            var sql = @" 
select * from t_inout where vip_no=@VipID and [status]='700'
   {4}
                 ";  //总数据的表tab[0]
            sql = sql + @"select * from ( select ROW_NUMBER()over(order by {0} {3}) _row,a.*
                                    from t_inout a 
                                 WHERE 1 = 1 and vip_no=@VipID and [status]='700'
   {4}  
                                ) t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}
";

            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), ls.ToArray());
        }
    }
}
