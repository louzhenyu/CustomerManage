Ext.onReady(function () {
    InitVE();
    InitStore();
    InitView();
    JITPage.HandlerUrl.setValue("ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx?mid=" + __mid);
    fnSearch();

});
function fnSearch() {
    Ext.getStore("TransferStore").proxy.url = "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx?type=Product&action=GetCustomerWithdrawalList&req={\"Parameters\":{\"Status\":\"30\",\"PageSize\":\"2\"}} ";
    Ext.getStore("TransferStore").load();
    Ext.getCmp("pageBar").store.proxy.url = "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx?type=Product&action=GetCustomerWithdrawalList&req={\"Parameters\":{\"Status\":\"30\",\"PageSize\":\"2\"}} ";
    Ext.getCmp("pageBar").store.pageSize =2;

}
function fnPay(SeriaNo) {

    var handlerUrl = "";
    if (SeriaNo == "") {
        handlerUrl = "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx?type=Product&action=Pay&req={\"Locale\":null,\"CustomerID\":null,\"UserID  \":null,\"OpenID\":null,\"Token\":null,\"Parameters\":{\"SeriaNo\":\"" + SeriaNo + "\"}}";

    } else {
        handlerUrl = "/ApplicationInterface/WithdrawalAndBooking/WithdrawalGateway.ashx?type=Product&action=Pay&req={\"Locale\":null,\"CustomerID\":null,\"UserID  \":null,\"OpenID\":null,\"Token\":null,\"Parameters\":{\"SeriaNo\":\"" + SeriaNo + "\"}}";

    }
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();
    Ext.Ajax.request({
        url: handlerUrl,
        params: {

    },
    method: 'POST',
    success: function (response, opts) {
        debugger;
        var jdata = Ext.JSON.decode(response.responseText);
        if (jdata.ResultCode == "0") {
            myMask.hide();
            Ext.Msg.show({
                title: '提示',
                msg: '代付成功',
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.INFO,
                fn: function () {
                    id = null;
                    fnSearch();

                }
            });
        } else {
            myMask.hide();
            Ext.Msg.show({
                title: '异常',
                msg: jdata.Message,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    }
});

}
