Jit.AM.defindPage({
	name: 'Activity',
	elements: {},
	objects: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		YangtzeHandle.dataInfo = YangtzeHandle.getDataInfo();
		if (!YangtzeHandle.dataInfo) {
			self.toPage('Home');
		};
		self.elements.txtSerial = $('#txtSerial');
		self.elements.txtSerial.text(YangtzeHandle.dataInfo.MicroNumberNo);
		self.elements.hotList = $('#hotList');
		self.elements.hotScroll = $('#hotScroll');
		self.objects.MicroTypeID = self.getUrlParam('MicroTypeID');
		self.objects.pageIndex = 1;
		self.objects.pageSize = 3;
		self.objects.isLastPage = false;
		self.loadPageInfo(self.objects.pageIndex);
		template.isEscape = false;

	},
	loadPageInfo: function(pageIndex) {
		var self = this;
		Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/MicroIssueHandler.ashx',
			interfaceMode: 'V2.0',
			data: {
				action: 'MicroIssuePageGet',
				MicroTypeID: self.objects.MicroTypeID,
				MicroNumberID: YangtzeHandle.dataInfo.MicroNumberID,
				PageIndex: pageIndex,
				PageSize: self.objects.pageSize
			},
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.EclubMicros.Entities.length) {
					var htmlList = template.render('tpDataList', {
						'datas': result.Data.EclubMicros.Entities
					});
					self.elements.hotScroll.append(htmlList);
					YangtzeHandle.initShareHande();
				} else {
					self.objects.isLastPage = true;

				}
			}
		});
	},
	//绑定事件
	initEvent: function() {
		var self = this;
		self.elements.hotScroll.delegate('.seeMore', self.eventType, function() {
			var element = $(this),
				prevContent = element.parent().prev();
			if (element.hasClass('on')) {

				prevContent.css('height', '66px');
				element.removeClass('on');
				element.find('label').text("展开");

			} else {
				prevContent.css('height', 'auto');
				element.addClass('on');
				element.find('label').text("收起");
						microID = prevContent.prev('.essayInfo').attr('id');
				YangtzeHandle.addMicroStats(microID, 'Clicks', function(count) {
					prevContent.prev('.essayInfo').find('.see').text(count);
				});
			}
		});

		self.bindScrollEvent();


	},
	bindScrollEvent: function() {
		var self = this;

		if (this.objects.indexNavScroll) {
			this.objects.indexNavScroll.refresh();

		} else {
			this.elements.hotList.height($(window).height());
			this.objects.indexNavScroll = new iScroll('hotList', {
				// snap: true,
				// momentum: false,
				hScrollbar: false,
				vScrollbar: true,
				// hScroll: false,
				checkDOMChanges: true,
				onScrollEnd: function() {
					if (self.objects.isLastPage) {
						return false;
					};


					if (Math.abs(this.maxScrollY - this.y) <= 60) {

						self.objects.pageIndex++;
						self.loadPageInfo(self.objects.pageIndex);

					};

				}
			});
		}


	}

});