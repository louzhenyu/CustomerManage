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
            type: "int"
        }, {
            name: "Sequence",
            type: "int"
        }, {
            name: "ParentID",
            type: "int"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "ObjectGroupText",
            type: "string"
        }]
    });
}