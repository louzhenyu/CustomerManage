Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WarehouseHandler.ashx?mid=");
    
    var warehouse_id = getUrlParam("warehouse_id");
    if (warehouse_id != "null" && warehouse_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_warehouse_by_id",
            params: { warehouse_id: warehouse_id },
            method: 'POST',
            success: function (response) {
                var storeId = "warehouseEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtParentUnit").jitSetValue([{ "id":d.unit_id, "text":d.unit_name }]);

                Ext.getCmp("txtWarehouseName").jitSetValue(getStr(d.wh_name));
                Ext.getCmp("txtWarehouseCode").jitSetValue(getStr(d.wh_code));
                Ext.getCmp("txtWarehouseEnglish").jitSetValue(getStr(d.wh_name_en));
                Ext.getCmp("txtWarehouseContacter").jitSetValue(getStr(d.wh_contacter));
                Ext.getCmp("txtWarehouseTel").jitSetValue(getStr(d.wh_tel));
                Ext.getCmp("txtWarehouseFax").jitSetValue(getStr(d.wh_fax));
                Ext.getCmp("txtAddress").jitSetValue(getStr(d.wh_address));

                Ext.getCmp("txtIsDefaultWarehouse").setDefaultValue(getStr(d.is_default));
                Ext.getCmp("txtWarehouseStatus").setDefaultValue(getStr(d.wh_status));

                Ext.getCmp("txtRemark").jitSetValue(getStr(d.wh_remark));
                
                Ext.getCmp("txtCreateUserName").setValue(getStr(d.create_user_name));
                Ext.getCmp("txtCreateTime").setValue(getStr(d.create_time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.modify_user_name));
                Ext.getCmp("txtModifyTime").setValue(getStr(d.modify_time));

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

fnLoadRole = function() {
    var store = Ext.getStore("warehouseEditRoleStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_warehouse_role_info_by_warehouse_id&warehouse_id=" + 
            getUrlParam("warehouse_id"),
        params: { start: 0, limit: 0 }
    });
}

function fnClose() {
    CloseWin('WarehouseEdit');
}

function fnSave() {
    var flag;
    var warehouse = {};
        
    var hdPurchaseUnitCtrl = Ext.getCmp("txtParentUnit");
    var tbWarehouseCodeCtrl = Ext.getCmp("txtWarehouseCode");
    var tbWarehouseNameCtrl = Ext.getCmp("txtWarehouseName");
    var tbWarehouseEnglishCtrl = Ext.getCmp("txtWarehouseEnglish");
    var tbWarehouseAddressCtrl = Ext.getCmp("txtAddress");
    var tbWarehouseContacterCtrl = Ext.getCmp("txtWarehouseContacter");
    var tbWarehouseTelCtrl = Ext.getCmp("txtWarehouseTel");
    var tbWarehouseFaxCtrl = Ext.getCmp("txtWarehouseFax");
    var hdIsDefaultWarehouseCtrl = Ext.getCmp("txtIsDefaultWarehouse");
    var hdWarehouseStatusCtrl = Ext.getCmp("txtWarehouseStatus");
    var tbRemarkCtrl = Ext.getCmp("txtRemark");

    warehouse.warehouse_id = getUrlParam("warehouse_id");
    warehouse.unit_id = hdPurchaseUnitCtrl.jitGetValue();
    warehouse.wh_code = tbWarehouseCodeCtrl.jitGetValue();
    warehouse.wh_name = tbWarehouseNameCtrl.jitGetValue();
    warehouse.wh_name_en = tbWarehouseEnglishCtrl.jitGetValue();
    warehouse.wh_address = tbWarehouseAddressCtrl.jitGetValue();
    warehouse.wh_contacter = tbWarehouseContacterCtrl.jitGetValue();
    warehouse.wh_tel = tbWarehouseTelCtrl.jitGetValue();
    warehouse.wh_fax = tbWarehouseFaxCtrl.jitGetValue();
    warehouse.is_default = hdIsDefaultWarehouseCtrl.jitGetValue();
    warehouse.wh_status = hdWarehouseStatusCtrl.jitGetValue();
    warehouse.wh_remark = tbRemarkCtrl.jitGetValue();

    if (warehouse.unit_id == null || warehouse.unit_id == "") {
        showError("请选择所属单位");
        return;
    }
    if (warehouse.wh_code == null || warehouse.wh_code == "") {
        showError("请填写仓库编码");
        return;
    }
    if (warehouse.wh_name == null || warehouse.wh_name == "") {
        showError("请填写仓库名称");
        return;
    }
    if (warehouse.wh_tel == null || warehouse.wh_tel == "") {
        showError("请填写电话");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Basic/Warehouse/Handler/WarehouseHandler.ashx?method=warehouse_save&warehouse_id=' + warehouse.warehouse_id, 
        params: {
            "warehouse": Ext.encode(warehouse)
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
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnCloseWin();
}

function fnCloseWin() {
    CloseWin('WarehouseEdit');
}

