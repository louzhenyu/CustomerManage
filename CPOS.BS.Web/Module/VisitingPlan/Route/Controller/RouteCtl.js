Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/RouteHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    window.location = "RouteTab.aspx?mid=" + __mid + "&btncode=create";
}

function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetRouteList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        ClientStructureID: Ext.getCmp("ClientStructureID").jitGetValue(),
        ClientUserID: Ext.getCmp("ClientUserID").jitGetValue()
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
    Ext.getCmp("ClientStructureID").jitSetValue("");
    Ext.getCmp("ClientUserID").jitSetValue("");
}

function fnDelete() {
    JITPage.delList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "RouteID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "RouteID" })
        },
        handler: function () {
            Ext.getStore("routeStore").load();
        }
    });
}

function fnColumnUpdate(value, p, r) {

    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"RouteTab.aspx?mid=" + __mid + "&btncode=update&id=" + r.data.RouteID + "\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"RouteTab.aspx?mid=" + __mid + "&btncode=search&id=" + r.data.RouteID + "\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }

}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}

function fnColumnMustDo(value, p, r) {
    return (value == 1 ? "√" : "")
}
function fnRenderStatus(value, p, r) {
    return (value == 1 ? "启用" : "停止")
}