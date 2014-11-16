Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/PropHandler.ashx?mid=");

    if (getUrlParam("ParentPropId")) {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_prop_by_id",
            params: { PropId: getUrlParam("ParentPropId") },
            method: 'POST',
            sync: true,
            async: false,
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                Ext.getCmp("txtParentId").jitSetValue([{ "id": getStr(getUrlParam("ParentPropId")), "text": d.Prop_Name}]);
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });


    }
     var PropId = getUrlParam("PropId");

    if (getUrlParam("ParentPropId")&&(PropId==null||PropId=="")) {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=createIndex",
            params: { partentPropID: getUrlParam("ParentPropId"), propID: "" },
            method: 'POST',
            sync: true,
            async: false,
            success: function (response) {
                var d = Ext.decode(response.responseText);
                Ext.getCmp('txtDisplayIndex').jitSetValue(d.displayIndex)
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    if (getUrlParam("domain")) {
        Ext.getCmp("txtDomain").jitSetValue(getStr(getUrlParam("domain")));
    }

   
    if (PropId != "null" && PropId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_prop_by_id",
            params: { PropId: PropId },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                if (d.Parent_Prop_id != null) {
                    Ext.getCmp("txtParentId").jitSetValue([{ "id": d.Parent_Prop_id, "text": d.Parent_Prop_Name}]);
                }
                Ext.getCmp("txtName").jitSetValue(getStr(d.Prop_Name));
                Ext.getCmp("txtCode").jitSetValue(getStr(d.Prop_Code));
                Ext.getCmp("txtType").jitSetValue(getStr(d.Prop_Type));
                Ext.getCmp("txtDomain").jitSetValue(getStr(d.Prop_Domain));
                Ext.getCmp("txtDefaultValue").jitSetValue(getStr(d.Prop_Default_Value));
                //Ext.getCmp("txtMaxLength").jitSetValue(d.Prop_Max_Length);
                Ext.getCmp("txtDisplayIndex").jitSetValue(getStr(d.Display_Index));

                var propInputFlag = "";
                switch (d.Prop_Input_Flag) {
                    case "label": propInputFlag = "2"; break;
                    case "text": propInputFlag = "3"; break;
                    case "select": propInputFlag = "4"; break;
                    case "select-date-(yyyy-MM-dd)": propInputFlag = "5"; break;
                    case "select-date-(yyyy-MM)": propInputFlag = "6"; break;
                    case "textarea": propInputFlag = "7"; break;
                    case "htmltextarea": propInputFlag = "8"; break;
                    case "fileupload": propInputFlag = "9"; break;
                    case "keyvalue": propInputFlag = "11"; break;
                }
                Ext.getCmp("txtInputFlag").setDefaultValue(getStr(propInputFlag));

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
    CloseWin('PropEdit');
}

function fnSave() {
    var flag;
    var prop = {};
    prop.Prop_Id = getUrlParam("PropId");
    prop.Parent_Prop_id = Ext.getCmp("txtParentId").jitGetValue();
    if (prop.Parent_Prop_id == undefined || prop.Parent_Prop_id == null) prop.Parent_Prop_id = "-99";
    prop.Prop_Name = Ext.getCmp("txtName").jitGetValue();
    prop.Prop_Code = Ext.getCmp("txtCode").jitGetValue();
    prop.Prop_Type = Ext.getCmp("txtType").jitGetValue();
    //prop.Prop_Level = Ext.getCmp("txtLevel").jitGetValue();
    prop.Prop_Domain = Ext.getCmp("txtDomain").jitGetValue();
    //prop.Prop_Max_Length = Ext.getCmp("txtMaxLength").jitGetValue();
    prop.Display_Index = Ext.getCmp("txtDisplayIndex").jitGetValue();
    prop.Prop_Default_Value = Ext.getCmp("txtDefaultValue").jitGetValue();
    prop.Prop_Status = "1";

    switch (Ext.getCmp("txtInputFlag").jitGetValue()) {
        case "2": prop.Prop_Input_Flag = "label"; break;
        case "3": prop.Prop_Input_Flag = "text"; break;
        case "4": prop.Prop_Input_Flag = "select"; break;
        case "5": prop.Prop_Input_Flag = "select-date-(yyyy-MM-dd)"; break;
        case "6": prop.Prop_Input_Flag = "select-date-(yyyy-MM)"; break;
        case "7": prop.Prop_Input_Flag = "textarea"; break;
        case "8": prop.Prop_Input_Flag = "htmltextarea"; break;
        case "9": prop.Prop_Input_Flag = "fileupload"; break;
        case "10": prop.Prop_Input_Flag = "textnumber"; break;
        case "11": prop.Prop_Input_Flag = "keyvalue"; break;
    }

    if (prop.Parent_Prop_id != null && prop.Parent_Prop_id != "" && prop.Prop_Id == prop.Parent_Prop_id) {
        showError("上级菜单选择错误");
        return;
    }
    if (prop.Prop_Name == null || prop.Prop_Name == "") {
        showError("请填写名称");
        return;
    }
    if (prop.Prop_Code == null || prop.Prop_Code == "") {
        showError("请填写代码");
        return;
    }
    if (prop.Prop_Type == null || prop.Prop_Type == "") {
        showError("请选择类型");
        return;
    }
    if (prop.Prop_Domain == null || prop.Prop_Domain == "") {
        showError("请选择属性域");
        return;
    }
    if (prop.Display_Index == null || prop.Display_Index == "") {
        showError("请输入序号");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Prop/Handler/PropHandler.ashx?method=prop_save&PropId=' + prop.Prop_Id,
        params: {
            "item": Ext.encode(prop)
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
    CloseWin('PropEdit');
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
        id: "PropAddItem",
        title: "选择素材",
        url: "PropAddItem.aspx?op=1&" + param
    });
    win.show();
}

function fnCreateIndex(data) {

    var PropId = getUrlParam("PropId");
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=createIndex",
        params: { partentPropID: data.id, propID: PropId },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            Ext.getCmp('txtDisplayIndex').jitSetValue(d.displayIndex)
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });

}
