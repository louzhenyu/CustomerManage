function InitStore() {
    new Ext.data.Store({
        storeId: "optionsDefinedStore",
        model: "OptionsDefinedEntity",
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
        storeId: "fixedoptionsDefinedStore",
        model: "OptionsDefinedEntity",
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
        storeId: "optionsEditStore",
        pageSize: 15,
        model: "OptionsEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });


}