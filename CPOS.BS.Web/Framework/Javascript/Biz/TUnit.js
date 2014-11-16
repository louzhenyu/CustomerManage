Ext.define("ContorlTUnitEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "unit_id",
        type: "string"
    }, {
        name: "unit_name",
        type: "string"
    }]
});

// SupplierUnit 业务控件
Ext.define('jit.biz.CustomerUnit', {
    alias: 'widget.jitbiztunit',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlTUnitEntity",
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
                        "unit_id": '',
                        "unit_name": "--请选择--"
                    });
                    //}
                }
            }
        });
        if (args.Type == undefined || args.Type == null) {
            args.Type = "配送商";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/TUnitHandler.ashx?method=GetTUnit&Type=" + args.Type;
        store.load({
            //limit: 10,
            //page: 0
        });
        defaultConfig = {
            store: store
            , valueField: 'unit_id'
            , displayField: 'unit_name'
        };
        args = Ext.applyIf(args, defaultConfig);
        return Ext.create('Jit.form.field.ComboBox', args);
    }
})