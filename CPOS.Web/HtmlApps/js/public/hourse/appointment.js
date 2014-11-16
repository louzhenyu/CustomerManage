Jit.AM.defindPage({
	name:"appointment",
	elements:{},
	onPageLoad : function() {
		this.initPage();
		this.initEvent();
	},
	initPage : function() {
		var me=this;
		//确定城市和门店的
		this.elements.section=$("#section");
		this.elements.selectFormArea=$(".selectFormArea");
		this.elements.affirmFormArea=$(".affirmFormArea");
		
		this.elements.name=$("#name");//姓名
		this.elements.phone=$("#phone");
		this.elements.sex=$('input[name="sex"]:checked');
		this.elements.txtDate = $('#txtDate');
		this.elements.txtTime = $('#txtTime');
		
		this.ajaxSending = false;
		
	},
	initEvent:function(){
		var self=this;
		self.alice(this.elements.selectFormArea);
		this.elements.section.delegate("#cityOKBtn","tap",function(){
			self.elements.selectFormArea.hide();
			self.elements.affirmFormArea.show();
			self.alice(self.elements.affirmFormArea);
			self.DateTimeEvent();
		}).delegate("#submit","tap",function(){
			self.submit();
		});
	},
	alice:function(ele){
		alice.init().slide({
			elems : ele,
			move : "up",
			duration : {
				"value" : "1000ms",
				"randomness" : "30%",
				"offset" : "100ms"
			},
			"overshoot" : "5"
		});
	},
	DateTimeEvent : function() {
		//日期事件
		var self = this, 
			currYear = (new Date()).getFullYear(),
			opt = {};
		//opt.datetime = { preset : 'datetime', minDate: new Date(2014,3,25,15,22), maxDate: new Date(2014,7,30,15,44), stepMinute: 5  };
		opt.datetime = {preset : 'datetime'};
		opt.date = {
			preset : 'date',
			minDate:new Date()
		};
		opt.time = {
			preset : 'time',
			minDate:new Date()
		};
		opt["default"] = {
			theme : 'android-ics light', //皮肤样式
			display : 'modal', //显示方式
			mode : 'scroller', //日期选择模式
			lang : 'zh',
			startYear : currYear - 10, //开始年份
			endYear : currYear + 10, //结束年份,
			CallBack : function(a,b,c) {
			}
		};

		$("#txtDate").mobiscroll().date($.extend(opt['date'], opt['default']));
		$("#txtTime").mobiscroll().time($.extend(opt['time'], opt['default']));

	},
	submit:function(){
		var self =this;
		if(!self.ajaxSending){
			if(!$.trim(self.elements.name.val())){
				self.alert("请输入姓名");
				return false;
			}
			if(!$.trim(self.elements.phone.val())){
				self.alert("请输入手机号");
				return false;
			}else if(!Jit.valid.isPhoneNumber(self.elements.phone.val())){
				self.alert("请输入正确的手机号");
				return false;
			}
			if(!$.trim(self.elements.txtDate.val())){
				self.alert("请选择预约日期");
				return false;
			}
			if(!$.trim(self.elements.txtTime.val())){
				self.alert("请选择预约时间");
				return false;
			}
			Jit.UI.Dialog({
				type : "Alert",
				content : "预约成功",
				CallBackOk : function() {
					Jit.UI.Dialog("CLOSE");
					Jit.AM.toPage("Landrover");
				}
			});
		}
	},
	getCitys:function(){
		var self =this;
		//获取省份+城市组合名称
		this.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : {
				'action' : 'getProvinceCityName'
			},
			success : function(data) {
				if (data.code == 200) {
					me.loadPageData(data.content);
				} else {
					self.alert(data.description);
					return false;
				}
			}
		});
	},
	alert:function(text,callback){
		Jit.UI.Dialog({
			'content' : text,
			'type' : 'Alert',
			'CallBackOk' : function() {
				Jit.UI.Dialog('CLOSE');
				if(callback){
					callback();
				}
			}
		});
	}
});
