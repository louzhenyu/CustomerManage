/*Jermyn 2013-04-03 获取商品的库存数量*/

function fnGetStockNum(id, unit_id, warehouse_id) {
    var num = 0;
    if (unit_id === undefined || unit_id == "undefined") {
        unit_id = getUrlParam("unit_id");
    }
    if (warehouse_id === undefined || warehouse_id == "undefined") {
        warehouse_id = getUrlParam("warehouse_id");
    }
    Ext.Ajax.request({
        method: 'GET',
        url: '/Framework/Javascript/Biz/Handler/ItemStockBalanceNo.ashx?method=stock_num&id='+id+'&unit_id= '+ unit_id + '&warehouse_id=' + warehouse_id + '',
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                num = parseFloat(d.data);
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showError(result.responseText);
        }
    });
    return num;
};