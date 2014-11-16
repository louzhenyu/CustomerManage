var id, btncode;

Ext.onReady(function () {

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/ParameterHandler.ashx?mid=" + __mid);

    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //加载编辑页
    InitParameterEditStore();
    InitParameterEditView();

    //加载选项选择窗口
    InitOptionSelectVE();
    InitOptionSelectStore();
    InitOptionWindowView();

    fnSearch();
});

function fnCreate() {
    id = "";
    btncode = "create";
    Ext.getCmp("btnSave").show();
    Ext.getCmp("parameterEditPanel").getForm().reset();
    Ext.getStore("unitStore").removeAll();
    Ext.getStore("unitStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetUnitList";
    Ext.getStore("unitStore").load();
    Ext.getCmp("parameterEditWin").show();
}

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
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingParameterID" }),
        url: JITPage.HandlerUrl.getValue() + "&btncode=delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingParameterID" })
        },
        handler: function () {
            Ext.getStore("list_parameterStore").load();
        }
    });
}

function fnColumnUpdate(value, p, r) {
    if (!__getHidden("update")) {
        //修改权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.VisitingParameterID + "','update');\">" + value + "</a>";
    }
    else if (__getHidden("update") && !__getHidden("search")) {
        //查询权限
        return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnUpdate('" + r.data.VisitingParameterID + "','search');\">" + value + "</a>";
    }
    else if (__getHidden("update") && __getHidden("search")) {
        //无修改、查询(update,search)权限
        return "<a href=\"javascript:;\">" + value + "</a>";
    }
}

function fnColumnDelete(value, p, r) {
    return "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnDelete()\">删除</a>";
}

function fnColumnMustDo(value, p, r) {
    return (value == 1 ? "√" : "-")
}

function fnUpdate(pid,bcode) {
    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    id = pid;
    btncode = bcode;
    btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();

    Ext.getStore("unitStore").removeAll();
    Ext.getStore("unitStore").proxy.url = JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetUnitList";
    Ext.getStore("unitStore").load();

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetByID",
        params: { id: id },
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);

            //加载form
            Ext.getStore("parameterStore").removeAll();
            Ext.getStore("parameterStore").add(jdata);
            Ext.getCmp("parameterEditPanel").getForm().loadRecord(Ext.getStore("parameterStore").first());

            Ext.getCmp("ddl_ParameterType").jitSetValue(jdata.ParameterType);
            Ext.getCmp("ControlType").jitSetValue(jdata.ControlType);
            if (jdata.ControlType == 1) {
                Ext.getCmp("Num_MaxValue").jitSetValue(jdata.MaxValue);
                Ext.getCmp("Num_MinValue").jitSetValue(jdata.MinValue);
            }
            if (jdata.ControlType == 2) {
                Ext.getCmp("Num_MaxValue").jitSetValue(jdata.MaxValue);
                Ext.getCmp("Num_MinValue").jitSetValue(jdata.MinValue);
                Ext.getCmp("Num_DefaultValue").jitSetValue(jdata.DefaultValue);
            }
            if (jdata.ControlType == 3) {
                Ext.getCmp("Del_DefaultValue").jitSetValue(jdata.DefaultValue);
            }
            if (jdata.ControlType == 7) {
                Ext.getCmp("YN_DefaultValue").jitSetValue(jdata.DefaultValue);
            }
            if (jdata.ControlType == 8) {
                Ext.getCmp("Date_DefaultValue").jitSetValue(jdata.DefaultValue);
            }
            if (jdata.ControlType == 9) {               
                Ext.getCmp("Time_DefaultValue").jitSetValue(jdata.DefaultValue);
            }
            if (jdata.ControlType == 12) {
                Ext.getCmp("Num_MaxValue").jitSetValue(jdata.MaxValue);
                Ext.getCmp("Num_MinValue").jitSetValue(jdata.MinValue);
                Ext.getCmp("Num_DefaultValue").jitSetValue(jdata.DefaultValue);
            }

            Ext.getCmp("parameterEditWin").show();

            myMask.hide();
        },
        failure: function (response) {
            Ext.Msg.alert("提示", "获取参数数据失败");
            myMask.hide();
        }
    });
}