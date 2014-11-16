function InitVE() {

    Ext.define("userEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "ClientUserID",
            type: "int"
        }, {
            name: "UserName",
            type: "string"
        }, {
            name: "StructureName",
            type: "string"
        }, {
            name: "POPCount",
            type: "int"
        }, {
            name: "CallDate",
            type: "datetime"
        }, {
            name:"PositionName",
            type:"string"
        }]
    });
}