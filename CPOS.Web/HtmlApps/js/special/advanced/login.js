Jit.AM.defindPage({
	name: 'Login',
	ValidationID: '',
	CourseList: '',
	keys: {
		value: 'value'
	},
	elements: {
		tipValCode: 'tipValCode',
	},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.txtUserName = $('#txtUserName');
		self.elements.txtMobileOrEmail = $('#txtMobileOrEmail');
		// self.elements.txtEmail = $('#txtEmail');
		self.elements.txtValCode = $('#txtValCode');
		self.elements.submitLogin = $('.registerBtn');
		self.elements.tipValCode = $('.qrCard');
		self.elements.selectCourseList = $('#selectCourseList');
		// self.elements.selectClassList = $('#selectClassList');
		self.elements.boxCourseList = $('#boxCourseList');
		// self.elements.boxClassList = $('#boxClassList');
		self.elements.selectBox = $('.selectbox');
		self.elements.selectBoxCancel = $('.selectcancel');
		self.elements.btGetPassword = $('#getPassword');
		self.elements.tabList = $('.pace a');
		self.elements.txtMoe=$('.moe');
		//获取课程列表
		// self.ajax({
		// 	url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
		// 	interfaeMode: 'V2.0',
		// 	data: {
		// 		'action': 'getCourseInfoList',
		// 	},
		// 	success: function(result) {
		// 		if (result && result.ResultCode == 0 && result.Data.CourseInfos) {
		// 			self.CourseList = result.Data.CourseInfos;
		// 			var htmlList = new StringBuilder();
		// 			for (var i = 0; i < result.Data.CourseInfos.length; i++) {
		// 				var dataInfo = result.Data.CourseInfos[i];
		// 				htmlList.appendFormat("<li data-value=\"{0}\">{1}</li>", dataInfo.CourseInfoID, dataInfo.CourseInfoName);
		// 			};
		// 			self.elements.boxCourseList.html(htmlList.toString());
		// 		};
		// 	}
		// });
	},
	// setClassList: function(courseInfoID) {
	// 	var self = this;
	// 	Jit.UI.Loading(true);
	// 	//获取班级列表
	// 	self.ajax({
	// 		url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
	// 		interfaeMode: 'V2.0',
	// 		data: {
	// 			'action': 'getClassInfoList',
	// 			'CourseInfoID': courseInfoID
	// 		},
	// 		success: function(result) {
	// 			Jit.UI.Loading(false);
	// 			if (result && result.ResultCode == 0 && result.Data.ClassInfos) {
	// 				var htmlList = new StringBuilder();
	// 				for (var i = 0; i < result.Data.ClassInfos.length; i++) {
	// 					var dataInfo = result.Data.ClassInfos[i];
	// 					htmlList.appendFormat("<li data-value=\"{0}\">{1}</li>", dataInfo.ClassInfoID, dataInfo.ClassInfoName);
	// 				};
	// 				self.elements.boxClassList.html(htmlList.toString());
	// 			};
	// 		}
	// 	});
	// },
	GetLoginInfo: function() {
		var self = this,
			datas = {
				mobileOrEmail: self.elements.txtMobileOrEmail.val(),
				valCode: self.elements.txtValCode.val(),
				vipName: self.elements.txtUserName.val(),
				// courseInfoID: self.elements.selectCourseList.data(self.keys.value),
				courseInfoID:'',
				isSelectEmail:function(){
					var result=0;
					self.elements.tabList.each(function(){
						var element=$(this),index=self.elements.tabList.index(this);
							if (element.hasClass('on')&&index==1) {
								result=1;
							};
					});
					return result;
				}
			};
		// if (!datas.courseInfoID) {
		// 	return Jit.UI.Dialog({
		// 		'content': '请选择课程!',
		// 		'type': 'Alert',
		// 		'CallBackOk': function() {
		// 			Jit.UI.Dialog('CLOSE');
		// 		}
		// 	});
		// }
		if (datas.isSelectEmail()&&datas.mobileOrEmail) {
			datas.mobileOrEmail=self.elements.txtMobileOrEmail.val()+'@saif.sjtu.edu.cn';
					// datas.mobileOrEmail=self.elements.txtMobileOrEmail.val();
		}

		if (!datas.mobileOrEmail) {
			return Jit.UI.Dialog({
				'content': '请输入您的手机或者邮箱!',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
		}
		if (!(Validates.isMobile(datas.mobileOrEmail) || Validates.isEmail(datas.mobileOrEmail))) {
			return Jit.UI.Dialog({
				'content': '您输入的手机或者邮箱格式有误！',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
					
				}
			});
		};
		if (!datas.vipName&&!datas.isSelectEmail()) {
			return Jit.UI.Dialog({
				'content': '请输入您的姓名',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
					self.elements.txtUserName.focus();
				}
			});
		};
		return datas;
	},
	//绑定事件
	initEvent: function() {
		var self = this;
		//提交登陆
		self.elements.submitLogin.bind(self.eventType, function() {
			var loginInfo = self.GetLoginInfo();
			if (!loginInfo || !loginInfo.mobileOrEmail) {
				return false
			};
			if (!loginInfo.isSelectEmail()&&!self.ValidationID) {
				Jit.UI.Dialog({
					'content': '请先获取验证码',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
				self.elements.txtValCode.focus();
				return false;
			};


			if (!loginInfo.valCode) {
				Jit.UI.Dialog({
					'content':loginInfo.isSelectEmail()?'请输入密码' :'请输入验证码',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
				self.elements.txtValCode.focus();
				return false;
			};
			Jit.UI.Loading(true);
			self.ajax({
				url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
				interfaeMode: 'V2.0',
				data: {
					'action': 'getUserByValidation',
					'ValidationID': self.ValidationID,
					'Code': loginInfo.valCode,
					'UserName':loginInfo.mobileOrEmail.replace('@saif.sjtu.edu.cn',''),
					'Pwd':loginInfo.valCode,
					'IsPwd':loginInfo.isSelectEmail()
				},
				success: function(result) {
					Jit.UI.Loading(false);
					if (result && result.ResultCode == 0&&result.Data.UserId) {
						//设置基础用户id Data
						var baseInfo = self.getBaseInfo();
						baseInfo.userId = result.Data.UserId;
						Jit.AM.setBaseAjaxParam(baseInfo,true);
						if (Jit.AM.hasHistory()) { //验证是否有浏览历史，如果没有跳转到指定页面
							self.pageBack();
						} else {
							self.toPage('Account');
						}
					} else {
						return Jit.UI.Dialog({
							'content': result.Message,
							'type': 'Alert',
							'CallBackOk': function() {
								Jit.UI.Dialog('CLOSE');
							}
						});
					}
				}
			});
		});
		//获取验证码
		self.elements.tipValCode.bind(self.eventType, function() {
			if (self.elements.tipValCode.hasClass('unenable')) {
			return false;

			};
			self.getVcode();
		});
		//选择课程联动
		// self.elements.selectCourseList.bind('change', function() {
		// 	var val = self.elements.selectCourseList.val();
		// 	self.setClassList(val);
		// });
		//选择课程和班级事件
		self.elements.selectCourseList.bind(self.eventType, function() {
			self.elements.selectBox.show();
			self.elements.boxCourseList.show();
			// self.elements.boxClassList.hide();
		});
		// self.elements.selectClassList.bind(self.eventType, function() {
		// 	self.elements.selectBox.show();
		// 	self.elements.boxCourseList.hide();
		// 	self.elements.boxClassList.show();
		// });
		self.elements.boxCourseList.delegate('li', self.eventType, function() {
			var element = $(this);
			self.elements.selectCourseList.data(self.keys.value, element.data(self.keys.value));
			self.elements.selectCourseList.html(element.html());
			self.elements.selectBox.hide();
			// self.setClassList(element.data(self.keys.value));
		});
		// self.elements.boxClassList.delegate('li', self.eventType, function() {
		// 	var element = $(this);
		// 	self.elements.selectClassList.data(self.keys.value, element.data(self.keys.value));
		// 	self.elements.selectClassList.html(element.html());
		// 	self.elements.selectBox.hide();
		// });
		self.elements.selectBoxCancel.bind(self.eventType, function() {
			self.elements.selectBox.hide();
		});
		//end
		//找回密码(临时,开发完成后可删除这段代码)
		self.elements.btGetPassword.bind(self.eventType, function() {
			Jit.UI.Dialog({
				'content': '请联系技术支持王海东(13817153474)',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
		});
		//tab切换事件
		self.elements.tabList.bind(self.eventType, function() {
			var element = $(this);
			self.elements.tabList.removeClass('on');
			element.addClass('on');
			if (self.elements.tabList.index(this) == 0) {
				self.elements.txtMobileOrEmail.attr('placeholder', '输入手机');
				self.elements.txtValCode.attr('placeholder','输入验证码');
				//self.elements.txtValCode.attr('placeholder','输入密码');
				self.elements.txtValCode.attr('type','text');
				self.elements.tipValCode.show();
				self.elements.txtMoe.removeClass('on');
			} else {
				self.elements.txtMobileOrEmail.attr('placeholder', '输入SAIF ID');
				self.elements.txtValCode.attr('placeholder','输入密码');
				self.elements.txtValCode.attr('type','password');
				self.elements.tipValCode.hide();
				self.elements.txtMoe.addClass('on');
			}
		});
	},
	getVcode: function() {
		var self = this,
			loginInfo = self.GetLoginInfo();
		if (!loginInfo || !loginInfo.mobileOrEmail) {
			return false
		};
		Jit.UI.Loading(1);
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				'action': 'getCodeByPhoneOrEmail',
				'LoginName': loginInfo.mobileOrEmail,
				'CourseInfoID': loginInfo.courseInfoID,
				// 'ClassInfoID': loginInfo.classInfoID,
				'VipName': loginInfo.vipName,
				'IsPhone': Validates.isMobile(loginInfo.mobileOrEmail),
				'Sign': '交大高级金融学院'
			},
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.ValidationID) {
					self.ValidationID = result.Data.ValidationID;
					self.countDown();
				} else {
					return Jit.UI.Dialog({
						'content': result.Message,
						'type': 'Alert',
						'CallBackOk': function() {
							Jit.UI.Dialog('CLOSE');
						}
					});
				}
			}
		});
	},
	countDown: function() {
		var self = this;
		self.elements.tipValCode.addClass('unenable');
		self.timeNum = 60;
		self.getCodeOnOff = false;
		self.timer = setInterval(function() {
			if (self.timeNum > 0) {
				self.elements.tipValCode.html(self.timeNum + '秒后重新获取');
				self.timeNum--;
			} else {
				self.getCodeOnOff = true;
				self.elements.tipValCode.html('重新获取验证');
				self.elements.tipValCode.removeClass('unenable');
				clearTimeout(self.timer);
				self.timer = null;
			}
		}, 1200);
	},
});