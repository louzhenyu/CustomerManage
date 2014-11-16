function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "menuStore",
        model: "MenuViewEntity",
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

    Ext.create('Ext.data.TreeStore', {
        proxy: {
            type: 'ajax',
            url: ''
        },
        listeners: {
            beforeload: {
                fn: function (store, operation, options) {
                    operation.params.multiSelect = false;
                    operation.params.isSelectLeafOnly = false;    //告诉后台是否为只选择叶子节点
                }
            }
        },
        autoLoad: false,
        storeId: 'meauTreeStore'
    });

}


