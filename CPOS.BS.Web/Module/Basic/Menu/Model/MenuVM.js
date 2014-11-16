function InitVE() {
    Ext.define("MenuViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Menu_Id', type: 'string' }, 
            { name: 'Reg_App_Id', type: 'string' }, 
            { name: 'Reg_App_Name', type: 'string' }, 
            { name: 'Menu_Code', type: 'string' }, 
            { name: 'Parent_Menu_Id', type: 'string' }, 
            { name: 'Menu_Level', type: 'string' }, 
            { name: 'Url_Path', type: 'string' }, 
            { name: 'Icon_Path', type: 'string' }, 
            { name: 'Display_Index', type: 'string' }, 
            { name: 'Menu_Name', type: 'string' }, 
            { name: 'User_Flag', type: 'string' }, 
            { name: 'Menu_Eng_Name', type: 'string' },
            { name: 'Status', type: 'string' }, 
            { name: "Create_User_Id", type: 'string' }, 
            { name: "Create_User_Name", type: 'string' }, 
            { name: "Create_Time", type: 'string' }, 
            { name: "Modify_User_Id", type: 'string' }, 
            { name: "Modify_User_Name", type: 'string' }, 
            { name: "Modify_Time", type: 'string' }
            ]
    });

}