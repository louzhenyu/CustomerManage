Jit.AM.defindPage({

	name:'HomePage',
	
	onPageLoad:function(){
		
		//当页面加载完成时触发
		Jit.log('进入HomePage.....');
		
		this.initEvent();
	},
	
	initEvent:function(){
		
		Jit.log('初始化HomePage页面事件....');
		
		var me = this;
		
		me.setParams('cjstest','success...');

		me.ajax({
			url:'cjstext.php',
			data:{A:'asdf',B:'asdfdd'}
		});
		
		Jit.log(me.getUrlParam('urltest'));
	}
});