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

    var vipSourceId = getUrlParam("vip_source_id");
    var mid = getUrlParam("mid");
    switch (mid) {
        case 'F91B71802359487D95D7DF2FBBAAABCC':
            vipSourceId = '3';
            Ext.getCmp("txtVipSource").hide(true);
            break;
        case "1D371395A804426E8882056796AC1B5D":
            vipSourceId = '';
            break;
        case '2B03548AC0D34E00AC403C4165FF656B':
            vipSourceId = '2';
            Ext.getCmp("txtVipSource").hide(true);
            break;
    }
    //alert(getUrlParam("mid"));
    Ext.getCmp("txtVipSource").setDefaultValue(getStr(vipSourceId));

    fnSearch(vipSourceId);
    
});

function fnCreate() {
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "IntegralRuleEdit",
        title: "积分规则",
        url: "IntegralRuleEdit.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function (vipSourceId) {
    
    if (vipSourceId != null && typeof (vipSourceId) != "string") {
        vipSourceId = "";
    }
    var store = Ext.getStore("IntegralRuleStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_IntegralRule";
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
        id: "IntegralRuleEdit",
        title: "积分规则信息",
        url: "IntegralRuleEdit.aspx?IntegralRuleID=" + id
    });
	win.show();
}
function fnDelete(id) {

    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "IntegralRuleID" }),
        url: JITPage.HandlerUrl.getValue() + "&method=IntegralRule_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "IntegralRuleID" })
        },
        handler: function () {
            Ext.getStore("IntegralRuleStore").load();
        }
    });
}

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isExpand) {
      //  document.getElementById("view_Search").style.height = "156px";
        Ext.getCmp("searchPanel").isExpand = true;
        Ext.getCmp("txtVipLevel").show(true);
        Ext.getCmp("txtStatus").show(true);
        Ext.getCmp("txtTags").show(true);
        Ext.getCmp("txtTagsGroup").show(true);
//        Ext.getCmp("txtAddedTags").show(true);
        Ext.getCmp("btnAddGroup").show(true);
        Ext.getCmp("txtRegistrationTime").hidden = false;
        Ext.getCmp("txtRegistrationTime").setVisible(true);
        Ext.getCmp("txtRecentlySalesDate").hidden = false;
        Ext.getCmp("txtRecentlySalesDate").setVisible(true);
        Ext.getCmp("txtIntegration").hidden = false;
        Ext.getCmp("txtIntegration").setVisible(true);
        Ext.getCmp("txtMembershipShop").hidden = false;
        Ext.getCmp("txtMembershipShop").setVisible(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        Ext.getCmp("searchPanel").doLayout();
        document.getElementById("divTags").style.visibility = "visible";
    } else {
   //     document.getElementById("view_Search").style.height = "44px";
        Ext.getCmp("searchPanel").isExpand = false;
        Ext.getCmp("txtVipLevel").hide(true);
        Ext.getCmp("txtStatus").hide(true);
        Ext.getCmp("txtTags").hide(true);
        Ext.getCmp("txtTagsGroup").hide(true);
        
        Ext.getCmp("btnAddGroup").hide(true);
        Ext.getCmp("txtRegistrationTime").hidden = true;
        Ext.getCmp("txtRegistrationTime").setVisible(false);
        Ext.getCmp("txtRecentlySalesDate").hidden = true;
        Ext.getCmp("txtRecentlySalesDate").setVisible(false);
        Ext.getCmp("txtIntegration").hidden = true;
        Ext.getCmp("txtIntegration").setVisible(false);
        Ext.getCmp("txtMembershipShop").hidden = true;
        Ext.getCmp("txtMembershipShop").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText);
        Ext.getCmp("searchPanel").doLayout();

        document.getElementById("divTags").style.visibility = "hidden";
       
    }
}

