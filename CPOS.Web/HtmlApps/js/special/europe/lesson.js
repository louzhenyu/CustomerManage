Jit.AM.defindPage({
	name: 'Lesson',
	elements: {

	},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		self.elements.btBroseDetail=$('.dropIcon');
		self.elements.txtDetail=$('.popupText');
	},
	initEvent: function() {
		var self = this;
		self.elements.btBroseDetail.bind(self.eventType,function(){

			self.elements.txtDetail.toggleClass('on');
		});
	
    }
});