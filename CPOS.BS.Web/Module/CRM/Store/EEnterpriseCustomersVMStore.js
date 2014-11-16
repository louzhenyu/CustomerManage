function InitStore() {

    Ext.create('Ext.data.Store', {
        storeId: "EEnterpriseCustomersStore",
        model: "EEnterpriseCustomersViewEntity",
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
        storeId: "VipEnterpriseStore",
        model: "VipEnterpriseViewEntity",
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
        storeId: "ObjectDownloadsStore",
        model: "ItemImageViewEntity",
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
        storeId: "ESalesVisitVipStore",
        model: "ESalesVisitVipViewEntity",
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
}