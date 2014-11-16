function InitVE() {

    Ext.define("ClientPositionEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "ClientPositionID",
            type: "int"
        }, {
            name: "PositionNo",
            type: "string"
        }, {
            name: "PositionName",
            type: "string"
        }, {
            name: "PositionLevel",
            type: "int"
        }, {
            name: "ParentID",
            type: "int"
        }, {
            name: "IsLeaf",
            type: "int"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }, {
            name: "CreateBy",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }, {
            name: "LastUpdateBy",
            type: "int"
        }, {
            name: "LastUpdateTime",
            type: "datetime"
        }, {
            name: "ObjectID",
            type: "string"
        }, {
            name: "Target1ID",
            type: "int"
        }]
    });

}