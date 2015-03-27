function InitVE() {
    Ext.define("UserViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'User_Id', type: 'string' }, 
            { name: 'User_Code', type: 'string' }, 
            { name: 'User_Name', type: 'string' }, 
            { name: 'User_Name_En', type: 'string' }, 
            { name: 'User_Email', type: 'string' }, 
            { name: 'User_Cellphone', type: 'string' }, 
            { name: 'User_Tel', type: 'string' }, 
            { name: 'User_Status', type: 'string' }, 
            { name: 'User_Status_Desc', type: 'string' }, 
            { name: 'User_Gender', type: 'string' }, 
            { name: 'User_Birthday', type: 'string' }, 
            { name: 'User_Identity', type: 'string' }, 
            { name: 'User_Address', type: 'string' }, 
            { name: 'User_Postcode', type: 'string' }, 
            { name: 'QQ', type: 'string' }, 
            { name: 'MSN', type: 'string' }, 
            { name: 'Blog', type: 'string' }, 
            { name: 'User_Remark', type: 'string' },
            { name: "Create_User_Id", type: 'string' }, 
            { name: "Create_User_Name", type: 'string' }, 
            { name: "Create_Time", type: 'string' }, 
            { name: "Modify_User_Id", type: 'string' }, 
            { name: "Modify_User_Name", type: 'string' },
        { name: "User_Telephone", type: 'string' },
        { name: "user_genderText", type: 'string' },
         { name: "UnitName", type: 'string' },//所属门店
        
            { name: "Modify_Time", type: 'string' }
            ]
    });


}