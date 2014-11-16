function InitVE() {
    Ext.define("QuestionViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'QuestionID', type: 'string' },
            { name: 'QuestionnaireID', type: 'string' },
            { name: 'QuestionType', type: 'string' },
            { name: 'QuestionTypeDesc', type: 'string' },
            { name: 'QuestionDesc', type: 'string' },
            { name: 'QuestionValue', type: 'string' },
            { name: 'MinSelected', type: 'string' },
            { name: 'MaxSelected', type: 'string' },
            { name: 'QuestionValueCount', type: 'string' },
            { name: 'OptionsCount', type: 'string' },
            { name: 'IsRequired', type: 'string' },
            { name: 'IsOpen', type: 'string' },
            { name: 'IsSaveOutEvent', type: 'string' },
            { name: 'DisplayIndex', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'CookieName', type: 'string' },
            { name: 'DisplayIndexNo', type: 'string' }
        ]
    });

    
    Ext.define("QuesOptionViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'OptionsID', type: 'string' },
            { name: 'QuestionID', type: 'string' },
            { name: 'OptionsText', type: 'string' },
            { name: 'IsSelect', type: 'string' },
            { name: 'DisplayIndex', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' }
        ]
    });

}