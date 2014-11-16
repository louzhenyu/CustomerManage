function InitVE() {
    Ext.define("EnterpriseMemberEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "EnterpriseMemberID",
            type: "string"
        }, {
            name: "MemberName",
            type: "string"
        }, {
            name: "MemberNameEn",
            type: "string"
        }, {
            name: "Address",
            type: "string"
        }, {
            name: "CityCode",
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