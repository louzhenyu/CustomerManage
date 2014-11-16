function InitStore() {

    new Ext.create('Ext.data.Store', {
        storeId: "serchCityStore",
        fields: [{ name: 'typename', type: "string" }, { name: 'typevalue', type: "string"}],
        data: [
            { "typename": "隐藏", "typevalue": "0" },
            { "typename": "显示", "typevalue": "1" }
        ]
    });
    new Ext.create('Ext.data.Store', {
        storeId: "isAll",
        fields: [{ name: 'typename', type: "string" }, { name: 'typevalue', type: "string"}],
        data: [
            { "typename": "否", "typevalue": "0" },
            { "typename": "是", "typevalue": "1" }
        ]
    });

    new Ext.data.Store({
        storeId: "customeTypeStore",
        model: "customeTypeEntit",
        proxy: {
            type: 'ajax',
            reader: {
                type: 'json'

            }
        }
    });


}