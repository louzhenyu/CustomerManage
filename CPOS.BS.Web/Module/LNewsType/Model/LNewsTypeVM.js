function InitVE() {
    Ext.define("LNewsTypeEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "NewsTypeId",
            type: "string"
        }, {
            name: "NewsTypeName",
            type: "string"
        }, {
            name: "TypeLevel",
            type: "string"
        }, {
            name: "ParentTypeName",
            type: "string"
        },
        {
            name: "CreateTime",
            type: "string"
        }]
    });
    Ext.define("ParentTypeEntit", {
        extend: "Ext.data.Model",
       // url: JITPage.HandlerUrl.getValue() + "&method=GetPartentNewsType",//这里是通过url添加值吗？
        fields: [{
            name: "NewsTypeId",
            type: "string"
        }, {
            name: "NewsTypeName",
            type: "string"
        }]
    })
}