function InitStore() {

    //gridview
    new Ext.data.Store({
        storeId: "brandStore",
        pageSize: 15,
        model: "BrandEntity",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'
            }
        }
    });

}