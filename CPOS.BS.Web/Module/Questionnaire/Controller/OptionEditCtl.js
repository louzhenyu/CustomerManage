var K;
var htmlEditor;

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
    JITPage.HandlerUrl.setValue("Handler/QuestionnaireHandler.ashx?mid=");


    var OptionID = new String(JITMethod.getUrlParam("OptionID"));
    if (OptionID != "null" && OptionID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_Option_by_id",
            params: {
                OptionID: OptionID
            },
            method: 'post',
            success: function (response) {
                var storeId = "OptionEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtOptionDesc").setDefaultValue(getStr(d.OptionsText));
                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.DisplayIndex));

                Ext.getCmp("txtIsSelect").setDefaultValue(getStr(d.IsSelect));

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

function fnClose() {
    CloseWin('OptionEdit');
}

function fnSave() {
    var Option = {};
    Option.QuestionID = getUrlParam("QuestionID");
    Option.OptionID = getUrlParam("OptionID");
    Option.OptionsText = Ext.getCmp("txtOptionDesc").getValue();
    Option.DisplayIndex = Ext.getCmp("txtDisplayIndex").getValue();
    Option.IsSelect = Ext.getCmp("txtIsSelect").getValue();

    if (Option.OptionsText == null || Option.OptionsText == "") {
        showError("必须输入试题描述");
        return;
    }
    if (Option.DisplayIndex == null || Option.DisplayIndex == "") {
        showError("必须输入排序");
        return;
    }
    if (Option.IsSelect == null || Option.IsSelect == "") {
        showError("必须选择是否选中");
        return;
    }

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/QuestionnaireHandler.ashx?method=Option_save&OptionID=' + Option.OptionID,
        params: {
            "Option": Ext.encode(Option)
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

