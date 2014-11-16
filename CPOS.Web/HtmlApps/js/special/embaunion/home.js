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
		self.elements.sliderWrap = $('#sliderWrap');
		self.elements.sliderClose = $('#sliderClose');
		self.elements.homeWrapper = $('#homeWrapper');
		self.elements.btInEmba = $('#btInEmba');
		self.elements.indexNav=$('.indexNav');
		self.onBgSize();
		$(window).resize(function() {

			self.onBgSize();
		});


		//随机设置背景
		var homeBg = $('.bgImgWrap img');
			bgList = [
				'../../../images/special/embaunion/indexBg.jpg',
				'../../../images/special/embaunion/indexBg.jpg',
				'../../../images/special/embaunion/indexBg.jpg'
			];
		homeBg.each(function(i) {
			$(this).attr('src', bgList[i]);
		});


		UIBase.loading.show();
		var firstImg = homeBg.eq(0);
		firstImg[0].onload = function() {
			UIBase.loading.hide();
			self.navMenuEvent();
		};




	
	},
	onBgSize: function() {

		var elBody = $('body'),
			w = elBody.width(),
			h = elBody.height(),
			self = this;
		$("#intoShow").css('width', w);
		self.elements.homeWrapper.find('li').css('width', w);
		self.elements.sliderWrap.css({
			'width': w,
			'right': -w
		});
		self.elements.sliderWrap.css({
			'width': w
		});

		self.elements.sliderWrap.find("section").css('width', w);


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

		self.elements.btInEmba.tap(function() {

			if (Validates.isLogin()) {

				// Jit.UI.Dialog({
				// 	'content': '您当前已登录，去完善个人信息？',
				// 	'LabelCancel': '取消',
				// 	'type': 'Confirm',
				// 	'CallBackOk': function() {
				// 		self.toPage('NewAddUserInfo', 'pageIndex=1');
				// 	},
				// 	'CallBackCancel': function() {
				// 		Jit.UI.Dialog('CLOSE');
				// 	}
				// });
			self.toPage('Account');
			} else {
				self.toPage('NewAddUserInfo');
			}
		});



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
			self.elements.sliderWrap.show();
			self.elements.sliderWrap.animate({
				right: 0
			}, 300);
		
		});
			self.onSlider();


		self.elements.sliderClose.bind('tap', function() {
			self.elements.sliderWrap.animate({
				'right': -document.body.offsetWidth
			}, 300, function() {
				self.elements.sliderWrap.hide();
			});
		});



	}

});