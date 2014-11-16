Jit.AM.defindPage({
	name: 'Fete',
	elements: {},
	onPageLoad: function() {
		//当页面加载完成时触发
		this.initLoad();
		this.initEvent();
	}, //加载数据
	initLoad: function() {
		var self = this;
	},
	initEvent: function() {
		var self = this,
			myScroll;
		//拖拽事件
		var goodsScroll = $('#indexMenu'),
			menuList = goodsScroll.find('a');
		//重新設置大小
		ReSize();

		function ReSize() {
			goodsScroll.find('.fete_indexNavList ').css({
				width: menuList.width() * (menuList.size()+1)
			});
		}
		//綁定滾動事件
		myScroll = new iScroll('indexMenu', {
			snap: true,
			momentum: false,
			hScrollbar: false,
			vScroll: false
		});
		$(window).resize(function() {
			ReSize();
			myScroll.refresh();
		});
	}
});