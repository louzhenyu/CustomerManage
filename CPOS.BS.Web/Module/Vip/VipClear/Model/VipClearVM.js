function InitVE() {
    Ext.define("VIPClearEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VIPClearID",
            type: "int"
        }, {
            name: "VIPClearListID",
            type: "int"
        }, {
            name: "VipID",
            type: "string"
        }, {
            name: "UpdateVipID",
            type: "string"
        }, {
            name: "VIPFieldName",
            type: "string"
        }, {
            name: "DuplicateGroup",
            type: "string"
        }, {
            name: "DrawbackNum",
            type: "int"
        }, {
            name: "DuplicateNum",
            type: "int"
        }, {
            name: "InvalidNum",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }]
    });
}