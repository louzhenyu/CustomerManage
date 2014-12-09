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
    JITPage.HandlerUrl.setValue("Handler/PropHandler.ashx?mid=" + __mid);
    
    fnLoadTree();
    fnSearch();
});

function fnCreate() {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) {
        alert("请选择属性域");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PropEdit",
        title: "属性",
        url: "PropEdit.aspx?domain=" + ApplicationId +
            "&ParentPropId=" + get("tree_selected").value
    });
	win.show();
}

fnSearch = function() {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) return;
    var parentId = get("tree_selected").value;
    if (parentId == undefined) {
        parentId = null;
    }
    
    if (ApplicationId == "SKU" && (parentId == "" || parentId=="-99")) {
        parentId = "-88";
    }

    var store = Ext.getStore("PropStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_prop";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        parentId: parentId,
        propType: get("tree_prop_type").value,
        ApplicationId: ApplicationId
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    store.load();
}
fnSearch2 = function() {
    get("tree_selected").value = "";
    get("tree_prop_type").value = "1";
    fnSearch();
}
fnAppSearch = function() {
    get("hAppId").value = Ext.getCmp("txtApplicationId").jitGetValue();
    fnSearch2();
    fnLoadTree();
}
fnAppSave = function() {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) {
        alert("请选择公众平台");
        return;
    }
    if (!confirm("您发布的微信菜单会在24小时后生效，请确认！")) return;
    var flag;
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/basic/prop/Handler/PropHandler.ashx?method=Prop_publish&ApplicationId=' + ApplicationId, 
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

fnLoadTree = function() {
    var ApplicationId = get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) return;
    var id = get("tree_selected").value;
    Ext.getStore("PropParentStore").proxy.url = JITPage.HandlerUrl.getValue() + 
        "&method=search_prop_tree&ApplicationId=" + ApplicationId;
    Ext.getStore("PropParentStore").load({
        params: {  }
        ,callback: function(r, options, success) {
            if (r != undefined && r != null) { 
                for (var i = 0; i < r.length; i++) {
                    if (r[i].data.ID == id) {
                        Ext.getCmp("span_tree1").getSelectionModel().select(r[i]);
                        return;
                    }
                    if (r[i].childNodes != null){
                        fnSelectTreeNode(r[i].childNodes, id);
                    }
                }
            }
        }
    });
}
fnSelectTreeNode = function(r, id) {
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
        id: "PropEdit",
        title: "属性",
        url: "PropEdit.aspx?PropId=" + id + "&domain=" + ApplicationId +
            "&ParentPropId=" + get("tree_selected").value
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "Prop_Id" }),
        url: JITPage.HandlerUrl.getValue() + "&method=prop_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "Prop_Id" })
        },
        handler: function () {
            Ext.getStore("PropStore").load({
                params: {  }
                ,callback: function(r, options, success) {
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

