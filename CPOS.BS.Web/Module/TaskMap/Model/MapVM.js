function InitVE() {
    Ext.define("MapAnalysisEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingTaskID",
            type: "string"
        }, {
            name: "VisitingTaskName",
            type: "string"
        }]
    });
}