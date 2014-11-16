function InitVE() {
    Ext.define("OptionsEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "OptionsID",
            type: "int"
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

    Ext.define("OptionsDefinedEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "DefinedID",
            type: "int"
        }, {
            name: "OptionName",
            type: "string"
        }, {
            name: "Title",
            type: "string"
        }, {
            name: "GourpID",
            type: "int"
        }, {
            name: "OptionType",
            type: "int"
        }]
    });

}