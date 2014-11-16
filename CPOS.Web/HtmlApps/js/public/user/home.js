Jit.AM.defindPage({

    name: 'Home',
    elements: {
        homeAdList: '',
        btInEmba: '',
        scrollList: '',
        scrolItems: '',
        scrollMenu: '',
        scrollTitle: ''
    },
    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        self.initLoad();
        self.initEvent();


    }, //加载数据
    initLoad: function() {
        var self = this;

    },
    //绑定事件
    initEvent: function() {
        var self = this;

        $('.indexMenu').bind('click', function() {
            var self = $(this),
                subElement = $('.menuWrap');
            if (self.hasClass('on')) {
                subElement.animate({
                    left: -160
                }, 400);
                self.removeClass('on');
            } else {
                subElement.animate({
                    left: 65
                }, 400);
                self.addClass('on');
            }


        });

    }

});