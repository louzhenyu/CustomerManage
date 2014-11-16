function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "routeStore",
        model: "RouteViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: "",
                ClientStructureID:"",
                ClientUserID:""
            },
            actionMethods: { read: 'POST' }
        }
    });
}