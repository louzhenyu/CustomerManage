Jit.AM.defindPage({

    name: 'Activity',
    elements: {
        actAdList: '',
        actList: '',
        scrollList: '',
        scrolItems: '',
        scrollMenu: '',
        scrollTitle: ''
    },

    onPageLoad: function() {

        //当页面加载完成时触发
        Jit.log('页面进入' + this.name);

        this.initLoad();
        this.initEvent();
    }, //加载数据
    initLoad: function() {
        var self = this;
        self.elements.actAdList = $('#actAdList');
        self.elements.actList = $('#actList');

        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            beforeSend: function() {
            },
            data: {
                'action': 'getActivityList',
                "page": 1,
                "pageSize": 1000
            },
            beforeSend:function(){
                UIBase.loading.show();


            },
            success: function(data) {
                UIBase.loading.hide();
                if (data && data.code == 200 && data.content.ItemList.length > 0) {
                    self.elements.actList.html(GetActListHtml(data.content.ItemList));
                }
            }
        });



        self.elements.scrollList = $('#actAdList');
        self.elements.scrolItems = self.elements.scrollList.find('.picList');
        self.elements.scrollMenu = self.elements.scrollList.find('.dot');
        self.elements.scrollTitle = self.elements.scrollList.find('#scrollTitle');

        self.ajax({
            url: '/dynamicinterface/data/data.aspx',
            data: {
                'action': 'getTopList',
                "page": 1,
                "pageSize": 5,
                'NewsType': 0,
                'Type': 2
            },
            success: function(data) {

                if (data && data.code == 200 && data.content.ItemList) {
                    self.elements.scrolItems.html(GetAdListHtml(data.content.ItemList));
                    self.elements.scrollMenu.html(GetAdMenuHtml(data.content.ItemList));
                    self.elements.scrollTitle.html(data.content.ItemList[0].NewsTitle.cut(22, '...'));
                    self.ScrollEvent();
                } else {
                    self.elements.scrollList.hide();
                }
            }
        });



    }, //绑定事件
    initEvent: function() {
        var self = this;



    },
    ScrollEvent: function() {
        self = this;
        // 绑定滚动事件
        var menuList = $('.asideShow .dot em', self.elements.scrollList);

        //重新設置大小
        ReSize();

        function ReSize() {
            self.elements.scrollList.find('.commonBanner').css({
                width: (self.elements.scrollList.width()) * menuList.size()
            });

            self.elements.scrollList.find('.picList li').css({
                width: (self.elements.scrollList.width())
            });
        }
        myScroll = new iScroll('actAdList', {
            snap: true,
            momentum: false,
            hScrollbar: false,
            vScroll: false,
            onScrollEnd: function() {
                if (this.currPageX > (menuList.size() - 1)) {
                    return false;
                };
                menuList.removeClass('on');
                menuList.eq(this.currPageX).addClass('on');
                self.elements.scrollTitle.html(menuList.eq(this.currPageX).data(KeyList.val));
            }
        });
        // menuList.bind('click', function() {
        //     myScroll.scrollToPage(menuList.index(this));
        // });
        $(window).resize(function() {
            ReSize();
            myScroll.refresh();
        });


    }

});



function GetAdListHtml(datas) {
    var adListHtml = new StringBuilder(),
        toPageName = '';
    for (var i = 0; i < datas.length; i++) {
        var dataInfo = datas[i];

        switch (parseInt(dataInfo.Type)) {
            case 1:
                toPageName = "NewDetail";
                break;
            case 2:
                toPageName = "ActDetail";
                break;
            case 3:
                toPageName = "Home";
                break;
        }
        adListHtml.appendFormat("<li ><a href=\"javascript:Jit.AM.toPage('{0}','&newsId={1}');\"><img src=\"{2}\"  ></a></li>", toPageName, dataInfo.NewsId, dataInfo.ImageUrl);
    };


    return adListHtml.toString();
}

function GetAdMenuHtml(datas) {
    var adListHtml = new StringBuilder();
    for (var i = 0; i < datas.length; i++) {
        var dataInfo = datas[i];
        adListHtml.appendFormat("<em data-val=\"{0}\" class=\"{1}\"></em>", dataInfo.NewsTitle.cut(22, '...'), i == 0 ? 'on' : '');
    };

    return adListHtml.toString();
}



function GetActListHtml(datas) {
    var actListHtml = new StringBuilder(),
        endDay = 0,
        beginDay = 0;
    for (var i = 0; i < datas.length; i++) {
        var actItem = datas[i];
        endDay = parseInt(actItem.EndDay);
        beginDay = parseInt(actItem.BeginDay);

        actListHtml.appendFormat("<a class=\"{1}\" href=\"javascript:Jit.AM.toPage('ActDetail','&newsId={0}')\"><div>", actItem.ActivityID, endDay <= 0 ? '' : beginDay <= 0 ? 'on' : 'ho');

        actListHtml.append("<div class=\"item\" >");
        actListHtml.appendFormat("<div>{0}</div>", actItem.ActivityTitle.cut(66, '...'));

        if (endDay <=0) {
            actListHtml.appendFormat("<div><i></i>{0}<span>已结束</span></div>", actItem.BeginTime);

        } else {
            if (beginDay <= 0) {
                actListHtml.appendFormat("<div><i></i>{0}<span>已开始</span></div>", actItem.BeginTime);

            } else {
                actListHtml.appendFormat("<div><i></i>{0}<span>{1}</span></div>", actItem.BeginTime, beginDay > 0 && beginDay < 1 ? "即将开始" : parseInt(beginDay) + "天后");
            }

        }

        actListHtml.appendFormat("<div><i></i>{0} {1}</div>", actItem.ActivityCity, endDay >0&&beginDay<0? "<span>已报名" + actItem.UserCount + "人</span>" : '');

        // if (actItem.UserCount > 0) {

        //     actListHtml.appendFormat("<div><i></i>已有{0}人参加 <span>已报名30人</span></div>", actItem.UserCount);
        // };

        actListHtml.append("<i></i>");
        actListHtml.append("</div>");

        actListHtml.append("</div></a>");
    };


    return actListHtml.toString();
}