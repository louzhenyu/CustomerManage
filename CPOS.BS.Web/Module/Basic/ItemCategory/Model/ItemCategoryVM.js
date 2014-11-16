function InitVE() {
    Ext.define("ItemCategoryViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Item_Category_Id', type: 'string' }, 
            { name: 'Item_Category_Code', type: 'string' }, 
            { name: 'Item_Category_Name', type: 'string' },
            { name: 'Pyzjm', type: 'string' },
            { name: 'Parent_Id', type: 'string' }, 
            { name: 'Parent_Name', type: 'string' }, 
            { name: 'Status', type: 'string' }, 
            { name: 'Status_desc', type: 'string' }, 
            { name: 'Item_Category_Remark', type: 'string' }, 
            { name: 'Create_User_Id', type: 'string' }, 
            { name: 'Create_Time', type: 'string' }, 
            { name: 'Modify_User_Id', type: 'string' },
            { name: 'Modify_Time', type: 'string' },
            { name: 'DisplayIndex', type: 'string' }
            ]
    });

}