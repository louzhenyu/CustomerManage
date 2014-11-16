var id = JITMethod.getUrlParam("id");
var btncode = JITMethod.getUrlParam("btncode");
var urlparams = window.location.search;
var pageLanguage = new Object();

var pnlSearch; //查询pannel
var pnlWork; //操作pannel

var btnAdd; //增加
var gridStoreList; //终端数据表

var editMethod;

var callBack = {
    saveCallBack: function () { }
};


//选择终端 新增
function _map_Graphic_OnClick(obj) {
    var storeinfo = fnGetStoreInfoByMapStoreID(obj.StoreID);
    if (Ext.getStore("storeStore").getById(storeinfo.StoreID) == null) {
        var r = Ext.create('storeEntity', {
            MappingID: storeinfo.MappingID,
            MapStoreID: storeinfo.MapStoreID,
            StoreID: storeinfo.StoreID,
            StoreName: storeinfo.StoreName,
            Coordinate: storeinfo.Coordinate
        });
        Ext.getStore("storeStore").insert(Ext.getStore("storeStore").getCount(), r);

        renderLine();
    }
    //从删除列表排除
    if (storeinfo.MappingID != "") {
        for (var i = 0; i < deleteList.length; i++) {
            if (deleteList[i] == storeinfo.MappingID) {
                deleteList[i] = "";
            }
        }
    }
}

var storeList = new Array(); //做关系映射用，用户 地图  与 gridstore  id 之间的对接
function fnInitMap() {

    //grid未加载完  或者  map未加载完 均返回，等双方均加载完成后再执行函数
    if (window.frames["frmFlashMap"].index != undefined && window.frames["frmFlashMap"].index._map_RemoveStores != undefined && gridStoreList != undefined) {

        window.frames["frmFlashMap"].index._map_RemoveStores("");
        window.frames["frmFlashMap"].index._map_ClearLines("");
        deleteList = new Array();
        Ext.getStore("storeStore").removeAll();

        var PointArray = new Array();
        storeList = new Array();
        for (var i = 0; i < gridStoreList.getStore().getCount(); i++) {
            var r = gridStoreList.getStore().data.items[i];
            var Point = new Object();
            Point.StoreID = i;
            Point.Lng = r.data.Coordinate.split(",")[0];
            Point.Lat = r.data.Coordinate.split(",")[1];
            Point.Icon = "os.png";
            Point.IsAssigned = "true";  //默认为true
            Point.IsEdit = false;
            //                Point.LabelID = (i + 1).toString();
            //Point.LabelContent = r.data.StoreName;
            Point.Tips = r.data.StoreName;

            PointArray.push(Point);

            //grid数据
            if (r.data.MappingID != "") {
                if (Ext.getStore("storeStore").getById(r.data.StoreID) == null) {
                    var entity = Ext.create('storeEntity', {
                        MappingID: r.data.MappingID,
                        MapStoreID: i,
                        StoreID: r.data.StoreID,
                        StoreName: r.data.StoreName,
                        Coordinate: r.data.Coordinate,
                        Sequence: r.data.Sequence
                    });
                    Ext.getStore("storeStore").insert(Ext.getStore("storeStore").getCount(), entity);
                }
            }
            //所有门店数据，存在内存
            storeList.push({
                "MappingID": r.data.MappingID,
                "MapStoreID": i.toString(),
                "StoreID": r.data.StoreID,
                "StoreName": r.data.StoreName,
                "Coordinate": r.data.Coordinate,
                "Sequence": r.data.Sequence
            });
        }
        window.frames["frmFlashMap"].index._map_AddStores(Ext.JSON.encode(PointArray), true);
        renderLine();

//        if (Ext.getStore("storeStore").getCount() > 1) {
            window.frames["frmFlashMap"].index._map_MoveToStores();
//        }
//        else {
//            window.frames["frmFlashMap"].index._map_MoveToStores();
//        }
    }
}

function renderLine() {
    window.frames["frmFlashMap"].index._map_ClearLines("1");
    var points = new Array();

    for (var i = 0; i < Ext.getStore("storeStore").getCount(); i++) {
        Ext.getStore("storeStore").data.items[i].data.Sequence = (i + 1);

        var r = Ext.getStore("storeStore").data.items[i];
        var Point = new Object();
        Point.StoreID = r.data.MapStoreID;
        Point.Lng = r.data.Coordinate.split(",")[0];
        Point.Lat = r.data.Coordinate.split(",")[1];
        Point.Icon = "g.png";
        Point.IsAssigned = "true";  //默认为true
        Point.IsEdit = false;
        Point.LabelID = (i + 1).toString();
        //        Point.LabelContent = r.data.StoreName;
        Point.Tips = r.data.StoreName;

        points.push(Point);
    }

    Ext.getCmp("gridView").reconfigure(Ext.getStore("storeStore"));
    window.frames["frmFlashMap"].index._map_RemoveStores(Ext.JSON.encode(points));
    window.frames["frmFlashMap"].index._map_AddStores(Ext.JSON.encode(points), true);
    window.frames["frmFlashMap"].index._map_CreateLine("1", Ext.JSON.encode(points), 'line', 'true', false, 'none', '1');
}

//现在已经废除 by zhongbao.xiao 2013.5.27
var deleteList = new Array();
function fnDelete() {
    var sm = Ext.getCmp("gridView").getSelectionModel();

    //重画一个点
    var r = Ext.getCmp("gridView").getSelectionModel().getSelection()[0];
    //alert(r.data.MapStoreID);
    var points = new Array();
    var Point = new Object();
    Point.StoreID = r.data.MapStoreID;
    Point.Lng = r.data.Coordinate.split(",")[0];
    Point.Lat = r.data.Coordinate.split(",")[1];
    Point.Icon = "os.png";
    Point.IsAssigned = "true";  //默认为true
    Point.IsEdit = false;
    Point.LabelID = "";
    Point.LabelContent = r.data.StoreName;
    Point.Tips = "";
    points.push(Point);
    window.frames["frmFlashMap"].index._map_RemoveStores(Ext.JSON.encode(points));
    window.frames["frmFlashMap"].index._map_AddStores(Ext.JSON.encode(points), true);

    //删除grid里的点，并记录删除的记录
    Ext.getStore("storeStore").remove(sm.getSelection());
    if (r.data.MappingID != "") {
        deleteList.push(r.data.MappingID);
    }
    if (Ext.getStore("storeStore").getCount() > 0) {
        sm.select(Ext.getStore("storeStore").getCount() - 1);
    }

    //渲染线路
    renderLine();
}

//通过地图storeid 从内存获取 store信息
function fnGetStoreInfoByMapStoreID(mapstoreid) {
    for (var i = 0; i < storeList.length; i++) {
        if (storeList[i].MapStoreID == mapstoreid) {
            return storeList[i];
        }
    }
}

//路线优化
function fnRouteAdjust() {
    try {
        var dat = new Array();

        for (var i = 0; i < Ext.getStore("storeStore").getCount(); i++) {
            var r = Ext.getStore("storeStore").data.items[i];

            var Point = new Object();
            Point.StoreID = r.data.StoreID;
            Point.StoreName = r.data.StoreName;
            Point.Coordinate = r.data.Coordinate;
            dat.push(Point);
        }
        var result = getRoute(dat);
        if (result != null && result.length > 0) {
            for (var i = 0; i < result.length; i++) {
                var rID = result[i].StoreID;
                var row = Ext.getStore("storeStore").getById(rID);
                row.data.Sequence = (i + 1);
            }
        }
        Ext.getStore("storeStore").sort({ property: 'Sequence', direction: 'ASC' });
        renderLine();
    }
    catch (e) {
        //alert(e);
    }
}

var EARTH_RADIUS = 6378137.0; //单位M 
var PI = Math.PI;
Array.prototype.remove = function (dx) {
    if (isNaN(dx) || dx > this.length) { return false; }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[i] != this[dx]) {
            this[n++] = this[i]
        }
    }
    this.length -= 1
}
function getRad(d) {
    return d * PI / 180.0;
}

/** 
* caculate the great circle distance 
* @param {Object} lat1 
* @param {Object} lng1 
* @param {Object} lat2 
* @param {Object} lng2 
*/
function getGreatCircleDistance(lat1, lng1, lat2, lng2) {
    var radLat1 = getRad(lat1);
    var radLat2 = getRad(lat2);

    var a = radLat1 - radLat2;
    var b = getRad(lng1) - getRad(lng2);

    var s = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a / 2), 2) + Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(b / 2), 2)));
    s = s * EARTH_RADIUS;
    s = Math.round(s * 10000) / 10000.0;

    return s;
}

//传一个起始作标
function getRoute(pDataArray) {
    //获取作标
    var slat = pDataArray[0].Coordinate.split(",")[1];
    var slng = pDataArray[0].Coordinate.split(",")[0];
    var p = new Object();
    p.Lat = slat;
    p.Lng = slng;
    p.range = 0;
    p.Index = 0;

    var transitXY = new Array();
    transitXY.push(p); //中转座标数组
    var retrunDataArray = new Array(); //返回数据的数组
    for (var j = 0; j < transitXY.length; j++) {
        var LastData = null; //最后一次保存的数据值
        for (var i = 0; i < pDataArray.length; i++) {
            // 第一次 把起点与相邻的点的距离直接赋给起点
            var bl = true;
            if (i == 0) {
                transitXY[j].range = getGreatCircleDistance(transitXY[j].Lat, transitXY[j].Lng, pDataArray[i].Coordinate.split(",")[1], pDataArray[i].Coordinate.split(",")[0]);
                transitXY[j].Index = i;
                LastData = pDataArray[i];
            }
            else { //把与当前坐标与数据中坐标相比较小于当前距离的值直接赋给当前的距离
                if (transitXY[j].range > getGreatCircleDistance(transitXY[j].Lat, transitXY[j].Lng, pDataArray[i].Coordinate.split(",")[1], pDataArray[i].Coordinate.split(",")[0])) {
                    transitXY[j].range = getGreatCircleDistance(transitXY[j].Lat, transitXY[j].Lng, pDataArray[i].Coordinate.split(",")[1], pDataArray[i].Coordinate.split(",")[0]);
                    transitXY[j].Index = i;
                    LastData = pDataArray[i];
                }
            }
        }
        if (LastData != null) {
            //增加
            var lat = LastData.Coordinate.split(",")[1];
            var lng = LastData.Coordinate.split(",")[0];
            pDataArray.remove(transitXY[j].Index);
            transitXY.push({ Lat: lat, Lng: lng, range: 0, Index: 0 });
            retrunDataArray.push(LastData);
            //删除指定数组的数据
        }
    }
    return retrunDataArray;
}



//路线优化回调函数
function _map_CreateLine_Points(len, index, error) {
    CityIDs = index;
    if (CityIDs != null && CityIDs != "" && CityIDs != "0,0") {
        fnSave();
    } else {
        alert("完成");
    }
}

//保存
function fnSave() {
    var btn = this;
    btn.setDisabled(true);
    //new
    var array = new Array();
    if (Ext.getStore("storeStore").getCount() > 0) {
        var array = [];
        for (var i = 0; i < Ext.getStore("storeStore").getCount(); i++) {
            var b = new Object();
            b.MappingID = Ext.getStore("storeStore").getAt(i).data.MappingID;
            b.StoreID = Ext.getStore("storeStore").getAt(i).data.StoreID;
            array.push(b);
        }
    }
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=" + editMethod,
        params: {
            id: id,
            form: Ext.JSON.encode(array),
            deleteList: deleteList
        },
        method: 'post',
        success: function (response) {
            Ext.Msg.alert("提示", "操作成功");
            btn.setDisabled(false);

            gridStoreList.pagebar.moveFirst();
            callBack.saveCallBack();
        },
        failure: function () {
            Ext.Msg.alert("提示", "操作失败");
            btn.setDisabled(false);
        }
    });
}

//上升（by zhongbao.xiao 2013.4.24）
function fnUpUpdate() {
    var pValue = JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "Sequence" });
    if (pValue != null && pValue != "") {
        if (pValue == 1) {
            return;
        }
        var current = Ext.getStore("storeStore").getAt(parseInt(pValue) - 1);
        var prev = Ext.getStore("storeStore").getAt(parseInt(pValue) - 2);
        current.data.Sequence = parseInt(pValue) - 1;    //注意：Sequence是以1开始,rowIndex是以0开始
        prev.data.Sequence = parseInt(pValue);
        Ext.getStore("storeStore").sort({ property: 'Sequence', direction: 'ASC' });   //排序会强制刷新grid
        renderLine();
    } else {
        Ext.Msg.show({
            title: '提示',
            msg: '请选择一家门店进行操作',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
    }
}

//下降(by zhongbao.xiao at 2013.4.24)
function fnDownUpdate() {
    var pValue = JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "Sequence" });
    if (pValue != null && pValue != "") {
        if (pValue == Ext.getStore("storeStore").getCount()) {
            return;
        }
        var current = Ext.getStore("storeStore").getAt(parseInt(pValue) - 1);
        var next = Ext.getStore("storeStore").getAt(parseInt(pValue));
        current.data.Sequence = parseInt(pValue) + 1;    //注意：Sequence是以1开始,rowIndex是以0开始
        next.data.Sequence = parseInt(pValue);
        Ext.getStore("storeStore").sort({ property: 'Sequence', direction: 'ASC' });   //排序会强制刷新grid
        renderLine();
    } else {
        Ext.Msg.show({
            title: '提示',
            msg: '请选择一家门店进行操作',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
    }
}

//删除(新的删除方法 (原来的fnDelete 已经废除) by zhongbao.xiao 2013.4.27)
function fnDeleteUpdate() {
    var pValue = JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "Sequence" });
    if (pValue != null && pValue != "") {
        var sm = Ext.getCmp("gridView").getSelectionModel();
        //重画一个点
        var r = Ext.getCmp("gridView").getSelectionModel().getSelection()[0];
        var points = new Array();
        var Point = new Object();
        Point.StoreID = r.data.MapStoreID;
        Point.Lng = r.data.Coordinate.split(",")[0];
        Point.Lat = r.data.Coordinate.split(",")[1];
        Point.Icon = "os.png";
        Point.IsAssigned = "true";  //默认为true
        Point.IsEdit = false;
        Point.LabelID = "";
        //Point.LabelContent = r.data.StoreName;
        Point.Tips = r.data.StoreName;
        points.push(Point);
        window.frames["frmFlashMap"].index._map_RemoveStores(Ext.JSON.encode(points));
        window.frames["frmFlashMap"].index._map_AddStores(Ext.JSON.encode(points), true);

        //删除grid里的点，并记录删除的记录
        Ext.getStore("storeStore").remove(sm.getSelection());
        if (r.data.MappingID != "") {
            deleteList.push(r.data.MappingID);
        }
        if (Ext.getStore("storeStore").getCount() > 0) {
            sm.select(Ext.getStore("storeStore").getCount() - 1);
        }

        //渲染线路
        renderLine();

    } else {
        Ext.Msg.show({
            title: '提示',
            msg: '请选择一家门店进行操作',
            buttons: Ext.Msg.OK,
            icon: Ext.Msg.INFO
        });
    }
}

//1.地图框选事件
function _map_Extent_Select(obj) {
    if (obj != null) {
        for (var i = 0; i < obj.length; i++) {
            var storeinfo = fnGetStoreInfoByMapStoreID(obj[i].StoreID);
            if (Ext.getStore("storeStore").getById(storeinfo.StoreID) == null) {
                var r = Ext.create('storeEntity', {
                    MappingID: storeinfo.MappingID,
                    MapStoreID: storeinfo.MapStoreID,
                    StoreID: storeinfo.StoreID,
                    StoreName: storeinfo.StoreName,
                    Coordinate: storeinfo.Coordinate,
                    Sequence: storeinfo.Sequence
                });
                Ext.getStore("storeStore").insert(Ext.getStore("storeStore").getCount(), r);
            }
        }
        renderLine();
    }
}