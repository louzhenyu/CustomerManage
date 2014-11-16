Ext.define('Jit.biz.MapSelect', {
    alias: 'widget.jitbizmapselect',
    textField: null,
    button: null,
    map: null,
    constructor: function (args) {
        if (args.width == null) { args.width = 183; }
        var me = this;
        var defaultConfig = {
            id: 'mapSelect'   //panel 的id 默认为mapSelect
            , fieldLabel: '地图选择'  //textField 的fieldLabel
            , text: '选 择'  //button的显示文本
            , renderTo: null //panel的renderTo
            , width: args.width
        }
        args = Ext.applyIf(args, defaultConfig);

        //文本框
        me.textField = Ext.create('Jit.form.field.Text', {
            fieldLabel: args.fieldLabel
            , jitSize: 'small'
            , margin: '0 0 0 0'
            , readOnly: true
            , width: (args.width - 17)
        });

        me.textField.addListener('focus', function () {
            if (args.readOnly == false || args.readOnly == null) {
                me.map.show();
                me.map._map_RemoveStores();
                me.map.jitSetValue(instance.jitGetValue());
                me.map._map_LoadMap();
            }
        });
        //按钮
        me.button = Ext.create('Jit.button.Button', {
            text: ''
            , margin: '0 0 0 0'
            , height: 22
            , width: 17
            , border: 0
            , imgName: 'select'
            , handler: function () {
                if (args.readOnly == false || args.readOnly == null) {
                    me.map.show();
                    me.map._map_RemoveStores();
                    me.map.jitSetValue(instance.jitGetValue());
                    me.map._map_LoadMap();
                }
            }
        });
        //地图控件
        me.map = Ext.create('Jit.window.MapWindow', {
            id: '__selectMapID' + args.id,
            title: args.mapTitle,
            jitPoint: {
                pointID: '0',           //整数，唯一标识，必须
                lng: 0,                 //浮点数，商店GPS坐标的经度，必须，范围0-180.
                lat: 0,                 //浮点数，商店GPS坐标的纬度，必须，范围0-90            
                isEditable: true        //是否可拖拽  
            },
            handler: function (pObj) {
                if (pObj.lng != "" && pObj.lat != "") {
                    me.textField.jitSetValue(pObj.lng + "," + pObj.lat);
                } else {
                    me.textField.jitSetValue("");
                }
                me.map.hide();
            }
        });

        //panel的默认属性
        var panelConfig = {
            items: [me.textField, me.button],
            margin: '0 10 10 10',
            width: 300,
            height: 22,
            layout: 'column',
            border: 0
        }
        args = Ext.applyIf(args, panelConfig);

        //创建panel对象
        var instance = Ext.create('Ext.panel.Panel', args);

        /*
        查询数据  数据格式 {"Lat":123.32,"Lng":32.12,"Type":1}
        */
        instance.jitGetValue = function () {
            var pValue = me.textField.jitGetValue();
            if (pValue != null && pValue != "") {
                return pValue;
            } else {
                return "";
            }
        }

        /*
        设置数据  老数据格式 {"Lng":123.32,"Lat":32.12,"Type":1}新数据格式(经度,纬度)
        */
        instance.jitSetValue = function (pValue) {

            if (pValue != null && pValue != "") {
                // var pJsonValue = eval("(" + pValue + ")");
                me.textField.jitSetValue(pValue);
            } else {
                me.textField.jitSetValue("");
            }
        }
        return instance;
    }
});