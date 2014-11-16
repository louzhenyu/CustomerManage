function InitStore() {
    new Ext.data.Store({
        storeId: "EnterpriseMemberStructureEditStore",
        model: "EnterpriseMemberStructureEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: "",
                ClientPositionID: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}