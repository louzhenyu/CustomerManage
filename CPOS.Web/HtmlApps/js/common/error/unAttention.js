Jit.AM.defindPage({
	
    name: 'unAttention',
	
    onPageLoad: function() {
	
		var cfg = Jit.AM.getAppVersion();
		
		$('.tip').html('你还没有关注 '+cfg.APP_NAME+'，请先关注该公众号');
    }
});