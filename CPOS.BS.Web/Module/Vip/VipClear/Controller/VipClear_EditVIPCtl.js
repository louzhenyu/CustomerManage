var id = JITMethod.getUrlParam("id");
var type = JITMethod.getUrlParam("type");
var pnlSearch; //查询pannel
var pnlEditView; //操作pannel
var gridStoreList; //终端数据表

Ext.onReady(function () {
    JITPage.HandlerUrl.setValue("Handler/VipClear_InvalidHandler.ashx?mid=" + __mid);
    InitView();
});

/*编辑会员信息方法*/
function fnUpdateEditView() {
    var cm = gridStoreList.getSelectionModel().getSelection();
    var pKeyValue = cm[0].data.VIPID;
    pnlEditView.action = "update";
    pnlEditView.BtnCode = "update";
    pnlEditView.setTitle('修改');
    pnlEditView.KeyValue = pKeyValue;
    pnlEditView.fnShowUpdate(pnlEditView);
    document.getElementById("tab1").src = "/Module/Vip/VipSearch/VipEdit.aspx?vip_id=" + pKeyValue + "&type=1";
    document.getElementById("tab2").src = "/Module/Vip/VipSearch/VipEdit.aspx?vip_id=" + pKeyValue + "&type=2";
    document.getElementById("tab3").src = "/Module/Vip/VipSearch/VipEdit.aspx?vip_id=" + pKeyValue + "&type=3";
    document.getElementById("tab4").src = "/Module/Vip/VipSearch/VipEdit.aspx?vip_id=" + pKeyValue + "&type=4";
    document.getElementById("tab5").src = "/Module/Vip/VipSearch/VipEdit.aspx?vip_id=" + pKeyValue + "&type=5";
}