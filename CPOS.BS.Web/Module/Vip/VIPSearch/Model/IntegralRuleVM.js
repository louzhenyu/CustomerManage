function InitVE() {

    Ext.define("IntegralRuleViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'IntegralRuleID', type: 'string' }, 
            { name: 'IntegralSourceID', type: 'string' }, 
            { name: 'Integral', type: 'string' }, 
            { name: 'IntegralDesc', type: 'string' }, 
            { name: 'IntegralSourceName', type: 'string' }, 
            { name: 'BeginDate', type: 'string' }, 
            { name: 'EndDate', type: 'string' }, 
            { name: 'CreateTime', type: 'string' }, 
            { name: 'CreateBy', type: 'string' },
            { name: 'LastUpdateTime', type: 'string' },
            { name: 'TypeCodeDesc', type: 'string' }, 
            { name: 'LastUpdateBy', type: 'string' }
            ]
    });
    
}