function InitVE() {
    Ext.define("RoleViewEntity", {
        extend: "Ext.data.Model",
        fields: [
            { name: 'Role_Id', type: 'string' }, 
            { name: 'Role_Code', type: 'string' }, 
            { name: 'Role_Name', type: 'string' }, 
            { name: 'Role_Eng_Name', type: 'string' }, 
            { name: 'Is_Sys', type: 'string' }, 
            { name: 'Role_Email', type: 'string' }, 
            { name: 'Role_Cellphone', type: 'string' }, 
            { name: 'Role_Tel', type: 'string' }, 
            { name: 'Role_Status', type: 'string' }, 
            { name: 'Role_Status_Desc', type: 'string' }, 
            { name: 'Role_Remark', type: 'string' },
            { name: 'Def_App_Id', type: 'string' }, 
            { name: 'Def_App_Name', type: 'string' }, 
            { name: "Create_User_Id", type: 'string' }, 
            { name: "Create_User_Name", type: 'string' }, 
            { name: "Create_Time", type: 'string' }, 
            { name: "Modify_User_Id", type: 'string' }, 
            { name: "Modify_User_Name", type: 'string' }, 
            { name: "Modify_Time", type: 'string' }
            ]
    });

}