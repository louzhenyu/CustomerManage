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
        title: "会员秀内容",
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
        title: "会员秀内容",
        url: "EventsEdit.aspx?EventID=" + id
    });

    win.show();
}

function fnViewImage(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "ImageList",
        title: "图片",
        url: "ImageList.aspx?EventID=" + id
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
        url: "EventsUserList2.aspx?EventID=" + id
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
        url: "EventsPrizesList.aspx?EventID=" + id
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
        url: "EventsLotteryLogList.aspx?EventID=" + id
    });

    win.show();
}

function fnDelete(id, val) {
    if (val == "1") {
        if (!confirm("确认停用?")) return;
    }
    if (val == "0") {
        if (!confirm("确认启用?")) return;
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=events_delete",
        params: { ids: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d.success) {
                alert("操作成功");
                fnSearch();
                return;
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}
function fnPraise(id, vipId) {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=events_praise",
        params: { id: id, vipId: vipId },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d.success) {
                alert("操作成功");
                fnSearch();
                return;
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}
function fnPass(id, val) {
    if (val == "1") {
        if (!confirm("确认通过?")) return;
    }
    if (val == "0") {
        if (!confirm("确认不通过?")) return;
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=events_pass",
        params: { id: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (d.success) {
                alert("操作成功");
                fnSearch();
                return;
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}

fnReset = function (){
    Ext.getCmp("txtVipName").jitSetValue("");
    Ext.getCmp("txtExperience").jitSetValue("");
    Ext.getCmp("txtItemName").jitSetValue("");
    Ext.getCmp("txtPublishDate").jitSetValue("");
}
