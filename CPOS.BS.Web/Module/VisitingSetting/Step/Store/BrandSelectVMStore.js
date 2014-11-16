function InitStore() {

    new Ext.data.Store({
        storeId: "brandStore",
        model: "BrandViewEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json',
                root: "topics",
                totalProperty: "totalCount"
            },
            extraParams: {
                form: "",
                id: ""
            },
            actionMethods: { read: 'POST' }
        }
    });


    new Ext.create('Ext.data.Store', {
        storeId: "typeStore",
        fields: [{ name: 'name', type: "string" }, { name: 'value', type: "string"}],
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });
}