function InitStore() {
    
    Ext.create('Ext.data.Store', {
        storeId: "eventsLotteryLogListStore",
        model: "LotteryLogViewEntity",
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