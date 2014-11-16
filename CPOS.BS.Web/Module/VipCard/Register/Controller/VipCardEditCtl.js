Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), {
        msg: myMask_info
    });
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/RegisterHandler.ashx?mid=");

    //设置开始时间和结束时间
    var d = new Date();
    var dateNow = d.getFullYear() + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate());
    Ext.getCmp("txtBeginDate").setValue(dateNow);
    dateNow = (d.getFullYear() + 2) + "-" + (addZero(d.getMonth() + 1)) + "-" + addZero(d.getDate());
    Ext.getCmp("txtEndDate").setValue(dateNow);

    var VipCardID = new String(JITMethod.getUrlParam("VipCardID"));
    if (VipCardID != "null" && VipCardID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_vipcard_by_id",
            params: {
                VipCardID: VipCardID
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;
                
                Ext.getCmp("txtVipCardCode").setValue(d[0].VipCardCode);
                Ext.getCmp("txtVipCardName").setValue(d[0].VipCardName);

                Ext.getCmp("txtVipCardType").setDefaultValue(getStr(d[0].VipCardTypeID));
                Ext.getCmp("txtVipCardStatus").setDefaultValue(getStr(d[0].VipCardStatusId));
                Ext.getCmp("txtVipCardGrade").setDefaultValue(getStr(d[0].VipCardGradeID));
                Ext.getCmp("txtUnitName").jitSetValue([{ "id": d[0].UnitID, "text": d[0].UnitName}]);

                var startTime = new Date(d[0].BeginDate);
                var endTime = new Date(d[0].EndDate);
                Ext.getCmp("txtBeginDate").setValue(startTime);
                Ext.getCmp("txtEndDate").setValue(endTime);
                Ext.getCmp("txtBalanceAmount").setValue(d[0].BalanceAmount);

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        Ext.getCmp("txtVipCardType").setDefaultValue("1");
        Ext.getCmp("txtVipCardStatus").setDefaultValue("1");
        Ext.getCmp("txtVipCardGrade").setDefaultValue("1");
        myMask.hide();
    }
});

//关闭
function fnClose() {
    CloseWin('VipCardEdit');
}

function fnSave() {
    var vipCards = {};

    vipCards.VipId = getUrlParam("VipID");
    vipCards.VipCardID = getUrlParam("VipCardID");
    vipCards.VipCardCode = Ext.getCmp("txtVipCardCode").getValue();
    vipCards.VipCardName = Ext.getCmp("txtVipCardName").getValue();

    vipCards.VipCardTypeID = Ext.getCmp("txtVipCardType").getValue();
    vipCards.VipCardStatusId = Ext.getCmp("txtVipCardStatus").getValue();
    vipCards.VipCardGradeID = Ext.getCmp("txtVipCardGrade").getValue();
    vipCards.UnitID = Ext.getCmp("txtUnitName").jitGetValue();

    vipCards.BeginDate = Ext.getCmp("txtBeginDate").jitGetValueText();
    vipCards.EndDate = Ext.getCmp("txtEndDate").jitGetValueText();

    vipCards.BalanceAmount = Ext.getCmp("txtBalanceAmount").getValue();

//    if (vipCards.VipCardCode == null || vipCards.VipCardCode == "") {
//        showError("必须输入会员卡号");
//        return;
//    }
    if (vipCards.VipCardTypeID == null || vipCards.VipCardTypeID == "") {
        showError("必须选择卡类型");
        return;
    }
    if (vipCards.VipCardStatusId == null || vipCards.VipCardStatusId == "") {
        showError("必须选择卡状态");
        return;
    }
    if (vipCards.VipCardGradeID == null || vipCards.VipCardGradeID == "") {
        showError("必须选择卡等级");
        return;
    }
    if (vipCards.UnitID == null || vipCards.UnitID == "") {
        showError("必须选择会籍店");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/RegisterHandler.ashx?method=save_vipcard&VipCardID=' + vipCards.VipCardID,
        params: {
            "vipCards": Ext.encode(vipCards)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnLoadVipCard();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    if (flag) fnClose();
}

