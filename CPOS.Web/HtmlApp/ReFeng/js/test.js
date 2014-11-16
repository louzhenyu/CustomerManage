Jit.AM.defindPage({
	name:'TestPage',
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入TestPage.....');
		
		this.initEvent();
	},
	
	initEvent:function(){
		
		Jit.log('初始化TestPage页面事件....');
		
		var me = this;
		
		console.log( me.getParams('cjstest'));
	}
});