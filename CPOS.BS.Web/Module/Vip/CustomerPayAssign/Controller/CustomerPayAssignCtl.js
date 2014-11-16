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
    JITPage.HandlerUrl.setValue("Handler/CustomerPayAssignHandler.ashx?mid=");

    fnSearch();
    
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "CustomerPayAssignEdit",
        title: "信息",
        url: "CustomerPayAssignEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function () {
    var store = Ext.getStore("CustomerPayAssignStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_CustomerPayAssign";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
        //UnitId: Ext.getCmp("txtUnitName").jitGetValue(),
        //MembershipShopId: Ext.getCmp("txtMembershipShop").jitGetValue(),
        //VipSourceId: vipSourceId,
        //Tags: tags
    };
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width()) });

}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "CustomerPayAssignEdit",
        title: "信息",
        url: "CustomerPayAssignEdit.aspx?AssignId=" + id
    });
	win.show();
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "AssignId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=CustomerPayAssign_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "AssignId" })
        },
        handler: function () {
            Ext.getStore("CustomerPayAssignStore").load();
        }
    });
}

function fnCancel() {
    get("txtAddedTags").innerHTML = "";
    tagsData = [];
}
