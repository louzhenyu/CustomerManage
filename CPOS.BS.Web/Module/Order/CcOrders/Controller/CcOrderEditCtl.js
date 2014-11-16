Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    var myOrderStatus = ""; //订单状态

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/CcOrderHandler.ashx?mid=");
    var op = new String(JITMethod.getUrlParam("op"));

    if (op == "new") {
        fnGetOrderNo();
        Ext.getCmp("btnAddItem" + "_ext").show(true);
        Ext.getCmp("btnGetItems" + "_ext").show(true);
        Ext.getCmp("btnSave").show(true);
    }

    var order_id = new String(JITMethod.getUrlParam("order_id"));
    if (order_id != "null" && order_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_cc_info_by_id",
            params: {
                order_id: order_id
            },
            method: 'post',
            success: function (response) {
                var storeId = "ccOrderEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtOrderNo").setValue(getStr(d.order_no));
                Ext.getCmp("txtOrderDate").setValue(getStr(d.order_date));
                Ext.getCmp("txtCompleteDate").setValue(getStr(d.complete_date));
                Ext.getCmp("txtUnitId").jitSetValue([{
                    "id": d.unit_id,
                    "text": d.unit_name
                }]);
                Ext.getCmp("txtWarehouseId").fnLoad();
                Ext.getCmp("txtWarehouseId").jitSetValue(d.warehouse_id);

                Ext.getCmp("txtRemark").setValue(getStr(d.remark));
                Ext.getCmp("txtCreateUserName").setValue(getStr(d.create_user_name));
                Ext.getCmp("txtCreateTime").setValue(getStr(d.create_time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.modify_user_name));
                Ext.getCmp("txtModifyTime").setValue(getStr(d.modify_time));

                fnCalTotal();
                fnSetEditMode(d.status);

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        myMask.hide();
    }

    fnLoadItems();
});

function fnGetOrderNo() {
    Ext.getCmp("txtOrderNo").setValue(newOrderNo("new_order_no_cc"));
}

function fnLoadItems() {
    var storeId = "ccOrderEditItemStore";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_cc_detail_info_by_id";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        order_id: new String(JITMethod.getUrlParam("order_id"))
    };

    Ext.getStore(storeId).load({
        callback: function () {
            fnCalTotal();
        }
    });
}

function fnAddItem(id, op, param) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "CcOrderItem",
        title: "商品",
        url: "CcOrderItem.aspx?op=" + op + "&id=" + id + getStr(param)
    });

    win.show();
}

function fnDeleteItem() {
    var grid = Ext.getCmp("gridView");
    var ids = JITPage.getSelected({
        gridView: grid,
        id: "order_detail_id"
    });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择商品");
        return;
    };

    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("order_detail_id", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
    fnCalTotal();
}

function fnClose() {
    CloseWin('CcOrderEdit');
}

function fnCalTotal() {
    var pnl = Ext.getCmp("editPanel");
    var grid = Ext.getCmp("gridView");

    var tbTotalStockCtrl = pnl.query('jittextfield[name="total_stock"]')[0];
    var tbTotalNumCtrl = pnl.query('jittextfield[name="total_num"]')[0];

    var totalStock = 0, totalNum = 0;

    if (grid.store.data.map != null) {
        for (var tmpItem in grid.store.data.map) {
            var objData = grid.store.data.map[tmpItem].data;
            var objItem = {};
            objItem.end_qty = getFloat(objData.end_qty);
            objItem.order_qty = getFloat(objData.order_qty);

            totalStock += objItem.end_qty;
            totalNum += objItem.order_qty;
        }
    }

    tbTotalStockCtrl.setValue(totalStock);
    tbTotalNumCtrl.setValue(totalNum);
}

function fnCalCelQty(obj, sku_id) {
    var grid = Ext.getCmp("gridView");

    var index = grid.store.find("sku_id", sku_id);
    var d = grid.store.data.items[index].data;
    var c1 = parseFloat(d.end_qty);
    var c2 = parseFloat(obj.value);
    grid.store.data.items[index].set("order_qty", c2);
    grid.store.data.items[index].set("difference_qty", c1 - c2);

    grid.store.commitChanges();

    fnCalTotal();
}

function fnChangeCellRemark(obj, sku_id) {
    var grid = Ext.getCmp("gridView");

    var index = grid.store.find("sku_id", sku_id);
    var d = grid.store.data.items[index].data;
    grid.store.data.items[index].set("remark", obj.value);
    grid.store.commitChanges();
}

function fnGetItems() {
    if (!confirm("是否加载库存中的商品？(当前单据的商品将清除)")) return;

    var hdPurchaseUnitIdCtrl = Ext.getCmp("txtUnitId"); //盘点单位
    var hdWarehouseCtrl = Ext.getCmp("txtWarehouseId"); //仓库

    var param = {};
    var paramStr = "";

    param.unit_id = hdPurchaseUnitIdCtrl.jitGetValue();
    paramStr += "&unit_id=" + param.unit_id;
    if (param.unit_id.length == 0) {
        showInfo("请选择盘点单位");
        return;
    }

    param.warehouse_id = hdWarehouseCtrl.getValue();
    paramStr += "&warehouse_id=" + param.warehouse_id;
    if (param.warehouse_id.length == 0) {
        showInfo("请选择仓库");
        return;
    }

    var storeId = "ccOrderEditItemStore";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_cc_detail_info_by_id";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        order_id: new String(JITMethod.getUrlParam("order_id")),
        unit_id: hdPurchaseUnitIdCtrl.jitGetValue(),
        warehouse_id: hdWarehouseCtrl.getValue()
    };

    Ext.getStore(storeId).load({
        callback: function () {
            fnCalTotal();
        }
    });
}

function fnSave() {
    var _grid = Ext.getStore("ccOrderEditItemStore");
    var order = {};

    var hdPurchaseUnitIdCtrl = Ext.getCmp("txtUnitId"); //盘点单位
    var hdWarehouseCtrl = Ext.getCmp("txtWarehouseId"); //仓库

    var tbOrderCodeCtrl = Ext.getCmp("txtOrderNo");
    var tbRemarkCtrl = Ext.getCmp("txtRemark");

    var tbTotalStockCtrl = Ext.getCmp("txtTotalStock");
    var tbTotalNumCtrl = Ext.getCmp("txtTotalNum");

    var order_id = getUrlParam("order_id");
    var order_no = tbOrderCodeCtrl.getValue();
    var order_date = Ext.getCmp("txtOrderDate").jitGetValueText();
    var complete_date = Ext.getCmp("txtCompleteDate").jitGetValueText();
    var unit_id = hdPurchaseUnitIdCtrl.jitGetValue();
    var warehouse_id = hdWarehouseCtrl.getValue();
    var remark = tbRemarkCtrl.getValue();

    order.order_id = order_id;
    order.order_no = order_no;
    order.order_date = order_date;
    order.complete_date = complete_date;
    order.unit_id = unit_id;
    order.warehouse_id = warehouse_id;
    order.remark = remark;

    order.total_qty = parseFloat(tbTotalNumCtrl.getValue());

    order.cCDetailInfoList = [];
    if (_grid.data.map != null) {
        for (var tmpItem in _grid.data.map) {
            var objData = _grid.data.map[tmpItem].data;
            var objItem = {};
            objItem.sku_id = objData.sku_id;
            objItem.end_qty = getFloat(objData.end_qty);
            objItem.order_qty = getFloat(objData.order_qty);
            objItem.difference_qty = objItem.end_qty - objItem.order_qty;
            objItem.prop_1_detail_name = objData.prop_1_detail_name;
            objItem.prop_2_detail_name = objData.prop_2_detail_name;
            objItem.prop_3_detail_name = objData.prop_3_detail_name;
            objItem.prop_4_detail_name = objData.prop_4_detail_name;
            objItem.prop_5_detail_name = objData.prop_5_detail_name;
            objItem.remark = objData.remark;
            order.cCDetailInfoList.push(objItem);
        }
    }

    if (order.order_no == null || order.order_no == "") {
        showError("必须获取单据号码");
        return;
    }

    if (order.order_date == null || order.order_date == "") {
        showError("必须选择单据日期");
        return;
    }

    if (order.complete_date == null || order.complete_date == "") {
        showError("必须选择完成日期");
        return;
    }

    if (order.unit_id == null || order.unit_id == "") {
        showError("必须选择盘点单位");
        return;
    }

    if (order.warehouse_id == null || order.warehouse_id == "") {
        showError("必须选择仓库");
        return;
    }

    if (order.cCDetailInfoList.length == null || order.cCDetailInfoList.length == "0") {
        showError("必须选择商品");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/CcOrderHandler.ashx?method=cc_order_save&order_id=' + order_id,
        params: {
            "order": Ext.encode(order)
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

    if (flag) fnClose();
}

function fnSetEditMode(status) {
    if (parseInt(status) >= 10) {
        Ext.getCmp("txtRemark").setReadOnly(true);
        Ext.getCmp("txtOrderDate").setReadOnly(true);
        Ext.getCmp("txtCompleteDate").setReadOnly(true);
        Ext.getCmp("txtUnitId").setReadOnly(true);
        Ext.getCmp("txtWarehouseId").setReadOnly(true);
        Ext.getCmp("btnAddItem" + "_ext").hide(true);
        Ext.getCmp("btnGetItems" + "_ext").hide(true);
        Ext.getCmp("btnSave").hide(true);
    } else {
        Ext.getCmp("txtRemark").setReadOnly(false);
        Ext.getCmp("txtOrderDate").setReadOnly(false);
        Ext.getCmp("txtCompleteDate").setReadOnly(false);
        Ext.getCmp("txtUnitId").setReadOnly(false);
        Ext.getCmp("txtWarehouseId").setReadOnly(false);
        Ext.getCmp("btnAddItem" + "_ext").show(true);
        Ext.getCmp("btnGetItems" + "_ext").show(true);
        Ext.getCmp("btnSave").show(true);
    }
    myOrderStatus = status;
}