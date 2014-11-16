//创建MapWindow对象,根据Url中传入的MapWindowID获取上级页面的Window对象，并给__mapContainer赋值
var __mapHeight = 500;
var __mapContainer = _map_SetContainer();

function _map_load() {
    if (__mapContainer != null && __mapContainer.jitPoint.isEditable) {
        document.getElementById("dv_mapsearch").style.display = 'block';
    }
 }

//获取上级Window对象
function _map_SetContainer() {  
 
    var Url = window.location.href;
    var MapID = Url.substring(((Url.lastIndexOf("MapWindowID=") + 12)));
    if (MapID != null && MapID != "") {
        __mapHeight = parent.Ext.getCmp(MapID).getHeight() - 35;
        if (parent.Ext.getCmp(MapID).jitPoint.isEditable) {
            __mapHeight = __mapHeight - 25;
        }
        return parent.Ext.getCmp(MapID);
    }
    return null;
}

//地图初始化 回调函数
function _map_InitMap() {  
    if (__mapContainer!=null&&__mapContainer._map_InitMap != null) {
        __mapContainer._map_InitMap();
    }
}

//地图点击事件
function _map_OnClick(pLng, pLat) {
    if (__mapContainer != null && __mapContainer._map_OnClick != null) {
        __mapContainer._map_OnClick(pLng, pLat);
    }
}

//地图元素单点事件 obj对象为 Store 数据结构
function _map_Graphic_OnClick(pObj) {
    if (__mapContainer != null && __mapContainer._map_Graphic_OnClick != null) {
        __mapContainer._map_Graphic_OnClick(pObj);
    }
}

//地图双击事件
function _map_Graphic_OnDoubleClick(pObj) {
    if (__mapContainer != null && __mapContainer._map_Graphic_OnDoubleClick != null) {
        __mapContainer._map_Graphic_OnDoubleClick(pObj);
    }
}

//要素编辑通知函数
function _map_Graphic_MoveEdit(pObj) {
    if (__mapContainer != null && __mapContainer._map_Graphic_MoveEdit != null) {    
        __mapContainer._map_Graphic_MoveEdit(pObj);
    }
}


//地图修改事件
function _map_Update() {
    if (__mapContainer != null && __mapContainer._map_Update != null) {
        __mapContainer._map_Update();
    }
 }


 //地图清除事件
 function _map_RemoveStores() {
     if (__mapContainer != null && __mapContainer._map_RemoveStores != null) {
         __mapContainer._map_RemoveStores("");
     }
 }

 //地图百度坐标转换谷歌坐标
 function _map_XYChange(lng, lat) {
     if (__mapContainer != null && __mapContainer._map_XYChange != null) {
        return  __mapContainer._map_XYChange(lng, lat);
     }
}

function _map_RemoveTitle() {
    window.frames["frmFlashMapSearch"]._map_RemoveTitle();  
}