function InitVE() {
    Ext.define("ItemViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: "Item_Id", type: 'string' },
            { name: "Item_Category_Id", type: 'string' },
            { name: "Item_Category_Name", type: 'string' },
            { name: "Item_Code", type: 'string' },
            { name: "Item_Name", type: 'string' },
            { name: "Item_Name_En", type: 'string' },
            { name: "Item_Name_Short", type: 'string' },
            { name: "Status", type: 'string' },
            { name: "Status_Desc", type: 'string' },
            { name: "Item_Remark", type: 'string' },
            { name: "Pyzjm", type: 'string' },
            { name: "Create_User_Id", type: 'string' },
            { name: "Create_Time", type: 'string' },
            { name: "Modify_User_Id", type: 'string' },
            { name: "Modify_Time", type: 'string' },
            { name: "ifgifts", type: 'string' },
            { name: "ifoften", type: 'string' },
            { name: "ifservice", type: 'string' },
            { name: "isGB", type: 'string' },
            { name: "display_index", type: 'string' },
            { name: "Image_Url", type: 'string' },
            { name: "IsPause", type: 'string' },
            { name: "IsItemCategory",type:'string' }
            ]
    });

}