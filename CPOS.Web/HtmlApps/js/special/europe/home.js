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
		// self.elements.sliderWrap = $('#sliderWrap');
		// self.elements.sliderClose = $('#sliderClose');
		self.elements.homeWrapper = $('#homeWrapper');
		self.elements.indexNav = $('.indexNav');


		// self.elements.indexNav.css({
		// 	'left': '-80px'
		// });
		self.onBgSize();
		window.onresize = self.onBgSize;


		//设置背景
		var homeBg = $('.bgImgWrap img'),
			bgList = [
				'../../../images/special/europe/indexBg3.jpg',
				'../../../images/special/europe/indexBg.jpg',
				'../../../images/special/europe/indexBg1.jpg'
				// ,
				// '../../../images/special/europe/indexBg2.jpg'
			];
		// index = Math.round(Math.random() * (bgList.length - 1));
		homeBg.each(function(i) {
			$(this).attr('src', bgList[i]);
		});



		UIBase.loading.show();
		var firstImg = homeBg.eq(0);
		firstImg[0].onload = function() {
			UIBase.loading.hide();
			self.navMenuEvent();
		};

		self.onBgImgSlider();
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