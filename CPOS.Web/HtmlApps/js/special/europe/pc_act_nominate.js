var JitPage = {
	name: 'ActNominate',
	keyUserInfo: 'userinfos',
	activityInfo: '',
	enTickedId: 'AE49CE91CFA64329A56CB423A8E9C9E9', //报名ID
	enTickedPrice: 0,
	activityId: '43eda030caa1e55a5fc5f80f513638de',
	language: "1",
	common: {
		"openId": null,
		"customerId": "75a232c2cf064b45b1b6393823d2431e",
		"userId": "",
		"locale": null,
		"roleId": null
	},
	elements: {
		submitError: false
	},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //初始化数据
	initLoad: function() {
		var self = this;
		self.elements.btSignUp = $('#btSignUp');
		self.elements.actBox = $('.submit_area');
		self.elements.actBoxInfo = $('#actboxinfo');
		self.elements.actBgShade = $('.actBgShade');
		self.elements.actBoxSubmit = $('#actBoxSubmit');
		var EventData = {
			'action': 'getEventByEventID',
			'ReqContent': JSON.stringify({
				'common': self.common,
				'special': {
					'action': 'getEventByEventID',
					'ActivityID': self.activityId
				}
			})
		};
		$.ajax({
			url: '/dynamicinterface/data/data.aspx',
			data: EventData,
			dataType: 'json',
			beforeSend: function() {
				UIBase.loading.show();
			},
			type: 'post',
			success: function(data) {
				UIBase.loading.hide();
				if (data && data.code == 200) {
					self.setPageInfo(data.content.Activity);
				}
			}
		});
		var UserData = {
			'action': 'getUserDefinedByUserID',
			'ReqContent': JSON.stringify({
				'common': self.common,
				'special': {
					TypeID: '2',
					'EventID': self.activityId
				}
			})
		};
		// 加载报名字段信息
		$.ajax({
			url: '/dynamicinterface/data/data.aspx',
			data: UserData,
			dataType: 'json',
			type: 'post',
			success: function(data) {
				self.elements.actBoxInfo.empty();
				if (data && data.code == 200 && data.content.pageList) {
					self.elements.actBoxInfo.html(GetlistHtml(data.content.pageList));
					self.controlDatas = data.content.pageList;
					self.LoadUserCache();
					//修正样式
					$('.commonBox .comline').last().addClass('curline');
				} else {
					self.elements.actBoxInfo.html("很抱歉，没有加载到你的配置信息。");
				}
			}
		});
	},
	LoadUserCache: function() { //加载用户缓存信息
		var self = this,
			cacheUserInfos = null;
		if (cacheUserInfos) {
			for (var i = 0; i < cacheUserInfos.length; i++) {
				var userInfo = cacheUserInfos[i],
					element = $('#' + userInfo.ControlId);
				if (element.size()) {
					if (!element.val()) {
						element.val(userInfo.Value);
					};
				};
			};
		};
	},
	setPageInfo: function(activityInfo) {
		var self = this;
		self.activityInfo = activityInfo;
	}, //绑定事件
	initEvent: function() {
		var self = this;
		self.elements.actBoxSubmit.bind('click', function() {
			var element = $(this);
			if (element.hasClass('on')) {
				return false;
			};
			self.submitEnroll();
		});
		$('.nomichange a').bind('click', function() {
			var element = $(this),
				val = element.data('val'),
				commonList = $('.commonTit');
			self.language = val;
			commonList.each(function() {
				var elementCommon = $(this),
					en = elementCommon.data('val'),
					ch = elementCommon.data('chval');
				if (self.language == "1") {
					elementCommon.html(ch);
					self.elements.actBoxSubmit.html("提&nbsp;&nbsp;交");
				} else {
					elementCommon.html(en);
					self.elements.actBoxSubmit.html("Submit");
				}
			});
			$('.noi_title').hide();
			if (self.language == "1") {
				$('#chTitle').show();
			} else {
				$('#enTitle').show();
			}
			$('.nomichange a').removeClass('on');
			element.addClass('on');
		});
	},
	tips: function(str, obj) {
		UI.Dialog({
			'content': str,
			'type': 'Alert',
			'CallBackOk': function() {
				UI.Dialog('CLOSE');
				if (obj) {
					obj.focus();
				};
			}
		});
	},
	getTicketItem: function(tickedId) {
		var self = this,
			tickedInfo;
		for (var i = 0; i < self.activityInfo.Ticket.length; i++) {
			var item = self.activityInfo.Ticket[i];
			if (item.TicketID == tickedId) {
				tickedInfo = item;
				break;
			};
		};
		return tickedInfo;
	},
	submitEnroll: function() {
		var self = this;
		self.submitError = false;
		var controList = self.GetControlInfos(1);
		if (self.submitError || !controList) {
			return false;
		};
		var addEventData = {
			'action': 'addEventInfo',
			'ReqContent': JSON.stringify({
				'common': self.common,
				'special': {
					'action': 'addEventInfo',
					'Control': controList,
					'ActivityID': self.activityInfo.ActivityID,
					'TicketID': self.enTickedId,
					'TicketPrice': self.enTickedPrice
				}
			})
		};
		$.ajax({
			url: '/dynamicinterface/data/data.aspx',
			data: addEventData,
			dataType: 'json',
			type: 'post',
			beforeSend: function() {
				UIBase.loading.show();
			},
			success: function(data) {
				UIBase.loading.hide();
				if (data && data.code == 200) {
					// self.elements.btSignUp.addClass('on');
					// self.elements.btSignUp.val("您已报名");
					// self.elements.actBox.hide();
					// self.elements.actBgShade.hide();
					self.elements.actBoxSubmit.addClass('on')
					self.elements.actBoxSubmit.html(self.language == "1" ? '您已提名' : 'complete');
					var curTitketItem = self.getTicketItem(self.enTickedId),
						enrollTip = self.activityInfo.VipSum > self.activityInfo.TicketSum ? '此活动名额已满，您将作为此次活动的备选人员' : '您已报名成功，正在审核';
					//未登录的用户自动登陆
					// if (data.content.VipID && !Validates.isLogin()) {
					//     var baseInfo = Jit.AM.getBaseAjaxParam();
					//     baseInfo.userId = data.content.VipID;
					//     baseInfo.openId = data.content.VipID;
					//     Jit.AM.setBaseAjaxParam(baseInfo);
					// };
					UI.Dialog({
						'content': self.language == "1" ? '感谢您参与提名，请转发并分享给校友，让更多校友参与！' : 'Thank you for participating in the nomination, please forward and share to the alumni, let more alumni participation！',
						'type': 'Alert',
						LabelOk: self.language == "1" ? '确定' : 'OK',
						'CallBackOk': function() {
							UI.Dialog('CLOSE');
						}
					});
				} else {
					return UI.Dialog({
						'content': data.description,
						'type': 'Alert',
						'CallBackOk': function() {
							UI.Dialog('CLOSE');
						}
					});
				}
			}
		});
	},
	GetControlItem: function(page, controId) {
		var self = this;
		for (var i = 0; i < self.controlDatas.length; i++) {
			if (parseInt(self.controlDatas[i].PageNum) == page) {
				for (var j = 0; j < self.controlDatas[i].Block.length; j++) {
					var subBlock = self.controlDatas[i].Block[j];
					for (var p = 0; p < subBlock.Control.length; p++) {
						var controInfo = subBlock.Control[p];
						if (controInfo.ControlID == controId) {
							return controInfo;
						};
					};
				};
			};
		};
		return null;
	},
	GetControlInfos: function(pageNumber) {
		var self = this,
			curPages = $('#page' + pageNumber);
		var controList = [];
		$('input,select,textarea', curPages).each(function() {
			if (self.submitError) {
				return;
			};
			var item = $(this),
				controlInfo = {
					ControlId: item.attr('id'),
					ColumnName: '',
					Value: ''
				},
				dataControlInfo = self.GetControlItem(pageNumber, controlInfo.ControlId);
			//获取信息
			switch (parseInt(dataControlInfo.ControlType)) {
				case 1:
				case 6:
				case 9:
				case 10:
					controlInfo.ColumnName = dataControlInfo.ColumnName;
					controlInfo.Value = item.val();
					break;
			}
			//验证是否输入
			if (dataControlInfo.IsMustDo && !controlInfo.Value) {
				self.submitError = true;
				//获取信息
				switch (parseInt(dataControlInfo.ControlType)) {
					case 1:
					case 9:
						self.tips(self.language == "1" ? "请输入" + dataControlInfo.ColumnDesc : "Please input " + dataControlInfo.ColumnDescEN, item);
						break;
					case 6:
						self.tips("请选择" + dataControlInfo.ColumnDesc, item);
						break;
				}
				return false;
			}
			// 1：文本; 2: 整数；3：小数；4:日期；5：时间；6：邮件 ；7：电话；8：手机；9验证Url网址；10：验证身份
			//验证类型
			switch (parseInt(dataControlInfo.AuthType)) {
				case 1:
					break;
				case 2:
					if (controlInfo.Value && !Validates.isInteger(controlInfo.Value)) {
						self.submitError = true;
						self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
						return false;
					};
					break;
				case 3:
					if (controlInfo.Value && !Validates.isDecimal(controlInfo.Value)) {
						self.submitError = true;
						self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
						return false;
					};
					break;
				case 4:
					break;
				case 5:
					break;
				case 6:
					if (controlInfo.Value && !Validates.isEmail(controlInfo.Value)) {
						self.submitError = true;
						self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
						return false;
					};
					break;
				case 7:
					if (controlInfo.Value && !Validates.isPhone(controlInfo.Value)) {
						self.submitError = true;
						self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
						return false;
					};
					break;
				case 8:
					if (controlInfo.Value && !Validates.isMobile(controlInfo.Value)) {
						self.submitError = true;
						self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
						return false;
					};
					break;
				case 9:
					break;
				case 10:
					break;
			}
			controList.push(controlInfo);
		});
		return controList;
	}
};
//获取用户基础信息
function GetlistHtml(pages) {
	//ControlType      类型：1:文本；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；9:超文本,10:密码类型
	if (!pages.length) {
		return ""
	};
	var htmlList = new StringBuilder(),
		hasItem = "<i>*</i> ",
		selectChecked = "selected=selected",
		controlType = 0;
	for (var i = 0; i < pages.length; i++) {
		var pageItem = pages[i];
		// 页面标题
		htmlList.appendFormat("<div data-val=\"{0}\"  style=\"display:{1}\" name=\"pages\" id=\"page" + pageItem.PageNum + "\" > ", pageItem.PageNum, i > 0 ? "none" : "block");
		// if (pageItem.PageName) {
		//     htmlList.appendFormat("<div class=\"topTitle\" ><h3>{0}</h3></div>", pageItem.PageName);
		// };
		//块级元素
		for (var j = 0; j < pageItem.Block.length; j++) {
			var blockItem = pageItem.Block[j];
			if (!blockItem.Control) {
				break;
			};
			htmlList.append("<div class=\"commonBox\"  >");
			for (var p = 0; p < blockItem.Control.length; p++) {
				var controlItem = blockItem.Control[p];
				controlType = parseInt(controlItem.ControlType);
				//支持当前类型
				switch (controlType) {
					case 1:
						htmlList.append("<p class=\"commonList\">");
						htmlList.appendFormat("<em class=\"commonTit\" data-val=\"{1}\" data-chval=\"{0}\">{0}{2}</em>", controlItem.ColumnDesc, controlItem.ColumnDescEN, controlItem.IsMustDo ? hasItem : '：');
						htmlList.appendFormat("<span class=\"wrapInput\"><input id=\"{0}\" maxlength=\"128\"  type=\"{2}\" value=\"{1}\"  ></span>", controlItem.ControlID, controlItem.Value ? controlItem.Value : '', GetInputType(controlItem.AuthType));
						htmlList.append("</p>");
						// htmlList.append("<p class=\"comline\" style=\"margin-left:0;\" ></p>");
						break;
					case 6:
						htmlList.append("<p class=\"commonList\">");
						htmlList.appendFormat("<em class=\"commonTit\" data-val=\"{3}\" data-chval=\"{0}\">{0}</em><span class=\"wrapInput\">{2}<select id=\"{1}\" >   ", controlItem.ColumnDesc, controlItem.ControlID, controlItem.IsMustDo ? hasItem : '：', controlItem.ColumnDescEN);
						for (var v = 0; v < controlItem.Options.length; v++) {
							var optionItem = controlItem.Options[v];
							htmlList.appendFormat(" <option value=\"{0}\" {2}>{1}</option>", optionItem.OptionID, optionItem.OptionText, optionItem.IsSelected ? selectChecked : '');
						};
						htmlList.append("</select></span></p>");
						htmlList.append("<p class=\"comline\"></p>");
						break;
					case 9:
						htmlList.append("<div class=\"subtxt\">");
						htmlList.appendFormat("<div class=\"commonTitle\" data-val=\"{2}\" data-chval=\"{0}\" >{0}{1}</div>", controlItem.ColumnDesc, controlItem.IsMustDo ? hasItem : '：', controlItem.ColumnDescEN);
						htmlList.appendFormat("<div class=\"commonArea\"><textarea id=\"{0}\" maxlength=\"500\"  >{1}</textarea></div>", controlItem.ControlID, controlItem.Value ? controlItem.Value : '');
						htmlList.append("</div>");
						break;
					case 10:
						htmlList.append("<p class=\"commonList\">");
						htmlList.appendFormat("<em class=\"commonTit\" data-val=\"{1}\" data-chval=\"{0}\">{0}</em>", controlItem.ColumnDesc, controlItem.ColumnDescEN);
						htmlList.appendFormat("<span class=\"wrapInput\">{2}<input id=\"{0}\" maxlength=\"128\"  type=\"password\" value=\"{1}\"  ></span>", controlItem.ControlID, controlItem.Value ? controlItem.Value : '', controlItem.IsMustDo ? hasItem : '：');
						htmlList.append("</p>");
						htmlList.append("<p class=\"comline\"></p>");
						break;
				}
			};
			htmlList.append(" </div>");
		};
		htmlList.append("</div>");
		htmlList.append("</div>");
	};
	//通过验证类型获取文本类型
	function GetInputType(valType) {
		var inputType;
		switch (parseInt(valType)) {
			case 1:
			case 7:
			case 8:
				inputType = "text";
				break;
			case 2:
			case 3:
			case 10:
				inputType = "number";
				break;
			case 4:
				inputType = "date";
				break;
			case 5:
				inputType = "datetime ";
				break;
			case 6:
				inputType = "email";
				break;
			case 9:
				inputType = "url ";
				break;
			default:
				inputType = 'text';
		}
		return inputType;
	}
	return htmlList.toString();
}
//验证用户信息
var valKeyList = {
	userId: '_userId'
};
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
	hasPageList: ['UnionSchool'], //需要验证的列表页面
	isLogin: function() { //验证用户是否登陆
		var me = Jit.AM,
			baseInfo = me.getBaseAjaxParam();
		return (baseInfo && baseInfo.userId) ? true : false;
	},
	Customer: function() { //验证客户身份
		// if (Jit.AM.getBaseAjaxParam() == null || Jit.AM.getBaseAjaxParam().customerId == null) { //如¨?果?userId不?为a空?，ê?则¨°表À¨ª示º?缓o存ä?已°?有®D基¨´础ä?数ºy据Y，ê?如¨?果?无T，ê?则¨°需¨¨要°a给?值¦Ì
		//     if (Jit.AM.getUrlParam('customerId') != null && Jit.AM.getUrlParam('customerId') != "") {
		//         Jit.AM.setBaseAjaxParam({
		//             "customerId": Jit.AM.getUrlParam('customerId'),
		//             "userId": "",
		//             "openId": ""
		//         });
		//     }
		// }
	},
	Status: function() { //验证当前用户浏览权限
		// var me = Jit.AM,
		//     self = this,
		//     baseInfo = me.getBaseAjaxParam();
		// if (!baseInfo.roleId || baseInfo.roleId == null) {
		//     baseInfo.roleId = 'CE1FF5BE7B6A4B0E8ACF5E2501B43D78'; //设置默认的角色ID
		//     Jit.AM.setBaseAjaxParam(baseInfo);
		// };
		// if (baseInfo && baseInfo.userId) {
		//     return false;
		// };
		// var userId = localStorage.getItem(valKeyList.userId);
		// if (userId) {
		//     baseInfo.userId = userId;
		//     Jit.AM.setBaseAjaxParam(baseInfo);
		//     return false;
		// };
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
var UI = {
	//临时代码
	Dialog: function(cfg) {
		if (cfg == 'CLOSE') {
			var panel = $('.jit-ui-panel');
			if (panel) {
				(panel.parent()).remove();
			}
		} else {
			cfg.LabelOk = cfg.LabelOk ? cfg.LabelOk : '确定';
			cfg.LabelCancel = cfg.LabelOk ? cfg.LabelCancel : '取消';
			var panel, btnstr;
			if (cfg.type == 'Alert' || cfg.type == 'Confirm') {
				btnstr = (cfg.type == 'Alert') ? '<a id="jit_btn_ok" style="margin:0 auto">' + cfg.LabelOk + '</a>' : '<a id="jit_btn_cancel">' + cfg.LabelCancel + '</a><a id="jit_btn_ok">' + cfg.LabelOk + '</a>';
				panel = $('<div id="dialog_div"><div class="jit-ui-panel"></div><div name="jitdialog" style="margin-top:120px" class="popup br-5">' + '<p class="ac f14 white" id="dialog__content">' + cfg.content + '</p><div class="popup_btn">' + btnstr + '</div></div></div>');
			} else if (cfg.type == 'Dialog') {
				if (cfg.isAppend) { //追加内容
					if ($("#dialog__content").length) {
						$("#dialog__content").append("<br/>" + cfg.content);
					} else {
						panel = $('<div id="dialog_div"><div class="jit-ui-panel"></div><div style="margin-top:120px" class="popup br-5"><p class="ac f14 white" id="dialog__content">' + cfg.content + '</p></div></div>');
					}
				} else {
					panel = $('<div id="dialog_div"><div class="jit-ui-panel"></div><div style="margin-top:120px" class="popup br-5"><p class="ac f14 white" id="dialog__content">' + cfg.content + '</p></div></div>');
				}
				if (cfg.times) {
					setTimeout(function() {
						$("#dialog_div").hide();
					}, cfg.times);
				}
			}
			if (panel) {
				panel.css({
					'position': 'fixed',
					'left': '0',
					'right': '0',
					'top': '0',
					'bottom': '0',
					'z-index': '99'
				});
				if ($("#dialog_div").length) {
					$("#dialog_div").remove();
				}
				panel.appendTo($('body'));
				(function(panel, cfg) {
					setTimeout(function() {
						if (cfg.CallBackOk) {
							$(panel.find('#jit_btn_ok')).bind('click', cfg.CallBackOk);
						}
						if (cfg.CallBackCancel) {
							$(panel.find('#jit_btn_cancel')).bind('click', cfg.CallBackCancel);
						} else {
							$(panel.find('#jit_btn_cancel')).bind('click', function() {
								UI.Dialog('CLOSE');
							});
						}
					}, 16);
				})(panel, cfg);
			}
			/*
                var dialogdom =$('[name=jitdialog]');
                dialogdom.css({
                    'left':(Jit.winSize.width-dialogdom.width())/2,
                    'top':(Jit.winSize.height-dialogdom.height())/2,
                });
                */
		}
	},
	Masklayer: {
		show: function() {
			if ($('#masklayer').length <= 0) {
				var mask = $('<div id="masklayer" style="position:fixed;background-color:#ECECEC;width:100%;height:100%;line-height:100%;z-index:9999;top:0;left:0;text-align:center"><img src="../../../images/common/loading.gif" style="margin:30px auto;" alt="" /></div>');
				mask.appendTo('body');
			}
			$('#masklayer').css({
				'opacity': '0.6'
			}).show();
		},
		hide: function() {
			$('#masklayer').hide();
		}
	},
	Loading: function(display) {
		if (display || arguments.length == 0) {
			$('body').append("<div id=\"wxloading\" class=\"wx_loading\"><div class=\"wx_loading_inner\"><i class=\"wx_loading_icon\"></i>正在加载...</div></div>");
		} else {
			$('#wxloading').remove();
		}
	},
	AjaxTips: {
		//显示ajax加载数据的时候   出现加载图标
		Loading: function(flag) {
			if (flag || arguments.length == 0) {
				//显示loading
				UI.Loading(true);
			} else {
				//隐藏loading
				UI.Loading(false);
			}
		},
		//加载数据
		Tips: function(options) {
			var left = "50%",
				top = "50%";
			if (options.left) {
				left = options.left;
			}
			if (options.top) {
				top = options.top;
			}
			if (options.show) { //显示tips
				if ($("#ajax__tips").length > 0) {
					$("#ajax__tips").remove();
				}
				var style = "position:fixed;top:" + top + ";  left:" + left + ";  width:100px;  height:100px;line-height:100px;  margin-top:-50px;margin-left:-50px;text-align: center;line-height100px;";
				var $div = $("<div id='ajax__tips' style='" + style + "'>" + (options.tips ? options.tips : "暂无数据") + "</div>");
				$("body").append($div);
			} else { //隐藏tips
				$("#ajax__tips").hide();
			}
		}
	},
	Image: {
		getSize: function(src, size) {
			return src;
			if (!src) {
				return '/HtmlApps/images/common/misspic.png';
			}
			var _src = src.replace(/(.png)|(.jpg)/, function(s) {
				return '_' + size + s;
			});
			return _src;
		}
	},
	showPicture: function(img) {
		var layer = $('<div class="pic-view"><div class="pic-view-close"><span></span></div></div>');
		layer.appendTo('body');
		layer.animate({
			opacity: 0.7,
		}, 'ease', function() {
			console.log(111)
		})
		/*
            $(img).animate({
                left : 100,
                opacity: 1,
                width:'150px',
                rotateZ:'0deg',
                translate3d: '0,10px,0'
            },'ease',function(){
                console.log(111)
            })
            */
	}
}
$(function() {
	JitPage.onPageLoad();
})