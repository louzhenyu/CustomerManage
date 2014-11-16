Jit.AM.defindPage({
	name: 'NewDetail',
	curNewsId: '',
	curNewsType: '',
	eventStatsID: '',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.curNewsId = self.getUrlParam('newsId');
		self.elements.detailArea = $('#detailArea');
		self.elements.detailTitle = $('#detailTitle');
		self.elements.NewsTitleImage = $('#NewsTitleImage');
		FootHandle.init({
			praiseCount: 0,
			browseCount: 0,
			shareCount: 0,
			hideJitAd: true,
			praiseEvent: function() {
				self.shareOrPraise(self.curNewsId, 2, 1);
			},
			shareEvent: function() {
				self.shareOrPraise(self.curNewsId, 4, 1);
			}
		});
		self.ajax({
			url: '/Project/CEIBS/CEIBSHandler.ashx',
			data: {
				'action': 'getNewsDetailByNewsID',
				'newsId': self.curNewsId
			},
			beforeSend: function() {
				UIBase.loading.show();
			},
			success: function(data) {
				UIBase.loading.hide();
				if (data && data.code == 200 && data.content.News) {
					var newsInfo = data.content.News;
					self.elements.detailTitle.html(newsInfo.NewsTitle);
					self.elements.detailArea.html(newsInfo.Content);
					FootHandle.setPraiseCount(newsInfo.PraiseCount || 0);
					FootHandle.setBroseCount(newsInfo.BrowseCount || 0);
					FootHandle.setShareCount(newsInfo.ShareCount || 0);
					//设置微信分享
					WeiXinShare.title = htmlDecode(newsInfo.NewsTitle);
					WeiXinShare.desc = htmlDecode((self.elements.detailArea.text()).cut(300, '...'));
					if (newsInfo.ImageUrl) {
						WeiXinShare.imageUrl = newsInfo.ImageUrl;
						self.elements.NewsTitleImage.attr('src', newsInfo.ImageUrl);
					} else {
						self.elements.NewsTitleImage.hide();
					}

				}
			}
		});
	}, //分享回调函数
	shareOrPraise: function(sId, countType, newsType) {
		this.ajax({
			url: '/Project/CEIBS/CEIBSHandler.ashx',
			data: {
				'action': 'AddEventStats',
				'id': sId,
				'CountType': countType,
				'NewsType': newsType
			},
			success: function(data) {
				if (data && data.code == 200) {}
			}
		});
	},
	//绑定事件
	initEvent: function() {
		//点击收藏事件
		var self = this;
		self.elements.NewsTitleImage.error(function() {
			this.src = '../../../images/public/xiehuibao/newstemp01.jpg';
		});
	}
});