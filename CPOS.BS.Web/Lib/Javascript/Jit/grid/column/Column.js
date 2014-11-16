Ext.define('Jit.grid.column.Column', {
    extend: 'Ext.grid.column.Column'
    , alias: 'widget.jitcolumn'
    , config: {
        /*@jitDataType jit的数据类型*/
        jitDataType: null
        /*@jitTimespanFormatter 时间的格式化*/
        , jitTimespanFormatter: null
    }
    , statics: {
        /*@jitYesText 当jitDataType=Boolean时，值为true的文本*/
        jitYesText: '是'
        /*@jitNoText 当jitDataType=Boolean时，值为false的文本*/
        , jitNoText: '否'
        /*@jitTimespanFormatter 时间跨度的格式字符串*/
        , jitTimespanFormatter: {
            dayText: '天'
            , hourText: '小时'
            , minuteText: '分'
            , secondText: '秒'
        }
    }
    /*@constructor 构造函数*/
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            jitDataType: 'int'
            , sortable: true
            , hideable: false
        };
        //处理配置项      
        var cfg = Ext.applyIf(cfg, defaultConfig);
        var dataType = cfg.jitDataType.toLowerCase();
        switch (dataType) {
            case 'int': // 整数，数值靠右对齐
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderInt;
                }
                break;
            case 'decimal': //定点小数，数值靠右对齐，小数点后只保留两位
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDecimal;
                }
                break;
            case 'boolean': //布尔，如果是true则为是，为false则为否，否则为空。
                {
                    cfg.align = 'center';
                    cfg.renderer = this.renderBoolean;
                }
                break;
            case 'string': //字符串，文本靠左对齐。
                {
                    cfg.align = 'left';
                }
                break;
            case 'date': //日期，数据类型为Date。格式化为yyyy-MM-dd，即4位年+2位月+2位日
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDate;
                }
                break;
            case 'datetime': //日期时间，数据类型为Date。格式化为 yyyy-MM-dd HH:mi:ss，即4位年+2位月+2位日+2位24小时制的小时+2位分+2位秒。
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderDateTime;
                }
                break;
            case 'monthdayhourminute':
                {
                    cfg.align = 'right';
                    cfg.renderer = this.renderMonthDayHourMinute;
                }
                break;
            case 'time': //时间，数据类型为Date。格式化为HH:mi:ss，即2位24小时制的小时+2位分+2位秒。
                {
                    cfg.align = 'left';
                    cfg.renderer = this.renderTime;
                }
                break;
            case 'timespan':
                {
                    /* 时间跨度，数据为int，值为时间跨度的秒数。按 xxx天xxx小时xxx分xxx秒的方式格式化，最后只保留2节。
                    例如
                    	1天14小时
                    	1小时14分
                    	58分38秒
                    	44秒
                    */
                    cfg.align = 'left';
                    cfg.renderer = this.renderTimespan;
                }
                break;
            case 'coordinate': //地图列
                {
                    cfg.align = 'center';
                    var me = this;
                    cfg.renderer = function (val, metaData, record, rowIndex, colIndex, store, view) {
                        return me.renderCoordinate(val, metaData, record, rowIndex, colIndex, store, view, cfg.getMapTitle);
                    }
                }
                break;
            case 'photo': //图片列
                {
                    cfg.align = 'center';
                    cfg.renderer = this.renderPhoto;
                }
                break;

            default:
                Ext.Error.raise("无效的jitDataType的值:" + dataType + '.');
                break;
        }
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    }
    /*
    呈现整数
    */
    , renderInt: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null || val == "") {
            return "0";
        }
        else {
            var a = val.toString();
            var fuhao = a.indexOf("-")
            if (fuhao > -1) {
                a = a.substring(1);
            }
            var b = "";
            if (a.length > 3) {
                var sum = a.length;
                var c = a.substring(sum);
                var k = sum % 3;
                var j = parseInt(sum / 3);
                b = b + a.substring(0, k);
                for (var i = 0; i < j; i++) {
                    var one = (k + 3 * i);
                    var two = k + 3 * (i + 1);
                    var d = ",";

                    b = b + d + a.substring(one, two);
                }
                b = b + c;
            }
            else { b = a; }
            if (k == 0) {
                b = b.substring(1);
            }
            if (fuhao > -1) {
                b = "-" + b;
            }
            return b;
        }
    }
    /*
    呈现定点小数
    */
    , renderDecimal: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null || val == "") {
            return "0.00";
        }
        else {
            var a = parseFloat(val).toFixed(2).toString();
            var fuhao = a.indexOf("-")
            if (fuhao > -1) {
                a = a.substring(1);
            }
            var b = "";
            if (a.length > 3) {
                var sum = a.lastIndexOf('.');
                if (sum < 1) {
                    sum = a.length;
                }
                var c = a.substring(sum);
                var k = sum % 3;
                var j = parseInt(sum / 3);
                b = b + a.substring(0, k);
                for (var i = 0; i < j; i++) {
                    var one = (k + 3 * i);
                    var two = k + 3 * (i + 1);
                    var d = ",";
                    b = b + d + a.substring(one, two);
                }
                b = b + c;
            }
            else { b = a; }
            if (k == 0) {
                b = b.substring(1);
            }
            if (fuhao > -1) {
                b = "-" + b;
            }
            return b;
        }
    }
    /*
    呈现布尔值
    */
    , renderBoolean: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (val)
                return Jit.grid.column.Column.jitYesText;
            else
                return Jit.grid.column.Column.jitNoText;
        }
    }
    /*
    呈现日期
    */
    , renderDate: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'Y-m-d');
        }
    }
    /*
    呈现日期时间
    */
    , renderDateTime: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'Y-m-d H:i:s');
        }
    }
    /*
    呈现月日时分 06-04 18：08 
    */
    , renderMonthDayHourMinute: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'm-d H:i');
        }
    }
    /*
    呈现时间
    */
    , renderTime: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (typeof (val) != 'object' && val.constructor != Date) {
                val = Ext.Date.parse(val, 'c');
            }
            return Ext.Date.format(val, 'H:i:s');
        }
    }
    /*
    呈现时间跨度
    */
    , renderTimespan: function (val, metaData, record, rowIndex, colIndex, store, view) {
        if (val == null)
            return '';
        else {
            if (val > 3600) {
                return parseInt(val / 3600) + Jit.grid.column.Column.jitTimespanFormatter.hourText + parseInt((val - parseInt(val / 3600) * 3600) / 60) + Jit.grid.column.Column.jitTimespanFormatter.minuteText + (val - parseInt(val / 60) * 60) + Jit.grid.column.Column.jitTimespanFormatter.secondText;
            } else if (val > 60) {
                return parseInt(val / 60) + Jit.grid.column.Column.jitTimespanFormatter.minuteText + (val - parseInt(val / 60) * 60) + Jit.grid.column.Column.jitTimespanFormatter.secondText;
            } else {
                return val + Jit.grid.column.Column.jitTimespanFormatter.secondText;
            }
        }
    }
    /*
    呈现地图
    */
   , renderCoordinate: function (val, metaData, record, rowIndex, colIndex, store, view, getMapTitle) {
       if (val != null && val != "") {
           try {
               var title = null;
               if (getMapTitle && Ext.isFunction(getMapTitle)) {
                   title = getMapTitle(val, record);
               }
               var Values = val.split(",");
               var Lng = 0;
               var Lat = 0;
               var inGPSType = 0;
               if (Values.length > 1) {
                   Lng = Values[0];
                   Lat = Values[1];
               }
               if (Values.length == 3) {
                   inGPSType = Values[2];
               }
               //  var Id = this.columns[colIndex].getId();
               //Ext.getCmp(\"" + Id + "\").mapShow(" + Lng + "," + Lat + "," + inGPSType + ",\"" + title + "\")
               return "<img src='/Lib/Image/icon_world.gif' style='cursor:pointer' onclick='___fnMapShow(" + Lng + "," + Lat + "," + inGPSType + ",\"" + title + "\")' /> ";
           } catch (e) {
               return "<img src='/Lib/Image/icon_noworld.jpg' /> ";
           }
       }
       return "<img src='/Lib/Image/icon_noworld.jpg' /> ";
   }
    /*
    呈现照片
    */
    , renderPhoto: function (val, metaData, record, rowIndex, colIndex, store, view) {
        var photoValue = "";
        var pClientUserID=0;
        if (val != null && val != "") {
            try {
                if (val != null && val != "") {
                    var value = eval(val);
                    if (value != null && value.length > 0) {
                        photoValue = value[0].FileName;
                        pClientUserID=value[0].ClientUserID;
                    }
                }
                return "<img src='/Lib/Image/image.png' style='cursor:pointer' onclick='___fnPhotoShow(\"" + photoValue + "\","+pClientUserID+")' /> ";
            } catch (e) {
                return "<img src='/Lib/Image/noimage.png' /> ";
            }
        }
        return "<img src='/Lib/Image/noimage.png' /> ";
    }
});


___fnMapShow = function (pLng, pLat, pA, pTitle) {
    if (pTitle == null || pTitle == "" || pTitle == "null") {
        pTitle = "Map";
    }
    Ext.create('Jit.window.MapWindow', {
        id: '__columnMapID',
        title: pTitle,
        jitPoint: {
            pointID: '0',            //整数，唯一标识，必须
            lng: pLng,               // 浮点数，商店GPS坐标的经度，必须，范围0-180.
            lat: pLat,               // 浮点数，商店GPS坐标的纬度，必须，范围0-90             
            isEditable: false,       // 是否可编辑      
            inGPSType: pA,
            mapScale: 15           //地图级别
        }
    });
    Ext.getCmp("__columnMapID").show();
}

/*照片查看*/
___fnPhotoShow = function (value,clientUserID) {
    Ext.create('Jit.window.PhotoWindow', {
        id: '__columnPhotoID',
        title: '照片查看',
        pClientUserID:clientUserID,
        value: value
    });
    Ext.getCmp("__columnPhotoID").show();
}