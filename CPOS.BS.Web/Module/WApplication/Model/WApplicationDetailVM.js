function InitVE() {
    Ext.define("EncryptModel", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' }
        ]
    });
}