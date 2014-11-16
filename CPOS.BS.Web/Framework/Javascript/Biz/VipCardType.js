Ext.define("ContorlVipCardTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping: "VipCardTypeID",
        type: "string"
    }, {
        name: "value",
        mapping: "VipCardTypeID",
        type: "string"
    }, {
        name: "text",
        mapping: "VipCardTypeName",
        type: "string"
    }]
});

// VipCardType 业务控件
Ext.define('jit.biz.VipCardType', {
    alias: 'widget.jitbizvipcardtype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlVipCardTypeEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            listeners: {
                load: function (store) {
                    store.insert(0, {
                        "name": args.Name,
                        "value": '',
                        "text": "--请选择--"
                    });
                }
            }
        });
        if (args.dataType == undefined || args.dataType == null) {
            args.dataType = "VipCardType";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/VipCardTypeHandler.ashx?method=" + args.dataType;
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

        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.setDefaultValue = function (defValue) {
            store.load({
                params: {}
                , callback: function (r, options, success) {
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