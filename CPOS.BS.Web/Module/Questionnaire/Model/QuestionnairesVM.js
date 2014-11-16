function InitVE() {
    Ext.define("QuestionnairesViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'QuestionnaireID', type: 'string' },
            { name: 'QuestionnaireName', type: 'string' },
            { name: 'QuestionnaireDesc', type: 'string' },
            { name: 'DisplayIndex', type: 'string' },
            { name: 'QuestionCount', type: 'string' },
            { name: 'CreateTime', type: 'string' },
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'IsDelete', type: 'string' }
        ]
    });
}