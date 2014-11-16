//UI工具
var UIBase = {
	middleShow: function(_settings) { //局中显示
		var settings = {
			obj: '',
			offsetLeft: 0, //偏移位置
			offsetTop: 0 //偏移位置
		};
		if (typeof(_settings) == 'string') {
			settings.obj = $(_settings);
		} else {
			$.extend(settings, _settings);
		}
		settings.obj.css('opacity', 0).show();
		var cssList = {
			left: '50%',
			top: '50%',
			'position': 'fixed',
			'margin-left': -Math.abs(settings.obj.width() / 2) - settings.offsetLeft,
			'margin-top': -Math.abs(settings.obj.height() / 2) - settings.offsetTop,
			'opacity': 1
		};
		settings.obj.css(cssList);
	}, //loading效果(基于CSS3动画)
	loading: {
		show: function() {
			$('body').append("<div id=\"wxloading\" class=\"wx_loading\"><div class=\"wx_loading_inner\"><i class=\"wx_loading_icon\"></i>正在加载...</div></div>");
		},
		hide: function() {
			$('#wxloading').remove();
		}
	}
	//自动隐藏loadding
};
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
			var linksDom=$(".indexNav a");  
			var length=param.links.length;
			for(var i=0;i<length;i++){
				var item=param.links[i];
					var doma=$(linksDom[i]);
					if(item.backgroundColor){
						doma.css({
							"background-color":item.backgroundColor
						});
					}
					if(item.backgroundImg){
						doma.css({
							"background":"rgba(255,255,255,0.6) url("+item.backgroundImg+") no-repeat",
							"background-size":"34px 30px",
							"background-position":"center 15px"
						});
					}
					doma.html(item.title);
					doma.attr("href",item.toUrl);
			}
		}
		if(param.menus){
			var menusDom=$(".menuWrap a");  
			var length=param.menus.length;
			for(var i=0;i<length;i++){
				var item=param.menus[i];
					var doma=$(menusDom[i]);

					if(item.backgroundImg){
						doma.css({
							"background":"url("+item.backgroundImg+") no-repeat",
							"background-size":"100%"
						});
					}
					if(item.backgroundColor){
						doma.css({
							"background-color":item.backgroundColor
						});
					}
					doma.attr("href",item.toUrl);
			}
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
		// index = Math.round(Math.random() * (bgList.length - 1));
		homeBg.each(function(i) {
			$(this).attr('src', bgList[i].imgUrl);
		});
		


		UIBase.loading.show();
		var firstImg = homeBg.eq(0);
		firstImg[0].onload = function() {
			UIBase.loading.hide();
			self.navMenuEvent();
		};

		self.onBgImgSlider();
		setTimeout(function(){
			$('.indexMenu').trigger("click");
		},1000);
		
	},
	navMenuEvent: function() { //导航动画效果
	
		var self = this,
			aList = self.elements.indexNav.find('a'),
			number = 0,moveItem=aList.first(),menuTime=200;
		// self.elements.indexNav.animate({
		// 	'left': 23,
		// }, 300);
	
		setTimeout(ShowMenuItem, menuTime);
		function ShowMenuItem() {
			moveItem.addClass('show');
			moveItem=moveItem.next();
			if (moveItem.size()) {
				setTimeout(ShowMenuItem,menuTime+=80);
			};
		}

	},
	onBgSize: function() {
		var w = document.body.offsetWidth,
			self = this;
		$("#intoShow").css('width', w);
		self.elements.homeWrapper.find('li').css('width', w);
		// self.elements.sliderWrap.css({
		// 	'width': w,
		// 	'right': -w
		// });
		// $("#sliderWrap section").css('width', w);


	},
	//背景图片滑动
	onBgImgSlider: function() {
		var myScroll = new iScroll('homeWrapper', {
			snap: true,
			momentum: false,
			hScrollbar: false
		});
	},
	//滑动层
	onSlider: function() {
		myScroll2 = new iScroll('sliderWrap', {
			snap: 'section',
			momentum: false,
			hScrollbar: false,
			vScroll: false
		});
	},
	//绑定事件
	initEvent: function() {
		var self = this;

		$('.indexMenu').bind('click', function() {
			var self = $(this),
				subElement = $('.menuWrap');
			if (self.hasClass('on')) {
				subElement.animate({
					left: -160
				}, 400);
				self.removeClass('on');
			} else {
				subElement.animate({
					left: 65
				}, 400);
				self.addClass('on');
			}
		});


		//拨号
		// $('#indexTelBtn').bind('click', function() {
		// 	// // $("#intoShow").hide();
		// 	// self.elements.sliderWrap.show();
		// 	// self.elements.sliderWrap.animate({
		// 	// 	right: 0
		// 	// }, 300); 
		// 	// self.onSlider();
		// 	// self.elements.homeWrapper.hide();
		// 	var teWrapper = $('#te-Wrapper');

		// 	if (teWrapper.size()) {
		// 		teWrapper.show();

		// 	} else {
		// 		Jit.UI.DomView.init('.schoolArea');

		// 	}
		// });


	}
	// ,
	// CloseTeWrapper: function() {
	// 	var self = this;
	// 	$('#te-Wrapper').hide();
	// 	self.elements.homeWrapper.show();

	// }

});