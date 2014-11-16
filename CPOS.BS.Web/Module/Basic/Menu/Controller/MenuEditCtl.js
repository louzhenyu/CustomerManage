Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();


    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/MenuHandler.ashx?mid=");

    var menu_id = new String(JITMethod.getUrlParam("menu_id"));
    if (menu_id != "null" && menu_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_menu_by_id",
            params: { menu_id: menu_id },
            method: 'post',
            success: function (response) {
                var storeId = "menuEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtAppSys").setReadOnly(true);
                Ext.getCmp("txtAppSys").setValue(d.Reg_App_Id);
                Ext.create('Jit.Biz.MenuSelectTree', {
                    id: "txtParentMenuId",
                    text: "",
                    renderTo: "txtParentMenuId",
                    width: 100

                });
                //Ext.getCmp("txtAppSys").jitSetValue([{ "id":d.Def_App_Id, "text":d.Def_App_Name }]);
                Ext.getCmp("txtClass").setValue(getStr(d.Icon_Path));
                Ext.getCmp("txtMenuCode").setValue(getStr(d.Menu_Code));
                Ext.getCmp("txtMenuName").setValue(getStr(d.Menu_Name));
                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.Display_Index));
                Ext.getCmp("txtUrl").setValue(getStr(d.Url_Path));

                Ext.getCmp("txtParentMenuId").jitSetValue([{ "id": d.Parent_Menu_Id, "text": d.Parent_Menu_Name}]);


                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        Ext.Ajax.request({
            url: "/Framework/Javascript/Biz/Handler/AppSysHandler.ashx?method=get_app_sys_list",//从根目录开始
            method: 'post',
            success: function (response) {
                debugger;
                var jdata = Ext.decode(response.responseText);
                if (jdata.data.length > 0) {
                    Ext.getCmp("txtAppSys").setValue(jdata.data[0].Def_App_Id);
                }

                Ext.create('Jit.Biz.MenuSelectTree', {
                    id: "txtParentMenuId",
                    text: "",
                    renderTo: "txtParentMenuId",
                    width: 100

                });

            }
        });
        myMask.hide();
    }

});

function fnClose() {
    CloseWin('MenuEdit');
}

function fnSave() {
    var menu = {};
    var hdAppSysCtrl = Ext.getCmp("txtAppSys");
    var tbMenuCodeCtrl = Ext.getCmp("txtMenuCode");
    var tbMenuNameCtrl = Ext.getCmp("txtMenuName");

    var menu_Id = getUrlParam("menu_id");
    menu.Reg_App_Id = hdAppSysCtrl.getValue();
    menu.Menu_Id = getUrlParam("menu_id");
    menu.Menu_Code = tbMenuCodeCtrl.getValue();
    menu.Menu_Name = tbMenuNameCtrl.getValue();
    menu.Parent_Menu_Id = Ext.getCmp("txtParentMenuId").jitGetValue();
    menu.Url_Path = Ext.getCmp("txtUrl").getValue();
    menu.Display_Index = Ext.getCmp("txtDisplayIndex").getValue();
    menu.icon_path = Ext.getCmp("txtClass").getValue();

    if (menu.Reg_App_Id == null || menu.Reg_App_Id == "") {
        showError("请选择应用系统");
        return;
    }
    if (menu.Menu_Code == null || menu.Menu_Code == "") {
        showError("请填写菜单编码");
        return;
    }
    if (menu.Menu_Name == null || menu.Menu_Name == "") {
        showError("请填写菜单名称");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/MenuHandler.ashx?method=menu_save&menu_id=' + menu_Id,
        params: { "menu": Ext.encode(menu) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnClose();
}


function fnLoadMenus(app_sys_id, menu_id) {
    debugger;
    if (app_sys_id == null || app_sys_id == "") {
        return;
    }
    //    app_sys_id = getStr(app_sys_id);
    //    menu_id = getStr(menu_id);
    //    var menus_data = {};
    //    Ext.Ajax.request({
    //        method: 'GET',
    //        url: "/Framework/Javascript/Biz/Handler/MenuSelectTreeHandler.ashx?method=get_menu_tree&parent_id=&reg_app_id=" + app_sys_id,
    //        params: {},
    //        sync: true,
    //        async: false,
    //        success: function (result, request) {
    //            debugger;
    //            var d = Ext.decode(result.responseText);
    //            if (d != null) {
    //                menus_data = d;
    //            } else {
    //                showInfo("页面超时，请重新登录");
    //            }
    //        },
    //        failure: function () {
    //            showInfo("页面超时，请重新登录");
    //        }
    //    });

    //    var storeMenus = Ext.create('Ext.data.TreeStore', {
    //        root: {
    //            expanded: true,
    //            children: menus_data
    //        },
    //        fields: [
    //            { name: 'id', type: 'string', mapping: 'Menu_Id' },
    //            { name: 'text', type: 'string', mapping: 'Menu_Name' },
    //            { name: 'checked', type: 'boolean', mapping: 'check_flag' },
    //            { name: 'expanded', type: 'boolean', mapping: 'expanded_flag' },
    //            { name: 'leaf', type: 'boolean', mapping: 'leaf_flag' },
    //            { name: 'cls', type: 'string', mapping: 'cls_flag' }
    //        ]
    //    });

    //    treeMenu = Ext.create('Ext.tree.Panel', {
    //        store: storeMenus,
    //        rootVisible: false,
    //        useArrows: true,
    //        frame: false,
    //        title: '',
    //        renderTo: 'treeMenu',
    //        width: 372,
    //        height: 268
    //    });
}
