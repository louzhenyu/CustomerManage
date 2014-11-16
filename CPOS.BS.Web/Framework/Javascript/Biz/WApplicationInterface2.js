Ext.define("ContorlWApplicationInterfaceEntity2", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"WeiXinID",
        type: "string"
    }, {
        name: "value",
        mapping:"WeiXinID",
        type: "string"
    }, {
        name: "text",
        mapping:"WeiXinName",
        type: "string"
    }]
});

// WApplicationInterface 业务控件
Ext.define('jit.biz.WApplicationInterface2', {
    alias: 'widget.jitbizwapplicationinterface2',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlWApplicationInterfaceEntity2",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store) {
                    //if (args.isDefault != null && args.isDefault) {
                        store.insert(0, {
                            "name": args.Name,
                            "value": '',
                            "text": "--请选择--"
                        });
                    //}
                }
            }
        }); 
        if (args.dataType == undefined || args.dataType == null) {
            args.dataType = "get_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/WApplicationInterfaceHandler.ashx?method=" + args.dataType;
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
                    if (fn != undefined && fn != null) {
                        fn();
                    }
                }
            });
        };
        result.setDefaultValue = function(defValue) {
            store.load({
                sync: true, async: false,
                params: {  }
                ,callback: function(r, options, success) {
                    for (var i = 0; i < r.length; i++) {
                        var rawValue = r[i].data.id;
                        if (rawValue == defValue) {
                            result.setValue(rawValue);
                        }
                    }
                    if (args.fn != undefined && args.fn != null) {
                        args.fn();
                    }
                }
            });
        };

        return result;
  }
})