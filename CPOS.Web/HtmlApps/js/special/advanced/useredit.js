Jit.AM.defindPage({
	name: 'UserEdit',
	curCityList: [],
	curDataList:{'FollowTrade':[],'Trade':[]},
	onkeys:{editInfo:'onEditInfo'},
	elements: {},
	defaultValues: {
		empty: '无',
		secret: '保密'
	},
	options: {
		index: 0,
		isUpload: false, //防止重复上传
		imagesUrl: [], //提交时候带的数据
		uploadSuccess: false //是否图片都上传成功
	},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this,
			baseInfo = Jit.AM.getBaseAjaxParam();
		self.elements.btSwitchList = $('.commonBox .switch');
		self.elements.controlArea = $('.businessCardArea');
		self.elements.infoMenuList = $('.infoMenu span');
		self.elements.tabList = $('.tab');
		self.elements.txtUserName = $('#txtUserName');
		self.elements.imgUserHead = $('#imgUserHead');
		self.elements.txtEnglishName = $('#txtEnglishName');
		self.elements.txtPosition = $('#txtPosition');
		self.elements.txtClass = $('#txtClass');
		self.elements.txtMobile = $('#txtMobile');
		self.elements.txtEmail = $('#txtEmail');
		self.elements.txtCompany = $('#txtCompany');
		self.elements.txtCompanyEnglishName = $('#txtCompanyEnglishName');
		self.elements.txtBranch = $('#txtBranch');
		self.elements.txtCompanyUrl = $('#txtCompanyUrl');
		self.elements.txtCompanyPhone = $('#txtCompanyPhone');
		self.elements.txtAssistantMobile = $('#txtAssistantMobile');
		self.elements.txtAssistantEmail = $('#txtAssistantEmail');
		self.elements.txtFollowTrade = $('#txtFollowTrade');
		self.elements.txtSpecial = $('#txtSpecial');
		self.elements.txtMyIntroduce = $('#txtMyIntroduce');
		self.elements.txtBirthday = $('#txtBirthday');
		self.elements.txtNationality = $('#txtNationality');
		self.elements.txtNation = $('#txtNation');
		self.elements.txtContactAddress = $('#txtContactAddress');
		self.elements.txtHobby = $('#txtHobby');
		self.elements.txtBlog = $('#txtBlog');
		self.elements.txtSaifEmail = $('#txtSaifEmail');
		self.elements.txtEnrolDate = $('#txtEnrolDate');
		self.elements.txtGraduationDate = $('#txtGraduationDate');
		self.elements.txtTrade = $('#txtTrade');
		self.elements.txtWeiBo = $('#txtWeiBo');
		self.elements.txtSpreadPhone = $('#txtSpreadPhone');
		self.elements.txtWishActivityPosition = $('#txtWishActivityPosition');
		self.elements.txtAssistantName = $('#txtAssistantName');
		self.elements.fileUserFile = $('#fileUp');
		self.elements.userheadarea = $('.userheadarea');
		self.elements.btEditSave = $('#editSave');
		self.elements.txtCitys = $('#txtCitys');
		self.elements.txtNativePlace = $('#txtNativePlace');
		self.elements.btCity = $('#City');
		self.elements.txtOftenAddress = $('#OftenAddress');
		self.elements.moreSelectList = $('div[name=moreSelect]');
		self.elements.btEducation = $('#Education');
		self.elements.moreCityList = $('#moreCityList');
		self.elements.moreEducationList = $('#moreEducationList');
		self.elements.txtSocietyPosition=$('#txtSocietyPosition');
		self.elements.txtWeiXinNumber=$('#txtWeiXinNumber');
		self.elements.txtCompanyIntro=$('#txtCompanyIntro');
		Jit.UI.Loading(true);
		//加载用户信息
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				'action': 'getUserByID',
				'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
				'UserID': baseInfo.userId
			},
			success: function(result) {
				Jit.UI.Loading(false);
				if (result && result.ResultCode == 0 && result.Data.Pages) {
					self.pageDataInfo = new PageDataInfo(result.Data.Pages);
					self.setPageInfo();
					 self.UpdateItemValues(result.Data.Pages);
					self.initValidate();
				};
			}
		});
		//加载行业信息
		if (!ControlMenu.Trade.Options ) {
			self.SetOptionInfo(ControlMenu.Trade.ControlType, ControlMenu.OptionName.Nationality, '', function(values) {
				self.curDataList.Trade = values;
				ControlMenu.Trade.Options = values;
			});
		}
		//加载关注行业
		if (!ControlMenu.FollowTrade.Options) {
			self.SetOptionInfo(107, ControlMenu.OptionName.Nationality, '', function(values) {
				self.curDataList.FollowTrade = values;
				ControlMenu.FollowTrade.Options = values;
			});
		}


		//加载国籍信息
		if (!ControlMenu.Nationality.Options) {
			self.SetOptionInfo(ControlMenu.Nationality.ControlType, ControlMenu.OptionName.Nationality, '', function(values) {
				ControlMenu.Nationality.Options = values;
			});
		}
		//加载城市信息
		if (!ControlMenu.City.Options) {
			self.SetOptionInfo(27, ControlMenu.OptionName.Nationality, '', function(values) {
				self.curCityList = values;
				ControlMenu.City.Options = values;
			});
		}
		//加成常来往信息
		if (!ControlMenu.OftenAddress.Options) {
			self.SetOptionInfo(28, ControlMenu.OptionName.Nationality, '', function(values) {
				ControlMenu.OftenAddress.Options = values;
			});
		}
		//加载兴趣爱好
		if (!ControlMenu.Hobby.Options) {
			self.SetOptionInfo(7, ControlMenu.OptionName.Interest, '', function(values) {
				ControlMenu.Hobby.Options = values;
			});
		}
		//加载个人专长
		if (!ControlMenu.Special.Options) {
			self.SetOptionInfo(7, ControlMenu.OptionName.Specialty, '', function(values) {
				ControlMenu.Special.Options = values;
			});
		}
		//我愿意在活动中担任
		if (!ControlMenu.WishActivityPosition.Options) {
			self.SetOptionInfo(8, ControlMenu.OptionName.Ositions, '', function(values) {
				ControlMenu.WishActivityPosition.Options = values;
			});
		}
	}, //获取数据列表
	SetOptionInfo: function(controlType, optionName, parentId, callback) {
		var self = this, options = [];
		Jit.AM.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				'action': 'getDataCollList',
				'optionName': optionName,
				'ParentID': parentId,
				'ControlType': controlType
			},
			success: function(result) {
				if (result && result.ResultCode == 0 && result.Data.Values) {
					
					for (var i = 0; i < result.Data.Values.length; i++) {
						var dataInfo = result.Data.Values[i];
						var optionInfo = {
							OptionID: dataInfo.ID || dataInfo.Text,
							OptionText: dataInfo.Text,
							IsSelected: 0
						};
						options.push(optionInfo);
					};
	
				};
			callback(options);
			},error:function(){
				callback(options)
			}
		});
	},
	//更新控件列表
	UpdateItemValues: function(pages) {
		for (var i = 0; i < pages.length; i++) {
			var pageItem = pages[i];
			for (var j = 0; j < pageItem.Blocks.length; j++) {
				var blockItem = pageItem.Blocks[j];
				if (!blockItem.Controls) {
					break;
				};
				for (var p = 0; p < blockItem.Controls.length; p++) {
					ControlMenu.UpdateItemValues(blockItem.Controls[p]);
				};
			}
		}
	},
	setPageInfo: function() {
		var self = this;
		self.elements.txtUserName.text(self.pageDataInfo.getValue(ControlMenu.VipName));
		self.elements.txtEnglishName.val(self.pageDataInfo.getValue(ControlMenu.EnglishName));
		self.elements.txtPosition.val(self.pageDataInfo.getValue(ControlMenu.Position));
		self.elements.imgUserHead.attr('src', self.pageDataInfo.getValue(ControlMenu.HeadImgUrl) || '../../../images/special/advanced/defaulthead.png');
		self.elements.txtMobile.val(self.pageDataInfo.getValue(ControlMenu.Mobile));
		self.elements.txtEmail.val(self.pageDataInfo.getValue(ControlMenu.Email));
		self.elements.txtClass.text(self.pageDataInfo.getValue(ControlMenu.Class) || self.defaultValues.empty);
		self.elements.txtCompany.val(self.pageDataInfo.getValue(ControlMenu.Company));
		self.elements.txtCompanyEnglishName.val(self.pageDataInfo.getValue(ControlMenu.CompanyEnglishName));
		self.elements.txtBranch.val(self.pageDataInfo.getValue(ControlMenu.Branch));
		self.elements.txtCompanyUrl.val(self.pageDataInfo.getValue(ControlMenu.CompanyUrl));
		self.elements.txtCompanyPhone.val(self.pageDataInfo.getValue(ControlMenu.CompanyPhone));
		self.elements.txtAssistantMobile.val(self.pageDataInfo.getValue(ControlMenu.AssistantMobile));
		self.elements.txtAssistantEmail.val(self.pageDataInfo.getValue(ControlMenu.AssistantEmail));
		self.elements.txtTrade.text(self.pageDataInfo.getValue(ControlMenu.Trade) || self.defaultValues.empty);
		self.elements.txtFollowTrade.text(self.pageDataInfo.getValue(ControlMenu.FollowTrade) || self.defaultValues.empty);
		self.elements.txtSpecial.text(self.pageDataInfo.getValue(ControlMenu.Special) || self.defaultValues.empty);
		self.elements.txtMyIntroduce.val(self.pageDataInfo.getValue(ControlMenu.MyIntroduce) || self.defaultValues.empty);
		self.elements.txtBirthday.val(self.pageDataInfo.getValue(ControlMenu.Birthday) || self.defaultValues.empty);
		self.elements.txtNationality.text(self.pageDataInfo.getValue(ControlMenu.Nationality) || self.defaultValues.empty);
		self.elements.txtNation.val(self.pageDataInfo.getValue(ControlMenu.Nation) || self.defaultValues.empty);
		self.elements.btCity.find('.text').text(self.pageDataInfo.getValue(ControlMenu.City) || self.defaultValues.empty);
		self.elements.txtOftenAddress.find('.text').text(self.pageDataInfo.getValue(ControlMenu.OftenAddress) || self.defaultValues.empty);
		self.elements.txtNativePlace.val(self.pageDataInfo.getValue(ControlMenu.NativePlace));
		self.elements.txtContactAddress.val(self.pageDataInfo.getValue(ControlMenu.ContactAddress) || self.defaultValues.empty);
		self.elements.txtHobby.text(self.pageDataInfo.getValue(ControlMenu.Hobby) || self.defaultValues.empty);
		self.elements.txtWeiBo.val(self.pageDataInfo.getValue(ControlMenu.WeiBo));
		self.elements.txtBlog.val(self.pageDataInfo.getValue(ControlMenu.Blog));
		self.elements.txtSaifEmail.val(self.pageDataInfo.getValue(ControlMenu.SaifEmail));
		self.elements.txtEnrolDate.text(self.pageDataInfo.getValue(ControlMenu.EnrolDate) || self.defaultValues.empty);
		self.elements.txtGraduationDate.text(self.pageDataInfo.getValue(ControlMenu.GraduationDate) || self.defaultValues.empty);
		self.elements.txtSpreadPhone.val(self.pageDataInfo.getValue(ControlMenu.SpreadPhone));
		self.elements.txtWishActivityPosition.text(self.pageDataInfo.getValue(ControlMenu.WishActivityPosition) || self.defaultValues.empty);
		self.elements.txtAssistantName.val(self.pageDataInfo.getValue(ControlMenu.AssistantName));
		self.elements.txtCitys.text(self.GetEditCityInfo() || self.defaultValues.empty);
		self.elements.btEducation.find('.text').text(self.pageDataInfo.getValue(ControlMenu.Education) || self.defaultValues.empty);
		self.elements.txtSocietyPosition.val(self.pageDataInfo.getValue(ControlMenu.SocietyPosition));
		self.elements.txtWeiXinNumber.val(self.pageDataInfo.getValue(ControlMenu.WeiXinNumber)|| self.defaultValues.empty);
		self.elements.txtCompanyIntro.val(self.pageDataInfo.getValue(ControlMenu.CompanyIntro)|| self.defaultValues.empty);
			
	},
	initValidate: function() { //初始化隐私选项
		var self = this;
		self.elements.btSwitchList.each(function() {
			var element = $(this),
				preItem = element.prev('.info'),
				key = preItem.attr('id');
			if (!key) {
				return false
			};
			privacyValue = self.pageDataInfo.getValue(ControlMenu[key], true);
			element.data(EditBoxHandle.keys.value, privacyValue);
			if (privacyValue && privacyValue == 1) {
				preItem.parents('.item').addClass('off');
			} else {
				preItem.parents('.item').removeClass('off');
			}
		});
	},
	bindEditBox: function(obj) {
		var self = this,
			element = $(obj),
			elementId = element.attr('id'),
			controlInfo = ControlMenu.GetItem(elementId);
		if (element.hasClass('notevent') || !controlInfo) {
			return false;
		};
		EditBoxHandle.init(controlInfo, function(values) {
			element.find('.text').html(values.Text.toString());
			controlInfo.value = values.ID.toString();
			controlInfo.Values=ControlMenu.ConvertToValues(values);
			element.data(EditBoxHandle.keys.value, controlInfo.value);
		});
	},
	initEvent: function() {
		var self = this;
		//下拉框编辑事件
		$('.commonBox .select').bind(self.eventType, function() {
			self.bindEditBox(this);
		});
		//文本框编辑事件
		$('.commonBox .info input').bind('change', function() {
			var element = $(this),
				value = element.val(),
				parentElement = element.parents('.info');
			parentElement.data(EditBoxHandle.keys.value, value);
		});
		//tab切换事件
		self.elements.infoMenuList.bind(self.eventType, function() {
			var element = $(this),
				index = self.elements.infoMenuList.index(this);
			self.elements.infoMenuList.removeClass('on');
			element.addClass('on');
			self.elements.tabList.hide();
			self.elements.tabList.eq(index).show();
		});
		//隐私开关事件
		self.elements.btSwitchList.bind(self.eventType, function() {
			var element = $(this),
				parentItem = element.prev('.info'),
				elementId = parentItem.attr('id'),
				controlInfo = ControlMenu.GetItem(elementId);
			controlInfo.PrivacyValue = element.data(EditBoxHandle.keys.value);;
			if (!controlInfo) {
				return false;
			};
			ConcealBoxHandle.init(controlInfo, function(values) {
				if (values && values == 1) {
					parentItem.parents('.item').addClass('off');
				} else {
					parentItem.parents('.item').removeClass('off');
				}
				element.data(EditBoxHandle.keys.value, values);
				controlInfo.PrivacyValue = values;
				var controls = [];
				controls.push(controlInfo);
				self.ajax({
					url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
					interfaeMode: 'V2.0',
					data: {
						'action': 'submitUserPrivacyByID',
						'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
						'Controls': controls,
					},
					success: function(result) {
						if (result && result.ResultCode == 0) {};
					}
				});
			});
			return false;
		});
		self.elements.imgUserHead.error(function() {
			this.src = '../../../images/special/advanced/defaulthead.png';
		});
		//点击上传头像
		self.elements.imgUserHead.parents('.item').bind(self.eventType, function() {
			// var element=$(this);
			self.elements.userheadarea.toggleClass('on');
		});
		//用户上传头像
		self.BindFileChange();
		//所在行业多选事件
		self.elements.txtTrade.parents('.info').bind(self.eventType, TradeEvent);
		//关注行业多选事件
		self.elements.txtFollowTrade.parents('.info').bind(self.eventType, TradeEvent);

		function TradeEvent() {
			var element = $(this),
				elementId = element.attr('id'),
				controlInfo = ControlMenu.GetItem(elementId);
			controlInfo.Options = self.curDataList[elementId];
			controlInfo.MaxSelected=1;//个人编辑中，所在行业可以多选
			EditBoxHandle.init(controlInfo, function(values) {
				element.find('.text').text(values.Text.toString());
				controlInfo.value = values.ID.toString();
				controlInfo.Values=ControlMenu.ConvertToValues(values);
				element.data(EditBoxHandle.keys.value, controlInfo.value);
				Jit.UI.Loading(1);
				self.SetOptionInfo(107, ControlMenu.OptionName.Nationality, values.ID.toString(), function(dataValues) {
					Jit.UI.Loading(0);
					if (dataValues && dataValues.length) {
						controlInfo.Options = dataValues;
						EditBoxHandle.init(controlInfo, function(subValues) {
						   values.ID=values.ID.toString()+','+subValues.ID.toString();
						   values.Text=values.Text.toString()+','+subValues.Text.toString();
							element.find('.text').text(values.Text.toString());
							controlInfo.value = values.ID.toString();
							controlInfo.Values=ControlMenu.ConvertToValues(values);
							element.data(EditBoxHandle.keys.value, values.ID.toString());
						});
					};
				});
			});
		}
		






		//城市联动事件
		self.elements.btCity.bind(self.eventType, function() {
			var element = $(this),
				elementId = element.attr('id'),
				controlInfo = ControlMenu.GetItem(elementId);
			controlInfo.Options = self.curCityList;
			EditBoxHandle.init(controlInfo, function(values) {
				self.elements.btCity.find('.text').text(values.Text.toString());
				controlInfo.value = values.ID.toString();
				self.elements.btCity.data(EditBoxHandle.keys.value, controlInfo.value);
				Jit.UI.Loading(1);
				self.SetOptionInfo(28, ControlMenu.OptionName.Nationality, values.ID.toString(), function(dataValues) {
					Jit.UI.Loading(0);
							if (dataValues&&dataValues.length<=1) {
								return false;
							};
					if (dataValues && dataValues.length) {
						controlInfo.Options = dataValues;
						EditBoxHandle.init(controlInfo, function(subValues) {
							self.elements.btCity.find('.text').text(values.Text.toString() + "、" + subValues.Text.toString());
							controlInfo.value = subValues.ID.toString();
							self.elements.btCity.data(EditBoxHandle.keys.value, controlInfo.value);
						});
					};
				});
			});
		});
		//编辑保存事件
		self.elements.btEditSave.bind(self.eventType, function() {

			var controlList = self.GetEditInfo();
			if (!controlList.length) {
				Jit.UI.Dialog({
					'content': '您未进行修改!',
					'type': 'Alert',
					'CallBackOk': function() {
						Jit.UI.Dialog('CLOSE');
					}
				});
				return false;
			};
			Jit.UI.Loading(1);
			self.ajax({
				url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
				interfaeMode: 'V2.0',
				data: {
					'action': 'submitUserByID',
					'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
					'Controls': controlList,
				},
				success: function(result) {
					Jit.UI.Loading(0);
					if (result && result.ResultCode == 0) {
						Jit.UI.Dialog({
							'content': '保存成功',
							'type': 'Alert',
							'CallBackOk': function() {
								Jit.UI.Dialog('CLOSE');
							}
						});
					} else {
						Jit.UI.Dialog({
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
		//选择出生年月[日期选择框事件]
		var opt = {},
			currYear = (new Date()).getFullYear();
		opt.date = {
			preset: 'date'
		};
		opt.datetime = {
			preset: 'datetime'
		};
		opt.time = {
			preset: 'time'
		};
		opt["default"] = {
			theme: 'android-ics light', //皮肤样式
			display: 'modal', //显示方式
			mode: 'scroller', //日期选择模式
			lang: 'zh',
			startYear: 1930, //开始年份
			endYear: currYear + 10, //结束年份,
			CallBack: function() { //回调函数
			self.elements.txtBirthday.parents('.info').data(EditBoxHandle.keys.value,self.elements.txtBirthday.val());
			}
		};
		self.elements.txtBirthday.val('').scroller('destroy').scroller($.extend(opt['date'], opt['default']));




		//多选项框编辑事件
		$('.morelist .editcancel').bind(self.eventType, function() {
			var element = $(this),
				parentElement = element.parents('.morelist');
				parentElement.hide();
				parentElement.trigger(self.onkeys.editInfo);
		});
		self.elements.moreSelectList.bind(self.eventType, function() {
			var element = $(this),
				selectBox = element.next('.morelist');
			selectBox.show();
		});
		self.elements.moreCityList.bind(self.onkeys.editInfo, function() {
			self.elements.txtCitys.text(self.GetEditCityInfo());
		});
		self.elements.moreEducationList.bind(self.onkeys.editInfo, function() {
			var editValue=self.GetEditEducationInfo();
			self.elements.btEducation.find('.text').text(editValue);
			self.elements.btEducation.data(EditBoxHandle.keys.value,editValue);
		})
		// end
	}, //获取编辑城市联动信息
	GetEditCityInfo: function() {
		var self = this,
			values = '',
			cityText = self.elements.btCity.find('.text').text(),
			oftenAddress = self.elements.txtOftenAddress.find('.text').text(),
			nativePlace = self.elements.txtNativePlace.val();
		if (nativePlace && nativePlace != self.defaultValues.empty) {
			values += nativePlace + "(籍贯)、";
		};
		if (cityText && cityText != self.defaultValues.empty) {
			values += cityText + "(常驻)、";
		};
		if (oftenAddress && oftenAddress != self.defaultValues.empty) {
			values += oftenAddress + "(常来往)、";
		};
		if (values.lastIndexOf('、')) {
			values = values.substring(0, values.lastIndexOf('、'));
		};
		return values;
	},
	GetEditEducationInfo: function() { //获取更多经历编辑信息
		var self = this,
			values = '';
		self.elements.moreEducationList.find('input').each(function() {
			var element = $(this),
				preElement = element.prev();
			if (element.val()) {
				values += element.val() + "(" + preElement.text() + ")、";
			};
		});
		if (values.lastIndexOf('、')) {
			values = values.substring(0, values.lastIndexOf('、'));
		};
		return values;
	},
	//获取编辑信息
	GetEditInfo: function() {
		var self = this,
			controlList = [];
		$('.commonBox .info').each(function() {
			var element = $(this),
				elementId = element.attr('id'),
				value = element.data(EditBoxHandle.keys.value);
			if (elementId && value) {
				var controlInfo = ControlMenu.GetItem(elementId, 1);
				controlInfo.value = value;
				controlInfo.Options = [];
				controlList.push(controlInfo);
			};
		});
		return controlList;
	},
	//绑定图片修改事件
	BindFileChange: function() {
		var self = this;
		var userAgent = navigator.userAgent.toLowerCase(),
			isIpad = userAgent.match(/ipad/i) == "ipad",
			isIphone = userAgent.match(/iphone os/i) == "iphone os";
		self.elements.fileUserFile.bind("change", function(e) {
			if (isIpad || isIphone) {
				self.iosUPfiles(e);
			} else {
				self.upFiles(e);
			}
		});
	},
	getUpFileUrl: function() {
		var self = this,
			baseInfo = Jit.AM.getBaseAjaxParam();
		return "/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx?Action=FileUpload&Type=Product&req={\"CustomerID\":\"" + baseInfo.customerId + "\",\"UserID\":\"" + baseInfo.userId + "\",\"Parameters\":{\"FilePath\":\"/File/gaojin/images/\",\"ImageName\":\"\",\"IsUpdate\":\"\",\"Field\":\"HeadImgUrl\"}}";
	},
	upFiles: function(e) { //普通上传图片，适用与安卓系统
		Jit.UI.Loading(1);
		var self = this,
			file = self.elements.fileUserFile[0];
		fu = new QuickUpload(file, {
			action: self.getUpFileUrl(),
			onFinish: function(iframe) {
				Jit.UI.Loading(0);
				try { //处理返回信息(需要后台配合)
					var result = eval("(" + iframe.contentWindow.document.body.innerHTML + ")");
					if (result.ResultCode == 0) {
						self.elements.imgUserHead.attr('src', result.Data.ImgUrl);
					} else {
						Jit.UI.Dialog({
							'content': result.Message,
							'type': 'Alert',
							'CallBackOk': function() {
								Jit.UI.Dialog('CLOSE');
							}
						});
					}
				} catch (ex) { //获取数据出错
					Jit.UI.Dialog({
						'content': "服务器繁忙，请稍后。",
						'type': 'Alert',
						'CallBackOk': function() {
							Jit.UI.Dialog('CLOSE');
						}
					});
				}
			},
		});
		var thefile = file.value,
			reg = /\.(jpe?g|gif|png|bmp)$/i;
		if (!reg.test(thefile)) {
			Jit.UI.Dialog({
				'content': "请选择图片jpg,png,bmp,gif!",
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
			return false;
		}
		fu.upload();
	},
	iosUPfiles: function(e) { //IOS上传图片
		Jit.UI.Loading(1);
		var me = JitPage;
		me.options.isUpload = false;
		if (window.File && window.FileList && window.FileReader && window.Blob) {} else {
			Jit.UI.Dialog({
				'content': '您的手机不支持FileAPI',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
			return false;
		}
		// e = e || window.event;
		//获取file input中的图片信息列表
		var f = me.elements.fileUserFile[0].files[0],
			reg = /\.(jpe?g|gif|png|bmp)$/i;
		//把这个if判断去掉后，也能上传别的文件
		if (!reg.test(f.name)) {
			Jit.UI.Dialog({
				'content': "您上传的文件格式不正确。",
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
		}
		var reader = new FileReader();
		//类似于原生JS实现tab一样（闭包的方法），参见http://www.css119.com/archives/1418
		reader.onload = (function(file) {
			//获取图片相关的信息
			var fileSize = (file.size / 1024).toFixed(2) + "K",
				fileName = file.name,
				fileType = file.type;
			return function(e) {
				var curImg = me.elements.imgUserHead[0];
				curImg.src = e.target.result;
				curImg.addEventListener("load", imgLoaded, false);

				function imgLoaded() {
					//插入图片
					curImg.src = e.target.result;
					me.upFile(file)
				}
			}
		})(f);
		//读取文件内容
		reader.readAsDataURL(f);
	},
	//上传文件
	upFile: function(singleImg) {
		var self = this;
		var xhr = new XMLHttpRequest();
		if (xhr.upload) {
			xhr.onreadystatechange = function(e) {
				if (xhr.readyState == 4) {
					Jit.UI.Loading(0);
					var result = {};
					if (xhr.status == 200) {
						result = eval("(" + xhr.responseText + ")");
					}
					if (xhr.status == 200 && result.ResultCode == 0) {
						self.elements.imgUserHead.attr('src'.result.Data.ImgUrl);
					} else {
						Jit.UI.Dialog({
							'content': "上传失败!请重新提交",
							'type': 'Alert',
							'CallBackOk': function() {
								Jit.UI.Dialog('CLOSE');
							}
						});
					}
				}
			};
			var formdata = new FormData();
			//模拟form的input的名称
			formdata.append("fileUp", singleImg);
			// 开始上传
			xhr.open("POST", self.getUpFileUrl(), true);
			xhr.send(formdata);
		}
	}
	//更新信息
	// UpdateInfo: function(controls) {
	// 	var self = this,
	// 		controlList = [];
	// 	controlList.push(controls);
	// 	self.ajax({
	// 		url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
	// 		interfaeMode: 'V2.0',
	// 		data: {
	// 			'action': 'submitUserByID',
	// 			'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
	// 			'Controls': controlList,
	// 		},
	// 		success: function(result) {
	// 			if (result && result.ResultCode == 0) {};
	// 		}
	// 	});
	// }
});
// 上传图片api
eval(function(p, a, c, k, e, r) {
	e = function(c) {
		return (c < 62 ? '' : e(parseInt(c / 62))) + ((c = c % 62) > 35 ? String.fromCharCode(c + 29) : c.toString(36))
	};
	if ('0'.replace(0, e) == 0) {
		while (c--) r[e(c)] = k[c];
		k = [

			function(e) {
				return r[e] || e
			}
		];
		e = function() {
			return '([3-59cf-hj-mo-rt-yCG-NP-RT-Z]|[12]\\w)'
		};
		c = 1
	};
	while (c--)
		if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]);
	return p
}('5 $$,$$B,$$A,$$F,$$D,$$E,$$S;(3(){5 O,B,A,F,D,E,S;O=3(id){4"1L"==1t id?P.getElementById(id):id};O.extend=3(G,10){H(5 Q R 10){G[Q]=10[Q]}4 G};O.deepextend=3(G,10){H(5 Q R 10){5 17=10[Q];9(G===17)continue;9(1t 17==="c"){G[Q]=I.callee(G[Q]||{},17)}J{G[Q]=17}}4 G};B=(3(K){5 b={11:/11/.x(K)&&!/1u/.x(K),1u:/1u/.x(K),1M:/webkit/.x(K)&&!/1v/.x(K),1N:/1N/.x(K),1v:/1v/.x(K)};5 1i="";H(5 i R b){9(b[i]){1i="1M"==i?"18":i;19}}b.18=1i&&1w("(?:"+1i+")[\\\\/: ]([\\\\d.]+)").x(K)?1w.$1:"0";b.ie=b.11;b.1O=b.11&&1y(b.18)==6;b.ie7=b.11&&1y(b.18)==7;b.1P=b.11&&1y(b.18)==8;4 b})(1z.navigator.userAgent.toLowerCase());A=3(){5 l={isArray:3(1Q){4 Object.1R.toString.L(1Q)==="[c 1S]"},1A:3(C,12,p){9(C.1A){4 C.1A(12)}J{5 M=C.1a;p=1T(p)?0:(p<0)?1j.1U(p)+M:1j.1V(p);H(;p<M;p++){9(C[i]===12)4 i}4-1}},1B:3(C,12,p){9(C.1B){4 C.1B(12)}J{5 M=C.1a;p=1T(p)||p>=M-1?M-1:p<0?1j.1U(p)+M:1j.1V(p);H(;p>-1;p--){9(C[i]===12)4 i}4-1}}};3 X(c,q){9(undefined===c.1a){H(5 f R c){9(w===q(c[f],f,c))19}}J{H(5 i=0,M=c.1a;i<M;i++){9(i R c){9(w===q(c[i],i,c))19}}}};X({1W:3(c,q,j){X.L(j,c,3(){q.Y(j,I)})},map:3(c,q,j){5 l=[];X.L(j,c,3(){l.1X(q.Y(j,I))});4 l},1k:3(c,q,j){5 l=[];X.L(j,c,3(1Y){q.Y(j,I)&&l.1X(1Y)});4 l},every:3(c,q,j){5 l=1b;X.L(j,c,3(){9(!q.Y(j,I)){l=w;4 w}});4 l},some:3(c,q,j){5 l=w;X.L(j,c,3(){9(q.Y(j,I)){l=1b;4 w}});4 l}},3(1Z,f){l[f]=3(c,q,j){9(c[f]){4 c[f](q,j)}J{4 1Z(c,q,j)}}});4 l}();F=(3(){5 1c=1S.1R.1c;4{bind:3(1l,j){5 1m=1c.L(I,2);4 3(){4 1l.Y(j,1m.20(1c.L(I)))}},bindAsEventListener:3(1l,j){5 1m=1c.L(I,2);4 3(g){4 1l.Y(j,[E.1d(g)].20(1m))}}}})();D={1n:3(m){5 13=m?m.21:P;4 13.22.23||13.24.23},1o:3(m){5 13=m?m.21:P;4 13.22.25||13.24.25},1C:3(a,b){4(u.1C=a.26?3(a,b){4!!(a.26(b)&16)}:3(a,b){4 a!=b&&a.1C(b)})(a,b)},v:3(m){5 o=0,N=0,T=0,U=0;9(!m.27||B.1P){5 n=m;while(n){o+=n.offsetLeft,N+=n.offsetTop;n=n.offsetParent};T=o+m.offsetWidth;U=N+m.offsetHeight}J{5 v=m.27();o=T=u.1o(m);N=U=u.1n(m);o+=v.o;T+=v.T;N+=v.N;U+=v.U};4{"o":o,"N":N,"T":T,"U":U}},clientRect:3(m){5 v=u.v(m),1D=u.1o(m),1E=u.1n(m);v.o-=1D;v.T-=1D;v.N-=1E;v.U-=1E;4 v},28:3(k){4(u.28=P.1p?3(k){4 P.1p.29(k,2a)}:3(k){4 k.1q})(k)},2b:3(k,f){4(u.2b=P.1p?3(k,f){5 h=P.1p.29(k,2a);4 f R h?h[f]:h.getPropertyValue(f)}:3(k,f){5 h=k.1q;9(f=="Z"){9(/1F\\(Z=(.*)\\)/i.x(h.1k)){5 Z=parseFloat(1w.$1);4 Z?Z/2c:0}4 1};9(f=="2d"){f="2e"}5 l=h[f]||h[S.1G(f)];9(!/^\\-?\\d+(px)?$/i.x(l)&&/^\\-?\\d/.x(l)){h=k.h,o=h.o,2g=k.1H.o;k.1H.o=k.1q.o;h.o=l||0;l=h.pixelLeft+"px";h.o=o;k.1H.o=2g}4 l})(k,f)},setStyle:3(1e,h,14){9(!1e.1a){1e=[1e]}9(1t h=="1L"){5 s=h;h={};h[s]=14}A.1W(1e,3(k){H(5 f R h){5 14=h[f];9(f=="Z"&&B.ie){k.h.1k=(k.1q.1k||"").2h(/1F\\([^)]*\\)/,"")+"1F(Z="+14*2c+")"}J 9(f=="2d"){k.h[B.ie?"2e":"cssFloat"]=14}J{k.h[S.1G(f)]=14}}})}};E=(3(){5 1f,1g,15=1;9(1z.2i){1f=3(r,t,y){r.2i(t,y,w)};1g=3(r,t,y){r.removeEventListener(t,y,w)}}J{1f=3(r,t,y){9(!y.$$15)y.$$15=15++;9(!r.V)r.V={};5 W=r.V[t];9(!W){W=r.V[t]={};9(r["on"+t]){W[0]=r["on"+t]}}W[y.$$15]=y;r["on"+t]=1r};1g=3(r,t,y){9(r.V&&r.V[t]){delete r.V[t][y.$$15]}};3 1r(){5 1s=1b,g=1d();5 W=u.V[g.t];H(5 i R W){u.$$1r=W[i];9(u.$$1r(g)===w){1s=w}}4 1s}}3 1d(g){9(g)4 g;g=1z.g;g.pageX=g.clientX+D.1o();g.pageY=g.clientY+D.1n();g.target=g.srcElement;g.1J=1J;g.1K=1K;switch(g.t){2j"mouseout":g.2k=g.toElement;19;2j"mouseover":g.2k=g.fromElement;19};4 g};3 1J(){u.cancelBubble=1b};3 1K(){u.1s=w};4{"1f":1f,"1g":1g,"1d":1d}})();S={1G:3(s){4 s.2h(/-([a-z])/ig,3(all,2l){4 2l.toUpperCase()})}};9(B.1O){try{P.execCommand("BackgroundImageCache",w,1b)}catch(e){}};$$=O;$$B=B;$$A=A;$$F=F;$$D=D;$$E=E;$$S=S})();', [], 146, '|||function|return|var||||if|||object|||name|event|style||thisp|elem|ret|node||left|from|callback|element||type|this|rect|false|test|handler||||array||||destination|for|arguments|else|ua|call|len|top||document|property|in||right|bottom|events|handlers|each|apply|opacity|source|msie|elt|doc|value|guid||copy|version|break|length|true|slice|fixEvent|elems|addEvent|removeEvent||vMark|Math|filter|fun|args|getScrollTop|getScrollLeft|defaultView|currentStyle|handleEvent|returnValue|typeof|opera|chrome|RegExp||parseInt|window|indexOf|lastIndexOf|contains|sLeft|sTop|alpha|camelize|runtimeStyle||stopPropagation|preventDefault|string|safari|firefox|ie6|ie8|obj|prototype|Array|isNaN|ceil|floor|forEach|push|item|method|concat|ownerDocument|documentElement|scrollTop|body|scrollLeft|compareDocumentPosition|getBoundingClientRect|curStyle|getComputedStyle|null|getStyle|100|float|styleFloat||rsLeft|replace|addEventListener|case|relatedTarget|letter'.split('|'), 0, {}));
var QuickUpload = function(file, options) {
	this.file = $$(file);
	this._sending = false; //是否正在上传
	this._timer = null; //定时器
	this._iframe = null; //iframe对象
	this._form = null; //form对象
	this._inputs = {}; //input对象
	this._fFINISH = null; //完成执行函数
	$$.extend(this, this._setOptions(options));
};
QuickUpload._counter = 1;
QuickUpload.prototype = {
	//设置默认属性
	_setOptions: function(options) {
		this.options = { //默认值
			action: "", //设置action
			timeout: 0, //设置超时(秒为单位)
			parameter: {}, //参数对象
			onReady: function() {}, //上传准备时执行
			onFinish: function() {}, //上传完成时执行
			onStop: function() {}, //上传停止时执行
			onTimeout: function() {} //上传超时时执行
		};
		return $$.extend(this.options, options || {});
	},
	//上传文件
	upload: function() {
		//停止上一次上传
		this.stop();
		//没有文件返回
		if (!this.file || !this.file.value) return;
		//可能在onReady中修改相关属性所以放前面
		this.onReady();
		//设置iframe,form和表单控件
		this._setIframe();
		this._setForm();
		this._setInput();
		//设置超时
		if (this.timeout > 0) {
			this._timer = setTimeout($$F.bind(this._timeout, this), this.timeout * 1000);
		}
		//开始上传
		this._form.submit();
		this._sending = true;
	},
	//设置iframe
	_setIframe: function() {
		if (!this._iframe) {
			//创建iframe
			var iframename = "QUICKUPLOAD_" + QuickUpload._counter++,
				iframe = document.createElement($$B.ie ? "<iframe name=\"" + iframename + "\">" : "iframe");
			iframe.name = iframename;
			iframe.style.display = "none";
			//记录完成程序方便移除
			var finish = this._fFINISH = $$F.bind(this._finish, this);
			//iframe加载完后执行完成程序
			if ($$B.ie) {
				iframe.attachEvent("onload", finish);
			} else {
				iframe.onload = $$B.opera ? function() {
					this.onload = finish;
				} : finish;
			}
			//插入body
			var body = document.body;
			body.insertBefore(iframe, body.childNodes[0]);
			this._iframe = iframe;
		}
	},
	//设置form
	_setForm: function() {
		if (!this._form) {
			var form = document.createElement('form'),
				file = this.file;
			//设置属性
			$$.extend(form, {
				target: this._iframe.name,
				method: "post",
				encoding: "multipart/form-data"
			});
			//设置样式
			$$D.setStyle(form, {
				padding: 0,
				margin: 0,
				border: 0,
				backgroundColor: "transparent",
				display: "inline"
			});
			//提交前去掉form
			file.form && $$E.addEvent(file.form, "submit", $$F.bind(this.dispose, this));
			//插入form
			file.parentNode.insertBefore(form, file).appendChild(file);
			this._form = form;
		}
		//action可能会修改
		this._form.action = this.action;
	},
	//设置input
	_setInput: function() {
		var form = this._form,
			oldInputs = this._inputs,
			newInputs = {},
			name;
		//设置input
		for (name in this.parameter) {
			var input = form[name];
			if (!input) {
				//如果没有对应input新建一个
				input = document.createElement("input");
				input.name = name;
				input.type = "hidden";
				form.appendChild(input);
			}
			input.value = this.parameter[name];
			//记录当前input
			newInputs[name] = input;
			//删除已有记录
			delete oldInputs[name];
		}
		//移除无用input
		for (name in oldInputs) {
			form.removeChild(oldInputs[name]);
		}
		//保存当前input
		this._inputs = newInputs;
	},
	//停止上传
	stop: function() {
		if (this._sending) {
			this._sending = false;
			clearTimeout(this._timer);
			//重置iframe
			if ($$B.opera) { //opera通过设置src会有问题
				this._removeIframe();
			} else {
				this._iframe.src = "";
			}
			this.onStop();
		}
	},
	//销毁程序
	dispose: function() {
		this._sending = false;
		clearTimeout(this._timer);
		//清除iframe
		if ($$B.firefox) {
			setTimeout($$F.bind(this._removeIframe, this), 0);
		} else {
			this._removeIframe();
		}
		//清除form
		this._removeForm();
		//清除dom关联
		this._inputs = this._fFINISH = this.file = null;
	},
	//清除iframe
	_removeIframe: function() {
		if (this._iframe) {
			var iframe = this._iframe;
			$$B.ie ? iframe.detachEvent("onload", this._fFINISH) : (iframe.onload = null);
			document.body.removeChild(iframe);
			this._iframe = null;
		}
	},
	//清除form
	_removeForm: function() {
		if (this._form) {
			var form = this._form,
				parent = form.parentNode;
			if (parent) {
				parent.insertBefore(this.file, form);
				parent.removeChild(form);
			}
			this._form = this._inputs = null;
		}
	},
	//超时函数
	_timeout: function() {
		if (this._sending) {
			this._sending = false;
			this.stop();
			this.onTimeout();
		}
	},
	//完成函数
	_finish: function() {
		if (this._sending) {
			this._sending = false;
			this.onFinish(this._iframe);
		}
	}
}
// end