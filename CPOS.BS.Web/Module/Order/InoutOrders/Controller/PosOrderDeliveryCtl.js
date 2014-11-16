var VipCardID = null;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    //InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/InoutHandler.ashx?mid=");
    fnLoad();
    
});

fnLoad = function() {
    var order_id = getStr(getUrlParam("order_id"));
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetInoutInfoById",
        params: { order_id: order_id },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText).topics;

            if (d.order_no != null && d.order_no.length > 0)
                get("txtOrderNo").innerHTML = getStr(d.order_no);
            else
                get("txtOrderNo").innerHTML = "&nbsp;";
            
            if (d.order_date != null && d.order_date.length > 0)
                get("txtOrderDate").innerHTML = getStr(d.order_date);
            else
                get("txtOrderDate").innerHTML = "&nbsp;";

            if (d.Field3 != null && d.Field3.length > 0)
                get("txtField3").innerHTML = getStr(d.Field3);
            else
                get("txtField3").innerHTML = "&nbsp;";
            
            if (d.Field6 != null && d.Field6.length > 0)
                get("txtTel").innerHTML = getStr(d.Field6);
            else
                get("txtTel").innerHTML = "&nbsp;";

            if (d.Field6 != null && d.Field4.length > 0)
                get("txtField6").innerHTML = getStr(d.Field4);
            else
                get("txtField6").innerHTML = "&nbsp;";

            if (d.Field5 != null && d.Field5.length > 0)
                get("txtField5").innerHTML = getStr(d.Field5);
            else
                get("txtField5").innerHTML = "&nbsp;";

            Ext.getCmp("txtCarrierId").jitSetValue(getStr(d.carrier_id));
            Ext.getCmp("txtField2").jitSetValue(getStr(d.Field2));
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

function fnClose() {
    CloseWin('PosOrderDelivery');
}

function fnSave() {
    var flag;
    var item = {};
    item.order_id = getUrlParam("order_id");
    item.carrier_id = Ext.getCmp("txtCarrierId").jitGetValue();
    item.Field2 = Ext.getCmp("txtField2").jitGetValue();
    item.Field7 = '3';

    if (item.carrier_id == null || item.carrier_id.length == 0) {
        alert("请填写配送商");
        return;
    }
    if (item.Field2 == null || item.Field2.length == 0) {
        alert("请填写配送单号");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/order/inoutorders/Handler/InoutHandler.ashx?method=SavePosDelivery&order_id=' + item.order_id, 
        params: {
            order: Ext.encode(item)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.Description);
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

