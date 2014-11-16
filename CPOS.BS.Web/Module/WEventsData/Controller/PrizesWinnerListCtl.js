
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
    CloseWin('PrizesWinnerList');
}

function fnSearch(eventId) {
    Ext.getStore("prizesWinnerListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_prizes_winner_list";
    Ext.getStore("prizesWinnerListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("prizesWinnerListStore").proxy.extraParams = {
        EventId: eventId
    };
    Ext.getStore("prizesWinnerListStore").load();
}

function fnCreate() {
    var condition = {
        type: "stock",
        EventID: getUrlParam("EventID")
    };
    $.post("Handler/ExportToExcel3.ashx?mid=", condition,
        function (data) {
            if (data) {
                if (data == "" || data == "/ExportTemp/") {
                    showInfo("未查询到数据");
                } else {
                    window.open(data);
                }
            }
        },
    "text");
    return false;
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
            Ext.getStore("prizesWinnerListStore").load();
        }
    });
}

