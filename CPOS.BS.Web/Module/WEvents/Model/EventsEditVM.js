function InitVE() {
    Ext.define("DrawMethodEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'DrawMethodId', type: 'int' },
            { name: 'DrawMethodName', type: 'string' }
        ]
    });

    Ext.define("PersonCountEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });
    Ext.define("ModelEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });
    Ext.define("FlagEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });


}