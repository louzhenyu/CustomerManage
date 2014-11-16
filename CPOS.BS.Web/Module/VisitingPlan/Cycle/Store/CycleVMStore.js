function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "listStore",
        model: "CycleEntity",
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
            actionMethods: { read: 'POST' }
        }
    });

    new Ext.data.Store({
        storeId: "editStore",
        model: "CycleEntity"
    });

    new Ext.create('Ext.data.Store', {
        storeId: "cycleTypeStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
        data: [
        { "name": "周", "value": 1 },
        { "name": "月", "value": 2 },
        { "name": "自定义", "value": 3 }
    ]
    });
}