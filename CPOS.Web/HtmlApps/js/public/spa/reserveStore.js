Jit.AM.defindPage({

	name:'StoreList',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入'+this.name);
		var self = this;
		self.loadPageData();
		self.initPageEvent();
	},
	initPageEvent:function(){
		var self = this;
		$("#storeWwapper").delegate(".info","tap",function(){
			Jit.AM.toPage('CateList',"storeId="+$(this).data("storeid"));
		}).delegate(".location","tap",function(){
			var $this = $(this);
			Jit.AM.toPage('Map',"storeId="+$this.data("id")+"&lng="+$this.data("lng")+"&lat="+$this.data("lat")+"&addr="+$this.data("address")+"&store="+$this.data("storename"));
		});
	},
	loadPageData:function(){
		this.loadStore();
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
				self.hideMask();
				console.log("请求分类耗时"+(new Date().getTime()- self.timer)+"毫秒");
			},
			success:function(data){
				if(data.code==200){
					if(callback){
						callback(data.content);
					}else{
						if(data.content.storeList.length==0){
							Jit.UI.AjaxTips.Tips({show:true,tips:"该分类暂无数据"});
							$("#storeList").empty();
						}else{
							$("#storeList").html(template.render('storeListTemp',data.content));
							if(!self.storeWwapper){
								self.storeWwapper = new iScroll('storeWwapper', { hScrollbar: false, vScrollbar: false });	
							}else{
								self.storeWwapper.refresh();
							}
						}
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