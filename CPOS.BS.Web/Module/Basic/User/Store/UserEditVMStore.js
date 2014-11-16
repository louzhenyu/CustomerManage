function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "userEditStore",
        model: "UserViewEntity",
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
        storeId: "userEditRoleStore",
        model: "UserRoleViewEntity",
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