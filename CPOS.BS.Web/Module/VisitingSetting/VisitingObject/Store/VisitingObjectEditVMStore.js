function InitStore() {

    new Ext.data.Store({
        storeId: "objectStore",
        model: "VisitingObjectEntity"
    });

    new Ext.create('Ext.data.Store', {
        storeId: "stateStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
        data: [
        { "name": "未启用", "value": 0 },
        { "name": "启用", "value": 1 }
    ]
    });

    new Ext.data.Store({
        storeId: "objectParentStore",
        model: "VisitingObjectEntity"
    });
}