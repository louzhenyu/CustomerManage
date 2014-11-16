Jit.AM.defindPage({
	name: 'benefitDetail',
	elements: {},
	objects: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.btPrevPage = $('#prevPage');
		self.elements.btNextPage = $('#nextPage');
		self.elements.txtSerial = $('#txtSerial');
		self.elements.txtSee = $('#txtSee');
		self.elements.txtPraise = $('#txtPraise');
		self.elements.txtShare = $('#txtShare');
		self.elements.txtContent = $('#txtContent');
		self.elements.imagIntro = $('#imagIntro');
		self.elements.txtMicroID = $('#txtMicroID');
		self.elements.txtTitle=$('#txtTitle');
		self.elements.txtSerial.text(YangtzeHandle.dataInfo.MicroNumberNo);
			template.isEscape=false;

		self.objects.MicroID = self.getUrlParam('MicroID');
		self.objects.MicroTypeID = self.getUrlParam('MicroTypeID');
		self.objects.MicroNumberID = self.getUrlParam('MicroNumberID');

		self.objects.curPageNo = 0;
		self.loadPageInfo(self.objects.MicroID, self.objects.MicroTypeID, self.objects.MicroNumberID);


	},
	loadPageInfo: function(microID, microTypeID, microNumberID) {
		var self=this;
		//加载详情信息
		Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/MicroIssueHandler.ashx',
			interfaceMode: 'V2.0',
			data: {
				action: 'MicroIssueDetailGet',
				MicroID: microID,
				MicroTypeID: microTypeID,
				MicroNumberID: microNumberID
			},
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.EclubMicros) {
					self.objects.dataList = result.Data.MicroIDS;
					self.setPageInfo(result.Data.EclubMicros);
				};
			}
		});
	},
	setPageInfo: function(eclubMicrosInfo) {
		var self = this;
		self.elements.txtSee.text(eclubMicrosInfo.Clicks);
		self.elements.txtPraise.text(eclubMicrosInfo.Goods);
		self.elements.txtShare.text(eclubMicrosInfo.Shares);
		self.elements.txtContent.html(eclubMicrosInfo.Content);
		self.elements.imagIntro.attr('src', eclubMicrosInfo.ImageUrl);
		self.elements.txtMicroID.attr('id', eclubMicrosInfo.MicroID);
		self.elements.txtTitle.text(eclubMicrosInfo.MicroTitle);
		YangtzeHandle.initShareHande();
	},
	//绑定事件
	initEvent: function() {
		var self = this;

		//上一页
		self.elements.btPrevPage.bind(self.eventType, function() {
			if (self.objects.curPageNo <= 0) {
				return false;
			};
			self.loadPageInfo(self.objects.dataList[self.objects.curPageNo], self.objects.MicroTypeID, self.objects.MicroNumberID);
			self.objects.curPageNo -=1;
			if (self.objects.curPageNo <= 0) {
				self.elements.btPrevPage.addClass('on');
			} else {
				self.elements.btPrevPage.removeClass('on');
			}
					if (self.objects.curPageNo >= self.objects.dataList.length) {
				self.elements.btNextPage.addClass('on');
			} else {
				self.elements.btNextPage.removeClass('on');
			}

		});

		//下一页
		self.elements.btNextPage.bind(self.eventType, function() {
			if (self.objects.curPageNo >= self.objects.dataList.length) {
				return false;
			};
			self.loadPageInfo(self.objects.dataList[self.objects.curPageNo], self.objects.MicroTypeID, self.objects.MicroNumberID);
			self.objects.curPageNo +=1;
			if (self.objects.curPageNo >= self.objects.dataList.length) {
				self.elements.btNextPage.addClass('on');
			} else {
				self.elements.btNextPage.removeClass('on');
			}
		if (self.objects.curPageNo <= 0) {
				self.elements.btPrevPage.addClass('on');
			} else {
				self.elements.btPrevPage.removeClass('on');
			}

		})
	}

});