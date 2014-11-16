function InitStore() {
    
    Ext.create('Ext.data.Store', {
        storeId: "eventsRoundListStore",
        model: "RoundViewEntity",
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
        storeId: "RoundPrizesStore",
        model: "RoundPrizesViewEntity",
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