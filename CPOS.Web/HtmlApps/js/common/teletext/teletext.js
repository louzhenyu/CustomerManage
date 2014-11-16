Jit.AM.defindPage({
	name:'TeleText',
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		Jit.log('进入'+this.name);
		this.newsId = JitPage.getUrlParam("newsId");
		if(!this.newsId){
			this.alert("未获取到newsId");
			return false;
		}
		this.loadPageData();
	},
	loadPageData:function(callback){
		var self =this;
		require(['/HtmlApps/js/common/teletext/news.js'],function(news){
			$.news.getNewsDetailByNewsID({newsId:self.newsId},function(data){
				self.renderPage(data);
				self.hideMask();
			});
		});
	},
	renderPage:function(data){
		$("title").html(data.News.NewsTitle);
		$("section").html(data.News.Content);
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