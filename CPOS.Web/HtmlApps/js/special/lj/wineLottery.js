Jit.AM.defindPage({

	name : 'winlottery',

	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入'+this.name);
		
		this.eventId = this.getUrlParam("eventId")?this.getUrlParam("eventId"):"5136826a98673f61fae80798e58f4197";
		if(!this.eventId){
			this.alert("未取到eventId");
			return false;
		}
		
		this.ajaxSending = false;
		this.stateContent = null;
		this.addressList = [];
		
		
		this.initEvent();
		this.loadData();
	},
	initEvent : function() {
		var self = this;
		$("#section").delegate("#checkBtn", "click", function() {
			$(this).siblings("#tools").addClass("rotateState");
			setTimeout(function() {
				self.getPrize();
			}, 1000);
		}).delegate("#addressBtn", "click", function() {
			//Jit.AM.toPage("FCaddress");
			if (self.addressList.length == 0) {
				Jit.AM.toPage("FCaddress");
			} else {
				var idata = self.addressList[0];
				self.setParams('editAddressData', idata);
				self.toPage('FCaddress', '&type=edit&addressId=' + idata.vipAddressID + '&province=' + encodeURIComponent(idata.province) + '&city=' + encodeURIComponent(idata.cityName));
			}
		}).delegate("#commonBtn", "click", function() {
			var name = Jit.trim($("#regName").val()),
				phone=Jit.trim($("#regPhone").val());
			if (name.length == 0) {
				self.alert("姓名不能为空");
				return false;
			} else if(phone.length==0){
				self.alert("手机号不能为空");
				return false;
			}else{
				self.setSignUp(name,phone,function(){
					//注册成功，进入红包判断
					self.judgeState();
				});
			}
		});
	},
	loadData:function(){
		this.judgeSignIn();
		//this.getAddress();
	},
	setSignUp:function(name,phone,callback){
		var self = this;
		if(!self.ajaxSending){
			this.ajax({
				url : '/OnlineShopping/data/Data.aspx',
				data : {
					'action' : 'setSignUpLzlj',
					'name' : name,
					'phone': phone,
					'eventId':self.eventId
				},
				beforeSend:function(){
					self.ajaxSending=true;
				},
				complete:function(){
					self.ajaxSending=false;
				},
				success : function(data) {
					if (data.code == 200) {
						if(callback){
							callback();
						}
					}else{
						self.alert(data.description);
					}
				}
			});
		}
		
	},
	judgeSignIn : function() {
		var self = this;
		this.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : {
				'action' : 'checkSign',
				'eventId' : self.eventId
			},
			success : function(data) {
				if (data.code == 200) {
					if (data.content.isRegistered == 1&&data.content.isSigned==1) {
						//已签到，进入红包判断
						self.judgeState();
					} else {
						self.loadPageData("registerSec");
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	getAddress : function() {
		var self = this;
		var _data = {
			'action' : 'getVipAddressList',
			'page' : 1,
			'pageSize' : 99
		};
		this.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : _data,
			success : function(data) {
				if (data.code == 200) {
					var list = data.content.itemList;
					self.addressList = list;
				}
			}
		});
	},
	judgeState : function(callback) {
		var self = this;
		this.ajax({
			url : '/Lj/Interface/Data.aspx',
			data : {
				'action' : 'getEventPrizes',
				'eventId' : self.eventId
			},
			success : function(data) {
				if (data.code == 200) {
					self.stateContent = data.content;
					if (data.content.remark == "不要着急，现在还没到抽奖时间噢…") {
						self.alert(data.content.remark);
					} else {
						if (data.content.isLottery == 1 ) {						//isLottery		是否可以抽奖  1 = 是，0 = 否
							if (data.content.isWinning == 1) {
								self.loadPageData("winningSec");
							} else {
								self.loadPageData("loseSec");
							}
						} else {
							self.loadPageData("getPrizeSec");
						}
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	getPrize : function() {
		var self = this;
		this.ajax({
			url : '/Lj/Interface/Data.aspx',
			data : {
				'action' : 'setEventPrizes',
				'eventId' : self.eventId
			},
			success : function(data) {
				if (data.code == 200) {
					if (self.stateContent.isWinning == 1) {
						self.loadPageData("winningSec");
					} else {
						self.loadPageData("loseSec");
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	loadPageData : function(secName) {
		$("#section").html(template.render(secName));
	},
	alert:function(text,callback){
    	Jit.UI.Dialog({
			type : "Alert",
			content : text,
			CallBackOk : function() {
				Jit.UI.Dialog("CLOSE");
				if(callback){
					callback();
				}
			}
		});
    }
}); 