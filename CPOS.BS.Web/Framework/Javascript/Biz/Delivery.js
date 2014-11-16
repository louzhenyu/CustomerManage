Ext.define("ContorlDeliveryTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "DeliveryId",
        type: "int"
    }, {
        name: "DeliveryName",
        type: "string"
    }]
});

// EventType 业务控件
Ext.define('jit.biz.DeliveryType', {
    alias: 'widget.jitbizdelivery',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlDeliveryTypeEntity",
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
                            "DeliveryID": '',
                            "DeliveryName": "--请选择--"
                        });
                    //}
                }
            }
        });

            store.proxy.url = "/Framework/Javascript/Biz/Handler/DeliveryHandler.ashx?method=GetDeliveryType";
        store.load({
            //limit: 10,
            //page: 0
        });
        defaultConfig = {
            store: store
            , valueField: 'DeliveryId'
            , displayField: 'DeliveryName'
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