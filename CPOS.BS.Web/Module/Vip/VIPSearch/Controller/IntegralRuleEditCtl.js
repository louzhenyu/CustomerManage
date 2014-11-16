Ext.onReady(function () {
    //加载需要的文件
    //var myMask_info = "loading...";
    //var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();

    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/VipHandler.ashx?mid=");

    fnLoad();
});

fnLoad = function() {
    var IntegralRuleID = getUrlParam("IntegralRuleID");
    if (IntegralRuleID != "null" && IntegralRuleID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_IntegralRule_by_id",
            params: {
                IntegralRuleID: IntegralRuleID
            },
            method: 'post',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                if (d != null) {
                    Ext.getCmp("txtIntegralSourceID").jitSetValue(getStr(d.IntegralSourceID));
                    Ext.getCmp("txtIntegral").jitSetValue(getStr(d.Integral));
                    Ext.getCmp("txtBeginDate").jitSetValue(getDate(d.BeginDate));
                    Ext.getCmp("txtEndDate").jitSetValue(getDate(d.EndDate));
                    Ext.getCmp("txtIntegralDesc").jitSetValue(getStr(d.IntegralDesc));
                }
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
        
    }
}

function fnClose() {
    CloseWin('IntegralRuleEdit');
}

function fnCloseWin() {
    CloseWin('IntegralRuleEdit');
}

function fnSave() {
    var item = {};
    item.IntegralRuleID = getUrlParam("IntegralRuleID");
    item.IntegralSourceID = Ext.getCmp("txtIntegralSourceID").jitGetValue();
    item.Integral = Ext.getCmp("txtIntegral").jitGetValue();
    item.BeginDateStr = Ext.getCmp("txtBeginDate").jitGetValue();
    item.EndDateStr = Ext.getCmp("txtEndDate").jitGetValue();
    item.IntegralDesc = Ext.getCmp("txtIntegralDesc").jitGetValue();

    if (item.IntegralSourceID == null || item.IntegralSourceID.length == 0) {
        alert("积分变动行为不能为空");
        return;
    }
    if (item.Integral == null || item.Integral.length == 0) {
        alert("积分公式不能为空");
        return;
    }
    if (item.BeginDateStr == null || item.BeginDateStr.length == 0 ||
        item.EndDateStr == null || item.EndDateStr.length == 0) {
        alert("有效日期不能为空");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/VipHandler.ashx?method=IntegralRule_save',
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
    if (flag) fnClose();
}

