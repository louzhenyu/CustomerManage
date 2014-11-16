function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "salesOrderEditStore",
        model: "SalesOrderViewEntity",
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
        storeId: "salesOrderEditItemStore",
        model: "SalesOrderDetailItemViewEntity",
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