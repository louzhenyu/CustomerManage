it.AM.defindPage({
	name: 'MyClass',
	elements: {},
	curDataList: [], //当前查找结果
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.dataList = $('#dataList');
		self.elements.btSubmitSearch = $('.serchBtn');
		self.elements.txtSearch = $('#txtSearch');
		baseInfo = Jit.AM.getBaseAjaxParam();
		if (!baseInfo.userId) { //未登录用户跳转到登陆界面
			self.toPage('Login');
		};
		//加载我得班级信息
		Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				'action': 'getAlumniList',
				'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
				'Control': [],
				'page': 0,
				'pageSize': 10000,
				'IsSameClass': 1
			},
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.AlumniList) {
					self.curDataList = result.Data.AlumniList;
					self.setDataList(result.Data.AlumniList);
				} 
			}
		});
	},
	setDataList: function(dataList) {
		var self = this;
		var htmlList = template.render('tpDataList', {
			'datas': dataList
		});
		self.elements.dataList.html(htmlList);
	},
	//绑定事件
	initEvent: function() {
		var self = this;
		//在结果中查找。
		self.elements.btSubmitSearch.bind(self.eventType, function() {
			var
				searchKey = self.elements.txtSearch.val();
			if (!self.curDataList) {
				return false;
			};
			if (!searchKey) {
				self.setDataList(self.curDataList);
				return false;
			};
			var searchDataList = [];
			for (var i = 0; i < self.curDataList.length; i++) {
				var dataInfo = self.curDataList[i];
				if (dataInfo.VipName.indexOf(searchKey) > -1) {
					searchDataList.push(dataInfo);
				};
			};
			self.setDataList(searchDataList);
		});
	}
});