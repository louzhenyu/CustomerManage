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


    if (order_id != "null" && order_id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetInoutInfoById",
            params: { order_id: order_id },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                //Ext.getCmp("txtUnit").jitSetValue([{ "id": d.purchase_unit_id, "text": d.purchase_unit_name}]);

                get("txtOrderNo").innerHTML = d.order_no;
                get("txtOrderAmount").innerHTML = d.total_amount;
                get("txtVipName").innerHTML = d.vip_name;
                get("txtPhone").innerHTML = d.vipPhone;
                get("txtSendTime").innerHTML = d.order_no;
                get("txtAddress").innerHTML = d.Field4;

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });

        
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=GetOrderTable",
            params: {
                id: order_id
            },
            method: 'post',
            success: function (response) {
                var d = response.responseText;
                if (d != null) {
                    get("txtList").innerHTML = d;
                
                }
            },
            failure: function (result) {
                alert("错误：" + result.responseText);
            }
        });

    }
    else {
        myMask.hide();
    }


});

function fnClose() {
    CloseWin('PosOrder2Unit');
}

function fnSave() {
    var order = {};
    var flag = false;
    
    var unitId = $('input[name="order"]:checked').val();
    if (unitId == null || unitId.length == 0) {
        alert("请选择门店");
        return;
    }

    order.order_id = getUrlParam("order_id");
    order.purchase_unit_id = unitId;
    order.Field7 = "2";

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/InoutHandler.ashx?method=UpdateOrderUnit&order_id=' + order.order_id,
        params: { "order": Ext.encode(order) },
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
        failure: function () {

        }
    });
    if (flag) fnClose();
}
