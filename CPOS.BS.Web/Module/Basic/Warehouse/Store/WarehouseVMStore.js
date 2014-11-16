function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "warehouseStore",
        model: "WarehouseViewEntity",
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
}