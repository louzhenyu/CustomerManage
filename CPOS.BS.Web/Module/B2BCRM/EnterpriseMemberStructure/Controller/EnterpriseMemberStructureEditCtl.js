var btncode = JITMethod.getUrlParam("btncode");
var id = JITMethod.getUrlParam("id");
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/EnterpriseMemberStructureHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnSearch() {
    if (id != null && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetEnterpriseMemberStructureByID",
            params: { id: id },
            method: 'post',
            success: function (response) {
                var jdata = eval(response.responseText);
                //加载form
                Ext.getStore("EnterpriseMemberStructureEditStore").add(jdata[0]);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("EnterpriseMemberStructureEditStore").first());
//                alert(Ext.getStore("EnterpriseMemberStructureEditStore").first().data.ParentID)
//                Ext.getCmp("ParentID").jitSetValue(Ext.getStore("EnterpriseMemberStructureEditStore").first().data.ParentID);

            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
}
function fnSubmit() {
    var form = this.up('form').getForm();
    if (!form.isValid()) {
        return false;
    }
    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditEnterpriseMemberStructure",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: '保存成功',
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });

                parent.Ext.getStore("EnterpriseMemberStructureStore").load();
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: '保存失败',
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg:'保存失败',
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}
