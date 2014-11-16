Jit.AM.defindPage({

	name : 'CateSeats',
	storeId : '',
	elements : {
		btSelectCount : '',
		btSelectArea : '',
		txtCount : '',
		btBookSubmit : '',
		txtDate : '',
		txtTime : '',
		txtContacts : '',
		txtPhone : '',
		txtRemark : '',
		txtWeek : ''
	},
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		this.storeId = this.getUrlParam('storeId');
		this.orderId = this.getUrlParam('orderId');
		if(!this.storeId){
			console.log("URL获取不到storeId！");
			document.getElementById("storeArea").style.display = "block";
			this.loadStore(function(data){
				for(var i=0;i<data.storeList.length;i++){
					var idata = data.storeList[i];
					document.getElementById("storeSelect").options.add(new Option(idata.storeName,idata.storeId));
				}
			});
		}
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad : function() {
		var self = this;
		self.elements.btSelectArea = $('#btSelectArea');
		self.elements.btSelectCount = $('#btSelectCount');
		self.elements.txtCount = $('#txtCount');
		self.elements.btBookSubmit = $('.bookSubmitArea a');
		self.elements.txtDate = $('#txtDate');
		self.elements.txtTime = $('#txtTime');
		self.elements.txtContacts = $('#txtContacts');
		self.elements.txtPhone = $('#txtPhone');
		self.elements.txtRemark = $('#txtRemark');
		self.elements.txtWeek = $('#txtWeek');

		var optionHtml = [];
		for (var i = 1; i <= 20; i++) {
			optionHtml.push("<option value=\"" + i + "\">" + i + "</option>");
		};
		self.elements.btSelectCount.html(optionHtml.join(''));

	},
	initEvent : function() {
		var self = this;

		self.elements.btSelectCount.change(function() {
			self.elements.txtCount.html(self.elements.btSelectCount.val());
		});

		//选择性别事件
		var sexList = $('.bookContactsArea span');
		sexList.bind('click', function() {
			sexList.removeClass('on');
			$(this).addClass('on');
		});

		self.elements.btBookSubmit.bind('click', function() {
			self.SubmitSeats();
		});

		self.DateTimeEvent();

	},
	DateTimeEvent : function() {//日期事件
		var self = this, currYear = (new Date()).getFullYear(), opt = {};
		opt.date = {
			preset : 'date'
		};
		
		//opt.datetime = { preset : 'datetime', minDate: new Date(2012,3,10,9,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
		opt.datetime = {
			preset : 'datetime'
		};
		opt.time = {
			preset : 'time'
		};

			opt["default"] = {
				theme : 'android-ics light', //皮肤样式
				display : 'modal', //显示方式
				mode : 'scroller', //日期选择模式
				lang : 'zh',
				startYear : 1900, //开始年份
				endYear : currYear + 10, //结束年份,
				CallBack : function() {
	
				}
			};


	

		jQuery("#txtDate").val('').scroller('destroy').scroller(jQuery.extend(opt['date'], opt['default']));
		var optDateTime = jQuery.extend(opt['datetime'], opt['default']);
		var optTime = jQuery.extend(opt['time'], opt['default']);

		jQuery("#txtTime").mobiscroll(optTime).time(optTime);

	},
	SubmitSeats : function() {//提交订座
		var self = this, sexList = $('.bookContactsArea span');
		var datas = {
			'action' : 'setSeatOrderInfo',
			'storeId' : self.storeId?self.storeId:$("#storeSelect").val(),
			'orderId' : self.orderId,
			'qty' : parseInt(self.elements.btSelectCount.val()),
			'orderDate' : self.elements.txtDate.val(),
			'orderTime' : self.elements.txtTime.val(),
			'contact' : self.elements.txtContacts.val(),
			'gender' : -1,
			'tel' : self.elements.txtPhone.val(),
			'remark' : self.elements.txtRemark.val()
		};
		
		

		if (!datas.orderDate) {
			self.alert("请选择日期");
			return false;
		}

		if (!datas.orderTime) {
			self.alert("请选择时间");
			return false;
		}
		if(!datas.storeId){
			self.alert("请选择门店!");
			return false;
		}
		if (!datas.qty) {
			self.alert("请选择您的座位");
			return false;
		}

		if (!datas.contact) {
			self.alert("请输入联系人名称",function(){
				self.elements.txtContacts.focus();
			});
			return false;
		}

		if (!datas.tel) {
			self.alert("请输入手机号码",function(){
				self.elements.txtPhone.focus();
			});
			return false;
		} else if (!IsMobileNumber(datas.tel)) {
			self.alert("手机号码有误",function(){
				self.elements.txtPhone.focus();
			});
			return false;
		}

		sexList.each(function() {
			var el = $(this);
			if (el.hasClass('on')) {
				datas.gender = el.data('val');
				return false;
			};
		});
		self.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : datas,
			success : function(data) {
				if (data && data.code == 200) {
					self.alert("订座成功",function(){
						self.toPageWithParam('CateList','cateType=cateInner&orderId='+data.content.orderId+'&storeId='+datas.storeId);
					});
				} else {
					self.alert(data.description);
				}
			}
		});
	},
	loadStore:function(callback){
		var self=this;
		self.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getStoreListByItem',
				'page':1,
				'pageSize':20
			},
			beforeSend:function(){
				self.timer = new Date().getTime();
				Jit.UI.AjaxTips.Tips({show:false});
			},
			complete:function(){
				console.log("请求门店列表耗时"+(new Date().getTime()- self.timer)+"毫秒");
			},
			success:function(data){
				if(data.code==200){
					if(callback){
						callback(data.content);
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	alert : function(text, callback) {
		Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if (callback) {
					callback();
				}
			}
		});
	}
}); 