/*全局变量*/
var id, btncode;
Ext.onReady(function () {

    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/VipClearHandler.ashx?mid=" + __mid);

    fnSearch();
});

/*查询方法*/
function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=SearchAllList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        pStartDate: Ext.getCmp("pStartDate").jitGetValue(),
        pEndDate: Ext.getCmp("pEndDate").jitGetValue()
    };
    Ext.getCmp("pageBar").moveFirst();
}

/*查看会员信息*/
function fnShowWindow(id, type) {
    var parameters = "?mid=" + __mid + "&id=" + id + "&type=" + type + "&r=" + Math.random();
    var src = "";
    switch (type) {
        case 1:
            src = "VipClear_Invalid.aspx" + parameters;
            break;
        case 2:
            src = "VipClear_Repeat.aspx" + parameters;
            break;
        case 3:
            src = "VipClear_EditVIP.aspx" + parameters;
            break;
        default:
            src = "VipClear_EditVIP.aspx" + parameters;
            break;
    }

    Ext.getCmp("showVipWin").show();
    window.frames["showVipFrame"].location.href = src;
}

