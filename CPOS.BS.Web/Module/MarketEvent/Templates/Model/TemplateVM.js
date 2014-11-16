function InitVE() {
    Ext.define("MarketTemplateEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'TemplateID', type: 'string' }
            , { name: 'TemplateType', type: 'string' }
            , { name: 'TemplateContent', type: 'string' }
            , { name: 'CreateTime', type: 'string' }
            , { name: 'CreateBy', type: 'string' }
            , { name: 'LastUpdateBy', type: 'string' }
            , { name: 'LastUpdateTime', type: 'string' }
            , { name: 'TemplateDesc', type: 'string' }
            ]
    });

}