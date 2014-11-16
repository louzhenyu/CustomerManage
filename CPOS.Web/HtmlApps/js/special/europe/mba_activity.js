Jit.AM.defindPage({
    name: 'MBAActiviry',
    elements: {},
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.loadPageData();
        this.initPageEvent();
    },
    initPageEvent: function () {
        var self = this;
        //点击活动条目
        self.elements.recentActivity.bind(self.eventType, function () {
            var news_id=$(this).attr('data-news-id');
            window.location.href='http://www.o2omarketing.cn:9004/HtmlApps/html/public/xiehuibao/act_detail.html?customerId=75a232c2cf064b45b1b6393823d2431e&&newsId='+news_id+'&version=2014070401';
        });
    },
    //加载数据
    loadPageData: function () {
        var self = this;
        //活动条目
        self.elements.recentActivity = $('.recentActivity li');
    }
});