Jit.AM.defindPage({
	name: 'SignInSuccess',
	elements: {},
	objects: {
		keySignInfo: 'signInfo',
		empty: '无'
	},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.txtName = $('#txtName');
		self.elements.txtCompany = $('#txtCompany');
		self.elements.txtPosition = $('#txtPosition');
		self.elements.txtPhone = $('#txtPhone');
		self.elements.txtGroup = $('#txtGroup');
		self.elements.txtStatus = $('#txtStatus');
		self.elements.btPrint = $('#btPrint');
		self.elements.btBrose = $('#btBrose');

		var cacheSignInfo = self.getParams(self.objects.keySignInfo);
		if (!cacheSignInfo) {
			return false;
		};
		cacheSignInfo.action = 'EventCheckIn';

		Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/NwEvents/NwEventsGateway.ashx',
			interfaeMode: 'V2.0',
			data: cacheSignInfo,
			success: function(result) {

				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.CheckinInfos) {
					var dataInfo = result.Data.CheckinInfos;
					//设置信息
					self.elements.txtName.text(dataInfo.Name);
					self.elements.txtCompany.text(dataInfo.Company);
					self.elements.txtPosition.text(dataInfo.Job);
					self.elements.txtPhone.text(dataInfo.Mobile);
					self.elements.txtGroup.text(dataInfo.GroupName || self.objects.empty);
					self.elements.txtStatus.text(dataInfo.IsPay == "0" ? "未支付" : "支付成功");
					cacheSignInfo.EventId = dataInfo.EventID;
					cacheSignInfo.GroupName = dataInfo.GroupName;
					self.setParams(self.objects.keySignInfo, cacheSignInfo);
					if (dataInfo.IsPay== "1") {
						self.elements.btBrose.removeClass('btn-gray').addClass('btn-red');
						self.elements.btBrose.bind(self.eventType, function() {
							self.toPage('MyGroup');
						});
					};

				};
			}
		});


	},
	//绑定事件
	initEvent: function() {
		var self = this;



		self.elements.btPrint.bind(self.eventType, function() {
			Jit.UI.Dialog({
				'content': '功能正在开发中。',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
		});


	}
});