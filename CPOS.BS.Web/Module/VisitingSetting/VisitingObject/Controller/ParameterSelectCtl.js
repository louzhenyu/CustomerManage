var id = "";//stepid
var btncode = JITMethod.getUrlParam("btncode");

var updateData = new Array();
Ext.onReady(function () {
    //加载需要的文件
    //页面加载
    JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/VisitingObjectHandler.ashx?mid=" + __mid);

    InitVE();
    InitStore();

    id = JITMethod.getUrlParam("id");
    if (id != "null" && id != "") {
        fnSearch(id);
    };
});

function fnGetValue(v, pValue) {
    if (v.allSelectorStatus == 0 || v.allSelectorStatus == 2) {
        if (v.includeList.length > 0) {
            for (var j = 0; j < v.includeList.length; j++) {
                if (v.includeList[j] == pValue) {
                    return true;
                }
            }
        }
    } else {
        if (v.excludeList.length > 0) {
            for (var j = 0; j < v.excludeList.length; j++) {
                if (v.excludeList[j] == pValue) {
                    return false;
                }
            }
            return true
        } else { return true; }
    }
    return false;
}

var initView = false;
function fnSearch(id) {
    CheckBoxModel.jitClearValue();
    Ext.getStore("parameterStore").proxy.url = JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=GetObjectParameterList";
    Ext.getStore("parameterStore").pageSize = 1000000;
    Ext.getStore("parameterStore").proxy.extraParams = {
        form: "",
        id: id
    };
    //Ext.getCmp("pageBar").moveFirst();

    Ext.getStore("parameterStore").load({
        callback: function (records, options, success) {
            pagedData = new Array();
            InitStoreMemory(records);
            if (!initView) {
                initView = true;
                InitView();
                btncode == "search" ? Ext.getCmp("btnSave").hide() : Ext.getCmp("btnSave").show();
            }
        }
    });
}

function fnSave() {
    var v = CheckBoxModel.jitGetValue();
    var btn = this;
    btn.setDisabled(true);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue()
        + "&btncode=" + btncode + "&method=EditObjectParameter",
        params: {
            id: id,
            allSelectorStatus: v.allSelectorStatus,
            defaultList: v.defaultList.toString(),
            includeList: v.includeList.toString(),
            excludeList: v.excludeList.toString(),
            updateData: Ext.JSON.encode(updateData)
        },
        method: 'post',
        success: function (response) {
            btn.setDisabled(false);

            var jdata = Ext.JSON.decode(response.responseText);
            if (!JITPage.checkAjaxPermission(jdata)) {
                return;
            }
            Ext.Msg.alert("提示", "操作成功");
            window.location = window.location;
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            btn.setDisabled(false);
        }
    });
}