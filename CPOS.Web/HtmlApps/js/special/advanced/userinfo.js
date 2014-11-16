Jit.AM.defindPage({
	name: 'UserInfo',
	toUserId: '',
	elements: {},
	isValidate: false, //是否有权限查看信息
	defaultValues: {
		empty: '无',
		secret: '未公开'
	},
	onPageLoad: function() {
		//console.log(222);
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this,
			baseInfo = Jit.AM.getBaseAjaxParam();
		self.elements.btSwitchList = $('.commonBox .switch');
		// self.elements.controlArea = $('.businessCardArea');
		self.elements.infoMenuList = $('.infoMenu span');
		self.elements.tabList = $('.tab');
		self.elements.txtUserName = $('#txtUserName');
		self.elements.imgUserHead = $('#imgUserHead');
		self.elements.txtEnglishName = $('#txtEnglishName');
		self.elements.txtPosition = $('#txtPosition');
				self.elements.tabTxtPosition = $('#tabTxtPosition');
		self.elements.txtClass = $('#txtClass');
		self.elements.txtMobile = $('#txtMobile');
		self.elements.txtEmail = $('span[name=txtEmail]');
		self.elements.txtCompany = $('#txtCompany');
		self.elements.tabTxtCompany = $('#tabTxtCompany');
		// self.elements.txtCompanyEnglishName = $('#txtCompanyEnglishName');
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
		self.elements.txtCity = $('#txtCity');
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
		self.elements.btTranspondBtn = $('.transpondBtn');
		self.elements.btCollectOrEdit = $('#btCollectOrEdit');
		self.elements.txtCompanyIntro=$('#txtCompanyIntro');
		self.elements.txtEducation=$('#txtEducation');
		self.elements.txtWeiXinNumber=$('#txtWeiXinNumber');
		self.elements.txtSocietyPosition=$('#txtSocietyPosition');
		self.elements.txtLearnItem=$('#txtLearnItem');
	
		self.toUserId = self.getUrlParam('toUserId');
		Jit.UI.Loading(true);


		//加载校友信息
		if (self.toUserId) {
			self.ajax({
				url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
				interfaeMode: 'V2.0',
				data: {
					'action': 'getAlumniByID',
					'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
					'AlumniID': self.toUserId
				},
				success: function(result) {
					Jit.UI.Loading(false);
					if (result && result.ResultCode == 0 && result.Data.Pages) {

						self.pageDataInfo = new PageDataInfo(result.Data.Pages);
						self.setPageInfo();
						if (result.Data.IsBookMark) {
							self.elements.btCollectOrEdit.html('已收藏');
							self.elements.btCollectOrEdit.addClass('on');
						};
					};
				}
			});
		} else { //加载个人信息
			self.ajax({
				url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
				interfaeMode: 'V2.0',
				data: {
					'action': 'getUserByID',
					'MobileModuleID': 'F6CD4DF9-1D51-42E2-8419-E6F050D30F96',
				},
				success: function(result) {
					Jit.UI.Loading(false);
					if (result && result.ResultCode == 0 && result.Data.Pages) {

						self.pageDataInfo = new PageDataInfo(result.Data.Pages);
						self.setPageInfo();
					};
				}
			});
		}
	},
	setPageInfo: function() {

		var self = this,englishName=self.pageDataInfo.getValue(ControlMenu.EnglishName);
		self.elements.txtUserName.text(self.pageDataInfo.getValue(ControlMenu.VipName));
		self.elements.txtEnglishName.text(englishName?"| "+englishName:'');
		self.elements.txtPosition.text(self.pageDataInfo.getValue(ControlMenu.Position));
		self.elements.tabTxtPosition.text(self.pageDataInfo.getValue(ControlMenu.Position)|| self.defaultValues.empty);
		self.elements.imgUserHead.attr('src', self.pageDataInfo.getValue(ControlMenu.HeadImgUrl|| self.defaultValues.emptyadImgUrl) || '../../../images/special/advanced/defaulthead.png');
		self.elements.imgUserHead.parent().attr('href',self.elements.imgUserHead.attr('src'));
		self.elements.txtMobile.text(self.pageDataInfo.getValue(ControlMenu.Mobile) || self.defaultValues.secret);
		self.elements.txtEmail.text(self.pageDataInfo.getValue(ControlMenu.Email) || self.defaultValues.empty);
		self.elements.txtClass.text(self.pageDataInfo.getValue(ControlMenu.Class) || self.defaultValues.empty);
		self.elements.txtCompany.text(self.pageDataInfo.getValue(ControlMenu.Company));
		self.elements.tabTxtCompany.text(self.pageDataInfo.getValue(ControlMenu.Company)|| self.defaultValues.empty);
		self.elements.txtCompanyIntro.text(self.pageDataInfo.getValue(ControlMenu.CompanyIntro)||self.defaultValues.empty);
		self.elements.txtEducation.text(self.pageDataInfo.getValue(ControlMenu.Education)||self.defaultValues.empty);
		self.elements.txtBranch.text(self.pageDataInfo.getValue(ControlMenu.Branch) || self.defaultValues.empty);
		self.elements.txtCompanyUrl.text(self.pageDataInfo.getValue(ControlMenu.CompanyUrl) || self.defaultValues.empty);
		self.elements.txtCompanyPhone.text(self.pageDataInfo.getValue(ControlMenu.CompanyPhone) || self.defaultValues.empty);
		self.elements.txtAssistantMobile.text(self.pageDataInfo.getValue(ControlMenu.AssistantMobile) || self.defaultValues.empty);
		self.elements.txtAssistantEmail.text(self.pageDataInfo.getValue(ControlMenu.AssistantEmail) || self.defaultValues.empty);
		self.elements.txtTrade.text(self.pageDataInfo.getValue(ControlMenu.Trade) || self.defaultValues.empty);
		self.elements.txtFollowTrade.text(self.pageDataInfo.getValue(ControlMenu.FollowTrade) || self.defaultValues.empty);
		self.elements.txtSpecial.text(self.pageDataInfo.getValue(ControlMenu.Special) || self.defaultValues.empty);
		self.elements.txtMyIntroduce.text(self.pageDataInfo.getValue(ControlMenu.MyIntroduce) || self.defaultValues.empty);
		self.elements.txtBirthday.text(self.pageDataInfo.getValue(ControlMenu.Birthday) || self.defaultValues.secret);
		self.elements.txtNationality.text(self.pageDataInfo.getValue(ControlMenu.Nationality) || self.defaultValues.empty);
		self.elements.txtNation.text(self.pageDataInfo.getValue(ControlMenu.Nation) || self.defaultValues.empty);
		self.elements.txtContactAddress.text(self.pageDataInfo.getValue(ControlMenu.ContactAddress) || self.defaultValues.secret);
		self.elements.txtHobby.text(self.pageDataInfo.getValue(ControlMenu.Hobby) || self.defaultValues.empty);
		self.elements.txtWeiBo.text(self.pageDataInfo.getValue(ControlMenu.WeiBo) || self.defaultValues.empty);
		self.elements.txtWeiXinNumber.text(self.pageDataInfo.getValue(ControlMenu.WeiXinNumber) || self.defaultValues.empty);
		self.elements.txtBlog.text(self.pageDataInfo.getValue(ControlMenu.Blog) || self.defaultValues.empty);
		self.elements.txtSaifEmail.text(self.pageDataInfo.getValue(ControlMenu.SaifEmail) || self.defaultValues.secret);
		self.elements.txtEnrolDate.text(self.pageDataInfo.getValue(ControlMenu.EnrolDate) || self.defaultValues.empty);
		self.elements.txtGraduationDate.text(self.pageDataInfo.getValue(ControlMenu.GraduationDate) || self.defaultValues.empty);
		self.elements.txtSpreadPhone.text(self.pageDataInfo.getValue(ControlMenu.SpreadPhone) || self.defaultValues.empty);
		self.elements.txtWishActivityPosition.text(self.pageDataInfo.getValue(ControlMenu.WishActivityPosition) || self.defaultValues.empty);
		self.elements.txtAssistantName.text(self.pageDataInfo.getValue(ControlMenu.AssistantName) || self.defaultValues.empty);
		self.elements.txtCity.text(self.GetEditCityInfo() || self.defaultValues.empty);
		self.elements.txtLearnItem.text(self.pageDataInfo.getValue(ControlMenu.LearnItem) || self.defaultValues.empty);
		self.elements.txtSocietyPosition.text(self.pageDataInfo.getValue(ControlMenu.SocietyPosition) || self.defaultValues.empty);
	
	}
	, //获取所在地信息
	GetEditCityInfo: function() {
		var self = this,
			values = '',
			cityText = self.pageDataInfo.getValue(ControlMenu.City),
			oftenAddress = self.pageDataInfo.getValue(ControlMenu.OftenAddress),
			nativePlace = self.pageDataInfo.getValue(ControlMenu.NativePlace);
		if (nativePlace) {
			values += nativePlace + "(籍贯)、";
		};
		if (cityText) {
			values += cityText + "(常驻)、";
		};
		if (oftenAddress) {
			values += oftenAddress + "(常来往)、";
		};
		if (values.lastIndexOf('、')) {
			values = values.substring(0, values.lastIndexOf('、'));
		};
		return values;
	},
	initEvent: function() {
		var self = this,
			baseInfo = Jit.AM.getBaseAjaxParam();
		if (!self.toUserId) {
			self.elements.btCollectOrEdit.html('编辑资料');
			self.elements.btCollectOrEdit.bind(self.eventType, function() {
				self.toPage('UserEdit');
			});
		} else {
			//点击收藏
			self.elements.btCollectOrEdit.bind(self.eventType, function() {
				var element = $(this);
				if (element.hasClass('on')) {
					return false;
				};
				self.ajax({
					url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
					interfaeMode: 'V2.0',
					data: {
						'action': 'setVipBookMarkInfo',
						'UserID': self.toUserId,
						'AlumniID': self.toUserId
					},
					success: function(result) {
						if (result && result.ResultCode == 0) {
							self.elements.btCollectOrEdit.html('已收藏');
							self.elements.btCollectOrEdit.addClass('on');
						};
					}
				});
			});
		}
		self.elements.btTranspondBtn.bind(self.eventType, function() {
			return Jit.UI.Dialog({
				'content': '功能正在开发中。',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
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
		self.elements.imgUserHead.error(function() {
			this.src = '../../../images/special/advanced/defaulthead.png';
		});
	}
});