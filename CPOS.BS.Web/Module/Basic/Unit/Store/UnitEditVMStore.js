function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "unitEditStore",
        model: "UnitViewEntity",
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
        storeId: "itemEditBrandStore",
        model: "ItemBrandViewEntity",
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