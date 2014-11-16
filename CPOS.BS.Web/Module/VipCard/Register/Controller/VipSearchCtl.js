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
    JITPage.HandlerUrl.setValue("Handler/RegisterHandler.ashx?mid=");

    fnLoad();
});

fnLoad = function () {
    var store = Ext.getStore("VipStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_vip_list&vipName="
        + getUrlParam("vipName") + "&phone=" + getUrlParam("phone");
    store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp('gridVipSearch').getStore().getProxy().startParam = 0;
    Ext.getCmp('pageBar').moveFirst();
    store.load();
}

fnSave = function () {
    var data = {};
    data.vips = [];

    var d = Ext.getCmp('gridVipSearch').getSelectionModel().getSelection();
    if (d != null) {
        for (var i = 0; i < d.length; i++) {
            data.vips.push(d[i].data);
        }
    }

    parent.fnSetSelectData(data.vips);

    fnCloseWin();
}
function fnCloseWin() {
    CloseWin('VipSearch');
}