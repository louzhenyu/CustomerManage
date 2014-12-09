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
    
    //加载树形结构
    fnLoadTree();
    //查询
    fnSearch();

    $("#span_create").hide();
});





function fnCreate(type) {
        
    var ApplicationId =  get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) {
        alert("请选择属性域");
        return;
    }

    var tree_selected = get("tree_selected").value;
    if (type==1) {
        tree_selected = "";
    }
    else if(type == 2)
    {
        //添加规格值时，只能给下拉添加
        if (get("propInputFlag").value!="select") {
            alert("只能给下拉选项的规格设置规格值。");
            return;
        }
    }


    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PropEdit",
        title: "属性",
        url: "PropSkuEdit.aspx?domain=" + ApplicationId +
            "&ParentPropId=" + tree_selected
            + "&type=" + type
            + "&propInputFlag=" + get("propInputFlag").value
    });
    win.show();
}

fnSearch = function () {
    var ApplicationId =  get("hAppId").value;
    if (ApplicationId == null || ApplicationId.length == 0) return;
    var parentId = get("tree_selected").value;

    if (parentId=="") {
        parentId = "-88";
    }

    if (parentId == undefined) {
        parentId = null;
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
fnSearch2 = function () {
    get("tree_selected").value = "-88";
    get("tree_prop_type").value = "1";
    fnSearch();
}

/*
选择属性域屏蔽，sku修改用上了。

fnAppSearch = function () {
    get("hAppId").value = "SKU"; // Ext.getCmp("txtApplicationId").jitGetValue();
    fnSearch2();
    fnLoadTree();
}
*/

/*
fnAppSave = function () {
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
*/

fnLoadTree = function () {
    var ApplicationId = "SKU";
    if (ApplicationId == null || ApplicationId.length == 0) return;
    var id = get("tree_selected").value;
    Ext.getStore("PropParentStore").proxy.url = JITPage.HandlerUrl.getValue() +
        "&method=search_prop_tree&ApplicationId=" + ApplicationId;
    Ext.getStore("PropParentStore").load({
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
    var ApplicationId =  get("hAppId").value;
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "PropEdit",
        title: "属性",
        url: "PropSkuEdit.aspx?PropId=" + id + "&domain=" + ApplicationId +
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
                params: {}
                , callback: function (r, options, success) {
                    fnLoadTree();
                }
            });
        }
    });
}

function fnDeleteByMenu(id) {
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=prop_delete",
        params: {
            ids: id
        },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            Ext.getStore("PropStore").load({
                params: {}
               , callback: function (r, options, success) {
                   fnLoadTree();
               }
            });
        },
        failure: function () {
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

