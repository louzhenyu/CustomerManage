var tabs3;

Ext.onReady(function () {
    //加载需要的文件
    var myMask_info = "loading...";
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    //myMask.show();
    
    InitVE();
    InitStore();
    InitView();
    
    //页面加载
    JITPage.HandlerUrl.setValue("Handler/WApplicationHandler.ashx?mid="); 

    //fnSearch();

    Ext.getCmp("gv2").getStore().on('load', setTdCls); //设置表格加载数据完毕后，更改表格TD样式为垂直居中  
    Ext.getCmp("gv3").getStore().on('load', setTdCls2); //设置表格加载数据完毕后，更改表格TD样式为垂直居中  
    
    Ext.getCmp("tabs3").setActiveTab(2);

    Ext.Ajax.request({
        url: "/Framework/Javascript/Biz/Handler/WApplicationInterfaceHandler.ashx?method=get_list",
        params: { },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText).data;
            if (d != null && d.length > 0) {
                Ext.getCmp("txtApplicationId").jitSetValue(d[0].ApplicationId);
                Ext.Ajax.request({
                    url: "/Framework/Javascript/Biz/Handler/WModelHandler.ashx?method=get_list",
                    params: { pid: d[0].ApplicationId },
                    method: 'POST',
                    sync: true,
                    async: false,
                    success: function (response) {
                        var d = Ext.decode(response.responseText).data;
                        if (d != null && d.length > 0) {
                            Ext.getCmp("txtWModel").store.load({ 
                            params: { pid: Ext.getCmp("txtApplicationId").jitGetValue() }
                                ,callback: function(r, options, success) {
                                    Ext.getCmp("txtWModel").jitSetValue(d[0].ModelId);
                                    fnSearch();
                                }
                            }); 
                        }
                    },
                    failure: function () {
                        Ext.Msg.alert("提示", "获取参数数据失败");
                    }
                });
            }
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
        }
    });

});

function setTdCls() {
    var gridJglb = document.getElementById("grid2");
    var tables = gridJglb.getElementsByTagName("table"); //找到每个表格  
    for (var k = 0; k < tables.length; k++) {
        var tableV = tables[k];
        if (tableV.className == "x-grid-table x-grid-table-resizer") {
            var trs = tables[k].getElementsByTagName("tr"); //找到每个tr  
            for (var i = 0; i < trs.length; i++) {
                var tds = trs[i].getElementsByTagName("td"); //找到每个TD  
                for (var j = 1; j < tds.length; j++) {
                    tds[j].style.cssText = "width:202px;text-align:center;line-height:130px;vertical-align:middle;";
                }
            }
        };
    }
}

function setTdCls2() {
    var gridJglb = document.getElementById("grid3");
    var tables = gridJglb.getElementsByTagName("table"); //找到每个表格  
    for (var k = 0; k < tables.length; k++) {
        var tableV = tables[k];
        if (tableV.className == "x-grid-table x-grid-table-resizer") {
            var trs = tables[k].getElementsByTagName("tr"); //找到每个tr  
            for (var i = 0; i < trs.length; i++) {
                var tds = trs[i].getElementsByTagName("td"); //找到每个TD  
                for (var j = 1; j < tds.length; j++) {
                    tds[j].style.cssText = "width:202px;text-align:center;line-height:130px;vertical-align:middle;";
                }
            }
        };
    }
} 

fnSearch = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请选择微信账号");
        return;
    }
    var WModelId = Ext.getCmp("txtWModel").getValue();
    if (WModelId == null || WModelId == "") {
        alert("请选择模块");
        return;
    }
    
    Ext.getCmp("btnSearch").setText("查询中...");
    Ext.getCmp("btnSearch").disable(false);
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=get_wmodel_by_id",
        params: { ModelId: WModelId },
        method: 'POST',
        sync: true,
        async: false,
        success: function (response) {
            var d = Ext.decode(response.responseText).data;
            get("MaterialId").value = getStr(d.MaterialId);

            fnLoadItems1();
            fnLoadItems2();
            fnLoadItems3();
            fnLoadItems4();

            Ext.getCmp("btnSearch").setText("查询");
            Ext.getCmp("btnSearch").enable(true);
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
            Ext.getCmp("btnSearch").setText("查询");
        }
    });
}
fnReset = function() {
    
}

fnLoadItems1 = function() {
    var WModelId = Ext.getCmp("txtWModel").getValue();
    var MaterialId = get("MaterialId").value;
    var store = Ext.getStore("WMenuAddItem1Store");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_items1&Id=" + WModelId;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            if (records == null) return;
            for (var i = 0; i < records.length; i++) {
                if (records[i].data.WritingId == MaterialId) {
                    Ext.getCmp("gv1").getSelectionModel().select(i, true);
                }
            }
        }
    });
}
fnLoadItems2 = function() {
    var WModelId = Ext.getCmp("txtWModel").getValue();
    var MaterialId = get("MaterialId").value;
    var store = Ext.getStore("WMenuAddItem2Store");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_items2&Id=" + WModelId;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            if (records == null) return;
            for (var i = 0; i < records.length; i++) {
                if (records[i].data.ImageId == MaterialId) {
                    Ext.getCmp("gv2").getSelectionModel().select(i, true);
                }
            }
        }
    });
}
fnLoadItems3 = function() {
    var WModelId = Ext.getCmp("txtWModel").getValue();
    var MaterialId = get("MaterialId").value;
    //if (WModelId == undefined || WModelId == null || WModelId == "") return;
    var store = Ext.getStore("WMenuAddItem3Store");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_items3&Id=" + WModelId;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            if (records == null) return;
            for (var i = 0; i < records.length; i++) {
                if (records[i].data.TextId == MaterialId) {
                    Ext.getCmp("gv3").getSelectionModel().select(i, true);
                }
            }
        }
    });
}
fnLoadItems4 = function() {
    var WModelId = Ext.getCmp("txtWModel").getValue();
    var MaterialId = get("MaterialId").value;
    var store = Ext.getStore("WMenuAddItem4Store");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_items4&Id=" + WModelId;
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0 
    };
    store.load({
        scope:this,
        callback:function(records, operation, success){
            if (records == null) return;
            for (var i = 0; i < records.length; i++) {
                if (records[i].data.VoiceId == MaterialId) {
                    Ext.getCmp("gv4").getSelectionModel().select(i, true);
                }
            }
        }
    });
}

function fnSave() {
    var flag;
    var item = {};
    item.ModelId = Ext.getCmp("txtWModel").getValue();

    if (item.ModelId == null || item.ModelId == "") {
        alert("请先选择模块");
        return;
    }

    var activeTab = Ext.getCmp("tabs3").getActiveTab();
    var typeId = Ext.getCmp("tabs3").items.findIndex('id', activeTab.id) + 1;
    switch (typeId) {
        case 1:
            var list = Ext.getCmp('gv1').getSelectionModel().getSelection();
            if (list == null || list.length == 0) {
                alert("请选择素材");
                return;
            }
            item.MaterialTypeId = typeId;
            item.MaterialId = list[0].data.WritingId;
            break;
        case 2:
            var list = Ext.getCmp('gv2').getSelectionModel().getSelection();
            if (list == null || list.length == 0) {
                alert("请选择素材");
                return;
            }
            item.MaterialTypeId = typeId;
            item.MaterialId = list[0].data.ImageId;
            break;
        case 3:
            var list = Ext.getCmp('gv3').getSelectionModel().getSelection();
            if (list == null || list.length == 0) {
                alert("请选择素材");
                return;
            }
            else if (list.length > 10) {
                alert("最多只能选择10条数据");
                return;
            }
            item.MaterialTypeId = typeId;
            item.MaterialId = list[0].data.TextId;
            item.MaterialTextList = [];

            for (var i = 0; i < list.length; i++) {
                item.MaterialTextList.push(list[i].data);
            }

            break;
        case 4:
            var list = Ext.getCmp('gv4').getSelectionModel().getSelection();
            if (list == null || list.length == 0) {
                alert("请选择素材");
                return;
            }
            item.MaterialTypeId = typeId;
            item.MaterialId = list[0].data.VoiceId;
            break;
        case 5:
            break;
    }
    
    if (item.MaterialTypeId == null || item.MaterialTypeId == "") {
        alert("请选择素材");
        return;
    }
    if (item.MaterialId == null || item.MaterialId == "") {
        alert("请选择素材");
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: '/Module/WApplication/Handler/WApplicationHandler.ashx?method=wmodel_save&ModelId=' + item.ModelId, 
        params: {
            "item": Ext.encode(item)
        },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                showSuccess("保存数据成功");
                flag = true;
                fnSearch();
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

fnAddItem1 = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var ModelId = Ext.getCmp("txtWModel").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请先选择微信账号");
        return;
    }
    if (ModelId == null || ModelId == "") {
        alert("请先选择模块");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialWritingEdit",
        title: "素材",
        url: "WMaterialWritingEdit.aspx?ModelId=" + ModelId + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
fnAddItem2 = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var ModelId = Ext.getCmp("txtWModel").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请先选择微信账号");
        return;
    }
    if (ModelId == null || ModelId == "") {
        alert("请先选择模块");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialImageEdit",
        title: "素材",
        url: "WMaterialImageEdit.aspx?ModelId=" + ModelId + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
fnAddItem3 = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var ModelId = Ext.getCmp("txtWModel").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请先选择微信账号");
        return;
    }
    if (ModelId == null || ModelId == "") {
        alert("请先选择模块");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialTextEdit",
        title: "素材",
        url: "MaterialTextEdit.aspx?ModelId=" + ModelId + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
fnAddItem4 = function() {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var ModelId = Ext.getCmp("txtWModel").getValue();
    if (ApplicationId == null || ApplicationId == "") {
        alert("请先选择微信账号");
        return;
    }
    if (ModelId == null || ModelId == "") {
        alert("请先选择模块");
        return;
    }
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialVoiceEdit",
        title: "素材",
        url: "WMaterialVoiceEdit.aspx?ModelId=" + ModelId + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
fnAddItem5 = function() {
    alert("123");
}

function fnView1(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialWritingEdit",
        title: "素材",
        url: "WMaterialWritingEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView2(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialImageEdit",
        title: "素材",
        url: "WMaterialImageEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView3(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialTextEdit",
        title: "素材",
        url: "MaterialTextEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView4(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialVoiceEdit",
        title: "素材",
        url: "WMaterialVoiceEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}
function fnView5(id) {
    var ApplicationId = Ext.getCmp("txtApplicationId").jitGetValue();
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight,
        id: "WMaterialTextEdit",
        title: "素材",
        url: "MaterialTextEdit.aspx?Id=" + id + "&ApplicationId=" + ApplicationId 
    });
	win.show();
}


function fnDelete1(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv1"), id: "WritingId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialWriting_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv1"), id: "WritingId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem1Store").load();
        }
    });
}
function fnDelete2(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv2"), id: "ImageId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialImage_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv2"), id: "ImageId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem2Store").load();
        }
    });
}
function fnDelete3(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv3"), id: "TextId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialText_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv3"), id: "TextId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem3Store").load();
        }
    });
}
function fnDelete4(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv4"), id: "VoiceId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialVoice_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv4"), id: "VoiceId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem4Store").load();
        }
    });
}
function fnDelete5(id) {
    JITPage.deleteList({
        ids: JITPage.getSelected({ gridView: Ext.getCmp("gv5"), id: "TextId" }),
        url: JITPage.HandlerUrl.getValue() + "&method=WMaterialVoice_delete",
        params: {
            ids: JITPage.getSelected({ gridView: Ext.getCmp("gv5"), id: "TextId" })
        },
        handler: function () {
            Ext.getStore("WMenuAddItem5Store").load();
        }
    });
}

