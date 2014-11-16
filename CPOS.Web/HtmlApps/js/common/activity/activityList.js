//UI工具
var UIBase = {
	middleShow: function(_settings) { //局中显示
		var settings = {
			obj: '',
			offsetLeft: 0, //偏移位置
			offsetTop: 0 //偏移位置
		};
		if (typeof(_settings) == 'string') {
			settings.obj = $(_settings);
		} else {
			$.extend(settings, _settings);
		}
		settings.obj.css('opacity', 0).show();
		var cssList = {
			left: '50%',
			top: '50%',
			'position': 'fixed',
			'margin-left': -Math.abs(settings.obj.width() / 2) - settings.offsetLeft,
			'margin-top': -Math.abs(settings.obj.height() / 2) - settings.offsetTop,
			'opacity': 1
		};
		settings.obj.css(cssList);
	}, //loading效果(基于CSS3动画)
	loading: {
		show: function() {
			$('body').append("<div id=\"wxloading\" class=\"wx_loading\"><div class=\"wx_loading_inner\"><i class=\"wx_loading_icon\"></i>正在加载...</div></div>");
		},
		hide: function() {
			$('#wxloading').remove();
		}
	}
	//自动隐藏loadding
};
//拼接字符串，该方法效率要高于str+="str";
function StringBuilder() {
  this.strList = [];
  this.append = function(v) {
    if (v) {
      this.strList.push(v);
    };
  };
  this.appendFormat = function(v) {
    if (v) {
      if (arguments.length > 1) {
        for (var i = 1; i <arguments.length; i++) {
                var Rep = new RegExp("\\{" + (i - 1) + "\\}", "gi");
                v = v.replace(Rep, arguments[i]);
        };
      }
      this.strList.push(v);
    };
  };
  //替换
  this.toString = function() {
    return this.strList.join('');
  };
}
String.prototype.cut = function (length,str) {
    if (length) {
	     return this.substring(0,66+1)+str;
    };
}
Jit.AM.defindPage({
	name: 'Activity',
	eventTypeID: '',
	curPage: 1, //
	scrollLock: false, //正在加载或者发生异常时 锁定加载
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.actAdList = $('#actAdList');
		self.elements.actList = $('#actList');
		self.elements.actListTab = $('#actListTab');
		self.elements.topTabList = $('.iscrooactListTab');
		self.elements.scrollList = $('#actListTab');
		self.elements.scrolItems = self.elements.scrollList.find('.picList');
		self.elements.scrollMenu = self.elements.scrollList.find('.dot');
		self.elements.scrollTitle = self.elements.scrollList.find('#scrollTitle');
		self.elements.nextPosition = $('#nextPosition');
		self.ajax({
			url: '/DynamicInterface/data/Data.aspx',
			data: {
				'action': 'getEventTypeList'
			},
			success: function(data) {
				if (data && data.code == 200 && data.content) {
					// self.elements.scrolItems.html(GetAdListHtml(data.content.ItemList));
					var allType = {
						EventTypeID: '',
						Title: '全部'
					};
					data.content.unshift(allType);
					self.elements.topTabList.html(GetTopMenuHtml(data.content));
					// self.elements.scrollTitle.html(data.content.ItemList[0].NewsTitle.cut(22, '...'));
					self.ScrollEvent();
				} else {
					self.elements.scrollList.hide();
				}
			}
		});
		self.bindDataList();
		self.ListScrollEvent();
	}, //绑定事件
	initEvent: function() {
		var self = this;
		self.elements.topTabList.delegate('a', 'click', function() {
			var el = $(this);
			self.eventTypeID = el.data('val');
			self.curPage = 1;
			self.elements.actList.empty();
			self.bindDataList();
		});
	},
	ListScrollEvent: function() { //列表滚动事件
		var self = this,
			curWindow = $(window),
			atHeight = 600;
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
			url: '/dynamicinterface/data/data.aspx',
			data: {
				'action': 'getEventList',
				"page": self.curPage,
				"pageSize": 5,
				EventTypeID: self.eventTypeID
			},
			beforeSend: function() {
				UIBase.loading.show();
			},
			success: function(data) {
				self.scrollLock = false;
				UIBase.loading.hide();
				if (data && data.code == 200 && data.content.ItemList) {
					self.elements.actList.append(GetActListHtml(data.content.ItemList));
				} else {
					self.scrollLock = true;
				}
			},
			error: function() {
				self.scrollLock = true;
			}
		});
	},
	ScrollEvent: function() {
		self = this;
		// 绑定滚动事件
		var menuList = $('.asideShow .dot em', self.elements.scrollList),
			myScroll, isWidth = 0,
			aTabList = $('.iscrooactListTab a');
		//重新設置大小
		ReSize();

		function ReSize() {
			aTabList.each(function(i) {
				var el = $(this);
				isWidth += el.width() + 30;
			});
			self.elements.scrollList.find('.iscrooactListTab').css({
				width: isWidth
			});
		}
		myScroll = new iScroll('actListTab', {
			snap: true,
			momentum: false,
			hScrollbar: false,
			vScroll: false
		});
		$(window).resize(function() {
			ReSize();
			myScroll.refresh();
		});
	}
});

function GetAdListHtml(datas) {
	var adListHtml = new StringBuilder(),
		toPageName = '';
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
		adListHtml.appendFormat("<li ><a href=\"javascript:Jit.AM.toPage('{0}','&newsId={1}');\"><img src=\"{2}\"  ></a></li>", toPageName, dataInfo.NewsId, dataInfo.ImageUrl);
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

function GetTopMenuHtml(datas) {
	var adListHtml = new StringBuilder();
	for (var i = 0; i < datas.length; i++) {
		var dataInfo = datas[i];
		adListHtml.appendFormat("<a  data-val=\"{0}\" >{1}</a>", dataInfo.EventTypeID, dataInfo.Title);
	};
	return adListHtml.toString();
}

function GetActListHtml(datas) {
	var actListHtml = new StringBuilder(),
		endDay = 0,
		beginDay = 0;
	for (var i = 0; i < datas.length; i++) {
		var actItem = datas[i];
		endDay = parseFloat(actItem.EndDay);
		beginDay = parseFloat(actItem.BeginDay);
		actListHtml.appendFormat("<a class=\"{1}\" href=\"javascript:Jit.AM.toPage('EventDetail','&newsId={0}')\"><div>", actItem.ActivityID, endDay <= 0 ? '' : beginDay <= 0 ? 'on' : 'ho');
		actListHtml.append("<div class=\"item\" >");
		actListHtml.appendFormat("<div>{0}</div>", actItem.ActivityTitle.cut(66, '...'));
		if (endDay <= 0) {
			actListHtml.appendFormat("<div><i></i>{0}<span>已结束</span></div>", actItem.BeginTime);
		} else {
			if (beginDay <= 0) {
				actListHtml.appendFormat("<div><i></i>{0}<span>已开始</span></div>", actItem.BeginTime);
			} else {
				actListHtml.appendFormat("<div><i></i>{0}<span>{1}</span></div>", actItem.BeginTime, beginDay > 0 && beginDay < 1 ? "即将开始" : parseInt(beginDay) + "天后");
			}
		}
		actListHtml.appendFormat("<div><i></i>{0} {1}</div>", actItem.ActivityCity, endDay > 0 && beginDay < 0 ? "<span>已报名" + actItem.UserCount + "人</span>" : '');
		actListHtml.append("<i></i>");
		actListHtml.append("</div>");
		actListHtml.append("</div></a>");
	};
	return actListHtml.toString();
}