new Ext.data.Store({
    storeId: "houseTypeStore",
    proxy: {
        type: 'ajax',
        reader: {
            type: 'json'
        }
    }, fields: [{ name: 'item_name', type: "string" }, { name: 'sku_id', type: "string"}]
});

new Ext.data.Store({
    storeId: "unitStore",
    proxy: {
        type: 'ajax',
        reader: {
            type: 'json'
        }
    }, fields: [{ name: 'unit_id', type: "string" }, { name: 'unit_name', type: "string"}]
});

new Ext.create('Ext.data.Store', {
    storeId: "weekStore",
    fields: [{ name: 'name', type: "string" }, { name: 'value', type: "int"}],
    data: [
        { "name": "星期日", "value": 0 },
        { "name": "星期一", "value": 1 },
        { "name": "星期二", "value": 2 },
        { "name": "星期三", "value": 3 },
        { "name": "星期四", "value": 4 },
        { "name": "星期五", "value": 5 },
        { "name": "星期六", "value": 6 }
    ]
});