function fnRenderDelete(a, b, c) {
    return "<a href='#' style='color:blue;' onclick='fnDelete()'>删除</a>";
}
//显示添加页面

function fnAddEditView() {
    pnlEditView.BtnCode = "create";
    pnlEditView.action = "create";
    pnlEditView.isInit = true;
    pnlEditView.KeyValue = ""
    pnlEditView.setTitle('添加终端');
    pnlEditView.fnShowAdd(pnlEditView);
}
//显示修改
function fnUpdateEditView() {
    var cm = gridStoreList.getSelectionModel().getSelection();
    var pKeyValue = cm[0].data.StoreID;
    if (__getHidden("update")) {
        pnlEditView.action = "search";
        pnlEditView.BtnCode = "search";
        pnlEditView.setTitle('查看终端');
    }
    else {
        pnlEditView.action = "update";
        pnlEditView.BtnCode = "update";
        pnlEditView.setTitle('修改终端');
    }
    pnlEditView.KeyValue = pKeyValue;
    pnlEditView.fnShowUpdate(pnlEditView);
}
//删除操作
function fnDelete() {
    var cm = gridStoreList.getSelectionModel().getSelection();
    if (cm.length == 0) {
        Ext.Msg.alert("提示", '选择删除行');
        return;
    }

    var pKeyValue = '';
    for (var i = 0; i < cm.length; i++) {
        if (i == 0) {
            pKeyValue = cm[i].data.StoreID;
        }
        else {
            pKeyValue = pKeyValue + ',' + cm[i].data.StoreID;
        }
    }


    pnlEditView.isInit == false;
    pnlEditView.BtnCode = "delete";
    pnlEditView.action = "delete";

    pnlEditView.KeyValue = pKeyValue;
    pnlEditView.fnDelete(pnlEditView);

}


//导出操作
function fnExport() {
    pnlEditView.isInit == false;
    pnlEditView.BtnCode = "export";
    pnlEditView.action = "export";
    pnlEditView.pnlSearch = pnlSearch;
    pnlEditView.fnExport(pnlEditView);

}