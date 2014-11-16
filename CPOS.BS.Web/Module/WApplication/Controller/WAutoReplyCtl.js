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
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WAutoReplyEdit",
        title: "关键字自动回复",
        url: "WAutoReplyEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function() {
    var WAutoReplyId = "1";
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=get_WAutoReply_by_id",
        params: { WAutoReplyId: WAutoReplyId },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText).data;
            if (d != null) { 
                Ext.getCmp("txtApplicationId").fnLoad(function(){
                    Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));
                    Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));
                    Ext.getCmp("txtApplicationId").ApplicationId = getStr(d.ApplicationId);
                });
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}
fnSave = function() {
    var flag;
    var WAutoReply = {};
    WAutoReply.ReplyId = "1";
    WAutoReply.NewsType = "1";
    WAutoReply.ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    WAutoReply.ModelId = Ext.getCmp("txtModelId").jitGetValue();

    if (WAutoReply.ApplicationId == null || WAutoReply.ApplicationId == "") {
        showError("请填写公共平台");
        return;
    }
    if (WAutoReply.ModelId == null || WAutoReply.ModelId == "") {
        showError("请填写模板");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WAutoReply_save&WAutoReplyId=' + WAutoReply.ReplyId, 
        params: {
            "item": Ext.encode(WAutoReply)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WAutoReplyEdit",
        title: "关键字自动回复",
        url: "WAutoReplyEdit.aspx?WAutoReplyId=" + id
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ReplyId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WAutoReply_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ReplyId" })
        },
        handler: function () {
            Ext.getStore("WAutoReplyStore").load();
        }
    });
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "74px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtTel").show(true);
        Ext.getCmp("txtStatus").show(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtTel").hide(true);
        Ext.getCmp("txtStatus").hide(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}

