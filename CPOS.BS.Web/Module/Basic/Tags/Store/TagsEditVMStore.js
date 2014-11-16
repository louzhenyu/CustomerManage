function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "tagsEditStore",
        model: "TagsViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "data",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
    

}