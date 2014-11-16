Jit.AM.defindPage({

	name: 'AccountPayment',
	inOrderId: '',
	inItemId: '',
	totalAmount:'',
	elements: {},
	onPageLoad: function() {

		var me = this;

		me.LoadOrderInfo();
		me.initEvent();

	},
	LoadOrderInfo: function() {
		var me = this,
			orderInfo;
		me.elements.txtPayExplain = $('#payExplain');
		me.elements.txtPayItemtime = $('.apa_time');

		me.ajax({
			url: '/Interface/data/OrderData.aspx',
			data: {
				'action': 'GetPaymentListBycId'
			},
			success: function(data) {
				if (data.code == 200) {
					me.initPaymentType(data.content.paymentList);
				}
			}
		});

		me.ajax({
			url: '/Project/CEIBS/CEIBSHandler.ashx',
			data: {
				'action': 'GetVipPayMent'
			},
			success: function(data) {

				if (data.code == 200 && data.content) {
					var payInfo = data.content;
					me.inItemId = payInfo.itemId;
					me.elements.txtPayExplain.html(payInfo.itemName);
					me.elements.txtPayItemtime.html(payInfo.beginTime + '~' + payInfo.endTime);
					me.totalAmount=payInfo.itemPrice;
					$('#orderTotal').html('￥' + payInfo.itemPrice);
				}
			}
		});
	},

	initPaymentType: function(typelist) {

		var types = {};

		for (var i = 0; i < typelist.length; i++) {

			types[typelist[i]['paymentTypeId']] = true;

		}

		$('table').each(function(i, dom) {

			if (types[$($(dom).find('i').get(0)).attr('val')]) {

				$(dom).show();
			}
		});

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
			if (payType == 2) {
				phomeArea.show();
			} else {
				phomeArea.hide();
			}
		});
		//submitPay
		$('#submitPay').bind('click', function() {
			if (me.inOrderId) {
				me.SubmitPay();
			} else {
				me.SubmitOrder();
			}
		});

	},
	SubmitOrder: function() {
		var self = this;
		self.ajax({
			url: '/Project/CEIBS/CEIBSHandler.ashx',
			data: {
				'action': 'SubmitVipPayMent',
				'itemId': self.inItemId
			},
			beforeSend: function() {
				UIBase.loading.show();
			},
			success: function(data) {
				UIBase.loading.hide();
				if (data.code == 100) {
					self.inOrderId = data.orderId;
					self.SubmitPay();
				}
			}
		});


	},
	SubmitPay: function() { //提交订单
		var me = this;

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
		} else if (parseInt(payType) == 2) { //银联语音支付
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
			} else if (!IsMobileNumber(phonenum.val())) {

				Jit.UI.Dialog({
					'content': '您输入的支付手机号码格式不正确',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
						phonenum.focus();
					}
				});
				return;
			} else {
				Jit.UI.Dialog({
					'content': '',
					'type': 'Confirm',
					'LabelOk': '取消',
					'LabelCancel': '支付完成',
					'CallBackOk': function() {
						Jit.UI.Dialog("CLOSE");
					},
					'CallBackCancel': function() {
						me.toPage('PaySuccess', '&orderId=' + JitPage.getUrlParam('orderId') + '&payType=' + payType);
					}
				});
			}

		}

		var baseInfo = me.getBaseInfo();
		var toUrl = "http://" + location.host + "/HtmlApps/html/special/europe/pay_success.html?customerId=" + me.getUrlParam('customerId');

		me.setParams('orderId_' + baseInfo.userId, self.inOrderId);

		//"orderPay",
		var hashdata = {
			action: 'setPayOrder',
			paymentId: payType,
			orderID: self.inOrderId,
			returnUrl: toUrl,
			mobileNo: parseInt(payType) == 2 ? phonenum.val() : "", //为实现 语音支付
			amount: me.totalAmount*100,
			dataFromId: 2,
		};

		me.ajax({
			url: '/Interface/data/OrderData.aspx',
			data: hashdata,
			beforeSend: function() {
				UIBase.loading.show();
			},
			success: function(data) {
				UIBase.loading.hide();
				if (data.code == "200" && parseInt(payType) != 2) {
					window.location = data.content.PayUrl;
				}
			}
		});

	}
});