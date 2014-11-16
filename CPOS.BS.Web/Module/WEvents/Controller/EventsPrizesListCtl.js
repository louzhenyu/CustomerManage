
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
    CloseWin('EventsPrizesList');
}

function fnSearch(eventId) {
    Ext.getStore("eventsPrizesListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=events_prizes_list_query";
    Ext.getStore("eventsPrizesListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventsPrizesListStore").proxy.extraParams = {
        EventId: eventId
    };
    Ext.getStore("eventsPrizesListStore").load();
}
 
function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "PrizesEdit",
        title: "奖品",
        url: "PrizesEdit.aspx?EventID=" + getUrlParam("EventID")
    });
	win.show();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "PrizesEdit",
        title: "奖品",
        url: "PrizesEdit.aspx?EventID=" + getUrlParam("EventID") + "&PrizesID=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "PrizesID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=prizes_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "PrizesID"
            })
        },
        handler: function () {
            Ext.getStore("eventsPrizesListStore").load();
        }
    });
}

