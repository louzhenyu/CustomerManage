function InitStore() {
    new Ext.data.Store({
        storeId: "vipClearStore",
        model: "VIPClearEntity",
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