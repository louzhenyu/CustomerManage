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
    JITPage.HandlerUrl.setValue("Handler/UnitHandler.ashx?mid=" + __mid);
    Ext.getCmp("txtStatus").jitSetValue("1");
    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "UnitEdit",
        title: "门店",
        url: "UnitEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function() {
    var store = Ext.getStore("unitStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_unit";
   store.pageSize =JITPage.PageSize.getValue();//JITPage做分页操作
   //  alert(store.pageSize);
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()), 
        unit_city_id: Ext.getCmp("txtCity").jitGetValue()
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
        id: "UnitEdit",
        title: "门店",
        url: "UnitEdit.aspx?unit_id=" + id
    });
	win.show();
}
function fnDelete(id, val) {
    if (val == "-1") {
        if (!confirm("确认停用?")) return;
    }
    if (val == "1") {
        if (!confirm("确认启用?")) return;
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=unit_delete",
        params: { ids: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {//alert(response.responseText);
            var d = Ext.decode(response.responseText);
            if (d.success) {
                alert("操作成功.");
            }
            fnSearch();
        },
        failure: function () {
            Ext.Msg.alert("提示", "提交数据失败");
        }
    });
    return true;
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isexpand) {
        Ext.getCmp("searchPanel").isexpand = true;
        Ext.getCmp("txtCity").setVisible(true);
        Ext.getCmp("txtStatus").setVisible(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
    } else {
        Ext.getCmp("searchPanel").isexpand = false;
        Ext.getCmp("txtCity").setVisible(false);
        Ext.getCmp("txtStatus").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
    }
}
