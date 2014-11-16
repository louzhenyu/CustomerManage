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

    var VipExpandID = new String(JITMethod.getUrlParam("VipExpandID"));
    if (VipExpandID != "null" && VipExpandID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_vipexpand_by_id",
            params: {
                VipExpandID: VipExpandID
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtLicensePlateNo").setValue(d[0].LicensePlateNo);
                Ext.getCmp("txtChassisNumber").setValue(d[0].ChassisNumber);

                Ext.getCmp("txtCarBrand").setValue(getStr(d[0].CarBrandID));
                Ext.getCmp("txtCarModels").fnLoad();
                Ext.getCmp("txtCarModels").jitSetValue(getStr(d[0].CarModelsID));

                Ext.getCmp("txtCompartmentsForm").setValue(d[0].CompartmentsForm);
                Ext.getCmp("txtPurchaseTime").jitSetValue(d[0].PurchaseTime);
                Ext.getCmp("txtRemark").setValue(d[0].Remark);

                myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    } else {
        myMask.hide();
    }
});

//关闭
function fnClose() {
    CloseWin('VipExpandEdit');
}

function fnSave() {
    var vipExpands = {};

    vipExpands.VipExpandID = getUrlParam("VipExpandID");
    vipExpands.VipID = getUrlParam("VipID");
    vipExpands.LicensePlateNo = Ext.getCmp("txtLicensePlateNo").getValue();
    vipExpands.ChassisNumber = Ext.getCmp("txtChassisNumber").getValue();

    vipExpands.CarBrandID = Ext.getCmp("txtCarBrand").getValue();
    vipExpands.CarModelsID = Ext.getCmp("txtCarModels").getValue();

    vipExpands.CompartmentsForm = Ext.getCmp("txtCompartmentsForm").getValue();
    vipExpands.PurchaseTime = Ext.getCmp("txtPurchaseTime").jitGetValueText();
    vipExpands.Remark = Ext.getCmp("txtRemark").getValue();

    if (vipExpands.LicensePlateNo == null || vipExpands.LicensePlateNo == "") {
        showError("必须输入车牌号");
        return;
    }
    if (vipExpands.CarBrandID == null || vipExpands.CarBrandID == "") {
        showError("必须选择车品牌");
        return;
    }
    if (vipExpands.CarModelsID == null || vipExpands.CarModelsID == "") {
        showError("必须选择车型");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/RegisterHandler.ashx?method=save_vipexpand&VipExpandID=' + vipExpands.VipExpandID,
        params: {
            "vipExpands": Ext.encode(vipExpands)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                parent.fnLoadVipExpand();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

    if (flag) fnClose();
}

