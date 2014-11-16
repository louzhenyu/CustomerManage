
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
    
    var QuestionnaireID = getUrlParam("QuestionnaireID");
    fnSearch(QuestionnaireID);

    myMask.hide();
});

function fnClose() {
    parent.fnSearch();
    CloseWin('QuestionList');
}

function fnSearch() {
    Ext.getStore("QuestionListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=QuestionList_query";
    Ext.getStore("QuestionListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("QuestionListStore").proxy.extraParams = {
        QuestionnaireId: getUrlParam("QuestionnaireID")
    };
    Ext.getStore("QuestionListStore").load();
}

function fnCreate(id, op, param) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "QuestionEdit",
        title: "试题",
        url: "QuestionEdit.aspx?QuestionnaireID=" + getUrlParam("QuestionnaireID")
    });
	win.show();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "QuestionEdit",
        title: "试题",
        url: "QuestionEdit.aspx?QuestionnaireID=" + getUrlParam("QuestionnaireID") + "&QuestionID=" + id
    });
    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "QuestionID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=Question_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "QuestionID"
            })
        },
        handler: function () {
            Ext.getStore("QuestionListStore").load();
        }
    });
}

function fnOptionListView(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 500,
        width: 600,
        id: "OptionList",
        title: "选项列表",
        url: "OptionList.aspx?QuestionID=" + id
    });

    win.show();
}


