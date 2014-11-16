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
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid=");

    var typeId = getUrlParam("typeId");
    if (typeId == undefined || typeId == null || typeId == "" || typeId.length > 1) 
        typeId = "1";

    Ext.getCmp("txtMaterialTypeId").setDefaultValue(typeId);

    fnSearch();

    if (typeId == "1") {
        var tmpText = parent.Ext.getCmp("txtMaterialTypeName").getValue();
        tmpText = tmpText.substring(tmpText.indexOf("文本: ") + 4);
        Ext.getCmp("DivGridView1").setValue(tmpText);
    }
});

function fnCreate() {
    var typeId = Ext.getCmp("txtMaterialTypeId").value;
    switch (typeId) {
        case "2": 
            location.href = "WMaterialImageEdit.aspx?op=" + getUrlParam("op") + 
                "&typeId=" + typeId + "&value=" + getUrlParam("value") + "";
            break;
        case "3": 
            location.href = "MaterialTextEdit.aspx?op=" + getUrlParam("op") + 
                "&typeId=" + typeId + "&value=" + getUrlParam("value") + "";
            break;
        case "4": 
            location.href = "WMaterialVoiceEdit.aspx?op=" + getUrlParam("op") + 
                "&typeId=" + typeId + "&value=" + getUrlParam("value") + "";
            break;
    }
}

fnSearch = function() {
    get("txtDiv").style.display = "";
    get("DivGridView1").style.display = "none";
    get("DivGridView2").style.display = "none";
    get("DivGridView3").style.display = "none";
    get("DivGridView4").style.display = "none";
    get("DivGridView5").style.display = "none";
    Ext.getCmp("span_create").hide(true);

    var typeId = Ext.getCmp("txtMaterialTypeId").value;
    if (typeId == undefined || typeId == null || typeId == "" || typeId.length > 1) {
        return;
    }
    
    get("txtDiv").style.display = "none";
    get("DivGridView" + typeId).style.display = "";
        
    switch (typeId) {
        case "2": 
            Ext.getCmp("span_create").show(true);
            break;
        case "3": 
            Ext.getCmp("span_create").show(true);
            break;
        case "4": 
            Ext.getCmp("span_create").show(true);
            break;
    }

    var store = Ext.getStore("WMenuAddItem" + typeId + "Store");
    store.proxy.url = JITPage.HandlerUrl.getValue()
        + "&method=search_wmenu_items_" + typeId;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        //form: Ext.JSON.encode(Ext.getCmp("searchPanel").getValues())
    };
    store.load();
}

function fnView(id) {
    if (id == undefined || id == null) id = "";
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMenuEdit",
        title: "菜单",
        url: "WMenuEdit.aspx?WMenuId=" + id
    });
	win.show();
}
function fnView2(id) {
    var typeId = Ext.getCmp("txtMaterialTypeId").value;
    location.href = "WMaterialImageEdit.aspx?Id=" + id + "&op=" + getUrlParam("op") + 
        "&typeId=" + typeId + "&value=" + getUrlParam("value") + "";
}
function fnView3(id) {
    var typeId = Ext.getCmp("txtMaterialTypeId").value;
    location.href = "MaterialTextEdit.aspx?Id=" + id + "&op=" + getUrlParam("op") + 
        "&typeId=" + typeId + "&value=" + getUrlParam("value") + "";
}
function fnView4(id) {
    var typeId = Ext.getCmp("txtMaterialTypeId").value;
    location.href = "WMaterialVoiceEdit.aspx?Id=" + id + "&op=" + getUrlParam("op") + 
        "&typeId=" + typeId + "&value=" + getUrlParam("value") + "";
}
function fnDelete(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ID" }),
        url: JITPage.HandlerUrl.getValue() + "&method=wmenu_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "ID" })
        },
        handler: function () {
            Ext.getStore("wMenuStore").load();
        }
    });
}
function fnDelete2(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView2"), id: "ImageId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialImage_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView2"), id: "ImageId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem2Store").load();
        }
    });
}
function fnDelete3(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView3"), id: "TextId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialText_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView3"), id: "TextId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem3Store").load();
        }
    });
}
function fnDelete4(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView4"), id: "VoiceId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialVoice_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView4"), id: "VoiceId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem4Store").load();
        }
    });
}
function fnDelete5(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView5"), id: "TextId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialVoice_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gridView5"), id: "TextId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem5Store").load();
        }
    });
}

function fnSave() {
    var typeId = Ext.getCmp("txtMaterialTypeId").value;
    if (typeId == undefined || typeId == null || typeId == "" || typeId.length > 1) {
        return;
    }
    var pType = parent.document.getElementById("hMaterialType");
    var txtMaterialTypeName = parent.Ext.getCmp("txtMaterialTypeName");
    var txtMaterialTypeId = parent.get("txtMaterialTypeId");
    var data = "";
    
    var titlePrefix = "";
    if (typeId != "1") {
        var list = Ext.getCmp("gridView" + typeId).getSelectionModel().getSelection();
        if (list != null && list.length > 0) {
            switch (typeId) {
                case "2" :
                    titlePrefix = "图片: ";
                    data = list[0].data.TextId;
                    txtMaterialTypeName.setValue(titlePrefix + list[0].data.Title);
                    break;
                case "3" :
                    titlePrefix = "图文: ";
                    data = list[0].data.TextId;
                    txtMaterialTypeName.setValue(titlePrefix + list[0].data.Title);
                    break;
                case "4" :
                    titlePrefix = "语音: ";
                    data = list[0].data.TextId;
                    txtMaterialTypeName.setValue(titlePrefix + list[0].data.Title);
                    break;
                case "5" :
                    titlePrefix = "视频: ";
                    data = list[0].data.TextId;
                    txtMaterialTypeName.setValue(titlePrefix + list[0].data.Title);
                    break;
            }
        } else {
            txtMaterialTypeId.value = "";
            pType.value = "";
            txtMaterialTypeName.setValue("");
            fnClose();
            return;
        }
    } else {
        titlePrefix = "文本: ";
        data = Ext.getCmp("DivGridView1").getValue();
        txtMaterialTypeName.setValue(titlePrefix + data);
    }
    txtMaterialTypeId.value = typeId;
    pType.value = data;
    fnClose();
}
function fnClose() {
    CloseWin('WMenuAddItem');
}

