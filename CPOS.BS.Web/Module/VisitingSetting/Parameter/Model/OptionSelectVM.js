function InitOptionSelectVE() {
    Ext.define("OptionsEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingParameterOptionsID",
            type: "string"
        }, {
            name: "OptionName",
            type: "string"
        }, {
            name: "OptionValue",
            type: "int"
        }, {
            name: "OptionText",
            type: "string"
        }, {
            name: "OptionTextEn",
            type: "string"
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
            name: "OptionCount",
            type: "int"
        }]
    });

}