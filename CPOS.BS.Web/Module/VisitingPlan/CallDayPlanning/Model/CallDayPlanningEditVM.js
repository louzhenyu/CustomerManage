function InitCDPEditVE() {
    Ext.define("CallDayPlanningEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "PlanningID",
            type: "string"
        }, {
            name: "CallDate",
            type: "datetime"
        }, {
            name: "POPID",
            type: "string"
        }, {
            name: "POPType",
            type: "int"
        }, {
            name: "ClientUserID",
            type: "int"
        }, {
            name: "Sequence",
            type: "int"
        }, {
            name: "PlanningType",
            type: "int"
        }, {
            name: "RouteID",
            type: "string"
        }, {
            name: "CallStatus",
            type: "int"
        }, {
            name: "Remark",
            type: "string"
        }]
    });
}