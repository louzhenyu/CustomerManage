Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });

    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/OrderHandler.ashx?mid=");

    fnLoadData();
});

function fnLoadData() {
    var tbItemCodeCtrl = Ext.getCmp("txtItemCode");
    var tbItemNameCtrl = Ext.getCmp("txtItemName");
    var tbEnterQtyCtrl = Ext.getCmp("txtEnterQty"); //预定数量
    var tbOrderQtyCtrl = Ext.getCmp("txtOrderQty"); //订单数量
    var tbStdPriceCtrl = Ext.getCmp("txtStdPrice"); //建议零售价
    var tbOrderDiscountRateCtrl = Ext.getCmp("txtOrderDiscountRate"); //合同折扣
    var tbEnterPriceCtrl = Ext.getCmp("txtEnterPrice"); //合同折扣价
    var tbDiscountRateCtrl = Ext.getCmp("txtDiscountRate"); //折上折
    var tbRetailPriceCtrl = Ext.getCmp("txtRetailPrice"); //最终零售价

    tbEnterQtyCtrl.onChange = function () {
        tbEnterQtyCtrl.setValue(parseFloat(tbEnterQtyCtrl.value));
        tbOrderQtyCtrl.setValue(tbEnterQtyCtrl.value);
    };
    tbOrderQtyCtrl.onChange = function () {
        tbOrderQtyCtrl.setValue(parseFloat(tbOrderQtyCtrl.value));
    };
    tbStdPriceCtrl.onChange = function () {
        tbStdPriceCtrl.setValue(parseFloat(tbStdPriceCtrl.value));
        tbEnterPriceCtrl.setValue(parseFloat(tbStdPriceCtrl.value) * parseFloat(tbOrderDiscountRateCtrl.value) / 100);
        tbRetailPriceCtrl.setValue(parseFloat(tbEnterPriceCtrl.value) * parseFloat(tbDiscountRateCtrl.value) / 100);
    };
    tbOrderDiscountRateCtrl.onChange = function () {
        tbOrderDiscountRateCtrl.setValue(parseFloat(tbOrderDiscountRateCtrl.value));
        tbEnterPriceCtrl.setValue(parseFloat(tbStdPriceCtrl.value) * parseFloat(tbOrderDiscountRateCtrl.value) / 100);
        tbRetailPriceCtrl.setValue(parseFloat(tbEnterPriceCtrl.value) * parseFloat(tbDiscountRateCtrl.value) / 100);
    };
    tbEnterPriceCtrl.onChange = function () {
        tbEnterPriceCtrl.setValue(parseFloat(tbEnterPriceCtrl.value));
    };
    tbDiscountRateCtrl.onChange = function () {
        tbDiscountRateCtrl.setValue(parseFloat(tbDiscountRateCtrl.value));
        tbRetailPriceCtrl.setValue(parseFloat(tbEnterPriceCtrl.value) * parseFloat(tbDiscountRateCtrl.value) / 100);
    };
    tbRetailPriceCtrl.onChange = function () {
        tbRetailPriceCtrl.setValue(parseFloat(tbRetailPriceCtrl.value));
    };

    var paramStr = "";
    var param = {};
    param.index = getStr(getUrlParam("index"));
    param.order_detail_id = getStr(getUrlParam("order_detail_id"));
    param.sku_id = getStr(getUrlParam("sku_id"));
    param.item_code = getStr(getUrlParam("item_code"));
    param.item_name = getStr(getUrlParam("item_name"));

    param.enter_qty = getStr(getUrlParam("enter_qty"));
    param.order_qty = getStr(getUrlParam("order_qty"));
    param.enter_price = getStr(getUrlParam("enter_price"));
    param.enter_amount = getStr(getUrlParam("enter_amount"));
    param.display_name = getStr(getUrlParam("display_name"));
    param.enter_price = getStr(getUrlParam("enter_price"));
    param.retail_price = getStr(getUrlParam("retail_price"));
    param.std_price = getStr(getUrlParam("std_price"));
    param.order_discount_rate = getStr(getUrlParam("order_discount_rate"));
    param.discount_rate = getStr(getUrlParam("discount_rate"));

    sku_selected_data = param;

    tbItemCodeCtrl.setValue(param.sku_id);
    tbEnterQtyCtrl.setValue(param.enter_qty);
    tbOrderQtyCtrl.setValue(param.order_qty);
    tbStdPriceCtrl.setValue(param.std_price);
    tbOrderDiscountRateCtrl.setValue(param.order_discount_rate);
    tbEnterPriceCtrl.setValue(param.enter_price);
    tbDiscountRateCtrl.setValue(param.discount_rate);
    tbRetailPriceCtrl.setValue(param.retail_price);

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
    var tbEnterQtyCtrl = Ext.getCmp("txtEnterQty"); //预定数量
    var tbOrderQtyCtrl = Ext.getCmp("txtOrderQty"); //订单数量
    var tbStdPriceCtrl = Ext.getCmp("txtStdPrice"); //建议零售价
    var tbOrderDiscountRateCtrl = Ext.getCmp("txtOrderDiscountRate"); //合同折扣
    var tbEnterPriceCtrl = Ext.getCmp("txtEnterPrice"); //合同折扣价
    var tbDiscountRateCtrl = Ext.getCmp("txtDiscountRate"); //折上折
    var tbRetailPriceCtrl = Ext.getCmp("txtRetailPrice"); //最终零售价
    var txtItemCode = Ext.getCmp("txtItemCode");

    var skuId = txtItemCode.getValue();
    if (skuId == null || skuId == "") {
        showError("请选择商品");
        return;
    }

    var checkEnterQty = checkIsFloat(tbEnterQtyCtrl.value);
    if (!checkEnterQty.status) {
        showError("请输入预定数量");
        return;
    }

    var checkOrderQty = checkIsFloat(tbOrderQtyCtrl.value);
    if (!checkOrderQty.status) {
        showError("请输入订单数量");
        return;
    }

    var checkStdPrice = checkIsFloat(tbStdPriceCtrl.value);
    if (!checkStdPrice.status) {
        showError("请输入建议零售价");
        return;
    }

    var checkOrderDiscountRate = checkIsFloat(tbOrderDiscountRateCtrl.value);
    if (!checkOrderDiscountRate.status) {
        showError("请输入合同折扣");
        return;
    }

    var checkEnterPrice = checkIsFloat(tbEnterPriceCtrl.value);
    if (!checkEnterPrice.status) {
        showError("请输入合同折扣价");
        return;
    }

    var checkDiscountRate = checkIsFloat(tbDiscountRateCtrl.value);
    if (!checkDiscountRate.status) {
        showError("请输入折上折");
        return;
    }

    var checkRetailPrice = checkIsFloat(tbRetailPriceCtrl.value);
    if (!checkRetailPrice.status) {
        showError("请输入最终零售价");
        return;
    }

    var parentGrid = parent.Ext.getCmp("gridView");
    var item = {};
    item.index = sku_selected_data.index;
    item.order_detail_id = sku_selected_data.order_detail_id;
    item.sku_id = sku_selected_data.sku_id;
    item.item_code = sku_selected_data.item_code;
    item.item_name = sku_selected_data.item_name;

    item.enter_qty = checkEnterQty.value;
    item.order_qty = checkOrderQty.value;
    item.std_price = checkStdPrice.value;
    item.order_discount_rate = checkOrderDiscountRate.value;
    item.enter_price = checkEnterPrice.value;
    item.discount_rate = checkDiscountRate.value;
    item.retail_price = checkRetailPrice.value;

    item.enter_amount = item.enter_qty * item.enter_price;
    item.retail_amount = item.order_qty * item.retail_price;

    item.prop_1_detail_name = sku_selected_data.prop_1_detail_name;
    item.prop_2_detail_name = sku_selected_data.prop_2_detail_name;
    item.prop_3_detail_name = sku_selected_data.prop_3_detail_name;
    item.prop_4_detail_name = sku_selected_data.prop_4_detail_name;
    item.prop_5_detail_name = sku_selected_data.prop_5_detail_name;
    item.display_name = sku_selected_data.display_name;

    var index = parentGrid.store.find("order_detail_id", sku_selected_data.order_detail_id);
    if (index > -1 && sku_selected_data.order_detail_id != null && sku_selected_data.order_detail_id != "") {
        parentGrid.store.removeAt(index);
        parentGrid.store.insert(index, item);
        parentGrid.store.commitChanges();
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
    CloseWin('SalesOrderItem');
}