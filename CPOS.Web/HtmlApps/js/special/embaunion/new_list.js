Jit.AM.defindPage({

	name : 'NewList',
	KeyList : {
		newsId : 'newsId',
		newsType : 'newsType'
	},
	elements : {
		homeAdList : '',
		adItemTitle : '',
		newList : '',
		scrollList : '',
		scrolItems : '',
		scrollMenu : '',
		scrollTitle : ''
	},
	onPageLoad : function() {

		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);

		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad : function() {
		var self = this;

		self.elements.homeAdList = $('#newAdList');
		self.elements.adItemTitle = $('#adItemTitle');
		self.elements.newList = $('#newList');
		self.ajax({
			url : '/Project/CEIBS/CEIBSHandler.ashx',
			data : {
				'action' : 'GetEventStats'
			},
			beforeSend : function() {
				UIBase.loading.show();

			},
			success : function(data) {
				UIBase.loading.hide();
				if (data && data.code == 200 && data.content.concernList) {
					self.elements.newList.html(GetNewListHtml(data.content.concernList));
				}
			}
		});

		self.elements.scrollList = $('#newAdList');
		self.elements.scrolItems = self.elements.scrollList.find('.picList');
		self.elements.scrollMenu = self.elements.scrollList.find('.dot');
		self.elements.scrollTitle = self.elements.scrollList.find('#scrollTitle');
		
	
	}, //绑定事件
	initEvent : function() {
		var self = this;

		self.elements.newList.delegate('.heart', 'click', function() {

			var el = $(this), elParent = el.parents('.followArea'), txtNumber = el.find('i'), newsId = elParent.attr(self.KeyList.newsId), newsType = elParent.attr(self.KeyList.newsType);
			if (el.hasClass('on')) {
				return false;
			};

			self.ajax({
				url : '/Project/CEIBS/CEIBSHandler.ashx',
				data : {
					'action' : 'addEventStats',
					'id' : newsId,
					'CountType' : '2',
					'NewsType' : newsType,

				},
				success : function(data) {
					if (data && data.code == 200) {
						var curPraiseCount = parseInt(txtNumber.html());
						el.addClass('on')
						txtNumber.html(curPraiseCount += 1);
					}
				}
			});
		});

	},
	ScrollEvent : function() {
		self = this;
		// 绑定滚动事件
		var menuList = $('.asideShow .dot em', self.elements.homeAdList);

		//重新設置大小
		ReSize();

		function ReSize() {
			self.elements.homeAdList.find('.commonBanner').css({
				width : (self.elements.homeAdList.width()) * menuList.size()
			});

			self.elements.homeAdList.find('.picList li').css({
				width : (self.elements.homeAdList.width())
			});
		}

		myScroll = new iScroll('newAdList', {
			snap : true,
			momentum : false,
			hScrollbar : false,
			vScroll : false,
			onScrollEnd : function() {
				if (this.currPageX > (menuList.size() - 1)) {
					return false;
				};
				menuList.removeClass('on');
				menuList.eq(this.currPageX).addClass('on');
				self.elements.scrollTitle.html(menuList.eq(this.currPageX).data(KeyList.val));
			}
		});
		// menuList.bind('click', function() {
		//     myScroll.scrollToPage(menuList.index(this));
		// });
		$(window).resize(function() {
			ReSize();
			myScroll.refresh();
		});
	}
});

function GetAdListHtml(datas) {
	var adListHtml = new StringBuilder(), toPageName = '';
	for (var i = 0; i < datas.length; i++) {
		var dataInfo = datas[i];

		switch (parseInt(dataInfo.Type)) {
			case 1:
				toPageName = "NewDetail";
				break;
			case 2:
				toPageName = "ActDetail";
				break;
			case 3:
				toPageName = "Home";
				break;
		}
		adListHtml.appendFormat("<li ><a href=\"javascript:Jit.AM.toPage('{0}','&newsId={1}');\"><img src=\"{2}\" ></a></li>", toPageName, dataInfo.NewsId, dataInfo.ImageUrl);
	};

	return adListHtml.toString();
}

function GetAdMenuHtml(datas) {
	var adListHtml = new StringBuilder();
	for (var i = 0; i < datas.length; i++) {
		var dataInfo = datas[i];
		adListHtml.appendFormat("<em data-val=\"{0}\" class=\"{1}\"></em>", dataInfo.NewsTitle.cut(22, '...'), i == 0 ? 'on' : '');
	};

	return adListHtml.toString();
}

function GetNewListHtml(datas) {
	var htmlList = new StringBuilder(), keyStr, typeStr;
	for (var i = 0; i < datas.length; i++) {
		var newInfo = datas[i];
		htmlList.appendFormat("<div class=\"followArea\" newsId=\"{0}\"  newsType=\"{1}\" >", newInfo.newsID, newInfo.newsType);

		if (newInfo.newsType != 1) {

			if (newInfo.newsType == 2) {
				keyStr = "ActDetail";

			} else if (newInfo.newsType == 3) {
				keyStr = "NewDetail";
			}
			;
			htmlList.appendFormat("<a href=\"javascript:Jit.AM.toPage(\'{0}\',\'newsId={1}\');\">", keyStr, newInfo.newsID);
		};

		htmlList.appendFormat("<h2 class=\"title t-overflow\">{0}</h2>", newInfo.title.len() > 33 ? newInfo.title.cut(33, '...') : newInfo.title);

		htmlList.append("<p class=\"textWrap\">");

		if (newInfo.newsType == 1) {
			htmlList.appendFormat(" <video src=\"{0}\" poster=\"{1}\" -webkit-playsinline=\"true\" controls=\"controls\" class=\"video\" ></video>", newInfo.videoUrl, newInfo.imageUrl);

		} else if (newInfo.newsType == 2 || newInfo.newsType == 3) {
			htmlList.appendFormat('<img  class=\"pic\" src=\"{0}\">', newInfo.imageUrl);

		}
		;

		htmlList.appendFormat('{0}', newInfo.intro.len() > 106 ? newInfo.intro.cut(106, '...') : newInfo.intro);
		htmlList.append("</p>");

		htmlList.append("<p class=\"clearfix\">");

		if (newInfo.newsType != 1) {
			htmlList.append("<span class=\"fullText\">全文</span>");

		};

		switch (newInfo.newsType) {
			case 1:
				typeStr = "视频";
				break;

			case 2:
				typeStr = "活动";
				break;

			case 3:
				typeStr = "资讯";
				break;
		}

		htmlList.appendFormat("<span class=\"special\">{0}</span>", typeStr);
		htmlList.append("</p>");
		if (newInfo.newsType != 1) {
			htmlList.append("</a>");
		}

		htmlList.append("<div class=\"handle clearfix\">");
		htmlList.appendFormat("<span class=\"heart\">赞（<i>{0}</i>）</span>", newInfo.praiseCount);
		htmlList.appendFormat("<span class=\"see\">浏览（{0}）</span>", newInfo.browseCount);
		htmlList.append("</div>");
		htmlList.appendFormat("<span class=\"time\">{0}</span>", parseInt(newInfo.agoTime) > 23 ? "1天前" : newInfo.agoTime + '小时前');

		htmlList.append("</div>");
	};

	return htmlList.toString();
}