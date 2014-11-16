Ext.Loader.setConfig({
    enabled: true
});

Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');

Ext.require(['Ext.grid.*', 'Ext.data.*', 'Ext.util.*', 'Ext.state.*', 'Ext.form.*', 'Ext.ux.CheckColumn']);

Ext.onReady(function () { 
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.HandlerUrl.setValue("Handler/CRMHandler.ashx?mid=" + __mid);

    fnSearch();
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "VipEnterpriseEdit",
        title: "联系人",
        url: "VipEnterpriseEdit.aspx?mid=" + __mid + "&op=new"
    });

    win.show();
}

function fnSearch() {
    Ext.getStore("VipEnterpriseStore").proxy.url = JITPage.HandlerUrl.getValue() + "&method=VipEnterprise_query";
    Ext.getStore("VipEnterpriseStore").pageSize = JITPage.PageSize.getValue();
    Ext.getStore("VipEnterpriseStore").proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        ECCustomerId: get("hECCustomerId").value
    };

    Ext.getStore("VipEnterpriseStore").load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: 550,
        id: "VipEnterpriseEdit",
        title: "联系人内容",
        url: "VipEnterpriseEdit.aspx?id=" + id
    });

    win.show();
}

function fnDelete(id, val) {
    if (val == "1") {
        if (!confirm("确认停用?")) return;
    } else {
        if (!confirm("确认启用?")) return;
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=VipEnterprise_delete",
        params: { ids: id, status: val },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText);
            if (!d.success) {
                alert(d.msg);
                return;
            }
            fnSearch();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });
    return true;
}

fnReset = function (){
    Ext.getCmp("txtVipName").jitSetValue("");
    Ext.getCmp("txtEnterpriseCustomerId").setValue("");
    get("hECCustomerId").value = "";
    Ext.getCmp("txtStatus").jitSetValue("");
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
        document.getElementById("view_Search").style.height = "118px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtCityId").show(true);
        Ext.getCmp("txtScaleId").show(true);
        Ext.getCmp("txtStatus").show(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
    } else {
        document.getElementById("view_Search").style.height = "86px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtCityId").hide(true);
        Ext.getCmp("txtScaleId").hide(true);
        Ext.getCmp("txtStatus").hide(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();
    }
}
