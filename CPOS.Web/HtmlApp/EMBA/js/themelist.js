Jit.AM.defindPage({
    name: 'UserEdit',
    onPageLoad: function () {
        CheckSign();
        //当页面加载完成时触发

    
        this.initEvent();

        this.initData();
    },
    initData: function () {
     
        //加载主题信息
        var me = this,
        keyVal = 'val',
        datas = {
            eventId: Jit.AM.getBaseAjaxParam().eventId
        },
        eventList=[],
        aList=$('.thlist_list .hl_item a')
        ;
        me.ajax({
            url: '/OnlineShopping/data/Data.aspx?action=getEvents',
            data: datas,
            success: function (data) {
                if (data.code == 200) {
                 eventList=data.content.eventList;
                 for (var i = 0; i < eventList.length; i++) {
                 var eventInfo=eventList[i];
                 aList.eq(parseInt(eventInfo.displayIndex)-1).attr('href','/fuxing/html/theme.html?toEventId='+eventInfo.eventID);
                 };

                }
            }
        });


    },
    initEvent: function () {

    }
});