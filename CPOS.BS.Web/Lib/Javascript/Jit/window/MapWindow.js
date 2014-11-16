//地图基础控件
Ext.define('Jit.window.MapWindow', {
    alias: 'widget.jitmapwindow',
    config: {
        jitPoint: null
    },
    constructor: function (args) {
        if (args.id == null || args.id == "") {
            args.id = "__MapID";
        }
        var defaultConfig = {
            jitSize: "custom",
            title: 'Map',
            width: 700,
            height: 450,
            constrain: true,
            modal: true,
            resizable: true,
            inGPSType: 0,
            html: '<iframe  height="100%"  marginheight="0" marginwidth="0" scrolling="no"  frameborder="no"  id="' + args.id + 'frmFlashMap"  width="100%" src="/Lib/MapFlash/index.html?config=mapconfig.xml&MapWindowID=' + args.id + '"></iframe>'
        }
        var defaultPoint = {
            pointID: '0',           //[StoreID] 整数，唯一标识，必须
            lng: '0',               //[Lng] 浮点数，商店GPS坐标的经度，必须，范围0-180.
            lat: '0',               //[Lat] 浮点数，商店GPS坐标的纬度，必须，范围0-90
            icon: 'g.png',          //[Icon] 图片样式，g.png绿色点（gs.png选择后），b.png蓝色点（bs.png选择后），o.png橙色点（os.png选择后），必须
            isEditable: false,      //[IsEdit] 是否可拖拽
            insideText: '',         //[LabelID] 图片上放的文字
            pointTitle: '',         //[LabelContent] 图片边上文字
            pointInfoHeight: '0',   //[PopInfoHeight] 弹出框长度
            pointInfoWidth: '0',    //[PopInfoWidth] 弹出框宽度
            tips: '',               //[Tips] 字符串，鼠标悬停到点上时显示的文本信息
            pointInfo: new Array(),  //[PopInfo] 门店信息,必须 {"title":"客户名称","value":"坂田医院","type":"1" }type=1为文本、type=2为图片、type=3为按钮,type=4 为iframe
            mapScale: 0              //地图等级
        };
        args.jitPoint = Ext.applyIf(args.jitPoint || {}, defaultPoint);
        args = Ext.applyIf(args, defaultConfig);
        if (args.id != null && args.id != "") {
            if (Ext.getCmp(args.id) != null) {
                Ext.getCmp(args.id).destroy();
            }
            var instance = Ext.create('Jit.window.Window', args);
            /*
            获取地图对象
            */
            instance.getFlashMapObject = function () {
                var frame = window.frames[args.id + "frmFlashMap"];
                return frame;
            }

            /*
            Flash地图是否加载完毕
            */
            instance._map_InitMap = function () {
                //var frame = instance.getFlashMapObject();
                //frame.index._map_SetMapScale(this.jitPoint.mapScale);
                this._map_LoadMap();
            }

            instance._map_LoadMap = function () {

                if (this.jitPoint.lng > 0 && this.jitPoint.lat > 0) {
                    if (this.jitPoint.inGPSType == 1) {
                        var xy = instance._map_XYGpsChange(this.jitPoint.lng, this.jitPoint.lat);
                        this.jitPoint.lng = xy.split(',')[0];
                        this.jitPoint.lat = xy.split(',')[1];
                    }
                    var frame = instance.getFlashMapObject();
                    if (frame != null && frame.index != null) {
                        frame.index._map_RemoveStores("");
                        frame.index._map_AddStores(instance.jitGetPoint(), true);
                        var p = 0;
                        if (this.jitPoint.mapScale != null && this.jitPoint.mapScale > 0) {
                            p = this.jitPoint.mapScale;
                        }
                        if (p == 0) {
                            p = frame.index._map_GetMapScale();
                            if (p <= 4) {
                                p = 15;
                            }
                        }
                        if (p == 0) {
                            frame.index._map_MoveToStore(this.jitPoint.pointID);
                        } else {
                            frame.index._map_MoveToStoreByScale(this.jitPoint.pointID, p)
                        }
                    }
                }
            }
            /*
            地图单击事件,重新调用            
            */
            instance._map_OnClick = function (pLng, pLat) {
                if (this.jitPoint.isEditable) {
                    this.jitPoint.lng = pLng;
                    this.jitPoint.lat = pLat;
                    this._map_LoadMap();
                }
            }
            /*
            地图修改事件，影响
            */
            instance._map_Update = function () {
                if (args.handler != null) {
                    args.handler(this.jitPoint);
                }
            }

            /*
            设置数据  数据格式 {"Lat":123.32,"Lng":32.12,"Type":1}
            */
            instance.jitSetValue = function (pValue) {
                if (pValue != null && pValue != "") {
                    var Values = pValue.split(",");
                    if (Values.length > 1) {
                        if (this.jitPoint.isEditable) {
                            this.jitPoint.lng = Values[0];
                            this.jitPoint.lat = Values[1];
                            this._map_LoadMap();
                        }
                    }
                } else {
                    this.jitPoint.lng = "";
                    this.jitPoint.lat = "";
                    this._map_LoadMap();
                }
            }

            /*
            获取数据
            */
            instance.jitGetValue = function () {
                return this.jitPoint;
            }
            /*
            要素编辑事件
            */
            instance._map_Graphic_MoveEdit = function (pObj) {
                if (this.jitPoint.isEditable) {
                    this.jitPoint.lng = pObj.Lng;
                    this.jitPoint.lat = pObj.Lat;
                    this._map_LoadMap();
                }
            }

            /*
            Flash地图清除点
            */
            instance._map_RemoveStores = function () {
                var frame = instance.getFlashMapObject();
                if (frame != null && frame.index != null && frame.index._map_RemoveStores != null) {
                    frame.index._map_RemoveStores("");
                    frame._map_RemoveTitle();
                    this.jitPoint.lng = "";
                    this.jitPoint.lat = "";
                    this._map_LoadMap();
                }
            }

            /*
            Flash地图百度转换谷歌坐标
            */
            instance._map_XYChange = function (lng, lat) {
                var frame = instance.getFlashMapObject();
                var xy = frame.index._map_XYChange(lng, lat, 1);
                return xy;
            }
            /*gps 转google*/
            instance._map_XYGpsChange = function (lng, lat) {

                var frame = instance.getFlashMapObject();
                var xy = frame.index._map_XYChange(lng, lat, 3);
                return xy;
            }
            /*
            Flash地图清空查询数据
            */
            instance._map_RemoveTitle = function (lng, lat) {
                var frame = instance.getFlashMapObject();
                frame.index._map_RemoveTitle();
            }

            /*Flash地图所需要的Json 字符串,
            @return string类型的Json
            */
            instance.jitGetPoint = function () {
                var Point = new Object();
                Point.StoreID = this.jitPoint.pointID;
                Point.Lng = this.jitPoint.lng;
                Point.Lat = this.jitPoint.lat;
                Point.Icon = this.jitPoint.icon;
                Point.IsAssigned = "true";  //默认为true
                Point.IsEdit = this.jitPoint.isEditable;
                Point.LabelID = this.jitPoint.insideText;
                Point.LabelContent = this.jitPoint.pointTitle;
                if (this.jitPoint.pointInfoHeight > 0) {
                    Point.PopInfoHeight = this.jitPoint.pointInfoHeight;
                }
                if (this.jitPoint.pointInfoWidth > 0) {
                    Point.PopInfoWidth = this.jitPoint.pointInfoWidth;
                }
                Point.Tips = this.jitPoint.tips;
                if (this.jitPoint.pointInfo != null && this.jitPoint.pointInfo.toString() != "") {
                    Point.PopInfo = [this.jitPoint.pointInfo];
                }
                var PointArray = new Array();
                PointArray.push(Point);
                var PointJson = Ext.JSON.encode(PointArray);
                return PointJson;
            }
            return instance;
        } else {
            return null;
        }
    }
});