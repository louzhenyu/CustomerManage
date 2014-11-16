Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/CallDayPlanningHandler.ashx?mid=" + __mid);

    //alert(new Date().getFullYear() + '-' + (new Date().getMonth()+1));
    //alert(new Date().getDate());
    var d = new Date();
    var y = d.getFullYear();
    var m = (d.getMonth() + 1) >= 10 ? (d.getMonth() + 1) : "0" + (d.getMonth() + 1);
    Ext.getCmp("CallDate").jitSetValue(y + '-' + m);

    fnSearch();
});

function fnCreate() {
    window.location = "CallDayPlanningEdit.aspx?mid=" + __mid;
}

function fnSearch() {
    if (Ext.getCmp("CallDate").jitGetValue() == null) {
        Ext.Msg.alert("提示", "请选择月度");
        return;
    }
    
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetUserCDPList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        ClientStructureID: Ext.getCmp("ClientStructureID").jitGetValue(),
        ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue(),
        ClientUserID: Ext.getCmp("ClientUserID").jitGetValue(),
        CallDate: Ext.getCmp("CallDate").jitGetValue()
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
    Ext.getCmp("ClientStructureID").jitSetValue("");
    Ext.getCmp("ClientPositionID").jitSetValue("");
    Ext.getCmp("ClientUserID").jitSetValue("");
    var d = new Date();
    var y = d.getFullYear();
    var m = (d.getMonth() + 1) >= 10 ? (d.getMonth() + 1) : "0" + (d.getMonth() + 1);
    Ext.getCmp("CallDate").jitSetValue(y + '-' + m);
}

function fnColumnUpdate(value, p, r) {

    var params = "?mid=" + __mid
        + "&ClientStructureID=" + Ext.getCmp("ClientStructureID").jitGetValue()
        + "&ClientPositionID=" + (Ext.getCmp("ClientPositionID").jitGetValue() == undefined ? "" : Ext.getCmp("ClientPositionID").jitGetValue())
        + "&ClientUserID=" + r.data.ClientUserID
        + "&CallDate=" + Ext.getCmp("CallDate").getRawValue();

    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"CallDayPlanningUserDate.aspx" + params + "&btncode=update\" \>" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"CallDayPlanningUserDate.aspx" + params + "&btncode=search\" \>" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }

}