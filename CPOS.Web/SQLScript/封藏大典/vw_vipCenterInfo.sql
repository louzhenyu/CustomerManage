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
	,CONVERT(NVARCHAR(10), ISNULL((SELECT COUNT(*) FROM dbo.ItemKeep x WHERE x.vipid = a.VIPID and KeepStatus='1'),0)) ItemKeepCount		--�ղ�����
	,0 couponCount		--�Ż�ȯ����
	,(SELECT COUNT(*) FROM shoppingcart x WHERE x.vipid = a.vipid AND IsDelete = 0 AND Qty > 0 ) ShoppingCartCount		--���ﳵ��Ʒ����
	--,(SELECT ISNULL(COUNT(*),0) FROM dbo.MVipShow x WHERE x.VipId = a.VIPID 
	--AND x.IsDelete = 0 
	--AND x.IsCheck=1 AND IsLottery='0') LotteryCount  --���Գ齱�齱����
	,(select COUNT(*) From LEventsVipObject x where a.VIPID = x.VipId and x.IsLottery = '0' group by x.VipId) LotteryCount --���Գ齱�齱���� Jermyn20131225
	
	,CASE WHEN a.WeiXinUserId IS NULL OR a.WeiXinUserId = '' THEN 0 ELSE 1 END IsWXPush	--�Ƿ���Է���΢����Ϣ
	,CASE WHEN a.Phone IS NULL OR a.Phone = '' THEN 0 ELSE 1 END IsSMSPush					--�Ƿ���Է��Ͷ�����Ϣ
	,(SELECT ISNULL(COUNT(*),0) FROM dbo.PushUserBasic x WHERE x.UserId = a.VIPID AND x.DeviceToken IS NOT NULL AND x.DeviceToken <> '' ) IsAppPush --�Ƿ���Է���APP��Ϣ 
	FROM dbo.Vip a
	LEFT JOIN dbo.VipIntegral b
	ON(a.VIPID = b.VipID)
	WHERE a.IsDelete = '0'
	;
	



GO


