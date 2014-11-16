function InitStore() {
    new Ext.data.Store({
        storeId: "ordersStore",
        model: "OrdersEntity",
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
        storeId: "typeStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
        data: [
            { "name": "--请选择--", "value": 0 },
            { "name": "已提交认证", "value": 12 },
            { "name": "未提交认证", "value": 11}
        ]
    });


    new Ext.create('Ext.data.Store', {
        storeId: "statusStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
        data: [
            { "name": "--请选择--", "value": 0 },
            { "name": "通过", "value": 13 },
            { "name": "不通过", "value": 14 }
        ]
    });
}