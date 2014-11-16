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


    var QuestionnaireID = new String(JITMethod.getUrlParam("QuestionnaireID"));
    if (QuestionnaireID != "null" && QuestionnaireID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_Questionnaires_by_id",
            params: {
                QuestionnaireID: QuestionnaireID
            },
            method: 'post',
            success: function (response) {
                var storeId = "QuestionnairesEditStore";
                var pnl = Ext.getCmp("editPanel");
                var d = Ext.decode(response.responseText).topics;

                Ext.getCmp("txtQuestionnaireName").setValue(getStr(d.QuestionnaireName));
                Ext.getCmp("txtDisplayIndex").setValue(getStr(d.DisplayIndex));
                Ext.getCmp("txtQuestionnaireDesc").setValue(getStr(d.QuestionnaireDesc));


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
    CloseWin('QuestionnairesEdit');
}

function fnSave() {
    var Questionnaires = {};

    Questionnaires.QuestionnaireID = getUrlParam("QuestionnaireID");
    Questionnaires.QuestionnaireName = Ext.getCmp("txtQuestionnaireName").getValue();
    Questionnaires.DisplayIndex = Ext.getCmp("txtDisplayIndex").getValue();
    Questionnaires.QuestionnaireDesc = Ext.getCmp("txtQuestionnaireDesc").getValue();

    //if (Questionnaires.EventType == null || Questionnaires.EventType == "") {
    //    showError("必须选择类型");
    //    return;
    //}

    var flag = false;

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/QuestionnaireHandler.ashx?method=Questionnaires_save&QuestionnaireID=' + Questionnaires.QuestionnaireID,
        params: {
            "Questionnaires": Ext.encode(Questionnaires)
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

