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
    JITPage.HandlerUrl.setValue("Handler/NewsHandler.ashx?mid=" + __mid);

    fnSearch("");
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "NewsEdit",
        title: "资讯内容",
        url: "NewsEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch(lnewstypeid) {
    Ext.getStore("newsStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=news_query";
    Ext.getStore("newsStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("newsStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        typeid: lnewstypeid
    };

    Ext.getStore("newsStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "NewsEdit",
        title: "资讯内容",
        url: "NewsEdit.aspx?NewsId=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "NewsId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=news_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "NewsId"
            })
        },
        handler: function () {
            Ext.getStore("newsStore").load();
        }
    });
}

function fnFormartShow(val) {
    if (val == "1") {
        return "是";
    } else if (val == "2") {
        return "否";
    } else {
        return "";
    }
}
