function InitStore() {
    new Ext.data.Store({
        storeId: "EventStatsStore",
        model: "EventStatsEntity",
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
        storeId: "titleStore",
        model: "TitleEntit",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
    new Ext.data.Store({
        storeId: "serchStore",
        model: "TitleEntit",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}