function InitVE() {
    Ext.define("TaskViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingTaskID",
            type: "string"
        }, {
            name: "VisitingTaskName",
            type: "string"
        }]
    });

    Ext.define("StepViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingTaskStepID",
            type: "string"
        }, {
            name: "VisitingTaskID",
            type: "string"
        }, {
            name: "StepName",
            type: "string"
        }]
    });
}