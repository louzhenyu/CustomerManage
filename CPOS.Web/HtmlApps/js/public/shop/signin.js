Jit.AM.defindPage({
	name: 'SignIn',
	objects: {
		keySignInfo: 'signInfo'
	},
	elements: {},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.btSignIn = $('#btSignIn');
		self.elements.txtSignCode = $('#txtSignCode');
		self.elements.txtMobile = $('#txtMobile');
	},
	//绑定事件
	initEvent: function() {
		var self = this;

		//提交验证
		self.elements.btSignIn.bind(self.eventType, function() {
			var dataParams = {
				'action': 'EventCheckIn',
				'RegCode': self.elements.txtSignCode.val(),
				'Mobile': self.elements.txtMobile.val()
			};

			if (!dataParams.RegCode && !dataParams.Mobile) {
				return Jit.UI.Dialog({
					'content': '请输入签到码或者手机号码。',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			};
			if (dataParams.Mobile && !/^([0\+]\d{2,3})?(0?1[3458]\d{9})$/.test(dataParams.Mobile)) {
				return Jit.UI.Dialog({
					'content': '您输入的手机号码格式不正确。',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
			};
			Jit.UI.Loading(true);
			self.ajax({
				url: '/ApplicationInterface/NwEvents/NwEventsGateway.ashx',
				interfaeMode: 'V2.0',
				data: dataParams,
				success: function(result) {

					Jit.UI.Loading(false);
					if (result && result.ResultCode == 0&&result.Data.IsOk) {
						var cacheSignInfo = {
							'RegCode': dataParams.RegCode,
							'Mobile': dataParams.Mobile
						};
						self.setParams(self.objects.keySignInfo, cacheSignInfo);
						self.toPage('SignInSuccess');
					} else {
						return Jit.UI.Dialog({
							'content':"签到码或者手机号不存在。",
							'type': 'Alert',
							'CallBackOk': function() {
								Jit.UI.Dialog('CLOSE');
							}
						});
					}
				}
			});
		});


	}
});