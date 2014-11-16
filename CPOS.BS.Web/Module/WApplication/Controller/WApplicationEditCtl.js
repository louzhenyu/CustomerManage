Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();
    Ext.getCmp("txtCustomerId").jitSetValue(getStr(cId));
    //alert(cId);
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");
    var WApplicationId = getUrlParam("WApplicationId");
    if (WApplicationId != "null" && WApplicationId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_wapplication_by_id",
            params: { WApplicationId: WApplicationId },
            method: 'POST',
            success: function (response) {
                var storeId = "wApplicationEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;

                Ext.getCmp("txtWeiXinName").jitSetValue(getStr(d.WeiXinName));
                Ext.getCmp("txtWeiXinID").jitSetValue(getStr(d.WeiXinID));
                Ext.getCmp("txtURL").jitSetValue(getStr(d.URL));
                Ext.getCmp("txtToken").jitSetValue(getStr(d.Token));
                Ext.getCmp("txtAppID").jitSetValue(getStr(d.AppID));
                Ext.getCmp("txtAppSecret").jitSetValue(getStr(d.AppSecret));
                Ext.getCmp("txtServerIP").jitSetValue(getStr(d.ServerIP));
                Ext.getCmp("txtFileAddress").jitSetValue(getStr(d.FileAddress));
                Ext.getCmp("txtIsHeight").setDefaultValue(getStr(d.IsHeight));
                Ext.getCmp("txtLoginUser").jitSetValue(getStr(d.LoginUser));
                Ext.getCmp("txtLoginPass").jitSetValue(getStr(d.LoginPass));
                Ext.getCmp("txtCustomerId").jitSetValue(getStr(d.CustomerId));
                Ext.getCmp("txtWeiXinType").setDefaultValue(getStr(d.WeiXinTypeId));
                Ext.getCmp("txtAuthUrl").jitSetValue(getStr(d.AuthUrl));
                //加解密字段
                //if (d.EncryptType != null && d.EncryptType != '') {
                //    Ext.getCmp("txtEncryptType").jitSetValue(getStr(d.EncryptType));
                //}
                //else {
                //    Ext.getCmp("txtEncryptType").jitSetValue(getStr('0'));
                //}

                //Ext.getCmp("txtCurrentAESKey").jitSetValue(getStr(d.CurrentEncodingAESKey));
                //Ext.getCmp("txtPrevAESKey").jitSetValue(getStr(d.PrevEncodingAESKey));

                if (d.IsMoreCS == 1) {
                    Ext.getCmp("ckIsMoreCS").jitSetValue(true);
                }
                else {
                    Ext.getCmp("ckIsMoreCS").jitSetValue(false);
                }
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

fnLoadRole = function () {
    var store = Ext.getStore("wApplicationEditRoleStore");
    store.load({
        url: JITPage.HandlerUrl.getValue() + "&method=get_wApplication_role_info_by_wApplication_id&WApplicationId=" +
            getUrlParam("WApplicationId"),
        params: { start: 0, limit: 0 }
    });
}

function fnClose() {
    CloseWin('WApplicationEdit');
}

function fnSave() {
    var flag;
    var wApplication = {};
    wApplication.ApplicationId = $.trim(getUrlParam("WApplicationId"));
    wApplication.WeiXinName = $.trim(Ext.getCmp("txtWeiXinName").jitGetValue());
    wApplication.WeiXinID = $.trim(Ext.getCmp("txtWeiXinID").jitGetValue());
    wApplication.URL = $.trim(Ext.getCmp("txtURL").jitGetValue());
    wApplication.Token = $.trim(Ext.getCmp("txtToken").jitGetValue());
    wApplication.AppID =$.trim(Ext.getCmp("txtAppID").jitGetValue());
    wApplication.AppSecret = $.trim(Ext.getCmp("txtAppSecret").jitGetValue());
    wApplication.ServerIP = $.trim(Ext.getCmp("txtServerIP").jitGetValue());
    wApplication.FileAddress = $.trim(Ext.getCmp("txtFileAddress").jitGetValue());
    wApplication.IsHeight = $.trim(Ext.getCmp("txtIsHeight").jitGetValue());
    wApplication.LoginUser = $.trim(Ext.getCmp("txtLoginUser").jitGetValue());
    wApplication.LoginPass = $.trim(Ext.getCmp("txtLoginPass").jitGetValue());
    wApplication.CustomerId = $.trim(Ext.getCmp("txtCustomerId").jitGetValue());
    wApplication.WeiXinTypeId = $.trim(Ext.getCmp("txtWeiXinType").jitGetValue());
    wApplication.AuthUrl = $.trim(Ext.getCmp("txtAuthUrl").jitGetValue());
    //加解密字段 2014-10-21 zoukun
    wApplication.EncryptType = '0';// $.trim(Ext.getCmp("txtEncryptType").jitGetValue());
    wApplication.CurrentEncodingAESKey = '';// $.trim(Ext.getCmp("txtCurrentAESKey").jitGetValue());
    wApplication.PrevEncodingAESKey = '';//$.trim(Ext.getCmp("txtPrevAESKey").jitGetValue());
    // = Ext.getCmp("ckIsMoreCS").jitGetValue();
    if (Ext.getCmp("ckIsMoreCS").jitGetValue() == true) {
        wApplication.IsMoreCS = 1;
    }
    else {
        wApplication.IsMoreCS = 0;
    }
    if (wApplication.WeiXinName == null || wApplication.WeiXinName == "") {
        showError("请填写微信账号名称");
        return;
    }
    if (wApplication.WeiXinID == null || wApplication.WeiXinID == "") {
        showError("请填写平台唯一码");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=wapplication_save&WApplicationId=' + wApplication.ApplicationId,
        params: {
            "item": Ext.encode(wApplication)
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
    CloseWin('WApplicationEdit');
}

