/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/21 14:59:53
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
    /// 表T_CTW_LEvent的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_CTW_LEventDAO : Base.BaseCPOSDAO, ICRUDable<T_CTW_LEventEntity>, IQueryable<T_CTW_LEventEntity>
    {
        /// <summary>
        /// 根据商户CTWEventId获取信息
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetLeventInfoByCTWEventId(string strCTWEventId)
        {

            string strSql = "SELECT a.TemplateId,a.[CTWEventId],a.name,a.[ActivityGroupId],a.InteractionType,a.ImageURL,a.OnlineQRCodeId,a.OfflineQRCodeId,a.Status,b.OriginalThemeId,b.[ThemeId],b.H5Url,b.H5TemplateId";
            strSql += ",c.InteractionType,c.DrawMethodCode,c.LeventId,d.ActivityGroupCode ";
            strSql += ",e.ImageUrl QRCodeImageUrlForOnline,f.ImageUrl QRCodeImageUrlForUnit,b.worksId ";
            strSql += " FROM [dbo].[T_CTW_LEvent]a	LEFT JOIN [dbo].[T_CTW_LEventTheme]b ON a.[CTWEventId]=b.[CTWEventId] AND b.isdelete=0 ";
            strSql += " LEFT JOIN  T_CTW_LEventInteraction c ON a.[CTWEventId] = c.[CTWEventId] AND c.isdelete = 0 ";
            strSql += " LEFT JOIN dbo.SysMarketingGroupType d ON a.ActivityGroupId=d.ActivityGroupId ";
            strSql += " LEFT JOIN dbo.WQRCodeManager e ON a.OnlineQRCodeId=e.ObjectId ";
            strSql += " LEFT JOIN dbo.WQRCodeManager f ON a.OfflineQRCodeId=e.ObjectId ";
            strSql += string.Format(" WHERE a.isdelete=0 AND  a.[CTWEventId]='{0}'", strCTWEventId);
            return SQLHelper.ExecuteDataset(strSql);
        }
        public DataSet GetMaterialTextInfo(string strOnlineQRCodeId)
        {
            string sql = @"   	    select d.*,b.ReplyId,c.MappingId from WQRCodeManager  a
	                    inner join WKeywordReply b on CONVERT(varchar(50), a.QRCodeId)=b.Keyword
	                    inner join  WMenuMTextMapping c on b.ReplyId=c.MenuId
	                    inner join WMaterialText d on c.TextId=d.TextId
	                    where a.QRCodeId = '" + strOnlineQRCodeId + "'";
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        public DataSet GetLeventInfo(string strStatus, string strActivityGroupId, string strEventName)
        {
            string sql = string.Format(@" SELECT  e.CTWEventId ,
                                            e.Name ,
                                            e.ImageURL ,
                                            e.Status ,
                                            CASE WHEN Status = 10 THEN '待发布'
                                                 WHEN Status = 20 THEN '运行中'
                                                 WHEN Status = 30 THEN '暂停'
                                                 WHEN Status = 40 THEN '结束'
                                            END StatusName ,
                                            i.ThemeId ,
                                            i.DrawMethodCode ,
                                            i.LeventId ,
                                            wqrOffLine.ImageUrl QRCodeImageUrlForUnit,
                                            wqrOnLine.ImageUrl QRCodeImageUrlForOnline,
                                            s.ActivityGroupCode,
                                            i.InteractionType,
                                            e.StartDate,
                                            e.EndDate,
                                            e.ActivityGroupId,
                                            e.TemplateId,
                                            e.OfflineRedirectUrl,
                                            e.OnlineRedirectUrl,
                                            e.OnlineQRCodeId
                                    FROM    T_CTW_LEvent e
                                            INNER JOIN T_CTW_LEventTheme t ON e.CTWEventId = t.CTWEventId
                                            INNER JOIN T_CTW_LEventInteraction i ON e.CTWEventId = i.CTWEventId
                                            LEFT JOIN dbo.ObjectImages wqrOffLine ON e.OfflineQRCodeId = wqrOffLine.ImageId
                                            LEFT JOIN WQRCodeManager wqrOnLine ON e.OnlineQRCodeId= wqrOnLine.QRCodeId
                                            INNER JOIN SysMarketingGroupType S on e.ActivityGroupId=s.ActivityGroupId
                                    WHERE e.IsDelete=0  AND E.CustomerId='{0}' 
                                        AND (e.Status='{1}' or '{1}'='') 
                                        AND (S.ActivityGroupCode='{2}' or '{2}'='')
                                        AND (e.Name LIKE '%{3}%' or '{3}'='')  order by e.createtime desc", CurrentUserInfo.ClientID, strStatus, strActivityGroupId, strEventName);
            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intType"></param>
        /// <param name="strEventid"></param>
        /// <returns></returns>
        public DataSet GetEventInfoByLEventId(int intType, string strEventid)
        {
            string strSql = string.Empty;
            if (intType == 1)
            {
                strSql = string.Format("SELECT BeginTime ,EndTime ,EventID,Title  FROM dbo.LEvents WHERE IsDelete=0 AND IsCTW=1 AND EventID='{0}'AND  CustomerId='{1}'  ", strEventid, CurrentUserInfo.ClientID);
            }
            if (intType == 2)
            {
                strSql = string.Format("SELECT BeginTime ,EndTime ,EventID,EventName Title  FROM dbo.PanicbuyingEvent WHERE IsDelete=0 AND IsCTW=1 AND EventID='{0}'AND  CustomerId='{1}'  ", strEventid, CurrentUserInfo.ClientID);
            }
            return this.SQLHelper.ExecuteDataset(strSql);
        }
        /// <summary>
        /// 统计各个营销类型活动数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventStatusCount(string strCustomerId)
        {
            string strSql = string.Format(@"SELECT *
                                            FROM 
                                            (
                                            SELECT  s.Name ,
                                                    s.ActivityGroupCode,
                                               
                                                    CASE WHEN e.Status = 10 THEN 'Prepare'
                                                         WHEN e.Status = 20 THEN 'Running'
                                                         WHEN e.Status = 30 THEN 'Pause'
                                                         WHEN e.Status = 40 THEN 'End'
                                                    END StatusName ,
                                                    COUNT(1) TotalCount
                                            FROM    dbo.T_CTW_LEvent e
                                                    INNER JOIN dbo.SysMarketingGroupType s ON e.ActivityGroupId = s.ActivityGroupId
                                            where e.customerid='{0}'
                                            GROUP BY s.Name ,
                                                    s.ActivityGroupCode,e.Status
		                                            ) a PIVOT(max(TotalCount) FOR StatusName IN(Prepare,Running,[Pause],[End])) ab", strCustomerId);
            return this.SQLHelper.ExecuteDataset(strSql);
        }

        public DataSet GetT_CTW_LEventList(string EventName,
        string BeginTime,
                string EndTime,
            string EventStatus, string ActivityGroupId,
                int PageSize,
             int PageIndex,
        string customerid)
        {
            //if (string.IsNullOrEmpty(OrderBy))
            string OrderBy = "temp.CreateTime";
            //  if (string.IsNullOrEmpty(sortType))
            string sortType = "DESC";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", customerid));
            string sqlWhere = "";
            string strSql = "";
            string strColumn = "";
            if (!string.IsNullOrEmpty(EventName))
            {
                ls.Add(new SqlParameter("@EventName", EventName));
                sqlWhere += " and temp.Name=@EventName";
            }
            if (!string.IsNullOrEmpty(EventStatus) && EventStatus!="-1")
            {
                ls.Add(new SqlParameter("@EventStatus", EventStatus));
                sqlWhere += " and temp.status=@EventStatus";
            }
            if (!string.IsNullOrEmpty(ActivityGroupId) )
            {
                ls.Add(new SqlParameter("@ActivityGroupId", ActivityGroupId));
                sqlWhere += " and temp.ActivityGroupId=@ActivityGroupId";
            }
            if (!string.IsNullOrEmpty(BeginTime))//已核销
            {
                ls.Add(new SqlParameter("@BeginTime", BeginTime));
                sqlWhere += " and temp.StartDate>=@BeginTime";
              // strColumn = " CONVERT(VARCHAR(50), f.CreateTime, 23) AS UseTime ,";
              //  strSql += " INNER JOIN CouponUse f ON a.CouponID = f.CouponID  AND f.UnitID = @RetailTraderID"; //内链接的****
            }
            if (!string.IsNullOrEmpty(EndTime))//已核销
            {
                ls.Add(new SqlParameter("@EndDate", EndTime));
                sqlWhere += " and temp.EndDate<=@EndDate";
                // strColumn = " CONVERT(VARCHAR(50), f.CreateTime, 23) AS UseTime ,";
                //  strSql += " INNER JOIN CouponUse f ON a.CouponID = f.CouponID  AND f.UnitID = @RetailTraderID"; //内链接的****
            }
          
            //总数据表
            string sql = @"  SELECT Count(1) TotalCount
                        FROM     T_CTW_LEvent temp
                            inner join T_CTW_LEventInteraction  b on temp.CTWEventId=b.CTWEventId
                             WHERE   1 = 1 and  temp.CustomerID=@CustomerId
                               {4}
                  ";

            /**
                               a.*, 
      b.LeventId,----游戏或促销活动Id
	---  b.InteractionType  ,----- 1.吸粉：游戏  --2.促销：团购，抢购
 (case   a.InteractionType when 1 then (select BeginTime from Levents  c where c.eventid=b.LeventId  ) when 2 then  (select BeginTime from PanicbuyingEvent   d where d.eventid=b.LeventId  )  end) EventBeginTime,---开始时间

     (case   a.InteractionType when 1 then (select EndTime from Levents  c where c.eventid=b.LeventId  ) when 2 then  (select EndTime from PanicbuyingEvent   d where d.eventid=b.LeventId  )  end) EventEndTime,---结束时间
          ---奖品发送
	 (case  a.InteractionType when 1  then (	select isnull(count(1),0)   from lprizewinner x inner join LPrizes  y    on x.PrizeID=y.PrizesID where y.EventId=  b.LeventId ) else 0 end ) PrizeGet,
	
		   ----活动销售（如果是游戏，则取由优惠券导致的金额，如果是团购抢购，则取团购抢购的金额）
  	(case  a.InteractionType when 1 then   ---游戏的
	                  (select     isnull(sum(actual_amount) ,0) from 
											vipcouponmapping x
											 inner join TOrderCouponMapping y   on x.couponid=y.couponid
												inner join t_inout  z on  y.OrderId=z.order_id
												where x.objectid=b.LeventId
											) 
					   else 
						 (select isnull(sum(actual_amount),0) from PanicbuyingEventOrderMapping x inner join  t_inout  z on x.OrderId=z.order_id)
				   
		 end)
					 as EventSales  ,
	(select count(1) from  T_LEventsRegVipLog x where objectid=a.CTWEventId and (RegVipId is not null and RegVipId!='')  )  NewVip, ---新增会员（注册的）
	 (select count(1) from  T_LEventsRegVipLog x where objectid=a.CTWEventId and (FocusVipId is not null and FocusVipId!='')  ) NewAtten  ---新增粉丝（关注的）
                    {6}
                  
                  FROM     T_CTW_LEvent a
  inner join T_CTW_LEventInteraction  b on a.CTWEventId=b.CTWEventId

             * 
             * ***/
            

            //取到某一页的
            //优化后的***
            sql += @"SELECT * FROM (  
                    SELECT  ROW_NUMBER()over(order by {0} {3}) _row,*  ";
            sql += @"
from (
      select
                     a.*,
       b.LeventId, ----游戏或促销活动Id
       ----- 1.吸粉：游戏  --2.促销：团购，抢购
         c.BeginTime as   EventBeginTime, ---开始时间
        c.EndTime                            
        EventEndTime, ---结束时间
       ---奖品发送
       t2.PrizeGet  PrizeGet,
       ----活动销售（如果是游戏，则取由优惠券导致的金额，如果是团购抢购，则取团购抢购的金额）
      t3.EventSales  as EventSales,
       t.NewVip, ---新增会员（注册的）
       t.NewAtten ---新增粉丝（关注的）
  from T_CTW_LEvent a
  inner join T_CTW_LEventInteraction b on a.CTWEventId = b.CTWEventId 
			  and a.interactiontype=1 
			  and b.interactiontype=1
  inner  join Levents c on b.LeventId = c.eventid    ----游戏的
   left join (
				select LPrizes.EventId,count(1) PrizeGet
                  from lprizewinner  WITH (NOLOCK)  
                  inner join LPrizes WITH (NOLOCK)  on lprizewinner.PrizeID = LPrizes.PrizesID
			       group by LPrizes.EventId
				   )  t2 on t2.EventId = b.LeventId
       left  join (select vipcouponmapping.objectid,isnull(sum(actual_amount), 0) EventSales
               from vipcouponmapping WITH (NOLOCK)
              inner join TOrderCouponMapping WITH (NOLOCK) on vipcouponmapping.couponid = TOrderCouponMapping.couponid
              inner join t_inout  WITH (NOLOCK) on TOrderCouponMapping.OrderId =t_inout.order_id
              group by vipcouponmapping.objectid
			) t3 on t3.objectid = b.LeventId
    left join (select T_LEventsRegVipLog.objectid,
                    count(case when RegVipId is not null and RegVipId != '' then 1 end) NewVip,
                    count(case when FocusVipId is not null and FocusVipId != '' then 1 end) NewAtten
               from T_LEventsRegVipLog WITH (NOLOCK)
              group by T_LEventsRegVipLog.objectid
            ) t on a.CTWEventId = t.objectid

union all

    select a.*,
        b.LeventId, ----游戏或促销活动Id
       ----- 1.吸粉：游戏  --2.促销：团购，抢购
          d.BeginTime  EventBeginTime, ---开始时间
       d.EndTime   EventEndTime, ---结束时间
       ---奖品发送
         0  PrizeGet,
       ----活动销售（如果是游戏，则取由优惠券导致的金额，如果是团购抢购，则取团购抢购的金额）
        t4.EventSales   as EventSales,
       t.NewVip, ---新增会员（注册的）
       t.NewAtten ---新增粉丝（关注的）
  from T_CTW_LEvent a
  inner join T_CTW_LEventInteraction b on   (a.CTWEventId = b.CTWEventId and a.interactiontype=2 and b.interactiontype=2)
  left join PanicbuyingEvent  d on b.LeventId = d.eventid  ---促销
  left join (select   eventid,isnull(sum(actual_amount), 0) EventSales
               from PanicbuyingEventOrderMapping WITH (NOLOCK)
              inner join t_inout WITH (NOLOCK) on PanicbuyingEventOrderMapping.OrderId = t_inout.order_id
			   group by eventid
            ) t4 on   t4.eventid=b.LeventId
  left join (select T_LEventsRegVipLog.objectid,
                    count(case when RegVipId is not null and RegVipId != '' then 1 end) NewVip,
                    count(case when FocusVipId is not null and FocusVipId != '' then 1 end) NewAtten
               from T_LEventsRegVipLog WITH (NOLOCK)
              group by T_LEventsRegVipLog.objectid
            ) t on a.CTWEventId = t.objectid

) temp



                        
                  WHERE     1 = 1 and  temp.CustomerID=@CustomerId
                            {4}
                ";
            sql += @") tout
                  where tout._row>{1}*{2} and tout._row<=({1}+1)*{2}";

            sql = string.Format(sql, OrderBy, PageIndex - 1, PageSize, sortType, sqlWhere, strSql, strColumn);
      
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数
            return ds;



        }


        public DataSet GetEventPrizeList(string LeventId,
            int PageSize,
         int PageIndex,
    string customerid)
        {

            //if (string.IsNullOrEmpty(OrderBy))
            string OrderBy = "a.CreateTime";
            //  if (string.IsNullOrEmpty(sortType))
            string sortType = "DESC";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", customerid));
            string sqlWhere = "";
            string strSql = "";
            string strColumn = "";
            if (!string.IsNullOrEmpty(LeventId))
            {
                ls.Add(new SqlParameter("@LeventId", LeventId));
                sqlWhere += " and a.EventId=@LeventId";
            }        
            //if (!string.IsNullOrEmpty(BeginTime))//已核销
            //{
            //    ls.Add(new SqlParameter("@BeginTime", EventName));
            //    //strColumn = " CONVERT(VARCHAR(50), f.CreateTime, 23) AS UseTime ,";
            //    strSql += " INNER JOIN CouponUse f ON a.CouponID = f.CouponID  AND f.UnitID = @RetailTraderID"; //内链接的****
            //}

            //总数据表
            string sql = @"  SELECT Count(1) TotalCount                   
                                from LPrizes   a
                                WHERE   1 = 1                        
                               {4}
                  ";
            //取到某一页的
            sql += @"SELECT * FROM (  
                    SELECT  ROW_NUMBER()over(order by {0} {3}) _row,
               
a.*,
     (select  isnull( count(1),0) from lprizewinner x  where x.PrizeID=a.PrizesID ) as winnerCount  ,---中奖人数，已发放数
	 a.CountTotal-(select  isnull( count(1),0) from lprizewinner x  where x.PrizeID=a.PrizesID ) as RemindCount  ,---剩余数量
	(select count(1) from coupon x 
	                       inner join PrizeCouponTypeMapping y on x.coupontypeid=y.coupontypeid
						   inner join vipcouponmapping z on x.couponid=z.couponid 
						      where y.PrizesID=a.PrizesID    and z.objectid=a.EventId  and x.status=1   )  as UsedCount  ,  ----已经使用的
		(select count(1) from coupon x 
	                       inner join PrizeCouponTypeMapping y on x.coupontypeid=y.coupontypeid
						   inner join vipcouponmapping z on x.couponid=z.couponid 
						      where y.PrizesID=a.PrizesID    and z.objectid=a.EventId  and x.status=0  ) as NotUsedCount ,----已发放未使用
 ---代动销量
       (select     isnull(sum(actual_amount) ,0) from vipcouponmapping x   ---如果是游戏活动，则objectid是游戏活动的id
											 inner join TOrderCouponMapping y   on x.couponid=y.couponid
												inner join t_inout  z on  y.OrderId=z.order_id
                                               inner join coupon  on coupon.couponid=x.couponid
												inner join PrizeCouponTypeMapping PrizeCou on coupon.coupontypeid=PrizeCou.coupontypeid    ---是这个奖品对应的
												where x.objectid=a.EventId  and PrizeCou.PrizesID=a.PrizesID
											)  as  prizeSales,
  isnull((select top 1 coupontypeid  from prizecoupontypemapping where prizesid=a.prizesid),'') CouponTypeID 
from LPrizes   a

                            {5}
                  WHERE     1 = 1  
                            {4}
                ";
            sql += @") t
                  where t._row>{1}*{2} and t._row<=({1}+1)*{2}";

            sql = string.Format(sql, OrderBy, PageIndex - 1, PageSize, sortType, sqlWhere, strSql, strColumn);

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数
            return ds;
        }






        public DataSet GetEventPrizeDetailList(string LeventId,
            int PageSize,
         int PageIndex,
    string customerid)
        {

            //if (string.IsNullOrEmpty(OrderBy))
            string OrderBy = "a.CreateTime";
            //  if (string.IsNullOrEmpty(sortType))
            string sortType = "DESC";
            List<SqlParameter> ls = new List<SqlParameter>();
          //  ls.Add(new SqlParameter("@CustomerId", customerid));
            string sqlWhere = "";
            string strSql = "";
            string strColumn = "";
            if (!string.IsNullOrEmpty(LeventId))
            {
                ls.Add(new SqlParameter("@LeventId", LeventId));
                sqlWhere += " and a.EventId=@LeventId";
            }        
        

            //总数据表
            string sql = @"  SELECT Count(1) TotalCount                   
                                from    LPrizes a  ,PrizeCouponTypeMapping c ,vipcouponmapping d  ,coupon e ,Vip 
where  c.prizesid=a.prizesid   and     a.eventid= d.objectid  and e.coupontypeid=  c.coupontypeid and d.couponid=e.couponid    and vip.vipid=d.VIPID
                                                  
                               {4}
                  ";
            //取到某一页的
            sql += @"SELECT * FROM (  
                    SELECT  ROW_NUMBER()over(order by {0} {3}) _row,

 a.*,d.CreateTime as winTime,d.VipID,
 vipname,viprealname , 
 e.status as PrizeUsed ,  ---获取券是否被使用,0未使用，1已经使用
  (case  vip.vipsourceid when 3  then (case when vip.status>=1 then 1 else 0 end  )    else 0 end)  as subscribe     -----0代表未关注
 
               
 from  LPrizes a  ,PrizeCouponTypeMapping c ,vipcouponmapping d  ,coupon e ,Vip    

                            {5}
                  WHERE     c.prizesid=a.prizesid   and     a.eventid= d.objectid  and e.coupontypeid=  c.coupontypeid and d.couponid=e.couponid    and vip.vipid=d.VIPID
                            {4}
                ";
            sql += @") t
                  where t._row>{1}*{2} and t._row<=({1}+1)*{2}";

            sql = string.Format(sql, OrderBy, PageIndex - 1, PageSize, sortType, sqlWhere, strSql, strColumn);

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数
            return ds;
        }
        /// <summary>
        /// 带游戏的创意仓库活动统计
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetCTW_LEventStats(string strCTWEventId)
        {
            
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CTWEventId", strCTWEventId));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_CTW_LEventStats", ls.ToArray());    
            return ds;
        }
        /// <summary>
        /// 带促销的创意仓库订单排行统计
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetCTW_PanicbuyingEventRankingStats(string strCTWEventId)
        {

            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CTWEventId", strCTWEventId));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_CTW_PanicbuyingEventRankingStats", ls.ToArray());
            return ds;
        }




        public DataSet GeEventItemList(string LeventId,
            int PageSize,
         int PageIndex,
    string customerid)
        {

            //if (string.IsNullOrEmpty(OrderBy))
            string OrderBy = "ps.CreateTime";
            //  if (string.IsNullOrEmpty(sortType))
            string sortType = "DESC";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", customerid));
            string sqlWhere = "";
            string strSql = "";
            string strColumn = "";
            if (!string.IsNullOrEmpty(LeventId))
            {
                ls.Add(new SqlParameter("@LeventId", LeventId));
                sqlWhere += " and ps.EventId=@LeventId";
            }        
            //if (!string.IsNullOrEmpty(BeginTime))//已核销
            //{
            //    ls.Add(new SqlParameter("@BeginTime", EventName));
            //    //strColumn = " CONVERT(VARCHAR(50), f.CreateTime, 23) AS UseTime ,";
            //    strSql += " INNER JOIN CouponUse f ON a.CouponID = f.CouponID  AND f.UnitID = @RetailTraderID"; //内链接的****
            //}

            //总数据表
            string sql = @"  SELECT Count(1) TotalCount                   
                                 from PanicbuyingEventSkuMapping as pe 							
							inner join   PanicbuyingEventItemMapping ps  on ps.EventItemMappingId=pe.EventItemMappingId and ps.IsDelete=0  
						    inner join  	vw_sku  as vw on pe.SkuId=vw.sku_id                          
                            inner join vw_sku_price vp on vp.sku_id=vw.sku_id and  vp.item_price_type_id='77850286E3F24CD2AC84F80BC625859D'
            
                                WHERE   1 = 1  and   vw.status='1'    and pe.IsDelete=0                    
                               {4}
                  ";
            //取到某一页的
            sql += @"SELECT * FROM (  
                    SELECT  ROW_NUMBER()over(order by {0} {3}) _row,
               
     pe.createtime,vw.item_id
                            MappingId,vw.sku_id  as SkuID 
                            ,isnull(vp.price,0)price 
							---,ps.price as skuPrice  --这个不准
							,ISNULL(pe.SalesPrice,0)SalesPrice
                            ,(CASE WHEN LEN(vw.prop_1_detail_name) > 0
                                   THEN vw.prop_1_detail_name
                                        + CASE WHEN LEN(vw.prop_2_detail_name) > 0
                                               THEN ',' + vw.prop_2_detail_name
                                               ELSE ''
                                          END + CASE WHEN LEN(vw.prop_3_detail_name) > 0
                                                     THEN ',' + vw.prop_3_detail_name
                                                     ELSE ''
                                                END
                                        + CASE WHEN LEN(vw.prop_4_detail_name) > 0
                                               THEN ',' + vw.prop_4_detail_name
                                               ELSE ''
                                          END + CASE WHEN LEN(vw.prop_5_detail_name) > 0
                                                     THEN ',' + vw.prop_5_detail_name
                                                     ELSE ''
                                                END
                                   ELSE ''
                              END ) SkuName
							    ,vw.item_name
                            ,ISNULL(pe.Qty,0) Qty,ISNULL(pe.KeepQty,0) KeepQty   ---已售数量基数
							,ISNULL(pe.SoldQty,0) SoldQty, ---已经销售数量
                            (ISNULL(pe.Qty,0)-ISNULL(pe.KeepQty,0)-ISNULL(pe.SoldQty,0)) InverTory,  ----剩余数量

							ISNULL(pe.SoldQty,0)*ISNULL(pe.SalesPrice,0)  TotalSales,
								ISNULL(pe.SoldQty,0)/ISNULL(pe.Qty,1)  TurnoverRate,
                            (case when MappingId is null or CONVERT(NVARCHAR(50),MappingId)='' then 'false' else 'true' end)IsSelected
                            from PanicbuyingEventSkuMapping as pe 							
							inner join   PanicbuyingEventItemMapping ps  on ps.EventItemMappingId=pe.EventItemMappingId and ps.IsDelete=0  
						    inner join  	vw_sku  as vw on pe.SkuId=vw.sku_id                       
                            inner join vw_sku_price vp on vp.sku_id=vw.sku_id and  vp.item_price_type_id='77850286E3F24CD2AC84F80BC625859D'
            
                            {5}
                  WHERE     1 = 1  and vw.status='1'   and pe.IsDelete=0     
                            {4}
                ";
            sql += @") t
                  where t._row>{1}*{2} and t._row<=({1}+1)*{2}";

            sql = string.Format(sql, OrderBy, PageIndex - 1, PageSize, sortType, sqlWhere, strSql, strColumn);

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数
            return ds;
        }




        public DataSet GeEventItemDetailList(string LeventId, int PageSize,int PageIndex,string customerid)
        {

            //if (string.IsNullOrEmpty(OrderBy))
            string OrderBy = "ps.CreateTime";
            //  if (string.IsNullOrEmpty(sortType))
            string sortType = "DESC";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", customerid));
            string sqlWhere = "";
            string strSql = "";
            string strColumn = "";
            if (!string.IsNullOrEmpty(LeventId))
            {
                ls.Add(new SqlParameter("@LeventId", LeventId));
                sqlWhere += " and ps.EventId=@LeventId";
            }        
            //if (!string.IsNullOrEmpty(BeginTime))//已核销
            //{
            //    ls.Add(new SqlParameter("@BeginTime", EventName));
            //    //strColumn = " CONVERT(VARCHAR(50), f.CreateTime, 23) AS UseTime ,";
            //    strSql += " INNER JOIN CouponUse f ON a.CouponID = f.CouponID  AND f.UnitID = @RetailTraderID"; //内链接的****
            //}

            //总数据表
            string sql = @"  SELECT Count(1) TotalCount                   
                                    from PanicbuyingEventSkuMapping as pe 							
							inner join   PanicbuyingEventItemMapping ps  on ps.EventItemMappingId=pe.EventItemMappingId and ps.IsDelete=0  
						    inner join  	vw_sku  as vw on pe.SkuId=vw.sku_id                       
                            inner join vw_sku_price vp on vp.sku_id=vw.sku_id and  vp.item_price_type_id='77850286E3F24CD2AC84F80BC625859D'
                             inner join PanicbuyingEventOrderMapping pOrder  on  pOrder.eventid=ps.EventId 							
							 inner join  t_inout_detail on (t_inout_detail.order_id=pOrder.orderid and t_inout_detail.sku_id=pe.skuid)
						 inner join t_inout  on t_inout.order_id=t_inout_detail.order_id --订单和订单详情数据关联
						 inner join vip on vip.vipid=t_inout.vip_no
						 inner join Delivery on t_inout.field8=Delivery.DeliveryId

                                WHERE   1 = 1   and  vw.status='1'       and pe.IsDelete=0                    
                               {4}
                  ";
            //取到某一页的
            sql += @"SELECT * FROM (  
                    SELECT  ROW_NUMBER()over(order by {0} {3}) _row,
               
     pe.createtime,
                            pe.MappingId,vw.sku_id  as SkuID 
                            ,isnull(vp.price,0)price 
							--,ps.price as skuPrice
							,ISNULL(pe.SalesPrice,0)SalesPrice
                            ,(CASE WHEN LEN(vw.prop_1_detail_name) > 0
                                   THEN vw.prop_1_detail_name
                                        + CASE WHEN LEN(vw.prop_2_detail_name) > 0
                                               THEN ',' + vw.prop_2_detail_name
                                               ELSE ''
                                          END + CASE WHEN LEN(vw.prop_3_detail_name) > 0
                                                     THEN ',' + vw.prop_3_detail_name
                                                     ELSE ''
                                                END
                                        + CASE WHEN LEN(vw.prop_4_detail_name) > 0
                                               THEN ',' + vw.prop_4_detail_name
                                               ELSE ''
                                          END + CASE WHEN LEN(vw.prop_5_detail_name) > 0
                                                     THEN ',' + vw.prop_5_detail_name
                                                     ELSE ''
                                                END
                                   ELSE ''
                              END ) SkuName
							    ,vw.item_name
                            ,ISNULL(pe.Qty,0) Qty,ISNULL(pe.KeepQty,0) KeepQty,ISNULL(pe.SoldQty,0) SoldQty,
                            (ISNULL(pe.Qty,0)-ISNULL(pe.KeepQty,0)-ISNULL(pe.SoldQty,0)) InverTory,
                            (case when pe.MappingId is null or CONVERT(NVARCHAR(50),pe.MappingId)='' then 'false' else 'true' end)IsSelected
                          ,t_inout.order_no
						 ,t_inout.create_time
						 ,vip.vipname
						  ,vip.viprealname
						  ,Delivery.DeliveryName                      


                             from PanicbuyingEventSkuMapping as pe 							
							inner join   PanicbuyingEventItemMapping ps  on ps.EventItemMappingId=pe.EventItemMappingId and ps.IsDelete=0  
						    inner join  	vw_sku  as vw on pe.SkuId=vw.sku_id                           
                            inner join vw_sku_price vp on vp.sku_id=vw.sku_id and  vp.item_price_type_id='77850286E3F24CD2AC84F80BC625859D'
                             inner join PanicbuyingEventOrderMapping pOrder  on  pOrder.eventid=ps.EventId 							
							 inner join  t_inout_detail on (t_inout_detail.order_id=pOrder.orderid and t_inout_detail.sku_id=pe.skuid)
						 inner join t_inout  on t_inout.order_id=t_inout_detail.order_id --订单和订单详情数据关联
						 inner join vip on vip.vipid=t_inout.vip_no
						 inner join Delivery on t_inout.field8=Delivery.DeliveryId

                            {5}
                  WHERE     1 = 1  and vw.status='1'  and pe.IsDelete=0 
                            {4}
                ";
            sql += @") t
                  where t._row>{1}*{2} and t._row<=({1}+1)*{2}";

            sql = string.Format(sql, OrderBy, PageIndex - 1, PageSize, sortType, sqlWhere, strSql, strColumn);

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //计算总行数
            return ds;
        }
        /// <summary>
        /// 带促销活动的创意仓库的统计
        /// </summary>
        /// <param name="cTwEventId"></param>
        /// <returns></returns>
        public DataSet GetPanicbuyingEventStats(string cTwEventId)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CTWEventId", cTwEventId));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_CTW_PanicbuyingEventStats", ls.ToArray());
            return ds;
        }
        /// <summary>
        /// 游戏与促销会员增长排行
        /// </summary>
        /// <param name="cTwEventId"></param>
        /// <returns></returns>
        public DataSet GetVipAddRankingStats(string cTwEventId)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CTWEventId", cTwEventId));

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_CTW_VipAddRankingStats", ls.ToArray());
            return ds;
        }
    }
}
