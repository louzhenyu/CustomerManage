Jit.AM.defindPage({

	name : 'CourseList',
	onPageLoad : function() {
		//当页面加载完成时触发
		
		this.typeId = JitPage.getUrlParam("typeId");
		if(!this.typeId){
			this.alert("URL获取不到typeId！",function(){
				Jit.AM.pageBack();
			});
			return false;
		}
		
		
		var self = this;
		Jit.log('页面进入' + this.name);
		this.loadPageData(function(){
			self.initPageEvent();
		});
	},
	initPageEvent : function() {
		$("#heroImage").touchSlider({
			container : this,
			duration : 350, // the speed of the sliding animation in milliseconds
			delay : 3000, // initial auto-scrolling delay for each loop
			margin : 0, // borders size. The margin is set in pixels.
			mouseTouch : true,
			namespace : "touchslider",
			//next : ".touchslider-next", // jQuery object for the elements to which a "scroll forwards" action should be bound.
			pagination : ".navItem",
			currentClass : "on", // class name for current pagination item.
			//prev : ".touchslider-prev", // jQuery object for the elements to which a "scroll backwards" action should be bound.
			//scroller : viewport.children(),
			autoplay : true, // whether to move from image to image automatically
			viewport : ".picList-viewport"
		});
		new iScroll('section', { hScrollbar: false, vScrollbar: false }); 
	},
	//加载数据
	loadPageData : function(callback) {
		var self=this;
		self.ajax({
			url:'/Project/CEIBS/CEIBSHandler.ashx',
			data:{
				action:'GetCourseInfo',
				type:self.typeId
			},
			beforeSend:function(){
			},
			success:function(data){
				if(data.code==200){
					$("#section").html(template.render("courseInfo",data.content));
					if(callback){
						callback();
					}
				}else{
					self.alert(data.description,function(){
						Jit.AM.pageBack();
					});
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

