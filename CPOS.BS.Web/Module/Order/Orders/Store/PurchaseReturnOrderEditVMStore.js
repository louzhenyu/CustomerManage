function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "purchaseReturnOrderEditStore",
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
            actionMethods: {
                read: 'POST'
            }
        }
    });

    Ext.create('Ext.data.Store', {
        storeId: "purchaseReturnOrderEditItemStore",
        model: "OrderDetailItemViewEntity",
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
            actionMethods: {
                read: 'POST'
            }
        }
    });
}