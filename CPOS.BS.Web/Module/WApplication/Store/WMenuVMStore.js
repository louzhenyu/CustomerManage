function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "wMenuStore",
        model: "WMenuViewEntity",
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
        storeId: "WMenuParentStore",
        model: "WMenuViewEntity",
        root: {  
            expanded: true  
        },
        proxy: {
            type: 'ajax',
            url: '',
            reader: {
                type: 'json',
                root: "children"
            },
            actionMethods: { read: 'POST' }
        }
    });

}