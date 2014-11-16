Jit.AM.defindPage({

	name: 'OrderPay',

	onPageLoad: function() {

		var me = this;
		//是否显示header
		var hasHeader=Jit.AM.getUrlParam("hasHeader");
		if(hasHeader==0){
			$("#topNav").hide();
			$(".goods_wrap").css("margin-top","0");
			$(".op_area").css("margin-top","0");
			
		}

		me.initEvent();
		
		me.LoadOrderInfo();
		
		TopMenuHandle.ReCartCount();
		
		var isSourceGoods = Jit.AM.getUrlParam('isGoodsPage');
		
		if (isSourceGoods) {
		
			$('#toPageBack').attr('href', "javascript:Jit.AM.toPage('GoodsList')");
		};
		
	},
	LoadOrderInfo: function() {
		var me = this,
			orderInfo;  
			
		me.ajax({
			url: '/Interface/data/OrderData.aspx',
			data: {
				'action': 'GetPaymentListBycId',
				'channelId':'2'
			},
			success: function(data) {
				
				if(data.code == 200){
				
					me.initPaymentType(data.content.paymentList);
				}
			}
		});
		
		me.ajax({
			url: '/OnlineShopping/data/Data.aspx',
			data: {
				'action': 'getOrderList',
				'orderId': me.getUrlParam('orderId'),
				'page': 1,
				'pageSize': 10000
			},
			success: function(data) {
			
				if (data.code == 200 && data.content.orderList.length > 0) {
				
					var orderInfo = data.content.orderList[0];

					me.totalAmount = orderInfo.totalAmount;
					//应付总额
					var realMoney=me.getUrlParam('realMoney');
					//$('#orderTotal').html('￥' + orderInfo.totalAmount);
					if(realMoney){
						$('#orderTotal').html('￥' + realMoney);
					}else{
						$('#orderTotal').html('￥' + orderInfo.totalAmount);
					}
				}else{
                    alert("获取订单失败");
                }
			}
		});
	},
	
	initPaymentType:function(typelist){
	
		var types = {};
		
		for(var i=0;i<typelist.length;i++){
		
			types[typelist[i]['paymentTypeId']] = true;
			
		}
		
		$('table').each(function(i,dom){
			
			if(types[$($(dom).find('i').get(0)).attr('val')]){
				
				$(dom).show();
			}
		});
		//是否隐藏货到付款支付方式
		var isHideGetToPay=Jit.AM.getUrlParam("isHideGetToPay");
		if(isHideGetToPay==1){
			$("#getToPay").hide();
		}
		
	},
	initEvent: function() {
		var me = this,
			phomeArea = $('#phoneArea');
		//绑定选择支付类型事件
		var iList = $('.op_pay_list .items');
		iList.bind('click', function() {
			var self = $(this);
			iList.find('i').removeClass('on');
			self.find('i').addClass('on');
			var payType = self.find('i').attr('val');
			if (payType == '7730ABEECF3048BE9E207D7E83C944AF') {
				phomeArea.show();
			} else {
				phomeArea.hide();
			}
		});
		//submitPay
		$('#submitPay').bind('click', function() {
			me.SubmitPay()
		});

	},
	SubmitPay: function() { //提交订单
		
		var me = this;

		if(me.hasSubmit){

			return;
		}

		me.hasSubmit = true;
		
		var payType = $('.op_pay_list .items i.on').attr('val'),
			phomeArea = $('#phoneArea'),
			phonenum = $('#phonenum');

		if (!payType) {
			Jit.UI.Dialog({
				'content': '请选择支付方式',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
			return false;
		};

		console.log(payType);
		if (parseInt(payType) == 0) { //货到付款
			Jit.UI.Dialog({
				'content': '订单已完成!',
				'type': 'Confirm',
				'LabelOk': '去逛逛',
				'LabelCancel': '我的订单',
				'CallBackOk': function() {
					me.toPage('GoodsList');
				},
				'CallBackCancel': function() {
					me.toPage('MyOrder');
				}
			});
			return;
		}else if (payType == '7730ABEECF3048BE9E207D7E83C944AF') { //银联语音支付
			console.log(phonenum.val());
			if (phonenum.val() == "") {
				Jit.UI.Dialog({
					'content': '请填写您的支付手机号码',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
						phonenum.focus();
					}
				});

				return;
			}else if (!IsMobileNumber(phonenum.val()) ) {

					Jit.UI.Dialog({
					'content': '您输入的支付手机号码格式不正确',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
						phonenum.focus();
					}
				});
					return;
			}
			 else {
				Jit.UI.Dialog({
					'content': '',
					'type': 'Confirm',
					'LabelOk': '取消',
					'LabelCancel': '支付完成',
					'CallBackOk': function() {
						Jit.UI.Dialog("CLOSE");
					},
					'CallBackCancel': function() {
						me.toPage('PaySuccess', '&orderId=' + JitPage.getUrlParam('orderId')+'&payType='+payType);
					}
				});
			}

		}

		var baseInfo=me.getBaseInfo();
		// var toUrl="http://"+location.host+"/HtmlApps/auth.html?pageName=PaySuccess&eventId="+baseInfo.eventId+"&customerId="+baseInfo.customerId+"&openId="+baseInfo.openId+"&userId="+baseInfo.userId+"&orderId"+me.getUrlParam('orderId');

		var toUrl="http://"+location.host+"/HtmlApps/html/public/shop/pay_success.html?customerId="+me.getUrlParam('customerId');
		
		me.setParams('orderId_'+baseInfo.userId,me.getUrlParam('orderId'));
		
		//"orderPay",
		var hashdata = {
			action: 'setPayOrder',
			paymentId: payType,
			orderID: me.getUrlParam('orderId'),
			returnUrl: toUrl,
			mobileNo: payType == '7730ABEECF3048BE9E207D7E83C944AF' ? phonenum.val() : "", //为实现 语音支付
			amount:me.totalAmount,
			actualAmount:me.totalAmount,
			dataFromId:2
		};
			
		me.ajax({
			url: '/Interface/data/OrderData.aspx',
			data: hashdata,
			success: function(data) {
				if (data.code == "200" && parseInt(payType) != 2) {
					
					if(payType == 'DFD3E26D5C784BBC86B075090617F44B'){
						
						var wxpackage = JSON.parse(data.content.WXPackage);
						WeixinJSBridge.invoke('getBrandWCPayRequest',wxpackage,function(res){
			               	if(res.err_msg == "get_brand_wcpay_request:ok" ) {
			               		
			               		window.location = toUrl;
			               	}else if(res.err_msg=="get_brand_wcpay_request:cancel"){
			               	}else{
			               		alert("支付失败");
			               	}
			            }); 
			             
			        }else if(payType=="257E95A658624C91AFCC8B6CE3DF8BFB"){
			        	Jit.UI.Dialog({
							'content': '订单已完成!',
							'type': 'Confirm',
							'LabelOk': '去逛逛',
							'LabelCancel': '我的订单',
							'CallBackOk': function() {
								me.toPage('GoodsList');
							},
							'CallBackCancel': function() {
								me.toPage('MyOrder');
							}
						});
			        	
			        }else{
			        	window.location = data.content.PayUrl;
			        }
			        me.hasSubmit=false;
				}else{
                    alert(data.description);
                }
			}
		});


	}

});