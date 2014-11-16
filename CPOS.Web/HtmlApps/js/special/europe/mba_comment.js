Jit.AM.defindPage({
    name: 'MBAComment',
    elements: {},
    onPageLoad: function () {
        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);
        this.loadPageData();
        this.initPageEvent();
    },
    initPageEvent: function () {
        var self = this;
        //点击头像事件
        self.elements.alumnusPicList.bind(self.eventType, function () {
            var element = $(this), index = self.elements.alumnusPicList.index(this);
            self.elements.alumnusPicList.removeClass('on');
            element.addClass('on');
            self.elements.detailList.hide();
            self.elements.detailList.eq(index).show();
        });
        self.scrollEvent();
    },
    scrollEvent: function () {
        self = this;
        // 绑定滚动事件
        var myScroll, isWidth = 0;
        //重新设置大小
        ReSize();

        function ReSize() {
            self.elements.alumnusPicList.each(function (i) {
                var el = $(this);
                isWidth += el.width() + 10;
            });
            self.elements.scrollList.css({
                width: isWidth
            });
        }

        myScroll = new iScroll('alumnusPicList', {
            snap: true,
            momentum: false,
            hScrollbar: false,
            vScroll: false
        });
        $(window).resize(function () {
            ReSize();
            myScroll.refresh();
        });
    },
    //加载数据
    loadPageData: function () {
        var self = this;
        //校友头像缩略图容器
        self.elements.alumnusPicList = $('#alumnusPicList li');
        //校友详情容器
        self.elements.detailList = $('.detailInfo');
        //校友头像缩略图子容器（滚动）
        self.elements.scrollList = $('#scrollList');
    }
});