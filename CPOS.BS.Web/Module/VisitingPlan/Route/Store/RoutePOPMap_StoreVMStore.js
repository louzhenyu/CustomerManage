function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "storeStore",
        model: "storeEntity"
    });
}