function InitVE() {
    Ext.define("EventStatsEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "EventStatsID",
            type: "string"
        }, {
            name: "ObjectType",
            type: "string"
        }, {
            name: "Type_Name",
            type: "string"
        }, {
            name: "Title",
            type: "string"
        }, {
            name: "BrowseNum",
            type: "int"
        }, {
            name: "PraiseNum",
            type: "int"
        }, {
            name: "BookMarkNum",
            type: "int"
        }, {
            name: "ShareNum",
            type: "int"
        }, {
            name: "Sequence",
            type: "int"
        }]
    });
    Ext.define("TitleEntit", {
        extend: "Ext.data.Model",
        url: JITPage.HandlerUrl.getValue() + "&method=GetOptionID&objecttype=1",
        fields: [{
            name: "NewsID",
            type: "string"
        }, {
            name: "Title",
            type: "string"
        }]
    })
}