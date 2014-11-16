Jit.AM.defindPage({

	name : 'FClottery',

	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入Lottery.....');
		this.initEvent();
	},
	initEvent : function() {
		var self = this;
		this.ajaxSending = false;
		this.stateContent = null;
		this.addressList = [];
		this.eventId = this.getUrlParam("eventId")?this.getUrlParam("eventId"):"45486dd1c7a1de1231199eb41b04a669";
		if(!this.eventId){
			Jit.UI.Dialog({
				type : "Alert",
				content : "未取到eventId,取到的eventId为"+this.eventId,
				CallBackOk : function() {
					Jit.AM.toPage('FCsignIn');
				}
			});
		}
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

		});
		this.judgeSignIn();
		this.getAddress();
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
				if (!data) {
					Jit.log("服务器返回数据错误");
				} else {
					Jit.log("客户端已接收到数据");
					if (data.code == 200) {
						if (data.content.isSigned == 1) {
							self.judgeState();
							//已签到，进入红包判断
						} else {
							self.loadPageData("getPrizeSec");
							Jit.UI.Dialog({
								type : "Alert",
								content : "您未签到,现在就去签到！",
								CallBackOk : function() {
									Jit.AM.toPage('FCsignIn');
								}
							});
						}
					}
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
						Jit.UI.Dialog({
							type : "Alert",
							content : data.content.remark,
							CallBackOk : function() {
								Jit.AM.toPage('FCindex');
							}
						});
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
				}
			}
		});
	},
	loadPageData : function(secName) {
		$("#section").html(template.render(secName));
	}
}); 