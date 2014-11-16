Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

var myMask;
var ids;
var eventId;

Ext.onReady(function () {
    myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: JITPage.Msg.SubmitData
        , removeMask: true //完成后移除  
    });

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
        height: 200,
        id: "EventVipEdit",
        title: "创建员工信息",
        url: "EventVipEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch() {
    eventId = Ext.getCmp("txtEvent").jitGetValue();

    if (Ext.getCmp("txtEvent").jitGetValue() != "") {
        Ext.getStore("eventEventVipStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=EventVip_query";
        Ext.getStore("eventEventVipStore").pageSize = JITPage.PageSize.getValue();
        Ext.getStore("eventEventVipStore").proxy.extraParams = {
            form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
            , eventId: Ext.getCmp("txtEvent").jitGetValue()
        };

        Ext.getStore("eventEventVipStore").loadPage(1);
    }
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 200,
        id: "EventVipEdit",
        title: "联系人内容",
        url: "EventVipEdit.aspx?id=" + id
    });

    win.show();
}

fnReset = function () {
    //Ext.getCmp("txtVipName").jitSetValue("");
    //Ext.getCmp("txtEnterpriseCustomerId").setValue("");
    //get("hECCustomerId").value = "";
    //Ext.getCmp("txtStatus").jitSetValue("");
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        //document.getElementById("view_Search").style.height = "118px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtSign").show();
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        //Ext.getCmp("searchPanel").doLayout();
    } else {
        //document.getElementById("view_Search").style.height = "86px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtSign").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        //Ext.getCmp("searchPanel").doLayout();
    }
}

function fnGenerateQR() {
    myMask.show();
    debugger;
    ids = JITPage.getSelected({
        gridView: Ext.getCmp("gridView"),
        id: "SignUpID"
    });

    Ext.Ajax.request({
        method: 'POST',
        async: true,
        url: JITPage.HandlerUrl.getValue() + '&method=GenerateQR',
        params: {
            "ids": ids
        },
        timeout: 900000,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("生成二维码失败：" + d.msg);
            } else {
                if (d.status == null || d.status == "")
                    showSuccess("生成二维码成功");
                else
                    showSuccess(d.status + ":" + d.msg);
            }
            myMask.hide();
        },
        failure: function (result) {
            showError("生成二维码失败：" + result.responseText);
            myMask.hide();
        }
    });
}

function fnPushWeixin() {
    ids = JITPage.getSelected({
        gridView: Ext.getCmp("gridView"),
        id: "SignUpID"
    });

    if (ids == null || ids.length < 1) {
        if (!confirm("请注意：确认发送给所有签到会员？")) return;
    }
    else {
        if (!confirm("确认发送给选中的" + ids.length + "个签到会员？")) return;
    };

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "EventVipPushMessage",
        title: "联系人内容",
        url: "EventVipPushWeixin.aspx"
    });

    win.show();
}

function UnRegister() {
    if (!confirm("确认取消注册？")) return;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=Unregister",
        params: { ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "EventVipID" }) },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
                return;
            }
            else
                Ext.Msg.alert("提示", "操作成功");
            fnSearch();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}

function fnDelete() {
    myMask.show();
    if (!confirm("确认删除？")) return;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=DeleteEventVip",
        params: { ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "EventVipID" }) },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
            }
            else
                Ext.Msg.alert("提示", "操作成功");

            fnSearch();
            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
            myMask.hide();
        }
    });
}

function fnForbidLottery() {
    ids = JITPage.getSelected({
        gridView: Ext.getCmp("gridView"),
        id: "EventVipID"
    });

    Ext.MessageBox.prompt("输入框", "请输入需屏蔽的用户编号："
        , function (button, nos) {
            if (button == "ok") {
                if (nos != "") {
                    if (!confirm("确认屏蔽？")) return;
                    fnForbid(nos);
                }
                else
                    Ext.MessageBox.alert("提示", "编号不能为空");
            }
        }
        , this
        , true
        , ids
    );
}

function fnForbid(nos) {
    myMask.show();
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=ForbidLottery",
        params: { nos: nos },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
            }
            else
                Ext.Msg.alert("提示", "操作成功");
            fnSearch();
            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
            myMask.hide();
        }
    });
}