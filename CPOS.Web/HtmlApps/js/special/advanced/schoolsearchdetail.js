Jit.AM.defindPage({
	name: 'SchoolSearchDetail',
	elements: {},
	searchTotalCount: 0,
	curSearchTotalCount: 0,
	curDataList: [], //当前查找结果
	curPage: 0, //
	scrollLock: false, //正在加载或者发生异常时 锁定加载
	cacheControlList: '',
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.cacheControlList = self.getParams('SearchInfo');
		self.elements.SearchList = $('#SearchList');
		self.elements.btSubmitSearch = $('.serchBtn');
		self.elements.txtSearch = $('#txtSearch');
		self.elements.txtSearchCount = $('#txtSearchCount');
		self.elements.nextPosition = $('#nextPosition');
		self.elements.txtSearchTotalCount = $('#searchTotalCount');
		self.elements.txtScrollMore = $('#scrollMore');
		if (self.cacheControlList) {
			self.bindDataList();
			self.ListScrollEvent();
		};
	},
	ListScrollEvent: function() { //列表滚动事件
		var self = this,
			curWindow = $(window),
			atHeight = 500;
		curWindow.scroll(function() {
			if (self.scrollLock) {
				return false;
			};
			var rollTop = curWindow.scrollTop() + atHeight;
			if (rollTop >= self.elements.nextPosition.position().top) {
				self.curPage++;
				self.bindDataList();
			};
		});
	},
	bindDataList: function() {
		var self = this;
		self.scrollLock = true;
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				'action': 'getAlumniList',
				'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
				'Control': self.cacheControlList,
				'page': self.curPage,
				'pageSize': 15,
				'IsSameClass': 0
			},
			beforeSend: function() {
				Jit.UI.Loading(1);
			},
			success: function(result) {
				self.scrollLock = false;
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.AlumniList) {
					for (var i = 0; i < result.Data.AlumniList.length; i++) {
						self.curDataList.push(result.Data.AlumniList[i]);
					};	
					self.searchTotalCount = result.Data.rowCount;
					self.curSearchTotalCount = result.Data.rowCount;
					self.setDataList(result.Data.AlumniList);

				} else {
					self.scrollLock = true;
				}
			},
			error: function() {
				self.scrollLock = true;
			}
		});
	},
	setDataList: function(dataList) {
		var self = this;
		var htmlList = template.render('tpDataList', {
			'datas': dataList
		});
		self.elements.SearchList.append(htmlList);
		self.elements.txtSearchCount.html(self.elements.SearchList.find('.item').size());
		self.elements.txtSearchTotalCount.html(self.curSearchTotalCount);
		if (self.elements.SearchList.find('.item').size() >= self.curSearchTotalCount) {
			self.elements.txtScrollMore.text("。");
		} else {
			self.elements.txtScrollMore.text(",下拉显示更多。");

		}
	}, //绑定事件
	initEvent: function() {
		var self = this;
		//在结果中查找。
		self.elements.btSubmitSearch.bind(self.eventType, function() {
			var searchKey = self.elements.txtSearch.val();
			if (!self.curDataList) {
				return false;
			};
			if (!searchKey) {
				self.curSearchTotalCount = self.searchTotalCount;
				self.elements.SearchList.empty();
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
			self.curSearchTotalCount = searchDataList.length;
			self.elements.SearchList.empty();
			self.setDataList(searchDataList);
		});
	}
});