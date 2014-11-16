var btncode = JITMethod.getUrlParam("btncode");
var id = JITMethod.getUrlParam("id");

var pageLanguage = new Object();


var pnlSearch; //查询pannel
var pnlWork; //操作pannel


var btnAdd; //增加

var gridStoreList; //终端数据表

Ext.onReady(function () {

    JITPage.HandlerUrl.setValue("/Module/VisitingSetting/Step/Handler/StepHandler_SKU.ashx?mid=" + __mid);

    InitView();

    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

});

function fnSubmit() {
        var v = gridStoreList.selModel.jitGetValue();

        var btn = this;
        btn.setDisabled(true);
        Ext.Ajax.request({
            url: "/Module/VisitingSetting/Step/Handler/StepHandler_SKU.ashx?mid="
                        + __mid
                        + "&btncode=" + btncode + "&method=EditStepObject_SKU",
            params: {
                id: id,
                sCondition: gridStoreList.searchConditions,
                allSelectorStatus: v.allSelectorStatus,
                defaultList: v.defaultList.toString(),
                includeList: v.includeList.toString(),
                excludeList: v.excludeList.toString()
            },
            method: 'post',
            success: function (response) {
                Ext.Msg.alert("提示", "操作成功");
                btn.setDisabled(false);

                gridStoreList.pagebar.moveFirst();
                //fnSearch(id);
            },
            failure: function () {
                Ext.Msg.alert("提示", "操作失败");
                btn.setDisabled(false);
            }
        });
}