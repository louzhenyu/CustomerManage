Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/QuestionnaireHandler.ashx?mid=");

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "QuestionnairesEdit",
        title: "问题内容",
        url: "QuestionnairesEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch() {
    Ext.getStore("QuestionnairesStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=Questionnaires_query";
    Ext.getStore("QuestionnairesStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("QuestionnairesStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };

    Ext.getStore("QuestionnairesStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "QuestionnairesEdit",
        title: "问题内容",
        url: "QuestionnairesEdit.aspx?QuestionnaireID=" + id
    });

    win.show();
}

function fnQuestionListView(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "QuestionList",
        title: "问题列表",
        url: "QuestionList.aspx?QuestionnaireID=" + id
    });

    win.show();
}

function fnViewUserList2(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "QuestionnairesUserList",
        title: "问题人员",
        url: "QuestionnairesUserList2.aspx?QuestionnaireID=" + id
    });

    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "QuestionnaireID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=Questionnaires_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "QuestionnaireID"
            })
        },
        handler: function () {
            Ext.getStore("QuestionnairesStore").load();
        }
    });
}

