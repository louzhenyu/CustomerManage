Ext.define("ContorlHierarchyEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "HierarchyName",
        type: "string"
    }, {
        name: "ClientHierarchyID",
        type: "string"
    }]
});

//Hierarchy业务控件
Ext.define('Jit.biz.Hierarchy', {
    alias: 'widget.jitbizhierarchy',
    constructor: function (args) {
        var argsConfig = { emptyText: '--请选择--' };
        args = Ext.applyIf(args, argsConfig);
        var store = new Ext.data.Store({
            storeId: args.storeId,
            pageSize: args.pageSize || 15,
            model: "ContorlHierarchyEntity",
            proxy: {
                type: 'ajax',
                reader: {
                    type: 'json'
                }
            },
            listeners: {
                load: function (store) {
                    if (args.isDefault != null && args.isDefault) {
                        store.insert(0, {
                         "ClientHierarchyID": "0",
                         "HierarchyName": "--请选择--"
                        });
                    }
                }
            }
        });
     store.proxy.url = "/Framework/Javascript/Biz/Handler/HierarchyHandler.ashx?method=GetHierarchyByType&HierarchyType=" + args.HierarchyType;
        store.load({
            limit: 1,
            page: 1
        });
        defaultConfig = {
            store: store
            , valueField: 'ClientHierarchyID'
            , displayField: 'HierarchyName'
        };      
        args = Ext.applyIf(args, defaultConfig);
        return Ext.create('Jit.form.field.ComboBox', args);
  }
})