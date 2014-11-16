
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
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=");
    
    var EventID = new String(JITMethod.getUrlParam("EventID"));
    fnSearch(EventID);

    myMask.hide();
});

function fnClose() {
    CloseWin('EventsUserList');
}

function fnSearch(eventId) {
    Ext.getStore("eventsUserListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=events_user_list_query";
    Ext.getStore("eventsUserListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventsUserListStore").proxy.extraParams = {
        EventId: eventId
    };
    Ext.getStore("eventsUserListStore").load();
}

