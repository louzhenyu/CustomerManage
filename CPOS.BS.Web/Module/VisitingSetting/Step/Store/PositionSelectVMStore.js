function InitStore() {

    new Ext.data.Store({
        storeId: "positionStore",
        model: "ClientPositionEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: "",
                id: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}