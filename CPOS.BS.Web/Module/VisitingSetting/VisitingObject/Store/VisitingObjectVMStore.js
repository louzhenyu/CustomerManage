function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "list_objectStore",
        model: "VisitingObjectEntity",
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