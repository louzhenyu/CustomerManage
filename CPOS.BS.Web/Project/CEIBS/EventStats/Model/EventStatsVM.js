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
            type: "string"
        }, {
            name: "PraiseNum",
            type: "string"
        }, {
            name: "BookMarkNum",
            type: "string"
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