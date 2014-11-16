﻿function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "purchaseInOrderItemStore",
        model: "InoutOrderDetailItemViewEntity",
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