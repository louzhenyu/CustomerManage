function InitVE() {
    Ext.define("storeEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "MappingID",
            type: "string"
        }, {
            name: "DistributorID", //grid里的storeid
            type: "string"
        }, {
            name: "MapDistributorID", //地图里的storeid
            type: "int"
        }, {
            name: "Distributor",
            type: "string"
        }, {
            name: "Sequence",
            type: "int"
        }, {
            name: "Coordinate",
            type: "string"
        }],
        idProperty: 'DistributorID'
    });
}