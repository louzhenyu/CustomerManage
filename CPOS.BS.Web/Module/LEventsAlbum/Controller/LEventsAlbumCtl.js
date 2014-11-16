Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/LEventsAlbumHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 380,
        id: "NewsEdit",
        title: "视频内容",
        url: "LEventsAlbumEdit.aspx?mid=" + __mid + "&op=LEventsAlbum"
    });

    win.show();
}

function fnSearch() {
    Ext.getStore("newsStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetLEventsAlbumData";
    Ext.getStore("newsStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("newsStore").proxy.extraParams = {
        Title: Ext.getCmp("txtTitle").jitGetValue()
    };
    Ext.getStore("newsStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 380,
        id: "NewsEdit",
        title: "视频内容",
        url: "LEventsAlbumEdit.aspx?mid=" + __mid+"&AlbumId=" + id
    });
    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "AlbumId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=LEventsAlbumDeleteData",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "AlbumId"
            })
        },
        handler: function () {
            Ext.getStore("newsStore").load();
        }
    });
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
    fnSearch();
}