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
    JITPage.HandlerUrl.setValue("Handler/OrderHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PurchaseReturnOrderEdit",
        title: "采购退货",
        url: "PurchaseReturnOrderEdit.aspx?mid=" + __mid + "&op=new"
    });
    win.show();
}

function fnSearch() {
    Ext.getStore("purchaseReturnOrderStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=purchase_return_order";
    Ext.getStore("purchaseReturnOrderStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("purchaseReturnOrderStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        purchase_unit_id: Ext.getCmp("txtPurchaseUnit").jitGetValue()
    };

    Ext.getStore("purchaseReturnOrderStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PurchaseReturnOrderEdit",
        title: "采购退货",
        url: "PurchaseReturnOrderEdit.aspx?order_id=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "order_id"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=order_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "order_id"
            })
        },
        handler: function () {
            Ext.getStore("purchaseReturnOrderStore").load();
        }
    });
}

function fnPass(id) {
    if (!confirm("确认审核?")) return;
    if (id == undefined || id == null) id = "";
    Ext.Ajax.request({
        method: 'GET',
        sync: true,
        url: 'Handler/OrderHandler.ashx?method=order_pass&order_id=' + id,
        params: {},
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (!d.success) {
                alert("操作失败：" + d.msg);
            } else {
                alert("审核成功");
                fnSearch();
            }
        },
        failure: function (result) {
            alert("操作失败：" + result.responseText);
        }
    });
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "74px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtOrderDate").hidden = false;
        Ext.getCmp("txtOrderDate").setVisible(true);
        Ext.getCmp("txtRequestDate").hidden = false;
        Ext.getCmp("txtRequestDate").setVisible(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtOrderDate").hidden = true;
        Ext.getCmp("txtOrderDate").setVisible(false);
        Ext.getCmp("txtRequestDate").hidden = true;
        Ext.getCmp("txtRequestDate").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}