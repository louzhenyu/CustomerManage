function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "ccOrderEditStore",
        model: "CcOrderViewEntity",
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
        storeId: "ccOrderEditItemStore",
        model: "CcOrderDetailItemViewEntity",
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