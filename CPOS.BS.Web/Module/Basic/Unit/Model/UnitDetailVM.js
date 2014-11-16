function InitVE() {
//    Ext.define("UnitDetailViewEntity", {
//        extend: "Ext.data.Model",
//        fields: [
//            { name: 'Id', type: 'string' }, 
//            { name: 'parent_Unit_Name', type: 'string' }, 
//            { name: 'parent_Unit_Id', type: 'string' }, 
//            { name: 'name', type: 'string' }, 
//            { name: 'UnitId', type: 'string' }, 
//            { name: 'UnitName', type: 'string' }, 
//            { name: 'DefaultFlag', type: 'string' }, 
//            { name: 'DefaultFlagDesc', type: 'string' }
//            ]
//    });

    Ext.define("ItemImageViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'ImageId', type: 'string' },
            { name: 'ObjectId', type: 'string' },
            { name: 'ImageURL', type: 'string' },
            { name: 'DisplayIndex', type: 'string' }
            ]
    });
    
    Ext.define("ItemBrandViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'MappingId', type: 'string' },
            { name: 'StoreId', type: 'string' },
            { name: 'BrandId', type: 'string' },
            { name: 'BrandName', type: 'string' }
            ]
    });
}