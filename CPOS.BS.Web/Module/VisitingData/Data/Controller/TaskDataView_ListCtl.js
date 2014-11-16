var btncode = "search";

//UI相关
var bfdxwidth = 280; //拜访对象列 宽度
var zhiwidth = 200;//值列 宽度
var canshuwidth = 200; //参数列 宽度

var ordernowidth=140;//订单编号 宽度
var orderpdwidth = 170; //订单产品 宽度

//summary相关
var oWorkingHoursIndoor, oWorkingHoursJourneyTime, oWorkingHoursTotal;

var userid, exectime;
//var taskid;
var r = "";
var mapData;

//for xtemplate
Ext.Loader.setConfig({ enabled: true });
Ext.Loader.setPath('Ext.ux.DataView', '/Lib/Javascript/Ext4.1.0/ux/DataView');
Ext.require([
    'Ext.data.*',
    'Ext.util.*',
    'Ext.view.View',
    'Ext.ux.DataView.Animated',
    'Ext.XTemplate',
    'Ext.panel.Panel',
    'Ext.toolbar.*',
    'Ext.slider.Multi'
]);

Ext.onReady(function () {

    //加载需要的文件
    InitVE();
    InitStore();
    InitView();

    //页面加载
    JITPage.PageSize.setValue(15);
    JITPage.HandlerUrl.setValue("Handler/TaskDataHandler.ashx?mid=" + __mid);

    //设置值
//    Ext.getCmp("ClientStructureID").jitSetValue(JITMethod.getUrlParam("ClientStructureID"));
//    Ext.getCmp("ClientPositionID").jitSetValue(JITMethod.getUrlParam("ClientPositionID"));
//    Ext.getCmp("ClientUserName").jitSetValue(unescape(JITMethod.getUrlParam("ClientUserName")));
//    Ext.getCmp("ExecutionTime").setValue(JITMethod.getUrlParam("sExecutionTime"));
    //Ext.getCmp("ExecutionTime").setValue(new Date());

    fnSearch();
});

function fnSearch() {
    Ext.getCmp("gridView").setLoading();
    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetTaskUserData",
        params: {
            ClientStructureID: Ext.getCmp("ClientStructureID").jitGetValue(),
            ClientPositionID: Ext.getCmp("ClientPositionID").jitGetValue(),
            ClientUserID: JITMethod.getUrlParam("ClientUserID"),
            ExecutionTime: Ext.getCmp("ExecutionTime").getValue()
        }, //三个参数
        method: 'post',
        success: function (response) {
            var jdata = Ext.JSON.decode(response.responseText);

            //加载列表
            Ext.getStore("taskDataStore").removeAll();
            Ext.getStore("taskDataStore").add(jdata.data);
            oWorkingHoursIndoor = jdata.summary[0].oWorkingHoursIndoor;
            oWorkingHoursJourneyTime = jdata.summary[0].oWorkingHoursJourneyTime;
            oWorkingHoursTotal = jdata.summary[0].oWorkingHoursTotal;
            
            Ext.getCmp("gridView").reconfigure(Ext.getStore("taskDataStore"));
            Ext.getCmp("gridView").setLoading().hide();
            
        },
        failure: function () {
            //Ext.Msg.alert("提示", "获取数据失败");
        }
    });
}

function fnReset() {
    Ext.getCmp("ExecutionTime").setValue(JITMethod.getUrlParam("sExecutionTime"));
}

//返回
function fnBack() {
    window.location = 'TaskData.aspx' + window.location.search;
}

function fnInitMap() {
    if (mapData == null) {
        return;
    }
    window.frames["frmFlashMap"].index._map_RemoveStores("");
    if (mapData.data.length > 0) {
        Ext.getCmp("map_ckfact").setValue(true);
    }
    else {
        Ext.getCmp("map_ckfact").setValue(false);
    }

    if (mapData.plan.length > 0) {
        Ext.getCmp("map_ckplan").setValue(true);
    }
    else {
        Ext.getCmp("map_ckplan").setValue(false);
    }

    fnMapFactLine();
    fnMapPlanLine();
    window.frames["frmFlashMap"].index._map_MoveToStores();
}

function fnView(pPopid, pPopname, pTaskdataid, pUserid) {

    var popid = pPopid;
    var popname = pPopname;
    var taskdataid = pTaskdataid;
    userid = pUserid;
    exectime = Ext.getCmp("ExecutionTime").getRawValue();
//     taskid = JITPage.getSelected({ gridView: Ext.getCmp("gridView"), id: "VisitingTaskID" })[0];
     //alert(taskdataid);
     r = Math.random().toString().split('.')[1];

     renderTab(popid, popname, taskdataid);

    Ext.create('Jit.window.Window', {
        jitSize: "large",
        id: "stepEditWin" + r,
        modal: false,
        style: "z-index:100",
        layout: 'column',
        draggable: true,
        items: [{
            xtype: "tabpanel",
            style: "width:80%;",
            activeTab: 0,
            id: "stepitems" + r
            //items: stepitems
        }, {
            xtype: "dataview",
            store: Ext.getStore("taskDataStore"),
            style: "width:20%;",
            border: 1,
            height: 363,
            tpl: Ext.create('Ext.XTemplate',
                        '<tpl for=".">',
                            '<div class="POPLIST" id="POPLIST_'+r+'_{POPID}" onclick="renderTab(\'{POPID}\',\'{POPName}\',\'{VisitingTaskDataID}\');">',
                                '<a href="javascript:;"><strong>{#}.{POPName}</strong></a>',
                            '</div>',
                        '</tpl>'
                    ),
            itemSelector: "div.POPLIST",
            autoScroll: true
        }
                ],
        border: 0,
        closeAction: "destory",
        autoScroll: false
    });

    Ext.getCmp("stepEditWin" + r).show();

//    vpopid = popid;
//    Ext.getCmp("stepEditWin" + r).addListener({
//        'afterlayout': function () {
//            //alert($(".x-item-selected").length);
//            $("#POPLIST_" + vpopid).addClass("POPLISTCLICK");
//        }
//    });
}

function renderTab(popid, popname, taskdataid) {
    /**/
    //alert();

    //console.log("#POPLIST_" + popid);

    var myMask_info = JITPage.Msg.GetData;
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: myMask_info });
    myMask.show();

    Ext.Ajax.request({
        url: JITPage.HandlerUrl.getValue() + "&btncode=" + btncode + "&method=GetTaskStepParameterList",
        params: {
            ClientUserID: userid,
            ExecutionTime: exectime,
            POPID: popid,
            VisitingTaskDataID: taskdataid
        }, //三个参数
        method: 'post',
        success: function (response) {

            //try {
            var jdata = eval(response.responseText)[0];
            var stepitems = [];

            for (var i = 0; i < jdata.stepitemslength; i++) {
                //store
                Ext.create('Ext.data.Store', {
                    id: 'Store_' + i,
                    fields: eval("jdata.field" + i),
                    data: eval("jdata.data" + i)
                });

                //grid
                
                
                    Ext.create('Ext.grid.Panel', {
                        store: Ext.getStore("Store_" + i),
                        id: "grid_" + r + i,
                        columnLines: true,
                        forceFit: true,
                        columns: eval("jdata.column" + i),
                        width: "100%",
                        border: 0,
                        height: 343,
                        listeners: {
                            'afterlayout': function () {
                                setImg(this);
                            }
                        }
                    });
                
                //tabitem
                stepitems.push({
                    title: jdata.stepitems[i].StepName,
                    items: [Ext.getCmp("grid_" + r + i)]
                });
            }

            //do for POP click
            Ext.getCmp("stepitems" + r).removeAll();
            Ext.getCmp("stepitems" + r).add(stepitems);
            Ext.getCmp("stepitems" + r).setActiveTab(0);

            Ext.getCmp("stepEditWin" + r).setTitle(Ext.getCmp("ClientUserName").jitGetValue() + " " + Ext.getCmp("ExecutionTime").getRawValue() + " " + popname);

            $(".POPLIST").removeClass("POPLISTselected");
            $("#POPLIST_" + r + "_" + popid).addClass("POPLISTselected");

            myMask.hide();
        },
        failure: function () {
            Ext.Msg.alert("提示", "获取参数数据失败");
            myMask.hide();
        }
    });
}

function fnColumnView(value, p, r) {
    var html = "";

    html = "<a style='color:blue;' href=\"javascript:;\" onclick=\"fnView('"
    + r.data.POPID + "','"
    + r.data.POPName + "','"
    + r.data.VisitingTaskDataID + "','"
    + r.data.ClientUserID  + "')\">" + value + "</a>";

    return html;
}

function fnColumnTime(value, p, r) {
    var res = "";
    var min = parseInt(value);
    if (!isNaN(min)) {
        if (min >= 60) {
            res = Math.floor(min / 60) + "小时" + (min % 60) + "分钟";
        }
        else if (min < 0) {
            res = "0分钟";
        }
        else {
            res = min + "分钟";
        }
    }
    else {
        res = value;
    }
    return res;
}

//照片列的处理
function fnColumnPhoto(value, p, r) {
    var res = "";
    var photoSrces = JITMethod.getPictures(__imgpath, __clientid, r.data.ClientUserID, value);
    if (photoSrces != null && photoSrces.length > 0) {
        for (var i = 0; i < photoSrces.length; i++) {
            //' + photoSrces[i] + '

            res += '<a rel="fancybox_group' + this.getId() + '" href="' + photoSrces[i] + '"><img height="15px" width="15px" src="/Framework/Image/icon/image.png" /></a>';
        }
    }
    return res;
}
//照片行处理
function fnColumnPhotoByType(value, p, r) {
    var res = "";
    if (parseInt(r.data.ControlType) == 12) {
        //照片类型
        var photoSrces = JITMethod.getPictures(__imgpath, __clientid, r.data.ClientUserID, value);
        if (photoSrces != null && photoSrces.length > 0) {
            for (var i = 0; i < photoSrces.length; i++) {
                //' + photoSrces[i] + '

                res += '<a rel="fancybox_group' + this.getId() + '" href="' + photoSrces[i] + '"><img height="15px" width="15px" src="/Framework/Image/icon/image.png" /></a>';
            }
        }
    }
    else {
        res = value;
    }
    return res;
}
//有效拜访列处理
function fnColumnEffectiveVisit(value, p, r) {
    var html = "否";
    if (!isNaN(parseInt(value))) {
        if (parseInt(value) > 0) {
            html = "是";
        }
    }
    return html;
}
//地图列处理
function fnCoordinate(value, p, r) {
    var html = "";
    if (value.split(',').length <= 1) {
    //无定位
        html = "无定位";
    }
    else {
        //有定位
        var Point = new Object();
        Point.Lng = value.split(",")[0];
        Point.Lat = value.split(",")[1];
        if (value.split(",")[2]
                && value.split(",")[2].toString() == "1") {
            //需要纠偏的数据
            Point.Lng = value.split(",")[0];
            Point.Lat = value.split(",")[1];
//            var xy = window.frames["frmFlashMap"].index._map_XYChange(Point.Lng, Point.Lat, 3);
//            Point.Lng = xy.split(',')[0];
//            Point.Lat = xy.split(',')[1];
        }
        //获取偏离距离
        var tanc = 0;
        if (r.data.POPCoordinate != null) {
            tanc = getGreatCircleDistance(r.data.POPCoordinate.split(',')[0], r.data.POPCoordinate.split(',')[1], Point.Lng, Point.Lat);
        }

        if (tanc > 500) {
            html = "<a onclick=\"___fuMapShow('" + Point.Lng + "','" + Point.Lat + "','0','地图-" + r.data.POPName + "')\"' href=\"javascript:;\" style='color:blue;' title='偏离终端：" + parseInt(tanc) + "米（偏差在500米以内为准确定位）'>定位不吻合</a>";
        }
        else {
            html = "<a onclick=\"___fuMapShow('" + Point.Lng + "','" + Point.Lat + "','0','地图-" + r.data.POPName + "')\"' href=\"javascript:;\" style='color:blue;' title='偏离终端：" + parseInt(tanc) + "米(偏差在500米以内为准确定位)'>准确定位</a>";
        }
    }
    return html;
}

//照片步骤的照片
function fnStepPhoto(group, photosrc, photoname) {
    return '<a rel="fancybox_group' + group+'" href="' + photosrc + '"><img class="fancyimg" title="' + photoname + '" src="' + photosrc + '" /></a>';
}

function fnSetMapTool() {
    if (Ext.getCmp("tabs").getActiveTab().id == "tabs2") {
        Ext.getCmp("map_ckplan").show();
        Ext.getCmp("map_ckfact").show();
    }
    else {
        Ext.getCmp("map_ckplan").hide();
        Ext.getCmp("map_ckfact").hide();
    }
}

function fnMapFactLine() {
    var PointArray = new Array();
    var storeIDs = new Array();
    for (var i = 0; i < mapData.data.length; i++) {
        
        //有定位
            var Point = new Object();
            Point.StoreID = i;

            if (mapData.data[i].InCoordinate != null) {
                //有定位
                if (mapData.data[i].InCoordinate.split(",")[2]
                && mapData.data[i].InCoordinate.split(",")[2].toString() == "1") {
                
                    Point.Lng = mapData.data[i].InCoordinate.split(",")[0];
                    Point.Lat = mapData.data[i].InCoordinate.split(",")[1];
                    var xy = window.frames["frmFlashMap"].index._map_XYChange(Point.Lng, Point.Lat, 3);
                    Point.Lng = xy.split(',')[0];
                    Point.Lat = xy.split(',')[1];
                }
                else {
                    Point.Lng = mapData.data[i].InCoordinate.split(",")[0];
                    Point.Lat = mapData.data[i].InCoordinate.split(",")[1];
                }

                //获取偏离距离
                var tanc = 0;
                if (mapData.plan[i].Coordinate != null) {
                    tanc = getGreatCircleDistance(mapData.plan[i].Coordinate.split(",")[0], mapData.plan[i].Coordinate.split(",")[1], Point.Lng, Point.Lat);
                }
                
                if (tanc > 1000) {
                    //偏离超过1KM
                    Point.Icon = "r.png";
                }
                else {
                    Point.Icon = "g.png";
                }
                Point.Tips = mapData.data[i].POPName + "  偏离计划：" + parseInt(tanc) + "米";
            }
            else {
                //未定位
                if (mapData.plan[i].Coordinate != null) {
                    Point.Lng = parseFloat(mapData.plan[i].Coordinate.split(",")[0]) + 0.0003;
                    Point.Lat = parseFloat(mapData.plan[i].Coordinate.split(",")[1]) - 0.0003;
                }
                Point.Icon = "r.png";
                Point.Tips = mapData.data[i].POPName + "  未定位";
            }
            
            //Point.Icon = "g.png";
            Point.IsAssigned = "true";  //默认为true
            Point.IsEdit = false;
            Point.LabelID = (i + 1).toString();
            Point.LabelContent = "";
            
            Point.PopInfoHeight = 320;
            Point.PopInfoWidth = 270;

            //组织传递的数据
            var DataInfo = new Object();
            DataInfo.ExecutionTime = JITMethod.getDate(mapData.data[i].ExecutionTime);
            DataInfo.InTime = JITMethod.getMonthDayHourMinute(mapData.data[i].InTime);
            DataInfo.OutTime = JITMethod.getMonthDayHourMinute(mapData.data[i].OutTime);
            DataInfo.WorkingHoursJourneyTime = fnColumnTime(mapData.data[i].WorkingHoursJourneyTime);
            DataInfo.WorkingHoursIndoor = fnColumnTime(mapData.data[i].WorkingHoursIndoor);
            DataInfo.WorkingHoursTotal = fnColumnTime(mapData.data[i].WorkingHoursTotal);

            DataInfo.POPID = mapData.data[i].POPID;

            Point.PopInfo = [{ "title": "", "value": "/Module/VisitingData/Data/TaskDataView_List_POPDetail.aspx?mid=" + __mid
                        + "&viewtype=fact"
                        + "&POPID=" + mapData.data[i].POPID
                        + "&POPType=" + mapData.data[i].POPType
                        + "&VisitingTaskDataID=" + mapData.data[i].VisitingTaskID
                        + "&data=" + escape( Ext.JSON.encode(DataInfo)), "type": "4"
            }];

            PointArray.push(Point);
            storeIDs.push({ "StoreID": i.toString() });
        
    }

    if (Ext.getCmp("map_ckfact").getValue() == true) {
        window.frames["frmFlashMap"].index._map_AddStores(Ext.JSON.encode(PointArray), true);
        window.frames["frmFlashMap"].index._map_CreateLine("1", Ext.JSON.encode(storeIDs), 'line', 'true', false, '1', 'none', '1', "0x008000");

        window.frames["frmFlashMap"].index._map_MoveToStores();
    }
    else {
        window.frames["frmFlashMap"].index._map_RemoveStores(Ext.JSON.encode(PointArray));
        window.frames["frmFlashMap"].index._map_ClearLines("1");
    }
}

function fnMapView(popid) {
    var rdata = Ext.getStore("taskDataStore").getById(popid).data;
    fnView(rdata.POPID, rdata.POPName, rdata.VisitingTaskDataID, rdata.ClientUserID);

}

function fnMapPlanLine() {
    var PointArray = new Array();
    var storeIDs = new Array();

    for (var i = 0; i < mapData.plan.length; i++) {
        if (mapData.plan[i].Coordinate != null) {
            var j = i + 100;
            var Point = new Object();
            Point.StoreID = j;

            Point.Lng = mapData.plan[i].Coordinate.split(",")[0];
            Point.Lat = mapData.plan[i].Coordinate.split(",")[1];

            Point.Icon = "b.png";
            Point.IsAssigned = "true";  //默认为true
            Point.IsEdit = false;
            Point.LabelID = (i + 1).toString();
            Point.LabelContent = "";
            Point.Tips = mapData.plan[i].POPName;
            Point.PopInfoHeight = 240;
            Point.PopInfoWidth = 270;
            Point.PopInfo = [{ "title": "", "value": "/Module/VisitingData/Data/TaskDataView_List_POPDetail.aspx?mid=" + __mid
                        + "&viewtype=plan"
                        + "&POPID=" + mapData.plan[i].POPID
                        + "&POPType=" + mapData.plan[i].POPType
                        + "&VisitingTaskDataID=" + mapData.plan[i].VisitingTaskID, "type": "4"
            }];

            PointArray.push(Point);
            storeIDs.push({ "StoreID": j.toString() });
        }
    }

    if (Ext.getCmp("map_ckplan").getValue() == true) {
        window.frames["frmFlashMap"].index._map_AddStores(Ext.JSON.encode(PointArray), true);
        window.frames["frmFlashMap"].index._map_CreateLine("2", Ext.JSON.encode(storeIDs), 'line', 'true', false, '1', 'none', '1', "0x0000FF ");
        window.frames["frmFlashMap"].index._map_MoveToStores();
    }
    else {
        window.frames["frmFlashMap"].index._map_RemoveStores(Ext.JSON.encode(PointArray));
        window.frames["frmFlashMap"].index._map_ClearLines("2");
    }
}

//图片加载fancybox
function setImg(obj) {

    if ($('a[rel=fancybox_group'+obj.getId()+']').length == 0) {
        return;
    }

//    $("a[rel=fancybox_group]").fancybox({
//        'transitionIn': 'none',
//        'transitionOut': 'none',
//        'titlePosition': 'over',
//        'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
//            return '';
//        }
//    });
    //$('a[rel=fancybox_group' + obj.getId() + ']').fancybox();

    $('a[rel=fancybox_group' + obj.getId() + ']').fancybox({
        openEffect: 'none',
        closeEffect: 'none',

        prevEffect: 'none',
        nextEffect: 'none',

        closeBtn: true

       
//        afterLoad: function () {
//            this.title =  (this.index + 1) + ' / ' + this.group.length + (this.title ? ' - ' + this.title : '');
//        }
    });

};


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