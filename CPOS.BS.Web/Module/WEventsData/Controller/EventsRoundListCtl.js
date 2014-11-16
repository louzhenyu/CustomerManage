
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
    CloseWin('EventsRoundList');
}

function fnSearch(eventId) {
    Ext.getStore("eventsRoundListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=events_round_list_query" + "&eventId="+eventId;
    Ext.getStore("eventsRoundListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventsRoundListStore").proxy.extraParams = {
        EventId: eventId
    };
    Ext.getStore("eventsRoundListStore").load();
}

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "RoundEdit",
        title: "轮次",
        url: "RoundEdit.aspx?EventID=" + getUrlParam("EventID")
    });
	win.show();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "RoundEdit",
        title: "轮次",
        url: "RoundEdit.aspx?EventID=" + getUrlParam("EventID") + "&RoundId=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "RoundId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=round_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "RoundId"
            })
        },
        handler: function () {
            Ext.getStore("eventsRoundListStore").load();
        }
    });
}

function fnPool(id) {
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        url: 'Handler/WEventsHandler.ashx?method=events_pool&EventID='+getUrlParam("EventID"),
        params: {},
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                alert("操作失败：" + d.msg);
            } else {
                alert("操作成功!");
            }
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}
