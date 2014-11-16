Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.HandlerUrl.setValue("Handler/EventListHandler.ashx?mid=" + __mid);

    var MarketEventId = getUrlParam("id");
    //MarketEventId = "41D86F55613649539C480E697ADA9EBB";
    //alert(MarketEventID)

    fnSearch(MarketEventId);
});

function fnSearch(MarketEventId) {
    //debugger;
    Ext.getStore("responsePersonStore").proxy.url = "Handler/EventListHandler.ashx?mid=&method=eventResponse_query&eventId=" + MarketEventId;
    Ext.getStore("responsePersonStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("responsePersonStore").proxy.extraParams = {
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("responsePersonStore").load();

}




