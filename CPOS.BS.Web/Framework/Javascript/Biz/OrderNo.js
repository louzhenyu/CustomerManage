function newOrderNo(type) {
    var order_no = "";
    Ext.Ajax.request({
        method: 'GET',
        //        url: '/Controls/Data.aspx?data_type=' + type,
        url: '/Framework/Javascript/Biz/Handler/OrderNo.ashx?method=' + type,
        params: {},
        sync: true,
        async: false,
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.data != null) {
                order_no = d.data;
            } else {
                showInfo("页面超时，请重新登录");
            }
        },
        failure: function (result) {
            showError(result.responseText);
        }
    });
    return order_no;
};