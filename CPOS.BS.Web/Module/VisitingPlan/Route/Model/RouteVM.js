function InitVE() {
    Ext.define("RouteViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "RouteID",
            type: "string"
        }, {
            name: "RouteNo",
            type: "string"
        }, {
            name: "RouteName",
            type: "string"
        }, {
            name: "Status",
            type: "int"
        }, {
            name: "StartDate",
            type: "datetime"
        }, {
            name: "EndDate",
            type: "datetime"
        }, {
            name: "POPType",
            type: "int"
        }, {
            name: "Distance",
            type: "string"
        }, {
            name: "TripMode",
            type: "int"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "ClientUserID",
            type: "int"
        }, {
            name: "UserName",
            type: "string"
        }, {
            name: "PositionName",
            type: "string"
        }]
    });

}