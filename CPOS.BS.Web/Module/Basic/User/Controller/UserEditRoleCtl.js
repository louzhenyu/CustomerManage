Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    JITPage.HandlerUrl.setValue("Handler/UserHandler.ashx?mid=");

    fnLoadData();
});

function fnLoadData() {
    
}

function fnSave() {
    var hdAppSysCtrl = Ext.getCmp("txtAppSys");
    var hdRoleCtrl = Ext.getCmp("txtRole");
    var hdUnitCtrl = Ext.getCmp("txtUnit");
    var rdDefaultFlagCtrl = Ext.getCmp("txtDefaultFlag");

    var appSysId = hdAppSysCtrl.getValue();
    if (appSysId == null || appSysId == "") {
        showError("请选择应用系统");
        return;
    }
    var roleId = hdRoleCtrl.getValue();
    if (roleId == null || roleId == "") {
        showError("请选择角色");
        return;
    }
    var unitId = hdUnitCtrl.getValue();
    if (unitId == null || unitId == "") {
        showError("请选择单位");
        return;
    }
    
    z_selected_data = {};

    var parentGrid = parent.Ext.getCmp("gridRole");
    var item = {};
    item.index = z_selected_data.index;
    item.Id = z_selected_data.Id;
    item.User_id = getUrlParam("user_id");
    item.App_sys_id = hdAppSysCtrl.jitGetValue();
    item.App_sys_name = hdAppSysCtrl.rawValue;
    item.RoleId = hdRoleCtrl.jitGetValue().toString();
    item.RoleName = hdRoleCtrl.rawValue;
    item.UnitId = hdUnitCtrl.jitGetValue().toString();
    item.UnitName = hdUnitCtrl.rawValue;

    item.DefaultFlag = rdDefaultFlagCtrl.getValue();
    item.DefaultFlagName = rdDefaultFlagCtrl.rawValue;
    
    var index = parentGrid.store.find("Id", item.Id);
    if (index > -1 &&
        z_selected_data.Id != null && 
        z_selected_data.Id != "") {
        parentGrid.store.removeAt(index);
        parentGrid.store.insert(index, item);
        parentGrid.store.commitChanges();
    } else {
        item.Id = newGuid();
        parentGrid.store.add(item);
        parentGrid.store.commitChanges();
    }
    fnCloseWin();
}
function fnClose() {
    fnCloseWin();
}
function fnCloseWin() {
    CloseWin('UserEditRole');
}

