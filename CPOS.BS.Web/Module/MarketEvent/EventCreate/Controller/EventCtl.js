Ext.Loader.setConfig({
    enabled: true
});
Ext.Loader.setPath('Ext.ux', '/Lib/Javascript/Ext4.1.0/ux');
Ext.require([
    'Ext.grid.*',
    'Ext.data.*',
    'Ext.util.*',
    'Ext.state.*',
    'Ext.form.*',
    'Ext.ux.CheckColumn'
]);

Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    //JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/EventHandler.ashx?mid=");

    fnLoad();
});

fnCheckAmount = function() {
    if (Ext.getCmp('chkAmount1').getValue()) {
        Ext.getCmp('txtAmount1').setDisabled(false);
        Ext.getCmp('txtAmount2').setDisabled(true);
    } else {
        Ext.getCmp('txtAmount1').setDisabled(true);
        Ext.getCmp('txtAmount2').setDisabled(false);
    }
};

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "RoleEdit",
        title: "角色",
        url: "RoleEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnLoad = function () {
    var MarketEventID = getUrlParam("MarketEventID");

    get("txtCode").innerHTML = newOrderNo("CA");
    $(".wrap,.header").css("width", $(".wrap>table").eq(0).width());
    if (MarketEventID != "null" && MarketEventID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_event_by_id",
            params: { MarketEventID: MarketEventID },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;

                get("txtCode").innerHTML = getStr(d.EventCode);
                Ext.getCmp("txtBrand").setDefaultValue(d.BrandID);
                //Ext.getCmp("txtEventType").setDefaultValue(d.EventType);
                Ext.getCmp("txtEventMode").setDefaultValue(d.EventMode);
                Ext.getCmp("txtAmount1").setValue(d.BudgetTotal);
                Ext.getCmp("txtAmount2").setValue(d.PerCapita);
                Ext.getCmp("txtEventDesc").setValue(d.EventDesc);

                if (d.BudgetTotal != 0) {
                    Ext.getCmp("chkAmount1").setDisabled(false);
                } else {
                    Ext.getCmp("chkAmount2").setDisabled(true);
                }
                
                //myMask.hide();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
    else {
        //myMask.hide();
    }
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "RoleEdit",
        title: "角色",
        url: "RoleEdit.aspx?role_id=" + id
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "role_id" }),
        url: JITPage.HandlerUrl.getValue() + "&method=role_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "role_id" })
        },
        handler: function () {
            Ext.getStore("roleStore").load();
        }
    });
}
fnReset = function() {
    Ext.getCmp("txtBrand").setValue("");
    Ext.getCmp("txtEventType").setValue("");
    Ext.getCmp("txtEventMode").setValue("");
    Ext.getCmp("txtEventDesc").setValue("");
    Ext.getCmp("txtAmount1").setValue("0");
    Ext.getCmp("txtAmount2").setValue("0");
}
fnSave = function() {
    var data = {};

    var MarketEventID = getUrlParam("MarketEventID");
    data.MarketEventID = MarketEventID;
    if (data.MarketEventID == null) {
        data.MarketEventID = newGuid();
    }
    data.EventCode = get("txtCode").innerHTML;
    data.BrandID = Ext.getCmp("txtBrand").getValue();
    //data.EventType = Ext.getCmp("txtEventType").getValue();
    data.EventMode = Ext.getCmp("txtEventMode").getValue();
    data.BudgetTotal = Ext.getCmp("txtAmount1").getValue();
    data.PerCapita = Ext.getCmp("txtAmount2").getValue();
    data.EventDesc = Ext.getCmp("txtEventDesc").getValue();

    if (Ext.getCmp("chkAmount1").getValue()) {
        data.PerCapita = 0;
    } else {
        data.BudgetTotal = 0;
    }

    //if (data.BrandID == null || data.BrandID == "") {
    //    showError("请选择品牌");
    //    return;
    //}
    //if (data.EventType == null || data.EventType == "") {
    //    showError("请选择活动类型");
    //    return;
    //}
    //if (data.EventMode == null || data.EventMode == "") {
    //    showError("请选择活动方式");
    //    return;
    //}
        
    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventHandler.ashx?method=event_save&MarketEventID=' + MarketEventID,
        params: { "data": Ext.encode(data) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                flag = true;
                location.href = "EventTime.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + d.data;
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}
