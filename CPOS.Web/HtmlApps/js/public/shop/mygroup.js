Jit.AM.defindPage({
	name: 'SignIn',
	elements: {},
	objects: {
		keySignInfo: 'signInfo'
	},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.myMemberList = $('#myMemberList');
		var cacheSignInfo = self.getParams(self.objects.keySignInfo);
		if (!cacheSignInfo) {
			return false;
		};
		cacheSignInfo.action = 'GetCheckInGroups';

		Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/NwEvents/NwEventsGateway.ashx',
			interfaeMode: 'V2.0',
			data: cacheSignInfo,
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.GroupList.length) {

					for (var i = 0; i < result.Data.GroupList.length; i++) {
						result.Data.GroupList[i].formatMobile = self.formatMobileNumber(result.Data.GroupList[i].Mobile)
					};
					var htmlList = template.render('tpDataList', {
						'datas': result.Data.GroupList
					});
					self.elements.myMemberList.html(htmlList);
				};
			}
		});
	}, //格式化手机号码
	formatMobileNumber: function(mobile) {
		var newMobile = "";
		if (!mobile) {
			return newMobile;
		};
		var strs = mobile.split('');
		for (var i = 0; i < strs.length; i++) {
			if (i > 2 && i < 8) {
				newMobile += "*";
			} else {
				newMobile += strs[i];
			}
		};
		return newMobile;
	},

	//绑定事件
	initEvent: function() {
		var self = this;

	}
});