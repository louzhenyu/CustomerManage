﻿Ext.define("ContorlEventStatusEntity", {
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

// EventStatus 业务控件
Ext.define('jit.biz.EventStatus', {
    alias: 'widget.jitbizeventstatus',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlEventStatusEntity",
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
            args.dataType = "EventStatus";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/EventStatusHandler.ashx?method=" + args.dataType;
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