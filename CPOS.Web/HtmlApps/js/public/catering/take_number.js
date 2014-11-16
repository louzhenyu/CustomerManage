Jit.AM.defindPage({

	name:'CateTakeNumber',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入页面：'+this.name);
		this.orderId = JitPage.getUrlParam("orderId");
		this.storeId = JitPage.getUrlParam("storeId");
		// if(!this.orderId){
			// this.hideMask();
			// this.alert("获取不到orderId,请检查URL！",function(){
				// Jit.AM.pageBack();
			// });
			// return false;
		// }
		// if(!this.storeId){
			// this.hideMask();
			// this.alert("获取不到storeId,请检查URL！",function(){
				// Jit.AM.pageBack();
			// });
			// return false;
		// }
		
		var self = this;
		self.loadPageData();
		self.initPageEvent();
		
	},
	loadPageData:function(callback){
		// var self =this;
		// self.ajax({
			// url:'/OnlineShopping/data/Data.aspx',
			// data:{
				// 'action':'getOrderList',
				// 'page':1,
				// 'pageSize':99,
				// 'orderId':self.orderId
			// },
			// success:function(data){
				// if(data.code==200){
					// if(callback){
						// callback(data.content);
					// }else{
						// self.renderPage(data.content.orderList[0]);
					// }
				// }else{
					// self.alert(data.description);
				// }
			// }
		// });
		this.state = Math.floor(Math.random()*4);
		this.renderPage(this.state);
	},
	renderPage:function(data){
		$("#section").html(template.render('state'+data));
		this.hideMask();
	},
	initPageEvent:function(){
		var self = this;
		$("#footer").delegate(".dishes","tap",function(){
			Jit.AM.toPage('CateList',"&orderId="+self.orderKey+"&storeId="+self.storeId);
		}).delegate(".rowNumber","tap",function(){
			Jit.AM.toPage('CateTakeNumber',"&orderId="+self.orderKey+"&storeId="+self.storeId);
		}).delegate(".book","tap",function(){
			Jit.AM.toPage('CateSeats',"&orderId="+self.orderKey+"&storeId="+self.storeId);
		}).delegate(".myOrder","tap",function(){
			Jit.AM.toPage('CateOrderList',"&storeId="+self.storeId);
		}).delegate("#totalCount","tap",function(){
			Jit.AM.toPage('CateOrder',"&storeId="+self.storeId+"&orderId="+self.orderKey);
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