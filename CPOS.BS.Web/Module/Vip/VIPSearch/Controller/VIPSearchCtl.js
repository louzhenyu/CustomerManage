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
var loadTags = false;
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
            Ext.getCmp("txtVipSource").disable(true);
            break;
        case "1D371395A804426E8882056796AC1B5D":
            vipSourceId = '';
            break;
        case '2B03548AC0D34E00AC403C4165FF656B':
            vipSourceId = '2';
            Ext.getCmp("txtVipSource").disable(true);
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
        id: "vipEdit",
        title: "会员",
        url: "vipEdit2.aspx?mid=" + __mid
    });
	win.show();
}

fnSearch = function (vipSourceId) {
    var tags = "";
    for (var i = 0; i < tagsData.length; i++) {
        tags += ";" + tagsData[i].tags + "," + (tagsData[i].tagsGroup == undefined ? "3" : tagsData[i].tagsGroup);
    }
    if (vipSourceId != null && typeof (vipSourceId) != "string") {
        vipSourceId = "";
    }
    
    if (!loadTags && getUrlParam("TagsId").length > 0 && tags.length == 0) {
        tags += ";" + getUrlParam("TagsId") + ",3";
    }

    var store = Ext.getStore("vipSearchStore");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_vip";
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues()),
        UnitId: Ext.getCmp("txtUnitName").jitGetValue(),
        MembershipShopId: Ext.getCmp("txtMembershipShop").jitGetValue(),
        VipSourceId: vipSourceId,
        Tags: tags
    };
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width()) });

}

function fnView(id, tabsIndex) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";

    var url = "vipEdit2.aspx?vip_id=" + id;
    if (tabsIndex != undefined && tabsIndex != null) {
        url += "&tabsIndex=" + tabsIndex
    }

    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "vipEdit",
        title: "会员信息",
        url: url
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

function fnMoreSearchView(type) {
    if (!Ext.getCmp("searchPanel").isexpand) {
        Ext.getCmp("searchPanel").isexpand = true;
        Ext.getCmp("txtVipLevel").setVisible(true);
        Ext.getCmp("txtStatus").setVisible(true);
        Ext.getCmp("txtTags").setVisible(true);
        Ext.getCmp("txtTagsGroup").setVisible(true);
        Ext.getCmp("btnAddGroup").setVisible(true);
        Ext.getCmp("txtRegistrationTime").setVisible(true);
        Ext.getCmp("txtRecentlySalesDate").setVisible(true);
        Ext.getCmp("txtIntegration").setVisible(true);
        Ext.getCmp("txtMembershipShop").setVisible(true);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnHideText);
        document.getElementById("divTags").style.visibility = "visible";
        
        if (!loadTags && getUrlParam("TagsId").length > 0) {
            Ext.getCmp("txtTags").jitSetValue(getUrlParam("TagsId"));
            Ext.getCmp("txtTagsGroup").jitSetValue("3");
            fnAddGroup();
            loadTags = true;
        }
    } else {
        Ext.getCmp("searchPanel").isexpand = false;
        Ext.getCmp("txtVipLevel").setVisible(false);
        Ext.getCmp("txtStatus").setVisible(false);
        Ext.getCmp("txtTags").setVisible(false);
        Ext.getCmp("txtTagsGroup").setVisible(false);
        Ext.getCmp("btnAddGroup").setVisible(false);
        Ext.getCmp("txtRegistrationTime").setVisible(false);
        Ext.getCmp("txtRecentlySalesDate").setVisible(false);
        Ext.getCmp("txtIntegration").setVisible(false);
        Ext.getCmp("txtMembershipShop").setVisible(false);
        Ext.getCmp("btnMoreSearchView").setText(SearchPanelMoreBtnText); 
        document.getElementById("divTags").style.visibility = "hidden";
    }
}

fnAddGroup = function () {
    var tags = Ext.getCmp("txtTags").jitGetValue();
    var tagsText = Ext.getCmp("txtTags").rawValue;
    var tagsGroup = Ext.getCmp("txtTagsGroup").jitGetValue();
    var tagsGroupText = Ext.getCmp("txtTagsGroup").rawValue;
    if (tags == undefined || tags == null || tags == "") {
        alert("请选择标签");
        return;
    }
    for (var i = 0; i < tagsData.length; i++) {
        if (tagsData[i].tags == tags && (tagsGroup == undefined || tagsGroup == null || tagsGroup == "")) {
            alert("请选择组合关系");
            return;
        }
    }
    tagsData.push({ tags: tags, tagsGroup: tagsGroup,
        tagsText: tagsText, tagsGroupText: tagsGroupText
    });
    var str = "";
    for (var i = 0; i < tagsData.length; i++) {
        if (i == 0) {
            tagsData[i].tagsGroup = "3";
            str += str += "<div class=\"z_event_p1\">" +
                "<div class=\"z_event_p2\"><a href=\"#\" onclick=\"fnViewTags('" + tagsData[i].tags + "')\" style='text-decoration: underline; color:blue;'>" + tagsData[i].tagsText + "</a></div></div>";
        } else {
            str += "<div class=\"z_event_p1\"><div class=\"z_event_p3\">" + tagsData[i].tagsGroupText +
                "</div><div class=\"z_event_p2\"><a href=\"#\" onclick=\"fnViewTags('" + tagsData[i].tags + "')\" style='text-decoration: underline; color:blue;'>" + tagsData[i].tagsText + "</a></div></div>";
        }
    }
    get("txtAddedTags").innerHTML = str;
}


function fnViewTags(id) {
    if (id == undefined || id == null) id = "";
    //if (op == undefined || op == null) op = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "TagsEdit",
        title: "标签",
        url: "/module/basic/Tags/TagsEdit.aspx?TagsId=" + id
    });
    win.show();
}

function fnCancel() {
    get("txtAddedTags").innerHTML = "";
    tagsData = [];
}
