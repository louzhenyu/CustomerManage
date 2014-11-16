function InitVE() {
    Ext.define("OptionViewEntity", {
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
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'CookieName', type: 'string' }
        ]
    });

}