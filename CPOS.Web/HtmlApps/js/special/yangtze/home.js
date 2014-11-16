Jit.AM.defindPage({
	name: 'Home',
	elements: {},
	objects: {},
	onPageLoad: function() {
		//当页面加载完成时触发


		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.menuList = $('.indexNav');
		self.elements.txtSerial = $('#txtSerial');

		self.objects.toMicroNumberID = self.getUrlParam('toMicroNumberID');
		YangtzeHandle.dataList = YangtzeHandle.getDataList();
		//从缓存中获取期刊
		if (!YangtzeHandle.dataList || !YangtzeHandle.dataList.length) {
			//加载微刊期数信息
			self.ajax({
				url: '/ApplicationInterface/Product/Eclub/Module/MicroIssueHandler.ashx',
				interfaceMode: 'V2.0',
				data: {
					action: 'MicroIssueNperGet'
				},
				success: function(result) {
					Jit.UI.Loading(0);
					if (result && result.ResultCode == 0 && result.Data.EclubMicroNumbers.length) {
						YangtzeHandle.dataList = result.Data.EclubMicroNumbers;
						self.setParams(YangtzeHandle.keyDataList, YangtzeHandle.dataList);
						if (self.objects.toMicroNumberID) {
							YangtzeHandle.dataInfo = self.getNumberInfo(self.objects.toMicroNumberID);
						} else {
							YangtzeHandle.dataInfo = YangtzeHandle.dataList[2];
						}
						self.elements.txtSerial.text(YangtzeHandle.dataInfo.MicroNumber);
						self.loadMicroTypes();
						self.setParams(YangtzeHandle.keyDataInfo, YangtzeHandle.dataInfo);
					};
				}
			});
		} else {
			if (self.objects.toMicroNumberID) {
				YangtzeHandle.dataInfo = self.getNumberInfo(self.objects.toMicroNumberID);

			} else {
				YangtzeHandle.dataInfo = YangtzeHandle.getDataList()[2];
			}
			self.setParams(YangtzeHandle.keyDataInfo, YangtzeHandle.dataInfo);

			self.elements.txtSerial.text(YangtzeHandle.dataInfo.MicroNumber);
			self.loadMicroTypes();
		}

	}, //活得期刊信息
	getNumberInfo: function(id) {
		var numberInfo, dataList = YangtzeHandle.getDataList();
		for (var i = 0; i < dataList.length; i++) {
			if (dataList[i].MicroNumberID == id) {
				numberInfo = dataList[i];
				break;
			};
		};
		return numberInfo;
	},
	//绑定事件
	initEvent: function() {
		var self = this;
	},
	loadMicroTypes: function() {

		var self = this;

		Jit.UI.Loading(1);

		//加载微刊期数信息
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/MicroIssueHandler.ashx',
			interfaceMode: 'V2.0',
			data: {
				action: 'MicroIssueTypeGet',
				ParentID: YangtzeHandle.dataInfo.MicroTypeID
			},
			success: function(result) {
				Jit.UI.Loading(0);
				//1=热点2=活动3=学员4=公益
				if (result && result.ResultCode == 0 && result.Data.EclubMicroTypes.length) {
					for (var i = 0; i < result.Data.EclubMicroTypes.length; i++) {
						var dataInfo = result.Data.EclubMicroTypes[i];
						switch (parseInt(dataInfo.Style)) {
							case 1:
								dataInfo.MicroType = "Hot";
								break;
							case 2:
								dataInfo.MicroType = "Activity";
								break;
							case 3:
								dataInfo.MicroType = "Students";
								break;
							case 4:
								dataInfo.MicroType = "Benefit";
								break;
						}
					};
					var htmlList = template.render('tpDataList', {
						'datas': result.Data.EclubMicroTypes
					});
					self.elements.menuList.find('ul').html(htmlList);
					self.menuAnimation();

				};
			}
		});



	},
	menuAnimation: function() {
		var self = this,
			moveItem = self.elements.menuList.find('li').first(),
			menuTime = 300;
		//导航动画
		setTimeout(ShowMenuItem, menuTime);

		function ShowMenuItem() {
			moveItem.css('margin-right', 2);
			moveItem = moveItem.next();
			if (moveItem.size()) {
				setTimeout(ShowMenuItem, menuTime += 80);
			}
		}

		setTimeout(function() {
			self.elements.menuList.css('width', $('body').width());
			var ulWidth = self.elements.menuList.find('li').size() * (self.elements.menuList.find('li').width() + 2);
			self.elements.menuList.find('ul').css('width', ulWidth);
			var indexNavScroll = new iScroll('indexNav', {
				// snap: true,
				// momentum: false,
				hScrollbar: false,
				vScrollbar: true,
				// hScroll: false,
				checkDOMChanges: true
			});
		}, 3000);


	}

});