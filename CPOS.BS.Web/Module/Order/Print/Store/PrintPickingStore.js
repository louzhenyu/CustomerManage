function InitStore() {
    new Ext.data.Store({
        storeId: "PrintPickingStore",
        model: "PrintPickingEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "data",
                totalProperty: "totalCount"
            },
            extraParams: {
                orderID: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}