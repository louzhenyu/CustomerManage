function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderEditStore",
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

    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderEditItemStore",
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