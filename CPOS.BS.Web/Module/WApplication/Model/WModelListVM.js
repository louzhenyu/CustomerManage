function InitVE() {
    Ext.define("WModelListViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "ModelId", type: 'string' }, 
            { name: "ModelCode", type: 'string' }, 
            { name: "ModelName", type: 'string' }, 
            { name: "CustomerId", type: 'string' }, 
            { name: "MaterialTypeId", type: 'string' }, 
            { name: "MaterialId", type: 'string' }, 
            { name: "ApplicationId", type: 'string' }, 
            { name: "CreateTime", type: 'string' }, 
            { name: "CreateBy", type: 'string' }, 
            { name: "CreateByName", type: 'string' }, 
            { name: "LastUpdateBy", type: 'string' }, 
            { name: "LastUpdateTime", type: 'string' }, 
            { name: "IsDelete", type: 'string' }
            ]
    });

}