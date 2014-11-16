function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "VipCardStore",
        model: "VipCardEntity",
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