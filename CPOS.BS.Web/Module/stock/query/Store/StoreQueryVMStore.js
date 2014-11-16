/*Jermyn 2013-04-02*/
function InitStore() {
    Ext.create('Ext.data.Store', {
        storeId: "storeQueryStore",
        model: "StockBalanceEntity",
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