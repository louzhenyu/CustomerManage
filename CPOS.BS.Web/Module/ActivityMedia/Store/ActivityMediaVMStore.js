function InitStore() {
    new Ext.data.Store({
        storeId: "ActivityMediaStore",
        model: "ActivityMediaEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: "",
                ClientPositionID: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}