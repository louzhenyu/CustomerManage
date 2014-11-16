Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WQRCodeTypeHandler.ashx?mid="); 
    
    
    var wQRCodeType_id = new String(JITMethod.getUrlParam("wQRCodeType_id"));
    if (wQRCodeType_id != "null" && wQRCodeType_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_wQRCodeType_by_id",
            params: { wQRCodeType_id: wQRCodeType_id },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtWQRCodeTypeCode").setValue(getStr(d.TypeCode));
                Ext.getCmp("txtWQRCodeTypeName").setValue(getStr(d.TypeName));
                
                //Ext.getCmp("txtApplicationId").setValue(getStr(d.ApplicationId));
                //Ext.getCmp("txtWModel").jitSetValue(d.WModelIds);
                //alert(d.WModelIds);
                Ext.getCmp("txtApplicationId").fnLoad(function(){
                    Ext.getCmp("txtApplicationId").jitSetValue(getStr(d.ApplicationId));
                    Ext.getCmp("txtWModel").setDefaultValue("", null, function() {
                        //alert(d.WModelIds);
                        Ext.getCmp("txtWModel").jitSetValue(d.WModelIds);
                    });
                    Ext.getCmp("txtApplicationId").ApplicationId = getStr(d.ApplicationId);
                });

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
    CloseWin('WQRCodeTypeEdit');
}

function fnSave() {
    var wQRCodeType = {};
    var tbWQRCodeTypeCodeCtrl = Ext.getCmp("txtWQRCodeTypeCode");
    var tbWQRCodeTypeNameCtrl = Ext.getCmp("txtWQRCodeTypeName");

    var wQRCodeType_Id = getUrlParam("wQRCodeType_id");
    wQRCodeType.QRCodeTypeId = getUrlParam("wQRCodeType_id");
    wQRCodeType.TypeCode = tbWQRCodeTypeCodeCtrl.getValue();
    wQRCodeType.TypeName = tbWQRCodeTypeNameCtrl.getValue();
    wQRCodeType.WModelIds = Ext.getCmp("txtWModel").jitGetValue();
    
    if (wQRCodeType.TypeCode == null || wQRCodeType.TypeCode == "") {
        showError("请填写类型号码");
        return;
    }
    if (wQRCodeType.TypeName == null || wQRCodeType.TypeName == "") {
        showError("请填写类型名称");
        return;
    }
        
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/WQRCodeTypeHandler.ashx?method=wQRCodeType_save&wQRCodeType_id=' + wQRCodeType_Id,
        params: { "wQRCodeType": Ext.encode(wQRCodeType) },
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


function fnLoadWQRCodeTypes(app_sys_id, wQRCodeType_id) {
    //alert(app_sys_id);return;
    get("treeWQRCodeType").innerHTML = "";
    app_sys_id = getStr(app_sys_id);
    wQRCodeType_id = getStr(wQRCodeType_id);
    var wQRCodeTypes_data = { };
    Ext.Ajax.request({
        method: 'GET',
        url: '/Module/Basic/WQRCodeType/Handler/WQRCodeTypeHandler.ashx?method=get_sys_wQRCodeTypes_by_wQRCodeType_id&wQRCodeType_id=' + wQRCodeType_id + 
            '&app_sys_id=' + app_sys_id,
        params: { },
        sync: true,
        async : false,
        success: function(result, request) {
            var d =  Ext.decode(result.responseText);
            if (d.data != null) {
                wQRCodeTypes_data = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure : function() {
            showInfo("页面超时，请重新登录");
        }
    });

    var storeWQRCodeTypes = Ext.create('Ext.data.TreeStore', {
        root: {
            expanded: true,
            children: wQRCodeTypes_data
        },
        fields: [
            {name: 'id', type: 'string', mapping: 'WQRCodeType_Id'},
            {name: 'text', type: 'string', mapping: 'WQRCodeType_Name'},
            {name: 'checked', type: 'boolean', mapping: 'check_flag'},
            {name: 'expanded', type: 'boolean', mapping: 'expanded_flag'},
            {name: 'leaf', type: 'boolean', mapping: 'leaf_flag'},
            {name: 'cls', type: 'string', mapping: 'cls_flag'}
        ]
    });

    treeWQRCodeType = Ext.create('Ext.tree.Panel', {
        store: storeWQRCodeTypes,
        rootVisible: false,
        useArrows: true,
        frame: false,
        title: '',
        renderTo: 'treeWQRCodeType',
        width: 372,
        height: 268
    });
}
