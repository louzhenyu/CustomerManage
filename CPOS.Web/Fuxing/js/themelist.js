Jit.AM.defindPage({
    name: 'UserEdit',
    onPageLoad: function() {
        CheckSign();
        //当页面加载完成时触发


        this.initEvent();

        this.initData();
    },
    initData: function() {

        //加载主题信息
        var me = this,
            keyVal = 'val',
            datas = {
                eventId: Jit.AM.getBaseAjaxParam().eventId
            },
            eventList = [],
            aList = $('.thlist_list .hl_item a');
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?action=getEvents',
            data: datas,
            success: function(data) {
                if (data.code == 200) {
                    eventList = data.content.eventList;
                    for (var i = 0; i < eventList.length; i++) {
                        var eventInfo = eventList[i];
                        aList.eq(parseInt(eventInfo.displayIndex) - 1).attr('href', '/fuxing/html/theme.html?toEventId=' + eventInfo.eventID);
                    };

                }
            }
        });

        //主题拖动效果
        $(window).resize(function() {
            ChangeThemeAdListWidth();
        });

        ChangeThemeAdListWidth();


        //改变主题列表大小
        function ChangeThemeAdListWidth() {
            var curBody = $('body'),
                curWidth = curBody.width();
            var themeAdList = $('#themeAdList');
            themeAdList.width(curBody.width());
            themeAdList.find('ul:eq(0) li').width(curBody.width());
            themeAdList.find('ul:eq(0)').width(themeAdList.find('ul:eq(0) li').width() * themeAdList.find('ul:eq(0) li').size());
        }



        // myScroll = new iScroll('themeAdList', {
        //     snap: true,
        //     momentum: false,
        //     hScrollbar: false,
        //     onScrollEnd: function() {
        //         document.querySelector('#AdNavList li.on').className = '';
        //         document.querySelector('#AdNavList li:nth-child(' + (this.currPageX + 1) + ')').className = 'on';
        //     }
        // });


    },
    initEvent: function() {

    }
});