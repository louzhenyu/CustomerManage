function fnOperator() {
    var btncode = "";
    var id = new String(JITMethod.getUrlParam("id"));
    if (id != "null" && id != "") {
        btncode = "update";
    }
    else {
        id = "";
        btncode = "create";
    }

    Ext.getCmp("optionSelectPageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetOptionList";
    Ext.getCmp("optionSelectPageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("optionSelectPageBar").store.proxy.extraParams = {

};
Ext.getCmp("optionSelectPageBar").moveFirst();
Ext.getCmp("optionSelectWin").show();
}

function fnColumnSelect() {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnSelectOption()\">选择</a>"
}

function fnSelectOption() {
    Ext.getCmp("ControlName").setValue(JITPage.getSelected({ gridView: Ext.getCmp("optionSelectGridView"), id: "OptionName" })[0]);
    Ext.getCmp("optionSelectWin").hide();
}


function fnCancel() {
    Ext.getCmp("parameterEditWin").hide();
}