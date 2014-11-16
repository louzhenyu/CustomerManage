function InitVE() {
    Ext.define("WQRCodeManagerViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'QRCodeId', type: 'string' }, 
            { name: 'QRCode', type: 'string' }, 
            { name: 'QRCodeTypeId', type: 'string' }, 
            { name: 'QRCodeTypeName', type: 'string' }, 
            { name: 'IsUse', type: 'string' }, 
            { name: 'ObjectId', type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "ModifyTime", type: 'string' }
            ]
    });

}