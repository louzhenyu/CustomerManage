Ext.define("ContorlOrderStatusEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"Status",
        type: "string"
    }, {
        name: "value",
        mapping:"Status",
        type: "string"
    }, {
        name: "text",
        mapping:"Description",
        type: "string"
    }]
});

// OrderStatus 业务控件
Ext.define('jit.biz.OrderStatus', {
    alias: 'widget.jitbizorderstatus',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlOrderStatusEntity",
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
        store.proxy.url = "/Framework/Javascript/Biz/Handler/OrderStatusHandler.ashx?method=" + args.orderType;
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
        return Ext.create('Jit.form.field.ComboBox', args);
  }
})