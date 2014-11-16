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

    Ext.Ajax.request({
        url: "/Framework/Javascript/Biz/Handler/WApplicationInterfaceHandler.ashx?method=get_list",
        params: {},
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText).data;
            if (d != null && d.length > 0) {
                Ext.getCmp("txtApplicationId").jitSetValue(d[0].ApplicationId);
                get("hAppId").value = d[0].ApplicationId;
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });

    fnSearch();
});

function fnCreate() {
    var appId = get("hAppId").value;
    if (appId == null || appId.length == 0) {
        alert("请选择微信账号");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WModelListEdit",
        title: "分类管理",
        url: "WModelListEdit.aspx?mid=" + __mid
    });
    win.show();
}

fnSearch = function () {
    var appId = get("hAppId").value;
    if (appId == null || appId.length == 0) {
        //alert("请选择微信账号");
        return;
    }

    var store = Ext.getStore("WModelListStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_WModelList";
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
        id: "WModelListEdit",
        title: "微信模板",
        url: "WModelListEdit.aspx?ModelId=" + id
    });
    win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ModelId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WModelList_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ModelId" })
        },
        handler: function () {
            Ext.getStore("WModelListStore").load();
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

