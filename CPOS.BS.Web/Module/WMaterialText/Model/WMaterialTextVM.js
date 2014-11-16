function InitVE() {
    Ext.define("WMaterialTextEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "TextId",
            type: "string"
        }, {
            name: "ParentTextId",
            type: "string"
        }, {
            name: "Title",
            type: "string"
        }, {
            name: "Author",
            type: "string"
        }, {
            name: "CoverImageUrl",
            type: "string"
        }, {
            name: "Text",
            type: "string"
        }, {
            name: "OriginalUrl",
            type: "string"
        }, {
            name: "DisplayIndex",
            type: "int"
        }, {
            name: "ApplicationId",
            type: "string"
        }, {
            name: "TypeId",
            type: "string"
        }, {
            name: "TypeName",
            type: "string"
        }, {
            name: "CreateTime",
            type: "datatime"
        }, {
            name: "Author",
            type: "string"
        }, {
            name: "LastUpdateBy",
            type: "string"
        }, {
            name: "LastUpdateTime",
            type: "datatime"
        }]
    });

    Ext.define("WMaterialTextTypeEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "ModelId",
            type: "string"
        }, {
            name: "ModelName",
            type: "string"
        }
        ]
    });
}