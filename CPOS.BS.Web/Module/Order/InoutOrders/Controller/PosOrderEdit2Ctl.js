Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/InoutHandler.ashx?mid=");

    var order_id = new String(JITMethod.getUrlParam("order_id"));
    var op = new String(JITMethod.getUrlParam("op"));


    if (order_id != "null" && order_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetInoutInfoById",
            params: { order_id: order_id },
            method: 'post',
            success: function (response) {
                var storeId = "salesOutOrderEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtOrderNo").setValue(getStr(d.order_no));
                Ext.getCmp("txtOrderDate").setValue(getStr(d.order_date));


                Ext.getCmp("txtSalesUnitId").jitSetValue([{ "id": d.sales_unit_id, "text": d.sales_unit_name}]);
                Ext.getCmp("txtPurchaseUnitName").setValue(getStr(d.purchase_unit_name));

                Ext.getCmp("tbDiscountRate").setValue(getStr(d.discount_rate)); //折扣
                Ext.getCmp("tbKeepTheChange").setValue(getStr(d.keep_the_change)); //找零
                Ext.getCmp("tbWipingZero").setValue(getStr(d.wiping_zero)); //抹零
                Ext.getCmp("txtCreateUserName").setValue(getStr(d.create_user_name)); //收银员
                Ext.getCmp("tbSYCreateTime").setValue(getStr(d.create_time)); //收银时间

                Ext.getCmp("txtCreateUserName").setValue(getStr(d.create_user_name));
                Ext.getCmp("txtCreateTime").setValue(getStr(d.create_time));
                Ext.getCmp("txtModifyUserName").setValue(getStr(d.modify_user_name));
                Ext.getCmp("txtModifyTime").setValue(getStr(d.modify_time));

                Ext.getCmp("txtField10").setValue(getStr(d.Field10));
                Ext.getCmp("txtVipName").setValue(getStr(d.vip_name));
                Ext.getCmp("txtVipPhone").setValue(getStr(d.vipPhone));
                Ext.getCmp("txtDefrayTypeName").setValue(getStr(d.DefrayTypeName));
                Ext.getCmp("txtDeliveryName").setValue(getStr(d.DeliveryName));
                Ext.getCmp("txtDataFromName").setValue(getStr(d.data_from_name));
                Ext.getCmp("txtActualAmount").setValue(getStr(d.actual_amount));
                Ext.getCmp("txtTotalRetail").setValue(getStr(d.total_retail));
                Ext.getCmp("txtSendTime").setValue(getStr(d.send_time));
                Ext.getCmp("txtField14").setValue(getStr(d.Field14));
                Ext.getCmp("txtField6").setValue(getStr(d.Field6));
                Ext.getCmp("txtField5").setValue(getStr(d.Field5));
                Ext.getCmp("txtField4").setValue(getStr(d.Field4));
                Ext.getCmp("txtCarrierName").setValue(getStr(d.carrier_name));
                Ext.getCmp("txtField2").setValue(getStr(d.Field2));

                if (d.Field7 == "0") {
                    Ext.getCmp("txtCancelTime").setValue(getStr(d.modify_time));
                }

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

    fnLoadItems();


});

//function fnGetOrderNo(op) {
//    if (op == "new") {
//        Ext.getCmp("txtOrderNo").setValue(newOrderNo("new_order_no_do"));
//    }
//}

function fnLoadItems() {
    var storeId = "salesOutOrderEditItemStore";
    //debugger;
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=GetInoutDetailInfoById";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        order_id: new String(JITMethod.getUrlParam("order_id"))
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    Ext.getStore(storeId).load(
    {
        callback: function () {
            fnCalTotal();
        }
    }
    );


}

function fnAddItem(id, op, param) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "SalesOutOrderItem",
        title: "商品",
        url: "SalesOutOrderItem.aspx?op=" + op + "&id=" + id + getStr(param)
    });
    win.show();
}
function fnDeleteItem() {
    var grid = Ext.getCmp("gridView");
    var ids = JITPage.getSelected({ gridView: grid, id: "order_detail_id" });

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
    CloseWin('PosOrderEdit');
}

function fnCalTotal() {
    var pnl = Ext.getCmp("editPanel");
    var grid = Ext.getCmp("gridView");

    var tbTotalAmountCtrl = pnl.query('jittextfield[name="total_amount"]')[0];
    var tbTotalNumCtrl = pnl.query('jittextfield[name="total_num"]')[0];

    var totalAmount = 0, totalNum = 0;
    if (grid.store.data.map != null) {
        //                alert(grid.store.data.map)
        for (var tmpItem in grid.store.data.map) {
            //            alert("111");
            var objData = grid.store.data.map[tmpItem].data;
            var objItem = {};
            objItem.enter_price = getFloat(objData.enter_price);
            objItem.enter_qty = getFloat(objData.enter_qty);
            objItem.order_qty = getFloat(objData.order_qty);
            objItem.enter_amount = objItem.enter_price; //objItem.order_qty * objItem.enter_price;

            totalAmount += objItem.enter_amount;
            totalNum += objItem.order_qty;
            //            alert(totalAmount)
        }
    }
    //    alert(totalAmount)
    tbTotalAmountCtrl.setValue(totalAmount);
    tbTotalNumCtrl.setValue(totalNum);
}

//function fnSave() {
//    //    debugger;
//    var _grid = Ext.getStore("salesOutOrderEditItemStore");
//    var order = {};
//    var flag = false;

//    var hdSupplierUnitIdCtrl = Ext.getCmp("txtSalesUnitId");
//    var hdPurchaseUnitIdCtrl = Ext.getCmp("txtPurchaseUnitId");
//    var hdWarehouseCtrl = Ext.getCmp("txtSalesWarehuouse");
//    var tbOrderCodeCtrl = Ext.getCmp("txtOrderNo");
//    var tbRemarkCtrl = Ext.getCmp("txtRemark");

//    var tbTotalAmountCtrl = Ext.getCmp("txtTotalAmount");
//    var tbTotalNumCtrl = Ext.getCmp("txtTotalNum");

//    var order_id = getUrlParam("order_id");
//    var order_no = tbOrderCodeCtrl.getValue();
//    var order_date = Ext.getCmp("txtOrderDate").jitGetValueText();
//    var complete_date = Ext.getCmp("txtCompleteDate").jitGetValueText();
//    var sales_unit_id = hdSupplierUnitIdCtrl.jitGetValue();
//    var purchase_unit_id = hdPurchaseUnitIdCtrl.getValue();
//    //    alert(purchase_unit_id)
//    var warehouse_id = hdWarehouseCtrl.value;

//    var remark = tbRemarkCtrl.getValue();

//    order.order_id = order_id;
//    order.order_no = order_no;
//    order.order_date = order_date;
//    order.complete_date = complete_date;
//    order.sales_unit_id = sales_unit_id;
//    order.purchase_unit_id = purchase_unit_id;
//    order.warehouse_id = warehouse_id;

//    order.remark = remark;

//    order.total_amount = parseFloat(tbTotalAmountCtrl.getValue());
//    order.total_qty = parseFloat(tbTotalNumCtrl.getValue());

//    order.InoutDetailList = [];
//    if (_grid.data.map != null) {
//        for (var tmpItem in _grid.data.map) {
//            var objData = _grid.data.map[tmpItem].data;
//            var objItem = {};
//            objItem.sku_id = objData.sku_id;
//            objItem.enter_price = getFloat(objData.enter_price);
//            objItem.enter_qty = getFloat(objData.enter_qty);
//            objItem.order_qty = getFloat(objData.order_qty);
//            objItem.retail_price = getFloat(objData.retail_price);
//            objItem.enter_amount = objItem.enter_qty * objItem.enter_price;
//            objItem.retail_amount = objItem.order_qty * objItem.retail_price;
//            objItem.prop_1_detail_name = objData.prop_1_detail_name;
//            objItem.prop_2_detail_name = objData.prop_2_detail_name;
//            objItem.prop_3_detail_name = objData.prop_3_detail_name;
//            objItem.prop_4_detail_name = objData.prop_4_detail_name;
//            objItem.prop_5_detail_name = objData.prop_5_detail_name;
//            order.InoutDetailList.push(objItem);
//        }
//    }

//    if (order.order_no == null || order.order_no == "") {
//        showError("必须获取订单号");
//        return;
//    }

//    if (order.order_date == null || order.order_date == "") {
//        showError("必须选择订单日期");
//        return;
//    }

//    if (order.complete_date == null || order.complete_date == "") {
//        showError("必须选择出库日期");
//        return;
//    }

//    if (order.sales_unit_id == null || order.sales_unit_id == "") {
//        showError("必须选择销售单位");
//        return;
//    }

//    if (order.purchase_unit_id == null || order.purchase_unit_id == "") {
//        showError("必须选择客户");
//        return;
//    }

//    if (order.warehouse_id == null || order.warehouse_id == "") {
//        showError("必须选择销售仓库");
//        return;
//    }

//    if (order.InoutDetailList.length == null || order.InoutDetailList.length == "0") {
//        showError("必须选择商品");
//        return;
//    }

//    Ext.Ajax.request({
//        method: 'POST',
//        sync: true,
//        async: false,
//        url: 'Handler/InoutHandler.ashx?method=SalesOutOrder_Save&order_id=' + order_id,
//        params: { "order": Ext.encode(order) },
//        success: function (result, request) {
//            var d = Ext.decode(result.responseText);
//            if (d.success == false) {
//                showError("保存数据失败：" + d.msg);
//                flag = false;
//            } else {
//                showSuccess("保存数据成功");
//                flag = true;
//                parent.fnSearch();

//            }
//        },
//        failure: function () {

//        }
//    });
//    if (flag) fnClose();
//}
