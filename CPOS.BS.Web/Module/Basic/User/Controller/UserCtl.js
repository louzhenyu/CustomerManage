Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/UserHandler.ashx?mid=" + __mid);
    Ext.getCmp("txtStatus").jitSetValue("1");
    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "UserEdit",
        title: "用户",
        url: "UserEdit.aspx?mid=" + __mid
    });
    win.show();
}
function fnRevert(id) {
    Ext.Msg.confirm("请确认", "是否要重置密码？", function (button) {
        if (button == "yes") {
            url: JITPage.HandlerUrl.getValue() + "&method=revertPassword",
            Ext.Ajax.request({
                url: JITPage.HandlerUrl.getValue() + "&method=revertPassword",
                params: {
                    user: id,
                    password: '888888'

                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '重置成功,' + "密码:888888",
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: fnSearch()
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
            });
        }
    });

}

fnSearch = function () {
    var store = Ext.getStore("userStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_user";   //设置代理请求的路径
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    store.load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "UserEdit",
        title: "角色",
        url: "UserEdit.aspx?user_id=" + id
    });
    win.show();
}
function fnDelete(id, val) {
    if (val == "-1") {
        if (!confirm("确认停用?")) return;
    }
    if (val == "1") {
        if (!confirm("确认启用?")) return;
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=user_delete",
        params: { ids: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
                return;
            } else {
                alert("操作成功")
            }
            fnSearch();
        },
        failure: function () {
            Ext.Msg.alert("提示", "提交数据失败");
        }
    });
    return true;
}

