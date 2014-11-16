function fnRenderDelete(a, b, c) {
    return "<a href='#' style='color:blue;' onclick='fnDelete()'>删除</a>";
}
//显示添加页面
function fnAddEditView() {
    pnlEditView.BtnCode = "create";
    pnlEditView.action = "create";
    pnlEditView.isInit = true;
    pnlEditView.KeyValue = "";
    tabState.keyValue = "";
    pnlEditView.setTitle('添加会员');
    pnlEditView.fnShowAdd(pnlEditView);

    //显示所有控件，并设置readonly
    if (pnlEditView.showPannel.items.items[0].items.items != null && pnlEditView.showPannel.items.items[0].items.items.length > 0) {
        for (var i = 0; i < pnlEditView.showPannel.items.items[0].items.items.length; i++) {
            pnlEditView.showPannel.items.items[0].items.items[i].setVisible(true);
            try {
                pnlEditView.showPannel.items.items[0].items.items[i].setReadOnly(false);
            } catch (e) { }
        }
    }
    Ext.getCmp("__wp_btn_1").setVisible(false);
    Ext.getCmp("__wp_btn_2").setVisible(true);
    Ext.getCmp("__wp_btn_3").setVisible(true);
    Ext.getCmp("__wp_btn_4").setVisible(false);
    Ext.getCmp("__wp_btn_5").setVisible(false);
}

//显示修改
function fnUpdateEditView() {
    var cm = gridStoreList.getSelectionModel().getSelection();
    var pKeyValue = cm[0].data.VIPID;
    pnlEditView.KeyValue = pKeyValue;
    tabState.keyValue = pKeyValue; //编辑页的tab需要
    pnlEditView.fnShowUpdate(pnlEditView);
    document.getElementById("tab1").src = "/Module/Vip/VipSearch/VipEdit.aspx?vip_id=" + pKeyValue + "&type=1";
    Ext.getCmp("PannelWindow").show();
}
//删除操作
function fnDelete() {
    var cm = gridStoreList.getSelectionModel().getSelection();

    var pKeyValue = '';
    for (var i = 0; i < cm.length; i++) {
        if (i == 0) {
            pKeyValue = cm[i].data.VIPID;
        }
        else {
            pKeyValue = pKeyValue + ',' + cm[i].data.VIPID;
        }
    }

    pnlEditView.isInit == false;
    pnlEditView.BtnCode = "delete";
    pnlEditView.action = "delete";
    pnlEditView.KeyValue = pKeyValue;
    pnlEditView.fnDelete(pnlEditView);
}

function fnExport() {
    pnlEditView.isInit == false;
    pnlEditView.BtnCode = "export";
    pnlEditView.action = "export";
    pnlEditView.pnlSearch = pnlSearch;
    pnlEditView.fnExport(pnlEditView);

}

//导入操作
function fnImport() {
    Ext.getCmp("FileUploadID").jitClreaValue();
    Ext.getCmp("FileUploadID").show();
}