Ext.Loader.setConfig({
    enabled: true
});

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*']);

Ext.onReady(function () { 
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/TicketHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 350,
        id: "TicketEdit",
        title: "票务详情",
        url: "TicketEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch() {
    Ext.getStore("ticketStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=ticket_query";
    Ext.getStore("ticketStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("ticketStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        EventID: Ext.getCmp("EventID").jitGetValue()
    };

    Ext.getStore("ticketStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 350,
        id: "TicketEdit",
        title: "票务详情",
        url: "TicketEdit.aspx?TicketID=" + id
    });

    win.show();
}


function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "TicketID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=ticket_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "TicketID"
            })
        },
        handler: function () {
            Ext.getStore("ticketStore").load();
        }
    });
}

fnReset = function (){
    Ext.getCmp("EventID").jitSetValue({ id: "", text: "" });
    Ext.getCmp("TicketName").jitSetValue("");
}
