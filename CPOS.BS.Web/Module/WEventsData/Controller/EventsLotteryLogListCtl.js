
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WEventsHandler.ashx?mid=");
    
    var EventID = new String(JITMethod.getUrlParam("EventID"));
    fnSearch(EventID);

    myMask.hide();
});

function fnClose() {
    CloseWin('EventsLotteryLogList');
}

function fnSearch(eventId) {
    Ext.getStore("eventsLotteryLogListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=events_lotterylog_list_query";
    Ext.getStore("eventsLotteryLogListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventsLotteryLogListStore").proxy.extraParams = {
        EventId: eventId
    };
    Ext.getStore("eventsLotteryLogListStore").load();
}

