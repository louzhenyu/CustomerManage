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
    JITPage.HandlerUrl.setValue("Handler/EventListHandler.ashx?mid=" + __mid);

    fnSearch();
});

//function fnCreate() {
//    var win = Ext.create('jit.biz.Window', {
//        jitSize: "large",
//        height: 550,
//        id: "NewsEdit",
//        title: "新闻内容",
//        url: "NewsEdit.aspx?mid=" + __mid + "&op=new"
//    });

//    win.show();
//}

function fnSearch() {
    //debugger;
    Ext.getStore("eventListStore").proxy.url = "Handler/EventListHandler.ashx?mid=&method=eventlist_query";
    Ext.getStore("eventListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventListStore").proxy.extraParams = {
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("eventListStore").load();

}

function fnModify(id) {
    //alert(id);
    location.href = "../EventCreate/Event.aspx?mid=3ABBEAAA6D134C66BCC8AD3C99E7A5CA&MarketEventID=" + id;
}

function fnRun(id) {
    //    location.href = "ResponsePersonList.aspx?mid=&id=" + id;
    location.href = "EventRun.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=" + id;
}

function fnAnalysis(id) {
    //alert(id);
    location.href = "EventAnalysisList.aspx?mid=ABD7E597BA9345A3B7D8282A075F6F2A&id=" + id;
}



