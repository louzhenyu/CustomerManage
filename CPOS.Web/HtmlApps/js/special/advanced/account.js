Jit.AM.defindPage({
	name: 'Account',
	pageDataInfo: '',
	elements: {},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.txtUserName = $('#txtUserName');
		self.elements.btCollectOrEdit = $('#btCollectOrEdit');
		self.elements.imgUserHead = $('#imgUserHead');
		self.elements.txtEnglishName = $('#txtEnglishName');
		self.elements.txtPosition = $('#txtPosition');
		self.elements.toUserInfo = $('#toUserInfo');
		self.elements.btSetUserInfo = $('#btSetUserInfo');
		self.elements.txtCompany = $('#txtCompany');
		self.elements.btExit = $('#userExit');
		Jit.UI.Loading(true);
		//加载用户信息
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaceMode: 'V2.0',
			data: {
				'action': 'getUserByID',
				'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96'
			},
			success: function(result) {
				Jit.UI.Loading(false);
				if (result && result.ResultCode == 0 && result.Data.Pages) {
					self.pageDataInfo = new PageDataInfo(result.Data.Pages);
					var englishName = self.pageDataInfo.getValue(ControlMenu.EnglishName);
					self.elements.txtUserName.text(self.pageDataInfo.getValue(ControlMenu.VipName));
					self.elements.txtEnglishName.text(englishName ? "| " + englishName : '');
					self.elements.txtPosition.text(self.pageDataInfo.getValue(ControlMenu.Position));
					self.elements.imgUserHead.attr('src', self.pageDataInfo.getValue(ControlMenu.HeadImgUrl) || '../../../images/special/advanced/defaulthead.png');
					self.elements.txtCompany.text(self.pageDataInfo.getValue(ControlMenu.Company));
				};
			}
		});
	}, //绑定事件
	initEvent: function() {
		var self = this,
			baseInfo = Jit.AM.getBaseAjaxParam();
		self.elements.toUserInfo.bind(self.eventType, function() {
			self.toPage('UserInfo', 'userId=' + baseInfo.userId);
		});
		self.elements.btSetUserInfo.bind(self.eventType, function() {
			return Jit.UI.Dialog({
				'content': '功能正在开发中。',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
		});

		//用户退出
		self.elements.btExit.bind(self.eventType, function() {
			var baseInfo = self.getBaseInfo();
			baseInfo.userId = '';
			Jit.AM.setBaseAjaxParam(baseInfo, true);
			self.toPage('Login');
		});

	}
});