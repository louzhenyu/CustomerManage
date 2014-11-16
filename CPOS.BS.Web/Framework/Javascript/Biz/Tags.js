Ext.define("ContorlTagsEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"TagsId",
        type: "string"
    }, {
        name: "value",
        mapping:"TagsId",
        type: "string"
    }, {
        name: "text",
        mapping:"TagsName",
        type: "string"
    }]
});

// OrderStatus 业务控件
Ext.define('jit.biz.Tags', {
    alias: 'widget.jitbiztags',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlTagsEntity",
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
        if (args.orderType == undefined || args.orderType == null) {
            args.orderType = "";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/TagsHandler.ashx?method=" + args.orderType;
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