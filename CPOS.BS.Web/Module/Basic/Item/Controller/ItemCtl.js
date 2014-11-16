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
    JITPage.HandlerUrl.setValue("Handler/ItemHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "ItemEdit",
        title: "商品",
        url: "ItemEdit.aspx?mid=" + __mid

    });
    win.show();
}

fnSearch = function () {
    if (Ext.getCmp("txtStatus").getValue() == undefined)
        Ext.getCmp("txtStatus").setValue("1");

    var store = Ext.getStore("itemStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_item";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        item_category_id: getStr(Ext.getCmp("txtItemCategory").jitGetValue())
    };
    //alert(getStr(Ext.getCmp("txtItemCategory").jitGetValue()));
    store.load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "ItemEdit",
        title: "商品",
        url: "ItemEdit.aspx?item_id=" + id

    });
    win.show();
}

function fnEnable(val) {
    if (val == "-1") {
        if (!confirm("确认停用?")) return;
    }
    if (val == "1") {
        if (!confirm("确认启用?")) return;
    }

    var ids = JITPage.getSelected({
        gridView: Ext.getCmp("gridView"),
        id: "Item_Id"
    });

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=item_enable",
        params: { ids: ids, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.status) {
                alert(d.msg);
                return;
            }
            fnSearch();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}

function fnMoreSearchView() {
    if (!Ext.getCmp("searchPanel").isexpand) {
        Ext.getCmp("searchPanel").isexpand = true;
        Ext.getCmp("txtCanRedeem").setVisible(true);
        Ext.getCmp("btnMoreSearchView").setText("隐藏高级查询");
        //document.getElementById("divTags").style.visibility = "visible";
    } else {
        Ext.getCmp("searchPanel").isexpand = false;
        Ext.getCmp("txtCanRedeem").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText("高级查询");
        //document.getElementById("divTags").style.visibility = "hidden";
    }
}

function fnRowRenderer(record, rowIndex, rowParams, store) {
    debugger;
    var cls;
    var jdata = record.data;
    if (jdata.IsPause == "true") {
        cls = "x-grid-back-belowIsPause";
        //Ext.getCmp("gridView").getView().getRow(index).style.backgroundColor = '#CCCCCC';
    }
    else if (jdata.IsItemCategory == "true") {
        cls = "x-grid-back-belowIsItemCategory"
    }
    return cls;
}
