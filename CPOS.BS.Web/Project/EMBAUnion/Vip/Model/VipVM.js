function InitVE() {
    Ext.define("DefindControlEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "ControlType",
            type: "int"
        }, {
            name: "ControlName",
            type: "string"
        }, {
            name: "ControlValue",
            type: "string"
        }]
    });
}