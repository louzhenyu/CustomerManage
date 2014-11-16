function InitVE() {

    Ext.define("BrandViewEntity", {
        extend: "Ext.data.Model",
        fields: [ {
            name: "BrandNo",
            type: "string"
        }, {
            name: "BrandName",
            type: "string"
        }, {
            name: "IsOwner",
            type: "int"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "ParentBrandName",
            type: "string"
        }, {
            name: "CategoryID",
            type: "int"
        }, {
            name: "Firm",
            type: "string"
        }, {
            name: "ObjectID",
            type: "string"
        }, {
            name: "Target1ID",
            type: "string"
        }, {
            name: "Level",
            type: "string"
        }, {
            name: "CategoryName",
            type: "string"
        }]
    });

}