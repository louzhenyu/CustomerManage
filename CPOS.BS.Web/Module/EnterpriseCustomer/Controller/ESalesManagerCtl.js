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
    JITPage.HandlerUrl.setValue("Handler/ESalesManagerHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "ESalesEdit",
        title: "销售业务",
        url: "/Module/CRM/ESalesEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch() {
    Ext.getStore("salesStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=ESales_query";
    Ext.getStore("salesStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("salesStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        ECCustomerId: get("hECCustomerId").value
    };

    Ext.getStore("salesStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "ESalesEdit",
        title: "销售线索",
        url: "/Module/CRM/ESalesEdit.aspx?SalesId=" + id
    });

    win.show();
}

//function fnViewUserList(id, type) {
//    if (id == undefined || id == null) id = "";

//    var win = Ext.create('jit.biz.Window', {
//        jitSize: "large",
//        height: 550,
//        id: "EventsUserList",
//        title: "活动人员",
//        url: "EventsUserList.aspx?ForumId=" + id
//    });

//    win.show();
//}

//function fnViewUserList2(id, type) {
//    if (id == undefined || id == null) id = "";

//    var win = Ext.create('jit.biz.Window', {
//        jitSize: "large",
//        height: 550,
//        id: "EventsUserList",
//        title: "活动人员",
//        url: "EventsUserList2.aspx?ForumId=" + id
//    });

//    win.show();
//}

//function fnViewPrizesList(id, type) {
//    if (id == undefined || id == null) id = "";

//    var win = Ext.create('jit.biz.Window', {
//        jitSize: "large",
//        height: 550,
//        id: "EventsPrizesList",
//        title: "活动奖品",
//        url: "EventsPrizesList.aspx?ForumId=" + id
//    });

//    win.show();
//}

//function fnViewLotteryLogList(id, type) {
//    if (id == undefined || id == null) id = "";

//    var win = Ext.create('jit.biz.Window', {
//        jitSize: "large",
//        height: 550,
//        id: "EventsLotteryLogList",
//        title: "抽奖日志",
//        url: "EventsLotteryLogList.aspx?ForumId=" + id
//    });

//    win.show();
//}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "SalesId"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=ESales_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "SalesId"
            })
        },
        handler: function () {
            Ext.getStore("salesStore").load();
        }
    });
}

fnReset = function () {
    Ext.getCmp("txtSalesName").jitSetValue("");
    Ext.getCmp("txtEnterpriseCustomer").jitSetValue("");
    Ext.getCmp("txtSalesProduct").jitSetValue("");
    Ext.getCmp("txtSalesStage").jitSetValue("");
    Ext.getCmp("txtSalesVipId").jitSetValue("");
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "118px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtSalesVipId").show(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "86px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtSalesVipId").hide(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}
