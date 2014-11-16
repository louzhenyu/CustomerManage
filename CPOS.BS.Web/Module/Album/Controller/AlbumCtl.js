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
    JITPage.HandlerUrl.setValue("Handler/AlbumHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnSearch() {
    Ext.getStore("AlbumStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=album_query";
    Ext.getStore("AlbumStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("AlbumStore").proxy.extraParams = {
        start: 0, limit: 0,
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getCmp('gridView').getStore().getProxy().startParam = 0;
    Ext.getCmp('pageBar').moveFirst();
    Ext.getStore("AlbumStore").load();
}

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 560,
        id: "AlbumEdit",
        title: "相册信息",
        url: "AlbumEdit.aspx"
    });

    win.show();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 560,
        id: "AlbumEdit",
        title: "相册信息",
        url: "AlbumEdit.aspx?AlbumId=" + id
    });

    win.show();
}

function fnViewImage(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 560,
        id: "AlbumImages",
        title: "相片管理",
        url: "AlbumImages.aspx?AlbumId=" + id
    });

    win.show();
}

function fnDelete(id) {

    Ext.Msg.confirm("请确认", "是否要删除数据(且删除相册下面的相片)？", function (button) {
        if (button == "yes") {
            var handlerUrl = JITPage.HandlerUrl.getValue() + "&method=album_delete";
            Ext.Ajax.request({
                url: handlerUrl,
                params: {
                    ids: JITPage.getSelected({
                        gridView: Ext.getCmp("gridView"),
                        id: "AlbumId"
                    })
                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '删除成功',
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: fnSearch()
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
            });
        }
    });






    //JITPage.deleteList({
    //    ids: JITPage.getSelected({
    //        gridView: Ext.getCmp("gridView"),
    //        id: "AlbumId"
    //    }),
    //    url: JITPage.HandlerUrl.getValue() + "&method=album_delete",
    //    params: {
    //        ids: JITPage.getSelected({
    //            gridView: Ext.getCmp("gridView"),
    //            id: "AlbumId"
    //        })
    //    },
    //    handler: function () {
    //        Ext.getStore("AlbumStore").load();
    //    }
    //});
}

fnReset = function () {
    Ext.getCmp("txtTitle").jitSetValue("");
    Ext.getCmp("txtAlbumType").jitSetValue("");
    Ext.getCmp("txtAlbumModuleType").jitSetValue("");
    Ext.getCmp("txtModuleTypeName").jitSetValue("");
}
