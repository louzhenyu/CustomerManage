Jit.AM.defindPage({
	name: 'Home',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.homeWrapper = $('#homeWrapper');
		self.elements.indexNav = $('.indexNav');
		self.onBgSize();
		window.onresize = self.onBgSize;
        var param=this.pageParam;
		if(param.links){
			var length=param.links.length,finalLength=length;
			
			var html="";
			var logoList=[
				"../../../images/common/home_default/images4/logoIcon.png",
				"../../../images/common/home_default/images4/zxdtIcon.png",
				"../../../images/common/home_default/images4/hdzqIcon.png",
				"../../../images/common/home_default/images4/xkzsIcon.png"
			];
			for(var i=0;i<length;i++){
				var item=param.links[i];
				if(!param.links[i].title){finalLength--;continue;}
				var style="'";
				html+="<li>";
				if(item.backgroundColor){
					style+="background-color:"+item.backgroundColor+";";
				}
				if(item.backgroundImg){
					style+="background:"+"rgba(0,0,0,0.6) url("+item.backgroundImg+") no-repeat left 32px;"+"background-size:74px 188px";
				}else{
					var imgUrl=Math.floor(Math.random(3)*3);
					style+="background:"+"rgba(0,0,0,0.6) url("+logoList[imgUrl]+") no-repeat left 32px;"+"background-size:74px 188px";
				}
				style+="'";
				html+="<a href="+item.toUrl+" style="+style+"><span class='tit'>"+item.title+"</span></a></li>";
			}
			//每个li是74宽度
			$(".navList").css("width",finalLength*76);
			$("#list").html(html);
		}
		//设置背景
		var homeBg = $('.bgImgWrap img'),
			bgList =[
				{
					imgUrl:'../../../images/special/europe/indexBG.jpg'
				},
				{
					imgUrl:'../../../images/special/europe/indexBg2.jpg'
				},
				{
					imgUrl:'../../../images/special/europe/indexBg1.jpg'
				}
			];
		//设置多个背景图
		if(param.backgroundImgArr&&param.backgroundImgArr[0].imgUrl){
			bgList=param.backgroundImgArr;
		}
		homeBg.each(function(i) {
			$(this).attr('src', bgList[i].imgUrl);
		});
		 self.onBgImgSlider();
		 
		self.onMenuSlider();
		
	},
	onBgSize: function() {
		var w = document.body.offsetWidth,
			self = this;
		self.elements.homeWrapper.find('li').css('width', w);
		$(self.elements.homeWrapper.find('li')[0]).addClass("tm2");
	},
	//背景图片滑动
	onBgImgSlider: function() {
		var myScroll = new iScroll('homeWrapper', {
			snap: true,
			momentum: false,
			hScrollbar: false
		});
        //myScroll. scrollToElement("li:nth-child(0)",100);
        var i=0;
        setInterval(function() {
            console.debug(i) ;
              i<$("#homeWrapper li").length?i++:i=0;
            var el="li:nth-child(" +i+")";
            myScroll. scrollToElement(el);
        },2000);

	},
	//菜单可以左右滑动
	onMenuSlider:function(){
		var myScroll = new iScroll('menuWrapper', {
			snap:"li",
			momentum: true,
			hScroll: true
		});
		this.elements.indexNav.find("a").addClass("tm");
	},
	//绑定事件
	initEvent: function() {
		var self = this;

	}
});