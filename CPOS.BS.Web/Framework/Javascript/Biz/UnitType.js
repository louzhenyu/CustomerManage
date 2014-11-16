Ext.define("ContorlUnitTypeEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "id",
        mapping:"Type_Id",
        type: "string"
    }, {
        name: "value",
        mapping:"Type_Id",
        type: "string"
    }, {
        name: "text",
        mapping:"Type_Name",
        type: "string"
    }]
});

// OrderStatus 业务控件
Ext.define('jit.biz.UnitType', {
    alias: 'widget.jitbizunittype',
    constructor: function (args) {
        var store = new Ext.data.Store({
            storeId: args.storeId,
            //pageSize: args.pageSize || 15,
            model: "ContorlUnitTypeEntity",
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
            args.orderType = "get_unit_type_list";
        }
        store.proxy.url = "/Framework/Javascript/Biz/Handler/UnitTypeHandler.ashx?method=" + args.orderType;
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