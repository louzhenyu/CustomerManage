Jit.AM.defindPage({
	name:'TeleTextList',
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		Jit.log('进入'+this.name);
		this.typeId = JitPage.getUrlParam("typeId");
		this.loadPageData();
		if(!this.typeId){
			this.alert("未获取到typeId");
			return false;
		}
	},
	loadPageData:function(callback){
		var self =this;
		require(['/HtmlApps/js/common/teletext/news.js'],function(news){
			$.news.getNewsList({NewsType:self.typeId,page:1,pageSize:20},function(data){
				self.renderPage(data);
				self.hideMask();
			});
		});
	},
	renderPage:function(data){
		$("#newList ul").html(template('listTemp',{list:data}));
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