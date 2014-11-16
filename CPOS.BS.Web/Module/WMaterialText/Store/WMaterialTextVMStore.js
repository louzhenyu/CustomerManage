function InitStore() {
    new Ext.data.Store({
        storeId: "WmateriaStore",
        model: "WMaterialTextEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        }
    });

    new Ext.data.Store({
        storeId: "TypeStore",
        model: "WMaterialTextTypeEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}