function InitStore() {
    new Ext.data.Store({
        storeId: "LNewsTypeStore",
        model: "LNewsTypeEntity",
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

    new Ext.data.Store({
        storeId: "searchPartentTypeStore",
        model: "ParentTypeEntit",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
    new Ext.data.Store({
        storeId: "PartentTypeStore",
        model: "ParentTypeEntit",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}