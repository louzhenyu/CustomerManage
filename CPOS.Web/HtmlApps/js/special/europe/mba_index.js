Jit.AM.defindPage({
    name: 'MBAIndex',
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.loadPageData();
        this.initPageEvent();
    },
    initPageEvent: function () {
        var self = this;
    },
    //加载数据
    loadPageData: function () {
        var self = this;
        self.setCourseBriefBoxSize();
    },
    //课程简介框的大小设置
    setCourseBriefBoxSize: function () {
        var box_size = {
            width: 100,
            height: 100
        };
        $('#brief_box').css({
            width: box_size.width,
            height: box_size.height
        });
    }
});