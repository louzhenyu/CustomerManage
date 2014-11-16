function InitVE() {
    Ext.define("AttachmentViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
                name: "AttachmentID",
                type: "string"
            }, {
                name: "AttachmentTitle",
                type: "string"
            }, {
                name: "FileName",
                type: "string"
            }, {
                name: "Path",
                type: "string"
            }, {
                name: "AttachmentSize",
                type: "string"
            }, {
                name: "ClientID",
                type: "string"
            }, {
                name: "CreateTime",
                type: "datetime"
            }, {
                name: "CreateBy",
                type: "string"
            }, {
                name: "LastUpdateTime",
                type: "datetime"
            }, {
                name: "LastUpdateBy",
                type: "string"
            }, {
                name: "IsDelete",
                type: "int"
            }]
    });
}