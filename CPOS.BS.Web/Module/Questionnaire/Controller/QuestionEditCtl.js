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


    var QuestionID = new String(JITMethod.getUrlParam("QuestionID"));
    if (QuestionID != "null" && QuestionID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_Question_by_id",
            params: {
                QuestionID: QuestionID
            },
            method: 'post',
            success: function (response) {
                var storeId = "QuestionEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtQuestionType").setDefaultValue(getStr(d.QuestionType));
                Ext.getCmp("txtQuestionDesc").setValue(getStr(d.QuestionDesc));
                Ext.getCmp("txtQuestionValue").setValue(getStr(d.QuestionValue));

                Ext.getCmp("txtMinSelected").setValue(getStr(d.MinSelected));
                Ext.getCmp("txtMaxSelected").setValue(getStr(d.MaxSelected));
                Ext.getCmp("txtQuestionValueCount").setValue(getStr(d.QuestionValueCount));
                Ext.getCmp("txtIsRequired").setDefaultValue(getStr(d.IsRequired));
                Ext.getCmp("txtIsOpen").setDefaultValue(getStr(d.IsOpen));
                Ext.getCmp("txtIsSaveOutEvent").setDefaultValue(getStr(d.IsSaveOutEvent));
                Ext.getCmp("txtCookieName").setValue(getStr(d.CookieName));
                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.DisplayIndexNo));

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
    CloseWin('QuestionEdit');
}

function fnSave() {
    var Question = {};
    Question.QuestionnaireID = getUrlParam("QuestionnaireID");
    Question.QuestionID = getUrlParam("QuestionID");
    Question.QuestionType = Ext.getCmp("txtQuestionType").getValue();
    Question.QuestionDesc = Ext.getCmp("txtQuestionDesc").getValue();
    Question.QuestionValue = Ext.getCmp("txtQuestionValue").getValue();
    Question.MinSelected = Ext.getCmp("txtMinSelected").getValue();
    Question.MaxSelected = Ext.getCmp("txtMaxSelected").getValue();
    Question.QuestionValueCount = Ext.getCmp("txtQuestionValueCount").getValue();
    Question.IsRequired = Ext.getCmp("txtIsRequired").getValue();
    Question.IsOpen = Ext.getCmp("txtIsOpen").getValue();
    Question.IsSaveOutEvent = Ext.getCmp("txtIsSaveOutEvent").getValue();
    Question.CookieName = Ext.getCmp("txtCookieName").getValue();
    Question.DisplayIndexNo = Ext.getCmp("txtDisplayIndex").getValue();

    if (Question.QuestionType == null || Question.QuestionType == "") {
        showError("必须选择类型");
        return;
    }
    if (Question.QuestionDesc == null || Question.QuestionDesc == "") {
        showError("必须输入试题描述");
        return;
    }
    if (Question.MinSelected == null || Question.MinSelected == "") {
        showError("必须输入最少选中项");
        return;
    }
    if (Question.MaxSelected == null || Question.MaxSelected == "") {
        showError("必须输入最多选中项");
        return;
    }
    if (Question.QuestionValueCount == null || Question.QuestionValueCount == "") {
        showError("必须输入问题答案数量");
        return;
    }
    if (Question.IsRequired == null || Question.IsRequired == "") {
        showError("必须选择是否必填项");
        return;
    }
    if (Question.IsOpen == null || Question.IsOpen == "") {
        showError("必须选择试题是否开放");
        return;
    }
    if (Question.IsSaveOutEvent == null || Question.IsSaveOutEvent == "") {
        showError("必须选择是否公共的问题");
        return;
    }
    //if (Question.CookieName == null || Question.CookieName == "") {
    //    showError("必须输入cookie存储名字");
    //    return;
    //}

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/QuestionnaireHandler.ashx?method=Question_save&QuestionID=' + Question.QuestionID,
        params: {
            "Question": Ext.encode(Question)
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

