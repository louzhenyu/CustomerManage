Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/CustomerPayAssignHandler.ashx?mid=");

    var AssignId = getUrlParam("AssignId");
    if (AssignId != "null" && AssignId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_CustomerPayAssign_by_id",
            params: { AssignId: AssignId },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtPaymentTypeId").jitSetValue(getStr(d.PaymentTypeId));
                Ext.getCmp("txtCustomerAccountNumber").jitSetValue(getStr(d.CustomerAccountNumber));
                Ext.getCmp("txtCustomerProportion").jitSetValue(getStr(d.CustomerProportion));
                Ext.getCmp("txtJITProportion").jitSetValue(getStr(d.JITProportion));
                Ext.getCmp("txtRemark").jitSetValue(getStr(d.Remark));

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

});

function fnClose() {
    CloseWin('CustomerPayAssignEdit');
}

function fnSave() {
    var flag;
    var item = {};
    item.AssignId = getUrlParam("AssignId");
    item.PaymentTypeId = Ext.getCmp("txtPaymentTypeId").jitGetValue();
    item.CustomerAccountNumber = Ext.getCmp("txtCustomerAccountNumber").jitGetValue();
    item.CustomerProportion = Ext.getCmp("txtCustomerProportion").jitGetValue();
    item.JITProportion = Ext.getCmp("txtJITProportion").jitGetValue();
    item.Remark = Ext.getCmp("txtRemark").jitGetValue();

    if (item.PaymentTypeId == null || item.PaymentTypeId == "") {
        showError("请填写支付类型");
        return;
    }
    if (item.CustomerAccountNumber == null || item.CustomerAccountNumber == "") {
        showError("请填写客户帐号");
        return;
    }
    if (item.CustomerProportion == null || item.CustomerProportion == "") {
        showError("请填写客户分成比例");
        return;
    }
    if (item.JITProportion == null || item.JITProportion == "") {
        showError("请填写杰亦特截留比例");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/Vip/CustomerPayAssign/Handler/CustomerPayAssignHandler.ashx?method=CustomerPayAssign_save&AssignId=' + item.AssignId, 
        params: {
            "item": Ext.encode(item)
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
    if (flag) fnCloseWin();
}

function fnCloseWin() {
    CloseWin('CustomerPayAssignEdit');
}



