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

fnLoadMarketPerson = function () {
    var store = Ext.getStore("MarketPersonStore");
    store.proxy.url = JITPage.HandlerUrl.getValue() + "&method=get_person_list&MarketEventID=" +
        getUrlParam("MarketEventID");
    store.pageSize = 1000; //JITPage.PageSize.getValue();
    store.proxy.extraParams = {
        start: 0, limit: 0
    };
    store.load(function () { $(".wrap,.header").css("width", $(".wrap>table").eq(0).width()); });
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
    var ids = JITPage.getSelected({ gridView: grid, id: "MarketPersonID" });

    if (ids == undefined || ids == null || ids.length == 0) {
        showInfo("请选择记录");
        return;
    };

    for (var idObj in ids) {
        if (ids[idObj] != null && (ids[idObj]).toString().trim().length > 0) {
            var index = grid.store.find("MarketPersonID", (ids[idObj]).toString().trim());
            grid.store.removeAt(index);
            grid.store.commitChanges();
        }
    }
}
fnReset = function() {
    var grid = Ext.getCmp("gridMarketPerson");
    var _grid = Ext.getStore("MarketPersonStore");
    if (_grid.data.map != null) {
        for (var tmpItem in _grid.data.map) {
            var objData = _grid.data.map[tmpItem].data;
            var index = grid.store.find("MarketPersonID", (objData.MarketPersonID).toString().trim());

            grid.store.removeAt(index);
        }
        grid.store.commitChanges();
    }
}
var pnlImport = get("pnlImport");
fnImport = function() {
    pnlImport = get("pnlImport");
    if (pnlImport.style.display != "") {
        pnlImport.style.display = "";
    } else {
        pnlImport.style.display = "none";
    }
}
fnOpenImport = function(url, text) {
    pnlImport.style.display = "none";
    url += "?MarketEventID=" + getUrlParam("MarketEventID");
    var win = Ext.create('jit.biz.Window', {
        jitSize: "large",
        height: detailWinHeight + 50,
        id: "VipAdd",
        title: text,
        url: url
    });
	win.show();
}
fnSetSelectData = function(data) {
    var parentGrid = parent.Ext.getCmp("gridMarketPerson");
    if (data != null && data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            var index = parentGrid.store.find("VIPID", data[i].VIPID);
            if (index > -1 &&
                data[i].VIPID != null &&
                data[i].VIPID != "") {
                //parentGrid.store.removeAt(index);
                //parentGrid.store.insert(index, item);
                //parentGrid.store.commitChanges();
            } else {
                data[i].MarketPersonID = newGuid();
                //parentGrid.store.add(data[i]);
                parentGrid.store.insert(0, data[i]);
                parentGrid.store.commitChanges();
            }
        }
    }
}
fnSave = function() {
    var _grid = Ext.getStore("MarketPersonStore");
    var flag = false;

    var data = {};
    var MarketEventID = getUrlParam("MarketEventID");
    data.MarketEventID = MarketEventID;

    //if (data.BrandID == null || data.BrandID == "") {
    //    showError("请选择品牌");
    //    return;
    //}
    
    data.MarketPersonInfoList = [];
    if (_grid.data.map != null) {
        for (var tmpItem in _grid.data.map) {
            var objData = _grid.data.map[tmpItem].data;
            var objItem = {};
            objItem.MarketPersonID = objData.MarketPersonID;
            objItem.MarketEventID = objData.MarketEventID;
            objItem.VIPID = objData.VIPID;

            data.MarketPersonInfoList.push(objItem);
        }
    }

    Ext.Ajax.request({
        method: 'POST',
        sync: true,
        async: false,
        url: 'Handler/EventHandler.ashx?method=event_person_save&MarketEventID=' + MarketEventID,
        params: { "data": Ext.encode(data) },
        success: function (result, request) {
            var d = Ext.decode(result.responseText);
            if (d.success == false) {
                showError("保存数据失败：" + d.msg);
                flag = false;
            } else {
                //showSuccess("保存数据成功");
                flag = true;
                location.href = "MarketTemplate.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + MarketEventID;
            }
        },
        failure: function (result) {
            showError("保存数据失败：" + result.responseText);
        }
    });
}

fnPre = function() {
    fnSave();
    location.href = "MarketStore.aspx?mid=" + getUrlParam("mid") + "&MarketEventID=" + getUrlParam("MarketEventID");
}