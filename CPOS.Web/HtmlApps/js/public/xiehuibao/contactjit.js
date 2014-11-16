Jit.AM.defindPage({
    name: 'ContactJit',
    elements: {
        broExplain: '',
        broMore: '',
        btInEmba: ''
    },
    onPageLoad: function() {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {},
    initEvent: function() {
        var self = this;
        FootHandle.init({
            praiseCount: 0,
            browseCount: 0,
            shareCount: 0,
            hideCount: true,
            hideJitAd: true,
            bindButton: $('#JitShare')
        });
        $('body').bind('touchmove', self.stopEvent);
    },
    stopEvent: function(e) {
        var evt = e || window.event;
        evt.stopPropagation ? evt.stopPropagation() : (evt.cancelBubble = true);
        evt.preventDefault ? evt.preventDefault() : null;
    }
});