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

    fnBindEvents();
    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/MenuHandler.ashx?mid=" + __mid);

    Ext.Ajax.request({
        url: "/Framework/Javascript/Biz/Handler/AppSysHandler.ashx?method=get_app_sys_list",
        method: 'post',
        success: function (response) {
            debugger;
            var jdata = Ext.decode(response.responseText);
            if (jdata.data.length > 0) {
                Ext.getCmp("txtAppSys").setValue(jdata.data[0].Def_App_Id);
                var menuStore = Ext.getStore('meauTreeStore');
                menuStore.proxy.url = "Handler/MeauTreeHandler.ashx?reg_app_id=" + jdata.data[0].Def_App_Id;
                menuStore.load();

            }
            else {
                var menuStore = Ext.getStore('meauTreeStore');
                menuStore.proxy.url = "Handler/MeauTreeHandler.ashx";
                menuStore.load();
            }

        }
    });
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "MenuEdit",
        title: "菜单",
        url: "MenuEdit.aspx?mid=" + __mid
    });
    win.show();
}

fnSearch = function () {
    debugger
    var menuStore = Ext.getStore('meauTreeStore');
    menuStore.proxy.url = "Handler/MeauTreeHandler.ashx?reg_app_id=" + Ext.getCmp("txtAppSys").jitGetValue();
    menuStore.load();

}

function fnRefsh(id) {
    debugger;
    //    id = Ext.getStore('meauTreeStore').getNodeById(id).parentNode.internalId
    //    Ext.getStore('meauTreeStore').proxy.url = "Handler/MeauTreeHandler.ashx?reg_app_id=" + Ext.getCmp("txtAppSys").jitGetValue();
    //    Ext.getStore("meauTreeStore").load({
    //        params: {}
    //        , callback: function (r, options, success) {
    //            if (r != undefined && r != null) {
    //                for (var i = 0; i < r.length; i++) {
    //                    if (r[i].data.ID == id) {
    //                        Ext.getCmp("meauTree").getSelectionModel().select(r[i]);
    //                        return;
    //                    }
    //                    if (r[i].childNodes != null) {
    //                        fnSelectTreeNode(r[i].childNodes, id);
    //                    }
    //                }
    //            }
    //        }
    //    });

    debugger; // Ext.getStore('meauTreeStore').getNodeById(record.get("id")).parentNode.expand(); 
    //var menuStore = Ext.getStore('meauTreeStore');
    Ext.getCmp("meauTree").on("load", function () { Ext.getStore('meauTreeStore').getNodeById(id).parentNode.expand(); });
    Ext.getStore('meauTreeStore').load();
}

fnSelectTreeNode = function (r, id) {
    if (r != null && r.length > 0) {
        for (var i = 0; i < r.length; i++) {
            if (r[i].data.ID == id) {
                Ext.getCmp("meauTree").getSelectionModel().select(r[i]);
                return;
            }
            if (r[i].childNodes != null) {
                fnSelectTreeNode(r[i].childNodes, id);
            }
        }
    }
};
function fnView(id) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "MenuEdit",
        title: "菜单",
        url: "MenuEdit.aspx?menu_id=" + id
    });
    win.show();
}
function fnDelete(id) {
    if (!confirm("确认删除?")) return;
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=menu_delete",
        params: { ids: id },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
                return;
            }
            fnRefsh();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}


function fnBindEvents() {
    //树的上下文菜单
    var treePanel = Ext.getCmp('meauTree');
    treePanel.on('itemcontextmenu', this.fnTrpMeauTree_ItemContextMenu_Click, this);

    //树上下文菜单项的点击事件
    var ctnMenuItemAdd = Ext.getCmp('ctnMenuItemAdd');
    ctnMenuItemAdd.on('click', this.fnCtnMenuItemAdd_Click);

}
/*@private
商品分类树面板的右键上下文菜单事件处理
*/
function fnTrpMeauTree_ItemContextMenu_Click(view, record, item, index, e, options) {
    var menu = Ext.getCmp('ctnMenu');
    menu.showAt(e.getXY());
    menu.__current_record = record; //菜单引用当前选中的记录
    e.stopEvent();
}

/*
@private
[上下文菜单项 - 添加]的点击事件处理
*/
function fnCtnMenuItemAdd_Click(view, record, item, e, options) {
    var menu = Ext.getCmp('ctnMenu');
    var parent = menu.__current_record;
    var id = parent.get('id');
    debugger;
    Ext.Msg.confirm("请确认", "是否要删除数据？", function (button) {
        if (button == "yes") {
            url: JITPage.HandlerUrl.getValue() + "&method=menu_delete",
            Ext.Ajax.request({
                url: JITPage.HandlerUrl.getValue() + "&method=menu_delete",
                params: {
                    ids: id
                },
                method: 'POST',
                callback: function (options, success, response) {
                    debugger;
                    var jdata = Ext.JSON.decode(response.responseText);
                    var record = Ext.getCmp('meauTree').getStore().getNodeById(id);
                    Ext.getCmp("meauTree").on("load", function () { Ext.getStore('meauTreeStore').getNodeById(record.get("id")).parentNode.expand(); });
                    Ext.getStore('meauTreeStore').load();

                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '删除成功',
                            buttons: Ext.Msg.OK
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                },
                failure: function () {
                    Ext.Msg.alert("提示", "获取参数数据失败");
                }
            });
        }
    });

}
