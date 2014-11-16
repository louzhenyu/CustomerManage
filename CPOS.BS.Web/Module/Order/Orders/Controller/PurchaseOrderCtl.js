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
    JITPage.HandlerUrl.setValue("Handler/OrderHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    //window.location = "PurchaseOrderEdit.aspx?mid=" + __mid;

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PurchaseOrderEdit",
        title: "采购订单",
        url: "PurchaseOrderEdit.aspx?mid=" + __mid + "&op=new"
    });
	win.show();
}

function fnSearch() {
    Ext.getStore("purchaseOrderStore").proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_purchase_order";
    Ext.getStore("purchaseOrderStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("purchaseOrderStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        purchase_unit_id: Ext.getCmp("txtPurchaseUnit").jitGetValue()
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    Ext.getStore("purchaseOrderStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PurchaseOrderEdit",
        title: "采购订单",
        url: "PurchaseOrderEdit.aspx?order_id=" + id
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "order_id" }),
        url: JITPage.HandlerUrl.getValue() + "&method=order_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "order_id" })
        },
        handler: function () {
            Ext.getStore("purchaseOrderStore").load();
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

function fnColumnUpdate(value, p, r) {
//    if (__getHidden("update")) {
//        //无权限
//        return "<a href=\"javascript:;\">" + value + "</a>";
//    }
//    else {
//        //有权限
//        return "<a style='color:blue;' href=\"ParameterEdit.aspx?mid=" + __mid + 
//            "&id=" + r.data.VisitingParameterID + "\">" + value + "</a>";
//    }
}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}

function fnColumnMustDo(value, p, r) {
    return (value == 1 ? "√" : "")
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