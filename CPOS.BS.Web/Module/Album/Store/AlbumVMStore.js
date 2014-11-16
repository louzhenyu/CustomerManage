function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "AlbumStore",
        model: "AlbumViewEntity",
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
            actionMethods: {
                read: 'POST'
            }
        }
    });

    Ext.create('Ext.data.Store', {
        storeId: "AlbumModuleStore",
        model: "AlbumModuleViewEntity",
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
            actionMethods: {
                read: 'POST'
            }
        }
    });

    Ext.create('Ext.data.Store', {
        storeId: "AlbumImagesStore",
        model: "AlbumImageViewEntity",
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
            actionMethods: {
                read: 'POST'
            }
        }
    });
    new Ext.data.Store({
        storeId: "AlbumITypeStore",
        model: "AlbumTypeEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
        
            }
        }
    });
}