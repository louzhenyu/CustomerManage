var id = JITMethod.getUrlParam("id");
var type = JITMethod.getUrlParam("type");
var pnlSearch; //查询pannel
var pnlWork; //操作pannel
var btnAdd; //增加
var gridStoreList; //终端数据表

Ext.onReady(function () {
    JITPage.HandlerUrl.setValue("Handler/VipClear_InvalidHandler.ashx?mid=" + __mid);
    InitView();
});