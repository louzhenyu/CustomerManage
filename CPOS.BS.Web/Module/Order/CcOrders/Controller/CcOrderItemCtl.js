Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });

    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/CcOrderHandler.ashx?mid=");

    fnLoadData();
});

function fnLoadData() {
    var tbItemCodeCtrl = Ext.getCmp("txtItemCode");
    var tbItemNameCtrl = Ext.getCmp("txtItemName");
    var tbEndQtyCtrl = Ext.getCmp("txtEndQty");     //库存数
    var tbOrderQtyCtrl = Ext.getCmp("txtOrderQty"); //盘点数
    var tbDifQtyCtrl = Ext.getCmp("txtDifQty");     //差异数

    var paramStr = "";
    var param = {};
    param.index = getStr(getUrlParam("index"));
    param.order_detail_id = getStr(getUrlParam("order_detail_id"));
    param.sku_id = getStr(getUrlParam("sku_id"));
    param.item_code = getStr(getUrlParam("item_code"));
    param.item_name = getStr(getUrlParam("item_name"));

    param.end_qty = getStr(getUrlParam("end_qty"));
    param.order_qty = getStr(getUrlParam("order_qty"));
    param.difference_qty = getStr(getUrlParam("difference_qty"));
    param.display_name = getStr(getUrlParam("display_name"));

    sku_selected_data = param;

    tbItemCodeCtrl.setValue(param.sku_id);
    tbEndQtyCtrl.setValue(param.end_qty);
    tbOrderQtyCtrl.setValue(param.order_qty);
    tbDifQtyCtrl.setValue(param.difference_qty);

    var fnCalDifQty = function () {
        var c1 = parseFloat(Ext.getCmp("txtEndQty").getValue());
        var c2 = parseFloat(Ext.getCmp("txtOrderQty").getValue());
        Ext.getCmp("txtDifQty").setValue(c1 - c2);
    };
    tbEndQtyCtrl.onChange = fnCalDifQty;
    tbOrderQtyCtrl.onChange = fnCalDifQty;

    if (param.order_detail_id != null && param.order_detail_id.length > 0) {
        tbItemCodeCtrl.setReadOnly(true);
    }

    if (param.sku_id != null && param.sku_id.length > 0) {
        Ext.Ajax.request({
            method: 'GET',
            url: JITPage.HandlerUrl.getValue() + '&method=get_sku_by_id&id=' + param.sku_id,
            params: {},
            sync: true,
            async: false,
            success: function (result, request) {
                var d = Ext.decode(result.responseText).data;
                tbItemCodeCtrl.setValue(getStr(d.display_name));
                tbItemNameCtrl.setValue(getStr(d.item_name));
                sku_selected_data.item_name = d.item_name;
                sku_selected_data.display_name = d.display_name;

                setSkuPropsDisplay(d);
            },
            failure: function (result) {
                showError(result.responseText);
            }
        });
    }
}

function fnSave() {
    var tbEndQtyCtrl = Ext.getCmp("txtEndQty");     //库存数
    var tbOrderQtyCtrl = Ext.getCmp("txtOrderQty"); //盘点数
    var tbDifQtyCtrl = Ext.getCmp("txtDifQty");     //差异数
    var txtItemCode = Ext.getCmp("txtItemCode");

    var skuId = txtItemCode.getValue();
    if (skuId == null || skuId == "") {
        showError("请选择商品");
        return;
    }

    var checkEndQty = checkIsFloat(tbEndQtyCtrl.value);
    if (!checkEndQty.status) {
        showError("请输入库存数");
        return;
    }

    var checkOrderQty = checkIsFloat(tbOrderQtyCtrl.value);
    if (!checkOrderQty.status) {
        showError("请输入盘点数");
        return;
    }

    var checkDifQty = checkIsFloat(tbDifQtyCtrl.value);
    if (!checkDifQty.status) {
        showError("差异数量数值错误");
        return;
    }

    var parentGrid = parent.Ext.getCmp("gridView");
    var item = {};
    item.index = sku_selected_data.index;
    item.order_detail_id = sku_selected_data.order_detail_id;
    item.sku_id = sku_selected_data.sku_id;
    item.item_code = sku_selected_data.item_code;
    item.item_name = sku_selected_data.item_name;

    item.end_qty = checkEndQty.value;
    item.order_qty = checkOrderQty.value;
    item.difference_qty = checkDifQty.value;

    item.prop_1_detail_name = sku_selected_data.prop_1_detail_name;
    item.prop_2_detail_name = sku_selected_data.prop_2_detail_name;
    item.prop_3_detail_name = sku_selected_data.prop_3_detail_name;
    item.prop_4_detail_name = sku_selected_data.prop_4_detail_name;
    item.prop_5_detail_name = sku_selected_data.prop_5_detail_name;
    item.display_name = sku_selected_data.display_name;

    var index = parentGrid.store.find("sku_id", sku_selected_data.sku_id);
    if (index > -1 &&
        sku_selected_data.sku_id != null &&
        sku_selected_data.sku_id != "") {
        showInfo("商品已添加");
        return;
    } else {
        item.order_detail_id = newGuid();
        parentGrid.store.add(item);
        parentGrid.store.commitChanges();
    }
    parent.fnCalTotal();
    fnCloseWin();
}
function fnClose() {
    fnCloseWin();
}
function fnCloseWin() {
    CloseWin('CcOrderItem');
}