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
    JITPage.HandlerUrl.setValue("Handler/VipCardSearchHandler.ashx?mid=");
    fnLoad();
    
});

fnLoad = function() {
    VipCardId = getStr(getUrlParam("VipCardID"));
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetVipCardInfo",
        params: { VipCardID: VipCardId, Lock:"1", UnitID:getUrlParam("UnitID") },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            //get("txtVipCardCode").innerHTML = getStr(d.VipCardCode);
            //get("txtTotalAmount").innerHTML = getStr(d.BalanceAmount);
            //get("txtVipCardGrade").innerHTML = getStr(d.VipCardGradeName);
            //get("txtVipCardStatus").innerHTML = getStr(d.VipStatusName);
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

function fnClose() {
    CloseWin('VipCardRecharge');
}
function fnClose2() {
    var flag;
    var vip = {};

    vip.VipCardID = getUrlParam("VipCardID");
    vip.VipID = getUrlParam("VipID");
    vip.UnitID = getUrlParam("UnitID");

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/VipCard/Search/Handler/VipCardSearchHandler.ashx?method=LockVipCard&VipCardId=' + vip.VipCardID, 
        params: {
            "vip": Ext.encode(vip)
            , Lock:"1", UnitID:getUrlParam("UnitID"), VipCardStatusId:"1"
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                //showError("保存数据失败：" + d.Description);
                flag = false;
            } else {
                //showSuccess("保存数据成功");
                flag = true;
                //parent.fnSearch(1);
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnClose();
}

function fnSave() {
    var flag;
    var vip = {};

    vip.VipCardID = getUrlParam("VipCardID");
    vip.VipID = getUrlParam("VipID");
    vip.UnitID = getUrlParam("UnitID");
    vip.OrderNo = Ext.getCmp("txtOrderNo").getValue();
    vip.Remark = Ext.getCmp("txtRemark").getValue();
    vip.RechargeAmount = Ext.getCmp("txtRechargeAmount").getValue();
    
    if (Ext.getCmp('chkAmount1').getValue()) {
        vip.PaymentTypeID = "B4085585D16B496D9C2D576D8F03724C";
    } else {
        vip.PaymentTypeID = "6D3739E493B2416EA8C3DC44D388BC8C";
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/VipCard/Search/Handler/VipCardSearchHandler.ashx?method=SaveVipCardRecharge&VipCardId=' + vip.VipCardID, 
        params: {
            "vip": Ext.encode(vip)
            , Lock:"1", UnitID:getUrlParam("UnitID"), VipCardStatusId:"1"
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.Description);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnSearch(1);
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
    if (flag) fnClose();
}

fnCheckAmount = function() {
    if (Ext.getCmp('chkAmount1').getValue()) {
        Ext.getCmp('txtCashAmount').setDisabled(false);
        Ext.getCmp('txtCardAmount').setDisabled(true);
    } else {
        Ext.getCmp('txtCashAmount').setDisabled(true);
        Ext.getCmp('txtCardAmount').setDisabled(false);
    }
};
