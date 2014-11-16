function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "routeStore",
        model: "RouteViewEntity",
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


    new Ext.create('Ext.data.Store', {
        storeId: "stateStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
        data: [
        { "name": "启用", "value": 1 },
        { "name": "停止", "value": 2 }
    ]
    });

    new Ext.create('Ext.data.Store', {
        storeId: "cycleStore",
        model: "CycleEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });

    new Ext.create('Ext.data.Store', {
        storeId: "cycleDetailStore",
        model: "CycleDetailEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}