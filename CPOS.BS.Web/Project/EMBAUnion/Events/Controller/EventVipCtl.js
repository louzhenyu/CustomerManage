var id, btncode;
Ext.QuickTips.init();
Ext.onReady(function () {
    InitVE();
    InitStore();
    InitEditView();

    JITPage.HandlerUrl.setValue("Handler/EventVipHandler.ashx?mid=");

    fnSearch();
});

function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=GetList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        pVipName: Ext.getCmp("txt_VipName").jitGetValue(),
        pEventID: JITMethod.getUrlParam("id")
    };
    Ext.getCmp("pageBar").moveFirst();
}

/*导出数据*/
function fnExportData() {
    window.open("Handler/EventVipHandler.ashx?method=ExportUserList&r=" + Math.random() + "&pVipName=" + Ext.getCmp("txt_VipName").jitGetValue() + "&pEventID=" + JITMethod.getUrlParam("id"));
}


function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}

function fnDelete() {
    Ext.Msg.confirm("请确认", "是否要删除数据？", function (button) {
        if (button == "yes") {
            var handlerUrl = JITPage.HandlerUrl.getValue() + "&method=delete";
            Ext.Ajax.request({
                url: handlerUrl,
                params: {
                    id: JITPage.getSelected({
                        gridView: Ext.getCmp("gridView"),
                        id: "SignUpID"
                    })
                },
                method: 'POST',
                success: function (response, opts) {
                    var jdata = Ext.JSON.decode(response.responseText);
                    if (jdata.success) {
                        Ext.Msg.show({
                            title: '提示',
                            msg: '删除成功',
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.INFO,
                            fn: function () {
                                parent.Ext.getStore("eventsStore").load();
                                Ext.getStore("vipStore").load();
                            }
                        });
                    } else {
                        Ext.Msg.show({
                            title: '错误',
                            msg: jdata.msg,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                }
            });
        }
    });
}