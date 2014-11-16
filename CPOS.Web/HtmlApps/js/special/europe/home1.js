Jit.AM.defindPage({

	name: 'Home',
	elements: {
		homeBg: '',
		sliderWrap: '',
		sliderClose: '',
		homeWrapper: ''
	},
	onPageLoad: function() {

		//当页面加载完成时触发
		Jit.log('页面进入' + this.name);


		this.initLoad();
		this.initEvent();


	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.sliderWrap = $('#sliderWrap');
		self.elements.sliderClose = $('#sliderClose');
		self.elements.homeWrapper = $('#homeWrapper');
		self.elements.indexNav = $('.indexNav');
		self.elements.school3dArea = $('.school3dArea');
		self.elements.rotateItems = self.elements.school3dArea.find('.item3d');
		self.elements.curBody=$('body');


		self.elements.indexNav.css({
			'left': '-80px'
		});
		self.onBgSize();
		window.onresize = self.onBgSize;



		//随机设置背景
		var homeBg = $('.bgImgWrap img'),
			bgList = [
				'../../../images/special/europe/indexBg.jpg',
				'../../../images/special/europe/indexBg1.jpg',
				'../../../images/special/europe/indexBg2.jpg'
			],
			index = Math.round(Math.random() * (bgList.length - 1));
		homeBg.eq(0).attr('src', bgList[index]);

		UIBase.loading.show();
		var firstImg = homeBg.eq(0);
		firstImg[0].onload = function() {
			UIBase.loading.hide();
			self.navMenuEvent();
		};
		if (index === 0) {
			homeBg.eq(1).attr('src', bgList[1]);
			homeBg.eq(2).attr('src', bgList[2]);
		} else if (index === 1) {
			homeBg.eq(1).attr('src', bgList[0]);
			homeBg.eq(2).attr('src', bgList[2]);
		} else {
			homeBg.eq(1).attr('src', bgList[0]);
			homeBg.eq(2).attr('src', bgList[1]);
		}
		self.onBgImgSlider();
	},
	navMenuEvent: function() { //导航动画效果
		var self = this;
		self.elements.indexNav.animate({
			'left': 23,
		}, 300);

	},
	onBgSize: function() {
		var w = document.body.offsetWidth,
			h = document.body.offsetHeight,
			self = this;
		$("#intoShow").css('width', w);
		self.elements.homeWrapper.find('li').css('width', w);
		self.elements.sliderWrap.css({
			'width': w,
			'right': -w
		});
		$("#sliderWrap section").css('width', w);

		self.elements.rotateItems.height(h);


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
		// myScroll2 = new iScroll('sliderWrap', {
		// 	snap: 'section',
		// 	momentum: false,
		// 	hScrollbar: false,
		// 	vScroll: false
		// });
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
		$('#indexTelBtn').bind('click', function() {
			// $("#intoShow").hide();
			self.elements.sliderWrap.show();
			self.elements.sliderWrap.animate({
				right: 0
			}, 300);
			// self.onSlider();
			self.elements.homeWrapper.hide();
		});

		// $('.homebody').delegate('#sliderWrap', 'touchend', function() {
		// 	if (myScroll2.x > 0) {
		// 		self.elements.sliderWrap.css('right', -document.body.offsetWidth);
		// 	}
		// });
		self.elements.sliderClose.bind('tap', function() {
			self.elements.sliderWrap.animate({
				'right': -document.body.offsetWidth
			}, 300, function() {
				self.elements.sliderWrap.hide();
			});
				self.elements.homeWrapper.show();
		});



		self.elements.rotateItems.swipeUp(function() {
			var curElement = $(this),
				nextElement = $(this).next();
			if (!nextElement.size()) {
				return false;
			};

			self.elements.school3dArea.animate({
				'-webkit-transform': 'rotateX(' + nextElement.data('val') + 'deg)'
			}, 500);

		}).swipeDown(function() {
			var curElement = $(this),
				prevElement = $(this).prev();
			if (!prevElement.size()) {
				return false;
			};
			self.elements.school3dArea.animate({
				'-webkit-transform': 'rotateX(' + prevElement.data('val') + 'deg)'
			}, 500);
		});



	}

});