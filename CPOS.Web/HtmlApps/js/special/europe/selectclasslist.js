Jit.AM.defindPage({
	name: 'SelectClassList',
	elements: {},
	objects: {
		keyValue: "value",
		keyClassId: 'classid',
		keyCourseId: 'courseid',
		keyClassInfo: 'ClassInfo',
		curDataList: [],
	},
	onPageLoad: function() {
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.selClassList = $('#classList');
		self.elements.selCourseList = $('#courseList');
		self.elements.elClassList = $('.classitems');

		Jit.UI.Loading(1);
		//加载年级信息
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				action: 'getOptionListByName',
				OptionName: 'ZOGrade',
				IsSort: 'true'
			},
			success: function(result) {
	


				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.Options) {
					var htmlList = new StringBuilder();
					for (var i = 0; i < result.Data.Options.length; i++) {
						var dataInfo = result.Data.Options[i];
						htmlList.appendFormat("<a  classid=\"{0}\" >{1}</a>", dataInfo.OptionID, dataInfo.OptionText);
					};
					self.elements.selClassList.find('>div>div').html(htmlList.toString());
				};
			}
		});

		//加载课程信息
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: {
				action: 'getCourseInfoList'
			},
			success: function(result) {
				Jit.UI.Loading(0);
				if (result && result.ResultCode == 0 && result.Data.CourseInfos) {
					var htmlList = new StringBuilder();
					for (var i = 0; i < result.Data.CourseInfos.length; i++) {
						var dataInfo = result.Data.CourseInfos[i];
						htmlList.appendFormat("<a  courseid=\"{0}\" >{1}</a>", dataInfo.CourseInfoID, dataInfo.CourseInfoName);
					};
					self.elements.selCourseList.find('>div>div').html(htmlList.toString());
				};
			}
		});
	},
	setClassList: function() {
		var self = this,
			dataParams = {
				action: 'GetInfoCollect',
				GradeVal: self.elements.selClassList.attr(self.objects.keyClassId),
				CourseInfoID: self.elements.selCourseList.attr(self.objects.keyCourseId),
				ClassInfoID: ''
			};
		self.ajax({
			url: '/ApplicationInterface/Product/Eclub/Module/CommonGateway.ashx',
			interfaeMode: 'V2.0',
			data: dataParams,
			success: function(result) {
				if (result && result.ResultCode == 0 && result.Data.Infos) {
					var htmlList = template.render('tpDataList', {
						'datas': result.Data.Infos
					});
					self.objects.curDataList = result.Data.Infos;
					self.elements.elClassList.html(htmlList);
				};
			}
		});

	},
	//绑定事件
	initEvent: function() {
		var self = this;
		//选择下拉框
		$('.classbox').bind(self.eventType, function() {
			var element = $(this),
				selectBox = element.find('.classoption');
			selectBox.show();
			return false;
		});

		$('.classoption').delegate('a', self.eventType, function() {
			var element = $(this),
				parentOption = element.parents('.classoption'),
				preElement = parentOption.prev();
			parentOption.find('a').removeClass('on');
			element.addClass('on');
			preElement.text(element.text());
			if (parentOption.attr('id') == "classList") {
				console.log(1);
				self.elements.selClassList.attr(self.objects.keyClassId, element.attr(self.objects.keyClassId));
			} else if (parentOption.attr('id') == "courseList") {
				console.log(0);
				self.elements.selCourseList.attr(self.objects.keyCourseId, element.attr(self.objects.keyCourseId));
			};
			self.setClassList();
			parentOption.hide();
			return false;
		});

		$(document).bind(self.eventType, function() {
			$('.classoption').hide();

			return false;
		});


		//点击班级
		$('.classitems').delegate('.itembox', self.eventType, function() {
			var element = $(this),
				vid = element.attr('id');
			var selectDataInfo = self.getDataInfo(vid);
			if (selectDataInfo) {
				self.setParams(self.objects.keyClassInfo, selectDataInfo);
				self.toPage('ClassDetail');
			};
		});


		var	classListScroll = new iScroll('classListArea', {
			// snap: true,
			// momentum: false,
			hScrollbar: false,
			vScrollbar: true,
			// hScroll: false,
			checkDOMChanges: true
		});

		var	courseListScroll = new iScroll('courseListArea', {
			// snap: true,
			// momentum: false,
			hScrollbar: false,
			vScrollbar: true,
			// hScroll: false,
			checkDOMChanges: true
		});



	},
	getDataInfo: function(vid) {
		var dataInfo = '';
		for (var i = 0; i < this.objects.curDataList.length; i++) {
			var data = this.objects.curDataList[i];
			if (data.ClassInfoID == vid) {
				dataInfo = data;
				break;
			};
		};
		return dataInfo;
	}
});