function InitVE() {
    Ext.define("EnterpriseMemberStructureEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "EnterpriseMemberStructureID",
            type: "string"
        }, {
            name: "EnterpriseMemberID",
            type: "string"
        }, {
            name: "MemberTitle",
            type: "string"
        }, {
            name: "ParentID",
            type: "string"
        }, {
            name: "StructureTitle",
            type: "string"
        }, {
            name: "ParentTitle",
            type: "string"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "ClientID",
            type: "string"
        }, {
            name: "CreateBy",
            type: "string"
        }, {
            name: "CreateTime",
            type: "datetime"
        }, {
            name: "LastUpdateBy",
            type: "string"
        }, {
            name: "LastUpdateTime",
            type: "datetime"
        }, {
            name: "IsDelete",
            type: "int"
        }]
    });
}