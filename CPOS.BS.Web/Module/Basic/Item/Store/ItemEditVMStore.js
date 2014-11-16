function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "itemEditStore",
        model: "ItemViewEntity",
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
    
    Ext.create('Ext.data.Store', {
        storeId: "itemEditPriceStore",
        model: "ItemPriceViewEntity",
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
    //图片store
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
    
    Ext.create('Ext.data.Store', {
        storeId: "itemEditUnitStore",
        model: "ItemUnitViewEntity",
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

    Ext.create('Ext.data.TreeStore', {
        storeId: "categoryStore",
        model: 'ItemCategoryViewEntity',  
        proxy: {
            type: 'ajax',
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        },
        folderSort: true       
    }); 

    Ext.create('Ext.data.TreeStore', {
        storeId: "itemTagStore",
        model: 'ItemTagViewEntity',
        proxy: {
            type: 'ajax',
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        },
        folderSort: true
    });
}