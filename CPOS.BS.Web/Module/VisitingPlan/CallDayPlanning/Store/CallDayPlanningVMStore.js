function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "userStore",
        model: "userEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                ClientStructureID: "",
                ClientPositionID:"",
                ClientUserID:"",
                CallDate:""
            },
            actionMethods: { read: 'POST' }
        }
    });
}