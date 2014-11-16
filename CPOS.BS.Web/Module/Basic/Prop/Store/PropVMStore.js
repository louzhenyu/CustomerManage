function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "PropStore",
        model: "PropViewEntity2",
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
        storeId: "PropParentStore",
        model: "PropViewEntity",
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
    
    Ext.create('Ext.data.Store', {
        storeId: "BrandDetailStore",
        model: "BrandDetailViewEntity",
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
        storeId: "itemEditImageStore",
        model: "ItemImageViewEntity",
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