function InitStore() {
    new Ext.data.Store({
        storeId: "taskStore",
        model: "TaskViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });

    new Ext.data.Store({
        storeId: "taskStepStore",
        model: "StepViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}