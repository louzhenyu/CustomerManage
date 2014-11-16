var id, btncode;

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/ParameterOptionHandler.ashx?mid=" + __mid);

    fnSearch();
});



function fnSearch() {
    Ext.getCmp("pageBar").store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=search&method=GetList";
    Ext.getCmp("pageBar").store.pageSize = JITPage.PageSize.getValue();
    Ext.getCmp("pageBar").store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    Ext.getCmp("pageBar").moveFirst();
}

function fnReset() {
    Ext.getCmp("searchPanel").getForm().reset();
}

function fnDelete() {
    JITPage.delList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "OptionName" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            name: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "OptionName" })
        },
        handler: function () {
            Ext.getStore("optionsListStore").load();
        }
    });
}

function fnColumnUpdate(value, p, r) {

    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" +

value + "','update');\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" +

value + "','search');\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }

}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}



var valuei = 1;
var valuej = 1;
function fnCreate() {
    btncode = "create";
    Ext.getCmp("btnSave").show();
    valuei = 1;
    valuej = 1;
    Ext.getCmp("editPanel").getForm().reset();
    Ext.getCmp("OptionName").setDisabled(false);
    Ext.getStore("optionsEditStore").removeAll();
    Ext.getCmp("editWin").show();
}

function fnUpdate(name, bcode) {
    btncode = bcode;
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetOptionByName",
        params: { name: name },
        method: 'post',
        success: function (response) {
            var jdata = eval(response.responseText);

            //加载form
            //            Ext.getStore("optionsStore").add(jdata[0]);
            //            Ext.getCmp("editPanel").getForm().loadRecord(Ext.getStore("optionsStore").first());
            Ext.getCmp("OptionName").setValue(name);
            Ext.getCmp("OptionName").setDisabled(true);

            //加载grid
            Ext.getStore("optionsEditStore").removeAll();
            Ext.getStore("optionsEditStore").add(jdata);
            valuei = jdata.length + 1;
            valuej = jdata.length + 1;
            Ext.getCmp("editWin").show();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
}

function fnSubmit() {
    var form = Ext.getCmp('editPanel').getForm();
    if (!form.isValid()) {
        return false;
    }
    var oplist = [];
    for (var i = 0; i < Ext.getStore("optionsEditStore").getCount(); i++) {
        oplist.push(Ext.getStore("optionsEditStore").getAt(i).data);
    }
    if (oplist.length == 0) {
        Ext.Msg.show({
            title: '提示',
            msg: "请添加选项",
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
        return false;
    }


    form.submit({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=Edit",
        waitTitle: JITPage.Msg.SubmitDataTitle,
        waitMsg: JITPage.Msg.SubmitData,
        params: {
            options: Ext.JSON.encode(oplist),
            OptionName: Ext.getCmp("OptionName").getValue()
            //,Sequence: Ext.getCmp("Sequence").getValue()
        },
        success: function (fp, o) {
            if (o.result.success) {
                Ext.Msg.show({
                    title: '提示',
                    msg: o.result.msg,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.INFO,
                    fn: function () {
                        Ext.getCmp("editWin").hide(),
                        fnSearch()
                    }
                });
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

function fnCancel() {
    Ext.getCmp("editWin").hide();
}