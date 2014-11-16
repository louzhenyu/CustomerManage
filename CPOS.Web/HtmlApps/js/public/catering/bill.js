Jit.AM.defindPage({

	name:'CateBill',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入bill.....');
		this.orderId = JitPage.getUrlParam("orderId");
		this.storeId = JitPage.getUrlParam("storeId");
		if(!this.orderId){
			this.hideMask();
			this.alert("获取不到orderId,请检查URL！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		if(!this.storeId){
			this.hideMask();
			this.alert("获取不到storeId,请检查URL！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		
		var self = this;
		self.loadPageData();
		self.initPageEvent();
		
	},
	loadPageData:function(callback){
		var self =this;
		self.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'getOrderList',
				'page':1,
				'pageSize':99,
				'orderId':self.orderId
			},
			success:function(data){
				if(data.code==200){
					if(callback){
						callback(data.content);
					}else{
						self.renderPage(data.content.orderList[0]);
					}
				}else{
					self.alert(data.description);
				}
			}
		});
	},
	renderPage:function(data){
		$("#sectionInfo").html(template.render('sectionInfoTemp',data));
		this.hideMask();
	},
	initPageEvent:function(){
		var self = this;
		$("#footer").delegate(".submit","tap",function(e){
			if(!$("#tableNo").val()){
				self.alert("请输入桌号",function(){
					$("#tableNo").focus();
				});
				return false;
			}
			self.setOrderStatus(function(){
				Jit.AM.toPage("OrderPay","&payReturn=CateOrderList&orderId="+self.orderId+"&storeId="+self.storeId);
			});
		});
	},
	setOrderStatus:function(callback){
		var self =this;
		self.ajax({
			url:'/OnlineShopping/data/Data.aspx',
			data:{
				'action':'setOrderStatus',
				'status':100,
				'orderId':self.orderId,
				'tableNo':$("#tableNo").val()
			},
			success:function(data){
				if(data.code==200){
					if(callback){
						callback(data.content);
					}else{
						self.renderPage(data.content.orderList[0]);
					}
				}else{
					self.alert(data.description);
				}
			}
		});
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