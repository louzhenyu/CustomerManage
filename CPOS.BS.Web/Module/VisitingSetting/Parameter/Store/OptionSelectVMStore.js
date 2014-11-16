function InitOptionSelectStore() {
    new Ext.data.Store({
        storeId: "optionsListStore",
        model: "OptionsEntity",
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