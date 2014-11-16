function InitStore() {

    //Ext.create('Ext.data.Store', {
    //    storeId: "titleStore",
    //    model: "ContorlOptionsEntity",
    //    data: [{OptionValue:1,OptionText:"YES"},{OptionValue:0,OptionText:"NO"}]
    //});

    Ext.create('Ext.data.Store', {
        storeId: "newsEditStore",
        model: "NewsViewEntity",
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