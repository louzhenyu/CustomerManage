Jit.AM.defindPage({

	name:'CateHome',
	
	hideMask:function(){
		$("#masklayer").hide();
	},
	onPageLoad:function(){
		//当页面加载完成时触发
		Jit.log('进入'+this.name);
		var self = this;
		self.loaded = {};
		self.loadPageData();
		self.initPageEvent();
		
	},
	loadPageData:function(callback){
		var self =this;
		var topBannerTypeId = 'B28276DF6EC44F3A8644690BF4AC698E';
		var bottomInfoTypeId = '3A10353B1E3444F98944095ECA9C09D6';
		require(['/HtmlApps/js/common/teletext/news.js'],function(news){
			// load顶部信息
			$.news.getNewsList({NewsType:topBannerTypeId},function(data){
				self.renderPage("topBannerList",data);
				self.renderPage("dotList",data);
				self.loaded.topBanner = true;
				$(".touchslider-demo").touchSlider({
					container: this,
					duration: 350, // the speed of the sliding animation in milliseconds
					delay: 3000, // initial auto-scrolling delay for each loop
					margin: 0, // borders size. The margin is set in pixels.
					mouseTouch: true,
					namespace: "touchslider",
					pagination: ".touchslider-nav-item",
					currentClass: "touchslider-nav-item-current", // class name for current pagination item.
					autoplay: true, // whether to move from image to image automatically
					viewport: ".touchslider-viewport"
				});
				if(self.loaded.bottomInfo){
					new iScroll('section', {hScrollbar : false,vScrollbar : false});
					self.hideMask();
				}
			});
			
			// load 底部信息
			$.news.getNewsList({NewsType:bottomInfoTypeId,page:1,pageSize:5},function(data){
				self.renderPage("bottomInfoList",data);
				self.loaded.bottomInfo = true;
				if(self.loaded.topBanner){
					new iScroll('section', {hScrollbar : false,vScrollbar : false});
					self.hideMask();
				}
				
			});
		});
	},
	renderPage:function(selecter,data){
		$("#"+selecter).html(template.render(selecter+"Temp",{list:data}));
	},
	initPageEvent:function(){
		var self = this;
		$("#section").delegate(".newsListLink","tap",function(e){
			var typeId =$(this).data("typeid");
			Jit.AM.toPage("TeleTextList","typeId="+typeId);
		}).delegate(".newsLink","tap",function(e){
			var newsId =$(this).data("newsid");
			Jit.AM.toPage("TeleText","newsId="+newsId);
		}).delegate(".slinkTo","tap",function(e){
			Jit.AM.toPage($(this).data("page"));
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