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
        id: "WApplicationEdit",
        title: "申请接口",
        url: "WApplicationEdit.aspx?mid=" + __mid
    });
    win.show();
}

fnSearch = function () {
    var store = Ext.getStore("wApplicationStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_wapplication";
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
        id: "WApplicationEdit",
        title: "申请接口",
        url: "WApplicationEdit.aspx?WApplicationId=" + id
    });
    win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ApplicationId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=wapplication_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ApplicationId" })
        },
        handler: function () {
            Ext.getStore("wApplicationStore").load();
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
function fnRemoveSession(id) {


    Ext.Msg.confirm("请确认", "是否要清除Session？", function (button) {
        if (button == "yes") {
            var handlerUrl = JITPage.HandlerUrl.getValue() + "&method=RemoveSessionById";
            Ext.Ajax.request({
                url: handlerUrl,
                params: {
                    ApplicationId: id
                },
                method: 'POST',
                success: function (response, opts) {
                    debugger;
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '清空成功',
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
}
function fnImport() {

    var loadMarsk = new Ext.LoadMask(Ext.getBody(), {
        msg: '正在处理数据，请稍候......',
        removeMask: true // 完成后移除    
    });
    Ext.Msg.confirm("请确认", "是否要批量导入数据？", function (button) {
        if (button == "yes") {
            loadMarsk.show();  //显示 
            var store = Ext.getStore("wApplicationStore");
            for (var i = 0; i < store.getCount(); i++) {
                var record = store.getAt(i).data;
                var handlerUrl = JITPage.HandlerUrl.getValue() + "&method=import";
                Ext.Ajax.request({
                    url: handlerUrl,
                    timeout: 1200000,
                    params: {
                        appId: record.AppID,
                        appSecret: record.AppSecret,
                        weixinId: record.WeiXinID
                    },
                    method: 'POST',
                    success: function (response, opts) {
                        var jdata = Ext.JSON.decode(response.responseText);
                        if (jdata.success) {
                            Ext.Msg.show({
                                title: '提示',
                                msg: '批量导入数据成功',
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.INFO
                            });
                            loadMarsk.hide();  //隐藏    
                        } else {
                            Ext.Msg.show({
                                title: '错误',
                                msg: '批量导入数据失败',
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.ERROR
                            });
                        }
                    }, failure: function () {
                        Ext.Msg.alert("提示", "超时!");
                        loadMarsk.hide();  //隐藏    

                    }
                });
            }

        }
    });

}
