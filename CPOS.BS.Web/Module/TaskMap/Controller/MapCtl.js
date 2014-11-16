var mapData;
var btncode = "search";
var task_CheckLoginState;
//var recentlyDate = JITMethod.GetDateString();
var b = false;
var searchType = 1;
var fp = true;
var map;
var mapData2;
Ext.onReady(function () {
    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载  
    JITPage.HandlerUrl.setValue("Handler/MapHandler.ashx?mid=");

    setInterval(function () {
        _interval();
    }, 15000);
    // Ext.getCmp("DateFrom").jitSetValue(GetDateString());
    //Ext.getCmp("DateTo").jitSetValue(GetDateString());
    initFullScreen();
    //$("#CCmapMenuDiv").removeClass("top");
    var screenWidth = window.width;
    if ($(window).width() > 1200) {
        $("#CCmapMenuDiv").css("top", "120px");
    } else {
        $("#CCmapMenuDiv").css("top", "150px");
    }


});
//function GetRecentlyDate() {
//    $.post("../VisitingData/File/Handler/TaskDataFileHandler.ashx?mid=" + __mid + "&btncode=search&method=GetRecentlyDate&r=" + Math.random(),
//                {
//                },
//                function (result) {
//                    if (result != "") {
//                        //alert(result);
//                        recentlyDate = result;
//                        Ext.getCmp("DateFrom").setValue(recentlyDate);
//                    }
//                    b = true;
//                    fnSearch();
//                });
//}
function _interval() {
    b = false;
    fnSearch();
}
function _map_InitMap() {
    //GetRecentlyDate();
    fnSearchType(1);
}
function fnSearch(id) {
    map = window.frames["MapFlash"].index;
    //if (id != undefined && typeof(id) == "string" && id.length > 0) {
    //    alert(id);
    //}

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&method=GetMapList",
        params: {
            type: searchType,
            name: Ext.getCmp("txtName").jitGetValue()
        },
        method: 'post',
        success: function (response) {
            var d = Ext.decode(response.responseText);
            //mapData2 = response.responseText;
            mapData2 = d;
            get("btnSearch1").innerHTML = "待处理 " + d.num1;
            get("btnSearch2").innerHTML = "计划中 " + d.num2;
            get("btnSearch3").innerHTML = "已完成 " + d.num3;
            get("txtAmount").innerHTML = "总金额: " + d.amount + " 元";
            //加载地图
            d.data.MapList = null;
            d.data.SendUserList = null;
            d.data.SendUserList2 = null;
            var jdata = JSON.stringify(d.data);
            mapData = jdata;
            try {
                removeAllPoint();
                _map_AddStores(b);
                if (fp) {
                    map._map_MoveToStoreByScale('1', 18);
                    fp = false;
                }
            }
            catch (e)
            { }
        },
        failure: function (result) {
            Ext.Msg.alert("提示", "系统忙，请稍后再试！");
        }
    });
    _map_AddStores();
}
function changeXY(data) {
    if (data != null && data != 'undefined') {
        var a = window.frames["MapFlash"];
        var dataObj = Ext.decode(data);
        for (var i = 0; i < dataObj.length; i++) {
            //如果是GPS类型，则纠偏成
            if (dataObj[i].InGPSType == "1") {
                var newXY = a.index._map_XYChange(dataObj[i].Lng, dataObj[i].Lat, 3);
                dataObj[i].Lng = newXY.split(',')[0];
                dataObj[i].Lat = newXY.split(',')[1];
            }
        }

        return Ext.encode(dataObj);
    }
    return data;
}

function fnReset() {
    //removeAllPoint();
    Ext.getCmp("searchPanel").getForm().reset();
    Ext.getCmp("DateFrom").jitSetValue(recentlyDate);
    //Ext.getCmp("DateTo").jitSetValue("");
    Ext.getCmp("Province").jitSetValue("");
    Ext.getCmp("City").jitSetValue("");
    Ext.getCmp("Supplier").jitSetValue("");
    Ext.getCmp("ClientUser").jitSetValue("");
    Ext.getCmp("txt_ClientStoreName").jitSetValue("");
    
}

function removeAllPoint() {
    var a = window.frames["MapFlash"];
    var b = a.index._map_RemoveStores('');
    return b;
}

function _map_AddStores(b) {
    var a = window.frames["MapFlash"];
    //a.index._map_ShowStoresCluster(true, 100);
    var lng = 100; var lat = 25;
    var ou = "";
    //var i = new Number(0);
    //for (i = 1; i < 100000; i++) {
    //    ou += pp(i,
	//	  parseFloat(lng) + parseFloat(i / 10000) * Math.random(),
	//	  parseFloat(lat) + parseFloat(i / 10000) * Math.random()) + ",";
    //}
    //var px = '[' + ou.substring(0, ou.length - 1) + ']';
    var px = changeXY(mapData);//  '[{"StoreID":"1","LabelID":"1","Menu":"0","LabelContent":"123232323","Lng":"121.5209","Lat":"31.2103","Icon":"g.png","IsAssigned":"false","IsEdit":"true","Tips":"这是一个测试点位1","PopInfoWidth":"200","PopInfoHeight":"","PopInfo":[{"type":"1","title":"客户名称","value":"坂田医院"},{"type":"2","title":"图片","value":"http://www.mobanwang.com/icon/UploadFiles_8971/200806/20080602162129496.png"},{"type":"3","title":"按钮1","value":"test1"}]},{"StoreID":"2","LabelID":"2","LabelContent":"12","Menu":"2","Lng":"121.4309","Lat":"31.231","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"这是一个测试点位2","PopInfoWidth":"200","PopInfo":[{"type":"1","tilte":"客户名称","value":"坂田医院"}]},{"StoreID":"3","LabelID":"3","LabelContent":"others","Lng":"121.4330","Lat":"31.3","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"这是一个测试点位2","PopInfo":[{"type":"1","tilte":"客户名称","value":"坂田医院"}]},{"StoreID":"5","LabelID":"5","Lng":"121.4","Lat":"31.2","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"frametest","PopInfoWidth":"200","PopInfoHeight":"200","PopInfo":[{"type":"4","tilte":"frame","value":"http://www.baidu.com"}]},{"StoreID":"4","LabelID":"4","Lng":"121.3","Lat":"31.2","Icon":"o.png","IsAssigned":"true","IsEdit":"false","Tips":"frametest","PopInfoWidth":"200","PopInfoHeight":"200","PopInfo":[{"type":"4","tilte":"frame","value":"http://www.baidu.com"}]},{"PopInfoWidth":"350","PopInfoHeight":"250" ,"StoreID": "8788", "LabelID": "","LabelContent":"LT", "Lng": "113.9185962", "Lat": "22.4912462", "Icon": "o.png", "IsAssigned": "false", "Tips": "客惠隆百货-草围", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}]}]';
    //var px= '[{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8767", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "114.016601", "Lat": "22.612537", "Icon": "g.png", "IsAssigned": "false", "Tips": "爱家--怡福店", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8768", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "114.017726", "Lat": "22.617372", "Icon": "g.png", "IsAssigned": "false", "Tips": "爱家--茂名店", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8769", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.9114552", "Lat": "22.4774082", "Icon": "g.png", "IsAssigned": "false", "Tips": "长发食杂店", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8770", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.918325", "Lat": "22.4829212", "Icon": "g.png", "IsAssigned": "false", "Tips": "诚信食杂店", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8771", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.89123", "Lat": "22.485301", "Icon": "g.png", "IsAssigned": "false", "Tips": "百里臣（海景分店）", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8775", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.91589355", "Lat": "22.49056625", "Icon": "g.png", "IsAssigned": "false", "Tips": "鲜记生活超市-水湾", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8788", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.917601", "Lat": "22.492683", "Icon": "g.png", "IsAssigned": "false", "Tips": "客惠隆百货-草围", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8802", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.980893", "Lat": "22.555994", "Icon": "g.png", "IsAssigned": "false", "Tips": "富永百货", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8814", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.88324738", "Lat": "22.49606514", "Icon": "g.png", "IsAssigned": "false", "Tips": "乐乐百货-南山", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8815", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.88315582", "Lat": "22.4962616", "Icon": "g.png", "IsAssigned": "false", "Tips": "家家惠-月亮湾", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] },{"PopInfoWidth":"350","PopInfoHeight":"300" ,"StoreID": "8825", "LabelID": "","LabelContent":"","Menu":"2", "Lng": "113.8960023", "Lat": "22.5002094", "Icon": "o.png", "IsAssigned": "true", "Tips": "深圳市同惠达贸易有限公司", "PopInfo": [{ "type": "4", "tilte": "frame", "value": "../Basic/Message.htm"}] }]';
    a.index._map_ShowStoresCluster(false, 1);
    
    a.index._map_AddStores(px, true);
    if (b) {
        //a.index._map_MoveToStores();
    }
}
function pp(id, lng, lat) {
    return '{"StoreID": "' + id + '", "LabelID": "' + id + '", "LabelContent": "' + id + '", "Lng": "' + lng + '", "Lat": "' + lat + '", "Icon": "o.png","IsEdit":"true","Tips":"dada"}';
}

function funFullScreen() {
    openFullWall();
}

function openFullWall() {
    var o = document.getElementById("hFullScreen");
    //var b = document.getElementById("btnFullScreen-btnInnerEl");
    var b = document.getElementById("jitbutton-1010-btnInnerEl");
    var getOval = $(o).attr("rel");
    if (getOval == "1") {
        $(b).text("退出全屏");
        $(o).attr("rel", "0");
        //$("#CCmapMenuDiv").hide();
        $("#asideDiv,#asideDiv>.aside").animate({ width: 0 }, 500, function() { $(this).hide(); });
    } else {
        $(b).text("全屏");
        $(o).attr("rel", "1");
        //$("#CCmapMenuDiv").show();
        $("#asideDiv,#asideDiv>.aside").show().animate({ width: 210 }, 500);
    }
}

//function keepSession() {
//    document.getElementById("imgKeep").src = "/default.aspx?r=" + Math.random();
//    setTimeout(function () {
//        keepSession();
//    }, 60000);
//}
//setTimeout(function () {
//    keepSession();
//}, 60000);


//function keepMstrSession() {
//    var mstrUrl = "nothing";
//    if (mstrUrl != null)
//        document.getElementById("ifmMstrKeep").src = "nothing" + "&r=" + Math.random();
//    setTimeout(function () {
//        keepMstrSession();
//    }, 1200000); //20分钟
//}
//setTimeout(function () {
//    keepMstrSession();
//}, 1200000);

function initFullScreen () {
    document.getElementsByTagName("body")[0].onkeydown = function() {
        if (event.keyCode == 8) {
            var elem = event.srcElement;
            var name = elem.nodeName;

            if (name != 'INPUT' && name != 'TEXTAREA') {
                event.returnValue = false;
                return;
            }
            var type_e = elem.type.toUpperCase();
            if (name == 'INPUT' && (type_e != 'TEXT' && type_e != 'TEXTAREA' && type_e != 'PASSWORD' && type_e != 'FILE')) {
                event.returnValue = false;
                return;
            }
            if (name == 'INPUT' && (elem.readOnly == true || elem.disabled == true)) {
                event.returnValue = false;
                return;
            }
        }
    };
}

fnSearchType = function(type) {
    get("btnSearch1").className = "z_btn2";
    get("btnSearch2").className = "z_btn2";
    get("btnSearch3").className = "z_btn2";
    switch (type) {
        case 1:
            get("btnSearch1").className = "z_btn1";
            break;
        case 2:
            get("btnSearch2").className = "z_btn1";
            break;
        case 3:
            get("btnSearch3").className = "z_btn1";
            break;
    }
    searchType = type;
    fnSearch();
}
fnOrder = function(id) {
    
}
