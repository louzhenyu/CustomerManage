Jit.AM.defindPage({
	name: 'Menus',
	elements: {},
	objects: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
		this.elements.menuList = $('#fan-holder');
		YangtzeHandle.dataList=YangtzeHandle.getDataList();

	},
	//绑定事件
	initEvent: function() {
		var self = this;
		//绑定菜单跳转
		self.elements.menuList.find('a').bind(self.eventType, function() {
			var index = self.elements.menuList.find('a').index(this);
			YangtzeHandle.dataInfo = YangtzeHandle.dataList[index];
			self.toPage('Home');
		});
		if (YangtzeHandle.dataList.length) {
			self.bindMenuEvent();
		};
	},
	bindMenuEvent:function(){
		var self=this,menus=self.elements.menuList.find('a');
		for (var i = 0; i < YangtzeHandle.dataList.length; i++) {
			menus.eq(i).attr('href','javascript:Jit.AM.toPage(\'Home\',\'toMicroNumberID='+YangtzeHandle.dataList[i].MicroNumberID+'\')');
			console.log(menus.eq(i))
		};
	}

});