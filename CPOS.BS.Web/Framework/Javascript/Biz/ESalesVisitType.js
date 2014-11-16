Ext.define("ContorlESalesVisitTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"VisitTypeId",
        type: "string"
    }, {
        name: "value",
        mapping:"VisitTypeId",
        type: "string"
    }, {
        name: "text",
        mapping:"VisitTypeName",
        type: "string"
    }]
});

// ESalesVisitType 业务控件
Ext.define('jit.biz.ESalesVisitType', {
    alias: 'widget.jitbizesalesvisittype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlESalesVisitTypeEntity",
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
        store.proxy.url = "/Framework/Javascript/Biz/Handler/ESalesVisitTypeHandler.ashx?method=" + args.dataType;
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