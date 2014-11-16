using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

using JIT.CPOS.BS.Entity;

using JIT.Utility.DataAccess;
using log4net;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 订单 
    /// 表Orders的数据访问类     
    /// </summary>
    public partial class TInoutDAO
    {
        #region GetOrdersList
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="pParems">条件 字典</param>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageSize">个数</param>
        /// <returns></returns>
        public PagedQueryResult<TInoutViewEntity> GetOrdersList(Dictionary<string, object> pParems, int pageIndex, int pageSize)
        {
            #region 接收参数
            string strWhere = "";
            //订单号
            if (pParems.ContainsKey("pOrdersNo"))
            {
                strWhere += string.Format(" and inout.order_no like '%{0}%'", pParems["pOrdersNo"]);
            }
            if (pParems.ContainsKey("pStartDate") && pParems.ContainsKey("pEndDate"))
            {
                strWhere += string.Format(" and (inout_detail.Field1 <= cast('{0}' as datetime) and inout_detail.Field1 >= cast('{1}' as datetime))", pParems["pStartDate"], pParems["pEndDate"]);
            }
            //门店
            if (pParems.ContainsKey("pStoreName"))
            {
                strWhere += string.Format(" and unit.unit_name like '%{0}%'", pParems["pStoreName"]);
            }
            //房型
            if (pParems.ContainsKey("pItemName"))
            {
                strWhere += string.Format(" and item.item_name like '%{0}%'", pParems["pItemName"]);
            }
            //客人姓名
            if (pParems.ContainsKey("pUserName"))
            {
                strWhere += string.Format(" and inout.field3 like '%{0}%'", pParems["pUserName"]);
            }
            //订单状态
            if (pParems.ContainsKey("pStatus"))
            {
                if (pParems["pStatus"].ToString() != "0")
                {
                    if (pParems["pStatus"].ToString() == "400")
                    {
                        strWhere += string.Format(" and inout.status in ('400','800')", pParems["pStatus"]);
                    }
                    else
                    {
                        strWhere += string.Format(" and inout.status = '{0}'", pParems["pStatus"]);
                    }

                }
            }
            #endregion

            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            /*获取订单列表*/
            select
	            ROW_NUMBER() over(order by inout.create_time desc) ID
	            ,inout.order_id OrdersID --订单ID
	            ,inout.order_no OrdersNo --订单编号
	            ,inout.order_date OrdersDate --订单日期
	            ,del.DeliveryName Payment --付款方式
                ,inout.Field11 PayType  --付款类型
	            ,inout.status OrdersStatus --订单状态
                ,opt.OptionText OrdersStatusText --订单状态文本
	            ,unit.unit_name StoreName --门店
	            ,item.item_name RoomTypeName --房型名称
	            ,inout.field14 GuestName --客人名称
                ,inout.vip_no VipID --用户ID
	            ,inout_detail.Field1 StartDate --入住日期
	            ,inout_detail.Field2 EndDate --离店日期
                ,ISNULL(inout.remark,'') Remark    --备注
	            ,cast(inout_detail.order_qty as int) RoomCount --房间数
            into #Result	
            from T_Inout inout  --订单
            inner join T_Inout_Detail inout_detail on inout.order_id=inout_detail.order_id  --订单明细
            left join t_unit unit on inout.purchase_unit_id=unit.unit_id and unit.customer_id=inout.customer_id --门店
            left join T_Sku sku on inout_detail.sku_id=sku.sku_id
            left join T_Item item on sku.item_id=item.item_id   --房型名称
            left join Delivery del on inout.Field11=del.DeliveryId and IsDelete=0  --付款方式
            inner join Options opt on inout.status=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID='{0}' and opt.IsDelete=0 --订单状态
            where inout.customer_id='{0}' and inout.status !=-1 {1}

            select
                * 
            from  #Result 
            where ID between {2} and {3}
            select count(*) from #Result
            drop table #Result", CurrentUserInfo.ClientID, strWhere, (pageSize * (pageIndex - 1)) + 1, pageSize * pageIndex);
            #endregion

            DataSet dsSource = this.SQLHelper.ExecuteDataset(strSql.ToString());
            PagedQueryResult<TInoutViewEntity> pageQuery = new PagedQueryResult<TInoutViewEntity>();
            pageQuery.Entities = ConvertHelper<TInoutViewEntity>.ConvertToList(dsSource.Tables[0]).ToArray();
            int pageCount = 0;
            int.TryParse(dsSource.Tables[1].Rows[0][0] + "", out pageCount);
            pageQuery.PageCount = pageCount;
            return pageQuery;
        }
        #endregion

        #region OrdersApprove
        /// <summary>
        /// 订单审批
        /// </summary>
        /// <param name="pParams"></param>
        public void OrdersApprove(Dictionary<string, string> pParams)
        {
            TInoutEntity orderEntity = new TInoutEntity();
            TInoutStatusEntity statusEntity = new TInoutStatusEntity();
            TInoutDetailEntity[] orderDetailEntity = null;

            #region 拼接查询SQL
            StringBuilder strSearchSql = new StringBuilder();
            strSearchSql.AppendFormat(@"
            select 
	            inout.order_id OrdersID
	            ,inout.order_no OrdersNo
                ,ISNULL(store.UsedAmount,0) UsedAmount
                ,ISNULL(store.StockAmount,0) StockAmount
                ,store.*
            from T_Inout inout
            inner join T_Inout_Detail inout_detail on inout.order_id=inout_detail.order_id
            inner join StoreItemDailyStatus store on inout_detail.sku_id=store.SkuID
            where inout.customer_id='{0}' and inout.order_id='{1}'
            and store.StatusDate between cast(inout_detail.Field1 as datetime) and convert(char(10),dateadd(dd,-1,inout_detail.Field2),120)                   
            order by store.StatusDate", CurrentUserInfo.ClientID, pParams["pOrdersID"]);
            #endregion

            DataSet dsSource = this.SQLHelper.ExecuteDataset(strSearchSql.ToString());
            StoreItemDailyStatusEntity[] storeEntity = ConvertHelper<StoreItemDailyStatusEntity>.ConvertToList(dsSource.Tables[0]).ToArray();

            SqlTransaction tran = this.SQLHelper.CreateTransaction();
            using (tran.Connection)
            {
                try
                {
                    //更新订单主表信息
                    if (pParams.ContainsKey("pOrdersID"))
                    {
                        orderDetailEntity = new TInoutDetailDAO(CurrentUserInfo).QueryByEntity(new TInoutDetailEntity { order_id = pParams["pOrdersID"] }, null);
                        orderEntity = GetByID(pParams["pOrdersID"]);
                        orderEntity.Status = pParams["pOrdersStatus"];
                        orderEntity.StatusDesc = pParams["pOrdersDesc"];
                        orderEntity.Field7 = orderEntity.Status;
                        orderEntity.Field10 = orderEntity.StatusDesc;

                        if (pParams["pRemark"] != null && pParams["pRemark"] != "")
                        {
                            orderEntity.Remark = pParams["pRemark"];
                        }
                        orderEntity.ModifyTime = DateTime.Now.ToString();
                        orderEntity.ModifyUserID = CurrentUserInfo.UserID;
                        if (orderEntity.Status == "500" || orderEntity.Status == "400")
                        {
                            T_InoutEntity[] entity = new T_InoutDAO(CurrentUserInfo).QueryByEntity(new T_InoutEntity { order_id = pParams["pOrdersID"] }, null);
                            new VipDAO(this.CurrentUserInfo).ProcSetCancelOrder(this.CurrentUserInfo.ClientID, pParams["pOrdersID"], entity[0].vip_no);
                        }
                        Update(orderEntity);
                    }

                    //更新房态 库存数量
                    if (storeEntity != null && storeEntity.Length > 0)
                    {
                        for (int i = 0; i < storeEntity.Length; i++)
                        {
                            if (pParams["pOrdersType"].ToString() == "1")
                            {
                                //1.审核通过
                                if (storeEntity[i].StockAmount != 0 && storeEntity[i].StockAmount > 0)
                                {
                                    storeEntity[i].StockAmount = storeEntity[i].StockAmount - (int)Math.Round((decimal)orderDetailEntity[0].OrderQty, 0);
                                }
                                if (storeEntity[i].UsedAmount != 100 && storeEntity[i].UsedAmount < 100)
                                {
                                    storeEntity[i].UsedAmount = storeEntity[i].UsedAmount + (int)Math.Round((decimal)orderDetailEntity[0].OrderQty, 0);
                                }
                                storeEntity[i].LastUpdateBy = CurrentUserInfo.UserID;
                                storeEntity[i].LastUpdateTime = DateTime.Now;
                            }
                            else
                            {
                                //2.审核不通过
                                if (storeEntity[i].StockAmount != 100 && storeEntity[i].StockAmount < 100)
                                {
                                    storeEntity[i].StockAmount += (int)Math.Round((decimal)orderDetailEntity[0].OrderQty, 0);
                                }
                                if (storeEntity[i].UsedAmount != 0 && storeEntity[i].UsedAmount > 0)
                                {
                                    storeEntity[i].UsedAmount -= (int)Math.Round((decimal)orderDetailEntity[0].OrderQty, 0);
                                }
                                storeEntity[i].LastUpdateBy = CurrentUserInfo.UserID;
                                storeEntity[i].LastUpdateTime = DateTime.Now;
                            }
                            new StoreItemDailyStatusDAO(CurrentUserInfo).Update(storeEntity[i]);
                        }

                    }

                    //添加订单操作流水
                    statusEntity.OrderID = pParams["pOrdersID"];
                    statusEntity.OrderStatus = int.Parse(pParams["pOrdersStatus"]);
                    if (pParams["pCheckResult"] != null && pParams["pCheckResult"] != "")
                    {
                        statusEntity.CheckResult = int.Parse(pParams["pCheckResult"]);
                    }
                    statusEntity.CustomerID = CurrentUserInfo.ClientID;
                    statusEntity.CreateBy = CurrentUserInfo.UserID;
                    new TInoutStatusDAO(CurrentUserInfo).Create(statusEntity);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo() { ClientID = CurrentUserInfo.ClientID, UserID = CurrentUserInfo.UserID, ErrorMessage = ex.Message });
                    tran.Rollback();
                }
            }
        }
        #endregion

        #region Complete
        /// <summary>
        /// 订单完成操作
        /// </summary>
        /// <param name="pOrdersID">订单ID</param>
        /// <param name="pStatus">订单状态</param>
        public void Complete(string pOrdersID, string pStatus)
        {
            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
                /*审核通过 更新订单状态*/
                update inout set status={2},modify_time=GETDATE()
                from T_Inout inout
                where order_id='{1}' and customer_id='{0}'

                /*添加订单操作流水*/
                insert into TInoutStatus(OrderID,OrderStatus,CustomerID)
                values('{1}','{2}','{0}')", CurrentUserInfo.ClientID, pOrdersID, pStatus);
            #endregion

            this.SQLHelper.ExecuteNonQuery(strSql.ToString());

        }
        #endregion

        #region GetOrdersListCount
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="pParems">条件 字典</param>
        /// <returns></returns>
        public DataSet GetOrdersListCount(Dictionary<string, string> pParems)
        {
            #region 接收参数
            string strWhere = "";
            //订单号
            if (pParems.ContainsKey("pOrdersNo"))
            {
                strWhere += string.Format(" and inout.order_no like '%{0}%'", pParems["pOrdersNo"]);
            }
            if (pParems.ContainsKey("pStartDate") && pParems.ContainsKey("pEndDate"))
            {
                strWhere += string.Format(" and (inout_detail.Field1 <= cast('{0}' as datetime) and inout_detail.Field1 >= cast('{1}' as datetime))", pParems["pStartDate"], pParems["pEndDate"]);
            }
            //门店
            if (pParems.ContainsKey("pStoreName"))
            {
                strWhere += string.Format(" and unit.unit_name like '%{0}%'", pParems["pStoreName"]);
            }
            //房型
            if (pParems.ContainsKey("pItemName"))
            {
                strWhere += string.Format(" and item.item_name like '%{0}%'", pParems["pItemName"]);
            }
            //客人姓名
            if (pParems.ContainsKey("pUserName"))
            {
                strWhere += string.Format(" and inout.field3 like '%{0}%'", pParems["pUserName"]);
            }
            #endregion

            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
                ROW_NUMBER() over(order by inout.create_time desc) ID
                ,inout.order_id OrdersID --订单ID
                ,inout.order_no OrdersNo --订单编号
                ,inout.order_date OrdersDate --订单日期
                ,del.DeliveryName Payment --付款方式
                ,inout.Field11 PayType  --付款类型
                ,inout.status OrdersStatus --订单状态
                ,opt.OptionText OrdersStatusText --订单状态文本
                ,unit.unit_name StoreName --门店
                ,item.item_name RoomTypeName --房型名称
                ,inout.field3 GuestName --客人名称
                ,inout_detail.Field1 StartDate --入住日期
                ,inout_detail.Field2 EndDate --离店日期
                ,ISNULL(inout.remark,'') Remark    --备注
                ,cast(inout_detail.order_qty as int) RoomCount --房间数
            into #tmp	
            from T_Inout inout  --订单
            inner join T_Inout_Detail inout_detail on inout.order_id=inout_detail.order_id  --订单明细
            left join t_unit unit on inout.purchase_unit_id=unit.unit_id and unit.customer_id=inout.customer_id --门店
            left join T_Sku sku on inout_detail.sku_id=sku.sku_id
            left join T_Item item on sku.item_id=item.item_id   --房型名称
            left join Delivery del on inout.Field11=del.DeliveryId and IsDelete=0  --付款方式
            left join Options opt on inout.status=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID=inout.customer_id and opt.IsDelete=0 --订单状态
            where inout.customer_id='{0}' {1}

            select
	            COUNT(1) ApproveCount
            from #tmp
            where OrdersStatus=100
            select
	            COUNT(1) CheckCount
            from #tmp
            where OrdersStatus=300
            select
	            COUNT(1) CompleteCount
            from #tmp
            where OrdersStatus=500
            select
	            COUNT(1) CancelCount
            from #tmp
            where OrdersStatus=600
            select
	            COUNT(1) NotAuditCount
            from #tmp
            where OrdersStatus=700
            select
	            COUNT(1) AllCount
            from #tmp
            where 1=1

            drop table #tmp", CurrentUserInfo.ClientID, strWhere);
            #endregion

            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        #region GetOrdersListCountFHotels
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="pParems">条件 字典</param>
        /// <returns></returns>
        public DataSet GetOrdersListCountFHotels(Dictionary<string, string> pParems)
        {
            #region 接收参数
            string strWhere = "";
            //订单号
            if (pParems.ContainsKey("pOrdersNo"))
            {
                strWhere += string.Format(" and inout.order_no like '%{0}%'", pParems["pOrdersNo"]);
            }
            if (pParems.ContainsKey("pStartDate") && pParems.ContainsKey("pEndDate"))
            {
                strWhere += string.Format(" and (inout_detail.Field1 <= cast('{0}' as datetime) and inout_detail.Field1 >= cast('{1}' as datetime))", pParems["pStartDate"], pParems["pEndDate"]);
            }
            //门店
            if (pParems.ContainsKey("pStoreName"))
            {
                strWhere += string.Format(" and unit.unit_name like '%{0}%'", pParems["pStoreName"]);
            }
            //房型
            if (pParems.ContainsKey("pItemName"))
            {
                strWhere += string.Format(" and item.item_name like '%{0}%'", pParems["pItemName"]);
            }
            //客人姓名
            if (pParems.ContainsKey("pUserName"))
            {
                strWhere += string.Format(" and inout.field3 like '%{0}%'", pParems["pUserName"]);
            }
            #endregion

            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
                ROW_NUMBER() over(order by inout.create_time desc) ID
                ,inout.order_id OrdersID --订单ID
                ,inout.order_no OrdersNo --订单编号
                ,inout.order_date OrdersDate --订单日期
                ,del.DeliveryName Payment --付款方式
                ,inout.Field11 PayType  --付款类型
                ,inout.status OrdersStatus --订单状态
                ,opt.OptionText OrdersStatusText --订单状态文本
                ,unit.unit_name StoreName --门店
                ,item.item_name RoomTypeName --房型名称
                ,inout.field3 GuestName --客人名称
                ,inout_detail.Field1 StartDate --入住日期
                ,inout_detail.Field2 EndDate --离店日期
                ,ISNULL(inout.remark,'') Remark    --备注
                ,cast(inout_detail.order_qty as int) RoomCount --房间数
            into #tmp	
            from T_Inout inout  --订单
            inner join T_Inout_Detail inout_detail on inout.order_id=inout_detail.order_id  --订单明细
            left join t_unit unit on inout.purchase_unit_id=unit.unit_id and unit.customer_id=inout.customer_id --门店
            left join T_Sku sku on inout_detail.sku_id=sku.sku_id
            left join T_Item item on sku.item_id=item.item_id   --房型名称
            left join Delivery del on inout.Field11=del.DeliveryId and IsDelete=0  --付款方式
            inner join Options opt on inout.status=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID=inout.customer_id and opt.IsDelete=0 --订单状态
            where inout.customer_id='{0}' {1}

            select
	            COUNT(1) ApproveCount
            from #tmp
            where OrdersStatus=100
            select
	            COUNT(1) CheckCount
            from #tmp
            where OrdersStatus=200
            select
	            COUNT(1) CompleteCount
            from #tmp
            where OrdersStatus=300
            select
	            COUNT(1) CancelCount
            from #tmp
            where OrdersStatus =400 or OrdersStatus =800
            select
	            COUNT(1) NotAuditCount
            from #tmp
            where OrdersStatus=500
            select
	            COUNT(1) AllCount
            from #tmp
            where 1=1

            drop table #tmp", CurrentUserInfo.ClientID, strWhere);
            #endregion

            return this.SQLHelper.ExecuteDataset(strSql.ToString());
        }
        #endregion

        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }

        public DataSet GetStoreInfo(string storeId)
        {
            List<SqlParameter> paras = new List<SqlParameter> { };
            paras.Add(new SqlParameter() { ParameterName = "@pStoreId", Value = storeId });
            StringBuilder sql = new StringBuilder();
            sql.Append("select unit_name,unit_address,unit_tel from t_unit where unit_id = @pStoreId");

            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
        }
    }
}