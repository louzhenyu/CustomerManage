Ext.define("ContorlSysIntegralSourceEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"Id",
        type: "string"
    }, {
        name: "value",
        mapping:"Id",
        type: "string"
    }, {
        name: "text",
        mapping:"Description",
        type: "string"
    }]
});

// SysIntegralSource 业务控件
Ext.define('jit.biz.SysIntegralSource', {
    alias: 'widget.jitbizsysintegralsource',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlSysIntegralSourceEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store) {
                    if (args.multiSelect == null || !args.multiSelect) {
                        store.insert(0, {
                            "name": args.Name,
                            "value": '',
                            "text": "--请选择--"
                        });
                    }
                }
            }
        }); 
        store.proxy.url = "/Framework/Javascript/Biz/Handler/SysIntegralSourceHandler.ashx?method=" + args.dataType + "&typeCode=" + args.typeCode;
        store.load({
            //limit: 10,
            //page: 0
        });
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);
        
        var result= Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function(fn) {
            store.load({
                params: {  }
                ,callback: function(r, options, success) {
                    //alert("123");
                    if (fn != undefined && fn != null) {
                        fn();
                    }
                }
            });
        };
        result.setDefaultValue = function(defValue) {
            store.load({
                params: {  }
                ,callback: function(r, options, success) {
                    for (var i = 0; i < r.length; i++) {
                        var rawValue = r[i].data.id;
                        if (rawValue == defValue) {
                            result.setValue(rawValue);
                        }
                    }
                }
            });
        };

        return result;
  }
})