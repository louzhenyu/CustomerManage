Jit.AM.defindPage({
	name: 'VideoEvents',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		//测试清空缓存
		var self = this;
		self.elements.btDetail = $('.detailFold');
		self.elements.txtContent = $('.rulesBox .content');
		self.elements.imgContent = $('#imgContent');
	},
	initEvent: function() {
		var self = this;
		self.elements.btDetail.bind(self.eventType, function() {
			var element = $(this);
			if (element.hasClass('on')) {
				element.removeClass('on');
				self.elements.txtContent.hide();
				self.elements.imgContent.attr('src', '../../../images/special/europe/scrollhover.jpg');
			} else {
				element.addClass('on');
				self.elements.imgContent.attr('src', '../../../images/special/europe/scrollBg.jpg');
				self.elements.txtContent.show();
			}
		});
	}
});