Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/RoleHandler.ashx?mid=");
    
    
    var role_id = new String(JITMethod.getUrlParam("role_id"));
    if (role_id != "null" && role_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_role_by_id",
            params: { role_id: role_id },
            method: 'post',
            success: function (response) {
                var storeId = "roleEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtAppSys").setReadOnly(true);
                Ext.getCmp("txtAppSys").setValue(d.Def_App_Id);
                //Ext.getCmp("txtAppSys").jitSetValue([{ "id":d.Def_App_Id, "text":d.Def_App_Name }]);

                Ext.getCmp("txtRoleCode").setValue(getStr(d.Role_Code));
                Ext.getCmp("txtRoleName").setValue(getStr(d.Role_Name));
                Ext.getCmp("txtRoleEnglish").setValue(getStr(d.Role_Eng_Name));

                Ext.getCmp("txtIsSys").setDefaultValue(getStr(d.Is_Sys));
                
                //Ext.getCmp("txtPurchaseUnitId").jitSetValue([{ "id":d.Purchase_unit_id, "text":d.Purchase_unit_name }]);
                
                Ext.getCmp("txtCreateUserName").setValue(getStr(d.Create_User_Name));
                Ext.getCmp("txtCreateTime").setValue(getStr(d.Create_Time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.Modify_User_Name));
                Ext.getCmp("txtModifyTime").setValue(getStr(d.Modify_Time));
                
                fnLoadMenus(d.Def_App_Id, d.Role_Id);

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        myMask.hide();
    }
    
});

function fnClose() {
    CloseWin('RoleEdit');
}

function fnSave() {
    var role = {};

    var hdAppSysCtrl = Ext.getCmp("txtAppSys");
    var tbRoleCodeCtrl = Ext.getCmp("txtRoleCode");
    var tbRoleNameCtrl = Ext.getCmp("txtRoleName");
    var tbRoleEnglishCtrl = Ext.getCmp("txtRoleEnglish");
    var hdIsSysCtrl = Ext.getCmp("txtIsSys");

    var role_Id = getUrlParam("role_id");
    role.Def_App_Id = hdAppSysCtrl.getValue();
    role.Role_Id = getUrlParam("role_id");
    role.Role_Code = tbRoleCodeCtrl.getValue();
    role.Role_Name = tbRoleNameCtrl.getValue();
    role.Role_Eng_Name = tbRoleEnglishCtrl.getValue();
    role.Is_Sys = hdIsSysCtrl.getValue();
        
    role.RoleMenuInfoList = [];
    var checkedData = treeMenu.getView().getChecked();
    for(var i = 0; i < checkedData.length; i++) {
        var tmp = checkedData[i].raw;
        tmp.Role_Id = role.Role_Id;
        role.RoleMenuInfoList.push(tmp);
    }

    if (role.Role_Code == null || role.Role_Code == "") {
        showError("请填写角色编码");
        return;
    }
    if (role.Role_Name == null || role.Role_Name == "") {
        showError("请填写角色名称");
        return;
    }
    if (role.Is_Sys == null || role.Is_Sys == "") {
        showError("请选择是否系统保留");
        return;
    }
        
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/RoleHandler.ashx?method=role_save&role_id=' + role_Id,
        params: { "role": Ext.encode(role) },
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


function fnLoadMenus(app_sys_id, role_id) {
    //alert(app_sys_id);return;
    get("treeMenu").innerHTML = "";
    app_sys_id = getStr(app_sys_id);
    role_id = getStr(role_id);
    var menus_data = { };
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/Basic/Role/Handler/RoleHandler.ashx?method=get_sys_menus_by_role_id&role_id=' + role_id + 
            '&app_sys_id=' + app_sys_id,
        params: { },
        sync: true,
        async : false,
        success: function(result, request) {
            var d =  Ext.decode(result.responseText);
            if (d.data != null) {
                menus_data = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure : function() {
            showInfo("页面超时，请重新登录");
        }
    });

    var storeMenus = Ext.create('Ext.data.TreeStore', {
        root: {
            expanded: true,
            children: menus_data
        },
        fields: [
            {name: 'id', type: 'string', mapping: 'Menu_Id'},
            {name: 'text', type: 'string', mapping: 'Menu_Name'},
            {name: 'checked', type: 'boolean', mapping: 'check_flag'},
            {name: 'expanded', type: 'boolean', mapping: 'expanded_flag'},
            {name: 'leaf', type: 'boolean', mapping: 'leaf_flag'},
            {name: 'cls', type: 'string', mapping: 'cls_flag'}
        ]
    });

    treeMenu = Ext.create('Ext.tree.Panel', {
        store: storeMenus,
        rootVisible: false,
        useArrows: true,
        frame: false,
        title: '',
        renderTo: 'treeMenu',
        width: 372,
        height: 268
    });

	
}
