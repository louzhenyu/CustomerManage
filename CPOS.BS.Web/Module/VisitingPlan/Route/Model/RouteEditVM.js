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
        }, {
            name: "CycleID",
            type: "string"
        }, {
            name: "CycleDetailID",
            type: "string"
        }]
    });

    Ext.define("CycleEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "CycleID",
            type: "string"
        }, {
            name: "CycleName",
            type: "string"
        }, {
            name: "CycleType",
            type: "int"
        }, {
            name: "CycleDay",
            type: "int"
        }, {
            name: "StartDate",
            type: "datetime"
        }, {
            name: "EndDate",
            type: "datetime"
        }, {
            name: "Remark",
            type: "string"
        }]
    });

    Ext.define("CycleDetailEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "CycleDetailID",
            type: "string"
        }, {
            name: "CycleID",
            type: "string"
        }, {
            name: "DayOfCycle",
            type: "string"
        }]
    });
}