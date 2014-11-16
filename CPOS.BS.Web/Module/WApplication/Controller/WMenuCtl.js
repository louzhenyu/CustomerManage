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
    debugger;
    fnDefault();
    fnLoadTree();
    fnSearch();
});
function fnDefault() {
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: "/Framework/Javascript/Biz/Handler/WApplicationInterfaceHandler.ashx?method=get_list",
        success: function (request) {
            debugger;
            var jdate = Ext.decode(request.responseText);
            if (jdate != null && jdate.data.length > 0) {
              get("hAppId").value = jdate.data[0].ApplicationId;
                Ext.getCmp("txtApplicationId").jitSetValue(jdate.data[0].ApplicationId)

            }
        }
    });

}

function fnCreate() {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) {
        alert("请选择微信账号");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMenuEdit",
        title: "菜单",
        url: "WMenuEdit.aspx?ApplicationId=" + ApplicationId
    });
    win.show();
}

fnSearch = function () {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) return;
    var parentId = get("tree_selected").value;
    if (parentId == undefined) {
        parentId = null;
    }
    var store = Ext.getStore("wMenuStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_wmenu";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        parentId: parentId,
        ApplicationId: ApplicationId
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    store.load();
}
fnSearch2 = function () {
    get("tree_selected").value = "";
    fnSearch();
}
fnAppSearch = function () {
    get("hAppId").value = Ext.getCmp("txtApplicationId").jitGetValue();
    fnSearch2();
    fnLoadTree();
}
fnAppSave = function () {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) {
        alert("请选择微信账号");
        return;
    }
    if (!confirm("您发布的微信菜单会在24小时后生效，请确认！")) return;
    var flag;
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WMenu_publish&ApplicationId=' + ApplicationId,
        params: {
    },
    success: function (result, request) {
        var d = Ext.decode(result.responseText);
        if (d.success == false) {
            showError("发布失败：" + d.msg);
            flag = false;
        } else {
            showSuccess("发布成功");
            flag = true;
        }
    },
    failure: function (result) {
        showError("发布失败：" + result.responseText);
    }
});
}

fnLoadTree = function () {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) return;
    var id = get("tree_selected").value;
    Ext.getStore("WMenuParentStore").proxy.url = JITPage.HandlerUrl.getValue() +
        "&method=search_wmenu_tree&ApplicationId=" + ApplicationId;
    Ext.getStore("WMenuParentStore").load({
        params: {}
        , callback: function (r, options, success) {
            if (r != undefined && r != null) {
                for (var i = 0; i < r.length; i++) {
                    if (r[i].data.ID == id) {
                        Ext.getCmp("span_tree1").getSelectionModel().select(r[i]);
                        return;
                    }
                    if (r[i].childNodes != null) {
                        fnSelectTreeNode(r[i].childNodes, id);
                    }
                }
            }
        }
    });
}
fnSelectTreeNode = function (r, id) {
    if (r != null && r.length > 0) {
        for (var i = 0; i < r.length; i++) {
            if (r[i].data.ID == id) {
                Ext.getCmp("span_tree1").getSelectionModel().select(r[i]);
                return;
            }
            if (r[i].childNodes != null) {
                fnSelectTreeNode(r[i].childNodes, id);
            }
        }
    }
};

function fnView(id) {
    var ApplicationId = get("hAppId").value;
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMenuEdit",
        title: "菜单",
        url: "WMenuEdit.aspx?WMenuId=" + id + "&ApplicationId=" + ApplicationId
    });
    win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ID" }),
        url: JITPage.HandlerUrl.getValue() + "&method=wmenu_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ID" })
        },
        handler: function () {
            Ext.getStore("wMenuStore").load({
                params: {}
                , callback: function (r, options, success) {
                    fnLoadTree();
                }
            });
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

