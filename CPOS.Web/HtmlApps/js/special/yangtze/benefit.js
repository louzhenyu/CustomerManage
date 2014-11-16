Jit.AM.defindPage({
	name: 'Benefit',
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
			template.isEscape=false;
						Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/MicroIssueHandler.ashx',
			interfaceMode: 'V2.0',
			data: {
				action: 'MicroIssuePageGet',
				MicroTypeID: self.objects.MicroTypeID,
				MicroNumberID: YangtzeHandle.dataInfo.MicroNumberID,
				PageIndex: 1,
				PageSize: 1000,
				SortField:'PublishTime',
				SortOrder:1
			},
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.EclubMicros.Entities) {
					for (var i = 0; i < result.Data.EclubMicros.Entities.length; i++) {
						var dataInfo = result.Data.EclubMicros.Entities[i];
						if (dataInfo.PublishDate) {
							var newTimes = dataInfo.PublishDate.split('-');
							dataInfo.PublishDate =parseInt(newTimes[1])|| 1;
						};
					};
					var htmlList = template.render('tpDataList', {
						'datas': result.Data.EclubMicros.Entities
					});
					self.elements.hotScroll.html(htmlList);
					self.bindScrollEvent();
				};
			}
		});

	},
	//绑定事件
	initEvent: function() {
		var self = this;
		//绑定查看更多
		self.elements.hotScroll.delegate('.seeMore', self.eventType, function() {
			var element = $(this);
			if (element.hasClass('on')) {
				element.removeClass('on');
				element.parents('.essayUnfold').prev().css('height', '66px');
				element.html("<em></em>展开");
			} else {
				element.parents('.essayUnfold').prev().css('height', 'auto');
				element.addClass('on');
				element.html("<em></em>收起");
			}
		});
	},
	bindScrollEvent: function() {
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
				checkDOMChanges: true
			});
		}


	}

});