function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "WModelListStore",
        model: "WModelListViewEntity",
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