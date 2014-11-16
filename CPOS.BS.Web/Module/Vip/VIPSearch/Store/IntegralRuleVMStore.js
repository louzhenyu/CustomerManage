function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "IntegralRuleStore",
        model: "IntegralRuleViewEntity",
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