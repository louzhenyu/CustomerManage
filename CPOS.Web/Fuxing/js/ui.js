Jit.AM.defindPage({
    name: 'MyInfoPage',
    onPageLoad: function() {
        CheckSign();
        //当页面加载完成时触发

        this.initEvent();

        this.initData();
    },
    initData: function() {



    },
    initEvent: function() {
        var toPrize = $('#toPrize');
        if (toPrize.size()) {
        var curBaseInfo = Jit.AM.getBaseAjaxParam();
         toPrize.attr('href','/OnlineClothing20131217/0114guagua.html?eventId=' + curBaseInfo.eventId + '&customerId=' + curBaseInfo.customerId + '&userId=' + curBaseInfo.userId + '&openId=' + curBaseInfo.openId+'&vs='+(new Date()).getTime()); 

        };

    }
});