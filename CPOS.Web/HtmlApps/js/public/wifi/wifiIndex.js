Jit.AM.defindPage({
	name: 'Init',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.txtWifiArea = $('#wifiarea');
		self.elements.txtNotWifiArea = $('#notwifiarea');

		var timer = setInterval(function(){

			if(typeof WeixinJSBridge != "undefined"){
				
				self.setPageInfo();
				
				clearInterval(timer);
			}
		},200);
		self.setPageInfo();
	},
	setPageInfo: function() {
		//event=LoginBefore&skey=9a064b03ee5cced4a725945f0ef475fc39eab96f
		var self = this,
			sKey = self.getUrlParam('skey'),
			evend = self.getUrlParam('event');
		self.elements.txtWifiArea.show();
		self.elements.txtNotWifiArea.hide();
		var baseInfo = self.getBaseInfo();
		self.elements.txtStore = $('#txtStore');
		self.elements.txtStoreAddress = $('#txtStoreAddress');
		self.elements.txtUserName = $('#txtUserName');
		self.elements.txtOfficeName = $('#txtOfficeName');

		alert('openId='+baseInfo.openId+',userId='+baseInfo.userId+',skey='+sKey+',evend='+evend);
		if (!sKey || !evend) {
			self.elements.txtStore.html('很抱歉，当前无法获取到信号信息。');
			return false;
		};
		if (evend == "LoginBefore") {
			location.href = "http://114.215.180.111/API/UserSession/" + sKey + "/UserLoginOk/180/";
			return false;
		}
		//LoginAfter=evend
		Jit.UI.Loading(true);
		//加载验证
		self.ajax({
			interfaceMode: 'V2.0',
			url: '/ApplicationInterface/Gateway.ashx',
			data: {
				'action': 'VIP.WifiSign.WifiSign',
				'DeviceID': sKey
			},
			success: function(result) {
				Jit.UI.Loading(false);
				if (result && result.ResultCode == 0 && result.Data.Items) {
					var itemInfo = result.Data.Items[0];
					self.elements.txtUserName.html(itemInfo.VipName);
					self.elements.txtStore.html(itemInfo.UnitName);
					self.elements.txtStoreAddress.html(itemInfo.LocationDesc);
				} else {
					alert(result.Message);
				}
			}
		});
	},
	initEvent: function() {
		var self = this;
	}
});