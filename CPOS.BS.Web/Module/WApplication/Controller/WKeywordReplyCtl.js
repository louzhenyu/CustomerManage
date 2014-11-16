Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WKeywordReplyEdit",
        title: "关键字自动回复",
        url: "WKeywordReplyEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function() {
    var store = Ext.getStore("WKeywordReplyStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_WKeywordReply";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    store.load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WKeywordReplyEdit",
        title: "关键字自动回复",
        url: "WKeywordReplyEdit.aspx?WKeywordReplyId=" + id
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ReplyId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WKeywordReply_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ReplyId" })
        },
        handler: function () {
            Ext.getStore("WKeywordReplyStore").load();
        }
    });
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "74px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtTel").show(true);
        Ext.getCmp("txtStatus").show(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtTel").hide(true);
        Ext.getCmp("txtStatus").hide(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}

