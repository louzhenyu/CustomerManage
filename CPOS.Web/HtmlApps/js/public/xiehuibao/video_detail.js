Jit.AM.defindPage({
	name: 'NewDetail',
	albumId: '',
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
		self.albumId = self.getUrlParam('newsId');
		self.elements.detailArea = $('#detailArea');
		self.elements.detailTitle = $('#detailTitle');
		self.elements.detailVideo=$('#detailVideo');
		FootHandle.init({
			praiseCount: 0,
			browseCount: 0,
			shareCount: 0,
			hideJitAd: true,
			praiseEvent: function() {
				self.shareOrPraise(self.albumId, 2, 2);
			},
			shareEvent: function() {
				self.shareOrPraise(self.albumId, 4, 2);
			}
		});
		self.ajax({
			url:  '/DynamicInterface/data/Data.aspx',
			data: {
				'action': 'GetEventAlbumByAlbumID',
				'AlbumID': self.albumId
			},
			beforeSend: function() {
				UIBase.loading.show();
			},
			success: function(data) {
			
				if (data && data.code == 200 && data.content.Album) {

					var videoDetail = data.content.Album;
					self.elements.detailTitle.html(videoDetail.Title);
					self.elements.detailArea.html(videoDetail.Intro);
					self.elements.detailVideo.attr('src',videoDetail.VideoUrl
);
					FootHandle.setPraiseCount(videoDetail.PraiseCount || 0);
					FootHandle.setBroseCount(videoDetail.BrowseCount || 0);
					FootHandle.setShareCount(videoDetail.ShareCount || 0);
					//设置微信分享
					WeiXinShare.title = htmlDecode(videoDetail.Title);
					WeiXinShare.desc = htmlDecode((videoDetail.Intro).cut(300, '...'));
					if (videoDetail.ImageUrl) {
						WeiXinShare.imageUrl = videoDetail.ImageUrl;
						self.elements.detailVideo.attr('poster', detailVideo.ImageUrl);
					}
				}
			},complete:function(){
					UIBase.loading.hide();
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
	
	}
});