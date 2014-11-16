Jit.AM.defindPage({
	name:'MyInfoPage',
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入MyInfoPage.....');
		
		this.initEvent();
		
		this.initData();
	},
	initData : function(){
	
		var baseInfo = this.getBaseInfo();
	
		$('#lab_name').html(baseInfo.userId);
	},
	initEvent:function(){
		
		Jit.log('初始化TestPage页面事件....');
		
		var me = this;
		
		console.log( me.getParams('cjstest'));
	}
});