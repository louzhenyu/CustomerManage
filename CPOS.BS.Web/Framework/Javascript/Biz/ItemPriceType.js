Ext.define("ContorlItemPriceTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"item_price_type_id",
        type: "string"
    }, {
        name: "value",
        mapping:"item_price_type_id",
        type: "string"
    }, {
        name: "text",
        mapping:"item_price_type_name",
        type: "string"
    }]
});

// ItemPriceType 业务控件
Ext.define('jit.biz.ItemPriceType', {
    alias: 'widget.jitbizitempricetype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlItemPriceTypeEntity",
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
            args.orderType = "get_item_price_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/ItemPriceTypeHandler.ashx?method=" + args.orderType;
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