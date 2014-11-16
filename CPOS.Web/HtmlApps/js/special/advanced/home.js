Jit.AM.defindPage({
	name: 'Home',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self=this;
		self.elements.navList=$('.indexNav');
		
		//导航动画
		self.elements.navList.addClass('on');

	},
	//绑定事件
	initEvent: function() {
		var self = this;
	
		
	}

});