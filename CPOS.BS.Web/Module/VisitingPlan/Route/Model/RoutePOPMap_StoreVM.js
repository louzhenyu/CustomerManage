function InitVE() {
    Ext.define("storeEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "MappingID",
            type: "string"
        }, {
            name: "StoreID", //grid里的storeid
            type: "string"
        }, {
            name: "MapStoreID", //地图里的storeid
            type: "int"
        }, {
            name: "StoreName",
            type: "string"
        }, {
            name: "Sequence",
            type: "int"
        }, {
            name: "Coordinate",
            type: "string"
        }],
        idProperty: 'StoreID'
    });
}