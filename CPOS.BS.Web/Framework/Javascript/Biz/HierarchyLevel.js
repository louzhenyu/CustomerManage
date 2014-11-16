Ext.define("ContorlHierarchyLevelEntity", {
    extend: "Ext.data.Model",
    fields: [{
        name: "LevelName",
        type: "string"
    }, {
        name: "ClientHierarachyLevelID",
        type: "string"
    }]
});

//HierarchyLevel业务控件
Ext.define('Jit.biz.HierarchyLevel', {
    alias: 'widget.jitbizhierarchylevel',
    constructor: function (args) {
        var argsConfig = { emptyText: '--请选择--' };
        args = Ext.applyIf(args, argsConfig);
        var store = new Ext.data.Store({
            storeId: args.storeId,
            pageSize: args.pageSize || 15,
            model: "ContorlHierarchyLevelEntity",
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
                         "ClientHierarachyLevelID": "0",
                         "LevelName": "--请选择--"
                        });
                    }
                }
            }
        });
     store.proxy.url = "/Framework/Javascript/Biz/Handler/HierarchyLevelHandler.ashx?method=GetHierarchyLevelByHierarchyID&HierarchyID=" + args.HierarchyID;
        store.load({
            limit: 1,
            page: 1
        });
        defaultConfig = {
            store: store
            , valueField: 'ClientHierarachyLevelID'
            , displayField: 'LevelName'
        };      
        args = Ext.applyIf(args, defaultConfig);
        return Ext.create('Jit.form.field.ComboBox', args);
  }
})