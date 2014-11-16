/*Jermyn 2013-04-02 调拨单类型*/

Ext.define("ContorlMvOrderTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping: "order_type_id",
        type: "string"
    }, {
        name: "value",
        mapping: "order_type_id",
        type: "string"
    }, {
        name: "text",
        mapping: "order_type_name",
        type: "string"
    }]
});

// AppSys 业务控件
Ext.define('jit.biz.MvOrderType', {
    alias: 'widget.jitbizmvordertype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            model: "ContorlMvOrderTypeEntity",
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
            args.dataType = "order_type_mv_inout";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/MvOrderType.ashx?method=" + args.dataType;
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

        args.listeners = {
            select: function (combo, record, index) {
                if (args.selectFn != undefined) args.selectFn();
            }
        };

        var result = Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function () {
            store.load({
                params: {}
            });
        };

        return result;
    }
})

