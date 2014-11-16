Jit.AM.defindPage({

	name : 'StoreList',

	hideMask : function() {
		$("#masklayer").hide();
	},
	onPageLoad : function() {
		//当页面加载完成时触发
		Jit.log('进入' + this.name);
		var self = this;
		self.minDistance = 3.5;
		
		self.loadPageData();
		self.initPageEvent();
	},
	initPageEvent : function() {
		var self = this;
		$("#storeWrapper").delegate(".info", "tap", function() {
			Jit.AM.toPage('CateList', "storeId=" + $(this).data("storeid"));
		}).delegate(".location", "tap", function() {
			var $this = $(this);
			Jit.AM.toPage('Map', "storeId=" + $this.data("id") + "&lng=" + $this.data("lng") + "&lat=" + $this.data("lat") + "&addr=" + $this.data("address") + "&store=" + $this.data("storename"));
		});
	},
	loadPageData : function() {
		var self = this;
		this.getLocation(function(position){
			console.log(JSON.stringify(position));
			self.loadStore(position);
		});
	},
	getLocation : function(callback) {
		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(callback);
		} else {
			this.alert("请允许定位服务，以便找到附近的门店");
		}
	},
	loadStore : function(position,callback) {
		var self = this;
		self.ajax({
			url : '/OnlineShopping/data/Data.aspx',
			data : {
				'action' : 'getStoreListByItem',
				'page' : 1,
				'pageSize' : 20,
				'longitude':position.coords.longitude,
				'latitude':position.coords.latitude
			},
			beforeSend : function() {
				self.timer = new Date().getTime();
				Jit.UI.AjaxTips.Tips({
					show : false
				});
			},
			complete : function() {
				self.hideMask();
				console.log("请求分类耗时" + (new Date().getTime() - self.timer) + "毫秒");
			},
			success : function(data) {
				if (data.code == 200) {
					if (callback) {
						callback(data.content);
					} else {
						if (data.content.storeList.length == 0) {
							Jit.UI.AjaxTips.Tips({
								show : true,
								tips : "该分类暂无数据"
							});
							$("#storeList").empty();
						} else {
							//对数据进行处理
							var list = [];
							for(var i=0;i<data.content.storeList.length;i++){
								var idata = data.content.storeList[i];
								if(parseFloat(idata.distance)-self.minDistance<=0){
									list.push(idata);
								}
							}
							if(list.length==0){
								Jit.UI.AjaxTips.Tips({
									show : true,
									tips : "附近"+self.minDistance+"km范围内没有门店"
								});
								$("#storeList").empty();
								return false;
							}
							
							$("#storeList").html(template.render('storeListTemp', data.content));
							//添加平滑滚动
							if (!self.storeWrapper) {
								self.storeWrapper = new iScroll('storeWrapper', {
									hScrollbar : false,
									vScrollbar : false
								});
							} else {
								self.storeWrapper.refresh();
							}
						}
					}
				} else {
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