Jit.AM.defindPage({
    name: 'MyInfoPage',
    onPageLoad: function() {
        
        //当页面加载完成时触发

        this.initEvent();

        this.initData();
    },
    initData: function() {

		setTimeout(function(){FxLoading.Hide();},100);

    },
    initEvent: function() {
		
    }
});