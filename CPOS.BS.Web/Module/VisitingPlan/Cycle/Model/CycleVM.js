function InitVE() {
    Ext.define("CycleEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "CycleID",
            type: "string"
        }, {
            name: "CycleName",
            type: "string"
        }, {
            name: "CycleType",
            type: "int"
        }, {
            name: "CycleDay",
            type: "int"
        }, {
            name: "StartDate",
            type: "datetime"
        }, {
            name: "EndDate",
            type: "datetime"
        }, {
            name: "Remark",
            type: "string"
        }]
    });
}