
var updateStore;
var selModel;
var gridCloumn = [];
var gridStoreList;

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

fnLoad = function() {
    //清空Model数据
    selModel.jitClearValue();

    var MarketEventID = getUrlParam("MarketEventID");
    
    fnLoadMarketPerson();

    if (MarketEventID != "null" && MarketEventID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_event_by_id",
            params: { MarketEventID: MarketEventID },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                
                //Ext.getCmp("txtBeginDate").setValue(d.BeginTime);
                //Ext.getCmp("txtEndDate").setValue(d.EndTime);

                //fnLoadStore();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
}

fnLoadMarketPerson = function() { 
    var tags = "";
    for (var i = 0; i < tagsData.length; i++) {
        tags += ";" + tagsData[i].tags + "," + (tagsData[i].tagsGroup == undefined ? "3" : tagsData[i].tagsGroup);
    }

    //if (tags.length > 0) {
    //    if (Ext.getCmp("txtUserName").getValue() == null || Ext.getCmp("txtUserName").getValue().length == 0) {
    //        alert("请输入姓名");
    //        return;
    //    }
    //    if (Ext.getCmp("txtGender").getValue() == null || Ext.getCmp("txtGender").getValue().length == 0) {
    //        alert("请选择性别");
    //        return;
    //    }
    //}

    var store = Ext.getStore("VipStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_vip_list&MarketEventID=" + 
        getUrlParam("MarketEventID");
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0
        ,EventId: getUrlParam("MarketEventID")
        ,Gender: Ext.getCmp("txtGender").getValue()
        ,UserName: Ext.getCmp("txtUserName").getValue()
        ,Tags: tags
        //,Enterprice: Ext.getCmp("txtCompany").getValue()
        //,IsChainStores: (Ext.getCmp("txtUnitSizeType").getValue())
        //,IsWeiXinMarketing: Ext.getCmp("txtIsWeiXinMarketing").getRawValue()
    };
    Ext.getCmp('gridMarketPerson').getStore().getProxy().startParam = 0;
    Ext.getCmp('pageBar').moveFirst();
    //store.load();
}

fnGetChkValue = function() {
    var chk1 = Ext.getCmp("chk1");
    var chk2 = Ext.getCmp("chk2");
    if (chk1.getValue()) return "男";
    else if (chk2.getValue()) return "女";
    return "";
}
fnGetMultiUnitValue = function() {
    var chk1 = Ext.getCmp("chkYes1");
    var chk2 = Ext.getCmp("chkYes2");
    if (chk1.getValue()) return "是";
    else if (chk2.getValue()) return "否";
    return "";
}
fnAddGroup = function() {
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
    tagsData.push({tags:tags, tagsGroup:tagsGroup, 
        tagsText:tagsText, tagsGroupText:tagsGroupText});
    var str = "";
    for (var i = 0; i < tagsData.length; i++) {
        if (i == 0) {
            tagsData[i].tagsGroup = "3";
            str += str += "<div class=\"z_event_p1\">" + 
                "<div class=\"z_event_p2\">" + tagsData[i].tagsText + "</div></div>";
        } else {
            //if (tagsData[i].tagsGroup == undefined || tagsData[i].tagsGroup == null || tagsData[i].tagsGroup == "") {
            //    alert("请选择组合关系");
            //    tagsData.pop(tagsData.length-1);
            //    return;
            //}
            str += "<div class=\"z_event_p1\"><div class=\"z_event_p3\">" + tagsData[i].tagsGroupText + 
                "</div><div class=\"z_event_p2\">" + tagsData[i].tagsText + "</div></div>";
        }
    }
    get("txtAddedTags").innerHTML = str;
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
    var grid = Ext.getCmp("gridMarketPerson");
    var ids = JITPage.getSelected({ gridView: grid, id: "VIPID" });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择记录");
        return;
    };

    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("VIPID", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
}
fnReset = function() {

}
fnSearchReset = function() {
    tagsData = [];
    get("txtAddedTags").innerHTML = tagsStr = "";
}
fnSave = function() {
    var flag = false;
    var data = {};
    var MarketEventID = getUrlParam("MarketEventID");
    data.MarketEventID = MarketEventID;

    //if (data.BrandID == null || data.BrandID == "") {
    //    showError("请选择品牌");
    //    return;
    //}
    
    //var d = Ext.getCmp('gridMarketPerson').getSelectionModel().getSelection();
    data.vips = [];
    //if (d != null) {
    //    for (var i = 0; i < d.length; i++) {
    //        var objData = d[i].data;
    //        var objItem = {};
    //        objItem.VIPID = objData.VIPID;

    //        data.vips.push(objData);
    //    }
    //}

    
    //获取选中行
    var v = selModel.jitGetValue(); 
    if (updateStore.getCount() > 0) {
        for (var i = 0; i < updateStore.getCount(); i++) {
            if (updateStore.data.items[i].data.MappingID == "" && updateStore.data.items[i].data.IsDelete == 1) {
            }
            else {
                data.vips.push(updateStore.data.items[i].data);
            }
        }
    }

    parent.fnSetSelectData(data.vips);

    //Ext.Ajax.request({
    //    method: 'POST',
    //    sync: true,
    //    async: false,
    //    url: 'Handler/EventHandler.ashx?method=event_person_save&MarketEventID=' + MarketEventID,
    //    params: { "data": Ext.encode(data) },
    //    success: function (result, request) {
    //        var d = Ext.decode(result.responseText);
    //        if (d.success == false) {
    //            showError("保存数据失败：" + d.msg);
    //            flag = false;
    //        } else {
    //            //showSuccess("保存数据成功");
    //            flag = true;
    //        }
    //    },
    //    failure: function (result) {
    //        showError("保存数据失败：" + result.responseText);
    //    }
    //});
    //if (flag) fnCloseWin();
    fnCloseWin();
}
function fnCloseWin() {
    CloseWin('VipAdd');
}


//selmodel相关
var selModelObject = {
    deselect: function (a, b, c) {
        var r = updateStore.getById(b.data.VIPID);
        if (r != null && r != undefined) {
        }
    },
    select: function (a, b, c) { //console.log(b.data.VIPID);
        if (a.checkOnly == true) {
            if (updateStore.getById(b.data.VIPID) == null) {
                updateStore.insert(0, b.data);
            } else {
                if (updateStore.getById(b.data.VIPID).data.IsDelete = 1) {
                    updateStore.getById(b.data.VIPID).data.IsDelete = 0;
                }
            }
        }
    }
};


/*
*grid翻页赋值操作
*/
function fnRenderPage() {
    //alert(1);
    if (gridStoreList == undefined) {
        return;
    }

    //修改选中数据
    for (var i = 0; i < gridStoreList.getStore().getCount(); i++) {
        if (updateStore.getById(gridStoreList.getStore().data.items[i].data.VIPID) != null && 
            updateStore.getById(gridStoreList.getStore().data.items[i].data.VIPID).data.IsDelete == 0) {

            //gridStoreList.getStore().getAt(i).set("ParameterOrder", 
            //    updateStore.getById(gridStoreList.getStore().data.items[i].data.VIPID).data.ParameterOrder);

        }
    }
}