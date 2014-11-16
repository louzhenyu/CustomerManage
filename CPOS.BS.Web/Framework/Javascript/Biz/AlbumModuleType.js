Ext.define("ContorlAlbumModuleTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping: "Id",
        type: "string"
    }, {
        name: "value",
        mapping: "Id",
        type: "string"
    }, {
        name: "text",
        mapping: "Description",
        type: "string"
    }]
});

// AlbumModuleType 业务控件
Ext.define('jit.biz.AlbumModuleType', {
    alias: 'widget.jitbizalbummoduletype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlAlbumModuleTypeEntity",
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
            args.dataType = "AlbumModuleType";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/AlbumModuleTypeHandler.ashx?method=" + args.dataType;
        store.load({
        });
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };
        args = Ext.applyIf(args, defaultConfig);

        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function (fn) {
            store.load({
                params: {}
                , callback: function (r, options, success) {
                    if (fn != undefined && fn != null) {
                        fn();
                    }
                }
            });
        };
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