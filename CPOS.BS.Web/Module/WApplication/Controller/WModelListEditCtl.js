Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");
    
    var ModelId = getUrlParam("ModelId");
    if (ModelId != "null" && ModelId != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_WModelList_by_id",
            params: { ModelId: ModelId },
            method: 'POST',
            success: function (response) {
                var storeId = "WModelListEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).data;
                
                Ext.getCmp("txtModelCode").jitSetValue(getStr(d.ModelCode));
                Ext.getCmp("txtModelName").jitSetValue(getStr(d.ModelName));
                Ext.getCmp("txtApplicationId").setDefaultValue(getStr(d.ApplicationId));

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
    CloseWin('WModelListEdit');
}

function fnSave() {
    var flag;
    var WModelList = {};
    WModelList.ModelId = getUrlParam("ModelId");
    WModelList.ModelCode = Ext.getCmp("txtModelCode").jitGetValue();
    WModelList.ModelName = Ext.getCmp("txtModelName").jitGetValue();
    WModelList.ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();

    if (WModelList.ModelCode == null || WModelList.ModelCode == "") {
        showError("请填写模板号码");
        return;
    }
    if (WModelList.ModelName == null || WModelList.ModelName == "") {
        showError("请填写模板名称");
        return;
    }
    if (WModelList.ApplicationId == null || WModelList.ApplicationId == "") {
        showError("请填写微信账号");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=WModelList_save&ModelId=' + WModelList.ModelId, 
        params: {
            "item": Ext.encode(WModelList)
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
    CloseWin('WModelListEdit');
}

