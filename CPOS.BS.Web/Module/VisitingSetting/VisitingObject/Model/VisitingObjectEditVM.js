function InitVE() {
    Ext.define("VisitingObjectEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingObjectID",
            type: "string"
        }, {
            name: "ObjectName",
            type: "string"
        }, {
            name: "Status",
            type: "int"
        }, {
            name: "ObjectGroup",
            type: "string"
        }, {
            name: "Sequence",
            type: "int"
        }, {
            name: "ParentID",
            type: "string"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "LayoutType",
            type: "string"
        }]
    });
    
}