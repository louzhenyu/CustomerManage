Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");
    
    //if (getUrlParam("ApplicationId")) {
    //    Ext.getCmp("txtWeiXinID").jitSetValue(getStr(getUrlParam("ApplicationId")));
    //}

    var WMenuId = getUrlParam("WMenuId");
    if (WMenuId != "null" && WMenuId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_wmenu_by_id",
            params: { WMenuId: WMenuId },
            method: 'POST',
            success: function (response) {
                var storeId = "wMenuEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;

                if (d.ParentId != null) {
                    Ext.getCmp("txtParentId").jitSetValue([{ "id": d.ParentId, "text": d.ParentName }]);
                }
                Ext.getCmp("txtName").jitSetValue(getStr(d.Name));
                //Ext.getCmp("txtWeiXinID").jitSetValue(getStr(d.ApplicationId));
                Ext.getCmp("txtName").jitSetValue(getStr(d.Name));
                Ext.getCmp("txtMenuURL").jitSetValue(getStr(d.MenuURL));
                //Ext.getCmp("txtKey").jitSetValue(getStr(d.Key));
                Ext.getCmp("txtType").jitSetValue(getStr(d.Type));
                //Ext.getCmp("txtLevel").jitSetValue(getStr(d.Level));
                Ext.getCmp("txtDisplayColumn").jitSetValue(getStr(d.DisplayColumn));
                Ext.getCmp("txtModelId").setDefaultValue(getStr(d.ModelId));

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
    CloseWin('WMenuEdit');
}

function fnSave() {
    var flag;
    var wMenu = {};
    wMenu.ID = getUrlParam("WMenuId");
    wMenu.ParentId =  Ext.getCmp("txtParentId").jitGetValue();
    if (wMenu.ParentId == undefined || wMenu.ParentId == null) wMenu.ParentId = "";
    //wMenu.ApplicationId = Ext.getCmp("txtWeiXinID").jitGetValue();
    wMenu.ApplicationId = getUrlParam("ApplicationId");
    wMenu.Name = Ext.getCmp("txtName").jitGetValue();
    wMenu.MenuURL = Ext.getCmp("txtMenuURL").jitGetValue();
    //wMenu.Key = Ext.getCmp("txtKey").jitGetValue();
    wMenu.Type = Ext.getCmp("txtType").jitGetValue();
    //wMenu.Level = Ext.getCmp("txtLevel").jitGetValue();
    wMenu.DisplayColumn = Ext.getCmp("txtDisplayColumn").jitGetValue();
    wMenu.ModelId = Ext.getCmp("txtModelId").jitGetValue();

    if (wMenu.ID != null && wMenu.ID != "" && wMenu.ID == wMenu.ParentId) {
        showError("上级菜单选择错误");
        return;
    }
    if (wMenu.Name == null || wMenu.Name == "") {
        showError("请填写名称");
        return;
    }
    if (wMenu.ApplicationId == null || wMenu.ApplicationId == "") {
        showError("请填写微信账号");
        return;
    }
    //if (wMenu.Key == null || wMenu.Key == "") {
    //    showError("请选择菜单KEY值");
    //    return;
    //}
    if (wMenu.Type == null || wMenu.Type == "") {
        showError("请选择类型");
        return;
    }
    //if (wMenu.Level == null || wMenu.Level == "") {
    //    wMenu.Level = 1;
    //}
    if (wMenu.DisplayColumn == null || wMenu.DisplayColumn == "") {
        showError("请输入序号");
        return;
    }
    //if (wMenu.Level > 3) {
    //    showError("请输入菜单级别最大为3");
    //    return;
    //}

    if (wMenu.MenuURL != null && wMenu.MenuURL != "") {
        if (wMenu.MenuURL.replace(/[^\x00-\xff]/g, "xx").length > 256) {
            showError("菜单链接长度不能超过256个字符.");
            return;
        } 
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=wmenu_save&WMenuId=' + wMenu.ID, 
        params: {
            "item": Ext.encode(wMenu)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch();
                parent.fnLoadTree();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}

function fnCloseWin() {
    CloseWin('WMenuEdit');
}

function fnAddItem(id, op, param) {
    if (id == undefined || id == null) id = "";
    var typeId = get("txtMaterialTypeId").value;
    if (typeId == undefined || typeId == null || typeId == "" || typeId.length > 1) 
        typeId = "1";

    param = "typeId=" + typeId + 
        "&value=" + Ext.encode(get("hMaterialType").value);

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 450,
        id: "WMenuAddItem",
        title: "选择素材",
        url: "WMenuAddItem.aspx?op=1&" + param
    });
    win.show();
}

