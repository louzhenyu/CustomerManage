function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "taskDataStore",
        model: "VisitingTaskDataSummaryViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                ClientStructureID: "",
                ClientPositionID: "",
                ClientUserID: "",
                ExecutionTime:""
            },
            actionMethods: { read: 'POST' }
        }
    });
}