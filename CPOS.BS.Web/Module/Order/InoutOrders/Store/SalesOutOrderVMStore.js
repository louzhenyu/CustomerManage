function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderStore",
        model: "InoutOrderEntity",
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
        storeId: "payStatusStore",
        fields: ['name', 'value'],
        data: [
        { "name": "--请选择--", "value": "" },
        { "name": "已付款", "value": "1" },
        { "name": "未付款", "value": "0"}]
    });
    
    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderStore1",
        model: "InoutOrderEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",   //数据的root
                totalProperty: "totalCount"  //总量的数字
            },
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
    
    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderStore2",
        model: "InoutOrderEntity",
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

    
    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderStore3",
        model: "InoutOrderEntity",
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

    
    Ext.create('Ext.data.Store', {
        storeId: "salesOutOrderStore4",
        model: "InoutOrderEntity",
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
}