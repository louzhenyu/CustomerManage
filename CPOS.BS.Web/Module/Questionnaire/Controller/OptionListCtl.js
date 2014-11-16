
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
    
    var QuestionID = getUrlParam("QuestionID");
    fnSearch(QuestionID);

    myMask.hide();
});

function fnClose() {
    parent.fnSearch();
    CloseWin('OptionList');
}

function fnSearch() {
    Ext.getStore("OptionListStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=OptionList_query";
    Ext.getStore("OptionListStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("OptionListStore").proxy.extraParams = {
        QuestionID: getUrlParam("QuestionID")
    };
    Ext.getStore("OptionListStore").load();
}

function fnCreate(id, op, param) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "small",
        height: 380,
        width: 300,
        id: "OptionEdit",
        title: "选项",
        url: "OptionEdit.aspx?OptionID=" + getUrlParam("OptionID") + "&QuestionID=" + getUrlParam("QuestionID")
    });
	win.show();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "big",
        height: 380,
        id: "OptionEdit",
        title: "试题",
        url: "OptionEdit.aspx?OptionnaireID=" + getUrlParam("OptionnaireID") + "&OptionID=" + id
    });
    win.show();
}

function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({
            gridView: Ext.getCmp("gridView"),
            id: "OptionsID"
        }),
        url: JITPage.HandlerUrl.getValue() + "&method=Option_delete",
        params: {
            ids: JITPage.getSelected({
                gridView: Ext.getCmp("gridView"),
                id: "OptionsID"
            })
        },
        handler: function () {
            Ext.getStore("OptionListStore").load();
        }
    });
}

function fnOptionListView(id, type) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "OptionList",
        title: "选项列表",
        url: "OptionList.aspx?OptionID=" + id
    });

    win.show();
}


