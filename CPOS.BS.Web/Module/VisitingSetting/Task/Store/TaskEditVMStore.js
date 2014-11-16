function InitStore() {
    new Ext.data.Store({
        storeId: "taskStore",
        model: "TaskViewEntity"
    }); 

    new Ext.create('Ext.data.Store', {
        storeId: "isCombinStore",
        fields: [{name:'name',type:"string"}, {name:'value',type:"int"}],
        data: [
        { "name": "与其它任务合并", "value": 1 },
        { "name": "与其它任务不合并", "value": 0 }
    ]
    });
}