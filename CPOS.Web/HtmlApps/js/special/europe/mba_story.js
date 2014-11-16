Jit.AM.defindPage({
    name: 'MBAStory',
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
            Jit.AM.toPage('NewsDetail','itemId='+news_id);
            //window.location.href='http://bs.aladingyidong.com/newsdetail.aspx?news_id='+news_id+'&customerId=75a232c2cf064b45b1b6393823d2431e';
        });
    },
    //加载数据
    loadPageData: function () {
        var self = this;
        //活动条目
        self.elements.recentActivity = $('.recentActivity li');
    }
});