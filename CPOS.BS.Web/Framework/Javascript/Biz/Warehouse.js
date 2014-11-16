Ext.define("ContorlWarehouseEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"warehouse_id",
        type: "string"
    }, {
        name: "value",
        mapping:"warehouse_id",
        type: "string"
    }, {
        name: "text",
        mapping:"wh_name",
        type: "string"
    }]
});

// Warehouse 业务控件
Ext.define('jit.biz.Warehouse', {
    alias: 'widget.jitbizwarehouse',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlWarehouseEntity",
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
                },
                expand: function(combo, record, index) {
                     try {
                         store.load({
                            params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
                         });
                     }
                     catch (ex) {
                         Ext.MessageBox.alert("错误", "错误:" + ex);
                     }
                }
            }
        });
        store.proxy.url = "/Framework/Javascript/Biz/Handler/WarehouseHandler.ashx?method=get_warehouse";
        store.load({
            //limit: 10,
            //page: 0,
            params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
        });
        defaultConfig = {
            store: store
            , valueField: 'value'
            , displayField: 'text'
        };      
        args = Ext.applyIf(args, defaultConfig);

        args.listeners = {
            expand: function(combo, record, index) {
                //alert(Ext.getCmp(args.parent_id).jitGetValue());
                try {
                    store.load({
                        params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
                    });
                }
                catch (ex) {
                    Ext.MessageBox.alert("错误", "错误:" + ex);
                }
            }
        };
        var result= Ext.create('Jit.form.field.ComboBox', args);
        result.store = store;
        result.fnLoad = function() {
            store.load({
                params: { pid: Ext.getCmp(args.parent_id).jitGetValue() }
            });
        };

        return result;
  }
})