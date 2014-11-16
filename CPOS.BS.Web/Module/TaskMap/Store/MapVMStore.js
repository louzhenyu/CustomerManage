function InitStore() {
    new Ext.data.Store({
        storeId: "taskStore",
        model: "MapAnalysisEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}