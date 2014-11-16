var ActivityBox = {
	elements: {},
	objects: {
		submitError: false,
		setings: {
			'action': 'getUserDefinedByUserID',
			TypeID: '2',
			'EventID': '',
			callback: function() { //点击回调函数
			}
		},
		strEmpty:''
	},
	tips: function(str, obj) {
		Jit.UI.Dialog({
			'content': str,
			'type': 'Alert',
			'CallBackOk': function() {
				Jit.UI.Dialog('CLOSE');
				if (obj) {
					obj.focus();
				};
			}
		});
	},
	init: function(userSetings) {
		var self = this;
		$.extend(self.objects.setings, userSetings);
		var self = this,
			curBody = $('body');
		curBody.find('#actBgShade').remove();
		curBody.find('#submitArea').remove();
		curBody.append("<div  class=\"actBgShade\" id=\"actBgShade\"></div><div  class=\"submit_area\" id=\"submitArea\"><div name=\"has\" class=\"thclose\"></div><div id=\"actboxinfo\"></div><div name=\"has\" class=\"sub\"><a id=\"actBoxCancel\" href=\"javascript:;\">取消</a>&nbsp;&nbsp;<a id=\"actBoxSubmit\" href=\"javascript:;\">提交</a></div></div>");
		self.elements.actBgShade = $('#actBgShade');
		self.elements.submitArea = $('#submitArea');
		self.elements.btActBoxCancel = $('#actBoxCancel');
		self.elements.btActBoxSubmit = $('#actBoxSubmit');
		self.elements.btActClose = curBody.find('.thclose');
		self.elements.actBoxInfo = $('#actboxinfo');
		//加载字段
		JitPage.ajax({
			url: '/dynamicinterface/data/data.aspx',
			data: {
				'action': self.objects.setings.action,
				'TypeID': self.objects.setings.TypeID,
				'EventID': self.objects.setings.EventID
			},
			success: function(data) {
				self.elements.actBoxInfo.empty();
				if (data && data.code == 200 && data.content.pageList) {
					self.elements.actBoxInfo.html(GetlistHtml(data.content.pageList));
					self.objects.controlDatas = data.content.pageList;;
					//修正样式
					$('.commonBox .comline').last().addClass('curline');
				} else {
					self.elements.actBoxInfo.html("很抱歉，没有找到提交信息。");

				}
			},
			error: function() {

				self.elements.actBoxInfo.html("很抱歉，没有找到提交信息。");
			}
		});


		//获取用户基础信息
		function GetlistHtml(pages) {
			//ControlType      类型：1:文本；4：日期；5：时间类型；6：下拉框；7 : 单选框 8: 多选框；9:超文本,10:密码类型
			if (!pages.length) {
				return self.objects.strEmpty;
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
								htmlList.appendFormat("<em class=\"commonTit\">{0}{1}</em>",controlItem.IsMustDo?hasItem : self.objects.strEmpty, controlItem.ColumnDesc);
								htmlList.appendFormat("<span class=\"wrapInput\"><input id=\"{0}\" maxlength=\"128\"  type=\"{2}\" value=\"{1}\"  ></span>", controlItem.ControlID, controlItem.Value ? controlItem.Value : self.objects.strEmpty,  GetInputType(controlItem.AuthType));
								htmlList.append("</p>");
								htmlList.append("<p class=\"comline\"></p>");
								break;
							case 6:
								htmlList.append("<p class=\"commonList\">");
								htmlList.appendFormat("<em class=\"commonTit\">{2}{0}</em><span class=\"wrapInput\"><select id=\"{1}\" >   ", controlItem.ColumnDesc, controlItem.ControlID, controlItem.IsMustDo ? hasItem : self.objects.strEmpty);
								for (var v = 0; v < controlItem.Options.length; v++) {
									var optionItem = controlItem.Options[v];
									htmlList.appendFormat(" <option value=\"{0}\" {2}>{1}</option>", optionItem.OptionID, optionItem.OptionText, optionItem.IsSelected ? selectChecked : self.objects.strEmpty);
								};
								htmlList.append("</select></span></p>");
								htmlList.append("<p class=\"comline\"></p>");
								break;
							case 9:
								htmlList.append("<div class=\"subtxt\">");
								htmlList.appendFormat("<div class=\"commonTitle\">{1}{0}</div>", controlItem.ColumnDesc, controlItem.IsMustDo ? hasItem : self.objects.strEmpty);
								htmlList.appendFormat("<div class=\"commonArea\"><textarea id=\"{0}\" maxlength=\"500\"  >{1}</textarea></div>", controlItem.ControlID, controlItem.Value ? controlItem.Value : self.objects.strEmpty);
								htmlList.append("</div>");
								break;
							case 10:
								htmlList.append("<p class=\"commonList\">");
								htmlList.appendFormat("<em class=\"commonTit\">{0}{1}</em>",controlItem.IsMustDo ? hasItem : self.objects.strEmpty, controlItem.ColumnDesc);
								htmlList.appendFormat("<span class=\"wrapInput\"><input id=\"{0}\" maxlength=\"128\"  type=\"password\" value=\"{1}\"  ></span>", controlItem.ControlID, controlItem.Value ? controlItem.Value : self.objects.strEmpty);
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


		//初始化事件
		self.initEvent();
	},
	initEvent: function() {
		var self = this;

		self.elements.btActBoxCancel.bind(JitPage.eventType, function() {
			self.hide();
		});
		self.elements.btActClose.bind(JitPage.eventType, function() {
			self.hide();
		});


		//绑定提交事件
		self.elements.btActBoxSubmit.bind(JitPage.eventType, function() {

			self.objects.submitError = false;
			var controList = GetControlInfos(1);
			if (self.objects.submitError || !controList) {
				return false;
			};

			JitPage.ajax({
				url: '/dynamicinterface/data/data.aspx',
				data: {
					'action': 'addEventInfo',
					'Control': controList,
					'ActivityID': self.objects.setings.EventID,
					'TicketID': self.objects.strem,
					'TicketPrice': self.objects.strem
				},
				beforeSend: function() {
					Jit.UI.Loading(1);
				},
				success: function(data) {
					Jit.UI.Loading(0);
					self.objects.setings.callback(data, controList)
				},
				error: function() {
					self.objects.setings.callback({
						"code": "1",
						"description": "服务器加载失败"
					});
				}
			});

		});

		function GetControlItem(page, controId) {
			for (var i = 0; i < self.objects.controlDatas.length; i++) {
				if (parseInt(self.objects.controlDatas[i].PageNum) == page) {
					for (var j = 0; j < self.objects.controlDatas[i].Block.length; j++) {
						var subBlock = self.objects.controlDatas[i].Block[j];
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
		}


		function GetControlInfos(pageNumber) {
			curPages = $('#page' + pageNumber);
			var controList = [];
			$('input,select,textarea', curPages).each(function() {
				if (self.objects.submitError) {
					return;
				};
				var item = $(this),
					controlInfo = {
						ControlId: item.attr('id'),
						ColumnName: self.objects.strem,
						Value: self.objects.strem
					},
					dataControlInfo = GetControlItem(pageNumber, controlInfo.ControlId);
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
					self.objects.submitError = true;
					//获取信息
					switch (parseInt(dataControlInfo.ControlType)) {
						case 1:
						case 9:
							self.tips("请输入" + dataControlInfo.ColumnDesc, item);
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
							self.objects.submitError = true;
							self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
							return false;
						};
						break;
					case 3:
						if (controlInfo.Value && !Validates.isDecimal(controlInfo.Value)) {
							self.objects.submitError = true;
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
							self.objects.submitError = true;
							self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
							return false;
						};
						break;
					case 7:
						if (controlInfo.Value && !Validates.isPhone(controlInfo.Value)) {
							self.objects.submitError = true;
							self.tips('你输入的' + dataControlInfo.ColumnDesc + '格式不正确', item);
							return false;
						};
						break;
					case 8:
						if (controlInfo.Value && !Validates.isMobile(controlInfo.Value)) {
							self.objects.submitError = true;
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


	}, //显示
	show: function() {
		this.elements.actBgShade.show();
		this.elements.submitArea.show();

	}, //隐藏
	hide: function() {
		this.elements.actBgShade.hide();
		this.elements.submitArea.hide();

	}


}