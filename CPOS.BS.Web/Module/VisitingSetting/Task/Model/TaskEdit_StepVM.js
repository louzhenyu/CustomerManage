function InitVE() {
    Ext.define("VisitingTaskStepViewEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "VisitingTaskStepID",
            type: "string"
        }, {
            name: "VisitingTaskID",
            type: "string"
        }, {
            name: "StepNo",
            type: "string"
        }, {
            name: "StepName",
            type: "string"
        }, {
            name: "StepType",
            type: "int"
        }, {
            name: "IsMustDo",
            type: "int"
        }, {
            name: "StepPriority",
            type: "int"
        }, {
            name: "IsOnePage",
            type: "int"
        }, {
            name: "Remark",
            type: "string"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }, {
            name: "CreateBy",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }, {
            name: "StepTypeText",
            type: "string"
        }, {
            name: "VisitingTaskName",
            type: "string"
        }]
    });

    Ext.define("VisitingTaskStepObjectEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "ObjectID",
            type: "string"
        }, {
            name: "VisitingTaskStepID",
            type: "string"
        }, {
            name: "Target1ID",
            type: "string"
        }, {
            name: "Target2ID",
            type: "string"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }, {
            name: "CreateBy",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }]
    });

    Ext.define("VisitingTaskParameterMappingEntity", {
        extend: "Ext.data.Model",
        fields: [{
            name: "MappingID",
            type: "string"
        }, {
            name: "VisitingTaskStepID",
            type: "string"
        }, {
            name: "VisitingParameterID",
            type: "string"
        }, {
            name: "ParameterOrder",
            type: "int"
        }, {
            name: "ClientID",
            type: "int"
        }, {
            name: "ClientDistributorID",
            type: "int"
        }, {
            name: "CreateBy",
            type: "int"
        }, {
            name: "CreateTime",
            type: "datetime"
        }]
    });

}