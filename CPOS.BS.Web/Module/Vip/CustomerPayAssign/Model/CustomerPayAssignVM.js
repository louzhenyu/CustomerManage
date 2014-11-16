function InitVE() {
    Ext.define("CustomerPayAssignViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "AssignId", type: 'string' }, 
            { name: "CustomerId", type: 'string' }, 
            { name: "PaymentTypeId", type: 'string' }, 
            { name: "PaymentTypeName", type: 'string' }, 
            { name: "CustomerAccountNumber", type: 'string' }, 
            { name: "CustomerProportion", type: 'string' }, 
            { name: "JITProportion", type: 'string' }, 
            { name: "Remark", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

}