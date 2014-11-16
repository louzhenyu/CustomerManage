Jit.AM.defindPage({
	name: 'SchoolSearch',
	CityList: [],
	elements: {},
	curCityList: [],
	curTradeList: [],
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.btSearchButtom = $('.searchBtn');
		self.elements.controlArea = $('.commonBox');
		self.elements.txtClass = $('#Class');
		self.elements.txtLearnItem = $('#LearnItem');
		self.elements.txtCity = $('#City');
		self.elements.txtTrade = $('#Trade');
		self.elements.btVipName=$('#VipName');
		self.elements.txtHobby = $('#Hobby');
		self.elements.txtCompany=$('#Company');

	

		//加载课程信息
		if (!ControlMenu.LearnItem.Options) {
			self.SetOptionInfo(102, ControlMenu.OptionName.Nationality, '', function(values) {
				ControlMenu.LearnItem.Options = values;
			});
		}

		
		
			//加载行业信息
		if (!ControlMenu.Trade.Options||!ControlMenu.FollowTrade.Options) {
			self.SetOptionInfo(ControlMenu.Trade.ControlType, ControlMenu.OptionName.Nationality, '', function(values) {
				self.curTradeList=values;
				ControlMenu.Trade.Options = values;
				ControlMenu.FollowTrade.Options=values;
			});
		}


				//加载城市信息
		if (!ControlMenu.City.Options) {
			self.SetOptionInfo(27, ControlMenu.OptionName.Nationality, '', function(values) {
				self.curCityList=values;
				ControlMenu.City.Options = values;
			});
		}
		//加载擅长数据
		if (!ControlMenu.Special.Options) {
			self.SetOptionInfo(7, ControlMenu.OptionName.Specialty, '', function(values) {
				ControlMenu.Special.Options = values;
			});
		}

		//加载兴趣爱好
		if (!ControlMenu.Hobby.Options) {
			self.SetOptionInfo(7, ControlMenu.OptionName.Interest, '', function(values) {
				ControlMenu.Hobby.Options = values;
			});
		}



	},
	SetOptionInfo: function(controlType, optionName, parentId, callback) {
		var self = this;
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
					var options = [];
					for (var i = 0; i < result.Data.Values.length; i++) {
						var dataInfo = result.Data.Values[i];
						var optionInfo = {
							OptionID: dataInfo.ID || dataInfo.Text,
							OptionText: dataInfo.Text,
							IsSelected: 0
						};
						options.push(optionInfo);
					};
					callback(options);
				};
			}
		});
	},

	 //绑定事件
	initEvent: function() {
		var self = this;
		//提交查询
		self.elements.btSearchButtom.bind(self.eventType, function() {
			SetTextValue();
			var controlList = self.GetControlList();
			self.setParams('SearchInfo', controlList);
			self.toPage('SchoolSearchDetail');
		});
		self.elements.controlArea.delegate('.item', self.eventType, function() {
			var element = $(this),
				elementId = element.attr('id'),
				controlInfo = ControlMenu.GetItem(elementId);
			if (element.hasClass('notevent')) {
				return false;
			};
			EditBoxHandle.init(controlInfo, function(values) {
				element.find('.text').html(values.Text.toString());
				element.data(EditBoxHandle.keys.value, values.ID.toString());
			});
		});
		//班级选择
		self.elements.txtClass.bind(self.eventType, function() {
			var element = $(this),courseInfoID = self.elements.txtLearnItem.data(EditBoxHandle.keys.value);
			courseInfoID=courseInfoID||'';
			Jit.UI.Loading(1);
			controlInfo = ControlMenu.GetItem(element.attr('id'));
			self.SetOptionInfo(ControlMenu.Class.ControlType, ControlMenu.OptionName.Nationality, courseInfoID, function(dataValues) {
							Jit.UI.Loading(0);
							if (dataValues&&!dataValues.length) {
															return Jit.UI.Dialog({
				'content': '很抱歉，您选择的课程暂无班级!',
				'type': 'Alert',
				'CallBackOk': function() {
					Jit.UI.Dialog('CLOSE');
				}
			});
							};
							controlInfo.Options = dataValues;
							EditBoxHandle.init(controlInfo, function(subValues) {
								element.find('.text').html(subValues.Text.toString());
								element.data(EditBoxHandle.keys.value, subValues.ID.toString());
							});
				});
		});
		//城市联动
		self.elements.txtCity.bind(self.eventType, function() {
			var element = $(this),
				elementId = element.attr('id'),
				controlInfo = ControlMenu.GetItem(elementId);
			controlInfo.ColumnDesc = "省份";
			controlInfo.Options = self.curCityList;
			EditBoxHandle.init(controlInfo, function(values) {
				element.find('.text').html(values.Text.toString());
				element.data(EditBoxHandle.keys.value, values.ID.toString());
				controlInfo.Values=ControlMenu.ConvertToValues(values);
				Jit.UI.Loading(1);
				self.SetOptionInfo(28, ControlMenu.OptionName.Nationality, values.ID.toString(), function(dataValues) {
							Jit.UI.Loading(0);
							if (dataValues&&dataValues.length<=1) {
								return false;
							};
							controlInfo.Options = dataValues;
							EditBoxHandle.init(controlInfo, function(subValues) {
								element.find('.text').html(subValues.Text.toString());
								element.data(EditBoxHandle.keys.value, subValues.ID.toString());
							});
				});

			});
		});
		//行业联动
		self.elements.txtTrade.bind(self.eventType, function() {
			var element = $(this),
				elementId = element.attr('id'),
				controlInfo = ControlMenu.GetItem(elementId);
			controlInfo.Options = self.curTradeList;
			EditBoxHandle.init(controlInfo, function(values) {
				element.find('.text').text(values.Text.toString());
				controlInfo.value = values.ID.toString();
				controlInfo.Values=ControlMenu.ConvertToValues(values);
				element.data(EditBoxHandle.keys.value, controlInfo.value);
				Jit.UI.Loading(1);
				self.SetOptionInfo(controlInfo.ControlType, ControlMenu.OptionName.Nationality, values.ID.toString(), function(dataValues) {
					Jit.UI.Loading(0);
					if (dataValues && dataValues.length) {
						controlInfo.Options = dataValues;
						EditBoxHandle.init(controlInfo, function(subValues) {
							element.find('.text').text(subValues.Text.toString());
							controlInfo.value = subValues.ID.toString();
							element.data(EditBoxHandle.keys.value, controlInfo.value);
						});
					};
				});
			});
		});

	function SetTextValue(){

		var txtVipNameValue=self.elements.btVipName.find('.text').val();
		if (txtVipNameValue) {
				self.elements.btVipName.data(EditBoxHandle.keys.value,txtVipNameValue);
		};
			var txtCompanyValue=self.elements.txtCompany.find('.text').val();
		if (txtCompanyValue) {
				self.elements.txtCompany.data(EditBoxHandle.keys.value,txtCompanyValue);
		};
	}


	//会员名称事件
	// self.elements.btVipName.find('.text').change(function(){
	// 	var element=$(this),value=element.val();
	// 		self.elements.btVipName.data(EditBoxHandle.keys.value,value);
	// });


	//公司名称事件
	// self.elements.txtCompany.find('.text').change(function(){
	// 	var element=$(this),value=element.val();
	// 		self.elements.txtCompany.data(EditBoxHandle.keys.value,value);
	// });


	},
	//获取填写的参数
	GetControlList: function() {
		var self = this,
			controlList = [];
		self.elements.controlArea.find('.item').each(function() {
			var element = $(this),
				elementId = element.attr('id'),
				value = element.data(EditBoxHandle.keys.value);
			if (elementId && value) {
				var controlInfo = ControlMenu.GetItem(elementId);
				controlInfo.value = value;
				controlList.push(controlInfo);
			};
		});
		return controlList;
	}
});