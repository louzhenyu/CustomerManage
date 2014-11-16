function InitStore() {
    new Ext.create('Ext.data.Store', {
        storeId: "tfStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
        data: [
        { "name": "否", "value": 0 },
        { "name": "是", "value": 1 }
    ]
    });

    new Ext.data.Store({
        storeId: "stepStore",
        model: "VisitingTaskStepEntity"
    });

}