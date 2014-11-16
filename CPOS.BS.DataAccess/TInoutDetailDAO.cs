using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using JIT.CPOS.BS.Entity;

using JIT.Utility.DataAccess;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 数据访问： 订单明细
    /// </summary>
    public partial class TInoutDetailDAO
    {
        #region GetOrderDetail
        /// <summary>
        /// 根据订单ID获取订单明细
        /// </summary>
        /// <param name="pEntity"></param>
        /// <param name="isHotel">是否为酒店房间 1：是 0：否 </param>
        /// <returns></returns>
        public DataSet GetOrderDetail(TInoutDetailEntity pEntity, string isHotel)
        {
            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            if (isHotel == "0" || isHotel == "")
            {

                strSql.AppendFormat(@"
            /*订单详情*/
            select
             inout.order_no OrdersNo			--订单号
             ,inout.Field14 GuestName			--'客人名称'
             ,inout.vip_no VipID 
             ,VipName = (select top(1) vipName from vip where vipId = inout.vip_no and isdelete = 0 )
             ,inout.Field6 LinkTel				--'联系电话'
             ,inout_detail.Field1 StartDate		-- '住宿日期'
             ,inout_detail.Field2 EndDate		--'住宿日期'
             ,Convert(varchar(100),inout.create_time,120) CreateTime --下单时间
             ,DATEDIFF(day,inout_detail.Field1,inout_detail.Field2) QTY	 	--住宿天数
             ,unit.unit_name StoreName			--'房间名称' 
             ,item.item_name RoomTypeName --房型名称
             ,ISNULL(del.DeliveryName,'') Payment			--'付款方式'
             ,cast(inout.total_amount as int) totalamount			--'共计'
             ,cast(inout.actual_amount as int) Amount			--'应付'
             ,cast(inout_detail.order_qty as int) RoomCount --房间数
             ,opt.OptionText OrdersStatusText --订单状态文本
             ,inout.remark Remark
             ,abs(isnull(c.integral,0)) as integral --积分金额
             ,abs(isnull(f.ParValue,0)) couponAmount --优惠劵金额
             ,abs(isnull(g.amount,0)) as vipEndAmount --余额抵扣金额
            from T_Inout inout
            inner join T_Inout_Detail inout_detail on inout.order_id=inout_detail.order_id
            left join t_unit unit on inout.purchase_unit_id=unit.unit_id and unit.customer_id=inout.customer_id--房间
            left join T_Sku sku on inout_detail.sku_id=sku.sku_id
            left join T_Item item on sku.item_id=item.item_id   --房型名称
            left join Delivery del on inout.Field11=del.DeliveryId--付款方式
            left join Options opt on inout.status=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID='{0}' and opt.IsDelete=0 --订单状态
            left join vipIntegralDetail c on inout.order_id = c.objectId and inout.vip_no = c.VIPID and c.IntegralSourceID = 20
            left join TOrderCouponMapping d on inout.order_id = d.orderId 
            left join Coupon e on d.couponId = e.couponId 
            left join CouponType f on e.couponTypeId = f.couponTypeId
            left join VipAmountDetail g on inout.order_id = g.objectId and inout.vip_no = g.VipId and g.AmountSourceId =1
            where inout.order_id='{1}' and unit.customer_id='{0}'

            /*获取订单状态(这里取所有订单状态,没有订单时,前台显示 0)*/
            select
				inout.create_time OrderDate
				,ISNULL(inout_status.OrderStatus,'') OrderStatus
				,ISNULL(Convert(varchar(100),inout_status.CreateTime,120),'') OperaterTime
				,ISNULL(opt.OptionText,'') OrderStatusText
	            ,ISNULL(inout.remark,'') Remark
            from T_Inout inout
            left join TInoutStatus inout_status on inout.order_id=inout_status.OrderID and inout.customer_id=inout_status.CustomerID and inout_status.IsDelete=0
            left join Options opt on inout_status.OrderStatus=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID=inout.customer_id
            where inout.order_id='{1}' and inout.customer_id='{0}'
            order by inout_status.CreateTime", CurrentUserInfo.ClientID, pEntity.OrderID);

            }
            else
            {
                strSql.AppendFormat(@"

                 select  sum(sis.LowestPrice) as  priceNew into #tempsum  from T_Inout i left join T_Inout_Detail ind on 
                i.order_id=ind.order_id 
                left join T_Sku s on ind.sku_id=s.sku_id 
                left join StoreItemDailyStatus sis on  sis.SkuID=ind.sku_id
                where ( sis.StatusDate between ind.Field1 and DATEADD(DAY,-1,convert(date,ind.Field2)) 
                )  
                and i.order_id='{1}' and i.customer_id='{0}' ;

            /*订单详情*/
            select
             inout.order_no OrdersNo			--订单号
             ,inout.Field14 GuestName			--'客人名称'
             ,inout.vip_no VipID 
             ,VipName = (select top(1) vipName from vip where vipId = inout.vip_no and isdelete = 0 )
             ,inout.Field6 LinkTel				--'联系电话'
             ,inout_detail.Field1 StartDate		-- '住宿日期'
             ,inout_detail.Field2 EndDate		--'住宿日期'
             ,Convert(varchar(100),inout.create_time,120) CreateTime --下单时间
             ,DATEDIFF(day,inout_detail.Field1,inout_detail.Field2) QTY	 	--住宿天数
             ,unit.unit_name StoreName			--'房间名称' 
             ,item.item_name RoomTypeName --房型名称
             ,ISNULL(del.DeliveryName,'') Payment			--'付款方式'
             ,case when (select * from #tempsum) IS NULL then cast(inout.total_amount as int) else convert(decimal(18,2),(select * from #tempsum)* inout.total_qty *inout_detail.discount_rate/100) end totalamount			--'共计'
             ,case when ((select * from #tempsum)-abs(isnull(c.integral,0))-abs(isnull(f.ParValue,0))-abs(isnull(g.amount,0))) IS NULL then cast(inout.actual_amount as int) else (convert(decimal(18,2),(select * from #tempsum)* inout.total_qty * inout_detail.discount_rate/100)-abs(isnull(c.integral,0))-abs(isnull(f.ParValue,0))-abs(isnull(g.amount,0))) end  Amount 				--'应付'
             ,cast(inout_detail.order_qty as int) RoomCount --房间数
             ,opt.OptionText OrdersStatusText --订单状态文本
             ,inout.remark Remark
             ,abs(isnull(c.integral,0)) as integral --积分金额
             ,abs(isnull(f.ParValue,0)) couponAmount --优惠劵金额
             ,abs(isnull(g.amount,0)) as vipEndAmount --余额抵扣金额
            from T_Inout inout
            inner join T_Inout_Detail inout_detail on inout.order_id=inout_detail.order_id
            left join t_unit unit on inout.purchase_unit_id=unit.unit_id and unit.customer_id=inout.customer_id--房间
            left join T_Sku sku on inout_detail.sku_id=sku.sku_id
            left join T_Item item on sku.item_id=item.item_id   --房型名称
            left join Delivery del on inout.Field11=del.DeliveryId--付款方式
            left join Options opt on inout.status=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID='{0}' and opt.IsDelete=0 --订单状态
            left join vipIntegralDetail c on inout.order_id = c.objectId and inout.vip_no = c.VIPID and c.IntegralSourceID = 20
            left join TOrderCouponMapping d on inout.order_id = d.orderId 
            left join Coupon e on d.couponId = e.couponId 
            left join CouponType f on e.couponTypeId = f.couponTypeId
            left join VipAmountDetail g on inout.order_id = g.objectId and inout.vip_no = g.VipId and g.AmountSourceId =1
            where inout.order_id='{1}' and unit.customer_id='{0}'

            /*获取订单状态(这里取所有订单状态,没有订单时,前台显示 0)*/
            select
				inout.create_time OrderDate
				,ISNULL(inout_status.OrderStatus,'') OrderStatus
				,ISNULL(Convert(varchar(100),inout_status.CreateTime,120),'') OperaterTime
				,ISNULL(opt.OptionText,'') OrderStatusText
	            ,ISNULL(inout.remark,'') Remark
            from T_Inout inout
            left join TInoutStatus inout_status on inout.order_id=inout_status.OrderID and inout.customer_id=inout_status.CustomerID and inout_status.IsDelete=0
            left join Options opt on inout_status.OrderStatus=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.CustomerID=inout.customer_id
            where inout.order_id='{1}' and inout.customer_id='{0}'
            order by inout_status.CreateTime", CurrentUserInfo.ClientID, pEntity.OrderID);


            }

            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion

        #region HS_GetOrderDetail
        /// <summary>
        /// 根据订单ID获取订单明细
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public DataSet HS_GetOrderDetail(TInoutEntity pEntity)
        {
            #region 拼接SQL
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"
            select
	            inout.order_id OrdersID
	            ,inout.order_no OrderNo
	            ,inout.order_date OrderDate
	            ,inout.Field1 
	            ,inout.Field2
	            ,inout.Field3
	            ,inout.Field4
	            ,inout.Field5
	            ,inout.Field6
	            ,inout.Field7
	            ,inout.Field8
	            ,inout.Field9
	            ,opt.OptionText Field10
                ,opt1.OptionText Field11
                ,opt2.OptionText Field15
                ,inout.Field12
                ,inout.Field13
                ,inout.Field14
                ,inout.Field16
                ,inout.Field17
                ,inout.Field18
                ,inout.Field19
                ,inout.Field20
	            ,vip.VipName
            from T_Inout inout
            left join vip vip on inout.vip_no=vip.VIPID and vip.ClientID=inout.customer_id and vip.IsDelete=0
            left join Options opt on inout.Field7=opt.OptionValue and opt.OptionName='OrdersStatus' and opt.ClientID=vip.ClientID and opt.IsDelete=0
            left join Options opt1 on inout.Field11=opt1.OptionValue and opt1.OptionName='BuyWay' and opt1.ClientID=vip.ClientID and opt1.IsDelete=0
            left join Options opt2 on inout.Field15=opt2.OptionValue and opt2.OptionName='GetWay' and opt2.ClientID=vip.ClientID and opt2.IsDelete=0
            where customer_id='{0}' and inout.order_id='{1}'", CurrentUserInfo.ClientID, pEntity.OrderID);

            return this.SQLHelper.ExecuteDataset(strSql.ToString());
            #endregion
        }
        #endregion


        public DataSet GetOrderDetailImageList(string itemIdList)
        {
            //List<SqlParameter> paras = new List<SqlParameter> { };
            //paras.Add(new SqlParameter() { ParameterName = "@pItemIdList", Value = itemIdList });

            // return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), paras.ToArray());
            StringBuilder sql = new StringBuilder();
            //sql.Append("select ObjectId,imageId,imageUrl from ObjectImages where ObjectId in (@pItemIdList) and isdelete = 0");

            sql.AppendFormat("select ObjectId,imageId,imageUrl,isnull(displayIndex,0) displayIndex from ObjectImages where ObjectId in ({0}) and isdelete = 0 order by displayIndex", itemIdList);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
    }
}
