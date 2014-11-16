/*Jermyn 2013-04-02*/

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
    JITPage.HandlerUrl.setValue("Handler/StoreBalanceHandler.ashx?mid=" + __mid);
    fnSearch();
//    var tt = fnGetStockNum("000C6D4FF3B94DE9BE7638E524EE1278","","");
});



function fnSearch() {
//    debugger;
    Ext.getStore("storeQueryStore").proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=StoreQuery";
    Ext.getStore("storeQueryStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("storeQueryStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        unit_id: Ext.getCmp("txtUnit").jitGetValue()
    };
    Ext.getStore("storeQueryStore").load();
}

function fnReset() {

    var hdUnitIdCtrl = Ext.getCmp("txtUnit");
    hdUnitIdCtrl.jitSetValueText(null);

    var hdWarehouseCtrl = Ext.getCmp("txtWarehouse");
    hdWarehouseCtrl.setValue(null);
}

function fnDownload() {
    var condition = {
        type: "stock",
        unit_id: Ext.getCmp("txtUnit").jitGetValue(),
        warehouse_id: Ext.getCmp("txtWarehouse").jitGetValue(),
        item_name: ""
    };
    $.post("Handler/ExportToExcel.ashx?mid=" + __mid, condition,
        function (data) {
            if (data) {
                if (data == "" || data == "/ExportTemp/") {
                    showInfo("未查询到数据");
                } else {
                    window.open(data);
                }
            }
        },
    "text");
    return false;
}

