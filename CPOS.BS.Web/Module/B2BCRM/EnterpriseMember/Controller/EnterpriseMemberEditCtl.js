var btncode = JITMethod.getUrlParam("btncode");
var id = JITMethod.getUrlParam("id");
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    JITPage.HandlerUrl.setValue("Handler/EnterpriseMemberHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnSearch() {
    if (id != null && id != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetEnterpriseMemberByID",
            params: { id: id },
            method: 'post',
            success: function (response) {
                var jdata = eval(response.responseText);
                //加载form
                Ext.getStore("EnterpriseMemberStore").add(jdata[0]);
                Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("EnterpriseMemberStore").first());

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
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=EditEnterpriseMember",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            id: id,
            VisitingTaskID: JITMethod.getUrlParam("tid")
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO
                });

                parent.Ext.getStore("EnterpriseMemberStore").load();
            }
            else {
                Ext.Msg.show({
                    title: '错误',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        },
        failure: function (fp, o) {
            Ext.Msg.show({
                title: '错误',
                msg: o.result.msg,
                buttons: Ext.Msg.OK,
                icon: Ext.Msg.ERROR
            });
        }
    });
}
