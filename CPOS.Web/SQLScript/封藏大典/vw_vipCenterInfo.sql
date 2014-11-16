/****** Object:  View [dbo].[vw_vipCenterInfo]    Script Date: 02/25/2014 21:19:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER view [dbo].[vw_vipCenterInfo] 
as
	SELECT a.* 
	,ISNULL(b.BeginIntegral,0) BeginIntegral
	,ISNULL(b.InIntegral,0) InIntegral
	,ISNULL(b.EndIntegral,0) EndIntegral
	,ISNULL(b.OutIntegral,0) OutIntegral
	,ISNULL(b.ValidIntegral,0) ValidIntegral
	,ISNULL(b.InvalidIntegral,0) InvalidIntegral
	,'' ImageUrl
	,CONVERT(NVARCHAR(10), ISNULL((SELECT COUNT(*) FROM dbo.ItemKeep x WHERE x.vipid = a.VIPID and KeepStatus='1'),0)) ItemKeepCount		--收藏数量
	,0 couponCount		--优惠券数量
	,(SELECT COUNT(*) FROM shoppingcart x WHERE x.vipid = a.vipid AND IsDelete = 0 AND Qty > 0 ) ShoppingCartCount		--购物车商品数量
	--,(SELECT ISNULL(COUNT(*),0) FROM dbo.MVipShow x WHERE x.VipId = a.VIPID 
	--AND x.IsDelete = 0 
	--AND x.IsCheck=1 AND IsLottery='0') LotteryCount  --可以抽奖抽奖次数
	,(select COUNT(*) From LEventsVipObject x where a.VIPID = x.VipId and x.IsLottery = '0' group by x.VipId) LotteryCount --可以抽奖抽奖次数 Jermyn20131225
	
	,CASE WHEN a.WeiXinUserId IS NULL OR a.WeiXinUserId = '' THEN 0 ELSE 1 END IsWXPush	--是否可以发送微信消息
	,CASE WHEN a.Phone IS NULL OR a.Phone = '' THEN 0 ELSE 1 END IsSMSPush					--是否可以发送短信信息
	,(SELECT ISNULL(COUNT(*),0) FROM dbo.PushUserBasic x WHERE x.UserId = a.VIPID AND x.DeviceToken IS NOT NULL AND x.DeviceToken <> '' ) IsAppPush --是否可以发送APP信息 
	FROM dbo.Vip a
	LEFT JOIN dbo.VipIntegral b
	ON(a.VIPID = b.VipID)
	WHERE a.IsDelete = '0'
	;
	



GO


