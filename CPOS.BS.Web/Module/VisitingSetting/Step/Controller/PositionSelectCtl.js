var id = "";
var btncode = JITMethod.getUrlParam("btncode");

Ext.onReady(function () {
    //加载需要的文件
    
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/StepHandler.ashx?mid=" + __mid);

    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    id = JITMethod.getUrlParam("id");
    if (id != "null" && id != "") {
        fnSearch(id);
    };
});

function fnSearch(id) {
    CheckBoxModel.jitClearValue();
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetStepPositionList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: "",
        id: id
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnSave() {
    var v = CheckBoxModel.jitGetValue();

    var btn = this;
    btn.setDisabled(true);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=EditStepObject_Position",
        params: {
            id: id,
            allSelectorStatus: v.allSelectorStatus,
            defaultList: v.defaultList.toString(),
            includeList: v.includeList.toString(),
            excludeList: v.excludeList.toString()
        },
        method: 'post',
        success: function (response) {
            Ext.Msg.alert("提示", "操作成功");
            btn.setDisabled(false);
            fnSearch(id);
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            btn.setDisabled(false);
        }
    });  

}
