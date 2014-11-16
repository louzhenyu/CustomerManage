//验证用户信息
var valKeyList = {
	userId: Jit.AM.getBaseAjaxParam().customerId + '_userId'
};
//分享图片地址
var defaultShareImage = '';
if (Jit.AM.getBaseAjaxParam().customerId == 'a2573925f3b94a32aca8cac77baf6d33') {
		defaultShareImage = 'http://www.o2omarketing.cn:9004/HtmlApps/images/public/xiehuibao/sharelogo.jpg';
	} else if (Jit.AM.getBaseAjaxParam().customerId == '75a232c2cf064b45b1b6393823d2431e') {
		defaultShareImage = 'http://www.o2omarketing.cn:9004/HtmlApps/images/special/europe/indexBg.jpg';
	}else if (Jit.AM.getBaseAjaxParam().customerId == '1c6a39e4a9e54fecb508abfa5cda9eaa') {
		defaultShareImage = 'http://dev.o2omarketing.cn:9004/HtmlApps/images/special/advanced/advancedlogo.jpg';
	}
//汉字及字母长度一致获取字符长度
String.prototype.len = function() {
	var l = 0;
	var a = this.split("");
	for (var i = 0; i < a.length; i++) {
		if (a[i].charCodeAt(0) < 299) {
			l++;
		} else {
			l += 2;
		}
	}
	return l;
};
//汉字及字母长度一致截断字符
String.prototype.cut = function(len, substr) {
	if (this.len() <= len) return this;
	sl = substr.len();
	var sb = new StringBuilder();
	var l = 0;
	var a = this.split("");
	for (var i = 0; i < a.length; i++) {
		if (l >= len - sl) break;
		if (a[i].charCodeAt(0) < 299) {
			l++;
		} else {
			l += 2;
		}
		if (l > len) sb.append(" ");
		else sb.append(a[i]);
	}
	sb.append(substr);
	return sb.toString();
};
//微商城公用类
var KeyList = {
	cartCount: 'cartCount',
	val: 'val',
	userId: Jit.AM.getAppVersion().APP_CUSTOMERID + '_userId'
}, ClientSession = { //当前客户的session对象
		get: function(k) {
			return Jit.AM.getAppSession(Jit.AM.getAppVersion(), k);
		},
		set: function(k, v) {
			return Jit.AM.setAppSession(Jit.AM.getAppVersion(), k, v);
		}
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
				for (var i = 1; i < arguments.length; i++) {
					var Rep = new RegExp("\\{" + (i - 1) + "\\}", "gi");
					v = v.replace(Rep, arguments[i]);
				};
			}
			this.strList.push(v);
		};
	};
	this.toString = function() {
		return this.strList.join('');
	};
}
//验证是否是手机浏览器
function IsMobileBrowser() {
	var hash = window.location.hash,
		result = false;
	if (!hash.match("fromapp")) {
		result = navigator.userAgent.match(/(iPhone|iPod|Android|ios)/i);
	}
	return result;
}
// 验证是否是手机号码
function IsMobileNumber(n) {
	return /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/.test(n);
}
var Validates = {
	// hasPageList: ['UnionSchool'], //需要验证的列表页面
	isLogin: function() { //验证用户是否登陆
		var me = Jit.AM,
			baseInfo = me.getBaseAjaxParam();
		return (baseInfo && baseInfo.userId) ? true : false;
	},
	Customer: function() { //验证客户身份
	},
	Status: function() { //验证当前用户浏览权限
	},
	isEmail: function(v) { //邮箱
		return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(v);
	},
	isPhone: function(v) { //电话
		return /^([0\+]\d{2,3}-?)?(0\d{2,3}-?)?(\d{7,8})([- ]+\d{1,6})?$/.test(v);
	},
	isMobile: function(v) { //手机
		return /^([0\+]\d{2,3})?(0?1[3458]\d{9})$/.test(v);
	},
	isPostCode: function(v) { //邮政编码
		return /^\d{6}$/.test(v);
	},
	isIdCard: function(v) { //身份证
		//15位数身份证正则表达式
		var regShort = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
		//18位数身份证正则表达式
		var regLong = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$/;
		if (str.match(regShort) == null && str.match(regLong) == null) {
			return false;
		} else {
			return true;
		}
		return regLong.test(v) || regShort.test(v);
	},
	isUrl: function(v) { //Url地址
		return /(http[s]?|ftp):\/\/[^\/\.]+?\..+\w$/.test(v);
	},
	isInteger: function(v) { //整数
		return /^[-]{0,1}[0-9]{1,}$/.test(v);
	},
	isDecimal: function(v) { //验证整数及浮点数
		if (isInteger(v)) return true;
		var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
		if (re.test(v)) {
			if (RegExp.$1 == 0 && RegExp.$2 == 0) return false;
			return true;
		} else {
			return false;
		}
	},
	isDate: function(date, fmt) { //验证日期格式
		if (fmt == null) fmt = "yyyyMMdd";
		var yIndex = fmt.indexOf("yyyy");
		if (yIndex == -1) return false;
		var year = date.substring(yIndex, yIndex + 4);
		var mIndex = fmt.indexOf("MM");
		if (mIndex == -1) return false;
		var month = date.substring(mIndex, mIndex + 2);
		var dIndex = fmt.indexOf("dd");
		if (dIndex == -1) return false;
		var day = date.substring(dIndex, dIndex + 2);
		if (!isNumber(year) || year > "2100" || year < "1900") return false;
		if (!isNumber(month) || month > "12" || month < "01") return false;
		if (day > getMaxDay(year, month) || day < "01") return false;

		function getMaxDay(year, month) {
			if (month == 4 || month == 6 || month == 9 || month == 11)
				return "30";
			if (month == 2)
				if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
					return "29";
				else
					return "28";
			return "31";
		}
		return true;
	}
};
//输入框获得焦点时隐藏底部导航
function FootNavHide(execute, flag) {
	var inputList = $('input[type=text]'),
		textareaList = $('textarea');
	if (inputList.size() || textareaList.size() || execute) {
		var win = {
			sH: window.innerHeight,
			sW: window.innerWidth
		}
		var botNav = $(flag ? flag : '.commonNav');
		$(window).resize(function() {
			if ((win.sH - window.innerHeight > 100) && (win.sW == window.innerWidth)) {
				botNav.hide();
			} else {
				botNav.show();
			}
		});
	};
}
//隐藏获得焦点元素
function HideFocusEvent() {
	var win = {
		sH: window.innerHeight,
		sW: window.innerWidth
	}
	var botNav = $('.commonNav'),
		foodArea = $('#foodShareBox');
	$(window).resize(function() {
		if ((win.sH - window.innerHeight > 100) && (win.sW == window.innerWidth)) {
			botNav.hide();
			foodArea.hide();
		} else {
			botNav.show();
			foodArea.show();
		}
	});
}
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
//公用导航
var TopMenuHandle = {
	BindScrollEvent: function() { //绑定滚动事件
		var navPane = $('#headerTop'),
			curWindow = $(window);
		curScrollTop = curWindow.scrollTop(), eventTop = 1, stime = '';
		curWindow.scroll(function() {
			eventTop = parseInt(curWindow.scrollTop());
			if (eventTop <= 2) { //如果当前滚动条小于2像素不执行任何效果。
				navPane.css({
					'top': 0,
					'position': 'static'
				});
				clearTimeout(stime);
				return false;
			}
			eventTop = curWindow.scrollTop();
			clearTimeout(stime);
			stime = setTimeout(function() {
				if (curScrollTop > eventTop) {
					navPane.css('position', 'fixed');
					navPane.animate({
						top: '0' //正常显示
					}, 150);
					// navPane.show();
				} else {
					// navPane.hide();
					navPane.animate({
						top: '-45px' //往上移动大小
					}, 150);
				}
				curScrollTop = curWindow.scrollTop();
				eventTop = curWindow.scrollTop();
			}, 100); //滚动条停止100毫秒后触发事件
		});
	}
};
//底部内容操作
var FootHandle = {
	settings: {
		praiseCount: 0, //赞数量
		browseCount: 0, //流量数量
		shareCount: 0, //分享数量
		praiseEvent: function() {},
		shareEvent: function() {},
		bindButton: '',
		hideCount: false, //是否隐藏数量
		hideJitAd: false, //是否隐藏广告
	},
	elements: {},
	init: function(settings) {
		var footTemplate = "<div class=\"footarea\"><div class=\"footnumber\"><ul><li><a href=\"javascript:;\" class=\"praise\">赞(<span>0</span>)</a></li><li><a href=\"javascript:;\" class=\"browse\">阅读(<span>0</span>)</a></li><li><a href=\"javascript:;\" class=\"share\">分享(<span>0</span>)</a></li></ul></div><div class=\"sharefriend\"><a id=\"shareFriends\" href=\"javascript:;\"> <i></i>分享给好友</a></div><div class=\"toapp\"><a href=\"javascript:Jit.AM.toPage('ContactJit')\"><img src=\"../../../images/public/xiehuibao/jitapplink.jpg\"></a></div><div id=\"foodShareBox\" class=\"footShare\"><table border=\"0\" align=\"center\" style=\"margin-left:auto; margin-right:auto\"><tbody><tr><td><a style=\"margin-left:0\" class=\"weixinShare\"><img width=\"52\" src=\"../../../images/public/xiehuibao/sendpyicn.png\"></a><a class=\"weixinShare\"><img width=\"52\" src=\"../../../images/public/xiehuibao/sendpyic.png\"></a><a id=\"sinaShare\"><img width=\"52\" src=\"../../../images/public/xiehuibao/weiboc.png\"></a><a id=\"tencentShare\"><img width=\"52\" src=\"../../../images/public/xiehuibao/qqweiboc.png\"></a></td></tr></tbody></table><div id=\"foodShareBoxClose\" class=\"Inputbtnpp\">取消</div></div><div class=\"footTipShare\"><div><img src=\"../../../images/public/xiehuibao/tipFoo.png\"></div></div></div>";
		$('body').append(footTemplate);
		var self = this,
			foodArea = $('.footarea');
		self.elements.txtPraiseCount = foodArea.find('.praise span');
		self.elements.txtBrowseCount = foodArea.find('.browse span');
		self.elements.txtShareCount = foodArea.find('.share span');
		self.elements.btLiPraise = self.elements.txtPraiseCount.parents('li');
		self.elements.shareFriends = foodArea.find('#shareFriends');
		self.elements.foodShareBox = foodArea.find('#foodShareBox');
		self.elements.foodShareBoxClose = foodArea.find('#foodShareBoxClose');
		self.elements.weixinShare = foodArea.find('.weixinShare');
		self.elements.sinaShare = foodArea.find('#sinaShare');
		self.elements.tencentShare = foodArea.find('#tencentShare');
		self.elements.footTipShare = foodArea.find('.footTipShare');
		$.extend(self.settings, settings);
		//设置数量
		self.elements.txtPraiseCount.html(self.settings.praiseCount);
		self.elements.txtBrowseCount.html(self.settings.browseCount);
		self.elements.txtShareCount.html(self.settings.shareCount);
		//隐藏操作
		if (self.settings.hideCount) {
			foodArea.find('.footnumber').hide();
		};
		if (self.settings.hideJitAd) {
			$('.tojit,.toapp', foodArea).hide();
		};
		if (self.settings.hideButton) { //隐藏分享按钮
			$('.sharefriend', foodArea).hide();
		};
		//事件绑定
		self.BindEvent();
	},
	BindEvent: function() { //绑定事件
		var self = this;
		//点击赞数
		self.elements.btLiPraise.tap(function() {
			var element = $(this);
			if (element.hasClass('on')) {
				return false;
			};
			var count = parseInt(self.elements.txtPraiseCount.text());
			element.addClass('on');
			count++
			self.elements.txtPraiseCount.html(count);
			if (self.settings.praiseEvent) {
				self.settings.praiseEvent();
			};
		});
		//显示分享好友框
		self.elements.shareFriends.tap(function() {
			self.elements.foodShareBox.addClass('show');
		});
		//关闭分享好友框
		self.elements.foodShareBoxClose.tap(function() {
			self.elements.foodShareBox.removeClass('show');
		});
		//微信点击显示提醒分享框
		self.elements.weixinShare.tap(function() {
			self.elements.footTipShare.show();
			SetShareData(this);
		});
		//新浪分享
		self.elements.sinaShare.tap(function() {
			var sinaurl = "http://service.weibo.com/share/share.php?title=" + document.title + "&url=" + encodeURIComponent(document.URL);
			location.href = sinaurl;
			SetShareData(this);
		});
		//腾讯分享
		self.elements.tencentShare.tap(function() {
			var qqWeibo = 'http://share.v.t.qq.com/index.php?c=share&a=index&title=' + document.title + '&url=' + encodeURIComponent(document.URL);
			location.href = qqWeibo;
			SetShareData(this);
		});
		//关闭分享提醒框
		self.elements.footTipShare.tap(function() {
			self.elements.footTipShare.hide();
			self.elements.foodShareBox.removeClass('show');
		});
		//绑定自定义事件
		if (self.settings.bindButton && self.settings.bindButton.size()) {
			self.settings.bindButton.tap(function() {
				self.elements.foodShareBox.addClass('show');
			});
		};

		function SetShareData(obj) {
			var element = $(obj);
			if (element.hasClass('on')) {
				return false;
			}
			if (self.settings.shareEvent) {
				self.settings.shareEvent();
				var count = parseInt(self.elements.txtShareCount.text());
				count++;
				self.setShareCount(count);
			};
			element.addClass('on');
		}
	},
	setPraiseCount: function(count) { //设置赞数量
		this.elements.txtPraiseCount.html(count);
	},
	setBroseCount: function(count) { //设置流量数量
		this.elements.txtBrowseCount.html(count);
	},
	setShareCount: function(count) { //设置分享数量
		this.elements.txtShareCount.html(count);
	}
};

//微信分享操作
var WeiXinShare = {
	appId: '',
	imageUrl: defaultShareImage||'', //分享图片地址
	link: document.URL, //分享地址
	title: document.title, //分享标题
	desc: document.URL, //分享内容，如果没有设置会取当前连接地址
	isComplate: false, //WeixinJSBridge对象是否已经完成加载
	init: function() {
		var self = this;
		if (typeof(WeixinJSBridge) == "undefined") {
			return false;
		};
		
		function shareEvent() {

			self.isComplate = true;
			//安卓发送给朋友触发方法
			WeixinJSBridge.on("menu:share:appmessage", function(s) {
				setTimeout(function(){
		WeixinJSBridge.invoke("sendAppMessage", {
					appid: self.appId,
					img_url: self.imageUrl,
					img_width: "640",
					img_height: "640",
					link: self.link,
					desc: self.desc || self.link,
					title: self.title
				}, function(e) {});
				},100);
			}),
			WeixinJSBridge.on("menu:share:timeline", function(e) {
				WeixinJSBridge.invoke("shareTimeline", {
					img_url: self.imageUrl,
					img_width: "640",
					img_height: "640",
					link: self.link,
					desc: self.desc || self.link,
					title: self.title
				}, function(e) {});
			});
			WeixinJSBridge.on("menu:share:weibo", function(e) {
				WeixinJSBridge.invoke("shareWeibo", {
					content: self.title,
					url: self.link
				}, function(e) {});
			}), WeixinJSBridge.on("menu:share:facebook", function(e) {
				WeixinJSBridge.invoke("shareFB", {
					img_url: self.imageUrl,
					img_width: "640",
					img_height: "640",
					link: self.link,
					desc: self.desc || self.link,
					title: self.title
				}, function(e) {});
			}), //ios发送给朋友触发方法
			WeixinJSBridge.on("menu:general:share", function(s) {
				s.generalShare({
					appid: self.sid,
					img_url: self.imageUrl,
					img_width: "640",
					img_height: "640",
					link: self.link,
					desc: self.desc || self.link,
					title: self.title
				}, function(e) {});
			});
		}
		typeof WeixinJSBridge == "undefined" ?
			document.addEventListener ?
			document.addEventListener("WeixinJSBridgeReady", shareEvent, !1) :
			document.attachEvent && (document.attachEvent("WeixinJSBridgeReady", shareEvent),
				document.attachEvent("onWeixinJSBridgeReady", shareEvent)) :
			shareEvent();
	}
};

function htmlDecode(e) {
	return e.replace(/&#39;/g, "'").replace(/<br\s*(\/)?\s*>/g, "\n").replace(/&nbsp;/g, " ").replace(/&lt;/g, "<").replace(/&gt;/g, ">").replace(/&quot;/g, '"').replace(/&amp;/g, "&");
}
(function($, undefined) {
	//绑定导航事件
	TopMenuHandle.BindScrollEvent();
		
	//中欧暂时关闭进入会员中心功能
	if (Jit.AM.getBaseAjaxParam().customerId == '75a232c2cf064b45b1b6393823d2431e') {
		$('#headerTop').find('.user').hide();
	};
	//微信分享初始化,时间控制微信API是否正常加载
	WeiXinShare.isComplate = false;
	var weiXintimer = 0;
	weiXintimer = window.setInterval(function() {
		WeiXinShare.init();
		if (WeiXinShare.isComplate) {
			clearInterval(weiXintimer);
		};
	}, 30);
})($)