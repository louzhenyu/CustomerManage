function InitVE() {
    Ext.define("VisitingTaskDataViewEntity", {
        extend: "Ext.data.Model",
        fields: [ {
            name: "VisitingTaskDataID",
            type: "string"
        }, {
            name: "ClientUserName",
            type: "string"
        }, {
            name: "ClientUserID",
            type: "string"
        }, {
            name: "POPID",
            type: "string"
        }, {
            name: "POPType",
            type: "int"
        }, {
            name: "POPName",
            type: "string"
        }, {
            name: "POPCoordinate",
            type: "string"
        }, {
            name: "ExecutionTime",
            type: "datetime",
            dataFormat: "c"
        }, {
            name: "InTime",
            type: "datetime",
            dataFormat: "c"
        }, {
            name: "OutTime",
            type: "datetime",
            dataFormat: "c"
        }, {
            name: "WorkingHoursIndoor",
            type: "string"
        }, {
            name: "WorkingHoursJourneyTime",
            type: "string"
        }, {
            name: "WorkingHoursTotal",
            type: "string"
        }, {
            name: "InCoordinate",
            type: "string"
        }, {
            name: "OutCoordinate",
            type: "string"
        }, {
            name: "EffectiveVisit",
            type: "int"
        }],
        idProperty: 'POPID'
    });
}