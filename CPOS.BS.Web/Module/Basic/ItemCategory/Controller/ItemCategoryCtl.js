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
    JITPage.HandlerUrl.setValue("Handler/ItemCategoryHandler.ashx?mid=" + __mid);

    Ext.getCmp("txtItemCategoryStatus").setDefaultValue("1");

    fnSearch("1");
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "ItemCategoryEdit",
        title: "商品分类",
        url: "ItemCategoryEdit.aspx?mid=" + __mid
    });
    win.show();
}

fnSearch = function (itemCategoryStatus) {
    var item_category_status = itemCategoryStatus == "1" ? "1" : getStr(Ext.getCmp("txtItemCategoryStatus").jitGetValue());
    var store = Ext.getStore("itemCategoryStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_item_category";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        item_category_id: getStr(Ext.getCmp("txtItemCategory").jitGetValue()),
        item_category_status: item_category_status
    };
    store.load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "ItemCategoryEdit",
        title: "商品分类",
        url: "ItemCategoryEdit.aspx?ItemCategoryId=" + id
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
        url: JITPage.HandlerUrl.getValue() + "&method=delete_item_category",
        params: { ids: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
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

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "74px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtItemCategory").show(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtItemCategory").hide(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}

