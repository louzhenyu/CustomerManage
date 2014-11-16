function InitStore() {
    //加解密字段，加解密方式
    debugger;
    Ext.create('Ext.data.Store', {
        storeId: "EncryptTypeStore",
        model: "EncryptModel",
        data: [
                { Id: '0', Name: '明文模式' },
                { Id: '1', Name: '兼容模式' },
                { Id: '2', Name: '安全模式' }
        ]
    });
    Ext.create('Ext.data.Store', {
        storeId: "wApplicationEditStore",
        model: "WApplicationViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "data",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}