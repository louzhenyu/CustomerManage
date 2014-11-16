function InitVE() {

    Ext.define("CategoryViewEntity", {
        extend: "Ext.data.Model",
        fields: [ {
            name: "CategoryName",
            type: "string"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "ParentCategoryName",
            type: "string"
        }, {
            name: "CategoryID",
            type: "int"
        }, {
            name: "BrandName",
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
            name: "Firm",
            type: "string"
        }]
    });

}