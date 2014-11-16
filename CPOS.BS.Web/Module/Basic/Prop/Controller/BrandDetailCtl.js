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
    JITPage.HandlerUrl.setValue("Handler/PropHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "BrandDetailEdit",
        title: "品牌属性",
        url: "BrandDetailEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function() {
    var store = Ext.getStore("BrandDetailStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_BrandDetail";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        //item_category_id: getStr(Ext.getCmp("txtItemCategory").jitGetValue())
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
        id: "BrandDetailEdit",
        title: "品牌属性",
        url: "BrandDetailEdit.aspx?item_id=" + id
    });
	win.show();
}
function fnDelete(id, val) {
    if (!confirm("确认删除?")) return;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=BrandDetail_delete",
        params: { ids: id },
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
//function fnDelete(id) {
//    JITPage.deleteList({
//        ids: JITPage.getSelected({
//            gridView: Ext.getCmp("gridView"),
//            id: "Item_Id"
//        }),
//        url: JITPage.HandlerUrl.getValue() + "&method=item_delete",
//        params: {
//            ids: JITPage.getSelected({
//                gridView: Ext.getCmp("gridView"),
//                id: "Item_Id"
//            })
//        },
//        handler: function () {
//            Ext.getStore("itemStore").load();
//        }
//    });
//}

