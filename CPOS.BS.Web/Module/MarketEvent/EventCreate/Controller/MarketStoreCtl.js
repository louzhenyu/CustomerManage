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
    var MarketEventID = getUrlParam("MarketEventID");
    
    fnLoadStore();

    if (MarketEventID != "null" && MarketEventID != "") {
        Ext.Ajax.request({
            url: JITPage.HandlerUrl.getValue() + "&method=get_event_by_id",
            params: { MarketEventID: MarketEventID },
            method: 'POST',
            success: function (response) {
                var d = Ext.decode(response.responseText).data;
                
                //Ext.getCmp("txtBeginDate").setValue(d.BeginTime);
                //Ext.getCmp("txtEndDate").setValue(d.EndTime);
                $(".wrap,.header").css("width", $(".wrap>table").eq(0).width());
                //fnLoadStore();
            },
            failure: function () {
                Ext.Msg.alert("提示", "获取参数数据失败");
            }
        });
    }
}

fnLoadStore = function () {
    var store = Ext.getStore("MarketStoreStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_store_list&MarketEventID=" +
        getUrlParam("MarketEventID");
    store.pageSize = JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0
    };
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width());});
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
    var grid = Ext.getCmp("gridStore");
    var ids = JITPage.getSelected({ gridView: grid, id: "MarketStoreID" });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择记录");
        return;
    };

    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("MarketStoreID", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
}
fnReset = function() {
    var grid = Ext.getCmp("gridStore");
    var _grid = Ext.getStore("MarketStoreStore");
    if (_grid.data.map != null) {
        for (var tmpItem in _grid.data.map) {
            var objData = _grid.data.map[tmpItem].data;
            var index = grid.store.find("MarketStoreID", (objData.MarketStoreID).toString().trim());

            grid.store.removeAt(index);
        }
        grid.store.commitChanges();
    }
}
fnSave = function() {
    var _grid = Ext.getStore("MarketStoreStore");
    var flag = false;

    var data = {};
    var MarketEventID = getUrlParam("MarketEventID");
    data.MarketEventID = MarketEventID;
    //data.BeginTime = Ext.getCmp("txtBeginDate").jitGetValueText();
    //data.EndTime = Ext.getCmp("txtEndDate").jitGetValueText();

    //if (data.BrandID == null || data.BrandID == "") {
    //    showError("请选择品牌");
    //    return;
    //}
    
    data.MarketStoreInfoList = [];
    if (_grid.data.map != null) {
        for (var tmpItem in _grid.data.map) {
            var objData = _grid.data.map[tmpItem].data;
            var objItem = {};
            objItem.StoreID = objData.StoreID;
            objItem.StoreName = objData.StoreName;
            objItem.MarketEventID = MarketEventID;

            data.MarketStoreInfoList.push(objItem);
        }
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventHandler.ashx?method=event_store_save&MarketEventID=' + MarketEventID,
        params: { "data": Ext.encode(data) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                //showSuccess("保存数据成功");
                flag = true;
                location.href = "MarketPerson.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + MarketEventID;
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });

}

fnMapAreaClose = function() {
    document.getElementById('mapArea').style.display='none';
    document.getElementById('mapAreaClose').style.display='none';
    
    var parentGrid = parent.Ext.getCmp("gridStore");
    var selectStores = get("selectStores");
    var ids = selectStores.value + ",";
    if (mapStoreData != null && ids.length > 0) {
        for (var i = 0; i < mapStoreData.length; i++) {
            if (ids.indexOf("," + mapStoreData[i].StoreID + ",") > -1) {
                var index = parentGrid.store.find("StoreID", mapStoreData[i].StoreID);
                if (index > -1 &&
                    mapStoreData[i].StoreID != null &&
                    mapStoreData[i].StoreID != "") {
                    //parentGrid.store.removeAt(index);
                    //parentGrid.store.insert(index, item);
                    //parentGrid.store.commitChanges();
                } else {
                    mapStoreData[i].MarketStoreID = newGuid();
                    parentGrid.store.add(mapStoreData[i]);
                    parentGrid.store.commitChanges();
                }
            }
        }
    }
}
var mapStoreData = null;
fnLoadMapStores = function() {
    var storesAll = "[";
    Ext.Ajax.request({
        url: "Handler/EventHandler.ashx?method=get_default_store_list",
        params: { page:1 },
        method: 'POST',
        sync: true,
        async : false,
        success: function (response) {
            var d = Ext.decode(response.responseText).topics;
            mapStoreData = d;
            for (var i = 0 ; i < d.length; i++) {
                var item = d[i];
                if (storesAll.length > 2) storesAll += ",";
                storesAll += '{"StoreID":"'+ item.StoreID +'","LabelID":"'+ item.StoreID +
                    '","Lng":"'+ item.Longitude +'","Lat":"'+ item.Latitude +
                    '","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"'+ 
                    item.StoreName + "：" + item.Address +
                    '","PopInfoWidth":"200","PopInfoHeight":"50","PopInfo":[{"type":"1","title":"'+ 
                    item.StoreName +'","value":"'+ item.Address +'"}]}';
            }
        },
        failure: function (response) {
            showError("错误：" + response.responseText);
        }
    });
    storesAll += "]";
    return storesAll;
}

fnPre = function() {
    fnSave();
    location.href = "EventTime.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}
