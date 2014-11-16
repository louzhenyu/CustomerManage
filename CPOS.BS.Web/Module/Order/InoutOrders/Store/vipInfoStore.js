function InitvipStore() {

    Ext.create('Ext.data.Store', {
        storeId: "vipStore",
        model: 'vipEntity'
    });    
}