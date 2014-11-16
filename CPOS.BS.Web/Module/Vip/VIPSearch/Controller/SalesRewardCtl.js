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
    JITPage.HandlerUrl.setValue("Handler/VipHandler.ashx?mid=");
    

    fnSearch();
    
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "vipEdit",
        title: "会员",
        url: "vipEdit2.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function (vipSourceId) {
    
    var store = Ext.getStore("SalesRewardStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_SalesReward";
    store.pageSize = JITPage.PageSize.getValue(); 
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        UnitId: Ext.getCmp("txtUnitName").jitGetValue(),
        IntegralSourceIds: fnGetIntegralSourceIds()
    };
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width()) });

}
fnGetIntegralSourceIds = function() {
    var list = Ext.getCmp("txtSysIntegralSource").jitGetValue();
    if (list != undefined && list != null && list != "") return list.join(',');
    return "";
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "vipEdit",
        title: "会员信息",
        url: "vipEdit2.aspx?vip_id=" + id
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "vip_id" }),
        url: JITPage.HandlerUrl.getValue() + "&method=vip_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "vip_id" })
        },
        handler: function () {
            Ext.getStore("vipStore").load();
        }
    });
}
function fnCancel(id) {
    Ext.getCmp("txtUnitName").jitSetValue("");
    Ext.getCmp("txtVipName").jitSetValue("");
    Ext.getCmp("txtSysIntegralSource").jitSetValue("");
    Ext.getCmp("txtDateBegin").jitSetValue("");
    Ext.getCmp("txtDateEnd").jitSetValue("");
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isexpand) {
        Ext.getCmp("searchPanel").isexpand = true;
        Ext.getCmp("txtDate").setVisible(true);
        get("btnMore").className = "z_btn3 closetwo";
    } else {
        Ext.getCmp("searchPanel").isexpand = false;
        Ext.getCmp("txtDate").setVisible(false);
        get("btnMore").className = "z_btn3 opentwo";
    }
}

function fnRenderVipColumn(a, b, c) {
    if (a > 0) {
        return "<a style='color:blue' target='_blank' href='/module/Vip/VipSearchNew/Vip.aspx?mid=" + __mid + "&type=2&paramValue=" + c.data.UserId + "'>" + a + "</a>";
    }
    return a;
}