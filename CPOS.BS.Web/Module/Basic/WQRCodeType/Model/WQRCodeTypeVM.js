function InitVE() {
    Ext.define("WQRCodeTypeViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'QRCodeTypeId', type: 'string' }, 
            { name: 'TypeCode', type: 'string' }, 
            { name: 'TypeName', type: 'string' }, 
            { name: 'Operating', type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "ModifyTime", type: 'string' }
            ]
    });

}