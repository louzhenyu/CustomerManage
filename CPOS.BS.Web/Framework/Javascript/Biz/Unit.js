Ext.define("ContorlUnitEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "UnitID",
        type: "string"
    }, {
        name: "UnitName",
        type: "string"
    }]
});

//Options业务控件
Ext.define('Jit.biz.Unit', {
    alias: 'widget.jitbizunit',
    constructor: function (args) {
        var argsConfig = { emptyText: '--请选择--', isDefaultLoad: false, __isLoad: false };
        args = Ext.applyIf(args, argsConfig);
        var store = new Ext.data.Store({
            storeId: args.storeId,
            pageSize: args.pageSize || 15,
            model: "ContorlUnitEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json'
                }
            },
            listeners: {
                load: function (store) {
                    if (args.isDefault != null && args.isDefault) {
                        store.insert(0, {                      
                         "UnitID": "",
                         "UnitName": "--请选择--"
                        });
                    }
                }
            }
        });
     store.proxy.url = "/Framework/Javascript/Biz/Handler/UnitHandler.ashx?method=GetUnitList";
        store.load({
            limit: 1,
            page: 1
        });
        defaultConfig = {
            store: store
            , valueField: 'UnitID'
            , displayField: 'UnitName'
        };      
        args = Ext.applyIf(args, defaultConfig);
        var instance=Ext.create('Jit.form.field.ComboBox', args);
        instance.jitSetValue = function (value) {
            if (value != null && value != "") {
                instance.__fnStoreLoad(value);
            } else {
                instance.setValue("");
            }
        };
        instance.__fnStoreLoad = function (value) {
            //判断当前是否已经加载数据 __isLoad 为true则加载数据,为false则未加载数据
            if (args.__isLoad == null || !args.__isLoad) {
                store.load({
                    callback: function () {
                        args.__isLoad = true; //加载数据完成
                        if (args.callback != null && args.callback != "") {
                            args.callback();
                        }
                        if (value != null && value != "") {
                            var values = value.toString().toLowerCase().split(',');
                            instance.setValue(values);
                        }
                    }
                });
            } else {
                if (value != null && value != "") {
                    var values = value.toString().toLowerCase().split(',');
                    instance.setValue(values);
                }
            }
        }
        /*判断是否默认初始化加载Store*/
        if (args.isDefaultLoad != null && args.isDefaultLoad) {
            instance.__fnStoreLoad("");
        } else {
            instance.on("focus", function () { instance.__fnStoreLoad(""); }    /*添加点击事件*/);
        }
        return instance
  }
})