function InitStore() {
    new Ext.data.Store({
        storeId: "TransferStore",
        model: "TransferEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "Data.CustomerWithdrawalList",
                totalProperty: "Data.TotalCount"
            },
            extraParams: {
                form: ""
            },
            actionMethods: { read: 'POST' }
        }
    });
}