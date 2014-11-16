Jit.AM.defindPage({

    name: 'PickStudent',
    elements: {
    },
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
     var self=this;
        self.elements.alumnusPicList=$('#alumnusPicList li');
        self.elements.detailList=$('.detailInfo');
        self.elements.scrollList=$('#scrollList');


    },
    initEvent: function() {
        var self = this;
        //点击头像事件
        self.elements.alumnusPicList.bind(self.eventType,function(){
          var element=$(this),index=self.elements.alumnusPicList.index(this);
          self.elements.alumnusPicList.removeClass('on');
          element.addClass('on');
          self.elements.detailList.hide();
          self.elements.detailList.eq(index).show();
        });
    

        self.scrollEvent();
        //滚动事件
    },scrollEvent:function(){
    self = this;
        // 绑定滚动事件
        var  myScroll, isWidth = 0;
        //重新設置大小
        ReSize();

        function ReSize() {
            self.elements.alumnusPicList.each(function(i) {
                var el = $(this);
                isWidth += el.width() + 30;
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
        $(window).resize(function() {
            ReSize();
            myScroll.refresh();
        });




    }

});

