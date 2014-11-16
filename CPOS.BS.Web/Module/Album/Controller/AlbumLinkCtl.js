var module_type = 1;

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
    JITPage.HandlerUrl.setValue("Handler/AlbumHandler.ashx?mid=");

    Ext.getCmp("txtAlbumModuleType").setDefaultValue(1);
    module_type = 1;

    fnSearch();
});

function fnSearch() {
    module_type = Ext.getCmp("txtAlbumModuleType").getValue();

    var store = Ext.getStore("AlbumModuleStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=album_module_query";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0,
        ModuleType: module_type,
        ModuleName: Ext.getCmp("txtModuleName").getValue()
    };
    Ext.getCmp('gridView').getStore().getProxy().startParam = 0;
    Ext.getCmp('pageBar').moveFirst();
    store.load();
}

function fnConfirm() {
    var moduleId = "";
    var moduleName = "";

    var d = Ext.getCmp('gridView').getSelectionModel().getSelection();
    if (d != null && d.length > 0) {
        for (var i = 0; i < d.length; i++) {
            moduleId = d[i].data.ID;
            moduleName = d[i].data.Title;
        }
    }
    else {
        alert("请选择模块");
        return;
    }

    if (module_type == null || module_type == undefined) {
        module_type = 1;
    }

    parent.fnSetLink(moduleId, module_type, moduleName);

    fnCloseWin();
}

function fnCloseWin() {
    CloseWin('AlbumLink');
}
