
Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WEventsHandler.ashx?mid=");
    fnSearch();
    myMask.hide();
});

var loadOp = false;
function fnSearch() {
    var EventID = new String(JITMethod.getUrlParam("EventID"));
    Ext.getStore("EventVipTicketStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetEventsVipData";
    Ext.getStore("EventVipTicketStore").pageSize = 15;
    Ext.getStore("EventVipTicketStore").proxy.extraParams = {
        EventID: EventID
    };
    Ext.getCmp("wpageBar").moveFirst();
}

  


