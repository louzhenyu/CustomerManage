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
    JITPage.HandlerUrl.setValue("Handler/LEventsEntriesHandler.ashx?mid=");

    fnLoad();
});

fnLoad = function () {
    var store = Ext.getStore("CommentStore");
    var entriesId = getUrlParam("EntriesId");
    var date = getUrlParam("date");
    var IsCrowdDaren = getUrlParam("IsCrowdDaren");

    if (entriesId != undefined && entriesId != "" && entriesId.length > 0) {
        //根据作品获取评论列表
        store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_comment_list&entriesId=" + entriesId;
        store.pageSize = JITPage.PageSize.getValue();
        Ext.getCmp('gridComment').getStore().getProxy().startParam = 0;
        Ext.getCmp('pageBar').moveFirst();
        store.load();
    } else { //if (date != undefined && date != "" && date.length > 0) {
        //根据日期获取围观达人
        store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_comment_list&date=" + date + "&IsCrowdDaren=" + IsCrowdDaren;
        store.pageSize = JITPage.PageSize.getValue();
        Ext.getCmp('gridComment').getStore().getProxy().startParam = 0;
        Ext.getCmp('pageBar').moveFirst();
        store.load();
    }
}

function fnCloseWin() {
    //parent.fnSearch();
    CloseWin('LEventsEntriesComment');
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridComment"),
            id: "CommentId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=comment_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridComment"),
                id: "CommentId"
            })
        },
        handler: function () {
            Ext.getStore("CommentStore").load();
        }
    });
}

function fnSetCrowdDaren(id, value) {
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/LEventsEntriesHandler.ashx?method=set_crowdaren&commentId=' + id + '&isCrowdDaren=' + value,
        params: {
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
            } else {
                showSuccess("保存数据成功");
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    fnLoad();
}