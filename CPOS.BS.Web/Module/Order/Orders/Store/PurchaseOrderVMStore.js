﻿function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "purchaseOrderStore",
        model: "OrderViewEntity",
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