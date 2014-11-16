
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
    JITPage.HandlerUrl.setValue("Handler/OrderIntegralHandler.ashx?mid=" + __mid);

    //查询
    fnSearch();
});


//查询方法
function fnSearch() {

    var storeId = "orderIntegralStore";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=GetOrderIntegralList";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getStore(storeId).load({
        params: {},
        callback: function (d) {

        }
    });

}


//导出订单
function fnSearchExcel() {

    //确定是否导出当前数据
    Ext.MessageBox.confirm('提示信息', '确定导出当前数据?', function ex(btn) {
        if (btn == 'yes') {
            //导出当前数据
            window.open(JITPage.HandlerUrl.getValue() + "&method=Export&param=" + Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
        }
    });

}
