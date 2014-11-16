// JavaScript Document

var vipTemplates = {
		vipCenter: function(data){
			var imageUrl = (data.imageUrl=="") ? "images/default.png" : data.imageUrl;
		var vipString = '<div class="vipInfo"><div class="vipInfoBg"><img src="images/vipbg.png" style="width:100%; min-height:150px; min-width:100%;" ></div><div class="vipInfoCon"><div class="vipheadwrap"><div class="viphead"><img src="'+imageUrl+'" width="100" height="100" style="margin: -10px 0 0 -12px; "></div></div><div class="vipheaderConnext"><div class="vipheaderConnextTop"><span style="font-size:16px;">'+data.vipName+'</span>&nbsp;<img class="VipIcn isVipIcn" src="images/tran1.png"><br>会员号：'+data.vipCode+'</div><div class="vipheaderConnextBottom"><ul><li><span class="vipheaderConnextBottomNum">'+parseInt(data.itemKeepCount)+'</span>收藏</li><li onClick="LinkAddCustomerId(\'vipIntegral.html\')"><span class="vipheaderConnextBottomNum">'+parseInt(data.integration)+'</span>积分</li><li><span class="vipheaderConnextBottomNum">'+parseInt(data.couponCount)+'</span>优惠券</li></ul></div></div></div><div class="vipInfoBottom"></div></div>'
 		vipString += '<div class="vipActionList"><ul><li><a href="vipCart.html"><span class="VipIcn VipIcn1"></span>购物车';
		if(data.shoppingCartCount!=0){
			vipString += '<span class="tipNum">'+parseInt(data.shoppingCartCount)+'</span>';
		}
		vipString += '<em class="jtpi VipIcn"></em></a></li><li style="border-bottom:none;"><a href="vipHistory.html"><span class="VipIcn VipIcn2"></span>浏览历史<em class="jtpi VipIcn"></em></a></li></ul></div>';
		 vipString += '<div class="vipActionList"><ul><li><a href="vipNoPayment.html"><span class="VipIcn VipIcn3"></span>待付款订单<em class="jtpi VipIcn"></em></a></li><li><a href="vipHasPayment.html"><span class="VipIcn VipIcn4"></span>待发货订单<em class="jtpi VipIcn"></em></a></li><li><a href="vipWaitSend.html"><span class="VipIcn VipIcn5"></span>待收货订单<em class="jtpi VipIcn"></em></a></li><li style="border-bottom:none;"><a href="vipComplete.html"><span class="VipIcn VipIcn6"></span>已完成订单<em class="jtpi VipIcn"></em></a></li></ul></div>';
		 return vipString;
			}	
}