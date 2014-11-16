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
    JITPage.HandlerUrl.setValue("Handler/EventsHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventsEdit",
        title: "活动内容",
        url: "EventsEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch() {
    //var beginDate = Ext.getCmp("txtPublishDateBegin").jitGetValue();
    //var endDate = Ext.getCmp("txtPublishDateEnd").jitGetValue();
    //if (endDate != "" && beginDate > endDate) {
    //    alert("起始时间不能大于终止时间");
    //    return;
    //}
    Ext.getStore("eventsStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=events_query";
    //alert(Ext.getCmp("txtPublishDateBegin").getValue())
    Ext.getStore("eventsStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("eventsStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("eventsStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventsEdit",
        title: "活动内容",
        url: "EventsEdit.aspx?ForumId=" + id
    });

    win.show();
}

function fnViewUserList(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventsUserList",
        title: "活动人员",
        url: "EventsUserList.aspx?ForumId=" + id
    });

    win.show();
}

function fnViewUserList2(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventsUserList",
        title: "活动人员",
        url: "EventsUserList2.aspx?ForumId=" + id
    });

    win.show();
}

function fnViewPrizesList(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventsPrizesList",
        title: "活动奖品",
        url: "EventsPrizesList.aspx?ForumId=" + id
    });

    win.show();
}

function fnViewLotteryLogList(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventsLotteryLogList",
        title: "抽奖日志",
        url: "EventsLotteryLogList.aspx?ForumId=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "ForumId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=events_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "ForumId"
            })
        },
        handler: function () {
            Ext.getStore("eventsStore").load();
        }
    });
}

fnReset = function (){
    Ext.getCmp("txtTitle").jitSetValue("");
    Ext.getCmp("txtCity").jitSetValue("");
    Ext.getCmp("txtForumType").jitSetValue("");
    Ext.getCmp("txtCourse").jitSetValue("");
    Ext.getCmp("txtBeginTime").jitSetValue("");
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "118px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtBeginTime").show(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "86px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtBeginTime").hide(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}
