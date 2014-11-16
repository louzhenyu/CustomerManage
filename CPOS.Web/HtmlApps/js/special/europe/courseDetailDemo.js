Jit.AM.defindPage({

	name: 'CourseDetailDemo',
	headerTime: 0,
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.loadPageData();
		this.initPageEvent();
	},
	//加载数据
	loadPageData: function(callback) {
		var self = this;
		self.elements.sourseBoxBg = $('.sourse_box_bg');
		self.elements.sourseSubmitBox = $('.sourse_submit_box');
		self.elements.btSubmit = $('.sourse_submit');
		self.elements.sourseBoxClose = $('.sbl_close .u-close');
		self.elements.PeopleBox = $('.m-people');
		self.elements.PeopleBoxClose = $('.m-people .u-close');
		self.elements.boxPeopleName = $('#boxPeopleName');
		self.elements.sourseExplainList = $('.sourse_explain_list');
		self.elements.crouseMenuArea = $('.courseMenu'),
        self.elements.menuList = self.elements.crouseMenuArea.find('a'),
        self.elements.btCourseMenu = self.elements.crouseMenuArea.find('.btCourseMenu');
        self.elements.courseMenu=$('.courseMenu');
        self.elements.detailItemList= self.elements.sourseExplainList.find('.sel_item');

           //加入底部内容
        FootHandle.init({
            praiseCount: 0,
            browseCount: 0,
            shareCount: 0,
            hideCount:true
        });

	},
	initPageEvent: function() {
		var self = this;
		var posInit = function(obj) {
			obj.options.ulBtn.find('li').find('a').css({
				'width': '20px',
				'height': '20px'
			});
		};
		var posSet = function(obj) {
			obj.options.ulBtn.find('li').find('a').css('background', 'rgba(255,255,255,0.3)');
			obj.options.ulBtn.find('li').eq(obj.options.pos - 1).find('a').css('background', 'rgba(31,181,191,1)');
		};
		//轮播效果
		$(".sourse_box").each(function() {
			var urls = $(this).find('input').val();
			new slidepic(this, {
				urls: urls,
				posInit: posInit,
				posSet: posSet
			});
		});

		//提交显示报名框
		self.elements.btSubmit.tap(function() {
			$('html,body').scrollTop(0);
			self.elements.sourseBoxBg.css('display', 'block');
			self.elements.sourseSubmitBox.css('display', 'block');

			self.elements.sourseBoxBg.addClass('show');
			// setTimeout(function() {
			// 	self.elements.sourseBoxBg.addClass('show');
			// }, 50);

			setTimeout(function() {
				self.elements.sourseSubmitBox.addClass('show');
			}, 200);

		});

		//关闭报名框
		self.elements.sourseBoxClose.tap(function() {
			self.elements.sourseBoxClose.addClass('start');
			setTimeout(function() {
				self.elements.sourseBoxBg.css('display', 'none');
				self.elements.sourseBoxBg.removeClass('show');
				self.elements.sourseSubmitBox.removeClass('show');
				self.elements.sourseSubmitBox.css('display', 'none');
				self.elements.sourseBoxClose.removeClass('start');
			}, 100);

		});


		//显示用户个人信息
		$('.u-imgHeader').tap(function() {

			var element = $(this),
				boxIndex = element.data(KeyList.val),
				peopleItemBox = $('#peopleBox' + boxIndex),
				cloneBox = element.clone(),
				boxHeaderBox = peopleItemBox.find('.u-imgHeader'),
				curBody = $('body');


			//动画初始化
			boxHeaderBox.removeClass('show');
			self.elements.sourseBoxBg.removeClass('show');
			self.elements.PeopleBox.removeClass('show');
			peopleItemBox.removeClass('show');
			self.elements.sourseBoxBg.css('display', 'block');
			self.elements.PeopleBox.css('display', 'block');
			peopleItemBox.css('display', 'block');


			//删除不要的元素
			cloneBox.find('.arrow').remove();

			cloneBox.css({
				'position': 'absolute',
				'top': element.offset().top,
				'left': element.offset().left,
				'z-index': 1100,
			});
			cloneBox[0].style.transform = 'scale(0.1)';
			cloneBox[0].style.MozTransform = 'scale(0.1)';
			cloneBox[0].style.WebkitTransform = 'scale(0.1)';

			curBody.append(cloneBox);

			cloneBox.animate({
				'top': peopleItemBox.offset().top + 13,
				'left': peopleItemBox.offset().left + 23,
			}, 1500);


			setTimeout(function() {
				self.MoveBox(cloneBox);
			}, 200)

			// 显示背景
			setTimeout(function() {
				self.elements.sourseBoxBg.addClass('show');
			}, 300);

			//显示介绍框
			setTimeout(function() {
				self.elements.PeopleBox.addClass('show');
				peopleItemBox.addClass('show');
			}, 500);

			//3秒后删除克隆元素
			setTimeout(function() {
				$('body>.u-imgHeader').remove();
				boxHeaderBox.addClass('show')
			}, 2000);

		});

		//关闭用户个人信息
		self.elements.PeopleBoxClose.tap(function() {
			self.elements.sourseBoxBg.css('display', 'none');
			self.elements.PeopleBox.css('display', 'none');
			self.elements.PeopleBox.find('li').css('display', 'none');
			self.elements.sourseBoxBg.removeClass('show');
			self.elements.PeopleBox.removeClass('show');

			$('body>.u-imgHeader').remove();
		});


		//菜单点击事件

		self.elements.btCourseMenu.tap(function() {

			if (self.elements.btCourseMenu.hasClass('on')) {
				self.elements.btCourseMenu.removeClass('on');
				self.elements.menuList.removeClass('show');
			} else {
				self.elements.menuList.addClass('show');
				self.elements.btCourseMenu.addClass('on');
			}
		});

		self.elements.crouseMenuArea.find('a').tap(function() {
			var element = $(this);
			if (element.hasClass('btCourseMenu')) {
				return false;
			};

			var toTop = 0;

			if (element.hasClass('one')) {

				toTop = self.elements.sourseExplainList.offset().top;
			};

			if (element.hasClass('two')) {

				toTop = $('.sourse_comment_list').offset().top;
			};

			if (element.hasClass('three')) {
				toTop = $('.sourse_address_list').offset().top;
			};
			if (element.hasClass('four')) {
				toTop = $('.sourse_comment_list').offset().top;
			};
			 // $("html,body").animate({"scrollTop":toTop});
			 document.body.scrollTop=toTop;
			
		});


		self.elements.detailItemList.tap(function() {
			var element = $(this);
				if (element.hasClass('not')) {
					return false;
				};
			if (element.hasClass('focus')) {
				element.removeClass('focus');
			} else {
				element.addClass('focus');
			}
		});

		$('#bttest').tap(function(){
		 $('#test1').velocity({left:0},1000);




		});

		self.detailEvent();

	}, //详情移动效果
	detailEvent: function() {
		var self = this,
			detailList = self.elements.sourseExplainList.find('.sel_detail'),
			curWindow = $(window),
			lock = false;

		//偏移距离
		detailList.css({
			'left': curWindow.width(),
			'opacity': 1
		});

		curWindow.scroll(SetMove);
		SetMove();

		function SetMove() {
			if (lock) {
				return false;

			};
			var curScrollTop = curWindow.scrollTop(),
				hasList = self.elements.sourseExplainList.find('div.on');
			lock = hasList.size() >= detailList.size();
			detailList.each(function() {
				var elementDetailItem = $(this);
				if (!elementDetailItem.hasClass('on')) {
					if ((curScrollTop + 500) >= elementDetailItem.offset().top) {
						elementDetailItem.css('left', '0');
						elementDetailItem.parents('.sel_item').addClass('on');
						// $('#test1').addClass('on');
					};
				};

			});
		}



	},
	//移动介绍框
	MoveBox: function(headerClone) {
		var self = this;

		function step() {
			self.headerTime += 1000 / 60;
			if (self.headerTime <= 400) {
				//头像变大
				var scale = 'scale(' + self.headerTime / 400 + ')';

				headerClone[0].style.transform = scale;
				headerClone[0].style.MozTransform = scale;
				headerClone[0].style.WebkitTransform = scale;

				//循环
				self.RAF(step)

			} else {
				//人物出现
				self.headerTime = 0; //让控制时间初始化为0

				var scale = 'scale(1)';
				headerClone[0].style.transform = scale;
				headerClone[0].style.MozTransform = scale;
				headerClone[0].style.WebkitTransform = scale;
			}

		}
		(step())
	},
	RAF: function(callback) {
		window.setTimeout(callback, 1000 / 60);
	},
	alert: function(text, callback) {
		Jit.UI.Dialog({
			type: "Alert",
			content: text,
			CallBackOk: function() {
				Jit.UI.Dialog("CLOSE");
				if (callback) {
					callback();
				}
			}
		});
	}
});