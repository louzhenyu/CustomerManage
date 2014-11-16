﻿var VipCardID = null;

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
        params: { VipCardID: VipCardId },
        method: 'POST',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            get("txtVipCardCode").innerHTML = getStr(d.VipCardCode);
            get("txtTotalAmount").innerHTML = getStr(d.BalanceAmount);
            get("txtReturnAmount").innerHTML = getStr(d.ReturnAmount);
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

function fnClose() {
    CloseWin('VipCardDisable');
}

function fnSave() {
    var flag;
    var vip = {};
    
    vip.VipCardID = getUrlParam("VipCardID");
    vip.VipID = getUrlParam("VipID");
    vip.UnitID = getUrlParam("UnitID");
    vip.StatusIDNow = getUrlParam("VipCardStatusId");
    vip.StatusIDNext = "2";
    vip.ReturnCash = Ext.getCmp("txtReturnCash").jitGetValue();
    vip.ReturnCard = Ext.getCmp("txtReturnCard").jitGetValue();
    vip.ReturnOrder = Ext.getCmp("txtReturnOrder").jitGetValue();

    //if (vip.ReturnCash == null || vip.ReturnCash == "") {
    //    showError("请输入现金实退");
    //    return;
    //}
    //if (vip.ReturnCard == null || vip.ReturnCard == "") {
    //    showError("请输入卡实退");
    //    return;
    //}
    
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/VipCard/Search/Handler/VipCardSearchHandler.ashx?method=SaveVipCardDisable&VipCardId=' + vip.VipCardID, 
        params: {
            "vip": Ext.encode(vip)
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

