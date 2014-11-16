Ext.define("ContorlAlbumTypeEntity", {
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
// AlbumType 业务控件
Ext.define('jit.biz.AlbumType', {
    alias: 'widget.jitbizalbumtype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlAlbumTypeEntity",
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
            args.dataType = "AlbumType";
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

// AlbumType 业务控件
Ext.define('jit.biz.AlbumTypeLink', {
    alias: 'widget.jitbizalbumtypelink',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlAlbumTypeEntity",
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
            args.dataType = "AlbumType";
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

    args.listeners = {
        select: function (combo, record, index) {
            if (combo.value == 2) {
                document.getElementById('inputContent').style.display = "";
                document.getElementById('fpicture').style.display = "none";
                document.getElementById('tPicType').style.display = "none";
            }
            else if (combo.value == 1) {
                document.getElementById('inputContent').style.display = "none";
                document.getElementById('fpicture').style.display = "";
                document.getElementById('tPicType').style.display = "";
            }
            else {
                document.getElementById('fpicture').style.display = "none";
                document.getElementById('tPicType').style.display = "none";
            }
        }
    };

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