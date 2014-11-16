Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/InoutHandler.ashx?mid=");

    fnLoadData();
});

function fnLoadData() {
    var tbItemCodeCtrl = Ext.getCmp("txtItemCode");
    var tbItemNameCtrl = Ext.getCmp("txtItemName");
    var tbPriceCtrl = Ext.getCmp("txtPrice");            //单价
    var tbRequestNumCtrl = Ext.getCmp("txtRequestNum");  //预定数量
    var tbInNumCtrl = Ext.getCmp("txtInNum");            //入库数量


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

    sku_selected_data = param;

    tbItemCodeCtrl.setValue(param.sku_id);
    tbPriceCtrl.setValue(param.enter_price);
    tbRequestNumCtrl.setValue(param.enter_qty);
    tbInNumCtrl.setValue(param.order_qty);


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

function fnLoadItems() {
    var storeId = "purchaseInOrderEditItemStore";
    Ext.getStore(storeId).proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=GetInoutDetailInfoById";
    Ext.getStore(storeId).pageSize = JITPage.PageSize.getValue();
    Ext.getStore(storeId).proxy.extraParams = {
        order_id: new String(JITMethod.getUrlParam("order_id"))
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    //alert(Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()));
    Ext.getStore(storeId).load();
}

function fnSave() {
//    var tbEnterQtyCtrl = Ext.getCmp("txtEnterQty"); //数量
//    var tbRetailPriceCtrl = Ext.getCmp("txtRetailPrice"); //零售价
//    var tbEnterPriceCtrl = Ext.getCmp("txtEnterPrice"); //采购价
    var tbPriceCtrl = Ext.getCmp("txtPrice");            //单价
    var tbRequestNumCtrl = Ext.getCmp("txtRequestNum");  //预定数量
    var tbInNumCtrl = Ext.getCmp("txtInNum");            //入库数量

    var txtItemCode = Ext.getCmp("txtItemCode");
    var skuId = txtItemCode.getValue();
    if (skuId == null || skuId == "") {
        showError("请选择商品");
        return;
    }
    var checkPrice = checkIsFloat(tbPriceCtrl.value);
    if (!checkPrice.status) {
        showError("请输入单价");
        return;
    }
    var checkRequestQty = checkIsFloat(tbRequestNumCtrl.value);
    if (!checkRequestQty.status) {
        showError("请输入预定数量");
        return;
    }
    var checkInQty = checkIsFloat(tbInNumCtrl.value);
    if (!checkInQty.status) {
        showError("请输入入库数量");
        return;
    }

    var parentGrid = parent.Ext.getCmp("gridView");
    var item = {};
    item.index = sku_selected_data.index;
    item.order_detail_id = sku_selected_data.order_detail_id;
    item.sku_id = sku_selected_data.sku_id;
    item.item_code = sku_selected_data.item_code;
    item.item_name = sku_selected_data.item_name;

    item.enter_price = checkPrice.value;
    item.retail_price = checkPrice.value;
    item.enter_qty = checkRequestQty.value;
    item.order_qty = checkInQty.value;

    item.enter_amount = item.enter_qty * item.enter_price;
    item.retail_amount = item.order_qty * item.retail_price;
    item.prop_1_detail_name = sku_selected_data.prop_1_detail_name;
    item.prop_2_detail_name = sku_selected_data.prop_2_detail_name;
    item.prop_3_detail_name = sku_selected_data.prop_3_detail_name;
    item.prop_4_detail_name = sku_selected_data.prop_4_detail_name;
    item.prop_5_detail_name = sku_selected_data.prop_5_detail_name;
    item.display_name = sku_selected_data.display_name;

    var index = parentGrid.store.find("order_detail_id", sku_selected_data.order_detail_id);
    if (index > -1 &&
    sku_selected_data.order_detail_id != null &&
    sku_selected_data.order_detail_id != "") {
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
    CloseWin('PurchaseInOrderItem');
}

